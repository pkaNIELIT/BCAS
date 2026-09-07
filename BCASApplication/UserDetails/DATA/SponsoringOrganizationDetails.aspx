
<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SponsoringOrganizationDetails.aspx.cs" Inherits="UserDetails_SponsoringOrganizationDetails" Title="BCAS-->Sponsoring Organization Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table width="100%" align="center">
        <tr>
            <td colspan="2" align="center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center" class="style1">
                <asp:Label ID="lblHeading" runat="server" Font-Bold="True" ForeColor="#990000" 
                    Text="Sponsoring Organization Details"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="30%">
                &nbsp;
            </td>
            <td width="70%">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td width="30%" colspan="2" style="width: 100%" align="center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td width="30%" colspan="2" style="width: 100%" align="center">
                <!-- GridView for Sponsoring Organization Details -->
                <asp:GridView ID="grdSponsoringOrganizationDetails" runat="server" AutoGenerateColumns="False" 
                    EmptyDataText="No Record Found" BorderColor="#000066" BorderStyle="Outset" 
                    BorderWidth="2px" Caption="Sponsoring Organization Details" 
                    CaptionAlign="Top" ForeColor="#333333" AllowPaging="True" 
                    AllowSorting="False" PageSize="20" DataKeyNames="Sponsoring_Organisation_Code"
                    OnPageIndexChanging="grdSponsoringOrganizationDetails_PageIndexChanging"
                    OnRowEditing="grdSponsoringOrganizationDetails_RowEditing"
                    OnRowUpdating="grdSponsoringOrganizationDetails_RowUpdating"
                    OnRowCancelingEdit="grdSponsoringOrganizationDetails_RowCancelingEdit">
                    <Columns>
                        <asp:CommandField ShowEditButton="True" HeaderText="Edit" ButtonType="Link">
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:CommandField>
                        <asp:BoundField DataField="Sponsoring_Organisation_Code" HeaderText="Sponsoring Organisation Code" 
                            ReadOnly="True" SortExpression="Sponsoring_Organisation_Code">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Sponsoring_Organisation_Name" HeaderText="Sponsoring Organisation Name" 
                            SortExpression="Sponsoring_Organisation_Name">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Sponsoring_Organisation_State" HeaderText="Sponsoring Organisation State" 
                            SortExpression="Sponsoring_Organisation_State">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Sponsoring_Address" HeaderText="Sponsoring Organisation Address" 
                            SortExpression="Sponsoring_Address">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="GST_NUMBER" HeaderText="GST Number" 
                            SortExpression="GST_NUMBER">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="State_Code" HeaderText="State Code" 
                            SortExpression="State_Code">
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
            <td width="30%" colspan="2" style="width: 100%" align="center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td width="30%" colspan="2" style="width: 100%" align="center">
                <!-- Label to display error or info messages -->
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="30%" colspan="2" style="width: 100%" align="center">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
