<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PayeeOrganizationDetails.aspx.cs" Inherits="UserDetails_PayeeOrganizationDetails" Title="BCAS-->Payee Organization Details" %>

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
                    Text="Payee Organization Details"></asp:Label>
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
                <!-- GridView for Payee Organization Details -->
                <asp:GridView ID="grdPayeeOrganizationDetails" runat="server" AutoGenerateColumns="False" 
                    EmptyDataText="No Record Found" BorderColor="#000066" BorderStyle="Outset" 
                    BorderWidth="2px" Caption="Payee Organization Details" 
                    CaptionAlign="Top" ForeColor="#333333" AllowPaging="True" 
                    AllowSorting="False" PageSize="20" DataKeyNames="Payee_Organisation_Code"
                    OnPageIndexChanging="grdPayeeOrganizationDetails_PageIndexChanging"
                    OnRowEditing="grdPayeeOrganizationDetails_RowEditing"
                    OnRowUpdating="grdPayeeOrganizationDetails_RowUpdating"
                    OnRowCancelingEdit="grdPayeeOrganizationDetails_RowCancelingEdit">
                    <Columns>
                        <asp:CommandField ShowEditButton="True" HeaderText="Edit" ButtonType="Link">
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:CommandField>
                        <asp:BoundField DataField="Payee_Organisation_Code" HeaderText="Payee Organisation Code" 
                            ReadOnly="True" SortExpression="Payee_Organisation_Code">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Payee_Organisation_Name" HeaderText="Payee Organisation Name" 
                            SortExpression="Payee_Organisation_Name">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Payee_Organisation_State" HeaderText="Payee Organisation State" 
                            SortExpression="Payee_Organisation_State">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Payee_Address" HeaderText="Payee Organisation Address" 
                            SortExpression="Payee_Address">      
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Location_Pincode" HeaderText="Location Pincode" 
                            SortExpression="Location_Pincode">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="GSTIN" HeaderText="GST Number" 
                            SortExpression="GSTIN">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SCODE" HeaderText="State Code" 
                            SortExpression="SCODE">
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
