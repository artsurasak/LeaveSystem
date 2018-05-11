<%@ Page Title="" Language="C#" MasterPageFile="~/MainFrame.Master" AutoEventWireup="true" CodeBehind="HistoryLeave.aspx.cs" Inherits="IS.Page.HistoryLeave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    ประะวัติการลา
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentDetail" runat="server">
    <asp:Label runat="server" class="font-weight-bold">ประวัติการลา</asp:Label>
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

        function calculateTime() {
            if (fdate != '' && ftime != '' && tdate != '' && ttime != '') {
                var fdate = new Date($("#txtFDateLeave").val());
                var ftime = $("#txtFTimeLeave").val();
                var tdate = new Date($("#txtTDateLeave").val());
                var ttime = $("#txtTTimeLeave").val();
                var timediff = Math.abs(tdate.getTime() - fdate.getTime());
                var diffDays = Math.ceil(timediff / (1000 * 3600 * 24));
            }
        }
    </script>
        <asp:Table runat="server">
            <asp:TableRow><asp:TableCell>เลือกวันที่</asp:TableCell></asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:RadioButtonList ID="rblType" runat="server">
                        <asp:ListItem Value="C" Selected="True" Text="วันที่สร้างคำร้องการลา"></asp:ListItem>
                        <asp:ListItem Value="L" Text="วันที่ลา"></asp:ListItem>
                    </asp:RadioButtonList>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtFDate" CssClass="datepicker" runat="server"></asp:TextBox>&nbsp;
                    <asp:Label runat="server">ถึง</asp:Label>&nbsp;
                    <asp:TextBox ID="txtTDate" CssClass="datepicker" runat="server"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell><asp:Label runat="server">ประเภทวันลา</asp:Label></asp:TableCell>
                <asp:TableCell><asp:DropDownList ID="ddlLeaveType" runat="server"></asp:DropDownList></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                    <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" OnClick="btnSearch_Click" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
</asp:Content>
