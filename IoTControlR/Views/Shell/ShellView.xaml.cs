using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using IoTControlR.ViewModels;
using IoTControlR.Services;

namespace IoTControlR.Views
{
    public sealed partial class ShellView : Page
    {
        public ShellView()
        {
            ViewModel = ServiceLocator.Current.GetService<ShellViewModel>();
            this.InitializeComponent();
            InitializeNavigation();
        }

        public ShellViewModel ViewModel { get; private set; }

        private void InitializeNavigation()
        {
            var navigationService = ServiceLocator.Current.GetService<INavigationService>();
            navigationService.Initialize(frame);
            var appView = ApplicationView.GetForCurrentView();
            //appView.Consolidated += OnViewConsolidated;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await ViewModel.LoadAsync(e.Parameter as ShellArgs);
            //ViewModel.Subscribe();
        }
    }
}
