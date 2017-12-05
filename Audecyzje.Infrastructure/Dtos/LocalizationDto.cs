using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audecyzje.Infrastructure.Dtos
{
    public class LocalizationDto
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string PostalCode { get; set; }
        public int DocumentId { get; set; }
    }
}
