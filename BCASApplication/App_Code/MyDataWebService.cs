using System;
using System.Web;
using System;
using System.Web;
using System.Collections;
using System.Configuration;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using BCASClassLibrary;

/// <summary>
/// Summary description for LocationService
/// </summary>
[WebService(Namespace = "https://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//[System.Web.Script.Services.ScriptService()]
public class MyDataWebService : System.Web.Services.WebService
{

    public MyDataWebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public AjaxControlToolkit.CascadingDropDownNameValue[] GetDropDownYesNo(string knownCategoryValues, string category)
    {
        List<AjaxControlToolkit.CascadingDropDownNameValue>
            cascadingValues = new
            List<AjaxControlToolkit.CascadingDropDownNameValue>();

        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("Y-Yes", "Y"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("N-No", "N"));

        return cascadingValues.ToArray();
    }

    [WebMethod]
    public AjaxControlToolkit.CascadingDropDownNameValue[] GetDropDownGender(string knownCategoryValues, string category)
    {
        List<AjaxControlToolkit.CascadingDropDownNameValue>
            cascadingValues = new
            List<AjaxControlToolkit.CascadingDropDownNameValue>();

        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("M-Male", "M"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("F-Female", "F"));

        return cascadingValues.ToArray();
    }

    [WebMethod]
    public AjaxControlToolkit.CascadingDropDownNameValue[] GetDropDownSecretQuestion(string knownCategoryValues, string category)
    {
        List<AjaxControlToolkit.CascadingDropDownNameValue>
            cascadingValues = new
            List<AjaxControlToolkit.CascadingDropDownNameValue>();

        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("What town were you born in?","What town were you born in?"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("What town was your father born in?","What town was your father born in?"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("What is the name of the hospital in which you were born?","What is the name of the hospital in which you were born?"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("What is the first name of your best childhood friend?","hat is the first name of your best childhood friend?"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("What was the name of your primary school?","What was the name of your primary school?"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("What town was your mother born in?","What town was your mother born in?"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("What was your favourite food as a child?","What was your favourite food as a child?"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("What is the title of your favourite book?","What is the title of your favourite book?"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("Who is your favourite author?","Who is your favourite author?"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("Who is your favourite sports personality?","Who is your favourite sports personality?"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("Who is your favourite movie character?","Who is your favourite movie character?"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("What was your favourite childhood game?","What was your favourite childhood game?"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("What was your favourite cartoon character?","What was your favourite cartoon character?"));

        return cascadingValues.ToArray();
    }

    [WebMethod]
    public AjaxControlToolkit.CascadingDropDownNameValue[] GetDropDownStateMaster(string knownCategoryValues, string category)
    {
        List<AjaxControlToolkit.CascadingDropDownNameValue>
            cascadingValues = new
            List<AjaxControlToolkit.CascadingDropDownNameValue>();

        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("01-JAMMU AND KASHMIR", "1"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("02-HIMACHAL PRADESH", "2"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("03-PUNJAB", "3"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("04-CHANDIGARH", "4"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("05-UTTRANCHAL", "5"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("06-HARYANA", "6"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("07-DELHI", "7"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("08-RAJASTHAN", "8"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("09-UTTAR PRADESH", "9"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("10-BIHAR", "10"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("11-SIKKIM", "11"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("12-ARUNACHAL PRADESH", "12"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("13-NAGALAND", "13"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("14-MANIPUR", "14"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("15-MIZORAM", "15"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("16-TRIPURA", "16"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("17-MEGHALAYA", "17"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("18-ASSAM", "18"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("19-WEST BENGAL", "19"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("20-JHARKHAND", "20"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("21-ORISSA", "21"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("22-CHHATTISGARH", "22"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("23-MADHYA PRADESH", "23"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("24-GUJARAT", "24"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("25-DAMAN AND DIU", "25"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("26-DADRA AND NAGAR HAVELI", "26"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("27-MAHARASHTRA", "27"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("28-ANDHRA PRADESH", "28"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("29-KARNATAKA", "29"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("30-GOA", "30"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("31-LAKSHDWEEP", "31"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("32-KERALA", "32"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("33-TAMIL NADU", "33"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("34-PONDICHERRY", "34"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("35-ANDAMAN AND NICOBAR ISLANDS", "35"));
        return cascadingValues.ToArray();
    }

    [WebMethod]
    public AjaxControlToolkit.CascadingDropDownNameValue[] GetDropDownAppliedPostMaster(string knownCategoryValues, string category)
    {
        List<AjaxControlToolkit.CascadingDropDownNameValue>
            cascadingValues = new
            List<AjaxControlToolkit.CascadingDropDownNameValue>();

        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("01-INVESTIGATOR", "1"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("02-SUPERVISOR", "2"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("03-SURVEYOR", "3"));
        return cascadingValues.ToArray();
    }

    [WebMethod]
    public AjaxControlToolkit.CascadingDropDownNameValue[] GetDropDownGraduateMaster(string knownCategoryValues, string category)
    {
        List<AjaxControlToolkit.CascadingDropDownNameValue>
            cascadingValues = new
            List<AjaxControlToolkit.CascadingDropDownNameValue>();

        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("01-BA", "1"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("02-BSC", "2"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("03-BCOM", "3"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("04-BCA", "4"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("05-BED", "5"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("06-BE", "6"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("07-BTECH", "7"));
        return cascadingValues.ToArray();
    }

    [WebMethod]
    public AjaxControlToolkit.CascadingDropDownNameValue[] GetDropDownPostGraduateMaster(string knownCategoryValues, string category)
    {
        List<AjaxControlToolkit.CascadingDropDownNameValue>
            cascadingValues = new
            List<AjaxControlToolkit.CascadingDropDownNameValue>();

        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("01-MA", "1"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("02-MSC", "2"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("03-MCOM", "3"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("04-MTECH", "4"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("05-MCA", "5"));
        return cascadingValues.ToArray();
    }

    [WebMethod]
    public AjaxControlToolkit.CascadingDropDownNameValue[] GetDropDownOtherQualificationMaster(string knownCategoryValues, string category)
    {
        List<AjaxControlToolkit.CascadingDropDownNameValue>
            cascadingValues = new
            List<AjaxControlToolkit.CascadingDropDownNameValue>();

        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("01-CA", "1"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("02-CS", "2"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("03-ICWA", "3"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("04-PHD", "4"));
        return cascadingValues.ToArray();
    }

    [WebMethod]
    public AjaxControlToolkit.CascadingDropDownNameValue[] GetDropDownSubjectMaster(string knownCategoryValues, string category)
    {
        List<AjaxControlToolkit.CascadingDropDownNameValue>
            cascadingValues = new
            List<AjaxControlToolkit.CascadingDropDownNameValue>();

        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("01-ENGLISH", "1"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("02-HINDI", "2"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("03-MATHS", "3"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("04-PUNJABI", "4"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("05-STATISTICS", "5"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("06-SOCIOLOGY", "6"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("07-ECONOMICS", "7"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("08-GEOGRAPHY", "8"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("09-CHEMISTRY", "9"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("10-PHYSICS", "10"));
        return cascadingValues.ToArray();
    }

    [WebMethod]
    public AjaxControlToolkit.CascadingDropDownNameValue[] GetDropDownCategoryMaster(string knownCategoryValues, string category)
    {
        List<AjaxControlToolkit.CascadingDropDownNameValue>
            cascadingValues = new
            List<AjaxControlToolkit.CascadingDropDownNameValue>();

        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("01-SC", "1"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("02-ST", "2"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("03-OBC", "3"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("04-GEN", "4"));
        return cascadingValues.ToArray();
    }

    [WebMethod]
    public AjaxControlToolkit.CascadingDropDownNameValue[] GetDropDownProfessionMaster(string knownCategoryValues, string category)
    {
        List<AjaxControlToolkit.CascadingDropDownNameValue>
            cascadingValues = new
            List<AjaxControlToolkit.CascadingDropDownNameValue>();

        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("01-EMPLOYED", "1"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("02-RETIRED FROM CENTRE GOVERNMENT", "2"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("03-RETIRED FROM STATE GOVERNMENT", "3"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("04-RETIRED FROM PRIVATE SECTOR", "4"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("05-STUDENT", "5"));
        cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue("06-UNEMPLOYED", "6"));
        return cascadingValues.ToArray();
    }
}
