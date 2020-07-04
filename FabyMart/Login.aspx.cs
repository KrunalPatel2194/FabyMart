using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Web.Services;
public partial class _Login : PageBase_Client
{
    tblCustomer objCustomrer;
    PageBase objPageBase;
    HttpCookie httpCookie;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ExitsSession();
            SetUpPageContent(ref metaDescription, ref metaKeywords);
            SetRegulerExpression();
            //     ResetRegistrationCotrol();

            if ((Request.Cookies.Get("FabyMartUsername") != null))
            {
                if (!string.IsNullOrEmpty(Request.Cookies.Get("FabyMartUsername").Value))
                {
                    txtEmail.Text = objEncrypt.Decrypt(Request.Cookies.Get("FabyMartUsername").Value, appFunctions.strKey);

                    if ((Request.Cookies.Get("FabyMartPassword") != null))
                    {
                        if (!string.IsNullOrEmpty(Request.Cookies.Get("FabyMartPassword").Value))
                        {
                            txtPassword.Attributes.Add("value", objEncrypt.Decrypt(Request.Cookies.Get("FabyMartPassword").Value, appFunctions.strKey));
                        }
                    }

                    chkRemeberMe.Checked = true;
                }
            }
            if ((Request.QueryString.Get("From") != null))
            {
                string From = "";
                objEncrypt = new clsEncryption();
                From = objEncrypt.Decrypt(Request.QueryString.Get("From"), appFunctions.strKey);
                objEncrypt = null;
                if (From == "ApprovedPageTrue")
                {
                    DInfo.ShowMessage("Your account has been activated successfully", Enums.MessageType.Successfull);
                }
                else if (From == "ApprovedPageFalse")
                {
                    DInfo.ShowMessage("Your account is already activated", Enums.MessageType.Information);
                }
            }
            objCommon = new clsCommon();
            string strRedirect = objCommon.GetParameterFromUrl(Enums.Enums_URLParameterType.ByNumber, "1");
            objCommon = null;
            if (strRedirect != "")
            {
                if (strRedirect == "TrackOrder")
                {
                    DInfo.ShowMessage("Please login first to track your order", Enums.MessageType.Information);
                }
                if (strRedirect == "WishList")
                {
                    DInfo.ShowMessage("Please login first to see your wishlist", Enums.MessageType.Information);
                }
            }

            //ExitsSession();
            //SetUpPageContent(ref metaDescription, ref metaKeywords);
            //SetRegulerExpression();
            //if (Request.Cookies.Get("FabyMartUsername") != null)
            //{
            //    objEncrypt = new clsEncryption();
            //    if (Request.Cookies.Get("FabyMartUsername").Value != "")
            //    {
            //        txtEmail.Text = objEncrypt.Decrypt(Request.Cookies.Get("FabyMartUsername").Value, appFunctions.strKey);
            //        if (Request.Cookies.Get("FabyMartPassword").Value != "")
            //        {

            //            txtPassword.Attributes.Add("value", objEncrypt.Decrypt(Request.Cookies.Get("FabyMartPassword").Value, appFunctions.strKey));
            //            chkRemeberMe.Checked = true;
            //        }
            //    }

            //    objEncrypt = null;
            //}
            //else
            //{
            //    chkRemeberMe.Checked = false;
            //}
            //if ((Request.QueryString.Get("From") != null))
            //{

