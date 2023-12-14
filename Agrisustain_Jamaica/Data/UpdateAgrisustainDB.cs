using Agrisustain_Jamaica.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace Agrisustain_Jamaica.Data
{
    public class UpdateAgrisustainDB
    {
        private readonly IConfiguration _configuration;

        public UpdateAgrisustainDB(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public void UpdateData(string databaseTable, Dictionary<string, object> fieldData, Guid id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Agri_Sus")))
            {
                connection.Open();

                //SET clause dynamically while ensuring that the field names and their values are properly handled through parameters. This significantly reduces the risk of SQL injection by separating the SQL command from the values being passed.
                string setClause = string.Join(", ", fieldData.Select((pair, i) => $"{pair.Key} = @Param{i + 1}"));
                string updateQuery = $"UPDATE {databaseTable} SET {setClause} WHERE Id = @Id";

                SqlCommand command = new SqlCommand(updateQuery, connection);

                foreach (var pair in fieldData.Select((value, index) => new { index, value }))
                {
                    command.Parameters.AddWithValue($"@Param{pair.index + 1}", pair.value.Value ?? DBNull.Value);
                }

                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();
                //connection.Open();

                //string updateQuery = $"UPDATE {databaseTable} SET " +
                //                     string.Join(", ", fieldData.Select((pair, i) => $"{pair.Key} = @Param{i + 1}")) +
                //                     " WHERE Id = @Id";

                //SqlCommand command = new SqlCommand(updateQuery, connection);
                //foreach (var pair in fieldData.Select((value, index) => new { index, value }))
                //{
                //    command.Parameters.AddWithValue($"@Param{pair.index + 1}", pair.value.Value ?? DBNull.Value);
                //}
                //command.Parameters.AddWithValue("@Id", id);

                //command.ExecuteNonQuery();
            }
        }


        //public void UpdateData(string databaseTable, object[] data)
        //{
        //    using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Agri_Sus")))
        //    {
        //        connection.Open();

        //        string updateQuery = $"UPDATE {databaseTable} SET " +
        //             $"{string.Join(", ", Enumerable.Range(0, data.Length).Select(i => $"FieldName{i + 1} = @Param{i + 1}"))} " +
        //             $"WHERE Id = @Id";

        //        SqlCommand command = new SqlCommand(updateQuery, connection);
        //        for (int i = 0; i < data.Length; i++)
        //        {
        //            command.Parameters.AddWithValue($"@Param{i + 1}", data[i] ?? DBNull.Value);
        //            //command.Parameters.AddWithValue($"@Param{i + 1}" , data[i]);
        //        }

        //        command.ExecuteNonQuery();

        //    }
        //}
    }
}

