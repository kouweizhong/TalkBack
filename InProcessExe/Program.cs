using System;
using InProcessLib;
using TalkBack;

namespace InProcessExe
{
  static class Program
  {
    static void Main ()
    {
      TalkBackInvoke.Action (sender => new Class ().Do (sender), message => Console.WriteLine (message.Severity + ": " + message.Text));
    }
  }
}
