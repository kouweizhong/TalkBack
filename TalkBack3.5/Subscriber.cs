using System;

namespace TalkBack
{
  internal abstract class Subscriber<TMessage> : ISubscriber where TMessage : Message
  {
    public Action<TMessage> Callback { get; private set; }

    protected Subscriber (Action<TMessage> callback)
    {
      Callback = callback;
    }

    public void Notify(Message message)
    {
      Callback((TMessage) message);
    }

    public abstract bool IsInterestedIn(Message message);
  }
}