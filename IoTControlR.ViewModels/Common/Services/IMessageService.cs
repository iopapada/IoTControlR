using System;
using System.Collections.Generic;
using System.Text;

namespace IoTControlR.Services
{
    public interface IMessageService
    {
        void Send<TSender, TArgs>(TSender sender, string message, TArgs args) where TSender : class;
    }
}
