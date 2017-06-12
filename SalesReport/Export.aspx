<%@ Page Title="" Language="C#" MasterPageFile="~/SiteHeader.Master" AutoEventWireup="true" CodeBehind="Export.aspx.cs" Inherits="SalesReport.Export" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#fmDate1").datepicker();
            $("#fmDate1").datepicker("option", "dateFormat", "yy-mm-dd");
            $("#fmDate2").datepicker();
            $("#fmDate2").datepicker("option", "dateFormat", "yy-mm-dd");
        })
    </script>
    <style type="text/css">
        .btn_width{
            width:80px;
        }
        .input_width{
            width:220px;
        }
        .file_width{
            width:600px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form action="Handlers/HandlerExport.ashx" method="post">
        <div class="form-inline">
            <label>开始时间：</label><br />
            <input type="text" name="fmDate1" id="fmDate1" value="" class="form-control" style="width:300px;"/> <br />
            <label>结束时间：</label><br />
            <input type="text" name="fmDate2" id="fmDate2" value="" class="form-control" style="width:300px;"/>
        </div>
        <div>
            <label>选择公司：</label>
            <select name="fmCompany" class="form-control" style="width:300px;">
                <option value="-1">-请选择-</option>
                <%
                    if (dtCompanys != null && dtCompanys.Rows.Count > 0) {
                        for (int i = 0; i < dtCompanys.Rows.Count; i++) {
                            Response.Write("<option value=\"" + dtCompanys.Rows[i][0] + "\">" + dtCompanys.Rows[i][1] + "</option>");
                        }
                    }
                %>
            </select>
        </div>
        <div class="form-inline">
            <label>上传考核模板文件</label>
            <input class="file_width" type="file" name="fmFile" value="" />
        </div>
        <input type="hidden" name="mod" value="dailylog" />
        <input type="submit" name="btnSubmit" value="导出报表" class="btn btn-success btn-width" />
    </form>
</asp:Content>
