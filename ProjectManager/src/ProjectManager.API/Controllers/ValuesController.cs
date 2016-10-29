using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Core;
using ProjectManager.Domain;
using ProjectManager.Model.Domain;
using ProjectManager.Model.Presentation;

namespace ProjectManager.API.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : BaseController
    {
        public ValuesController(IServiceClient serviceClient) : base(serviceClient)
        {

        }

        // GET api/values
        [HttpGet][Route("Readme")]
        public IActionResult Readme()
        {
            return View("/Project_Readme.html");
        }
    }
}
