using System;
using System.Collections.Generic;
using System.Text;

namespace IoTControlR.Services
{
    public interface ICommonServices
    {
        IContextService ContextService { get; }
        INavigationService NavigationService { get; }
        IMessageService MessageService { get; }
        IDialogService DialogService { get; }
        ILogService LogService { get; }
    }
}
