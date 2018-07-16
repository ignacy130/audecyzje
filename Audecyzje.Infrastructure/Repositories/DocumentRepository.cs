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
    public class DocumentRepository : Repository<Document>, IDocumentRepository
    {
        private readonly AppDbContext _context;
		private IMemoryCache _cache;

		public DocumentRepository(AppDbContext context, IMemoryCache memoryCache) : base(context)
        {
            _context = context;
			_cache = memoryCache;
        }

		enum CacheKeys
		{
			Documents
		}

		public async Task<ICollection<Document>> GetCachedDocuments()
		{
			ICollection<Document> documents;

			if (!_cache.TryGetValue(CacheKeys.Documents, out documents))
			{
				documents = await GetAll();

				var cacheEntryOptions = new MemoryCacheEntryOptions()
											.SetSlidingExpiration(TimeSpan.FromHours(1));

				_cache.Set(CacheKeys.Documents, documents, cacheEntryOptions);
			}

			return documents;
		}

        public async Task<List<Document>> GetByLocalization(string address)
        {
            var documents = await GetCachedDocuments();
            var resultTmpListOfDocuments = new Dictionary<Document, int>();
            var splitedAddress = address.Split(" ", StringSplitOptions.None);
            // wyszukiwanie w kazdym dokumencie
            foreach (var document in documents)
            {
                var isDocumentFitting = false;
                var documentLocalizationsArray = document.Localizations.ToArray();

                int i = 0;
                // Wyszukiwanie w wszystkich adresach danego dokumentu
                while (!isDocumentFitting && i < documentLocalizationsArray.Length)
                {
                    var documentLocalization = documentLocalizationsArray[i];
                    int fitCount = 0;
                    bool isMandatoryFit = false;
                    // Wyszukiwanie w każdej frazie danego adresu
                    foreach (var partOfSplitedAddress in splitedAddress)
                    {
                        if (documentLocalization.Street.ToLower().Contains(partOfSplitedAddress.ToLower()))
                        {
                            fitCount++;
                            isMandatoryFit = true;
                        }
                        if (documentLocalization.PostalCode.ToLower() == (partOfSplitedAddress.ToLower()))
                        {
                            fitCount++;
                            isMandatoryFit = true;
                        }
                        if (documentLocalization.Number.ToLower() == (partOfSplitedAddress.ToLower()))
                        {
                            fitCount++;
                        }
                        
                    }
                    if (isMandatoryFit)
                    {
                        // Jezeli jedna z wymaganych składowych adresu została odnaleziona to dokument ma zostac dodany
                        isDocumentFitting = true;
                        resultTmpListOfDocuments.Add(document,fitCount);
                    }
                    i++;
                }
            }
            var resultList = (resultTmpListOfDocuments
                .Select(e => e)
                .OrderByDescending(o => o.Value)).Select(e => e.Key);
            return resultList.ToList();
        }

        public async Task<List<Document>> GetByDecisionNumber(string decisionNumber)
        {
            var documents = await GetCachedDocuments();
            var searchResult = documents
                                        .Where(doc => doc.DecisionNumber == decisionNumber)
                                        .Select(doc => doc)
                                        .DefaultIfEmpty()
                                        .ToList();
            return searchResult;
        }

        public async Task<List<Document>> GetByDecisionDate(DateTime dateTime)
        {
            var documents = await GetCachedDocuments();
            var searchResult = documents
                                        .Where(doc => doc.Date == dateTime)
                                        .Select(doc => doc)
                                        .DefaultIfEmpty()
                                        .ToList();
            return searchResult;
        }

        public async Task<List<Document>> GetByLegalBasis(string legalBasis)
        {
            var documents = await GetCachedDocuments();
            var searchResult = documents
                                        .Where(doc => doc.LegalBasis == legalBasis)
                                        .Select(doc => doc)
                                        .DefaultIfEmpty()
                                        .ToList();
            return searchResult;
        }
    }
}
