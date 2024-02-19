using System.Configuration;
using System.Data.SqlClient;

namespace BW_BE_S4_Ecommerce
{
    public static class Db
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["DbEcommerce"].ToString();
        public static SqlConnection conn = new SqlConnection(connectionString);
    }
}