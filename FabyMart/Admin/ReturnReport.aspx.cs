using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;

public partial class AdminPendingReurnList : PageBase_Admin
{
    tblReturnOrder objReturnOrder;
    public PageBase objPageBase = new PageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["SortOrder"] = appFunctions.Enum_SortOrderBy.Asc;
            ViewState["SortColumn"] = "";
            
            if (Session[appFunctions.Session.ShowMessage.ToString()] != null)
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
            if ((Request.QueryString.Get("ID") != null))
            {
                objEncrypt = new clsEncryption();
                try
                {
                    string pid = objEncrypt.Decrypt(Request.QueryString.Get("ID"), appFunctions.strKey);
                }
                catch (Exception ex)
                {
                }
            }
            LoadDataGrid(true, false);
            objCommon = null;
        }
    }

    private void LoadDataGrid(bool IsResetPageIndex, bool IsSort)
    {
        objReturnOrder = new tblReturnOrder();
        objDataTable = objReturnOrder.LoadReturnReportOuterGrid();

        if (IsResetPageIndex)
        {
            if (OuterGrid.PageCount > 0)
            {
                OuterGrid.PageIndex = 0;
            }
        }

        OuterGrid.DataSource = null;
        OuterGrid.DataBind();
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
                OuterGrid.AllowPaging = false;
            }
            else
            {
                OuterGrid.AllowPaging = true;
                OuterGrid.PageSize = Convert.ToInt32(ddlPerPage.SelectedItem.Text);
            }

            lblCount.Text = objDataTable.Rows.Count.ToString();
            if (IsSort)
            {
                objDataTable = SortDatatable(objDataTable, ViewState["SortColumn"].ToString(), (appFunctions.Enum_SortOrderBy)ViewState["SortOrder"], IsSort);
            }
            OuterGrid.DataSource = objDataTable;
            OuterGrid.DataBind();
        }
        objReturnOrder = null;
    }

    protected void btnGO_Click(object sender, EventArgs e)
    {
        LoadDataGrid(true, false);
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        LoadDataGrid(true, false);
    }

    protected void OuterGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        OuterGrid.PageIndex = e.NewPageIndex;
        LoadDataGrid(false, false);
    }



    protected void OuterGrid_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnSelectedIDs.Value = "";
        ViewState["SortColumn"] = e.SortExpression;
        LoadDataGrid(false, true);
    }

    protected void OuterGrid_RowCreated(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                string strID = OuterGrid.DataKeys[e.Row.RowIndex].Values[0].ToString();
                break;
        }
    }

    protected void OuterGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataControlRowType itemType = e.Row.RowType;
        switch (itemType)
        {
            case DataControlRowType.DataRow:
                string strCustomerID = OuterGrid.DataKeys[e.Row.RowIndex].Values[0].ToString();
                GridView dgvSubDetail = (GridView)e.Row.FindControl("dgvSubDetail");
                if (dgvSubDetail != null)
                {
                    objReturnOrder = new tblReturnOrder();
                    dgvSubDetail.DataSource = objReturnOrder.GetProductData(strCustomerID);
                    dgvSubDetail.DataBind();
                    objReturnOrder = null;
                }
                break;
        }
    }
    

    protected void ddlPerPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnSelectedIDs.Value = "";
        LoadDataGrid(true, false);
    }

    protected void ddlSeller_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDataGrid(true, false);

    }


}