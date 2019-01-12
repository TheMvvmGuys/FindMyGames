using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace GameMetadata.Net
{
    internal class GameImageSearchResult
    {
        [JsonIgnore]
        public int StartIndex => (int) _additionalData.ElementAt(0).Value;

        [JsonIgnore]
        public int EndIndex => (int) _additionalData.ElementAt(1).Value;

        [JsonIgnore]
        public bool Value => (bool) _additionalData.ElementAt(2).Value;

        [JsonIgnore]
        public GameImage[] Images => _additionalData.ElementAt(3).Value as GameImage[];

        [JsonExtensionData]
        private IDictionary<string, object> _additionalData;
    }
}