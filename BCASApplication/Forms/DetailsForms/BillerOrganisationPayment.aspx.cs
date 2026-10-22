using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BCASClassLibrary;
using System.Data.SqlClient;

public partial class Forms_DetailsForms_BillerOrganisationPayment : System.Web.UI.Page
{
    
    TYP_PayeeOrganisationPayment objPay = new TYP_PayeeOrganisationPayment();
    
    TYP_FeePaymentsBySO objFee = new TYP_FeePaymentsBySO();

    TYP_PaymentResponse objRes = new TYP_PaymentResponse();

    protected void Page_Load(object sender, EventArgs e)
    {
        DisableForwordButton();
        HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
        HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.Cache.SetNoStore();
       

        if (!Page.IsPostBack)
        {

            //lblPayeeOrgName.Text = Session["OrgName"].ToString();



            if (Convert.ToString(HttpContext.Current.Session["UserId"]).Trim().Length == 0)
            {
                ClearCookiesSessions();
                Response.Redirect("~/Home.aspx");               
            }
            if (Convert.ToString(HttpContext.Current.Session["OrgName"]).Trim().Length == 0)
            {
                lblPayeeOrgName.Visible = false;
            }
            else
            {
                lblPayeeOrgName.Text = Session["OrgName"].ToString();
                lblPayeeOrgName.Visible = true;
            }
            ddlSponsoredOrg.SelectedValue = Session["UserOrg"].ToString();

            // Load proforma invoices for the selected organization
            LoadProformaInvoices();

        }


    }


    protected void btnPayNow_Click(object sender, EventArgs e)
    {
        CompareValidator1.ValueToCompare = "0";
        if (Convert.ToString(HttpContext.Current.Session["UserId"]).Trim().Length != 0)
        {
            //&&&&&&&&&&& Check in the Payment Balance Table for the record

            ////using (DataTable dt = objFee.getAmountBalance(Convert.ToInt32(ddlSponsoredOrg.SelectedValue), Convert.ToInt32(Session["UserOrg"])))
            ////{
            ////    if (dt.Rows.Count == 0)
            ////    {
            ////        //******** Insert Into Payment Balance Table

            ////        objRes.i_Sponsoring_Organisation_Code = Convert.ToInt32(ddlSponsoredOrg.SelectedValue);
            ////        objRes.i_Payee_Organisation_Code = Convert.ToInt32(Session["UserOrg"].ToString());
            ////        using (DataTable dt_new = objRes.Insert_PaymentBalance())
            ////        {
            ////            if (Convert.ToInt32(dt_new.Rows[0][0]) != 0)
            ////            {
            ////                //******** Insert Into Payment Balance Table
            ////                lbl_ErrorMsg.Text = "Error Occured";
            ////            }
            ////        }

            ////    }

            ////}

            //************
            //objPay.i_Payee_Organisation_Code = Convert.ToInt32(Session["UserOrg"].ToString());
            //objPay.i_Sponsoring_Organisation_Code = Convert.ToInt32(ddlSponsoredOrg.SelectedValue);
            //objPay.n_Amount_To_Pay = Convert.ToInt32(txtAmount.Text.Trim());
            //objPay.v_User_ID = Session["user_id"].ToString();
            Int32 OrgID = Convert.ToInt32(ddlSponsoredOrg.SelectedValue);
            Int64 ProfromaInvoiceID = Convert.ToInt64(ddlProformaInvoice.SelectedValue);
            string ProformaInvoiceNo = ddlProformaInvoice.SelectedItem.Text;
            Double TotalAmount = Convert.ToDouble(txtAmount.Text);
            DateTime LastDate = DateTime.Today.AddDays(-2);


            string User_ID = Session["user_id"].ToString();

            string constr = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection Conn = new SqlConnection(constr))
            {
                Conn.Open();
                var trans = Conn.BeginTransaction();
                try
                {

                    GetLastDateForPayment(ProfromaInvoiceID, trans, Conn);

                    LastDate = LastDatePayment;

                    if (LastDate >= DateTime.Today)
                    {
                        using (DataTable dt_Pay = objPay.InsertOnlineTransaction(OrgID, ProfromaInvoiceID, ProformaInvoiceNo, TotalAmount, User_ID))
                        {
                            if (Convert.ToInt32(dt_Pay.Rows[0][0]) != 0)
                            {
                                lbl_ErrorMsg.Text = "Error Occured";
                            }
                            else
                            {
                                Session["Transaction_ID"] = (dt_Pay.Rows[0][1].ToString());
                                Response.Redirect("~/Forms/DetailsForms/MakePayment.aspx");

                            }

                        }

                    }
                    else
                    {
                        lbl_ErrorMsg.Text = "Last Date " + LastDate.ToString("yyyy-MM-dd") + " for payment is Over!!";
                    }
                }
                
                catch (Exception ex)
                {
                    trans.Rollback();
                    trans.Dispose();

                }
                if (Conn.State != ConnectionState.Closed)
                    Conn.Close();
            }

           

        }

