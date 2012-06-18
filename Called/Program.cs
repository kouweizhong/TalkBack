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
      }
      else
      {
        Console.WriteLine("Talkback was not requested");
      }
    }
  }
}
