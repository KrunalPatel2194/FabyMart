using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.IO;
using System.Drawing;


public partial class CountryDetail : PageBase_Admin
{

    tblCountry objCountry;
    
    clsEncryption objEncrypt;
    clsCommon objClsCommon;
    int iCountryID = 0;
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
        objCountry = new tblCountry();
        if (objClsCommon.IsRecordExists("tblCountry", tblCountry.ColumnNames.AppCountry, tblCountry.ColumnNames.AppCountryID, txtCountry.Text, hdnPKID.Value))
        {
            DInfo.ShowMessage(" Country already exits.", Enums.MessageType.Error);
            return false;
        }
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objCountry.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value));
        }
        else
        {
            objCountry.AddNew();
            
           
        }

        objCountry.AppCountry = txtCountry.Text;

        //objCountry.AppIsActive = chkIsActive.Checked;



        objCountry.Save();
        iCountryID = objCountry.AppCountryID;
        objCountry = null;
        objClsCommon = null;
        return true;
    }

    private void SetValuesToControls()
    {
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objCountry = new tblCountry();
            if (objCountry.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value)))
            {
                txtCountry.Text = objCountry.AppCountry.ToString();
               // chkIsActive.Checked = objCountry.AppIsActive;

              
            }
            objCountry = null;
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Country.aspx", true);
    }

    private void ResetControls()
    {
        txtCountry.Text = "";
        //chkIsActive.Checked = true;
       
        hdnPKID.Value = "";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("Country has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("Country has been updated successfully", Enums.MessageType.Successfull);
            }
            hdnPKID.Value = iCountryID.ToString();
            SetValuesToControls();
        }
    }
    protected void lnkSaveAndClose_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "Country has been added successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            else
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "Country has been updated successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            Response.Redirect("Country.aspx");
        }
    }

    protected void lnkSaveAndAddnew_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("Country has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("Country has been updated successfully", Enums.MessageType.Successfull);
            }
            ResetControls();
        }
    }

}