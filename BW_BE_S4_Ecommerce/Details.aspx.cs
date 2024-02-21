using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;


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

            if (Log.log == false)
            {
                // COOKIE

                int prodID;
                if (int.TryParse(ProductID, out prodID))
                {
                    List<int> products;

                    if (Request.Cookies["ProductID"] == null || string.IsNullOrEmpty(Request.Cookies["ProductID"].Value))
                    {
                        products = new List<int>();
                    }
                    else
                    {
                        string[] productIDs = Request.Cookies["ProductID"].Value.Split(',');
                        products = new List<int>();

                        foreach (string id in productIDs)
                        {
                            int parsedID;
                            if (int.TryParse(id, out parsedID))
                            {
                                products.Add(parsedID);
                            }
                            else
                            {
                                // Gestisci il caso in cui il valore non sia un numero intero valido
                            }
                        }
                    }

                    products.Add(prodID);

                    Response.Cookies["ProductID"].Value = string.Join(",", products);
                    Response.Cookies["ProductID"].Expires = DateTime.Now.AddDays(1);
                }

                //// Session
                //int prodID = int.Parse(ProductID);
                //List<int> products;

                //if (Session["ProductID"] == null)
                //{
                //    products = new List<int>();
                //}
                //else
                //{
                //    products = (List<int>)Session["ProductID"];
                //}

                //products.Add(prodID);

                //Session["ProductID"] = products;
            }
            else if (Log.log == true)
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
            
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Db.conn.Open();

                // Elimina i record correlati nella tabella ProdottoInCarrello
                SqlCommand deleteRelatedCmd = new SqlCommand($"DELETE FROM ProdottoInCarrello WHERE ProdottoId = {ProductID}", Db.conn);
                deleteRelatedCmd.ExecuteNonQuery();

                // Elimina il prodotto dalla tabella Prodotto
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
