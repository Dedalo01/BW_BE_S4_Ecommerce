using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BW_BE_S4_Ecommerce
{
    public static class Db
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DbEcommerce"].ToString();
        public static SqlConnection conn = new SqlConnection(connectionString);
    }
}