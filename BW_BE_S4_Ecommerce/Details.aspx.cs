using System;
using System.Collections.Generic;
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
            // if (Log.log == false)
            // {
            // COOKIE

            int prodID;
            if (int.TryParse(ProductID, out prodID))
            {
                // Ottieni la quantità selezionata dall'utente
                int quantity = int.Parse(txtQuantity.Text);

                // Cookie per gli ID dei prodotti
                List<int> products;
                List<string> productQuantities;

                if (Request.Cookies["ProductID"] != null && !string.IsNullOrEmpty(Request.Cookies["ProductID"].Value))
                {
                    string[] productIDs = Request.Cookies["ProductID"].Value.Split(',');
                    products = new List<int>(Array.ConvertAll(productIDs, int.Parse));

                    // Cookie per le quantità
                    string[] quantityValues = Request.Cookies["ProductQuantity"].Value.Split(',');
                    productQuantities = new List<string>(quantityValues);

                    // Controlla se l'ID del prodotto è già presente
                    int index = products.IndexOf(prodID);
                    if (index != -1)
                    {
                        // Se presente, aggiorna la quantità
                        int existingQuantity = int.Parse(productQuantities[index]);
                        productQuantities[index] = (existingQuantity + quantity).ToString();
                    }
                    else
                    {
                        // Se l'ID del prodotto non è già presente, aggiungilo
                        products.Add(prodID);
                        productQuantities.Add(quantity.ToString());
                    }
                }
                else
                {
                    // Se non ci sono ID di prodotti nel cookie, aggiungili semplicemente
                    products = new List<int>();
                    products.Add(prodID);

                    productQuantities = new List<string>();
                    productQuantities.Add(quantity.ToString());
                }

                // Aggiorna i cookie
                Response.Cookies["ProductID"].Value = string.Join(",", products);
                Response.Cookies["ProductID"].Expires = DateTime.Now.AddDays(1);

                Response.Cookies["ProductQuantity"].Value = string.Join(",", productQuantities);
                Response.Cookies["ProductQuantity"].Expires = DateTime.Now.AddDays(1);


            }

            //}
            //else if (Log.log == true)
            //{
            //    try
            //    {
            //        Db.conn.Open();
            //        SqlCommand cmd = new SqlCommand("INSERT INTO ProdottoInCarrello(ProdottoID, Quantita) VALUES (@ProductID, @Quantita)", Db.conn);
            //        cmd.Parameters.AddWithValue("@ProductID", ProductID);

            //        if (int.TryParse(txtQuantity.Text, out int quantita))
            //        {
            //            cmd.Parameters.AddWithValue("@Quantita", quantita);
            //        }
            //        else
            //        {
            //            Response.Write("La quantitá non é un numero valido");
            //            return;
            //        }

            //        int affectedRows = cmd.ExecuteNonQuery();

            //        if (affectedRows > 0)
            //        {
            //            Response.Write("Prodotto aggiunto al carrello con successo!");
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Response.Write("Errore durante l'aggiunta al carrello: " + ex.Message);
            //    }
            //    finally
            //    {
            //        Db.conn.Close();
            //    }
            //}

        }
        //protected void btnDelete_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Db.conn.Open();

        //        // Elimina i record correlati nella tabella ProdottoInCarrello
        //        SqlCommand deleteRelatedCmd = new SqlCommand($"DELETE FROM ProdottoInCarrello WHERE ProdottoId = {ProductID}", Db.conn);
        //        deleteRelatedCmd.ExecuteNonQuery();

        //        // Elimina il prodotto dalla tabella Prodotto
        //        SqlCommand deleteProductCmd = new SqlCommand($"DELETE FROM Prodotto WHERE id = {ProductID}", Db.conn);
        //        int rowsAffected = deleteProductCmd.ExecuteNonQuery();

        //        if (rowsAffected > 0)
        //        {
        //            Response.Write("Prodotto eliminato con successo");

        //        }
        //        else
        //        {
        //            Response.Write("Impossibile eliminare il prodotto.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write(ex.ToString());
        //    }
        //    finally
        //    {
        //        if (Db.conn.State == System.Data.ConnectionState.Open)
        //        {
        //            Db.conn.Close();
        //        }
        //    }
        //}

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect($"EditProduct.aspx?product={ProductID}");
        }
    }
}
