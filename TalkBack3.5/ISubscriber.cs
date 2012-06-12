namespace TalkBack
{
  internal interface ISubscriber
  {
    void Notify(Message message);
    bool IsInterestedIn(Message message);
  }
}