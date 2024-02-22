using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
using System.Web;
using System.Linq;

namespace BW_BE_S4_Ecommerce
{ 

    public partial class ShoppingCart : System.Web.UI.Page
    {
        List<int> products;
        protected void Page_Load(object sender, EventArgs e)
        {
            
                
            

            if (!IsPostBack)
            {

                if (Log.log == false)
                {
                    RetrieveDataFromSession();
                }
                else if ( Log.log == true)
                {
                    BindCartRepeater();
                }
                
            }
        }


        // inserisco lettura carello session


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



            rptCartItems.DataSource = dt;
            rptCartItems.DataBind();
        }

        //List<int> product = (List<int>)Session["ProductID"];
        //DataTable dt = new DataTable();
        //dt.Columns.Add("ID", typeof(int));
        //dt.Columns.Add("Nome", typeof(string));
        //dt.Columns.Add("Prezzo", typeof(double));

        //if (product != null)
        //{
        //    foreach (int id in product)
        //    {
        //        try
        //        {
        //            Db.conn.Open();
        //            SqlCommand cmd = new SqlCommand($"SELECT * FROM Prodotto WHERE ID='{id}'", Db.conn);
        //            SqlDataReader dataReader = cmd.ExecuteReader();
        //            if (dataReader.HasRows)
        //            {
        //                dataReader.Read();
        //                dt.Rows.Add(dataReader["ID"], dataReader["Nome"], dataReader["Prezzo"]);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Response.Write(ex.ToString());
        //        }
        //        finally
        //        {
        //            if (Db.conn.State == ConnectionState.Open)
        //            {
        //                Db.conn.Close();
        //            }
        //        }
        //    }
        //}

        //rptCartItems.DataSource = dt;
        //rptCartItems.DataBind();



        // SESSION



        //HttpCookie cookie = Request.Cookies["ProductID"];
        //List<int> product = new List<int>();

        //if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
        //{
        //    string[] productIds = cookie.Value.Split(',');

        //    foreach (string id in productIds)
        //    {
        //        if (int.TryParse(id, out int productId))
        //        {
        //            product.Add(productId);
        //        }
        //    }
        //}

        //DataTable dt = new DataTable();
        //dt.Columns.Add("Nome", typeof(string));
        //dt.Columns.Add("Descrizione", typeof(string));

        //if (product != null)
        //{
        //    foreach (int id in product)
        //    {
        //        try
        //        {
        //            Db.conn.Open();
        //            SqlCommand cmd = new SqlCommand($"SELECT * FROM Prodotto WHERE Id='{id}'", Db.conn);
        //            SqlDataReader dataReader = cmd.ExecuteReader();
        //            if (dataReader.HasRows)
        //            {
        //                dataReader.Read();
        //                dt.Rows.Add(dataReader["Nome"], dataReader["Descrizione"]);

        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Response.Write(ex.ToString());
        //        }
        //        finally
        //        {
        //            if(Db.conn.State == ConnectionState.Open) 
        //            {
        //                Db.conn.Close();                        
        //            }
        //        }

        //    }
        //}

        //// COOKIE

        //List<int> products = null;

        //if (Request.Cookies["ProductID"] != null)
        //{
        //    HttpCookie cookie = Request.Cookies["ProductID"];
        //    string[] productIds = cookie.Value.Split(',');
        //    products = new List<int>();

        //    foreach (string id in productIds)
        //    {
        //        if (int.TryParse(id, out int productId))
        //        {
        //            products.Add(productId);
        //        }
        //    }

        //    DebugLabel.Text = "Numero di prodotti nel cookie: " + products.Count;

        //    foreach (int productId in products)
        //    {
        //        try
        //        {
        //            Db.conn.Open();
        //            SqlCommand cmd = new SqlCommand($"SELECT Nome, Descrizione " +
        //                                             $"FROM Prodotto " +
        //                                             $"WHERE Id = {productId}", Db.conn);

        //            SqlDataReader reader = cmd.ExecuteReader();

        //            if (reader.Read())
        //            {
        //                string nome = reader["Nome"].ToString();
        //                string descrizione = reader["Descrizione"].ToString();

        //                LblProdotto.Text += $"Nome: {nome}, Descrizione: {descrizione}<br/>";
        //            }
        //            reader.Close();
        //        }
        //        catch (Exception ex)
        //        {
        //            Response.Write(ex.ToString());
        //        }
        //        finally
        //        {
        //            if (Db.conn.State == ConnectionState.Open)
        //            {
        //                Db.conn.Close();
        //            }
        //        }
        //    }
        //}
        //else
        //{
        //    DebugLabel.Text = "Nessun prodotto nel cookie o i dati non sono stati salvati correttamente.";
        //}







        //protected void rptCartItems_ItemCommand(object source, RepeaterCommandEventArgs e)
        //{
        //    if (e.CommandName == "Delete")
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
        //}

       
            protected void rptCartItems_ItemCommand(object source, RepeaterCommandEventArgs e)
            {
                if (e.CommandName == "Increase")
                {
                    // Aumenta la quantità
                    int productId = int.Parse(e.CommandArgument.ToString());
                    TextBox quantityTextBox = (TextBox)e.Item.FindControl("quantityTextBox");
                    int quantity = int.Parse(quantityTextBox.Text);
                    quantity++;
                    quantityTextBox.Text = quantity.ToString();
                }
                else if (e.CommandName == "Decrease")
                {
                    // Diminuisci la quantità
                    int productId = int.Parse(e.CommandArgument.ToString());
                    TextBox quantityTextBox = (TextBox)e.Item.FindControl("quantityTextBox");
                    int quantity = int.Parse(quantityTextBox.Text);
                    if (quantity > 1)
                    {
                        quantity--;
                        quantityTextBox.Text = quantity.ToString();
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
        //    if (e.CommandName == "Delete")
        //    {
        //        int productId = Convert.ToInt32(e.CommandArgument);
        //        List<int> product = (List<int>)Session["ProductID"];

        //        if (product != null)
        //        {
        //            product.Remove(productId);
        //            Session["ProductID"] = product;
        //            RetrieveDataFromSession();
        //        }
        //    }
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



        //protected void btnClearSession_Click(object sender, EventArgs e)
        //{

        //    Session.Clear();

        //    RetrieveDataFromSession();
        //}

        private int GetCurrentUserId()
        {
            // unico utente col carrello al momento. Ha id 6
            return 6;
        }


    }
}