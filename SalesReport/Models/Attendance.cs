using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesReport.Models {
    /// <summary>
    /// 考勤签到
    /// </summary>
    public class Attendance {
        public int Id { set; get; }
        public int CompanyId { set; get; }
        public string EmployeeName { set; get; }
        public string Department { set; get; }
        public DateTime AssesDay { set; get; }
        public DateTime AttDateTime { set; get; }
        public string AttAddress { set; get; }
        public string AttClient { set; get; }
        public string AttContact { set; get; }
        public string AttRange { set; get; }
        public string AttDevState { set; get; }
        public string AttSysState { set; get; }
        public string AttRemark { set; get; }
    }
}