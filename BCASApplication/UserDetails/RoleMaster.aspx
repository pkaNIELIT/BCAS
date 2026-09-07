<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="RoleMaster.aspx.cs" Inherits="RoleMaster" Title="Role Master"
    culture="en-GB" UICulture="auto"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<table width="90%">
    <tr align="center">
        <td align="center">
            &nbsp;</td>
    </tr>
    <tr align="center">
        <td align="center">
            &nbsp;</td>
    </tr>
    <tr align="center">
        <td align="center">
        </td>
    </tr>
    <tr align="center" >
        
        <td align="center" >
        
        <table border=1 align="center" >     
    </td>
    </tr>
        <tr>
            <td style="height: 21px" align="center">
                <asp:Label ID="lbl_Heading" runat="server" meta:resourcekey="lbl_Heading" EnableTheming="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 35%"  align="center">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table style="height: 34%">
                            <tr>
                                <td style="height: 70%">
                                    <asp:Panel ID="frm_panel" runat="server">
                                        <table style="height: 70%; width: 506px;">
                                            <tr>
                                                <td style="width: 131px; height: 16px" align="left">
                                                    <asp:Label ID="lbl_role" runat="server" meta:resourcekey="lbl_role"></asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:TextBox ID="txt_role" runat="server" Width="336px" MaxLength="50"></asp:TextBox>
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
                                                <asp:Button ID="btn_CreateRole" runat="server" EnableTheming="False" meta:resourcekey="btn_CreateRole"
                                                    OnClick="btn_CreateRole_Click" CausesValidation="true" AccessKey="V" />
                                            </td>
                                           
                                            <td style="height: 26px">
                                                <asp:Button ID="btn_DeleteRole" runat="server" EnableTheming="False" meta:resourcekey="btn_DeleteRole"
                                                    OnClick="btn_DeleteRole_Click" CausesValidation="False" AccessKey="C" />
                                            </td>
                                             <td style="height: 26px">
                                                <asp:Button ID="btn_quit" runat="server" EnableTheming="False" meta:resourcekey="btn_quit"
                                                    OnClick="btn_quit_Click" CausesValidation="false" AccessKey="Q" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_choice" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lbl_error" runat="server" ></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <cc1:MaskedEditExtender ID="mee_role" runat="server" PromptCharacter="" TargetControlID="txt_role"
                            Mask="LA{19}" Filtered="[0123456789]">
                        </cc1:MaskedEditExtender>
                        <cc1:MaskedEditValidator ID="mev_role" runat="server" ControlExtender="mee_role"
                            ControlToValidate="txt_role" EmptyValueMessage="Form type must be entered"
                            IsValidEmpty="false" Display="None"></cc1:MaskedEditValidator>
                        <cc1:ValidatorCalloutExtender ID="vce_role" runat="server" TargetControlID="mev_role">
                        </cc1:ValidatorCalloutExtender>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        
        <tr align="center" >
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table style="width: auto; height: 96px" align="center" >
                            <tr align="center" >
                                <td align="center" >
                                      <asp:Panel ID="grd_panel" runat="server" BorderColor="#DEBA84" BorderWidth="1px">
                                          <asp:GridView ID="GridView1" runat="server" BackColor="#DEBA84" 
                                              BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                                              CellSpacing="2" onselectedindexchanged="GridView1_SelectedIndexChanged">
                                              <Columns>
                                                <asp:CommandField ShowSelectButton="True" CausesValidation="false" />
                                            </Columns>
                                              <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                              <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                              <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                              <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                              <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                                          </asp:GridView>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_CreateRole" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
        </td>
    </tr>
</table>

    
</asp:Content>
