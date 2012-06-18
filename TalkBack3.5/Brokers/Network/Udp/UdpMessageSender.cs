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

namespace TalkBack.Brokers.Network.Udp
{
  [MessageParticipiant ("udp", typeof (NetworkParticipiantConfiguration))]
  public class UdpMessageSender : MessageSender
  {
    private readonly UdpClient _client;
    private readonly Encoding _encoding;
    private readonly StringMessageSerializer _serializer;
    private readonly IPEndPoint _serverEndPoint;

    public UdpMessageSender(NetworkParticipiantConfiguration configuration)
    {
      _client = new UdpClient (0);
      _encoding = configuration.Encoding;
      _serializer = new StringMessageSerializer (configuration.Separator);
      _serverEndPoint = new IPEndPoint(IPAddress.Loopback, configuration.Port);
      Console.WriteLine("Client on port {0}", configuration.Port);
    }

    public override void SendMessage(Message message)
    {
      var data = _encoding.GetBytes((string) _serializer.Serialize(message));
      
      _client.Send(data, data.Length, _serverEndPoint);
    }

    protected override void DisposeManagedResources ()
    {
      _client.Close();
    }
  }
}