using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.IO;
using System.Drawing;


public partial class BannerDetail : PageBase_Admin
{
    tblBanner objBanner;
    clsEncryption objEncrypt;
    clsCommon objClsCommon;
    int iBannerID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
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



    private bool SaveData()
    {
        if (FileUploadImg.HasFile)
        {
            objClsCommon = new clsCommon();
            objBanner = new tblBanner();
            if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
            {
                objBanner.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value));
            }
            else
            {
                objBanner.AddNew();
                objBanner.AppDisplayOrder = objClsCommon.GetNextDisplayOrder("tblBanner", tblBanner.ColumnNames.AppDisplayOrder);

            }
            objBanner.AppTitle = txtBannerTitle.Text;
            objBanner.AppIsActive = chkIsActive.Checked;
            objBanner.AppUrl = txtappUrl.Text;
            objBanner.AppDescription = txtDescription.Text;
            if (FileUploadImg.HasFile)
            {

                string strError = "";
                string Time = Convert.ToString(DateTime.Now.Month) + Convert.ToString(DateTime.Now.Day) + Convert.ToString(DateTime.Now.Year) + Convert.ToString(DateTime.Now.Hour) + Convert.ToString(DateTime.Now.Minute) + Convert.ToString(DateTime.Now.Second);
                string strPath = objClsCommon.FileUpload_Images(FileUploadImg.PostedFile, txtBannerTitle.Text.Trim().Replace(" ", "_") + "_" + Time, "Uploads/Banner/", ref strError, 0, objBanner.s_AppImage, false, 600, 1600);
                if (strError == "")
                {
                    objBanner.AppImage = strPath;
                }
                else
                {
                    DInfo.ShowMessage(strError, Enums.MessageType.Error);
                    return false;
                }

            }


            objBanner.Save();
            iBannerID = objBanner.AppBannerID;
            objBanner = null;
            objClsCommon = null;
            return true;
        }
        else
        {
            DInfo.ShowMessage("Select Image First", Enums.MessageType.Error);
            return false;
        }
    }

    private void SetValuesToControls()
    {
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objBanner = new tblBanner();
            if (objBanner.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value)))
            {
                txtBannerTitle.Text = objBanner.AppTitle;
                chkIsActive.Checked = objBanner.AppIsActive;
                txtappUrl.Text = objBanner.AppUrl;
                txtDescription.Text = objBanner.AppDescription;
                if (objBanner.AppImage != "")
                {
                    img.ImageUrl = objBanner.AppImage;
                }
            }
            objBanner = null;
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Banner.aspx", true);
    }

    private void ResetControls()
    {
        txtBannerTitle.Text = "";
        chkIsActive.Checked = true;
        txtappUrl.Text = string.Empty;
        txtDescription.Text = "";
        img.ImageUrl = "";
        hdnPKID.Value = "";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("Banner has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("Banner has been updated successfully", Enums.MessageType.Successfull);
            }
            hdnPKID.Value = iBannerID.ToString();
            SetValuesToControls();
        }
    }
    protected void lnkSaveAndClose_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "Banner has been added successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            else
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "Banner has been updated successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            Response.Redirect("Banner.aspx");
        }
    }

    protected void lnkSaveAndAddnew_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("Banner has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("Banner has been updated successfully", Enums.MessageType.Successfull);
            }
            ResetControls();
        }
    }

}