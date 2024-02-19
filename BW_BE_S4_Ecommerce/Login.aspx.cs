using System;
using System.Data.SqlClient;

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


            Db.conn.Open();

            string query = "SELECT COUNT(*) FROM Utente WHERE Username= @username AND Password = @Password";


            using (SqlCommand cmd = new SqlCommand(query, Db.conn))
            {
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);



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