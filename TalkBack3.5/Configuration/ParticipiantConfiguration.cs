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
using System.Linq;
using System.Text;
using TalkBack.Configuration.Converters;

namespace TalkBack.Configuration
{
  public abstract class ParticipiantConfiguration
  {
    protected ParticipiantConfiguration()
    {
    }

    protected ParticipiantConfiguration(string configurationString)
    {
      ParseConfiguration(configurationString);
    }

    private void ParseConfiguration(string configurationString)
    {
      var dictionary = configurationString.Split(',').ToDictionary(s => s.Split('=')[0], s => s.Split('=')[1]);

      foreach (var property in GetType ().GetProperties ())
      {
        var attribute = (ConfigurationParameterAttribute)property.GetCustomAttributes(typeof (ConfigurationParameterAttribute), true).SingleOrDefault();
        if (attribute == null)
          continue;

        var value = dictionary[attribute.Key];
        if (attribute.Converter != null)
        {
          var converter = (IConverter) Activator.CreateInstance (attribute.Converter);
          property.SetValue (this, converter.Convert (value), null);
        }
        else
        {
          property.SetValue(this, value, null);
        }
      }
    }

    public override string ToString ()
    {
      var sb = new StringBuilder();

      foreach (var property in GetType ().GetProperties ())
      {
        var attribute = (ConfigurationParameterAttribute) property.GetCustomAttributes (typeof (ConfigurationParameterAttribute), true).SingleOrDefault ();
        if (attribute == null)
          continue;

        if (sb.Length > 0)
          sb.Append(',');

        sb.Append(attribute.Key);
        sb.Append('=');

        var value = property.GetValue(this, null);
        if (attribute.Converter != null)
          value = ((IConverter) Activator.CreateInstance(attribute.Converter)).ConvertBack(value);

        sb.Append(value);
      }

      return sb.ToString();
    }
  }
}
