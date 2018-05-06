<%@ Page Title="" Language="C#" MasterPageFile="~/MainFrame.Master" AutoEventWireup="true" CodeBehind="SearchRequestLeave.aspx.cs" Inherits="IS.Page.SearchRequestLeave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    ค้นหารายการร้องขออนุมัติ
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentDetail" runat="server">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link rel="stylesheet" href="/resources/demos/style.css" />
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script>
        $(function () {
            $(".datepicker").datepicker({
                dateFormat: "dd/mm/yy",
            });
        });
    </script>
    <asp:Label runat="server">ค้นหารายการร้องขออนุมัติ</asp:Label>
    <asp:Table runat="server">
        <asp:TableRow>
            <asp:TableCell>เลือกวันที่</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                    <asp:RadioButtonList ID="rblTypeDate" runat="server">
                        <asp:ListItem Value="L" Text="วันที่ลา" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="C" Text="วันที่สร้างคำร้องการลา"></asp:ListItem>
                    </asp:RadioButtonList>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="txtFDate" CssClass="datepicker" runat="server"></asp:TextBox>&nbsp;
                    <asp:Label runat="server">ถึง</asp:Label>&nbsp;
                    <asp:TextBox ID="txtTDate" CssClass="datepicker" runat="server"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell><asp:Label runat="server">ชื่อพนักงาน</asp:Label></asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell><asp:Label runat="server">รหัสพนักงาน</asp:Label></asp:TableCell>
            <asp:TableCell><asp:TextBox ID="txtEmpCode" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell><asp:Label runat="server">ประเภทการลา</asp:Label></asp:TableCell>
            <asp:TableCell><asp:DropDownList ID="ddlLeaveType" runat="server"></asp:DropDownList></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell><asp:Label runat="server">สถานะ</asp:Label></asp:TableCell>
            <asp:TableCell>
                <asp:DropDownList ID="ddlStatus" runat="server">
                    <asp:ListItem Value="*" Text="----กรุณาเลือก---"></asp:ListItem>
                    <asp:ListItem Value="A" Text="อนุมัติ"></asp:ListItem>
                    <asp:ListItem Value="I" Text="กำลังพิจารณา"></asp:ListItem>
                    <asp:ListItem Value="R" Text="ไม่อนุมัติ"></asp:ListItem>
                </asp:DropDownList>

            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" OnClick="btnSearch_Click" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
