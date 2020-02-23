using IoTControlR.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace IoTControlR.ViewModels
{
    #region AppLogListArgs
    public class AppLogListArgs
    {
        static public AppLogListArgs CreateEmpty() => new AppLogListArgs { IsEmpty = true };

        public AppLogListArgs()
        {
            OrderByDesc = r => r.DateTime;
        }

        public bool IsEmpty { get; set; }

        public string Query { get; set; }

        //public Expression<Func<AppLog, object>> OrderBy { get; set; }
        public Expression<Func<AppLog, object>> OrderByDesc { get; set; }
    }
    #endregion

    public class AppLogListVM
    {
    }
}
