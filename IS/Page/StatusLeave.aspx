﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MainFrame.Master" AutoEventWireup="true" CodeBehind="StatusLeave.aspx.cs" Inherits="IS.Page.StatusLeave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    สถานะคำร้องขอลา
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentDetail" runat="server">
    <asp:Label runat="server" class="font-weight-bold">
        <h4><i class="fa fa-th-list" style="font-size:24px"></i>&nbsp;สถานะคำร้องขอลา</h4>
    </asp:Label>
    <asp:Table runat="server" ID="tblSearch">
        <asp:TableRow>
            <asp:TableCell>รหัสพนักงาน</asp:TableCell>
            <asp:TableCell><asp:TextBox ID="txtEmpCode" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>ชื่อ</asp:TableCell>
            <asp:TableCell><asp:TextBox ID="txtName" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2" HorizontalAlign="Center"><asp:Button ID="btnSearch" Text="ค้นหา" runat="server" OnClick="btnSearch_Click" /></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <asp:DataGrid CssClass="table table-hover" HorizontalAlign="Center" ID="dtgList" runat="server" AutoGenerateColumns="false">
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
