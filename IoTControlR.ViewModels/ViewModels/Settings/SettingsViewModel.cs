using IoTControlR.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IoTControlR.ViewModels
{
    #region SettingsArgs
    public class SettingsArgs
    {
        static public SettingsArgs CreateDefault() => new SettingsArgs();
    }
    #endregion
    public class SettingsViewModel : ViewModelBase
    {
        public SettingsViewModel(ISettingsService settingsService, ICommonServices commonServices) : base(commonServices)
        {
            SettingsService = settingsService;
        }

        public ISettingsService SettingsService { get; }

        private string _sqlConnectionString = null;
        public string SqlConnectionString
        {
            get => _sqlConnectionString;
            set => Set(ref _sqlConnectionString, value);
        }

        private async void OnCreateDatabase()
        {
            //StatusReady();
            //DisableAllViews("Waiting for the database to be created...");
            var result = await SettingsService.CreateDabaseAsync(SqlConnectionString);
            //EnableOtherViews();
            //EnableThisView("");
            await Task.Delay(100);
            if (result.IsOk)
            {
                //StatusMessage(result.Message);
            }
            else
            {
                //StatusError("Error creating database");
            }
        }
    }
}
