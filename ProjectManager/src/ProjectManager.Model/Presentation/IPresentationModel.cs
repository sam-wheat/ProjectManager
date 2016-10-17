﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManager.Model.Presentation
{
    public interface IPresentationModel
    {
        string TmpID { get; set; }
        bool IsDeleted { get; set; }
        bool IsModified { get; set; }
    }
}