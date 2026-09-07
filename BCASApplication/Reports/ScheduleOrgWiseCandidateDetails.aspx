
<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ScheduleOrgWiseCandidateDetails.aspx.cs" Inherits="Reports_ScheduleOrgWiseCandidateDetails" Title="BCAS-->ScheduleOrgWiseCandidateDetails" culture="en-GB"%>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="95%" align="center" >
    <tr>
        <td>        
        </td>       
    </tr>
    <tr align="center">
        <td>        
               
            <asp:Label ID="lblHeading" runat="server" Font-Bold="True" Font-Size="Large" 
                ForeColor="#000099"></asp:Label>
               
        </td>       
    </tr>
    <tr align="center">
        <td align="center">        
        
            <table width="70%" align="center" border="2">
                <tr >
                    <td width="30%">
            <asp:Label ID="lblCourseSchedule" runat="server" Text="Course Schedule"></asp:Label>
                    </td>
                     <td width="70%">
        
            <asp:DropDownList ID="ddlSchedule" runat="server" 
                DataSourceID="SqlDataSource2" DataTextField="Course_Schedule" 
                DataValueField="Schedule_ID" Width="500px" AppendDataBoundItems="True" >
                <asp:ListItem Value="0">-Select Course Schedule-</asp:ListItem>
            </asp:DropDownList>
            
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="*" ControlToValidate ="ddlSchedule" SetFocusOnError="true" InitialValue="0" ></asp:RequiredFieldValidator>
            
                    </td>
                </tr>
                <tr>
                    <td><asp:Label ID="lblReportOption" runat="server" Text="Report By"></asp:Label></td>
                    <td><asp:RadioButtonList ID="rdoOption" runat ="server" AutoPostBack ="true" CausesValidation ="false" OnSelectedIndexChanged="rdoOption_SelectedIndexChanged" RepeatDirection="Horizontal" Width="356px" >
                        <asp:ListItem Value="S">Sponsoring Organisation</asp:ListItem>
                        <asp:ListItem Value="L">Location</asp:ListItem>
                        </asp:RadioButtonList></td>

                </tr>
            <tr id="trSpons" runat ="server" visible="false" >
                <td width="30%"><asp:Label ID="lblSpons" runat="server" 
                                Text="Sponsoring Organisation"></asp:Label></td>
                <td width="70%">
                    <asp:DropDownList ID="ddlOrganisation" runat="server" Width="300px" DataTextField="Organisation_Name" 
                     DataValueField="Organisation_Code"                 
                    AppendDataBoundItems="True"  >               
                    <asp:ListItem Value="0">--Select Sponsoring Organisation--</asp:ListItem>
                    <asp:ListItem Value="999999">GET ALL ORGANISATIONS</asp:ListItem>
                </asp:DropDownList>
            
                </td>
            </tr>

                  <tr id="trLocation" runat ="server" visible="false" >
                <td width="30%"><asp:Label ID="lblLocation" runat="server" 
                                Text="Location"></asp:Label></td>
                <td width="70%">
                    <asp:DropDownList ID="ddlExamLocation" runat="server" Width="300px" DataTextField="Exam_Location_Name" 
                     DataValueField="Exam_Location_Code"                 
                    AppendDataBoundItems="True"  >               
                    <asp:ListItem Value="0">--Select Location--</asp:ListItem>
                    
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
                      Order By Course_Schedule.Date_of_Exam desc">
            </asp:SqlDataSource>
                    </td>
                     
                     <td width="70%">
        
            <asp:Button ID="btnDisplay" runat="server" Text="Get Data" Width="250px" 
                onclick="btnDisplay_Click" TabIndex="4" />
        
                    </td>
                </tr>
            
            </table>
        </td>       
    </tr>
    <tr>
        <td>        
           </td>       
    </tr>
    <tr>
        <td align="center" Height="50px" valign="top" colspan ="2" >      
            <asp:ImageButton ID="imgpdf" runat="server" ImageUrl ="~/Images/pdf.jpg" width="1%" Height ="50%"  OnClick ="imgPDF_Click"  Visible ="false" /> &nbsp;&nbsp;
            <asp:ImageButton ID="imgExcel" runat="server" ImageUrl ="~/Images/Export_Exl.jpg" OnClick ="imgExcel_Click" visible="false" />
   
              &nbsp;</td> 
        </tr>
    <tr>
        <td align="left" colspan ="2"><div id="divReportData" runat="server" style="width:100%;"></div>
</td>      
    </tr>
   
    </table>
</asp:Content>

