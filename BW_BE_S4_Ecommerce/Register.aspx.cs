using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BW_BE_S4_Ecommerce
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String nome = TextBox1.Text;
            String cognome = TextBox2.Text;
            String email = TextBox3.Text;
            String password = TextBox4.Text;
            String username = TextBox5.Text;

            try
            {
                Db.conn.Open();

                string query = "INSERT INTO Utente (nome, Cognome, Email, Password, Username) VALUES (@Nome, @Cognome, @Email, @Password, @Username)";

                using (SqlCommand cmd = new SqlCommand(query, Db.conn))
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
            finally
            {
                Db.conn.Close();
            }
        }
    }
}