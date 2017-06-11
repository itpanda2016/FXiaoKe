using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesReport.Models {
    public class DailyLog {
        public int Id { set; get; }
        public DateTime AssesDay { set; get; }
        public int CompanyId { set; get; }
        public string EmployeeName { set; get; }
        public string Department { set; get; }
        public string Position { set; get; }
        public int Score { set; get; }      //日志被点评数
        public decimal ScoreAvg { set; get; }     //日志被点评平均分
        public int DlsSum { set; get; }     //日志总数
        public string ScoreChs { set; get; }    //日志得分中文【可不用】
        public DateTime DlsDateTime { set; get; }   //日记统计时间
        public int DlsType { set; get; }        //日志类型（-1表示未填，1表示已填）
    }
}