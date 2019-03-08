using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Jeuxjeux20.Mvvm;
using Newtonsoft.Json;
using Ninject;
using TheMvvmGuys.FindMyGames.UI.Themes;

namespace TheMvvmGuys.FindMyGames.ViewModels
{
    public class SettingsViewModel : ViewModelBase<Settings>, ISettings
    {
        private IThemeCollection _appThemes;
        
        public SettingsViewModel(IThemeCollection themes, Settings model = null) : base(model)
        {
            _appThemes = themes;
        }

        public IEnumerable<Theme> Themes => _appThemes.Select(t => t.Theme);
        public Theme Theme
        {
            get => Model.Theme;
            set
            {
                Model.Theme = value;
                OnPropertyChanged(nameof(Theme));
                _appThemes.SelectedItem = _appThemes.FirstOrDefault(t => t.Theme == value);
            }
        }

        public bool IsFirstRun
        {
            get => Model.IsFirstRun;
            set
            {
                Model.IsFirstRun = value;
                OnPropertyChanged(nameof(IsFirstRun));
            }
        }

        public void SaveSettings()
        {
            using (var file = new FileStream(Paths.SettingsFilePath, FileMode.Create, FileAccess.Write))
            {
                using (var streamWriter = new StreamWriter(file, Encoding.UTF8))
                {
                    streamWriter.WriteLine(JsonConvert.SerializeObject(Model, _serializerSettings));
                }
            }
        }
        private static JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            PreserveReferencesHandling = PreserveReferencesHandling.All
        };
    }
}