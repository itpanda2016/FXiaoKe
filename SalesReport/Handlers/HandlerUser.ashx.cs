using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SalesReport.Controllers;

namespace SalesReport.Handlers
{
    /// <summary>
    /// HandlerUser 的摘要说明
    /// </summary>
    public class HandlerUser : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write(context.Request["login"]);
            if (context.Request["act"] == "login")
            {
                Models.User modelUser = new Models.User();
                modelUser.UserName = context.Request["fmUserName"];
                modelUser.Password = context.Request["fmPassword"];
                if(modelUser.UserName != "" && modelUser.Password != "")
                {
                    bool ret = ControllerUser.LoginAuth(modelUser);
                    if (ret == true)
                    {
                        //context.Session["loginId"] = userName;
                        context.Session.Add("loginId", modelUser.UserName);
                        context.Response.Redirect("/Main.aspx");
                    }
                    else
                    {
                        context.Response.Write("用户名或密码错误。");
                        context.Response.Write("<a href=\"/Login.aspx\">登录</a>");
                    }
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        
    }
}