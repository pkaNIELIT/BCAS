<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CustomErrorPage.aspx.cs" Inherits="Forms_DetailForms_CustomErrorPage"
    Title="CustomErrorPage" ValidateRequest="false" EnableSessionState="True" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            height: 23px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="90%" align="center">
        <tr>
            <td>
                <table align="center" border="10" bordercolor="#CEEDEE" cellspacing="0" height="300"
                    width="60%" style="border-collapse: collapse" cellpadding="0">
                    <tr>
                        <td align="center" valign="top">
                            <table align="center" border="0" style="width: 81%">
                                <tr>
                                    <td align="center" class="style1">
                                        <asp:Label ID="lblTitle" runat="server" Font-Bold="False" Font-Names="Monotype Corsiva"
                                            Font-Size="X-Large" ForeColor="#990033" meta:resourcekey="lblTitle">Page Not Found !!!!!</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Image ID="Image4" runat="server" ImageAlign="Middle" ImageUrl="~/Images/PageNotFound.jpg" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="text-align: left">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
