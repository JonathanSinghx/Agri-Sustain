using Agrisustain_Jamaica.Models.WeatherForecastDataModels;
using System.Data;
using System.Data.SqlClient;

namespace Agrisustain_Jamaica.Data
{
    public class AddToAgrisustainDB
    {
        private readonly IConfiguration _configuration;

        public AddToAgrisustainDB(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public void AddData(string databaseTable, object[] data)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Agri_Sus")))
            {
                connection.Open();
              //  string paramPlaceholders = string.Join(", ", Enumerable.Range(0, data.Length).Select(i => $"@Param{i + 1}"));
               // string query = $"INSERT INTO {databaseTable} VALUES ({paramPlaceholders})";

                string query = $"INSERT INTO {databaseTable} VALUES ({string.Join(", ", Enumerable.Range(0, data.Length).Select(i => $"@Param{i + 1}"))})";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    for (int i = 0; i < data.Length; i++)
                    {
                        command.Parameters.AddWithValue($"@Param{i + 1}", data[i] ?? DBNull.Value);
                        //command.Parameters.AddWithValue($"@Param{i + 1}", data[i]);
                    }

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
