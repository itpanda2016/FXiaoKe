using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using FROST.Utility;
using System.Data;
using SalesReport.Controllers;
using NPOI.SS.UserModel;    //载入基础操作库
using NPOI.HSSF.UserModel;  //Excel2003版
using NPOI.XSSF.UserModel;  //Excel2007版


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
            if (context.Request["mod"] == "attendance") {
                if (ControllerAttendance.Count(fmDate, fmCompanyId) > 0) {
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
                        int k = 0;
                        for (int i = 0; i < dt.Rows.Count; i++) {
                            att.CompanyId = fmCompanyId;
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
            else if (context.Request["mod"] == "leave") {
                if (ControllerLeave.Count(fmDate, fmCompanyId) > 0) {
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
                            att.CompanyId = fmCompanyId;
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
                Message.Dialog(ret, "ImportLeave.aspx", true);      //导入成功的返回，链接到数据管理的界面（查询时，以URL + 参数的方式实现）
            }
            else if (context.Request["mod"] == "dailylog") {
                targetDirectory += "DailyLog/";
                string ret = "";
                ret += "上传文件（";
                if (file.FileName != "") {
                    int resYes = 0,resNo = 0;
                    string fileName = targetDirectory + DateTime.Now.ToString("yyyyMMddhhmmss") + Path.GetExtension(file.FileName);
                    file.SaveAs(fileName);
                    if (File.Exists(fileName)) {
                        IWorkbook wk = null;
                        
                        string ext = fileName.Substring(fileName.LastIndexOf(".")).ToString().ToLower();
                        FileStream fs = File.OpenRead(fileName);        //用File.OpenRead方法读取指定路径的文件，返回FileStream流
                        if (ext == ".xlsx") {
                            wk = new XSSFWorkbook(fs);
                        }
                        else {
                            wk = new HSSFWorkbook(fs);
                        }
                        ISheet sheet = wk.GetSheetAt(0);
                        Models.DailyLog mod = new Models.DailyLog();
                        for (int k = 6; k <= sheet.LastRowNum - 1; k++) {
                            IRow rows = sheet.GetRow(k);
                            mod.CompanyId = fmCompanyId;
                            mod.AssesDay = fmDate;
                            mod.EmployeeName = GetCellValue(rows.Cells[0]);        //姓名
                            mod.Department = GetCellValue(rows.Cells[1]);        //部门
                            mod.Position = GetCellValue(rows.Cells[2]);        //职位
                            mod.Score =Convert.ToInt32( GetCellValue(rows.Cells[3]));        //被点评数		
                            mod.ScoreAvg = Convert.ToDecimal(GetCellValue(rows.Cells[4]));        //平均分
                            mod.DlsSum = Convert.ToInt32(GetCellValue(rows.Cells[5]));        //总数
                            mod.ScoreChs= GetCellValue(rows.Cells[12]);        //得分
                            mod.DlsType = 1;     //日志类型，1表示已填 
                            mod.DlsDateTime = fmDate;
                            if (ControllerDailyLog.Add(mod)) {
                                resYes++;
                            }
                        }
                        sheet = wk.GetSheetAt(2);
                        mod = new Models.DailyLog();
                        for (int i = 5; i < sheet.LastRowNum - 1; i++) {
                            IRow row = sheet.GetRow(i);
                            mod.CompanyId = fmCompanyId;
                            mod.AssesDay = fmDate;
                            mod.EmployeeName = GetCellValue(row.Cells[0]);
                            mod.Department = GetCellValue(row.Cells[1]);
                            mod.Position = GetCellValue(row.Cells[2]);
                            mod.DlsType = -1;
                            mod.DlsDateTime = fmDate;
                            if (ControllerDailyLog.AddNull(mod)) {
                                resNo++;
                            }
                        }
                        Message.Dialog("共导入已填行数：" + resYes + "，未填人数：" + resNo, null, true);
                    }
                    
                }
            }
            else {
                Message.Dialog("无效的执行方法", "Main.aspx", true);
            }
        }
        /// <summary>
        /// 获取不同类型cell的值，并返回通用的string
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static string GetCellValue(ICell cell) {
            if (cell == null)
                return string.Empty;
            switch (cell.CellType) {
                case CellType.Blank:
                    return string.Empty;
                case CellType.Error:
                    return cell.ErrorCellValue.ToString();
                case CellType.Boolean:
                    return cell.BooleanCellValue.ToString();
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Numeric:
                    if (HSSFDateUtil.IsCellDateFormatted(cell))
                        return cell.DateCellValue.ToString();
                    else
                        return cell.NumericCellValue.ToString();
                case CellType.Formula:
                    switch (cell.CachedFormulaResultType) {
                        case CellType.String:
                            string strFORMULA = cell.StringCellValue;
                            if (strFORMULA != null && strFORMULA.Length > 0) {
                                return strFORMULA;
                            }
                            else {
                                return null;
                            }
                        case CellType.Numeric:
                            return cell.NumericCellValue.ToString();
                        case CellType.Boolean:
                            return cell.BooleanCellValue.ToString();
                        case CellType.Error:
                            return cell.ErrorCellValue.ToString();
                        default:
                            return "";
                    }
                case CellType.Unknown:
                default:
                    return cell.ToString();
            }
        }
        public bool IsReusable {
            get {
                return false;
            }
        }
    }
}