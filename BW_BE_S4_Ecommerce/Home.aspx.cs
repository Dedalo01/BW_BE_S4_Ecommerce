using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BW_BE_S4_Ecommerce
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Popolare la dropdown con tutte le categorie
            Db.conn.Open();

            try
            {
                // Query SQL corretta per selezionare i dati dalla tabella Prodotto
                SqlCommand cmd = new SqlCommand("SELECT * FROM Prodotto", Db.conn);
                SqlDataReader dataReader = cmd.ExecuteReader();

                // Inizializzazione della variabile htmlContent
                string htmlContent = "";

                if (dataReader.HasRows)
                {
                    // Ciclo sulle righe ottenute dal db e aggiungo l'HTML delle cards
                    while (dataReader.Read())
                    {
                        htmlContent += $@"<div class=""col"">
                     <div class=""card h-100"">
                        <img src=""{dataReader["ImmagineUrl"]}"" class=""card-img-top"" alt=""{dataReader["Nome"]}"">
                        <div class=""card-body d-flex flex-column"">
                            <h5 class=""card-title"">{dataReader["Nome"]}</h5>
                            <p class=""card-text"">Prezzo: {dataReader["Prezzo"]}</p>
                            <a href=""Details.aspx?product={dataReader["Id"]}"" class=""btn btn-primary mt-auto"">Dettagli</a>
                       <asp:Button ID=""DeleteButtonClick"" runat=""server"" CommandArgument='<%# Eval(""Id"") %>' Text=""Cancella"" CssClass=""btn"" OnClientClick=""return confirm('Sei sicuro di voler eliminare questo prodotto?')"" OnClick=""BtnDelete_Click"" />
                        </div>
                     </div>
                  </div>";
                    }
                }

                // Inserimento in RowCards il contenuto di htmlContent
                RowCards.InnerHtml = htmlContent;
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                // Chiusura della connessione
                if (Db.conn.State == ConnectionState.Open)
                {
                    Db.conn.Close();
                }
            }
        }
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            // Ottieni l'ID del prodotto dal comando del pulsante
            Button btn = (Button)sender;
            string productId = btn.CommandArgument;

            // eliminare la riga dal database
            try
            {
                Db.conn.Open();
                SqlCommand cmd = new SqlCommand($@"DELETE FROM Prodotto WHERE Id={productId}", Db.conn);
                int affectedRows = cmd.ExecuteNonQuery();

                if (affectedRows != 0)
                {
                    // ridirezionare alla Index
                    Response.Redirect("Home.aspx");
                }
                else
                {
                    Response.Write("Eliminazione non riuscita");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
            finally
            {
                // Chiudi la connessione
                if (Db.conn.State == ConnectionState.Open)
                {
                    Db.conn.Close();
                }
            }
        }
    }
}