using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.IO;
using System.Drawing;


public partial class CategoryDetail : PageBase_Admin
{
    tblCategory objCategory;
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
        if (objCommon.IsRecordExists("tblCategory", tblCategory.ColumnNames.AppCategory, tblCategory.ColumnNames.AppCategoryID, txtCategoryName.Text, hdnPKID.Value))
        {
            DInfo.ShowMessage("Category Name alredy exits.", Enums.MessageType.Error);
            return false;
        }
        objCategory = new tblCategory();
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objCategory.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value));
        }
        else
        {
            objCategory.AddNew();
            objCategory.s_AppCreatedBy = Session[appFunctions.Session.UserID.ToString()].ToString();
            objCategory.AppDisplayOrder = objCommon.GetNextDisplayOrder("tblCategory", tblCategory.ColumnNames.AppDisplayOrder);
           // objCategory.s_AppCreatedDate = FormatDateString(DateTime.Now.ToString(strInputDateFormat), strInputDateFormat, strOutputDateFormat);
            objCategory.AppCreatedDate = DateTime.Now;
        }

        objCategory.AppCategory = txtCategoryName.Text;
        objCategory.AppIsActive = chkIsActive.Checked;
        objCategory.Save();
        intPkId = objCategory.AppCategoryID;
        objCategory = null;
        objCommon = null;
        return true;
    }

    private void SetValuesToControls()
    {
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objCategory = new tblCategory();
            if (objCategory.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value)))
            {
                txtCategoryName.Text = objCategory.AppCategory;
                chkIsActive.Checked = objCategory.AppIsActive;

            }
            objCategory = null;
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Category.aspx", true);
    }

    private void ResetControls()
    {
        txtCategoryName.Text = "";
        chkIsActive.Checked = true;
        hdnPKID.Value = "";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("Category has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("Category has been updated successfully", Enums.MessageType.Successfull);
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
                Session[appFunctions.Session.ShowMessage.ToString()] = "Category has been added successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            else
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "Category has been updated successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            Response.Redirect("Category.aspx");
        }
    }

    protected void lnkSaveAndAddnew_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("Category has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("Category has been updated successfully", Enums.MessageType.Successfull);
            }
            ResetControls();
        }
    }

}