<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="repNominationAccepted.aspx.cs" Inherits="Reports_repNominationAccepted" Title="BCAS-->NominationAccepted" culture="en-GB"%>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table width="95%" align="center" >
    <tr>
        <td>        
        </td>       
    </tr>
    <tr>
        <td align="center">        
               
            &nbsp;</td>       
    </tr>
    <tr>
        <td align="center">        
               
            <asp:Label ID="lblHeading" runat="server" Font-Bold="True" Font-Size="Large" 
                ForeColor="#000099"></asp:Label>
               
        </td>       
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
                DataValueField="Schedule_ID" Width="500px" AppendDataBoundItems="True" >
                <asp:ListItem Value="0">-Select Course Schedule-</asp:ListItem>
            </asp:DropDownList>
            
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="*" ControlToValidate ="ddlSchedule" SetFocusOnError="true" InitialValue="0" ></asp:RequiredFieldValidator>
            
                    </td>
                </tr>
            
               <tr align="left">
                        <td width="30%">
                            <asp:Label ID="lblCourseSchedule0" runat="server" 
                                Text="Sponsoring Organisation"></asp:Label>
                        </td>
                        <td width="70%">
                             <asp:DropDownList ID="ddlOrganisation" runat="server" Width="300px" DataTextField="Organisation_Name" 
                DataValueField="Organisation_Code"                 
                AppendDataBoundItems="True"  >               
                <asp:ListItem Value="0">--Select Sponsoring Organisation--</asp:ListItem>
                                 <asp:ListItem Value="999999">GET ALL ORGANISATIONS</asp:ListItem>
            </asp:DropDownList>
            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ErrorMessage="*" InitialValue="0" SetFocusOnError="True" 
                                 ControlToValidate="ddlOrganisation"></asp:RequiredFieldValidator>
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
    
    <tr align="center" valign="top" >
        <td align="center" >      
   
             &nbsp;</td>       
    </tr>
    
    <tr align="center" valign="top" >
        <td align="center" >      
   
             </td>       
    </tr>
    
    

    <tr>
        <td align="center" Height="50px" valign="top" colspan ="2" >      
            <asp:ImageButton ID="imgpdf" runat="server" ImageUrl ="~/Images/pdf.jpg" width="6%" Height ="47%"  OnClick ="imgPDF_Click"  Visible ="false" /> &nbsp;&nbsp;
            <asp:ImageButton ID="imgExcel" runat="server" ImageUrl ="~/Images/Export_Exl.jpg" width="5%" Height ="55%" OnClick ="imgExcel_Click" visible="false" />
   
              &nbsp;</td> 
        </tr>
    <tr>
        <td align="left" colspan ="2"><div id="divReportData" runat="server" style="width:100%;"></div>
</td>      
    </tr>
  
    </table>
</asp:Content>

