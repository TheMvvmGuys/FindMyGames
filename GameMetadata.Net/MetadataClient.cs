using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GameMetadata.Net
{
    public class MetadataClient
    {
        private const string ApiEndpoint = "http://www.steamgriddb.com/search.php";
        
        private readonly static JsonSerializerSettings DefaultSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };

        private static T DeserializeObject<T>(string json) 
            => JsonConvert.DeserializeObject<T>(json, DefaultSettings);

        public async Task<SearchedGame> GetGameMetadataAsync(string query)
        {
            IEnumerable<GameImage> images = await GetAllGameImagesAsync(query);
            //metadata part here
            return new SearchedGame
            {
                Name = query,
                ImageResults = images
            };
        }

        private static async Task<IEnumerable<GameImage>> GetAllGameImagesAsync(string query)
        {
            IEnumerable<GameImage> images = await GetGameImagesAsync(query);
            List<GameImage> imagesList = images.ToList();

            if (imagesList.Count <= 12)
            {
                return imagesList;
            }

            await AddOtherPagesAsync(query, imagesList);
            return imagesList;
        }

        private static async Task AddOtherPagesAsync(string query, List<GameImage> previousImages)
        {
            var index = 0;
            var previousSize = 0;
            do
            {
                previousSize = previousImages.Count;

                previousImages.AddRange(await GetGameImagesAsync(query, index));
                index++;
            }
            while (previousImages.Count != previousSize);
        }

        private static async Task<IEnumerable<GameImage>> GetGameImagesAsync(string query, int page = 0)
        {
            string json = await new HttpClient().GetStringAsync(BuildUri(query, page));
            //Because it has in the array extra 3 objects
            object[] objectArray = DeserializeObject<object[]>(json);
            if (objectArray.Length <= 3 && objectArray[0] as string == "None")
            {
                throw new GameDataNotFoundException(query);
            }
            return objectArray.Skip(3)
               .Select(o => DeserializeObject<GameImage>(o.ToString()));
        }

        private static Uri BuildUri(string query, int page)
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>
            {
                ["name"] = query,
                ["page"] = page.ToString()
            };

            var queryBuilder = new StringBuilder();
            var index = 0;
            // ReSharper disable once SuggestVarOrType_Elsewhere
            foreach (var pair in parameters)
            {
                var format = "{0}={1}&";
                //Take down the '&' off last parameter
                if (++index == parameters.Count)
                {
                    format = format.TrimEnd('&');
                }

                queryBuilder.AppendFormat(CultureInfo.InvariantCulture, format, pair.Key, pair.Value.Replace(" ", "+"));
            }
            
            var builder = new UriBuilder(ApiEndpoint)
            {
                Query = queryBuilder.ToString()
            };
            return builder.Uri;
        }
    }
}