//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;

//namespace BW_BE_S4_Ecommerce
//{
//    public partial class EditProduct : System.Web.UI.Page
//    {
//        private string ProductId;
//        protected void Page_Load(object sender, EventArgs e)
//        {
//            if (IsPostBack)
//            {
//                if (Request.QueryString["product"] == null)
//                {
//                    Response.Redirect("Home.aspx");
//                }
//                else
//                {
//                    ProductId = Request.QueryString["product"].ToString();

//                    try
//                    {
//                        Db.conn.Open();


//                        SqlCommand cmd = new SqlCommand($"SELECT * FROM Prodotto WHERE Id= {ProductId}", Db.conn); // sostituisci id con ProductId
//                        SqlDataReader dataReader = cmd.ExecuteReader();

//                        if (dataReader.HasRows)
//                        {
//                            dataReader.Read();
//                            TxtNome.Text = dataReader["Nome"].ToString();
//                            TxtDescrizione.Text = dataReader["Descrizione"].ToString();
//                            TxtPrezzo.Text = dataReader["Prezzo"].ToString();
//                            TxtImmagineUrl.Text = dataReader["ImmagineUrl"].ToString();
//                        }
//                        dataReader.Close();
//                    }
//                    catch (Exception ex)
//                    {
//                        Response.Write(ex.ToString());
//                    }
//                    finally
//                    {
//                        Db.conn.Close();
//                    }
//                }
//            }
//        }

//        protected void BtnUpdate_Click(object sender, EventArgs e)
//        {
//            ProductId = Request.QueryString["product"].ToString();

//            if (ProductId == null)
//            {
//                Response.Write("ProductId non è stato inizializzato correttamente");
//                return;
//            }

//            try
//            {
//                Db.conn.Open();

//                string updateQuery = $@"UPDATE Prodotto
//                                     SET Nome = @Nome,
//                                         Descrizione = @Descrizione
//                                         Prezzo = @Prezzo
//                                         ImmagineUrl = @ImmagineUrl
//                                     WHERE Id = {ProductId}"; // stostitusci con ProductId

//                using (SqlCommand cmd = new SqlCommand(updateQuery, Db.conn))
//                {
//                    cmd.Parameters.AddWithValue("@Nome", TxtNome.Text);
//                    cmd.Parameters.AddWithValue("@Descizione", TxtDescrizione.Text);
//                    cmd.Parameters.AddWithValue("@Prezzo", decimal.Parse(TxtPrezzo.Text));
//                    cmd.Parameters.AddWithValue("@ImmagineUrl", TxtImmagineUrl.Text);

//                    int rowsAffected = cmd.ExecuteNonQuery();

//                    if (rowsAffected != 0)
//                    {
//                        Response.Write("Aggiornamento Effettuato");
//                    }
//                    else
//                    {
//                        Response.Write("Problemi durante l'aggiornamento");
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                Response.Write(ex.ToString());
//            }
//            finally
//            {
//                Db.conn.Close();
//            }
//        }
//    }
//}

using System;
using System.Data.SqlClient;

namespace BW_BE_S4_Ecommerce
{
    public partial class EditProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    Db.conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM Prodotto WHERE Id = 1", Db.conn);
                    SqlDataReader dataReader = cmd.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        dataReader.Read();
                        TxtNome.Text = dataReader["Nome"].ToString();
                        TxtDescrizione.Text = dataReader["Descrizione"].ToString();
                        TxtPrezzo.Text = dataReader["Prezzo"].ToString();
                        TxtImmagineUrl.Text = dataReader["ImmagineUrl"].ToString();
                    }
                    dataReader.Close();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
                finally
                {
                    Db.conn.Close();
                }
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Db.conn.Open();

                string updateQuery = @"UPDATE Prodotto
                                       SET Nome = @Nome,
                                           Descrizione = @Descrizione,
                                           Prezzo = @Prezzo,
                                           ImmagineUrl = @ImmagineUrl
                                       WHERE Id = 1";

                using (SqlCommand cmd = new SqlCommand(updateQuery, Db.conn))
                {
                    cmd.Parameters.AddWithValue("@Nome", TxtNome.Text);
                    cmd.Parameters.AddWithValue("@Descrizione", TxtDescrizione.Text);
                    cmd.Parameters.AddWithValue("@Prezzo", decimal.Parse(TxtPrezzo.Text));
                    cmd.Parameters.AddWithValue("@ImmagineUrl", TxtImmagineUrl.Text);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected != 0)
                    {
                        Response.Write("Aggiornamento Effettuato");
                    }
                    else
                    {
                        Response.Write("Problemi durante l'aggiornamento");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
            finally
            {
                Db.conn.Close();
            }
        }
    }
}
