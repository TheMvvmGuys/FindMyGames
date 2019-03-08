using System;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Ninject;
using Ninject.Activation;
using TheMvvmGuys.FindMyGames.ViewModels;

namespace TheMvvmGuys.FindMyGames.Providers
{
    public class SettingsProvider : Provider<Settings>
    {
        protected override Settings CreateInstance(IContext context)
        {
            try
            {
                using (var streamReader = new StreamReader(Paths.SettingsFilePath, Encoding.UTF8))
                using (var jsonReader = new JsonTextReader(streamReader))
                {
                    var finalSettings = new JsonSerializer
                    {
                        TypeNameHandling = TypeNameHandling.Auto,
                        PreserveReferencesHandling = PreserveReferencesHandling.All
                    }.Deserialize<Settings>(jsonReader);
                    return finalSettings;
                }
            }
            catch (Exception)
            {
                // It's fine we will save it later
                return new Settings();
            }
        }
    }
}