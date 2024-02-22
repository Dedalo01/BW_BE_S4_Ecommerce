using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BW_BE_S4_Ecommerce
{
    public partial class InsertProduct : System.Web.UI.Page
    {
        decimal prezzo;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnCrea_Click(object sender, EventArgs e)
        {
            
            
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
                SqlCommand saveProduct = new SqlCommand(@"INSERT INTO Prodotto (Nome, Descrizione, Prezzo, ImmagineUrl)
                                                          VALUES
                                                          (@Nome, @Descrizione, @Prezzo, @ImmagineUrl)", Db.conn);
                                                         saveProduct.Parameters.AddWithValue("@Nome", TxtNome.Text);
                                                         saveProduct.Parameters.AddWithValue("@Descrizione", TxtDescrizione.Text);
                if(double.TryParse(TxtPrezzo.Text, out double prezzo))
                {
                    saveProduct.Parameters.AddWithValue("@Prezzo", prezzo);
                }
                else
                {
                    Response.Write("Prezzo non è un numero valido");
                }
                                                          saveProduct.Parameters.AddWithValue("@ImmagineUrl", TxtImmagineUrl.Text);

                int affectedRows = saveProduct.ExecuteNonQuery();

                if(affectedRows != 0)
                {
                    Response.Write("Dati Inseriti Con Successo");
                }
            }
            catch (Exception ex)
            {
                Response.Write("Qualcosa non va: " + ex.Message);
            }
            finally
            {
                Db.conn.Close();
            }
        }
    }
}