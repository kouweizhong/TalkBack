using System;
using System.Collections.Generic;
using System.Linq;

namespace TalkBack
{
  public class TalkbackChannel : ITalkbackChannel, ISubscriptionChannel
  {
    private readonly HashSet<ISubscriber> _subscribers =
      new HashSet<ISubscriber> ();

    public void Send(Message message)
    {
      foreach(var subscriber in _subscribers.Where(s => s.IsInterestedIn(message)))
        subscriber.Notify(message);
    }

    public void SendInfo(string message, params object[] args)
    {
      Send (new Message (MessageSeverity.Info, message, args));
    }

    public void SendDebug(string message, params object[] args)
    {
      Send (new Message (MessageSeverity.Debug, message, args));
    }

    public void SendWarning(string message, params object[] args)
    {
      Send (new Message (MessageSeverity.Warning, message, args));
    }

    public void SendError(string message, params object[] args)
    {
      Send (new Message (MessageSeverity.Error, message, args));
    }

    private void Subscribe(ISubscriber subscriber)
    {
      _subscribers.Add(subscriber);
    }

    public void Subscribe<TMessage>(Action<TMessage> callback) where TMessage : Message
    {
      Subscribe(new TypeRestrictedSubscriber<TMessage>(callback));
    }

    public void Subscribe(Action<Message> callback, MessageSeverity interestingSeverities)
    {
      Subscribe(new SeverityRestrictedSubscriber(callback, interestingSeverities));
    }

    public void Unsubscribe<TMessage>(Action<TMessage> callback) where TMessage : Message
    {
      _subscribers.Remove(_subscribers.OfType<Subscriber<TMessage>>().Single(s => s.Callback == callback));
    }
  }
}