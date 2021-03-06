﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MainFrame.Master" AutoEventWireup="true" CodeBehind="ReportStatusLeave.aspx.cs" Inherits="IS.Page.ReportStatusLeave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    รายงานวันลา
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentDetail" runat="server">
    <asp:Label runat="server" class="font-weight-bold"><h4><span class="fa fa-list-alt"></span>&nbsp;รายงานสถานะคำร้องขอลา</h4></asp:Label>
    <asp:Table runat="server" ID="tblSearch" CellPadding="5" CellSpacing="5">
        <asp:TableRow>
            <asp:TableCell>รหัสพนักงาน</asp:TableCell>
            <asp:TableCell><asp:TextBox ID="txtEmpCode" CssClass="form-control" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>ชื่อ</asp:TableCell>
            <asp:TableCell><asp:TextBox ID="txtName" CssClass="form-control" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>ประเภทการลา</asp:TableCell>
            <asp:TableCell><asp:DropDownList CssClass="form-control" ID="ddlTypeLeave" runat="server"></asp:DropDownList></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                <asp:Button ID="btnSearch" Text="ค้นหา" runat="server" CssClass="btn" BackColor="#ae56c4" ForeColor="White" OnClick="btnSearch_Click" />&nbsp;
                <asp:Button ID="btnExport" runat="server" Text="Export" CssClass="btn" BackColor="#ae56c4" ForeColor="White" OnClick="btnExport_Click" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <asp:DataGrid HorizontalAlign="Center" CssClass="table table-hover" ID="dtgList" runat="server" AutoGenerateColumns="false">
        <HeaderStyle BackColor="#ae56c4" ForeColor="White" Font-Size="Larger" HorizontalAlign="Center" />
        <ItemStyle HorizontalAlign="Center" />
        <Columns>
            <asp:BoundColumn HeaderText="รหัสพนักงาน" DataField="EmpCode"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="ชื่อพนักงาน" DataField="Name"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="วันที่สร้างคำร้องการลา" DataField="CreateDate"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="วันที่ลา" DataField="LeaveDate"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="ประเภทการลา" DataField="LeaveType"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="สถานะอนุมัติ" DataField="Status"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="ผู้อนุมัติ" DataField="ApprName"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="วันเวลาอนุมัติ" DataField="ApprDate"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="สถานะยืนยัน" DataField="confirmStatus"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="ผู้ยืนยัน" DataField="confirmName"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="วันเวลายืนยัน" DataField="confirmDate"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
</asp:Content>
