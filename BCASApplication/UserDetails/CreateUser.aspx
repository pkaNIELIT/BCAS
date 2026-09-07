<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CreateUser.aspx.cs" Inherits="CreateUser" EnableEventValidation="false" Title="Create User"
    culture="en-GB" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="BCASClassLibrary" Namespace="BCASClassLibrary" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table align="center" border="0" width=95% style="border-color: #800000">
        <tr>
            <td align="center">
               </td>
        </tr>
        <tr>
            <td align="center">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="lbl_Heading" runat="server" width="350px" meta:resourcekey="lbl_Heading" Text="" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
               </td>
        </tr>
        <tr>
            <td align="center">
              <table width="60%" align="center" border="2">
                <tr align="left">
                    <td width="35%">
                <asp:Label ID="lblOrgType" runat="server" Text="Organisation Type"></asp:Label>
                        </td>
                     <td width="65%">
        <asp:DropDownList ID="ddlOrganisationType" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="ddlOrganisationType_SelectedIndexChanged" Width="204px">
                    <asp:ListItem Selected="True">-Select-</asp:ListItem>
                    <asp:ListItem Value="S">Sponsoring Organisation</asp:ListItem>
                    <asp:ListItem Value="P">Payee Organisation</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfv_OrgType" runat="server" 
                    ControlToValidate="ddlOrganisationType" ErrorMessage="*" InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                </tr>
                            
                <tr align="left">
                    <td width="35%">
                <asp:Label ID="lblOrgName" runat="server" Text="Organisation Name"></asp:Label>
                        </td>
                     <td width="65%">
        <asp:DropDownList ID="ddlOrgName" runat="server" DataSourceID="SqlDataSource1" 
                    DataTextField="Organisation_Name" DataValueField="Organisation_Code" 
                    Width="204px" TabIndex="1" AppendDataBoundItems="True">
            <asp:ListItem Value="0">-Select-</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfv_OrgName" runat="server" 
                    ControlToValidate="ddlOrgName" ErrorMessage="*" InitialValue="0"></asp:RequiredFieldValidator>
                         </td>
                </tr>
                            
                <tr align="left">
                    <td width="35%">
                <asp:Label ID="lbl_UserName" runat="server" meta:resourcekey="lbl_UserName"></asp:Label>
                        </td>
                     <td width="65%">
        
                <asp:TextBox ID="txt_UserName" runat="server" width="200px" TabIndex="2"></asp:TextBox>
    <cc1:MaskedEditExtender ID="mee_UserName" runat="server"  PromptCharacter="" TargetControlID="txt_UserName"
        Mask="A{20}" Filtered="[0123456789_#.$@]">
    </cc1:MaskedEditExtender>
        
                         </td>
                </tr>
                            
                <tr align="left">
                    <td width="35%">
                <asp:Label ID="lbl_Password" runat="server" meta:resourcekey="lbl_Password"></asp:Label>
                        </td>
                     <td width="65%">
        
                <asp:TextBox ID="txt_Password" runat="server" width="200px" TextMode="Password" 
                             TabIndex="3"></asp:TextBox>
    <cc1:MaskedEditExtender ID="mee_Password" runat="server"  PromptCharacter="" TargetControlID="txt_Password"
        Mask="LA{19}" Filtered="[0123456789+.,&-/  ]">
    </cc1:MaskedEditExtender>
        
                         </td>
                </tr>
                            
                <tr align="left">
                    <td width="35%">
                <asp:Label ID="lbl_ConfirmPassword" runat="server" 
                            meta:resourcekey="lbl_ConfirmPassword"></asp:Label>
                        </td>
                     <td width="65%">
        
                <asp:TextBox ID="txt_ConfirmPassword" runat="server" width="200px" 
                    TextMode="Password" TabIndex="4"></asp:TextBox>
    <cc1:MaskedEditExtender ID="mee_ConfirmPassword" runat="server"  PromptCharacter=""
        TargetControlID="txt_ConfirmPassword" Mask="LA{19}" Filtered="[0123456789+.,&-/  ]">
    </cc1:MaskedEditExtender>
        
                         </td>
                </tr>
                            
                <tr align="left">
                    <td width="35%">
                <asp:Label ID="lbluserName" runat="server" Text="User Name"></asp:Label>
                        </td>
                     <td width="65%">
        
                <asp:TextBox ID="txtNameofUser" runat="server" Width="200px" TabIndex="5"></asp:TextBox>
                <cc1:MaskedEditExtender ID="mee_NameofUser" runat="server"  PromptCharacter=""
        TargetControlID="txtNameofUser" Mask="LA{19}" Filtered="[0123456789+.,&-/  ]">
    </cc1:MaskedEditExtender>
                         </td>
                </tr>
                            
                <tr align="left">
                    <td width="35%">
                <asp:Label ID="lblMobileNo" runat="server" Text="Email ID"></asp:Label>
                        </td>
                     <td width="65%">
        <asp:TextBox ID="txt_email_id" runat="server" Width="200px" MaxLength="50" 
                                            TabIndex="6"></asp:TextBox>
                                       <asp:RequiredFieldValidator ID="rfv_email_id" runat="server" ControlToValidate="txt_email_id"
                                                             SetFocusOnError="true"  ErrorMessage="*"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="rev_email_id" runat="server" ControlToValidate="txt_email_id"
                                                             SetFocusOnError="true"  ErrorMessage="Please Enter Valid Email Id" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator> 
                         </td>
                </tr>
                            
                <tr align="left">
                    <td width="35%">
                <asp:Label ID="lblmaidID0" runat="server" Text="Mobile No"></asp:Label>
                        </td>
                     <td width="65%">
                         <asp:TextBox ID="txt_mobile_no" runat="server" Width="112px" MaxLength="10" 
                                            TabIndex="7" Height="22px"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="fte_mobile_no" runat="server" Enabled="True" TargetControlID="txt_mobile_no"
                                                            ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                                            runat="server" ControlToValidate="txt_mobile_no" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="rev_mobile_no" runat="server" ControlToValidate="txt_mobile_no"
                                                             SetFocusOnError="true"  ErrorMessage="Please Enter Valid Mobile No." ValidationExpression="^[1-9][0-9]{9}"></asp:RegularExpressionValidator>
                         </td>
                </tr>
                            
                <tr align="left">
                    <td width="35%">
            <asp:Label ID="lbl_ImageCode" runat="server" meta:resourcekey="lbl_ImageCode"></asp:Label>
                        </td>
                     <td width="65%">
        
                         <cc2:ImageVerifier ID="ImageVerifier1" runat="server" />
                    </td>
                </tr>
                            
                <tr align="left">
                    <td width="35%">
                <asp:Label ID="lbl_ImgVerifyCode" runat="server" meta:resourcekey="lbl_ImgVerifyCode"></asp:Label>
                        </td>
                     <td width="65%">
        <asp:TextBox ID="txt_ImgVerifyCode" runat="server" width="200px" TabIndex="8"></asp:TextBox>
                         </td>
                </tr>
                            
                <tr align="left">
                    <td width="35%">
                        &nbsp;</td>
                     <td width="65%">
        <asp:Button ID="btn_CreateUser" runat="server" width="200px" meta:resourcekey="btn_CreateUser"
                    OnClick="btn_CreateUser_Click" TabIndex="9" />
                         </td>
                </tr>
                            
                </table>
              </td>
        </tr>
               
       
        <tr>
            <td align="center" >
                <asp:Label ID="lbl_Message" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
               </td>
        </tr>
         <tr>
            <td align="center" >
                        <asp:BulletedList ID="bullet_NewUser" runat="server" BulletStyle="Numbered" Font-Bold="True" ForeColor="#FF3300">
                            </asp:BulletedList>
               </td>
        </tr>
               
       
        <tr>
            <td style="width: 131px; height: 16px" align="left">
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" 
                    SelectCommand="GetCombinedOrganisations" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlOrganisationType" DefaultValue="" 
                            Name="v_OrgType" PropertyName="SelectedValue" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
    <cc1:MaskedEditValidator ID="mev_UserName" runat="server"  ControlExtender="mee_UserName"
        ControlToValidate="txt_UserName" EmptyValueMessage="User ID must be entered"
        IsValidEmpty="false" Display="None"></cc1:MaskedEditValidator>
    <cc1:ValidatorCalloutExtender ID="vce_UserName" runat="server"  TargetControlID="mev_UserName">
    </cc1:ValidatorCalloutExtender>
    <cc1:MaskedEditValidator ID="mev_Password" runat="server"  ControlExtender="mee_Password"
        ControlToValidate="txt_Password" EmptyValueMessage="Password must be entered"
        IsValidEmpty="false" Display="None"></cc1:MaskedEditValidator>
    <cc1:ValidatorCalloutExtender ID="vce_Password" runat="server"  TargetControlID="mev_Password">
    </cc1:ValidatorCalloutExtender>
    <cc1:MaskedEditValidator ID="mev_ConfirmPassword" runat="server"  ControlExtender="mee_ConfirmPassword"
        ControlToValidate="txt_ConfirmPassword" EmptyValueMessage="Confirm Password must be entered"
        IsValidEmpty="false" Display="None"></cc1:MaskedEditValidator>
    <cc1:ValidatorCalloutExtender ID="vce_ConfirmPassword" runat="server"  TargetControlID="mev_ConfirmPassword">
    </cc1:ValidatorCalloutExtender>
    
    <cc1:MaskedEditExtender ID="mee_ImgVerifyCode" runat="server"  PromptCharacter=""
        TargetControlID="txt_ImgVerifyCode" Mask="A{5}" Filtered="[0123456789]">
    </cc1:MaskedEditExtender>
    <cc1:MaskedEditValidator ID="mev_ImgVerifyCode" runat="server"  ControlExtender="mee_ImgVerifyCode"
        ControlToValidate="txt_ImgVerifyCode" EmptyValueMessage="Verification Code must be entered"
        IsValidEmpty="false" Display="None"></cc1:MaskedEditValidator>
    <cc1:ValidatorCalloutExtender ID="vce_ImgVerifyCode" runat="server"  TargetControlID="mev_ImgVerifyCode">
    </cc1:ValidatorCalloutExtender>
    
    <cc1:MaskedEditValidator ID="mev_NameofUser" runat="server"  ControlExtender="mee_NameofUser"
        ControlToValidate="txtNameofUser" EmptyValueMessage="User Name must be entered"
        IsValidEmpty="false" Display="None"></cc1:MaskedEditValidator>
    <cc1:ValidatorCalloutExtender ID="vce_NameofUser" runat="server"  TargetControlID="mev_NameofUser">
    </cc1:ValidatorCalloutExtender>

</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

    </asp:Content>

