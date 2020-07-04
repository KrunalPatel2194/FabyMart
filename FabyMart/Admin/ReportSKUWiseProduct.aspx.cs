using System;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Web.Services;
using System.IO;

public partial class ReportSKUWiseProduct : PageBase_Admin
{
    tblProductDetail objProductDetail;
    override protected void OnInit(EventArgs e)
    {
        ExpFile.btnClick += new EventHandler(btnSetExportData_Click);
        base.OnInit(e);
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["SortOrder"] = appFunctions.Enum_SortOrderBy.Asc;
            ViewState["SortColumn"] = "";
            SetRegulerExpression();
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
            objCommon = null;
        }
    }

    public void SetRegulerExpression()
    {
        //REVQuantity.ValidationExpression = RXNumericRegularExpression;
        //REVQuantity.ErrorMessage = "Invalid Quantity [" + RXNumericRegularExpressionMsg + "]";
    }

    private void LoadDataGrid(bool IsResetPageIndex, bool IsSort)
    {
        objProductDetail = new tblProductDetail();
        objDataTable = objProductDetail.GetReportSKUNOWise(txtSKUNO.Text);
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
        //'Check for data into datatable
        if (objDataTable.Rows.Count <= 0)
        {
            DInfo.ShowMessage("No data found", Enums.MessageType.Information);
            btnExportExcel.Visible = false;
            return;
        }
        else
        {
            btnExportExcel.Visible = true;
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
        objProductDetail = null;
    }

    protected void btnGO_Click(object sender, System.EventArgs e)
    {
        LoadDataGrid(true, false);
    }

    protected void btnReset_Click(object sender, System.EventArgs e)
    {
        txtSKUNO.Text = "";
        LoadDataGrid(true, false);
    }

    protected void dgvGridView_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        dgvGridView.PageIndex = e.NewPageIndex;
        LoadDataGrid(false, false);
    }

    protected void dgvGridView_Sorting(object sender, System.Web.UI.WebControls.GridViewSortEventArgs e)
    {
        ViewState["SortColumn"] = e.SortExpression;
        LoadDataGrid(false, true);
    }

    protected void ddlPerPage_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        LoadDataGrid(true, false);
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        ExpFile.SetListBoxData(GetAllExportColumns());
        ExpFile.Show();
    }

    public DataTable GetAllExportColumns()
    {
        DataTable objDt = new DataTable();
        objDt.Columns.Add("ColumnName");
        objDt.Columns.Add("Text");

        DataRow dr = objDt.NewRow();
        dr[0] = "appProductCode";
        dr[1] = "Product Code";
        objDt.Rows.Add(dr);

        dr = objDt.NewRow();
        dr[0] = "appSKUNo";
        dr[1] = "SKUNo";
        objDt.Rows.Add(dr);

        dr = objDt.NewRow();
        dr[0] = "appProductName";
        dr[1] = "Product Name";
        objDt.Rows.Add(dr);

        dr = objDt.NewRow();
        dr[0] = "appColorName";
        dr[1] = "Color Name";
        objDt.Rows.Add(dr);

        dr = objDt.NewRow();
        dr[0] = "appColorCode";
        dr[1] = "Color Code";
        objDt.Rows.Add(dr);

        dr = objDt.NewRow();
        dr[0] = "appSize";
        dr[1] = "Size";
        objDt.Rows.Add(dr);

        dr = objDt.NewRow();
        dr[0] = "appQuantity";
        dr[1] = "Quantity";
        objDt.Rows.Add(dr);

        return objDt;
    }

    protected void btnSetExportData_Click(object sender, EventArgs e)
    {
        objProductDetail = new tblProductDetail();
        objDataTable = objProductDetail.GetReportSKUNOWise(txtSKUNO.Text);
        objProductDetail = null;
        ExpFile.SetFileName("SKUWiseProduct");
        ExpFile.SetExportData(objDataTable);
    }
}