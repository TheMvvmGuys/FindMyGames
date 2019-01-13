using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GameMetadata.Net.Tests
{
    [TestClass]
    [TestCategory("JSON Serialization tests")]
    public class SerializationUnitTests
    {
        public static readonly JsonSerializerSettings MissingMemberSettings = new JsonSerializerSettings
        {
            MissingMemberHandling = MissingMemberHandling.Error,
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };
        [TestMethod]
        public void Serialization_GameMetadata_AllMembersFound()
        {
            const string json = @"{
    ""grid_id"": ""KNMKGEELNM"",
    ""game_name"": ""Battlefield 1"",
    ""username"": ""007a83"",
    ""steam64"": ""76561198053440054"",
    ""avatar"": ""https://steamcdn-a.akamaihd.net/steamcommunity/public/images/avatars/c2/c293b9a602a0219fb0b18b463b775fd9450d45cb.jpg"",
    ""grid_style"": ""Alternate"",
    ""score"": ""1"",
    ""appid"": ""BKMLEEDLO"",
    ""voted"": ""didntvote"",
    ""delete"": false,
    ""grid_link"": ""https://s3.amazonaws.com/steamgriddb/grid/af87f7cdcda223c41c3f3ef05a3aaeea.png"",
    ""thumbnail_link"": ""https://s3.amazonaws.com/steamgriddb/thumb/af87f7cdcda223c41c3f3ef05a3aaeea.png""
  }";
            var deserialized = JsonConvert.DeserializeObject<GameImage>(json, MissingMemberSettings);
            Assert.IsNotNull(deserialized);
            Assert.IsFalse(deserialized.GridStyle == GridStyle.None);
        }
    }
}