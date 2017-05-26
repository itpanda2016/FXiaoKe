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
            
            HttpPostedFile file = context.Request.Files["fmFile"];
            if (file.FileName == "" ) {
                Message.Dialog("请选择需要上传的文件。" + Path.GetExtension(file.FileName), null, true);
                return;
            }
            if (Path.GetExtension(file.FileName).IndexOf("xls") < 0) {
                Message.Dialog("文件扩展名不正确。" + Path.GetExtension(file.FileName), null, true);
                return;
            }
            string targetDirectory = context.Server.MapPath("~/UpFiles/");
            if (context.Request["mod"] == "attendance") {
                if (ControllerAttendance.Count(fmDate, companyid) > 0) {
                    Message.Dialog("指定公司的数据已导入。", null, true);
                    return;
                }
                targetDirectory += "Attendance/";
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
            else if(context.Request["mod"] == "leave") {
                if (ControllerLeave.Count(fmDate, companyid) > 0) {
                    Message.Dialog("指定（公司、日期）的【请假】数据已导入。", null, true);
                    return;
                }
                targetDirectory += "Leave/";
                string ret = "";
                ret += "上传文件（";
                if (file.FileName != "") {
                    string fileName = targetDirectory + DateTime.Now.ToString("yyyyMMddhhmmss") + Path.GetExtension(file.FileName);
                    file.SaveAs(fileName);
                    if (File.Exists(fileName)) {
                        DataTable dt = NpoiHelper.ExcelToDataTable(fileName);
                        Models.Leave att = new Models.Leave();
                        int k = 0;
                        for (int i = 0; i < dt.Rows.Count; i++) {
                            att.CompanyId = companyid;
                            att.AssesDay = fmDate;
                            att.ApplyState = dt.Rows[i][0].ToString(); //审批状态 
                            att.EmployeeName = dt.Rows[i][1].ToString();    //报批人
                            att.Department = dt.Rows[i][2].ToString();  //部门
                            att.ApplyContent = dt.Rows[i][3].ToString();    //申请内容
                            att.ApplyPerson = dt.Rows[i][4].ToString();     //当前审批人
                            att.PostDate = Convert.ToDateTime(dt.Rows[i][5]);   //提交审批时间
                            att.ApplyDate = Convert.ToDateTime(dt.Rows[i][6]);  //最终批复时间
                            att.LeaveType = dt.Rows[i][7].ToString();   //请假事项
                            att.LeaveStart = Convert.ToDateTime(dt.Rows[i][8]); //开始时间
                            att.LeaveEnd = Convert.ToDateTime(dt.Rows[i][9]); //开始时间
                            att.LeaveHour = Convert.ToDecimal(dt.Rows[i][10]);  //小时
                            att.ReturnContent = dt.Rows[i][11].ToString();  //回复内容
                            ControllerLeave.Add(att);  //添加考勤 记录
                            k++;
                        }
                        ret += Path.GetFileName(file.FileName) + "，行数：" + k;
                    }
                }
                ret += "）成功";
                Message.Dialog(ret, "ImportLeave.aspx", true);
            }
            else {
                Message.Dialog("无效的执行方法", "Main.aspx", true);
            }
        }

        public bool IsReusable {
            get {
                return false;
            }
        }
    }
}