<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="QryUserDetails.aspx.cs" Inherits="Forms_DetailsForms_QryUserDetails" Title="BCAS-->QryUserDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
                Text="Query -User Details"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="30%">
            &nbsp;</td>
            <td width="70%">
            &nbsp;</td>
        </tr>
        <tr>
            <td width="30%" colspan="2" style="width: 100%" align="center"  >
            &nbsp;</td>
        </tr>
        <tr>
            <td width="30%" colspan="2" style="width: 100%" align="center"  >
              <%--  <asp:Panel ID="Panel1" runat="server" Height ="400px" Width="700px" 
                    ScrollBars="Vertical">
                
                </asp:Panel>--%>
                
                <asp:GridView ID="grdUserDetails" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" 
                EmptyDataText="No Record Found" BorderColor="#000066" BorderStyle="Outset" 
                    BorderWidth="2px" Caption="User Details" 
                CaptionAlign="Top" ForeColor="#333333" AllowPaging="True" 
                AllowSorting="True" PageSize="20" 
                    DataKeyNames="UserName"  >
                    <Columns>
                        <asp:BoundField DataField="UserName" HeaderText="UserName" 
                        SortExpression="UserName" ReadOnly="True" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Name" 
                        HeaderText="Name" 
                        SortExpression="Name" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Email" HeaderText="Email" 
                        SortExpression="Email" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Mobile_No" HeaderText="Mobile_No" 
                        SortExpression="Mobile_No" />
                        <asp:BoundField DataField="OrganisationCode" 
                            HeaderText="OrganisationCode" SortExpression="OrganisationCode" />
                        <asp:BoundField DataField="Organisation_Type" 
                            HeaderText="Organisation_Type" SortExpression="Organisation_Type" />
                        <asp:BoundField DataField="Organisation_Name" HeaderText="Organisation_Name" 
                            SortExpression="Organisation_Name" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="OrgType" HeaderText="OrgType" 
                            SortExpression="OrgType" />
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
                SelectCommand="SELECT UserDetails.UserName,UserDetails.Name,UserDetails.Email,UserDetails.Mobile_No, UserDetails.OrganisationCode,
UserDetails.Organisation_Type, V_Organisations.Organisation_Name, V_Organisations.OrgType
FROM  V_Organisations Right Outer Join UserDetails on
UserDetails.OrganisationCode=  V_Organisations.Organisation_Code 
Order By OrgType,UserName"></asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>

