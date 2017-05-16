<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SalesReport.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>后台 - 登录</title>
    <script type="text/javascript" src="Widgets/jquery-2.1.3.min.js"></script>
    <script type="text/javascript" src="Widgets/bootstrap-3.3.5-dist/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="Widgets/bootstrap-3.3.5-dist/css/bootstrap.min.css" media="screen" />
</head>
<body style="padding-top:15%;">
<div class="container" style="width:100%;text-align:center;">
    <form action="Login.aspx" method="post" class="form-inline">
        
        <div class="form-group">
            <label for="loginPassword">账号</label>
            <input class="form-control" id="loginUsername" type="text" name="loginUsername" value="a" />
        </div>
        <div class="form-group">
            <label for="loginPassword">口令</label>
            <input class="form-control" id="loginPassword" type="password" name="loginPassword" value="b" />
        </div>
        <input class="form-control btn btn-success" type="submit" id="btnLogin" value="确定" />
        <br />
        <div class="form-group">
            <label runat="server" id="lbMsg"></label>
        </div>
    </form>
    <div><a style="color:#b2b2b2" href="http://www.tqlsgroup.com" target="_blank">铁骑力士</a></div>
</div>
</body>
</html>
