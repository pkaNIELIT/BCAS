<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FeePaymentsBySO.aspx.cs" Culture="en-GB" Inherits="Forms_DetailsForms_FeePaymentsBySO" Title="BCAS-FeePaymentsBySO" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table width="95%" align="center">
    <tr>
        <td width="20%" colspan="2" style="width: 95%" align="center" >
         
            
            &nbsp;</td>
        
    </tr>
    <tr>
        <td width="20%" colspan="2" style="width: 95%" align="center" >
         
            
            <asp:Label ID="lblHeading" runat="server" Font-Bold="True" ForeColor="#990000" 
                Text="Fee Payments By Sponsoring Organisation"></asp:Label>
        </td>
        
    </tr>
    <tr>
        <td width="20%">
            &nbsp;</td>
        <td width="75%">
        
            &nbsp;</td>
        
    </tr>
    <tr>
        <td COLSPAN=2 align="center">
        
            <asp:Label ID="lblOrgName" runat="server" ForeColor="#003399" 
                style="text-align: center; font-weight: 700"></asp:Label>
        </td>
        
    </tr>
    <tr>
        <td width="20%">
            &nbsp;</td>
        <td width="75%">
        
            &nbsp;</td>
        
    </tr>
    <tr align="center">
        <td width="20%" colspan="2" style="width: 95%" align="center" >
        
            <table width="70%" align="center" border="2">
                <tr align="left">
                    <td width="30%">
            <asp:Label ID="lblCourseSchedule" runat="server" Text="Course Schedule"></asp:Label>
                    </td>
                     <td width="70%">
        
            <asp:DropDownList ID="ddlSchedule" runat="server" 
                DataSourceID="SqlDataSource2" DataTextField="Course_Schedule" 
                DataValueField="Schedule_ID" Width="600px" AppendDataBoundItems="True" >
                <asp:ListItem Value="0">-Select Course Schedule-</asp:ListItem>
            </asp:DropDownList>
            
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="*" ControlToValidate ="ddlSchedule" SetFocusOnError="true" InitialValue="0" ></asp:RequiredFieldValidator>
            
                    </td>
                </tr>
                <tr align="left">
                    <td width="30%">
            <asp:Label ID="lblRegdNo" runat="server" Text="Registration Number"></asp:Label>
                    </td>
                     <td width="70%">
        
            <asp:TextBox ID="txtRegdNo" runat="server" Width="250px" MaxLength="8" TabIndex="1"></asp:TextBox>
                
                <cc1:FilteredTextBoxExtender ID="fte_RegNo_no" runat="server" Enabled="True" TargetControlID="txtRegdNo"
                                                            ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                     <asp:RequiredFieldValidator ID="rfvEmpCode" runat="server" 
                ControlToValidate="txtRegdNo" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                        
                         </td>
                </tr>
                
              <%--  deep--%>
                 <tr align="left">
                    <td width="30%">
            <asp:Label ID="lblReappear" runat="server" Text="Whether Reappearing"></asp:Label>
                    </td>
                    <td width="70%">
        
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" 
                            RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="N">No</asp:ListItem>
                            <asp:ListItem Value="Y">Yes</asp:ListItem>
                        </asp:RadioButtonList>
                
                    </td>
                </tr>
                
               <%-- deep--%>
                
                               
                <tr align="left">
                    <td width="30%">
            <asp:Label ID="lblPayingOrg" runat="server" Text="Paying Organisation"></asp:Label>
                    </td>
                     <td width="70%">
                         <asp:DropDownList ID="ddlSponsoredOrg" runat="server" 
                DataSourceID="SqlDataPayeeOrg" DataTextField="Payee_Organisation_Name" 
                DataValueField="Payee_Organisation_Code" Width="600px" 
                AppendDataBoundItems="True" TabIndex="3" >
                <asp:ListItem Value="0">-Select Payee Organisation-</asp:ListItem>
            </asp:DropDownList>        
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                             ErrorMessage="*" ControlToValidate="ddlSponsoredOrg" InitialValue="0"></asp:RequiredFieldValidator>
                
                    </td>
                </tr>
            
                <tr align="left">
                    <td width="30%">
                        &nbsp;</td>
                     <td width="70%">
                      <asp:Button ID="btnGetDetails" runat="server" Text="Get Details" 
                Width="144px"  TabIndex="5" onclick="btnGetDetails_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        
            <asp:Button ID="btnSaveNomination" runat="server" Text="Pay Fee" 
                Width="144px" onclick="btnSaveNomination_Click" TabIndex="5" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        
            <asp:Button ID="btnDisplay" runat="server" Text="Show Details After Pay Fee" Width="200px" 
                onclick="btnDisplay_Click" TabIndex="4" />
        
                    </td>
                </tr>
            
            </table>
            </td>
        
    </tr>
    <tr>
        <td width="20%" colspan="2" style="width: 95%" align="center" >
        
            &nbsp;</td>
        
    </tr>
    <tr>
        <td width="20%" colspan="2" style="width: 95%" align="center" >
        
            <%--<div id="div1" style="overflow :scroll; height:75px; width:1000px;" >
            </div>--%>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="Registration_Number" DataSourceID="SqlDataSource1" 
                EmptyDataText="No Record Found" BorderColor="#000066" BorderStyle="Outset" 
                    BorderWidth="2px" Caption="Candidate Personal Details" 
                CaptionAlign="Top" ForeColor="#333333" >
                <Columns>
                    <asp:BoundField DataField="Employee_ID" HeaderText="Employee_ID" 
                        SortExpression="Employee_ID" />
                    <asp:BoundField DataField="Registration_Number" HeaderText="Registration_Number" 
                        SortExpression="Registration_Number" InsertVisible="False" 
                        ReadOnly="True" />
                    <asp:BoundField DataField="Candidate_Name" HeaderText="Candidate_Name" 
                        SortExpression="Candidate_Name" />
                    <asp:BoundField DataField="Sex_Code" HeaderText="Sex_Code" 
                        SortExpression="Sex_Code" />
                    <asp:BoundField DataField="Designation" HeaderText="Designation" 
                        SortExpression="Designation" />
                    <asp:BoundField DataField="Qualification" HeaderText="Qualification" 
                        SortExpression="Qualification" />
                    <asp:BoundField DataField="DOB" HeaderText="DOB" 
                        SortExpression="DOB" ReadOnly="True" />
                    <asp:BoundField DataField="Mobile_No" HeaderText="Mobile_No" 
                        SortExpression="Mobile_No" />
                    <asp:BoundField DataField="Country_Name" HeaderText="Country_Name" 
                        SortExpression="Country_Name" />
                    <asp:BoundField DataField="Fee_Status" HeaderText="Fee_Status" 
                        SortExpression="Fee_Status" />
                    <asp:BoundField DataField="BCAS_Approval_Status" 
                        HeaderText="BCAS_Approval_Status" 
                        SortExpression="BCAS_Approval_Status" />
                </Columns>
                <FooterStyle BackColor="#990000" />
                <HeaderStyle BackColor="#990000" ForeColor="White" />
                <AlternatingRowStyle BackColor="#FFFFCC" ForeColor="#333333" />
            </asp:GridView>
            
            
        </td>
        
    </tr>
    <tr>
        <td width="20%" colspan="2" style="width: 95%" align="center"  >     
         
            
            &nbsp;</td>
        
    </tr>
    <tr>
        <td width="20%" colspan="2" style="width: 95%" align="center"  >     
         
            
            <asp:GridView ID="grdPrevCourses" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataPrevCourses" 
                EmptyDataText="No Record Found" BorderColor="#000066" BorderStyle="Outset" 
                    BorderWidth="2px" Caption="Previous Courses Attended" 
                CaptionAlign="Top" ForeColor="#333333"  >
                <Columns>
                    <asp:BoundField DataField="Registration_Number" HeaderText="Registration_Number" 
                        SortExpression="Registration_Number" />
                    <asp:BoundField DataField="Course_Name" 
                        HeaderText="Course_Name" 
                        SortExpression="Course_Name" />
                    <asp:BoundField DataField="DateFrom" HeaderText="DateFrom" 
                        SortExpression="DateFrom" ReadOnly="True" />
                    <asp:BoundField DataField="Date_To" HeaderText="Date_To" 
                        SortExpression="Date_To" ReadOnly="True" />
                    <asp:BoundField DataField="Marks" HeaderText="Marks" 
                        SortExpression="Marks" />
                    <asp:BoundField DataField="Result" HeaderText="Result" 
                        SortExpression="Result" />
                </Columns>
                <FooterStyle BackColor="#990000" />
                <HeaderStyle BackColor="#990000" ForeColor="White" />
                <AlternatingRowStyle BackColor="#FFFFCC" ForeColor="#333333" />
            </asp:GridView>
            
            
        </td>
        
    </tr>
    <tr>
        <td width="20%" colspan="2" style="width: 95%" align="center"  >     
         
            
            &nbsp;</td>
        
    </tr>
    <tr>
        <td width="20%" colspan="2" style="width: 95%" align="center"  >     
         
            
            <asp:GridView ID="grdWorkingExp" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataWorkingExp" 
                EmptyDataText="No Record Found" BorderColor="#000066" BorderStyle="Outset" 
                    BorderWidth="2px" Caption="Working Experience" 
                CaptionAlign="Top" ForeColor="#333333" >
                <Columns>
                    <asp:BoundField DataField="Registration_Number" HeaderText="Registration_Number" 
                        SortExpression="Registration_Number" />
                    <asp:BoundField DataField="Organisation" 
                        HeaderText="Organisation" 
                        SortExpression="Organisation" />
                    <asp:BoundField DataField="Designation" HeaderText="Designation" 
                        SortExpression="Designation" />
                    <asp:BoundField DataField="DateFrom" HeaderText="DateFrom" 
                        SortExpression="DateFrom" ReadOnly="True" />
                    <asp:BoundField DataField="Date_To" HeaderText="Date_To" 
                        SortExpression="Date_To" ReadOnly="True" />
                </Columns>
                <FooterStyle BackColor="#990000" />
                <HeaderStyle BackColor="#990000" ForeColor="White" />
                <AlternatingRowStyle BackColor="#FFFFCC" ForeColor="#333333" />
            </asp:GridView>
            
            
        </td>
        
    </tr>
       <tr>
        <td width="20%" colspan="2" style="width: 95%" align="center" >
        
            &nbsp;</td>
        
    </tr>
    <tr>
        <td width="20%" colspan="2" style="width: 95%" align="center" >
        
               <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="Registration_Number" DataSourceID="SqlDataSource3" 
                EmptyDataText="No Record Found" BorderColor="#999999" BorderStyle="Solid" 
                    BorderWidth="1px" Caption="Candidate Details" 
                CaptionAlign="Top" ForeColor="Blue" BackColor="White" CellPadding="3" 
                   GridLines="Vertical" >
                <Columns>
                    <asp:BoundField DataField="Employee_ID" HeaderText="Employee_ID" 
                        SortExpression="Employee_ID" />
                    <asp:BoundField DataField="Registration_Number" HeaderText="Registration_Number" 
                        SortExpression="Registration_Number" InsertVisible="False" 
                        ReadOnly="True" />
                    <asp:BoundField DataField="Candidate_Name" HeaderText="Candidate_Name" 
                        SortExpression="Candidate_Name" />
                    <asp:BoundField DataField="Sex_Code" HeaderText="Sex_Code" 
                        SortExpression="Sex_Code" />
                    <asp:BoundField DataField="Designation" HeaderText="Designation" 
                        SortExpression="Designation" />
                    <asp:BoundField DataField="Qualification" HeaderText="Qualification" 
                        SortExpression="Qualification" />
                    <asp:BoundField DataField="DOB" HeaderText="DOB" 
                        SortExpression="DOB" ReadOnly="True" />
                    <asp:BoundField DataField="Mobile_No" HeaderText="Mobile_No" 
                        SortExpression="Mobile_No" />
                  <%--  <asp:BoundField DataField="Country_Name" HeaderText="Country_Name" 
                        SortExpression="Country_Name" />--%>
                    <%--<asp:BoundField DataField="Organisation_Name" HeaderText="Organisation_Name" 
                        SortExpression="Organisation_Name" />--%>
                   <%-- <asp:BoundField DataField="BCAS_Approval_Status" 
                        HeaderText="BCAS_Approval_Status" 
                        SortExpression="BCAS_Approval_Status" />--%>
                </Columns>
                <FooterStyle BackColor="#CCCCCC" />
                   <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                   <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#990000" ForeColor="White" />
                <AlternatingRowStyle BackColor="#CCCCCC" />
            </asp:GridView>
            
              <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" SelectCommand="SELECT Candidate_Registration.Employee_ID,
                Candidate_Registration.Registration_Number,Candidate_Registration.Candidate_Name,Candidate_Registration.Sex_Code,
                Designation_Master.Description Designation,Qualification_Master.Description Qualification,
		convert(varchar(10),Candidate_Registration.DOB,103) DOB,Candidate_Registration.Mobile_No
		FROM  Candidate_Registration 
		INNER Join Country_Master on
		Candidate_Registration.Nationality_Code =Country_Master.Country_Code 
		INNER JOIN Designation_Master ON
		Designation_Master.Designation_Code=Candidate_Registration.Designation_Code
		INNER JOIN Qualification_Master ON
		Qualification_Master.Qualification_Code=Candidate_Registration.Educational_Qualification_Code
	
		
		WHERE Candidate_Registration.Registration_Number=@Registration_Number
