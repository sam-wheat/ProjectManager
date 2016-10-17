using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManager.Core
{
    public interface IAsyncResult
    {
        bool Success { get; set; }
        string ErrorMessage { get; set; }
        int ResultCount { get; set; }
    }

    public interface IAsyncResult<T> : IAsyncResult
    {
        T Data { get; set; }
    }
}
