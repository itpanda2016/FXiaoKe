using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesReport.Models {
    public class User {
        public int Id { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public DateTime CreateDate { set; get; }
        public DateTime LoginDate { set; get; }
        public int State { set; get; }
        public string UserRole { set; get; }
    }
}