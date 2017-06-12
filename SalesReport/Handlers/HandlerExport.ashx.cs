using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;

namespace SalesReport.Handlers {
    /// <summary>
    /// HandlerExport 的摘要说明
    /// </summary>
    public class HandlerExport : IHttpHandler {
        public StringBuilder sb = new StringBuilder();
        public void ProcessRequest(HttpContext context) {
            context.Response.ContentType = "text/plain";
            DateTime fmDate1, fmDate2;
            try {
                fmDate1 = Convert.ToDateTime(context.Request["fmDate1"]);
                fmDate2 = Convert.ToDateTime(context.Request["fmDate2"]);
            }
            catch (Exception) {
                Message.Dialog("无效的日期。", null, true);
                return;
            }
            int fmCompanyId = Convert.ToInt32(context.Request["fmCompany"]);
            if (fmCompanyId <= 0) {
                Message.Dialog("请选择公司。", null, true);
                return;
            }

            HttpPostedFile file = context.Request.Files["fmFile"];
            if (file.FileName == "") {
                Message.Dialog("请选择需要上传的文件。" + Path.GetExtension(file.FileName), null, true);
                return;
            }
            if (Path.GetExtension(file.FileName).IndexOf("xls") < 0) {
                Message.Dialog("文件扩展名不正确。" + Path.GetExtension(file.FileName), null, true);
                return;
            }
            string targetDirectory = context.Server.MapPath("~/UpFiles/");
            targetDirectory += "Attendance/";
            string ret = "";
            ret += "上传文件（";
            string fileName = targetDirectory + DateTime.Now.ToString("yyyyMMddhhmmss") + Path.GetExtension(file.FileName);
            file.SaveAs(fileName);
            if (File.Exists(fileName)) {
                sb.Clear();
                //todo 读取所有记录到DT
                //创建一个临时的WORKBOOK，以DT的列 + 一个循环日期范围生成的列 + 公式列（可不加）
                #region 加工考勤数据
                    //第一层循环：日期范围
                    //第二层循环：员工明细，每处理一行，写对应的WORKBOOK列+行对应的字段值
                    //循环结束后，生成表格，提供下载
                #endregion
            }
            
            context.Response.Write("Hello World");
        }

        public static string MakeAttendance(string employee, string companyId, DateTime assesDay) {
            return "";
        }
        public bool IsReusable {
            get {
                return false;
            }
        }
    }
}