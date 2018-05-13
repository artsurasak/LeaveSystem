<%@ Page Title="" Language="C#" MasterPageFile="~/MainFrame.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="IS.Page.Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentDetail" runat="server">
    <%--<script src="https://code.jquery.com/jquery-1.10.2.js"></script>--%>
    <script type="text/javascript" src="../Bootstrape/JS/jquery/jquery-3.3.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //setRblList("1", $("#ContentDetail_ddlYears").val());
            $("#ContentDetail_rblPeriod").on('change', function () {
                var Years = $("#ContentDetail_ddlYears").val();
                if (document.getElementById("ContentDetail_rblPeriod_0").checked == true) {
                    setRblList("1", Years);
                } else if (document.getElementById("ContentDetail_rblPeriod_1").checked == true) {
                    setRblList("2", Years);
                }
            });
            $("#ContentDetail_ddlYears").change(function () {
                var Years = $("#ContentDetail_ddlYears").val();
                var Mode;
                if (document.getElementById("ContentDetail_rblPeriod_0").checked == true) {
                    Mode = "1"
                } else if (document.getElementById("ContentDetail_rblPeriod_1").checked == true) {
                    Mode = "2"
                }
                setRblList(Mode, Years);
            });
        });

        function setRblList(Mode, Years) {
            var strYear;
            strYear = Years.substring(2, 4);
            if (Mode == '1') {
                var strTextVocation;
                var HeaderPeriodMonth;
                strTextVocation = "เม.ย " + strYear + " - ก.ย " + strYear
                HeaderPeriodMonth = "ตั้งแต่เดือน 1 เมษายน " + Years + " - 30 กันยายน " + Years;
                $("#ContentDetail_M1").text("เมษายน");
                $("#ContentDetail_M2").text("พฤษภาคม");
                $("#ContentDetail_M3").text("มิถุนายน");
                $("#ContentDetail_M4").text("กรกฏาคม");
                $("#ContentDetail_M5").text("สิงหาคม");
                $("#ContentDetail_M6").text("กันยายน");
                //$("#VocationPeriod").text(strTextVocation)
            } else if (Mode == '2') {
                var strTextVocation;
                strTextVocation = "ต.ค " + strYear + " - มี.ค " + strYear
                HeaderPeriodMonth = "ตั้งแต่เดือน 1 ตุลาคม " + Years + " - 31 มีนาคม " + Years;
                $("#ContentDetail_M1").text("ตุลาคม");
                $("#ContentDetail_M2").text("พฤศจิกายน");
                $("#ContentDetail_M3").text("ธันวาคม");
                $("#ContentDetail_M4").text("มกราคม");
                $("#ContentDetail_M5").text("กุมภาพันธ์");
                $("#ContentDetail_M6").text("มีนาคม");
                //$("#VocationPeriod").text(strTextVocation)
            }
            $("#ContentDetail_VacationPeriod").text(strTextVocation)
            $("#ContentDetail_HeaderPeriodMonth").text(HeaderPeriodMonth)
        }
    </script>
    <asp:Label runat="server" class="font-weight-bold"><h4><span class="fa fa-list-alt"></span>&nbsp;รายงาน</h4></asp:Label>
    <asp:Table runat="server" CellPadding="5">
        <asp:TableRow>
            <asp:TableCell>เลือกช่วงเวลา</asp:TableCell>
            <asp:TableCell>
                <asp:RadioButtonList runat="server" CssClass="form-control" BorderStyle="None" ID="rblPeriod">
                    <asp:ListItem Value="1" Selected="True">ช่วงแรกของเดือน(เม.ย. - ก.ย.)</asp:ListItem>
                    <asp:ListItem Value="2">ช่วงหลังของเดือน(ต.ค. - มี.ค.)</asp:ListItem>
                </asp:RadioButtonList>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>ปีที่ต้องการ</asp:TableCell>
            <asp:TableCell>
                <asp:DropDownList Width="100%" ID="ddlYears" CssClass="form-control" runat="server"></asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                <asp:Button ID="btnSearch" runat="server" CssClass="btn" BackColor="#ae56c4" ForeColor="White" Text="ค้นหา" OnClick="btnSearch_Click" />&nbsp;
                <asp:Button ID="btnExport" runat="server" CssClass="btn" BackColor="#ae56c4" ForeColor="White" Text="Export" OnClick="btnExport_Click" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <div id="divContainData" runat="server">
        <asp:Table runat="server" Width="100%" HorizontalAlign="Center">
            <asp:TableHeaderRow HorizontalAlign="Center">
                <asp:TableHeaderCell ColumnSpan="12">
                    <asp:Label runat="server"><h4>แบบรายงานการปฎิบัติราชการของลูกจ้างประจำ สังกัด สำนักพัฒนาอุตสาหกรรมชุมชน</h4></asp:Label>
                </asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableHeaderRow HorizontalAlign="Center">
                <asp:TableHeaderCell ColumnSpan="12">
                    <asp:Label runat="server" ID="HeaderPeriodMonth"></asp:Label>
                </asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
        <asp:Table ID="tblist" CssClass="table table-hover" BorderColor="Black" GridLines="Both" BorderStyle="Solid" HorizontalAlign="Center" Width="99%" runat="server">
            <asp:TableHeaderRow HorizontalAlign="Center" style="background-color:#ae56c4; font-weight:normal; font-size:larger; color:white">
                <asp:TableHeaderCell RowSpan="4"><asp:Label runat="server">ลำดับที่</asp:Label></asp:TableHeaderCell>
                <asp:TableHeaderCell RowSpan="4"><asp:Label runat="server">ชื่อ - สกุล</asp:Label></asp:TableHeaderCell>
                <asp:TableHeaderCell RowSpan="4"><asp:Label runat="server">ตำแหน่ง</asp:Label></asp:TableHeaderCell>
                <asp:TableHeaderCell>
                    <asp:Label runat="server" ID="M1"></asp:Label>
                </asp:TableHeaderCell>
                <asp:TableHeaderCell>
                    <asp:Label runat="server" ID="M2"></asp:Label>
                </asp:TableHeaderCell>
                <asp:TableHeaderCell>
                    <asp:Label runat="server" ID="M3"></asp:Label>
                </asp:TableHeaderCell>
                <asp:TableHeaderCell>
                    <asp:Label runat="server" ID="M4"></asp:Label>
                </asp:TableHeaderCell>
                <asp:TableHeaderCell>
                    <asp:Label runat="server" ID="M5"></asp:Label>
                </asp:TableHeaderCell>
                <asp:TableHeaderCell>
                    <asp:Label runat="server" ID="M6"></asp:Label>
                </asp:TableHeaderCell>
                <asp:TableHeaderCell ColumnSpan="3"></asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableHeaderRow HorizontalAlign="Center" style="background-color:#ae56c4; font-size:larger; color:white">
                <asp:TableHeaderCell RowSpan="3"><asp:Label runat="server">ลาป่วย (วัน/ครั้ง)</asp:Label></asp:TableHeaderCell>
                <asp:TableHeaderCell RowSpan="3"><asp:Label runat="server">ลาป่วย (วัน/ครั้ง)</asp:Label></asp:TableHeaderCell>
                <asp:TableHeaderCell RowSpan="3"><asp:Label runat="server">ลาป่วย (วัน/ครั้ง)</asp:Label></asp:TableHeaderCell>
                <asp:TableHeaderCell RowSpan="3"><asp:Label runat="server">ลาป่วย (วัน/ครั้ง)</asp:Label></asp:TableHeaderCell>
                <asp:TableHeaderCell RowSpan="3"><asp:Label runat="server">ลาป่วย (วัน/ครั้ง)</asp:Label></asp:TableHeaderCell>
                <asp:TableHeaderCell RowSpan="3"><asp:Label runat="server">ลาป่วย (วัน/ครั้ง)</asp:Label></asp:TableHeaderCell>
                <asp:TableHeaderCell ColumnSpan="2"><asp:Label runat="server">รวม</asp:Label></asp:TableHeaderCell>
                <asp:TableHeaderCell RowSpan="3">
                    <asp:Label runat="server">ลาพักผ่อน</asp:Label><br />
                    <asp:Label ID="VacationPeriod" runat="server"></asp:Label>
                </asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableHeaderRow HorizontalAlign="Center" style="background-color:#ae56c4; font-size:larger; color:white">
                <asp:TableHeaderCell RowSpan="2"><asp:Label runat="server">ลาป่วย (วัน/ครั้ง)</asp:Label></asp:TableHeaderCell>
                <asp:TableHeaderCell RowSpan="2"><asp:Label runat="server">ลากิจ (วัน/ครั้ง)</asp:Label></asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableRow>
            </asp:TableRow>
        </asp:Table>
    </div>
</asp:Content>