        else
        {
            ClearCookiesSessions();
            Response.Redirect("~/Home.aspx");

        }
    }


    public void DisableForwordButton()
    {
        Response.Expires = 0;
        Response.Cache.SetNoStore();
        Response.AppendHeader("Pragma", "no-cache");
    }


    public void ClearCookiesSessions()
    {
        Session.Abandon();
        Session.Clear();
        Session.RemoveAll();

        string[] myCookies = Request.Cookies.AllKeys;
        foreach (string cookie in myCookies)
        {
            Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
        }
    }


    protected void ddlSponsoredOrg_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtAmount.Text = "";
        lblExamDate.Text = "";
        lblCourseName.Text = "";
        lblFeeAmount.Text = "";
        lblServiceTax.Text = "";

        LoadProformaInvoices();
    }

    private void LoadProformaInvoices()
    {
        ddlProformaInvoice.Items.Clear();
        ddlProformaInvoice.Items.Insert(0, new ListItem("--Select--", "0"));

        if (ddlSponsoredOrg.SelectedValue != null && ddlSponsoredOrg.SelectedValue != "0")
        {
            Int64 OrgID = Convert.ToInt64(ddlSponsoredOrg.SelectedValue);
            Int64 PayeeOrgID = Convert.ToInt64(Session["UserOrg"].ToString());

            string constr = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    // Query to get unpaid proforma invoices for the selected organization
                    cmd.CommandText = @"SELECT p.ID, p.Proforma_Invoice_No, cs.Date_of_Exam, cm.Course_Name 
                                       FROM Proforma_Invoice p 
                                       JOIN Course_Schedule cs ON p.Schedule_ID = cs.Schedule_ID 
                                       JOIN Course_Master cm ON cs.Course_Code = cm.Course_Code 
                                       WHERE p.Sponsoring_OrgCode = @OrgID 
                                       AND p.Payee_OrgCode = @PayeeOrgID 
                                       AND p.Payment_Status = 'Pending'
                                       AND cs.Date_of_Exam >= GETDATE()
                                       ORDER BY cs.Date_of_Exam";

                    cmd.Parameters.AddWithValue("@OrgID", OrgID);
                    cmd.Parameters.AddWithValue("@PayeeOrgID", PayeeOrgID);
                    cmd.Connection = con;
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string invoiceText = reader["Proforma_Invoice_No"].ToString();
                            string invoiceValue = reader["ID"].ToString();
                            ddlProformaInvoice.Items.Add(new ListItem(invoiceText, invoiceValue));
                        }
                    }
                    con.Close();
                }
            }
        }
    }

    DateTime LastDatePayment = DateTime.Today.AddDays(-2);
    public void GetLastDateForPayment(Int64 ProformaInvoiceID, SqlTransaction trans, SqlConnection Conn)
    {
        try
        {


            using (SqlCommand cmd = new SqlCommand("GetLastDateForPayment", Conn))
            {
                if (Conn.State != ConnectionState.Open)
                    Conn.Open();
                cmd.Transaction = trans;
                //DateTime LastDate = DateTime.Today.AddDays(-2);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProformaInvoiceID", ProformaInvoiceID);

                cmd.Parameters.Add("@LastDate", SqlDbType.Date).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                string id = cmd.Parameters["@LastDate"].Value.ToString();

                 LastDatePayment = Convert.ToDateTime(id);

            }
        }

        catch (Exception ex)
        {
            trans.Rollback();
        }

    }
     
    protected void ddlProformaInvoice_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtAmount.Text="";
        if (ddlProformaInvoice.SelectedValue != "0")
        {


            Int64 OrgID = Convert.ToInt64(ddlSponsoredOrg.SelectedValue);
            Int64 ProfromaInvoiceID = Convert.ToInt64(ddlProformaInvoice.SelectedValue);
            string constr = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select convert(VARCHAR(10), cs.Date_of_Exam,103) Date_of_Exam , cm.Course_Name,p.Fee_Amount, p.Service_Tax, p.Total_Fee_With_Service_Tax from Proforma_Invoice p join Course_Schedule cs on p.Schedule_ID=cs.Schedule_ID join Course_Master cm on cs.Course_Code=cm.Course_Code where  ID=@ProfromaInvoiceID and Payee_OrgCode=@OrgID";

                    cmd.Parameters.AddWithValue("@OrgID", OrgID);
                    cmd.Parameters.AddWithValue("@ProfromaInvoiceID", ProfromaInvoiceID);
                    cmd.Connection = con;
                    con.Open();
                    using (var  reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Double amount = Convert.ToDouble(reader["Total_Fee_With_Service_Tax"].ToString());
                            // Convert.ToDouble(cmd.ExecuteScalar());
                            txtAmount.Text = amount.ToString();
                            lblExamDate.Text = reader["Date_of_Exam"].ToString();
                            txtAmount.Text = string.Format("{0:0.00}", amount);
                            lblFeeAmount.Text = string.Format("{0:0.00}", Convert.ToDouble(reader["Fee_Amount"].ToString()));
                            lblServiceTax.Text = string.Format("{0:0.00}", Convert.ToDouble(reader["Service_Tax"].ToString()));
                            lblCourseName.Text = reader["Course_Name"].ToString();
                        }
                    }
                        

                    //txtAmount.Text = cmd.ExecuteScalar().ToString();
                   
                    con.Close();
                }
            }
        }
    }
}
