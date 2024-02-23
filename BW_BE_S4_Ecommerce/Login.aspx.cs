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

        protected void Annulla_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }


        protected void Login_Click(object sender, EventArgs e)
        {
            string email = EmailBox.Text;
            string password = passwordBox.Text;

            try
            {
                Db.conn.Open();

                string query = "SELECT Id, Email, RuoloId FROM Utente WHERE Email = @Email AND Password = @Password";

                using (SqlCommand cmd = new SqlCommand(query, Db.conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        int userId = (int)reader["Id"];
                        string userEmail = reader["Email"].ToString();
                        int ruoloId = (int)reader["RuoloId"];

                        reader.Close();

                        string insertCarrelloQuery = "IF NOT EXISTS (SELECT 1 FROM Carrello WHERE UtenteId = @UtenteId) " +
                                              "INSERT INTO Carrello (UtenteId) VALUES (@UtenteId)";

                        using (SqlCommand cmdcarrello = new SqlCommand(insertCarrelloQuery, Db.conn))
                        {
                            cmdcarrello.Parameters.AddWithValue("@UtenteId", userId);
                            cmdcarrello.ExecuteNonQuery();
                        }

                        Db.conn.Close();

                        if (ruoloId == 1)
                        {
                            Log._admin = true;
                        }

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

                        // L'email non esiste nel database, mostra un pulsante per reindirizzare all'iscrizione
                        pnlEmailExistsMessage.Visible = true;

                        Label3.Text = "Dati Errati";
                    }
                }
            }
            catch (Exception ex)
            {
                Label3.Text = "Si è verificato un errore durante il tentativo di accesso: " + ex.Message;
            }
        }
    }
}