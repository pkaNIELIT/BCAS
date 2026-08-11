<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="BCAS_Approval_Rejection.aspx.cs" Inherits="Forms_DetailsForms_BCAS_Approval_Rejection" Title="BCAS-->Modification/Rejection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table width="100%" align="center" >
    <tr>
        <td colspan="2" align="center"  >
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="2" align="center"  >
            <asp:Label ID="lblHeading" runat="server" Font-Bold="True" ForeColor="#990000" 
                Text="NOMINATION APPROVAL AND REJECTION BY BCAS"></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="30%" >
            </td>
        <td width="70%" >
            </td>
    </tr>
    <tr align="center" >
        <td width="30%" colspan="2" style="width: 100%"  >            
            <table width="65%" align="center" border="2"  >
                <tr align="left">
                    <td width="30%">
            <asp:Label ID="lblCourseSchedule" runat="server" Text="Course Schedule"></asp:Label>
                    </td>
                    <td width="70%">
            <asp:DropDownList ID="ddlSchedule" runat="server" DataTextField="Course_Schedule" 
                DataValueField="Schedule_ID" Width="535px" 
                onselectedindexchanged="ddlSchedule_SelectedIndexChanged" AppendDataBoundItems="True">
                <asp:ListItem Value="0">-Select Course Schedule-</asp:ListItem>
            </asp:DropDownList>
                    </td>
                </tr>
                 <tr align="left">
                    <td width="30%">
            <asp:Label ID="lblSponsoringOrg" runat="server" Text="Sponsoring Organisation"></asp:Label>
                    </td>
                    <td width="70%">
            <asp:DropDownList ID="ddlOrganisation" runat="server" Width="535px" DataTextField="Organisation_Name" 
                DataValueField="Organisation_Code" 
                AutoPostBack="True" 
                onselectedindexchanged="ddlOrganisation_SelectedIndexChanged" 
                AppendDataBoundItems="True"  >               
                <asp:ListItem Value="0">Select Sponsoring Organisation</asp:ListItem>
            </asp:DropDownList>
                    </td>
                </tr>
            </table>
            
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
            <asp:Panel ID="Panel1" runat="server" Width="1200px" ScrollBars ="Vertical" >
             <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" 
                    Font-Names = "Arial" Font-Size = "11pt" 
                    AlternatingRowStyle-BackColor = "#C2D69B" HeaderStyle-BackColor = "green" AllowPaging ="True"
                DataKeyNames="Registration_Number,Schedule_ID" 
                AutoGenerateColumns="False" ForeColor="#333333">
                <Columns>
                    <asp:CommandField ShowEditButton="True" />
                  <asp:TemplateField HeaderText="BCAS_Approval" ItemStyle-Width ="150px" SortExpression="BCAS_Approval_Status" ItemStyle-VerticalAlign ="Middle">
                        <EditItemTemplate>
                           <asp:DropDownList ID="ddl_Approval_Status" runat="server" Width="80px" 
                            SelectedValue='<%# Bind("BCAS_Approval_Status") %>' AutoPostBack="True" 
                            onselectedindexchanged="DropDownList1_SelectedIndexChanged" >
                            <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                          <asp:ListItem Value="Y">Yes</asp:ListItem>
                            <asp:ListItem Value="N">No</asp:ListItem>
                            <asp:ListItem Value="P">Pending</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvBCAS_Approval" runat="server" 
                            ControlToValidate="ddl_Approval_Status" ErrorMessage="*" InitialValue="0"></asp:RequiredFieldValidator>                          
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" 
                                Text='<%# Bind("BCAS_Approval_Status") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Middle" Width="150px" />
                    </asp:TemplateField>
                                    
                    <asp:TemplateField HeaderText="BCAS_Remarks" SortExpression="BCAS_Remarks" ItemStyle-Width ="150px" ItemStyle-HorizontalAlign ="Justify" ItemStyle-VerticalAlign ="Middle" >
                        <EditItemTemplate>
                            <asp:TextBox ID="txtRemarks" runat="server" Width=130px Text='<%# Bind("BCAS_Remarks") %>'></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvRemarks" runat="server" ErrorMessage="*" ControlToValidate="txtRemarks"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemTemplate >
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("BCAS_Remarks") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Justify" VerticalAlign="Middle" Width="150px" />
                    </asp:TemplateField>
                    
                    
                    <asp:BoundField DataField="Registration_Number" HeaderText="Regd_No" 
                        InsertVisible="False" ReadOnly="True" SortExpression="Registration_Number" />
                    <asp:BoundField DataField="Candidate_Name" HeaderText="Name" ReadOnly="true"
                        SortExpression="Candidate_Name" />
                    <asp:BoundField DataField="Sex_Code" HeaderText="Sex" ReadOnly="true"
                        SortExpression="Sex_Code" />
                    <asp:BoundField DataField="Designation" HeaderText="Designation" ReadOnly="true"
                        SortExpression="Designation" />
                    <asp:BoundField DataField="Qualification" HeaderText="Qualification" ReadOnly="true"
                        SortExpression="Qualification" />
                    <asp:BoundField DataField="DOB" HeaderText="DOB" ReadOnly="True" 
                        SortExpression="DOB" />
                    <asp:BoundField DataField="Mobile_No" HeaderText="Mobile_No" ReadOnly="true"
                        SortExpression="Mobile_No" />
                    <asp:BoundField DataField="Country_Name" HeaderText="Country" ReadOnly="true"
                        SortExpression="Country_Name" />
                    <asp:BoundField DataField="Organisation_Name" HeaderText="Organisation" ReadOnly="true"
                        SortExpression="Organisation_Name" />
                    <asp:BoundField DataField="Schedule_ID" HeaderText="Schedule_ID" 
                        InsertVisible="False" ReadOnly="True" SortExpression="Schedule_ID" />
                    
                    
                </Columns>
                 <HeaderStyle BackColor="#990000" ForeColor="White" />
                 <AlternatingRowStyle BackColor="#C2D69B" />
            </asp:GridView>
            </asp:Panel>           
           
        </td>
    </tr>
    <tr>
        <td width="30%" colspan="2" style="width: 100%" align="center"  >            
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" 
                SelectCommand="	SELECT Candidate_Registration.Registration_Number,Candidate_Registration.Candidate_Name,Candidate_Registration.Sex_Code,Designation_Master.Description Designation,Qualification_Master.Description Qualification,
	convert(varchar(10),Candidate_Registration.DOB,103) DOB,Candidate_Registration.Mobile_No,Country_Master.Country_Name,
	Sponsoring_Organisation_Master.Organisation_Name, Course_Schedule.Schedule_ID,isnull(NOMINATIONS_FOR_COURSES.BCAS_Approval_Status,'Pending')BCAS_Approval_Status,
	NOMINATIONS_FOR_COURSES.BCAS_Remarks 
	FROM  NOMINATIONS_FOR_COURSES INNER JOIN Course_Schedule 
	ON NOMINATIONS_FOR_COURSES.Schedule_ID = Course_Schedule.Schedule_ID 
	INNER JOIN Candidate_Registration on
	NOMINATIONS_FOR_COURSES.Registration_No=Candidate_Registration.Registration_Number INNER JOIN Sponsoring_Organisation_Master 
	on Sponsoring_Organisation_Master.Organisation_Code=Candidate_Registration.Organisation_Code INNER JOIN Country_Master ON
	Candidate_Registration.Nationality_Code =Country_Master.Country_Code INNER JOIN Designation_Master ON
	Designation_Master.Designation_Code=Candidate_Registration.Designation_Code
	INNER JOIN Qualification_Master ON
	Qualification_Master.Qualification_Code=Candidate_Registration.Educational_Qualification_Code
	WHERE NOMINATIONS_FOR_COURSES.Schedule_ID=@Schedule_ID
	AND Sponsoring_Organisation_Master.Organisation_Code=@Organisation_Code" 
                
                
                UpdateCommand="UPDATE NOMINATIONS_FOR_COURSES SET BCAS_Approval_Status = @BCAS_Approval_Status, BCAS_Approval_By = BCAS_Approval_By, BCAS_Approval_Date = @BCAS_Approval_Date,
