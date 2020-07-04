using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;

public partial class Admin_ExclusiveDetail : PageBase_Admin
{
    tblExclusive objExclusive;
    clsEncryption objEncrypt;
    clsCommon objCommon;
    int iExclusiveID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            lnkSaveAndAddnew.Visible = HasAdd;
            objCommon = new clsCommon();
            objCommon.FillDropDownList(ddlCategory, "tblCategory", tblCategory.ColumnNames.AppCategory, tblCategory.ColumnNames.AppCategoryID, "--Select Category--");
            objCommon = null;
            SetRegularExpression();
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
    private void SetRegularExpression()
    {
        
        revLink.ValidationExpression= RXURLRegularExpression;
        revLink.ErrorMessage = "Enter valid link(" + RXURLRegularExpressionMsg + ")";
    }

    private bool SaveData()
    {
        objCommon = new clsCommon();
        objExclusive = new tblExclusive();
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objExclusive.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value));
        }
        else
        {
            objExclusive.AddNew();
            objExclusive.AppDisplayOrder = objCommon.GetNextDisplayOrder("tblExclusive", tblExclusive.ColumnNames.AppDisplayOrder,tblExclusive.ColumnNames.AppCategoryID + "=" + ddlCategory.SelectedValue);

        }
        objExclusive.s_AppCategoryID = ddlCategory.SelectedValue;
        objExclusive.AppIsActive = chkIsActive.Checked;
        objExclusive.AppLink = txtLink.Text;
        objExclusive.AppTitle = txtTitle.Text;
        if (FileUploadImg.HasFile)
        {

            string strError = "";
            string Time = Convert.ToString(DateTime.Now.Month) + Convert.ToString(DateTime.Now.Day) + Convert.ToString(DateTime.Now.Year) + Convert.ToString(DateTime.Now.Hour) + Convert.ToString(DateTime.Now.Minute) + Convert.ToString(DateTime.Now.Second);
            string strPath = objCommon.FileUpload_Images(FileUploadImg.PostedFile, ddlCategory.SelectedValue.ToString().Trim().Replace(" ", "_") + "_" + Time, "Uploads/Exclusive/", ref strError, 0, objExclusive.s_AppImage, false, 0, 400);
            if (strError == "")
            {
                objExclusive.AppImage = strPath;
            }
            else
            {
                DInfo.ShowMessage(strError, Enums.MessageType.Error);
                return false;
            }

        }


        objExclusive.Save();
        iExclusiveID = objExclusive.AppExclusiveID;
        objExclusive = null;
        objCommon = null;
        return true;
    }

    private void SetValuesToControls()
    {
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objExclusive = new tblExclusive();
            if (objExclusive.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value)))
            {
                rfvFileUpload.Enabled = false;
                ddlCategory.SelectedValue = objExclusive.s_AppCategoryID;
                chkIsActive.Checked = objExclusive.AppIsActive;
                txtLink.Text = objExclusive.AppLink;
                txtTitle.Text = objExclusive.AppTitle;
                if (objExclusive.AppImage != "")
                {
                    img.ImageUrl = objExclusive.AppImage;
                }
            }
            objExclusive = null;
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Exclusive.aspx", true);
    }

    private void ResetControls()
    {
        ddlCategory.SelectedValue = "0";
        chkIsActive.Checked = true;
        txtLink.Text = string.Empty;
        
        img.ImageUrl = "";
        hdnPKID.Value = "";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("Exclusive offer has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("Exclusive offer has been updated successfully", Enums.MessageType.Successfull);
            }
            hdnPKID.Value = iExclusiveID.ToString();
            SetValuesToControls();
        }
    }
    protected void lnkSaveAndClose_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "Exclusive offer has been added successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            else
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "Exclusive offer has been updated successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            Response.Redirect("Exclusive.aspx");
        }
    }

    protected void lnkSaveAndAddnew_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("Exclusive offer has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("Exclusive offer has been updated successfully", Enums.MessageType.Successfull);
            }
            ResetControls();
        }
    }

}