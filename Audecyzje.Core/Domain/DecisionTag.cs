using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Audecyzje.Core.Domain
{
    public class DecisionTag
    {
        public int Id { get; set; }
        public int DecisionId { get; set; }
        public int TagId { get; set; }
        
        public Tag Tag { get; set; }
        public Decision Decision { get; set; }
    }
}
