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
    private static readonly IMessageParticipiantContainer<IMessageReceiver> ReceiverContainer = new ReflectionMessageParticipiantContainer<IMessageReceiver>();

    public static void Process(string executablePath, string arguments, Action<Message> callback)
    {
      string identifier = TalkBackConfiguration.Configuration.Identifier;
      string config = TalkBackConfiguration.Configuration.Options;

      var process = new Process
                      {
                        StartInfo =
                          {
                            FileName = executablePath,
                            Arguments = BuildArguments(arguments, identifier, config),
                            UseShellExecute = false,
                            CreateNoWindow = true
                          }
                      };

      process.Start();
      process.WaitForExit();

      var receiver = ReceiverContainer.GetMessageParticipiant (identifier, config);
      receiver.SetCallback (callback);
      receiver.ProcessMessages();
    }

    private static string BuildArguments(string arguments, string identifier, string config)
    {
      return string.Format("{0}={1} {2}", identifier, config, arguments);
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