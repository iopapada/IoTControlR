using IoTControlR.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoTControlR.ViewModels
{
    public class ShellArgs
    {
        public Type ViewModel { get; set; }
        public object Parameter { get; set; }
        public UserInfo UserInfo { get; set; }
    }

    public class ShellViewModel : ViewModelBase
    {
        public ShellViewModel(ILoginService loginService, ICommonServices commonServices) : base(commonServices)
        {
            IsLocked = !loginService.IsAuthenticated;
        }
        private bool _isLocked = false;
        public bool IsLocked
        {
            get => _isLocked;
            set => Set(ref _isLocked, value);
        }
    }
}
