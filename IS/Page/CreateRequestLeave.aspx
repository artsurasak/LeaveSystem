﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MainFrame.Master" AutoEventWireup="true" CodeBehind="CreateRequestLeave.aspx.cs" Inherits="IS.Page.CreateRequestLeave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    สร้างคำร้องการลา
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentDetail" runat="server">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link rel="stylesheet" href="/resources/demos/style.css" />
    <script type="text/javascript" src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script type="text/javascript" src="../WickedPicker/src/wickedpicker.js"></script>
    <link rel="stylesheet" href="../WickedPicker/stylesheets/wickedpicker.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("#ddlLeaveType").change(function () {
                alert('Change');
            });
        });

        $(function () {
            $(".form-control.datepicker").datepicker({
                dateFormat: "dd/mm/yy",
            });
            var currentdate = new Date();
            var timenow = currentdate.getHours() + " : " + currentdate.getMinutes();
            var ftime = '<%= hdftime.Value %>';
            if (ftime == '') { ftime = timenow; }
            var ttime = '<%=  hdttime.Value %>';
            if (ttime == '') { ttime = timenow; }
            $('.form-control.timepicker').wickedpicker({
                //now: "" + ftime + "",
                now: ftime,
                twentyFour: true,
                upArrow: 'wickedpicker__controls__control-up', //The up arrow class selector to use, for custom CSS 
                downArrow: 'wickedpicker__controls__control-down',//The down arrow class selector to use, for custom CSS
                close: 'wickedpicker__close', //The close class selector to use, for custom CSS 
                hoverState: 'hover-state', //The hover state class to use, for custom CSS 
                title: 'Timepicker', //The Wickedpicker's title, 
                showSeconds: false, //Whether or not to show seconds, 
                secondsInterval: 1, //Change interval for seconds, defaults to 1  , 
                minutesInterval: 1, //Change interval for minutes, defaults to 1 
                beforeShow: null, //A function to be called before the Wickedpicker is shown 
                show: null, //A function to be called when the Wickedpicker is shown 
                clearable: false //Make the picker's input clearable (has clickable "x")  };
            });

            $('.form-control.timepicker1').wickedpicker({
                //now: "" + ftime + "",
                now: ttime,
                twentyFour: true,
                upArrow: 'wickedpicker__controls__control-up', //The up arrow class selector to use, for custom CSS 
                downArrow: 'wickedpicker__controls__control-down',//The down arrow class selector to use, for custom CSS
                close: 'wickedpicker__close', //The close class selector to use, for custom CSS 
                hoverState: 'hover-state', //The hover state class to use, for custom CSS 
                title: 'Timepicker', //The Wickedpicker's title, 
                showSeconds: false, //Whether or not to show seconds, 
                secondsInterval: 1, //Change interval for seconds, defaults to 1  , 
                minutesInterval: 1, //Change interval for minutes, defaults to 1 
                beforeShow: null, //A function to be called before the Wickedpicker is shown 
                show: null, //A function to be called when the Wickedpicker is shown 
                clearable: false //Make the picker's input clearable (has clickable "x")  };
            });
            //}
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
    <asp:HiddenField ID="hdftime" runat="server" Value="" />
    <asp:HiddenField ID="hdttime" runat="server" Value="" />
    <asp:Label runat="server" class="font-weight-bold">
        <h4 style="display:inline"><span class="fa fa-edit"></span>&nbsp;สร้างคำร้องการลา</h4>
        
    </asp:Label>
    <%--<hr />--%>
    <asp:Table runat="server" style="margin-top:20px;" CellPadding="5" CellSpacing="5" HorizontalAlign="Center">
        <asp:TableRow>
            <asp:TableCell><asp:Label runat="server">ประเภทการลา</asp:Label></asp:TableCell>
            <asp:TableCell><asp:DropDownList ID="ddlLeaveType" CssClass="form-control" runat="server"></asp:DropDownList></asp:TableCell>
            <asp:TableCell ColumnSpan="2" HorizontalAlign="Right"><asp:Label runat="server">แนบไฟล์</asp:Label></asp:TableCell>
            <asp:TableCell ColumnSpan="4"><asp:FileUpload runat="server" ID="FileUpload" /></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell><asp:Label runat="server">วันที่ลา</asp:Label></asp:TableCell>
            <asp:TableCell><asp:TextBox ID="txtFDateLeave" CssClass="form-control datepicker" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell><asp:Label runat="server">เวลา</asp:Label></asp:TableCell>
            <asp:TableCell><asp:TextBox ID="txtFTimeLeave" CssClass="form-control timepicker" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell><asp:Label runat="server">ถึง</asp:Label></asp:TableCell>
            <asp:TableCell><asp:TextBox ID="txtTDateLeave" CssClass="form-control datepicker" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell><asp:Label runat="server">เวลา</asp:Label></asp:TableCell>
            <asp:TableCell><asp:TextBox ID="txtTTimeLeave" CssClass="form-control timepicker1" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell><asp:Button runat="server" Text="คำนวณวัน" ID="btnCal" OnClick="btnCal_Click" /></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell><asp:Label runat="server">จำนวนวันที่ลา</asp:Label></asp:TableCell>
            <asp:TableCell><asp:TextBox runat="server" ID="txtDay" CssClass="form-control"></asp:TextBox></asp:TableCell>
            <asp:TableCell><asp:Label runat="server">วัน</asp:Label></asp:TableCell>
            <asp:TableCell><asp:TextBox runat="server" ID="txtHour" CssClass="form-control"></asp:TextBox></asp:TableCell>
            <asp:TableCell><asp:Label runat="server">ชั่วโมง</asp:Label></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell><asp:Label runat="server">สาเหตุการลา</asp:Label></asp:TableCell>
            <asp:TableCell ColumnSpan="2"><asp:TextBox TextMode="MultiLine" CssClass="form-control" Rows="3" runat="server" ID="txtCauseleave"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell><asp:Label ID="lbltest" runat="server">ชื่อผู้ติดต่อระหว่างลา</asp:Label></asp:TableCell>
            <asp:TableCell><asp:TextBox Widt="100%" runat="server" CssClass="form-control" ID="txtContact"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell><asp:Label runat="server">เบอร์โทรศัพท์ผู้ติดต่อระหว่างลา</asp:Label></asp:TableCell>
            <asp:TableCell><asp:TextBox Widt="100%" runat="server" CssClass="form-control" ID="txtTelContact"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="10" HorizontalAlign="Center">
                <asp:Button ID="btnSave" CssClass="btn" BackColor="#ae56c4" ForeColor="White" runat="server" Text="บันทึก" OnClick="btnSave_Click" />&nbsp;
                <asp:Button ID="btnCancel" CssClass="btn" BackColor="#ae56c4" ForeColor="White" runat="server" Text="ยกเลิก" OnClick="btnCancel_Click" />
            </asp:TableCell>

        </asp:TableRow>

    </asp:Table>
</asp:Content>