and Candidate_Registration.Organisation_Code=@OrgCode

">
                <SelectParameters>
                    <asp:ControlParameter ControlID="txtRegdNo" Name="Registration_Number" 
                        PropertyName="Text" />
                    <asp:SessionParameter Name="OrgCode" SessionField="UserOrg" />
                   <%-- <asp:ControlParameter ControlID="ddlSchedule" Name="ScheduleID" 
                        PropertyName="SelectedValue" />--%>
                </SelectParameters>
            </asp:SqlDataSource>
        </td>
        
    </tr>
    <tr>
        <td width="20%" colspan="2" style="width: 95%" align="center"  >     
         
            
            <asp:Label ID="lblError" runat="server" ForeColor="#990000" Visible="False" 
                Font-Bold="False" Font-Names="Arial" Font-Size="X-Large"></asp:Label>
            <asp:Label ID="lblMsg" runat="server" Font-Size="X-Large" ForeColor="#000066" 
                Visible="False"></asp:Label>
        </td>
        
    </tr>
    <tr>
        <td width="20%" colspan="2" style="width: 95%" align="center" >
            <asp:SqlDataSource ID="SqlDataLocation" runat="server" 
                ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" 
                SelectCommand="SELECT [Exam_Location_Code], [Exam_Location_Name] FROM [ExamLocationMaster] order by Exam_Location_Name">
            </asp:SqlDataSource>
         
            
           <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" SelectCommand="SELECT Candidate_Registration.Employee_ID,Candidate_Registration.Registration_Number,Candidate_Registration.Candidate_Name,Candidate_Registration.Sex_Code,Designation_Master.Description Designation,Qualification_Master.Description Qualification,
		convert(varchar(10),Candidate_Registration.DOB,103) DOB,Candidate_Registration.Mobile_No,Country_Master.Country_Name,
		Sponsoring_Organisation_Master.Organisation_Name,
