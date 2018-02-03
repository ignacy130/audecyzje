using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Audecyzje.Core.Domain;
using Audecyzje.Core.Repositories;
using Audecyzje.Infrastructure.DatabaseContext;

namespace Audecyzje.Infrastructure.Repositories
{
    public class DocumentRepository : Repository<Document>, IDocumentRepository
    {
        private readonly AppDbContext _context;

        public DocumentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Document>> GetByLocalization(string address)
        {
            var documents = await GetAll();
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
                        if (documentLocalization.Street.ToLower().Contains(partOfSplitedAddress))
                        {
                            fitCount++;
                            isMandatoryFit = true;
                        }
                        if (documentLocalization.PostalCode.ToLower() == (partOfSplitedAddress))
                        {
                            fitCount++;
                            isMandatoryFit = true;
                        }
                        if (documentLocalization.Number.ToLower() == (partOfSplitedAddress))
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
            var documents = await GetAll();
            var searchResult = documents
                                        .Where(doc => doc.DecisionNumber == decisionNumber)
                                        .Select(doc => doc)
                                        .DefaultIfEmpty()
                                        .ToList();
            return searchResult;
        }

        public async Task<List<Document>> GetByDecisionDate(DateTime dateTime)
        {
            var documents = await GetAll();
            var searchResult = documents
                                        .Where(doc => doc.Date == dateTime)
                                        .Select(doc => doc)
                                        .DefaultIfEmpty()
                                        .ToList();
            return searchResult;
        }

        public async Task<List<Document>> GetByLegalBasis(string legalBasis)
        {
            var documents = await GetAll();
            var searchResult = documents
                                        .Where(doc => doc.LegalBasis == legalBasis)
                                        .Select(doc => doc)
                                        .DefaultIfEmpty()
                                        .ToList();
            return searchResult;
        }
    }
}
