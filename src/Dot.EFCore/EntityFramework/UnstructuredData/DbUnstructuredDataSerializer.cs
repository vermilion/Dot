using System;
using System.Reflection;
using System.Text.Json;

namespace Cofoundry.Domain.Data.Internal
{
    /// <summary>
    /// Handles serialization for unstructured data stored in the db, e.g.
    /// Page Module data
    /// </summary>
    public class DbUnstructuredDataSerializer : IDbUnstructuredDataSerializer
    {
        public DbUnstructuredDataSerializer()
        {
        }

        public object Deserialize(string serialized, Type type)
        {
            if (string.IsNullOrEmpty(serialized))
            {
                if (type.GetTypeInfo().IsValueType) return Activator.CreateInstance(type);
                return null;
            }

            return JsonSerializer.Deserialize(serialized, type);
        }

        public T Deserialize<T>(string serialized)
        {
            if (string.IsNullOrEmpty(serialized)) 
                return default;

            return JsonSerializer.Deserialize<T>(serialized);
        }

        public string Serialize(object toSerialize)
        {
            var s = JsonSerializer.Serialize(toSerialize);

            return s;
        }
    }
}
