using System.Text.Json;
using System;
using System.Text.Json.Serialization;

namespace WillowBatMarketWebApiService.helper
{
    public class CustomGuidConverter : JsonConverter<Guid>
    {
        public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (!Guid.TryParse(reader.GetString(), out var parsedGuid))
            {
                throw new Exception($"Unable to parse {reader.GetString()} to GUID");
            }
            return parsedGuid;
        }

        public override void Write(Utf8JsonWriter writer, Guid value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.ToString("D"));
    }
}
