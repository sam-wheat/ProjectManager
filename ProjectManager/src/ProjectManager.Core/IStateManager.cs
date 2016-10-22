using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManager.Core
{
    public interface IStateManager
    {
        int CurrentUserID { get; set; }
        string CurrentUserName { get; set; }
        ILogger Logger { get; }
        string StatusBarMessage { get; set; }
        void SetDockManager(object manager);
    }
}
