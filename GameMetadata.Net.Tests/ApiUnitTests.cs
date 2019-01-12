using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameMetadata.Net.Tests
{
    [TestClass]
    [TestCategory("API Requests")]
    public class ApiUnitTests
    {
        public const string SampleGameName = "Battlefield 1";
        public static MetadataClient Client { get; } = new MetadataClient();
        [TestMethod]
        public async Task Request_GameMetadata_HasAny()
        {
            var result = await Client.GetGameMetadataAsync(SampleGameName);
            Assert.IsTrue(result.ImageResults?.Any() ?? false);
        }      
        [TestMethod]
        public async Task Request_GameMetadata_ThrowsOnInvalid()
        {
            var random = new Random();
            var stringBuilder = new StringBuilder();
            for (int i = 0; i < 25; i++)
            {
                stringBuilder.Append((char) ('a' + random.Next(0, 26))); // Make some gibberish
            }
            Console.WriteLine("Gibberish: " + stringBuilder);
            await Assert.ThrowsExceptionAsync<GameDataNotFoundException>(() =>
                Client.GetGameMetadataAsync(stringBuilder.ToString()));
        }
    }
}
