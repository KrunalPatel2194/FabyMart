using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;

public partial class CouponCodeList : PageBase_Admin
{
    tblCouponCode objCouponCode;
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["SortOrder"] = appFunctions.Enum_SortOrderBy.Asc;
            ViewState["SortColumn"] = "";
            SetRegulerExpression();
            btnAdd.Visible = HasAdd;
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
            LoadDataGrid(true, false);
            txtSearch.Focus();
            objCommon = null;
        }
    }

    public void SetRegulerExpression()
    {
        REVStartDate.ValidationExpression = RXDateRegularExpression;
        REVStartDate.ErrorMessage = "Invalid  From Date [" + RXDateRegularExpressionMsg + "]";
        REVEndDate.ValidationExpression = RXDateRegularExpression;
        REVEndDate.ErrorMessage = "Invalid  To Date [" + RXDateRegularExpressionMsg + "]";
    }

    private void LoadDataGrid(bool IsResetPageIndex, bool IsSort)
    {
        objCouponCode = new tblCouponCode();
        objDataTable = objCouponCode.LoadGridData(ddlFields.SelectedValue, txtSearch.Text.Trim(), ddlDateType.SelectedValue, txtStartDate.Text, txtEndDate.Text);
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
        objCouponCode = null;
    }

    protected void btnGO_Click(object sender, System.EventArgs e)
    {
        LoadDataGrid(true, false);
    }

    protected void btnReset_Click(object sender, System.EventArgs e)
    {
        txtSearch.Text = "";
        ddlDateType.SelectedIndex = 0;
        txtStartDate.Text = "";
        txtEndDate.Text = "";
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
                e.Row.Cells[3].Text = e.Row.Cells[3].Text + " %";
                Label lblType = (Label)e.Row.FindControl("lblType");
                if (lblType.Text != "")
                {
                    if (Convert.ToInt32(lblType.Text) == Convert.ToInt32(Enums.Enum_CouponCodeType.Category))
                    {
                        lblType.Text = Enums.Enum_CouponCodeType.Category.ToString();
                    }
                    else if (Convert.ToInt32(lblType.Text) == Convert.ToInt32(Enums.Enum_CouponCodeType.Product))
                    {
                        lblType.Text = Enums.Enum_CouponCodeType.Product.ToString();
                    }
                    else if (Convert.ToInt32(lblType.Text) == Convert.ToInt32(Enums.Enum_CouponCodeType.SubCategory))
                    {
                        lblType.Text = Enums.Enum_CouponCodeType.SubCategory.ToString();
                    }
                    else if (Convert.ToInt32(lblType.Text) == Convert.ToInt32(Enums.Enum_CouponCodeType.General))
                    {
                        lblType.Text = Enums.Enum_CouponCodeType.General.ToString();
                    }
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
        DInfo.ShowMessage("Coupon Code has been deleted successfully", Enums.MessageType.Successfull);
        hdnSelectedIDs.Value = "";
    }

    private bool Delete(int intPKID)
    {
        bool retval = false;
        objCouponCode = new tblCouponCode();
        if (objCouponCode.LoadByPrimaryKey(intPKID))
        {
            objCouponCode.MarkAsDeleted();
            objCouponCode.Save();
            retval = true;
        }

        objCouponCode = null;
        return retval;
    }


    protected void dgvGridView_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
        {
            if (e.CommandName == "IsActive")
            {
                objCouponCode = new tblCouponCode();
                if (objCouponCode.LoadByPrimaryKey(Convert.ToInt32(e.CommandArgument.ToString())))
                {
                    if (objCouponCode.AppIsActive == true)
                    {
                        objCouponCode.AppIsActive = false;
                    }
                    else if (objCouponCode.AppIsActive == false)
                    {
                        objCouponCode.AppIsActive = true;
                    }
                    objCouponCode.Save();
                    LoadDataGrid(false, false);
                }
                objCouponCode = null;
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("CouponCodeDetail.aspx");
    }
}