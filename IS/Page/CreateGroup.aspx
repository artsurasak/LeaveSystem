<%@ Page Title="" Language="C#" MasterPageFile="~/MainFrame.Master" AutoEventWireup="true" CodeBehind="CreateGroup.aspx.cs" Inherits="IS.Page.CreateGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    CreateGroup
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentDetail" runat="server">
    <input type="hidden" runat="server" id="hdCountLine" />
    <asp:Label runat="server"><h4><i class="fa fa-group" style="font-size:24px"></i>&nbsp;สร้างกลุ่ม</h4></asp:Label><br />
    <asp:Label runat="server">ชื่อกลุ่ม</asp:Label>&nbsp;
    <asp:DropDownList ID="ddlGroup" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged"></asp:DropDownList>&nbsp;
    <asp:TextBox ID="txtAddGroup" runat="server" Visible="false"></asp:TextBox>&nbsp;
    <asp:Button ID="btnAddGroup" runat="server" CssClass="btn" BackColor="#ae56c4" ForeColor="White" Text="Add Group" OnClick="btnAddGroup_Click" />
    <asp:Table runat="server" ID="tbList"  CssClass="table table-hover" GridLines="Both"  HorizontalAlign="Center">
        <%--<asp:TableHeaderRow CssClass="table-active">
            <asp:TableHeaderCell><asp:Label runat="server">Menu</asp:Label></asp:TableHeaderCell>
            <asp:TableHeaderCell><asp:Label runat="server">Permission</asp:Label></asp:TableHeaderCell>
        </asp:TableHeaderRow>--%>
    </asp:Table>
    <div style="text-align: center;">
        <asp:Button ID="btnSave" CssClass="btn" BackColor="#ae56c4" ForeColor="White" runat="server" OnClick="btnSave_Click" Text="Save" />&nbsp;
        <asp:Button ID="btnCancelAddGroup" CssClass="btn btn-secondary" runat="server" OnClick="btnCancelAddGroup_Click" Text="Cancel Add Group" Visible="false" />
    </div>

</asp:Content>
