using IoTControlR.ViewModels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace IoTControlR.Services
{
    public class NavigationService : INavigationService
    {
        static private readonly ConcurrentDictionary<Type, Type> _viewModelMap = new ConcurrentDictionary<Type, Type>();

        static public int MainViewId { get; }
        static public void Register<TViewModel, TView>() where TView : Page
        {
            if (!_viewModelMap.TryAdd(typeof(TViewModel), typeof(TView)))
            {
                throw new InvalidOperationException($"ViewModel already registered '{typeof(TViewModel).FullName}'");
            }
        }
        static public Type GetView(Type viewModel)
        {
            if (_viewModelMap.TryGetValue(viewModel, out Type view))
            {
                return view;
            }
            throw new InvalidOperationException($"View not registered for ViewModel '{viewModel.FullName}'");
        }
        public bool IsMainView => CoreApplication.GetCurrentView().IsMain;

        public void Initialize(object frame)
        {
            Frame = frame as Frame;
        }
        public Frame Frame { get; private set; }

        public bool CanGoBack => Frame.CanGoBack;

        public void GoBack() => Frame.GoBack();

        public bool Navigate<TViewModel>(object parameter = null)
        {
            return Navigate(typeof(TViewModel), parameter);
        }
        public bool Navigate(Type viewModelType, object parameter = null)
        {
            if (Frame == null)
            {
                throw new InvalidOperationException("Navigation frame not initialized.");
            }
            return Frame.Navigate(GetView(viewModelType), parameter);
        }
        public async Task<int> CreateNewViewAsync(Type viewModelType, object parameter = null)
        {
            int viewId = 0;

            var newView = CoreApplication.CreateNewView();
            await newView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                viewId = ApplicationView.GetForCurrentView().Id;

                var frame = new Frame();
                var args = new ShellArgs
                {
                    ViewModel = viewModelType,
                    Parameter = parameter
                };
                frame.Navigate(typeof(ShellView), args);

                Window.Current.Content = frame;
                Window.Current.Activate();
            });

            if (await ApplicationViewSwitcher.TryShowAsStandaloneAsync(viewId))
            {
                return viewId;
            }

            return 0;
        }
        public async Task CloseViewAsync()
        {
            int currentId = ApplicationView.GetForCurrentView().Id;
            await ApplicationViewSwitcher.SwitchAsync(MainViewId, currentId, ApplicationViewSwitchingOptions.ConsolidateViews);
        }
    }
}
