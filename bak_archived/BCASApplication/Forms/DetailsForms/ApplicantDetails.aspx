<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ApplicantDetails.aspx.cs" Inherits="Forms_DetailsForms_ApplicantDetails"
    Title="BCAS-->Candidate Registration" Culture="en-GB"  uiculture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <script src='<%= ResolveUrl("~/Script/aes.js") %>' type="text/javascript"></script>
<script src='<%= ResolveUrl("~/Script/jQuery.js") %>' type="text/javascript"></script>

  <script type="text/javascript">
      //Function to allow only numbers to textbox
      function validate(key) {
          //getting key code of pressed key
          var keycode = (key.which) ? key.which : key.keyCode;
          var phn = document.getElementById("<%=txt_Aadhaar_Number.ClientID %>");
          //comparing pressed keycodes
          if (!(keycode == 8 || keycode == 46) && (keycode < 48 || keycode > 57)) {
              return false;
          }
          else {
              //Condition to check textbox contains ten numbers or not
              if (phn.value.length < 12) {
                  return true;
              }
              else {
                  return false;
              }
          }
      }
</script>
<script type="text/javascript">
    function ConfirmR() {
    var txtAadhaar_Number = document.getElementById("<%=txt_Aadhaar_Number.ClientID %>").value.trim();
     document.getElementById("<%=txt_Aadhaar_Number.ClientID %>").value =txtAadhaar_Number;
    }
    </script>
    
<script type="text/javascript">
    function ConfirmRecord(chk_declaration) {
        debugger;
        
            var txtAadhaar_Number = document.getElementById("<%=txt_Aadhaar_Number.ClientID %>").value.trim();

            if (txtAadhaar_Number.toString().length == 12) {
            
                var key = CryptoJS.enc.Utf8.parse('NiHq#K2021yDwMitUgo20202021Int08');
                var iv = CryptoJS.enc.Utf8.parse('HQ$2uNiGV$2oAd51');

                var encAdh = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(txtAadhaar_Number), key,
            {
                keySize: 128 / 8,
                iv: iv,
                mode: CryptoJS.mode.CBC,
                padding: CryptoJS.pad.Pkcs7
            });

                document.getElementById("<%=HAdh.ClientID %>").value = encAdh;
                document.getElementById("<%=txt_Aadhaar_Number.ClientID %>").value = encAdh;
            }
          
    }
