using System;

namespace TalkBack
{
  internal class SeverityRestrictedSubscriber : Subscriber<Message>
  {
    private readonly MessageSeverity _interestingSeverities;

    public SeverityRestrictedSubscriber(Action<Message> callback, MessageSeverity interestingSeverities) : base(callback)
    {
      _interestingSeverities = interestingSeverities;
    }
    
    public override bool IsInterestedIn(Message message)
    {
      return (message.Severity & _interestingSeverities) == message.Severity;
    }
  }
}