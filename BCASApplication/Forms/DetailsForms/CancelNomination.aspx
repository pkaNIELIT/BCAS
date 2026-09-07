<%@ Page Language="C#" MasterPageFile="~/MasterPage.master"  AutoEventWireup="true" CodeFile="CancelNomination.aspx.cs" Inherits="Forms_DetailsForms_CancelNomination" Title="Untitled Page" %>

<script runat="server">

     
</script>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" align="center">
        <tr>
            <td colspan="2" align="center">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Label ID="lblHeading" runat="server" Font-Bold="True" ForeColor="#990000"
                    Text="CANCEL OF NOMINATED CANDIDATE "></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="30%">&nbsp;</td>
            <td width="70%">&nbsp;</td>
        </tr>
        <tr align="center">
            <td width="30%" colspan="2" style="width: 100%">
                <table width="70%" align="center" border="2">
                    <tr align="left">
                        <td width="30%">
                            <asp:Label ID="lblCourseSchedule" runat="server" Text="Course Schedule"></asp:Label>
                        </td>
                        <td width="70%">
                            <asp:DropDownList ID="ddlSchedule" runat="server" DataTextField="Course_Schedule"
                                DataValueField="Schedule_ID" Width="510px"
                                AppendDataBoundItems="True">
                                <asp:ListItem Value="0">-Select Course Schedule-</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                ControlToValidate="ddlSchedule" ErrorMessage="*" InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr align="left">
                        <td width="30%">
                            <asp:Label ID="lblregistraion_no" runat="server" Text="Registration Number : "></asp:Label>
                        </td>
                        <td width="70%">
                            <asp:TextBox ID="TxtRegNo1" runat="server" Width="150px"></asp:TextBox>
                            &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnDetails" runat="server" Text="Details"
                        OnClick="btnDetails_Click" />

                        </td>

                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="GrdviewDetails1" runat="server" AutoGenerateColumns="False"
                                DataKeyNames="Registration_Number" DataSourceID="SqlDataSource1"
                                BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px"
                                CellPadding="4">
                                <RowStyle BackColor="White" ForeColor="#330099" />
                                <Columns>
                                    <asp:BoundField DataField="Registration_Number"
                                        HeaderText="RegistrationNumber" InsertVisible="False" ReadOnly="True"
                                        SortExpression="Registration_Number" />
                                    <asp:BoundField DataField="Candidate_Name" HeaderText="CandidateName"
                                        SortExpression="Candidate_Name" />
                                    <asp:BoundField DataField="Description" HeaderText="Designation"
                                        SortExpression="Description" />
                                    <asp:BoundField DataField="Organisation_Name" HeaderText="OrganisationName"
                                        SortExpression="Organisation_Name" />
                                    <asp:BoundField DataField="Exam_Location_Name" HeaderText="ExamCenter"
                                        SortExpression="Exam_Location_Name" />
                                </Columns>

                                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />

                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                                ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>"
                                SelectCommand="SELECT cr.Registration_Number, cr.Candidate_Name, dm.Description, so.Organisation_Name, em.Exam_Location_Name FROM NOMINATIONS_FOR_COURSES AS nc INNER JOIN Candidate_Registration AS cr ON nc.Registration_No = cr.Registration_Number AND nc.Nominating_OrgCode = cr.Organisation_Code INNER JOIN Sponsoring_Organisation_Master AS so ON nc.Nominating_OrgCode = so.Organisation_Code INNER JOIN ExamLocationMaster AS em ON nc.Exam_Location_Code = em.Exam_Location_Code INNER JOIN Designation_Master AS dm ON cr.Designation_Code = dm.Designation_Code WHERE (nc.Schedule_ID = @scheduleID) AND (nc.Registration_No = @RegistrationNo)
                                union SELECT cr.Registration_Number, cr.Candidate_Name, dm.Description, so.Organisation_Name, em.Exam_Location_Name FROM NOMINATIONS_FOR_COURSES AS nc INNER JOIN Candidate_Registration_Screeners AS cr ON nc.Registration_No = cr.Registration_Number AND nc.Nominating_OrgCode = cr.Organisation_Code INNER JOIN Sponsoring_Organisation_Master AS so ON nc.Nominating_OrgCode = so.Organisation_Code INNER JOIN ExamLocationMaster AS em ON nc.Exam_Location_Code = em.Exam_Location_Code INNER JOIN Designation_Master AS dm ON cr.Designation_Code = dm.Designation_Code WHERE (nc.Schedule_ID = @scheduleID) AND (nc.Registration_No = @RegistrationNo)"
                                OnSelected="SqlDataSource1_Selected">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ddlSchedule" Name="scheduleID"
                                        PropertyName="SelectedValue" />
                                    <asp:ControlParameter ControlID="TxtRegNo1" Name="RegistrationNo"
                                        PropertyName="Text" />
                                </SelectParameters>
                            </asp:SqlDataSource>

                        </td>
                    </tr>
                    <tr align="center">
                        <td colspan="2">

                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Medium"
                                ForeColor="Red"></asp:Label>

                        </td>
                    </tr>
                    <tr align="Left">
                        <td width="30%">
                            <asp:Label ID="lblRemarksForCancel" runat="server" Text="Remarks For Nomination Cancel :"></asp:Label></td>
                        <td width="70%">
                            <asp:TextBox ID="txtRemarks" runat="server" Width="303px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                                            
                    <asp:Button ID="BtnRemarksCancel" runat="server"
                        Text="Cancel Nominated Candidate" Font-Bold="True" Width="315px"
                        OnClick="BtnRemarksCancel_Click" />
                        </td>
                    </tr>

                    <tr align="center">
                        <td colspan="2">
                            <asp:Label ID="lblRemarksCancel" runat="server" Font-Bold="true" Font-Size="Medium" ForeColor="Green"></asp:Label></td>
                    </tr>
                    <tr align="center">
                        <td colspan="2">
                            <asp:Label ID="lblheadingnominatedcandidate" runat="server"></asp:Label></td>
                    </tr>
                    <tr align="center">
                        <td colspan="2" style="font-size: small">
                            <asp:GridView ID="grdcancelofnominatedcandidate" runat="server"
                                AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84"
                                BorderWidth="1px" CellPadding="3" DataKeyNames="Registration_Number"
                                DataSourceID="SqlDataSource2" BorderStyle="None" CellSpacing="2"
                                ForeColor="#0033CC">
                                <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                <Columns>
                                    <asp:BoundField DataField="Registration_Number"
                                        HeaderText="RegistrationNumber" InsertVisible="False" ReadOnly="True"
                                        SortExpression="Registration_Number" />
                                    <asp:BoundField DataField="Candidate_Name" HeaderText="CandidateName"
                                        SortExpression="Candidate_Name" />
                                    <asp:BoundField DataField="Description" HeaderText="Designation"
                                        SortExpression="Description" />
                                    <asp:BoundField DataField="Organisation_Name" HeaderText="OrganisationName"
                                        SortExpression="Organisation_Name" />
                                    <asp:BoundField DataField="Exam_Location_Name" HeaderText="ExamLocationName"
                                        SortExpression="Exam_Location_Name" />
                                    <asp:BoundField DataField="BCAS_Nomination_Cancel_Remarks"
                                        HeaderText="BcasNominationCancelRemarks"
                                        SortExpression="BCAS_Nomination_Cancel_Remarks" />
                                    <asp:BoundField DataField="Nominating_Cancel_Date"
                                        HeaderText="NominatingCancelDate" SortExpression="Nominating_Cancel_Date" DataFormatString="{0:F}" />
                                </Columns>
                                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                <PagerStyle ForeColor="#8C4510"
                                    HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#738A9C" ForeColor="White" Font-Bold="True" />
                                <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                                ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>"
                                SelectCommand="SELECT cr.Registration_Number, cr.Candidate_Name, dm.Description, so.Organisation_Name, em.Exam_Location_Name, nc.BCAS_Nomination_Cancel_Remarks, nc.Nominating_Cancel_Date FROM NOMINATIONS_CANCELATION_HISTORY AS nc INNER JOIN Candidate_Registration AS cr ON nc.Registration_No = cr.Registration_Number AND nc.Nominating_OrgCode = cr.Organisation_Code INNER JOIN Sponsoring_Organisation_Master AS so ON nc.Nominating_OrgCode = so.Organisation_Code INNER JOIN ExamLocationMaster AS em ON nc.Exam_Location_Code = em.Exam_Location_Code INNER JOIN Designation_Master AS dm ON cr.Designation_Code = dm.Designation_Code WHERE (nc.Schedule_ID = @scheduleID) AND (nc.Registration_No = @RegistrationNo) union SELECT cr.Registration_Number, cr.Candidate_Name, dm.Description, so.Organisation_Name, em.Exam_Location_Name, nc.BCAS_Nomination_Cancel_Remarks, nc.Nominating_Cancel_Date FROM NOMINATIONS_CANCELATION_HISTORY AS nc INNER JOIN Candidate_Registration_Screeners AS cr ON nc.Registration_No = cr.Registration_Number AND nc.Nominating_OrgCode = cr.Organisation_Code INNER JOIN Sponsoring_Organisation_Master AS so ON nc.Nominating_OrgCode = so.Organisation_Code INNER JOIN ExamLocationMaster AS em ON nc.Exam_Location_Code = em.Exam_Location_Code INNER JOIN Designation_Master AS dm ON cr.Designation_Code = dm.Designation_Code WHERE (nc.Schedule_ID = @scheduleID) AND (nc.Registration_No = @RegistrationNo)"
                                OnSelected="SqlDataSource2_Selected">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ddlSchedule" Name="scheduleID"
                                        PropertyName="SelectedValue" />
                                    <asp:ControlParameter ControlID="TxtRegNo1" Name="RegistrationNo"
                                        PropertyName="Text" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                </table>

            </td>
        </tr>
    </table>
</asp:Content>

