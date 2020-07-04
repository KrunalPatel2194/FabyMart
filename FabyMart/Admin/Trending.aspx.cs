﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.IO;
using System.Drawing;


public partial class Trending: PageBase_Admin
{

    tblTrending objTrending;
    tblProduct objProduct;
    int iFeaturedProductID = 0;
    int intPkId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["SortOrder"] = appFunctions.Enum_SortOrderBy.Asc;
            ViewState["SortColumn"] = "";
            btnDelete.Visible = HasDelete;
            dgvGridView.Columns[0].Visible = HasDelete;
            dgvGridView.Columns[1].Visible = HasEdit;
            
            if ((Session[appFunctions.Session.ShowMessage.ToString()] != null))
            {
                if (!string.IsNullOrEmpty(Session[appFunctions.Session.ShowMessage.ToString()].ToString()))
                {
                    DInfo.ShowMessage(Session[appFunctions.Session.ShowMessage.ToString()].ToString(), (Enums.MessageType)Session[appFunctions.Session.ShowMessageType.ToString()]);
                    Session[appFunctions.Session.ShowMessage.ToString()] = "";
                    Session[appFunctions.Session.ShowMessageType.ToString()] = "";
                }
            }

            objCommon = new clsCommon();
            objCommon.FillRecordPerPage(ref ddlPerPage);
            objCommon.FillDropDownList(ddlCategory, "tblCategory ", tblCategory.ColumnNames.AppCategory, tblCategory.ColumnNames.AppCategoryID, "--Select Category--");
            ddlSubCate.Items.Add(new ListItem("--Select Sub Category--", "0"));
            objCommon.FillDropDownList(ddlColor, "tblColor ", tblColor.ColumnNames.AppColorName, tblColor.ColumnNames.AppColorID, "--Select Color--");
            LoadDataGrid(true, false);
            txtSearch.Focus();
            objCommon = null;
        }
    }

    private void LoadDataGrid(bool IsResetPageIndex, bool IsSort, string strFieldName = "", string strFieldValue = "")
    {
        objTrending = new tblTrending();

        objDataTable = objTrending.LoadGridData(ddlFields.SelectedValue, txtSearch.Text.Trim());

        //'Reset PageIndex of gridviews
        if (IsResetPageIndex)
        {
            if (dgvGridView.PageCount > 0)
            {
                dgvGridView.PageIndex = 0;
            }
        }

        dgvGridView.DataSource = null;
        dgvGridView.DataBind();
        lblCount.Text = 0.ToString();
        hdnSelectedIDs.Value = "";

        //'Check for data into datatable
        if (objDataTable.Rows.Count <= 0)
        {
            DInfo.ShowMessage("No data found", Enums.MessageType.Information);
            return;
        }
        else
        {
            if (ddlPerPage.SelectedItem.Text.ToLower() == "all")
            {
                dgvGridView.AllowPaging = false;
            }
            else
            {
                dgvGridView.AllowPaging = true;
                dgvGridView.PageSize = Convert.ToInt32(ddlPerPage.SelectedItem.Text);
            }

            lblCount.Text = objDataTable.Rows.Count.ToString();
            objDataTable = SortDatatable(objDataTable, ViewState["SortColumn"].ToString(), (appFunctions.Enum_SortOrderBy)ViewState["SortOrder"], IsSort);
            dgvGridView.DataSource = objDataTable;
            dgvGridView.DataBind();
        }

        objTrending = null;
    }
    protected void ddlPerPage_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        hdnSelectedIDs.Value = "";
        LoadDataGrid(true, false);
    }

    protected void dgvGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        dgvGridView.PageIndex = e.NewPageIndex;
        LoadDataGrid(false, false);
    }
    protected void dgvGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
        {
            objCommon = new clsCommon();
            hdnPKID.Value = e.CommandArgument.ToString();
            if (e.CommandName == "Up")
            {
                LinkButton inkButton = (LinkButton)e.CommandSource;
                GridViewRow drCurrent = (GridViewRow)inkButton.Parent.Parent;
                if (drCurrent.RowIndex > 0)
                {
                    GridViewRow drUp = dgvGridView.Rows[drCurrent.RowIndex - 1];
                    objCommon.SetDisplayOrder("tblTrending", tblTrending.ColumnNames.AppTrendingID, tblTrending.ColumnNames.AppDisplayOrder, (int)dgvGridView.DataKeys[drCurrent.RowIndex].Values[0], (int)dgvGridView.DataKeys[drCurrent.RowIndex].Values[2], (int)dgvGridView.DataKeys[drUp.RowIndex].Values[0], (int)dgvGridView.DataKeys[drUp.RowIndex].Values[2]);
                    LoadDataGrid(false, false);
                    objCommon = null;
                }
            }
            else if (e.CommandName == "Down")
            {
                LinkButton lnkButton = (LinkButton)e.CommandSource;
                GridViewRow drCurrent = (GridViewRow)lnkButton.Parent.Parent;
                if (drCurrent.RowIndex < dgvGridView.Rows.Count - 1)
                {
                    GridViewRow drUp = dgvGridView.Rows[drCurrent.RowIndex + 1];
                    objCommon.SetDisplayOrder("tblTrending", tblTrending.ColumnNames.AppTrendingID, tblTrending.ColumnNames.AppDisplayOrder, (int)dgvGridView.DataKeys[drCurrent.RowIndex].Values[0], (int)dgvGridView.DataKeys[drCurrent.RowIndex].Values[2], (int)dgvGridView.DataKeys[drUp.RowIndex].Values[0], (int)dgvGridView.DataKeys[drUp.RowIndex].Values[2]);
                    //objCommon.SetDisplayOrder("tblBestSeller", tblBestSeller.ColumnNames.AppBestSellerID, tblBestSeller.ColumnNames.AppDisplayOrder, (int)dgvGridView.DataKeys[drCurrent.RowIndex].Values[0], (int)dgvGridView.DataKeys[drCurrent.RowIndex].Values[2], (int)dgvGridView.DataKeys[drUp.RowIndex].Values[0], (int)dgvGridView.DataKeys[drUp.RowIndex].Values[2]);
                    LoadDataGrid(false, false);
                    objCommon = null;
                }
            }


            else if (e.CommandName == "IsActive")
            {
                objTrending = new tblTrending();

                if (objTrending.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value)))
                {

                    if (objTrending.AppIsActive == true)
                    {
                        objTrending.AppIsActive = false;
                    }
                    else if (objTrending.AppIsActive == false)
                    {
                        objTrending.AppIsActive = true;
                    }
                    objTrending.Save();
                    LoadDataGrid(false, false, "", "");
                }
                objTrending = null;
            }
        }
    }
    protected void dgvGridView_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnSelectedIDs.Value = "";
        ViewState["SortColumn"] = e.SortExpression;
        LoadDataGrid(false, true);
    }
    protected void dgvGridView_RowCreated(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                string strID = dgvGridView.DataKeys[e.Row.RowIndex].Values[0].ToString();
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSelectRow");
                chk.ID = "chkSelectRow_" + strID;
                break;
        }
    }
    protected void dgvGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataControlRowType itemType = e.Row.RowType;

        switch (itemType)
        {
            case DataControlRowType.DataRow:
                string strID = dgvGridView.DataKeys[e.Row.RowIndex].Values[0].ToString();
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSelectRow");

                if ((chk != null))
                {
                    chk.Attributes.Add("OnClick", "javascript:SelectRow(this," + strID + ")");
                }

                break;
        }
    }
    protected void btnGO_Click(object sender, EventArgs e)
    {
        LoadDataGrid(true, false);
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtSearch.Text = "";
        LoadDataGrid(true, false);
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string[] arIDs = hdnSelectedIDs.Value.ToString().TrimEnd(',').Split(',');
        bool IsDelete = false;

        for (int i = 0; i <= arIDs.Length - 1; i++)
        {
            if (!string.IsNullOrEmpty(arIDs.GetValue(i).ToString()))
            {
                if (Delete(Convert.ToInt32(arIDs.GetValue(i))))
                {
                    IsDelete = true;
                }
            }
        }

        if (IsDelete)
        {
            LoadDataGrid(false, false);
            if (ddlCategory.SelectedIndex > 0)
            {
                LoadTrending1(false, false);
                
            }
        }

        DInfo.ShowMessage("Trending has been deleted successfully", Enums.MessageType.Successfull);
        hdnSelectedIDs.Value = "";
        LoadDataGrid(false, false);
    }

    private bool Delete(int intPKID)
    {
        bool retval = false;
        objTrending = new tblTrending();

        var _with1 = objTrending;

        if (_with1.LoadByPrimaryKey(intPKID))
        {

            _with1.MarkAsDeleted();
            _with1.Save();
        }

        retval = true;
        objTrending = null;
        return retval;
    }

    public void LoadTrending1(bool IsResetPageIndex, bool IsSort, string strFieldName = "", string strFieldValue = "")
    {

        tblTrending objTrending = new tblTrending();

        objDataTable = objTrending.LoadBestTrendingList(ddlCategory.SelectedValue, ddlSubCate.SelectedValue, ddlColor.SelectedValue);

        if (IsResetPageIndex)
        {
            if (dgvProduct.PageCount > 0)
            {
                dgvProduct.PageIndex = 0;
            }
        }

        dgvProduct.DataSource = null;
        dgvProduct.DataBind();
        lblProductCount.Text = 0.ToString();
        hdnSelectedIDs.Value = "";

        //'Check for data into datatable
        if (objDataTable.Rows.Count <= 0)
        {
            DinfoProduct.ShowMessage("No data found", Enums.MessageType.Information);
            return;
        }
        else
        {
            if (ddlPerPage.SelectedItem.Text.ToLower() == "all")
            {
                dgvProduct.AllowPaging = false;
            }
            else
            {
                dgvProduct.AllowPaging = true;
                dgvProduct.PageSize = Convert.ToInt32(ddlPerPage.SelectedItem.Text);
            }

            lblProductCount.Text = objDataTable.Rows.Count.ToString();
            objDataTable = SortDatatable(objDataTable, ViewState["SortColumn"].ToString(), (appFunctions.Enum_SortOrderBy)ViewState["SortOrder"], IsSort);
            dgvProduct.DataSource = objDataTable;
            dgvProduct.DataBind();
        }

        objTrending = null;

    }

    protected void btnProductReset_Click(object sender, EventArgs e)
    {
        ddlSubCate.SelectedIndex = 0;
        ddlColor.SelectedIndex = 0;
        ddlCategory.SelectedIndex = 0;
        lblProductCount.Text = "0";
        dgvProduct.DataSource = null;
        dgvProduct.DataBind();

    }

    protected void dgvProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dgvGridView.PageIndex = e.NewPageIndex;
        LoadTrending1(false, false);
    }

    protected void dgvProduct_RowCreated(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                string strID = dgvProduct.DataKeys[e.Row.RowIndex].Values[0].ToString();

                break;

        }
    }

    protected void dgvProduct_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void dgvProduct_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnSelectedIDs.Value = "";
        ViewState["SortColumn"] = e.SortExpression;
        LoadTrending1(false, true);
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon = new clsCommon();
        objCommon.FillDropDownList(ddlSubCate, "tblSubCategory ", tblSubCategory.ColumnNames.AppSubCategory, tblSubCategory.ColumnNames.AppSubCategoryID, "--Select SubCategory--", tblSubCategory.ColumnNames.AppCategoryID + "=" + ddlCategory.SelectedValue);
        if (ddlCategory.SelectedIndex > 0)
        {
            LoadTrending1(true, false);
        }
        else
        {
            dgvProduct.DataSource = null;
            dgvProduct.DataBind();
        }
        objCommon = null;

    }
    protected void ddlColor_SelectedIndexChanged(object sender, EventArgs e)
    {

        LoadTrending1(true, false);
    }

    protected void ddlProductPerPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnSelectedIDs.Value = "";
        LoadTrending1(true, false);
    }

    protected void dgvProduct_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
        {
            objCommon = new clsCommon();
            hdnPKID.Value = e.CommandArgument.ToString();
            if (e.CommandName == "Save")
            {
                objCommon = new clsCommon();
                objTrending = new tblTrending();

                objTrending.AddNew();
                objTrending.AppDisplayOrder = objCommon.GetNextDisplayOrder("tblTrending", tblTrending.ColumnNames.AppDisplayOrder);

                objTrending.s_AppProductID = e.CommandArgument.ToString();
                objTrending.AppIsActive = true;

                objTrending.Save();

                objTrending = null;
                objCommon = null;
                LoadTrending1(true, false);
                LoadDataGrid(true, false);

            }

        }

    }

    protected void btnProductDelete_Click(object sender, EventArgs e)
    {
    }
    protected void dgvProduct_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }

    protected void dgvProduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void ddlSubCate_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadTrending1(true, false);
    }
}
