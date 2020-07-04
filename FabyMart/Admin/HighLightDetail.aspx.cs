using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.IO;
using System.Drawing;


public partial class HighLightDetail: PageBase_Admin
{
    tblHighLight objHighLight;
    clsEncryption objEncrypt;
    clsCommon objClsCommon;
    int iHighLightID = 0;
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
        objClsCommon = new clsCommon();
        objHighLight = new tblHighLight();
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objHighLight.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value));
        }
        else
        {
            objHighLight.AddNew();
            objHighLight.AppDisplayOrder = objClsCommon.GetNextDisplayOrder("tblHighLight", tblHighLight.ColumnNames.AppDisplayOrder);

        }
        objHighLight.AppTitle = txtHighLightTitle.Text;
        objHighLight.AppIsActive = chkIsActive.Checked;
        objHighLight.AppUrl = txtappUrl.Text;
        //objHighLight.AppDescription = txtDescription.Text;
        if (FileUploadImg.HasFile)
        {

            string strError = "";
            string Time = Convert.ToString(DateTime.Now.Month) + Convert.ToString(DateTime.Now.Day) + Convert.ToString(DateTime.Now.Year) + Convert.ToString(DateTime.Now.Hour) + Convert.ToString(DateTime.Now.Minute) + Convert.ToString(DateTime.Now.Second);
            string strPath = objClsCommon.FileUpload_Images(FileUploadImg.PostedFile, txtHighLightTitle.Text.Trim().Replace(" ", "_") + "_" + Time, "Uploads/HighLight/", ref strError, 0, objHighLight.s_AppImage, false, 0, 2000);
            if (strError == "")
            {
                objHighLight.AppImage = strPath;
            }
            else
            {
                DInfo.ShowMessage(strError, Enums.MessageType.Error);
                return false;
            }

        }


        objHighLight.Save();
        iHighLightID = objHighLight.AppHighLightID;
        objHighLight = null;
        objClsCommon = null;
        return true;
    }

    private void SetValuesToControls()
    {
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objHighLight  = new tblHighLight();
            if (objHighLight.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value)))
            {
                txtHighLightTitle.Text = objHighLight.AppTitle;
                chkIsActive.Checked = objHighLight.AppIsActive;
                txtappUrl.Text = objHighLight.AppUrl;
                //txtDescription.Text = objBanner.AppDescription;
                if (objHighLight.AppImage != "")
                {
                    img.ImageUrl = objHighLight.AppImage;
                }
            }
            objHighLight = null;
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("HighLight.aspx", true);
    }

    private void ResetControls()
    {
        txtHighLightTitle.Text = "";
        chkIsActive.Checked = true;
        txtappUrl.Text = string.Empty;
        //txtDescription.Text = "";
        img.ImageUrl = "";
        hdnPKID.Value = "";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("HighLight has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("HighLight has been updated successfully", Enums.MessageType.Successfull);
            }
            hdnPKID.Value = iHighLightID .ToString();
            SetValuesToControls();
        }
    }
    protected void lnkSaveAndClose_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "HighLight has been added successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            else
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "HighLight has been updated successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            Response.Redirect("HighLight.aspx");
        }
    }

    protected void lnkSaveAndAddnew_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("HighLight has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("HighLight has been updated successfully", Enums.MessageType.Successfull);
            }
            ResetControls();
        }
    }

}