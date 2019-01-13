using System;
using Newtonsoft.Json;

namespace GameMetadata.Net
{
    internal class VoteConverter : JsonConverter<bool>
    {
        public override void WriteJson(JsonWriter writer, bool value, JsonSerializer serializer) =>
            throw new NotImplementedException();

        public override bool ReadJson(
            JsonReader reader,
            Type objectType,
            bool existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            var jsonValue = ((string) reader.Value);
            //didntvote = false, else = true
            return jsonValue.Equals("didntvote", StringComparison.OrdinalIgnoreCase);
        }
    }
}