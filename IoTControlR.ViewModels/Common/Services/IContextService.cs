using System;
using System.Threading.Tasks;

namespace IoTControlR.Services
{
    public interface IContextService
    {
        int MainViewID { get; }
        int ContextID { get; }
        bool IsMainView { get; }
        void Initialize(object dispatcher, int contextID, bool isMainView);
        Task RunAsync(Action action);
    }
}
