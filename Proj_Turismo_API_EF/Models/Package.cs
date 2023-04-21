namespace Proj_Turismo_API_EF.Models
{
    public class Package
    {
        public int Id { get; set; }
        public Hotel? Hotel { get; set; }
        public Ticket? Ticket { get; set; }
        public Client? Client { get; set; }
        public decimal Value { get; set; }
        public DateTime Registration_Date { get; set; }
    }
}
