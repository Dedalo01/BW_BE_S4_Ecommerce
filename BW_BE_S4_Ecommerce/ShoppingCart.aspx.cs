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
        protected void Page_Load(object sender, EventArgs e)
        {




            if (!IsPostBack)
            {

                if (Log.log == false)
                {
                    RetrieveDataFromSession();

                }
                else if (Log.log == true)
                {
                    BindCartRepeater();
                }

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
        }



        protected void rptCartItems_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Increase")
            {
                // Aumenta la quantità
                int productId = int.Parse(e.CommandArgument.ToString());
                TextBox quantityTextBox = (TextBox)e.Item.FindControl("quantityTextBox");
                //HtmlGenericControl quantityLabel = (HtmlGenericControl)e.Item.FindControl("lblQuantita");
                //Label quantityLabel = (Label)e.Item.FindControl("lblQuantita");

                int quantity = int.Parse(quantityTextBox.Text);
                //int quantity = int.Parse(quantityLabel.Text);
                quantity++;
                quantityTextBox.Text = quantity.ToString();
                //quantityLabel.Text = quantity.ToString();

                UpdateQuantityInDataTable(productId, quantity);

                TotalCartPrice(ShoppingCartDataTable.CartTable);
            }
            else if (e.CommandName == "Decrease")
            {
                // Diminuisci la quantità
                int productId = int.Parse(e.CommandArgument.ToString());
                TextBox quantityTextBox = (TextBox)e.Item.FindControl("quantityTextBox");
                //Label quantityLabel = (Label)e.Item.FindControl("lblQuantita");
                // HtmlGenericControl quantityLabel = (HtmlGenericControl)e.Item.FindControl("lblQuantita");
                //Label quantityLabel = (Label)e.Item.FindControl("lblQuantita");
                int quantity = int.Parse(quantityTextBox.Text);
                //int quantity = int.Parse(quantityLabel.Text);
                //quantityLabel.Text = quantity.ToString();
                if (quantity > 0)
                {
                    quantity--;
                    quantityTextBox.Text = quantity.ToString();
                    //quantityLabel.Text = quantity.ToString();

                    UpdateQuantityInDataTable(productId, quantity);
                    TotalCartPrice(ShoppingCartDataTable.CartTable);

                }

            }
            else if (e.CommandName == "Delete")


            {
                int productId = Convert.ToInt32(e.CommandArgument);
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
        }



        //protected void rptCartItems_ItemCommand(object source, RepeaterCommandEventArgs e)
        //{
        //    if (e.CommandName == "Increase")
        //    {
        //        // Aumenta la quantità
        //        int productId = int.Parse(e.CommandArgument.ToString());
        //        Label quantityLabel = (Label)e.Item.FindControl("lblQuantita");
        //        Response.Write(quantityLabel.Text);
        //        int quantity;
        //        if (int.TryParse(quantityLabel.Text, out quantity))
        //        {
        //            quantity++;
        //            quantityLabel.Text = quantity.ToString();

        //            UpdateQuantityInDataTable(productId, quantity);
        //            TotalCartPrice(ShoppingCartDataTable.CartTable);
        //        }
        //        else
        //        {
        //            // Gestisci il caso in cui la conversione non riesce
        //            // Puoi impostare un valore predefinito o gestire l'errore in altro modo
        //            quantityLabel.Text = "T'OH BECCHI AR CULO";
        //        }
        //    }
        //    else if (e.CommandName == "Decrease")
        //    {
        //        // Diminuisci la quantità
        //        int productId = int.Parse(e.CommandArgument.ToString());
        //        Label quantityLabel = (Label)e.Item.FindControl("lblQuantita");

        //        int quantity;
        //        if (int.TryParse(quantityLabel.Text, out quantity) && quantity > 0)
        //        {
        //            quantity--;
        //            quantityLabel.Text = quantity.ToString();

        //            UpdateQuantityInDataTable(productId, quantity);
        //            TotalCartPrice(ShoppingCartDataTable.CartTable);
        //        }
        //        else
        //        {
        //            // Gestisci il caso in cui la conversione non riesce o la quantità è già 0
        //            // Puoi impostare un valore predefinito o gestire l'errore in altro modo
        //            quantityLabel.Text = "T'OH BECCHI AR CULO IN REVERSE";
        //        }
        //    }
        //    else if (e.CommandName == "Delete")


        //    {
        //        int productId = Convert.ToInt32(e.CommandArgument);
        //        HttpCookie cookie = Request.Cookies["ProductID"];

        //        if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
        //        {
        //            List<int> productIds = cookie.Value.Split(',').Select(id => Convert.ToInt32(id)).ToList();

        //            if (productIds.Contains(productId))
        //            {
        //                productIds.Remove(productId);

        //                cookie.Value = string.Join(",", productIds);
        //                Response.Cookies.Add(cookie);

        //                RetrieveDataFromSession();
        //            }
        //        }
        //    }
        //    // Resto del codice...
        //}






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

            Response.Cookies["ProductID"].Value = "";
            Response.Cookies["ProductID"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["ProductQuantity"].Value = "";
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
                    break;
                }
            }
        }
    }
}