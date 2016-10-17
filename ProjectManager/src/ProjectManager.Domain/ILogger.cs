using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManager.Domain
{
    public interface ILogger
    {
        void LogInfo(string message);
        void LogError(string message);
        void LogException(string message, Exception ex);
    }
}
