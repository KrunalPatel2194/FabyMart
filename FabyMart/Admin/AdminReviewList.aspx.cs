using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;

public partial class Admin_ReviewList :  PageBase_Admin
{
    tblReviews objReviews;
    public PageBase objPageBase = new PageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["SortOrder"] = appFunctions.Enum_SortOrderBy.Asc;
            ViewState["SortColumn"] = "";       
            dgvGridView.Columns[0].Visible = HasDelete;          
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
            txtSearch.Focus();
            objCommon = null;
        }

    }
    private void LoadDataGrid(bool IsResetPageIndex, bool IsSort)
    {
        objReviews = new tblReviews();
        objDataTable = objReviews.LoadPendingReviewGridDataAdmin(ddlFields.SelectedValue,txtSearch.Text,"");

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
            if (IsSort)
            {
                objDataTable = SortDatatable(objDataTable, ViewState["SortColumn"].ToString(), (appFunctions.Enum_SortOrderBy)ViewState["SortOrder"], IsSort);
            }
            dgvGridView.DataSource = objDataTable;
            dgvGridView.DataBind();
        }

        objReviews = null;

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
            objCommon = null;
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
                //CheckBox chk = (CheckBox)e.Row.FindControl("chkSelectRow");
                //chk.ID = "chkSelectRow_" + strID;
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
                //CheckBox chk = (CheckBox)e.Row.FindControl("chkSelectRow");

                //if ((chk != null))
                //{
                //    chk.Attributes.Add("OnClick", "javascript:SelectRow(this," + strID + ")");
                //}
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

    protected void ddlProductName_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDataGrid(true, false);
    }
}   