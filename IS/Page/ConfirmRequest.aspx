<%@ Page Title="" Language="C#" MasterPageFile="~/MainFrame.Master" AutoEventWireup="true" CodeBehind="ConfirmRequest.aspx.cs" Inherits="IS.Page.ConfirmRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Confirm Request
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentDetail" runat="server">
    <asp:Label runat="server" class="font-weight-bold">ยืนยันการอนุมัติการลา</asp:Label>
    <asp:DataGrid ID="dtgList" runat="server" AutoGenerateColumns="false" HorizontalAlign="Center">
        <HeaderStyle BackColor="Gainsboro" HorizontalAlign="Center" />
        <Columns>
            <asp:TemplateColumn HeaderText="อนุญาต">
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:RadioButton ID="rd1" GroupName="rb" runat="server" />
                </ItemTemplate>
            </asp:TemplateColumn>
           <%-- <asp:TemplateColumn HeaderText="ไม่อนุญาต">
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:RadioButton ID="rd2" GroupName="rb" runat="server" />
                </ItemTemplate>
            </asp:TemplateColumn>--%>
            <asp:BoundColumn DataField="reqID" Visible="false"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="รหัสพนักงาน" DataField="EmpCode"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="ชื่อพนักงาน" DataField="Name"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="ประเภทการลา" DataField="Type"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="วันที่ลา" DataField="LeaveDate"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="ผู้อนุมัติ" DataField="confirmName"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="สาเหตุ" DataField="Note"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    <div style="display: flex; align-items: center; justify-content: center; margin-top:10px;">
        <asp:Button ID="btnSave" runat="server" Text="ตกลง" OnClick="btnSave_Click" />&nbsp;
        <%--<asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" />--%>
    </div>
</asp:Content>
