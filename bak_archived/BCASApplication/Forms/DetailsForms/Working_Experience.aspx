<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" Culture="en-GB" UICulture="auto" 
CodeFile="Working_Experience.aspx.cs" Inherits="Working_Experience" Title="Working_Experience" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" border="0"  align="center" bgcolor="#FBEBE6" >
        <tr>
            <td style="height: 21px" align="center">
                &nbsp;</td>
        </tr>
        <tr align="right" >
            <td style="height: 21px; text-align: right;" align="right" >
                                    <asp:HyperLink ID="hlinkWorkingExp" runat="server" 
                                        
                    NavigateUrl="~/Forms/DetailsForms/PrintDetails.aspx" Font-Size="Large" 
                    ForeColor="#000066">Click Here To Complete Registration</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td style="height: 21px" align="center">
                <asp:Label ID="lbl_Heading" runat="server" meta:resourcekey="lbl_Heading" 
                    EnableTheming="True" style="font-weight: 700"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 35%" align="center">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table style="height: 34%">
                            <tr>
                                <td style="height: 70%">
                                    <asp:Panel ID="frm_panel" runat="server" >
                                        <table style="height: 70%; width: auto;" border="2">
                                            <tr id="R1" runat="server">
                                                <td align="left">
                                                    <asp:Label ID="lbl_sr_no" runat="server" meta:resourcekey="lbl_sr_no"></asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:TextBox ID="txt_sr_no" runat="server" Width="336px" MaxLength="10" Enabled="False" ></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    &nbsp;</td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_registration_number" runat="server" 
                                                        meta:resourcekey="lbl_registration_number"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 87px; height: 16px">
                                                    <asp:TextBox ID="txt_registration_number" runat="server" Width="336" 
                                                        Enabled="False"></asp:TextBox>
                                                    <%--<asp:TextBox ID="txt_registration_number" runat="server" MaxLength="10" 
                                                        Width="336px" Enabled="False"></asp:TextBox>--%>
                                                    <%--<cc1:MaskedEditExtender ID="mee_registration_number" runat="server" 
                                                        Mask="9999999999" PromptCharacter="" TargetControlID="txt_registration_number">
                                                    </cc1:MaskedEditExtender>
                                                    <cc1:MaskedEditValidator ID="mev_registration_number" runat="server" 
                                                        ControlExtender="mee_registration_number" 
                                                        ControlToValidate="txt_registration_number" Display="None" 
                                                        EmptyValueMessage="Registration Number must be entered" IsValidEmpty="false"></cc1:MaskedEditValidator>
                                                    <cc1:ValidatorCalloutExtender ID="vce_registration_number" runat="server" 
                                                        TargetControlID="mev_registration_number">
                                                    </cc1:ValidatorCalloutExtender>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    &nbsp;</td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblName" runat="server" Text="Candidate Name"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 87px; height: 16px">
                                                    <asp:TextBox ID="txt_Candidate_Name" runat="server" Enabled="False" 
                                                        Width="336px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    &nbsp;</td>
                                                <td align="left" style="width: 87px; height: 16px">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_organisation" runat="server" 
                                                        meta:resourcekey="lbl_organisation"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 87px; height: 16px">
                                                    <asp:TextBox ID="txt_organisation" runat="server" MaxLength="100" Width="336px"></asp:TextBox>
                                                    <cc1:MaskedEditExtender ID="mee_organisation" runat="server" TargetControlID="txt_organisation"
                                                          Mask="LA{99}" Filtered="[0123456789+.,&-/  ]" PromptCharacter="">
                                                    </cc1:MaskedEditExtender>
                                                    <cc1:MaskedEditValidator ID="mev_organisation" runat="server" ControlExtender="mee_organisation"
                                                          ControlToValidate="txt_organisation" EmptyValueMessage="Organisation must be entered"
                                                          IsValidEmpty="false" Display="None"></cc1:MaskedEditValidator>
                                                    <cc1:ValidatorCalloutExtender ID="vce_organisation" runat="server" TargetControlID="mev_organisation">
                                                    </cc1:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    &nbsp;</td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_designation" runat="server" 
                                                        meta:resourcekey="lbl_designation"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 87px; height: 16px">
                                                    <asp:TextBox ID="txt_designation" runat="server" MaxLength="50" Width="336px"></asp:TextBox>
                                                    <cc1:MaskedEditExtender ID="mee_designation" runat="server" 
                                                        Filtered="[0123456789+.,&amp;-/  ]" Mask="LA{49}" PromptCharacter="" 
                                                        TargetControlID="txt_designation">
                                                    </cc1:MaskedEditExtender>
                                                    <cc1:MaskedEditValidator ID="mev_designation" runat="server" 
                                                        ControlExtender="mee_designation" ControlToValidate="txt_designation" 
                                                        Display="None" EmptyValueMessage="Designation must be entered" 
                                                        IsValidEmpty="false"></cc1:MaskedEditValidator>
                                                    <cc1:ValidatorCalloutExtender ID="vce_designation" runat="server" 
                                                        TargetControlID="mev_designation">
                                                    </cc1:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    &nbsp;</td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_date_from" runat="server" meta:resourcekey="lbl_date_from"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 87px; height: 16px">
                                                    <asp:TextBox ID="txt_date_from" runat="server" MaxLength="10" Width="336px"></asp:TextBox>
                                                    <cc1:MaskedEditExtender ID="mee_date_from" runat="server" CultureName="en-GB" 
                                                        Mask="99/99/9999" MaskType="Date" TargetControlID="txt_date_from" 
                                                        UserDateFormat="DayMonthYear">
                                                    </cc1:MaskedEditExtender>
                                                    <cc1:MaskedEditValidator ID="mev_date_from" runat="server" 
                                                        ControlExtender="mee_date_from" ControlToValidate="txt_date_from" 
                                                        Display="None" EmptyValueMessage="Date From must be entered" 
                                                        IsValidEmpty="false"></cc1:MaskedEditValidator>
                                                    <cc1:ValidatorCalloutExtender ID="vce_date_from" runat="server" 
                                                        TargetControlID="mev_date_from">
                                                    </cc1:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    &nbsp;</td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:CompareValidator ID="Com_v_date_from" runat="server" 
                                                        ControlToValidate="txt_date_from" ErrorMessage="Invalid Date From" Operator="LessThanEqual" 
                                                        Type="Date"></asp:CompareValidator>
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_date_to" runat="server" meta:resourcekey="lbl_date_to"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 87px; height: 16px">
                                                    <asp:TextBox ID="txt_date_to" runat="server" MaxLength="10" Width="336px"></asp:TextBox>
                                                    <cc1:MaskedEditExtender ID="mee_date_to" runat="server" CultureName="en-GB" 
                                                        Mask="99/99/9999" MaskType="Date" TargetControlID="txt_date_to" 
                                                        UserDateFormat="DayMonthYear">
                                                    </cc1:MaskedEditExtender>
                                                    <cc1:MaskedEditValidator ID="mev_date_to" runat="server" 
                                                        ControlExtender="mee_date_to" ControlToValidate="txt_date_to" Display="None" 
                                                        EmptyValueMessage="Date To must be entered" IsValidEmpty="false"></cc1:MaskedEditValidator>
                                                    <cc1:ValidatorCalloutExtender ID="vce_date_to" runat="server" 
                                                        TargetControlID="mev_date_to">
                                                    </cc1:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    &nbsp;</td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                                        ControlToValidate="txt_date_to" 
                                                         ErrorMessage="Date To should be &gt; Date From" 
                                                         ControlToCompare ="txt_date_from" Operator="GreaterThan" 
                                                        Type="Date"></asp:CompareValidator>
                                                        <asp:CompareValidator ID="Comp_Val_DateTo" runat="server" 
                                                        ControlToValidate="txt_date_to" ErrorMessage="Invalid Date To" 
                                                        Operator="LessThanEqual" Type="Date"></asp:CompareValidator>
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_remarks" runat="server" meta:resourcekey="lbl_remarks"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 87px; height: 16px">
                                                    <asp:TextBox ID="txt_remarks" runat="server" MaxLength="100" Width="336px"></asp:TextBox>
                                                    <cc1:MaskedEditExtender ID="mee_remarks" runat="server" 
                                                        Filtered="[0123456789+.,&amp;-/  ]" Mask="LA{99}" PromptCharacter="" 
                                                        TargetControlID="txt_remarks">
                                                    </cc1:MaskedEditExtender>
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
                                                    OnClick="btn_insert_Click" CausesValidation="false" AccessKey="I" />
                                            </td>
                                            <td style="height: 26px">
                                                <asp:Button ID="btn_update" runat="server" EnableTheming="False" meta:resourcekey="btn_update"
                                                    OnClick="btn_update_Click" CausesValidation="false" Enabled="false" 
                                                    AccessKey="U" Visible="False" />
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
                                            <td style="height: 26px">
                                                &nbsp;</td>
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
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table style="width: auto; height: 96px">
                            <tr align="center">
                                <td>
                                    <asp:Panel ID="grd_panel" runat="server" BorderColor="#DEBA84" BorderWidth="1px">
                                     <div id=div1 style="overflow:scroll;height:200px;width: 900px" >
                                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" BackColor="#DEBA84"
                                            BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2"
                                            PageSize="5" Height="50%" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
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
                                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetWorking_Experience"
                                            TypeName="BCASClassLibrary.TYP_WORKING_EXPERIENCE">
                                            <SelectParameters>
                                                <asp:Parameter Name="pSr_no" Type="Int32" DefaultValue="-1" />
                                                <asp:Parameter Name="pRegistration_Number" Type="Int32" DefaultValue="-1" />
                                                <asp:Parameter Name="pOrganisation" Type="String" DefaultValue="*" />
                                                <asp:Parameter Name="pDesignation" Type="String" DefaultValue="*" />
                                                <asp:Parameter Name="pDate_From" Type="String" DefaultValue="01/01/1900" />
                                                <asp:Parameter Name="pDate_To" Type="String" DefaultValue="01/01/1900" />
                                                <asp:Parameter Name="pRemarks" Type="String" DefaultValue="*" />
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

