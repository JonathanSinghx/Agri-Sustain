using System.Data.SqlClient;

namespace Agrisustain_Jamaica.Data
{
    public class DeleteFromAgrisustainDB
    {
        private readonly IConfiguration _configuration;

        public DeleteFromAgrisustainDB(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public void DeleteData(string databaseTable, Guid id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Agri_Sus")))
            {
                connection.Open();

                string deleteQuery = $"DELETE FROM {databaseTable} WHERE Id = @Id";

                SqlCommand command = new SqlCommand(deleteQuery, connection);
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();
            }
        }

    }
}
