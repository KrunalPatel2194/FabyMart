using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;

public partial class Admin_DealDetail : PageBase_Admin
{
    tblProduct objProduct;
    tblDeal objDeal;
    int intPkId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            lnkSaveAndAddnew.Visible = HasAdd;
            SetRegExpresssion();
            objCommon = new clsCommon();
            objCommon.FillDropDownList(ddlProduct, "tblProduct", tblProduct.ColumnNames.AppProductName, tblProduct.ColumnNames.AppProductID, "-- Select Product --");
            objCommon = null;
            img.Visible = false;
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
        REVRate.ErrorMessage = "Invalid Rate ( " + RXDecimalRegularExpression + ")";
    }
    private void SetValuesToControls()
    {
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objDeal = new tblDeal();
            if (objDeal.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value)))
            {
                ddlProduct.SelectedValue = objDeal.s_AppProductID;
                txtTitle.Text = objDeal.AppTitle;
                txtDiscount.Text = objDeal.AppDiscountPer.ToString();
                txtDescription.Text = objDeal.AppDescription;                
                chkIsActive.Checked = objDeal.AppIsActive;

                objProduct = new tblProduct();
                objDataTable = objProduct.LoadProductImageByProductID(objDeal.s_AppProductID);
                if (objDataTable.Rows.Count > 0)
                {
                    img.Visible = true;
                    img.ImageUrl = objDataTable.Rows[0][tblProductImage.ColumnNames.AppSmallImage].ToString();
                }
                objProduct = null;
            }
            objDeal = null;
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Deal.aspx", true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("Deal has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("Deal has been updated successfully", Enums.MessageType.Successfull);
            }
            hdnPKID.Value = intPkId.ToString();
            SetValuesToControls();
        }
    }
    private bool SaveData()
    {
        objCommon = new clsCommon();
        if (objCommon.IsRecordExists("tblDeal", tblDeal.ColumnNames.AppProductID, tblDeal.ColumnNames.AppDealID, ddlProduct.SelectedValue, hdnPKID.Value))
        {
            DInfo.ShowMessage("Deal with this product already exists", Enums.MessageType.Error);
            return false;
        }
        objDeal = new tblDeal();
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objDeal.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value));
        }
        else
        {
            objDeal.AddNew();
            objDeal.AppDisplayOrder = objCommon.GetNextDisplayOrder("tblDeal", tblDeal.ColumnNames.AppDisplayOrder);
        }
        objDeal.s_AppProductID = ddlProduct.SelectedValue;
        objDeal.AppTitle = txtTitle.Text;
        objDeal.AppDescription = txtDescription.Text;
        objDeal.AppDiscountPer = Convert.ToDecimal(txtDiscount.Text);
        objDeal.AppIsActive = chkIsActive.Checked;
        objDeal.Save();
        intPkId = objDeal.AppDealID;
        objDeal = null;
        objCommon = null;
        return true;
    }
    protected void lnkSaveAndClose_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "Deal has been added successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            else
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "Deal has been updated successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            Response.Redirect("Deal.aspx");
        }
    }

    protected void lnkSaveAndAddnew_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("Deal has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("Deal has been updated successfully", Enums.MessageType.Successfull);
            }
            ResetControls();
        }
    }
    private void ResetControls()
    {
        txtTitle.Text = "";
        txtDescription.Text = "";
        txtDiscount.Text = "";
        ddlProduct.SelectedValue = "0";
        chkIsActive.Checked = false;
        hdnPKID.Value = "";
        img.Visible = false;
    }
    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        objProduct = new tblProduct();
        objDataTable = objProduct.LoadProductImageByProductID(ddlProduct.SelectedValue);
        if (objDataTable.Rows.Count > 0)
        {
            img.Visible = true;
            if (objDataTable.Rows[0][tblProductImage.ColumnNames.AppSmallImage].ToString() != "")
            {
                img.ImageUrl = objDataTable.Rows[0][tblProductImage.ColumnNames.AppSmallImage].ToString();
            }
            
        }
        else {
            img.Visible = false;
            img.ImageUrl = "";
        }
        objProduct = null;
    }
}