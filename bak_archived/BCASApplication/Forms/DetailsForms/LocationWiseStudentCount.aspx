<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LocationWiseStudentCount.aspx.cs" Inherits="Forms_DetailsForms_LocationWiseStudentCount" Title="BCAS-->LocationWiseStudentCount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="100%" align="center" >
    <tr>
        <td colspan="2" align="center"  >
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="2" align="center"  >
            <asp:Label ID="lblHeading" runat="server" Font-Bold="True" ForeColor="#990000" 
                Text="TOTAL LOCATION WISE STUDENT COUNT"></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="30%">
            &nbsp;</td>
        <td width="70%">
            &nbsp;</td>
    </tr>
    <tr align="center" >
        <td width="30%" colspan="2" style="width: 100%"  >            
            <table width="70%" align="center" border="2"  >
                <tr align="left">
                    <td width="30%">
            <asp:Label ID="lblCourseSchedule" runat="server" Text="Course Schedule"></asp:Label>
                    </td>
                    <td width="70%">
            <asp:DropDownList ID="ddlSchedule" runat="server" DataTextField="Course_Schedule" 
                DataValueField="Schedule_ID" Width="510px" 
                AppendDataBoundItems="True" >
                <asp:ListItem Value="0">-Select Course Schedule-</asp:ListItem>
            </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="ddlSchedule" ErrorMessage="*" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                 </table>
            
            </td>
    </tr>
    <tr>
        <td width="30%" colspan="2" style="width: 100%" align="center"  >            
            <asp:Button ID="btnCount" runat="server" Font-Bold="True" ForeColor="Maroon" 
                onclick="btnCount_Click" Text="Get Count" />
        </td>
    </tr>
    <tr>
        <td width="30%" colspan="2" style="width: 100%" align="center"  >            
            &nbsp;</td>
    </tr>
    <tr>
        <td width="30%" colspan="2" style="width: 100%" align="center"  >            
         
            
            <asp:GridView ID="grdOrgWiseCount" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" 
                EmptyDataText="No Record Found" BorderColor="#000066" BorderStyle="Outset" 
                    BorderWidth="2px" Caption="Total Location Wise Count" 
                CaptionAlign="Top" ForeColor="#333333" AllowPaging="True" 
                AllowSorting="True" Visible="False" PageSize="20"  >
                <Columns>
                    <asp:BoundField DataField="Exam_Location_Name" HeaderText="Exam Location" 
                        SortExpression="Exam_Location_Name" />
                    <asp:BoundField DataField="Total_Nominated_Candidates" 
                        HeaderText="Total Nominated Candidates" 
                        SortExpression="Total_Nominated_Candidates" ReadOnly="True" />
                    <asp:BoundField DataField="BCAS_Approval" HeaderText="BCAS Approval" 
                        SortExpression="BCAS_Approval" ReadOnly="True" />
                    <asp:BoundField DataField="Fee_Status" HeaderText="Fee Status" 
                        SortExpression="Fee_Status" ReadOnly="True" />
                </Columns>
                <FooterStyle BackColor="#990000" />
                <HeaderStyle BackColor="#990000" ForeColor="White" />
                <AlternatingRowStyle BackColor="#FFFFCC" ForeColor="#333333" />
            </asp:GridView>
            
            
        </td>
    </tr>
    <tr>
        <td width="30%" colspan="2" style="width: 100%" align="center"  >            
            &nbsp;</td>
    </tr>
    <tr>
        <td width="30%" colspan="2" style="width: 100%" align="center"  >            
            &nbsp;</td>
    </tr>
    <tr>
        <td width="30%" colspan="2" style="width: 100%; text-align: left;" 
            align="center"  >            
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" 
                SelectCommand="SELECT ExamLocationMaster.Exam_Location_Name,COUNT(*) Total_Nominated_Candidates,
COUNT(BCAS_Approval_Status) BCAS_Approval,COUNT (Fee_Status) Fee_Status
FROM NOMINATIONS_FOR_COURSES 
INNER JOIN ExamLocationMaster ON
NOMINATIONS_FOR_COURSES.Exam_Location_Code =ExamLocationMaster.Exam_Location_Code     
where  Schedule_ID= @Schedule
group by ExamLocationMaster.Exam_Location_Name">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ddlSchedule" Name="Schedule" 
                        PropertyName="SelectedValue" />
                </SelectParameters>
            </asp:SqlDataSource>
        </td>
    </tr>
    </table> 

</asp:Content>

