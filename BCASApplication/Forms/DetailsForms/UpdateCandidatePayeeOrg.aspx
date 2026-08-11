<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UpdateCandidatePayeeOrg.aspx.cs" Culture="en-GB" Inherits="Forms_DetailsForms_UpdateCandidatePayeeOrg" Title="BCAS-UpdateCandidatePayeeOrg" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" align="center">
        <tr>
            <td width="20%" colspan="2" style="width: 95%" align="center">&nbsp;</td>

        </tr>
        <tr>
            <td width="20%">&nbsp;</td>
            <td width="75%">&nbsp;</td>

        </tr>
        <tr>
            <td width="20%">&nbsp;</td>
            <td width="75%">&nbsp;</td>

        </tr>
        <tr align="center">
            <td width="20%" colspan="2" style="width: 95%">
                <table width="65%" align="center" border="2">
                    <tr align="left">
                        <td width="30%">
                            <asp:Label ID="lblCourseSchedule" runat="server" Text="Course Schedule"></asp:Label>
                        </td>
                        <td width="70%">

                            <asp:DropDownList ID="ddlSchedule" runat="server"
                                DataSourceID="SqlDataSource2" DataTextField="Course_Schedule"
                                DataValueField="Schedule_ID" Width="500px" AppendDataBoundItems="True"
                                AutoPostBack="True">
                                <asp:ListItem Value="0">-Select Course Schedule-</asp:ListItem>
                            </asp:DropDownList>
                            <%-- <asp:DropDownList ID="ddlSchedule" runat="server"
                                 Width="500px" AppendDataBoundItems="True"
                                >
                              <asp:ListItem Value="0">-Select Course Schedule-</asp:ListItem>
                                </asp:DropDownList>--%>
                                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>"
                                        SelectCommand="getCourseWithSchedule" SelectCommandType="StoredProcedure">
                                        </asp:SqlDataSource>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                ErrorMessage="*" ControlToValidate="ddlSchedule" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr align="left">
                        <td width="30%">
                            <asp:Label ID="lblSearch" runat="server" Text="Registration Number"></asp:Label>
                        </td>
                        <td width="70%">

                            <asp:TextBox ID="txtRegNo" runat="server" Width="250px" MaxLength="8" TabIndex="3"></asp:TextBox>

                            <cc1:FilteredTextBoxExtender ID="fte_RegNo_no" runat="server" Enabled="True" TargetControlID="txtRegNo"
                                ValidChars="0123456789">
                            </cc1:FilteredTextBoxExtender>

                            <asp:RequiredFieldValidator ID="rfvRegNo" runat="server"
                                ControlToValidate="txtRegNo" ErrorMessage="*"></asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr align="left">
                        <td width="30%"></td>
                        <td width="70%">

                            <asp:Button ID="btnDisplay" runat="server" Text="Submit" Width="250px"
                                OnClick="btnDisplay_Click" TabIndex="5" />

                        </td>
                    </tr>
                </table>

            </td>

        </tr>
        <tr>
            <td width="20%" colspan="2" style="width: 95%" align="center">&nbsp;</td>

        </tr>
        <tr>
            <td width="20%" colspan="2" style="width: 95%" align="center">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                    DataKeyNames="Registration_Number" DataSourceID="SqlDataSource1"
                    EmptyDataText="No Record Found" BorderColor="#000066" BorderStyle="Outset"
                    BorderWidth="2px" Caption="Candidate Personal Details"
                    CaptionAlign="Top" ForeColor="#333333" OnRowDataBound="GridView1_RowDataBound">
                    <Columns>
                       <%-- <asp:BoundField DataField="Employee_ID" HeaderText="Pass ID"
                            SortExpression="Employee_ID" />--%>
                        <asp:BoundField DataField="Registration_Number"
                            HeaderText="Registration No" InsertVisible="False" ReadOnly="True"
                            SortExpression="Registration_Number" />
                        <asp:BoundField DataField="Candidate_Name" HeaderText="Name"
                            SortExpression="Candidate_Name" />
                        <asp:BoundField DataField="Organisation_Name" HeaderText="Sponsoring Organisation"
                            SortExpression="Organisation_Name" />
                        <asp:BoundField DataField="Payee_Organisation_Name" HeaderText="Payee Organisation"
                            SortExpression="Payee_Organisation_Name" />
                          <asp:BoundField DataField="EbasId" HeaderText="EBCAS ID"
                                SortExpression="EbasId" />
                    </Columns>
                    <FooterStyle BackColor="#990000" />
                    <HeaderStyle BackColor="#990000" ForeColor="White" />
                    <AlternatingRowStyle BackColor="#FFFFCC" ForeColor="#333333" />
                </asp:GridView>
                <br />
                <asp:Button ID="GridViewBtnEdit" runat="server" CommandName="Edit" Text="Update Payee Organization" Visible="false" OnClick="GridViewBtnEdit_Click" />
            </td>

        </tr>
        <tr>
            <td width="100%" align="center">
                <div id="divBilling" runat="server">
                    <asp:Label ID="lblBillingOrganisation" runat="server">Payee Organisation Name</asp:Label>
                    <font color="red">*</font>:
                    <asp:DropDownList ID="ddl_BillerOrgCode" runat="server" DataSourceID="SqlDataSource3" AppendDataBoundItems="true"
                        DataTextField="Payee_Organisation_Name" DataValueField="Payee_Organisation_Code" TabIndex="1"
                        Width="300px" Enabled="true">
                        <asp:ListItem>Select</asp:ListItem>

                    </asp:DropDownList>
                   <%-- <asp:DropDownList ID="ddl_BillerOrgCode" runat="server" AppendDataBoundItems="true"
                        TabIndex="1"
                        Width="300px" Enabled="true">
                        <asp:ListItem>Select</asp:ListItem>
                    </asp:DropDownList>--%>
                    <asp:Button ID="btnUpdatePayee" runat="server" CommandName="Update" Text="Save" OnClick="btnUpdatePayee_Click"  />
                </div>
            </td>
            <td width="10%" colspan="2" style="width: 40%" align="center">

                   <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>"
       SelectCommand="GetPayeeOrganisation" SelectCommandType="StoredProcedure">
   </asp:SqlDataSource>

                &nbsp;

   
            </td>

        </tr>
        <tr>
            <td width="20%" colspan="2" style="width: 95%" align="center">&nbsp;</td>

        </tr>
        <tr>
            <td width="20%" colspan="2" style="width: 95%" align="center">&nbsp;</td>

        </tr>
        <tr>
            <td width="20%" colspan="2" style="width: 95%" align="center">


                <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                    ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" 
                    SelectCommand="getCandidateDetailByRegNoForPayeeChange" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtRegNo" Name="Registration_Number"
                            PropertyName="Text" />
                         <asp:ControlParameter ControlID="ddlSchedule" Name="ScheduleID" 
                            PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>

        </tr>
        <tr>
            <td width="20%">&nbsp;</td>
            <td width="75%">

                <asp:Label ID="lblError" runat="server" ForeColor="#990000" Visible="False"></asp:Label>
            </td>
        </tr>


    </table>
</asp:Content>

