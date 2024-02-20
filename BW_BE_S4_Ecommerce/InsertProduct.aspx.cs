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
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnCrea_Click(object sender, EventArgs e)
        {
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