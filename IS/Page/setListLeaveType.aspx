<%@ Page Title="" Language="C#" MasterPageFile="~/MainFrame.Master" AutoEventWireup="true" CodeBehind="setListLeaveType.aspx.cs" Inherits="IS.Page.setListLeaveType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    จัดการการลา
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentDetail" runat="server">
    <script type="text/javascript">
        function popUpManageLeave(Role, Leave) {
            var url = "popUp/ManageLeave.aspx?Role=" + Role + "&Leave=" + Leave;
            var h = 400;
            var w = 400;
            var title = 'test';
            var left = (screen.width / 2) - (w / 2);
            var top = (screen.height / 2) - (h / 2);
            return window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
        }
    </script>
    <asp:Label runat="server" class="font-weight-bold">

        <h4><i class="fa fa-gears" style="font-size:24px"></i>&nbsp;จัดการการลา</h4>
    </asp:Label>
    <div style="width:99%; text-align:right;">
        <asp:Button ID="btnAdd" runat="server" OnClientClick="popUpManageLeave();" CssClass="btn" BackColor="#ae56c4" ForeColor="White" Text="Add" />
    </div>
    <asp:DataGrid ID="dtg" CssClass="table table-hover" Width="99%" runat="server" HorizontalAlign="Center" AllowSorting="true" AutoGenerateColumns="false">
        <HeaderStyle BackColor="#ae56c4" ForeColor="White" Font-Size="Larger" HorizontalAlign="Center" />
        <Columns>
            <asp:BoundColumn HeaderText="Role" DataField="Role"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="ประเภทการลา" DataField="LeaveType"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="จำนวนวันที่ลาได้" DataField="NoLeave"></asp:BoundColumn>
            <asp:BoundColumn DataField="RoleID" Visible="false"></asp:BoundColumn>
            <asp:BoundColumn DataField="Leave" Visible="false"></asp:BoundColumn>
            <asp:TemplateColumn HeaderText="แก้ไข">
                <ItemTemplate>
                    <asp:LinkButton ID="Link1" runat="server" CommandName="Edit" Text="Edit" 
                        OnClientClick='<%#string.Format("return popUpManageLeave(\"{0}\",\"{1}\");",DataBinder.Eval(Container.DataItem, "RoleID"),DataBinder.Eval(Container.DataItem, "Leave")) %>'>Edit</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateColumn>
        </Columns>

    </asp:DataGrid>
</asp:Content>
