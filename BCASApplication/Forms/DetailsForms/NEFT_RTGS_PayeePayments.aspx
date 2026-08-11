<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="NEFT_RTGS_PayeePayments.aspx.cs" Inherits="Forms_DetailsForms_NEFT_RTGS_PayeePayments"
    Title="BCAS-->NEFT_RTGS_Pymt" Culture="en-GB" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            text-align: left;
            width: 65%;
            vertical-align: top;
        }
        .style2
        {
            width: 45%;
        }
        .style3
        {
            text-align: justify;
        }
        .style4
        {
            text-decoration: underline;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table border="0" align="center" width="90%">
        <tr align="center">
            <td align="center">
                <table border="0" cellpadding="0" cellspacing="7" width="90%" bordercolor="#ECC4B9"
                    bgcolor="#FBEBE6" align="center">
                    <tr>
                        <td align="center" height="50px">
                            <asp:Label ID="lblPayeeOrgName" runat="server" ForeColor="#000099"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <strong><font color="#990000">RTGS/NEFT Payments</font><hr color="#990000" size="1">
                            </strong>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="style3">
                            <i><span class="style4">For the RTGS/NEFT transactions the below given account details
                                may kindly be used:</span><br class="style4" />
                                <br />
                            </i><b>A/C No. :</b> 604810210000010<br />
                            <b>IFSC Code:</b> BKID0006048
                            <br />
                            <b>Bank Name:</b> Bank of India
                            <br />
                            <b>Branch:</b> CGO Complex
                            <br />
                            <b>MICR No:</b> 110013052
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <hr color="#990000" size="1">
                        </td>
                    </tr>
                    <tr align="center">
                        <td align="center">
                            <table style="border: thin groove #993366" width="100%" style="text-align: right"
                                cellpadding="5" cellspacing="0">
                                <tr>
                                    <td style="text-align: right; vertical-align: top" width="40%">
                                    </td>
                                    <td width="60%" align="left" >
                                        <strong><font color="#990000">Verification of RTGS/NEFT Payments</font></strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; vertical-align: top" width="40%">
                                        <asp:Label ID="lbl_CourseName" runat="server" meta:resourcekey="lbl_Category">Payment on behalf of Sponsoring Organisation</asp:Label>
                                    </td>
                                    <td width="60%" align="left">
                                        <asp:DropDownList ID="ddlSponsoredOrg" runat="server" DataSourceID="SqlDataPayeeSponsoredOrg"
                                            DataTextField="Organisation_Name" DataValueField="Organisation_Code" Width="550px"
                                            AppendDataBoundItems="True">
                                            <asp:ListItem Value="0">-Select Course Schedule-</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                            ControlToValidate="ddlSponsoredOrg" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; vertical-align: top" width="40%">
                                        <asp:Label ID="Label3" runat="server" Text="UTR Number"></asp:Label>
                                    </td>
                                    <td width="60%" align="left">
                                        <asp:TextBox ID="txtUTR_No" runat="server" TabIndex="1" MaxLength="30" Width="200px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUTR_No"
                                            ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; vertical-align: top" width="40%">
                                        <asp:Label ID="Label4" runat="server" Text="Transaction Date"></asp:Label>
                                    </td>
                                    <td width="60%" align="left">
                                        <asp:TextBox ID="txtTXN_Date" runat="server" TabIndex="2" Width="200px"></asp:TextBox>
                                        <cc1:MaskedEditExtender ID="txtTXN_Date_MaskedEditExtender" runat="server" CultureName="en-GB"
                                            Mask="99/99/9999" MaskType="Date" TargetControlID="txtTXN_Date" UserDateFormat="DayMonthYear">
                                        </cc1:MaskedEditExtender>
                                        <cc1:MaskedEditValidator ID="mev_txtTXN_Date" runat="server" ControlExtender="txtTXN_Date_MaskedEditExtender"
                                            ControlToValidate="txtTXN_Date" Display="None" EmptyValueMessage="Date Must Be Entered"
                                            IsValidEmpty="false">
                                        </cc1:MaskedEditValidator>
                                        <cc1:ValidatorCalloutExtender ID="mev_callout_extender" runat="server" TargetControlID="mev_txtTXN_Date">
                                        </cc1:ValidatorCalloutExtender>
                                        (dd/mm/yyyy)
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; vertical-align: top" width="40%">
                                        <asp:Label ID="lbl_Fee_Amount" runat="server" meta:resourcekey="lbl_Date_of_birth">RTGS/NEFT Amount (INR) Paid</asp:Label>
                                    </td>
                                    <td width="60%" align="left">
                                        <asp:TextBox ID="txtAmount" runat="server" MaxLength="8" TabIndex="3" Width="200px"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="fte_Amount" runat="server" Enabled="True" TargetControlID="txtAmount"
                                            ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ControlToValidate="txtAmount"
                                            ErrorMessage="*"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtAmount"
                                            ErrorMessage="Amount must be &gt; 0" Operator="GreaterThan" Type="Integer" ValueToCompare="0"></asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr bgcolor="#ffcccc">
                                    <td style="text-align: left" width="40%">
                                        <asp:SqlDataSource ID="SqlDataPayeeSponsoredOrg" runat="server" ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>"
                                            SelectCommand="SELECT Sponsoring_Organisation_Master.Organisation_Code ,Sponsoring_Organisation_Master.Organisation_Name 
FROM Sponsoring_Organisation_Master INNER JOIN Payee_Sponsored_Organisation on
Sponsoring_Organisation_Master.Organisation_Code=Payee_Sponsored_Organisation.Sponsoring_Organisation_Code 
WHERE Payee_Sponsored_Organisation.Payee_Organisation_Code =@PayeeOrgCode
ORDER BY Sponsoring_Organisation_Master.Organisation_Name">
                                            <SelectParameters>
                                                <asp:SessionParameter Name="PayeeOrgCode" SessionField="UserOrg" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </td>
                                    <td width="60%" align="left" valign="middle">                                        
                                        <asp:Button ID="btnPayNow" runat="server" ForeColor="#000099" Text="Submit for Verification"
                                            OnClick="btnPayNow_Click" TabIndex="4" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="left" style="text-align: center">
                                        <asp:Label ID="lbl_ErrorMsg" runat="server" ForeColor="#990000" Font-Size="Large"
                                            Style="text-align: center"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="msg" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
