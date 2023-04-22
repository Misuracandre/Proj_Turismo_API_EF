namespace Proj_Turismo_API_EF.Models
{
    public class Package
    {
        public int Id { get; set; }
        public virtual Hotel? Hotel { get; set; }
        public virtual Ticket? Ticket { get; set; }
        public virtual Client? Client { get; set; }
        public decimal Value { get; set; }
        public DateTime Registration_Date { get; set; }
    }
}
