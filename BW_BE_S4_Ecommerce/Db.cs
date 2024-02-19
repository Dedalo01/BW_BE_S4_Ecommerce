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
        public static string connectionString = ConfigurationManager.ConnectionStrings["DBEcommerce"].ToString();
        public static SqlConnection conn = new SqlConnection(connectionString);
    }
}