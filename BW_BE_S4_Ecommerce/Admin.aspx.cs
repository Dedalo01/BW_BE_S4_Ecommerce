using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BW_BE_S4_Ecommerce
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Log._admin)
            {
                Response.Redirect("Home.aspx");
            }




            if (!IsPostBack)
            {
                BindProductData();
            }
        }

        private void BindProductData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Db.conn.ConnectionString))
                {
                    string query = "SELECT * FROM Prodotto ORDER BY Id";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    connection.Open();

                    SqlDataReader dataReader = cmd.ExecuteReader();
                    ProductRepeaterAdmin.DataSource = dataReader;
                    ProductRepeaterAdmin.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("Si è verificato un errore durante il recupero dei dati: " + ex.Message);
            }
        }


        protected void ProductRepeaterAdmin_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int productId = Convert.ToInt32(e.CommandArgument);

                if (DeleteProduct(productId))
                {
                    BindProductData();
                }
                else
                {
                    Response.Write("Errore durante l'eliminazione del prodotto.");
                }
            }

            if (e.CommandName == "Modifica")
            {
                RepeaterItem item = e.Item;

                // Trova i controlli di visualizzazione
                Label nomeLabel = (Label)item.FindControl("NomeLabel");
                Label descrizioneLabel = (Label)item.FindControl("DescrizioneLabel");
                Label prezzoLabel = (Label)item.FindControl("PrezzoLabel");
                Label immagineUrlLabel = (Label)item.FindControl("ImmagineUrlLabel");

                // Trova i controlli di modifica
                TextBox nomeTextBox = (TextBox)item.FindControl("NomeTextBox");
                TextBox descrizioneTextBox = (TextBox)item.FindControl("DescrizioneTextBox");
                TextBox prezzoTextBox = (TextBox)item.FindControl("PrezzoTextBox");
                TextBox immagineUrlTextBox = (TextBox)item.FindControl("ImmagineUrlTextBox");

                // Mostra i controlli di modifica e nascondi quelli di visualizzazione
                nomeLabel.Visible = false;
                descrizioneLabel.Visible = false;
                prezzoLabel.Visible = false;
                immagineUrlLabel.Visible = false;

                nomeTextBox.Visible = true;
                descrizioneTextBox.Visible = true;
                prezzoTextBox.Visible = true;
                immagineUrlTextBox.Visible = true;

                // Mostra il pulsante di conferma modifica
                ((Button)item.FindControl("ConfirmButton")).Visible = true;
                ((Button)item.FindControl("EditButton")).Visible = false;
            }

            if (e.CommandName == "Confirm")
            {

                RepeaterItem item = e.Item;


                ((Button)item.FindControl("EditButton")).Visible = true;

                TextBox nomeTextBox = (TextBox)item.FindControl("NomeTextBox");
                TextBox descrizioneTextBox = (TextBox)item.FindControl("DescrizioneTextBox");
                TextBox prezzoTextBox = (TextBox)item.FindControl("PrezzoTextBox");
                TextBox immagineUrlTextBox = (TextBox)item.FindControl("ImmagineUrlTextBox");


                // Recupera i valori aggiornati dalle caselle di testo
                string nuovoNome = nomeTextBox.Text;
                string nuovaDescrizione = descrizioneTextBox.Text;
                decimal nuovoPrezzo;
                if (!decimal.TryParse(prezzoTextBox.Text, out nuovoPrezzo))
                {
                    // Gestisci il caso in cui il prezzo non sia un numero decimale valido
                    Console.WriteLine("Il prezzo non è valido.");
                    return;
                }
                string nuovoImmagineUrl = immagineUrlTextBox.Text;




                using (SqlConnection connection = new SqlConnection(Db.conn.ConnectionString))
                {
                    int productId = Convert.ToInt32(e.CommandArgument);


                    string query = @"UPDATE Prodotto
                         SET 
                             Nome = @nuovoNome,
                             Descrizione = @nuovaDescrizione,
                             Prezzo = @nuovoPrezzo,
                             ImmagineUrl = @nuovoImmagineUrl
                         WHERE
                             Id = @prodottoId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        // Imposta i parametri
                        command.Parameters.AddWithValue("@nuovoNome", nuovoNome);
                        command.Parameters.AddWithValue("@nuovaDescrizione", nuovaDescrizione);
                        command.Parameters.AddWithValue("@nuovoPrezzo", nuovoPrezzo);
                        command.Parameters.AddWithValue("@nuovoImmagineUrl", nuovoImmagineUrl);
                        command.Parameters.AddWithValue("@prodottoId", productId);

                        connection.Open();
                            command.ExecuteNonQuery();
                            connection.Close();


                            // Aggiorna la visibilità dei controlli dopo aver confermato le modifiche
                            nomeTextBox.Visible = false;
                            descrizioneTextBox.Visible = false;
                            prezzoTextBox.Visible = false;
                            immagineUrlTextBox.Visible = false;


                            ((Label)item.FindControl("NomeLabel")).Visible = true;
                            ((Label)item.FindControl("DescrizioneLabel")).Visible = true;
                            ((Label)item.FindControl("PrezzoLabel")).Visible = true;
                            ((Label)item.FindControl("ImmagineUrlLabel")).Visible = true;

                            BindProductData();

                            

                    }
                }
            }
        }


        private bool DeleteProduct(int productId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Db.conn.ConnectionString))
                {
                    connection.Open();

                    string queryCarrelloProdotto = "DELETE FROM ProdottoInCarrello WHERE ProdottoId = @ProductId";
                    SqlCommand cmdCarrelloProdotto = new SqlCommand(queryCarrelloProdotto, connection);
                    cmdCarrelloProdotto.Parameters.AddWithValue("@ProductId", productId);
                    int rowsAffectedCarrelloProdotto = cmdCarrelloProdotto.ExecuteNonQuery();

                    string query = "DELETE FROM Prodotto WHERE ID = @ProductId";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    int rowsAffectedProdotto = cmd.ExecuteNonQuery();

                    return rowsAffectedCarrelloProdotto > 0 || rowsAffectedProdotto > 0;
                }
            }
            catch (Exception ex)
            {
                Response.Write("Si è verificato un errore durante l'eliminazione del prodotto: " + ex.Message);
                return false;
            }
        }





        protected void Inserimento_Click(object sender, EventArgs e)
        {
            string nome = nomeProdottoBox.Text;
            string descrizione = descrizioneProdottoBox.Text;
            decimal prezzo = decimal.Parse(prezzoProdottoBox.Text);
            string immagineUrl = immageUrlProdottoBox.Text;

            try
            {
                Db.conn.Open();

                string query = "INSERT INTO Prodotto (Nome, Descrizione, Prezzo, ImmagineUrl) VALUES (@nome, @descrizione, @prezzo, @immagineUrl)";

                using (SqlCommand cmd = new SqlCommand(query, Db.conn))
                {
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@descrizione", descrizione); // corrected parameter name
                    cmd.Parameters.AddWithValue("@prezzo", prezzo);
                    cmd.Parameters.AddWithValue("@immagineUrl", immagineUrl);

                    cmd.ExecuteNonQuery();

                    Db.conn.Close();
                }
            }
            catch (Exception ex)
            {
                Label3.Text = "Si è verificato un errore durante il tentativo di aggiungere prodotti." + ex;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
    }
}