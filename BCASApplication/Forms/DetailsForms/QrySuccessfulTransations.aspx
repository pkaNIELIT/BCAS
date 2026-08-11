<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="QrySuccessfulTransations.aspx.cs" Inherits="Forms_DetailsForms_QrySuccessfulTransations" Culture="en-GB" Title="BCAS-->QrySuccessfulTransations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            height: 21px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <table width="100%" align="center" >
        <tr>
            <td colspan="2" align="center"  >
            &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" align="center" class="style1"  >
                <asp:Label ID="lblHeading" runat="server" Font-Bold="True" ForeColor="#990000" 
                Text="Successful Transaction"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="30%">
            &nbsp;</td>
            <td width="70%">
            &nbsp;</td>
        </tr>
        <tr align="center" >
            <td width="30%" colspan="2" style="width: 100%"  >
                &nbsp;</td>
        </tr>
        <tr>
            <td width="30%" colspan="2" style="width: 100%" align="center"  >
                <asp:Button ID="btnCount" runat="server" Font-Bold="True" ForeColor="Maroon" 
                onclick="btnCount_Click" Text="Get Count" />
            </td>
        </tr>
        <tr>
            <td width="30%" colspan="2" style="width: 100%" align="center"  >
            &nbsp;</td>
        </tr>
        <tr>
            <td width="30%" colspan="2" style="width: 100%" align="center"  >
                <asp:GridView ID="grdSucccessfulTransaction" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" 
                EmptyDataText="No Record Found" BorderColor="#000066" BorderStyle="Outset" 
                    BorderWidth="2px" Caption="Successful Transactions" 
                CaptionAlign="Top" ForeColor="#333333" AllowPaging="True" 
                AllowSorting="True" Visible="False" PageSize="20" 
                    DataKeyNames="Transaction_id"  >
                    <Columns>
                        <asp:BoundField DataField="Reference_No" HeaderText="Reference_No" 
                        SortExpression="Reference_No" />
                        <asp:BoundField DataField="Transaction_id" 
                        HeaderText="Transaction_id" 
                        SortExpression="Transaction_id" ReadOnly="True" />
                        <asp:BoundField DataField="Amount_Recieved" HeaderText="Amount_Recieved" 
                        SortExpression="Amount_Recieved" />
                        <asp:BoundField DataField="Date_of_Payment" HeaderText="Date_of_Payment" 
                        SortExpression="Date_of_Payment" ReadOnly="True" />
                        <asp:BoundField DataField="Payee_Organisation_Name" 
                            HeaderText="Payee_Organisation_Name" 
                            SortExpression="Payee_Organisation_Name" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Sponsoring_Organisation" 
                            HeaderText="Sponsoring_Organisation" 
                            SortExpression="Sponsoring_Organisation" >
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
            <td width="30%" colspan="2" style="width: 100%" align="center"  >
            &nbsp;</td>
        </tr>
        <tr>
            <td width="30%" colspan="2" style="width: 100%" align="center"  >
                &nbsp;</td>
        </tr>
        <tr>
            <td width="30%" colspan="2" style="width: 100%" align="center"  >
            &nbsp;</td>
        </tr>
        <tr>
            <td width="30%" colspan="2" style="width: 100%; text-align: left;" 
            align="center"  >
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" 
                SelectCommand="SELECT Reference_No,Transaction_id,Amount_Recieved,convert(varchar(10),Date_of_Payment,105) Date_of_Payment,
Payee_Organisation_Master.Payee_Organisation_Name ,Sponsoring_Organisation_Master.Organisation_Name Sponsoring_Organisation 
from Successful_Transactions
INNER JOIN Sponsoring_Organisation_Master on
Successful_Transactions.Sponsoring_Organisation_Code =Sponsoring_Organisation_Master.Organisation_Code 
INNER JOIN Payee_Organisation_Master on
Successful_Transactions.Payee_Organisation_Code =Payee_Organisation_Master.Payee_Organisation_Code  
order by Date_of_Payment,Reference_No"></asp:SqlDataSource>
            </td>
        </tr>
    </table>


</asp:Content>

