using System;
using System.Data.SqlClient;

namespace BW_BE_S4_Ecommerce
{
    public partial class Details : System.Web.UI.Page
    {
        private string ProductID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["product"] == null)
            {
                Response.Redirect("Home.aspx");
            }

            ProductID = Request.QueryString["product"];

            try
            {
                Db.conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Prodotto WHERE id = @ProductID", Db.conn);
                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.HasRows)
                {
                    dataReader.Read();
                    txtProduct.InnerText = dataReader["Nome"].ToString();
                    img.Src = dataReader["ImmagineUrl"].ToString();
                    txtDescription.InnerText = dataReader["Descrizione"].ToString();
                    txtPrice.InnerText = $"{dataReader["Prezzo"]}€";
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
            finally
            {
                if (Db.conn.State == System.Data.ConnectionState.Open)
                {
                    Db.conn.Close();
                }
            }
        }

        protected void btnAddCart_Click(object sender, EventArgs e)
        {
            try
            {
                Db.conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO ProdottoInCarrello(ProdottoID, Quantita) VALUES (@ProductID, @Quantita)", Db.conn);
                cmd.Parameters.AddWithValue("@ProductID", ProductID);

                if (int.TryParse(txtQuantity.Text, out int quantita))
                {
                    cmd.Parameters.AddWithValue("@Quantita", quantita);
                }
                else
                {
                    Response.Write("La quantitá non é un numero valido");
                    return;
                }

                int affectedRows = cmd.ExecuteNonQuery();

                if (affectedRows > 0)
                {
                    Response.Write("Prodotto aggiunto al carrello con successo!");
                }
            }
            catch (Exception ex)
            {
                Response.Write("Errore durante l'aggiunta al carrello: " + ex.Message);
            }
            finally
            {
                Db.conn.Close();
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Db.conn.Open();
                SqlCommand cmd = new SqlCommand($"DELETE FROM Prodotto WHERE id={ProductID}", Db.conn);
                SqlDataReader dataReader = cmd.ExecuteReader();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Response.Write("Prodotto eliminato con successso");
                    Response.Redirect("Home.aspx");
                }
                else
                {

                    Response.Write("Impossibile eliminare il prodotto.");
                }

            }
            catch (Exception ex)
            {

                Response.Write(ex.ToString());
            }
            finally
            {
                if (Db.conn.State == System.Data.ConnectionState.Open)
                {
                    Db.conn.Close();
                }
            }



        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect($"EditProduct.aspx?product={ProductID}");
        }
    }
}
