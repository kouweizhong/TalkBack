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

namespace TalkBack.Serializers
{
  public interface IMessageSerializer
  {
    object Serialize (Message message);
    Message Deserialize (object serialized);
  }

  public abstract class MessageSerializer<T> : IMessageSerializer
  {
    public object Serialize(Message message)
    {
      return SerializeInternal(message);
    }

    public Message Deserialize (object serialized)
    {
      return DeserializeInternal ((T) serialized);
    }

    protected abstract T SerializeInternal(Message message);
    protected abstract Message DeserializeInternal (T serialized);
  }

  public class StringMessageSerializer : MessageSerializer<string>
  {
    public string Separator { get; private set; }

    public StringMessageSerializer(string separator = ":")
    {
      Separator = separator;
    }

    protected override string SerializeInternal(Message message)
    {
      return string.Format ("{0}{1}{2}", message.Severity, Separator, message.Text);
    }

    protected override Message DeserializeInternal(string serialized)
    {
      var split = serialized.Split(new[] {Separator}, StringSplitOptions.RemoveEmptyEntries);
      return new Message((MessageSeverity) Enum.Parse(typeof (MessageSeverity), split[0]), split[1]);
    }
  }
}
