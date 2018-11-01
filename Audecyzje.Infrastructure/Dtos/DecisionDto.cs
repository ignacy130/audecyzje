using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audecyzje.Infrastructure.Dtos
{
    public class DecisionDto
    {
		public DateTime SubmissionDate { get; set; }
		public string Localization { get; set; }
		public DateTime Date { get; set; }
		public string LegalBasis { get; set; }
		public DateTime UploadedTime { get; set; }
		public string SourceLink { get; set; }
		public string DecisionNumber { get; set; }
		public ICollection<LocalizationDto> Localizations { get; set; }
		public string Content { get; set; }
	}
}