NOMINATIONS_FOR_COURSES.BCAS_Approval_Status 
		FROM  Candidate_Registration 
		INNER Join Country_Master on
		Candidate_Registration.Nationality_Code =Country_Master.Country_Code 
		INNER JOIN Designation_Master ON
		Designation_Master.Designation_Code=Candidate_Registration.Designation_Code
		INNER JOIN Qualification_Master ON
		Qualification_Master.Qualification_Code=Candidate_Registration.Educational_Qualification_Code
		INNER JOIN NOMINATIONS_FOR_COURSES on
		Candidate_Registration.Registration_Number =NOMINATIONS_FOR_COURSES.Registration_No
		INNER JOIN Sponsoring_Organisation_Master on
		NOMINATIONS_FOR_COURSES.Nominating_OrgCode=Sponsoring_Organisation_Master.Organisation_Code 
		WHERE Candidate_Registration.Registration_Number=@Registration_Number
AND NOMINATIONS_FOR_COURSES.Nominating_OrgCode =@OrgCode
AND NOMINATIONS_FOR_COURSES.BCAS_Approval_Status ='Y'
AND NOMINATIONS_FOR_COURSES.Schedule_ID=@ScheduleID">
                <SelectParameters>
                    <asp:ControlParameter ControlID="txtRegdNo" Name="Registration_Number" 
                        PropertyName="Text" />
                    <asp:SessionParameter Name="OrgCode" SessionField="UserOrg" />
                    <asp:ControlParameter ControlID="ddlSchedule" Name="ScheduleID" 
                        PropertyName="SelectedValue" />
                </SelectParameters>
            </asp:SqlDataSource>--%>
            
            
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" SelectCommand="SELECT Candidate_Registration.Employee_ID,
                Candidate_Registration.Registration_Number,Candidate_Registration.Candidate_Name,Candidate_Registration.Sex_Code,
                Designation_Master.Description Designation,Qualification_Master.Description Qualification,
		convert(varchar(10),Candidate_Registration.DOB,103) DOB,Candidate_Registration.Mobile_No,Country_Master.Country_Name,
		NOMINATIONS_FOR_COURSES.Fee_Status,NOMINATIONS_FOR_COURSES.BCAS_Approval_Status 
		
		FROM  Candidate_Registration 
		INNER Join Country_Master on
		Candidate_Registration.Nationality_Code =Country_Master.Country_Code 
		INNER JOIN Designation_Master ON
		Designation_Master.Designation_Code=Candidate_Registration.Designation_Code
		INNER JOIN Qualification_Master ON
		Qualification_Master.Qualification_Code=Candidate_Registration.Educational_Qualification_Code
		INNER JOIN NOMINATIONS_FOR_COURSES on
		Candidate_Registration.Registration_Number =NOMINATIONS_FOR_COURSES.Registration_No
		
		WHERE Candidate_Registration.Registration_Number=@Registration_Number

