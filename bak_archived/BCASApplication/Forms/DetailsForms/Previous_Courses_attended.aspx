<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" culture="en-GB" UICulture="auto" 
CodeFile="Previous_Courses_attended.aspx.cs" Inherits="Previous_Courses_attended" Title="Previous_Courses_attended" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" border="0" align="center" bgcolor="#FBEBE6">
        <tr align="center" >
            <td style="height: 21px; text-align: right;" align="center">
                &nbsp;</td>
        </tr>
        <tr align="center" >
            <td style="height: 21px; text-align: right;" align="center">
                                    <asp:HyperLink ID="hlinkWorkingExp" runat="server" 
                                        
                    NavigateUrl="~/Forms/DetailsForms/Working_Experience.aspx" Font-Size="Large" 
                    ForeColor="#000066">Continue To Working Experience Details</asp:HyperLink>
                                </td>
        </tr>
        <tr align="center" >
            <td style="height: 21px" align="center">
                <asp:Label ID="lbl_Heading" runat="server" meta:resourcekey="lbl_Heading" 
                    EnableTheming="True" Font-Bold="True" Font-Size="Large"></asp:Label>
            </td>
        </tr>
        <tr align="center" >
            <td style="height: 35%" align="center">
                <asp:Label ID="lblWhetherprevious" Text="Whether Previous Course Attended:" runat ="server" >

                                    </asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:DropDownList ID="ddlWhetherPrevious" runat ="server" AutoPostBack="True" OnSelectedIndexChanged="ddlWhetherPrevious_SelectedIndexChanged" >
                                        <asp:ListItem  Value="1">Yes</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                    </asp:DropDownList>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible ="false">
                    <ContentTemplate>
                        <table style="height: 34%" align="center" >
                            <tr align="center" >
                                <td style="height: 70%" align="center" >
                                    
                                    <asp:Panel ID="frm_panel" runat="server">
                                        <table style="height: 70%; width: auto;" bgcolor="#FBEBE6" align="center" border="2" >
                                            <tr id="R1" runat ="server" >
                                                <td align="left">
                                                    <asp:Label ID="lbl_sr_no" runat="server" meta:resourcekey="lbl_sr_no"></asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:TextBox ID="txt_sr_no" runat="server" Width="336px" MaxLength="10" Enabled="False" ></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_registration_number" runat="server" meta:resourcekey="lbl_registration_number"></asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:TextBox ID="txt_registration_number" runat="server" Width="336px" 
                                                        MaxLength="10" Enabled="False"></asp:TextBox>
                                                    <cc1:MaskedEditExtender ID="mee_registration_number" runat="server" TargetControlID="txt_registration_number"
                                                          Mask="9999999999" PromptCharacter="">
                                                    </cc1:MaskedEditExtender>
                                                    <cc1:MaskedEditValidator ID="mev_registration_number" runat="server" ControlExtender="mee_registration_number"
                                                          ControlToValidate="txt_registration_number" EmptyValueMessage="Registration Number must be entered"
                                                          IsValidEmpty="false" Display="None"></cc1:MaskedEditValidator>
                                                    <cc1:ValidatorCalloutExtender ID="vce_registration_number" runat="server" TargetControlID="mev_registration_number">
                                                    </cc1:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" class="style1">
                                                    &nbsp;</td>
                                                <td align="left" class="style2">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td align="left" class="style1">
                                                    <asp:Label ID="lblName" runat="server" Text="Candidate Name"></asp:Label>
                                                </td>
                                                <td align="left" class="style2">
                                                    <asp:TextBox ID="txt_CandidateName" runat="server" Enabled="False" 
                                                        Width="336px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    &nbsp;</td>
                                                <td align="left" style="width: 87px; height: 16px">
                                                    <%--<asp:TextBox ID="txt_course_code" runat="server" MaxLength="10" Width="336px"></asp:TextBox>
                                                    <cc1:MaskedEditExtender ID="mee_course_code" runat="server" Mask="9999999999" 
                                                        PromptCharacter="" TargetControlID="txt_course_code">
                                                    </cc1:MaskedEditExtender>
                                                    <cc1:MaskedEditValidator ID="mev_course_code" runat="server" 
                                                        ControlExtender="mee_course_code" ControlToValidate="txt_course_code" 
                                                        Display="None" EmptyValueMessage="Course Code must be entered" 
                                                        IsValidEmpty="false"></cc1:MaskedEditValidator>
                                                    <cc1:ValidatorCalloutExtender ID="vce_course_code" runat="server" 
                                                        TargetControlID="mev_course_code">
                                                    </cc1:ValidatorCalloutExtender>--%>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_course_code" runat="server" 
                                                        meta:resourcekey="lbl_course_code"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 87px; height: 16px">
                                                    <%--<asp:TextBox ID="txt_course_code" runat="server" MaxLength="10" Width="336px"></asp:TextBox>
                                                    <cc1:MaskedEditExtender ID="mee_course_code" runat="server" Mask="9999999999" 
                                                        PromptCharacter="" TargetControlID="txt_course_code">
                                                    </cc1:MaskedEditExtender>
                                                    <cc1:MaskedEditValidator ID="mev_course_code" runat="server" 
                                                        ControlExtender="mee_course_code" ControlToValidate="txt_course_code" 
                                                        Display="None" EmptyValueMessage="Course Code must be entered" 
                                                        IsValidEmpty="false"></cc1:MaskedEditValidator>
                                                    <cc1:ValidatorCalloutExtender ID="vce_course_code" runat="server" 
                                                        TargetControlID="mev_course_code">
                                                    </cc1:ValidatorCalloutExtender>--%><asp:DropDownList ID="ddlCourseCode" 
                                                        runat="server" AppendDataBoundItems="True" DataSourceID="objSQLDS_CourseCode" 
                                                        DataTextField="Course_Name" DataValueField="Course_Code" Width="330px">
                                                        <asp:ListItem Value="0">-Select Course Name-</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="objSQLDS_CourseCode" runat="server" 
                                                        ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" 
                                                        SelectCommand="SELECT [Course_Code], [Course_Name] FROM [Course_Master]">
                                                    </asp:SqlDataSource>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    &nbsp;</td>
                                                <td align="left" style="width: 87px; height: 16px">
                                                    <asp:RegularExpressionValidator ID="rev_Couse_Code" runat="server" 
                                                        ControlToValidate="ddlCourseCode" ErrorMessage="*" SetFocusOnError="True" 
                                                        ValidationExpression="^[1-9]"></asp:RegularExpressionValidator>
                                                </td>
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
                                                        Display="None" EmptyValueMessage="Date from must be entered" 
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
                                                        ErrorMessage="Date To should be greater than Date From" 
                                                        ControlToCompare ="txt_date_from" Operator="GreaterThan" 
                                                        Type="Date"></asp:CompareValidator>
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_marks" runat="server" meta:resourcekey="lbl_marks"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 87px; height: 16px">
                                                    <asp:TextBox ID="txt_marks" runat="server" MaxLength="3" Width="336px"></asp:TextBox>
                                                    <cc1:MaskedEditExtender ID="mee_marks" runat="server" Mask="999" 
                                                        PromptCharacter="" TargetControlID="txt_marks">
                                                    </cc1:MaskedEditExtender>
                                                    <cc1:MaskedEditValidator ID="mev_marks" runat="server" 
                                                        ControlExtender="mee_marks" ControlToValidate="txt_marks" Display="None" 
                                                        EmptyValueMessage="Marks must be entered" IsValidEmpty="false"></cc1:MaskedEditValidator>
                                                    <cc1:ValidatorCalloutExtender ID="vce_marks" runat="server" 
                                                        TargetControlID="mev_marks">
                                                    </cc1:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    &nbsp;</td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:RangeValidator ID="RangeValidator1" runat="server" 
                                                        ControlToValidate="txt_marks" 
                                                        ErrorMessage="Marks should be between 0 and 100" MaximumValue="100" 
                                                        MinimumValue="0" SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_result" runat="server" meta:resourcekey="lbl_result"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 87px; height: 16px">
                                                    <asp:DropDownList ID="ddlResult" runat="server" Width="336px">
                                                        <asp:ListItem Value="0">- Select -</asp:ListItem>
                                                        <asp:ListItem Value="P">Pass</asp:ListItem>
                                                        <asp:ListItem Value="F">Fail</asp:ListItem>
                                                    </asp:DropDownList>                                                
                                                    <%--<asp:TextBox ID="txt_result" runat="server" MaxLength="1" Width="336px"></asp:TextBox>
                                                    <cc1:MaskedEditExtender ID="mee_result" runat="server" 
                                                        Filtered="[0123456789+.,&amp;-/  ]" Mask="LA{0}" PromptCharacter="" 
                                                        TargetControlID="txt_result">
                                                    </cc1:MaskedEditExtender>
                                                    <cc1:MaskedEditValidator ID="mev_result" runat="server" 
                                                        ControlExtender="mee_result" ControlToValidate="txt_result" Display="None" 
                                                        EmptyValueMessage="Result must be entered" IsValidEmpty="false"></cc1:MaskedEditValidator>
                                                    <cc1:ValidatorCalloutExtender ID="vce_result" runat="server" 
                                                        TargetControlID="mev_result">
                                                    </cc1:ValidatorCalloutExtender>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    &nbsp;</td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                        ControlToValidate="ddlResult" ErrorMessage="*" InitialValue="0" 
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
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
                            <tr align="center" >
                                <td style="height: 20px;" align="center">
                                    <table align="center" >
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
            <td align="center" >
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table width="90%" align="center" >
                            <tr align="center" >
                                <td align="center" >
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
                                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetPrevious_Courses_attended"
                                            TypeName="BCASClassLibrary.TYP_PREVIOUS_COURSES_ATTENDED">
                                            <SelectParameters>
                                                <asp:Parameter Name="pSr_No" Type="Int32" DefaultValue="-1" />
                                                <asp:Parameter Name="pRegistration_Number" Type="Int32" DefaultValue="-1" />
                                                <asp:Parameter Name="pCourse_Code" Type="Int32" DefaultValue="-1" />
                                                <asp:Parameter Name="pDate_from" Type="String" DefaultValue="01/01/1900" />
                                                <asp:Parameter Name="pDate_To" Type="String" DefaultValue="01/01/1900" />
                                                <asp:Parameter Name="pMarks" Type="Int32" DefaultValue="-1" />
                                                <asp:Parameter Name="pResult" Type="String" DefaultValue="*" />
                                                <asp:Parameter Name="pRemarks" Type="String" DefaultValue="*" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" >
                                    &nbsp;</td>
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

<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

    <style type="text/css">
        .style1
        {
            height: 15px;
        }
        .style2
        {
            width: 87px;
            height: 15px;
        }
    </style>

</asp:Content>


