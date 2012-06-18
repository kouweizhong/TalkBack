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
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using TalkBack.Serializers;

namespace TalkBack.Brokers.Network.Tcp
{
  [MessageParticipiant ("tcp", typeof (NetworkParticipiantConfiguration))]
  public class TcpMessageReceiver : MessageReceiver
  {
    private readonly Thread _serverThread;
    private readonly Encoding _encoding;
    private readonly TcpListener _listener;
    private readonly StringMessageSerializer _serializer;
    private bool _aborted;

    public TcpMessageReceiver (NetworkParticipiantConfiguration configuration)
    {
      _encoding = configuration.Encoding;
      _listener = new TcpListener (IPAddress.Loopback, configuration.Port);
      _listener.Start();
      _serverThread = new Thread(RunServer);
      _serializer = new StringMessageSerializer(configuration.Separator);
    }

    private byte[] Read(NetworkStream ns, int size)
    {
      int totalBytes = 0;
      var result = new byte[size];
      while (totalBytes < size)
      {
        int readBytes = ns.Read(result, totalBytes, size - totalBytes);
        if (readBytes <= 0)
          return null;

        totalBytes += readBytes;
      }

      return result;
    }

    private void RunServer()
    {
      try
      {
        var client = _listener.AcceptTcpClient ();
        var ns = client.GetStream ();
        ns.ReadTimeout = 100;
        while (!_aborted || client.Available > 0)
        {
          var lengthBytes = Read(ns, sizeof (int));

          if (lengthBytes == null)
            break;

          var length = BitConverter.ToInt32(lengthBytes, 0);

          var data = Read(ns, length);
          OnMessage(_serializer.Deserialize(_encoding.GetString(data, 0, length)));
        }
      }
      catch (ThreadAbortException)
      {
        
      }
    }

    public override void OnStartSender ()
    {
      _serverThread.Start();
    }

    public override void OnEndSender ()
    {
      _aborted = true;
      _listener.Stop();
    }

    public override string BuildSenderConfig()
    {
      return
        new NetworkParticipiantConfiguration (((IPEndPoint) _listener.LocalEndpoint).Port, _encoding, _serializer.Separator).
          ToString();
    }
  }
}
