<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PaymentBalance.aspx.cs" Inherits="Forms_DetailsForms_PaymentBalance" Culture="en-GB" Title="BCAS-->PaymentBalance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="90%" align="center" >
    <tr>
        <td colspan="2" align="center"  >
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="2" align="center"  >
            <asp:Label ID="lblHeading" runat="server" Font-Bold="True" ForeColor="#990000" 
                Text="PAYMENT BALANCE "></asp:Label>
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
            <table width="80%" align="center" border="2"  >
                <tr align="left">
                    <td width="30%">
            <asp:Label ID="lblSponsoringOrg" runat="server" Text="Sponsoring Organisation"></asp:Label>
                    </td>
                    <td width="70%">
            <asp:DropDownList ID="ddlSponOrg" runat="server" DataTextField="Organisation_Name" 
                DataValueField="Sponsoring_Organisation_Code" Width="500px" 
                AppendDataBoundItems="True" DataSourceID="SqlDataSponOrg" >
                <asp:ListItem Value="0">-Select -</asp:ListItem>
            </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="ddlSponOrg" ErrorMessage="*" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                 </table>
            
            </td>
    </tr>
    <tr>
        <td width="30%" colspan="2" style="width: 100%" align="center"  >            
            <asp:Button ID="btnCount" runat="server" Font-Bold="True" ForeColor="Maroon" 
                onclick="btnCount_Click" Text="Get Balance" />
        </td>
    </tr>
    <tr>
        <td width="30%" colspan="2" style="width: 100%" align="center"  >            
            &nbsp;</td>
    </tr>
    <tr>
        <td width="30%" colspan="2" style="width: 100%" align="center"  >            
         
            
            <asp:GridView ID="grdBalance" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataDisplay" 
                EmptyDataText="No Record Found" BorderColor="#000066" BorderStyle="Outset" 
                    BorderWidth="2px" Caption="Payment Balance" 
                CaptionAlign="Top" ForeColor="#333333" 
                AllowSorting="True" Visible="False"  >
                <Columns>
                    <asp:BoundField DataField="Sponsoring_Organisation" HeaderText="Sponsoring Organisation" 
                        SortExpression="Sponsoring_Organisation" >
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Payee_Organisation_Name" 
                        HeaderText="Payee Organisation Name" 
                        SortExpression="Payee_Organisation_Name" >
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Total_Amount" HeaderText="Total Amount" 
                        SortExpression="Total_Amount" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Balance_Amount" HeaderText="Balance Amount" 
                        SortExpression="Balance_Amount" >
                        <ItemStyle HorizontalAlign="Center" />
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
        <td width="30%" colspan="2" style="width: 100%; text-align: left;" 
            align="center"  >            
            <asp:SqlDataSource ID="SqlDataSponOrg" runat="server" 
                ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" 
                SelectCommand="SELECT distinct Sponsoring_Organisation_Master.Organisation_Name,Payee_Sponsored_Organisation.Sponsoring_Organisation_Code  
 FROM Payee_Sponsored_Organisation 
 INNER JOIN Sponsoring_Organisation_Master ON
 Sponsoring_Organisation_Master.Organisation_Code =Payee_Sponsored_Organisation.Sponsoring_Organisation_Code
 ORDER BY Sponsoring_Organisation_Master.Organisation_Name ">
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataPayOrg" runat="server" 
                ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" 
                SelectCommand="SELECT distinct Payee_Organisation_Master.Payee_Organisation_Name,Payee_Sponsored_Organisation.Payee_Organisation_Code   
FROM Payee_Sponsored_Organisation 
INNER JOIN Payee_Organisation_Master ON 
Payee_Sponsored_Organisation.Payee_Organisation_Code =Payee_Organisation_Master.Payee_Organisation_Code
where Payee_Sponsored_Organisation.Sponsoring_Organisation_Code =@SponOrg
ORDER BY Payee_Organisation_Master.Payee_Organisation_Name">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ddlSponOrg" Name="SponOrg" 
                        PropertyName="SelectedValue" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataDisplay" runat="server" 
                ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" SelectCommand="SELECT Sponsoring_Organisation_Master.Organisation_Name Sponsoring_Organisation,Payee_Organisation_Master.Payee_Organisation_Name, 
	Payment_Balances.Total_Amount ,Payment_Balances.Balance_Amount  
	FROM Payment_Balances INNER JOIN Payee_Organisation_Master ON
	Payment_Balances.Payee_Organisation_Code =Payee_Organisation_Master.Payee_Organisation_Code 
	INNER JOIN Sponsoring_Organisation_Master ON
	Payment_Balances.Sponsoring_Organisation_Code =Sponsoring_Organisation_Master.Organisation_Code 
	WHERE Payment_Balances.Sponsoring_Organisation_Code =@SponsOrg
	ORDER BY Sponsoring_Organisation_Master.Organisation_Name ">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ddlSponOrg" Name="SponsOrg" 
                        PropertyName="SelectedValue" />
                </SelectParameters>
            </asp:SqlDataSource>
        </td>
    </tr>
    </table> 
</asp:Content>

