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
using System.Text.RegularExpressions;
using TalkBack.Brokers;

namespace TalkBack
{
  public static class TalkBackChannel
  {
    private static readonly IMessageParticipiantContainer<IMessageSender> MessageParticipiantContainer = new ReflectionMessageParticipiantContainer<IMessageSender> ();

    private static readonly Regex s_cliPattern = new Regex(string.Format("^{0}(.+?)=(.+)$", Regex.Escape(TalkBackInvoke.Prefix)));
    
    public static string[] Initialize(string[] args)
    {
      Out = BuildMessageSink(args.First());

      if (Out == null)
        return args;

      AppDomain.CurrentDomain.ProcessExit += (sender, eventArgs) => ((MessageParticipiant) Out).Dispose ();

      return args.Skip(1).ToArray();
    }
    
    private static IMessageSender BuildMessageSink(string talkBackConfig)
    {
      var match = s_cliPattern.Match(talkBackConfig);

      if(!match.Success)
      {
        if (talkBackConfig.StartsWith(TalkBackInvoke.Prefix))
          throw new ArgumentException(string.Format("Invalid TalkBack configuration '{0}'", talkBackConfig), "talkBackConfig");
        else
          return null;
      }

      return MessageParticipiantContainer.GetMessageParticipiant(match.Groups[1].Value, match.Groups[2].Value);
    }

    public static IMessageSender Out { get; private set; }
  }
}
