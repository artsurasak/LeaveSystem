<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="IS.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>IS Project </title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <%--<link rel="icon" href="" />--%>
    <link href="Bootstrape/CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="Bootstrape/CSS/signin.css" rel="stylesheet" />
</head>
<body>
    <div class="container">

        <form class="form-signin" runat="server">
            <h2 class="form-signin-heading">Please Log in</h2>
            <asp:Label class="sr-only" runat="server">User Name:</asp:Label>
            <asp:TextBox ID="txtUsername" placeholder="UserName" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:Label class="sr-only" runat="server">Password</asp:Label>
            <asp:TextBox ID="txtPassword" placeholder="Password" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>
            <%--<input type="password" id="inputPassword" class="form-control" placeholder="Password" required>--%>
           <%-- <div class="checkbox">
                <label>
                    <input type="checkbox" value="remember-me">
                    Remember me
         
                </label>
            </div>--%>
            <asp:Button ID="btnSubmit" class="btn btn-lg btn-primary btn-block" Text="Submit" runat="server" OnClick="btnSubmit_Click" />
        </form>

    </div>
    <!-- /container -->
</body>
</html>
