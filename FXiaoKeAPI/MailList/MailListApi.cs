using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FROST.Utility;

namespace FxkAPI.MailList {
    public class MailListApi {
        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="corpAccesstoken"></param>
        /// <param name="corpId"></param>
        /// <returns></returns>
        public static DepartmentListResult GetDepartmentList(string corpAccesstoken, string corpId) {
            string url = "https://open.fxiaoke.com/cgi/department/list";
            DepartmentListPost dlpost = new DepartmentListPost() {
                corpAccessToken = corpAccesstoken,
                corpId = corpId
            };
            return JsonConvert.DeserializeObject<DepartmentListResult>(
                General.CurlByDotNet(url, CurlMethod.POST, JsonConvert.SerializeObject(dlpost))
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
    /// 部门
    /// </summary>
    public class Department {
        public int id { get; set; }
        public string name { get; set; }
        public int parentId { get; set; }
        public int order { get; set; }
    }

}
