using System.Collections.Generic;
using System.Threading.Tasks;
using Audecyzje.Infrastructure.Dtos;
using Audecyzje.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Audecyzje.Client.Controllers
{
    [Route("api/[controller]")]
    public class DecisionsController : Controller
    {
        private readonly IDecisionsService _decisionsService;

        public DecisionsController(IDecisionsService documentService)
        {
            _decisionsService = documentService;
        }
        
        [HttpGet]
        public async Task<IEnumerable<DecisionDto>> Get()
        {
            return await _decisionsService.GetAll();
        }

        [HttpGet]
        [Route("search")]
        public async Task<IEnumerable<DecisionDto>> Search(string query)
        {
            return await _decisionsService.Search(query);
        }

        [HttpGet("GetByDecisionNumber/{number}")]
		public async Task<IEnumerable<DecisionDto>> GetByDecisionNumber(string number)
		{
			return await _decisionsService.GetByDecisionNumber(number);
		}

		[HttpGet("GetByLegalBasis/{legalBasis}")]
        public async Task<IEnumerable<DecisionDto>> GetByLegalBasis(string legalBasis)
        {
            return await _decisionsService.GetByLegalBasis(legalBasis);
        }

        [HttpGet("GetByAddress/{address}")]
        public async Task<IEnumerable<DecisionDto>> GetByAddress(string address)
        {
            return await _decisionsService.GetByAddress(address);
        }


    }
}
