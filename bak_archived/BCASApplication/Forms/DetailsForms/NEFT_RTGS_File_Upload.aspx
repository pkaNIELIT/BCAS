<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="NEFT_RTGS_File_Upload.aspx.cs" Inherits="Forms_DetailsForms_NEFT_RTGS_File_Upload" Title="BCAS->NEFT-RTGS" Debug="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table width="100%" align="center" >
    <tr>
        <td colspan="2" align="center"  >
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="2" align="center"  >
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="2" align="center"  >
            <asp:Label ID="lblHeading" runat="server" Font-Bold="True" ForeColor="#990000" 
                Text="NEFT/RTGS FILE UPLOAD"></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="30%" >
            </td>
        <td width="70%" >
            </td>
    </tr>
    <tr align="center" >
        <td width="30%" colspan="2" style="width: 100%"  >            
            <table width="65%" align="center" border="2"  >
                <tr align="left">
                    <td width="30%">
            <asp:Label ID="lblCourseSchedule" runat="server" Text="Choose File Name"></asp:Label>
                    </td>
                    <td width="70%">
            <asp:FileUpload ID="FileUploadCsv" runat="server" 
                meta:resourcekey="FileUpload1Resource1" Height="22px"  />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="FileUploadCsv" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                 <tr align="left">
                    <td width="30%" colspan="2" style="width: 100%; text-align: center">
                        <asp:Label ID="lblError" runat="server" Font-Size="Large" ForeColor="#990033" 
                            Visible="False"></asp:Label>
                     </td>
                </tr>
            </table>
            
            </td>
    </tr>
    <tr>
        <td width="30%" colspan="2" style="width: 100%" align="center"  >            
            <asp:Button ID="btnSave" runat="server" meta:resourcekey="btnSaveResource1" 
                Text="Port Data" onclick="btnSave_Click" Visible="False" />
            <asp:Button ID="btnUpload" runat="server" onclick="btnUpload_Click" 
                Text="Upload CSV File" />
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
    <tr id="DuplicateRecord" runat="server" align="center"  >
        <td width="30%" colspan="2" style="width: 100%" align="center"  >
               
                    <asp:GridView ID="grdDuplicateRecords" runat="server" 
                AutoGenerateColumns="true " Width="600px" 
                        Caption="Following UTR Nos. already exists (No Records Uploaded)">
                        <HeaderStyle BackColor="Silver" BorderColor="#990033" BorderStyle="Solid" 
                            BorderWidth="2px" />
                    </asp:GridView>
                   
                     </td>
    </tr>
    <tr id="PortedRecord" runat="server" >
        <td width="30%" colspan="2" style="width: 100%" align="center"  >            
            <asp:Panel ID="Panel1" runat="server" style="text-align: center" 
                    ScrollBars="Vertical" Width="70%" Height="200px">               
                    <asp:GridView ID="grdDisplay" runat="server" AutoGenerateColumns="False" 
                        DataSourceID="SqlDataSource1" Width="100%" 
                        Caption="Records Uploaded Successfully">
                        <Columns>
                            <asp:BoundField DataField="Serial_No" HeaderText="Serial_No" 
                                SortExpression="Serial_No" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UTR_Number" HeaderText="UTR_Number" 
                                SortExpression="UTR_Number" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Sender_IFSC" HeaderText="Sender_IFSC" 
                                SortExpression="Sender_IFSC" >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Sender_Name" HeaderText="Sender_Name" 
                                SortExpression="Sender_Name" >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Transaction_date" HeaderText="Transaction_date" 
                                SortExpression="Transaction_date" >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Amount" HeaderText="Amount" 
                                SortExpression="Amount" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle BackColor="Silver" BorderColor="#990033" BorderStyle="Solid" 
                            BorderWidth="2px" />
                    </asp:GridView>
                     </asp:Panel>
            </td>
    </tr>
    <tr>
        <td width="30%" colspan="2" style="width: 100%" align="center"  >            
            &nbsp;</td>
    </tr>
    <tr>
        <td width="30%" colspan="2" style="width: 100%" align="center"  >
          
           
             <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" 
                    
                 SelectCommand="SELECT [Serial_No], [UTR_Number], [Sender_IFSC], [Sender_Name], convert(varchar(10),[Transaction_date],103) Transaction_date, [Amount] FROM [Rtgs_Neft_File]
">
                </asp:SqlDataSource>
            </td>
    </tr>
    </table>


</asp:Content>