</script>



    <table border="0" align="center" width="100%" height="100%">
        <tr align="center">
            <td valign="top" align="center">
                <table border="0" align="center" width="90%" height="100%">
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="5" width="100%" bordercolor="#ECC4B9"
                                bgcolor="#FBEBE6" align="center">
                                <tr>
                                    <td colspan="2" align="Right">
                                        <b>Step 1 </b>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <strong><font color="#990000">Online Application For Registration</font></strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <hr color="#990000" size="1">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2" bgcolor="#cccccc">
                                        Organisation
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
                                        <asp:Label ID="lbl_Org" runat="server" >Sponsoring Organisation Name</asp:Label>
                                        <font color="red">*</font>:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddl_OrgCode" runat="server" DataSourceID="SqlDataSource1"
                                            DataTextField="Organisation_Name" DataValueField="Organisation_Code" TabIndex="1"
                                            Width="300px" Enabled="true" >
                                            <asp:ListItem >Select</asp:ListItem>
                                             <asp:ListItem Value="23" >Select</asp:ListItem>
                                        </asp:DropDownList>
                                       
                                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                            ControlToValidate="ddl_OrgCode" ErrorMessage="*" InitialValue="0" 
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                            
                                       <%--<asp:RegularExpressionValidator ID="rev_OrgCode" runat="server" ControlToValidate="ddl_OrgCode"
                                            ErrorMessage="*" SetFocusOnError="True" ValidationExpression="^[1-9[0-9][0-9]*" ></asp:RegularExpressionValidator>--%>
                                            
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>"
                                            SelectCommand="SELECT [Organisation_Code], [Organisation_Name] 
                                                FROM [Sponsoring_Organisation_Master]
                                                WHERE Sponsoring_Organisation_Master.Organisation_Code=@OrgCode">
                                            <SelectParameters>
                                                <asp:SessionParameter Name="OrgCode" SessionField="UserOrg" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
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
                                        <asp:Label ID="lblBillingOrganisation" runat="server" >Payee Organisation Name</asp:Label>
                                        <font color="red">*</font>:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddl_BillerOrgCode" runat="server" DataSourceID="SqlDataSource2" AppendDataBoundItems="true"
                                            DataTextField="Payee_Organisation_Name" DataValueField="Payee_Organisation_Code" TabIndex="1"
                                            Width="300px" Enabled="true" >
                                            <asp:ListItem >Select</asp:ListItem>
                                            
                                        </asp:DropDownList>
                                       
                                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>"
                                            SelectCommand="SELECT [Payee_Organisation_Code], [Payee_Organisation_Name] 
                                                            FROM [Payee_Organisation_Master]">
                                            <SelectParameters>
                                                <asp:SessionParameter Name="OrgCode" SessionField="UserOrg" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
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
                                        &nbsp;</td>
                                    <td align="left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2" bgcolor="#cccccc">
                                        Personal Details
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        &nbsp;</td>
                                    <td align="left">
                                        &nbsp;</td>
                                </tr>

                                <!-- Added for validation change-->
                               <tr>
                                    <td align="left">
                                        <asp:Label ID="Label4" runat="server">EBCAS ID</asp:Label>
                                        <font color="red">*</font>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="Txt_EbasId" runat="server" Width="230px" MaxLength="12"
                                            TabIndex="9"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Txt_EbasId"
                                            ErrorMessage="Please Enter EBCAS ID"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">&nbsp;</td>
                                    <td align="left">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td><asp:Button ID="btnViewCandidateDetailsFrmEBCASNo" runat="server" Enabled="true"
                                            TabIndex="63"
                                            Text="View Candidate By EBCASId" Style="height: 26px" 
                                        OnClick="btnViewCandidateDetailsFrmEBCASNo_Click" CausesValidation="false" /></td>
                                </tr>
                                <tr>
                                    <td align="left">&nbsp;</td>
                                    <td align="left">&nbsp;</td>
                                </tr>
