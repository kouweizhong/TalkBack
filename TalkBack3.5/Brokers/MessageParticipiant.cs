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

namespace TalkBack.Brokers
{
  public abstract class MessageParticipiant : IMessageParticipiant, IDisposable
  {
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    ~MessageParticipiant()
    {
      Dispose(false);
    }

    protected virtual void DisposeManagedResources()
    {
      
    }

    protected virtual void DisposeNativeResources()
    {
      
    }

    private void Dispose(bool disposing)
    {
      if (disposing)
        DisposeManagedResources();

      DisposeNativeResources();
    }
  }
}