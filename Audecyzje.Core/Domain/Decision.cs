using System;
using System.Collections.Generic;

namespace Audecyzje.Core.Domain
{
    public class Decision :BaseEntity
    {
        // Czemu tu jest Localization i Localizations
        // bo w założeniu od MJN może sie pojawić sytuacja że pojedyncza decyzja dotyczy więcej niż jednego adresu
        public DateTime SubmissionDate { get; set; }
        public DateTime Date { get; set; }
        public string LegalBasis { get; set; }
		public DateTime UploadedTime { get; set; }
		public IEnumerable<DecisionTag> LinkedTags { get; set; }
		public string SourceLink { get; set; }
		public string DecisionNumber { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string Street { get; set; }
		public string Number { get; set; }
		public string PostalCode { get; set; }

		public string Localization { get; set; }
        public ICollection<Localization> Localizations { get; set; }

        public string Content { get; set; }
	}
}
