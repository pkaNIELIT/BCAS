<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ForgotPassword.aspx.cs" Inherits="UserDetails_ChangePassword"  EnableEventValidation="false"
    Title="Reset Password" culture="en-GB" UICulture="auto"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="BCASClassLibrary" Namespace="BCASClassLibrary"
    TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<table width="90%" align="center" >
    <tr>
        <td align="center"  >
            </td>
    </tr>
    <tr>
        <td align="center"  >
                            &nbsp;</td>
    </tr>
    <tr>
        <td align="center"  >
                            <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Times New Roman"
                                Font-Size="X-Large" ForeColor="#990033" Text="Reset Password" 
                                EnableTheming="False"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="center"  >
                            &nbsp;</td>
    </tr>
    <tr>
        <td align="center"  >
                <table width="60%" border="10" bordercolor="#CEEDEE" height="170px" >
                    <tr >
                        <td align="center">



<table>
        <tr>
            <td colspan="2" align="center">
                <asp:Label ID="lbl_Heading" runat="server" Width="350px" meta:resourcekey="lbl_Heading"
                    Text="" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 87px; height: 16px" align="left">
                <asp:Label ID="lbl_UserName" runat="server" Width="150px" meta:resourcekey="lbl_UserName"></asp:Label>
            </td>
            <td style="width: 131px; height: 16px" align="left">
                <asp:TextBox ID="txt_UserName" runat="server" Width="300px"></asp:TextBox>
    <cc1:MaskedEditExtender ID="mee_UserName" runat="server" PromptCharacter="" TargetControlID="txt_UserName"
        Mask="A{20}" Filtered="[0123456789_#.$@]">
    </cc1:MaskedEditExtender>
            </td>
        </tr>

        <%-- <tr>
            <td style="width: 87px; height: 16px" align="left">
                <asp:Label ID="lbl_PasswordQuestion" runat="server" Width="150px" meta:resourcekey="lbl_PasswordQuestion"></asp:Label>
            </td>
            <td style="width: 131px; height: 16px" align="left">
                <asp:DropDownList ID="ddl_PasswordQuestion" runat="server" Width="300px" Height="25px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 87px; height: 16px" align="left">
                <asp:Label ID="lbl_PasswordAnswer" runat="server" Width="150px" meta:resourcekey="lbl_PasswordAnswer"></asp:Label>
            </td>
            <td style="width: 131px; height: 16px" align="left">
                <asp:TextBox ID="txt_PasswordAnswer" runat="server" Width="300px" TextMode="Password"></asp:TextBox>
            </td>
        </tr>--%>
        <tr>
            <td valign="top">
                <asp:Label ID="lbl_ImageCode" runat="server" Width="150px" 
                    meta:resourcekey="lbl_ImageCode" style="text-align: left"></asp:Label>
            </td>
            <td valign="top" style="width: 251px; height: 16px">
                <cc2:ImageVerifier ID="ImageVerifier1" runat="server"></cc2:ImageVerifier>
            </td>
        </tr>
        <tr>
            <td style="width: 87px; height: 16px" align="left" valign="top">
                <asp:Label ID="lbl_ImgVerifyCode" runat="server" Width="150px" 
                    meta:resourcekey="lbl_ImgVerifyCode"></asp:Label>
            </td>
            <td style="width: 131px; height: 16px" align="left" valign="top">
                <asp:TextBox ID="txt_ImgVerifyCode" runat="server" Width="300px"></asp:TextBox>
    <cc1:MaskedEditExtender ID="mee_ImgVerifyCode" runat="server" PromptCharacter=""
        TargetControlID="txt_ImgVerifyCode" Mask="A{5}" Filtered="[0123456789]">
    </cc1:MaskedEditExtender>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btn_ResetPassword" runat="server" Width="300px" meta:resourcekey="btn_ResetPassword"
                    OnClick="btn_ResetPassword_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: left">
                <asp:Label ID="lbl_Message" runat="server" Width="300px" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 131px; height: 16px" align="left" colspan="2">
                <table>
                    <thead>
                        <asp:Label ID="lbl_ResetPassword_errors" runat="server" Width="150px" Font-Bold="true"
                            meta:resourcekey="lbl_ResetPassword_errors" Visible="false"></asp:Label>
                    </thead>
                    <tr>
                        <td style="height: 28px; width: auto;">
                            <asp:BulletedList ID="bullet_ResetPassword" runat="server" Width="350px" BulletStyle="Numbered"
                                Font-Bold="True" ForeColor="#FF3300">
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
        </td>
    </tr>
    <tr>
        <td >
            </td>
    </tr> 
    
    </table>



    <cc1:MaskedEditValidator ID="mev_UserName" runat="server" ControlExtender="mee_UserName"
        ControlToValidate="txt_UserName" EmptyValueMessage="User Name must be entered"
        IsValidEmpty="false" Display="None"></cc1:MaskedEditValidator>
    <cc1:ValidatorCalloutExtender ID="vce_UserName" runat="server" TargetControlID="mev_UserName">
    </cc1:ValidatorCalloutExtender>
    <cc1:MaskedEditValidator ID="mev_ImgVerifyCode" runat="server" ControlExtender="mee_ImgVerifyCode"
        ControlToValidate="txt_ImgVerifyCode" EmptyValueMessage="Verification Code must be entered"
        IsValidEmpty="false" Display="None"></cc1:MaskedEditValidator>
    <cc1:ValidatorCalloutExtender ID="vce_ImgVerifyCode" runat="server" TargetControlID="mev_ImgVerifyCode">
    </cc1:ValidatorCalloutExtender>
  <%--<cc1:CascadingDropDown ID="cdd_PasswordQuestion" runat="server" ServiceMethod="GetDropDownSecretQuestion"
        ServicePath="~/WebServices/MyDataWebService.asmx" LoadingText="Loading Data..."
        PromptText="[Select Secret Question]" TargetControlID="ddl_PasswordQuestion"
        Category="SecretQuestion">
    </cc1:CascadingDropDown>
    <asp:RequiredFieldValidator ID="rfv_PasswordQuestion" runat="server" ControlToValidate="ddl_PasswordQuestion"
        ErrorMessage="Secret Questionmust be entered" Display="None"></asp:RequiredFieldValidator>
    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="rfv_PasswordQuestion">
    </cc1:ValidatorCalloutExtender>
    <cc1:MaskedEditExtender ID="mee_PasswordAnswer" runat="server" PromptCharacter=""
        TargetControlID="txt_PasswordAnswer" Mask="A{20}">
    </cc1:MaskedEditExtender>
    <cc1:MaskedEditValidator ID="mev_PasswordAnswer" runat="server" ControlExtender="mee_PasswordAnswer"
        ControlToValidate="txt_PasswordAnswer" EmptyValueMessage="PasswordAnswer must be entered"
        IsValidEmpty="false" Display="None"></cc1:MaskedEditValidator>
    <cc1:ValidatorCalloutExtender ID="vce_PasswordAnswer" runat="server" TargetControlID="mev_PasswordAnswer">
    </cc1:ValidatorCalloutExtender>
    --%>
    
    <%--<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true"
        EnableViewState="true" EnableScriptLocalization="true" ScriptMode="Auto">
    </cc1:ToolkitScriptManager>--%>
    
    
    
    
</asp:Content>