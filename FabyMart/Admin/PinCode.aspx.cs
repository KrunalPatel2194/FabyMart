using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;

public partial class PinCode : PageBase_Admin
{
    tblPinCode objPinCode;

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {

            ViewState["SortOrder"] = appFunctions.Enum_SortOrderBy.Asc;
            ViewState["SortColumn"] = "";

            btnAdd.Visible = HasAdd;
            btnDelete.Visible = HasDelete;
            dgvGridView.Columns[0].Visible = HasDelete;
            dgvGridView.Columns[1].Visible = HasEdit;

            objCommon = new clsCommon();
            objCommon.FillDropDownList(ddlCountry, "tblCountry", tblCountry.ColumnNames.AppCountry, tblCountry.ColumnNames.AppCountryID, "-- Select Country --");
            ddlState.Items.Clear();
            ddlState.Items.Add(new ListItem("-- Select State --", "0"));
            ddlCity.Items.Clear();
            ddlCity.Items.Add(new ListItem("-- Select City --", "0"));
            objCommon = null;
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
            LoadDataGrid(true, false);
            txtSearch.Focus();
            objCommon = null;
        }

    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon = new clsCommon();
        objCommon.FillDropDownList(ddlState, "tblState", tblState.ColumnNames.AppState, tblState.ColumnNames.AppStateID, "-- Select State --", tblState.ColumnNames.AppCountryID + "=" + ddlCountry.SelectedValue);
        objCommon = null;
        ddlCity.Items.Clear();
        ddlCity.Items.Add(new ListItem("-- Select City --", "0"));
        LoadDataGrid(true, false);
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon = new clsCommon();
        objCommon.FillDropDownList(ddlCity, "tblCity", tblCity.ColumnNames.AppCity, tblCity.ColumnNames.AppCityID, "-- Select City --", tblCity.ColumnNames.AppStateID + "=" + ddlState.SelectedValue);
        objCommon = null;
        LoadDataGrid(true, false);
    }
    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDataGrid(true, false);
    }

    private void LoadDataGrid(bool IsResetPageIndex, bool IsSort)
    {
        objPinCode = new tblPinCode();

        objDataTable = objPinCode.LoadGridData(txtSearch.Text.Trim(), ddlCountry.SelectedValue.ToString(), ddlState.SelectedValue.ToString(), ddlCity.SelectedValue.ToString());

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

        objPinCode = null;
    }

    protected void btnGO_Click(object sender, System.EventArgs e)
    {
        LoadDataGrid(true, false);
    }

    protected void btnReset_Click(object sender, System.EventArgs e)
    {
        ddlCountry.SelectedIndex = 0;
        ddlState.Items.Clear();
        ddlState.Items.Add(new ListItem("-- Select State --", "0"));
        ddlCity.Items.Clear();
        ddlCity.Items.Add(new ListItem("-- Select City --", "0"));
        txtSearch.Text = "";
        LoadDataGrid(true, false);
    }

    protected void dgvGridView_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        dgvGridView.PageIndex = e.NewPageIndex;
        LoadDataGrid(false, false);
    }

    protected void dgvGridView_RowCreated(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
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

    protected void dgvGridView_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
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

    protected void dgvGridView_Sorting(object sender, System.Web.UI.WebControls.GridViewSortEventArgs e)
    {
        hdnSelectedIDs.Value = "";
        ViewState["SortColumn"] = e.SortExpression;
        LoadDataGrid(false, true);
    }

    protected void ddlPerPage_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        hdnSelectedIDs.Value = "";
        LoadDataGrid(true, false);
    }

    protected void btnDelete_Click(object sender, System.EventArgs e)
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
        }

        DInfo.ShowMessage("PIN Code has been deleted successfully", Enums.MessageType.Successfull);
        hdnSelectedIDs.Value = "";
    }

    private bool Delete(int intPKID)
    {
        bool retval = false;
        objPinCode = new tblPinCode();
        if (objPinCode.LoadByPrimaryKey(intPKID))
        {
            objPinCode.MarkAsDeleted();
            objPinCode.Save();
            retval = true;
        }
        objPinCode = null;
        return retval;
    }


    protected void dgvGridView_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
        {
            objCommon = new clsCommon();
            hdnPKID.Value = e.CommandArgument.ToString();

            if (e.CommandName == "IsActive")
            {
                objPinCode = new tblPinCode();

                if (objPinCode.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value)))
                {
                    if (objPinCode.s_AppIsActive != "")
                    {
                        if (objPinCode.AppIsActive == true)
                        {
                            objPinCode.AppIsActive = false;
                        }
                        else if (objPinCode.AppIsActive == false)
                        {
                            objPinCode.AppIsActive = true;
                        }
                    }
                    else
                    {
                        objPinCode.AppIsActive = true;
                    }
                    objPinCode.Save();
                    LoadDataGrid(false, false);
                }
                objPinCode = null;
            }
        }
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("PinCodeDetail.aspx");
    }
}