using System;
using System.Linq;

namespace TheMvvmGuys.FindMyGames.Extensions
{
    internal static class UriExtensions
    {
        public static string GetLastElement(this Uri u) => u.OriginalString.Split('/', '\\').Last();
    }
}