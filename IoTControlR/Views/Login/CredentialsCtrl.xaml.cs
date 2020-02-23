using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace IoTControlR.Views
{
    public sealed partial class CredentialsCtrl : UserControl
    {
        public CredentialsCtrl()
        {
            this.InitializeComponent();
        }
        public string UserName
        {
            get { return (string)GetValue(UserNameProperty); }
            set { SetValue(UserNameProperty, value); }
        }
        public static readonly DependencyProperty UserNameProperty = DependencyProperty.Register(nameof(UserName), typeof(string), typeof(CredentialsCtrl), new PropertyMetadata(null));

        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }
        public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register(nameof(Password), typeof(string), typeof(CredentialsCtrl), new PropertyMetadata(null));
        public ICommand LoginCommand
        {
            get { return (ICommand)GetValue(LoginCommandProperty); }
            set { SetValue(LoginCommandProperty, value); }
        }

        public static readonly DependencyProperty LoginCommandProperty = DependencyProperty.Register(nameof(LoginCommand), typeof(ICommand), typeof(CredentialsCtrl), new PropertyMetadata(null));
    }
}
