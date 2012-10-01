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
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using TalkBack.Serializers;

namespace TalkBack.Brokers.Network.Udp
{
  [MessageParticipiant("udp", typeof (NetworkParticipiantConfiguration))]
  public class UdpMessageReceiver : MessageReceiver
  {
    private readonly Encoding _encoding;
    private readonly StringMessageSerializer _serializer;
    private readonly UdpClient _server;
    private readonly Thread _serverThread;
    private bool _aborted;

    public UdpMessageReceiver(NetworkParticipiantConfiguration configuration)
    {
      _server = new UdpClient(configuration.Port);
      _serverThread = new Thread(RunServer);
      _encoding = configuration.Encoding;
      _serializer = new StringMessageSerializer(configuration.Separator);
    }

    private void RunServer()
    {
      var sender = new IPEndPoint(IPAddress.Any, 0);

      while (!_aborted || _server.Available > 0)
      {
        byte[] data = _server.Receive(ref sender);

        string message = _encoding.GetString(data);
        OnMessage(_serializer.Deserialize(message));
      }

      _server.Close();
    }

    public override void OnStartSender()
    {
      _serverThread.Start();
    }

    public override void OnEndSender()
    {
      _aborted = true;
    }

    public override string BuildSenderConfig()
    {
      return new NetworkParticipiantConfiguration(((IPEndPoint) _server.Client.LocalEndPoint).Port, _encoding,
                                                  _serializer.Separator).ToString();
    }
  }
}