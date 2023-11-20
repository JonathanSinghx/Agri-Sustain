namespace Agrisustain_Jamaica.Models
{
    public class PestDiseaseSubmission
    {
        public int Id { get; set; }
        public string FarmLocation { get; set; }
        public DateTime DateOfSighting { get; set; }
        public string Pest { get; set; }
        public string Disease { get; set; }
        public string Description { get; set; }
        public string PhotoPath { get; set; }
    }
}
