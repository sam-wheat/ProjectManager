using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
namespace ProjectManager.Core
{
    public class BootstrapHelper
    {
        // todo: implement this


        public static void VerifyApplicationDirectories(string dir)
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            
        }
    }
}
