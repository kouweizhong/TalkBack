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
using System.Linq;
using TalkBack.Brokers;

namespace TalkBack
{
  public static class TalkBackChannel
  {
    private static readonly IMessageParticipiantContainer<IMessageSender> MessageParticipiantContainer = new ReflectionMessageParticipiantContainer<IMessageSender> ();
    
    public static string[] Initialize(string[] args)
    {
      _out = BuildMessageSink(args.First());

      AppDomain.CurrentDomain.ProcessExit += (sender, eventArgs) => ((MessageParticipiant) _out).Dispose ();

      return args.Skip(1).ToArray();
    }
    
    private static IMessageSender BuildMessageSink(string talkBackConfig)
    {
      if (!talkBackConfig.Contains("="))
        throw new ArgumentException(string.Format("Invalid TalkBack configuration '{0}'", talkBackConfig), "talkBackConfig");

      var split = talkBackConfig.Split('=');

      return MessageParticipiantContainer.GetMessageParticipiant(split[0], split[1]);
    }

    private static IMessageSender _out;
    public static IMessageSender Out
    {
      get
      {
        if (_out == null)
          throw new InvalidOperationException("TalkBack was not initialized!");
        return _out;
      }
    }
  }
}
