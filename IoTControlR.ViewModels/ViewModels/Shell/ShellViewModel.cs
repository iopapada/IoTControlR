﻿using IoTControlR.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
        private bool _isEnabled = true;
        public bool IsEnabled
        {
            get => _isEnabled;
            set => Set(ref _isEnabled, value);
        }

        private string _message = "Ready";
        public string Message
        {
            get => _message;
            set => Set(ref _message, value);
        }

        private bool _isError = false;
        public bool IsError
        {
            get => _isError;
            set => Set(ref _isError, value);
        }

        public UserInfo UserInfo { get; protected set; }

        public ShellArgs ViewModelArgs { get; protected set; }

        virtual public Task LoadAsync(ShellArgs args)
        {
            ViewModelArgs = args;
            if (ViewModelArgs != null)
            {
                UserInfo = ViewModelArgs.UserInfo;
                NavigationService.Navigate(ViewModelArgs.ViewModel, ViewModelArgs.Parameter);
            }
            return Task.CompletedTask;
        }
    }
}
