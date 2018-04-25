using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Audecyzje.Core.Repositories;
using Audecyzje.Infrastructure.Dtos;
using Audecyzje.Infrastructure.Services.Interfaces;
using AutoMapper;
using Audecyzje.Core.Domain;
using System.Text.RegularExpressions;

namespace Audecyzje.Infrastructure.Services
{
    public class DocumentService : Service, IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IMapper _mapper;

        public DocumentService(IDocumentRepository documentRepository, IMapper mapper)
        {
            _documentRepository = documentRepository;
            _mapper = mapper;
        }

        public async Task<List<DocumentDto>> GetAll()
        {
           return _mapper.Map<List<DocumentDto>>(await _documentRepository.GetAll());
        }

        public async Task<List<DocumentDto>> GetByDecisionNumber(string decisionNumber)
        {
            return _mapper.Map<List<DocumentDto>>(await _documentRepository.GetByDecisionNumber(decisionNumber));
        }

        public async Task<List<DocumentDto>> GetByDecisionDate(DateTime dateTime)
        {
            return _mapper.Map<List<DocumentDto>>(await _documentRepository.GetByDecisionDate(dateTime));
        }

        public async Task<List<DocumentDto>> GetByLegalBasis(string legalBasis)
        {
            return _mapper.Map<List<DocumentDto>>(await _documentRepository.GetByLegalBasis(legalBasis));
        }

        public async Task<IEnumerable<DocumentDto>> GetByAddress(string address)
        {
            var listOfDocuments = await _documentRepository.GetByLocalization(address);
            return _mapper.Map<List<DocumentDto>>(listOfDocuments);
        }

		public async Task<IEnumerable<DocumentDto>> SearchInContent(string query)
		{
			var listOfDocuments = (await _documentRepository.GetAll()).Where(d => d.Content.Contains(query));
			return _mapper.Map<List<DocumentDto>>(listOfDocuments);
		}

		public async Task<IEnumerable<DocumentDto>> Search(string query)
		{
			var decisionNumberRegex = new Regex(@"[0-9]{1,100}\/GK\/DW\/[0-9]{4}");
			if (decisionNumberRegex.IsMatch(query))
			{
				return await GetByDecisionNumber(query);
			}
			else 
			{
				var addressResults = await GetByAddress(query);
				var fulltextResults = await SearchInContent(query);
				addressResults = addressResults.Concat(fulltextResults);
				return addressResults;
			}
		}
	}
}
