<%@ Page Title="" Language="C#" MasterPageFile="~/SiteHeader.Master" AutoEventWireup="true" CodeBehind="ImportAttendance.aspx.cs" Inherits="SalesReport.ImportAttendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#fmDate").datepicker();
            $("#fmDate").datepicker("option", "dateFormat", "yy-mm-dd");
        })
    </script>
    <style type="text/css">
        .btn_width{
            width:80px;
        }
        .input_width{
            width:220px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>考勤签到，数据导入</h2>
    <hr />
    <form action="Handlers/HandlerImport.ashx" enctype="multipart/form-data" method="post">
        <div class="form-inline">
            <label>日期：</label>
            <input type="text" name="fmDate" id="fmDate" value="" class="form-control input_width" />
        </div>
        <div>
            <label>公司：</label>
            <select name="fmCompany" class="form-control" style="width:220px;">
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
        <p>
            <label>上传文件</label>
            <input type="file" name="fmFile" value="" />
            <!--
                暂时不需要上传多个文件
                实际这种.ashx处理的方式，这儿是可以通过Ajax去ashx文件先上传图片并载入图片到这儿，实现预览效果的
                -->
            <%--<input type="file" name="fmFile" value="" />--%>
        </p>
        <input type="hidden" name="act" value="" />
        <input type="hidden" name="mod" value="attendance" />
        <input type="submit" name="btnSubmit" value="上传" class="btn btn-success btn-width" />
    </form>

</asp:Content>
