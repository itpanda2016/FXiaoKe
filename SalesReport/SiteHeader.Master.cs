using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SalesReport.Controllers;

namespace SalesReport {
    public partial class SiteHeader : System.Web.UI.MasterPage {
        public string actMenu;
        protected void Page_Load(object sender, EventArgs e) {
            if (Session["loginid"] == null)
                Response.Redirect("Login.aspx");
            actMenu = Request.Url.ToString();
        }
    }
}