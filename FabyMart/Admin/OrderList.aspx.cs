using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;

public partial class OrderList : PageBase_Admin
{
    tblSubOrder objSubOrder;
    tblOrder objOrder;
    public PageBase objPageBase = new PageBase();
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["SortOrder"] = appFunctions.Enum_SortOrderBy.Asc;
            ViewState["SortColumn"] = "";
            objCommon = new clsCommon();
            ddlStatus.Items.Add(new ListItem("--Select Status--", "0"));
            objCommon.FillDropDownList(ddlStatus, "tblOrderStatus ", tblOrderStatus.ColumnNames.AppOrderStatus, tblOrderStatus.ColumnNames.AppOrderStatusID, "--Select Status--", tblOrderStatus.ColumnNames.AppDisplayOrder, appFunctions.Enum_SortOrderBy.Asc, tblCourierCompany.ColumnNames.AppIsActive + "=1");
            objCommon = null;
            //dgvGridView.Columns[0].Visible = HasDelete;
            //dgvGridView.Columns[1].Visible = HasEdit;
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




    private void LoadDataGrid(bool IsResetPageIndex, bool IsSort, string strFieldName = "", string strFieldValue = "")
    {
        objOrder = new tblOrder();
        objDataTable = objOrder.GetOrderList(ddlFields.SelectedValue, txtSearch.Text.Trim(), ddlStatus.SelectedValue, "", "", "", "", txtStartDate.Text, txtEndDate.Text);
        //       objDataTable = objSubOrder.GetSubOrderListAdmin(ddlFields.SelectedValue, txtSearch.Text.Trim(), ddlDateType.SelectedValue, txtStartDate.Text, txtEndDate.Text, ddlStatus.SelectedValue, ddlSeller.SelectedValue, ddlCourierComapny.SelectedValue);

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

        objOrder = null;
    }

    protected void btnGO_Click(object sender, System.EventArgs e)
    {
        LoadDataGrid(true, false);
    }

    protected void btnReset_Click(object sender, System.EventArgs e)
    {
        txtSearch.Text = "";
        LoadDataGrid(true, false);
        txtEndDate.Text = "";
        txtStartDate.Text = "";
        txtSearch.Text = "";
        ddlFields.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;
        ddlDateType.SelectedIndex = 0;
    }
    protected void ddlCourierComapny_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDataGrid(true, false);
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
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
                chk.Attributes.Add("OnClick", "javascript:SelectRow(this," + strID + ")");
                break;
        }
    }


    protected void dgvGridView_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        DataControlRowType itemType = e.Row.RowType;
        switch (itemType)
        {
            case DataControlRowType.DataRow:
                string strOrderID = dgvGridView.DataKeys[e.Row.RowIndex].Values[0].ToString();
                string strPaymentMode = dgvGridView.DataKeys[e.Row.RowIndex].Values[1].ToString();
                if (strOrderID != "")
                {
                    GridView dgvGrid = (GridView)e.Row.FindControl("dgvSubDetail");
                    if (dgvGrid != null)
                    {
                        objSubOrder = new tblSubOrder();
                        objDataTable = objSubOrder.GetSubOrderDetailList(strOrderID, "");
                        dgvGrid.DataSource = objDataTable;
                        dgvGrid.DataBind();
                        objSubOrder = null;
                    }
                }
                if (strPaymentMode != "")
                {
                    Label lblPaymentMode = (Label)e.Row.FindControl("lblPaymentMode");
                    if (strPaymentMode == Convert.ToInt32(Enums.PaymentMode.COD).ToString())
                    {
                        lblPaymentMode.Text = "COD";
                        lblPaymentMode.BackColor = System.Drawing.Color.Brown;
                    }
                    if (strPaymentMode == Convert.ToInt32(Enums.PaymentMode.PayNow).ToString())
                    {
                        lblPaymentMode.Text = "Pre-Paid";
                        lblPaymentMode.BackColor = System.Drawing.Color.Green;
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






}