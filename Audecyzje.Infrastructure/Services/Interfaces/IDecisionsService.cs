using Audecyzje.Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Audecyzje.Infrastructure.Services.Interfaces
{
    public interface IDecisionsService
    {
        Task<List<DecisionDto>> GetAll();
		Task<IEnumerable<DecisionDto>> Search(string query);
		Task<List<DecisionDto>> GetByDecisionNumber(string decisionNumber);
        Task<List<DecisionDto>> GetByDecisionDate(DateTime dateTime);
        Task<List<DecisionDto>> GetByLegalBasis(string legalBasis);
        Task<IEnumerable<DecisionDto>> GetByAddress(string address);
        Task<bool> AddNewDecision(DecisionDto dto);
        Task<DecisionDto> GetFirstUnparsedDecisionNotCachedRepository();
    }
}
