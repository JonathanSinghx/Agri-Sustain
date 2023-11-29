using System.Text.Json;
using System.Text.Json.Nodes;
namespace Agri_Sustain
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string weather_data = @"{""today_date"": ""25/9/2023 10:06:06 pm"",
            ""temp"": ""33"",
            ""precip"": ""20"",
            ""wind"": ""5 km/h"",
            ""humidity"": ""82""}";
            var j_data = JsonSerializer.Deserialize<JsonObject>(weather_data);
            Assert.IsNotNull(j_data);
            string dt = j_data["today_date"].ToString();
            Assert.AreEqual("25/9/2023 10:06:06 pm", dt);
            Assert.AreEqual("33", j_data["temp"].ToString());
        }
        [TestMethod]
        public void TestMethod2() 
        {
            List<d_fc> d_Fcs = new List<d_fc>();
            d_Fcs.Add(new d_fc { day = "Monday", weather_type = "light rain" });
            d_Fcs.Add(new d_fc { day = "Tuesday", weather_type = "over cast clouds" });
            d_Fcs.Add(new d_fc { day = "Wednesday", weather_type = "broken clouds" });
            d_Fcs.Add(new d_fc { day = "Thursday", weather_type = " clear sky" });
            d_Fcs.Add(new d_fc { day = "Friday", weather_type = "clear sky" });
            d_Fcs.Add(new d_fc { day = "Saturday", weather_type = "moderate rain" });
            d_Fcs.Add(new d_fc { day = "Sunday", weather_type = "over cast clouds" });
            var j_data = JsonSerializer.Serialize(d_Fcs);
            Assert.IsNotNull(j_data);
        }
        public class d_fc
        {
            public string day { get; set; }
            public string weather_type { get; set; }
        }
    }
}