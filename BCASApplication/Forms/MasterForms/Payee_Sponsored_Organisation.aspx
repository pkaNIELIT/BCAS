<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" Culture="en-GB" UICulture="auto" 
CodeFile="Payee_Sponsored_Organisation.aspx.cs" Inherits="Payee_Sponsored_Organisation" Title="Payee_Sponsored_Organisation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="90%" align="center">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr align="center">
            <td align="center">
                <table style="width: auto; height: 96px; border: 1" align="center">
                    <tr>
                        <td colspan="2" style="height: 21px" align="center">
                            <asp:Label ID="lbl_Heading" runat="server" meta:resourcekey="lbl_Heading" EnableTheming="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 35%" align="center">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <table style="height: 34%" border="1">
                                        <tr>
                                            <td style="height: 70%">
                                                <asp:Panel ID="frm_panel" runat="server" Enabled="false">
                                                    <table style="height: 70%; width: auto;" border="1">
                                                        <tr id="R1" runat="server">
                                                            <td align="left">
                                                                <asp:Label ID="lbl_s_no" runat="server" meta:resourcekey="lbl_s_no"></asp:Label>
                                                            </td>
                                                            <td style="width: 87px; height: 16px" align="left">
                                                                <asp:TextBox ID="txt_s_no" runat="server" Width="250px" MaxLength="10" 
                                                                    Enabled="False"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="lbl_sponsoring_organisation_code" runat="server" meta:resourcekey="lbl_sponsoring_organisation_code"></asp:Label>
                                                            </td>
                                                            <td style="width: 87px; height: 16px" align="left">
                                                                <%--<asp:TextBox ID="txt_sponsoring_organisation_code" runat="server" Width="336px" MaxLength="10"></asp:TextBox>--%>
                                                                <asp:DropDownList ID="ddlSponsoringOrganisation" runat="server" DataSourceID="SqlDataSponsoringOrg"
                                                                    DataTextField="Organisation_Name" DataValueField="Organisation_Code" Width="550px"
                                                                    AppendDataBoundItems="True">
                                                                    <asp:ListItem Value="0">-Select Sponsoring Organisation-</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RegularExpressionValidator ID="rev_SponsoringOrg" runat="server" 
                                                                    ControlToValidate="ddlSponsoringOrganisation" ErrorMessage="*" 
                                                                    SetFocusOnError="True" ValidationExpression="^[1-9]"></asp:RegularExpressionValidator>
                                                                <asp:SqlDataSource ID="SqlDataSponsoringOrg" runat="server" ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>"
                                                                    SelectCommand="SELECT [Organisation_Code], [Organisation_Name] FROM [Sponsoring_Organisation_Master]">
                                                                </asp:SqlDataSource>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="lbl_payee_organisation_code" runat="server" meta:resourcekey="lbl_payee_organisation_code"></asp:Label>
                                                            </td>
                                                            <td style="width: 87px; height: 16px" align="left">
                                                                <%--<asp:TextBox ID="txt_payee_organisation_code" runat="server" Width="336px" MaxLength="10"></asp:TextBox>
                                                    <cc1:MaskedEditExtender ID="mee_payee_organisation_code" runat="server" TargetControlID="txt_payee_organisation_code"
                                                          Mask="9999999999" PromptCharacter="">
                                                    </cc1:MaskedEditExtender>
                                                    <cc1:MaskedEditValidator ID="mev_payee_organisation_code" runat="server" ControlExtender="mee_payee_organisation_code"
                                                          ControlToValidate="txt_payee_organisation_code" EmptyValueMessage="Payee Organisation Code must be entered"
                                                          IsValidEmpty="false" Display="None"></cc1:MaskedEditValidator>
                                                    <cc1:ValidatorCalloutExtender ID="vce_payee_organisation_code" runat="server" TargetControlID="mev_payee_organisation_code">
                                                    </cc1:ValidatorCalloutExtender>--%><asp:DropDownList ID="ddlPayeeOrg" runat="server" DataSourceID="SqlDataPayeeOrg"
                                                                    DataTextField="Payee_Organisation_Name" DataValueField="Payee_Organisation_Code"
                                                                    Width="550px" AppendDataBoundItems="True">
                                                                    <asp:ListItem Value="0">-Select Payee Organisation-</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="ddlPayeeOrg"
                                                                    ErrorMessage="*" SetFocusOnError="True" ValidationExpression="^[1-9]"></asp:RegularExpressionValidator>--%>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                                    ControlToValidate="ddlPayeeOrg" ErrorMessage="*" InitialValue="0" 
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="height: 20px;" align="center">
                                                <table>
                                                    <tr>
                                                        <td style="height: 26px">
                                                            <asp:Button ID="btn_insert" runat="server" EnableTheming="False" meta:resourcekey="btn_insert"
                                                                OnClick="btn_insert_Click" CausesValidation="false" AccessKey="I" />
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
                                                <asp:SqlDataSource ID="SqlDataPayeeOrg" runat="server" 
                                                    ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" 
                                                    SelectCommand="SELECT [Payee_Organisation_Code], [Payee_Organisation_Name] FROM [Payee_Organisation_Master]">
                                                </asp:SqlDataSource>
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
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <table style="width: auto; height: 96px">
                                        <tr>
                                            <td>
                                                <asp:Panel ID="grd_panel" runat="server" BorderColor="#DEBA84" BorderWidth="1px">
                                                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" BackColor="#DEBA84"
                                                        BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2"
                                                        DataSourceID="ObjectDataSource1" PageSize="50" Height="50%" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
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
                                                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetPayee_Sponsored_Organisation"
                                                        TypeName="BCASClassLibrary.TYP_PAYEE_SPONSORED_ORGANISATION">
                                                        <SelectParameters>
                                                            <asp:Parameter Name="pS_No" Type="Int32" DefaultValue="-1" />
                                                            <asp:Parameter Name="pSponsoring_Organisation_Code" Type="Int32" DefaultValue="-1" />
                                                            <asp:Parameter Name="pPayee_Organisation_Code" Type="Int32" DefaultValue="-1" />
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
            </td>
        </tr>
    </table>
</asp:Content>

