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
        List<int> products;
        double totalCartPrice = 0;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {




            if (!IsPostBack)
            {

                if (Log.log == false)
                {
                    RetrieveDataFromSession();

                    //TotalCartPrice(dt);
                }
                else if (Log.log == true)
                {
                    BindCartRepeater();
                }

            }
        }


        // inserisco lettura carello session
        // private DataTable GenerateDtCartItems()
        //{
        //    DataTable dt = new DataTable();
        //dt.Columns.Add("ID", typeof(int));
        //    dt.Columns.Add("Nome", typeof(string));
        //    dt.Columns.Add("Prezzo", typeof(double));
        //    dt.Columns.Add("Quantita", typeof(int));

        //    return dt;
        //}

        protected void RetrieveDataFromSession()
        {
            HttpCookie cookie = Request.Cookies["ProductID"];
            HttpCookie cookieQuantity = Request.Cookies["ProductQuantity"];

            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Nome", typeof(string));
            dt.Columns.Add("Prezzo", typeof(double));
            dt.Columns.Add("Quantita", typeof(int));

            if (cookie != null && !string.IsNullOrEmpty(cookie.Value) && cookieQuantity != null && !string.IsNullOrEmpty(cookieQuantity.Value))
            {
                string[] productIds = cookie.Value.Split(',');
                string[] quantities = cookieQuantity.Value.Split(',');

                Dictionary<int, int> productQuantities = new Dictionary<int, int>();

                for (int i = 0; i < productIds.Length; i++)
                {
                    if (int.TryParse(productIds[i], out int id) && int.TryParse(quantities[i], out int quantity))
                    {
                        if (productQuantities.ContainsKey(id))
                        {
                            // Se l'ID del prodotto esiste già nel dizionario, aggiungi la quantità
                            productQuantities[id] += quantity;
                        }
                        else
                        {
                            // Altrimenti, aggiungi l'ID del prodotto al dizionario
                            productQuantities.Add(id, quantity);
                        }
                    }
                }

                // Ora hai un dizionario con ID prodotto come chiavi e quantità sommate come valori
                // Ora puoi utilizzare questo dizionario per recuperare i dettagli dei prodotti e aggiungerli alla tabella

                foreach (var kvp in productQuantities)
                {
                    int id = kvp.Key;
                    int quantity = kvp.Value;

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
                    finally
                    {
                        Db.conn.Close();
                    }
                }
            }

            ShoppingCartDataTable.CartTable = dt;
            rptCartItems.DataSource = ShoppingCartDataTable.CartTable;
            rptCartItems.DataBind();
            TotalCartPrice(ShoppingCartDataTable.CartTable);
        }




        protected void rptCartItems_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Increase" || e.CommandName == "Decrease")
            {
                int productId = 0;
                if (int.TryParse(e.CommandArgument.ToString(), out productId))
                {
                    RepeaterItem item = e.Item as RepeaterItem;
                    Label quantityLabel = item.FindControl("quantityLabel") as Label;

                    if (quantityLabel != null)
                    {
                        int quantity = 0;
                        if (int.TryParse(quantityLabel.Text, out quantity))
                        {
                            if (e.CommandName == "Increase")
                                quantity++;
                            else if (e.CommandName == "Decrease" && quantity > 1)
                                quantity--;

                            quantityLabel.Text = quantity.ToString();
                            UpdateCart(productId, quantity);
                        }
                    }
                }
            }
            else if (e.CommandName == "Delete")
            {
                int productId = 0;
                if (int.TryParse(e.CommandArgument.ToString(), out productId))
                {
                    RemoveProductFromCart(productId);
                }
            }
        }



        private void UpdateQuantity(Label quantityLabel, int change)
        {
            int quantity = int.Parse(quantityLabel.Text) + change;
            if (quantity >= 1)
            {
                quantityLabel.Text = quantity.ToString();
                RepeaterItem item = (RepeaterItem)quantityLabel.NamingContainer;
                HiddenField productIdHiddenField = (HiddenField)item.FindControl("productIdHiddenField");

                if (productIdHiddenField != null)
                {
                    int productId = int.Parse(productIdHiddenField.Value);
                    UpdateCart(productId, quantity);
                }
            }
        }


        private void RemoveProductFromCart(int productId)
        {
            HttpCookie cookie = Request.Cookies["ProductID"];
            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
            {
                List<int> productIds = cookie.Value.Split(',').Select(id => Convert.ToInt32(id)).ToList();
                if (productIds.Contains(productId))
                {
                    productIds.Remove(productId);
                    cookie.Value = string.Join(",", productIds);
                    Response.Cookies.Add(cookie);
                    RetrieveDataFromSession();
                }
            }
        }

        private void UpdateCart(int productId, int quantity)
        {
            // Il codice per aggiornare il carrello con il productId e la quantità specificati
            TotalCartPrice(ShoppingCartDataTable.CartTable, quantity.ToString(), productId);
            UpdateTotalCartPrice();
        }





        private void UpdateTotalCartPrice()
        {
            double totalPrice = 0;

            foreach (DataRow row in ShoppingCartDataTable.CartTable.Rows)
            {
                if (double.TryParse(row["Prezzo"].ToString(), out double prezzoValue) &&
                    int.TryParse(row["Quantita"].ToString(), out int quantitaValue))
                {
                    totalPrice += prezzoValue * quantitaValue;
                }
            }

            LblPrezzo.Text = totalPrice.ToString();
        }


        private void BindCartRepeater()
        {


            int userId = GetCurrentUserId();
            string selectProductForCartQuery = @"SELECT p.Id AS ProdottoId, p.Nome, p.Prezzo, pc.Quantita FROM Carrello c
                             JOIN ProdottoInCarrello pc ON c.Id = pc.CarrelloId
                             JOIN Prodotto p ON pc.ProdottoId = p.Id
                             WHERE c.UtenteId = @UtenteId";

            try
            {
                Db.conn.Open();
                SqlCommand cmd = new SqlCommand(selectProductForCartQuery, Db.conn);
                cmd.Parameters.AddWithValue("@UtenteId", userId);

                SqlDataReader reader = cmd.ExecuteReader();
                CartRepeater.DataSource = reader;
                CartRepeater.DataBind();

                reader.Close();
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

        protected void CartRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Rimuovi")
            {
                int prodottoId = Convert.ToInt32(e.CommandArgument);

                int utenteId = GetCurrentUserId();

                RemoveProductFromCart(utenteId, prodottoId);

                BindCartRepeater();
            }
        }

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

        protected void rptCartItems_ItemCommand1(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "AddToCart")
            {
                int prodID;
                if (int.TryParse(e.CommandArgument.ToString(), out prodID))
                {


                    List<int> products;
                    if (Request.Cookies["ProductID"] == null || string.IsNullOrEmpty(Request.Cookies["ProductID"].Value))
                    {
                        products = new List<int>();
                    }
                    else
                    {
                        string[] productIDs = Request.Cookies["ProductID"].Value.Split(',');
                        products = new List<int>(Array.ConvertAll(productIDs, int.Parse));
                    }

                    Response.Cookies["ProductID"].Value = string.Join(",", products);
                    Response.Cookies["ProductID"].Expires = DateTime.Now.AddDays(1);
                }
            }
        }


        protected void btnClearSession_Click(object sender, EventArgs e)
        {

            
            Response.Cookies["ProductID"].Expires = DateTime.Now.AddDays(-1);
           
            Response.Cookies["ProductQuantity"].Expires = DateTime.Now.AddDays(-1);


            RetrieveDataFromSession();
        }

        
       


        private int GetCurrentUserId()
        {
            // unico utente col carrello al momento. Ha id 6
            return 6;
        }



        private void TotalCartPrice(DataTable ShoppingListTable)
        {
            double totalPrice = 0;

            foreach (DataRow row in ShoppingListTable.Rows)
            {
                if (double.TryParse(row["Prezzo"].ToString(), out double prezzoValue) &&
                    int.TryParse(row["Quantita"].ToString(), out int quantitaValue))
                {
                    totalPrice += prezzoValue * quantitaValue;
                }
                else
                {
                    // Gestisci il caso in cui la conversione non riesce
                    // Qui potresti segnalare un errore o impostare un valore predefinito per il prezzo totale.
                    // In questo esempio, il prezzo totale verrà impostato a 0.
                    totalPrice = 0;
                    break; // Esci dal loop se la conversione fallisce
                }
            }

            LblPrezzo.Text = totalPrice.ToString();
        }


        //private void TotalCartPrice(DataTable ShoppingListTable, string quantita, int prodottoId)
        //{
        //    double totalPrice = 0;
        //    double totalPriceRow = 0;
        //    double quantitaValue = double.Parse(quantita);

        //    foreach (DataRow row in ShoppingListTable.Rows)
        //    {
        //        int prodId = int.Parse(row["ID"].ToString());
        //        double quantityFromTable = double.Parse(row["Quantita"].ToString());
        //        if (prodId == prodottoId && row["Prezzo"].ToString() is string prezzo)
        //        {
        //            if (double.TryParse(prezzo, out double prezzoValue))
        //            {
        //                totalPriceRow = prezzoValue * quantitaValue;
        //                totalPrice += totalPriceRow;
        //            }
        //            else
        //            {
        //                // Gestire il caso in cui la conversione non riesce
        //                // Ad esempio, è possibile impostare un valore predefinito o segnalare un errore.
        //                // Qui verrà impostato un valore di default a 0, ma puoi personalizzarlo in base alle tue esigenze.
        //                totalPrice += prezzoValue * quantitaValue;

        //            }



        //        }
        //        else
        //        {
        //            // Gestire il caso in cui Prezzo o Quantita non siano stringhe
        //            // Qui verrà impostato un valore di default a 0, ma puoi personalizzarlo in base alle tue esigenze.
        //            totalPrice = 999999991;
        //        }
        //    }

        //    LblPrezzo.Text = totalPrice.ToString();
        //}


        private void TotalCartPrice(DataTable ShoppingListTable, string quantita, int prodottoId)
        {
            double quantitaValue = double.Parse(quantita);

            foreach (DataRow row in ShoppingListTable.Rows)
            {
                if (row["ID"] is int prodId && prodId == prodottoId &&
                    row["Quantita"] is string quantitaString && double.TryParse(quantitaString, out double quantityFromTable) &&
                    row["Prezzo"] is string prezzoString && double.TryParse(prezzoString, out double prezzoValue))
                {
                    double totalPriceRow = prezzoValue * quantitaValue;
                    // Aggiungi il valore calcolato al prezzo totale esistente
                    double totalPriceExisting = double.TryParse(LblPrezzo.Text, out double existingPrice) ? existingPrice : 0;
                    double totalPrice = totalPriceExisting + totalPriceRow;
                    LblPrezzo.Text = totalPrice.ToString();
                    break; // Esci dal loop dopo aver trovato e processato la riga corrispondente
                }
            }
        }



    }
}