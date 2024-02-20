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
        protected void DeleteButtonClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string productId = btn.CommandArgument;

            // Apri la connessione al database
            Db.conn.Open();

            try
            {
                // Query SQL per eliminare il prodotto in base all'ID
                string deleteQuery = $"DELETE FROM Prodotto WHERE Id = {productId}";
                SqlCommand cmd = new SqlCommand(deleteQuery, Db.conn);
                int rowsAffected = cmd.ExecuteNonQuery();

                // Verifica se l'eliminazione è avvenuta con successo
                if (rowsAffected > 0)
                {
                    // Puoi anche aggiungere un messaggio di conferma o aggiornare la pagina
                    Response.Write("Prodotto eliminato con successo.");
                }
                else
                {
                    Response.Write("Errore durante l'eliminazione del prodotto.");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                // Chiudi la connessione
                if (Db.conn.State == ConnectionState.Open)
                {
                    Db.conn.Close();
                }

                // Aggiorna la pagina o esegui altre azioni necessarie dopo l'eliminazione
                Response.Redirect(Request.RawUrl);
            }
        }
    }

}
