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

namespace TalkBack.Brokers.FileBased.Text
{
  [MessageParticipiant ("textFile", typeof (FileMessageConfiguration))]
  public class TextFileMessageSender : FileMessageSender
  {
    private readonly StreamWriter _writer;

    public TextFileMessageSender (FileMessageConfiguration configuration) : base(configuration)
    {
      _writer = File.CreateText(FilePath);
      _writer.AutoFlush = true;
    }

    public override void SendMessage(Message message)
    {
      _writer.WriteLine (string.Format ("{0}:{1}", message.Severity, message.Text));
    }

    protected override void DisposeManagedResources ()
    {
      _writer.Dispose();
    }
  }
}