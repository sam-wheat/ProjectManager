using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Model.Presentation
{
    public abstract class BasePresentationModel : IPresentationModel
    {
        public string TmpID { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsModified { get; set; }

        public BasePresentationModel()
        {
            TmpID = Guid.NewGuid().ToString();
        }
    }
}
