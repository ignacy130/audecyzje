using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Audecyzje.Core.Domain;
using Audecyzje.Core.Repositories;
using Audecyzje.Infrastructure.DatabaseContext;
using Microsoft.Extensions.Caching.Memory;

namespace Audecyzje.Infrastructure.Repositories
{
	public class DecisionsRepository : Repository<Decision>, IDecisionsRepository
	{
		private readonly WarsawContext _context;
		private IMemoryCache _cache;

		public DecisionsRepository(WarsawContext context, IMemoryCache memoryCache) : base(context)
		{
			_context = context;
			_cache = memoryCache;
		}

		enum CacheKeys
		{
			Documents
		}

		public async Task<ICollection<Decision>> GetCachedDocuments()
		{
			ICollection<Decision> documents;

			if (!_cache.TryGetValue(CacheKeys.Documents, out documents))
			{
				documents = await GetAll();

				var cacheEntryOptions = new MemoryCacheEntryOptions()
											.SetSlidingExpiration(TimeSpan.FromSeconds(1));

				_cache.Set(CacheKeys.Documents, documents, cacheEntryOptions);
			}

			return documents;
		}

		public async Task<List<Decision>> GetByLocalization(string address)
		{
			var documents = await GetCachedDocuments();
			var resultTmpListOfDocuments = new List<Tuple<Decision, int>>();
			var splitedAddress = address.Split(" ", StringSplitOptions.None).ToList();
			splitedAddress.Remove("ul.");
			splitedAddress.Remove("al.");
			// wyszukiwanie w kazdym dokumencie
			foreach (var document in documents)
			{
				if (!string.IsNullOrEmpty(document.Address))
				{
					var dist = document.Address.LevenshteinDistanceTo(address);
					if (dist < 10)
					{
						resultTmpListOfDocuments.Add(new Tuple<Decision, int>(document, dist));
					}
				}
			}

			var resultList = (resultTmpListOfDocuments
				.Select(e => e)
				.OrderByDescending(o => o.Item2)).Select(e => e.Item1);
			return resultList.ToList();
		}

		public async Task<List<Decision>> GetByDecisionNumber(string decisionNumber)
		{
			var documents = await GetCachedDocuments();
			var searchResult = documents
										.Where(doc => doc.DecisionNumber == decisionNumber)
										.Select(doc => doc)
										.DefaultIfEmpty()
										.ToList();
			return searchResult;
		}

		public async Task<List<Decision>> GetByDecisionDate(DateTime dateTime)
		{
			var documents = await GetCachedDocuments();
			var searchResult = documents
										.Where(doc => doc.Date == dateTime)
										.Select(doc => doc)
										.DefaultIfEmpty()
										.ToList();
			return searchResult;
		}

		public async Task<List<Decision>> GetByLegalBasis(string legalBasis)
		{
			var documents = await GetCachedDocuments();
			var searchResult = documents
										.Where(doc => doc.LegalBasis == legalBasis)
										.Select(doc => doc)
										.DefaultIfEmpty()
										.ToList();
			return searchResult;
		}
        public Decision GetFirstEmptyDecisionNotCached()
        {
            var documents = _context.Decisions.ToList();
            var decision = documents.Where(doc => string.IsNullOrEmpty(doc.Content)).FirstOrDefault();
            return decision;
        }
    }
}
