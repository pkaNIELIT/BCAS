<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="NominationEntryBySO.aspx.cs" Culture="en-GB" Inherits="Forms_DetailsForms_NominationEntryBySO" Title="BCAS-NominationEntryBySO" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table width="100%" align="center">
    <tr>
        <td width="20%" colspan="2" style="width: 95%" align="center" >
         
            
            &nbsp;</td>
        
    </tr>
    <tr>
        <td width="20%" colspan="2" style="width: 95%" align="center" >
         
            
            <asp:Label ID="lblHeading" runat="server" Font-Bold="True" ForeColor="#990000" 
                Text="NOMINATION FORM"></asp:Label>
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
    <tr align="center" >
        <td width="20%" colspan="2" style="width: 95%" >
            <table width="65%" align="center" border="2">
                <tr align="left" >
                    <td width="30%">
            <asp:Label ID="lblCourseSchedule" runat="server" Text="Course Schedule"></asp:Label>
                    </td>
                    <td width="70%">
        
            <asp:DropDownList ID="ddlSchedule" runat="server" 
                DataSourceID="SqlDataSource2" DataTextField="Course_Schedule" 
                DataValueField="Schedule_ID" Width="500px" AppendDataBoundItems="True" 
                            AutoPostBack="True" onselectedindexchanged="ddlSchedule_SelectedIndexChanged" >
                <asp:ListItem Value="0">-Select Course Schedule-</asp:ListItem>
            </asp:DropDownList>
            
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="*" ControlToValidate ="ddlSchedule" SetFocusOnError="true" InitialValue="0" ></asp:RequiredFieldValidator>
            
                    </td>
                </tr>
                 <tr align="left">
                    <td width="30%">
            <asp:Label ID="lblSearch" runat="server" Text="Registration Number"></asp:Label>
                    </td>
                    <td width="70%">
        
            <asp:TextBox ID="txtRegNo" runat="server" Width="250px" MaxLength="8" TabIndex="3"></asp:TextBox>
                
                <cc1:FilteredTextBoxExtender ID="fte_RegNo_no" runat="server" Enabled="True" TargetControlID="txtRegNo"
                                                            ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                        
            <asp:RequiredFieldValidator ID="rfvRegNo" runat="server" 
                ControlToValidate="txtRegNo" ErrorMessage="*"></asp:RequiredFieldValidator>
                
                    </td>
                </tr>
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
                 <tr align="left">
                    <td width="30%">
            <asp:Label ID="lblLocation" runat="server" Text="Exam Location"></asp:Label>
                    </td>
                    <td width="70%">
        
            <asp:DropDownList ID="ddlExamLocation" runat="server" 
                Width="250px" TabIndex="4">
                <asp:ListItem Value="0">-- Select Location --</asp:ListItem>
            </asp:DropDownList>
            <asp:RegularExpressionValidator ID="rev_Location" runat="server" ControlToValidate="ddlExamLocation"
                                            ErrorMessage="*" SetFocusOnError="True" ValidationExpression="^[1-9[0-9][0-9]*" ></asp:RegularExpressionValidator>
            
           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ErrorMessage="*" ControlToValidate ="ddlExamLocation" SetFocusOnError="true" InitialValue="0" ></asp:RequiredFieldValidator>--%>
                        
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ErrorMessage="*" ControlToValidate="ddlExamLocation" InitialValue="0"></asp:RequiredFieldValidator>--%>
        
                    </td>
                </tr>
                 <tr align="left">
                    <td width="30%">
                    </td>
                    <td width="70%">
        
            <asp:Button ID="btnDisplay" runat="server" Text="Get Details" Width="250px" 
                onclick="btnDisplay_Click" TabIndex="5" />
        
            <asp:Button ID="btnSaveNomination" runat="server" Text="Nominate" 
                Width="250px" onclick="btnSaveNomination_Click" TabIndex="6" />
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
            </div>     Added On Rowdatabound by madhur--%>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="Registration_Number" DataSourceID="SqlDataSource1" 
                EmptyDataText="No Record Found" BorderColor="#000066" BorderStyle="Outset" 
                    BorderWidth="2px" Caption="Candidate Personal Details" 
                CaptionAlign="Top" ForeColor="#333333" OnRowDataBound="GridView1_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="Employee_ID" HeaderText="Pass ID" 
                        SortExpression="Employee_ID" />
                    <asp:BoundField DataField="Registration_Number" 
                        HeaderText="Registration No" InsertVisible="False" ReadOnly="True" 
                        SortExpression="Registration_Number" />
                    <asp:BoundField DataField="Candidate_Name" HeaderText="Name" 
                        SortExpression="Candidate_Name" />
                    <asp:BoundField DataField="Sex_Code" HeaderText="Sex" 
                        SortExpression="Sex_Code" />
                    <asp:BoundField DataField="Designation" HeaderText="Designation" 
                        SortExpression="Designation" />
                    <asp:BoundField DataField="Qualification" HeaderText="Qualification" 
                        SortExpression="Qualification" />
                    <asp:BoundField DataField="DOB" HeaderText="DOB" ReadOnly="True" 
                        SortExpression="DOB" />
                    <asp:BoundField DataField="Mobile_No" HeaderText="Mobile No" 
                        SortExpression="Mobile_No" />
                    <asp:BoundField DataField="Country_Name" HeaderText="Country" 
                        SortExpression="Country_Name" />
                    <asp:BoundField DataField="Organisation_Name" HeaderText="Sponsoring Organisation" 
                        SortExpression="Organisation_Name" />
                    <asp:BoundField DataField="BillerOrganisation_Code" HeaderText="Payee Organisation" />
                </Columns>
                <FooterStyle BackColor="#990000" />
                <HeaderStyle BackColor="#990000" ForeColor="White" />
                <AlternatingRowStyle BackColor="#FFFFCC" ForeColor="#333333" />
            </asp:GridView>
             <br />
                <asp:Button ID="GridViewBtnEdit" runat="server" CommandName="Edit" Text="Add Payee Organization" Visible="false" OnClick="GridViewBtnEdit_Click" />
            
            
        </td>
        
    </tr>
    <tr>
        <td width="100%"   align="center"  >  
            <!-- Added code for Edit Payee Madhur-->   
         <div id="divBilling" runat="server">
                    <asp:Label ID="lblBillingOrganisation" runat="server">Payee Organisation Name</asp:Label>
                    <font color="red">*</font>:
                    <asp:DropDownList ID="ddl_BillerOrgCode" runat="server" DataSourceID="SqlDataSource3" AppendDataBoundItems="true"
                        DataTextField="Payee_Organisation_Name" DataValueField="Payee_Organisation_Code" TabIndex="1"
                        Width="300px" Enabled="true">
                        <asp:ListItem>Select</asp:ListItem>

                    </asp:DropDownList>
                    <asp:Button ID="btnUpdatePayee" runat="server" CommandName="Update" Text="Update Payee Organisation" OnClick="btnUpdatePayee_Click"  />
                </div>
            </td>
            <td width="10%" colspan="2" style="width: 40%" align="center">


                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>"
                    SelectCommand="SELECT [Payee_Organisation_Code], [Payee_Organisation_Name] 
                     FROM [Payee_Organisation_Master]">
                    <SelectParameters>
                        <asp:SessionParameter Name="OrgCode" SessionField="UserOrg" />
                    </SelectParameters>
                </asp:SqlDataSource>

            <!-- till here-->
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
        <td width="20%" colspan="2" style="width: 95%" align="center"  >     
         
            
            &nbsp;</td>
        
    </tr>
    <tr>
        <td width="20%" colspan="2" style="width: 95%" align="center" >
            <asp:SqlDataSource ID="SqlDataLocation" runat="server" 
                ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" 
                
                SelectCommand="SELECT [Exam_Location_Code], [Exam_Location_Name] FROM [ExamLocationMaster]">
            </asp:SqlDataSource>
         
            
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" SelectCommand="SELECT Candidate_Registration.Employee_ID,Candidate_Registration.Registration_Number,Candidate_Registration.Candidate_Name,Candidate_Registration.Sex_Code,Designation_Master.Description Designation,Qualification_Master.Description Qualification,
		convert(varchar(10),Candidate_Registration.DOB,103) DOB,Candidate_Registration.Mobile_No,Country_Master.Country_Name,
		Sponsoring_Organisation_Master.Organisation_Name ,ISNull((select Payee_Organisation_Name from Payee_Organisation_Master 
		where Payee_Organisation_Code =candidate_registration.BillerOrganisation_Code ),0) as BillerOrganisation_Code
		FROM  Candidate_Registration INNER JOIN Sponsoring_Organisation_Master on
		Candidate_Registration.Organisation_Code =Sponsoring_Organisation_Master.Organisation_Code
		INNER Join Country_Master on
		Candidate_Registration.Nationality_Code =Country_Master.Country_Code 
		INNER JOIN Designation_Master ON
		Designation_Master.Designation_Code=Candidate_Registration.Designation_Code
		INNER JOIN Qualification_Master ON
		Qualification_Master.Qualification_Code=Candidate_Registration.Educational_Qualification_Code
		WHERE Candidate_Registration.Registration_Number=@Registration_Number
