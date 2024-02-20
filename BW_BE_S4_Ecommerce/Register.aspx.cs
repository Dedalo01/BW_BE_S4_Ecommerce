using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace BW_BE_S4_Ecommerce
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["UserDetails"] != null)
            {
                Response.Redirect("Home.aspx");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String nome = TextBox1.Text;
            String cognome = TextBox2.Text;
            String email = TextBox3.Text;
            String password = TextBox4.Text;
            String username = TextBox6.Text;

            try
            {
                Db.conn.Open();

                if (nome.Length > 2 || cognome.Length > 2 || username.Length > 2)
                {
                    if(IsValidEmail(email))
                    {
                        if (TextBox4.Text == TextBox5.Text)
                        {
                            string checkEmail = "SELECT  COUNT(*) FROM Utente WHERE Email = @Email";
                            using (SqlCommand checkCmd = new SqlCommand(checkEmail, Db.conn))
                            {
                                checkCmd.Parameters.AddWithValue("@Email", email);
                                int count = (int)checkCmd.ExecuteScalar();
                                if (count > 0)
                                {
                                    pnlEmailExistsMessage.Visible = true;
                                    return;
                                }
                            }
                        }
                        else
                        {
                            Response.Write("La password non corrisponde, ritenta");
                            return;
                        }
                    }
                    else
                    {
                        Response.Write("L'email non è valida");
                    }
                  
                }
                else
                {
                    
                   Response.Write("Dati non validi, Nome,Cognome e Username non possono essere più corti di 2 caratteri. ");
                    return;
                    
                }
                  



                string insertQuery = "INSERT INTO Utente (nome, Cognome, Email, Password, Username) VALUES (@Nome, @Cognome, @Email, @Password, @Username)";
                using (SqlCommand cmd = new SqlCommand(insertQuery, Db.conn))
                {
                    cmd.Parameters.AddWithValue("@Nome", nome);
                    cmd.Parameters.AddWithValue("@Cognome", cognome);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.Parameters.AddWithValue("@Username", username);

                    cmd.ExecuteNonQuery();
                }

                TextBox1.Text = "";
                TextBox2.Text = "";
                TextBox3.Text = "";
                TextBox4.Text = "";
                TextBox5.Text = "";
            }
            catch (Exception err)
            {
                Response.Write("Errore: " + err.Message);
            }
            finally
            {
                Db.conn.Close();
            }
        }


        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            return Regex.IsMatch(email, pattern);
        }
    }
}