AND NOMINATIONS_FOR_COURSES.Schedule_ID=@ScheduleID">
                <SelectParameters>
                    <asp:ControlParameter ControlID="txtRegdNo" Name="Registration_Number" 
                        PropertyName="Text" />
                    <asp:SessionParameter Name="OrgCode" SessionField="UserOrg" />
                    <asp:ControlParameter ControlID="ddlSchedule" Name="ScheduleID" 
                        PropertyName="SelectedValue" />
                </SelectParameters>
            </asp:SqlDataSource>
         
            
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" 
                SelectCommand="SELECT     Course_Schedule.Schedule_ID, Course_Master.Course_Name +' - Start DT '+ CONVERT(VARCHAR(10),Course_Schedule.Start_Date,103) + ' - End DT ' + CONVERT(VARCHAR(10),Course_Schedule.End_Date,103) +' - Exam DT '+  CONVERT(VARCHAR(10),Course_Schedule.Date_of_Exam,103) Course_Schedule
FROM         Course_Master INNER JOIN
                      Course_Schedule ON Course_Master.Course_Code = Course_Schedule.Course_Code
                      WHERE Date_of_Exam&gt;=  convert(date,GETDATE ());">
            </asp:SqlDataSource>
           <%-- <asp:SqlDataSource ID="SqlDataPrevCourses" runat="server" 
                ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" SelectCommand="SELECT PCA.Registration_Number, CM.Course_Name, convert(varchar(10),PCA.Date_from,105) DateFrom, 
