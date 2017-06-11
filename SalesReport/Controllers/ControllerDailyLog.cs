using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using FROST.Utility;

namespace SalesReport.Controllers {
    public class ControllerDailyLog {
        public static StringBuilder sb = new StringBuilder();
        /// <summary>
        /// 添加日志记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Add(Models.DailyLog model) {
            sb.Clear();
            sb.Append("INSERT INTO [DailyLogs] ([AssesDay], [CompanyId], [EmployeeName], [Department], [Position], [Score], [ScoreAvg], [DlsSum], [ScoreChs], [DlsDateTime], [DlsType]) VALUES ");
            sb.Append(" (@AssesDay,@CompanyId,@EmployeeName,@Department,@Position,@Score,@ScoreAvg,@DlsSum,@ScoreChs,@DlsDateTime,@DlsType)");
            SqlParameter[] paras = {
                new SqlParameter("@AssesDay",model.AssesDay),
                new SqlParameter("@CompanyId",model.CompanyId),
                new SqlParameter("@EmployeeName",model.EmployeeName),
                new SqlParameter("@Department",model.Department),
                new SqlParameter("@Position",model.Position),
                new SqlParameter("@Score",model.Score),
                new SqlParameter("@ScoreAvg",model.ScoreAvg),
                new SqlParameter("@DlsSum",model.DlsSum),
                new SqlParameter("@ScoreChs",model.ScoreChs),
                new SqlParameter("@DlsDateTime",model.DlsDateTime),
                new SqlParameter("@DlsType",model.DlsType)
            };
            int ret = MsSQLHelper.ExecuteNonQuery(sb.ToString(), CommandType.Text, paras);
            if (ret == 1) {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 添加日志记录（未填写）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddNull(Models.DailyLog model) {
            sb.Clear();
            sb.Append("INSERT INTO [DailyLogs] ([AssesDay], [CompanyId], [EmployeeName], [Department], [Position], [DlsDateTime], [DlsType]) VALUES ");
            sb.Append(" (@AssesDay,@CompanyId,@EmployeeName,@Department,@Position,@DlsDateTime,@DlsType)");
            SqlParameter[] paras = {
                new SqlParameter("@AssesDay",model.AssesDay),
                new SqlParameter("@CompanyId",model.CompanyId),
                new SqlParameter("@EmployeeName",model.EmployeeName),
                new SqlParameter("@Department",model.Department),
                new SqlParameter("@Position",model.Position),
                new SqlParameter("@DlsDateTime",model.DlsDateTime),
                new SqlParameter("@DlsType",model.DlsType)
            };
            int ret = MsSQLHelper.ExecuteNonQuery(sb.ToString(), CommandType.Text, paras);
            if (ret == 1) {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 根据考核日期、公司ID统计日志数量
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Count(DateTime assesDay, int companyId) {
            sb.Clear();
            sb.Append("select count(*) from DailyLogs where assesday = '" + assesDay + "' and CompanyId = " + companyId);
            int ret = (int)MsSQLHelper.ExecuteScalar(sb.ToString());
            return ret;
        }
        /// <summary>
        /// 根据考核日期、公司ID删除日志记录
        /// </summary>
        /// <param name="assesDay"></param>
        /// <returns></returns>
        public static bool Delete(DateTime assesDay, int companyId) {
            sb.Clear();
            sb.Append("delete from DailyLogs where AssesDay = '" + assesDay + "' and CompanyId = " + companyId);
            int ret = MsSQLHelper.ExecuteNonQuery(sb.ToString());
            if (ret > 0) {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 获取日志列表，根据考核日期、公司ID
        /// </summary>
        /// <param name="assesDay"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public static DataTable DailyLogList(DateTime assesDay, int companyId) {
            sb.Clear();
            sb.Append("select ID,[AssesDay], [CompanyId], [EmployeeName], [Department], [Position], [Score], [ScoreAvg], [DlsSum], [ScoreChs], [DlsDateTime], [DlsType] FROM DailyLogs");
            sb.Append(" where AssesDay = '" + assesDay + "' and CompanyId = " + companyId);
            DataTable ret = MsSQLHelper.ExecuteDataTable(sb.ToString());
            if (ret != null && ret.Rows.Count > 0) {
                return ret;
            }
            return null;
        }
    }
}