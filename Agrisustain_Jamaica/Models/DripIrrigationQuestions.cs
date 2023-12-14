namespace Agrisustain_Jamaica.Models
{
    public class DripIrrigationQuestions
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public List<string> Options { get; set; }
        public string CorrectAnswer { get; set; }
    }
}
