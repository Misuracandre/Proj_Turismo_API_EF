namespace Proj_Turismo_API_EF.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string? Street { get; set; }
        public int? Number { get; set; }
        public string? Neighborhood { get; set; }
        public string? ZipCode { get; set; }
        public string? Extension { get; set; }
        public City? City { get; set; }
        public DateTime Registration_Date { get; set; }
    }
}
