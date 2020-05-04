using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.ViewManagement;

using IoTControlR.Services;
using IoTControlR.ViewModels;
using IoTControlR.Views;

namespace IoTControlR
{
    static public class Startup
    {
        static private readonly ServiceCollection _serviceCollection = new ServiceCollection();

        static public async Task ConfigureAsync()
        {
            ServiceLocator.Configure(_serviceCollection);

            ConfigureNavigation();

            await EnsureLogDbAsync();
            //await EnsureDatabaseAsync();
            //await ConfigureLookupTables();

            var logService = ServiceLocator.Current.GetService<ILogService>();
            await logService.WriteAsync(Data.LogType.Information, "Startup", "Configuration", "Application Start", $"Application started.");

            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(500, 500));
        }

        private static void ConfigureNavigation()
        {
            NavigationService.Register<LoginViewModel, LoginView>();
            NavigationService.Register<ShellViewModel, ShellView>();
            NavigationService.Register<MainShellViewModel, MainShellView>();

            NavigationService.Register<DashboardViewModel, DashboardView>();
            NavigationService.Register<AppLogsViewModel, AppLogsView>();
            NavigationService.Register<SettingsViewModel, SettingsView>();

            //Organization Cycle
            //NavigationService.Register<CustomersViewModel, CustomersView>();
            //NavigationService.Register<SuppliersViewModel, SuppliersView>();
            //NavigationService.Register<ProductsViewModel, ProductsView>();

            //Automations Cycle
            //NavigationService.Register<TemperaturesViewModel, TemperaturesView>();
            //NavigationService.Register<WScalesViewModel, WScalesView>();
            //NavigationService.Register<LightsViewModel, LightsView>();

            //Security Cycle
            //NavigationService.Register<LockersViewModel, LockersView>();
            //NavigationService.Register<VehiclesViewModel, VehiclesView>();
            //NavigationService.Register<CCTVsViewModel, CCTVsView>();

        }
        static private async Task EnsureLogDbAsync()
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            var appLogFolder = await localFolder.CreateFolderAsync(AppSettings.AppLogPath, CreationCollisionOption.OpenIfExists);
            if (await appLogFolder.TryGetItemAsync(AppSettings.AppLogName) == null)
            {
                var sourceLogFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/AppLog/AppLog.db"));
                var targetLogFile = await appLogFolder.CreateFileAsync(AppSettings.AppLogName, CreationCollisionOption.ReplaceExisting);
                await sourceLogFile.CopyAndReplaceAsync(targetLogFile);
            }
        }
    }
}
