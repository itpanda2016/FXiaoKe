using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FROST.Utility;

namespace SalesReport.Controllers
{
    public class ControllerUser
    {
        private static string sql;
        /// <summary>
        /// 验证用户名密码是否正确
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool LoginAuth(Models.User model)
        {
            sql = "select count(*) from Users where UserName = '" + model.UserName + "' and PassWord = '" + model.Password + "'";
            int ret = (int)MsSQLHelper.ExecuteScalar(sql);
            if (ret == 1)
            {
                return true;
            }
            return false;
        }

    }
}