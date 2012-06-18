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
  public class TextFileMessageSender : FileMessageSender
  {
    private readonly StreamWriter _writer;
    private readonly StringMessageSerializer _serializer;

    public TextFileMessageSender (TextFileParticipiantConfiguration configuration) : base(configuration)
    {
      _writer = new StreamWriter(Stream) {AutoFlush = true};
      _serializer = new StringMessageSerializer(configuration.Separator);
    }

    public override void SendMessage(Message message)
    {
      _writer.WriteLine(_serializer.Serialize(message));
    }

    protected override void OnClosing ()
    {
      _writer.Close();
    }
  }
}