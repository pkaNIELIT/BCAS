<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RepInvoiceDwnExcel.aspx.cs" Inherits="Reports_RepInvoiceDwnExcel" Culture="en-GB" Title="BCAS_InvoiceInExcel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">

        <%--function formatDate(dateString) {
            var parts = dateString.split('/');
            return parts[1] + '/' + parts[0] + '/' + parts[2];
        }

        debugger;
        function compareDates() {

            debugger;
            // Get the values from the textboxes
            var fromDate_Value = document.getElementById("<%=txt_from_date.ClientID %>").value;
        var toDate_Value = document.getElementById("<%=txt_to_date.ClientID %>").value;

            // Convert textbox values to Date objects
            var date1 = new Date(formatDate(fromDate_Value));
            var date2 = new Date(formatDate(toDate_Value));

            // Compare the dates
            if (date1 > date2) {
                alert("ToDate can't be before FromDate");
                return false;
            }
            if ((date1 > new Date()) || (date2 > new Date())) {
                alert("Date can't be After Today");
                return false;
            }
            var monthFromDate = date1.getMonth();
            var monthToDate = date2.getMonth();
            var dateFromDate = date1.getDate();
            var dateToDate = date2.getDate();
            var yearFromDate = date1.getFullYear();
            var yearToDate = date2.getFullYear();
            var monthDiff = (yearToDate - yearFromDate) * 12 + (monthToDate - monthFromDate);
            var dateDiff = (dateToDate - dateFromDate)

            if (monthDiff >= 1) {
                if (dateDiff >= 0) {
                    alert(" Date Range must be less than one month ");
                    return false;
                }
                else
                    return true;

            }
            else
                return true;
        }--%>
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" align="center">
        <tr>
            <td colspan="2" align="center">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Label ID="lblHeading" runat="server" Font-Bold="True" ForeColor="#990000"
                    Text="BCAS TAX INVOICE FOR FINANCE SECTION"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="30%"></td>
            <td width="70%"></td>
        </tr>
        <tr align="center">
            <td width="30%" colspan="2" style="width: 100%">
                <table width="65%" align="center" border="2">

                    <%--     <tr align="left">
                    <td width="30%">
            <asp:Label ID="lblSponsoringOrg" runat="server" Text="Sponsoring Organisation" Visible="False"></asp:Label>
                    </td>
                    <td width="70%">
            <asp:DropDownList ID="ddlOrganisation" runat="server" Width="535px" DataTextField="Organisation_Name" 
                DataValueField="Organisation_Code" 
                AutoPostBack="True" 
                onselectedindexchanged="ddlOrganisation_SelectedIndexChanged" 
                AppendDataBoundItems="True"  Enabled="false" Visible="False" >               
                <asp:ListItem Value="0">Select Sponsoring Organisation</asp:ListItem>
            </asp:DropDownList>
                    </td>
                </tr>--%>

                    <tr id="trSpons" runat="server" visible="true">
                        <td width="30%">
                            <asp:Label ID="lblSpons" runat="server"
                                Text="Payee Organisation"></asp:Label></td>
                        <td width="70%">
                            <asp:DropDownList ID="ddlPayeeOrganisation" runat="server" Width="300px" DataTextField="Payee_Organisation_Name"
                                DataValueField="Payee_Organisation_Code"
                                AppendDataBoundItems="True">
                                <asp:ListItem Value="0">--Select Paying Organisation--</asp:ListItem>
                                <asp:ListItem Value="999999">GET ALL ORGANISATIONS</asp:ListItem>
                            </asp:DropDownList>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblOrg" runat="server" Visible="false"
                                Text=""></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td align="left">
                            <asp:Label ID="lbl_date_range" runat="server">Select Date Range</asp:Label>
                            (dd/mm/yyyy) <font color="red">*</font> :
                        </td>
                        <td align="left">
                            <asp:Label ID="LabelFrom" runat="server">From</asp:Label>
                            <asp:TextBox ID="txt_from_date" runat="server" TabIndex="13" Enabled="true"></asp:TextBox>
                            <cc1:MaskedEditExtender ID="txt_from_date_MaskedEditExtender" runat="server" CultureName="en-GB"
                                Mask="99/99/9999" MaskType="Date"
                                TargetControlID="txt_from_date" UserDateFormat="DayMonthYear">
                            </cc1:MaskedEditExtender>
                            <cc1:MaskedEditValidator ID="mev_txt_from_date" runat="server"
                                ControlExtender="txt_from_date_MaskedEditExtender"
                                ControlToValidate="txt_from_date" Display="None"
                                EmptyValueMessage="From Date Must Be Entered" IsValidEmpty="false">
                            </cc1:MaskedEditValidator>
                            <%--OnTextChanged="txt_from_date_TextChanged"--%>
                            <cc1:ValidatorCalloutExtender ID="mev_callout_extender_From" runat="server"
                                TargetControlID="mev_txt_from_date">
                            </cc1:ValidatorCalloutExtender>
                            <asp:Label ID="LabelTo" runat="server">To</asp:Label>
                            <asp:TextBox ID="txt_to_date" runat="server" TabIndex="13" Enabled="true"></asp:TextBox>
                            <cc1:MaskedEditExtender ID="txt_to_date_MaskedEditExtender" runat="server" CultureName="en-GB"
                                Mask="99/99/9999" MaskType="Date"
                                TargetControlID="txt_to_date" UserDateFormat="DayMonthYear">
                            </cc1:MaskedEditExtender>
                            <cc1:MaskedEditValidator ID="mev_txt_to_date" runat="server"
                                ControlExtender="txt_to_date_MaskedEditExtender"
                                ControlToValidate="txt_to_date" Display="None"
                                EmptyValueMessage="To Date Must Be Entered" IsValidEmpty="false">
                            </cc1:MaskedEditValidator>
                            <%--OnTextChanged="txt_to_date_TextChanged"--%>
                            <cc1:ValidatorCalloutExtender ID="mev_callout_extender_To" runat="server"
                                TargetControlID="mev_txt_to_date">
                            </cc1:ValidatorCalloutExtender>

                        </td>


                      
                    </tr>

                </table>

            </td>
        </tr>
        <tr>
            <td width="30%" colspan="2" style="width: 100%" align="center">&nbsp;</td>
        </tr>
        <tr>
            <td width="30%" colspan="2" style="width: 100%" align="center">&nbsp;</td>
        </tr>

        <tr align="left">
            <td width="30%">&nbsp;</td>
            <td width="70%">

                <asp:Button ID="btnDisplay" runat="server" Text="Get Data" Width="250px"
                    OnClick="btnDisplay_Click" TabIndex="4"/>

            </td>
        </tr>


        <tr>
            <td align="center" height="50px" valign="top" colspan="2">
                <%--<asp:ImageButton ID="imgpdf" runat="server" ImageUrl="~/Images/pdf.jpg" Width="1%" Height="50%" OnClick="imgPDF_Click" Visible="false" />
        &nbsp;&nbsp;--%>
                <asp:ImageButton ID="imgExcel" runat="server" ImageUrl="~/Images/Export_Exl.jpg" OnClick="imgExcel_Click" Visible="false" />

                &nbsp;</td>
        </tr>
        <tr>
            <td align="left" colspan="2">
                <div id="divReportData" runat="server" style="width: 100%;"></div>
            </td>
        </tr>
        <tr>
            <td width="30%" colspan="2" style="width: 100%"></td>
        </tr>
    </table>
</asp:Content>

