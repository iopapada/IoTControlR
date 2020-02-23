using IoTControl;
using IoTControlR.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace IoTControlR.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel(ILoginService loginService, ISettingsService settingsService, ICommonServices commonServices) : base(commonServices)
        {
            LoginService = loginService;
            SettingsService = settingsService;
        }

        public ILoginService LoginService { get; }
        public ISettingsService SettingsService { get; }
        private ShellArgs ViewModelArgs { get; set; }

        private bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { Set(ref _isBusy, value); }
        }
        private string _userName = null;
        public string UserName
        {
            get { return _userName; }
            set { Set(ref _userName, value); }
        }

        private string _password = "UserPassword";
        public string Password
        {
            get { return _password; }
            set { Set(ref _password, value); }
        }
        public ICommand LoginCommand => new RelayCommand(Login);
        public async void Login()
        {
            IsBusy = true;
            var result = ValidateInput();
            if (result.IsOk)
            {
                if (await LoginService.SignInWithPasswordAsync(UserName, Password))
                {
                    SettingsService.UserName = UserName;
                    EnterApplication();
                    return;
                }
            }
            await DialogService.ShowAsync(result.Message, result.Description);
            IsBusy = false;
        }
        private void EnterApplication()
        {
            if (ViewModelArgs.UserInfo.AccountName != UserName)
            {
                ViewModelArgs.UserInfo = new UserInfo
                {
                    AccountName = UserName,
                    FirstName = UserName,
                    //PictureSource = null
                };
            }
            NavigationService.Navigate<MainShellViewModel>(ViewModelArgs);
        }
        private Result ValidateInput()
        {
            if (String.IsNullOrWhiteSpace(UserName))
            {
                return Result.Error("Login error", "Please, enter a valid user name.");
            }
            if (String.IsNullOrWhiteSpace(Password))
            {
                return Result.Error("Login error", "Please, enter a valid password.");
            }
            return Result.Ok();
        }
    }
}
