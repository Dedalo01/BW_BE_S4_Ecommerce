using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace BW_BE_S4_Ecommerce
{
    public partial class ShoppingCart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCart();
            }
        }

        private void BindCart()
        {
            DataTable dtCart = GetCartDataFromDb();
            Carrello.DataSource = dtCart;
            Carrello.DataBind();
        }

        private DataTable GetCartDataFromDb()
        {
            DataTable dtCart = new DataTable();

            Db.conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT Nome, Prezzo FROM Carrello", Db.conn);
            SqlDataReader CarrelloTableReader = cmd.ExecuteReader();
            dtCart.Load(CarrelloTableReader);
            CarrelloTableReader.Close();

            Db.conn.Close();

            return dtCart;
        }

        protected void Carrello_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Rimuovi")
            {
                int productId = Convert.ToInt32(e.CommandArgument);

                RemoveProductFromDb(productId);

                BindCart();
            }
        }

        private void RemoveProductFromDb(int productId)
        {
            Db.conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM Prodotto WHERE Id = @ProductId", Db.conn);

            // Evita attacchi SQL Injection
            cmd.Parameters.AddWithValue("@ProductId", productId);

            cmd.ExecuteNonQuery();

            Db.conn.Close();
        }
    }
}