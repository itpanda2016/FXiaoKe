using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using FROST.Utility;
using System.Data;
using SalesReport.Controllers;

namespace SalesReport.Handlers {
    /// <summary>
    /// HandlerImport 的摘要说明
    /// </summary>
    public class HandlerImport : IHttpHandler {
        public void ProcessRequest(HttpContext context) {
            context.Response.ContentType = "text/plain";
            DateTime fmDate;
            try {
                fmDate = Convert.ToDateTime(context.Request["fmDate"]);
            }
            catch (Exception) {
                Message.Dialog("无效的日期。", null, true);
                return;
            }
            int companyid = Convert.ToInt32(context.Request["fmCompany"]);
            if (companyid <= 0) {
                Message.Dialog("请选择公司。", null, true);
                return;
            }
            if (ControllerAttendance.Count(fmDate,companyid) > 0) {
                Message.Dialog("指定公司的数据已导入。", null, true);
                return;
            }
            string targetDirectory = context.Server.MapPath("~/UpFiles/");
            if (context.Request["mod"] == "attendance") {
                targetDirectory += "Attendance/";
                HttpPostedFile file = context.Request.Files["fmFile"];
                string ret = "";
                ret += "上传文件（";
                if (file.FileName != "") {
                    string fileName = targetDirectory + DateTime.Now.ToString("yyyyMMddhhmmss") + Path.GetExtension(file.FileName);
                    file.SaveAs(fileName);
                    if (File.Exists(fileName)) {
                        DataTable dt = NpoiHelper.ExcelToDataTable(fileName);
                        Models.Attendance att = new Models.Attendance();
                        int k =0;
                        for (int i = 0; i < dt.Rows.Count; i++) {
                            att.CompanyId = companyid;
                            att.AssesDay = fmDate;
                            att.EmployeeName = dt.Rows[i][0].ToString();    //员工姓名
                            att.Department = dt.Rows[i][1].ToString();  //所在部门
                            att.AttDateTime = Convert.ToDateTime(dt.Rows[i][2]);    //签到时间
                            att.AttAddress = dt.Rows[i][3].ToString();  //签到地址
                            att.AttClient = dt.Rows[i][4].ToString();   //关联客户
                            att.AttContact = dt.Rows[i][5].ToString();  //联系人
                            att.AttRange = dt.Rows[i][6].ToString();    //签到距离
                            att.AttDevState = dt.Rows[i][7].ToString(); //设备状态 
                            att.AttSysState = dt.Rows[i][8].ToString();     //系统风险
                            att.AttRemark = dt.Rows[i][9].ToString();   //文字描述
                            ControllerAttendance.Add(att);  //添加签到记录
                            k++;
                        }
                        ret += Path.GetFileName(file.FileName) + "，行数：" + k;
                    }
                }
                ret += "）成功";
                Message.Dialog(ret, "ImportAttendance.aspx", true);
            }
        }

        public bool IsReusable {
            get {
                return false;
            }
        }
    }
}