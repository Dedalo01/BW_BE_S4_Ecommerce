using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace BW_BE_S4_Ecommerce
{

    public partial class ShoppingCart : System.Web.UI.Page
    {

        DataTable dt;
        //int userId;
        //int cartId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //if (Log.log == false)
                //{

                //}
                //else if (Log.log == true)
                //{
                //    //BindCartRepeater();
                //}

                int userId = GetCurrentUserId();

                if (userId > 0)
                {
                    RetrieveDataFromDatabase(userId);

                }
                else
                {

                    RetrieveDataFromSession();
                }
            }
        }

        protected void RetrieveDataFromDatabase(int userId)
        {

            string selectProductForCartQuery = @"SELECT p.Id AS ID, p.Nome, p.Prezzo, pc.Quantita FROM Carrello c
                                 JOIN ProdottoInCarrello pc ON c.Id = pc.CarrelloId
                                 JOIN Prodotto p ON pc.ProdottoId = p.Id
                                 WHERE c.UtenteId = @UtenteId";

            try
            {
                Db.conn.Open();
                SqlCommand cmd = new SqlCommand(selectProductForCartQuery, Db.conn);
                cmd.Parameters.AddWithValue("@UtenteId", userId);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                //SqlDataReader reader = cmd.ExecuteReader();
                //rptCartItems.DataSource = reader;
                //rptCartItems.DataBind();

                rptCartItems.DataSource = dataTable;
                rptCartItems.DataBind();

                double totalAmount = 0;
                foreach (DataRow row in dataTable.Rows)
                {
                    int quantity = Convert.ToInt32(row["Quantita"]);
                    double price = Convert.ToDouble(row["Prezzo"]);

                    double productTotal = quantity * price;
                    totalAmount += productTotal;
                }

                LblPrezzo.Text = totalAmount.ToString();

                //while (reader.Read())
                //{
                //    int quantity = Convert.ToInt32(reader["pc.Quantita"]);
                //    int price = Convert.ToInt32(reader["p.Prezzo"]);

                //    double productTotal = quantity * price;

                //    totalAmount += productTotal;
                //}


                //reader.Close();
                // LblPrezzo.Text = totalAmount.ToString();
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

        protected void RetrieveDataFromSession()
        {
            HttpCookie cookie = Request.Cookies["ProductID"];
            HttpCookie cookieQuantity = Request.Cookies["ProductQuantity"];

            dt = new DataTable();

            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Nome", typeof(string));
            dt.Columns.Add("Prezzo", typeof(double));
            dt.Columns.Add("Quantita", typeof(int));

            if (cookie != null && !string.IsNullOrEmpty(cookie.Value) && cookieQuantity != null && !string.IsNullOrEmpty(cookieQuantity.Value))
            {
                string[] productIds = cookie.Value.Split(',');
                string[] quantities = cookieQuantity.Value.Split(',');

                for (int i = 0; i < productIds.Length; i++)
                {
                    if (int.TryParse(productIds[i], out int id) && int.TryParse(quantities[i], out int quantity))
                    {
                        try
                        {
                            Db.conn.Open();
                            SqlCommand cmd = new SqlCommand($"SELECT * FROM Prodotto WHERE ID=@Id", Db.conn);
                            cmd.Parameters.AddWithValue("@Id", id);
                            SqlDataReader dataReader = cmd.ExecuteReader();

                            if (dataReader.HasRows)
                            {
                                dataReader.Read();
                                dt.Rows.Add(dataReader["ID"], dataReader["Nome"], dataReader["Prezzo"], quantity);
                            }
                            dataReader.Close();
                        }
                        catch (Exception ex)
                        {

                        }
                        finally
                        {
                            Db.conn.Close();
                        }
                    }
                }

            }

            ShoppingCartDataTable.CartTable = dt;
            rptCartItems.DataSource = ShoppingCartDataTable.CartTable;
            rptCartItems.DataBind();
            TotalCartPrice(ShoppingCartDataTable.CartTable);

            // salvo nel db se loggato
            if (Request.Cookies["UserDetails"] != null)
            {
                // Ottieni il valore del cookie
                HttpCookie userCookie = Request.Cookies["UserDetails"];

                // Controlla se ci sono i valori di userId ed email nel cookie
                if (userCookie["UserId"] != null)
                {
                    string userId = userCookie["UserId"];
                    //string userId = "15";

                    string findCartQuery = @"SELECT c.Id FROM Carrello c
                                            JOIN Utente u ON c.UtenteId = u.Id
                                            WHERE u.Id = @userId";

                    try
                    {
                        Db.conn.Open();

                        SqlCommand cmd = new SqlCommand(findCartQuery, Db.conn);
                        cmd.Parameters.AddWithValue("userId", userId);
                        object cartIdObj = cmd.ExecuteScalar();
                        string cartId = Convert.ToString(cartIdObj);

                        string checkIdAlreadyInDbQuery = "SELECT * FROM ProdottoInCarrello WHERE ProdottoId = @idToCheck AND CarrelloId = @cartId";

                        string addCartToDbQuery = "INSERT INTO ProdottoInCarrello (CarrelloId, ProdottoId, Quantita) VALUES (@cartId, @prodId, @qt)";
                        Response.Write(cartId + "<- ID DEL CARRELLO");
                        string updateQuery = @"UPDATE ProdottoInCarrello
                                                        SET Quantita = @quantita
                                                        WHERE CarrelloId = @cartId AND ProdottoId = @prodId";

                        foreach (DataRow row in ShoppingCartDataTable.CartTable.Rows)
                        {
                            SqlCommand checkIdInDb = new SqlCommand(checkIdAlreadyInDbQuery, Db.conn);
                            checkIdInDb.Parameters.AddWithValue("idToCheck", row["ID"].ToString());
                            checkIdInDb.Parameters.AddWithValue("cartId", cartId);

                            object result = checkIdInDb.ExecuteScalar();
                            int? res = Convert.ToInt32(result);
                            //Response.Write("\n\nrisultato di query checkIdInDb -> " + res);
                            //int RowsAffected = checkIdInDb.ExecuteNonQuery();

                            //SqlDataReader checkId = cmd.ExecuteReader();



                            if (res != null && res != 0)
                            {
                                // fai UPDATE della quantità se esiste
                                SqlCommand updateCmd = new SqlCommand(updateQuery, Db.conn);
                                updateCmd.Parameters.AddWithValue("quantita", row["Quantita"].ToString());
                                updateCmd.Parameters.AddWithValue("cartId", cartId);
                                updateCmd.Parameters.AddWithValue("prodId", row["ID"].ToString());

                                updateCmd.ExecuteNonQuery();
                            }
                            else
                            {
                                // inserisci in db se NON esiste
                                SqlCommand insertRowInDb = new SqlCommand(addCartToDbQuery, Db.conn);
                                insertRowInDb.Parameters.AddWithValue("cartId", cartId);
                                insertRowInDb.Parameters.AddWithValue("prodId", row["ID"].ToString());
                                insertRowInDb.Parameters.AddWithValue("qt", row["Quantita"].ToString());

                                insertRowInDb.ExecuteNonQuery();

                            }

                            //checkId.Close();
                            // Response.Write("\n\nRisultato SqlDataReader: " + checkId);

                            //SqlCommand insertRowInDb = new SqlCommand(addCartToDbQuery, Db.conn);
                            //insertRowInDb.Parameters.AddWithValue("cartId", cartId);
                            //insertRowInDb.Parameters.AddWithValue("prodId", row["ID"].ToString());
                            //insertRowInDb.Parameters.AddWithValue("qt", row["Quantita"].ToString());

                            //insertRowInDb.ExecuteNonQuery();
                        }

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
            }
        }



        protected void rptCartItems_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            int userId = GetCurrentUserId();
            if (e.CommandName == "Increase")
            {
                // Aumenta la quantità
                int productId = int.Parse(e.CommandArgument.ToString());
                TextBox quantityTextBox = (TextBox)e.Item.FindControl("quantityTextBox");

                int quantity = int.Parse(quantityTextBox.Text);

                quantity++;
                quantityTextBox.Text = quantity.ToString();

                UpdateQuantityInDataTable(productId, quantity);
                TotalCartPrice(ShoppingCartDataTable.CartTable);
                if (userId > 0)
                {
                    UpdateQuantityInDatabase(productId, quantity, userId);
                }
            }
            else if (e.CommandName == "Decrease")
            {
                // Diminuisci la quantità
                int productId = int.Parse(e.CommandArgument.ToString());
                TextBox quantityTextBox = (TextBox)e.Item.FindControl("quantityTextBox");


                int quantity = int.Parse(quantityTextBox.Text);

                if (quantity > 0)
                {
                    quantity--;
                    quantityTextBox.Text = quantity.ToString();

                    UpdateQuantityInDataTable(productId, quantity);
                    TotalCartPrice(ShoppingCartDataTable.CartTable);
                    if (userId > 0)
                    {
                        UpdateQuantityInDatabase(productId, quantity, userId);
                    }
                    //TODO: Bottone decrease rimuove 1 di quantità / sovrascrive nuova quantità ?

                }

            }
            else if (e.CommandName == "Delete")
            {
                int productId = Convert.ToInt32(e.CommandArgument);
                HttpCookie cookieId = Request.Cookies["ProductID"];
                HttpCookie cookieQuantity = Request.Cookies["ProductQuantity"];


                if (userId >= 0)
                {
                    RemoveProductFromCart(userId, productId);
                }

                if (cookieId != null && !string.IsNullOrEmpty(cookieId.Value) && cookieQuantity != null && !string.IsNullOrEmpty(cookieQuantity.Value))
                {
                    List<int> productIds = cookieId.Value.Split(',').Select(id => Convert.ToInt32(id)).ToList();
                    List<int> productQuantities = cookieQuantity.Value.Split(',').Select(qty => Convert.ToInt32(qty)).ToList();

                    if (productIds.Contains(productId))
                    {
                        int index = productIds.IndexOf(productId);
                        productIds.RemoveAt(index);
                        productQuantities.RemoveAt(index);

                        cookieId.Value = string.Join(",", productIds);
                        cookieQuantity.Value = string.Join(",", productQuantities);

                        Response.Cookies.Add(cookieId);
                        Response.Cookies.Add(cookieQuantity);

                        RetrieveDataFromSession();
                    }
                }


            }

        }

        private void UpdateQuantityInDatabase(int productId, int quantity, int userId)
        {
            string findCartQuery = @"SELECT c.Id FROM Carrello c
                                            JOIN Utente u ON c.UtenteId = u.Id
                                            WHERE u.Id = @userId";
            string updateQtInDbQuery = "UPDATE ProdottoInCarrello SET Quantita = @quantita WHERE CarrelloId = @cartId AND ProdottoId = @prodId";

            try
            {
                Db.conn.Open();
                SqlCommand findCartId = new SqlCommand(findCartQuery, Db.conn);
                findCartId.Parameters.AddWithValue("userId", userId);
                int cartId = (int)findCartId.ExecuteScalar();

                SqlCommand updateQt = new SqlCommand(updateQtInDbQuery, Db.conn);
                updateQt.Parameters.AddWithValue("quantita", quantity);
                updateQt.Parameters.AddWithValue("cartId", cartId);
                updateQt.Parameters.AddWithValue("prodId", productId);
                updateQt.ExecuteNonQuery();
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

        //private void BindCartRepeater()
        //{


        //    int userId = GetCurrentUserId();
        //    string selectProductForCartQuery = @"SELECT p.Id AS ProdottoId, p.Nome, p.Prezzo, pc.Quantita FROM Carrello c
        //                     JOIN ProdottoInCarrello pc ON c.Id = pc.CarrelloId
        //                     JOIN Prodotto p ON pc.ProdottoId = p.Id
        //                     WHERE c.UtenteId = @UtenteId";

        //    try
        //    {
        //        Db.conn.Open();
        //        SqlCommand cmd = new SqlCommand(selectProductForCartQuery, Db.conn);
        //        cmd.Parameters.AddWithValue("@UtenteId", userId);

        //        SqlDataReader reader = cmd.ExecuteReader();
        //        CartRepeater.DataSource = reader;
        //        CartRepeater.DataBind();

        //        reader.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write(ex.Message);
        //    }
        //    finally
        //    {
        //        Db.conn.Close();
        //    }


        //}

        //protected void CartRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        //{
        //    if (e.CommandName == "Rimuovi")
        //    {
        //        int prodottoId = Convert.ToInt32(e.CommandArgument);

        //        int utenteId = GetCurrentUserId();

        //        RemoveProductFromCart(utenteId, prodottoId);

        //        BindCartRepeater();
        //    }
        //}

        private void RemoveProductFromCart(int utenteId, int prodottoId)
        {
            string deleteQuery = @"DELETE FROM ProdottoInCarrello 
                                    WHERE CarrelloId IN (SELECT Id FROM Carrello WHERE UtenteId = @UtenteId) AND ProdottoId = @ProdottoId";

            try
            {
                Db.conn.Open();

                SqlCommand cmd = new SqlCommand(deleteQuery, Db.conn);
                cmd.Parameters.AddWithValue("@UtenteId", utenteId);
                cmd.Parameters.AddWithValue("@ProdottoId", prodottoId);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Db.conn.Close();
            }

        }

        //protected void rptCartItems_ItemCommand1(object source, RepeaterCommandEventArgs e)
        //{
        //    if (e.CommandName == "AddToCart")
        //    {
        //        int prodID;
        //        if (int.TryParse(e.CommandArgument.ToString(), out prodID))
        //        {


        //            List<int> products;
        //            if (Request.Cookies["ProductID"] == null || string.IsNullOrEmpty(Request.Cookies["ProductID"].Value))
        //            {
        //                products = new List<int>();
        //            }
        //            else
        //            {
        //                string[] productIDs = Request.Cookies["ProductID"].Value.Split(',');
        //                products = new List<int>(Array.ConvertAll(productIDs, int.Parse));
        //            }

        //            Response.Cookies["ProductID"].Value = string.Join(",", products);
        //            Response.Cookies["ProductID"].Expires = DateTime.Now.AddDays(1);
        //        }
        //    }
        //}


        protected void ClearAllItemsCart_Click(object sender, EventArgs e)
        {
            int userId = GetCurrentUserId();
            if (Request.Cookies["ProductID"] != null && Request.Cookies["ProductQuantity"] != null)
            {
                Request.Cookies["ProductID"].Value = "";
                Response.Cookies["ProductID"].Expires = DateTime.Now.AddDays(-1);
                Request.Cookies["ProductQuantity"].Value = "";
                Response.Cookies["ProductQuantity"].Expires = DateTime.Now.AddDays(-1);


                ShoppingCartDataTable.CartTable.Clear();
                RetrieveDataFromSession();

            }
            if (userId > 0)
            {
                ClearProductsInCartFromDb();
            }

        }

        private void ClearProductsInCartFromDb()
        {
            int userId = GetCurrentUserId();
            int cartId = GetUserCartId(userId);
            try
            {
                Db.conn.Open();
                string clearProdsFromCartQuery = "DELETE FROM ProdottoInCarrello WHERE CarrelloId = @cartId";

                SqlCommand clearProdsCmd = new SqlCommand(clearProdsFromCartQuery, Db.conn);
                clearProdsCmd.Parameters.AddWithValue("cartId", cartId);
                clearProdsCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                Db.conn.Close();
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

        /// <summary>
        /// Questa Funzione calcola il totale del carrello.
        /// 
        /// </summary>
        /// <param name="ShoppingListTable"></param>
        private void TotalCartPrice(DataTable ShoppingListTable)
        {
            double totalPrice = 0;

            foreach (DataRow row in ShoppingListTable.Rows)
            {
                if (row["Prezzo"].ToString() is string prezzo && row["Quantita"].ToString() is string quantita)
                {
                    if (double.TryParse(prezzo, out double prezzoValue) && double.TryParse(quantita, out double quantitaValue))
                    {
                        totalPrice += prezzoValue * quantitaValue;
                    }
                    else
                    {
                        // Gestire il caso in cui la conversione non riesce
                        // Ad esempio, è possibile impostare un valore predefinito o segnalare un errore.
                        // Qui verrà impostato un valore di default a 0, ma puoi personalizzarlo in base alle tue esigenze.
                        totalPrice = 99999999;
                    }
                }
                else
                {
                    // Gestire il caso in cui Prezzo o Quantita non siano stringhe
                    // Qui verrà impostato un valore di default a 0, ma puoi personalizzarlo in base alle tue esigenze.
                    totalPrice = 999999991;
                }
            }

            LblPrezzo.Text = totalPrice.ToString();

        }

        private void UpdateQuantityInDataTable(int productId, int newQuantity)
        {
            foreach (DataRow row in ShoppingCartDataTable.CartTable.Rows)
            {
                if (row["ID"] is int prodId && prodId == productId)
                {
                    row["Quantita"] = newQuantity;
                    return;
                }
            }
        }
    }
}