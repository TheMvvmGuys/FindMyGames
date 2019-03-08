using System;
using System.IO;

namespace TheMvvmGuys.FindMyGames
{
    internal class Paths
    {
        internal static string ApplicationDataPath =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\FindMyGames\\");
        internal static string SettingsFilePath =>
            Path.Combine(ApplicationDataPath, "settings.json");
    }
}