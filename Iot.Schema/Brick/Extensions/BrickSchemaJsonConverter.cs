using System.Text.Json;
using System.Text.Json.Serialization;

namespace Iot.Schema.Brick.Extensions
{
    public class BrickSchemaJsonConverter : JsonConverter<BrickSchema>
    {
        public override BrickSchema Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                if (!doc.RootElement.TryGetProperty("Name", out JsonElement nameElement))
                    throw new JsonException("Missing 'Name' property");

                string typeName = nameElement.GetString();
                Type type = Type.GetType($"Iot.Schema.{typeName}, {typeof(BrickSchema).Assembly.FullName}");

                if (type == null)
                    throw new JsonException($"Unknown type: {typeName}");

                return (BrickSchema)JsonSerializer.Deserialize(doc.RootElement.GetRawText(), type, options);
            }
        }

        public override void Write(Utf8JsonWriter writer, BrickSchema value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, (object)value, value.GetType(), options);
        }
    }
}
