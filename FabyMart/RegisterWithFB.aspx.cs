using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.Script.Serialization;
using BusinessLayer;
public partial class RegisterWithFB : PageBase_Client
{
    public PageBase objPageBase = new PageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SetUpPageContent(ref metaDescription, ref metaKeywords);
            
        }
       
        if (Request.Form["signed_request"] != null)
        {
            string strName = string.Empty;
            string strBirthday = string.Empty;
            string strGender = string.Empty;
            string strEmail = string.Empty;
            string strPwd = string.Empty;
            Location strLocation = default(Location);

            string[] requestArray = Request.Form["signed_request"].ToString().Split('.');
            string dataString = base64Decode(requestArray[1]);

            JavaScriptSerializer js = new JavaScriptSerializer();
            FBResponse fb = js.Deserialize<FBResponse>(dataString);
            strName = fb.registration.name;
            strBirthday = fb.registration.birthday;
            strGender = fb.registration.gender;
            strEmail = fb.registration.email;
            strPwd = fb.registration.password;
            strLocation = fb.registration.location;

          /*  objEncrypt = new clsEncryption();
            tblCustomer objCustomer = new tblCustomer();
            objCustomer.AddNew();
            objCustomer.AppSellerName = strName;
            objSeller.AppPassword = objEncrypt.Encrypt(strPwd, appFunctions.strKey);
            objSeller.s_AppDateOfBirth = FormatDateString(strBirthday, "MM-dd-yyyy", "dd-MM-yyyy");
            if (strGender.ToLower() == "male")
            {
                objSeller.AppGender = false;
            }
            else
            {
                objSeller.AppGender = true;
            }
            objSeller.AppEmail = strEmail;
            objSeller.AppIsEmailVerify = true;
            objSeller.AppIsMobileVerify = false;
            objSeller.AppApproved = true;
            if ((strLocation != null))
            {
                Array strLocationArray = strLocation.name.Split(",");

                tblState objState = new tblState();
                objState.Where.AppState.Value = strLocationArray(1).ToString.Trim();
                objState.Query.Load();
                if (objState.RowCount > 0)
                {
                    objSeller.AppStateID = objState.AppStateID;
                    tblCity objCity = new tblCity();
                    objCity.Where.AppCityName.Value = strLocationArray(0).ToString.Trim();
                    objCity.Query.Load();
                    if (objCity.RowCount > 0)
                    {
                        objSeller.AppCityID = objCity.AppCityID;
                    }
                    objCity = null;
                }
                objState = null;
            }
            tblMembership objMemeberShip = new tblMembership();
            objMemeberShip.Where.AppIsFree.Value = true;
            objMemeberShip.Query.Load();
            if (objMemeberShip.RowCount > 0)
            {
                objSeller.AppMembershipID = objMemeberShip.AppMembershipID;
            }
            objMemeberShip = null;
            objSeller.Save();
            Session(appFunctions.Session.ClientUserID.ToString) = objSeller.AppSellerID;
            Session(appFunctions.Session.ClientUserName.ToString) = objSeller.AppSellerName;
            Response.Redirect(objPageBase.GetAlias("MyAccount.aspx"));*/
        }
    }

    public static string base64Decode(string data)
    {
        data = data.Replace("-", "+").Replace("_", "/");
        if (data.Length % 4 != 0)
        {
            while (data.Length % 4 != 0)
            {
                data = data + "=";
            }
        }
        byte[] binary = Convert.FromBase64String(data);
        return Encoding.UTF8.GetString(binary);
    }
}

public class Location
{
    public string name
    {
        get { return l_name; }
        set { l_name = value; }
    }
    private string l_name;
    public string id
    {
        get { return l_id; }
        set { l_id = value; }
    }
    private string l_id;
}
public class FBData
{
    public string name
    {
        get { return m_name; }
        set { m_name = value; }
    }
    private string m_name;
    public string birthday
    {
        get { return m_birthday; }
        set { m_birthday = value; }
    }
    private string m_birthday;
    public string gender
    {
        get { return m_gender; }
        set { m_gender = value; }
    }
    private string m_gender;
    public string email
    {
        get { return m_email; }
        set { m_email = value; }
    }
    private string m_email;
    public string password
    {
        get { return m_password; }
        set { m_password = value; }
    }
    private string m_password;
    public Location location
    {
        get { return m_location; }
        set { m_location = value; }
    }
    private Location m_location;
}
public class FBResponse
{
    public FBData registration
    {
        get { return m_registration; }
        set { m_registration = value; }
    }
    private FBData m_registration;
    public string algorithm
    {
        get { return m_algorithm; }
        set { m_algorithm = value; }
    }
    private string m_algorithm;
}