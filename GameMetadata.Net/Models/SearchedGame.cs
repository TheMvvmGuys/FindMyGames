using System;
using System.Collections.Generic;
using System.Linq;

namespace TheMvvmGuys.GameMetadata.Net
{
    public class SearchedGame
    {
        public string Name { get; set; }

        public Uri GridUrl => ImageResults.First().GridLink;

        public IEnumerable<GameImage> ImageResults { get; set; }

        public string Description { get; set; }
    }
}