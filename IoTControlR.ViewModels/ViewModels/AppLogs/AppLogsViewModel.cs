using IoTControlR.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoTControlR.ViewModels
{
    public class AppLogsViewModel : ViewModelBase
    {
        public AppLogsViewModel(ICommonServices commonServices) : base(commonServices)
        {
            AppLogList = new AppLogListVM();
            AppLogDetails = new AppLogDetailsVM();
        }

        public AppLogListVM AppLogList { get; }
        public AppLogDetailsVM AppLogDetails { get; }
    }
}
