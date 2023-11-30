namespace Agrisustain_Jamaica.Models.WeatherForecastDataModels
{
    public class WeatherTriggerModel
    {
        public Guid Id { get; set; }
        public string TriggerName { get; set; }
        //public string Location { get; set; }
        public string WeatherCondition { get; set; }
        public int ConditionLevel { get; set; }
        public string Condition { get; set; }
        public string Units { get; set; }
        public int Duration { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    //public class SavedTriggerEventsModel
    //{
    //    List<WeatherTriggerModel> triggerEvents = new List<WeatherTriggerModel>();
    //    public void AddTriggerEvent(WeatherTriggerModel triggerEvent)
    //    {
    //        triggerEvents.Add(triggerEvent);
    //    }

    //}
}
