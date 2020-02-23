using System;
using System.Collections.Generic;
using System.Text;

namespace IoTControlR.ViewModels
{
    #region AppLogDetailsArgs
    public class AppLogDetailsArgs
    {
        static public AppLogDetailsArgs CreateDefault() => new AppLogDetailsArgs();

        public long AppLogID { get; set; }
    }
    #endregion
    public class AppLogDetailsVM
    {
    }
}
