using Audecyzje.Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audecyzje.Infrastructure.Services.Interfaces
{
    public interface IDocumentService
    {
        Task<List<DocumentDto>> GetAll();
    }
}
