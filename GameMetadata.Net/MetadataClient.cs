using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
            IEnumerable<GameImage> images = await GetAllGameImages(query);
            return new Game();
        }

        private static async Task<IEnumerable<GameImage>> GetAllGameImages(string query)
        {
            var result = await GetGameImages(query, 0);

            GameImage[] images = result.Images;
            if (images.Length <= 12)
            {
                return images;
            }
            await AddOtherPages(query, images.ToList());

            return images;
        }

        private static async Task AddOtherPages(string query, List<GameImage> previousImages)
        {
            var index = 0;
            var previousSize = 0;
            do
            {
                previousSize = previousImages.Count;

                GameImageSearchResult result = await GetGameImages(query, index);
                previousImages.AddRange(result.Images);

                index++;
            }
            while (previousImages.Count != previousSize);
        }

        private static async Task<GameImageSearchResult> GetGameImages(string query, int page)
        {
            string json = await new HttpClient().GetStringAsync(BuildUri(query, page));
            GameImageSearchResult result = JsonConvert.DeserializeObject<GameImageSearchResult>(json);
            return result;
        }

        private static Uri BuildUri(string query, int page)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                ["name"] = query,
                ["page"] = page.ToString()
            };

            var queryBuilder = new StringBuilder();
            int index = 0;
            foreach (KeyValuePair<string, string> pair in parameters)
            {
                var format = "{0}={1}&";
                if (++index == parameters.Count)
                {
                    format = format.TrimEnd('&');
                }
                queryBuilder.AppendFormat(CultureInfo.InvariantCulture, format, pair.Key, pair.Value.Replace(" ", "+"));
            }

            var builder = new UriBuilder("http://www.steamgriddb.com/search.php")
            {
                Query = queryBuilder.ToString()
            };
            return builder.Uri;
        }
    }
}