<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" Culture="en-GB" UICulture="auto" 
CodeFile="Relieved_Candidates_Updation.aspx.cs" Inherits="Relieved_Candidates_Updation" Title="Candidate_Registration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" align="center" bgcolor="#FBEBE6">
        <tr>
            <td style="height: 21px" align="center">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="height: 21px" align="center">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="height: 21px" align="center">
                <asp:Label ID="lbl_Heading" runat="server" meta:resourcekey="lbl_Heading" 
                    EnableTheming="True" style="font-weight: 700"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 21px" align="center">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="height: 35%" align="center">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table style="height: 34%">
                            <tr>
                                <td style="height: 70%">
                                    <asp:Panel ID="frm_panel" runat="server" Enabled="false">
                                        <table style="height: 70%; width: auto;" border="1">
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_organisation_code" runat="server" meta:resourcekey="lbl_organisation_code"></asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                <asp:DropDownList ID="ddl_OrgCode" runat="server" DataSourceID="SqlDataSource1"
                                            DataTextField="Organisation_Name" DataValueField="Organisation_Code" TabIndex="1"
                                            Width="250px" Enabled="False" >
                                            <asp:ListItem Value="0" >--Select--</asp:ListItem>
                                        </asp:DropDownList>
                                                    <asp:RegularExpressionValidator ID="rev_OrgCode" runat="server" ControlToValidate="ddl_OrgCode"
                                            ErrorMessage="*" SetFocusOnError="True" ValidationExpression="^[1-9[0-9][0-9]*" ></asp:RegularExpressionValidator>
                                                    <%--<asp:TextBox ID="txt_organisation_code" runat="server" Width="336px" MaxLength="10"></asp:TextBox>
                                                    <cc1:MaskedEditExtender ID="mee_organisation_code" runat="server" TargetControlID="txt_organisation_code"
                                                          Mask="9999999999" PromptCharacter="">
                                                    </cc1:MaskedEditExtender>
                                                    <cc1:MaskedEditValidator ID="mev_organisation_code" runat="server" ControlExtender="mee_organisation_code"
                                                          ControlToValidate="txt_organisation_code" EmptyValueMessage="Organisation Code must be entered"
                                                          IsValidEmpty="false" Display="None"></cc1:MaskedEditValidator>
                                                    <cc1:ValidatorCalloutExtender ID="vce_organisation_code" runat="server" TargetControlID="mev_organisation_code">
                                                    </cc1:ValidatorCalloutExtender>--%>
                                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                                        ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" 
                                                        
                                                        SelectCommand="SELECT [Organisation_Code], [Organisation_Name] FROM [Sponsoring_Organisation_Master] WHERE [Organisation_Code]=@OrgCode">
                                                        <SelectParameters>
                                                            <asp:SessionParameter Name="OrgCode" SessionField="UserOrg" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>
                                                </td>
                                            </tr>
                                            
                                            <tr> 
                                            <td style="text-align: left">Previous Organisation</td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddl_NewOrgCode" runat="server" 
                                                    DataSourceID="SqlDataSource2" AppendDataBoundItems="True" 
                                                    DataTextField="Organisation_Name" DataValueField="Organisation_Code" 
                                                    Enabled="False" Width="250px">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                                                    ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" 
                                                    SelectCommand="SELECT [Organisation_Code], [Organisation_Name] FROM [Sponsoring_Organisation_Master]">
                                                </asp:SqlDataSource>
                                                </td></tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_employee_id" runat="server" meta:resourcekey="lbl_employee_id"></asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:TextBox ID="txt_employee_id" runat="server" Width="336px" MaxLength="12"></asp:TextBox>
                                                    <cc1:MaskedEditExtender ID="mee_employee_id" runat="server" TargetControlID="txt_employee_id"
                                                          Mask="A{12}" Filtered="[0123456789+.,&-/  ]" PromptCharacter="">
                                                    </cc1:MaskedEditExtender>
                                                    <cc1:MaskedEditValidator ID="mev_employee_id" runat="server" ControlExtender="mee_employee_id"
                                                          ControlToValidate="txt_employee_id" EmptyValueMessage="Employee ID must be entered"
                                                          IsValidEmpty="false" Display="None"></cc1:MaskedEditValidator>
                                                    <cc1:ValidatorCalloutExtender ID="vce_employee_id" runat="server" TargetControlID="mev_employee_id">
                                                    </cc1:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_candidate_name" runat="server" meta:resourcekey="lbl_candidate_name"></asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:TextBox ID="txt_candidate_name" runat="server" Width="336px" MaxLength="100"></asp:TextBox>
                                                    <cc1:MaskedEditExtender ID="mee_candidate_name" runat="server" TargetControlID="txt_candidate_name"
                                                          Mask="LA{99}" Filtered="[0123456789+.,&-/  ]" PromptCharacter="">
                                                    </cc1:MaskedEditExtender>
                                                    <cc1:MaskedEditValidator ID="mev_candidate_name" runat="server" ControlExtender="mee_candidate_name"
                                                          ControlToValidate="txt_candidate_name" EmptyValueMessage="Candidate Name must be entered"
                                                          IsValidEmpty="false" Display="None"></cc1:MaskedEditValidator>
                                                    <cc1:ValidatorCalloutExtender ID="vce_candidate_name" runat="server" TargetControlID="mev_candidate_name">
                                                    </cc1:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_sex_code" runat="server" meta:resourcekey="lbl_sex_code"></asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                 
                                                    <asp:DropDownList ID="ddl_gender_code" runat="server" TabIndex="9" >
                                            <asp:ListItem Value="X" >Select Gender </asp:ListItem>
                                            <asp:ListItem Value="M" >Male</asp:ListItem>
                                            <asp:ListItem Value="F" >Female</asp:ListItem>
                                        </asp:DropDownList>
                                        
                                        <asp:RegularExpressionValidator ID="rev_gender_code" runat="server" ControlToValidate="ddl_gender_code"
                                            SetFocusOnError="True" ErrorMessage="Please Select Gender Code" 
                                            ValidationExpression="M|F" ></asp:RegularExpressionValidator>
                                                    <%--<asp:TextBox ID="txt_sex_code" runat="server" Width="336px" MaxLength="1"></asp:TextBox>
                                                    <cc1:MaskedEditExtender ID="mee_sex_code" runat="server" TargetControlID="txt_sex_code"
                                                          Mask="LA{0}" Filtered="[0123456789+.,&-/  ]" PromptCharacter="">
                                                    </cc1:MaskedEditExtender>
                                                    <cc1:MaskedEditValidator ID="mev_sex_code" runat="server" ControlExtender="mee_sex_code"
                                                          ControlToValidate="txt_sex_code" EmptyValueMessage="Sex Code must be entered"
                                                          IsValidEmpty="false" Display="None"></cc1:MaskedEditValidator>
                                                    <cc1:ValidatorCalloutExtender ID="vce_sex_code" runat="server" TargetControlID="mev_sex_code">
                                                    </cc1:ValidatorCalloutExtender>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_designation_code" runat="server" meta:resourcekey="lbl_designation_code"></asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <%--<asp:TextBox ID="txt_designation_code" runat="server" Width="336px" MaxLength="10"></asp:TextBox>
                                                    <cc1:MaskedEditExtender ID="mee_designation_code" runat="server" TargetControlID="txt_designation_code"
                                                          Mask="9999999999" PromptCharacter="">
                                                    </cc1:MaskedEditExtender>
                                                    <cc1:MaskedEditValidator ID="mev_designation_code" runat="server" ControlExtender="mee_designation_code"
                                                          ControlToValidate="txt_designation_code" EmptyValueMessage="Designation Code must be entered"
                                                          IsValidEmpty="false" Display="None"></cc1:MaskedEditValidator>
                                                    <cc1:ValidatorCalloutExtender ID="vce_designation_code" runat="server" TargetControlID="mev_designation_code">
                                                    </cc1:ValidatorCalloutExtender>--%><asp:DropDownList ID="ddl_Designation" runat="server" AppendDataBoundItems="True"
                                            DataSourceID="SqlDataDesignation" DataTextField="Description" DataValueField="Designation_Code"
                                            TabIndex="5" Width="250px" >
                                            <asp:ListItem >Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="ddl_Designation"
                                            ErrorMessage="*" SetFocusOnError="True" ValidationExpression="^[1-9[0-9][0-9]*" ></asp:RegularExpressionValidator>
                                        <asp:SqlDataSource ID="SqlDataDesignation" runat="server" ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>"
                                            SelectCommand="SELECT [Designation_Code], [Description] FROM [Designation_Master]">
                                        </asp:SqlDataSource>
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_dob" runat="server" meta:resourcekey="lbl_dob"></asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:TextBox ID="txt_dob" runat="server" Width="336px" MaxLength="10"></asp:TextBox>
                                                    <cc1:MaskedEditExtender ID="mee_dob" runat="server" TargetControlID="txt_dob"
                                                          Mask="99/99/9999" MaskType = "Date" UserDateFormat="DayMonthYear" CultureName="en-GB">
                                                    </cc1:MaskedEditExtender>
                                                    <cc1:MaskedEditValidator ID="mev_dob" runat="server" ControlExtender="mee_dob"
                                                          ControlToValidate="txt_dob" EmptyValueMessage="DOB must be entered"
                                                          IsValidEmpty="false" Display="None"></cc1:MaskedEditValidator>
                                                    <cc1:ValidatorCalloutExtender ID="vce_dob" runat="server" TargetControlID="mev_dob">
                                                    </cc1:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_email_id" runat="server" meta:resourcekey="lbl_email_id"></asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:TextBox ID="txt_email_id" runat="server" Width="336px" MaxLength="50"></asp:TextBox>
                                                    <cc1:MaskedEditExtender ID="mee_email_id" runat="server" TargetControlID="txt_email_id"
                                                          Mask="LA{49}" Filtered="[0123456789+.,&-/@]" PromptCharacter="">
                                                    </cc1:MaskedEditExtender>
                                                    <cc1:MaskedEditValidator ID="mev_email_id" runat="server" ControlExtender="mee_email_id"
                                                          ControlToValidate="txt_email_id" EmptyValueMessage="Email Id must be entered"
                                                          IsValidEmpty="false" Display="None"></cc1:MaskedEditValidator>
                                                    <cc1:ValidatorCalloutExtender ID="vce_email_id" runat="server" TargetControlID="mev_email_id">
                                                    </cc1:ValidatorCalloutExtender>
                                                    
                                                    <asp:RequiredFieldValidator ID="rfv_email_id" runat="server" ControlToValidate="txt_email_id"
                                                             SetFocusOnError="true"  ErrorMessage="Please Enter Valid Email Id"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="rev_email_id" runat="server" ControlToValidate="txt_email_id"
                                                             SetFocusOnError="true"  ErrorMessage="Please Enter Valid Email Id" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_mobile_no" runat="server" meta:resourcekey="lbl_mobile_no"></asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:TextBox ID="txt_mobile_no" runat="server" Width="336px" MaxLength="10"></asp:TextBox>
                                                    <cc1:MaskedEditExtender ID="mee_mobile_no" runat="server" TargetControlID="txt_mobile_no"
                                                          Mask="9999999999" Filtered="[0123456789+.,&-/  ]" PromptCharacter="">
                                                    </cc1:MaskedEditExtender>
                                                    <cc1:MaskedEditValidator ID="mev_mobile_no" runat="server" ControlExtender="mee_mobile_no"
                                                          ControlToValidate="txt_mobile_no" EmptyValueMessage="Mobile No must be entered"
                                                          IsValidEmpty="false" Display="None"></cc1:MaskedEditValidator>
                                                    <cc1:ValidatorCalloutExtender ID="vce_mobile_no" runat="server" TargetControlID="mev_mobile_no">
                                                    </cc1:ValidatorCalloutExtender>
                                                    
                                                    <asp:RegularExpressionValidator ID="rev_mobile_no" runat="server" ControlToValidate="txt_mobile_no"
                                                             SetFocusOnError="true"  ErrorMessage="Please Enter Valid Mobile No." ValidationExpression="^[1-9][0-9]{9}"></asp:RegularExpressionValidator>
                                                    
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_nationality_code" runat="server" meta:resourcekey="lbl_nationality_code"></asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                <asp:DropDownList ID="ddl_Nationality" runat="server" AppendDataBoundItems="True"
                                            DataSourceID="SqlDataNationality" DataTextField="Country_Name" DataValueField="Country_Code"
                                            TabIndex="12" Width="250px" >
                                            <asp:ListItem >Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="ddl_Nationality"
                                            ErrorMessage="*" SetFocusOnError="True" ValidationExpression="^[1-9[0-9][0-9]*"></asp:RegularExpressionValidator>
                                        <asp:SqlDataSource ID="SqlDataNationality" runat="server" ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>"
                                            
                                            SelectCommand="SELECT [Country_Code], [Country_Name] FROM [Country_Master]">
                                        </asp:SqlDataSource>
                                                    <%--<asp:TextBox ID="txt_nationality_code" runat="server" Width="336px" MaxLength="10"></asp:TextBox>
                                                    <cc1:MaskedEditExtender ID="mee_nationality_code" runat="server" TargetControlID="txt_nationality_code"
                                                          Mask="9999999999" PromptCharacter="">
                                                    </cc1:MaskedEditExtender>
                                                    <cc1:MaskedEditValidator ID="mev_nationality_code" runat="server" ControlExtender="mee_nationality_code"
                                                          ControlToValidate="txt_nationality_code" EmptyValueMessage="Nationality Code must be entered"
                                                          IsValidEmpty="false" Display="None"></cc1:MaskedEditValidator>
                                                    <cc1:ValidatorCalloutExtender ID="vce_nationality_code" runat="server" TargetControlID="mev_nationality_code">
                                                    </cc1:ValidatorCalloutExtender>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_educational_qualification_code" runat="server" meta:resourcekey="lbl_educational_qualification_code"></asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <%--<asp:TextBox ID="txt_educational_qualification_code" runat="server" Width="336px" MaxLength="10"></asp:TextBox>
                                                    <cc1:MaskedEditExtender ID="mee_educational_qualification_code" runat="server" TargetControlID="txt_educational_qualification_code"
                                                          Mask="9999999999" PromptCharacter="">
                                                    </cc1:MaskedEditExtender>
                                                    <cc1:MaskedEditValidator ID="mev_educational_qualification_code" runat="server" ControlExtender="mee_educational_qualification_code"
                                                          ControlToValidate="txt_educational_qualification_code" EmptyValueMessage="Educational Qualification Code must be entered"
                                                          IsValidEmpty="false" Display="None"></cc1:MaskedEditValidator>
                                                    <cc1:ValidatorCalloutExtender ID="vce_educational_qualification_code" runat="server" TargetControlID="mev_educational_qualification_code">
                                                    </cc1:ValidatorCalloutExtender>--%>
                                                    
                                                    <asp:DropDownList ID="ddl_Qualification" runat="server" AppendDataBoundItems="True"
                                            DataSourceID="SqlDataQualification" DataTextField="Description" DataValueField="Qualification_Code"
                                            TabIndex="19" Width="250px">
                                            <asp:ListItem>Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="ddl_Qualification"
                                            ErrorMessage="*" SetFocusOnError="True" ValidationExpression="^[1-9[0-9][0-9]*"></asp:RegularExpressionValidator>
                                        
                                        <asp:SqlDataSource ID="SqlDataQualification" runat="server" ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>"
                                            SelectCommand="SELECT [Qualification_Code], [Description] FROM [Qualification_Master]">
                                        </asp:SqlDataSource> 
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_registration_number" runat="server" meta:resourcekey="lbl_registration_number"></asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:TextBox ID="txt_registration_number" runat="server" Width="336px" MaxLength="10" Enabled="False" ></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 20px;" align="center">
                                    <table>
                                        <tr>
                                            <td style="height: 26px">
                                                <asp:Button ID="btn_insert" runat="server" EnableTheming="False" meta:resourcekey="btn_insert"
                                                    OnClick="btn_insert_Click" CausesValidation="false" AccessKey="I" 
                                                    Visible="False" />
                                            </td>
                                            <td style="height: 26px">
                                                <asp:Button ID="btn_update" runat="server" EnableTheming="False" meta:resourcekey="btn_update"
                                                    OnClick="btn_update_Click" CausesValidation="false" Enabled="false" AccessKey="U" />
                                            </td>
                                            <td style="height: 26px">
                                                <asp:Button ID="btn_search" runat="server" EnableTheming="False" meta:resourcekey="btn_search"
                                                    CausesValidation="false" Enabled="false" OnClick="btn_search_Click" AccessKey="S" />
                                            </td>
                                            <td style="height: 26px">
                                                <asp:Button ID="btn_save" runat="server" EnableTheming="False" meta:resourcekey="btn_save"
                                                    OnClick="btn_save_Click" CausesValidation="true" AccessKey="V" />
                                            </td>
                                            <td style="height: 26px">
                                                <asp:Button ID="btn_quit" runat="server" EnableTheming="False" meta:resourcekey="btn_quit"
                                                    OnClick="btn_quit_Click" CausesValidation="false" AccessKey="Q" 
                                                    Visible="False" />
                                            </td>
                                            <td style="height: 26px">
                                                <asp:Button ID="btn_cancel" runat="server" EnableTheming="False" meta:resourcekey="btn_cancel"
                                                    OnClick="btn_cancel_Click" CausesValidation="False" AccessKey="C" />
                                            </td>
                                            <td style="height: 26px">
                                                <asp:Button ID="btn_display" runat="server" EnableTheming="False" meta:resourcekey="btn_display"
                                                    OnClick="btn_display_Click" CausesValidation="False" AccessKey="D" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_choice" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lbl_error" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table>
                                        <thead>
                                            <asp:Label ID="lbl_errors" runat="server" Font-Bold="true" meta:resourcekey="lbl_errors"
                                                Visible="false"></asp:Label>
                                        </thead>
                                        <tr>
                                            <td style="height: 28px; width: auto;">
                                                <asp:BulletedList ID="bullet_errors" runat="server" BulletStyle="Numbered">
                                                </asp:BulletedList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr align="center" >
            <td align="center">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table style="width: auto; height: 96px" align="center">
                            <tr align="center">
                                <td>
                                    <asp:Panel ID="grd_panel" runat="server" BorderColor="#DEBA84" BorderWidth="1px">
                                    <div id=div1 style="overflow:scroll;height:250px;width: 900px" >
                                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" BackColor="#DEBA84"
                                            BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2"
                                            DataSourceID="ObjectDataSource1" PageSize="5" Height="50%" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                            OnRowDataBound="GridView1_RowDataBound">
                                            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" HorizontalAlign="Center" Wrap="False" />
                                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                            <Columns>
                                                <asp:CommandField ShowSelectButton="True" />
                                            </Columns>
                                            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                                        </asp:GridView>
                                        </div>
                                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetCandidate_Registration_Relieved"
                                            TypeName="BCASClassLibrary.TYP_CANDIDATE_REGISTRATION">
                                            <SelectParameters>
                                                <asp:Parameter Name="pOrganisation_Code" Type="Int32" DefaultValue="-1" />
                                                <asp:Parameter Name="pEmployee_ID" Type="String" DefaultValue="*" />
                                                <asp:Parameter Name="pCandidate_Name" Type="String" DefaultValue="*" />
                                                <asp:Parameter Name="pSex_Code" Type="String" DefaultValue="*" />
                                                <asp:Parameter Name="pDesignation_Code" Type="Int32" DefaultValue="-1" />
                                                <asp:Parameter Name="pDOB" Type="String" DefaultValue="01/01/1900" />
                                                <asp:Parameter Name="pEmail_Id" Type="String" DefaultValue="*" />
                                                <asp:Parameter Name="pMobile_No" Type="String" DefaultValue="*" />
                                                <asp:Parameter Name="pNationality_Code" Type="Int32" DefaultValue="-1" />
                                                <asp:Parameter Name="pEducational_Qualification_Code" Type="Int32" DefaultValue="-1" />
                                                <asp:Parameter Name="pRegistration_Number" Type="Int32" DefaultValue="1" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_save" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btn_display" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>

