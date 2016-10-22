using System;
using System.Collections.Generic;
using System.Linq;
using ProjectManager.Model.Domain;
using ProjectManager.Model.Presentation;

namespace ProjectManager.Domain
{
    public interface IPresentationModel : IDisposable
    {
        string TmpID { get; set; }
        bool IsDeleted { get; set; }
        bool IsModified { get; set; }
    }
}