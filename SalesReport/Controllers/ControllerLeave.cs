using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using FROST.Utility;

namespace SalesReport.Controllers {
    public class ControllerLeave {
        public static StringBuilder sb = new StringBuilder();
        /// <summary>
        /// 添加请假记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Add(Models.Leave model) {
            sb.Clear();
            sb.Append("insert into Leaves (AssesDay,CompanyId,ApplyState,EmployeeName,Department,ApplyContent,ApplyPerson,PostDate,ApplyDate,LeaveType,LeaveStart,LeaveEnd,LeaveHour,ReturnContent) ");
            sb.Append(" values (@assesDay,@companyId,@applyState,@employeeName,@department,@applyContent,@applyPerson,@postDate,@applyDate,@leaveType,@leaveStart,@leaveEnd,@leaveHour,@returnContent)");
            SqlParameter[] paras = {
                new SqlParameter("@assesDay",model.AssesDay),
                new SqlParameter("@companyId",model.CompanyId),
                new SqlParameter("@applyState",model.ApplyState),
                new SqlParameter("@employeeName",model.EmployeeName),
                new SqlParameter("@department",model.Department),
                new SqlParameter("@applyContent",model.ApplyContent),
                new SqlParameter("@applyPerson",model.ApplyPerson),
                new SqlParameter("@postDate",model.PostDate),
                new SqlParameter("@applyDate",model.ApplyDate),
                new SqlParameter("@leaveType",model.LeaveType),
                new SqlParameter("@leaveStart",model.LeaveStart),
                new SqlParameter("@leaveEnd",model.LeaveEnd),
                new SqlParameter("@leaveHour",model.LeaveHour),
                new SqlParameter("@returnContent",model.ReturnContent)
            };
            int ret = MsSQLHelper.ExecuteNonQuery(sb.ToString(), CommandType.Text, paras);
            if (ret == 1) {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 根据考核日期、公司ID统计请假数量
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Count(DateTime assesDay, int companyId) {
            sb.Clear();
            sb.Append("select count(*) from Leaves where assesday = '" + assesDay + "' and CompanyId = " + companyId);
            int ret = (int)MsSQLHelper.ExecuteScalar(sb.ToString());
            return ret;
        }
        /// <summary>
        /// 根据考核日期、公司ID删除请假记录
        /// </summary>
        /// <param name="assesDay"></param>
        /// <returns></returns>
        public static bool Delete(DateTime assesDay, int companyId) {
            sb.Clear();
            sb.Append("delete from Leaves where AssesDay = '" + assesDay + "' and CompanyId = " + companyId);
            int ret = MsSQLHelper.ExecuteNonQuery(sb.ToString());
            if (ret > 0) {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 获取假期列表，根据考核日期、公司ID
        /// </summary>
        /// <param name="assesDay"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public static DataTable LeaveList(DateTime assesDay,int companyId) {
            sb.Clear();
            sb.Append("SELECT Id, ApplyState, EmployeeName, Department, ApplyContent, ApplyPerson, PostDate, ApplyDate, LeaveType, LeaveStart, LeaveEnd, LeaveHour, ReturnContent, AssesDay, CompanyId FROM Leaves");
            sb.Append(" where AssesDay = '" + assesDay + "' and CompanyId = " + companyId);
            DataTable ret = MsSQLHelper.ExecuteDataTable(sb.ToString());
            if (ret != null && ret.Rows.Count > 0) {
                return ret;
            }
            return null;
        }
    }
}