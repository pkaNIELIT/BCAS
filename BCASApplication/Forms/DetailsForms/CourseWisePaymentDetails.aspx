<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CourseWisePaymentDetails.aspx.cs" Inherits="Forms_DetailsForms_CourseWisePaymentDetails" Culture="en-GB" MaintainScrollPositionOnPostBack="true" Title="BCAS-->CourseWisePaymentDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        /* ============================================================
           PAGE LAYOUT
           ============================================================ */
        .cwp-page            { width: 98%; margin: 0 auto; padding: 10px 0 20px 0; }
        .cwp-heading         { text-align: center; margin: 8px 0 14px 0; }
        .cwp-heading span    { font-size: 16px; font-weight: bold; color: #990000; }

        /* Each block on the page (filters, candidates, receipt) is a "section" */
        .cwp-section         { margin: 0 auto 16px auto; }
        .cwp-section-title   { background-color: #990000; color: #FFFFFF; font-weight: bold;
                               padding: 5px 10px; text-align: left; }

        /* ============================================================
           1. FILTER SECTION
           ============================================================ */
        .payment-filter-table { border: thin groove #993366; background-color: #FBEBE6;
                                width: 100%; border-collapse: collapse; }
        .payment-label       { width: 20%; text-align: right; vertical-align: middle;
                               font-weight: bold; padding: 8px 10px; }
        .payment-field       { width: 80%; text-align: left; vertical-align: middle; padding: 8px 10px; }
        .payment-actions     { background-color: #FFCCCC; text-align: left; padding: 6px 10px; }
        .payment-message     { text-align: left; padding: 4px 10px; }

        /* ============================================================
           2. PAYMENT DETAILS GRID
           ============================================================ */
        .grid-scroll         { width: 100%; overflow-x: auto; }
        .grid-scroll table   { margin: 0 auto; }

        .verify-btn          { color: #FFFFFF !important; font-weight: bold; min-width: 110px;
                               padding: 3px 8px; border: 1px solid #333333; cursor: pointer; }
        .verify-no           { background-color: #CC0000 !important; }
        .verify-yes          { background-color: #008000 !important; }

        /* ============================================================
           3. CANDIDATE SELECTION SECTION
           ============================================================ */
        .cand-toolbar        { text-align: left; padding: 8px 10px; }
        .cand-toolbar input  { margin-right: 4px; }
        .cand-message        { text-align: left; padding: 0 10px 6px 10px; }
        .cand-grid           { text-align: left; padding: 6px 10px 10px 10px; }

        /* ============================================================
           4. PAYMENT RECEIPT SECTION
           ============================================================ */
        .receipt-table       { border: thin groove #993366; background-color: #FFFFCC;
                               width: 100%; border-collapse: collapse; }
        .receipt-title       { text-align: center; font-weight: bold; color: #990000; padding: 6px; }
        .receipt-label       { width: 30%; text-align: right; font-weight: bold; padding: 4px 10px; }
        .receipt-field       { width: 70%; text-align: left; padding: 4px 10px; }
        .receipt-actions     { text-align: center; padding: 8px; }
        .receipt-narrow      { width: 70%; }

        /* Print: show only the receipt */
        @media print {
            .cwp-hide-print  { display: none !important; }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="cwp-page">

        <%-- ===================== PAGE HEADING ===================== --%>
        <div class="cwp-heading cwp-hide-print">
            <asp:Label ID="lblHeading" runat="server" Font-Bold="True" ForeColor="#990000" Text="Course-wise Payment Receipt / Payment Details"></asp:Label>
        </div>

        <%-- Everything below refreshes in place (no full page reload, page stays where it is).
             Excel downloads are listed as PostBackTriggers because a file download cannot happen inside a partial refresh. --%>
        <asp:UpdatePanel ID="updMain" runat="server" UpdateMode="Always">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExportExcel" />
                <asp:PostBackTrigger ControlID="btnCandDownload" />
            </Triggers>
            <ContentTemplate>

        <%-- ===================== 1. FILTERS ===================== --%>
        <div class="cwp-section cwp-hide-print">
            <table cellpadding="0" cellspacing="0" class="payment-filter-table">
                <tr>
                    <td class="payment-label">Course Schedule</td>
                    <td class="payment-field">
                        <asp:DropDownList ID="ddlSchedule" runat="server" Width="550px" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlSchedule_SelectedIndexChanged">
                            <asp:ListItem Value="0">--All Course Schedules--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="payment-actions">&nbsp;</td>
                    <td class="payment-actions">
                        <asp:Button ID="btnClear" runat="server" CausesValidation="False" ForeColor="#000099" Text="Clear" OnClick="btnClear_Click" />
                        &nbsp;
                        <asp:Button ID="btnExportExcel" runat="server" CausesValidation="False" ForeColor="#000099" Text="Download Excel" OnClick="btnExportExcel_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="payment-message">
                        <asp:Label ID="lbl_ErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>

        <%-- ===================== 2. PAYMENT DETAILS GRID ===================== --%>
        <div class="cwp-section cwp-hide-print">
            <div class="grid-scroll">
                <asp:GridView ID="grdPaymentDetails" runat="server" AutoGenerateColumns="False" EmptyDataText="No Payment Details Found" BorderColor="#000066" BorderStyle="Outset" BorderWidth="2px" Caption="Payment Details" CaptionAlign="Top" ForeColor="#333333" AllowPaging="True" AllowSorting="True" PageSize="20" DataKeyNames="Transaction_id" OnRowCommand="grdPaymentDetails_RowCommand" OnRowCreated="grdPaymentDetails_RowCreated" OnPageIndexChanging="grdPaymentDetails_PageIndexChanging" Visible="False" CellPadding="4">
                    <Columns>
                        <%-- ---- Receipt ---- --%>
                        <asp:TemplateField HeaderText="Receipt">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkReceipt" runat="server" CommandName="ShowReceipt" CommandArgument='<%# Eval("Transaction_id") %>' Text="View"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%-- ---- Course details ---- --%>
                        <asp:BoundField DataField="Course_Name" HeaderText="Course" SortExpression="Course_Name" />
                        <asp:BoundField DataField="CourseSchedule" HeaderText="Course Schedule" SortExpression="CourseSchedule" />
                        <asp:BoundField DataField="Sponsoring_Organisation" HeaderText="Sponsoring Organisation" SortExpression="Sponsoring_Organisation" />
                        <asp:BoundField DataField="Payee_Organisation_Name" HeaderText="Payee Organisation" SortExpression="Payee_Organisation_Name" />

                        <%-- ---- Payment identifiers ---- --%>
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

                        <%-- ---- Amount and payment status ---- --%>
                        <asp:BoundField DataField="Amount_Recieved" HeaderText="Amount" SortExpression="Amount_Recieved" />
                        <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date" SortExpression="PaymentDate" />
                        <asp:BoundField DataField="PaymentTime" HeaderText="Payment Time" SortExpression="PaymentTime" />
                        <asp:BoundField DataField="Payment_Status" HeaderText="Payment Status" SortExpression="Payment_Status" />
                        <asp:BoundField DataField="ResponseDescription" HeaderText="Payment Description" SortExpression="ResponseDescription" />

                        <%-- ---- Payee details ---- --%>
                        <asp:BoundField DataField="GST_No" HeaderText="Payee GST Number" SortExpression="GST_Number" />
                        <asp:BoundField DataField="Pin_Code" HeaderText="Payee Pin Code" SortExpression="Payee_Pincode" />
                        <asp:BoundField DataField="Complete_Address" HeaderText="Payee Address" SortExpression="Payee_Address" />

                        <%-- ---- Verification ---- --%>
                        <asp:TemplateField HeaderText="Not Verified / Verified Status">
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
                                    Enabled='<%# IsVerified(Eval("Exam_Details_Verified")) %>'
                                    ToolTip="Select / view the candidates sent for verification (works after the payment is Verified)"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Wrap="False" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#990000" />
                    <HeaderStyle BackColor="#990000" ForeColor="White" />
                    <AlternatingRowStyle BackColor="#FFFFCC" ForeColor="#333333" />
                </asp:GridView>
            </div>
        </div>

        <%-- ===================== 3. CANDIDATE SELECTION (Step 2) ===================== --%>
        <asp:Panel ID="pnlCandidates" runat="server" Visible="False" CssClass="cwp-hide-print">
            <div class="cwp-section">
                <table cellpadding="0" cellspacing="0" class="receipt-table">
                    <tr>
                        <td class="receipt-title">
                            <asp:Label ID="lblCandTitle" runat="server" ForeColor="#990000"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="cand-toolbar">
                            <asp:Button ID="btnCandSelectAll" runat="server" CausesValidation="False" Text="Select All" OnClick="btnCandSelectAll_Click" />
                            <asp:Button ID="btnCandClearAll" runat="server" CausesValidation="False" Text="Clear All" OnClick="btnCandClearAll_Click" />
                            <asp:Button ID="btnCandSave" runat="server" CausesValidation="False" Text="Save Selected Candidates" OnClick="btnCandSave_Click" />
                            <asp:Button ID="btnCandDownload" runat="server" CausesValidation="False" Text="Download Excel" OnClick="btnCandDownload_Click" />
                            <asp:Button ID="btnCandClose" runat="server" CausesValidation="False" Text="Close" OnClick="btnCandClose_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="cand-message">
                            <asp:Label ID="lblCandMsg" runat="server" ForeColor="#006600"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="cand-grid">
                            <div class="grid-scroll">
                                <asp:GridView ID="grdCandidates" runat="server" AutoGenerateColumns="True" DataKeyNames="Candidate_ID"
                                    EmptyDataText="No candidates found" BorderColor="#000066" BorderStyle="Outset" BorderWidth="2px" CellPadding="4">
                                    <Columns>
                                        <%-- NEW: running serial number (1, 2, 3 ...) --%>
                                        <asp:TemplateField HeaderText="S.No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
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
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>

        <%-- ===================== 4. PAYMENT RECEIPT ===================== --%>
        <asp:Panel ID="pnlReceipt" runat="server" Visible="False">
            <div class="cwp-section receipt-narrow">
                <table cellpadding="0" cellspacing="0" class="receipt-table">
                    <tr>
                        <td colspan="2" class="receipt-title">Payment Receipt</td>
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
                        <td colspan="2" class="receipt-actions cwp-hide-print">
                            <asp:Button ID="btnPrintReceipt" runat="server" CausesValidation="False" Text="Print Receipt" OnClientClick="window.print(); return false;" />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>

            </ContentTemplate>
        </asp:UpdatePanel>

    <%-- Keeps the page exactly where it was after ANY click (Select Candidates, tick-boxes, Verify, Save ...).
         It remembers the scroll position (window + any scrolling box + the wide grids) before the click and puts it back afterwards. --%>
    <script type="text/javascript">
    (function () {
        var KEY = 'cwpScrollPos';

        function snapshot() {
            var s = { x: window.pageXOffset || 0, y: window.pageYOffset || 0, els: [], grids: [] };
            var all = document.getElementsByTagName('*');
            for (var i = 0; i < all.length; i++) {
                var el = all[i];
                if ((el.scrollTop > 0 || el.scrollLeft > 0) && el.className.toString().indexOf('grid-scroll') < 0) {
                    s.els.push({ el: el, t: el.scrollTop, l: el.scrollLeft });
                }
            }
            var g = document.querySelectorAll('.grid-scroll');
            for (var j = 0; j < g.length; j++) {
                s.grids.push({ t: g[j].scrollTop, l: g[j].scrollLeft });
            }
            return s;
        }

        function restore(s) {
            if (!s) { return; }
            function apply() {
                window.scrollTo(s.x, s.y);
                for (var i = 0; i < s.els.length; i++) {
                    s.els[i].el.scrollTop = s.els[i].t;
                    s.els[i].el.scrollLeft = s.els[i].l;
                }
                var g = document.querySelectorAll('.grid-scroll');
                for (var j = 0; j < s.grids.length && j < g.length; j++) {
                    g[j].scrollTop = s.grids[j].t;
                    g[j].scrollLeft = s.grids[j].l;
                }
            }
            apply();
            setTimeout(apply, 0);
            setTimeout(apply, 100);
        }

        // 1) In-place (partial) refresh: save before the click, restore after the update
        var snap = null;
        function hookAjax() {
            if (typeof Sys === 'undefined' || !Sys.WebForms || !Sys.WebForms.PageRequestManager) { return; }
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_beginRequest(function () { snap = snapshot(); });
            prm.add_endRequest(function () { restore(snap); });
        }
        window.addEventListener('load', hookAjax);

        // 2) Safety net: if a click ever does a full page reload, restore the position after it loads
        window.addEventListener('beforeunload', function () {
            try {
                var s = snapshot();
                sessionStorage.setItem(KEY, JSON.stringify({ u: location.pathname, ts: new Date().getTime(), x: s.x, y: s.y, grids: s.grids }));
            } catch (e) { }
        });
        window.addEventListener('load', function () {
            try {
                var raw = sessionStorage.getItem(KEY);
                if (!raw) { return; }
                sessionStorage.removeItem(KEY);
                var d = JSON.parse(raw);
                if (d.u === location.pathname && (new Date().getTime() - d.ts) < 10000) {
                    d.els = [];
                    restore(d);
                }
            } catch (e) { }
        });
    })();
    </script>

    </div>
</asp:Content>



<%--
=====================================================================
 STORY OF THIS PAGE (explained like you are 10 years old)
 Last updated: 19 Nov 2026 (SERVER version)
=====================================================================

 WHAT THIS PAGE IS
 -----------------
 A big notebook of "who paid money for which exam". One line = one payment.

 WHAT WE HAD ALREADY DONE
 ------------------------
 1. Column "Verify Exam Details": checkbox + button.
    Red "Not Verified" -> click -> Green "Verified" (click again = red).
 2. FINANCE can only LOOK (greyed-out controls) and sees only the
    Verified payments.
 3. "Number of Candidates" is a link: click = Excel with all details of the
    candidates of that payment.
 4. Column "Verify Candidates": after "Verified", click "Select Candidates
    (5/10)", tick the candidates and press "Save Selected Candidates".

 WHAT WE DID NOW (SERVER VERSION)
 --------------------------------
 1. FINANCE can now open the link, which reads "View Candidates (2/4)"
    for finance. They see ONLY the ticked candidates (2 of 4), with only
    "Download Excel" and "Close". No tick-boxes, no Select All, no Save.
 2. Behind the page, the .aspx.cs now uses its own plain SQL instead of
    new stored procedures (the server does not use them). Nothing you see
    on this page changed because of that.

 WHAT IS LEFT TO DO
 ------------------
 1. Run the small SQL script on the server database, then upload this
    .aspx and the .aspx.cs file.
 2. Test with 3 logins: admin, payee/sponsoring, finadmin.
 3. Old payments start as "Not Verified", so finance sees nothing until
    somebody verifies payments.
 4. Ask Varun sir: if a payment goes back to "Not Verified", should the
    ticked candidates be cleared? (Now they are kept.)
 5. Later idea: names instead of codes (gender, designation ...) in Excel.
=====================================================================
--%>
