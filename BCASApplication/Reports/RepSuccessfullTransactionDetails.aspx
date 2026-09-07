<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RepSuccessfullTransactionDetails.aspx.cs" Inherits="Reports_RepSuccessfullTransactionDetails" Title="BCAS--> RepSuccessfullTransactionDetails" Culture="en-GB" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
       <%-- document.addEventListener('DOMContentLoaded', function () {

            var today = new Date();
            var month = ('0' + (today.getMonth() + 1)).slice(-2);
            var day = ('0' + today.getDate()).slice(-2);
            var year = today.getFullYear();
           /* var date = year + '-' + month + '-' + day;*/
        /*    var date = day + '/' + month + '/' + year;*/
            var date = day + '-' + month + '-' + year;
            var inputs = document.querySelectorAll('[id*=txtfrmDate]');
            inputs.forEach(function (input) {
                input.setAttribute('max', date);
            });
            var inputs1 = document.querySelectorAll('[id*=txttoDate]');
            inputs1.forEach(function (input) {
                input.setAttribute('max', date);
            });


            var date = new Date(); // or whatever your max date is
            var meetxtfrmDate = document.getElementById('<%= txtfrmDate_MaskedEditExtender.ClientID %>');

        
            meetxtfrmDate._maximumValue = date.toLocaleDateString();
            var meetxttoDate = document.getElementById('<%= txttoDate_MaskedEditExtender.ClientID %>');
            meetxttoDate._maximumValue = date.toLocaleDateString();

        });--%>

       <%-- function compareDates() {
            
            var textbox1Value = document.getElementById('<%= txtfrmDate.ClientID %>').value.trim();
            var textbox2Value = document.getElementById('<%= txttoDate.ClientID %>').value.trim();

            // Convert textbox values to Date objects
            var date1 = new Date(textbox1Value);
            var date2 = new Date(textbox2Value);

            // Compare the dates
            if (date1 > date2) {
                alert("ToDate can't be before FromDate");
                return 0;

                var button= document.getElementById('<%= imgpdf.ClientID %>')
                var button1= document.getElementById('<%= imgExcel.ClientID %>')
                var div=  document.getElementById('<%= divReportData.ClientID %>')
                button.style.visibility = "hidden";
                button1.style.visibility = "hidden";
                div.style.visibility = "hidden";

            }

        }--%>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="95%" align="center">
        <tr>
            <td align="center"></td>
        </tr>
        <tr align="center">
            <td></td>
        </tr>
        <tr align="center">
            <td>

                <asp:Label ID="lblHeading" runat="server" Font-Bold="True" Font-Size="Large"
                    ForeColor="#000099"></asp:Label>
                <asp:Label ID="lblOrg" runat="server" Font-Bold="True" Font-Size="Large" Visible="false"
                    ForeColor="#000099"></asp:Label>

            </td>
        </tr>
        <tr>
            <td></td>
        </tr>
        <tr align="center">
            <td align="center">

                <table width="70%" align="center" border="2">

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

                    <tr align="left">
                        <td width="30%">
                            <asp:Label ID="lblfrmDate" runat="server" Text="From Date"></asp:Label>
                        </td>
                        <td width="70%">

                            <%--  <asp:TextBox id="txtfrmDate" runat="server" TextMode="Date"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ErrorMessage="*" ControlToValidate ="txtfrmDate" SetFocusOnError="true">
                                </asp:RequiredFieldValidator>--%>

                            <asp:TextBox ID="txtfrmDate" runat="server" TabIndex="13" OnTextChanged="txtfrmDate_TextChanged"></asp:TextBox>

                      <%--         <asp:Image ID="Imbtn1" runat="server" ImageUrl="~/IMG/cal.png" Image-Align="Top"  
                        Height="24px" Width="23px" />  
                    <cc1:CalendarExtender ID="cal1" PopupButtonID="Imbtn1" runat="server" TargetControlID="txtfrmDate"  
                        Format="dd/MM/yyyy">  
                    </cc1:CalendarExtender>--%> 
                            <cc1:MaskedEditExtender ID="txtfrmDate_MaskedEditExtender" runat="server" CultureName="en-GB"
                                Mask="99/99/9999" MaskType="Date"
                                TargetControlID="txtfrmDate" UserDateFormat="DayMonthYear">
                            </cc1:MaskedEditExtender>
                            <cc1:MaskedEditValidator ID="mev_txtfrmDate" runat="server"
                                ControlExtender="txtfrmDate_MaskedEditExtender"
                                ControlToValidate="txtfrmDate" Display="None"
                                EmptyValueMessage="Date of Birth Must Be Entered" IsValidEmpty="false">
                            </cc1:MaskedEditValidator>
                            <cc1:ValidatorCalloutExtender ID="mev_callout_extender" runat="server"
                                TargetControlID="mev_txtfrmDate">
                            </cc1:ValidatorCalloutExtender>

                            <asp:CompareValidator ID="cv1" runat="server" Operator="LessThanEqual" ForeColor = "Red" ControlToValidate="txtfrmDate"
                                
        ErrorMessage="Maximum Date Must be Current Date" Type="Date" Display="Dynamic"></asp:CompareValidator>

                        </td>
                    </tr>
                    <tr align="left">
                        <td width="30%">
                            <asp:Label ID="lbltoDate" runat="server" Text="To Date"></asp:Label>
                        </td>
                        <td width="70%">

                            <%--  <asp:TextBox id="txttoDate" runat="server" TextMode="Date" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ErrorMessage="*" ControlToValidate ="txttoDate" SetFocusOnError="true">
                                </asp:RequiredFieldValidator>--%>


                  <asp:TextBox ID="txttoDate" runat="server" TabIndex="13" OnTextChanged="txttoDate_TextChanged"></asp:TextBox>
                       <%--  <asp:Image ID="Image1" runat="server" ImageUrl="~/IMG/cal.png" Image-Align="Top"  
                        Height="24px" Width="23px" />  
                        <cc1:CalendarExtender ID="cal2" PopupButtonID="Image1" runat="server" TargetControlID="txttoDate"  
                        Format="dd/MM/yyyy">  
                        </cc1:CalendarExtender> --%>
                            <cc1:MaskedEditExtender ID="txttoDate_MaskedEditExtender" runat="server" CultureName="en-GB"
                                Mask="99/99/9999" MaskType="Date"
                                TargetControlID="txttoDate" UserDateFormat="DayMonthYear">
                            </cc1:MaskedEditExtender>
                            <cc1:MaskedEditValidator ID="mev_txttoDate" runat="server"
                                ControlExtender="txttoDate_MaskedEditExtender"
                                ControlToValidate="txttoDate" Display="None"
                                EmptyValueMessage="Date of Birth Must Be Entered" IsValidEmpty="false">
                            </cc1:MaskedEditValidator>
                            <cc1:ValidatorCalloutExtender ID="mev_callout_extender1" runat="server"
                                TargetControlID="mev_txttoDate">
                            </cc1:ValidatorCalloutExtender>
                            
                            <asp:CompareValidator ID="cv2" runat="server" Operator="LessThanEqual" ForeColor = "Red" ControlToValidate="txttoDate"
                               
                            ErrorMessage="Maximum Date Must be Current Date" Type="Date" Display="Dynamic"></asp:CompareValidator>
                            
                            <asp:CompareValidator ID="cv3" ValidationGroup = "Date" ForeColor = "Red" runat="server" 
                                ControlToValidate = "txtfrmDate" ControlToCompare = "txttoDate" Operator = "LessThan" Type = "Date"
                                ErrorMessage="From date must be Before To date." Display="Dynamic"></asp:CompareValidator>


                        </td>
                    </tr>

                    <tr align="left">
                        <td width="30%">

                        &nbsp;</td>
                        <td width="70%">

                            <asp:Button ID="btnDisplay" runat="server" Text="Get Data" Width="250px"
                                OnClick="btnDisplay_Click" TabIndex="4" />

                        </td>
                    </tr>

                </table>
            </td>
        </tr>
        <tr>
            <td></td>
        </tr>

        <tr>
            <td align="center" height="50px" valign="top" colspan="2">
                <asp:ImageButton ID="imgpdf" runat="server" ImageUrl="~/Images/pdf.jpg" Width="1%" Height="50%" OnClick="imgPDF_Click" Visible="false" />
                &nbsp;&nbsp;
            <asp:ImageButton ID="imgExcel" runat="server" ImageUrl="~/Images/Export_Exl.jpg" OnClick="imgExcel_Click" Visible="false" />

                &nbsp;</td>
        </tr>
        <tr>
            <td align="left" colspan="2">
                <div id="divReportData" runat="server" style="width: 100%;"></div>
            </td>
        </tr>


    </table>

</asp:Content>

