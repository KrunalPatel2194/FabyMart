using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer;
using System.Text.RegularExpressions;
public partial class Login : ControlBase
{
    public PageBase objPageBase = new PageBase();
    HttpCookie httpCookie;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SetRegulerExpression();
            if (Request.Cookies.Get("FabyMartUsername") != null)
            {
                clsEncryption objEncrypt = new clsEncryption();
                if (Request.Cookies.Get("FabyMartUsername").Value != "")
                {
                    txtEmail.Text = objEncrypt.Decrypt(Request.Cookies.Get("FabyMartUsername").Value, appFunctions.strKey);
                    if (Request.Cookies.Get("FabyMartPassword").Value != "")
                    {

                        txtpassword.Attributes.Add("value", objEncrypt.Decrypt(Request.Cookies.Get("FabyMartPassword").Value, appFunctions.strKey));
                        chkRemeber.Checked = true;
                    }
                }

                objEncrypt = null;
            }
            else
            {
                chkRemeber.Checked = false;
            }
        }
    }
    public void SetRegulerExpression()
    {
        REVEmail.ValidationExpression = objPageBase.RXEmailRegularExpression;
        REVEmail.ErrorMessage = "Invalid Email (" + objPageBase.RXEmailRegularExpressionMsg + ")";

    }
    public void ShowLogin(bool IsPage = false)
    {
        hdnIsPage.Value = IsPage.ToString();
        MPELogin.Show();
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        
       if (IsLogin())
       {
           if (Convert.ToBoolean(hdnIsPage.Value))
            {
                MPELogin.Hide();
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                Response.Redirect(objPageBase.GetAlias("Order.aspx"));
            }

       }
    }

    public Boolean IsLogin()
    {
        if (txtEmail.Text.Trim() == "" || txtpassword.Text.Trim() == "")
        {
            DInfo.ShowMessage("Enter email and password.", Enums.MessageType.Error);
            return false;
        }
        string strRegex = "^([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}" + "\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\" + ".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$";
        Regex re = new Regex(strRegex);
        if (!(re.IsMatch(txtEmail.Text.Trim())))
        {
            DInfo.ShowMessage("Invalid Email (Ex. abc@ClothesonClick.com)", Enums.MessageType.Error);
            return false;
        }
        tblCustomer objCustomer = new tblCustomer();
        objCustomer.Where.AppEmailID.Value = txtEmail.Text.Trim();
        objCustomer.Query.Load();
        if (objCustomer.RowCount > 0)
        {
            clsEncryption objEncrypt = new clsEncryption();
            if (string.Compare(objEncrypt.Decrypt(objCustomer.AppPassword, appFunctions.strKey), txtpassword.Text, false) != 0)
            {
                DInfo.ShowMessage("Invalid email or password.", Enums.MessageType.Error);
                return false;
            }
            else
            {
                if (objCustomer.AppIsVerified)
                {
                    if (chkRemeber.Checked)
                    {
                        httpCookie = new HttpCookie("FabyMartUsername", objEncrypt.Encrypt(txtEmail.Text, appFunctions.strKey));
                        httpCookie.Expires = DateTime.Today.AddDays(10);
                        Response.Cookies.Add(httpCookie);
                        httpCookie = new HttpCookie("FabyMartPassword", objEncrypt.Encrypt(txtpassword.Text, appFunctions.strKey));
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
                  

                }
                else
                {
                    DInfo.ShowMessage("Your account is not verified", Enums.MessageType.Error);
                    return false;
                }
            }
            objEncrypt = null;
        }
        else
        {
            DInfo.ShowMessage("Invalid email or password.", Enums.MessageType.Error);
            return false;
        }
        objCustomer = null;
        return true;
    }
}