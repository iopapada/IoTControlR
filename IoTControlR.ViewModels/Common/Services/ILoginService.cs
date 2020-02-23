using System.Threading.Tasks;

namespace IoTControlR.Services
{
    public interface ILoginService
    {
        bool IsAuthenticated { get; set; }

        Task<bool> SignInWithPasswordAsync(string userName, string password);

        void Logoff();
    }
}
