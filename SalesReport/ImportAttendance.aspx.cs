using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using FROST.Utility;

namespace SalesReport {
    public partial class ImportAttendance : System.Web.UI.Page {
        public DataTable dtCompanys;
        public string sql;
        protected void Page_Load(object sender, EventArgs e) {
            sql = "select id,companyName from Companys";
            dtCompanys = MsSQLHelper.ExecuteDataTable(sql);
        }
        ///// <summary>
        ///// 读取数据
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void btnRead_click(object sender,EventArgs e) {
        //    string targetDirectory = Server.MapPath("~/UpFiles/");
        //    if (fmFileUpload1.FileName != "") {
        //        string fileName = targetDirectory + "/Attendance/" + DateTime.Now.ToString("yyyyMMddhhmmss") + Path.GetExtension(fmFileUpload1.FileName);
        //        fmFileUpload1.SaveAs(fileName);
        //        if (File.Exists(fileName)) {
        //            DataTable dt = NpoiHelper.ExcelToDataTable(fileName);
        //            if (dt.Rows.Count > 0) {
        //                Models.Attendance att = new Models.Attendance();
        //                for (int i = 0; i < dt.Rows.Count; i++) {
        //                    att.CompanyId = 
        //                }
        //                lblMsg.Text = fmFileUpload1.FileName + "：上传成功，共导入数据（" + dt.Rows.Count + "）条";
        //            }
        //        }
        //        //导入后台数据库，部门字段直接存储，不拆分现在的部门
        //    }
        //}
        
    }
}