<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CountRegistrations.aspx.cs" Inherits="Forms_DetailsForms_CountRegistrations" Title="BCAS-->CountRegistrations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="100%" align="center" >
    <tr>
        <td align="center"  >
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center"  >
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center"  >
            <asp:Label ID="lblHeading" runat="server" Font-Bold="True" ForeColor="#990000" 
                Text="Total Organisation Wise Registration Count"></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="30%" style="width: 100%" align="center"  >            
            &nbsp;</td>
    </tr>
    <tr>
        <td width="30%" style="width: 100%" align="center"  >            
         
            
            <asp:GridView ID="grdOrgWiseCount" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" 
                EmptyDataText="No Record Found" BorderColor="#000066" BorderStyle="Outset" 
                    BorderWidth="2px" Caption="Total Organisation Wise Registration Count" 
                CaptionAlign="Top" ForeColor="#333333" AllowPaging="True" 
                AllowSorting="True" PageSize="20" Width="700px"  >
                <Columns>
                    <asp:BoundField DataField="Organisation_Name" HeaderText="Organisation_Name" 
                        SortExpression="Organisation_Name" >
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Total_Registrations" 
                        HeaderText="Total_Registrations" 
                        SortExpression="Total_Registrations" ReadOnly="True" />
                    <asp:BoundField DataField="CompleteApplications" HeaderText="CompleteApplications" 
                        SortExpression="CompleteApplications" ReadOnly="True" />
                    <asp:BoundField DataField="IncompleteApplications" HeaderText="IncompleteApplications" 
                        SortExpression="IncompleteApplications" ReadOnly="True" />
                </Columns>
                <FooterStyle BackColor="#990000" />
                <HeaderStyle BackColor="#990000" ForeColor="White" />
                <AlternatingRowStyle BackColor="#FFFFCC" ForeColor="#333333" />
            </asp:GridView>
            
            
        </td>
    </tr>
    <tr>
        <td width="30%" style="width: 100%" align="center"  >            
            &nbsp;</td>
    </tr>
    <tr>
        <td width="30%" style="width: 100%" align="center"  >            
            &nbsp;</td>
    </tr>
    <tr>
        <td width="30%" style="width: 100%; text-align: left;" 
            align="center"  >            
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" 
                SelectCommand="SELECT Sponsoring_Organisation_Master.Organisation_Name, COUNT (*) Total_Registrations, 
CompleteApplications=count(case when Candidate_Registration.Is_Application_Complete ='Y' then 1 end),
IncompleteApplications=count(case when Candidate_Registration.Is_Application_Complete ='N' then 2 end)	
FROM Candidate_Registration INNER JOIN Sponsoring_Organisation_Master ON
Candidate_Registration.Organisation_Code =Sponsoring_Organisation_Master.Organisation_Code 	
group by Sponsoring_Organisation_Master.Organisation_Name 
order by Sponsoring_Organisation_Master.Organisation_Name">
            </asp:SqlDataSource>
        </td>
    </tr>
    </table> 
</asp:Content>

