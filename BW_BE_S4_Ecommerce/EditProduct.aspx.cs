using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace BW_BE_S4_Ecommerce
{
    public partial class EditProduct : System.Web.UI.Page
    {
        private string ProductId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["product"] == null)
                {
                    Response.Redirect("Home.aspx");
                }
                else
                {
                    ProductId = Request.QueryString["product"].ToString();

                    try
                    {
                        Db.conn.Open();

                        SqlCommand cmd = new SqlCommand($"SELECT * FROM Prodotto WHERE Id= {ProductId}", Db.conn);
                        SqlDataReader dataReader = cmd.ExecuteReader();

                        if (dataReader.HasRows)
                        {
                            dataReader.Read();
                            TxtNome.Text = dataReader["Nome"].ToString();
                            TxtDescrizione.Text = dataReader["Descrizione"].ToString();
                            TxtPrezzo.Text = dataReader["Prezzo"].ToString();
                            TxtImmagineUrl.Text = dataReader["ImmagineUrl"].ToString();
                        }
                        dataReader.Close();
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.ToString());
                    }
                    finally
                    {
                        Db.conn.Close();
                    }
                }
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            ProductId = Request.QueryString["product"].ToString();

            if (ProductId == null)
            {
                Response.Write("ProductId non è stato inizializzato correttamente");
                return;
            }
            decimal prezzo;
            
            if (TxtNome.Text.Length < 3 || TxtNome.Text.Length > 50 )
            {
                LblErrore.Text = "Il nome del prodotto deve essere compreso tra 3 e 50 caratteri.";
                return;
            }
            if (TxtDescrizione.Text.Length > 300)
            {
                LblErrore.Text = "La descrizione del del prodotto deve essere minore di 300 caratteri.";
                return;
            }
            
            if (!decimal.TryParse(TxtPrezzo.Text, out prezzo) || prezzo <= 0)
            {
                LblErrore.Text = "Il prezzo deve essere un numero maggiore di zero.";
                return;
            }
            if (string.IsNullOrWhiteSpace(TxtImmagineUrl.Text))
            {
                
                TxtImmagineUrl.Text = "https://t4.ftcdn.net/jpg/04/73/25/49/360_F_473254957_bxG9yf4ly7OBO5I0O5KABlN930GwaMQz.jpg"; 
            }

            try
            {
                Db.conn.Open();

                string updateQuery = $@"UPDATE Prodotto
                                         SET Nome = @Nome,
                                             Descrizione = @Descrizione,
                                             Prezzo = @Prezzo,
                                             ImmagineUrl = @ImmagineUrl
                                         WHERE Id = {ProductId}";

                using (SqlCommand cmd = new SqlCommand(updateQuery, Db.conn))
                {
                    cmd.Parameters.AddWithValue("@Nome", TxtNome.Text);
                    cmd.Parameters.AddWithValue("@Descrizione", TxtDescrizione.Text);
                    cmd.Parameters.AddWithValue("@Prezzo", decimal.Parse(TxtPrezzo.Text));
                    cmd.Parameters.AddWithValue("@ImmagineUrl", TxtImmagineUrl.Text);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected != 0)
                    {
                        Response.Write("Aggiornamento Effettuato");
                    }
                    else
                    {
                        Response.Write("Problemi durante l'aggiornamento");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
            finally
            {
                Db.conn.Close();
            }
        }
    }
}
