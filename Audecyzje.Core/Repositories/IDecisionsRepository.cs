using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Audecyzje.Core.Domain;

namespace Audecyzje.Core.Repositories
{
    public interface IDecisionsRepository : IRepository<Decision>
    {
        Task<List<Decision>> GetByLocalization(string address);
        Task<List<Decision>> GetByDecisionNumber(string decisionNumber);
        Task<List<Decision>> GetByDecisionDate(DateTime dateTime);
        Task<List<Decision>> GetByLegalBasis(string legalBasis);
        Decision GetFirstEmptyDecisionNotCached();
    }

	public interface IPostsRepository : IRepository<Post>
	{

	}
}
