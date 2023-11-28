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
    }
}