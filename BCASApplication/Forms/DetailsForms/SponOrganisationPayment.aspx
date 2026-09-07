<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SponOrganisationPayment.aspx.cs" Culture="en-GB" Inherits="Forms_DetailsForms_SponOrganisationPayment" Title="BCAS-->SponOrganisationPayment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            text-align: left;
            width: 60%;
            vertical-align:top;
        }
        .style2
        {
            width: 40%;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" align="center" width="90%" >
                    <tr align="center" >
                        <td align="center" >
                            <table border="0" cellpadding="0" cellspacing="7" width="90%" bordercolor="#ECC4B9"
                                bgcolor="#FBEBE6" align="center" >
                                <tr>
                                    <td align="center" height="50px">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <strong><font color="#990000">Online Fee Payments</font></strong></td>
                                </tr>
                                <tr>
                                    <td>
                                        <hr color="#990000" size="1">
                                    </td>
                                </tr>
                                <tr align="center" >
                                    <td align="center">
                                    <table style="border: thin groove #993366" width="100%"  style="text-align: right" cellpadding ="5" cellspacing ="0">
                                            <tr>
                                                    <td style="text-align: right; vertical-align:top" class="style2">
                                                        &nbsp;</td>
                                                    <td width="55%" class="style1">
        
                                                        <asp:Label ID="lblPayeeOrgName" runat="server" ForeColor="#000099"></asp:Label>
        
                                                    </td>
                                            </tr> 
                                            <tr>
                                                    <td style="text-align: right; vertical-align:top" class="style2">
                                                        &nbsp;</td>
                                                    <td width="55%" class="style1">
        
                                                        &nbsp;</td>
                                            </tr> 
                                            <tr>
                                                    <td style="text-align: right; vertical-align:top" class="style2">
                                        <asp:Label ID="lbl_CourseName" runat="server" 
                                            meta:resourcekey="lbl_Category">Sponsoring Organisation</asp:Label>
                                                    </td>
                                                    <td width="55%" class="style1">
        
            <asp:DropDownList ID="ddlSponsoredOrg" runat="server" 
                DataSourceID="SqlDataPayeeSponsoredOrg" DataTextField="Organisation_Name" 
                DataValueField="Organisation_Code" Width="550px" AppendDataBoundItems="True"  Enabled="false"
                >
                <asp:ListItem Value="0">-Select Course Schedule-</asp:ListItem>
            </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="*" ControlToValidate ="ddlSponsoredOrg" SetFocusOnError="true" InitialValue="0" ></asp:RequiredFieldValidator>
                                                    </td>
                                            </tr> 

                                        <tr>
                                                    <td style="text-align: right; vertical-align:top" class="style2">
                                        <asp:Label ID="Label1" runat="server" 
                                            meta:resourcekey="lbl_Category">Payment on behalf of Proforma Invoice No.</asp:Label>
                                                    </td>
                                                    <td width="55%" class="style1">
        
            <asp:DropDownList ID="ddlProformaInvoice" runat="server" 
                DataSourceID="SqlDataProformaInvoice" DataTextField="ProformaInvoiceNo" 
                DataValueField="ID" Width="650px" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlProformaInvoice_SelectedIndexChanged" 
                >
                <asp:ListItem Value="0">--Select--</asp:ListItem>
            </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ErrorMessage="*" ControlToValidate ="ddlProformaInvoice" SetFocusOnError="true" InitialValue="0" ></asp:RequiredFieldValidator>
                                                    </td>

 <asp:SqlDataSource ID="SqlDataProformaInvoice" runat="server" 
                ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" 
                SelectCommand="SELECT  ID, ProformaInvoiceNo + '  [ ProFormaInvoiceDate - ' +  CONVERT(varchar,ProformaInvoice_Date,105) +'; #Candidates - '+ CAST(NoOfCandidates as varchar)+'; FeeAmount - '+ CAST(Total_Fee_With_Service_Tax as varchar) +' ]' as ProformaInvoiceNo  from Proforma_Invoice where Nominating_OrgCode=@PayeeOrgCode and PaymentStatus_ID=0
ORDER BY ID">
                <SelectParameters>
                    <asp:SessionParameter Name="PayeeOrgCode" SessionField="UserOrg" />
                </SelectParameters>
            </asp:SqlDataSource>

                                            </tr> 


                                            <tr>
                                                    <td style="text-align: right; vertical-align:top" class="style2">
                                        <asp:Label ID="lbl_Fee_Amount" runat="server" 
                                            meta:resourcekey="lbl_Date_of_birth">Amount To Pay (INR)</asp:Label>
                                                    </td>
                                                    <td class="style1">

                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server"> 
                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtAmount" runat="server" MaxLength="8" Enabled="false" TabIndex="1" Font-Bold="true" BackColor="#ffffcc"></asp:TextBox>
                                                        
                                                       <%-- <cc1:FilteredTextBoxExtender ID="fte_Amount" runat="server" Enabled="True" TargetControlID="txtAmount"
                                                            ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>--%>
                                                        
            <asp:RequiredFieldValidator ID="rfvAmount" runat="server" 
                ControlToValidate="txtAmount" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                        <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                                            ControlToValidate="txtAmount" ErrorMessage="Amount must be &gt; 0" 
                                                            Operator="GreaterThan" Type="double" ValueToCompare="0"></asp:CompareValidator>
                                        </ContentTemplate></asp:UpdatePanel>           
                                         </td>
                                            </tr>
                                            <tr bgcolor="#ffcccc">
                                                    <td style="text-align: left" class="style2">
         
            
            <asp:SqlDataSource ID="SqlDataPayeeSponsoredOrg" runat="server" 
                ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" 
                SelectCommand="SELECT Sponsoring_Organisation_Master.Organisation_Code ,Sponsoring_Organisation_Master.Organisation_Name 
FROM Sponsoring_Organisation_Master 
WHERE Organisation_Code =@PayeeOrgCode
ORDER BY Sponsoring_Organisation_Master.Organisation_Name">
                <SelectParameters>
                    <asp:SessionParameter Name="PayeeOrgCode" SessionField="UserOrg" />
                </SelectParameters>
            </asp:SqlDataSource>
                                                    </td>
                                                    <td width="55%" style="text-align: left; width: 75%;" valign="middle">
                                                        <%--<asp:LoginView ID="LoginView1" runat="server">
                            </asp:LoginView> --%>
                                                        <asp:Button ID="btnPayNow" runat="server" ForeColor="#000099" 
                                                            Text="Make Payment" onclick="btnPayNow_Click" TabIndex="2" />
                                                    </td>
                                            </tr>                                           
                                     <tr><td colspan="2" align="left">
                                         <asp:Label ID="lbl_ErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                                         </td></tr>
                                               
                                     </table>
                                       </td>
                                </tr>
                                 
                                </table>
                            <asp:HiddenField ID="msg" runat="server" />
                        </td>
                    </tr>
                </table>
</asp:Content>

