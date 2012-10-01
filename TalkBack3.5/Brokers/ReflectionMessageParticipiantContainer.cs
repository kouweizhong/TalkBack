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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TalkBack.Brokers
{
  public class ReflectionMessageParticipiantContainer<T> : IMessageParticipiantContainer<T>
  {
    private class MessageParticipiant
    {
      public Type ParticipiantType { get; private set; }
      public Type ConfigurationType { get; private set; }

      public MessageParticipiant(Type sinkType, Type configurationType)
      {
        ParticipiantType = sinkType;
        ConfigurationType = configurationType;
      }
    }

    private readonly IDictionary<string, MessageParticipiant> _messageParticipiants = new Dictionary<string, MessageParticipiant>();

    public ReflectionMessageParticipiantContainer()
    {
      foreach (
        var messageSinkType in
          Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterfaces().Any(i => i == typeof (T))))
      {
        var attribute =
          (MessageParticipiantAttribute) messageSinkType.GetCustomAttributes(typeof (MessageParticipiantAttribute), false).SingleOrDefault();

        if (attribute != null)
          _messageParticipiants.Add(attribute.Identifier,
                                new MessageParticipiant(messageSinkType, attribute.ConfigurationType));
      }
    }

    public T GetMessageParticipiant(string identifier, string configuration)
    {
      if (!_messageParticipiants.ContainsKey(identifier))
        throw new InvalidOperationException(
          string.Format("No message sink could be found for the communication identifier '{0}'!",
                        identifier));

      var messageSinkType = _messageParticipiants[identifier];

      var configurationObject = Activator.CreateInstance (messageSinkType.ConfigurationType, configuration);
      return (T) Activator.CreateInstance (messageSinkType.ParticipiantType, configurationObject);
    }
  }
}