<%@ Page Title="" Language="C#" MasterPageFile="~/MainFrame.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="IS.Page.UserList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    รายการ User
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentDetail" runat="server">
    <script type="text/javascript">
        function editUser(userName) {
            var url = "CreateUserList.aspx?Mode=Edit&userName=" + userName;
            return window.open(url, '_blank');
        }
    </script>
    <asp:Label runat="server" class="font-weight-bold"><h4><span class="fa fa-list-alt"></span>&nbsp;รายการผู้ใช้งาน</h4></asp:Label>
    <asp:DataGrid ID="dtg" Width="100%" CssClass="table table-hover" runat="server" AutoGenerateColumns="false" HorizontalAlign="Center">
        <HeaderStyle BackColor="#ae56c4" ForeColor="White" Font-Size="Larger" HorizontalAlign="Center" />
        <Columns>
            <asp:BoundColumn HeaderText="รหัสพนักงาน" DataField="EMP_CODE"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="ชื่อ - นามสกุล" DataField="NAME"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="UserName" DataField="USER_NAME"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="ชื่อกลุ่ม" DataField="GroupName"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="Line_ID" DataField="LINE_ID"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="แผนก" DataField="DEPARTMENT_NAME"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="Email" DataField="EMAIL"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="ผู้อนุมัติ" DataField="APPROVE_NAME"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="ผู้ยืนยัน" DataField="COMFIRM_NAME"></asp:BoundColumn>
            <asp:TemplateColumn HeaderText="แก้ไข">
                <ItemTemplate>
                    <asp:LinkButton ID="Link1" runat="server" CommandName="Edit" Text="Edit"
                        OnClientClick='<%#string.Format("return editUser(\"{0}\");",DataBinder.Eval(Container.DataItem, "USER_NAME")) %>'>Edit</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateColumn>
        </Columns>
    </asp:DataGrid>
</asp:Content>
