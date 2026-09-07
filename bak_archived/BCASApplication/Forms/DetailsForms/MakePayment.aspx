<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MakePayment.aspx.cs" Inherits="Forms_DetailsForms_MakePayment" Culture="en-GB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>BCAS-->MakePayment</title>
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
        </style>
        
        <style>
        body
        {
            background-position: 50% 50%;
            margin: 0px;
            padding: 0px !important;
            height: 100%;
        }
    </style>

    <style>
        div
        {
            margin: 0;
        }
        table
        {
            margin: 0px;
            border-collapse: collapse;
            padding: 0px;
        }
    </style>
    
</head>
<body>
    <form id="form1" runat="server"> 
    <div>
     <table width="100%" align="center" cellspacing="0" border="0" >
            <tr>
                <td  width="5%" bgcolor="#09638e">
                </td>
                <td width="90%">                
                    <table width="100%">
                        <tr>
                            <td>                                                        
                            <%--<asp:Image ID="Image1" runat="server" ImageUrl="~/Images/20-5-2013.jpg" Height="200px" Width="100%" />--%>
                            <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/bcas2.png" Height="150px" Width="100%" />
                            </td>
                        </tr>
                        <tr bgcolor="#09638e">
                            <td height="5px">
                            
                               </td>
                        </tr>
                         <tr>
                            <td>
                                <table width="100%" bgcolor="#FBEBE6">
                                    <tr>
                                        <td valign="top" style="width: 20%; text-align: left;" align="right" 
                                            bgcolor="#99CCFF"  >
                                            Welcome user :<asp:LoginView ID="ctlRightNavbar" runat="server">
                                     <AnonymousTemplate>
                                        <%-- <a class="navbar"></a>--%>
                                     </AnonymousTemplate>
                                     <LoggedInTemplate>
                                      <%--<a class="navbar">Welcome: 
                                             <asp:LoginName ID="ctlUserName" runat="server" />
                                         </a>--%>
                                    <asp:LoginName ID="ctlUserName" runat="server" />
                                     </LoggedInTemplate>
                                 </asp:LoginView>  
                                            <%--<a class="navbar">Welcome: 
                                             <asp:LoginName ID="ctlUserName" runat="server" />
                                         </a>--%>                                                                                                            
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                            <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Online System of Registration for BCAS Training </b>
                                            &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                            <%--<asp:LinkButton ID="lnkHome" runat="server" PostBackUrl="~/Home.aspx" 
                                                CausesValidation="False">Home</asp:LinkButton>||--%>
                                                &nbsp;<asp:LoginStatus 
                                                ID="LoginStatus" runat="server" 
                                LogoutPageUrl="~/Home.aspx" 
                                Visible="False" onloggedout="LoginStatus_LoggedOut" />
                                            </td>
                                    </tr>
                                   
                                    </table>
                            </td>
                        </tr>                                                
                          <tr>                          
                            <td style="border-top:thin solid #09638e; border-bottom :thin solid #09638e; text-align: center;" >
                            <table align="center" width="100%">
                                <tr>
                                    <td bgcolor="#FBEBE6" >
                                    <table border="0" align="center" width="100%" height="100%" height="400px">
        <tr align="center" >
            <td valign="top" align="center" >
                <table border="0" align="center" width="90%" height="100%" height="100%">
                    <tr align="center" >
                        <td align="center" >
                            <table border="0" cellpadding="0" cellspacing="7" width="90%" bordercolor="#ECC4B9"
                                bgcolor="#FBEBE6" align="center" height="390px">
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
                                            meta:resourcekey="lbl_Category">Payment by</asp:Label>
                                                    </td>
                                                    <td width="55%" class="style1">
        
            <asp:DropDownList ID="ddlSponsoredOrg" runat="server" 
                DataSourceID="SqlDataPayeeSponsoredOrg" DataTextField="Payee_Organisation_Name" 
                DataValueField="Payee_Organisation_Code" Width="300px" AppendDataBoundItems="True" Enabled="False" 
                >
                <asp:ListItem Value="0">-Select Organisation-</asp:ListItem>
            </asp:DropDownList>
        
                                                    </td>
                                            </tr> 
                                            <tr>
                                                    <td style="text-align: right; vertical-align:top" class="style2">
                                        <asp:Label ID="lbl_Fee_Amount" runat="server" 
                                            meta:resourcekey="lbl_Date_of_birth">Amount To Pay (INR)</asp:Label>
                                                    </td>
                                                    <td class="style1">
                                                        <asp:TextBox ID="txtAmount" runat="server" Enabled="False"></asp:TextBox>
                                                    </td>
                                            </tr>
                                         <tr>
                                                    <td colspan ="2" style="text-align: center; vertical-align:top" >
                                        <asp:Label ID="lblMessage" runat="server"><b>Note: Any Extra Service Charges for use of Online Payment Services, if imposed shall be borne by the Payee Organisation, <br />depending upon the Mode of Payment chosen.</b></asp:Label>
                                                      <br />  <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="/Upload/Payment_Service_Charges.pdf" Text ="Check Charges Details" Target="_blank"/>
                                                        <!--<a href="/Upload/Payment_Service_Charges.pdf" target ="_blank" >Check Charges Details</a>-->
                                                    </td>
                                                    
                                            </tr>
                                            <tr bgcolor="#ffcccc">
                                                    <td style="text-align: left" class="style2">
         
            
            <asp:SqlDataSource ID="SqlDataPayeeSponsoredOrg" runat="server" 
                ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" 
                SelectCommand="SELECT p.Payee_Organisation_Code ,p.Payee_Organisation_Name 
                                FROM Payee_Organisation_Master p 
                                WHERE p.Payee_Organisation_Code  =@PayeeOrgCode
                                
                                ORDER BY p.Payee_Organisation_Name">
                <SelectParameters>
                    <asp:SessionParameter Name="PayeeOrgCode" SessionField="UserOrg" />
                </SelectParameters>
            </asp:SqlDataSource>
                                                    </td>
                                                    <td width="55%" style="text-align: left; width: 75%;" valign="middle">
                                                        &nbsp;<asp:Button ID="btnPayNow" runat="server" ForeColor="#000099" Text="Pay Now" 
                                                            PostBackUrl="https://www.billdesk.com/pgidsk/PGIMerchantPayment"  />
                                                    </td>
                                            </tr>                                           
                                     <tr><td colspan="2" align="left">
                                         <asp:Label ID="lbl_ErrorMsg" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                                         </td></tr>
                                               
                                     <tr><td colspan="2" align="left">
                                         <asp:Label ID="lblMsgValue" runat="server" Font-Bold="True" Font-Size="Medium" 
                                             ForeColor="#000066" Visible="False"></asp:Label>
                                         </td></tr>
                                               
                                     <tr><td colspan="2" align="left"><font color="#990000" size="2"> <u>Note </u>: 
                                         Clicking on the Pay Now button you will be redirected to Payment Gateway!!!</font></td></tr>
                                               
                                    </table>
                                    <asp:HiddenField ID="msg" runat="server" />
                                    <%--<asp:HiddenField ID="hidRequestId" runat="server" />
                                    <asp:HiddenField ID="hidOperation" runat="server" />--%>
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
                                <tr align="center" >
                                    <td align="center" >
                                     <asp:Menu ID="Menu2" runat="server"  Font-Bold="False" 
                                                Font-Size="Medium" ForeColor="Black" Orientation="Horizontal" 
                                    Font-Names="Calibri">
                                                <StaticMenuItemStyle HorizontalPadding="5px" />
                                                <DynamicHoverStyle BackColor="#3e9ec9" ForeColor="White" />
                                                <DynamicMenuStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                                <DynamicMenuItemStyle BackColor="#3e9ec9" ForeColor="Black" VerticalPadding="3px" />
                                                <StaticHoverStyle BackColor="#3e9ec9" ForeColor="White" />
                                                <Items>
                                                    <asp:MenuItem Enabled="False" Text="|" Value="|"></asp:MenuItem>
                                                    <asp:MenuItem Text="Website Policies" Value="Website Policies" 
                                                        NavigateUrl="~/Forms/DetailForms/frmWebsitePolicies.aspx">
                                                    </asp:MenuItem>
                                                    <asp:MenuItem Text="|" Value="|" Enabled="False"></asp:MenuItem>
                                                    <asp:MenuItem Text="Help" Value="Help" 
                                                        NavigateUrl="~/Forms/DetailForms/frmUnderConstruction.aspx">
                                                    </asp:MenuItem>
                                                    <asp:MenuItem Text="|" Value="|" Enabled="False"></asp:MenuItem>
                                                    <asp:MenuItem Text="Terms &amp; Conditions" Value="Terms &amp; Conditions" 
                                                        NavigateUrl="~/Forms/DetailForms/frmUnderConstruction.aspx">
                                                    </asp:MenuItem>
                                                    <asp:MenuItem Text="|" Value="|" Enabled="False"></asp:MenuItem>
                                                    <asp:MenuItem Text="Sitemap" Value="Sitemap" 
                                                        NavigateUrl="~/Forms/DetailForms/frmUnderConstruction.aspx">
                                                    </asp:MenuItem>
                                                    <asp:MenuItem Enabled="False" Text="|" Value="|"></asp:MenuItem>
                                                </Items>
                                            </asp:Menu>
                                    </td>
                                </tr>
                            </table>
                           
                                </td>
                            
                         </tr>                      
                                
                         <tr>
                            <td bgcolor="#ff5b0d" align="center">
                                <asp:Label ID="lblfooter1" runat="server" ForeColor="White" Text="Designed and Developed by:"></asp:Label>
                                &nbsp;<asp:HyperLink ID="hlfooter3" runat="server" NavigateUrl="http://www.nielit.in/index.jsp"
                                    ForeColor="White" Target="http://www.nielit.in/index.jsp" Text="National Institute of Electronics and Information Technology (NIELIT) Delhi, New Delhi"></asp:HyperLink>&nbsp;</td>
                        </tr>
                                
                         <tr>
                       <td bgcolor="#09638e" align="center">
                                <asp:Label ID="Label2" runat="server" ForeColor="White" Text="This site is best optimized for 1366 x 768 screen resolution and IE 9.0 & above"></asp:Label>
                                &nbsp;</td>
                                </tr>
                              </td>
            </tr>
                        
         </table>
    </div>
    </form>
</body>
</html>
