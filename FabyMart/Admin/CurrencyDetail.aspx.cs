using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.IO;
using System.Drawing;


public partial class CurrencyDetail : PageBase_Admin
{
    tblCurrency objCurrency;
    int intPkId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            lnkSaveAndAddnew.Visible = HasAdd;
            SetRegExpresssion();
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

    public void SetRegExpresssion()
    {
        REVRate.ValidationExpression = RXDecimalRegularExpression;
        REVRate.ErrorMessage  ="Invalid Rate ( "+RXDecimalRegularExpression+")";
    }

    private bool SaveData()
    {
        objCommon = new clsCommon();
        if (objCommon.IsRecordExists("tblCurrency", tblCurrency.ColumnNames.AppCurrency, tblCurrency.ColumnNames.AppCurrencyID, txtCurrencyName.Text, hdnPKID.Value))
        {
            DInfo.ShowMessage("Currency Name alredy exits.", Enums.MessageType.Error);
            return false;
        }
        objCurrency = new tblCurrency();
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objCurrency.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value));
        }
        else
        {
            objCurrency.AddNew();
            objCurrency.AppDisplayOrder = objCommon.GetNextDisplayOrder("tblCurrency", tblCurrency.ColumnNames.AppDisplayOrder);
        }
        objCurrency.AppCurrency = txtCurrencyName.Text;
        objCurrency.AppCurrencyCode = txtCurrencyCode.Text;
        objCurrency.s_AppRate = txtRate.Text;
        objCurrency.s_AppSymbol = txtSymbol.Text;
        objCurrency.AppIsActive = chkIsActive.Checked;
        if (chkIsDefault.Checked)
        {
            tblColor ObjTempcolor = new tblColor();
            ObjTempcolor.SetDefaultColor();
            ObjTempcolor = null;
            objCurrency.AppIsActive = true;
            objCurrency.AppIsDefault = true;
        }
        else
        {
            if (objCurrency.AppDisplayOrder == 1)
            {
                objCurrency.AppIsActive = true;
                objCurrency.AppIsDefault = true;
            }
            else
            {
                objCurrency.AppIsDefault = false;
            }
        }
        objCurrency.Save();
        intPkId = objCurrency.AppCurrencyID;
        objCurrency = null;
        objCommon = null;
        return true;
    }

    private void SetValuesToControls()
    {
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objCurrency = new tblCurrency ();
            if (objCurrency.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value)))
            {
                txtCurrencyName.Text = objCurrency.AppCurrency ;
                txtCurrencyCode.Text = objCurrency.AppCurrencyCode;
                txtRate.Text = objCurrency.s_AppRate;
                txtSymbol.Text = objCurrency.s_AppSymbol;
                chkIsActive.Checked = objCurrency.AppIsActive;
                chkIsDefault.Checked = objCurrency.AppIsDefault;
                if (chkIsDefault.Checked)
                {
                    chkIsDefault.Enabled = false;
                }
            }
            objCurrency = null;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Currency.aspx", true);
    }

    private void ResetControls()
    {
        txtCurrencyName.Text = "";
        txtRate .Text = "";
        txtSymbol.Text = "";
        chkIsActive.Checked = true;
        chkIsActive.Checked = false;
        chkIsDefault.Enabled = true;
        hdnPKID.Value = "";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("Currency has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("Currency has been updated successfully", Enums.MessageType.Successfull);
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
                Session[appFunctions.Session.ShowMessage.ToString()] = "Currency has been added successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            else
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "Currency has been updated successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            Response.Redirect("Currency.aspx");
        }
    }

    protected void lnkSaveAndAddnew_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("Currency has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("Currency has been updated successfully", Enums.MessageType.Successfull);
            }
            ResetControls();
        }
    }

}