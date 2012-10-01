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
using System.Text;
using TalkBack.Configuration;
using TalkBack.Configuration.Converters;

namespace TalkBack.Brokers.Network
{
  public class NetworkParticipiantConfiguration : ParticipiantConfiguration
  {
    [ConfigurationParameter ("port", Converter = typeof (IntConverter))]
    public int Port { get; private set; }

    [ConfigurationParameter ("sep")]
    public string Separator { get; private set; }

    [ConfigurationParameter("enc", Converter = typeof(EncodingConverter))]
    public Encoding Encoding { get; private set; }

    public NetworkParticipiantConfiguration (string configurationString) : base(configurationString)
    {
    }

    public NetworkParticipiantConfiguration(int port, Encoding encoding, string separator)
    {
      Port = port;
      Encoding = encoding;
      Separator = separator;
    }
  }
}
