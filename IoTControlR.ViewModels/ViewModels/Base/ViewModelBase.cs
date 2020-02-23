using IoTControlR.Data;
using IoTControlR.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoTControlR.ViewModels
{
    public class ViewModelBase : ObservableObject
    {
        public ViewModelBase(ICommonServices commonServices)
        {
            ContextService = commonServices.ContextService;
            NavigationService = commonServices.NavigationService;
            MessageService = commonServices.MessageService;
            DialogService = commonServices.DialogService;
            LogService = commonServices.LogService;
        }
        public IContextService ContextService { get; }
        public INavigationService NavigationService { get; }
        public IMessageService MessageService { get; }
        public IDialogService DialogService { get; }
        public ILogService LogService { get; }

        public bool IsMainView => ContextService.IsMainView;
        virtual public string Title => String.Empty;

        public async void LogInformation(string source, string action, string message, string description)
        {
            await LogService.WriteAsync(LogType.Information, source, action, message, description);
        }
        public async void LogWarning(string source, string action, string message, string description)
        {
            await LogService.WriteAsync(LogType.Warning, source, action, message, description);
        }
        public void LogException(string source, string action, Exception exception)
        {
            LogError(source, action, exception.Message, exception.ToString());
        }
        public async void LogError(string source, string action, string message, string description)
        {
            await LogService.WriteAsync(LogType.Error, source, action, message, description);
        }
    }
}
