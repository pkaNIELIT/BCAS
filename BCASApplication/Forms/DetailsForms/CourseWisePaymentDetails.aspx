<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CourseWisePaymentDetails.aspx.cs" Inherits="Forms_DetailsForms_CourseWisePaymentDetails" Culture="en-GB" Title="BCAS-->CourseWisePaymentDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .payment-filter-table
        {
            border: thin groove #993366;
            background-color: #FBEBE6;
            width: 95%;
        }
        .payment-label
        {
            width: 30%;
            text-align: right;
            vertical-align: top;
            font-weight: bold;
        }
        .payment-field
        {
            width: 70%;
            text-align: left;
            vertical-align: top;
        }
        .receipt-table
        {
            border: thin groove #993366;
            background-color: #FFFFCC;
            width: 70%;
        }
        .receipt-label
        {
            width: 35%;
            text-align: right;
            font-weight: bold;
        }
        .receipt-field
        {
            width: 65%;
            text-align: left;
        }
        .verify-btn
        {
            color: #FFFFFF !important;
            font-weight: bold;
            min-width: 110px;
            padding: 3px 8px;
            border: 1px solid #333333;
            cursor: pointer;
        }
        .verify-no  { background-color: #CC0000 !important; }
        .verify-yes { background-color: #008000 !important; }
        .auto-style1 {
            height: 36px;
        }
        .auto-style2 {
            width: 70%;
            text-align: left;
            vertical-align: top;
            height: 36px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table width="100%" align="center">
        <tr>
            <td align="center">&nbsp;</td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="lblHeading" runat="server" Font-Bold="True" ForeColor="#990000" Text="Course-wise Payment Receipt / Payment Details"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">&nbsp;</td>
        </tr>
        <tr>
            <td align="center">
                <table cellpadding="5" cellspacing="0" class="payment-filter-table">
                    <tr>
                        <td class="payment-label">Course Schedule</td>
                        <td class="payment-field">
                            <asp:DropDownList ID="ddlSchedule" runat="server" Width="550px" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlSchedule_SelectedIndexChanged">
                                <asp:ListItem Value="0">--All Course Schedules--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr bgcolor="#ffcccc">
                        <td class="auto-style1"></td>
                        <td class="auto-style2">
                            <asp:Button ID="btnClear" runat="server" CausesValidation="False" ForeColor="#000099" Text="Clear" OnClick="btnClear_Click" />
                            &nbsp;
                            <asp:Button ID="btnExportExcel" runat="server" CausesValidation="False" ForeColor="#000099" Text="Download Excel" OnClick="btnExportExcel_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left">
                            <asp:Label ID="lbl_ErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">&nbsp;</td>
        </tr>
        <tr>
            <td align="center">
                <asp:GridView ID="grdPaymentDetails" runat="server" AutoGenerateColumns="False" EmptyDataText="No Payment Details Found" BorderColor="#000066" BorderStyle="Outset" BorderWidth="2px" Caption="Payment Details" CaptionAlign="Top" ForeColor="#333333" AllowPaging="True" AllowSorting="True" PageSize="20" DataKeyNames="Transaction_id" OnRowCommand="grdPaymentDetails_RowCommand" OnPageIndexChanging="grdPaymentDetails_PageIndexChanging" Visible="False">
                    <Columns>
                        <asp:TemplateField HeaderText="Receipt">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkReceipt" runat="server" CommandName="ShowReceipt" CommandArgument='<%# Eval("Transaction_id") %>' Text="View"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Course_Name" HeaderText="Course" SortExpression="Course_Name" />
                        <asp:BoundField DataField="CourseSchedule" HeaderText="Course Schedule" SortExpression="CourseSchedule" />
                        <asp:BoundField DataField="Sponsoring_Organisation" HeaderText="Sponsoring Organisation" SortExpression="Sponsoring_Organisation" />
                        <asp:BoundField DataField="Payee_Organisation_Name" HeaderText="Payee Organisation" SortExpression="Payee_Organisation_Name" />
                        <asp:BoundField DataField="ProformaInvoiceNo" HeaderText="Proforma Invoice No." SortExpression="ProformaInvoiceNo" />
                        <asp:BoundField DataField="Transaction_id" HeaderText="Transaction ID" SortExpression="Transaction_id" />
                        <asp:BoundField DataField="Reference_No" HeaderText="Reference No." SortExpression="Reference_No" />
                        <%-- NEW (19 Nov 2026): number is now a clickable link. Click = Excel with all candidates of that payment --%>
                        <asp:TemplateField HeaderText="Number of Candidates" SortExpression="No_of_candidates">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkCandidates" runat="server" CausesValidation="False"
                                    CommandName="DownloadCandidates" CommandArgument='<%# Eval("Transaction_id") %>'
                                    Text='<%# Eval("NoOfCandidates") %>' ToolTip="Click to download all candidate details in Excel"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Amount_Recieved" HeaderText="Amount" SortExpression="Amount_Recieved" />
                        <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date" SortExpression="PaymentDate" />
                        <asp:BoundField DataField="PaymentTime" HeaderText="Payment Time" SortExpression="PaymentTime" />
                        <asp:BoundField DataField="Payment_Status" HeaderText="Payment Status" SortExpression="Payment_Status" />
                        <asp:BoundField DataField="ResponseDescription" HeaderText="Payment Description" SortExpression="ResponseDescription" />
                        <asp:BoundField DataField="GST_No" HeaderText="Payee GST Number" SortExpression="GST_Number" />
                        <asp:BoundField DataField="Pin_Code" HeaderText="Payee Pin Code" SortExpression="Payee_Pincode" />
                        <asp:BoundField DataField="Complete_Address" HeaderText="Payee Address" SortExpression="Payee_Address" />
                        <asp:TemplateField HeaderText="Verify Exam Details">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkVerified" runat="server" AutoPostBack="True"
                                    Checked='<%# IsVerified(Eval("Exam_Details_Verified")) %>'
                                    Enabled='<%# !IsFinanceUser %>'
                                    OnCheckedChanged="chkVerified_CheckedChanged" CausesValidation="False" />
                                <asp:Button ID="btnVerify" runat="server" CausesValidation="False"
                                    CommandName="ToggleVerify" CommandArgument='<%# Eval("Transaction_id") %>'
                                    Enabled='<%# !IsFinanceUser %>'
                                    Text='<%# IsVerified(Eval("Exam_Details_Verified")) ? "Verified" : "Not Verified" %>'
                                    CssClass='<%# IsVerified(Eval("Exam_Details_Verified")) ? "verify-btn verify-yes" : "verify-btn verify-no" %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Wrap="False" />
                        </asp:TemplateField>
                        <%-- NEW (Step 2): choose which candidates are sent for verification. Works only AFTER "Verified" --%>
                        <asp:TemplateField HeaderText="Verify Candidates">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkSelectCandidates" runat="server" CausesValidation="False"
                                    CommandName="SelectCandidates" CommandArgument='<%# Eval("Transaction_id") %>'
                                    Text='<%# GetCandidateButtonText(Eval("Candidates_Selected"), Eval("NoOfCandidates")) %>'
                                    Enabled='<%# !IsFinanceUser && IsVerified(Eval("Exam_Details_Verified")) %>'
                                    ToolTip="Verify exam details first, then choose candidates"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Wrap="False" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#990000" />
                    <HeaderStyle BackColor="#990000" ForeColor="White" />
                    <AlternatingRowStyle BackColor="#FFFFCC" ForeColor="#333333" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td align="center">&nbsp;</td>
        </tr>
        <tr>
            <td align="center">
                <%-- NEW (Step 2): candidate selection box --%>
                <asp:Panel ID="pnlCandidates" runat="server" Visible="False">
                    <table cellpadding="5" cellspacing="0" class="receipt-table" style="width:95%">
                        <tr>
                            <td align="center">
                                <strong><asp:Label ID="lblCandTitle" runat="server" ForeColor="#990000"></asp:Label></strong>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Button ID="btnCandSelectAll" runat="server" CausesValidation="False" Text="Select All" OnClick="btnCandSelectAll_Click" />
                                <asp:Button ID="btnCandClearAll" runat="server" CausesValidation="False" Text="Clear All" OnClick="btnCandClearAll_Click" />
                                <asp:Button ID="btnCandSave" runat="server" CausesValidation="False" Text="Save Selected Candidates" OnClick="btnCandSave_Click" />
                                <asp:Button ID="btnCandDownload" runat="server" CausesValidation="False" Text="Download Excel" OnClick="btnCandDownload_Click" />
                                <asp:Button ID="btnCandClose" runat="server" CausesValidation="False" Text="Close" OnClick="btnCandClose_Click" />
                                <br />
                                <asp:Label ID="lblCandMsg" runat="server" ForeColor="#006600"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="overflow:auto">
                                <asp:GridView ID="grdCandidates" runat="server" AutoGenerateColumns="True" DataKeyNames="Candidate_ID"
                                    EmptyDataText="No candidates found" BorderColor="#000066" BorderStyle="Outset" BorderWidth="2px">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCand" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#990000" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#FFFFCC" ForeColor="#333333" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td align="center">&nbsp;</td>
        </tr>
        <tr>
            <td align="center">
                <asp:Panel ID="pnlReceipt" runat="server" Visible="False">
                    <table cellpadding="5" cellspacing="0" class="receipt-table">
                        <tr>
                            <td colspan="2" align="center">
                                <strong><font color="#990000">Payment Receipt</font></strong>
                            </td>
                        </tr>
                        <tr><td class="receipt-label">Course</td><td class="receipt-field"><asp:Label ID="lblReceiptCourse" runat="server"></asp:Label></td></tr>
                        <tr><td class="receipt-label">Course Schedule</td><td class="receipt-field"><asp:Label ID="lblReceiptSchedule" runat="server"></asp:Label></td></tr>
                        <tr><td class="receipt-label">Sponsoring Organisation</td><td class="receipt-field"><asp:Label ID="lblReceiptSponsoringOrg" runat="server"></asp:Label></td></tr>
                        <tr><td class="receipt-label">Payee Organisation</td><td class="receipt-field"><asp:Label ID="lblReceiptPayeeOrg" runat="server"></asp:Label></td></tr>
                        <tr><td class="receipt-label">Proforma Invoice No.</td><td class="receipt-field"><asp:Label ID="lblReceiptInvoice" runat="server"></asp:Label></td></tr>
                        <tr><td class="receipt-label">Transaction ID</td><td class="receipt-field"><asp:Label ID="lblReceiptTransaction" runat="server"></asp:Label></td></tr>
                        <tr><td class="receipt-label">Reference No.</td><td class="receipt-field"><asp:Label ID="lblReceiptReference" runat="server"></asp:Label></td></tr>
                        <tr><td class="receipt-label">Amount</td><td class="receipt-field"><asp:Label ID="lblReceiptAmount" runat="server"></asp:Label></td></tr>
                        <tr><td class="receipt-label">Payment Date</td><td class="receipt-field"><asp:Label ID="lblReceiptPaymentDate" runat="server"></asp:Label></td></tr>
                        <tr><td class="receipt-label">Payment Time</td><td class="receipt-field"><asp:Label ID="lblReceiptPaymentTime" runat="server"></asp:Label></td></tr>
                        <tr><td class="receipt-label">Payment Status</td><td class="receipt-field"><asp:Label ID="lblReceiptStatus" runat="server"></asp:Label></td></tr>
                        <tr><td class="receipt-label">Payment Description</td><td class="receipt-field"><asp:Label ID="lblReceiptDescription" runat="server"></asp:Label></td></tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="btnPrintReceipt" runat="server" CausesValidation="False" Text="Print Receipt" OnClientClick="window.print(); return false;" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>



<%--
=====================================================================
 STORY OF THIS PAGE (explained like you are 10 years old)
 Last updated: 19 Nov 2026 (FINAL version)
=====================================================================

 WHAT THIS PAGE IS
 -----------------
 A big notebook of "who paid money for which exam". One line = one payment.

 WHAT WE HAD ALREADY DONE
 ------------------------
 1. Column "Verify Exam Details": checkbox + button.
    Red "Not Verified" -> click -> Green "Verified" (click again = red).
 2. FINANCE can only LOOK (greyed-out controls), and the database shows
    finance only the Verified payments.
 3. The first SQL (3 new columns, UpdateExamDetailsVerification, finance
    filter, FINANCE role key) was run on the live database.

 WHAT WE DID NOW (FINAL)
 -----------------------
 1. "Number of Candidates" is a link. Click = Excel with ALL details of
    the candidates of that payment (name, registration no, DOB, mobile,
    Aadhaar, organisation, nomination and fee details ...).
    The details come from 3 tables: NOMINATIONS_FOR_COURSES (who was
    nominated) + Candidate_Registration + Candidate_Registration_Screeners
    (who the person is). The SQL handles the different column names
    (Registration_No vs Registration_Number).
 2. New column "Verify Candidates" ("Select Candidates (5/10)"). It wakes up
    only AFTER the payment is "Verified". Click it -> a box with every
    candidate and a checkbox. Tick the ones to send for verification
    (e.g. 5 out of 10) and press "Save Selected Candidates".
    Select All / Clear All / Download Excel / Close buttons are in the box.
 3. Finance cannot tick anything. Finance's candidate Excel contains ONLY
    the ticked candidates. Admin / Payee / Sponsoring see all candidates.
 4. Only these two page files changed - BCASClassLibrary is untouched.

 WHAT IS LEFT TO DO
 ------------------
 1. Take a backup, then run CourseWisePaymentDetails_FINAL_LiveDB_Script.sql
    on the LIVE database BEFORE uploading these page files.
 2. Run the checks at the bottom of that SQL file. Check (b) must return NO
    rows (it means the candidates found = Number of Candidates on every
    invoice). If it returns rows, the link between invoice and nominations
    needs a small fix - tell the developer.
 3. Test with 3 logins: admin, a payee/sponsoring user, finadmin.
 4. Ask Varun sir: if a payment goes back to "Not Verified", should the
    ticked candidates be cleared? (Now they are kept.)
 5. Later idea: show names instead of codes (Sex_Code, Designation_Code,
    Nationality_Code, Qualification_Code) in the Excel by joining the
    master tables.
=====================================================================
--%>
