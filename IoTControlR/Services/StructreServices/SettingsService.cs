using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IoTControl;

namespace IoTControlR.Services
{
    public class SettingsService : ISettingsService
    {
        public SettingsService(IDialogService dialogService)
        {
            DialogService = dialogService;
        }

        public IDialogService DialogService { get; }

        public string Version => AppSettings.Current.Version;

        public string DbVersion => AppSettings.Current.DbVersion;

        public string UserName
        {
            get => AppSettings.Current.UserName;
            set => AppSettings.Current.UserName = value;
        }

        public DataProviderType DataProvider
        {
            get => AppSettings.Current.DataProvider;
            set => AppSettings.Current.DataProvider = value;
        }

        public string SQLServerConnectionString
        {
            get => AppSettings.Current.SQLServerConnectionString;
            set => AppSettings.Current.SQLServerConnectionString = value;
        }

        public Task<Result> CreateDabaseAsync(string connectionString)
        {
            //var dialog = new CreateDatabaseView(connectionString);
            //var res = await dialog.ShowAsync();
            //switch (res)
            //{
            //    case ContentDialogResult.Secondary:
            //        return Result.Ok("Operation canceled by user");
            //    default:
            //        break;
            //}
            //return dialog.Result;
            return null;
        }

        public Task<Result> ResetLocalDataProviderAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result> ValidateConnectionAsync(string connectionString)
        {
            //var dialog = new ValidateConnectionView(connectionString);
            //var res = await dialog.ShowAsync();
            //switch (res)
            //{
            //    case ContentDialogResult.Secondary:
            //        return Result.Ok("Operation canceled by user");
            //    default:
            //        break;
            //}
            //return dialog.Result;
            return null;
        }
    }
}
