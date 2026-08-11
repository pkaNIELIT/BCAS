<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ExamDateExtension.aspx.cs" Inherits="Forms_DetailsForms_ExamDateExtension"
    Title="BCAS-->Exam Date Extension" Culture="en-GB"  uiculture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../Script/aes.js" type="text/javascript"></script>
<script src="../../Script/jQuery.js" type="text/javascript"></script>

    <table border="0" align="center" width="100%" height="75%">
        <tr align="center">
            <td valign="top" align="center">
                <table border="0" align="center" width="90%" height="100%">
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="5" width="100%" bordercolor="#ECC4B9"
                                bgcolor="#FBEBE6" align="center">
                                
                                <tr>
                                    <td colspan="2" align="center">
                                        <strong><font color="#990000">Last Date Extension For a Perticular Exam Session </font></strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <hr color="#990000" size="1">
                                    </td>
                                </tr>
                               
                                <tr>
                                    <td align="left">
                                        &nbsp;</td>
                                    <td align="left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lblCourse" runat="server" DataTextField="Course_Name" 
                                        DataValueField="Course_Code" >Select Course</asp:Label>
                                        <font color="red">*</font>:
                                    </td>
                                    <td align="left">
                                         <asp:DropDownList ID="ddlExistingCourse" runat="server" DataTextField="Course_Name" 
                                         DataValueField="Course_Code" OnSelectedIndexChanged="ddlExistingCourse_SelectedIndexChanged" AutoPostBack="true" Width="500px" 
                                          AppendDataBoundItems="True" >
                                             <asp:ListItem Value="0">-Select Course--</asp:ListItem>
                                         </asp:DropDownList>
                                    
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="left">
                                        &nbsp;</td>
                                    <td align="left">
                                        &nbsp;</td>
                                </tr>
                                  <tr>
                                    <td align="left">
                                        <asp:Label ID="lblBillingOrganisation" runat="server" >Select Exam Schedule Start Date</asp:Label>
                                        <font color="red">*</font>:
                                    </td>
                                    <td align="left">
                                      <asp:DropDownList ID="ddlSchedule" runat="server" 
                                        DataTextField="start_date" 
                                        DataValueField="start_date" Width="500px" AppendDataBoundItems="True" >
                                        <asp:ListItem Value="0">-Select Start Date--</asp:ListItem>
                                     </asp:DropDownList>
                                       
                                       
                                    </td>
                                </tr>

                                <tr>
                                    <td align="left">
                                        &nbsp;</td>
                                    <td align="left">
                                        &nbsp;</td>
                                </tr>
                              
                                      <tr>
                                         <td align="left">
                                             <asp:Label ID="Label1" runat="server" >Select New Date For Extension</asp:Label>
                                             <font color="red">*</font>:
                                        </td>
                                         <td align="left">
                                        <asp:Calendar ID="Calendar1" runat="server"  ></asp:Calendar>
                                     </td>
                                    </tr>

                                <tr>
                                    <td align="left">
                                        &nbsp;</td>
                                    <td align="left">
                                        &nbsp;</td>
                                </tr>

                                <tr> 
                                    <td align="left"> </td>
                                    <td align="left">
                                        <asp:Button ID="SubmitButton" runat="server" Text="Update" OnClick="SubmitButton_Click" />
                                        <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                               
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>

            

    </table>
</asp:Content>
