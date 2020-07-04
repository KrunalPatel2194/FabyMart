using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.IO;
using System.Drawing;


public partial class CityDetail : PageBase_Admin
{
    tblCity objCity;
    int iCityID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            lnkSaveAndAddnew.Visible = HasAdd;
            objCommon = new clsCommon();
            objCommon.FillDropDownList(ddlCountry, "tblCountry", tblCountry.ColumnNames.AppCountry, tblCountry.ColumnNames.AppCountryID, "-- Select Category --");
            ddlState.Items.Clear();
            ddlState.Items.Add(new ListItem("-- Select State --", "0"));
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



    private bool SaveData()
    {
        objCommon = new clsCommon();
        if (objCommon.IsRecordExists("tblCity", tblCity.ColumnNames.AppCity, tblCity.ColumnNames.AppCityID, txtCity.Text, hdnPKID.Value))
        {
            DInfo.ShowMessage("City already exits.", Enums.MessageType.Error);
            return false;
        }
        objCommon = null;
        objCity = new tblCity();
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objCity.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value));
        }
        else
        {
            objCity.AddNew();
        }
        objCity.AppCity = txtCity.Text;
        objCity.s_AppStateID = ddlState.SelectedValue.ToString();
        objCity.Save();
        iCityID = objCity.AppStateID;
        objCity = null;
        return true;
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon = new clsCommon();
        objCommon.FillDropDownList(ddlState, "tblState", tblState.ColumnNames.AppState, tblState.ColumnNames.AppStateID, "-- Select State --", tblState.ColumnNames.AppCountryID + "=" + ddlCountry.SelectedValue);
        objCommon = null;
    }
    private void SetValuesToControls()
    {
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objCity = new tblCity();
            if (objCity.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value)))
            {
                txtCity.Text = objCity.AppCity.ToString();
                tblState objState = new tblState();
                if(objState.LoadByPrimaryKey(objCity.AppStateID))
                {
                    objCommon = new clsCommon();
                    ddlCountry.SelectedValue = objState.s_AppCountryID;
                    objCommon.FillDropDownList(ddlState, "tblState", tblState.ColumnNames.AppState, tblState.ColumnNames.AppStateID, "-- Select State --", tblState.ColumnNames.AppCountryID + "=" + ddlCountry.SelectedValue);
                    objCommon = null;
                    ddlState.SelectedValue = objCity.s_AppStateID;
                }
                objState = null;
            }
            objCity = null;
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("City.aspx", true);
    }

    private void ResetControls()
    {
        txtCity.Text = "";
        ddlCountry.SelectedIndex = 0;
        ddlState.Items.Clear();
        ddlState.Items.Add(new ListItem("-- Select State --", "0"));
        hdnPKID.Value = "";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("City has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("City has been updated successfully", Enums.MessageType.Successfull);
            }
            hdnPKID.Value = iCityID.ToString();
            SetValuesToControls();
        }
    }
    protected void lnkSaveAndClose_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "City has been added successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            else
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "City has been updated successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            Response.Redirect("City.aspx");
        }
    }

    protected void lnkSaveAndAddnew_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("City has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("City has been updated successfully", Enums.MessageType.Successfull);
            }
            ResetControls();
        }
    }

}