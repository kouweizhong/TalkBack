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

namespace TalkBack.Brokers.FileBased
{
  public abstract class FileMessageSender : MessageSender
  {
    protected string FilePath { get; private set; }
    protected Stream FileStream { get; private set; }

    protected FileMessageSender(FileMessageConfiguration configuration)
    {
      FilePath = configuration.FilePath;
      FileStream = File.Create(configuration.FilePath);
    }

    protected override void Close ()
    {
      FileStream.Close();
    }
  }
}
