<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Add_AadhaarNumber.aspx.cs" Culture="en-GB" Inherits="Forms_DetailsForms_Add_AadhaarNumber"
    Title="BCAS-->UpdateAadhaarDetails" %>
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
                                Font-Size="X-Large" ForeColor="#990033" Text="Enter Registration Number" 
                                EnableTheming="False"></asp:Label>
                        </td>
                    </tr>
                    <tr valign="middle">
                        <td width="25%" align="right">
                            <asp:Label ID="UserNameLabel" runat="server" >Registration No:</asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtRegNo" runat="server" Width="220px" MaxLength="8" 
                                AutoCompleteType="Disabled" ></asp:TextBox>
                            
                            <asp:RequiredFieldValidator ID="RegNoRequired" runat="server" ControlToValidate="txtRegNo"
                                ErrorMessage="*" ToolTip="Registration No is required." SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr valign="middle">
                        <td width="25%">
                            &nbsp;
                        </td>
                        <td align="left">
                            <asp:Button ID="LoginButton" runat="server" Font-Bold="False"
                                Style="text-align: left; font-weight: 700;" Text="View Details" 
                                OnClick="LoginButton_Click" />
                        </td>
                    </tr>
                    <tr valign="middle">
                        <td width="25%" colspan="2">
                            <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#CC0000"
                                Visible="False" EnableTheming="false"></asp:Label>
                        </td>
                    </tr>
                    
                </table>
            </td>
        </tr>
       <tr>
            <td  align="center" colspan="2">
                <asp:Panel ID="Panel1" runat="server" Enabled="true"  Visible="false"> 

    ***** Candidate Details *****
    <table style="height: 70%; width: auto;" border="1" align="center">
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_organisation_code" runat="server" >Organisation Name</asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                <asp:TextBox ID="txt_organisation_Name" runat="server" Width="375px" 
                                                        MaxLength="400" Enabled="False" ReadOnly="True"></asp:TextBox>
                                                                                                       
                                                   
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_employee_id" runat="server" >Pass ID</asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:TextBox ID="txt_employee_id" runat="server" Width="175px" MaxLength="12" 
                                                        Enabled="False" ReadOnly="True"></asp:TextBox>
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_candidate_name" runat="server" >Candidate Name</asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:TextBox ID="txt_candidate_name" runat="server" Width="375px" 
                                                        MaxLength="100" ReadOnly="True" Enabled="False"></asp:TextBox>
                                                   
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_sex_code" runat="server" >Sex</asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                 
                                                    <asp:TextBox ID="txt_sex" runat="server" Width="75px" MaxLength="7" 
                                                        Enabled="False" ReadOnly="True"></asp:TextBox>
                                                                                                                                  
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_designation_code" runat="server" >Designation</asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                                                                       
                                       <asp:TextBox ID="txt_designation" runat="server" Width="275px" MaxLength="100" 
                                                        Enabled="False" ReadOnly="True"></asp:TextBox>                                        
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_dob" runat="server" >DOB</asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:TextBox ID="txt_dob" runat="server" Width="130px" MaxLength="10" 
                                                        ReadOnly="True" Enabled="False"></asp:TextBox>
                                                   
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_email_id" runat="server" >Email Id</asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:TextBox ID="txt_email_id" runat="server" Width="355px" MaxLength="50" 
                                                        ReadOnly="True" Enabled="False"></asp:TextBox>
                                                                                                       
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_mobile_no" runat="server" >Mobile No</asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:TextBox ID="txt_mobile_no" runat="server" Width="130px" MaxLength="10" 
                                                        Enabled="False" ReadOnly="True"></asp:TextBox>
                                                                                                        
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_nationality_code" runat="server" >Nationality</asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                <asp:TextBox ID="txt_nationality" runat="server" Width="120px" MaxLength="12" 
                                                        Enabled="False" ReadOnly="True"></asp:TextBox>
                                        
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_educational_qualification_code" runat="server" >Educational Qualification</asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                       
                                       <asp:TextBox ID="txt_educational_qualification" runat="server" Width="325px" MaxLength="50" 
                                                        Enabled="False" ReadOnly="True"></asp:TextBox>                                                                                      
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_registration_number" runat="server" >Registration Number</asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:TextBox ID="txt_registration_number" runat="server" Width="130px" 
                                                        MaxLength="10" Enabled="False" ReadOnly="True" ></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_AadhaarNumber" runat="server" >Enter Aadhaar Number</asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:TextBox ID="txt_AadhaarNumber" runat="server" Width="130px" 
                                                        MaxLength="12"   ></asp:TextBox>
                                                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True" TargetControlID="txt_AadhaarNumber"
                                                            ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txt_AadhaarNumber"
                                                             SetFocusOnError="true"  ErrorMessage="Aadhaar Number must be 12 digit." ValidationExpression="^[0-9]{12,12}$"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td align="left" colspan="2">
                                                    <asp:Button ID="btn_Update_AadhaarNumber" runat="server" Text="Update" 
                                                        Font-Bold="True" Font-Size="Medium" ForeColor="#FF66CC" 
                                                        onclick="btn_Update_AadhaarNumber_Click" BackColor="#0066FF" 
                                                        BorderStyle="Groove" />
                                                    <asp:Label ID="lblmessage" runat="server" Text=""></asp:Label>
                                                </td>
                                                
                                            </tr>
                                        </table>
                </asp:Panel>
            </td>
        </tr>
  

 <tr>
            <td  align="center" colspan="2">
                <asp:Panel ID="Panel2" runat="server" Enabled="true"  Visible="false"> 

    ***** Candidate Details *****
    <table style="height: 70%; width: auto;" border="1" align="center">
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_organisation_code2" runat="server" >Organisation Name</asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                <asp:TextBox ID="txt_organisation_Name2" runat="server" Width="375px" 
                                                        MaxLength="400" Enabled="False" ReadOnly="True"></asp:TextBox>
                                                                                                       
                                                   
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_employee_id2" runat="server" >Pass ID</asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:TextBox ID="txt_employee_id2" runat="server" Width="175px" MaxLength="12" 
                                                        Enabled="False" ReadOnly="True"></asp:TextBox>
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_candidate_name2" runat="server" >Candidate Name</asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:TextBox ID="txt_candidate_name2" runat="server" Width="375px" 
                                                        MaxLength="100" ReadOnly="True" Enabled="False"></asp:TextBox>
                                                   
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_sex_code2" runat="server" >Sex</asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                 
                                                    <asp:TextBox ID="txt_sex2" runat="server" Width="75px" MaxLength="7" 
                                                        Enabled="False" ReadOnly="True"></asp:TextBox>
                                                                                                                                  
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_designation_code2" runat="server" >Designation</asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                                                                       
                                       <asp:TextBox ID="txt_designation2" runat="server" Width="275px" MaxLength="100" 
                                                        Enabled="False" ReadOnly="True"></asp:TextBox>                                        
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_dob2" runat="server" >DOB</asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:TextBox ID="txt_dob2" runat="server" Width="130px" MaxLength="10" 
                                                        ReadOnly="True" Enabled="False"></asp:TextBox>
                                                   
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_email_id2" runat="server" >Email Id</asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:TextBox ID="txt_email_id2" runat="server" Width="355px" MaxLength="50" 
                                                        ReadOnly="True" Enabled="False"></asp:TextBox>
                                                                                                       
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_mobile_no2" runat="server" >Mobile No</asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:TextBox ID="txt_mobile_no2" runat="server" Width="130px" MaxLength="10" 
                                                        Enabled="False" ReadOnly="True"></asp:TextBox>
                                                                                                        
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_nationality_code2" runat="server" >Nationality</asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                <asp:TextBox ID="txt_nationality2" runat="server" Width="120px" MaxLength="12" 
                                                        Enabled="False" ReadOnly="True"></asp:TextBox>
                                        
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_educational_qualification_code2" runat="server" >Educational Qualification</asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                       
                                       <asp:TextBox ID="txt_educational_qualification2" runat="server" Width="325px" MaxLength="50" 
                                                        Enabled="False" ReadOnly="True"></asp:TextBox>                                                                                      
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_registration_number2" runat="server" >Registration Number</asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:TextBox ID="txt_registration_number2" runat="server" Width="130px" 
                                                        MaxLength="10" Enabled="False" ReadOnly="True" ></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_AadhaarNumber2" runat="server" >Aadhaar Number</asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:TextBox ID="txt_AadhaarNumber2" runat="server" Width="130px" 
                                                        MaxLength="12" Enabled="False"   ></asp:TextBox>
                                                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True" TargetControlID="txt_AadhaarNumber2"
                                                            ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txt_AadhaarNumber2"
                                                             SetFocusOnError="true"  ErrorMessage="Aadhaar Number must be 12 digit." ValidationExpression="^[0-9]{12,12}$"></asp:RegularExpressionValidator>
                                               
                                                </td>
                                            </tr>                                            
                                        </table>
                </asp:Panel>
            </td>
        </tr>


 </table>
</asp:Content>
