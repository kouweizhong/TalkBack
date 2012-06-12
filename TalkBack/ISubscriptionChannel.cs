using System;

namespace TalkBack
{
  public interface ISubscriptionChannel
  {
    void Subscribe<TMessage> (Action<TMessage> callback) where TMessage : Message;
    void Subscribe (Action<Message> callback, MessageSeverity interestingSeverities);

    void Unsubscribe<TMessage> (Action<TMessage> callback) where TMessage : Message;
  }
}