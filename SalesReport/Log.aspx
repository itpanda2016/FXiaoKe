<%@ Page Title="" Language="C#" MasterPageFile="~/SiteHeader.Master" AutoEventWireup="true" CodeBehind="Log.aspx.cs" Inherits="SalesReport.Log" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>操作日志</h1>
    <p>当前时间：<%=DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") %></p>
    <div class="container">
        使用GridView控件显示
    </div>
</asp:Content>
