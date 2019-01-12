using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GameMetadata.Net
{
    public class MetadataClient
    {
        public async Task<SearchedGame> GetGameMetadata(string query)
        {
            IEnumerable<GameImage> images = await GetAllGameImages(query);
            //metadata part here
            return new SearchedGame
            {
                Name = query,
                ImageResults = images
            };
        }

        private static async Task<IEnumerable<GameImage>> GetAllGameImages(string query)
        {
            IEnumerable<GameImage> images = await GetGameImages(query, 0);

            if (images.Count() <= 12)
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

                previousImages.AddRange(await GetGameImages(query, index));

                index++;
            }
            while (previousImages.Count != previousSize);
        }

        private static async Task<IEnumerable<GameImage>> GetGameImages(string query, int page)
        {
            string json = await new HttpClient().GetStringAsync(BuildUri(query, page));
            //Because it has in the array extra 3 objects
            object[] objectArray = JsonConvert.DeserializeObject<object[]>(json);

            return objectArray.Skip(3)
               .Select(o => JsonConvert.DeserializeObject<GameImage>(o.ToString()));
        }

        private static Uri BuildUri(string query, int page)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                ["name"] = query,
                ["page"] = page.ToString()
            };

            var queryBuilder = new StringBuilder();
            var index = 0;
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