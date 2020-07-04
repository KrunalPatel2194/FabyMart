using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.IO;
using System.Drawing;


public partial class PinCodeDetail : PageBase_Admin
{

    tblPinCode objPinCode;
    
    clsEncryption objEncrypt;
    clsCommon objClsCommon;
    int iBrandID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            lnkSaveAndAddnew.Visible = HasAdd;
            objCommon = new clsCommon();
            objCommon.FillDropDownList(ddlCountry, "tblCountry", tblCountry.ColumnNames.AppCountry, tblCountry.ColumnNames.AppCountryID, "-- Select Country --");
            ddlState.Items.Clear();
            ddlState.Items.Add(new ListItem("-- Select State --","0"));
            ddlCity.Items.Clear();
            ddlCity.Items.Add(new ListItem("-- Select City --", "0"));
            objCommon = null;
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

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon = new clsCommon();
        objCommon.FillDropDownList(ddlState, "tblState", tblState.ColumnNames.AppState, tblState.ColumnNames.AppStateID, "-- Select State --", tblState.ColumnNames.AppCountryID + "=" + ddlCountry.SelectedValue);
        ddlCity.Items.Clear();
        ddlCity.Items.Add(new ListItem("-- Select City --", "0"));
        objCommon = null;
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon = new clsCommon();
        objCommon.FillDropDownList(ddlCity, "tblCity", tblCity.ColumnNames.AppCity, tblCity.ColumnNames.AppCityID, "-- Select City --", tblCity.ColumnNames.AppStateID + "=" + ddlState.SelectedValue);
        objCommon = null;
    }

    private bool SaveData()
    {
        objClsCommon = new clsCommon();
        objPinCode = new tblPinCode();
        if (objClsCommon.IsRecordExists("tblPinCode", tblPinCode.ColumnNames.AppPinCode, tblPinCode.ColumnNames.AppPinCodeID, txtPINCode.Text, hdnPKID.Value))
        {
            DInfo.ShowMessage(" Pin Code already exits.", Enums.MessageType.Error);
            return false;
        }
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objPinCode.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value));
        }
        else
        {
            objPinCode.AddNew();
         }
        objPinCode.AppPinCode = Convert.ToInt32( txtPINCode.Text);
        objPinCode.s_AppCityID = ddlCity.SelectedValue.ToString();
        objPinCode.AppIsActive = chkIsActive.Checked;

        objPinCode.Save();
        iBrandID = objPinCode.AppPinCodeID;
        objPinCode = null;
        objClsCommon = null;
        return true;
    }

    private void SetValuesToControls()
    {
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objPinCode = new tblPinCode();
            if (objPinCode.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value)))
            {
                txtPINCode.Text = objPinCode.AppPinCode.ToString();
                chkIsActive.Checked = objPinCode.AppIsActive;
                tblCity objCity = new tblCity();
                if (objCity.LoadByPrimaryKey(objPinCode.AppCityID))
                {
                    objCommon = new clsCommon();
                    tblState objState = new tblState();
                    if (objState.LoadByPrimaryKey(objCity.AppStateID))
                    {
                        ddlCountry.SelectedValue = objState.s_AppCountryID;
                        objCommon.FillDropDownList(ddlState, "tblState", tblState.ColumnNames.AppState, tblState.ColumnNames.AppStateID, "-- Select State --", tblState.ColumnNames.AppCountryID + "=" + ddlCountry.SelectedValue);
                        ddlState.SelectedValue = objState.s_AppStateID;
                        objCommon.FillDropDownList(ddlCity, "tblCity", tblCity.ColumnNames.AppCity, tblCity.ColumnNames.AppCityID, "-- Select City --", tblCity.ColumnNames.AppStateID + "=" + ddlState.SelectedValue);
                        ddlCity.SelectedValue = objCity.s_AppCityID;
                    }
                    objState = null;
                    objCommon = null;
                }
                objCity = null;
            }
            objPinCode = null;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PinCode.aspx", true);
    }

    private void ResetControls()
    {
        txtPINCode.Text = "";
        chkIsActive.Checked = true;
        ddlState.Items.Clear();
        ddlState.Items.Add(new ListItem("-- Select State --", "0"));
        ddlCity.Items.Clear();
        ddlCity.Items.Add(new ListItem("-- Select City --", "0"));
        hdnPKID.Value = "";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("PIN Code has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("PIN Code has been updated successfully", Enums.MessageType.Successfull);
            }
            hdnPKID.Value = iBrandID.ToString();
            SetValuesToControls();
        }
    }
    protected void lnkSaveAndClose_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "PIN Code has been added successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            else
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "PIN Code has been updated successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            Response.Redirect("PinCode.aspx");
        }
    }

    protected void lnkSaveAndAddnew_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("PIN Code has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("PIN Code has been updated successfully", Enums.MessageType.Successfull);
            }
            ResetControls();
        }
    }

}