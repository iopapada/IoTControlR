using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTControlR.Data.DbContexts
{
    public class AppLogDb : DbContext
    {
        private string _connectionString = null;

        public AppLogDb(string connectionString)
        {
            _connectionString = connectionString;
        }
        public DbSet<AppLog> Logs { get; set; }

        public async Task<AppLog> GetLogAsync(long id)
        {
            return await Logs.Where(r => r.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> CreateLogAsync(AppLog appLog)
        {
            appLog.DateTime = DateTime.UtcNow;
            Entry(appLog).State = EntityState.Added;
            return await SaveChangesAsync();
        }

        public async Task<int> DeleteLogsAsync(params AppLog[] logs)
        {
            Logs.RemoveRange(logs);
            return await SaveChangesAsync();
        }
    }
}
