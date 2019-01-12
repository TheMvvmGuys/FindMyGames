using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GameMetadata.Net
{
    public class MetadataClient
    {
        public async Task<Game> GetGameMetadata(string query)
        {
            List<GameImage> images = await GetAllGameImages(query);
            return new Game();
        }

        private static async Task<List<GameImage>> GetAllGameImages(string query)
        {
            List<GameImage> result = await GetGameImages(query, 0);

            if (result.Count <= 12)
            {
                return result;
            }
            await AddOtherPages(query, result);

            return result;
        }

        private static async Task AddOtherPages(string query, List<GameImage> result)
        {
            var index = 0;
            var previousSize = 0;
            do
            {
                previousSize = result.Count;
                result.AddRange(await GetGameImages(query, index));
                index++;
            }
            while (result.Count != previousSize);
        }

        private static async Task<List<GameImage>> GetGameImages(string query, int page)
        {
            string json = await new HttpClient().GetStringAsync(BuildUri(query, page));
            List<GameImage> result = JsonConvert.DeserializeObject<List<GameImage>>(json);
            return result;
        }

        private static Uri BuildUri(string query, int page)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                ["keyword"] = query,
                ["page"] = page.ToString()
            };

            var queryBuilder = new StringBuilder();
            foreach (KeyValuePair<string, string> pair in parameters)
            {
                queryBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}={1}&", pair.Key, pair.Value);
            }

            var builder = new UriBuilder("http://www.steamgriddb.com/search.php")
            {
                Query = queryBuilder.ToString()
            };
            return builder.Uri;
        }
    }
}