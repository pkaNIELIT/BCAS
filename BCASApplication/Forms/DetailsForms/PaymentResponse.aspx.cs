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

public partial class Forms_DetailsForms_PaymentResponse : System.Web.UI.Page
{
   
    CalculateCheckSum objChkSum = new CalculateCheckSum();
    TYP_PaymentResponse objRes = new TYP_PaymentResponse();

    protected void Page_Load(object sender, EventArgs e)
    {

        trReciept.Visible = false;

        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //Request.Form["msg"]= "NIELIT|10000004|MSBI3364833892|IGU8802402|00003372.00|SBI|BILL_NIELIT|NA|INR|DIRECT|NA|NA|NA|29-05-2014 14:25:36|0300|NA|100025|BCAS01|20|NA|NA|NA|NA|NA|Completed successfully.|B8C6E796783D030FD63274BF7C78D0E508D75D7574037906766C78A25763E4EE";
        
        if (Request.Form["msg"] == null)
        {
            lblError.Text = "Unable to Recieve Response from Payment Gateway.";
            lblError.Visible = true;
        }
        else
        {
            string m = Request.Form["msg"].ToString();

            if (Request.Form["msg"].Length != 0)
            {

                string strResponse = "";
                strResponse = Request.Form["msg"].Trim(); //must be uncommented before publish

                
                //strResponse = "NIELIT|10000001|MCIT2846863919|110657-047933|2.00|CIT|22270726|NA|INR|DIRECT|NA|NA|NA|08-15-2012 15:08:42|0300|NA|1|NA|1|NA|NA|NA|NA|NA|Success|1903263657";

                //*******1057848156                      
                string chkSum = strResponse.Substring(strResponse.LastIndexOf("|") + 1);
                string strResWithoutChkSum = strResponse.Substring(0, strResponse.LastIndexOf("|") + 1);
                strResWithoutChkSum = strResWithoutChkSum + "WYZZHkyZuU9K";

                SHASample dataprg = new SHASample();
                String hash = String.Empty;
                hash = dataprg.GetSHA256(strResWithoutChkSum);
                hash = hash.ToUpper();             
                
                if (chkSum == hash )
                {                  
                    objRes.v_Response_String = strResponse;
                    using (DataTable dt_Pay = objRes.InsertTemp_ChkSum_Matched())
                    {
                        if (Convert.ToInt32(dt_Pay.Rows[0][0]) != 0)
                        {
                            lblError.Text = "Error Occured during CheckSum Matched.";
                        }

                    }                  

                    int rPayee_Organisation_Code;
                    int rSponsoring_Organisation_Code;
                    int rTransaction_ID;
                    string rTxnRef_No = "";
                    decimal rAmount_Deducted;
                    string rAuthStatus = "";
                    DateTime rTxn_Date;
                    string[] msgArray = strResponse.Split('|');
                    string trans;
                    trans = msgArray[1].ToString();
                    if(msgArray[1].ToString().Contains("-"))
                    {
                        trans = trans.Substring(9);
                        rTransaction_ID = Convert.ToInt32(trans);
                    }
                    else
                        rTransaction_ID = Convert.ToInt32(msgArray[1].ToString().Trim());
                    rPayee_Organisation_Code = Convert.ToInt32(msgArray[16].ToString().Trim());//payyee 18--sponsered_py
                    rSponsoring_Organisation_Code = Convert.ToInt32(msgArray[18].ToString().Trim());
                    rTxnRef_No = msgArray[2].ToString().Trim();
                    rAmount_Deducted = Convert.ToDecimal(msgArray[4].ToString().Trim());
                    rAuthStatus = msgArray[14].ToString().Trim();
               string     rAuthStatuss = msgArray[24].ToString().Trim();
                    //ORDRE0001-OrderId already exists with given mercid
                    //NIELIT|1234567|NA|NA|1.00|NA|NA|NA|INR|NA|NA|NA|NA|NA|NA|NA|3|BCAS01|3|NA|NA|NA|NA|ERR120|ORDRE0001-OrderId already exists with given mercid|9B2221AF481475DF00614A448B93561E3DB1E8B72DFA72F3AAC9417EDDD194D9
                    //*****               

                  //  rTxn_Date = DateTime.ParseExact(msgArray[13].ToString().Trim(), "dd-MM-yyyy HH:mm:ss", new System.Globalization.DateTimeFormatInfo());
                    rTxn_Date = DateTime.Now;
                    //**********             
                    objRes.i_Transaction_id = rTransaction_ID;
                    objRes.i_Payee_Organisation_Code = rPayee_Organisation_Code;
                    objRes.i_Sponsoring_Organisation_Code = rSponsoring_Organisation_Code;
                    objRes.v_Transaction_Reference_No = rTxnRef_No;
                    objRes.n_Fee_Amount_Deducted = (float)rAmount_Deducted;
                   // objRes.n_Fee_Amount_Deducted = Convert.ToDouble(rAmount_Deducted);
                    objRes.v_Payment_Status = rAuthStatus.Trim();
                    objRes.d_Transaction_Date = rTxn_Date;
                    objRes.v_Response_String = strResponse;

                    using (DataTable dt_Pay = objRes.Insert_PaymentResponse())
                    {
                        if (Convert.ToInt32(dt_Pay.Rows[0][0]) != 0)
                        {
                            lblError.Text = "Error Occured";
                        }
                        else
                        {

                            //********* Update Online transaction Table For Successful Payment*********
                            //if (rAuthStatus == ("0300").Trim())
                            if (rAuthStatus == ("0300").Trim())
                            {

                                rTxn_Date = DateTime.ParseExact(msgArray[13].ToString().Trim(), "dd-MM-yyyy HH:mm:ss", new System.Globalization.DateTimeFormatInfo());
                                TYP_PaymentResponse objUpdateResp = new TYP_PaymentResponse();
                                objUpdateResp.i_Transaction_id = rTransaction_ID;
                                objUpdateResp.n_Amount_Recieved = (float)rAmount_Deducted;
                                //objUpdateResp.n_Amount_Recieved = Convert.ToDouble( rAmount_Deducted); GetResponseDescription(txnResponseCode);
                                objUpdateResp.i_Payee_Organisation_Code = rPayee_Organisation_Code;
                                objUpdateResp.i_Sponsoring_Organisation_Code = rSponsoring_Organisation_Code;
                                objUpdateResp.v_Reference_No = rTxnRef_No;
                                objUpdateResp.d_Date_of_Payment = rTxn_Date;
                                objUpdateResp.v_Response_String = strResponse;
                                objUpdateResp.v_Payment_Status = rAuthStatus;
                                objUpdateResp.v_ResponseDescription = GetResponseDescription(rAuthStatus);
                                                               

                                //*****                            
                                using (DataTable dt_Update = objUpdateResp.Update_PaymentResponseOfSpon())
                                {
                                    if (Convert.ToInt32(dt_Update.Rows[0][0]) != 0)
                                    {
                                        lblError.Text = "Transaction Failed";
                                        lblError.Visible = true;
                                        trReciept.Visible = false;
                                    }
                                    else
                                    {
                                                                                
                                        //*******Display Payment Reciept                                        
                                        //********Select Sponsoring Organisation Name for display....
                                        using (DataTable dt = objRes.getPayeeOrgName(rPayee_Organisation_Code, rTransaction_ID))
                                        {
                                            if (dt.Rows.Count != 0)
                                            {
                                                vlblSponOrg.Text = dt.Rows[0]["Payee_Organisation_Name"].ToString();
                                                vlblCourse.Text = dt.Rows[0]["Course_Name"].ToString();
                                            }
                                        }
                                        //********Till Here
 
                                        vlblTransactionNo.Text = rTransaction_ID.ToString();
                                        vlblFeeAmount.Text = rAmount_Deducted.ToString();
                                        vlblPaymentDate.Text = msgArray[13].ToString().Trim();
                                        vlblPaymentStatus.Text = "SUCCESS";
                                        //*******
                                        //Label1.Text = "Payment Successful";
                                        trReciept.Visible = true;
                                        lblError.Visible = false;
                                        trError.Visible = false;
                                    }
                                }
                            }
                            else // update for failed transaction id for new id
                            {
                                if (rAuthStatus == "NA")
                                {
                                    rTxn_Date = DateTime.Now;
                                }
                                else
                                {
                                    rTxn_Date = DateTime.ParseExact(msgArray[13].ToString().Trim(), "dd-MM-yyyy HH:mm:ss", new System.Globalization.DateTimeFormatInfo());
                                }
                               // UpdateTransactionData(rTransaction_ID, rSponsoring_Organisation_Code);
                                //UpdateTransactionData(rTransaction_ID, rSponsoring_Organisation_Code, rTxnRef_No, rAuthStatus, GetResponseDescription(rAuthStatus), rTxn_Date, strResponse);
                                UpdateTransactionData(rTransaction_ID, rPayee_Organisation_Code, rTxnRef_No, rAuthStatus, GetResponseDescription(rAuthStatus), rTxn_Date, strResponse);

                                lblError.Text = "Transaction Failed, Try again for payment of Application Fees";
                                trReciept.Visible = false;
                                lblError.Visible = true;
                            }
                        }
                    }
                }
                else
                {

                    //*********              
                    objRes.v_Response_String = strResponse;
                    using (DataTable dt_Pay = objRes.InsertTempPaymetResponse())
                    {
                        if (Convert.ToInt32(dt_Pay.Rows[0][0]) != 0)
                        {
                            lblError.Text = "Error Occured during payment response Insertion";
                        }
                    }
                    //Tempered Message
                    lblError.Text = "Transaction Failed Due To Malicious Activities.";
                    lblError.Visible = true;
                    trReciept.Visible = false;
                }
            }
            else
            {
                lblError.Text = "Unable to Recieve Response from Payment Gateway";
                trReciept.Visible = false;

            } 
        } 
    }

