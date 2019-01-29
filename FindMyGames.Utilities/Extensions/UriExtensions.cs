using System;
using System.Linq;

namespace TheMvvmGuys.FindMyGames.Utilities.Extensions
{
    public static class UriExtensions
    {
        public static string GetLastElement(this Uri u) => u.OriginalString.Split('/', '\\').Last();
    }
}