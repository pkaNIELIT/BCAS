<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TaxInvoiceUpload.aspx.cs" Inherits="Forms_DetailsForms_TaxInvoiceUpload" Culture="en-GB" Title="BCAS - Tax Invoice Upload / Download" %>

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
        .action-cell
        {
            text-align: left;
            white-space: nowrap;
        }
        .action-section
        {
            padding: 3px 0;
        }
        .action-divider
        {
            border-top: 1px dashed #990000;
            margin: 4px 0;
        }
        .refno-cell
        {
            max-width: 90px;
            word-break: break-all;
            white-space: normal;
        }
        .btn-download
        {
            background-color: #CC0000;
            color: #FFFFFF;
            font-weight: bold;
            border: 1px solid #990000;
            padding: 2px 8px;
            cursor: pointer;
        }
        .btn-download:hover
        {
            background-color: #A30000;
        }
        .btn-download[disabled]
        {
            background-color: #E0A3A3;
            border-color: #C99;
            cursor: not-allowed;
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
                <asp:Label ID="lblHeading" runat="server" Font-Bold="True" ForeColor="#990000" Text="Tax Invoice Upload"></asp:Label>
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
                            <br />
                            <asp:Label ID="lblScheduleHint" runat="server" ForeColor="#663300" Font-Italic="True"></asp:Label>
                        </td>
                    </tr>
                    <tr bgcolor="#ffcccc">
                        <td>&nbsp;</td>
                        <td class="payment-field">
                            <asp:Button ID="btnClear" runat="server" CausesValidation="False" ForeColor="#000099" Text="Clear" OnClick="btnClear_Click" />
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
                <asp:GridView ID="grdPaymentDetails" runat="server" AutoGenerateColumns="False" EmptyDataText="No Payment Details Found" BorderColor="#000066" BorderStyle="Outset" BorderWidth="2px" Caption="Payment Details" CaptionAlign="Top" ForeColor="#333333" AllowPaging="True" AllowSorting="True" PageSize="20" DataKeyNames="Transaction_id" OnRowCommand="grdPaymentDetails_RowCommand" OnPageIndexChanging="grdPaymentDetails_PageIndexChanging" OnRowDataBound="grdPaymentDetails_RowDataBound" Visible="False">
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
                        <asp:BoundField DataField="Reference_No" HeaderText="Reference No." SortExpression="Reference_No">
                            <ItemStyle CssClass="refno-cell" Width="90px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Amount_Recieved" HeaderText="Amount" SortExpression="Amount_Recieved" />
                        <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date" SortExpression="PaymentDate" />
                        <asp:BoundField DataField="PaymentTime" HeaderText="Payment Time" SortExpression="PaymentTime" />
                        <asp:BoundField DataField="Payment_Status" HeaderText="Payment Status" SortExpression="Payment_Status" />
                        <asp:BoundField DataField="ResponseDescription" HeaderText="Payment Description" SortExpression="ResponseDescription" />

                        <asp:TemplateField HeaderText="Tax Invoice">
                            <ItemTemplate>
                                <div class="action-cell">
                                    <asp:Label ID="lblUploadStatus" runat="server" Font-Bold="True"></asp:Label>

                                    <%-- Top section: Finance / Admin only - choose a PDF, upload it, and
                                         download back whatever is currently on file for this row. --%>
                                    <asp:Panel ID="pnlUploadSection" runat="server" CssClass="action-section">
                                        <div class="action-divider"></div>
                                        <asp:FileUpload ID="fuInvoice" runat="server" />
                                        <asp:Button ID="btnUpload" runat="server" Text="Upload" CommandName="UploadInvoice"
                                            CommandArgument='<%# Eval("Transaction_id") %>' CausesValidation="false" />
                                        <asp:Button ID="btnDownloadInvoice" runat="server" Text="Download" CommandName="DownloadInvoice"
                                            CommandArgument='<%# Eval("Transaction_id") %>' CausesValidation="false" CssClass="btn-download" />
                                    </asp:Panel>

                                    <%-- Bottom section: Payee only - download the tax invoice on file. --%>
                                    <asp:Panel ID="pnlDownloadSection" runat="server" CssClass="action-section">
                                        <div class="action-divider"></div>
                                        <asp:LinkButton ID="lnkDownloadInvoice" runat="server" CommandName="DownloadInvoice"
                                            CommandArgument='<%# Eval("Transaction_id") %>' Text="Download Tax Invoice"></asp:LinkButton>
                                        <asp:Label ID="lblNoInvoice" runat="server" ForeColor="Red" Text="No invoice available yet" Visible="false"></asp:Label>
                                    </asp:Panel>
                                </div>
                            </ItemTemplate>
                            <ItemStyle Width="200px" />
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