AND Candidate_Registration.Organisation_Code=@OrgCode">
                <SelectParameters>
                    <asp:ControlParameter ControlID="txtRegNo" Name="Registration_Number" 
                        PropertyName="Text" />
                    <asp:SessionParameter Name="OrgCode" SessionField="UserOrg" />
                </SelectParameters>
            </asp:SqlDataSource>
         
             
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" 
                SelectCommand="SELECT     Course_Schedule.Schedule_ID, Course_Master.Course_Name +' - Start DT '+ CONVERT(VARCHAR(10),Course_Schedule.Start_Date,103) + ' - End DT ' + CONVERT(VARCHAR(10),Course_Schedule.End_Date,103) +' - Exam DT '+  CONVERT(VARCHAR(10),Course_Schedule.Date_of_Exam,103) Course_Schedule
FROM         Course_Master INNER JOIN
                      Course_Schedule ON Course_Master.Course_Code = Course_Schedule.Course_Code
                      WHERE Date_of_Exam  &gt;=  convert(date,GETDATE ()) and Course_Master.Course_Code in(1,2);">
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataPrevCourses" runat="server" 
                ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" SelectCommand="SELECT PCA.Registration_Number, CM.Course_Name, convert(varchar(10),PCA.Date_from,105) DateFrom, 