    public static byte[] StrToByteArray(string str)
    {
        System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
        return encoding.GetBytes(str);
    }


    private void UpdateTransactionData(int transactionId, int orgId, string TxnRef_No, string rAuthStatus, string ResponseDescription, DateTime dop, string strResponse)
    {
        string constr = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
        using (SqlConnection Conn = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("UpdateForFailedTransaction", Conn))
            {
                Conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@transactionId", transactionId);
                cmd.Parameters.AddWithValue("@orgId", orgId);
                cmd.Parameters.AddWithValue("@TxnRef_No", TxnRef_No);
                cmd.Parameters.AddWithValue("@rAuthStatus", rAuthStatus);
                cmd.Parameters.AddWithValue("@ResponseDescription", ResponseDescription);
                cmd.Parameters.AddWithValue("@dop", dop);
                cmd.Parameters.AddWithValue("@strResponse", strResponse);
                cmd.ExecuteNonQuery();
            }
        }

    }
    public String GetResponseDescription(String responseCode)
    {
        String response = "unknown";
        switch (responseCode)
        {
            case "0300": response = "Success";
                break;
            case "0399": response = "Invalid Authentication at Bank";
                break;
            case "NA": response = "Invalid Input in the Request Message";
                break;
            case "0002": response = "Payment Gateway is waiting for Response from Bank";
                break;
            case "0001": response = "Error at Payment Gateway";
                break;
        }
        return response;
    }
}