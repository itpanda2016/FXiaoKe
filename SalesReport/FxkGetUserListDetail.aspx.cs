using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FxkApi;
using FxkApi.MailList;
namespace SalesReport {
    public partial class FxkGetUserListDetail : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            //1005-成都公司
            AccessTokenReturn retToken = ContainerAccessToken.GetCorpAccessToken();
            if (retToken.errorCode == 0) {
                PotUserList po = new PotUserList() {
                    corpAccessToken = retToken.corpAccessToken,
                    corpId = retToken.corpId,
                    departmentId = 1005,
                    fetchChild = true,
                    showDepartmentIdsDetail = false
                };
                RetUserListDetail retUserList = MailListApi.GetUserListByDptId(po);
                if (retUserList.errorCode == 0) {
                    Response.Write("成都公司（1005）员工总数：" + retUserList.userlist.Length + "<br />");
                    foreach (var item in retUserList.userlist) {
                        Response.Write("OpenUserId:" + item.openUserId + "," + "Name:" + item.name
                            + ",NickName" + item.nickName + ",员工状态：" + item.isStop
                            + "，所在部门及父部门列表：" + item.departmentIds
                            );
                        Response.Write("<br />");
                    }
                }
                else {
                    Response.Write("UserList:" + retUserList.errorCode + "-" + retUserList.errorMessage);
                }
            }
            else {
                Response.Write("Token:" + retToken.errorCode + "-" + retToken.errorMessage);
            }
        }
    }
}