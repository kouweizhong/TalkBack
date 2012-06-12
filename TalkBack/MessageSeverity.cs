using System;

namespace TalkBack
{
  [Flags]
  public enum MessageSeverity
  {
    Debug = 0x01,
    Info = 0x02,
    Warning = 0x04,
    Error = 0x08
  }
}