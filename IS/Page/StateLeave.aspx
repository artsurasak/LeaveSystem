<%@ Page Title="" Language="C#" MasterPageFile="~/MainFrame.Master" AutoEventWireup="true" CodeBehind="StateLeave.aspx.cs" Inherits="IS.Page.StateLeave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    สถิติการลา
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentDetail" runat="server">
    <asp:Label runat="server" class="font-weight-bold">
        <h4><i class="fa fa-list-ul" style="font-size:24px"></i>&nbsp;สถิติการลา</h4>
    </asp:Label>
    <asp:DataGrid CssClass="table table-hover" ID="gvState" runat="server" HorizontalAlign="Center" AutoGenerateColumns="false" >
        <HeaderStyle BackColor="#ae56c4" ForeColor="White" Font-Size="Larger" HorizontalAlign="Center" />
        <Columns>
            <asp:BoundColumn HeaderText="ประเภทการลา" DataField="TYPE"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="จำนวนวันลาทั้งหมด" DataField="NO_LEAVE"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="จำนวนวันลาไปแล้ว" DataField="NoLeave"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="จำนวนวันลาคงเหลือ" DataField="remain"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
</asp:Content>
