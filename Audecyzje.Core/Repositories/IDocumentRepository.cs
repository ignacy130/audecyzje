using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Audecyzje.Core.Domain;

namespace Audecyzje.Core.Repositories
{
    public interface IDocumentRepository : IRepository<Document>
    {
        Task<List<Document>> GetByLocalization(string address);
        Task<List<Document>> GetByDecisionNumber(string decisionNumber);
        Task<List<Document>> GetByDecisionDate(DateTime dateTime);
        Task<List<Document>> GetByLegalBasis(string legalBasis);
    }
}
