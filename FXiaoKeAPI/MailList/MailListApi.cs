using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FROST.Utility;

namespace FxkApi.MailList {
    public class MailListApi {
        private static string url;

        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="corpAccesstoken"></param>
        /// <param name="corpId"></param>
        /// <returns></returns>
        public static DepartmentListResult GetDepartmentList(string corpAccesstoken, string corpId) {
            url = "https://open.fxiaoke.com/cgi/department/list";
            DepartmentListPost dlpost = new DepartmentListPost() {
                corpAccessToken = corpAccesstoken,
                corpId = corpId
            };
            return JsonConvert.DeserializeObject<DepartmentListResult>(
                General.CurlByDotNet(url, CurlMethod.POST, JsonConvert.SerializeObject(dlpost))
                );
        }
        /// <summary>
        /// 获取部门下成员信息(详细)
        /// </summary>
        /// <param name="pot"></param>
        /// <returns></returns>
        public static RetUserListDetail GetUserListByDptId(PotUserList pot) {
            url = "https://open.fxiaoke.com/cgi/user/list";
            return JsonConvert.DeserializeObject<RetUserListDetail>(
                General.CurlByDotNet(url, CurlMethod.POST, JsonConvert.SerializeObject(pot))
                );
        }
    }
    /// <summary>
    /// 部门列表（POST参数）
    /// </summary>
    public class DepartmentListPost {
        public string corpAccessToken { get; set; }
        public string corpId { get; set; }
    }
    /// <summary>
    /// 部门列表（POST返回结果）
    /// </summary>
    public class DepartmentListResult {
        public int errorCode { get; set; }
        public string errorMessage { get; set; }
        public Department[] departments { get; set; }
    }
    /// <summary>
    /// 部门参数对象
    /// </summary>
    public class Department {
        public int id { get; set; }
        public string name { get; set; }
        public int parentId { get; set; }
        public int order { get; set; }
    }
    /// <summary>
    /// 获取部门下成员列表（详细）【POST参数】
    /// </summary>
    public class PotUserList {
        public string corpAccessToken { get; set; }
        public string corpId { get; set; }
        public int departmentId { get; set; }
        public bool fetchChild { get; set; }
        public bool showDepartmentIdsDetail { set; get; }
    }
    /// <summary>
    /// 部门成员列表（详细）【返回结果】
    /// </summary>
    public class RetUserListDetail {
        public UserlistDetail[] userlist { get; set; }
        public int errorCode { get; set; }
        public string errorMessage { get; set; }
    }
    /// <summary>
    /// 成员列表（详细）【返回结果】
    /// </summary>
    public class UserlistDetail {
        public string openUserId { get; set; }
        public string name { get; set; }
        public string nickName { get; set; }
        public bool isStop { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string gender { get; set; }
        public string position { get; set; }
        public string profileImageUrl { get; set; }
        public int[] departmentIds { get; set; }

    }

}
