<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="PaymentResponse.aspx.cs" Inherits="Forms_DetailsForms_PaymentResponse" Culture="en-GB" Title="BCAS-->PaymentResponse" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">



<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
   
      
     <script type="text/javascript" language="javascript">

         function DisableBackButton() {

             window.history.forward()

         }
         DisableBackButton();
         window.onload = DisableBackButton;
         window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
         window.onunload = function () { void (0) }


</script>

    
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
    <form id="form1" runat="server" autocomplete="off">
         <div id="selector" align="center"  >
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
                        </table>
                    </td></tr>
                   <tr>
                <td  width="5%" bgcolor="#FBEBE6" colspan="2">
<table width="100%" align="center" >
<tr>
    <td></td>
</tr>
<tr align="right">
    <td> 

        <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/Home.aspx">Login</asp:LinkButton>
    </td>
</tr>
<tr align="center" > 
    <td>
     <table width="60%" align="center" border="2">
    <tr>
        <td></td>
    </tr>
    <tr id="trReciept" runat="server"  >
        <td align="center">
            <table width="80%" border="1" >
            
                <tr align="center" >
                    <td width="40%" colspan="2" style="width: 80%">
                        <asp:Label ID="lblPaymentDetails" runat="server" Font-Bold="False" 
                            Font-Names="Monotype Corsiva" Font-Size="X-Large" ForeColor="#990033">Payment Details</asp:Label>
                    </td>                
                </tr>
                <tr align="left" >
                    <td width="40%" class="style1">
                        <%--<asp:Label ID="lblPayeeOrg" runat="server" Text="Paid By (Payee)"></asp:Label>--%>
                        <asp:Label ID="lblPayeeOrg" runat="server" Text="Paid By (Payee Org)"></asp:Label>
                    </td>                
                    <td width="40%" class="style1">
                        <%--<asp:Label ID="vlblPayeeOrg" runat="server"></asp:Label>--%>
                        <asp:Label ID="vlblSponOrg" runat="server"></asp:Label>
                    </td>
                </tr>
                <%--    <tr align="left" >
                    <td width="40%" class="style1">
                        <asp:Label ID="lblSponOrg" runat="server" Text="Payment For"></asp:Label>
                    </td>                
                    <td width="40%" class="style1">
                       <asp:Label ID="vlblSponOrg" runat="server"></asp:Label>
                    </td>
                </tr>--%>
                <tr align="left" >
                    <td width="40%" class="style1">
                        Course</td>                
                    <td width="40%" class="style1">
                        <asp:Label ID="vlblCourse" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr align="left" >
                    <td width="40%" class="style1">
                        <asp:Label ID="lblTransactionNo" runat="server" Text="Transaction No"></asp:Label>
                    </td>                
                    <td width="40%" class="style1">
                        <asp:Label ID="vlblTransactionNo" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr align="left" >
                    <td width="40%">
                        <asp:Label ID="lblFeeAmount" runat="server" Text="Fee Amount"></asp:Label>
                    </td>                
                    <td width="40%">
                        <asp:Label ID="vlblFeeAmount" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr align="left" >
                    <td width="40%">
                        <asp:Label ID="lblPaymentDate" runat="server" Text="Date of Payment "></asp:Label>
                    </td>                
                    <td width="40%">
                        <asp:Label ID="vlblPaymentDate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr align="left" >
                    <td width="40%">
                        <asp:Label ID="lblPaymentStatus" runat="server">Payment Status</asp:Label>
                    </td>                
                    <td width="40%">
                        <asp:Label ID="vlblPaymentStatus" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr >
                    <td width="40%" colspan="2" style="width: 80%; text-align: center">
                        <input id="btnPrint" type="button" value="Print" onclick="window.print();" style="width: 114px" /></td>                
                </tr>
            </table>
        </td>
    </tr>
    <tr id="trError" runat="server" >
        <td style="text-align: center">
<asp:Label ID="lblError" runat="server" ForeColor="#990033" Visible="False"></asp:Label>
        
        </td>
    </tr>
</table>
    </td>
</tr>

</table>
</td></tr></table>
</div>
    </form>
</body>
</html>