convert(varchar(10),PCA.Date_To,105) Date_To,PCA.Marks,PCA.Result   
FROM Previous_Courses_attended PCA INNER JOIN Course_Master CM ON
PCA .Course_Code =CM.Course_Code INNER JOIN Candidate_Registration on
Candidate_Registration.Registration_Number=PCA.Registration_Number
WHERE PCA.Registration_Number=@RegNo
AND Candidate_Registration.Organisation_Code =@OrgCode 

">
                <SelectParameters>
                    <asp:ControlParameter ControlID="txtRegNo" Name="RegNo" PropertyName="Text" />
                    <asp:SessionParameter Name="OrgCode" SessionField="UserOrg" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataWorkingExp" runat="server" 
                ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" SelectCommand="SELECT Working_Experience.Registration_Number,Working_Experience.Organisation,
Working_Experience.Designation ,convert(varchar(10),Working_Experience.Date_from,105) DateFrom, 
convert(varchar(10),Working_Experience.Date_To,105) Date_To  
FROM Working_Experience INNER JOIN Candidate_Registration ON
Candidate_Registration.Registration_Number=Working_Experience.Registration_Number 
WHERE Candidate_Registration.Registration_Number=@RegNo
AND Candidate_Registration.Organisation_Code =@OrgCode">
                <SelectParameters>
                    <asp:ControlParameter ControlID="txtRegNo" Name="RegNo" PropertyName="Text" />
                    <asp:SessionParameter Name="OrgCode" SessionField="UserOrg" />
                </SelectParameters>
            </asp:SqlDataSource>
        </td>
        
    </tr>
    <tr>
       
        <td width="75%" colspan ="2">
        
            <asp:Label ID="lblError" runat="server" ForeColor="#990000" Visible="False"></asp:Label>
        </td>
    </tr>
    

    </table>
</asp:Content>

