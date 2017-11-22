using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audecyzje.Core.Domain
{
    public class Document :BaseEntity
    {
        public string Localization { get; set; }
        public string DecisionNumber { get; set; }
        public DateTime Date { get; set; }
        public string LegalBasis { get; set; }
        public DateTime SubmissionDate { get; set; }
        public ICollection<Localization> Localizations { get; set; }
        public string Content { get; set; }

    }
}
