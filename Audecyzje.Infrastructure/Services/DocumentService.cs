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
    }
}
