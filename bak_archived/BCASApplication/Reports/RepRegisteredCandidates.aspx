<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RepRegisteredCandidates.aspx.cs" Inherits="Reports_RepRegisteredCandidates" Title="BCAS-->RegisteredCandidates" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="95%" align="center" >
    <tr>
        <td></td>
    </tr>
    <tr>
        <td></td>
    </tr>
     <tr align="left">
                        <td width="30%">
                            <asp:Label ID="lblCourseSchedule0" runat="server" 
                                Text="Sponsoring Organisation"></asp:Label>
                        </td>
                        <td width="70%">
                             <asp:DropDownList ID="ddlOrganisation" runat="server" Width="300px" DataTextField="Organisation_Name" 
                DataValueField="Organisation_Code"                 
                AppendDataBoundItems="True" TabIndex="2"  >               
                <asp:ListItem Value="0">--Select Sponsoring Organisation--</asp:ListItem>
                                
            </asp:DropDownList>
            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ErrorMessage="*" InitialValue="0" SetFocusOnError="True" 
                                 ControlToValidate="ddlOrganisation"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
     <tr align="left">
                        <td width="30%">
                           
                        </td>
                        <td width="70%">
                            <asp:Button ID="btnDisplay" runat="server" Text="Get Data" Width="250px" OnClick="btnDisplay_Click"
                            CausesValidation="false"    TabIndex="3" />
                        <asp:Label ID="lbltot" Visible="false" runat="server" Text="0"></asp:Label>
                        <asp:Label ID="lblOrg" Visible="false" runat="server" Text="0"></asp:Label></td>
                    </tr>
   <tr><td></td></tr>
    <tr align="center">
        <td colspan ="2">     
               
            <asp:Label ID="lblHeading" runat="server" Font-Bold="True" Font-Size="Large" 
                ForeColor="#000099"></asp:Label>
               
        </td>
        
    </tr>
   
    <tr>
        <td>     
               
            &nbsp;</td>
        
    </tr>
   
   
    <tr>
        <td align="center" Height="50px" valign="top" colspan ="2" >      
            <asp:ImageButton ID="imgpdf" runat="server" ImageUrl ="~/Images/pdf.jpg" width="6%" Height ="47%"  OnClick ="imgPDF_Click"  Visible ="false" /> &nbsp;&nbsp;
            <asp:ImageButton ID="imgExcel" runat="server" ImageUrl ="~/Images/Export_Exl.jpg" width="5%" Height ="55%" OnClick ="imgExcel_Click" visible="false" />
   
              &nbsp;</td> 
        </tr>
    <tr>
        <td align="left" colspan ="2"><div id="divReportData" runat="server" style="width:100%;"></div>
</td>      
    </tr>
  
   
</table>
</asp:Content>

