using System;
using System.Data.SqlClient;
using System.Web;

namespace BW_BE_S4_Ecommerce
{
    public partial class Login1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["UserDetails"] != null)
            {
                Response.Redirect("Home.aspx");
            }
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            string username = usernameBox.Text;
            string password = passwordBox.Text;

            try
            {
                Db.conn.Open();

                string query = "SELECT Id, Email FROM Utente WHERE Username = @username AND Password = @Password";

                using (SqlCommand cmd = new SqlCommand(query, Db.conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        int userId = (int)reader["Id"];
                        string userEmail = reader["Email"].ToString();

                        reader.Close();

                        Db.conn.Close();

                        HttpCookie userCookie = new HttpCookie("UserDetails");
                        userCookie["UserId"] = userId.ToString();
                        userCookie["UserEmail"] = userEmail;

                        userCookie.Expires = DateTime.Now.AddDays(1); 

                        Response.Cookies.Add(userCookie);

                        Log.log = true;

                        Response.Redirect("Home.aspx");
                    }
                    else
                    {
                        reader.Close();

                        Db.conn.Close();

                        Label3.Text = "Dati Errati";
                    }
                }
            }
            catch (Exception ex)
            {
                Label3.Text = "Si è verificato un errore durante il tentativo di accesso.";
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            if (Request.Cookies["UserDetails"] != null)
            {
                HttpCookie userCookie = Request.Cookies["UserDetails"];
                userCookie.Expires = DateTime.Now.AddDays(-21);
                Response.Cookies.Add(userCookie);
            }
            Response.Redirect("Login.aspx");
        }
    }
}