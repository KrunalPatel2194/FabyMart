using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;

public partial class ForgetPassword : PageBase_Client
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ExitsSession();
            SetUpPageContent(ref metaDescription, ref metaKeywords);
            SetRegulerExpression();

            txtEmail.Text = "";
        }
    }
    public void SetRegulerExpression()
    {
        revEmail.ValidationExpression = RXEmailRegularExpression;
        revEmail.ErrorMessage = "Invalid Email (" + RXEmailRegularExpressionMsg + ")";

    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        if (SendPassword())
        {
            DInfo.ShowMessage("Your password has been sent on registered email address, Please check your mail account.", Enums.MessageType.Successfull);
            txtEmail.Text = "";
        }
    }
    public bool SendPassword()
    {

        tblCustomer objCustomer = new tblCustomer();
        objCustomer.Where.AppEmailID.Value = txtEmail.Text;
        objCustomer.Query.Load();
        if (objCustomer.RowCount > 0)
        {
            clsCommon objCommon = new clsCommon();
            clsEncryption objEncrypt = new clsEncryption();
            string StrBody = "";
            string strSubject = "Password Recovery Request";
            StrBody = objCommon.readFile(Server.MapPath("~/EmailTemplates/ForgetPassword.html"));
            StrBody = StrBody.Replace("`email`", objCustomer.AppEmailID);
            StrBody = StrBody.Replace("`password`", objEncrypt.Decrypt(objCustomer.AppPassword, appFunctions.strKey.ToString()));

            objCommon.SendConfirmationMail(objCustomer.AppEmailID, strSubject, StrBody, Enums.Enum_Confirmation_Mail_type.register);
            objEncrypt = null;
            objCommon = null;

            txtEmail.Text = "";

        }
        else
        {
            DInfo.ShowMessage("Invalid email address, Please enter registered email.", Enums.MessageType.Error);
            return false;

        }
        objCustomer = null;

        return true;
    }
}