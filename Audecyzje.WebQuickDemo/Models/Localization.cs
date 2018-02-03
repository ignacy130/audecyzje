namespace Audecyzje.WebQuickDemo.Models
{
    public class Localization
    {
        public int ID { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Decision Decision { get; set; }
        public int DecisionId { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string PostalCode { get; set; }

    }
}