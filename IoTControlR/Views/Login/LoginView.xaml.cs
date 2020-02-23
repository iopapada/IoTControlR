using IoTControlR.ViewModels;
using Windows.UI.Xaml.Controls;

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
    }
}
