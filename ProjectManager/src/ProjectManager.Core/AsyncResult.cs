using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManager.Core
{
    public class AsyncResult : IAsyncServiceResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public int Result { get; set; }  // May be an ID or count
    }

    public class AsyncResult<T> : AsyncResult, IAsyncServiceResult<T>
    {
        public T Data { get; set; }
    }
}
