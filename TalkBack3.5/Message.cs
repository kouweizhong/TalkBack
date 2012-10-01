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
namespace TalkBack
{
  public class Message
  {
    public MessageSeverity Severity { get; private set; }
    public string Text { get; private set; }

    public Message(MessageSeverity severity, string text)
    {
      Severity = severity;
      Text = text;
    }

    public Message (MessageSeverity severity, string text, params object[] args)
      : this (severity, string.Format (text, args))
    {

    }
  }
}