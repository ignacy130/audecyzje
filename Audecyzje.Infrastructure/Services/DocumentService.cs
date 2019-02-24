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
    public class DocumentService : Service, IDecisionsService
    {
        private readonly IDecisionsRepository _documentRepository;
        private readonly IMapper _mapper;

        public DocumentService(IDecisionsRepository documentRepository, IMapper mapper)
        {
            _documentRepository = documentRepository;
            _mapper = mapper;
        }

        public async Task<List<DecisionDto>> GetAll()
        {
            return _mapper.Map<List<DecisionDto>>(await _documentRepository.GetAll());
        }

        public async Task<List<DecisionDto>> GetByDecisionNumber(string decisionNumber)
        {
            return _mapper.Map<List<DecisionDto>>(await _documentRepository.GetByDecisionNumber(decisionNumber));
        }

        public async Task<List<DecisionDto>> GetByDecisionDate(DateTime dateTime)
        {
            return _mapper.Map<List<DecisionDto>>(await _documentRepository.GetByDecisionDate(dateTime));
        }

        public async Task<List<DecisionDto>> GetByLegalBasis(string legalBasis)
        {
            return _mapper.Map<List<DecisionDto>>(await _documentRepository.GetByLegalBasis(legalBasis));
        }

        public async Task<IEnumerable<DecisionDto>> GetByAddress(string address)
        {
            var listOfDocuments = await _documentRepository.GetByLocalization(address.ToLower());
            return _mapper.Map<List<DecisionDto>>(listOfDocuments);
        }

        public async Task<IEnumerable<DecisionDto>> SearchInContent(string query)
        {
            var listOfDocuments = (await _documentRepository.GetAll()).Where(d => d.Content.Contains(query));
            return _mapper.Map<List<DecisionDto>>(listOfDocuments);
        }

        public async Task<IEnumerable<DecisionDto>> Search(string query)
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

        Task<bool> IDecisionsService.AddNewDecision(DecisionDto dto)
        {
            //TODO nie jestem pewien jak dziala cache documentow na razie zrobilem na _context

            throw new NotImplementedException();
        }

        public async Task<DecisionDto> GetFirstUnparsedDecisionNotCachedRepository()
        {
            var decision = _documentRepository.GetFirstEmptyDecisionNotCached();
            return _mapper.Map<DecisionDto>(decision);
        }
    }
}
