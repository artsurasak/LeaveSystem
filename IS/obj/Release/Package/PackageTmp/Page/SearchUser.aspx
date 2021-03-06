﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MainFrame.Master" AutoEventWireup="true" CodeBehind="SearchUser.aspx.cs" Inherits="IS.Page.SearchUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    ค้นหาผู้ใช้งาน
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentDetail" runat="server">
    <asp:Label runat="server">ค้นหาผู้ใช้งาน</asp:Label>
    <asp:Table runat="server">
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2"><asp:Button ID="btnAdd" runat="server" Text="+ สร้างผู้ใช้งาน" OnClick="btnAdd_Click" /></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell><asp:Label runat="server">Username</asp:Label></asp:TableCell>
            <asp:TableCell><asp:TextBox runat="server" ID="txtUserName"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell><asp:Label runat="server">ขื่อพนักงาน</asp:Label></asp:TableCell>
            <asp:TableCell><asp:TextBox runat="server" ID="txtName"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell><asp:Label runat="server">ชื่อกลุ่ม</asp:Label></asp:TableCell>
            <asp:TableCell><asp:DropDownList ID="ddlGroup" runat="server"></asp:DropDownList></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell><asp:Label runat="server">รหัสพนักงาน</asp:Label> </asp:TableCell>
            <asp:TableCell><asp:TextBox ID="txtEmpCode" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell><asp:Label runat="server">แผนก</asp:Label></asp:TableCell>
            <asp:TableCell><asp:DropDownList ID="ddlDep" runat="server"></asp:DropDownList></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                <asp:Button runat="server" ID="btnSearch" OnClick="btnSearch_Click" Text="ค้นหา" />
            </asp:TableCell>
        </asp:TableRow>
        
    </asp:Table>
</asp:Content>
