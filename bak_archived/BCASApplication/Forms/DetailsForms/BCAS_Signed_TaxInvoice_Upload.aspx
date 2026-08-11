<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="BCAS_Signed_TaxInvoice_Upload.aspx.cs" Inherits="Forms_DetailsForms_BCAS_Signed_TaxInvoice_Upload" Culture="en-GB" Title="BCAS_TaxInvoice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--  <script type = "text/javascript">
     function checkDates() {
         // Get the values from the textboxes
         var txtDateOfSigning = document.getElementById("<%=txt_date_of_Signing.ClientID %>").value;
       

         // Convert textbox values to Date objects
         var date1 = new Date(formatDate(txtDateOfSigning));
        
        
         if (date1 >= new Date()) {
             alert("Date can't be After Today");
             return false;
         }
         else
             return true;
     }
     </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" align="center">
        <tr>
            <td colspan="2" align="center">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Label ID="lblHeading" runat="server" Font-Bold="True" ForeColor="#990000"
                    Text="BCAS SIGNED TAX INVOICE UPLOADS "></asp:Label>
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
                    <tr align="left">
                        <td width="30%">
                            <asp:Label ID="lblInvoice" runat="server" Text="Invoice No"></asp:Label>
                        </td>
                        <td width="70%">
                            <asp:TextBox ID="txt_Invoice_No" runat="server" TabIndex="13" Enabled="true"></asp:TextBox>
                        </td>
                    </tr>


                    <tr>
                        <td align="left">
                            <asp:Label ID="lbl_date_of_Signing" runat="server">Date of Signing</asp:Label>
                            (dd/mm/yyyy) <font color="red">*</font> :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txt_date_of_Signing" runat="server" TabIndex="13" Enabled="true"></asp:TextBox>
                            <cc1:MaskedEditExtender ID="txt_date_of_Signing_MaskedEditExtender" runat="server" CultureName="en-GB"
                                Mask="99/99/9999" MaskType="Date"
                                TargetControlID="txt_date_of_Signing" UserDateFormat="DayMonthYear">
                            </cc1:MaskedEditExtender>
                            <cc1:MaskedEditValidator ID="mev_txt_date_of_Signing" runat="server"
                                ControlExtender="txt_date_of_Signing_MaskedEditExtender"
                                ControlToValidate="txt_date_of_Signing" Display="None"
                                EmptyValueMessage="Signing Date Must Be Entered" IsValidEmpty="false">
                            </cc1:MaskedEditValidator>
                            <cc1:ValidatorCalloutExtender ID="mev_callout_extender_DateOfSigning" runat="server"
                                TargetControlID="mev_txt_date_of_Signing">
                            </cc1:ValidatorCalloutExtender>
                        </td>


                    </tr>
                    <tr>
                        <td align="left">Select File </td>
                        <td align="left">
                            <asp:FileUpload ID="FileUpload1" runat="server" ToolTip="Select Only Pdf File" />
                        </td>

                    </tr>
                    <tr>
                        <td width="30%"></td>
                        <td>
                            <asp:Button ID="btn_BCAS_SignedTaxInvoice" runat="server" Text="Upload" OnClick="btn_BCAS_SignedTaxInvoice_Click"/>

                            <%--OnClientClick = "return checkDates()"--%>

                           <%--  <asp:Button ID="btn_View_BCAS_SignedTaxInvoice" runat="server" Text="View" OnClick="btn_View_BCAS_SignedTaxInvoice_Click" />--%>
                            <asp:Label ID="lbl_Message" runat="server" Text="label" Visible="false"></asp:Label>
                        </td>
                    </tr>


                    <%--   $$$$$$$$$$$$$$$$$     Commented Demand Note For Date Range     $$$$$$$$$$$$$$$$$$$$$$$$$$$--%>

                    <%--  <tr align="left">
                    <td width="30%">
            <asp:Label ID="Label2" runat="server" Text="Demand Note"></asp:Label>
                    </td>
                    <td width="70%">
            <asp:DropDownList ID="ddlDemandNote" runat="server" Width="535px"  
                
                AppendDataBoundItems="True"  >               
                <asp:ListItem Value="0">--Select-- </asp:ListItem>
            </asp:DropDownList>
                    </td>
                </tr>--%>


                    <%--  $$$$$$$$$$$$$$$$$      Commented Demand Note For Date Range      $$$$$$$$$$$$$$$$$$$$$$$$$$$--%>
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
            <td width="30%" colspan="2" style="width: 100%"></td>
        </tr>
        <tr>
            <td width="30%" colspan="2" style="width: 100%" align="center">

                <%--   <asp:Button ID="btnUpdateStatus" runat="server" OnClientClick="return ConfirmDelete();" OnClick="btnUpdateStatus_Click"
                    Text="Download Tax Invoice" />--%>

            </td>
        </tr>

                <tr>
    <td width="20%" colspan="2" style="width: 95%" align="center">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                    DataKeyNames="ID" DataSourceID="SqlDataSource"
                    EmptyDataText="No Record Found" BorderColor="#000066" BorderStyle="Outset"
                    BorderWidth="2px" Caption="Uploaded Files Details"
                    CaptionAlign="Top" ForeColor="#333333">
                    <Columns>
                        <asp:BoundField DataField="ProformaInvoiceNo"
                            HeaderText="Proforma Invoice No" InsertVisible="False" ReadOnly="True"
                            SortExpression="ProformaInvoiceNo" />
                        <asp:BoundField DataField="TaxInvoiceSigningDate" HeaderText="Date of Sign"
                            SortExpression="TaxInvoiceSigningDate" />
                        <asp:BoundField DataField="TaxInvoiceUploadDate" HeaderText="Date of Upload"
                            SortExpression="TaxInvoiceUploadDate" />
                        <%-- <asp:BoundField DataField="TaxInvoiceUploadFile" HeaderText="URL Link"
                    SortExpression="TaxInvoiceUploadFile" />--%>
                    </Columns>
                    <FooterStyle BackColor="#990000" />
                    <HeaderStyle BackColor="#990000" ForeColor="White" />
                    <AlternatingRowStyle BackColor="#FFFFCC" ForeColor="#333333" />
                </asp:GridView>
                <br />
            </ContentTemplate>
        </asp:UpdatePanel>
    </td>

</tr>

        <tr>
    <td width="20%" colspan="2" style="width: 95%" align="center">


        <asp:SqlDataSource ID="SqlDataSource" runat="server"
            ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" 
            SelectCommand="spGetTaxInvoiceUploadedData" SelectCommandType="StoredProcedure" OnSelecting="SqlDataSource_Selecting">
         <%--   <SelectParameters>
                <asp:ControlParameter ControlID="txtRegNo" Name="Registration_Number"
                    PropertyName="Text" />
                 <asp:ControlParameter ControlID="ddlSchedule" Name="ScheduleID" 
                    PropertyName="SelectedValue" />
            </SelectParameters>--%>
        </asp:SqlDataSource>
    </td>

</tr>
    </table>
</asp:Content>

