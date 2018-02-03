using Audecyzje.Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Audecyzje.Infrastructure.Services.Interfaces
{
    public interface IDocumentService
    {
        Task<List<DocumentDto>> GetAll();
        Task<List<DocumentDto>> GetByDecisionNumber(string decisionNumber);
        Task<List<DocumentDto>> GetByDecisionDate(DateTime dateTime);
        Task<List<DocumentDto>> GetByLegalBasis(string legalBasis);
        Task<IEnumerable<DocumentDto>> GetByAddress(string address);
    }
}