convert(varchar(10),PCA.Date_To,105) Date_To,PCA.Marks,PCA.Result   
FROM Previous_Courses_attended PCA INNER JOIN Course_Master CM ON
PCA .Course_Code =CM.Course_Code INNER JOIN Candidate_Registration on
Candidate_Registration.Registration_Number=PCA.Registration_Number
INNER JOIN NOMINATIONS_FOR_COURSES ON
Candidate_Registration.Registration_Number=NOMINATIONS_FOR_COURSES.Registration_No
WHERE PCA.Registration_Number=@RegNo
AND NOMINATIONS_FOR_COURSES.Nominating_OrgCode =@OrgCode
AND NOMINATIONS_FOR_COURSES.BCAS_Approval_Status ='Y'
AND NOMINATIONS_FOR_COURSES.Schedule_ID=@ScheduleID">
                <SelectParameters>
                    <asp:ControlParameter ControlID="txtRegdNo" Name="RegNo" PropertyName="Text" />
                    <asp:SessionParameter Name="OrgCode" SessionField="UserOrg" />
                    <asp:ControlParameter ControlID="ddlSchedule" Name="ScheduleID" 
                        PropertyName="SelectedValue" />
                </SelectParameters>
            </asp:SqlDataSource>--%>
            
             <asp:SqlDataSource ID="SqlDataPrevCourses" runat="server" 
                ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" SelectCommand="SELECT PCA.Registration_Number, CM.Course_Name, convert(varchar(10),PCA.Date_from,105) DateFrom, 
convert(varchar(10),PCA.Date_To,105) Date_To,PCA.Marks,PCA.Result   
FROM Previous_Courses_attended PCA INNER JOIN Course_Master CM ON
PCA .Course_Code =CM.Course_Code INNER JOIN Candidate_Registration on
Candidate_Registration.Registration_Number=PCA.Registration_Number
INNER JOIN NOMINATIONS_FOR_COURSES ON
Candidate_Registration.Registration_Number=NOMINATIONS_FOR_COURSES.Registration_No
WHERE PCA.Registration_Number=@RegNo

