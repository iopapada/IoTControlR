using System.Threading.Tasks;

namespace IoTControlR.Services
{
    public class LoginService : ILoginService
    {
        public LoginService(IMessageService messageService, IDialogService dialogService)
        {
            IsAuthenticated = false;
            MessageService = messageService;
            DialogService = dialogService;
        }
        public IMessageService MessageService { get; }
        public IDialogService DialogService { get; }
        public Task<bool> SignInWithPasswordAsync(string userName, string password)
        {
            // Perform authentication here.
            // This sample accepts any user name and password.
            UpdateAuthenticationStatus(true);
            return Task.FromResult(true);
        }
        public bool IsAuthenticated { get; set; }
        public void Logoff()
        {
            UpdateAuthenticationStatus(false);
        }

        private void UpdateAuthenticationStatus(bool isAuthenticated)
        {
            IsAuthenticated = isAuthenticated;
            MessageService.Send(this, "AuthenticationChanged", IsAuthenticated);
        }
    }
}
