using System;
using System.Collections.Generic;
using System.Linq;

namespace GameMetadata.Net
{
    internal class Game
    {
        public string Name { get; set; }

        public Uri GridUrl => ImageResults.First().GridLink;

        public IEnumerable<GameImage> ImageResults { get; set; }
    }
}