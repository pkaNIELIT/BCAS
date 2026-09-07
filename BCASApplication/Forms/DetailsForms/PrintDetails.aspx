<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PrintDetails.aspx.cs" Inherits="Forms_DetailsForms_PrintDetails" Culture="en-GB" Title="BCAS-->PrintApplication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            text-align: left;
            width: 0%;
            vertical-align: top;
        }
        .style2
        {
            width: 40%;
        }
        .style3
        {
            text-align: left;
            width: 55%;
            vertical-align: top;
        }
    </style>
        
        <script language="javascript">
function printdiv(printpage)
{
var headstr = "<html><head><title></title></head><body>";
var footstr = "</body>";
var newstr = document.all.item(printpage).innerHTML;
var oldstr = document.body.innerHTML;
document.body.innerHTML = headstr+newstr+footstr;
window.print();
document.body.innerHTML = oldstr;
return false;
}
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table border="0" align="center" width="100%">
        <tr align="center">
            <td valign="top" align="center">
                <table border="0" align="center" width="90%">
                <tr>
                    <td height="30px">
                    </td>
                </tr>
                    <tr align="center">
                        <td align="center">
                        <div id="divPrint">
                            <table border="0" cellpadding="0" cellspacing="7" width="90%" bordercolor="#ECC4B9"
                                bgcolor="#FBEBE6" align="center">
                                <tr>
                                    <td align="center">
                                        <strong><font color="#990000">Online Application For BCAS</font></strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <hr color="#990000" size="1">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                    <strong><font color="#990000">Thank you for the Registartion, Kindly keep the Registration Number for future reference. </font></strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                    <table style="border: thin groove #993366" width="100%" style="text-align: right" cellpadding ="5" cellspacing ="0" border="1">
                                            <tr>
                                                    <td style="text-align: right; vertical-align:top" class="style2">
                                        <asp:Label ID="lbl_Organisation" runat="server" 
                                            meta:resourcekey="lbl_Category"></asp:Label>
                                                    </td>
                                                    <td class="style3">
                                        <asp:Label ID="lbl_valOrg" runat="server" meta:resourcekey="Label4Resource1" 
                                            EnableTheming="False" Font-Bold="True"></asp:Label>
                                                    </td>
                                                    <td width="55%" class="style1" rowspan="10" >
                                                    
                                <asp:Image ID="Img_Photo" runat="server" Width="120px" Height="143px" 
                                    meta:resourcekey="Image1Resource1" /><br /><br /><hr size="1"/>
                                     <asp:Image ID="ImgSign" runat="server" Width="120px" Height="50px" 
                                    meta:resourcekey="Image1Resource1" /><br />
                                                    
                                                    </td>
                                            </tr>
                                            <%--deep--%>
                                            <tr>
                                                    <td style="text-align: right; vertical-align:top" class="style2">
                                        <asp:Label ID="lbl_OrganisationNameofCandidate" runat="server" 
                                            meta:resourcekey="lbl_OrganisationNameofCandidate"></asp:Label>
                                                    </td>
                                                    <td class="style3">
                                        <asp:Label ID="lbl_OrganisationName" runat="server" meta:resourcekey="Label4Resource1" 
                                            EnableTheming="False" Font-Bold="True"></asp:Label>
                                                    </td>
                                            </tr> 
                                            <%--deep--%>
                                            <tr>
                                                    <td style="text-align: right; vertical-align:top" class="style2">
                                        <asp:Label ID="lbl_application_no" runat="server" meta:resourcekey="lbl_application_no"></asp:Label>
                                                    </td>
                                                    <td class="style3">
                                        <asp:Label ID="lbl_Apl_no" runat="server"
                                                            Width="139px"  
                                            EnableTheming="False" Font-Bold="True"></asp:Label>
                                                    </td>
                                            </tr>
                                            <tr>
                                                    <td style="text-align: right; vertical-align:top" class="style2">
                                        <asp:Label ID="lbl_Candidate_Name" runat="server" 
                                            meta:resourcekey="lbl_Candidate_Name"></asp:Label>
                                                    </td>
                                                    <td class="style3">
                                        <asp:Label ID="lbl_Name" runat="server" meta:resourcekey="Label4Resource1" 
                                            EnableTheming="False" Font-Bold="True"></asp:Label>
                                                    </td>
                                            </tr> 
                                            <tr>
                                                    <td style="text-align: right; vertical-align:top" class="style2">
                                                        <asp:Label ID="Label3" runat="server" Text="Sex"></asp:Label>
                                                    </td>
                                                    <td class="style3">
                                        <asp:Label ID="lbl_Sex" runat="server" 
                                            EnableTheming="False" Font-Bold="True"></asp:Label>
                                                    </td>
                                            </tr>
                                            <tr>
                                                    <td style="text-align: right; vertical-align:top" class="style2">
                                        <asp:Label ID="lbl_Designation" runat="server" 
                                            meta:resourcekey="lbl_Father_Name"></asp:Label>
                                                    </td>
                                                    <td class="style3">
                                        <asp:Label ID="lbl_valDesignation" runat="server" meta:resourcekey="Label4Resource1" 
                                            EnableTheming="False" Font-Bold="True"></asp:Label>
                                                    </td>
                                            </tr>
                                            <tr>
                                                    <td style="text-align: right; vertical-align:top" class="style2">
                                        <asp:Label ID="lbl_Date_of_birth" runat="server" 
                                            meta:resourcekey="lbl_Date_of_birth"></asp:Label>
                                                    </td>
                                                    <td class="style3">
                                        <asp:Label ID="lbl_dob" runat="server" meta:resourcekey="Label4Resource1" 
                                            EnableTheming="False" Font-Bold="True"></asp:Label>
                                                    </td>
                                            </tr>
                                            <tr>
                                                    <td style="text-align: right; vertical-align:top" class="style2">
                                                        <asp:Label ID="Label4" runat="server" Text="Qualification"></asp:Label>
                                                    </td>
                                                    <td class="style3">
                                        <asp:Label ID="lbl_Qualification" runat="server" 
                                            EnableTheming="False" Font-Bold="True"></asp:Label>
                                                    </td>
                                            </tr>
                                            <tr>
                                                    <td style="text-align: right; vertical-align:top" class="style2">
                                                        <asp:Label ID="Label5" runat="server" Text="Mobile No"></asp:Label>
                                                    </td>
                                                    <td class="style3">
                                        <asp:Label ID="lbl_Mobile" runat="server" 
                                            EnableTheming="False" Font-Bold="True"></asp:Label>
                                                    </td>
                                            </tr>
                                            <tr>
                                                    <td style="text-align: right; vertical-align:top" class="style2">
                                                        <asp:Label ID="Label6" runat="server" Text="Email"></asp:Label>
                                                    </td>
                                                    <td class="style3">
                                        <asp:Label ID="lbl_Email" runat="server" 
                                            EnableTheming="False" Font-Bold="True"></asp:Label>
                                                    </td>
                                            </tr>
                                            <tr>
                                                    <td style="text-align: right; vertical-align:top" class="style2">
                                                        <asp:Label ID="Label7" runat="server" Text="Country"></asp:Label>
                                                    </td>
                                                    <td class="style3">
                                        <asp:Label ID="lbl_Country" runat="server" 
                                            EnableTheming="False" Font-Bold="True"></asp:Label>
                                                    </td>
                                            </tr>
                                                                                     
                                          </table>
                                       </td>
                                </tr>
                                 
                                </table>
                        </div>
                        <asp:Button ID="btnPrint" runat="server"                              
                                            TabIndex="7" 
                                            Text="Print Application" onclientclick="printdiv('divPrint')" /> 
                                
                                        <asp:Label ID="lbl_ErrorMsg" runat="server" ForeColor="Red" Font-Size="Large"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

