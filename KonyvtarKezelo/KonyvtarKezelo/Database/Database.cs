using MySql.Data.MySqlClient;

namespace KonyvtarKezelo.Database
{
    public class Database
    {
        private static string connectionString =
            "server=localhost;database=konyvtar;uid=root;pwd=;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}