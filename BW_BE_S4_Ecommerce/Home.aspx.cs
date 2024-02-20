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
using BW_BE_S4_Ecommerce;

namespace BW_BE_S4_Ecommerce
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProductData();
            }
        }

        private void BindProductData()
        {
            Db.conn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Prodotto", Db.conn);
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
