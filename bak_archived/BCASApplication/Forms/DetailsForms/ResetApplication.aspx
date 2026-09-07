<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ResetApplication.aspx.cs" Inherits="Forms_DetailsForms_ResetApplication" Title="BCAS-->ResetApplication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
        .style4
    {
        height: 26px;
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
                        &nbsp;</td>
                </tr>
                <tr>
                    <td height="30px">
                        <strong><font color="#990000">Reset Application Status</font></strong>
                    </td>
                </tr>
                    <tr align="center">
                        <td align="center">
                        <div id="divPrint">
                            <table border="0" cellpadding="0" cellspacing="7" width="90%" bordercolor="#ECC4B9"
                                bgcolor="#FBEBE6" align="center">
                                <tr>
                                    <td align="center">
                                    
                                    <table width="60%" border="2" bordercolor="#CEEDEE" align="center" 
                                            style="border: thin none #800080;">
                    <tr valign="middle">
                        <td width="25%" align="right">
                            &nbsp;
                        </td>
                        <td align="left">
                            <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Times New Roman"
                                Font-Size="X-Large" ForeColor="#990033" Text="Enter Registration Number" 
                                EnableTheming="False"></asp:Label>
                        </td>
                    </tr>
                    <tr valign="middle">
                        <td width="25%" align="right">
                            <asp:Label ID="UserNameLabel" runat="server" >Registration No:</asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtRegNo" runat="server" Width="220px" MaxLength="8" 
                                AutoCompleteType="Disabled" ></asp:TextBox>
                            
                            <asp:RequiredFieldValidator ID="RegNoRequired" runat="server" ControlToValidate="txtRegNo"
                                ErrorMessage="*" ToolTip="Registration No is required." SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr valign="middle">
                        <td width="25%">
                            &nbsp;
                        </td>
                        <td align="left">
                            <asp:Button ID="LoginButton" runat="server" Font-Bold="False"
                                Style="text-align: left; font-weight: 700;" Text="View Details" 
                                OnClick="LoginButton_Click" TabIndex="1" />
                        <asp:Button ID="btnRelieve" runat="server"                              
                                            TabIndex="7" 
                                            Text="Reset Application" 
                                onclick="btnRelieve_Click" style="font-weight: 700" /> 
                                
                        </td>
                    </tr>
                    <tr valign="middle">
                        <td width="25%" colspan="2">
                            <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#CC0000"
                                Visible="False" EnableTheming="false"></asp:Label>
                        </td>
                    </tr>
                </table>
                                       </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="center" >
         
            
            <asp:GridView ID="grdDisplay" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" 
                EmptyDataText="No Record Found" BorderColor="#000066" BorderStyle="Outset" 
                    BorderWidth="2px" Caption="Candidate Details" 
                CaptionAlign="Top" ForeColor="#333333" PageSize="5" Width="900px" 
                DataKeyNames="Registration_Number" style="text-align: center"  >
                <Columns>
                    <asp:BoundField DataField="Registration_Number" 
                        HeaderText="Registration_Number" 
                        SortExpression="Registration_Number" InsertVisible="False" 
                        ReadOnly="True" />
                    <asp:BoundField DataField="Candidate_Name" HeaderText="Candidate_Name" 
                        SortExpression="Candidate_Name" >
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Designation" HeaderText="Designation" 
                        SortExpression="Designation" >
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Qualification" 
                        HeaderText="Qualification" SortExpression="Qualification" >
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DOB" 
                        HeaderText="DOB" SortExpression="DOB" ReadOnly="True" />
                    <asp:BoundField DataField="Mobile_No" HeaderText="Mobile_No" 
                        SortExpression="Mobile_No" />
                    <asp:BoundField DataField="Organisation_Name" HeaderText="Organisation_Name" 
                        SortExpression="Organisation_Name" >
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Is_Application_Complete" HeaderText="Is_Application_Complete" 
                        SortExpression="Is_Application_Complete" />
                </Columns>
                <FooterStyle BackColor="#990000" />
                <HeaderStyle BackColor="#990000" ForeColor="White" />
                <AlternatingRowStyle BackColor="#FFFFCC" ForeColor="#333333" />
            </asp:GridView>
            
            
                                        </td>
                                </tr>
                                 
                                <tr >
                                    <td align="center">
                            <asp:Label ID="lbl_Success" runat="server" Font-Bold="False" 
                                Font-Names="Times New Roman" Font-Size="Large" ForeColor="#000099"></asp:Label>
                                
                                        <asp:Label ID="lbl_ErrorMsg" runat="server" ForeColor="Red" Font-Size="Large"></asp:Label>
                                    </td>
                                </tr>
                                 
                                </table>
                        </div>
                                
                        </td>
                    </tr>
                    <tr align="center">
                        <td align="center">
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" 
                SelectCommand="SELECT Candidate_Registration.Registration_Number,Candidate_Registration.Candidate_Name,Designation_Master.Description Designation,Qualification_Master.Description Qualification,
convert(varchar(10),Candidate_Registration.DOB,103) DOB,Candidate_Registration.Mobile_No,
Sponsoring_Organisation_Master.Organisation_Name,Is_Application_Complete 
FROM  Candidate_Registration INNER JOIN Sponsoring_Organisation_Master on
Candidate_Registration.Organisation_Code =Sponsoring_Organisation_Master.Organisation_Code
INNER Join Country_Master on
Candidate_Registration.Nationality_Code =Country_Master.Country_Code 
INNER JOIN Designation_Master ON
Designation_Master.Designation_Code=Candidate_Registration.Designation_Code
INNER JOIN Qualification_Master ON
Qualification_Master.Qualification_Code=Candidate_Registration.Educational_Qualification_Code
WHERE Candidate_Registration.Registration_Number =@i_Registration_Number">
                <SelectParameters>
                    <asp:ControlParameter ControlID="txtRegNo" Name="i_Registration_Number" 
                        PropertyName="Text" />
                </SelectParameters>
            </asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

