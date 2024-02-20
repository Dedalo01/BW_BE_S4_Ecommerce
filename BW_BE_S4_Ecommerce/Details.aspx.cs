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
                
                if (int.TryParse(txtQuantity.Text, out int quantita) && quantita > 0) 
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO ProdottoInCarrello(ProdottoID, Quantita) VALUES (@ProductID, @Quantita)", Db.conn);
                    cmd.Parameters.AddWithValue("@ProductID", ProductID);
                    cmd.Parameters.AddWithValue("@Quantita", quantita);

                    int affectedRows = cmd.ExecuteNonQuery();

                    if (affectedRows > 0)
                    {
                        Response.Write("Prodotto aggiunto al carrello con successo!");
                    }
                }
                else
                {
                    Response.Write("La quantità non è un numero valido o è uguale a zero.");
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

           
                SqlCommand checkRelatedCmd = new SqlCommand($"SELECT COUNT(*) FROM ProdottoInCarrello WHERE ProdottoId = {ProductID}", Db.conn);
                int relatedCount = (int)checkRelatedCmd.ExecuteScalar();

                if (relatedCount > 0)
                {
                
                    Response.Write("Ci sono elementi nel carrello correlati a questo prodotto. L'eliminazione comporterà la rimozione di tali elementi dal carrello. Continuare con l'eliminazione?");

                   
                    SqlCommand deleteProductCmd = new SqlCommand($"DELETE FROM Prodotto WHERE id = {ProductID}", Db.conn);
                    int rowsAffected = deleteProductCmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        
                        Response.Write("Prodotto eliminato con successo");
                        Response.Redirect("Home.aspx");
                    }
                    else
                    {
                        Response.Write("Impossibile eliminare il prodotto.");
                    }
                }
                else
                {
                  
                    SqlCommand deleteProductCmd = new SqlCommand($"DELETE FROM Prodotto WHERE id = {ProductID}", Db.conn);
                    int rowsAffected = deleteProductCmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        
                        Response.Write("Prodotto eliminato con successo");
                        
                    }
                    else
                    {
                        Response.Write("Impossibile eliminare il prodotto.");
                    }
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
