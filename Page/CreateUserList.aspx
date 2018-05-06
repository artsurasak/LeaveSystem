<%@ Page Title="" Language="C#" MasterPageFile="~/MainFrame.Master" AutoEventWireup="true" CodeBehind="CreateUserList.aspx.cs" Inherits="IS.Page.CreateUserList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Create User
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentDetail" runat="server">
    <%--<form id="form1" runat="server">--%>
    <div>
        <asp:Label runat="server" ID="HeaderText" class="font-weight-bold">สรัางผู้ใช้งาน</asp:Label>
        <asp:Table runat="server" CellPadding="5" CellSpacing="5">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Right">
                    <asp:Label runat="server" CssClass="font-weight-normal">Username</asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txtUserName"></asp:TextBox>
                    <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtUserName"
                        ErrorMessage="User Name is a required field."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="trPassword" runat="server">
                <asp:TableCell HorizontalAlign="Right">
                    <asp:Label runat="server" CssClass="font-weight-normal">Password</asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" TextMode="Password" ID="txtPassword"></asp:TextBox>
                    <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtPassword"
                        ErrorMessage="Password is a required field."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Right">
                    <asp:Label runat="server" CssClass="font-weight-normal">Role</asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="ddlRole" runat="server">
                      <%--  <asp:ListItem Value="1">ผู้บริหาร</asp:ListItem>
                        <asp:ListItem Value="2">ผู้จัดการ</asp:ListItem>
                        <asp:ListItem Value="3">พนักงาน</asp:ListItem>--%>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator id="RequiredFieldValidator8" runat="server" 
                        ControlToValidate="ddlRole"
                        InitialValue = "*"
                        ErrorMessage="Please select Role."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Right">
                    <asp:Label runat="server">ชื่อกลุ่ม</asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="ddlGroup" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator id="RequiredFieldValidator10" runat="server" 
                        ControlToValidate="ddlGroup"
                        InitialValue = "*"
                        ErrorMessage="Please select Role."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Right">
                    <asp:Label runat="server">Line ID</asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txtLine"></asp:TextBox>
                    <asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server" 
                        ControlToValidate="txtLine"
                        ErrorMessage="Line ID is a required field."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Right">
                    <asp:Label runat="server">ชื่อจริง</asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txtFName"></asp:TextBox>
                    <asp:RequiredFieldValidator id="RequiredFieldValidator4" runat="server" 
                        ControlToValidate="txtFName"
                        ErrorMessage="ชื่อจริง is a required field."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Right">
                    <asp:Label runat="server">นามสกุล</asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txtLName"></asp:TextBox>
                    <asp:RequiredFieldValidator id="RequiredFieldValidator5" runat="server" 
                        ControlToValidate="txtLName"
                        ErrorMessage="นามสกุล is a required field."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Right">
                    <asp:Label runat="server">รหัสพนักงาน</asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txtEmpID"></asp:TextBox>
                    <asp:RequiredFieldValidator id="RequiredFieldValidator6" runat="server" 
                        ControlToValidate="txtEmpID"
                        ErrorMessage="รหัสพนักงาน is a required field."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Right">
                    <asp:Label runat="server">แผนก</asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="ddlDep" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator id="RequiredFieldValidator9" runat="server" 
                        ControlToValidate="ddlDep"
                        InitialValue = "*"
                        ErrorMessage="Please select Role."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Right">
                    <asp:Label runat="server">Email</asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator id="RequiredFieldValidator7" runat="server" 
                        ControlToValidate="txtEmail"
                        ErrorMessage="Email is a required field."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                    <asp:Button ID="btnSave" class="btn btn-secondary" runat="server" Text="บันทึก" OnClick="btnSave_Click" />&nbsp;
                    <asp:Button ID="btnDelete" class="btn btn-secondary" runat="server" Text="ลบ" Visible="false" OnClick="btnDelete_Click" />&nbsp;
                    <asp:Button ID="btnCancel" class="btn btn-secondary" runat="server" Text="ยกเลิก" OnClick="btnCancel_Click" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
    <%--</form>--%>
</asp:Content>
