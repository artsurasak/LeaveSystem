<%@ Page Title="" Language="C#" MasterPageFile="~/MainFrame.Master" AutoEventWireup="true" CodeBehind="ListRequestAppr.aspx.cs" Inherits="IS.Page.ListRequestAppr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    รายการคำร้องขออนุมัติ
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentDetail" runat="server">
    <script type="text/javascript">
        function showValues(value) {
            if (value == 'I') {
                return false;
            } else {
                return true;
            }
        }
    </script>
    <asp:Label runat="server" class="font-weight-bold"><h4><span class="fa fa-check"></span>&nbsp;รายการคำร้องขออนุมัติ</h4></asp:Label>
    <asp:DataGrid ID="dtgList" CssClass="table table-hover" runat="server" AutoGenerateColumns="false" 
        PersistedSelection="true"   
        ondatabound="mygrid_DataBound" 
        OnRowDataBound="mygrid_RowDataBound" 
        HorizontalAlign="Center">
        <HeaderStyle BackColor="#ae56c4" ForeColor="White" Font-Size="Larger" HorizontalAlign="Center" />
        <Columns>
            <asp:TemplateColumn HeaderText="อนุญาต">
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:RadioButton ID="rd1" GroupName="rb"  runat="server"/>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="ไม่อนุญาต">
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:RadioButton ID="rd2" GroupName="rb" runat="server" />
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="LeaveID" Visible="false"></asp:BoundColumn>
            <asp:BoundColumn DataField="STATUS" Visible="false"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="รหัสพนักงาน" DataField="EmpCode"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="ชื่อพนักงาน" DataField="Name"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="วันที่สร้างแบบฟอร์มลา" DataField="CreateRequest"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="วันที่ขอลา" DataField="LeaveDate"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="จำนวนวันที่ลา" DataField="NoLeave"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="จำนวนวันลาคงเหลือ" DataField="RemainDate"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="ประเภทวันลา" DataField="Type"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="สาเหตุ" DataField="Note"></asp:BoundColumn>
            <asp:TemplateColumn HeaderText="ความคิดเห็น">
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:TextBox ID="txtComment" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateColumn>
        </Columns>
    </asp:DataGrid>
    <div style="display: flex; align-items: center; justify-content: center; margin-top:10px;">
        <asp:Button ID="btnSave" runat="server" CssClass="btn" BackColor="#ae56c4" ForeColor="White" Text="ตกลง" OnClick="btnSave_Click" />&nbsp;
        <asp:Button ID="btnCancel" runat="server" CssClass="btn" BackColor="#ae56c4" ForeColor="White" Text="ยกเลิก" />
    </div>

</asp:Content>
