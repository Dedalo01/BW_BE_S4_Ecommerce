using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BW_BE_S4_Ecommerce
{
    public partial class Details : System.Web.UI.Page
    {
        private string ProductID;
        protected void Page_Load(object sender, EventArgs e)
        {

            {
                if (Request.QueryString["product"] == null)
                {
                    Response.Redirect("Home.aspx");
                }

                ProductID = Request.QueryString["product"].ToString();

            }
            try
            {
                Db.conn.Open();
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Prodotto WHERE id={ProductID}", Db.conn);
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.HasRows)
                {
                    dataReader.Read();
                    txtProduct.InnerText = dataReader["Nome"].ToString();
                    img.Src = dataReader["ImmagineUrl"].ToString();
                    txtDescription.InnerText = dataReader["Descrizione"].ToString();
                    txtPrice.InnerText = $"{dataReader["Prezzo"]}€";
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
            finally
            {
                if (Db.conn.State == System.Data.ConnectionState.Open)
                {
                    Db.conn.Close();
                }
            }
        }

        protected void btnAddCart_Click(object sender, EventArgs e)
        {
            int prodId = int.Parse(ProductID);
            List<int> products;
            if (Session["ProductID"] == null)
            {
                products = new List<int>();
            }
            else
            {
                products = (List<int>)Session["ProductID"];
            }

            products.Add(prodId);

            Session["ProductID"] = products;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Db.conn.Open();
                SqlCommand cmd = new SqlCommand($"DELETE FROM Prodotti WHERE id={ProductID}", Db.conn);
                SqlDataReader dataReader = cmd.ExecuteReader();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Response.Write("Prodotto eliminato con successso");
                    Response.Redirect("Home.aspx");
                }
                else
                {

                    Response.Write("Impossibile eliminare il prodotto.");
                }

            }
            catch (Exception ex)
            {

                Response.Write(ex.ToString());
            }
            finally
            {
                if (Db.conn.State == System.Data.ConnectionState.Open)
                {
                    Db.conn.Close();
                }
            }



        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect($"EditProduct.aspx?product={ProductID}");
        }

    }
}