<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UpdateCandidateDetails.aspx.cs" Inherits="Forms_DetailsForms_UpdateCandidateDetails" Title="BCAS-->UpdateCandidateDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style4 {
            height: 26px;
        }
    </style>

    <script language="javascript">
        function printdiv(printpage) {
            var headstr = "<html><head><title></title></head><body>";
            var footstr = "</body>";
            var newstr = document.all.item(printpage).innerHTML;
            var oldstr = document.body.innerHTML;
            document.body.innerHTML = headstr + newstr + footstr;
            window.print();
            document.body.innerHTML = oldstr;
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table border="0" align="center" width="100%">
        <tr align="center">
            <td valign="top" align="center">
                <table border="0" align="center" width="90%">
                    <tr>
                        <td height="30px">&nbsp;</td>
                    </tr>
                    <tr>
                        <td height="30px" align="center">
                            <strong><font color="#990000">Update Candidate Details</font></strong>
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
                                                    <td width="25%" align="right">&nbsp;
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Times New Roman"
                                                            Font-Size="X-Large" ForeColor="#990033" Text="Enter Registration Number"
                                                            EnableTheming="False"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr valign="middle">
                                                    <td width="25%" align="right">
                                                        <asp:Label ID="UserNameLabel" runat="server">Registration No:</asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtRegNo" runat="server" Width="220px" MaxLength="8"
                                                            AutoCompleteType="Disabled"></asp:TextBox>

                                                        <asp:RequiredFieldValidator ID="RegNoRequired" runat="server" ControlToValidate="txtRegNo"
                                                            ErrorMessage="*" ToolTip="Registration No is required." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr valign="middle">
                                                    <td width="25%">&nbsp;
                                                    </td>
                                                    <td align="left">
                                                        <asp:Button ID="btnView" runat="server" Font-Bold="False"
                                                            Style="text-align: left; font-weight: 700;" Text="View Details"
                                                            OnClick="btnView_Click" TabIndex="1" CausesValidation="False" />


                                                    </td>
                                                </tr>
                                                <tr valign="middle">
                                                    <td width="25%" colspan="2">
                                                        <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#CC0000"
                                                            Visible="False" EnableTheming="True"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="center">


                                            <asp:GridView ID="grdDisplay" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1"
                                                EmptyDataText="No Record Found" BorderColor="#000066" BorderStyle="Outset"
                                                BorderWidth="2px" Caption="Candidate Details"
                                                CaptionAlign="Top" ForeColor="#333333" PageSize="5" Width="900px"
                                                DataKeyNames="Registration_Number" Style="text-align: center">
                                                <Columns>
                                                    <asp:BoundField DataField="Registration_Number"
                                                        HeaderText="Registration_Number"
                                                        SortExpression="Registration_Number" InsertVisible="False"
                                                        ReadOnly="True" />
                                                    <asp:BoundField DataField="Candidate_Name" HeaderText="Candidate_Name"
                                                        SortExpression="Candidate_Name">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Designation" HeaderText="Designation"
                                                        SortExpression="Designation">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="state" HeaderText="State"
                                                        SortExpression="state">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="City" HeaderText="City"
                                                        SortExpression="City">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="EBasId" HeaderText="EBCAS Id"
                                                        SortExpression="EBasId">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Qualification"
                                                        HeaderText="Qualification" SortExpression="Qualification">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DOB"
                                                        HeaderText="DOB" SortExpression="DOB" ReadOnly="True" />
                                                    <asp:BoundField DataField="Mobile_No" HeaderText="Mobile_No"
                                                        SortExpression="Mobile_No" />
                                                    <asp:BoundField DataField="Email_Id" HeaderText="Email ID"
                                                        SortExpression="Email_Id" />
                                                    <asp:BoundField DataField="Organisation_Name" HeaderText="Organisation_Name"
                                                        SortExpression="Organisation_Name">
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
                                    <%--  cxvzzxvzxcvzxvzxcvzxvczcxv--%>
                                    <tr>
                                        <td align="left">&nbsp;</td>
                                        <td align="left">&nbsp;</td>
                                    </tr>
                                    <tr>

                                        <td align="center">

                                            <table border="0" id="CandidateDetail" runat="server" cellpadding="0" cellspacing="7" width="60%" bordercolor="#ECC4B9"
                                                bgcolor="#FBEBE6" visible="false" align="left">

                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="Label4" runat="server">EBCAS ID  <font color="red">*</font>:</asp:Label>

                                                        <%--  </td>
                                    <td>
                                                        --%>
                                                        <asp:TextBox ID="Txt_EbasId" runat="server" Width="230px" MaxLength="12" Enabled="true"
                                                            TabIndex="9"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfv_EbasId" runat="server" ControlToValidate="Txt_EbasId"
                                                            ErrorMessage="Please Enter EBCAS ID"
                                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        &nbsp;&nbsp;
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td align="left">&nbsp;</td>
                                                    <td align="left">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnViewCandidateDetailsScreenersFrmEBCASNo" runat="server" Enabled="true"
                                                            TabIndex="63"
                                                            Text="Validate Candidate By EBCASId" Style="height: 26px"
                                                            OnClick="btnViewCandidateDetailsScreenersFrmEBCASNo_Click" CausesValidation="false" /></td>
                                                </tr>
                                                <tr>
                                                    <td align="left">&nbsp;</td>
                                                    <td align="left">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="lbl_Postedat" runat="server">Posted at State <font color="red">*</font> :</asp:Label>


                                                        <%--   <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                         <ContentTemplate>--%>
                                                        <asp:DropDownList ID="drpStates" runat="server" DataSourceID="SqlDataSourceState" OnSelectedIndexChanged="drpStates_SelectedIndexChanged"
                                                            DataTextField="Name" DataValueField="ID" AppendDataBoundItems="true" TabIndex="7" AutoPostBack="true"
                                                            Width="300px" Enabled="true">
                                                            <asp:ListItem Text="Select" Value="0" />
                                                        </asp:DropDownList>

                                                    </td>
                                                    <td>
                                                        <asp:SqlDataSource ID="SqlDataSourceState" runat="server" ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>"
                                                            SelectCommand="  Select ID , Name  FROM Location where Parent_ID=1 order by name"></asp:SqlDataSource>
                                                        <%--    </ContentTemplate>
                                     </asp:UpdatePanel>--%>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="drpStates"
                                                            ErrorMessage="*" SetFocusOnError="True" ValidationExpression="^[1-9[0-9][0-9]*"></asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">&nbsp;</td>
                                                    <td align="left">&nbsp;</td>
                                                </tr>

                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="Label3" runat="server">Posted at City <font color="red">*</font> :</asp:Label>

                                                        <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                          <ContentTemplate>--%>
                                                        <asp:DropDownList ID="drpCity" runat="server"
                                                            DataTextField="Name" DataValueField="ID" AppendDataBoundItems="true" TabIndex="8"
                                                            Width="300px" Enabled="true">
                                                            <asp:ListItem Text="Select" Value="0" />
                                                        </asp:DropDownList>

                                                        <%--   </ContentTemplate>
                                      </asp:UpdatePanel>--%>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="drpCity"
                                                            ErrorMessage="*" SetFocusOnError="True" ValidationExpression="^[1-9[0-9][0-9]*"></asp:RegularExpressionValidator>
                                                    </td>


                                                </tr>
                                                <tr>
                                                    <td align="left">&nbsp;</td>
                                                    <td align="left">&nbsp;</td>
                                                </tr>

                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="lbl_mobile_no" runat="server">Mobile No <font color="red">*</font> :</asp:Label>


                                                        <asp:TextBox ID="txt_mobile_no" runat="server" Width="112px" MaxLength="10"
                                                            TabIndex="17" Height="22px"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="fte_mobile_no" runat="server" Enabled="True" TargetControlID="txt_mobile_no"
                                                            ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                        <asp:RequiredFieldValidator ID="rfv_mobile_no"
                                                            runat="server" ControlToValidate="txt_mobile_no" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="rev_mobile_no" runat="server" ControlToValidate="txt_mobile_no"
                                                            SetFocusOnError="true" ErrorMessage="Please Enter Valid Mobile No." ValidationExpression="^[1-9][0-9]{9}"></asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td align="left">&nbsp;</td>
                                                    <td align="left">&nbsp;</td>
                                                </tr>

                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="lbl_email_id" runat="server">Email ID</asp:Label>
                                                        <font color="red">* </font>:
                                   
                                        <asp:TextBox ID="txt_email_id" runat="server" Width="229px" MaxLength="50"
                                            TabIndex="18"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfv_email_id" runat="server" ControlToValidate="txt_email_id"
                                                            SetFocusOnError="true" ErrorMessage="Please Enter Valid Email Id"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="rev_email_id" runat="server" ControlToValidate="txt_email_id"
                                                            SetFocusOnError="true" ErrorMessage="Please Enter Valid Email Id" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                                <tr>

                                                    <td align="left">&nbsp;</td>
                                                    <td align="left">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnUpdate" runat="server"
                                                            TabIndex="7"
                                                            Text="Update Details"
                                                            OnClick="btnUpdate_Click" Style="font-weight: 700" Visible="false" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnNewReg" runat="server"
                                                            TabIndex="7"
                                                            Text="View Another Registration" CausesValidation="false"
                                                            OnClick="btnNewReg_Click" Style="font-weight: 700" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lbl_Success" runat="server" Font-Bold="False"
                                                            Font-Names="Times New Roman" Font-Size="Large" ForeColor="#000099"></asp:Label>

                                                        <asp:Label ID="lbl_ErrorMsg" runat="server" ForeColor="Red" Font-Size="Large"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>

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
                convert(varchar(10),Candidate_Registration.DOB,103) DOB,
                Candidate_Registration .Email_Id ,
                Candidate_Registration.Mobile_No,
                Sponsoring_Organisation_Master.Organisation_Name,
                (select distinct name from location where name=isnull(Candidate_Registration .PostedAtStateName ,0)) 'state',
                (select distinct name from location where name=isnull(Candidate_Registration .PostedAtCityName  ,0)) 'City',
                EbasId ,
                Is_Application_Complete 
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

