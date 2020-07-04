using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.IO;
using System.Drawing;


public partial class ColorDetail : PageBase_Admin
{
    tblColor objColor;
    int intPkId = 0;
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
        objCommon = new clsCommon();
        if (objCommon.IsRecordExists("tblColor", tblColor.ColumnNames.AppColorName, tblColor.ColumnNames.AppColorID, txtColorName.Text, hdnPKID.Value))
        {
            DInfo.ShowMessage("Color Name alredy exits.", Enums.MessageType.Error);
            return false;
        }
        objColor = new tblColor();
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objColor.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value));
        }
        else
        {
            objColor.AddNew();
            objColor.AppDisplayOrder = objCommon.GetNextDisplayOrder("tblColor", tblColor.ColumnNames.AppDisplayOrder);
        }
        // objColor.AppColorCode = txtColor.Text;
        string Time = Convert.ToString(DateTime.Now.Month) + Convert.ToString(DateTime.Now.Day) + Convert.ToString(DateTime.Now.Year) + Convert.ToString(DateTime.Now.Hour) + Convert.ToString(DateTime.Now.Minute) + Convert.ToString(DateTime.Now.Second);
        if (fileUploadColorImg.HasFile)
        {
            string strError = "";
            string strPath = objCommon.FileUpload_Images(fileUploadColorImg.PostedFile, txtColorName.Text.Trim().Replace(" ", "_") + "_" + Time, "Uploads/ColorCode/", ref strError, 0, objColor.s_AppColorImage, false, 20, 20);
             if (strError == "")
            {
                objColor.s_AppColorImage = strPath;
            }
            else
            {
                DInfo.ShowMessage(strError, Enums.MessageType.Error);
                return false;
            }
        }
        objColor.AppColorName = txtColorName.Text;
        objColor.AppIsActive = chkIsActive.Checked;
        if (chkIsDefault.Checked)
        {
            tblColor ObjTempcolor = new tblColor();
            ObjTempcolor.SetDefaultColor();
            ObjTempcolor = null;
            objColor.AppIsActive = true;
            objColor.AppIsDefault = true;
        }
        else
        {
            tblColor ObjTempcolor = new tblColor();
            ObjTempcolor.LoadAll();
            if (ObjTempcolor.RowCount <= 0)
            {
                objColor.AppIsActive = true;
                objColor.AppIsDefault = true;
            }
            else
            {
                objColor.AppIsDefault = false;
            }
            ObjTempcolor = null;
        }
        objColor.Save();
        intPkId = objColor.AppColorID;
        objColor = null;
        objCommon = null;
        return true;
    }

    private void SetValuesToControls()
    {
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objColor = new tblColor();
            if (objColor.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value)))
            {
             //   txtColor.Text = objColor.AppColorCode;
              //  txtColor.Style.Add("background-Color", objColor.s_AppColorCode);
                txtColorName.Text = objColor.AppColorName;
                chkIsActive.Checked = objColor.AppIsActive;
                chkIsDefault.Checked = objColor.AppIsDefault;
                if (chkIsDefault.Checked)
                {
                    chkIsDefault.Enabled = false;
                }
                if (objColor.AppColorImage != "")
                {
                    img.ImageUrl = objColor.AppColorImage;
                }
            }
            objColor = null;
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Color.aspx", true);
    }

    private void ResetControls()
    {
       // txtColor.Text = "";
        txtColorName.Text = "";
        chkIsActive.Checked = true;
        chkIsDefault.Enabled = true;
        hdnPKID.Value = "";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("Color has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("Color has been updated successfully", Enums.MessageType.Successfull);
            }
            hdnPKID.Value = intPkId.ToString();
            SetValuesToControls();
        }
    }
    protected void lnkSaveAndClose_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "Color has been added successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            else
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "Color has been updated successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            Response.Redirect("Color.aspx");
        }
    }

    protected void lnkSaveAndAddnew_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("Color has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("Color has been updated successfully", Enums.MessageType.Successfull);
            }
            ResetControls();
        }
    }

}