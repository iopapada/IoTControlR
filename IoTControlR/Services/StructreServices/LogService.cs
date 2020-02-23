using System;
using System.Threading.Tasks;
using IoTControlR.Data;
using IoTControlR.Data.DbContexts;

namespace IoTControlR.Services
{
    public class LogService : ILogService
    {
        public LogService(IMessageService messageService)
        {
            MessageService = messageService;
        }
        public IMessageService MessageService { get; }
        public async Task WriteAsync(LogType type, string source, string action, Exception ex)
        {
            await WriteAsync(LogType.Error, source, action, ex.Message, ex.ToString());
            Exception deepException = ex.InnerException;
            while (deepException != null)
            {
                await WriteAsync(LogType.Error, source, action, deepException.Message, deepException.ToString());
                deepException = deepException.InnerException;
            }
        }
        public async Task WriteAsync(LogType type, string source, string action, string message, string description)
        {
            var appLog = new AppLog()
            {
                User = AppSettings.Current.UserName ?? "App",
                Type = type,
                Source = source,
                Action = action,
                Message = message,
                Description = description,
            };

            appLog.IsRead = type != LogType.Error;

            await CreateLogAsync(appLog);
            MessageService.Send(this, "LogAdded", appLog);
        }
        private AppLogDb CreateDataSource()
        {
            return new AppLogDb(AppSettings.Current.AppLogConnectionString);
        }

        public async Task<int> CreateLogAsync(AppLog appLog)
        {
            using (var ds = CreateDataSource())
            {
                return await ds.CreateLogAsync(appLog);
            }
        }

    }
}
