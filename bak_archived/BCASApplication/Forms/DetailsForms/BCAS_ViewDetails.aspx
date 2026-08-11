<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="BCAS_ViewDetails.aspx.cs" Culture="en-GB" Inherits="Forms_DetailsForms_BCAS_ViewDetails" Title="BCAS-->ViewDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 95%;
            height: 21px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <table width="90%" align="center">
    <tr>
        <td width="20%" colspan="2" style="width: 95%" align="center" >
         
            
            &nbsp;</td>
        
    </tr>
    <tr>
        <td width="20%">
            &nbsp;</td>
        <td width="75%">
        
            &nbsp;</td>
        
    </tr>
    <tr>
        <td COLSPAN=2 align="center">
        
            
            <asp:Label ID="lblHeading" runat="server" Font-Bold="True" ForeColor="#990000" 
                Text="BCAS View Details"></asp:Label>
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
                        
            
        </td>
        
    </tr>
    <tr>
        <td width="20%" colspan="2" style="width: 95%" align="center"  >     
         
            
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="Registration_Number" DataSourceID="SqlDataSource1" 
                EmptyDataText="No Record Found" BorderColor="#000066" BorderStyle="Outset" 
                    BorderWidth="2px" Caption="Candidate Personal Details" 
                CaptionAlign="Top" ForeColor="#333333" >
                <Columns>
                    <asp:BoundField DataField="Employee_ID" HeaderText="Pass ID" 
                        SortExpression="Employee_ID" />
                    <asp:BoundField DataField="Registration_Number" 
                        HeaderText="Registration_Number" InsertVisible="False" ReadOnly="True" 
                        SortExpression="Regd. No" />
                    <asp:BoundField DataField="Candidate_Name" HeaderText="Candidate_Name" 
                        SortExpression="Name" />
                    <asp:BoundField DataField="Sex_Code" HeaderText="Sex_Code" 
                        SortExpression="Sex" />
                    <asp:BoundField DataField="Designation" HeaderText="Designation" 
                        SortExpression="Designation" />
                    <asp:BoundField DataField="Qualification" HeaderText="Qualification" 
                        SortExpression="Qualification" />
                    <asp:BoundField DataField="DOB" HeaderText="DOB" ReadOnly="True" 
                        SortExpression="DOB" />
                    <asp:BoundField DataField="Mobile_No" HeaderText="Mobile_No" 
                        SortExpression="Mobile No" />
                    <asp:BoundField DataField="Country_Name" HeaderText="Country_Name" 
                        SortExpression="Country" />
                    <asp:BoundField DataField="Organisation_Name" HeaderText="Organisation_Name" 
                        SortExpression="Organisation" />
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
        <td width="20%" colspan="2" align="center" class="style1"  >     
         
            
            </td>
        
    </tr>
    <tr>
        <td width="20%" colspan="2" style="width: 95%" align="center"  >     
         
            
            <asp:GridView ID="grdPrevCourses" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataPrevCourses" 
                EmptyDataText="No Record Found" BorderColor="#000066" BorderStyle="Outset" 
                    BorderWidth="2px" Caption="Previous Courses Attended" 
                CaptionAlign="Top" ForeColor="#333333"  >
                <Columns>
                    <asp:BoundField DataField="Registration_Number" HeaderText="Regd. No" 
                        SortExpression="Registration_Number" />
                    <asp:BoundField DataField="Course_Name" 
                        HeaderText="Course Name" 
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
         
            
            &nbsp;</td>
        
    </tr>
    <tr>
        <td width="20%" colspan="2" style="width: 95%" align="center"  >     
         
            
            <asp:GridView ID="grdWorkingExp" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataWorkingExp" 
                EmptyDataText="No Record Found" BorderColor="#000066" BorderStyle="Outset" 
                    BorderWidth="2px" Caption="Working Experience" 
                CaptionAlign="Top" ForeColor="#333333" >
                <Columns>
                    <asp:BoundField DataField="Registration_Number" HeaderText="Regd. No." 
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
         
            
            <asp:Label ID="lblError" runat="server" ForeColor="#990000" Visible="False"></asp:Label>
        </td>
        
    </tr>
    <tr>
        <td width="20%" colspan="2" style="width: 95%" align="center" >
         
            
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" SelectCommand="SELECT Candidate_Registration.Employee_ID,Candidate_Registration.Registration_Number,Candidate_Registration.Candidate_Name,Candidate_Registration.Sex_Code,Designation_Master.Description Designation,Qualification_Master.Description Qualification,
		convert(varchar(10),Candidate_Registration.DOB,103) DOB,Candidate_Registration.Mobile_No,Country_Master.Country_Name,
		Sponsoring_Organisation_Master.Organisation_Name 
		FROM  Candidate_Registration INNER JOIN Sponsoring_Organisation_Master on
		Candidate_Registration.Organisation_Code =Sponsoring_Organisation_Master.Organisation_Code
		INNER Join Country_Master on
		Candidate_Registration.Nationality_Code =Country_Master.Country_Code 
		INNER JOIN Designation_Master ON
		Designation_Master.Designation_Code=Candidate_Registration.Designation_Code
		INNER JOIN Qualification_Master ON
		Qualification_Master.Qualification_Code=Candidate_Registration.Educational_Qualification_Code
		WHERE Candidate_Registration.Registration_Number=@Registration_Number
">
                <SelectParameters>
                    <asp:QueryStringParameter Name="Registration_Number" 
                        QueryStringField="Regd_No" />
                </SelectParameters>
            </asp:SqlDataSource>
         
            
            <asp:SqlDataSource ID="SqlDataPrevCourses" runat="server" 
                ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" SelectCommand="SELECT PCA.Registration_Number, CM.Course_Name, convert(varchar(10),PCA.Date_from,105) DateFrom, 
convert(varchar(10),PCA.Date_To,105) Date_To,PCA.Marks,PCA.Result   
FROM Previous_Courses_attended PCA INNER JOIN Course_Master CM ON
PCA .Course_Code =CM.Course_Code INNER JOIN Candidate_Registration on
Candidate_Registration.Registration_Number=PCA.Registration_Number
WHERE PCA.Registration_Number=@RegNo


">
                <SelectParameters>
                    <asp:QueryStringParameter Name="RegNo" QueryStringField="Regd_No" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataWorkingExp" runat="server" 
                ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" SelectCommand="SELECT Working_Experience.Registration_Number,Working_Experience.Organisation,
Working_Experience.Designation ,convert(varchar(10),Working_Experience.Date_from,105) DateFrom, 
convert(varchar(10),Working_Experience.Date_To,105) Date_To  
FROM Working_Experience INNER JOIN Candidate_Registration ON
Candidate_Registration.Registration_Number=Working_Experience.Registration_Number 
WHERE Candidate_Registration.Registration_Number=@RegNo
">
                <SelectParameters>
                    <asp:QueryStringParameter Name="RegNo" QueryStringField="Regd_No" />
                </SelectParameters>
            </asp:SqlDataSource>
         
            
            </td>
        
    </tr>
    <tr>
        <td width="20%">
            &nbsp;</td>
        <td width="75%">
        
            &nbsp;</td>
    </tr>
    

    </table>
</asp:Content>

