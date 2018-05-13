<%@ Page Title="" Language="C#" MasterPageFile="~/MainFrame.Master" AutoEventWireup="true" CodeBehind="HistoryLeave1.aspx.cs" Inherits="IS.Page.HistoryLeave1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    ประวัติการลา
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentDetail" runat="server">
    <script type="text/javascript">
        function popUpManageReq(Req) {
            var url = "CreateRequestLeave.aspx?Req=" + Req
            return window.open(url, '_blank');
        }
        function deleteReq(Req) {
            var url = "popUp/DeleteRequest.aspx?Req=" + Req
            return window.open(url, '_blank', 'toolbar=no,status=no,menubar=no,scrollbars=no,resizable=no,left=50000, top=50000, width=10, height=10, visible=none');
            window.close();
        }
    </script>
    <asp:Label ID="lbl1" runat="server"></asp:Label>
    <asp:Label runat="server" class="font-weight-bold">
        <h4><i class="fa fa-th-list" style="font-size:24px"></i>&nbsp;ประวัติการลา</h4>
    </asp:Label>
    <asp:DataGrid ID="dtgList" CssClass="table table-hover" runat="server" AutoGenerateColumns="false" HorizontalAlign="Center">
        <HeaderStyle BackColor="#ae56c4" ForeColor="White" Font-Size="Larger" HorizontalAlign="Center" />
        <ItemStyle HorizontalAlign="Center" />
        <Columns>
            <asp:BoundColumn DataField="LeaveID" Visible="false"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="วันที่สร้างคำร้องการลา" DataField="CreateDate"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="วันที่ลา" DataField="LeaveDate"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="จำนวนวันลา" DataField="NoLeave"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="ประเภทการลา" DataField="LeaveType"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="สาเหตุการลา" DataField="Note"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="สถานะการอนุมัติ" DataField="STATUS"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="ผู้อนุมัติ" DataField="APPROVE_NAME"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="สถานะการยืนยัน" DataField="ConfirmStatus" ></asp:BoundColumn>
            <asp:BoundColumn HeaderText="ผู้ยืนยัน" DataField="COMFIRM_NAME"></asp:BoundColumn>
            <asp:TemplateColumn HeaderText="แก้ไข">
                <ItemTemplate>
                    <asp:LinkButton ID="Link1" runat="server" CommandName="Edit" Text="Edit"
                        OnClientClick='<%#string.Format("return popUpManageReq(\"{0}\");",DataBinder.Eval(Container.DataItem, "LeaveID")) %>'>แก้ไข </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="ยกเลิก">
                <ItemTemplate>
                    <asp:LinkButton ID="Link2" runat="server" CommandName="Delete" Text="Delete" 
                        OnClientClick='<%#string.Format("return deleteReq(\"{0}\");",DataBinder.Eval(Container.DataItem, "LeaveID")) %>'>ยกเลิก</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateColumn>
        </Columns>
    </asp:DataGrid>
</asp:Content>
