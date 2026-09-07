<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CompleteStep2.aspx.cs" Culture="en-GB" Inherits="Forms_DetailsForms_CompleteStep2"
    Title="BCAS-->CompleteStep2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table align="center" width="100%">
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center" height="200px">
                <table width="60%" border="5" bordercolor="#CEEDEE" align="center">
                    <tr valign="middle">
                        <td width="25%" align="right">
                            &nbsp;
                        </td>
                        <td align="left">
                            <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Times New Roman"
                                Font-Size="X-Large" ForeColor="#990033" Text="Enter Registration Number" 
                                EnableTheming="False"></asp:Label>
                        </td>
                    </tr>
                    <tr valign="middle">
                        <td width="25%" align="right">
                            <asp:Label ID="UserNameLabel" runat="server" >Registration No:</asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtRegNo" runat="server" Width="220px" MaxLength="8" 
                                AutoCompleteType="Disabled" ></asp:TextBox>
                            
                            <asp:RequiredFieldValidator ID="RegNoRequired" runat="server" ControlToValidate="txtRegNo"
                                ErrorMessage="*" ToolTip="Registration No is required." SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr valign="middle">
                        <td width="25%">
                            &nbsp;
                        </td>
                        <td align="left">
                            <asp:Button ID="LoginButton" runat="server" Font-Bold="False"
                                Style="text-align: left; font-weight: 700;" Text="View Details" 
                                OnClick="LoginButton_Click" />
                        </td>
                    </tr>
                    <tr valign="middle">
                        <td width="25%" colspan="2">
                            <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#CC0000"
                                Visible="False" EnableTheming="false"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
