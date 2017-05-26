using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesReport.Models {
    /// <summary>
    /// 请假
    /// </summary>
    public class Leave {
        public int Id { set; get; }
        public int CompanyId { set; get; }
        public DateTime AssesDay { set; get; }
        public string ApplyState { set; get; }
        public string EmployeeName { set; get; }
        public string Department { set; get; }
        public string ApplyContent { set; get; }
        public string ApplyPerson { set; get; }
        public DateTime PostDate { set; get; }
        public DateTime ApplyDate { set; get; }
        public string LeaveType { set; get; }
        public DateTime LeaveStart { set; get; }
        public DateTime LeaveEnd { set; get; }
        public decimal LeaveHour { set; get; }
        public string ReturnContent { set; get; }
    }
}