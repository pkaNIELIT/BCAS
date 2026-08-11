<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="repNominatedCandidates.aspx.cs" Inherits="Reports_repNominatedCandidates" Title="BCAS--> NominatedCandidates" Culture="en-GB" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="95%" align="center">
        <tr>
            <td align="center"></td>
        </tr>
        <tr align="center">
            <td></td>
        </tr>
        <tr align="center">
            <td>

                <asp:Label ID="lblHeading" runat="server" Font-Bold="True" Font-Size="Large"
                    ForeColor="#000099"></asp:Label>

            </td>
        </tr>
        <tr>
            <td></td>
        </tr>
        <tr align="center">
            <td align="center">

                <table width="70%" align="center" border="2">
                    <tr align="left">
                        <td width="30%">
                            <asp:Label ID="lblCourseSchedule" runat="server" Text="Course Schedule"></asp:Label>
                        </td>
                        <td width="70%">

                            <asp:DropDownList ID="ddlSchedule" runat="server"
                                DataSourceID="SqlDataSource2" DataTextField="Course_Schedule"
                                DataValueField="Schedule_ID" Width="500px" AppendDataBoundItems="True">
                                <asp:ListItem Value="0">-Select Course Schedule-</asp:ListItem>
                            </asp:DropDownList>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                ErrorMessage="*" ControlToValidate="ddlSchedule" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr id="trSpons" runat="server" visible="true">
                        <td width="30%">
                            <asp:Label ID="lblSpons" runat="server"
                                Text="Sponsoring Organisation"></asp:Label></td>
                        <td width="70%">
                            <asp:DropDownList ID="ddlOrganisation" runat="server" Width="300px" DataTextField="Organisation_Name"
                                DataValueField="Organisation_Code"
                                AppendDataBoundItems="True">
                                <asp:ListItem Value="0">--Select Sponsoring Organisation--</asp:ListItem>

                            </asp:DropDownList>

                        </td>
                    </tr>

                    <tr align="left">
                        <td width="30%">
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                                ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>"
                                SelectCommand="SELECT     Course_Schedule.Schedule_ID, Course_Master.Course_Name +' - Start DT '+ CONVERT(VARCHAR(10),Course_Schedule.Start_Date,103) + ' - End DT ' + CONVERT(VARCHAR(10),Course_Schedule.End_Date,103) +' - Exam DT '+  CONVERT(VARCHAR(10),Course_Schedule.Date_of_Exam,103) Course_Schedule
FROM         Course_Master INNER JOIN
                      Course_Schedule ON Course_Master.Course_Code = Course_Schedule.Course_Code
                      Order By Course_Schedule.Date_of_Exam desc"></asp:SqlDataSource>

                            &nbsp;</td>
                        <td width="70%">

                            <asp:Button ID="btnDisplay" runat="server" Text="Get Data" Width="250px"
                                OnClick="btnDisplay_Click" TabIndex="4" />
                            <asp:Label ID="lblOrg" runat="server" Text="0" Visible="false"></asp:Label>
                        </td>
                    </tr>

                </table>
            </td>
        </tr>
        <tr>
            <td></td>
        </tr>

        <tr>
            <td align="center" height="50px" valign="top" colspan="2">
                <asp:ImageButton ID="imgpdf" runat="server" ImageUrl="~/Images/pdf.jpg" Width="3%" Height="40%" OnClick="imgPDF_Click" Visible="false" />
                &nbsp;&nbsp;
            <asp:ImageButton ID="imgExcel" runat="server" ImageUrl="~/Images/Export_Exl.jpg" OnClick="imgExcel_Click" Visible="false" Height="19px" Width="43px" />

                &nbsp;</td>
        </tr>
        <tr>
            <td align="left" colspan="2">
                <div id="divReportData" runat="server" style="width: 100%;"></div>
            </td>
        </tr>


    </table>

</asp:Content>

