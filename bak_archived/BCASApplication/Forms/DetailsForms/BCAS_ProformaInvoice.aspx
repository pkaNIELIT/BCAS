<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="BCAS_ProformaInvoice.aspx.cs" Inherits="Forms_DetailsForms_BCAS_ProformaInvoice" Culture="en-GB" Title="BCAS_Approved_Proforma_Invoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .FixedHeader {
            font-weight: bold;
            width: 1200px;
            position: absolute;
        }
    </style>

    <%--                 <script type = "text/javascript"  src="../../Script/jquery-1.7.1.js"></script>
            <script type = "text/javascript" >
                $(document).ready(function () {
                    var gridHeader = $('#<%=gvAll.ClientID%>').clone(true); // Here Clone Copy of Gridview with style
                    $(gridHeader).find("tr:gt(0)").remove(); // Here remove all rows except first row (header row)
                    $('#<%=gvAll.ClientID%> tr th').each(function (i) {
                        // Here Set Width of each th from gridview to new table(clone table) th 
                        $("th:nth-child(" + (i + 1) + ")", gridHeader).css('width', ($(this).width()).toString() + "px");
                    });
                    $("#GHead").append(gridHeader);
                    $('#GHead').css('position', 'absolute');
                    $('#GHead').css('top', $('#<%=gvAll.ClientID%>').offset().top);

                });
            </script>--%>
    <script type="text/javascript">

        function Check_Click(objRef) {
            //Get the Row based on checkbox
            var row = objRef.parentNode.parentNode;

            //Get the reference of GridView
            var GridView = row.parentNode;

            //Get all input elements in Gridview
            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {
                //The First element is the Header Checkbox
                var headerCheckBox = inputList[0];

                //Based on all or none checkboxes
                //are checked check/uncheck Header Checkbox
                var checked = true;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }
            headerCheckBox.checked = checked;

        }
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        inputList[i].checked = true;
                    }
                    else {
                        if (row.rowIndex % 2 == 0) {
                            row.style.backgroundColor = "#C2D69B";
                        }
                        else {
                            row.style.backgroundColor = "white";
                        }
                        inputList[i].checked = false;
                    }
                }
            }
        }

    </script>

    <script type="text/javascript">
        function ConfirmDelete() {
            var count = document.getElementById("<%=hfCount.ClientID %>").value;
            var gv = document.getElementById("<%=gvAll.ClientID%>");
            var chk = gv.getElementsByTagName("input");
            for (var i = 0; i < chk.length; i++) {
                if (chk[i].checked && chk[i].id.indexOf("chkAll") == -1) {
                    count++;
                }
            }
            if (count == 0) {
                alert("No Records to Update.");
                return false;
            }
            else {
                return confirm("Do you want to Update " + count + " records.");
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" align="center">
        <tr>
            <td colspan="2" align="center">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Label ID="lblHeading" runat="server" Font-Bold="True" ForeColor="#990000"
                    Text="NOMINATION APPROVED DEMAND NOTE GENERATION"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="30%"></td>
            <td width="70%"></td>
        </tr>
        <tr align="center">
            <td width="30%" colspan="2" style="width: 100%">
                <table width="65%" align="center" border="2">
                    <tr align="left">
                        <td width="30%">
                            <asp:Label ID="lblCourseSchedule" runat="server" Text="Course Schedule"></asp:Label>
                        </td>
                        <td width="70%">
                            <asp:DropDownList ID="ddlSchedule" runat="server" DataTextField="Course_Schedule"
                                DataValueField="Schedule_ID" Width="535px"
                                OnSelectedIndexChanged="ddlSchedule_SelectedIndexChanged" AppendDataBoundItems="True" AutoPostBack="true">
                                <asp:ListItem Value="0">-Select Course Schedule-</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr align="left">
                        <td width="30%" style="margin-left: 80px">
                            <asp:Label ID="lblSponsoringOrg" runat="server" Text="Sponsoring Organisation" Visible="False"></asp:Label>
                        </td>
                        <td width="70%">
                            <asp:DropDownList ID="ddlOrganisation" runat="server" Width="535px" DataTextField="Organisation_Name"
                                DataValueField="Organisation_Code"
                                AutoPostBack="True"
                                OnSelectedIndexChanged="ddlOrganisation_SelectedIndexChanged"
                                AppendDataBoundItems="True" Enabled="false" Visible="False">
                                <asp:ListItem Value="0">Select Sponsoring Organisation</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr align="left">
                        <td width="30%" style="margin-left: 80px">
                            <asp:Label ID="lblBillerOrg" runat="server" Text="Payee Organisation"></asp:Label>
                        </td>
                        <td width="70%">
                            <asp:DropDownList ID="ddlBillerOrganisation" runat="server" Width="535px" DataTextField="Organisation_Name"
                                DataValueField="Organisation_Code"
                                AutoPostBack="True"
                                OnSelectedIndexChanged="ddlOrganisation_SelectedIndexChanged"
                                AppendDataBoundItems="True" Enabled="false">
                                <asp:ListItem Value="0">Select Biller Organisation</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>

            </td>
        </tr>
        <tr>
            <td width="30%" colspan="2" style="width: 100%" align="center">&nbsp;</td>
        </tr>
        <tr>
            <td width="30%" colspan="2" style="width: 100%" align="center">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lblerror" runat="server" ForeColor="#FF3300"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </td>
        </tr>
        <tr align="center">
            <td width="30%" colspan="2" align="center">
                <div align="center">

                    <div id="ScroolDiv" style="overflow: scroll; height: 500px; width: 1250px;">

                        <asp:GridView ID="gvAll" runat="server"
                            AutoGenerateColumns="False" Font-Names="Arial"
                            Font-Size="11pt" AlternatingRowStyle-BackColor="#C2D69B"
                            HeaderStyle-BackColor="green" AllowPaging="True"
                            PageSize="250"
                            OnPageIndexChanging="OnPaging" DataKeyNames="Registration_Number,Schedule_ID"
                            EmptyDataText="No record found." SelectedIndex="0" ForeColor="#333333"
                            Caption="Candidates For BCAS DEMAND NOTE GENERATION" CaptionAlign="Top">

                            <Columns>

                                <asp:TemplateField>

                                    <HeaderTemplate>

                                        <asp:CheckBox ID="chkAll" runat="server"
                                            onclick="checkAll(this);" Text="Select All" />

                                    </HeaderTemplate>

                                    <ItemTemplate>

                                        <asp:CheckBox ID="chk" runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sr. No.">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Regd. No">
                                    <%-- <EditItemTemplate>
               <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Registration_Number") %>'></asp:TextBox>
           </EditItemTemplate>--%>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="Label1" NavigateUrl='<%# Bind("Registration_Number","~/Forms/DetailsForms/BCAS_ViewDetails.aspx?Regd_No={0}") %>'
                                            runat="server" Target="_blank" Text='<%# Bind("Registration_Number") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" />
                                </asp:TemplateField>



                                <asp:BoundField ItemStyle-Width="250px" DataField="Candidate_Name"
                                    HeaderText="Name">

                                    <ItemStyle Width="250px"></ItemStyle>
                                </asp:BoundField>

                                <asp:BoundField ItemStyle-Width="50px" DataField="Sex_Code"
                                    HeaderText="Sex">

                                    <ItemStyle Width="50px"></ItemStyle>
                                </asp:BoundField>


                                <asp:BoundField ItemStyle-Width="175px" DataField="Designation"
                                    HeaderText="Designation">

                                    <ItemStyle Width="175px"></ItemStyle>
                                </asp:BoundField>


                                <asp:BoundField ItemStyle-Width="150px" DataField="Qualification"
                                    HeaderText="Qualification">

                                    <ItemStyle Width="150px"></ItemStyle>
                                </asp:BoundField>


                                <asp:BoundField ItemStyle-Width="100px" DataField="DOB"
                                    HeaderText="DOB">

                                    <ItemStyle Width="100px"></ItemStyle>
                                </asp:BoundField>

                                <asp:BoundField ItemStyle-Width="100px" DataField="Mobile_No"
                                    HeaderText="Mobile No">

                                    <ItemStyle Width="100px"></ItemStyle>
                                </asp:BoundField>


                                <asp:BoundField ItemStyle-Width="150px" DataField="BCAS_Approval_Status"
                                    HeaderText="BCASApprovalStatus">

                                    <ItemStyle Width="150px"></ItemStyle>
                                </asp:BoundField>

                                <asp:BoundField ItemStyle-Width="150px" DataField="Organisation_Name"
                                    HeaderText="Sponsoring Organisation">

                                    <ItemStyle Width="150px"></ItemStyle>
                                </asp:BoundField>

                                <asp:BoundField ItemStyle-Width="100px" DataField="Schedule_ID"
                                    HeaderText="ScheduleID">

                                    <ItemStyle Width="100px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField ItemStyle-Width="100px" DataField="Demand_Note_ID"
                                    HeaderText="DemandNoteID">

                                    <ItemStyle Width="100px"></ItemStyle>
                                </asp:BoundField>

                                <%-- <asp:HyperLinkField HeaderText="Candidate" 
           NavigateUrl="~/Forms/DetailsForms/Default.aspx" Text="asdf" />
                                --%>
                            </Columns>

                            <FooterStyle BackColor="#990000" />

                            <HeaderStyle BackColor="#990000" ForeColor="White"></HeaderStyle>

                            <AlternatingRowStyle BackColor="#FFFFCC" />

                        </asp:GridView>

                        <asp:HiddenField ID="hfCount" runat="server" Value="0" />

                        <asp:Label ID="lbl_error" runat="server"></asp:Label>


                    </div>
                </div>

            </td>
        </tr>
        <tr>
            <td width="30%" colspan="2" style="width: 100%"></td>
        </tr>
        <tr>
            <td width="30%" colspan="2" style="width: 100%" align="center">


                <asp:Button ID="btnUpdateStatus" runat="server" OnClientClick="return ConfirmDelete();" OnClick="btnUpdateStatus_Click"
                    Text="GENERATE DEMAND NOTE FOR APPROVED CANDIDATES " />

            </td>
        </tr>
    </table>
</asp:Content>

