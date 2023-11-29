namespace Agrisustain_Jamaica.Models.WeatherForecastDataModels
{
    public class WeatherTriggersModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string WeatherCondition { get; set; }
        public int Level { get; set; }
        public string Condition { get; set; }
        public string Units { get; set; }
        public int Length { get; set; }
    }

    public class SavedTriggerEventsModel
    {
        List<WeatherTriggersModel> triggerEvents = new List<WeatherTriggersModel>();
    }
}
