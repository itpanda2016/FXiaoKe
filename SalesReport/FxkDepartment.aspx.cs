using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FxkApi;
using FxkApi.MailList;

namespace SalesReport {
    public partial class FxkDepartment : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            AccessTokenReturn retToken = ContainerAccessToken.GetCorpAccessToken();
            if (retToken.errorCode == 0) {
                DepartmentListResult retDpts = MailListApi.GetDepartmentList(retToken.corpAccessToken, retToken.corpId);
                if (retDpts.errorCode == 0) {
                    foreach (var item in retDpts.departments) {
                        //todo 按ID排序，取上级为0或是没有上级的，然后此级为公司，以此ID，取出所有上级ID为该ID的（如果无，说明没有下级了）；再继续循环取出以这些ID为父级的下级ID
                        //todo 其实就是递归：http://www.cnblogs.com/Ferry/archive/2010/12/14/1905283.html
                        //todo 更简洁的递归：http://blog.csdn.net/kone0611/article/details/43053455
                        if (item.name.IndexOf("成都") >= 0) {
                            Response.Write("ID：" + item.id + "，NAME：" + item.name);
                            Response.Write("<br />");
                        }
                    }
                }
                else {
                    Response.Write(retDpts.errorCode + ":" + retDpts.errorMessage);
                }
            }
            else {
                Response.Write("Token:" + retToken.errorCode + ":" + retToken.errorMessage);
            }
        }
    }
}