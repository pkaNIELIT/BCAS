<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="BCAS_ExamData.aspx.cs" Inherits="Forms_DetailsForms_BCAS_ExamData" Culture="en-GB" Title="BCAS Examination Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .FixedHeader {
            font-weight: bold;
            width: 1200px;
            position: absolute;
        }
    </style>

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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" align="center">
        <tr>
            <td colspan="2" align="center">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Label ID="lblHeading" runat="server" Font-Bold="True" ForeColor="#990000"
                    Text="BCAS Examination Data"></asp:Label>
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
                                OnSelectedIndexChanged="ddlSchedule_SelectedIndexChanged" AppendDataBoundItems="True" AutoPostBack="true" Height="17px">
                                <asp:ListItem Value="0">-Select Course Schedule-</asp:ListItem>
                            </asp:DropDownList>
                        &nbsp;<asp:Button ID="btnGenExamData" runat="server"  onclick="btnGenExamData_Click" 
                Text="Generate Exam Data" />

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


                  <div id="ScroolDiv" style="overflow: scroll; height: 500px; width: 1250px; left: auto;">
                        <asp:GridView ID="gvAll" runat="server"
                        AutoGenerateColumns="False" Font-Names="Arial"
                        Font-Size="11pt" AlternatingRowStyle-BackColor="#C2D69B"
                        HeaderStyle-BackColor="green" AllowPaging="True"
                        PageSize="250"
                        OnPageIndexChanging="OnPaging" DataKeyNames="Registration_No"
                        EmptyDataText="No record found." SelectedIndex="0" ForeColor="#333333"
                        Caption=" BCAS Exam Data Download" CaptionAlign="Top">

                        <Columns>

                            <asp:BoundField ItemStyle-Width="250px" DataField="S_NO"
                                HeaderText="S_NO">

                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField ItemStyle-Width="250px" DataField="Registration_No"
                                HeaderText="Regd.No">

                                <ItemStyle Width="250px"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField ItemStyle-Width="250px" DataField="Candidate_Name"
                                HeaderText="Name">

                                <ItemStyle Width="250px"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField ItemStyle-Width="175px" DataField="Designation"
                                HeaderText="Designation">

                                <ItemStyle Width="175px"></ItemStyle>
                            </asp:BoundField>


                            <asp:BoundField ItemStyle-Width="150px" DataField="PostedAtStateName"
                                HeaderText="PostedAtStateName">

                                <ItemStyle Width="150px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="PostedAtCityName"
                                HeaderText="PostedAtCityName">

                                <ItemStyle Width="150px"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField ItemStyle-Width="100px" DataField="Organisation_Name"
                                HeaderText="Sponsering_Organisation_Name">


                                <ItemStyle Width="100px"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField ItemStyle-Width="100px" DataField="EbasId"
                                HeaderText="EBCAS No.">

                                <ItemStyle Width="100px"></ItemStyle>
                            </asp:BoundField>

                             <asp:BoundField ItemStyle-Width="100px" DataField="ROLL_NO" HeaderText="Roll No."> 
                                 <ItemStyle Width="100px"></ItemStyle>
                             </asp:BoundField>


                            <asp:BoundField ItemStyle-Width="150px" DataField="Exam_Location_Name"
                                HeaderText="Exam_Location_Name">

                                <ItemStyle Width="150px"></ItemStyle>
                            </asp:BoundField>

                        </Columns>

                        <FooterStyle BackColor="#990000" />

                        <HeaderStyle BackColor="#990000" ForeColor="White"></HeaderStyle>

                        <AlternatingRowStyle BackColor="#FFFFCC" />

                    </asp:GridView>

                    <asp:HiddenField ID="hfCount" runat="server" Value="0" />

                    <asp:Label ID="lbl_error" runat="server"></asp:Label>


                </div>

            </td>
        </tr>
        <tr>
            <td width="30%" colspan="2" style="width: 100%"></td>
        </tr>
        <tr>
            <td width="30%" colspan="2" style="width: 100%" align="center">


             
                   <asp:Button ID="btnExamData" runat="server"  onclick="btnExamData_Click" 
                Text="Download Exam Data " />

            </td>
        </tr>
    </table>
</asp:Content>

