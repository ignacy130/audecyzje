using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audecyzje.Infrastructure.Dtos
{
    public class DocumentDto
    {
        public string Localization { get; set; }
        public string DecisionNumber { get; set; }
        public DateTime Date { get; set; }
        public string LegalBasis { get; set; }
        public DateTime SubmissionDate { get; set; }
        public ICollection<LocalizationDto> Localizations { get; set; }
        public string Content { get; set; }
    }
}
