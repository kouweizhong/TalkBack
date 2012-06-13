using TalkBack.Brokers;

namespace InProcessLib
{
  public class Class
  {
    public void Do(IMessageSender channel)
    {
      channel.SendInfo ("INFO");
      channel.SendError ("ERROR");
      channel.SendDebug ("DEBUG");
      channel.SendWarning ("WARNING");
    }
  }
}
