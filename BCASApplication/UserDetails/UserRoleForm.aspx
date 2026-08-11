<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="UserRoleForm.aspx.cs" Inherits="UserRoleForm" Title="Forms In Roles"
    culture="en-GB" UICulture="auto"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" align="center">
        <tr align="center" >
            <td align="center" >
            </td>
        </tr>
        <tr align="center">
            <td align="center">
                <table align="center" width="90%" >
        <tr>
            <td colspan="4" style="height: 21px" align="center">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="4" style="height: 21px" align="center" rowspan="1">
                <asp:Label ID="lbl_Heading" runat="server" meta:resourcekey="lbl_Heading" 
                    EnableTheming="True" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_Roles" runat="server" meta:resourcekey="lbl_Roles" 
                    EnableTheming="True" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbl_FormRoles" runat="server" meta:resourcekey="lbl_FormRoles" 
                    EnableTheming="True" Font-Bold="True"></asp:Label>
            </td>
            <td>
            </td>
            <td>
                <asp:Label ID="lbl_Forms" runat="server" meta:resourcekey="lbl_Forms" 
                    EnableTheming="True" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:ListBox ID="lb_AllRoles" runat="server" OnSelectedIndexChanged="lb_AllRoles_SelectedIndexChanged"
                    AutoPostBack="True" Rows="20" Width="200px"></asp:ListBox>
            </td>
            <td>
                <asp:ListBox ID="lb_FormRoles" runat="server" SelectionMode="Multiple" 
                    Rows="20" Width="250px"></asp:ListBox>
            </td>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btn_RemoveRole" runat="server" Text=">" OnClick="btn_RemoveRole_Click" Font-Bold="True"
                                Font-Size="XX-Large" Height="50px" Width="50px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btn_RemoveAll" runat="server" Text=">>" OnClick="btn_RemoveAll_Click" Font-Bold="True"
                                Font-Size="XX-Large" Height="50px" Width="50px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btn_AddRole" runat="server" Text="<" OnClick="btn_AddRole_Click"  Font-Bold="True"
                                Font-Size="XX-Large" Height="50px" Width="50px"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btn_AddAll" runat="server" Text="<<" OnClick="btn_AddAll_Click" Font-Bold="True"
                                Font-Size="XX-Large" Height="50px" Width="50px" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <asp:ListBox ID="lb_Forms" runat="server" SelectionMode="Multiple" Rows="20" 
                    Width="250px"></asp:ListBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_error" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
            
            </td>
        </tr>
    </table>
    
</asp:Content>
