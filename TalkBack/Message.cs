using System;

namespace TalkBack
{
  public class Message
  {
    public MessageSeverity Severity { get; private set; }
    public string Text { get; private set; }
    public Exception CausingException { get; private set; }

    public Message(MessageSeverity severity, string text, Exception causingException = null)
    {
      Severity = severity;
      Text = text;
      CausingException = causingException;
    }

    public Message (MessageSeverity severity, string text, params object[] args)
      : this (severity, string.Format (text, args))
    {

    }

    public Message (MessageSeverity severity, string text, Exception causingException, params object[] args)
      : this (severity, string.Format (text, args), causingException)
    {
      
    }
  }
}