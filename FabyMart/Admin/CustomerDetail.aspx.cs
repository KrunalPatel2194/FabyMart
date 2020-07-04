using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.IO;
using System.Drawing;


public partial class CustomerDetail : PageBase_Admin
{
    tblCustomer objCustomer;
    clsEncryption objEncrypt;
    clsCommon objClsCommon;
    int iCustomerID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SetRegulerExpression();
            lnkSaveAndAddnew.Visible = HasAdd;

            if ((Request.QueryString.Get("ID") != null))
            {
                objEncrypt = new clsEncryption();
                try
                {
                    hdnPKID.Value = objEncrypt.Decrypt(Request.QueryString.Get("ID"), appFunctions.strKey);
                }
                catch (Exception ex)
                {
                    // noIdFoundRedirect("Employee.aspx");
                }
                objEncrypt = null;
                SetValuesToControls();
            }
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
        //revMobile1.ValidationExpression = RXPhoneRegularExpression;
        //revMobile1.ErrorMessage = "Invalid Phone Number (" + RXNumericRegularExpressionMsg + ")";
        REVFirstName.ValidationExpression = RXAlphaRegularExpression;
        REVFirstName.ErrorMessage = "Invalid First Name ( Alphabates Only)";
        REVLastName.ValidationExpression = RXAlphaRegularExpression;
        REVLastName.ErrorMessage = "Invalid Last Name ( Alphabates Only)";
       
    }


    private bool SaveData()
    {
        objClsCommon = new clsCommon();
        objCustomer = new tblCustomer();
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objCustomer.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value));
        }
        else
        {
            objCustomer.AddNew();
        }
        objCustomer.AppFirstName = txtFirstName.Text;
        objCustomer.AppLastName = txtLastName.Text;
        objCustomer.AppEmailID = txtEmail.Text;
        objEncrypt = new clsEncryption();
        objCustomer.AppPassword = objEncrypt.Encrypt(txtPassword.Text, appFunctions.strKey);
        objEncrypt = null;
        objCustomer.AppMobile = txtMobile.Text;
        objCustomer.AppPhone = txtPhone.Text;
        if (RbtnMale.Checked)
        {
            objCustomer.AppGender = true;
        }
        else
        {
            objCustomer.AppGender = false;
        }
        objCustomer.AppIsVerified = chkIsVerified.Checked;

        if (FileUploadImg.HasFile)
        {
            string strError = "";
            string Time = Convert.ToString(DateTime.Now.Month) + Convert.ToString(DateTime.Now.Day) + Convert.ToString(DateTime.Now.Year) + Convert.ToString(DateTime.Now.Hour) + Convert.ToString(DateTime.Now.Minute) + Convert.ToString(DateTime.Now.Second);
            string strPath = objClsCommon.FileUpload_Images(FileUploadImg.PostedFile, txtFirstName.Text.Trim().Replace(" ", "_") + "_" + Time, "Uploads/Customer/", ref strError, 0, objCustomer.s_AppImage, false, 0, 2000);
            if (strError == "")
            {
                objCustomer.AppImage = strPath;
            }
            else
            {
                DInfo.ShowMessage(strError, Enums.MessageType.Error);
                return false;
            }
        }
        objCustomer.AppIsActive = chkIsActive.Checked;
        objCustomer.AppIsNewsLetter = chkIsNewsLetter.Checked;
        objCustomer.Save();
        iCustomerID = objCustomer.AppCustomerID;
        objCustomer = null;
        objClsCommon = null;
        return true;
    }

    private void SetValuesToControls()
    {
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objCustomer = new tblCustomer();
            if (objCustomer.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value)))
            {
                txtFirstName.Text = objCustomer.AppFirstName;
                txtLastName.Text = objCustomer.AppLastName;
                txtEmail.Text = objCustomer.AppEmailID;
                objEncrypt = new clsEncryption();
                txtPassword.Attributes.Add("value", objEncrypt.Decrypt(objCustomer.AppPassword, appFunctions.strKey));
                objEncrypt = null;
                txtMobile.Text = objCustomer.AppMobile;
                txtPhone.Text = objCustomer.AppPhone;
                if (objCustomer.s_AppGender != "")
                {
                    if (objCustomer.AppGender)
                    {
                        RbtnMale.Checked = true;
                    }
                    else
                    {
                        rbtnFeMale.Checked = true;
                    }
                }
                else
                {
                    RbtnMale.Checked = true;
                }
                chkIsVerified.Checked = objCustomer.AppIsVerified;
                if (objCustomer.AppImage != "")
                {
                    img.ImageUrl = objCustomer.AppImage;
                }
                if (objCustomer.s_AppIsActive != "")
                {
                    chkIsActive.Checked = objCustomer.AppIsActive;
                }
                if (objCustomer.s_AppIsNewsLetter != "")
                {
                    chkIsNewsLetter.Checked = objCustomer.AppIsNewsLetter;
                }
                if (objCustomer.s_AppIsVerified != "")
                {
                    chkIsVerified.Checked = objCustomer.AppIsVerified;
                }
            }
            objCustomer = null;
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Customer.aspx", true);
    }

    private void ResetControls()
    {
        txtFirstName.Text = "";
        txtLastName.Text = "";
        txtEmail.Text = "";
        txtPassword.Text = "";
        txtMobile.Text = "";
        txtPhone.Text = "";
        rbtnFeMale.Checked = true;
        RbtnMale.Checked = true;
        chkIsVerified.Checked = true;
        chkIsActive.Checked = true;
        chkIsNewsLetter.Checked = true;
        img.ImageUrl = "";
        hdnPKID.Value = "";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("Customer has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("Customer has been updated successfully", Enums.MessageType.Successfull);
            }
            hdnPKID.Value = iCustomerID.ToString();
            SetValuesToControls();
        }
    }
    protected void lnkSaveAndClose_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "Customer has been added successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            else
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "Customer has been updated successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            Response.Redirect("Customer.aspx");
        }
    }

    protected void lnkSaveAndAddnew_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("Customer has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("Customer has been updated successfully", Enums.MessageType.Successfull);
            }
            ResetControls();
        }
    }

}