using IoTControlR.Animations;
using IoTControlR.Services;
using IoTControlR.ViewModels;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
//using IoTControlR.Animations;

namespace IoTControlR.Views
{
    public sealed partial class LoginView : Page
    {
        public LoginView()
        {
            ViewModel = ServiceLocator.Current.GetService<LoginViewModel>();
            InitializeComponent();
        }
        public LoginViewModel ViewModel { get; }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            _currentEffectMode = EffectMode.None;
            await ViewModel.LoadAsync(e.Parameter as ShellArgs);
            var navigationService = ServiceLocator.Current.GetService<INavigationService>();
            navigationService.Initialize(Frame);
        }

        private void OnBackgroundFocus(object sender, RoutedEventArgs e)
        {
            //DoEffectIn();
        }

        private void OnForegroundFocus(object sender, RoutedEventArgs e)
        {
            //DoEffectOut();
        }

        private EffectMode _currentEffectMode = EffectMode.None;

        private void DoEffectIn(double milliseconds = 1000)
        {
            if (_currentEffectMode == EffectMode.Foreground || _currentEffectMode == EffectMode.None)
            {
                _currentEffectMode = EffectMode.Background;
                background.Scale(milliseconds, 1.0, 1.1);
                background.Blur(milliseconds, 6.0, 0.0);
                foreground.Scale(500, 1.0, 0.95);
                foreground.Fade(milliseconds, 1.0, 0.75);
            }
        }

        private void DoEffectOut(double milliseconds = 1000)
        {
            if (_currentEffectMode == EffectMode.Background || _currentEffectMode == EffectMode.None)
            {
                _currentEffectMode = EffectMode.Foreground;
                background.Scale(milliseconds, 1.1, 1.0);
                background.Blur(milliseconds, 0.0, 6.0);
                foreground.Scale(500, 0.95, 1.0);
                foreground.Fade(milliseconds, 0.75, 1.0);
            }
        }

        public enum EffectMode
        {
            None,
            Background,
            Foreground,
            Disabled
        }
    }
}
