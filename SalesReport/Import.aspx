<%@ Page Title="" Language="C#" MasterPageFile="~/SiteHeader.Master" AutoEventWireup="true" CodeBehind="Import.aspx.cs" Inherits="SalesReport.Import" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>导入数据</h1>
    <p>当前时间：<%=DateTime.Now.ToString() %></p>
</asp:Content>
