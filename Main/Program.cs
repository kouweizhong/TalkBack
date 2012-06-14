using System;
using TalkBack;

namespace Main
{
  static class Program
  {
    static void Main ()
    {
      TalkBackInvoke.Process("Called.exe", "asdf -muh -qwer=3",
                             message => Console.WriteLine(message.Severity + ": " + message.Text));
    }
  }
}
