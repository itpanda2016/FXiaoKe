using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using SalesReport.Controllers;

namespace SalesReport
{
    public partial class Login : System.Web.UI.Page,IRequiresSessionState
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["act"] != null && Request["act"] == "logout" && Session["loginid"] != null)
            {
                Session.Clear();
            }
            if (Session["loginid"] != null)
                Response.Redirect("Main.aspx");
            if (Request["loginUsername"] != null & Request["loginUsername"] != "")
            {
                Models.User mUser = new Models.User();
                mUser.UserName = Request["loginUsername"].Trim();
                mUser.Password = Request["loginPassword"].Trim();   //MD5加密
                if (ControllerUser.LoginAuth(mUser))
                {
                    Session.Add("loginid", Request["loginUsername"].Trim());
                    Response.Redirect("Main.aspx");
                }
                else
                {
                    lbMsg.InnerText = "口令错误。";
                }
            }
        }
    }
}