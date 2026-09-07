<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="rptSuccessfulTransaction.aspx.cs" Inherits="Forms_MasterForms_rptSuccessfulTransaction" Title="Successful Transactions"  Culture="en-GB" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .right
        {
            text-align: right;
            width: 25%;
            color: #990000;
        }
        .left
        {
            text-align: left;
            width: 25%;
        }
        .style6
        {
            font-size: medium;
            text-align: center;
            font-family: Arial;
            color: Blue;
            font-weight: bold;
        }
        .style10
        {
            height: 32px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table width="100%" align="center" >
<tr align="center" > 
    <td>
<table width="90%" align="center" >
     <tr>
        <td height="50px">
        </td>
    </tr>
    <tr>
        <td>        
         <table width="90%" align="center" border="10" bordercolor="#CEEDEE" cellspacing="0" width="60%" style="border-collapse: collapse" cellpadding="0" height="250">
          <tr>
            <td>
                <table style="width: 75%;" border="1" align="center" bgcolor="#CEEDEE">
                    <tr>
                        <td colspan="2" class="style6" bgcolor="#ffcc00">
                            Report of Successful Transactions
                        </td>
                    </tr>
                    <tr>
                        <td class="right">
                            <asp:Label ID="lbl_from_date" runat="server" EnableTheming="True" meta:resourcekey="lbl_from_date"></asp:Label>
                        </td>
                        <td class="left">
                            <asp:TextBox ID="txtFomDate" runat="server"></asp:TextBox>
                            <cc1:maskededitextender ID="txtFromDate_MaskedEditExtender" runat="server" CultureName="en-GB"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtFomDate" 
                                UserDateFormat="DayMonthYear">
                            </cc1:maskededitextender>
                            <cc1:maskededitvalidator ID="mev_txtFomDate" runat="server" ControlExtender="txtFromDate_MaskedEditExtender"
                                ControlToValidate="txtFomDate" Display="None" EmptyValueMessage="From Date Must Be Entered"
                                IsValidEmpty="false" ValidationGroup="save"></cc1:maskededitvalidator>
                            <cc1:validatorcalloutextender ID="mev_callout_extender" runat="server" 
                                TargetControlID="mev_txtFomDate">
                            </cc1:validatorcalloutextender>
                            (dd/mm/yyyy)<asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                                runat="server" ControlToValidate="txtFomDate" ErrorMessage="*"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td class="right">
                            <asp:Label ID="lbl_to_date" runat="server" EnableTheming="True" meta:resourcekey="lbl_to_date"></asp:Label>
                        </td>
                        <td class="left">
                            <asp:TextBox ID="txtToDate" runat="server"></asp:TextBox>
                            <cc1:maskededitextender ID="txtToDate_MaskedEditExtender" runat="server" CultureName="en-GB"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtToDate" 
                                UserDateFormat="DayMonthYear">
                            </cc1:maskededitextender>
                            <cc1:maskededitvalidator ID="mev_txtToDate" runat="server" ControlExtender="txtToDate_MaskedEditExtender"
                                ControlToValidate="txtToDate" Display="None" EmptyValueMessage="To Date Must Be Entered"
                                IsValidEmpty="false" ValidationGroup="save"></cc1:maskededitvalidator>
                            <cc1:validatorcalloutextender ID="ValidatorCalloutExtender1" runat="server" 
                                TargetControlID="mev_txtToDate">
                            </cc1:validatorcalloutextender>
                            (dd/mm/yyyy)<asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
                                runat="server" ControlToValidate="txtToDate" ErrorMessage="*"></asp:RequiredFieldValidator><asp:CompareValidator 
                                ID="CompareValidator1" runat="server" ControlToCompare="txtFomDate"
                                ControlToValidate="txtToDate" ErrorMessage=" From Date &gt; To Date " Operator="GreaterThanEqual"
                                Type="Date"></asp:CompareValidator></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" class="style10">
                            <asp:Button ID="btn_generate" runat="server" AccessKey="G" CausesValidation="false"
                                EnableTheming="False" meta:resourcekey="btn_generate" OnClick="btn_generate_Click"
                                Style="height: 26px" />
                            <asp:Button ID="btn_cancel" runat="server" EnableTheming="False" meta:resourcekey="btn_cancel"
                                OnClick="btn_cancel_Click" />
                            <asp:Label ID="lbl_error" runat="server" Font-Bold="True" 
                                meta:resourcekey="lbl_errors"></asp:Label>
                        </td>
                    </tr>
                    <tr id="grdRow" runat="server" >
                        <td align="center" colspan="2">
                            <asp:Panel ID="Panel1" runat="server" Height="200px" ScrollBars="Vertical">
                                <asp:GridView ID="GridView1" runat="server" BorderColor="#990099" 
                                    BorderStyle="Solid" BorderWidth="2px" CaptionAlign="Top">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="2px" ForeColor="#CC0066" />
                                </asp:GridView>
                                <asp:Button ID="btnExport" runat="server" onclick="btnExport_Click" 
                                    Text="Export Data" />
                            </asp:Panel>
                        </td>
                    </tr>
                    </table>
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

