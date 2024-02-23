using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;


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
            int userId = GetCurrentUserId();
            int cartId;
            if (userId > 0)
            {
                cartId = GetUserCartId(userId);

                try
                {
                    Db.conn.Open();
                    string prodId = Request.QueryString["product"].ToString();
                    string saveProdInCartDb = "INSERT INTO ProdottoInCarrello(CarrelloId, ProdottoId, Quantita) VALUES (@cartId, @prodId, @qt)";
                    SqlCommand saveProductInCartDbCmd = new SqlCommand(saveProdInCartDb, Db.conn);
                    saveProductInCartDbCmd.Parameters.AddWithValue("cartId", cartId);
                    saveProductInCartDbCmd.Parameters.AddWithValue("prodId", prodId);
                    saveProductInCartDbCmd.Parameters.AddWithValue("qt", txtQuantity.Text);

                    saveProductInCartDbCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                finally
                {
                    Db.conn.Close();
                }
            }
            else
            {

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
            }


        }


        private int GetCurrentUserId()
        {
            if (Request.Cookies["UserDetails"] != null)
            {
                HttpCookie user = Request.Cookies["UserDetails"];
                int userId = int.Parse(user["UserId"]);

                return userId;
            }

            return -1;
        }

        private int GetUserCartId(int userId)
        {
            string getUserCartId = @"SELECT c.Id FROM Carrello c
                                            JOIN Utente u ON c.UtenteId = u.Id
                                            WHERE u.Id = @userId";
            try
            {
                Db.conn.Open();
                SqlCommand getUserCartCmd = new SqlCommand(getUserCartId, Db.conn);
                getUserCartCmd.Parameters.AddWithValue("userId", userId);

                int cartId = (int)getUserCartCmd.ExecuteScalar();

                return cartId;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Db.conn.Close();
            }

            return -1;
        }
    }
}
