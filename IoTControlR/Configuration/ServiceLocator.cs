using System;
using System.Collections.Concurrent;
using IoTControlR.Services;

using Microsoft.Extensions.DependencyInjection;
using Windows.UI.ViewManagement;

namespace IoTControlR
{
    public class ServiceLocator : IDisposable
    {
        static private readonly ConcurrentDictionary<int, ServiceLocator> _serviceLocators = new ConcurrentDictionary<int, ServiceLocator>();

        static private ServiceProvider _rootServiceProvider = null;
        static public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ISettingsService, SettingsService>();

            serviceCollection.AddSingleton<ILogService, LogService>();
            serviceCollection.AddSingleton<ILoginService, LoginService>();
            serviceCollection.AddScoped<INavigationService, NavigationService>();

            //serviceCollection.AddTransient<LoginViewModel>();
            //serviceCollection.AddTransient<DashboardViewModel>();

            //serviceCollection.AddTransient<AppLogsViewModel>();
            //serviceCollection.AddTransient<SettingsViewModel>();

            _rootServiceProvider = serviceCollection.BuildServiceProvider();
        }
        static public ServiceLocator Current
        {
            get
            {
                int currentViewId = ApplicationView.GetForCurrentView().Id;
                return _serviceLocators.GetOrAdd(currentViewId, key => new ServiceLocator());
            }
        }

        static public void DisposeCurrent()
        {
            int currentViewId = ApplicationView.GetForCurrentView().Id;
            if (_serviceLocators.TryRemove(currentViewId, out ServiceLocator current))
            {
                current.Dispose();
            }
        }
        private IServiceScope _serviceScope = null;

        private ServiceLocator()
        {
            _serviceScope = _rootServiceProvider.CreateScope();
        }
        public T GetService<T>()
        {
            return GetService<T>(true);
        }

        public T GetService<T>(bool isRequired)
        {
            if (isRequired)
            {
                return _serviceScope.ServiceProvider.GetRequiredService<T>();
            }
            return _serviceScope.ServiceProvider.GetService<T>();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_serviceScope != null)
                {
                    _serviceScope.Dispose();
                }
            }
        }
    }
}
