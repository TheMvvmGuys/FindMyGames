using Newtonsoft.Json;

namespace GameMetadata.Net
{
    public enum GridStyle
    {
        None,
        Alternate,
        Blurred,
        [JsonProperty("no_logo")]
        NoLogoTextLess,
        [JsonProperty("material")]
        MaterialMinimal
    }
}