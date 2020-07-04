using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;

public partial class ChangePassword : PageBase_Client
{
    public PageBase objPageBase = new PageBase();
    tblCustomer objCustomer;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CheckSession();
            SetUpPageContent(ref metaDescription, ref metaKeywords);
            SetRegulerExpression();
        }
    }
    public void SetRegulerExpression()
    {
        revoldPassword.ValidationExpression = RXPasswordRegularExpression;
        revoldPassword.ErrorMessage = "Invalid Password ( " + RXPasswordRegularExpressionMsg + ")";

        revRetryNewPassword.ValidationExpression = RXPasswordRegularExpression;
        revRetryNewPassword.ErrorMessage = "Invalid Password ( " + RXPasswordRegularExpressionMsg + ")";

        revNewPassword.ValidationExpression = RXPasswordRegularExpression;
        revNewPassword.ErrorMessage = "Invalid Password ( " + RXPasswordRegularExpressionMsg + ")";


    }
    public bool SaveCustomer()
    {

        tblCustomer objCustomer = new tblCustomer();
        if (objCustomer.LoadByPrimaryKey(Convert.ToInt32(Session[appFunctions.Session.ClientUserID.ToString()].ToString())))
        {
            objEncrypt = new clsEncryption();
            if (string.Compare(objEncrypt.Decrypt(objCustomer.AppPassword, appFunctions.strKey), txtOldPassword.Text, false) != 0)
            {
                DInfo.ShowMessage("Old password is incorrect", Enums.MessageType.Warning);
                return false;
            }
            objCustomer.AppPassword = objEncrypt.Encrypt(txtNewRetryPassword.Text, appFunctions.strKey);
            objEncrypt = null;
            objCustomer.Save();
        }
        objCustomer = null;
        return true;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveCustomer())
        {
            DInfo.ShowMessage("Password is updated successfully.", Enums.MessageType.Successfull);

        }
    }
}