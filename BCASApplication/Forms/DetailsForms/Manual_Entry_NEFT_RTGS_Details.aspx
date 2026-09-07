<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Manual_Entry_NEFT_RTGS_Details.aspx.cs" Inherits="Forms_DetailsForms_Manual_Entry_NEFT_RTGS_Details" Title="BCAS-->Manual_NEFT-RTGS_Upload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            text-align: left;
            width: 65%;
            vertical-align:top;
        }
        .style2
        {
            width: 45%;
        }
        .style3
        {
            text-align: center;
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
                                    <td align="center" height="40px">
        
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <strong><hr color="#990000" size="1">
                                        </strong></td>
                                </tr>
                                <tr>
                                    <td align="center" class="style3">
        
                                        <strong><font color="#990000" style="text-align: center">RTGS/NEFT Details Entry </font>
                                        </strong>
        
                                                    </td>
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
                                                        <asp:Label ID="Label5" runat="server" Text="Serial Number"></asp:Label>
                                                    </td>
                                                    <td width="55%" class="style1">
        
                                                        <asp:TextBox ID="txtSerial_No" runat="server" MaxLength="8" TabIndex="1"></asp:TextBox>                                                        
                                                        <cc1:FilteredTextBoxExtender ID="fte_mobile_no" runat="server" Enabled="True" TargetControlID="txtSerial_No"
                                                            ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                        
                                                        <asp:RequiredFieldValidator ID="rfvSerial_No" runat="server" 
                                                            ControlToValidate="txtSerial_No" ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    </td>
                                            </tr> 
                                            <tr>
                                                    <td style="text-align: right; vertical-align:top" class="style2">
                                                        <asp:Label ID="Label3" runat="server" Text="UTR Number"></asp:Label>
                                                    </td>
                                                    <td width="55%" class="style1">
        
                                                        <asp:TextBox ID="txtUTR_No" runat="server" TabIndex="2" MaxLength="30" 
                                                            Width="200px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                            ControlToValidate="txtUTR_No" ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    </td>
                                            </tr> 
                                            <tr>
                                                    <td style="text-align: right; vertical-align:top" class="style2">
                                                        <asp:Label ID="Label6" runat="server" Text="Sender IFSC Code"></asp:Label>
                                                    </td>
                                                    <td width="55%" class="style1">
        
                                                        <asp:TextBox ID="txtSenderIFSC" runat="server" TabIndex="3" Width="200px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                            ControlToValidate="txtSenderIFSC" ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    </td>
                                            </tr> 
                                            <tr>
                                                    <td style="text-align: right; vertical-align:top" class="style2">
                                                        <asp:Label ID="Label7" runat="server" Text="Sender Name"></asp:Label>
                                                    </td>
                                                    <td width="55%" class="style1">
        
                                                        <asp:TextBox ID="txtSenderName" runat="server" TabIndex="4" Width="200px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                                            ControlToValidate="txtSenderName" ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    </td>
                                            </tr> 
                                            <tr>
                                                    <td style="text-align: right; vertical-align:top" class="style2">
                                                        <asp:Label ID="Label4" runat="server" Text="Transaction Date"></asp:Label>
                                                    </td>
                                                    <td width="55%" class="style1">        
                                                        <asp:TextBox ID="txtTXN_Date" runat="server" TabIndex="5" Width="200px"></asp:TextBox>
                                                        <cc1:MaskedEditExtender ID="txtTXN_Date_MaskedEditExtender" runat="server"
                                                            CultureName="en-GB" Mask="99/99/9999" MaskType="Date" TargetControlID="txtTXN_Date"
                                                            UserDateFormat="DayMonthYear">
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
                                                    <td style="text-align: right; vertical-align:top" class="style2">
                                        <asp:Label ID="lbl_Fee_Amount" runat="server" 
                                            meta:resourcekey="lbl_Date_of_birth">RTGS/NEFT Amount (INR) Paid</asp:Label>
                                                    </td>
                                                    <td class="style1">
                                                        <asp:TextBox ID="txtAmount" runat="server" MaxLength="8" TabIndex="6" 
                                                            Width="200px"></asp:TextBox>
                                                        
                                                        <cc1:FilteredTextBoxExtender ID="fte_Amount" runat="server" Enabled="True" TargetControlID="txtAmount"
                                                            ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                        
            <asp:RequiredFieldValidator ID="rfvAmount" runat="server" 
                ControlToValidate="txtAmount" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                        <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                                            ControlToValidate="txtAmount" ErrorMessage="Amount must be &gt; 0" 
                                                            Operator="GreaterThan" Type="Integer" ValueToCompare="0"></asp:CompareValidator>
                                                    </td>
                                            </tr>
                                            <tr bgcolor="#ffcccc">
                                                    <td style="text-align: left" class="style2">
         
            
                                                        &nbsp;</td>
                                                    <td width="55%" style="text-align: left; width: 75%;" valign="middle">
                                                        <%--<asp:LoginView ID="LoginView1" runat="server">
                            </asp:LoginView> --%>
                                                        <asp:Button ID="btnSubmit" runat="server" ForeColor="#000099" 
                                                            Text="Submit" TabIndex="7" onclick="btnSubmit_Click" />
                                         <asp:Label ID="lbl_ErrorMsg" runat="server" ForeColor="#990000" 
                                             Font-Size="Large" style="text-align: center"></asp:Label>
                                         <asp:Label ID="lbl_Success" runat="server" Visible="False" Font-Size="Large" 
                                             ForeColor="#000099"></asp:Label>
                                                    </td>
                                            </tr>                                           
                                     <tr><td colspan="2" align="left" style="text-align: center">
                                         &nbsp;</td></tr>
                                               
                                     <tr><td colspan="2" align="left" style="text-align: center">
                   <%-- <asp:GridView ID="grdDisplay" runat="server" AutoGenerateColumns="False" Width="100%" 
                        Caption="Records Uploaded Successfully">
                        <Columns>
                            <asp:BoundField DataField="Serial_No" HeaderText="Serial_No" 
                                SortExpression="Serial_No" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UTR_Number" HeaderText="UTR_Number" 
                                SortExpression="UTR_Number" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Sender_IFSC" HeaderText="Sender_IFSC" 
                                SortExpression="Sender_IFSC" >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Sender_Name" HeaderText="Sender_Name" 
                                SortExpression="Sender_Name" >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Transaction_date" HeaderText="Transaction_date" 
                                SortExpression="Transaction_date" >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Amount" HeaderText="Amount" 
                                SortExpression="Amount" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle BackColor="Silver" BorderColor="#990033" BorderStyle="Solid" 
                            BorderWidth="2px" />
                    </asp:GridView>--%>
                                         </td></tr>
                                               
                                     </table>
                                       </td>
                                </tr>
                                 
                                </table>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" 
                                SelectCommand="SELECT * FROM [Rtgs_Neft_Details]"></asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
</asp:Content>

