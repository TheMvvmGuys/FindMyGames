using System;
using Newtonsoft.Json;

namespace GameMetadata.Net
{
    public class GameImage
    {
        [JsonProperty("grid_id")]
        public string GridId { get; set; }

        [JsonProperty("game_name")]
        public string GameName { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("steam64")]
        public string Steam64 { get; set; }

        [JsonProperty("avatar")]
        public Uri Avatar { get; set; }

        [JsonProperty("grid_style")]
        [JsonConverter(typeof(GridStyleConverter))]
        public GridStyle GridStyle { get; set; }

        [JsonProperty("score")]
        public long Score { get; set; }

        [JsonProperty("appid")]
        public string Appid { get; set; }

        [JsonProperty("voted")]
        [JsonConverter(typeof(VoteConverter))]
        public bool Voted { get; set; }

        [JsonProperty("delete")]
        public bool Delete { get; set; }

        [JsonProperty("grid_link")]
        public Uri GridLink { get; set; }

        [JsonProperty("thumbnail_link")]
        public Uri ThumbnailLink { get; set; }
    }
}