<!-- till here-->

                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lbl_candidate_name" runat="server" >Candidate Name</asp:Label><font
                                            color="red">*</font> :
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txt_candidate_name" runat="server" Width="230px" 
                                            MaxLength="50" TabIndex="2" Enabled="False"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="fte_candidate_name" runat="server" FilterMode="ValidChars"
                                                            Enabled="True" TargetControlID="txt_candidate_name" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZ abcdefghijklmnopqrstuvwxyz">
                                                        </cc1:FilteredTextBoxExtender>
                                                        <asp:RequiredFieldValidator ID="rfv_candidate_name" runat="server" ControlToValidate="txt_candidate_name"
                                                            ErrorMessage="Please Enter Name" SetFocusOnError="True"></asp:RequiredFieldValidator>
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
                                        <asp:Label ID="Label1" runat="server" >Organisation Name of Candidate</asp:Label><font
                                            color="red">*</font> :
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txt_organisation_name_of_candidate" runat="server" Width="400px" 
                                            MaxLength="100" TabIndex="3"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterMode="ValidChars"
                                                            Enabled="True" TargetControlID="txt_organisation_name_of_candidate" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZ abcdefghijklmnopqrstuvwxyz">
                                                        </cc1:FilteredTextBoxExtender>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_organisation_name_of_candidate"
                                                            ErrorMessage="Please Enter Organistion Name of Candidate" SetFocusOnError="True"></asp:RequiredFieldValidator>
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
                                        <asp:Label ID="lbl_EmpID" runat="server" >Pass ID</asp:Label>
                                        <font color="red">*</font>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txt_EmpID" runat="server" Width="230px" MaxLength="12" 
                                            TabIndex="5" AutoPostBack="True" OnTextChanged="txt_EmpID_TextChanged"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfv_EmpID" runat="server" ControlToValidate="txt_EmpID"
                                                            ErrorMessage="Please Enter Pass ID" 
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
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
                                        <asp:Label ID="lbl_Designation" runat="server" >Designation</asp:Label>
                                        <font color="red">*</font> :</td>
                                    <td align="left">
                                   
                                       <asp:DropDownList ID="ddl_Designation" runat="server" AppendDataBoundItems="True"
                                            DataSourceID="SqlDataDesignation" DataTextField="Description" DataValueField="Designation_Code"
                                            TabIndex="6" Width="300px" >
                                            <asp:ListItem >Select</asp:ListItem>
                                        </asp:DropDownList>
                                        
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="ddl_Designation" SetFocusOnError="True" InitialValue=0></asp:RequiredFieldValidator>--%>
                                       <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="ddl_Designation"
                                            ErrorMessage="*" SetFocusOnError="True" ValidationExpression="^[1-9[0-9][0-9]*" ></asp:RegularExpressionValidator>
                                            
                                        <asp:SqlDataSource ID="SqlDataDesignation" runat="server" ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>"
                                            
                                            SelectCommand="SELECT DISTINCT [Designation_Code], [Description] FROM [Designation_Master] ORDER BY Description">
                                        </asp:SqlDataSource>
                                        
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
                                         <asp:Label ID="lbl_Postedat" runat="server" >Posted at State</asp:Label>
                                        <font color="red">*</font> :</td>
                                    <td align="left">
                                     <asp:UpdatePanel ID="UpdatePanel2" runat="server"> 
                                    <ContentTemplate>
                                             <asp:DropDownList ID="drpStates" runat="server" DataSourceID="SqlDataSourceState" onselectedindexchanged="drpStates_SelectedIndexChanged"
                                            DataTextField="Name" DataValueField="ID" AppendDataBoundItems="true" TabIndex="7" AutoPostBack="true"
                                            Width="300px" Enabled="true" >
                                           <asp:ListItem Text="Select" Value="0" />                                        
                                        </asp:DropDownList>
                                       
                                        
                                            
                                        <asp:SqlDataSource ID="SqlDataSourceState" runat="server" ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>"
                                            SelectCommand="  Select ID , Name  FROM Location where Parent_ID=1 order by name">                                            
                                        </asp:SqlDataSource>
                                        </ContentTemplate></asp:UpdatePanel>
                                         <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="drpStates"
                                            ErrorMessage="*" SetFocusOnError="True" ValidationExpression="^[1-9[0-9][0-9]*" ></asp:RegularExpressionValidator>
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
                                         <asp:Label ID="Label3" runat="server" >Posted at City</asp:Label>
                                        <font color="red">*</font> :</td>
                                    <td align="left">
                                      <asp:UpdatePanel ID="UpdatePanel1" runat="server"> 
                                    <ContentTemplate>
                                             <asp:DropDownList ID="drpCity" runat="server" 
                                            DataTextField="Name" DataValueField="ID" AppendDataBoundItems="true" TabIndex="8"
                                            Width="300px" Enabled="true" 
                                                  >
                                           <asp:ListItem Text="Select" Value="0" />                                        
                                        </asp:DropDownList>
                                      
                                       </ContentTemplate></asp:UpdatePanel>
                                       <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="drpCity"
                                            ErrorMessage="*" SetFocusOnError="True" ValidationExpression="^[1-9[0-9][0-9]*" ></asp:RegularExpressionValidator>
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
                                        <asp:Label ID="Label5" runat="server"  Visible ="false" >BCAS ID</asp:Label>
                                       <%-- <font color="red">*</font>--%>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="Txt_BcasId" runat="server" Width="230px" MaxLength="12"  Visible ="false"
                                            TabIndex="10"></asp:TextBox>
                                                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Txt_BcasId"
                                                            ErrorMessage="Please Enter Bcas ID" 
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
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
                                        <asp:Label ID="Label2" runat="server" >Aadhaar Number</asp:Label><font
                                            color="red"></font> :
                                    </td>
                                    <td align="left">
                                    <asp:UpdatePanel ID="up1" runat="server"> 
                                    <ContentTemplate>
                                        <asp:TextBox ID="txt_Aadhaar_Number" runat="server" Width="150px" TextMode="Password"    onkeypress="return validate(event)" 
                                            MaxLength="150" TabIndex="11"></asp:TextBox><asp:Label ID="lblA" runat="server"
                                                Text="" Visible=false></asp:Label>
                                        <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterMode="ValidChars"
                                                            Enabled="True" TargetControlID="txt_organisation_name_of_candidate" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZ abcdefghijklmnopqrstuvwxyz">
                                                        </cc1:FilteredTextBoxExtender>--%>
                                                        <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True" TargetControlID="txt_Aadhaar_Number"
                                                            ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                       
                                                          <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txt_Aadhaar_Number"
                                                             SetFocusOnError="true"  ErrorMessage="Aadhaar Number must be 12 digit.." ValidationExpression="^[0-9]{12,12}$"></asp:RegularExpressionValidator>--%>
                                    <asp:HiddenField ID="HAdh" runat="server" />
                                     <asp:HiddenField ID="Hed" runat="server" />
                                     </ContentTemplate></asp:UpdatePanel>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lbl_gender_code" runat="server" >Gender</asp:Label>
                                        <font color="red">*</font> :
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddl_gender_code" runat="server" TabIndex="12" 
                                            >
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
                                        &nbsp;</td>
                                    <td align="left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lbl_date_of_birth" runat="server" >Date of Birth</asp:Label>
                                        (dd/mm/yyyy) <font color="red">*</font> :
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txt_date_of_birth" runat="server" TabIndex="13" Enabled="False" 
                                            ></asp:TextBox>
                                        <cc1:MaskedEditExtender ID="txt_date_of_birth_MaskedEditExtender" runat ="server" CultureName ="en-GB" 
                                Mask ="99/99/9999" MaskType ="Date" 
                             TargetControlID ="txt_date_of_birth" UserDateFormat ="DayMonthYear" >
                             </cc1:MaskedEditExtender>
                             <cc1:MaskedEditValidator ID ="mev_txt_date_of_birth" runat ="server" 
                                ControlExtender ="txt_date_of_birth_MaskedEditExtender"
                                ControlToValidate ="txt_date_of_birth" Display ="None" 
                                EmptyValueMessage ="Date of Birth Must Be Entered" IsValidEmpty ="false">
                             </cc1:MaskedEditValidator> 
                             <cc1:ValidatorCalloutExtender ID ="mev_callout_extender" runat ="server" 
                                TargetControlID ="mev_txt_date_of_birth">
                             </cc1:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <%-- <cc1:MaskedEditExtender ID="mee_relevant_experience" runat="server" CultureAMPMPlaceholder=""
                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                            Enabled="True" Mask="99.99" MaskType="Number" PromptCharacter="" TargetControlID="txt_relevant_experience">
                                                        </cc1:MaskedEditExtender>--%>
                                <tr>
                                    <td align="left">
                                        &nbsp;</td>
                                    <td align="left">
                                <asp:CompareValidator ID="Com_v_date_of_birth" runat="server" ControlToValidate="txt_date_of_birth"
                                            ErrorMessage="Date of Birth can't be Greater than Current Date OR Invalid Date" Operator="LessThanEqual"
                                                        Type="Date"></asp:CompareValidator>
                                                                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lbl_date_of_birth0" runat="server" >Nationality</asp:Label>
                                        <font color="red">*</font> :</td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddl_Nationality" runat="server" AppendDataBoundItems="True"
                                            DataSourceID="SqlDataNationality" DataTextField="Country_Name" DataValueField="Country_Code"
                                            TabIndex="14" Width="300px" >
                                            <asp:ListItem >Select</asp:ListItem>
                                        </asp:DropDownList>
                                       <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="ddl_Nationality"
                                            ErrorMessage="*" SetFocusOnError="True" ValidationExpression="^[1-9[0-9][0-9]*"></asp:RegularExpressionValidator>
                                         
                                            
                                        <asp:SqlDataSource ID="SqlDataNationality" runat="server" ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>"
                                            
                                            SelectCommand="SELECT [Country_Code], [Country_Name] FROM [Country_Master]">
                                        </asp:SqlDataSource>
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
                                        <asp:Label ID="lbl_mobile_no" runat="server" >Mobile No</asp:Label>
                                        <font color="red">*</font> :
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txt_mobile_no" runat="server" Width="112px" MaxLength="10" 
                                            TabIndex="17" Height="22px"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="fte_mobile_no" runat="server" Enabled="True" TargetControlID="txt_mobile_no"
                                                            ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                                            runat="server" ControlToValidate="txt_mobile_no" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="rev_mobile_no" runat="server" ControlToValidate="txt_mobile_no"
                                                             SetFocusOnError="true"  ErrorMessage="Please Enter Valid Mobile No." ValidationExpression="^[1-9][0-9]{9}"></asp:RegularExpressionValidator>
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
                                        <asp:Label ID="lbl_email_id" runat="server" >Email ID</asp:Label>
                                        <font color="red">* </font>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txt_email_id" runat="server" Width="229px" MaxLength="50" 
                                            TabIndex="18"></asp:TextBox>
                                       <asp:RequiredFieldValidator ID="rfv_email_id" runat="server" ControlToValidate="txt_email_id"
                                                             SetFocusOnError="true"  ErrorMessage="Please Enter Valid Email Id"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="rev_email_id" runat="server" ControlToValidate="txt_email_id"
                                                             SetFocusOnError="true"  ErrorMessage="Please Enter Valid Email Id" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
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
                                        <asp:Label ID="lbl_qualification" runat="server" >Educational Qualification</asp:Label>
                                        <font color="red">*</font> :
                                    </td>
                                    <td align="left">
                                        <%--<filteredtextboxextender id="fte_qualification" runat="server" enabled="True" targetcontrolid="txt_qualification"
                                            validchars="ABCDEFGHIJKLMNOPQRSTUVWXYZ abcdefghijklmnopqrstuvwxyz,.-_">  </filteredtextboxextender>--%>
                                            
                                        <asp:DropDownList ID="ddl_Qualification" runat="server" AppendDataBoundItems="True"
                                            DataSourceID="SqlDataQualification" DataTextField="Description" DataValueField="Qualification_Code"
                                            TabIndex="19" Width="300px">
                                            <asp:ListItem>Select</asp:ListItem>
                                        </asp:DropDownList>
                                       <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="ddl_Qualification"
                                            ErrorMessage="*" SetFocusOnError="True" ValidationExpression="^[1-9[0-9][0-9]*"></asp:RegularExpressionValidator>
                                           
                                            
                                            <asp:SqlDataSource ID="SqlDataQualification" runat="server" ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>"
                                            SelectCommand="SELECT [Qualification_Code], [Description] FROM [Qualification_Master]">
                                        </asp:SqlDataSource>
                                                      
                                        <%--<asp:RequiredFieldValidator ID="rfv_qualification" runat="server" ControlToValidate="ddl_Qualification"
                                            SetFocusOnError="True" ErrorMessage="Please Enter Qulifying Qualification" ></asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        &nbsp;</td>
                                    <td align="left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2" bgcolor="#cccccc">
                                        Declaration
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2">
                                        <asp:Label ID="lbl_agreed_declaration1" meta:resourcekey="lbl_agreed_declaration1" runat="server" 
                                            EnableTheming="False" ForeColor="#0000CC"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2" >
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2" >
                                    <asp:UpdatePanel  ID="up2" runat="server">
                                    <ContentTemplate>
                                   
                                    <%-- <asp:CheckBox ID="chk_declaration" runat="server" Text="I Agree" AutoPostBack="True" 
                                            TabIndex="21" oncheckedchanged="chk_declaration_CheckedChanged" />--%>
                                           <asp:CheckBox ID="chk_declaration" runat="server" Text="I Agree" 
                                            TabIndex="21"  />
                                        <font color="red">&nbsp; *</font> :
                                       
                                         </ContentTemplate></asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="style1">
                                        <font color="#990000" size="2">(*) Star marked fields are essentially to be filled by
                                            the candidate. </font>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2">
                                        <asp:Label ID="lbl_error" runat="server" Font-Size="Large"
                                            ForeColor="Red" ></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:Button ID="btn_confirm" runat="server" Enabled="true"
                                            TabIndex="63"    PostBackUrl="~/Forms/DetailsForms/ApplicantDetails_Preview.aspx"
                                            Text="Continue"  style="height: 26px" Visible="False" />

                                       <cc1:ConfirmButtonExtender ID="btn_confirm_ConfirmButtonExtender" ConfirmText="Press OK if Data entered is correct otherwise press Cancel to Make Corrections !!!!! "
                                            runat="server" Enabled="True"  TargetControlID="btn_confirm" OnClientCancel="" >
                                        </cc1:ConfirmButtonExtender>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2">
                                        <asp:BulletedList ID="bullet_NewUser" runat="server" Width="350px" BulletStyle="Numbered"
                                            Font-Bold="True" ForeColor="#FF3300" >
                                        </asp:BulletedList>
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
