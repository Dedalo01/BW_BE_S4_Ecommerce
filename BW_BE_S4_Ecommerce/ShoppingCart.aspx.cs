using System;
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
                BindCartRepeater();
            }

        }

        private void BindCartRepeater()
        {
            int userId = GetCurrentUserId();
            string selectProductForCartQuery = @"SELECT p.Id AS ProdottoId, p.Nome, p.Prezzo, pc.Quantita FROM Carrello c
                             JOIN ProdottoInCarrello pc ON c.Id = pc.CarrelloId
                             JOIN Prodotto p ON pc.ProdottoId = p.Id
                             WHERE c.UtenteId = @UtenteId";

            string query2 = @"SELECT pc.Id AS ProdottoId, p.Nome, p.Prezzo, pc.Quantita FROM ProdottoInCarrello pc
            JOIN Prodotto p ON pc.ProdottoId = p.Id";

            Db.conn.Open();
            SqlCommand cmd = new SqlCommand(selectProductForCartQuery, Db.conn);
            cmd.Parameters.AddWithValue("@UtenteId", userId);

            SqlDataReader reader = cmd.ExecuteReader();
            CartRepeater.DataSource = reader;
            CartRepeater.DataBind();

            reader.Close();
            Db.conn.Close();

        }




        protected void CartRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Rimuovi")
            {
                int prodottoId = Convert.ToInt32(e.CommandArgument);
                Response.Write("Prodotto id " + prodottoId);
                int utenteId = GetCurrentUserId();

                RemoveProductFromCart(utenteId, prodottoId);

                BindCartRepeater();
            }
        }

        private void RemoveProductFromCart(int utenteId, int prodottoId)
        {
            string deleteQuery = @"DELETE FROM ProdottoInCarrello 
WHERE CarrelloId IN (SELECT Id FROM Carrello WHERE UtenteId = @UtenteId) AND ProdottoId = @ProdottoId";

            string delete2 = @"DELETE FROM ProdottoInCarrello WHERE Id = @ProdottoId";
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

        private int GetCurrentUserId()
        {
            // unico utente col carrello al momento. Ha id 6
            return 6;
        }
    }
}