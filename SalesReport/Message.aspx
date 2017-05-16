<%@ Page Title="" Language="C#" MasterPageFile="~/SiteHeader.Master" AutoEventWireup="true" CodeBehind="Message.aspx.cs" Inherits="SalesReport.Message" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="strMsg">
        <p><label runat="server" id="msgTitle"></label></p>
        <p><label runat="server" id="msgContent"></label></p>
        <p><a id="msgHref" runat="server" href="javascript:;" title=""></a></p>
    </div>
</asp:Content>
