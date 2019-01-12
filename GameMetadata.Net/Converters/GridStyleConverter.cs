using System;
using Newtonsoft.Json;

namespace GameMetadata.Net
{
    internal class GridStyleConverter : JsonConverter<GridStyle>
    {
        public override void WriteJson(JsonWriter writer, GridStyle value, JsonSerializer serializer) 
            => throw new NotImplementedException();

        public override GridStyle ReadJson(
            JsonReader reader,
            Type objectType,
            GridStyle existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            var readerValue = (string) reader.Value;
            switch (readerValue)
            {
                case "no_logo":
                    return GridStyle.NoLogoTextless;

                case "material":
                    return GridStyle.MaterialMinimal;

                default:
                    Enum.TryParse(readerValue, true, out GridStyle result);
                    return result;
            }
        }
    }
}
