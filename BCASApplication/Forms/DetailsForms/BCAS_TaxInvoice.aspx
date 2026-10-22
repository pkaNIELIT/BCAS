<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="BCAS_TaxInvoice.aspx.cs" Inherits="Forms_DetailsForms_BCAS_TaxInvoice" Culture="en-GB" Title="BCAS_TaxInvoice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <script type = "text/javascript">

//function Check_Click(objRef)
//{
//    //Get the Row based on checkbox
//    var row = objRef.parentNode.parentNode;

//    //Get the reference of GridView
//    var GridView = row.parentNode;

//    //Get all input elements in Gridview
//    var inputList = GridView.getElementsByTagName("input");

//    for (var i=0;i<inputList.length;i++)
//    {
//        //The First element is the Header Checkbox
//        var headerCheckBox = inputList[0];

//        //Based on all or none checkboxes
//        //are checked check/uncheck Header Checkbox
//        var checked = true;
//        if(inputList[i].type == "checkbox" && inputList[i] != headerCheckBox)
//        {
//            if(!inputList[i].checked)
//            {
//                checked = false;
//                break;
//            }
//        }
//    }
//    headerCheckBox.checked = checked;

        //}


//function checkAll(objRef)
//{
//    var GridView = objRef.parentNode.parentNode.parentNode;
//    var inputList = GridView.getElementsByTagName("input");
//    for (var i=0;i<inputList.length;i++)
//    {
//        var row = inputList[i].parentNode.parentNode;
//        if(inputList[i].type == "checkbox"  && objRef != inputList[i])
//        {
//            if (objRef.checked)
//            {
//                inputList[i].checked=true;
//            }
//            else
//            {
//                if(row.rowIndex % 2 == 0)
//                {
//                   row.style.backgroundColor = "#C2D69B";
//                }
//                else
//                {
//                   row.style.backgroundColor = "white";
//                }
//                inputList[i].checked=false;
//            }
//        }
//    }
//}

</script>

<script type = "text/javascript">
    <%--function ConfirmDelete()
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
    }--%>

    function formatDate(dateString) {
        var parts = dateString.split('/');
        return parts[1] + '/' + parts[0] + '/' + parts[2];
    }


    debugger;
    function compareDates() {

        debugger;
        // Get the values from the textboxes
        var fromDate_Value = document.getElementById("<%=txt_from_date.ClientID %>").value;
        var toDate_Value = document.getElementById("<%=txt_to_date.ClientID %>").value;

        // Convert textbox values to Date objects
        var date1 = new Date(formatDate(fromDate_Value));
        var date2 = new Date(formatDate(toDate_Value));

        // Compare the dates
        if (date1 > date2) {
            alert("ToDate can't be before FromDate");
            return false;
        }
        if ((date1 >= new Date()) || (date2 >= new Date())) {
            alert("Date can't be After Today");
            return false;
        }
        var monthFromDate = date1.getMonth();
        var monthToDate = date2.getMonth();
        var dateFromDate = date1.getDate();
        var dateToDate = date2.getDate();
        var yearFromDate = date1.getFullYear();
        var yearToDate = date2.getFullYear();
        var monthDiff = (yearToDate - yearFromDate) * 12 + (monthToDate - monthFromDate);
        var dateDiff = (dateToDate - dateFromDate)

        if (monthDiff >= 1) {
            if (dateDiff >= 0) {
                alert(" Date Range must be less than one month ");
                return false;
            }
            else
                return true;

        }
        else
            return true;
    }

   <%-- document.addEventListener("DOMContentLoaded", function () {
        console.log("Document is ready");
        var today = new Date();
        var month = ('0' + (today.getMonth() + 1)).slice(-2);
        var day = ('0' + today.getDate()).slice(-2);
        var year = today.getFullYear();
        var date = year + '-' + month + '-' + day;
        /*var elements = document.querySelectorAll('[id*=datetextBox]');*/
        var elements = document.querySelectorAll('[id*=<%=txt_from_date.ClientID %>]');
        var element1 = document.querySelectorAll('[id*=<%=txt_to_date.ClientID %>]');
        elements.forEach(function (element) {
            element.setAttribute('max', date);
        });
        element1.forEach(function (element) {
            element.setAttribute('max', date);
        });
    });--%>
