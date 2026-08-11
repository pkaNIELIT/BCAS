<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="QryCourseSchedule.aspx.cs" Inherits="Forms_DetailsForms_QryCourseSchedule" Title="BCAS-->QryCourseSchedule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="100%" align="center" >
        <tr>
            <td colspan="2" align="center"  >
            &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" align="center" class="style1"  >
                <asp:Label ID="lblHeading" runat="server" Font-Bold="True" ForeColor="#990000" 
                Text="Query -Course Schedule Details"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="30%">
            &nbsp;</td>
            <td width="70%">
            &nbsp;</td>
        </tr>
        <tr>
            <td width="30%" colspan="2" style="width: 100%" align="center"  >
            &nbsp;</td>
        </tr>
        <tr align="center" >
            <td width="30%" colspan="2" style="width: 100%" align="center"  >
            <asp:Panel ID="Panel1" runat="server" Height ="350px" Width="95%" 
                    ScrollBars="Both" >             
                               
                <asp:GridView ID="grdDetails" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" 
                EmptyDataText="No Record Found" BorderColor="#000066" BorderStyle="Outset" 
                    BorderWidth="2px" Caption="Course Details" 
                CaptionAlign="Top" ForeColor="#333333" AllowPaging="True" 
                AllowSorting="True" PageSize="20" 
                    DataKeyNames="ID" >
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" 
                        SortExpression="ID" ReadOnly="True" InsertVisible="False" />
                        <asp:BoundField DataField="Course" 
                        HeaderText="Course" 
                        SortExpression="Course" />
                        <asp:BoundField DataField="Start_Dt" HeaderText="Start_Dt" 
                        SortExpression="Start_Dt" ReadOnly="True" />
                        <asp:BoundField DataField="End_Dt" HeaderText="End_Dt" 
                        SortExpression="End_Dt" ReadOnly="True" />
                        <asp:BoundField DataField="Exam_Dt" 
                            HeaderText="Exam_Dt" SortExpression="Exam_Dt" ReadOnly="True" />
                        <asp:BoundField DataField="Last_Dt_Nomination" 
                            HeaderText="Last_Dt_Nomination" 
                            SortExpression="Last_Dt_Nomination" ReadOnly="True" />
                        <asp:BoundField DataField="Last_Dt_acceptance" HeaderText="Last_Dt_acceptance" 
                            SortExpression="Last_Dt_acceptance" ReadOnly="True" />
                        <asp:BoundField DataField="Last_Dt_Fee" 
                            HeaderText="Last_Dt_Fee" SortExpression="Last_Dt_Fee" ReadOnly="True" />
                        <asp:BoundField DataField="Last_Dt_nomination_reappearing" 
                            HeaderText="Last_Dt_nomination_reappearing" 
                            SortExpression="Last_Dt_nomination_reappearing" ReadOnly="True" />
                        <asp:BoundField DataField="Last_Dt_reappearing_fee" 
                            HeaderText="Last_Dt_reappearing_fee" 
                            SortExpression="Last_Dt_reappearing_fee" ReadOnly="True" />
                    </Columns>
                    <FooterStyle BackColor="#990000" />
                    <HeaderStyle BackColor="#990000" ForeColor="White" />
                    <AlternatingRowStyle BackColor="#FFFFCC" ForeColor="#333333" />
                </asp:GridView>
                </asp:Panel>
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
            <td width="30%" colspan="2" style="width: 100%" align="center"  >
            &nbsp;</td>
        </tr>
        <tr>
            <td width="30%" colspan="2" style="width: 100%; text-align: left;" 
            align="center"  >
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" 
                SelectCommand="SELECT Course_Schedule .Schedule_ID ID,Course_Master.Course_Name Course,
convert(varchar(10),Course_Schedule.[Start_Date],103) Start_Dt, 
convert(varchar(10),Course_Schedule .[End_Date],103) End_Dt,
convert(varchar(10),Course_Schedule .[Date_of_Exam],103) Exam_Dt,
convert(varchar(10),Course_Schedule .[Last_date_for_nomination],103) Last_Dt_Nomination,
convert(varchar(10),Course_Schedule .[Last_date_for_bcas_acceptance],103) Last_Dt_acceptance,
convert(varchar(10),Course_Schedule .[Last_date_for_payments],103) Last_Dt_Fee,
convert(varchar(10),Course_Schedule .[Last_date_for_nomination_reappearing],103) Last_Dt_nomination_reappearing,
convert(varchar(10),Course_Schedule .[Last_date_for_reappearing_fee_payments],103) Last_Dt_reappearing_fee
FROM Course_Schedule Inner Join Course_Master ON
Course_Schedule.Course_Code=Course_Master.Course_Code 
Order by Course_Schedule .Start_Date desc"></asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>

