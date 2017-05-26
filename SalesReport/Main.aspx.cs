using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FxkAPI;
using FxkAPI.MailList;

namespace SalesReport
{
    public partial class Main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) {
            AccessTokenReturn fxkAccess = AccessTokenContainer.GetCorpAccessToken();
            Response.Write("errorCode：" + AccessTokenContainer.GetCorpAccessToken().errorCode + "<br />");
            Response.Write("errorMessage：" + AccessTokenContainer.GetCorpAccessToken().errorMessage + "<br />");
            Response.Write("corpAccessToken：" + AccessTokenContainer.GetCorpAccessToken().corpAccessToken + "<br />");
            Response.Write("corpId：" + AccessTokenContainer.GetCorpAccessToken().corpId + "<br />");
            Response.Write("expiresIn：" + AccessTokenContainer.GetCorpAccessToken().expiresIn);
            Response.Write("<hr />");
            DepartmentListResult dptList = MailListApi.GetDepartmentList(fxkAccess.corpAccessToken, fxkAccess.corpId);
            if (dptList.errorCode == 0) {
                IEnumerable<Department> a = dptList.departments.OrderBy(p => p.parentId).ThenBy(p => p.id);
                foreach (Department item in a) {
                    //todo 按ID排序，取上级为0或是没有上级的，然后此级为公司，以此ID，取出所有上级ID为该ID的（如果无，说明没有下级了）；再继续循环取出以这些ID为父级的下级ID
                    //todo 其实就是递归：http://www.cnblogs.com/Ferry/archive/2010/12/14/1905283.html
                    //todo 更简洁的递归：http://blog.csdn.net/kone0611/article/details/43053455
                    Response.Write(item.id + "..." + item.name + "..." + item.parentId + "..." + item.order);
                    Response.Write("<hr />");
                }
            }
        }
    }
}