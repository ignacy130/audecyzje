using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Audecyzje.WebQuickDemo.Models
{
    public class Decision
    {
        public int ID { get; set; }
        public DateTime SubmissionDate { get; set; }
        public IEnumerable<DecisionTag> LinkedTags { get; set; }
        public string Content { get; set; }
        public string DecisionNumber { get; set; }
        public ICollection<Localization> Localizations { get; set; }
    }
}
