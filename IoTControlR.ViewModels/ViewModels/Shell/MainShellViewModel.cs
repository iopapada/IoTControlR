using IoTControlR.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoTControlR.ViewModels
{
    public class MainShellViewModel : ShellViewModel
    {
        public MainShellViewModel(ILoginService loginService, ICommonServices commonServices) : base(loginService, commonServices)
        {
            IsLocked = !loginService.IsAuthenticated;
        }
        public async void NavigateTo(Type viewModel)
        {
            switch (viewModel.Name)
            {
                case "DashboardViewModel":
                    NavigationService.Navigate(viewModel);
                    break;
                case "AppLogsViewModel":
                    NavigationService.Navigate(viewModel, new AppLogListArgs());
                    //await LogService.MarkAllAsReadAsync();
                    //await UpdateAppLogBadge();
                    break;
                case "SettingsViewModel":
                    //NavigationService.Navigate(viewModel, new SettingsArgs());
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
