using Agrisustain_Jamaica.Models.WeatherForecastDataModels;
using System.Data;
using System.Data.SqlClient;

namespace Agrisustain_Jamaica.Data
{
    public class AddWeatherTriggerDB
    {
        private readonly IConfiguration? _configuration;
        
        public void AddWeatherTrigger(string triggerName, string weatherCondition, int conditionLevel, string condition, string units, int duration, DateTime createdAt)
        {
          
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Agri_Sus"));
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM WEATHERTRIGGER", connection);

            SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("Agri_Sus"));
                SqlCommand createTrigger = new SqlCommand("insert into WEATHERTRIGGER values ('" + Guid.NewGuid() + "','" + triggerName + "', '" + weatherCondition + "','" + conditionLevel + "', '" + condition + "', '" + units + "', '" + duration + "', '" + createdAt + "')", sqlConnection);

                sqlConnection.Open();
                    createTrigger.ExecuteNonQuery();
                    sqlConnection.Close();
        }
       
    }
}
