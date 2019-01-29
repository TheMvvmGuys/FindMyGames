using System;
using System.Collections.Generic;
using System.Linq;

namespace TheMvvmGuys.FindMyGames.Utilities.Extensions
{
    public static class RelativeToAbsoluteExtensions
    {
        public static Uri[] MakeRelativeUris(this IEnumerable<string> partialUris)
        {
            return partialUris.Select(uri => new Uri(uri, UriKind.Relative)).ToArray();
        }
    }
}