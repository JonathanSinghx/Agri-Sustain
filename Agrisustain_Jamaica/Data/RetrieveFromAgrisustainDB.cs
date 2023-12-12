using Agrisustain_Jamaica.Models.WeatherForecastDataModels;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace Agrisustain_Jamaica.Data
{
    public class RetrieveFromAgrisustainDB
    {
        private readonly IConfiguration _configuration;

        public RetrieveFromAgrisustainDB(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DataTable GetData(string databaseTable)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Agri_Sus")))
            {
                connection.Open();

                string query = $"SELECT * FROM {databaseTable}";
                //if (!string.IsNullOrEmpty(condition))
                //{
                //    query += $" WHERE {condition}";
                //}

                SqlCommand sqlCommand = new SqlCommand(query, connection);

                //if (parameters != null)
                //{
                //    for (int i = 0; i < parameters.Length; i++)
                //    {
                //        sqlCommand.Parameters.AddWithValue($"@Param{i + 1}", parameters[i] ?? DBNull.Value);
                //    }
                //}

                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                adapter.Fill(dataTable);
            }

            return dataTable;
        }
    }












    //public class RetrieveFromAgrisustainDB
    //{
    //    private readonly IConfiguration? _configuration;

    //    public DataTable GetData(string databaseTable, object[] data)
    //    {
    //        DataTable dataTable = new DataTable();
    //        using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Agri_Sus")))
    //        {
    //            connection.Open();

    //            string query = $"SELECT * FROM {databaseTable}";
    //            SqlCommand sqlCommand = new SqlCommand(query, connection);
    //            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
    //            adapter.Fill(dataTable);
    //        }

    //        return dataTable;
    //    }
    //}
}