AND NOMINATIONS_FOR_COURSES.Schedule_ID=@ScheduleID">
                <SelectParameters>
                    <asp:ControlParameter ControlID="txtRegdNo" Name="RegNo" PropertyName="Text" />
                    <asp:SessionParameter Name="OrgCode" SessionField="UserOrg" />
                    <asp:ControlParameter ControlID="ddlSchedule" Name="ScheduleID" 
                        PropertyName="SelectedValue" />
                </SelectParameters>
            </asp:SqlDataSource>
            
            <%--<asp:SqlDataSource ID="SqlDataWorkingExp" runat="server" 
                ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" SelectCommand="SELECT Working_Experience.Registration_Number,Working_Experience.Organisation,
Working_Experience.Designation ,convert(varchar(10),Working_Experience.Date_from,105) DateFrom, 
convert(varchar(10),Working_Experience.Date_To,105) Date_To  
FROM Working_Experience INNER JOIN Candidate_Registration ON
Candidate_Registration.Registration_Number=Working_Experience.Registration_Number 
INNER JOIN NOMINATIONS_FOR_COURSES ON
Candidate_Registration.Registration_Number=NOMINATIONS_FOR_COURSES.Registration_No
WHERE Candidate_Registration.Registration_Number=@RegNo
AND NOMINATIONS_FOR_COURSES.Nominating_OrgCode =@OrgCode
AND NOMINATIONS_FOR_COURSES.BCAS_Approval_Status ='Y'
AND NOMINATIONS_FOR_COURSES.Schedule_ID=@SchedueID">
                <SelectParameters>
                    <asp:ControlParameter ControlID="txtRegdNo" Name="RegNo" PropertyName="Text" />
                    <asp:SessionParameter Name="OrgCode" SessionField="UserOrg" />
                    <asp:ControlParameter ControlID="ddlSchedule" Name="SchedueID" 
                        PropertyName="SelectedValue" />
                </SelectParameters>
            </asp:SqlDataSource>--%>
         <asp:SqlDataSource ID="SqlDataWorkingExp" runat="server" 
                ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" SelectCommand="SELECT Working_Experience.Registration_Number,Working_Experience.Organisation,
Working_Experience.Designation ,convert(varchar(10),Working_Experience.Date_from,105) DateFrom, 
convert(varchar(10),Working_Experience.Date_To,105) Date_To  
FROM Working_Experience INNER JOIN Candidate_Registration ON
Candidate_Registration.Registration_Number=Working_Experience.Registration_Number 
INNER JOIN NOMINATIONS_FOR_COURSES ON
Candidate_Registration.Registration_Number=NOMINATIONS_FOR_COURSES.Registration_No
WHERE Candidate_Registration.Registration_Number=@RegNo

AND NOMINATIONS_FOR_COURSES.Schedule_ID=@SchedueID">
                <SelectParameters>
                    <asp:ControlParameter ControlID="txtRegdNo" Name="RegNo" PropertyName="Text" />
                    <asp:SessionParameter Name="OrgCode" SessionField="UserOrg" />
                    <asp:ControlParameter ControlID="ddlSchedule" Name="SchedueID" 
                        PropertyName="SelectedValue" />
                </SelectParameters>
            </asp:SqlDataSource>
         
            
            <asp:SqlDataSource ID="SqlDataPayeeOrg" runat="server" 
                ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" 
                SelectCommand="SELECT Payee_Organisation_Master.Payee_Organisation_Code ,Payee_Organisation_Master.Payee_Organisation_Name
FROM Payee_Organisation_Master INNER JOIN Payee_Sponsored_Organisation on
Payee_Organisation_Master.Payee_Organisation_Code=Payee_Sponsored_Organisation.Payee_Organisation_Code
WHERE Payee_Sponsored_Organisation.Sponsoring_Organisation_Code =@SponOrgCode
ORDER BY Payee_Organisation_Master.Payee_Organisation_Name">
                <SelectParameters>
                    <asp:SessionParameter Name="SponOrgCode" SessionField="UserOrg" />
                </SelectParameters>
            </asp:SqlDataSource>
        </td>
        
    </tr>
        

    </table>
</asp:Content>

