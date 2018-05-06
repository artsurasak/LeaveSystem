<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageLeave.aspx.cs" Inherits="IS.Page.popUp.ManageLeave" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Bootstrape/CSS/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Table runat="server" CellPadding="5" CellSpacing="5">
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Right">
                    <asp:Label runat="server" CssClass="font-weight-normal">Role</asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList ID="ddlRole" runat="server">
                            <%--  <asp:ListItem Value="1">ผู้บริหาร</asp:ListItem>
                        <asp:ListItem Value="2">ผู้จัดการ</asp:ListItem>
                        <asp:ListItem Value="3">พนักงาน</asp:ListItem>--%>
                        </asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Right">
                    <asp:Label runat="server">ประเภทการลา</asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList ID="ddlLeaveType" runat="server"></asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Right">
                    <asp:Label runat="server">วันที่ลา</asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox runat="server" ID="txtNoLeave"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                        <asp:Button ID="btnSave" class="btn btn-secondary" runat="server" Text="บันทึก" OnClick="btnSave_Click"/>&nbsp;
                <asp:Button ID="btnCancel" class="btn btn-secondary" runat="server" Text="ยกเลิก" OnClick="btnCancel_Click" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
    </form>
</body>
</html>
