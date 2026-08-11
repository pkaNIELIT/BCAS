<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" culture="en-GB" UICulture="auto"
CodeFile="Exam_Schedule.aspx.cs" Inherits="Exam_Schedule" Title="Exam_Schedule" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" border: 1" align="center" >
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
                                            <tr id="RowSchedule" runat="server">
                                                <td align="left">
                                                    <asp:Label ID="lbl_schedule_id" runat="server" meta:resourcekey="lbl_schedule_id"></asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:TextBox ID="txt_schedule_id" runat="server" Width="336px" MaxLength="10" Enabled="False" ></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="top" >
                                                    <asp:Label ID="lbl_course_code" runat="server" meta:resourcekey="lbl_course_code" Text="Course"></asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <%--<asp:TextBox ID="txt_course_code" runat="server" Width="336px" MaxLength="10"></asp:TextBox>
                                                    <cc1:MaskedEditExtender ID="mee_course_code" runat="server" TargetControlID="txt_course_code"
                                                          Mask="9999999999" PromptCharacter="">
                                                    </cc1:MaskedEditExtender>
                                                    <cc1:MaskedEditValidator ID="mev_course_code" runat="server" ControlExtender="mee_course_code"
                                                          ControlToValidate="txt_course_code" EmptyValueMessage="Course Code must be entered"
                                                          IsValidEmpty="false" Display="None"></cc1:MaskedEditValidator>
                                                    <cc1:ValidatorCalloutExtender ID="vce_course_code" runat="server" TargetControlID="mev_course_code">
                                                    </cc1:ValidatorCalloutExtender>--%>
                                                    <asp:DropDownList ID="ddlCourseCode" runat="server" 
                                                        DataSourceID="objSQLDS_CourseCode" DataTextField="Course_Name" 
                                                        DataValueField="Course_Code" Width="330px" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlCourseCode_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">-Select Course Name-</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                        ControlToValidate="ddlCourseCode" ErrorMessage="*" InitialValue="0"></asp:RequiredFieldValidator>
                                                    <asp:SqlDataSource ID="objSQLDS_CourseCode" runat="server" 
                                                        ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" 
                                                        SelectCommand="SELECT [Course_Code], [Course_Name] FROM [Course_Master] where course_code in(1,2,3,4)">
                                                    </asp:SqlDataSource>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left"  valign="top" >
                                                    <asp:Label ID="lbl_start_date" runat="server" meta:resourcekey="lbl_start_date" Text="Start Date"></asp:Label>(dd/mm/yyyy)
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:TextBox ID="txt_start_date" runat="server" Width="336px" MaxLength="10"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="cal_start_date" runat="server" TargetControlID="txt_start_date"  Format="dd/MM/yyyy" PopupPosition="Right"></cc1:CalendarExtender>
                                                    <cc1:MaskedEditExtender ID="mee_start_date" runat="server" TargetControlID="txt_start_date"
                                                          Mask="99/99/9999" MaskType = "Date" UserDateFormat="DayMonthYear" CultureName="en-GB">
                                                    </cc1:MaskedEditExtender>
                                                    <cc1:MaskedEditValidator ID="mev_start_date" runat="server" ControlExtender="mee_start_date"
                                                          ControlToValidate="txt_start_date" EmptyValueMessage="Start Date must be entered"
                                                          IsValidEmpty="false" Display="None"></cc1:MaskedEditValidator>
                                                    <cc1:ValidatorCalloutExtender ID="vce_start_date" runat="server" TargetControlID="mev_start_date">
                                                    </cc1:ValidatorCalloutExtender>
                                                    <asp:CompareValidator ID="Com_v_start_date" runat="server" 
                                                        ControlToValidate="txt_start_date" 
                                                        ErrorMessage="Start Date Must be &gt; Current Date OR Invalid Date" 
                                                        Operator="GreaterThan" Type="Date"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left"  valign="top" >
                                                    <asp:Label ID="lbl_end_date" runat="server" meta:resourcekey="lbl_end_date" Text="End Date"></asp:Label>(dd/mm/yyyy)
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:TextBox ID="txt_end_date" runat="server" Width="336px" MaxLength="10"></asp:TextBox>
                                                     <cc1:CalendarExtender ID="cal_end_date" runat="server" TargetControlID="txt_end_date"  Format="dd/MM/yyyy" PopupPosition="Right"></cc1:CalendarExtender>
                                                     <cc1:MaskedEditExtender ID="mee_end_date" runat="server" TargetControlID="txt_end_date"
                                                          Mask="99/99/9999" MaskType = "Date" UserDateFormat="DayMonthYear" CultureName="en-GB">
                                                    </cc1:MaskedEditExtender>
                                                    <cc1:MaskedEditValidator ID="mev_end_date" runat="server" ControlExtender="mee_end_date"
                                                          ControlToValidate="txt_end_date" EmptyValueMessage="End Date must be entered"
                                                          IsValidEmpty="false" Display="None"></cc1:MaskedEditValidator>
                                                    <cc1:ValidatorCalloutExtender ID="vce_end_date" runat="server" TargetControlID="mev_end_date">
                                                    </cc1:ValidatorCalloutExtender>
                                                    <asp:CompareValidator ID="Com_v_End_date" runat="server" 
                                                        ControlToCompare="txt_start_date" ControlToValidate="txt_end_date" 
                                                        ErrorMessage="End Date Must be &gt; Start Date OR Invalid Date" 
                                                        Operator="GreaterThan" Type="Date"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left"  valign="top" >
                                                    <asp:Label ID="lbl_date_of_exam" runat="server" meta:resourcekey="lbl_date_of_exam" Text ="Date of Exam"></asp:Label>(dd/mm/yyyy)
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:TextBox ID="txt_date_of_exam" runat="server" Width="336px" MaxLength="10"></asp:TextBox>
                                                     <cc1:CalendarExtender ID="cal_date_of_exam" runat="server" TargetControlID="txt_date_of_exam"  Format="dd/MM/yyyy" PopupPosition="Right"></cc1:CalendarExtender>
                                                    <cc1:MaskedEditExtender ID="mee_date_of_exam" runat="server" TargetControlID="txt_date_of_exam"
                                                          Mask="99/99/9999" MaskType = "Date" UserDateFormat="DayMonthYear" CultureName="en-GB">
                                                    </cc1:MaskedEditExtender>
                                                    <cc1:MaskedEditValidator ID="mev_date_of_exam" runat="server" ControlExtender="mee_date_of_exam"
                                                          ControlToValidate="txt_date_of_exam" EmptyValueMessage="Date of Exam must be entered"
                                                          IsValidEmpty="false" Display="None"></cc1:MaskedEditValidator>
                                                    <cc1:ValidatorCalloutExtender ID="vce_date_of_exam" runat="server" TargetControlID="mev_date_of_exam">
                                                    </cc1:ValidatorCalloutExtender>
                                                    <asp:CompareValidator ID="Com_v_exam_date" runat="server" 
                                                        ControlToCompare="txt_end_date" ControlToValidate="txt_date_of_exam" 
                                                        ErrorMessage="Exam Date Must be &gt; End Date OR Invalid Date" 
                                                        Operator="GreaterThan" Type="Date"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left"  valign="top" >
                                                    <asp:Label ID="lbl_last_date_for_nomination" runat="server" meta:resourcekey="lbl_last_date_for_nomination" Text="Last Date for Nomination"></asp:Label>(dd/mm/yyyy)
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:TextBox ID="txt_last_date_for_nomination" runat="server" Width="336px" MaxLength="10" SkipLiterals="false"></asp:TextBox>
                                                     <cc1:CalendarExtender ID="cal_last_date_for_nomination" runat="server" TargetControlID="txt_last_date_for_nomination"  Format="dd/MM/yyyy" PopupPosition="Right"></cc1:CalendarExtender>
                                                    <cc1:MaskedEditExtender ID="mee_last_date_for_nomination" runat="server" TargetControlID="txt_last_date_for_nomination"
                                                          Mask="99/99/9999" MaskType = "Date" UserDateFormat="DayMonthYear" CultureName="en-GB" >
                                                    </cc1:MaskedEditExtender>
                                                    <cc1:MaskedEditValidator ID="mev_last_date_for_nomination" runat="server" ControlExtender="mee_last_date_for_nomination"
                                                          ControlToValidate="txt_last_date_for_nomination" EmptyValueMessage="Last date for nomination must be entered"
                                                          IsValidEmpty="false" Display="None"></cc1:MaskedEditValidator>
                                                    <cc1:ValidatorCalloutExtender ID="vce_last_date_for_nomination" runat="server" TargetControlID="mev_last_date_for_nomination">
                                                    </cc1:ValidatorCalloutExtender>
                                                    <!--<asp:CompareValidator ID="Com_v_last_date_for_nomination" runat="server" 
                                                        ControlToCompare="txt_start_date" ControlToValidate="txt_last_date_for_nomination" 
                                                        ErrorMessage="Nomination Date Must be &lt; Start Date OR Invalid Date" 
                                                        Operator="LessThan" Type="Date" SetFocusOnError="True"></asp:CompareValidator>-->
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left"  valign="top" >
                                                    <asp:Label ID="lbl_last_date_for_bcas_acceptance" runat="server" meta:resourcekey="lbl_last_date_for_bcas_acceptance" Text="Last Date for BCAS Acceptance"></asp:Label>(dd/mm/yyyy)
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:TextBox ID="txt_last_date_for_bcas_acceptance" runat="server" Width="336px" MaxLength="10" SkipLiterals="false"></asp:TextBox>
                                                     <cc1:CalendarExtender ID="cal_last_date_for_bcas_acceptance" runat="server" TargetControlID="txt_last_date_for_bcas_acceptance"  Format="dd/MM/yyyy" PopupPosition="Right"></cc1:CalendarExtender>
                                                    <cc1:MaskedEditExtender ID="mee_last_date_for_bcas_acceptance" runat="server" TargetControlID="txt_last_date_for_bcas_acceptance"
                                                          Mask="99/99/9999" MaskType = "Date" UserDateFormat="DayMonthYear" CultureName="en-GB"  >
                                                    </cc1:MaskedEditExtender>
                                                    <cc1:MaskedEditValidator ID="mev_last_date_for_bcas_acceptance" runat="server" ControlExtender="mee_last_date_for_bcas_acceptance"
                                                          ControlToValidate="txt_last_date_for_bcas_acceptance" EmptyValueMessage="Last date for bcas acceptance must be entered"
                                                          IsValidEmpty="false" Display="None"></cc1:MaskedEditValidator>
                                                    <cc1:ValidatorCalloutExtender ID="vce_last_date_for_bcas_acceptance" runat="server" TargetControlID="mev_last_date_for_bcas_acceptance">
                                                    </cc1:ValidatorCalloutExtender>
                                                    <asp:CompareValidator ID="Com_v_last_date_for_bcas_acceptance" runat="server" 
                                                        ControlToCompare="txt_last_date_for_nomination" ControlToValidate="txt_last_date_for_bcas_acceptance" 
                                                        ErrorMessage="BCAS Approval Date Must be &gt;= Nomination Date " 
                                                        Operator="GreaterThanEqual" Type="Date" SetFocusOnError="True"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left"  valign="top" class="auto-style1" >
                                                    <asp:Label ID="lbl_last_date_for_payments" runat="server" meta:resourcekey="lbl_last_date_for_payments" Text="Last Date for Payments"></asp:Label>(dd/mm/yyyy)
                                                </td>
                                                <td align="left" class="auto-style2">
                                                    <asp:TextBox ID="txt_last_date_for_payments" runat="server" Width="336px" MaxLength="10"></asp:TextBox>
                                                     <cc1:CalendarExtender ID="cal_last_date_for_payments" runat="server" TargetControlID="txt_last_date_for_payments"  Format="dd/MM/yyyy" PopupPosition="Right"></cc1:CalendarExtender>
                                                    <cc1:MaskedEditExtender ID="mee_last_date_for_payments" runat="server" TargetControlID="txt_last_date_for_payments"
                                                          Mask="99/99/9999" MaskType = "Date" UserDateFormat="DayMonthYear" CultureName="en-GB" ClearMaskOnLostFocus="false" >
                                                    </cc1:MaskedEditExtender>
                                                    <cc1:MaskedEditValidator ID="mev_last_date_for_payments" runat="server" ControlExtender="mee_last_date_for_payments"
                                                          ControlToValidate="txt_last_date_for_payments" EmptyValueMessage="Last date for payments must be entered"
                                                          IsValidEmpty="false" Display="None"></cc1:MaskedEditValidator>
                                                    <cc1:ValidatorCalloutExtender ID="vce_last_date_for_payments" runat="server" TargetControlID="mev_last_date_for_payments">
                                                    </cc1:ValidatorCalloutExtender>
                                                    
                                                    <asp:CompareValidator ID="Com_v_last_date_for_fee_payments" runat="server" 
                                                        ControlToCompare="txt_last_date_for_bcas_acceptance" ControlToValidate="txt_last_date_for_payments" 
                                                        ErrorMessage="Fee Payments Date Must be &gt;= BCAS Approval Date" 
                                                        Operator="GreaterThanEqual" Type="Date" SetFocusOnError="True"></asp:CompareValidator>
                                                        
                                                        <asp:CompareValidator ID="Com_v_last_date_for_fee_payments1" runat="server" 
                                                        ControlToCompare="txt_date_of_exam" ControlToValidate="txt_last_date_for_payments" 
                                                        ErrorMessage="Fee Payments Date &lt; Exam Date " 
                                                        Operator="LessThan" Type="Date" SetFocusOnError="True"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left"  valign="top" >
                                                    <asp:Label ID="lbl_last_date_for_nomination_reappearing" runat="server" meta:resourcekey="lbl_last_date_for_nomination_reappearing" Text="Last Date for Nomination Reappearing"></asp:Label>(dd/mm/yyyy)
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:TextBox ID="txt_last_date_for_nomination_reappearing" runat="server" Width="336px" MaxLength="10" Enabled="False"></asp:TextBox>
                                                    <cc1:MaskedEditExtender ID="mee_last_date_for_nomination_reappearing" runat="server" TargetControlID="txt_last_date_for_nomination_reappearing"
                                                          Mask="99/99/9999" MaskType = "Date" UserDateFormat="DayMonthYear" CultureName="en-GB">
                                                    </cc1:MaskedEditExtender>
                                                    <cc1:MaskedEditValidator ID="mev_last_date_for_nomination_reappearing" runat="server" ControlExtender="mee_last_date_for_nomination_reappearing"
                                                          ControlToValidate="txt_last_date_for_nomination_reappearing" EmptyValueMessage="Last date for nomination reappearing must be entered"
                                                          IsValidEmpty="false" Display="None"></cc1:MaskedEditValidator>
                                                    <cc1:ValidatorCalloutExtender ID="vce_last_date_for_nomination_reappearing" runat="server" TargetControlID="mev_last_date_for_nomination_reappearing">
                                                    </cc1:ValidatorCalloutExtender>
                                                    
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                                        ControlToCompare="txt_date_of_exam" ControlToValidate="txt_last_date_for_nomination_reappearing" 
                                                        ErrorMessage="Reappearing Nomination Date Must be &lt;= Exam Date " 
                                                        Operator="LessThanEqual" Type="Date" SetFocusOnError="True"></asp:CompareValidator>
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left"  valign="top" >
                                                    <asp:Label ID="lbl_last_date_for_reappearing_fee_payments" runat="server" meta:resourcekey="lbl_last_date_for_reappearing_fee_payments" Text="Last date for Reappearing Fee Payments"></asp:Label>(dd/mm/yyyy)
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:TextBox ID="txt_last_date_for_reappearing_fee_payments" runat="server" Width="336px" MaxLength="10" Enabled="False"></asp:TextBox>
                                                    <cc1:MaskedEditExtender ID="mee_last_date_for_reappearing_fee_payments" runat="server" TargetControlID="txt_last_date_for_reappearing_fee_payments"
                                                          Mask="99/99/9999" MaskType = "Date" UserDateFormat="DayMonthYear" CultureName="en-GB">
                                                    </cc1:MaskedEditExtender>
                                                    <cc1:MaskedEditValidator ID="mev_last_date_for_reappearing_fee_payments" runat="server" ControlExtender="mee_last_date_for_reappearing_fee_payments"
                                                          ControlToValidate="txt_last_date_for_reappearing_fee_payments" EmptyValueMessage="Last date for reappearing fee payments must be entered"
                                                          IsValidEmpty="false" Display="None"></cc1:MaskedEditValidator>
                                                    <cc1:ValidatorCalloutExtender ID="vce_last_date_for_reappearing_fee_payments" runat="server" TargetControlID="mev_last_date_for_reappearing_fee_payments">
                                                    </cc1:ValidatorCalloutExtender>
                                                    
                                                     <asp:CompareValidator ID="CompareValidator2" runat="server" 
                                                        ControlToCompare="txt_date_of_exam" ControlToValidate="txt_last_date_for_reappearing_fee_payments" 
                                                        ErrorMessage="Reappearing Fee Payments Date Must be &lt;= Exam Date " 
                                                        Operator="LessThanEqual" Type="Date" SetFocusOnError="True"></asp:CompareValidator>
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
                                                   Text="Insert" OnClick="btn_insert_Click" CausesValidation="false" AccessKey="I" />
                                            </td>
                                            <td style="height: 26px">
                                                <asp:Button ID="btn_update" runat="server" EnableTheming="False" meta:resourcekey="btn_update"
                                                  Text="Update"  OnClick="btn_update_Click" CausesValidation="false" Enabled="false" AccessKey="U" />
                                            </td>
                                            <td style="height: 26px">
                                                <asp:Button ID="btn_search" runat="server" EnableTheming="False" meta:resourcekey="btn_search"
                                                  Text="Search"  CausesValidation="false" Enabled="false" OnClick="btn_search_Click" AccessKey="S" />
                                            </td>
                                            <td style="height: 26px">
                                                <asp:Button ID="btn_save" runat="server" EnableTheming="False" meta:resourcekey="btn_save"
                                                 Text="Save"   OnClick="btn_save_Click" CausesValidation="true" AccessKey="V" />
                                            </td>
                                            <td style="height: 26px">
                                                <asp:Button ID="btn_quit" runat="server" EnableTheming="False" meta:resourcekey="btn_quit"
                                                 Text="Quit"   OnClick="btn_quit_Click" CausesValidation="false" AccessKey="Q" 
                                                    Visible="False" />
                                            </td>
                                            <td style="height: 26px">
                                                <asp:Button ID="btn_cancel" runat="server" EnableTheming="False" meta:resourcekey="btn_cancel"
                                                Text="Cancel"    OnClick="btn_cancel_Click" CausesValidation="False" AccessKey="C" />
                                            </td>
                                            <td style="height: 26px">
                                                <asp:Button ID="btn_display" runat="server" EnableTheming="False" meta:resourcekey="btn_display"
                                                Text="Display"    OnClick="btn_display_Click" CausesValidation="False" AccessKey="D" />
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
        <tr align="center">
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table style="width: auto; height: 96px" align="center">
                            <tr>
                                <td align="center" >
                                    <asp:Panel ID="grd_panel" runat="server" BorderColor="#DEBA84" BorderWidth="1px">
                                    
                                    <div id="div1" style="overflow :scroll; height:263px; width:1050px;" >
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
                                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetExam_Schedule"
                                            TypeName="BCASClassLibrary.TYP_COURSE_SCHEDULE">
                                            <SelectParameters>
                                                <asp:Parameter Name="pSchedule_ID" Type="Int32" DefaultValue="-1" />
                                                <asp:Parameter Name="pCourse_Code" Type="Int32" DefaultValue="-1" />
                                                <asp:Parameter Name="pStart_Date" Type="String" DefaultValue="01/01/1900" />
                                                <asp:Parameter Name="pEnd_Date" Type="String" DefaultValue="01/01/1900" />
                                                <asp:Parameter Name="pDate_of_Exam" Type="String" DefaultValue="01/01/1900" />
                                                <asp:Parameter Name="pLast_date_for_nomination" Type="String" DefaultValue="01/01/1900" />
                                                <asp:Parameter Name="pLast_date_for_bcas_acceptance" Type="String" DefaultValue="01/01/1900" />
                                                <asp:Parameter Name="pLast_date_for_payments" Type="String" DefaultValue="01/01/1900" />
                                                <asp:Parameter Name="pLast_date_for_nomination_reappearing" Type="String" DefaultValue="01/01/1900" />
                                                <asp:Parameter Name="pLast_date_for_reappearing_fee_payments" Type="String" DefaultValue="01/01/1900" />
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

<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    <style type="text/css">
    .auto-style1 {
        height: 16px;
    }
    .auto-style2 {
        width: 87px;
        height: 16px;
    }
</style>
</asp:Content>