            //    string From = "";
            //    objEncrypt = new clsEncryption();
            //    From = objEncrypt.Decrypt(Request.QueryString.Get("From"), appFunctions.strKey);
            //    objEncrypt = null;
            //    if (From == "ApprovedPageTrue")
            //    {
            //        DInfo.ShowMessage("Your account has been activated successfully", Enums.MessageType.Successfull);
            //    }
            //    else if (From == "ApprovedPageFalse")
            //    {
            //        DInfo.ShowMessage("Your account is already activated", Enums.MessageType.Information);
            //    }
            //}

        }
    }
    public void SetRegulerExpression()
    {
        revEmail.ValidationExpression = RXEmailRegularExpression;
        revEmail.ErrorMessage = "Invalid Email (" + RXEmailRegularExpressionMsg + ")";
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

            DisplayInfoRegistration.ShowMessage("Your account has been created successfully, For security purpose, check your mail to activate your account.", Enums.MessageType.Successfull);
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
        //   objCustomer.AppAddress = txtAddress.Text;
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

        // objCustomer.AppIsNewsLetter = ChkSubsucribNewsLetter.Checked;
        objCustomer.AppIsActive = true;
        objCustomer.AppIsVerified = false;
        objCustomer.AppCreatedDate = GetDateTime();
        objCustomer.Save();
        //      tblAddress objAdress = new tblAddress();
        //      objAdress.AddNew();
        ////      objAdress.AppAddress = txtAddress.Text;
        //      objAdress.AppCustomerID = objCustomer.AppCustomerID;

        //      objAdress.Save();
        SendMail(objCustomer.s_AppCustomerID);
        objCustomer = null;
        return true;
    }
    public void SendMail(string strId)
    {
        try
        {
            objCommon = new clsCommon();
            string Strbody = "";
            string strSubject = "Account Activation Process";
            Strbody = objCommon.readFile(Server.MapPath("~/EmailTemplates/NewUserMail.html"));
            Strbody = Strbody.Replace("`link`", strServerURL);
            Strbody = Strbody.Replace("`uname`", txtFirstName.Text + " " + txtLastName.Text);
            objEncrypt = new clsEncryption();
            Strbody = Strbody.Replace("`linkActive`", GetAlias("Approved.aspx") + objEncrypt.Encrypt(strId, appFunctions.strKey));
            objEncrypt = null;
            objCommon.SendConfirmationMail(txtEmailRegistration.Text, strSubject, Strbody, Enums.Enum_Confirmation_Mail_type.register);
            //objCommon.SendMail(txtEmailRegistration.Text, strSubject, Strbody);
            objCommon = null;
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
        //   txtAddress.Text = "";

        RbtnMale.Checked = true;


    }
    //protected void lbkCreateNewAccount_Click(object sender, EventArgs e)
    //{
    //    objPageBase = new PageBase();
    //    Response.Redirect(objPageBase.GetAlias("Register.aspx"));
    //    objPageBase = null;
    //}
    protected void lnkForgotPass_Click(object sender, EventArgs e)
    {
        objPageBase = new PageBase();
        Response.Redirect(objPageBase.GetAlias("ForgotPassword.aspx"));
        objPageBase = null;
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {

        tblCustomer objCustomer = new tblCustomer();
        objCustomer.Where.AppEmailID.Value = txtEmail.Text.Trim();
        objCustomer.Query.Load();
        if (objCustomer.RowCount > 0)
        {
            objEncrypt = new clsEncryption();
            if (string.Compare(objEncrypt.Decrypt(objCustomer.AppPassword, appFunctions.strKey), txtPassword.Text, false) != 0)
            {
                lblForError.InnerText = "Invalid Email or Password.";
                //DInfo.ShowMessage("Invalid User Name or Password.", Enums.MessageType.Error);
            }
            else
            {
                if (objCustomer.AppIsVerified)
                {
                    if (chkRemeberMe.Checked)
                    {
                        httpCookie = new HttpCookie("FabyMartUsername", objEncrypt.Encrypt(txtEmail.Text, appFunctions.strKey));
                        httpCookie.Expires = DateTime.Today.AddDays(10);
                        Response.Cookies.Add(httpCookie);
                        httpCookie = new HttpCookie("FabyMartPassword", objEncrypt.Encrypt(txtPassword.Text, appFunctions.strKey));
                        httpCookie.Expires = DateTime.Today.AddDays(10);
                        Response.Cookies.Add(httpCookie);
                    }
                    else
                    {
                        httpCookie = new HttpCookie("FabyMartUsername", "");
                        httpCookie.Expires = DateTime.Today.AddDays(0);
                        Response.Cookies.Add(httpCookie);
                        httpCookie = new HttpCookie("FabyMartPassword", "");
                        httpCookie.Expires = DateTime.Today.AddDays(0);
                        Response.Cookies.Add(httpCookie);
                    }
                    Session[appFunctions.Session.ClientUserID.ToString()] = objCustomer.AppCustomerID;
                    Session[appFunctions.Session.ClientUserName.ToString()] = objCustomer.AppFirstName + " " + objCustomer.AppLastName;
                    objCommon = new clsCommon();
                    string strRedirect = objCommon.GetParameterFromUrl(Enums.Enums_URLParameterType.ByNumber, "1");
                    objCommon = null;
                    if (strRedirect != "")
                    {
                        if (strRedirect == "Cart")
                        {
                            Response.Redirect(GetAlias("Cart.aspx"));
                        }
                        else if (strRedirect == "wishlist")
                        {
                            Response.Redirect(GetAlias("MyFavouriteProduct.aspx"));
                        }
                        else if (strRedirect == "TrackOrder")
                        {
                            Response.Redirect(GetAlias("TrackOrder.aspx"));
                        }
                        else if (strRedirect == "WishList")
                        {
                            Response.Redirect(GetAlias("MyFavouriteProduct.aspx"));
                        }
                        else if (strRedirect == "order")
                        {
                            Response.Redirect(GetAlias("order.aspx"));
                        }
                        else
                        {
                            Response.Redirect(GetAlias("Default.aspx"));
                        }
                    }
                    else
                    {
                        Response.Redirect(GetAlias("Default.aspx"));
                    }
                }
                else
                {
                    DInfo.ShowMessage("Your account is not verified, Please check your email to verify or contact to support", Enums.MessageType.Error);
                }
            }
            objEncrypt = null;
        }
        else
        {
            lblForError.InnerText = "Invalid Email or Password";
            // DInfo.ShowMessage("Invalid User Name or Password.", Enums.MessageType.Error);
        }
        objCustomer = null;
    }

    protected void btnRegisterWithFacebook_Click(object sender, EventArgs e)
    {
        if (hdnEmail.Value != "")
        {
            //tblCustomer objCustomer = new tblCustomer();
            //objCustomer.Where.AppEmailID.Value = hdnEmail.Value;
            //objCustomer.Query.Load();
            //if (objCustomer.RowCount > 0)
            //{
            //    Session[appFunctions.Session.ClientUserID.ToString()] = objCustomer.AppCustomerID;
            //    Session[appFunctions.Session.ClientUserName.ToString()] = objCustomer.AppFirstName + " " + objCustomer.AppLastName;
            //    Response.Redirect(GetAlias("Default.aspx"));
            //}
            //else
            //{
            //    Response.Redirect(GetAlias("RegisterWithFB.aspx"));
            //}
            //objCustomer = null;
        }
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
}