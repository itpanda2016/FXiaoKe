using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace SalesReport
{
    public partial class Message : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "消息" + Master.Page.Title;
            if (Request["msgtitle"] == null)
                msgTitle.InnerText = "提醒";
            else
                msgTitle.InnerText = Request["msgtitle"];
            if (Request["msgurl"] == null)
                msgHref.HRef = "javascript:history.go(-1);";
            else
                msgHref.HRef = Request["msgurl"];
            msgHref.InnerText = "点击此处继续";
            msgContent.InnerText = Request["msgcontent"];       //可以在这儿以br分割，然后循环做换行处理？
        }
    }
    /// <summary>
    /// 消息集中处理
    /// </summary>
    public partial class Message
    {
        /// <summary>
        /// 提示消息
        /// </summary>
        /// <param name="content">消息内容</param>
        /// <param name="url">跳转URL，可为空</param>
        public static void Dialog(string content, string url = null,bool isParent = false)
        {
            StringBuilder sb = new StringBuilder();
            if (isParent) {
                sb.Append("../Message.aspx?");
            }
            else {
                sb.Append("Message.aspx?");
            }
            sb.AppendFormat("msgcontent={0}", content);
            if (url != null)
                sb.AppendFormat("&msgurl={0}", url);
            HttpContext.Current.Response.Redirect(sb.ToString());
        }
    }
}