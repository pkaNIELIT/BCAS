<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="BCASReconcillation.aspx.cs" Inherits="Forms_DetailsForms_BCASReconcillation" Culture="en-GB" Title="BCAS Reconcillation" %>

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
    function ValidateFormFields() {

        if (!isBlank("<%=flUpload.ClientID %>", "Browse File Upload"))
          return false;
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
        <td width="30%" >
            </td>
        <td width="70%" >
            </td>
    </tr>
         <tr>
        <td width="30%" >
            </td>
        <td width="70%" >
            </td>
    </tr>
    <tr>
        <td colspan="2" align="center"  >
            <asp:Label ID="lblHeading" runat="server" Font-Bold="True" ForeColor="#990000" 
                Text="BCAS Online Payment Reconcillation"></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="30%" >
            </td>
        <td width="70%" >
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
            <asp:Label ID="lblerror" runat="server" Text=""
                        ForeColor="Red"></asp:Label>
                                                  </ContentTemplate>                                         
                                        </asp:UpdatePanel>
            </td>
    </tr>
    <tr align="center" >
        <td width="30%" colspan="2" style="width: 100%"  >            
            <table width="65%" align="center" border="2"  >
                <tr>
        <td width="30%" >
            <asp:Label ID="Label11" runat="server" SkinID="CaptionLabel" Text="Upload MS-Excel File (.xls/xlsx) &lt;b class='mandatory'&gt;*&lt;/b&gt;"></asp:Label>
            </td>
        <td width="70%" >
             <asp:FileUpload ID="flUpload" runat="server" Width="485px" />
                <asp:HiddenField ID="flpath" runat="server" />
            </td>
    </tr>

            </table>
            
            </td>
    </tr>
    <tr>
        <td width="30%"  style="width: 10%" align="center"  >
            </td>
        <td width="30%"  style="width: 100%" align="center"  >            
            &nbsp;
 <div id="divValidateData" runat="server" visible="false" height="150px" width="300px"
        >
        <table class="sample3" id="tblValidateData" style="width: 100%; text-align: left"
            border="0" cellpadding="3" cellspacing="1">
            <tr class="head1">
                <td align="left" colspan="2">
                    Validate Data
                </td>
            </tr>
            <tr class="gdrow1">
                <td width="20%">
                    Total Records
                </td>
                <td width="80%">
                    <asp:Label ID="lblTotalRecords" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="gdalternate1">
                <td width="40%">
                    Successful Records
                </td>
                <td>
                    <asp:Label ID="lblValidateRecords" runat="server"></asp:Label>
                </td>
            </tr>
             <tr class="gdrow1">
                <td width="40%" valign="top">
                    Refund Records
                </td>
                <td>
                     <asp:label id="lblRefundRecords" runat="server"></asp:label>
                </td>
            </tr>
            <tr class="gdrow1">
                <td width="40%" valign="top">
                    Failed Records
                </td>
                <td>
                    <asp:Label ID="lblFailedRecords" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="gdrow1">
                <td width="40%" colspan="2">
                    <asp:Label ID="Label12" runat="server" Text="The unsaved records may be due to data discrepancies or duplicacy or data not found in the databse."
                        ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    </div>

        </td>
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
           <asp:hiddenfield runat="server" id="hffilename" />
    <asp:hiddenfield runat="server" id="hffilepath" /></td>
    </tr>
    <tr>
        <td width="30%" colspan="2" style="width: 100%" align="center"  >
          
            <asp:Button ID="btnUploadData" runat="server" OnClientClick = "return ValidateFormFields();" onclick="btnUploadData_Click" 
                Text="Upload Data" />

            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             <asp:Button ID="btnCancel" runat="server" Text="Reset" OnClick="btnCancel_Click" />
           
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:button id="btndownload" runat="server" text="Download Refund File" 
            visible="false" onclick="btndownload_Click" />
           </td>
    </tr>
    </table>
</asp:Content>

