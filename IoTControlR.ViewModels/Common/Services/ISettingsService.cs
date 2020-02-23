using IoTControl;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IoTControlR.Services
{
    public enum DataProviderType
    {
        SQLServer,
        WebAPI
    }
    public interface ISettingsService
    {
        string Version { get; }
        string DbVersion { get; }
        string UserName { get; set; }
        DataProviderType DataProvider { get; set; }
        string SQLServerConnectionString { get; set; }
        Task<Result> ResetLocalDataProviderAsync();
        Task<Result> ValidateConnectionAsync(string connectionString);
        Task<Result> CreateDabaseAsync(string connectionString);
    }
}
