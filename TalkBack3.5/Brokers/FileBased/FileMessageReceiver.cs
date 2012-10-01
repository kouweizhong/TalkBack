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
using System.IO;

namespace TalkBack.Brokers.FileBased
{
  public abstract class FileMessageReceiver : MessageReceiver
  {
    private readonly FileParticipiantConfiguration _configuration;
    private Stream _stream;

    protected FileMessageReceiver (FileParticipiantConfiguration configuration)
    {
      _configuration = configuration;
    }

    public override void OnStartSender ()
    {
      _configuration.Path.Create().Close();
    }

    public override sealed void OnEndSender ()
    {
      try
      {
        _stream = _configuration.Path.OpenRead();
        ProcessFile (_stream);
      }
      catch(Exception ex)
      {
        throw new Exception ("Error processing file '" + _configuration.Path + "'.", ex);
      }
    }

    protected abstract void ProcessFile(Stream stream);

    protected virtual void OnClosing ()
    {
    }

    protected override sealed void Close ()
    {
      OnClosing ();
      if (_stream != null)
        _stream.Close ();
      _configuration.Path.Delete();
    }

    public override string BuildSenderConfig ()
    {
      return _configuration.ToString();
    }
  }
}