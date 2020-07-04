using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.IO;
using System.Drawing;


public partial class CourierCompanyDetail : PageBase_Admin
{
    tblCourierCompany objCourierCompany;
    tblCourierRate objCourierRate;
    int iCourierCompanyID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            ViewState["SortOrder"] = appFunctions.Enum_SortOrderBy.Asc;
            ViewState["SortColumn"] = "";
            lnkSaveAndAddnew.Visible = HasAdd;
            SetRegularExpression();

            //objCommon = new clsCommon();
            //objCommon.FillDropDownList(ddlCourierCompany, "tblCourierCompany", tblCourierCompany.ColumnNames.AppCourierCompany, tblCourierCompany.ColumnNames.AppCourierCompanyID, "-- Select Courier Company --", tblCourierCompany.ColumnNames.AppCourierCompany, appFunctions.Enum_SortOrderBy.Asc);
            //objCommon = null;
            objCommon = new clsCommon();
            objCommon.FillDropDownList(ddlPINCode, "tblPinCode", tblPinCode.ColumnNames.AppPinCode, tblPinCode.ColumnNames.AppPinCodeID, "--Select PIN Code--", tblPinCode.ColumnNames.AppPinCodeID, appFunctions.Enum_SortOrderBy.Asc, tblPinCode.ColumnNames.AppIsActive + "=1");
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
                LoadAllData();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "HideTab()", true);
            }
        }
    }



    private bool SaveData()
    {
        objCommon = new clsCommon();
        if (objCommon.IsRecordExists("tblCourierCompany", tblCourierCompany.ColumnNames.AppCourierCompany, tblCourierCompany.ColumnNames.AppCourierCompanyID, txtCourierCompanyName.Text, hdnPKID.Value))
        {
            DInfo.ShowMessage("Courier Company Name alredy exits.", Enums.MessageType.Error);
            return false;
        }

        objCourierCompany = new tblCourierCompany();
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objCourierCompany.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value));
        }
        else
        {
            objCourierCompany.AddNew();
            objCourierCompany.AppDisplayOrder = objCommon.GetNextDisplayOrder("tblCourierCompany", tblCourierCompany.ColumnNames.AppDisplayOrder);


        }
        objCourierCompany.AppCourierCompany = txtCourierCompanyName.Text;
        objCourierCompany.s_AppCODRate = txtCODRate.Text;
        objCourierCompany.AppEmail = txtEmail.Text;
        objCourierCompany.AppContactNo = txtContactNo.Text;
        objCourierCompany.AppWebsite = txtWebsite.Text;

        objCourierCompany.AppIsActive = chkIsActive.Checked;

        objCourierCompany.Save();
        iCourierCompanyID = objCourierCompany.AppCourierCompanyID;
        objCourierCompany = null;
        objCommon = null;
        return true;
    }

    private void SetValuesToControls()
    {
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objCourierCompany = new tblCourierCompany();
            if (objCourierCompany.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value)))
            {
                txtCourierCompanyName.Text = objCourierCompany.AppCourierCompany;
                txtCODRate.Text = objCourierCompany.s_AppCODRate;
                txtEmail.Text = objCourierCompany.AppEmail;
                txtContactNo.Text = objCourierCompany.AppContactNo;
                txtWebsite.Text = objCourierCompany.AppWebsite;

                chkIsActive.Checked = objCourierCompany.AppIsActive;

            }
            objCourierCompany = null;
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("CourierCompany.aspx", true);
    }

    private void ResetControls()
    {
        txtCODRate.Text = "";
        txtCourierCompanyName.Text = "";
        txtEmail.Text = "";
        txtContactNo.Text = "";
        txtWebsite.Text = "";
        chkIsActive.Checked = true;
        hdnPKID.Value = "";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "HideTab()", true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("Courier Company has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("Courier Company has been updated successfully", Enums.MessageType.Successfull);
            }
            hdnPKID.Value = iCourierCompanyID.ToString();
            SetValuesToControls();
            LoadAllData();
        }
    }
    protected void lnkSaveAndClose_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "Courier Company has been added successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            else
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "Courier Company has been updated successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            Response.Redirect("CourierCompany.aspx");
        }
    }

    protected void lnkSaveAndAddnew_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("Courier Company has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("Courier Company has been updated successfully", Enums.MessageType.Successfull);
            }
            ResetControls();
        }
    }

    private void SetRegularExpression()
    {
        REVCODRate.ValidationExpression = RXDecimalRegularExpression;
        REVCODRate.ErrorMessage = "Enter valid COD Rate(" + RXDecimalRegularExpression + ")";


        REVEmail.ValidationExpression = RXEmailRegularExpression;
        REVEmail.ErrorMessage = "Enter valid Email Address(" + RXEmailRegularExpressionMsg + ")";

        REVContact.ValidationExpression = RXPhoneRegularExpression;
        REVContact.ErrorMessage = "Enter valid Contact Number(" + RXPhoneRegularExpressionMsg + ")";

        REVWebsite.ValidationExpression = RXURLRegularExpression;
        REVWebsite.ErrorMessage = "Enter valid Website URL(" + RXURLRegularExpressionMsg + ")";
    }


    public void LoadAllData()
    {
        objCommon = new clsCommon();
        objCommon.FillRecordPerPage(ref ddlCourierRatePerPage);
        //objCommon.FillDropDownList(ddlCourierCompany, "tblCourierCompany ", tblCourierCompany.ColumnNames.AppCourierCompany, tblCourierCompany.ColumnNames.AppCourierCompanyID, "--Select Courier Company--", tblCourierCompany.ColumnNames.AppCourierCompany, appFunctions.Enum_SortOrderBy.Asc);
        objCommon = null;
        LoadCourierRate();
    }

    public void LoadCourierRate()
    {
        ResetCourierRateControl();
        LoadCourierRateData(true, false);
    }

    public void ResetCourierRateControl()
    {
        txtMaxWeight.Text = "";
        txtMinWeight.Text = "";
        txtRate.Text = "";
        chkIsCOD.Checked = false;
        hdnCourierRateId.Value = "";
        ddlPINCode.SelectedIndex = 0;
    }


    protected void btnSaveCourierRate_Click(object sender, EventArgs e)
    {
        if (SaveCourierRateData())
        {
            if (string.IsNullOrEmpty(hdnCourierRateId.Value))
            {
                DInfoSubCategory.ShowMessage("Courier Rate  has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfoSubCategory.ShowMessage("Courier Rate  has been updated successfully", Enums.MessageType.Successfull);
            }
            ResetCourierRateControl();
            LoadCourierRateData(false, false);
        }
    }

    protected void btnClearCourierRate_Click(object sender, EventArgs e)
    {
        ResetCourierRateControl();
        txtMaxWeight.Text = "";
        txtMinWeight.Text = "";
        txtRate.Text = "";
        chkIsCOD.Checked = false;
    }

    private bool SaveCourierRateData()
    {
        if (hdnPKID.Value != "")
        {

            objCommon = new clsCommon();
            if (objCommon.IsRecordExists("tblCourierRate", tblCourierRate.ColumnNames.AppPinCodeID, tblCourierRate.ColumnNames.AppCourierRateID, ddlPINCode.SelectedValue, "", tblCourierRate.ColumnNames.AppCourierCompanyID + "=" + hdnPKID.Value + " and " + tblCourierRate.ColumnNames.AppMinWeight + "=" + txtMinWeight.Text + " and " + tblCourierRate.ColumnNames.AppMaxWeight + "=" + txtMaxWeight.Text + " and " + tblCourierRate.ColumnNames.AppIsCOD + "='" + chkIsCOD.Checked + "'"))
            {
                ddlPINCode.SelectedIndex = 0;
                DInfoSubCategory.ShowMessage("Data for the same already exits.", Enums.MessageType.Error);
                return false;
            }
            objCourierRate = new tblCourierRate();

            if (!string.IsNullOrEmpty(hdnCourierRateId.Value) && hdnCourierRateId.Value != "")
            {
                objCourierRate.LoadByPrimaryKey(Convert.ToInt32(hdnCourierRateId.Value));
            }
            else
            {
                objCourierRate.AddNew();
            }
            objCourierRate.AppPinCodeID = Convert.ToInt32(ddlPINCode.SelectedValue.ToString());
            objCourierRate.s_AppCourierCompanyID = hdnPKID.Value;
            objCourierRate.s_AppMinWeight = txtMinWeight.Text;
            objCourierRate.s_AppMaxWeight = txtMaxWeight.Text;
            objCourierRate.s_AppRate = txtRate.Text;
            objCourierRate.AppIsCOD = chkIsCOD.Checked;
            objCourierRate.Save();
            objCourierRate = null;
            objCommon = null;
            return true;
        }
        else
        {
            return false;
        }
    }


    public void LoadCourierRateData(bool IsResetPageIndex, bool IsSort, string strFieldName = "", string strFieldValue = "")
    {
        objCourierRate = new tblCourierRate();

        objDataTable = objCourierRate.LoadCourierRateGridData(hdnPKID.Value);

        //'Reset PageIndex of gridviews
        if (IsResetPageIndex)
        {
            if (dgvCourierRate.PageCount > 0)
            {
                dgvCourierRate.PageIndex = 0;
            }
        }

        dgvCourierRate.DataSource = null;
        dgvCourierRate.DataBind();
        lblCourierRateCount.Text = 0.ToString();
        hdnSelectedIDs.Value = "";

        //'Check for data into datatable
        if (objDataTable.Rows.Count <= 0)
        {
            DinfoBranchGrid.ShowMessage("No data found", Enums.MessageType.Information);
            return;
        }
        else
        {
            if (ddlCourierRatePerPage.SelectedItem.Text.ToLower() == "all")
            {
                dgvCourierRate.AllowPaging = false;
            }
            else
            {
                dgvCourierRate.AllowPaging = true;
                dgvCourierRate.PageSize = Convert.ToInt32(ddlCourierRatePerPage.SelectedItem.Text);
            }

            lblCourierRateCount.Text = objDataTable.Rows.Count.ToString();
            objDataTable = SortDatatable(objDataTable, ViewState["SortColumn"].ToString(), (appFunctions.Enum_SortOrderBy)ViewState["SortOrder"], IsSort);
            dgvCourierRate.DataSource = objDataTable;
            dgvCourierRate.DataBind();
        }

        objCourierRate = null;
    }

    protected void dgvCourierRate_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dgvCourierRate.PageIndex = e.NewPageIndex;
        LoadCourierRateData(false, false);
    }

    protected void dgvCourierRate_RowCreated(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                string strID = dgvCourierRate.DataKeys[e.Row.RowIndex].Values[0].ToString();
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSelectRow");
                chk.ID = "chkSelectRow_" + strID;
                break;
        }
    }

    protected void dgvCourierRate_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataControlRowType itemType = e.Row.RowType;

        switch (itemType)
        {
            case DataControlRowType.DataRow:
                string strID = dgvCourierRate.DataKeys[e.Row.RowIndex].Values[0].ToString();
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSelectRow");
                if ((chk != null))
                {
                    chk.Attributes.Add("OnClick", "javascript:SelectRow(this," + strID + ")");
                }
                break;
        }
    }

    protected void dgvCourierRate_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnSelectedIDs.Value = "";
        ViewState["SortColumn"] = e.SortExpression;
        LoadCourierRateData(false, true);
    }

    protected void ddlCourierRatePerPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnSelectedIDs.Value = "";
        LoadCourierRateData(true, false);
    }

    protected void dgvCourierRate_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
        {
            objCommon = new clsCommon();
            hdnCourierRateId.Value = e.CommandArgument.ToString();
            if (e.CommandName == "IsCOD")
            {
                objCourierRate = new tblCourierRate();

                if (objCourierRate.LoadByPrimaryKey(Convert.ToInt32(hdnCourierRateId.Value)))
                {

                    if (objCourierRate.AppIsCOD == true)
                    {
                        objCourierRate.AppIsCOD = false;
                    }
                    else if (objCourierRate.AppIsCOD == false)
                    {
                        objCourierRate.AppIsCOD = true;
                    }
                    objCourierRate.Save();
                    LoadCourierRateData(false, false);
                }
                objCourierRate = null;
            }


        }
    }



    protected void btnCourierRateDelete_Click(object sender, EventArgs e)
    {
        string[] arIDs = hdnSelectedIDs.Value.ToString().TrimEnd(',').Split(',');
        bool IsDelete = false;

        for (int i = 0; i <= arIDs.Length - 1; i++)
        {
            if (!string.IsNullOrEmpty(arIDs.GetValue(i).ToString()))
            {
                if (DeleteCourierRate(Convert.ToInt32(arIDs.GetValue(i))))
                {
                    IsDelete = true;
                }
            }
        }

        if (IsDelete)
        {
            LoadCourierRateData(false, false);
        }
        DInfoSubCategory.ShowMessage("Courier Rate has been deleted successfully", Enums.MessageType.Successfull);
        hdnSelectedIDs.Value = "";
    }

    private bool DeleteCourierRate(int intPKID)
    {
        bool retval = false;
        objCourierRate = new tblCourierRate();
        var _with1 = objCourierRate;
        if (_with1.LoadByPrimaryKey(intPKID))
        {
            _with1.MarkAsDeleted();
            _with1.Save();
        }
        retval = true;
        objCourierRate = null;
        return retval;
    }

    protected void dgvCourierRate_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void dgvCourierRate_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

}