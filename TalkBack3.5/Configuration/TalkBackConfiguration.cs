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

using System.Configuration;

namespace TalkBack.Configuration
{
  public class TalkBackConfiguration : ConfigurationSection
  {
    public static TalkBackConfiguration Configuration
    {
      get { return ConfigurationManager.GetSection("talkBack") as TalkBackConfiguration ?? GetDefaultConfig(); }
    }

    [ConfigurationProperty("identifier", IsRequired = true)]
    public string Identifier
    {
      get { return (string) this["identifier"]; }
      set { this["identifier"] = value; }
    }

    [ConfigurationProperty("options", IsRequired = true)]
    public string Options
    {
      get { return (string) this["options"]; }
      set { this["options"] = value; }
    }

    private static TalkBackConfiguration GetDefaultConfig()
    {
      return new TalkBackConfiguration { Identifier = "udp", Options = "port=0,enc=ASCII,sep=:" };
    }
  }
}