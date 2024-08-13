using Iot.Schema.Brick.Extensions;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Iot.Schema
{
    [JsonConverter(typeof(BrickSchemaJsonConverter))]
    public abstract class BrickSchema
    {
        public string Name => GetType().Name;

        // Method to return a list of names that can be contained by this entity
        public List<string> GetContainableNames()
        {
            // Get the list of property types that can be contained by this entity
            return GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                            .Where(prop => typeof(BrickSchema).IsAssignableFrom(prop.PropertyType))
                            .Select(prop => prop.PropertyType.Name)
                            .Distinct()
                            .ToList();
        }

        public abstract bool CanContain(BrickSchema entity);

        protected bool CanContainType(Type entityType)
        {
            // Check if any public property of this type returns the entity type
            return GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                             .Any(prop => prop.PropertyType == entityType);
        }

        // Converts the current instance to a JSON string
        public string ToJson()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        }

        // Static method to create an instance of the correct type from a JSON string
        public static BrickSchema? FromJson(string json)
        {
            return JsonSerializer.Deserialize<BrickSchema>(json, new JsonSerializerOptions { Converters = { new BrickSchemaJsonConverter() } });
        }
    }
}
