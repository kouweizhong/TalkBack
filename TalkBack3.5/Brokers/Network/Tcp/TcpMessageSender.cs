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

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TalkBack.Serializers;

namespace TalkBack.Brokers.Network.Tcp
{
  [MessageParticipiant ("tcp", typeof (NetworkParticipiantConfiguration))]
  public class TcpMessageSender : MessageSender
  {
    private readonly TcpClient _client;
    private readonly StringMessageSerializer _serializer;
    private readonly NetworkStream _stream;
    private readonly Encoding _encoding;

    public TcpMessageSender(NetworkParticipiantConfiguration configuration)
    {
      _encoding = configuration.Encoding;
      _client = new TcpClient (new IPEndPoint(IPAddress.Any, 0));
      _client.Connect(new IPEndPoint(IPAddress.Loopback, configuration.Port));
      _stream = _client.GetStream();
      _serializer = new StringMessageSerializer (configuration.Separator);
    }

    public override void SendMessage(Message message)
    {
      var messageString = (string) _serializer.Serialize(message);
      var data = _encoding.GetBytes(messageString);
      var length = BitConverter.GetBytes(_encoding.GetByteCount(messageString));
      _stream.Write(length, 0, length.Length);
      _stream.Write(data, 0, data.Length);
    }

    protected override void DisposeManagedResources ()
    {
      _client.Close();
    }
  }
}