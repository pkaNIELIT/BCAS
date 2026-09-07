<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" Culture="en-GB" UICulture="auto" 
CodeFile="Course_Schedule_Exam_location.aspx.cs" Inherits="Course_Schedule_Exam_location" Title="Course_Schedule_Exam_location" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" align="center" >
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
        <tr align="center" >
            <td style="height: 35%" align="center">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table style="height: 34%">
                            <tr>
                                <td style="height: 70%">
                                    <asp:Panel ID="frm_panel" runat="server" Enabled="false">
                                        <table style="height: 70%; width: auto;">
                                            <tr id="R1" runat="server" >
                                                <td align="left">
                                                    <asp:Label ID="lbl_sr_no" runat="server" meta:resourcekey="lbl_sr_no"></asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:TextBox ID="txt_sr_no" runat="server" Width="336px" MaxLength="10" Enabled="False" ></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_course_schedule_id" runat="server" meta:resourcekey="lbl_course_schedule_id"></asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                <asp:DropDownList ID="ddlSchedule" runat="server" 
                DataSourceID="SqlDataSource2" DataTextField="Course_Schedule" 
                DataValueField="Schedule_ID" Width="336px" AppendDataBoundItems="True" >
                <asp:ListItem Value="0">-Select Course Schedule-</asp:ListItem>
            </asp:DropDownList>
            
                                                    <%--<asp:TextBox ID="txt_course_schedule_id" runat="server" Width="336px" MaxLength="10"></asp:TextBox>
                                                    <cc1:MaskedEditExtender ID="mee_course_schedule_id" runat="server" TargetControlID="txt_course_schedule_id"
                                                          Mask="9999999999" PromptCharacter="">
                                                    </cc1:MaskedEditExtender>
                                                    <cc1:MaskedEditValidator ID="mev_course_schedule_id" runat="server" ControlExtender="mee_course_schedule_id"
                                                          ControlToValidate="txt_course_schedule_id" EmptyValueMessage="Course Schedule ID must be entered"
                                                          IsValidEmpty="false" Display="None"></cc1:MaskedEditValidator>
                                                    <cc1:ValidatorCalloutExtender ID="vce_course_schedule_id" runat="server" TargetControlID="mev_course_schedule_id">
                                                    </cc1:ValidatorCalloutExtender>--%>
                                                    
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                        ControlToValidate="ddlSchedule" ErrorMessage="*" InitialValue="0" 
                                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_exam_location_code" runat="server" meta:resourcekey="lbl_exam_location_code"></asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <%--<asp:TextBox ID="txt_exam_location_code" runat="server" Width="336px" MaxLength="10"></asp:TextBox>
                                                    <cc1:MaskedEditExtender ID="mee_exam_location_code" runat="server" TargetControlID="txt_exam_location_code"
                                                          Mask="9999999999" PromptCharacter="">
                                                    </cc1:MaskedEditExtender>
                                                    <cc1:MaskedEditValidator ID="mev_exam_location_code" runat="server" ControlExtender="mee_exam_location_code"
                                                          ControlToValidate="txt_exam_location_code" EmptyValueMessage="Exam Location Code must be entered"
                                                          IsValidEmpty="false" Display="None"></cc1:MaskedEditValidator>
                                                    <cc1:ValidatorCalloutExtender ID="vce_exam_location_code" runat="server" TargetControlID="mev_exam_location_code">
                                                    </cc1:ValidatorCalloutExtender>--%><asp:DropDownList ID="ddlExamLocation" 
                                                        runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataLocation" 
                                                        DataTextField="Exam_Location_Name" DataValueField="Exam_Location_Code" 
                                                        TabIndex="4" Width="336px">
                                                        <asp:ListItem Value="0">-- Select Location --</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                        ControlToValidate="ddlExamLocation" ErrorMessage="*" InitialValue="0" 
                                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
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
                                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" SelectCommand="SELECT     Course_Schedule.Schedule_ID, Course_Master.Course_Name +' - Start DT '+ CONVERT(VARCHAR(10),Course_Schedule.Start_Date,103) + ' - End DT ' + CONVERT(VARCHAR(10),Course_Schedule.End_Date,103) +' - Exam DT '+  CONVERT(VARCHAR(10),Course_Schedule.Date_of_Exam,103) Course_Schedule
FROM         Course_Master INNER JOIN
                      Course_Schedule ON Course_Master.Course_Code = Course_Schedule.Course_Code
                      WHERE Last_date_for_nomination  &gt;=  convert(date,GETDATE ());"></asp:SqlDataSource>
                                    <asp:SqlDataSource ID="SqlDataLocation" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" 
                                        SelectCommand="SELECT [Exam_Location_Code], [Exam_Location_Name] FROM [ExamLocationMaster] ORDER BY [Exam_Location_Name]">
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
        <tr align="center">
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table style="width: auto; height: 96px">
                            <tr>
                                <td>
                                    <asp:Panel ID="grd_panel" runat="server" BorderColor="#DEBA84" BorderWidth="1px">
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
                                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetCourse_Schedule_Exam_location"
                                            TypeName="BCASClassLibrary.TYP_COURSE_SCHEDULE_EXAM_LOCATION">
                                            <SelectParameters>
                                                <asp:Parameter Name="pSr_No" Type="Int32" DefaultValue="-1" />
                                                <asp:Parameter Name="pCourse_Schedule_ID" Type="Int32" DefaultValue="-1" />
                                                <asp:Parameter Name="pExam_Location_Code" Type="Int32" DefaultValue="-1" />
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

