using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audecyzje.Core.Domain
{
    public class Localization : BaseEntity
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string PostalCode { get; set; }
        public int DocumentId { get; set; }

        [NotMapped]
        public string FullAddressString => $"{City} {Street} {Number} {PostalCode}";

		public double Latitude { get; set; }
		public double Longitude { get; set; }
	}
}
