using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Audecyzje.API.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        public HomeController()
        {
            
        }
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "" };
        }

    }
}
