﻿using FROST.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SalesReport {
    public partial class Export : System.Web.UI.Page {
        public string sql;
        public DataTable dtCompanys;
        protected void Page_Load(object sender, EventArgs e) {
            sql = "select id,companyName from Companys";
            dtCompanys = MsSQLHelper.ExecuteDataTable(sql);
        }
    }
}