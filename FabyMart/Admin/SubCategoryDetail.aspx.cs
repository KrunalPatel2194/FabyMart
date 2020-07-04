using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.IO;
using System.Drawing;


public partial class SubCategoryDetail : PageBase_Admin
{
    tblSubCategory objSubCategory;
    tblSizeSubCategory objSizeSubCategory;
    int intPkId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            ViewState["SortOrder"] = appFunctions.Enum_SortOrderBy.Asc;
            ViewState["SortColumn"] = "";
            lnkSaveAndAddnew.Visible = HasAdd;
            objCommon = new clsCommon();
            objCommon.FillDropDownList(ddlCategory, "tblCategory", tblCategory.ColumnNames.AppCategory, tblCategory.ColumnNames.AppCategoryID, "-- Select Category --", tblCategory.ColumnNames.AppCategory, appFunctions.Enum_SortOrderBy.Asc);
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
    public void loadUnselectedSizeGrid()
    {

        objSizeSubCategory = new tblSizeSubCategory();

        objDataTable = objSizeSubCategory.LoadUnselectedSizeGridData(hdnPKID.Value);

        dgvUnSelectedSize.DataSource = null;
        dgvUnSelectedSize.DataBind();
        //'Check for data into datatable
        if (objDataTable.Rows.Count <= 0)
        {
            DInfoSizeGrid.ShowMessage("No data found", Enums.MessageType.Information);
            return;
        }
        else
        {
            dgvUnSelectedSize.AllowPaging = false;
            dgvUnSelectedSize.DataSource = objDataTable;
            dgvUnSelectedSize.DataBind();
        }

        objSizeSubCategory = null;


    }
    protected void dgvUnSelectedSize_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
        {
            if (e.CommandName == "IsAdd")
            {
                objCommon = new clsCommon();
                objSizeSubCategory = new tblSizeSubCategory();
                objSizeSubCategory.AddNew();
                objSizeSubCategory.s_AppDisplayOrder = objCommon.GetNextDisplayOrder("tblSizeSubCategory", tblSizeSubCategory.ColumnNames.AppDisplayOrder, tblSizeSubCategory.ColumnNames.AppSubCategoryID + "=" + hdnPKID.Value).ToString();
                objSizeSubCategory.s_AppSizeID = e.CommandArgument.ToString();
                objSizeSubCategory.s_AppSubCategoryID = hdnPKID.Value;
                objSizeSubCategory.Save();
                objSizeSubCategory = null;
                loadUnselectedSizeGrid();
                LoadSubCategoryData(false, false);
                objCommon = null;
            }
        }
    }

    private bool SaveData()
    {
        objCommon = new clsCommon();
        if (objCommon.IsRecordExists("tblSubCategory", tblSubCategory.ColumnNames.AppSubCategory, tblSubCategory.ColumnNames.AppSubCategoryID, txtSubCategoryName.Text, hdnPKID.Value, tblSubCategory.ColumnNames.AppCategoryID + "=" + ddlCategory.SelectedValue))
        {
            DInfo.ShowMessage("Sub Category Name alredy exits.", Enums.MessageType.Error);
            return false;
        }
        objSubCategory = new tblSubCategory();
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objSubCategory.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value));
        }
        else
        {
            objSubCategory.AddNew();
            objSubCategory.s_AppCreatedBy = Session[appFunctions.Session.UserID.ToString()].ToString();
            objSubCategory.AppDisplayOrder = objCommon.GetNextDisplayOrder("tblSubCategory", tblSubCategory.ColumnNames.AppDisplayOrder, tblSubCategory.ColumnNames.AppCategoryID + "=" + ddlCategory.SelectedValue);
            // objSubCategory.s_AppCreatedDate = FormatDateString(DateTime.Now.ToString(strInputDateFormat), strInputDateFormat, strOutputDateFormat);
            objSubCategory.AppCreatedDate = DateTime.Now;
        }
        objSubCategory.s_AppSubCategory = txtSubCategoryName.Text;
        objSubCategory.s_AppCategoryID = ddlCategory.SelectedValue;
        objSubCategory.AppIsActive = chkIsActive.Checked;
        objSubCategory.Save();
        intPkId = objSubCategory.AppSubCategoryID;
        objSubCategory = null;
        objCommon = null;
        return true;
    }

    private void SetValuesToControls()
    {
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objSubCategory = new tblSubCategory();
            if (objSubCategory.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value)))
            {
                ddlCategory.SelectedValue = objSubCategory.s_AppCategoryID;
                txtSubCategoryName.Text = objSubCategory.AppSubCategory;
                chkIsActive.Checked = objSubCategory.AppIsActive;

            }
            objSubCategory = null;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("SubCategory.aspx", true);
    }

    private void ResetControls()
    {
        ddlCategory.SelectedIndex = 0;
        txtSubCategoryName.Text = "";
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
                DInfo.ShowMessage("Sub Category has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("Sub Category has been updated successfully", Enums.MessageType.Successfull);
            }
            hdnPKID.Value = intPkId.ToString();
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
                Session[appFunctions.Session.ShowMessage.ToString()] = "Sub Category has been added successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            else
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "Sub Category has been updated successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            Response.Redirect("SubCategory.aspx");
        }
    }

    protected void lnkSaveAndAddnew_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("Sub Category has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("Sub Category has been updated successfully", Enums.MessageType.Successfull);
            }
            ResetControls();
        }
    }

    public void LoadAllData()
    {
        objCommon = new clsCommon();
        objCommon.FillRecordPerPage(ref ddlSubCategoryPerPage);
        //  objCommon.FillDropDownList(ddlSize, "tblSize ", tblSize.ColumnNames.AppSize, tblSize.ColumnNames.AppSizeID, "--Select Size--", tblSize.ColumnNames.AppSize, appFunctions.Enum_SortOrderBy.Asc);
        objCommon = null;
        loadUnselectedSizeGrid();
        LoadSubCategory();
        LoadSubCategoryData(false, false);
    }

    public void LoadSubCategory()
    {
        ResetSubCategoryControl();
        LoadSubCategoryData(true, false);
    }

    public void ResetSubCategoryControl()
    {
        //   ddlSize.SelectedIndex = 0;
        hdnSubCategoryId.Value = "";
    }

    protected void btnClearSubCategory_Click(object sender, EventArgs e)
    {
        ResetSubCategoryControl();
    }

    public void LoadSubCategoryData(bool IsResetPageIndex, bool IsSort, string strFieldName = "", string strFieldValue = "")
    {
        objSizeSubCategory = new tblSizeSubCategory();

        objDataTable = objSizeSubCategory.LoadSizeGridData(hdnPKID.Value);

        //'Reset PageIndex of gridviews
        if (IsResetPageIndex)
        {
            if (dgvSubCategory.PageCount > 0)
            {
                dgvSubCategory.PageIndex = 0;
            }
        }

        dgvSubCategory.DataSource = objDataTable;
        dgvSubCategory.DataBind();
        lblSubCategoryCount.Text = 0.ToString();
        hdnSelectedIDs.Value = "";

        //'Check for data into datatable
        if (objDataTable.Rows.Count <= 0)
        {
            DinfoBranchGrid.ShowMessage("No data found", Enums.MessageType.Information);
            return;
        }
        else
        {
            if (ddlSubCategoryPerPage.SelectedItem.Text.ToLower() == "all")
            {
                dgvSubCategory.AllowPaging = false;
            }
            else
            {
                dgvSubCategory.AllowPaging = true;
                dgvSubCategory.PageSize = Convert.ToInt32(ddlSubCategoryPerPage.SelectedItem.Text);
            }

            lblSubCategoryCount.Text = objDataTable.Rows.Count.ToString();
            objDataTable = SortDatatable(objDataTable, ViewState["SortColumn"].ToString(), (appFunctions.Enum_SortOrderBy)ViewState["SortOrder"], IsSort);
            dgvSubCategory.DataSource = objDataTable;
            dgvSubCategory.DataBind();
        }

        objSizeSubCategory = null;
    }

    protected void dgvSubCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dgvSubCategory.PageIndex = e.NewPageIndex;
        LoadSubCategoryData(false, false);
    }

    protected void dgvSubCategory_RowCreated(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                string strID = dgvSubCategory.DataKeys[e.Row.RowIndex].Values[0].ToString();
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSelectRow");
                chk.ID = "chkSelectRow_" + strID;
                break;
        }
    }

    protected void dgvSubCategory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataControlRowType itemType = e.Row.RowType;

        switch (itemType)
        {
            case DataControlRowType.DataRow:
                string strID = dgvSubCategory.DataKeys[e.Row.RowIndex].Values[0].ToString();
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSelectRow");
                if ((chk != null))
                {
                    chk.Attributes.Add("OnClick", "javascript:SelectRow(this," + strID + ")");
                }
                break;
        }
    }

    protected void dgvSubCategory_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnSelectedIDs.Value = "";
        ViewState["SortColumn"] = e.SortExpression;
        LoadSubCategoryData(false, true);
    }

    protected void ddlSubCategoryPerPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnSelectedIDs.Value = "";
        LoadSubCategoryData(true, false);
    }

    protected void dgvSubCategory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
        {
            objCommon = new clsCommon();
            if (e.CommandName == "Up")
            {
                LinkButton inkButton = (LinkButton)e.CommandSource;
                GridViewRow drCurrent = (GridViewRow)inkButton.Parent.Parent;
                if (drCurrent.RowIndex > 0)
                {
                    GridViewRow drUp = dgvSubCategory.Rows[drCurrent.RowIndex - 1];
                    objCommon.SetDisplayOrder("tblSizeSubCategory", tblSizeSubCategory.ColumnNames.AppSizeSubCategoryID, tblSizeSubCategory.ColumnNames.AppDisplayOrder, (int)dgvSubCategory.DataKeys[drCurrent.RowIndex].Values[0], (int)dgvSubCategory.DataKeys[drCurrent.RowIndex].Values[1], (int)dgvSubCategory.DataKeys[drUp.RowIndex].Values[0], (int)dgvSubCategory.DataKeys[drUp.RowIndex].Values[1]);
                    LoadSubCategoryData(false, false);
                    objCommon = null;
                }
            }
            else if (e.CommandName == "Down")
            {
                LinkButton lnkButton = (LinkButton)e.CommandSource;
                GridViewRow drCurrent = (GridViewRow)lnkButton.Parent.Parent;
                if (drCurrent.RowIndex < dgvSubCategory.Rows.Count - 1)
                {
                    GridViewRow drUp = dgvSubCategory.Rows[drCurrent.RowIndex + 1];
                    objCommon.SetDisplayOrder("tblSizeSubCategory", tblSizeSubCategory.ColumnNames.AppSizeSubCategoryID, tblSizeSubCategory.ColumnNames.AppDisplayOrder, (int)dgvSubCategory.DataKeys[drCurrent.RowIndex].Values[0], (int)dgvSubCategory.DataKeys[drCurrent.RowIndex].Values[1], (int)dgvSubCategory.DataKeys[drUp.RowIndex].Values[0], (int)dgvSubCategory.DataKeys[drUp.RowIndex].Values[1]);
                    LoadSubCategoryData(false, false);
                    objCommon = null;
                }
            }


        }
    }

    protected void btnSubCategoryDelete_Click(object sender, EventArgs e)
    {
        string[] arIDs = hdnSelectedIDs.Value.ToString().TrimEnd(',').Split(',');
        bool IsDelete = false;

        for (int i = 0; i <= arIDs.Length - 1; i++)
        {
            if (!string.IsNullOrEmpty(arIDs.GetValue(i).ToString()))
            {
                if (DeleteSubCategory(Convert.ToInt32(arIDs.GetValue(i))))
                {
                    IsDelete = true;
                }
            }
        }

        if (IsDelete)
        {
            LoadSubCategoryData(false, false);
        }
        DinfoBranchGrid.ShowMessage("Sub Category in size has been deleted successfully", Enums.MessageType.Successfull);
        hdnSelectedIDs.Value = "";
        loadUnselectedSizeGrid();
    }

    private bool DeleteSubCategory(int intPKID)
    {
        bool retval = false;
        objSizeSubCategory = new tblSizeSubCategory();
        var _with1 = objSizeSubCategory;
        if (_with1.LoadByPrimaryKey(intPKID))
        {
            _with1.MarkAsDeleted();
            _with1.Save();
        }
        retval = true;
        objSizeSubCategory = null;
        return retval;
    }

    protected void dgvSubCategory_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void dgvSubCategory_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}