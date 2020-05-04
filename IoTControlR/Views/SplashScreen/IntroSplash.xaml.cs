using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using IoTControlR.ViewModels;
using IoTControlR.Services;

namespace IoTControlR.Views.SplashScreen
{
    public sealed partial class IntroSplash : Page
    {
        internal Rect splashImageRect; // Rect to store splash screen image coordinates.
        private Windows.ApplicationModel.Activation.SplashScreen splashScreen; // Variable to hold the splash screen object.
        private Frame rootFrame;
        private IActivatedEventArgs activatedEventArgs;
        public IntroSplash(IActivatedEventArgs e, bool loadState)
        {
            this.InitializeComponent();

            Window.Current.SizeChanged += new WindowSizeChangedEventHandler(ExtendedSplash_OnResize);
            splashScreen = e.SplashScreen;
            this.activatedEventArgs = e;

            if (splashScreen != null)
            {
                // Retrieve the window coordinates of the splash screen image.
                splashImageRect = splashScreen.ImageLocation;
            }

            Resize();
            rootFrame = new Frame();
            LoadDataAsync(this.activatedEventArgs);
        }
        private async void LoadDataAsync(IActivatedEventArgs e)
        {
            var activationInfo = ActivationService.GetActivationInfo(e);

            await Startup.ConfigureAsync();

            var shellArgs = new ShellArgs
            {
                ViewModel = activationInfo.EntryViewModel,
                Parameter = activationInfo.EntryArgs,
                UserInfo = await TryGetUserInfoAsync(e as IActivatedEventArgsWithUser)
            };

            rootFrame.Navigate(typeof(LoginView), shellArgs);
            Window.Current.Content = rootFrame;
            Window.Current.Activate();
        }

        private void Resize()
        {
            if (this.splashScreen == null) return;

            this.introsplashImage.Height = this.splashScreen.ImageLocation.Height;
            this.introsplashImage.Width = this.splashScreen.ImageLocation.Width;

            this.introsplashImage.SetValue(Canvas.TopProperty, this.splashScreen.ImageLocation.Top);
            this.introsplashImage.SetValue(Canvas.LeftProperty, this.splashScreen.ImageLocation.Left);

            this.progressRing.SetValue(Canvas.TopProperty, this.splashScreen.ImageLocation.Top + this.splashScreen.ImageLocation.Height + 50);
            this.progressRing.SetValue(Canvas.LeftProperty, this.splashScreen.ImageLocation.Left + this.splashScreen.ImageLocation.Width / 2 - this.progressRing.Width / 2);
        }

        void ExtendedSplash_OnResize(Object sender, WindowSizeChangedEventArgs e)
        {
            // Safely update the extended splash screen image coordinates. This function will be fired in response to snapping, unsnapping, rotation, etc...
            if (splashScreen != null)
            {
                // Update the coordinates of the splash screen image.
                splashImageRect = splashScreen.ImageLocation;
                Resize();
            }
        }

        private async Task<UserInfo> TryGetUserInfoAsync(IActivatedEventArgsWithUser argsWithUser)
        {
            if (argsWithUser != null)
            {
                var user = argsWithUser.User;
                var userInfo = new UserInfo
                {
                    AccountName = await user.GetPropertyAsync(KnownUserProperties.AccountName) as String,
                    FirstName = await user.GetPropertyAsync(KnownUserProperties.FirstName) as String,
                    LastName = await user.GetPropertyAsync(KnownUserProperties.LastName) as String
                };
                if (!userInfo.IsEmpty)
                {
                    if (String.IsNullOrEmpty(userInfo.AccountName))
                    {
                        userInfo.AccountName = $"{userInfo.FirstName} {userInfo.LastName}";
                    }
                }
            }
            return UserInfo.Default;
        }
    }
}
