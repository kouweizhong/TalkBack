// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace TalkBack.Brokers
{
  public abstract class MessageSender : MessageParticipiant, IMessageSender
  {
    public abstract void SendMessage(Message message);

    public void SendInfo (string message, params object[] args)
    {
      SendMessage (new Message (MessageSeverity.Info, message, args));
    }

    public void SendDebug (string message, params object[] args)
    {
      SendMessage (new Message (MessageSeverity.Debug, message, args));
    }

    public void SendWarning (string message, params object[] args)
    {
      SendMessage (new Message (MessageSeverity.Warning, message, args));
    }

    public void SendError (string message, params object[] args)
    {
      SendMessage (new Message (MessageSeverity.Error, message, args));
    }
  }
}