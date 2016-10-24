using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManager.Core
{
    public interface IAsyncServiceResult
    {
        bool Success { get; set; }
        string ErrorMessage { get; set; }
        int Result { get; set; }
    }

    public interface IAsyncServiceResult<T> : IAsyncServiceResult
    {
        T Data { get; set; }
    }
}
