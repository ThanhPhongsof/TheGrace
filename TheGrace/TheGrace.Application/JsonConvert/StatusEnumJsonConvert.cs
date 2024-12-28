using Newtonsoft.Json;
using TheGrace.Domain.Enumerations;
using Exception = TheGrace.Domain.Exceptions.JsonException;

namespace TheGrace.Application.JsonConvert;

public class StatusEnumJsonConvert : JsonConverter<StatusEnum>
{
    public override StatusEnum? ReadJson(JsonReader reader, Type objectType, StatusEnum? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.String)
        {
            var enumName = (string)reader.Value;
            return StatusEnum.FromName(enumName);
        }
        else if (reader.TokenType == JsonToken.Integer)
        {
            var enunValue = Convert.ToInt32(reader.Value);
            return StatusEnum.FromValue(enunValue);
        }

        throw new Exception.JsonInvalidException(nameof(StatusEnum));
    }

    public override void WriteJson(JsonWriter writer, StatusEnum? value, JsonSerializer serializer)
    {
        writer.WriteValue(value.Name);
    }
}
