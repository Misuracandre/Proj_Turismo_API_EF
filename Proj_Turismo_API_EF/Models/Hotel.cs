namespace Proj_Turismo_API_EF.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Address? Address { get; set; }
        public decimal Value { get; set; }
        public DateTime Registration_Date { get; set; }
    }
}
