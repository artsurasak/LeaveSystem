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
   <%-- <link href="Bootstrape/CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="Bootstrape/CSS/signin.css" rel="stylesheet" />--%>
    <link href="Bootstrape/CSS/custom.css?version=1" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.0.0/css/bootstrap.min.css" />
	<script src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.0.0/js/bootstrap.min.js"></script>
	<link rel="stylesheet" type="text/css" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" />
</head>
<body class="body-login" runat="server">
    <%--<form runat="server">--%>
        <div class="container">
            <div class="row">
                <div class="col-md-4 offset-md-4 col-10 offset-1 login-main border rounded pb-4 pt-5">
                    <form action="" runat="server">
                        <div class="row">
                            <div class="col-md-4 col-12 heading-image text-white text-center" style="background-color: #632b87">
                                <i class="fa fa-camera" aria-hidden="true"></i>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 col-12 mt-5">
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text bg-white border-0 p-3"><i class="fa fa-user"></i></span>
                                    </div>
                                    <asp:TextBox ID="txtUsername" CssClass="form-control border-0 pl-3" placeholder="UserName" runat="server" aria-describedby="validationTooltipUsernamePrepend" required></asp:TextBox>
                                    <%--<input type="text" class="form-control border-0 pl-3" placeholder="Username" aria-describedby="validationTooltipUsernamePrepend" required>--%>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 col-12 mt-3">
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text bg-white border-0 p-3" id="validationTooltipUsernamePrepend"><i class="fa fa-lock" aria-hidden="true"></i></span>
                                    </div>
                                    <asp:TextBox ID="txtPassword" placeholder="Password" TextMode="Password" runat="server" CssClass="form-control border-0 pl-3 pb-0" aria-describedby="validationTooltipUsernamePrepend" required></asp:TextBox>
                                    <%--<input type="password" class="form-control border-0 pl-3 pb-0" id="validationTooltipUsername" placeholder="*******" aria-describedby="validationTooltipUsernamePrepend" required>--%>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 col-12 mt-3">
                                <asp:Button ID="btnSubmit" class="btn btn-info rounded-0 w-100" Text="Submit" runat="server" OnClick="btnSubmit_Click" style="background-color: #8b36c1; border-color: #8b36c1;"/>
                                <%--<button type="submit" class="btn btn-info rounded-0 w-100" style="background-color: #8b36c1; border-color: #8b36c1;">LOGIN</button>--%>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 col-12 mt-3 options">
                                <div class="row">
                                    <div class="col-md-6 col-12 pl-4">
                                        <div class="custom-control custom-checkbox mr-sm-2">
                                            <input type="checkbox" class="custom-control-input" id="customControlAutosizing">
                                            <label class="custom-control-label text-white" for="customControlAutosizing">Remember me</label>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-12 pl-4">
                                        <a href="#" class="text-white"><i>Forgot Password?</i></a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
                <%--<div class="col-md-4 offset-md-4 col-12 text-white text-center mt-2 copyright-main">
                    <p>Copyright &copy; 2018 Aatman Infotech,Inc</p>
                </div>--%>
            </div>
        </div>
        <ul class="bubble-boxes">
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
        </ul>
    <%--</form>--%>
    <%--<div class="container">

        <form class="form-signin" runat="server">
            <h2 class="form-signin-heading">Please Log in</h2>
            <asp:Label class="sr-only" runat="server">User Name:</asp:Label>
            <asp:TextBox ID="txtUsername" placeholder="UserName" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:Label class="sr-only" runat="server">Password</asp:Label>
            <asp:TextBox ID="txtPassword" placeholder="Password" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:Button ID="btnSubmit" class="btn btn-lg btn-primary btn-block" Text="Submit" runat="server" OnClick="btnSubmit_Click" />
        </form>

    </div>--%>
    <!-- /container -->
</body>
</html>
