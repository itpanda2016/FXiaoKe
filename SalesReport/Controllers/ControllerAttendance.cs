using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using FROST.Utility;

namespace SalesReport.Controllers {
    public class ControllerAttendance {
        public static StringBuilder sb = new StringBuilder();

        /// <summary>
        /// 添加签到记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Add(Models.Attendance model) {
            sb.Clear();
            sb.Append("Insert into Attendances ");
            sb.Append("(CompanyId,EmployeeName,Department,AssesDay,AttDateTime,AttAddress,AttClient,AttContact,AttRange,AttDevState,AttSysState,AttRemark) ");
            sb.Append("values (@companyId,@employeeName,@department,@assesDay,@attDateTime,@attAddress,@attClient,@attContact,@attRange,@attDevState,@attSysState,@attRemark)");
            SqlParameter[] paras = {
                new SqlParameter("@companyId",model.CompanyId),
                new SqlParameter("@employeeName",model.EmployeeName),
                new SqlParameter("@department",model.Department),
                new SqlParameter("@assesDay",model.AssesDay),
                new SqlParameter("@attDateTime",model.AttDateTime),
                new SqlParameter("@attAddress",model.AttAddress),
                new SqlParameter("@attClient",model.AttClient),
                new SqlParameter("@attContact",model.AttContact),
                new SqlParameter("@attRange",model.AttRange),
                new SqlParameter("@attDevState",model.AttDevState),
                new SqlParameter("@attSysState",model.AttSysState),
                new SqlParameter("@attRemark",model.AttRemark)
            };
            int ret = MsSQLHelper.ExecuteNonQuery(sb.ToString(), CommandType.Text, paras);
            if (ret == 1) {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 根据考核日期、公司ID统计签到数量
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Count(DateTime assesDay,int companyId) {
            sb.Clear();
            sb.Append("select count(*) from Attendances where assesday = '" + assesDay + "' and CompanyId = " + companyId);
            int ret = (int)MsSQLHelper.ExecuteScalar(sb.ToString());
            return ret;
        }
        /// <summary>
        /// 根据考核日期、公司ID删除签到记录
        /// </summary>
        /// <param name="assesDay"></param>
        /// <returns></returns>
        public static bool Delete(DateTime assesDay,int companyId) {
            sb.Clear();
            sb.Append("delete from Attendance where AssesDay = '" + assesDay + "' and CompanyId = " + companyId);
            int ret = MsSQLHelper.ExecuteNonQuery(sb.ToString());
            if (ret > 0) {
                return true;
            }
            return false;
        }
    }
}