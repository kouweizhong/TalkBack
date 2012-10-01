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
using System.IO;
using TalkBack.Serializers;

namespace TalkBack.Brokers.FileBased.Text
{
  [MessageParticipiant ("txt", typeof (TextFileParticipiantConfiguration))]
  public class TextFileMessageReceiver : FileMessageReceiver
  {
    private readonly StringMessageSerializer _serializer;
    private new readonly TextFileParticipiantConfiguration _configuration;

    public TextFileMessageReceiver (TextFileParticipiantConfiguration configuration) : base(configuration)
    {
      _configuration = configuration;
      _serializer = new StringMessageSerializer(configuration.Separator);
    }

    protected override void ProcessFile(Stream stream)
    {
      var sr = new StreamReader(stream);
      string line;
      while ((line = sr.ReadLine ()) != null)
        OnMessage(_serializer.Deserialize(line));
    }

    public override string BuildSenderConfig()
    {
      return _configuration.ToString();
    }
  }
}