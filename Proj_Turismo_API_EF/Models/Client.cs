namespace Proj_Turismo_API_EF.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public Address? Address { get; set; }
        public  DateTime Registration_Date { get; set; }
    }
}
