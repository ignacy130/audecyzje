using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Audecyzje.WebQuickDemo.Models
{
    public class DecisionTag
    {
        public int ID { get; set; }
        public int DecisionID { get; set; }
        public int TagID { get; set; }
        
        public Tag Tag { get; set; }
        public Decision Decision { get; set; }
    }
}
