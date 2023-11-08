namespace Agrisustain_Jamaica.Models
{
    public class Badge
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }

    }

    public class BadgeCriteria
    {
        public string Name { get; set; }
        public int RequiredCount { get; set; }
    }

    public class BadgeProgress
    {
        public string BadgeName { get; set; }
        public int DaysUsed { get; set; }
        public string ProgressMessage { get; set; }
    }
}
