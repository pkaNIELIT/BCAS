<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" culture="en-GB" UICulture="auto" 
CodeFile="Query_CandidateDetails_OrganisationWise.aspx.cs" Inherits="Query_CandidateDetails_OrganisationWise" Title="CandidateDetails_OrganistionWise" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
             <table width="100%" align="center" bgcolor="#FBEBE6">
              <tr>
            <td style="height: 21px" align="center">
                <asp:Label ID="lbl_Heading" runat="server"  EnableTheming="True" 
                    BackColor="#FF9933">Candidate Registration Information</asp:Label>
            </td>
        </tr>
        <tr>
        <td style="height: 35%" align="center">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate> 
            <table style="height: 34%" width="60%" border="1">
                <tr align="left">
                    <td width="20%">
            <asp:Label ID="lblCourseSchedule" runat="server" Text="Course Schedule" Width="150px"></asp:Label>
                    </td>
                     <td width="50%">
        
            <asp:DropDownList ID="ddlSchedule" runat="server" 
                DataSourceID="SqlDataSource2" DataTextField="Course_Schedule" 
                DataValueField="Schedule_ID" Width="450px" AppendDataBoundItems="True" >
                <asp:ListItem Value="0">-Select Course Schedule-</asp:ListItem>
            </asp:DropDownList>
            
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="*" ControlToValidate ="ddlSchedule" SetFocusOnError="true" InitialValue="0" ></asp:RequiredFieldValidator>
            
                    </td>
                </tr>
            
               <tr align="left">
                        <td width="30%">
                            <asp:Label ID="lblCourseSchedule0" runat="server" 
                                Text="Sponsoring Organisation" Width="150px"></asp:Label>
                        </td>
                        <td width="70%">
                             <asp:DropDownList ID="ddlOrganisation" runat="server" Width="400px" DataTextField="Organisation_Name" 
                DataValueField="Organisation_Code"                 
                AppendDataBoundItems="True" AutoPostBack="True"  >               
                <asp:ListItem Value="0">--Select Sponsoring Organisation--</asp:ListItem>
                                 <asp:ListItem Value="999999">GET ALL ORGANISATIONS</asp:ListItem>
            </asp:DropDownList>
            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ErrorMessage="*" InitialValue="0" SetFocusOnError="True" 
                                 ControlToValidate="ddlOrganisation"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
            
            <%--deep--%>
            
            <tr>
                  <td></td> <td></td>  <td align="right" rowspan="11" style="width: 87px; " valign="top"  >
                                                    <asp:Image ID="Img_Photo" runat="server" Height="143px" 
                                                        Width="150px" /><br /><br />
                                                    <asp:Image ID="ImgSign" runat="server" Height="50px" 
                                                        Width="130px" />
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_employee_id" runat="server" Width="150px" >Pass ID </asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:TextBox ID="txt_employee_id" runat="server" Width="150px" MaxLength="12"></asp:TextBox>
                                                    
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_candidate_name" runat="server" Width="150px" >Candidate Name</asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:TextBox ID="txt_candidate_name" runat="server" Width="250px" 
                                                        MaxLength="100"></asp:TextBox>
                                                     </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_sex_code" runat="server" meta:resourceKey="lbl_sex_code" 
                                                        Width="150px"  >Sex</asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                                                                      
                                                    <asp:DropDownList ID="ddl_gender_code" runat="server" TabIndex="9" >
                                            <asp:ListItem Value="X" >Select Gender </asp:ListItem>
                                            <asp:ListItem Value="M" >Male</asp:ListItem>
                                            <asp:ListItem Value="F" >Female</asp:ListItem>
                                        </asp:DropDownList>
                                        
                                        <asp:RegularExpressionValidator ID="rev_gender_code" runat="server" ControlToValidate="ddl_gender_code"
                                            SetFocusOnError="True" ErrorMessage="Please Select Gender Code" 
                                            ValidationExpression="M|F" ></asp:RegularExpressionValidator>
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_designation_code" runat="server" Width="150px" >Designation</asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                  <asp:DropDownList ID="ddl_Designation" runat="server" AppendDataBoundItems="True"
                                            DataSourceID="SqlDataDesignation" DataTextField="Description" DataValueField="Designation_Code"
                                            TabIndex="5" Width="250px" >
                                            <asp:ListItem >Select</asp:ListItem>
                                        </asp:DropDownList>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                                        ControlToValidate="ddl_Designation" ErrorMessage="*" SetFocusOnError="True" 
                                                        ValidationExpression="^[1-9[0-9][0-9]*"></asp:RegularExpressionValidator>
                                        <asp:SqlDataSource ID="SqlDataDesignation" runat="server" ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>"
                                            SelectCommand="SELECT [Designation_Code], [Description] FROM [Designation_Master]">
                                        </asp:SqlDataSource>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_dob" runat="server" Width="150px" >DOB</asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:TextBox ID="txt_dob" runat="server" Width="150px" MaxLength="10"></asp:TextBox>
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_email_id" runat="server" Width="150px" >Email Id</asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:TextBox ID="txt_email_id" runat="server" Width="250px" MaxLength="50"></asp:TextBox>
                                                   </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_mobile_no" runat="server" Width="150px" >Mobile No</asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:TextBox ID="txt_mobile_no" runat="server" Width="100px" MaxLength="10"></asp:TextBox>
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_nationality_code" runat="server" Width="150px" >Nationality</asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:DropDownList ID="ddl_Nationality" runat="server" AppendDataBoundItems="True"
                                            DataSourceID="SqlDataNationality" DataTextField="Country_Name" DataValueField="Country_Code"
                                            TabIndex="12" Width="250px" >
                                            <asp:ListItem >Select</asp:ListItem>
                                        </asp:DropDownList>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                                        ControlToValidate="ddl_Nationality" ErrorMessage="*" SetFocusOnError="True" 
                                                        ValidationExpression="^[1-9[0-9][0-9]*"></asp:RegularExpressionValidator>
                                        <asp:SqlDataSource ID="SqlDataNationality" runat="server" ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>"
                                            
                                            SelectCommand="SELECT [Country_Code], [Country_Name] FROM [Country_Master]">
                                        </asp:SqlDataSource>
                                        
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                             <asp:Label ID="lbl_educational_qualification_code" runat="server" Width="150px" >Educational Qualification </asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                                                                     
                                                    <asp:DropDownList ID="ddl_Qualification" runat="server" AppendDataBoundItems="True"
                                            DataSourceID="SqlDataQualification" DataTextField="Description" DataValueField="Qualification_Code"
                                            TabIndex="19" Width="250px">
                                            <asp:ListItem>Select</asp:ListItem>
                                        </asp:DropDownList>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                                                        ControlToValidate="ddl_Qualification" ErrorMessage="*" SetFocusOnError="True" 
                                                        ValidationExpression="^[1-9[0-9][0-9]*"></asp:RegularExpressionValidator>
                                        
                                        <asp:SqlDataSource ID="SqlDataQualification" runat="server" ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>"
                                            SelectCommand="SELECT [Qualification_Code], [Description] FROM [Qualification_Master]">
                                        </asp:SqlDataSource>                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbl_registration_number" runat="server" Width="150px" >Registration Number</asp:Label>
                                                </td>
                                                <td style="width: 87px; height: 16px" align="left">
                                                    <asp:TextBox ID="txt_registration_number" runat="server" Width="100px" 
                                                        MaxLength="10" ></asp:TextBox>
                                                </td>
                                            </tr>
                                         </table>
                         </ContentTemplate>  
                </asp:UpdatePanel>       
                   </td>                                 
                 </tr>                       
            <tr >
            <td>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table width="50%" align="center" border="1" >
                            <tr>
                            <td>
            <div id=div1 style="overflow:scroll;height:250px;width: 900px" >
                                        <asp:GridView ID="GridView1" runat="server" BackColor="#DEBA84"
                                            BorderColor="#DEBA84" BorderStyle="None" AllowPaging="True" Font-Size="Small" 
                    BorderWidth="1px" CellPadding="3" CellSpacing="2"
                                             DataSourceID="SqlDataSource1" PageSize="5" 
                    Height="30%" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                            OnRowDataBound="GridView1_RowDataBound" >
                                            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" HorizontalAlign="Center" Wrap="False" />
                                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                            <Columns>
                                                <asp:CommandField ShowSelectButton="True" />
                                              </Columns>
                                            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                                        </asp:GridView>
                                        </div>
                                        <tr><td>
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" 
                    SelectCommand="SELECT * FROM Candidate_Registration where Registration_Number in (select Registration_No from NOMINATIONS_FOR_COURSES where Schedule_ID=@Schedule_ID and Nominating_OrgCode=@Organisation_Code)" >
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="ddlOrganisation" Name="Organisation_Code" 
                                                    PropertyName="SelectedValue" Type="Decimal" />
                                                <asp:ControlParameter ControlID="ddlSchedule" Name="Schedule_ID" 
                                                    PropertyName="SelectedValue" Type="Int32" />
                                                     <asp:Parameter Name="pOrganisation_Code" Type="Int32" DefaultValue="-1" />
                                                <asp:Parameter Name="pEmployee_ID" Type="String" DefaultValue="*" />
                                                <asp:Parameter Name="pCandidate_Name" Type="String" DefaultValue="*" />
                                                <asp:Parameter Name="pSex_Code" Type="String" DefaultValue="*" />
                                                <asp:Parameter Name="pDesignation_Code" Type="Int32" DefaultValue="-1" />
                                                <asp:Parameter Name="pDOB" Type="String" DefaultValue="01/01/1900" />
                                                <asp:Parameter Name="pEmail_Id" Type="String" DefaultValue="*" />
                                                <asp:Parameter Name="pMobile_No" Type="String" DefaultValue="*" />
                                                <asp:Parameter Name="pNationality_Code" Type="Int32" DefaultValue="-1" />
                                                <asp:Parameter Name="pEducational_Qualification_Code" Type="Int32" DefaultValue="-1" />
                                                <asp:Parameter Name="pRegistration_Number" Type="Int32" DefaultValue="1" />
                                            </SelectParameters>
                </asp:SqlDataSource>
                                      </td> </tr> 
           </td>
           </tr>
                        </table>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                     </td></tr>
                     
            <%--deep--%>
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
                    </tr>
              </table>
                           
                  
</asp:Content>

