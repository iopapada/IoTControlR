using IoTControlR.Services;
using IoTControlR.ViewModels;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace IoTControlR.Views
{

    public sealed partial class MainShellView : Page
    {
        private INavigationService _navigationService = null;
        public MainShellView()
        {
            ViewModel = ServiceLocator.Current.GetService<MainShellViewModel>();
            InitializeComponent();
            InitializeNavigation();
        }
        public MainShellViewModel ViewModel { get; }
        private void InitializeNavigation()
        {
            _navigationService = ServiceLocator.Current.GetService<INavigationService>();
            _navigationService.Initialize(frame);
            frame.Navigated += OnFrameNavigated;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await ViewModel.LoadAsync(e.Parameter as ShellArgs);
            //ViewModel.Subscribe();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            //ViewModel.Unload();
            //ViewModel.Unsubscribe();
        }
        private void OnFrameNavigated(object sender, NavigationEventArgs e)
        {
            var targetType = NavigationService.GetViewModel(e.SourcePageType);
            switch (targetType.Name)
            {
                case "SettingsViewModel":
                    ViewModel.SelectedItem = navigationView.SettingsItem;
                    break;
                default:
                    ViewModel.SelectedItem = ViewModel.Items.Where(r => r.ViewModel == targetType).FirstOrDefault();
                    break;
            }
            //UpdateBackButton();
        }
        private void OnNavigationViewBackButton(object sender, RoutedEventArgs e)
        {
            if (_navigationService.CanGoBack)
            {
                _navigationService.GoBack();
            }
        }

        private async void OnLogoff(object sender, RoutedEventArgs e)
        {
            var dialogService = ServiceLocator.Current.GetService<IDialogService>();
            if (await dialogService.ShowAsync("Confirm logoff", "Are you sure you want to logoff?", "Ok", "Cancel"))
            {
                var loginService = ServiceLocator.Current.GetService<ILoginService>();
                loginService.Logoff();
                if (Frame.CanGoBack)
                {
                    Frame.GoBack();
                }
            }
        }
    }
}
