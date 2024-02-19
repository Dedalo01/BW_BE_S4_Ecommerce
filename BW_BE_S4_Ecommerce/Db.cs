namespace BW_BE_S4_Ecommerce
{
    public static class Db
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DBEcommerce"].ToString();
        public static SqlConnection conn = new SqlConnection(connectionString);
    }
}