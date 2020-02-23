using System;
using System.Linq.Expressions;

namespace IoTControlR.Data
{
    public class DataReq<T>
    {
        public string Query { get; set; }
        public Expression<Func<T, bool>> Where { get; set; }
    }
}
