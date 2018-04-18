using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Audecyzje.WebQuickDemo.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Audecyzje.WebQuickDemo.Controllers
{
    [Produces("application/json")]
    [Route("api/")]
    public class ApiController : Controller
    {
        private readonly WarsawContext _context;

        public ApiController(WarsawContext context)
        {
            _context = context;
        }
        
        //GET: api/Api/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(string id)
        {
            return JsonConvert.SerializeObject(_context.Descisions.Where(d=> d.DecisionNumber == id));
        }

        [HttpGet]
        [Route("alldecisions")]
        public string GetAll()
        {
            return JsonConvert.SerializeObject(_context.Descisions.ToList());
        }
    }
}
