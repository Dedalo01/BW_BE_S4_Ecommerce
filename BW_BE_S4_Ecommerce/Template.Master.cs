using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BW_BE_S4_Ecommerce
{
    public partial class Template : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void BtnLogout_Click(object sender, EventArgs e)
        {
            if (Request.Cookies["UserDetails"] != null)
            {
                HttpCookie userCookie = Request.Cookies["UserDetails"];
                userCookie.Expires = DateTime.Now.AddDays(-21);
                Response.Cookies.Add(userCookie);
            }
            Response.Redirect("Home.aspx");
        }
    }
}