using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Net;
using System.IO;
using ASPSnippets.GoogleAPI;
using System.Web.Script.Serialization;
using BusinessLayer;
using System.Web.Services;

public partial class GplusSignUp : PageBase_Client
{
    tblCustomer objCustomrer;
    PageBase objPageBase;
    HttpCookie httpCookie;
    protected void Page_Load(object sender, EventArgs e)
    {
        GoogleConnect.ClientId = "46997156311-dtin83fcl635bj6kissb7d591ktgd81h.apps.googleusercontent.com";
        GoogleConnect.ClientSecret = "HP2dL6DheppmHjzmCFCsFHYo";
        GoogleConnect.RedirectUri = Request.Url.AbsoluteUri.Split('?')[0];

        if (!string.IsNullOrEmpty(Request.QueryString["code"]))
        {
            string code = Request.QueryString["code"];
            string json = GoogleConnect.Fetch("me", code);
            GoogleProfile profile = new JavaScriptSerializer().Deserialize<GoogleProfile>(json);
            // lblId.Text = profile.Id;
            string[] arIDs = profile.DisplayName.TrimEnd(' ').Split(' ');

            txtFirstName.Text = arIDs[0];
            txtLastName.Text = arIDs[1];
            txtEmailRegistration.Text = profile.Emails.Find(email => email.Type == "account").Value;
            if (profile.Gender == "male")
            {
                RbtnMale.Checked = true;
            }
            else
            {
                RbtnFeMale.Checked = true;
            }

            btngplus.Visible = false;
        }
        if (Request.QueryString["error"] == "access_denied")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Access denied.')", true);
        }
        if (!Page.IsPostBack)
        {
            ExitsSession();
            SetUpPageContent(ref metaDescription, ref metaKeywords);
            SetRegulerExpression();
            //     ResetRegistrationCotrol();

            objCommon = new clsCommon();
            string strRedirect = objCommon.GetParameterFromUrl(Enums.Enums_URLParameterType.ByNumber, "1");
            objCommon = null;
            if (strRedirect != "")
            {
                if (strRedirect == "TrackOrder")
                {
                    DInfo.ShowMessage("Please registrate first to track your order", Enums.MessageType.Information);
                }
                if (strRedirect == "WishList")
                {
                    DInfo.ShowMessage("Please registrate first to see your wishlist", Enums.MessageType.Information);
                }
            }
            objCommon = new clsCommon();
            objCommon = null;

        }
    }
    public void SetRegulerExpression()
    {
        revEmailRegistration.ValidationExpression = RXEmailRegularExpression;
        revEmailRegistration.ErrorMessage = "Invalid Email (" + RXEmailRegularExpressionMsg + ")";
        revMobile.ValidationExpression = RXPhoneRegularExpression;
        revMobile.ErrorMessage = "Invalid Mobile Number (" + RXNumericRegularExpressionMsg + ")";
        revPassword.ValidationExpression = RXPasswordRegularExpression;
        revPassword.ErrorMessage = "Invalid Password (" + RXPasswordRegularExpressionMsg + ")";
        revConfirmPassword.ValidationExpression = RXPasswordRegularExpression;
        revConfirmPassword.ErrorMessage = "Invalid Password (" + RXPasswordRegularExpressionMsg + ")";
        REVFirstName.ValidationExpression = RXAlphaRegularExpression;
        REVFirstName.ErrorMessage = "Invalid First Name ( Alphabates Only)";
        REVLastName.ValidationExpression = RXAlphaRegularExpression;
        REVLastName.ErrorMessage = "Invalid Last Name ( Alphabates Only)";

    }
    protected void brnCreate_Click(object sender, EventArgs e)
    {
        if (SaveCustomer())
        {
            objPageBase = new PageBase();
            Response.Redirect(objPageBase.GetAlias("default.aspx"));
            objPageBase = null;
            //DisplayInfoRegistration.ShowMessage("Your account has been created successfully, For security purpose, check your mail to activate your account.", Enums.MessageType.Successfull);
            ResetRegistrationCotrol();
        }
    }
    public bool SaveCustomer()
    {
        objCommon = new clsCommon();
        if (objCommon.IsRecordExists("tblCustomer", tblCustomer.ColumnNames.AppEmailID, tblCustomer.ColumnNames.AppCustomerID, txtEmailRegistration.Text))
        {
            DisplayInfoRegistration.ShowMessage("Email address already exist.", Enums.MessageType.Error);
            return false;
        }
        objCommon = null;
        tblCustomer objCustomer = new tblCustomer();
        objCustomer.AddNew();
        objCustomer.AppFirstName = txtFirstName.Text.Trim();
        objCustomer.AppLastName = txtLastName.Text.Trim();
        objCustomer.AppEmailID = txtEmailRegistration.Text.Trim();
        objCustomer.AppMobile = txtMobile.Text;
        objCustomer.AppAddress = txtAddress.Text;
        if (RbtnMale.Checked)
        {
            objCustomer.AppGender = true;
        }
        else
        {
            objCustomer.AppGender = false;
        }
        objEncrypt = new clsEncryption();
        objCustomer.AppPassword = objEncrypt.Encrypt(txtPasswordRegistration.Text, appFunctions.strKey);
        objEncrypt = null;

        objCustomer.AppIsActive = true;
        objCustomer.AppIsVerified = true;
        objCustomer.AppCreatedDate = GetDateTime();
        objCustomer.Save();

        HttpContext.Current.Session[appFunctions.Session.ClientUserID.ToString()] = objCustomer.AppCustomerID;
        HttpContext.Current.Session[appFunctions.Session.ClientUserName.ToString()] = objCustomer.AppFirstName + " " + objCustomer.AppLastName;

        objCustomer = null;
        return true;
    }
    public void ResetRegistrationCotrol()
    {
        txtFirstName.Text = "";
        txtLastName.Text = "";
        txtEmailRegistration.Text = "";
        txtMobile.Text = "";
        txtPasswordRegistration.Attributes.Add("value", "");
        txtAddress.Text = "";
        RbtnMale.Checked = true;
    }

    protected void Login1(object sender, EventArgs e)
    {
        GoogleConnect.Authorize("profile", "email");
    }

    public class GoogleProfile
    {
        //public string Id { get; set; }
        public string DisplayName { get; set; }
        //public Image Image { get; set; }
        public List<Email> Emails { get; set; }
        public string Gender { get; set; }
        public string ObjectType { get; set; }
    }

    public class Email
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }

}