using System;
using Newtonsoft.Json;

namespace GameMetadata.Net
{
    public class GameImage
    {
        public string GridId { get; set; }

        public string GameName { get; set; }

        public string Username { get; set; }

        public string Steam64 { get; set; }

        public Uri Avatar { get; set; }

        public GridStyle GridStyle { get; set; }

        public long Score { get; set; }

        [JsonProperty("appid")]
        public string Appid { get; set; }

        [JsonConverter(typeof(VoteConverter))]
        public bool Voted { get; set; }

        public bool Delete { get; set; }

        public Uri GridLink { get; set; }

        public Uri ThumbnailLink { get; set; }
    }
}