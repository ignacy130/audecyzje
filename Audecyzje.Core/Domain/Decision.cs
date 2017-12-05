using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audecyzje.Core.Domain
{
    public class Decision :BaseEntity
    {
        public bool IsApproved { get; set; }
        public Document Document { get; set; }
        public string Content { get; set; }


    }
}
