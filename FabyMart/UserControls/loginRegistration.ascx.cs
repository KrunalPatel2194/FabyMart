using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer;
using System.Text.RegularExpressions;

using System.Web.Services;
public partial class loginRegistration : ControlBase
{
    public PageBase objPageBase = new PageBase();
    HttpCookie httpCookie;
    clsCommon objCommon;
    tblCustomer objCustomrer;
    clsEncryption objEncrypt;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SetRegulerExpression();
            httpCookie = new HttpCookie("IsFirstTime");
            httpCookie.Expires = DateTime.Today.AddDays(10);
            Response.Cookies.Add(httpCookie);
        }
    }
    public void SetRegulerExpression()
    {
        revEmail.ValidationExpression = objPageBase.RXEmailRegularExpression;
        revEmail.ErrorMessage = "Invalid Email (" + objPageBase.RXEmailRegularExpressionMsg + ")";
        revEmailRegistration.ValidationExpression = objPageBase.RXEmailRegularExpression;
        revEmailRegistration.ErrorMessage = "Invalid Email (" + objPageBase.RXEmailRegularExpressionMsg + ")";
        revMobile.ValidationExpression = objPageBase.RXPhoneRegularExpression;
        revMobile.ErrorMessage = "Invalid Mobile Number (" + objPageBase.RXNumericRegularExpressionMsg + ")";

        revPassword.ValidationExpression = objPageBase.RXPasswordRegularExpression;
        revPassword.ErrorMessage = "Invalid Password (" + objPageBase.RXPasswordRegularExpressionMsg + ")";

        //revConfirmPassword.ValidationExpression = objPageBase.RXPasswordRegularExpression;
        //revConfirmPassword.ErrorMessage = "Invalid Password (" + objPageBase.RXPasswordRegularExpressionMsg + ")";
    }
    public void ShowRegistration(bool IsPage = false)
    {
        hdnIsPage.Value = IsPage.ToString();
        MPEloginRegistration.Show();
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (IsLogin())
        {
            tblCustomer objCustomer = new tblCustomer();
            objCustomer.Where.AppEmailID.Value = txtEmail.Text.Trim();
            objCustomer.Query.Load();
            if (objCustomer.RowCount > 0)
            {
                objEncrypt = new clsEncryption();
                if (string.Compare(objEncrypt.Decrypt(objCustomer.AppPassword, appFunctions.strKey), txtPassword.Text, false) != 0)
                {
                    DInfo.ShowMessage("Invalid user name or password.", Enums.MessageType.Error);
                }
                else
                {
                    if (objCustomer.AppIsVerified)
                    {
                        httpCookie = new HttpCookie("FabyMartUsername", objEncrypt.Encrypt(txtEmail.Text, appFunctions.strKey));
                        httpCookie.Expires = DateTime.Today.AddDays(10);
                        Response.Cookies.Add(httpCookie);
                        httpCookie = new HttpCookie("FabyMartPassword", objEncrypt.Encrypt(txtPassword.Text, appFunctions.strKey));
                        httpCookie.Expires = DateTime.Today.AddDays(10);
                        Response.Cookies.Add(httpCookie);
                        Session[appFunctions.Session.ClientUserID.ToString()] = objCustomer.AppCustomerID;
                        Session[appFunctions.Session.ClientUserName.ToString()] = objCustomer.AppFirstName + " " + objCustomer.AppLastName;
                        objCommon = new clsCommon();
                        string strRedirect = objCommon.GetParameterFromUrl(Enums.Enums_URLParameterType.ByNumber, "1");
                        objCommon = null;
                        Response.Redirect(objPageBase.GetAlias("Default.aspx"));
                    }
                    else
                    {
                        DInfo.ShowMessage("Your account is Not Verified.Plase verify it by your registered email address .", Enums.MessageType.Error);
                    }
                }
                objEncrypt = null;
            }
            else
            {
                DInfo.ShowMessage("Invalid user name or password.", Enums.MessageType.Error);
            }
            objCustomer = null;
        }
    }

    protected void brnCreate_Click(object sender, EventArgs e)
    {
        if (IsLoginRegistration())
        {
            if (SaveCustomer())
            {
                DInfo.ShowMessage("Check your Email please. Verification mail has been sent to your account.", Enums.MessageType.Successfull);
                ResetRegistrationCotrol();
            }
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
        //objCustomer.AppAddress = txtAddress.Text;
        //if (RbtnMale.Checked)
        //{
        //    objCustomer.AppGender = true;
        //}
        //else
        //{
        //    objCustomer.AppGender = false;
        //}
        clsEncryption objEncrypt = new clsEncryption();
        objCustomer.AppPassword = objEncrypt.Encrypt(txtPasswordRegistration.Text, appFunctions.strKey);
        objEncrypt = null;

        objCustomer.AppIsActive = true;
        objCustomer.AppIsVerified = false;
        objCustomer.AppCreatedDate = objPageBase.GetDateTime();
        objCustomer.Save();
        SendMail(objCustomer.s_AppCustomerID);
        objCustomer = null;
        return true;
    }
    public void SendMail(string strId)
    {
        try
        {
            PageBase objPageBase = new PageBase();
            objCommon = new clsCommon();
            string Strbody = "";
            string strSubject = "Account Activation Process";
            Strbody = objCommon.readFile(Server.MapPath("~/EmailTemplates/NewUserMail.html"));
            Strbody = Strbody.Replace("`link`", strServerURL);
            Strbody = Strbody.Replace("`uname`", txtFirstName.Text + " " + txtLastName.Text);
            objEncrypt = new clsEncryption();
            Strbody = Strbody.Replace("`linkActive`", objPageBase.GetAlias("Approved.aspx") + objEncrypt.Encrypt(strId, appFunctions.strKey));
            objEncrypt = null;
            objCommon.SendConfirmationMail(txtEmailRegistration.Text, strSubject, Strbody, Enums.Enum_Confirmation_Mail_type.register);
            //objCommon.SendMail(txtEmailRegistration.Text, strSubject, Strbody);
            objCommon = null;
            objPageBase = null;
        }
        catch (Exception ex)
        {
            Response.Write(ex.StackTrace.ToString());
        }

    }
    public void ResetRegistrationCotrol()
    {
        txtFirstName.Text = "";
        txtLastName.Text = "";
        txtEmailRegistration.Text = "";
        txtMobile.Text = "";
        txtPasswordRegistration.Attributes.Add("value", "");
        //txtAddress.Text = "";
        //RbtnMale.Checked = true;
    }

    [WebMethod]
    public static string UserLogin(string EmailID)
    {
        string s = "";
        if (HttpContext.Current.Session[appFunctions.Session.ClientUserID.ToString()] == null)
        {

            PageBase objPageBase = new PageBase();

            if (!string.IsNullOrEmpty(EmailID))
            {
                tblCustomer objCustomer = new tblCustomer();
                objCustomer.Where.AppEmailID.Value = EmailID;
                objCustomer.Query.Load();
                if (objCustomer.RowCount > 0)
                {
                    HttpContext.Current.Session[appFunctions.Session.ClientUserID.ToString()] = objCustomer.AppCustomerID;
                    HttpContext.Current.Session[appFunctions.Session.ClientUserName.ToString()] = objCustomer.AppFirstName + " " + objCustomer.AppLastName;
                    s = "true";
                    //HttpContext.Current.Response.Redirect(objPageBase.GetAlias("MyAccount.aspx"));
                }
                else
                {

                    // Response.Redirect(objPageBase.GetAlias("RegisterWithFB.aspx"));
                }
            }
        }
        else
        {
            s = "";
        }
        return s;
    }
    public Boolean IsLogin()
    {
        if (txtEmail.Text.Trim() == "" || txtPassword.Text.Trim() == "")
        {
            DInfo.ShowMessage("Enter user name and password.", Enums.MessageType.Error);
            return false;
        }
        //string strRegex = "^([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}" + "\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\" + ".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$";
        //Regex re = new Regex(strRegex);
        //if (!(re.IsMatch(txtEmail.Text.Trim())))
        //{
        //    DInfo.ShowMessage("Invalid Email (Ex. abc@ClothesonClick.com)", Enums.MessageType.Error);
        //    return false;
        //}
        tblCustomer objCustomer = new tblCustomer();
        objCustomer.Where.AppEmailID.Value = txtEmail.Text.Trim();
        objCustomer.Query.Load();
        if (objCustomer.RowCount > 0)
        {
            clsEncryption objEncrypt = new clsEncryption();
            if (string.Compare(objEncrypt.Decrypt(objCustomer.AppPassword, appFunctions.strKey), txtPassword.Text, false) != 0)
            {
                DInfo.ShowMessage("Invalid user name and password.", Enums.MessageType.Error);
                return false;
            }
            else
            {
                if (objCustomer.AppIsVerified)
                {

                    httpCookie = new HttpCookie("FabyMartUsername", objEncrypt.Encrypt(txtEmail.Text, appFunctions.strKey));
                    httpCookie.Expires = DateTime.Today.AddDays(10);
                    Response.Cookies.Add(httpCookie);
                    httpCookie = new HttpCookie("FabyMartPassword", objEncrypt.Encrypt(txtPassword.Text, appFunctions.strKey));
                    httpCookie.Expires = DateTime.Today.AddDays(10);
                    Response.Cookies.Add(httpCookie);
                    Session[appFunctions.Session.ClientUserID.ToString()] = objCustomer.AppCustomerID;
                    Session[appFunctions.Session.ClientUserName.ToString()] = objCustomer.AppFirstName + " " + objCustomer.AppLastName;
                }
                else
                {
                    DInfo.ShowMessage("Your account is Not Verified .", Enums.MessageType.Error);
                    return false;
                }
            }
            objEncrypt = null;
        }
        else
        {
            DInfo.ShowMessage("Invalid user name and password.", Enums.MessageType.Error);
            return false;
        }
        objCustomer = null;
        return true;
    }

    public Boolean IsLoginRegistration()
    {
        //if (txtFirstName.Text.Trim() == "" || txtLastName.Text.Trim() == "" || txtMobile.Text.Trim() == "" || txtEmailRegistration.Text.Trim() == "" || txtPasswordRegistration.Text.Trim() == "" || txtConfirmPassword.Text.Trim() == "" || txtAddress.Text.Trim() == "")
        //{
        if (txtFirstName.Text.Trim() == "" || txtLastName.Text.Trim() == "" || txtMobile.Text.Trim() == "" || txtEmailRegistration.Text.Trim() == "" || txtPasswordRegistration.Text.Trim() == "")
        {
            DInfo.ShowMessage("Enter all the required feilds.", Enums.MessageType.Error);
            return false;
        }
        return true;
    }
}