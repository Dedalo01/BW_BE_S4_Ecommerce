﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BW_BE_S4_Ecommerce;

namespace BW_BE_S4_Ecommerce
{
    public partial class Home : System.Web.UI.Page
    {
        private const int ProductsPerPage = 18;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                checkAdmin();


                int currentPage = 1;
                if (Request.QueryString["page"] != null)
                {
                    currentPage = Convert.ToInt32(Request.QueryString["page"]);
                }

                BindProductData(currentPage);
                BindPagination(currentPage);
            }
        }

        private void BindProductData(int pageNumber)
        {
            Db.conn.Open();

            try
            {
                int offset = (pageNumber - 1) * ProductsPerPage;

                SqlCommand cmd = new SqlCommand($"SELECT * FROM Prodotto ORDER BY Id OFFSET {offset} ROWS FETCH NEXT {ProductsPerPage} ROWS ONLY", Db.conn);
                SqlDataReader dataReader = cmd.ExecuteReader();

                ProductRepeater.DataSource = dataReader;
                ProductRepeater.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                if (Db.conn.State == ConnectionState.Open)
                {
                    Db.conn.Close();
                }
            }
        }

        private void BindPagination(int currentPage)
        {
            int totalPages = GetTotalPagesCount();

            for (int i = 1; i <= totalPages; i++)
            {
                HyperLink pageLink = new HyperLink();
                pageLink.Text = i.ToString();
                pageLink.NavigateUrl = $"Home.aspx?page={i}";

                if (i == currentPage)
                {
                    pageLink.CssClass = "active";
                }

                paginationContainer.Controls.Add(pageLink);

                if (i < totalPages)
                {
                    LiteralControl separator = new LiteralControl(" | ");
                    paginationContainer.Controls.Add(separator);
                }
            }
        }

        private int GetTotalPagesCount()
        {
            Db.conn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Prodotto", Db.conn);
                int totalProducts = (int)cmd.ExecuteScalar();
                return (int)Math.Ceiling((double)totalProducts / ProductsPerPage);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                return 1;
            }
            finally
            {
                if (Db.conn.State == ConnectionState.Open)
                {
                    Db.conn.Close();
                }
            }
        }


        protected void checkAdmin()
        {
            // Verifica se il cookie UserDetails esiste
            if (Request.Cookies["UserDetails"] != null)
            {
                // Ottieni il valore del cookie
                HttpCookie cookie = Request.Cookies["UserDetails"];

                // Controlla se ci sono i valori di userId ed email nel cookie
                if (cookie["UserId"] != null && cookie["UserEmail"] != null)
                {
                    string userId = cookie["UserId"];
                    string email = cookie["UserEmail"];

                    try
                    {
                        Db.conn.Open();

                        string query = "SELECT RuoloId FROM Utente WHERE Id = @userId AND Email = @email";

                        using (SqlCommand cmd = new SqlCommand(query, Db.conn))
                        {
                            cmd.Parameters.AddWithValue("@userId", userId);
                            cmd.Parameters.AddWithValue("@email", email);

                            // Esegui la query e ottieni il risultato
                            int ruoloId = (int)cmd.ExecuteScalar();

                            // Controlla se l'utente ha il ruolo di amministratore
                            if (ruoloId == 1)
                            {
                                Log._admin = true;
                            }
                        }

                        Db.conn.Close();
                    }
                    catch (Exception ex)
                    {
                        // Gestisci eventuali eccezioni
                    }
                }
            }
        }





        protected void ProductRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string productId = e.CommandArgument.ToString();

                try
                {
                    Db.conn.Open();
                    SqlCommand cmd = new SqlCommand($"DELETE FROM Prodotto WHERE Id={productId}", Db.conn);
                    int affectedRows = cmd.ExecuteNonQuery();

                    if (affectedRows != 0)
                    {
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
                    if (Db.conn.State == ConnectionState.Open)
                    {
                        Db.conn.Close();
                    }
                }
            }
        }
    }
}
