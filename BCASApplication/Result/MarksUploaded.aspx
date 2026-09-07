<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="MarksUploaded.aspx.cs" Culture="en-GB" Inherits="Result_MarksUploaded"
    Title="BCAS-->UpdateAadhaarDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



 <table align="center" width="100%">
        
              <tr>
            <td align="center" height="150px">
                <table width="60%" border="5" bordercolor="#CEEDEE" align="center">
                    <tr valign="middle">
                        <td width="25%" align="right">
                            &nbsp;
                        </td>
                        <td align="left">
                            <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Times New Roman"
                                Font-Size="X-Large" ForeColor="#990033" Text="Please Select Excel File" 
                                EnableTheming="False"></asp:Label>
                        </td>
                    </tr>
                    <tr valign="middle">
                        <td width="25%" align="right">
                            <asp:Label ID="UserNameLabel" runat="server" >Resultfile:</asp:Label>
                        </td>
                        <td align="left">
                            <asp:FileUpload ID="FileUpload1" runat="server" />
                        </td>
                    </tr>
                    <tr valign="middle">
                        <td width="25%">
                            &nbsp;
                        </td>
                        <td align="left">
                          <asp:Button ID="Button1" Text="Upload" OnClick = "Upload" runat="server" />
                            <asp:Label ID="lblMessage" runat="server" Font-Bold="True" ForeColor="#009933"></asp:Label><br />
                        </td>
                    </tr>
                    <tr valign="middle">
                        <td width="25%" colspan="2">
                            &nbsp;</td>
                    </tr>
                    
                </table>
            </td>
        </tr>
      

 </table>
 
 
 
 
 
 
</asp:Content>
