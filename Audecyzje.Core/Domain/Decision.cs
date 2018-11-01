using System;
using System.Collections.Generic;

namespace Audecyzje.Core.Domain
{
    public class Decision :BaseEntity
    {
        // Czemu tu jest Localization i Localizations
        public DateTime SubmissionDate { get; set; }
        public string Localization { get; set; }
        public DateTime Date { get; set; }
        public string LegalBasis { get; set; }
		public DateTime UploadedTime { get; set; }
		public IEnumerable<DecisionTag> LinkedTags { get; set; }
		public string SourceLink { get; set; }
		public string DecisionNumber { get; set; }
        public ICollection<Localization> Localizations { get; set; }
        public string Content { get; set; }
	}
}
