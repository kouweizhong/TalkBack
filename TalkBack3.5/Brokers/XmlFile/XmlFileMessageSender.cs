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

using System.Xml;

namespace TalkBack.Brokers.XmlFile
{
  [MessageParticipiant ("xmlFile", typeof (XmlFileMessageConfiguration))]
  public class XmlFileMessageSender : MessageSender
  {
    private readonly XmlWriter _writer;

    public XmlFileMessageSender (XmlFileMessageConfiguration configuration)
    {
      _writer = XmlWriter.Create (configuration.FilePath);
      _writer.WriteStartDocument();
      _writer.WriteStartElement("Messages");
    }

    public override void SendMessage(Message message)
    {
      _writer.WriteElementString(message.Severity.ToString(), message.Text);
      _writer.Flush();
    }

    protected override void DisposeManagedResources ()
    {
      _writer.Flush();
      _writer.WriteEndElement();
      _writer.WriteEndDocument();
      _writer.Close();
    }
  }
}