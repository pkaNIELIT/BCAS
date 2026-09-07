<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="BCAS_DwnProformaInvoice.aspx.cs" Inherits="Forms_DetailsForms_BCAS_DwnProformaInvoice" Culture="en-GB" Title="BCAS_Download_Proforma_Invoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <script type = "text/javascript">

function Check_Click(objRef)
{
    //Get the Row based on checkbox
    var row = objRef.parentNode.parentNode;
    
    //Get the reference of GridView
    var GridView = row.parentNode;
    
    //Get all input elements in Gridview
    var inputList = GridView.getElementsByTagName("input");
    
    for (var i=0;i<inputList.length;i++)
    {
        //The First element is the Header Checkbox
        var headerCheckBox = inputList[0];
        
        //Based on all or none checkboxes
        //are checked check/uncheck Header Checkbox
        var checked = true;
        if(inputList[i].type == "checkbox" && inputList[i] != headerCheckBox)
        {
            if(!inputList[i].checked)
            {
                checked = false;
                break;
            }
        }
    }
    headerCheckBox.checked = checked;
    
}
function checkAll(objRef)
{
    var GridView = objRef.parentNode.parentNode.parentNode;
    var inputList = GridView.getElementsByTagName("input");
    for (var i=0;i<inputList.length;i++)
    {
        var row = inputList[i].parentNode.parentNode;
        if(inputList[i].type == "checkbox"  && objRef != inputList[i])
        {
            if (objRef.checked)
            {
                inputList[i].checked=true;
            }
            else
            {
                if(row.rowIndex % 2 == 0)
                {
                   row.style.backgroundColor = "#C2D69B";
                }
                else
                {
                   row.style.backgroundColor = "white";
                }
                inputList[i].checked=false;
            }
        }
    }
}

</script>

<script type = "text/javascript">
    function ConfirmDelete()
    {
        var count = document.getElementById("<%=hfCount.ClientID %>").value;
        var gv = document.getElementById("<%=gvAll.ClientID%>");
        var chk = gv.getElementsByTagName("input");
        for(var i=0;i<chk.length;i++)
        {
            if(chk[i].checked && chk[i].id.indexOf("chkAll") == -1)
            {
                count++;
            }
        }
        if(count == 0)
        {
            alert("No Records to Update.");
            return false;
        }
        else
        {
            return confirm("Do you want to Update " + count + " records.");
        }
    }
</script>
    
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
                Text="NOMINATION APPROVED PROFORMA INVOICE BY BCAS"></asp:Label>
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
                onselectedindexchanged="ddlSchedule_SelectedIndexChanged" AppendDataBoundItems="True" AutoPostBack="true">
                <asp:ListItem Value="0">-Select Course Schedule-</asp:ListItem>
            </asp:DropDownList>
                    </td>
                </tr>
                 <tr align="left">
                    <td width="30%">
            <asp:Label ID="lblSponsoringOrg" runat="server" Text="Sponsoring Organisation" Visible="False"></asp:Label>
                    </td>
                    <td width="70%">
            <asp:DropDownList ID="ddlOrganisation" runat="server" Width="535px" DataTextField="Organisation_Name" 
                DataValueField="Organisation_Code" 
                AutoPostBack="True" 
                onselectedindexchanged="ddlOrganisation_SelectedIndexChanged" 
                AppendDataBoundItems="True"  Enabled="false" Visible="False" >               
                <asp:ListItem Value="0">Select Sponsoring Organisation</asp:ListItem>
            </asp:DropDownList>
                    </td>
                </tr>
                <tr align="left">
                    <td width="30%">
            <asp:Label ID="Label3" runat="server" Text="Payee Organisation"></asp:Label>
                    </td>
                    <td width="70%">
            <asp:DropDownList ID="ddlBillerOrganisation" runat="server" Width="535px" DataTextField="Payee_Organisation_Name" 
                DataValueField="Payee_Organisation_Code" 
                AutoPostBack="True" 
                onselectedindexchanged="ddlBillerOrganisation_SelectedIndexChanged" 
                AppendDataBoundItems="True"  Enabled="false" >               
                <asp:ListItem Value="0">Select Biller Organisation</asp:ListItem>
            </asp:DropDownList>
                    </td>
                </tr>
                 <tr align="left">
                    <td width="30%">
            <asp:Label ID="Label2" runat="server" Text="Demand Note"></asp:Label>
                    </td>
                    <td width="70%">
            <asp:DropDownList ID="ddlDemandNote" runat="server" Width="535px"  
                
                AppendDataBoundItems="True"  >               
                <asp:ListItem Value="0">--Select-- </asp:ListItem>
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
    <tr align="center">
        <td width="30%" colspan="2" align="center"   >            
           <div align="center">
    <asp:GridView ID="gvAll" runat="server"  

    AutoGenerateColumns = "False" Font-Names = "Arial" 

    Font-Size = "11pt" AlternatingRowStyle-BackColor = "#C2D69B"  

    HeaderStyle-BackColor = "green" AllowPaging ="True"   

    OnPageIndexChanging = "OnPaging" DataKeyNames = "Registration_Number,Schedule_ID" 
                   EmptyDataText="No record found." SelectedIndex="0" ForeColor="#333333" 
                   Caption="Candidates For BCAS Proforma Invoice" CaptionAlign="Top" >

   <Columns>

    <asp:TemplateField>

        <HeaderTemplate>

            <asp:CheckBox ID="chkAll" runat="server" 

             onclick = "checkAll(this);" />

        </HeaderTemplate> 

        <ItemTemplate>

            <asp:CheckBox ID="chk" runat="server"/>

        </ItemTemplate>

    </asp:TemplateField> 

    <%--<asp:BoundField ItemStyle-Width = "150px" DataField = "Registration_Number" HeaderText = "Regd. No">

