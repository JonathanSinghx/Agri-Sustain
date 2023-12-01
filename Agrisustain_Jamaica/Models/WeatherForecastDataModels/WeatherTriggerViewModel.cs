namespace Agrisustain_Jamaica.Models.WeatherForecastDataModels
{
    public class WeatherTriggerViewModel
    {
        public string TriggerName { get; set; }
        //public string Location { get; set; }
        public string WeatherCondition { get; set; }
        public int ConditionLevel { get; set; }
        public string Condition { get; set; }
        public string Units { get; set; }
        public int Duration { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public class SavedTriggerEventsModel
    {
        public List<WeatherTriggerViewModel> triggerEvents = new List<WeatherTriggerViewModel>();
    }
}
