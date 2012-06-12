namespace TalkBack
{
  public interface ITalkbackChannel
  {
    void Send(Message message);
    void SendInfo(string message, params object[] args);
    void SendDebug(string message, params object[] args);
    void SendWarning(string message, params object[] args);
    void SendError(string message, params object[] args);
  }
}
