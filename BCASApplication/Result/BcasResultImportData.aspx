<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="BcasResultImportData.aspx.cs" Culture="en-GB" Inherits="Result_BcasResultImportData"
    Title="BCAS-->BcasResultImportData" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

 <table align="center" width="100%">
        
              <tr>
            <td align="center" height="150px">
                <table width="60%" border="5" bordercolor="#CEEDEE" align="center">
                    <tr valign="middle">
                        <td width="25%" align="right">
                            &nbsp;
                        </td>
                        <td align="left">
                            <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Times New Roman"
                                Font-Size="X-Large" ForeColor="#990033" Text="Please Select Excel File" 
                                EnableTheming="False"></asp:Label>
                        </td>
                    </tr>
                    <tr valign="middle">
                        <td width="25%" align="right">
                            <asp:Label ID="UserNameLabel" runat="server" >Result_File: </asp:Label>
                        </td>
                        <td align="left">
                            <asp:FileUpload ID="FileUpload1" runat="server" Width="400px" />
                    
                        </td>
                    </tr>
                    <tr valign="middle">
                        <td width="25%">
                            &nbsp;
                        </td>
                        <td align="left">
                          <asp:Button ID="Button1" Text="File Upload" OnClick = "Upload" runat="server" />
                            <asp:Label ID="lblMessage" runat="server" Font-Bold="True" ForeColor="#009933"></asp:Label><br />
                            
                        </td>
                    </tr>
                    <tr>
                    <td width="100%" colspan="2">
                    &nbsp;
                    </tr>
                    <tr >
                        <td width="100%" colspan="2">
                            <asp:Panel ID="Panel1" runat="server" Visible="False">
                             
                           <tr align="left">
                    <td width="25%">
            <asp:Label ID="lblCourseSchedule" runat="server" Text="Exam Date :  "></asp:Label>
                    </td>
                     <td width="75%">
        
            <asp:DropDownList ID="ddlSchedule" runat="server" 
                DataSourceID="SqlDataSource2" DataTextField="Course_Schedule" 
                DataValueField="Schedule_ID" Width="150px" AppendDataBoundItems="True" 
                             AutoPostBack="True" 
                             onselectedindexchanged="ddlSchedule_SelectedIndexChanged" Font-Bold="True" >
                <asp:ListItem Value="0">-Select ExamDate-</asp:ListItem>
            </asp:DropDownList>
            
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="*" ControlToValidate ="ddlSchedule" SetFocusOnError="true" InitialValue="0" ></asp:RequiredFieldValidator>
            
                    </td>
                </tr> <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" 
                SelectCommand="SELECT top 2 Course_Schedule.Schedule_ID,+  CONVERT(VARCHAR(10),Course_Schedule.Date_of_Exam,103) Course_Schedule
FROM Course_Master INNER JOIN Course_Schedule ON Course_Master.Course_Code = Course_Schedule.Course_Code order by Schedule_ID desc">
            </asp:SqlDataSource>
           
           <tr valign="middle">
                        <td width="100%" colspan="2" align="center">
                       
                            <asp:HiddenField ID="HiddenCountCandiate0" runat="server" />
                            <asp:HiddenField ID="HiddenCountCandiate" runat="server" />
                            <asp:Button ID="Finalize" runat="server" Text="Import Data" Enabled="False" 
                                Font-Bold="True" Font-Size="Small" onclick="Finalize_Click" />
                                
                                <%--  <asp:LinkButton ID="OnLnkDownload" runat="server" OnClick="OnLnkDownload_Click" Font-Underline="False">Download</asp:LinkButton>
                                <asp:Label ID="lblFilename" runat="server" Text="Browse:"></asp:Label>--%>
                               <%-- <asp:LinkButton ID="lnkDownload" Text = "Download" CommandArgument = '<%# Eval("Value") %>' runat="server" OnClick = "DownloadFile"></asp:LinkButton>--%>&nbsp;&nbsp;&nbsp;
                                
                                <asp:Label ID="lblMessage1" runat="server" Font-Bold="True" 
                                ForeColor="#009933"></asp:Label>
                            <asp:Label ID="lblMessage0" runat="server" Font-Bold="True" ForeColor="#009933"></asp:Label>
                            </td>
                    </tr>
           
           
                                
           
           
            </asp:Panel>
            </td>
                    </tr>
                    
                </table>
            </td>
        </tr>
      <tr><td> <a href="../Files/BCAS_ResultSheet_Format.xlsx">Click for BCAS Result Sheet Format</a></td></tr>

 </table>
 
 
</asp:Content>