BCAS_Remarks=@BCAS_Remarks
WHERE
Schedule_ID = @i_Schedule_ID AND Registration_No =@i_Registration_No" 
                onupdating="SqlDataSource1_Updating">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ddlSchedule" Name="Schedule_ID" 
                        PropertyName="SelectedValue" Type="Int32" />
                    <asp:ControlParameter ControlID="ddlOrganisation" Name="Organisation_Code" 
                        PropertyName="SelectedValue" Type="Int32" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:ControlParameter ControlID="GridView1" Name="BCAS_Approval_Status" 
                        PropertyName="SelectedValue" />
                    <asp:ControlParameter ControlID="GridView1" Name="BCAS_Approval_Date" 
                        PropertyName="SelectedValue" />
                    <asp:ControlParameter ControlID="GridView1" Name="BCAS_Remarks" 
                        PropertyName="SelectedValue" />
                    <asp:ControlParameter ControlID="GridView1" Name="i_Schedule_ID" 
                        PropertyName="SelectedValue" />
                    <asp:ControlParameter ControlID="GridView1" Name="i_Registration_No" 
                        PropertyName="SelectedValue" />
                </UpdateParameters>
            </asp:SqlDataSource>
        </td>
    </tr>
    <tr align="center">
        <td width="30%" colspan="2" align="center"   >            
           <div align="center">
 

            

     

    </div>
            
            </td>
    </tr>
    <tr>
        <td width="30%" colspan="2" style="width: 100%" align="center"  >
          
           
            &nbsp;</td>
    </tr>
    </table>
</asp:Content>

