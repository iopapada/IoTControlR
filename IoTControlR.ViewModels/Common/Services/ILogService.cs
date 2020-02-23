using IoTControlR.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoTControlR.Services
{
    public interface ILogService
    {
        Task WriteAsync(LogType type, string source, string action, string message, string description);
        Task WriteAsync(LogType type, string source, string action, Exception ex);
        //Task<AppLogModel> GetLogAsync(long id);
        //Task<IList<AppLogModel>> GetLogsAsync(DataReq<AppLog> request);
        //Task<IList<AppLogModel>> GetLogsAsync(int skip, int take, DataReq<AppLog> request);
        //Task<int> GetLogsCountAsync(DataReq<AppLog> request);

        //Task<int> DeleteLogAsync(AppLogModel model);
        //Task<int> DeleteLogRangeAsync(int index, int length, DataReq<AppLog> request);

        //Task MarkAllAsReadAsync();
    }
}
