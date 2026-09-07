<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ScreenersApplicantDetails_Preview.aspx.cs" Inherits="Forms_DetailsForms_ScreenersApplicantDetails_Preview" Culture="en-GB" Title="BCAS-->Screeners PreviewForm" %>
<%@ PreviousPageType VirtualPath="~/Forms/DetailsForms/ScreenersApplicantDetails.aspx" %> 


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">   
   
    <style type="text/css">
        .auto-style1 {
            height: 32px;
        }
        .auto-style2 {
            height: 24px;
        }
    </style>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <table border="0" align="center" width="100%" height="100%">
        <tr align="center">
            <td valign="top" align="center">
                <table border="0" align="center" width="90%" height="100%">
                    <tr align="center">
                        <td>
                            <table border="0" cellpadding="0" cellspacing="5" width="100%" bordercolor="#ECC4B9"
                                bgcolor="#FBEBE6" align="center">
                                <tr>
                                    <td colspan="2" align="Right">
                                        <b>Step 1 </b>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <strong><font color="#990000">Online Application For Nomination Screeners</font></strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <hr color="#990000" size="1">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2" bgcolor="#cccccc">
                                        Organisation
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        &nbsp;</td>
                                    <td align="left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="Label1" runat="server" >Sponsoring Organisation Name</asp:Label>
                                        <font color="red">*</font>:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddl_OrgCode" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSource3"
                                            DataTextField="Organisation_Name" DataValueField="Organisation_Code" TabIndex="1"
                                            Width="300px" BackColor="#FBEBE6" Enabled="False" >
                                            <asp:ListItem >Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>"
                                            SelectCommand="SELECT [Organisation_Code], [Organisation_Name] FROM [Sponsoring_Organisation_Master]">
                                        </asp:SqlDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="auto-style2">
                                        </td>
                                    <td align="left" class="auto-style2">
                                        </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lbl_BOrg" runat="server" >Payee Organisation Name</asp:Label>
                                        <font color="red">*</font>:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlBillingOrganisation" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSource11"
                                            DataTextField="Payee_Organisation_Name" DataValueField="Payee_Organisation_Code" TabIndex="1"
                                            Width="300px" BackColor="#FBEBE6" Enabled="False" >
                                            <asp:ListItem >Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource11" runat="server" ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>"
                                            SelectCommand="SELECT [Payee_Organisation_Code], [Payee_Organisation_Name] FROM [Payee_Organisation_Master]">
                                        </asp:SqlDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        &nbsp;</td>
                                    <td align="left">
                                        &nbsp;</td>
                                </tr>

                                
                               
                                <tr>
                                    <td align="left" colspan="2" bgcolor="#cccccc">
                                        Personal Details
                                    </td>
                                </tr>


                                 <tr align="left">
                                    <td width="30%">
                                        <asp:Label ID="lblCourseApplied" runat="server" Text="Course Applied"></asp:Label>
                                    </td>
                                    <td width="70%">
                                        <asp:DropDownList ID="ddl_Course" runat="server" DataTextField="Course_Name" 
                                        DataValueField="Course_Code" Width="535px" 
                                         AppendDataBoundItems="True" Enabled="False">
                                            <asp:ListItem Value="0">-Select Course--</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        &nbsp;</td>
                                    <td align="left">
                                        &nbsp;</td>
                                </tr>
                                 <tr align="left">
                                    <td width="30%">
                                        <asp:Label ID="Label2" runat="server" Text="Registration No."></asp:Label>
                                    </td>
                                    <td width="70%">
                                       <asp:Label ID="lblRegnNo" runat ="server" ></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        &nbsp;</td>
                                    <td align="left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lbl_EmpID" runat="server" >Pass ID</asp:Label>
                                        <font color="red">*</font>:
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lbl_prevEmpID" runat="server" Width="230px" Enabled="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="auto-style1">
                                        </td>
                                    <td align="left" class="auto-style1">
                                        </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lbl_Designation" runat="server" >Designation</asp:Label>
                                        <font color="red">*</font> :</td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddl_Designation" runat="server" AppendDataBoundItems="True"
                                            DataSourceID="SqlDataDesignation" DataTextField="Description" DataValueField="Designation_Code"
                                            TabIndex="1" Width="300px" BackColor="#FBEBE6" Enabled="False" >
                                            <asp:ListItem >Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataDesignation" runat="server" ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>"
                                            SelectCommand="SELECT [Designation_Code], [Description] FROM [Designation_Master]">
                                        </asp:SqlDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        &nbsp;</td>
                                    <td align="left">
                                        &nbsp;</td>
                                </tr>
                                 <tr>
                                    <td align="left">
                                         <asp:Label ID="lbl_Postedat" runat="server" >Posted at State</asp:Label>
                                        <font color="red">*</font> :</td>
                                    <td align="left">
                                     <asp:UpdatePanel ID="UpdatePanel2" runat="server"> 
                                    <ContentTemplate>
                                             <asp:DropDownList ID="drpStates" runat="server" DataSourceID="SqlDataSourceState" 
                                            DataTextField="Name" DataValueField="ID" AppendDataBoundItems="true" TabIndex="1" 
                                            Width="300px" Enabled="false" >
                                           <asp:ListItem Text="Select" Value="0" />                                        
                                        </asp:DropDownList>
                                       
                                        
                                            
                                        <asp:SqlDataSource ID="SqlDataSourceState" runat="server" ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>"
                                            SelectCommand="  Select ID , Name  FROM Location where Parent_ID=1">                                            
                                        </asp:SqlDataSource>
                                        </ContentTemplate></asp:UpdatePanel>
                                        
                                        </td>
                                </tr>
                                  <tr>
                                    <td align="left">
                                        &nbsp;</td>
                                    <td align="left">
                                        &nbsp;</td>
                                </tr>
                                
                                 <tr>
                                    <td align="left">
                                         <asp:Label ID="Label3" runat="server" >Posted at City</asp:Label>
                                        <font color="red">*</font> :</td>
                                    <td align="left">
                                      <asp:UpdatePanel ID="UpdatePanel1" runat="server"> 
                                    <ContentTemplate>
                                             <asp:DropDownList ID="drpCity" runat="server" 
                                            DataTextField="Name" DataValueField="ID" AppendDataBoundItems="true" TabIndex="1" DataSourceID="SqlDataSource2" 
                                            Width="300px" Enabled="false" 
                                                  >
                                           <asp:ListItem Text="Select" Value="0" />                                        
                                        </asp:DropDownList>
                                      <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>"
                                            SelectCommand="  Select ID , Name  FROM Location where (Parent_ID <40 or parent_id=4256) and ID >40">                                            
                                        </asp:SqlDataSource>
                                       </ContentTemplate></asp:UpdatePanel>
                                      
                                       </td>
                                            
                                       
                                </tr>
                                  <tr>
                                    <td align="left">
                                        &nbsp;</td>
                                    <td align="left">
                                        &nbsp;</td>
                                </tr>
                                 <tr>
                                    <td align="left">
                                        <asp:Label ID="Label4" runat="server" >EBCAS ID</asp:Label>
                                        <font color="red">*</font>:
                                    </td>
                                    <td align="left">
                                        
                                            <asp:Label ID="lbl_prevEbasId" runat="server" Width="230px" Enabled="False"></asp:Label>
                                                      
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        &nbsp;</td>
                                    <td align="left">
                                        &nbsp;</td>
                                </tr>
                                 <tr>
                                    <td align="left">
                                        <asp:Label ID="Label5" runat="server" >BCAS ID</asp:Label>
                                        <font color="red">*</font>:
                                    </td>
                                    <td align="left">
                                     
                                            <asp:Label ID="lbl_prevBcasId" runat="server" Width="230px" Enabled="False"></asp:Label>
                                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        &nbsp;</td>
                                    <td align="left">
                                        &nbsp;</td>
                                </tr>

                                <tr>
                                    <td align="left">
                                        &nbsp;</td>
                                    <td align="left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lbl_candidate_name" runat="server" >Candidate Name</asp:Label><font
                                            color="red">*</font> :
                                    </td>
                                    <td align="left">
                                        <filteredtextboxextender id="fte_candidate_name" runat="server" filtermode="ValidChars"
                                            enabled="True" targetcontrolid="txt_candidate_name" validchars="ABCDEFGHIJKLMNOPQRSTUVWXYZ abcdefghijklmnopqrstuvwxyz">
                                                       </filteredtextboxextender>
                                        <asp:Label ID="lbl_prevCandidateName" runat="server" Width="230px" 
                                            Enabled="False"></asp:Label>
                                            
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="left">
                                        &nbsp;</td>
                                    <td align="left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lbl_organisation_name_of_candidate" runat="server" >Organisation Name of Candidate</asp:Label><font
                                            color="red">*</font> :
                                    </td>
                                    <td align="left">
                                        <filteredtextboxextender id="fte_organisation_name_of_candidate" runat="server" filtermode="ValidChars"
                                            enabled="True" targetcontrolid="txt_organisation_name_of_candidate" validchars="ABCDEFGHIJKLMNOPQRSTUVWXYZ abcdefghijklmnopqrstuvwxyz">
                                                        </filteredtextboxextender>
                                        <asp:Label ID="lbl_prevOrganisationNameofCandidate" runat="server" Width="400px" 
                                            Enabled="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        &nbsp;</td>
                                    <td align="left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lbl_AadhaarNumber" runat="server" >Aadhaar Number</asp:Label><font
                                            color="red"></font> :
                                    </td>
                                    <td align="left">
                                        <filteredtextboxextender id="fte_prevAadhaarNumber" runat="server" filtermode="ValidChars"
                                            enabled="True" targetcontrolid="txt_Aadhaar_Number" validchars="0123456789">
                                                        </filteredtextboxextender>
                                        <asp:Label ID="lbl_prevAadhaarNumber" runat="server" Width="150px" 
                                            Enabled="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lbl_gender_code" runat="server" >Gender</asp:Label>
                                        <font color="red">*</font> :
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddl_gender_code" runat="server" TabIndex="7" 
                                            BackColor="#FBEBE6" Enabled="False" 
                                            >
                                            <asp:ListItem Value="X" >Select Gender </asp:ListItem>
                                            <asp:ListItem Value="M" >Male</asp:ListItem>
                                            <asp:ListItem Value="F" >Female</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        &nbsp;</td>
                                    <td align="left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lbl_date_of_birth" runat="server" >Date of Birth</asp:Label>
                                        <font color="red">*</font> :
                                    </td>
                                    <td align="left">
                                        <maskededitextender id="txt_date_of_birth_MaskedEditExtender" runat="server" culturename="en-GB"
                                            mask="99/99/9999" masktype="Date" targetcontrolid="txt_date_of_birth" userdateformat="DayMonthYear">
                             </maskededitextender>
                                        <asp:Label ID="lbl_prevDOB" runat="server" style="margin-right: 0px" 
                                            Width="230px" Enabled="False"></asp:Label>
                                        <maskededitvalidator id="mev_txt_date_of_birth" runat="server" controlextender="txt_date_of_birth_MaskedEditExtender"
                                            controltovalidate="txt_date_of_birth" display="None" emptyvaluemessage="Date of Birth Must Be Entered"
                                            isvalidempty="false">
                             </maskededitvalidator>
                                        <validatorcalloutextender id="mev_callout_extender" runat="server" targetcontrolid="mev_txt_date_of_birth">
                             </validatorcalloutextender>
                                        (dd/mm/yyyy)
                                        </td>
                                </tr>
                                <%-- <cc1:MaskedEditExtender ID="mee_relevant_experience" runat="server" CultureAMPMPlaceholder=""
                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                            Enabled="True" Mask="99.99" MaskType="Number" PromptCharacter="" TargetControlID="txt_relevant_experience">
                                                        </cc1:MaskedEditExtender>--%>
                                <tr>
                                    <td align="left">
                                        &nbsp;</td>
                                    <td align="left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lbl_date_of_birth0" runat="server" >Nationality</asp:Label>
                                        <font color="red">*</font> :</td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddl_Nationality" runat="server" AppendDataBoundItems="True"
                                            DataSourceID="SqlDataNationality" DataTextField="Country_Name" DataValueField="Country_Code"
                                            TabIndex="1" Width="300px" BackColor="#FBEBE6" Enabled="False" >
                                            <asp:ListItem >Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataNationality" runat="server" ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>"
                                            
                                            SelectCommand="SELECT [Country_Code], [Country_Name] FROM [Country_Master]">
                                        </asp:SqlDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        &nbsp;</td>
                                    <td align="left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lbl_mobile_no" runat="server" >Mobile No</asp:Label>
                                        <font color="red">*</font> :
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lbl_prevMobileNo" runat="server" Width="230px" Enabled="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        &nbsp;</td>
                                    <td align="left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lbl_email_id" runat="server" >Email ID</asp:Label>
                                        <font color="red">* </font>:
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lbl_prevEmail" runat="server" Width="230px" Enabled="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        &nbsp;</td>
                                    <td align="left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lbl_qualification" runat="server" >Educational Qualification</asp:Label>
                                        <font color="red">*</font> :
                                    </td>
                                    <td align="left">
                                        <filteredtextboxextender id="fte_qualification" runat="server" enabled="True" targetcontrolid="txt_qualification"
                                            validchars="ABCDEFGHIJKLMNOPQRSTUVWXYZ abcdefghijklmnopqrstuvwxyz,.-_">
                                                                                                    <asp:DropDownList ID="ddl_Qualification" runat="server" 
                                                                                                        
                                            AppendDataBoundItems="True" DataSourceID="SqlDataQualification" 
                                                                                                        
                                                            DataTextField="Description" 
                                            DataValueField="Qualification_Code" TabIndex="1" 
                                                                                                        Width="300px" 
                                            BackColor="#FBEBE6" Enabled="False" 
                                           >
                                                                                                        <asp:ListItem >Select</asp:ListItem>
                                                                                                    </asp:DropDownList>
                                                                                                    <asp:SqlDataSource ID="SqlDataQualification" runat="server" 
                                                                                                        ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" 
                                                                                                        
                                                            SelectCommand="SELECT [Qualification_Code], [Description] FROM [Qualification_Master]">
                                                                                                    </asp:SqlDataSource>
                                                        </filteredtextboxextender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        &nbsp;</td>
                                    <td align="left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2" bgcolor="#cccccc">
                                        Declaration
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2">
                                        <asp:Label ID="lbl_agreed_declaration1" runat="server" 
                                            EnableTheming="False" ForeColor="#0000CC"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2" >
                                        <asp:CheckBox ID="chk_declaration" runat="server" Text="I Agree" AutoPostBack="True"
                                            TabIndex="61" oncheckedchanged="chk_declaration_CheckedChanged" 
                                            Enabled="False" />
                                        <font color="red">&nbsp; *</font> :
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="style1">
                                        <font color="#990000" size="2">(*) Star marked fields are essentially to be filled by
                                            the candidate. </font>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2">
                                        <asp:Label ID="lbl_error" runat="server" Font-Size="Large"
                                            ForeColor="Red" ></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <confirmbuttonextender id="btn_confirm_ConfirmButtonExtender" confirmtext="Press OK if Data entered is correct otherwise press Cancel to Make Corrections !!!!! "
                                            runat="server" enabled="True" targetcontrolid="btn_confirm" onclientcancel="">
                                        </confirmbuttonextender>
                                        <asp:Button ID="btn_confirm" runat="server" 
                                            onclick="btn_confirm_Click" style="height: 26px" 
                                            Text="Submit Form and Proceed" />
                                        <asp:Button ID="btn_correction" runat="server" 
                                            
                                            style="height: 26px" Text="Cancel" Width="197px" 
                                            onclick="btn_correction_Click" />
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2">
                                        <asp:BulletedList ID="bullet_NewUser" runat="server" Width="350px" BulletStyle="Numbered"
                                            Font-Bold="True" ForeColor="#FF3300" >
                                        </asp:BulletedList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

