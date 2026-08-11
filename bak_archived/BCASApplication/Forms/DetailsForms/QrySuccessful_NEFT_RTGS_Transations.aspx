<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="QrySuccessful_NEFT_RTGS_Transations.aspx.cs" Inherits="Forms_DetailsForms_QrySuccessful_NEFT_RTGS_Transations" Title="BCAS-->NEFT_RTGS_Transations" %>

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
                Text="Successful NEFT RTGS Transations"></asp:Label>
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
                    BorderWidth="2px" Caption="Total Successful NEFT RTGS Transations" 
                CaptionAlign="Top" ForeColor="#333333" AllowPaging="True" 
                AllowSorting="True" PageSize="20" Width="900px" 
                DataKeyNames="UTR_Number"  >
                <Columns>
                    <asp:BoundField DataField="UTR_Number" HeaderText="UTR_Number" 
                        SortExpression="UTR_Number" ReadOnly="True" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Sender_Name" 
                        HeaderText="Sender_Name" 
                        SortExpression="Sender_Name" >
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Amount" HeaderText="Amount" 
                        SortExpression="Amount" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TransationDate" HeaderText="TransationDate" 
                        SortExpression="TransationDate" ReadOnly="True" >
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Payee_Organisation_Name" 
                        HeaderText="Payee_Organisation_Name" 
                        SortExpression="Payee_Organisation_Name" >
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Sponsoring_Organisation" 
                        HeaderText="Sponsoring_Organisation" 
                        SortExpression="Sponsoring_Organisation" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Verified" HeaderText="Verified" 
                        SortExpression="Verified" />
                    <asp:BoundField DataField="Verifying_Date" HeaderText="Verifying_Date" 
                        ReadOnly="True" SortExpression="Verifying_Date" />
                    <asp:BoundField DataField="Verified_By" HeaderText="Verified_By" 
                        SortExpression="Verified_By" >
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
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
                SelectCommand="SELECT Rtgs_Neft_Details.UTR_Number,Rtgs_Neft_Details.Sender_Name,Rtgs_Neft_Details.Amount ,convert(varchar(10),Rtgs_Neft_Details.Transaction_date,105) TransationDate,
Payee_Organisation_Master.Payee_Organisation_Name ,Sponsoring_Organisation_Master.Organisation_Name Sponsoring_Organisation,
Rtgs_Neft_Details.Verified, convert(varchar(10),Rtgs_Neft_Details.Verifying_Date,105)Verifying_Date,Rtgs_Neft_Details.Verified_By 
from Rtgs_Neft_Details 
INNER JOIN Sponsoring_Organisation_Master on
Rtgs_Neft_Details.Sponsoring_Organisation_Code =Sponsoring_Organisation_Master.Organisation_Code 
INNER JOIN Payee_Organisation_Master on
Rtgs_Neft_Details.Payee_Organisation_Code =Payee_Organisation_Master.Payee_Organisation_Code 
WHERE Verified ='Y' 
order by Rtgs_Neft_Details.Transaction_date">
            </asp:SqlDataSource>
        </td>
    </tr>
    </table> 
</asp:Content>