</script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    &nbsp;<table width="100%" align="center" >
    <tr>
        <td colspan="2" align="center"  >
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="2" align="center"  >
            <asp:Label ID="lblHeading" runat="server" Font-Bold="True" ForeColor="#990000" 
                Text="BCAS TAX INVOICE FOR FINANCE SECTION"></asp:Label>
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

               <tr>
                   <td align="left">
                       <asp:Label ID="lbl_date_range" runat="server">Select Date Range</asp:Label>
                       (dd/mm/yyyy) <font color="red">*</font> :
                   </td>
                   <td align="left">
                       <asp:Label ID="LabelFrom" runat="server">From</asp:Label>
                       <asp:TextBox ID="txt_from_date" runat="server" TabIndex="13" Enabled="true"></asp:TextBox>
                       <cc1:MaskedEditExtender ID="txt_from_date_MaskedEditExtender" runat="server" CultureName="en-GB"
                           Mask="99/99/9999" MaskType="Date"
                           TargetControlID="txt_from_date" UserDateFormat="DayMonthYear">
                       </cc1:MaskedEditExtender>
                       <cc1:MaskedEditValidator ID="mev_txt_from_date" runat="server"
                           ControlExtender="txt_from_date_MaskedEditExtender"
                           ControlToValidate="txt_from_date" Display="None"
                           EmptyValueMessage="From Date Must Be Entered" IsValidEmpty="false">
                       </cc1:MaskedEditValidator>
                       <cc1:ValidatorCalloutExtender ID="mev_callout_extender_From" runat="server"
                           TargetControlID="mev_txt_from_date">
                       </cc1:ValidatorCalloutExtender>
                       <asp:Label ID="LabelTo" runat="server">To</asp:Label>
                       <asp:TextBox ID="txt_to_date" runat="server" TabIndex="13" Enabled="true"></asp:TextBox>
                       <cc1:MaskedEditExtender ID="txt_to_date_MaskedEditExtender" runat="server" CultureName="en-GB"
                           Mask="99/99/9999" MaskType="Date"
                           TargetControlID="txt_to_date" UserDateFormat="DayMonthYear">
                       </cc1:MaskedEditExtender>
                       <cc1:MaskedEditValidator ID="mev_txt_to_date" runat="server"
                           ControlExtender="txt_to_date_MaskedEditExtender"
                           ControlToValidate="txt_to_date" Display="None"
                           EmptyValueMessage="To Date Must Be Entered" IsValidEmpty="false">
                       </cc1:MaskedEditValidator>
                       <cc1:ValidatorCalloutExtender ID="mev_callout_extender_To" runat="server"
                           TargetControlID="mev_txt_to_date">
                       </cc1:ValidatorCalloutExtender>

                   </td>


               <%--    <td align="left">
                       <asp:Label ID="lbl_date_range" runat="server">Select Date Range</asp:Label>
                       (dd/mm/yyyy) <font color="red">*</font> :
                   </td>
                   <td align="left">
                       <asp:Label ID="LabelFrom" runat="server">From</asp:Label>
                       <asp:TextBox ID="txt_from_date" runat="server" TabIndex="13" Enabled="true" TextMode="Date"></asp:TextBox>

                       <asp:Label ID="LabelTo" runat="server">To</asp:Label>
                       <asp:TextBox ID="txt_to_date" runat="server" TabIndex="13" Enabled="true" TextMode="Date"></asp:TextBox>

                   </td>--%>

   </tr>


        <%--   $$$$$$$$$$$$$$$$$     Commented Demand Note For Date Range     $$$$$$$$$$$$$$$$$$$$$$$$$$$--%>

               <%--  <tr align="left">
                    <td width="30%">
            <asp:Label ID="Label2" runat="server" Text="Demand Note"></asp:Label>
                    </td>
                    <td width="70%">
            <asp:DropDownList ID="ddlDemandNote" runat="server" Width="535px"  
                
                AppendDataBoundItems="True"  >               
                <asp:ListItem Value="0">--Select-- </asp:ListItem>
            </asp:DropDownList>
                    </td>
                </tr>--%>


        <%--  $$$$$$$$$$$$$$$$$      Commented Demand Note For Date Range      $$$$$$$$$$$$$$$$$$$$$$$$$$$--%>

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
      <!--  <td width="30%" colspan="2" align="center"   >            
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
            
            </td>-->
    </tr>
    <tr>
        <td width="30%" colspan="2" style="width: 100%" >   
           </td>
    </tr>
    <tr>
        <td width="30%" colspan="2" style="width: 100%" align="center"  >
          <%--OnClientClick = "return ConfirmDelete();"--%>
            <asp:Button ID="btnUpdateStatus" runat="server" onclick="btnUpdateStatus_Click" OnClientClick = "return compareDates()"
                Text="Download Tax Invoice" style="margin-bottom: 0px" />
           
           </td>
    </tr>
    </table>
</asp:Content>

