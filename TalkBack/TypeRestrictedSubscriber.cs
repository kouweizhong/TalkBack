using System;

namespace TalkBack
{
  internal class TypeRestrictedSubscriber<TMessage> : Subscriber<TMessage> where TMessage : Message
  {
    public TypeRestrictedSubscriber(Action<TMessage> callback) : base(callback)
    {
    }

    public override bool IsInterestedIn(Message message)
    {
      return message.GetType().IsSubclassOf(typeof (TMessage));
    }
  }
}