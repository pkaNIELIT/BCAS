<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="BCAS_Signed_TaxInvoice_Download_Payee.aspx.cs" Inherits="Forms_DetailsForms_BCAS_Signed_TaxInvoice_Download_Payee" Culture="en-GB" Title="BCAS_TaxInvoice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" align="center">
        <tr>
            <td colspan="2" align="center">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Label ID="lblHeading" runat="server" Font-Bold="True" ForeColor="#990000"
                    Text="BCAS SIGNED TAX INVOICE DOWNLOAD PAYEE"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="30%"></td>
            <td width="70%"></td>
        </tr>
        <tr align="center">
            <td width="30%" colspan="2" style="width: 100%">
                <table width="65%" align="center" border="2">
                    <tr align="left">
                        <td width="30%">
                            <asp:Label ID="lbl_Payee_Org" runat="server" Text="Payee Organisation"></asp:Label>
                        </td>
                        <td width="70%">
                            <asp:DropDownList ID="ddlBillerOrganisation" runat="server" Width="535px" DataTextField="Payee_Organisation_Name"
                                DataValueField="Payee_Organisation_Code"
                                AutoPostBack="True"
                                AppendDataBoundItems="True" Enabled="false">
                                <asp:ListItem Value="0">Select Biller Organisation</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                   <%-- <tr align="left">
                        <td width="30%">
                            <asp:Label ID="lblInvoice" runat="server" Text="Invoice No"></asp:Label>
                        </td>
                        <td width="70%">
                            <asp:TextBox ID="txt_Invoice_No" runat="server" TabIndex="13" Enabled="true"></asp:TextBox>
                        </td>
                    </tr>--%>

                    <tr align="left">
                        <td width="30%">
                            <asp:Label ID="lblCourseSchedule" runat="server" Text="Course Schedule"></asp:Label>
                        </td>
                        <td width="70%">
                            <asp:DropDownList ID="ddlSchedule" runat="server" DataTextField="Course_Schedule"
                                DataValueField="Schedule_ID" Width="535px"
                                AppendDataBoundItems="True">
                                <asp:ListItem Value="0">-Select Course Schedule-</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td width="30%"></td>
                        <td width="70%">
                            <asp:Button ID="btn_BCAS_SignedTaxInvoiceDownload" runat="server" Text="Download Signed Tax Invoice" OnClick="btn_BCAS_SignedTaxInvoiceDownload_Click"/>

                            <asp:Button ID="btn_BCAS_Signed_E_TaxInvoiceDownload" runat="server" Text="Download Signed E-Tax Invoice" OnClick="btn_BCAS_Signed_E_TaxInvoiceDownload_Click" />
                        </td>
                    </tr>
                       <asp:Label ID="lbl_Message" runat="server" Text="label" Visible="false"></asp:Label>
                </table>

            </td>
        </tr>
        <tr>
            <td width="30%" colspan="2" style="width: 100%" align="center">&nbsp;</td>
        </tr>
        <tr>
            <td width="30%" colspan="2" style="width: 100%" align="center">&nbsp;</td>
        </tr>

      

          

        <tr>
    <td width="20%" colspan="2" style="width: 95%" align="center">


       <%-- <asp:SqlDataSource ID="SqlDataSource" runat="server"
            ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" 
            SelectCommand="spGetTaxInvoiceUploadedData" SelectCommandType="StoredProcedure">
        </asp:SqlDataSource>--%>
    </td>

</tr>
    </table>
</asp:Content>

