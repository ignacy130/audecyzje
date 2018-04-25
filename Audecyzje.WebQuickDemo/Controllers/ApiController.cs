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
        public JsonResult Get(int id)
        {
			var l = _context.Descisions.SingleOrDefault(x => x.ID == id);

			return new JsonResult(l);
        }

		[Route("getpage")]
		public JsonResult GetPage(int number)
		{
			var l = _context.Descisions.Skip(number*100).Take(100).ToList();

			return new JsonResult(l);
		}


		[HttpGet]
        [Route("alldecisions")]
        public JsonResult GetAll()
        {
            return new JsonResult(_context.Descisions.ToList());
        }
    }
}
