using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
public partial class _Register : PageBase_Client
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SetUpPageContent(ref metaDescription, ref metaKeywords);
            SetRegulerExpression();
        }
    }
    public void SetRegulerExpression()
    {
        revEmail.ValidationExpression = RXEmailRegularExpression;
        revEmail.ErrorMessage = "Invalie Email (" + RXEmailRegularExpressionMsg + ")";
        revMobile.ValidationExpression = RXNumericRegularExpression;
        revMobile.ErrorMessage = "Invalie Mobile Number (" + RXNumericRegularExpressionMsg + ")";

        revPassword.ValidationExpression = RXPasswordRegularExpression;
        revPassword.ErrorMessage = "Invalie Password (" + RXPasswordRegularExpressionMsg + ")";

        revConfirmPassword.ValidationExpression = RXPasswordRegularExpression;
        revConfirmPassword.ErrorMessage = "Invalie Password (" + RXPasswordRegularExpressionMsg + ")";
    }
    protected void brnCreate_Click(object sender, EventArgs e)
    {
        if (SaveCustomer())
        {
            DInfo.ShowMessage("Check Ur Email please. Verification mail has been sent to ur account.", Enums.MessageType.Successfull);
            ResetRegistrationCotrol();
        }
    }
    public bool SaveCustomer()
    {
        objCommon = new clsCommon();
        if (objCommon.IsRecordExists("tblCustomer", tblCustomer.ColumnNames.AppEmailID, tblCustomer.ColumnNames.AppCustomerID, txtEmail.Text))
        {
            DInfo.ShowMessage("Email address already exist.", Enums.MessageType.Error);
            return false;
        }
        objCommon = null;
        tblCustomer objCustomer = new tblCustomer();
        objCustomer.AddNew();
        objCustomer.AppFirstName = txtFirstName.Text.Trim();
        objCustomer.AppLastName = txtLastName.Text.Trim();
        objCustomer.AppEmailID = txtEmail.Text.Trim();
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
        objCustomer.AppPassword = objEncrypt.Encrypt(txtPassword.Text, appFunctions.strKey);
        objEncrypt = null;

       // objCustomer.AppIsNewsLetter = ChkSubsucribNewsLetter.Checked;
        objCustomer.AppIsActive = true;
        objCustomer.AppIsVerified = false;
        objCustomer.AppCreatedDate = GetDateTime();
        objCustomer.Save();
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
            string strSubject = "Registration Detail";
            Strbody = objCommon.readFile(Server.MapPath("~/EmailTemplates/NewUserMail.html"));
            Strbody = Strbody.Replace("`uname`", txtFirstName.Text + " " + txtLastName.Text);
            Strbody = Strbody.Replace("`username`", txtEmail.Text.Trim());
            Strbody = Strbody.Replace("`password`", txtPassword.Text);

            objEncrypt = new clsEncryption();
            Strbody = Strbody.Replace("`Link`", PageBase.GetServerURL() + "/Approved.aspx?CID=" + objEncrypt.Encrypt(strId, appFunctions.strKey));
            objEncrypt = null;
            objCommon.SendMail(txtEmail.Text, strSubject, Strbody);
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
        txtEmail.Text = "";
        txtMobile.Text = "";
        txtPassword.Attributes.Add("value", "");
        txtAddress.Text = "";
       
        RbtnMale.Checked = true;
       

    }
}