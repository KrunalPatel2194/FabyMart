using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.IO;
using System.Drawing;


public partial class StateDetail : PageBase_Admin
{
    tblState objState;
    int iStateID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            lnkSaveAndAddnew.Visible = HasAdd;
            objCommon = new clsCommon();
            objCommon.FillDropDownList(ddlCountry, "tblCountry", tblCountry.ColumnNames.AppCountry, tblCountry.ColumnNames.AppCountryID, "-- Select Category --");
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
        if (objCommon.IsRecordExists("tblState", tblState.ColumnNames.AppState, tblState.ColumnNames.AppStateID, txtState.Text, hdnPKID.Value))
        {
            DInfo.ShowMessage(" State already exits.", Enums.MessageType.Error);
            return false;
        }
        objCommon = null;

        objState = new tblState();
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objState.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value));
        }
        else
        {
            objState.AddNew();
        }
        objState.AppState = txtState.Text;
        objState.s_AppCountryID = ddlCountry.SelectedValue.ToString();
        objState.Save();
        iStateID = objState.AppStateID;
        objState = null;
        return true;
    }

    private void SetValuesToControls()
    {
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objState = new tblState();
            if (objState.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value)))
            {
                txtState.Text = objState.AppState.ToString();
                ddlCountry.SelectedValue = objState.s_AppCountryID;
            }
            objState = null;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("State.aspx", true);
    }

    private void ResetControls()
    {
        txtState.Text = "";
        ddlCountry.SelectedIndex = 0;
        hdnPKID.Value = "";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("State has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("State has been updated successfully", Enums.MessageType.Successfull);
            }
            hdnPKID.Value = iStateID.ToString();
            SetValuesToControls();
        }
    }
    protected void lnkSaveAndClose_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "State has been added successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            else
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "State has been updated successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            Response.Redirect("State.aspx");
        }
    }

    protected void lnkSaveAndAddnew_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("State has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("State has been updated successfully", Enums.MessageType.Successfull);
            }
            ResetControls();
        }
    }

}