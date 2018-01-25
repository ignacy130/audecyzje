using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Audecyzje.Infrastructure.Dtos;
using Audecyzje.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Audecyzje.API.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private IDocumentService _documentService;

        public HomeController(IDocumentService documentService)
        {
            _documentService = documentService;
        }
        [HttpGet]
        public async Task<IEnumerable<DocumentDto>> Get()
        {
            return await _documentService.GetAll();
        }

    }
}
