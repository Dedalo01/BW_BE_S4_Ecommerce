using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BW_BE_S4_Ecommerce
{
    public partial class Login1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login_Click(object sender, EventArgs e)
        {
            string username = usernameBox.Text;
            string password = passwordBox.Text;



            using (SqlConnection conn = new SqlConnection(Db.conn.ConnectionString))
            {
                conn.Open();

                string query = "SELECT COUNT(*) FROM Utente WHERE Username= @username AND Password = @Password";


                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("username", username);
                    cmd.Parameters.AddWithValue("password", password);  



                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        Response.Redirect("Home.aspx");
                    }
                    else
                    {
                        Label3.Text = "Dati Errati";
                    }

                }
            }
        }
    }
}