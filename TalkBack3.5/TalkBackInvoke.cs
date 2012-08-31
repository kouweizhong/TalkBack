// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Diagnostics;
using TalkBack.Brokers;
using TalkBack.Brokers.Delegate;
using TalkBack.Configuration;

namespace TalkBack
{
  public static class TalkBackInvoke
  {
    internal const string Prefix = "__tb:";

    private static readonly IMessageParticipiantContainer<IMessageReceiver> ReceiverContainer = new ReflectionMessageParticipiantContainer<IMessageReceiver>();

    public static int Process(string executablePath, string arguments, Action<Message> callback)
    {
      string identifier = TalkBackConfiguration.Configuration.Identifier;
      string config = TalkBackConfiguration.Configuration.Options;

      var receiver = ReceiverContainer.GetMessageParticipiant (identifier, config);
      receiver.SetCallback (callback);

      var process = new Process
                      {
                        StartInfo =
                          {
                            FileName = executablePath,
                            Arguments = BuildArguments(arguments, identifier, receiver.BuildSenderConfig()),
                            UseShellExecute = false,
                            CreateNoWindow = true,
#if DEBUG
                            RedirectStandardOutput = true,
                            RedirectStandardError = true
#endif
                          }
                      };

      receiver.OnStartSender();
      process.Start();
      process.WaitForExit();
#if DEBUG
      Console.WriteLine(process.StandardOutput.ReadToEnd());
      Console.Error.WriteLine(process.StandardOutput.ReadToEnd());
#endif

      receiver.OnEndSender();
      return process.ExitCode;
    }

    private static string BuildArguments(string arguments, string identifier, string config)
    {
      return string.Format("{3}{0}-{1} {2}", identifier, config, arguments, Prefix);
    }

    public static void Action (Action<IMessageSender> action, Action<Message> callback)
    {
      var broker = new DelegateMessageBroker(callback);
      action(broker);
    } 

    public static T Action<T> (Func<IMessageSender, T> action, Action<Message> callback)
    {
      var broker = new DelegateMessageBroker(callback);
      return action(broker);
    } 
  }
}