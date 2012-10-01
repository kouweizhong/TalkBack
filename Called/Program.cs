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
using TalkBack;

namespace Called
{
  static class Program
  {
    static void Main (string[] args)
    {
      Console.WriteLine(args.Aggregate((a, b) => a + " " + b));

      args = TalkBackChannel.Initialize(args);

      if (TalkBackChannel.Out != null)
      {
        TalkBackChannel.Out.SendInfo ("INFO");
        TalkBackChannel.Out.SendError ("ERROR");
        TalkBackChannel.Out.SendDebug ("DEBUG");
        TalkBackChannel.Out.SendWarning ("WARNING");
        TalkBackChannel.Out.SendMessage (new Message (MessageSeverity.Info, "ARGS " + string.Join (" ", args)));
        TalkBackChannel.Out.SendWarning("L{0}NG", new string('O', 50000));
      }
      else
      {
        Console.WriteLine("Talkback was not requested");
      }
    }
  }
}