<ItemStyle Width="150px"></ItemStyle>
       </asp:BoundField>--%>
       
       <asp:TemplateField HeaderText="Regd. No">
          <%-- <EditItemTemplate>
               <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Registration_Number") %>'></asp:TextBox>
           </EditItemTemplate>--%>
           <ItemTemplate>
               <asp:HyperLink ID="Label1" NavigateUrl='<%# Bind("Registration_Number","~/Forms/DetailsForms/BCAS_ViewDetails.aspx?Regd_No={0}") %>' 
                   runat="server" Target="_blank"  Text='<%# Bind("Registration_Number") %>'></asp:HyperLink>
           </ItemTemplate>
           <ItemStyle Width="100px" />
       </asp:TemplateField>

       <%--<asp:TemplateField HeaderText="Candidate_Name">
           <EditItemTemplate>
               <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Candidate_Name") %>'></asp:TextBox>
           </EditItemTemplate>
           <ItemTemplate>
               <asp:HyperLink ID="Label1" NavigateUrl='<%# Bind("Candidate_Name","~/Forms/DetailsForms/BCAS_ViewDetails.aspx?asdf={0}&& regd_No={0}") %>' 
                   runat="server" Text='<%# Bind("Candidate_Name") %>'></asp:HyperLink>
           </ItemTemplate>
           <ItemStyle Width="150px" />
       </asp:TemplateField>--%>
       
     <asp:BoundField ItemStyle-Width = "250px" DataField = "Candidate_Name" 

       HeaderText = "Name">

<ItemStyle Width="250px"></ItemStyle>
       </asp:BoundField>

    <asp:BoundField ItemStyle-Width = "50px" DataField = "Sex_Code" 

       HeaderText = "Sex">

<ItemStyle Width="50px"></ItemStyle>
       </asp:BoundField>
     
     
     <asp:BoundField ItemStyle-Width = "175px" DataField = "Designation" 

       HeaderText = "Designation">

<ItemStyle Width="175px"></ItemStyle>
       </asp:BoundField>
       
       
       <asp:BoundField ItemStyle-Width = "150px" DataField = "Qualification" 

       HeaderText = "Qualification">

<ItemStyle Width="150px"></ItemStyle>
       </asp:BoundField>
       
       
       <asp:BoundField ItemStyle-Width = "100px" DataField = "DOB" 

       HeaderText = "DOB">

<ItemStyle Width="100px"></ItemStyle>
       </asp:BoundField>
       
       <asp:BoundField ItemStyle-Width = "100px" DataField = "Mobile_No" 

       HeaderText = "Mobile No">

<ItemStyle Width="100px"></ItemStyle>
       </asp:BoundField>
       
       
       <asp:BoundField ItemStyle-Width = "150px" DataField = "BCAS_Approval_Status" 

       HeaderText = "BCASApprovalStatus">

<ItemStyle Width="150px"></ItemStyle>
       </asp:BoundField>
     
     <asp:BoundField ItemStyle-Width = "150px" DataField = "Organisation_Name" 

       HeaderText = "Organisation">

<ItemStyle Width="150px"></ItemStyle>
       </asp:BoundField>
       
       <asp:BoundField ItemStyle-Width = "100px" DataField = "Schedule_ID" 

       HeaderText = "ScheduleID" >

<ItemStyle Width="100px"></ItemStyle>
       </asp:BoundField>    
           <asp:BoundField ItemStyle-Width = "100px" DataField = "Demand_Note_ID" 

       HeaderText = "DemandNoteID" >

<ItemStyle Width="100px"></ItemStyle>
       </asp:BoundField> 

      <%-- <asp:HyperLinkField HeaderText="Candidate" 
           NavigateUrl="~/Forms/DetailsForms/Default.aspx" Text="asdf" />
      --%>    

   </Columns> 

        <FooterStyle BackColor="#990000" />

<HeaderStyle BackColor="#990000" ForeColor="White"></HeaderStyle>

   <AlternatingRowStyle BackColor="#FFFFCC"  />

</asp:GridView> 

<asp:HiddenField ID="hfCount" runat="server" Value = "0" />

               <asp:Label ID="lbl_error" runat="server"></asp:Label>

     

    </div>
            
            </td>
    </tr>
    <tr>
        <td width="30%" colspan="2" style="width: 100%" >   
           </td>
    </tr>
    <tr>
        <td width="30%" colspan="2" style="width: 100%" align="center"  >
          
            <asp:Button ID="btnUpdateStatus" runat="server" OnClientClick = "return ConfirmDelete();" onclick="btnUpdateStatus_Click" 
                Text="Download Proforma Invoice" />
           
           </td>
    </tr>
    </table>
</asp:Content>

