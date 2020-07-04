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
using Excel = Microsoft.Office.Interop.Excel;
using ExcelAutoFormat = Microsoft.Office.Interop.Excel.XlRangeAutoFormat;

public partial class MenifestList : PageBase_Admin 
{
    tblManifest objManifest;
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
        REVStartDate.ValidationExpression = RXDateRegularExpression;
        REVStartDate.ErrorMessage = "Invalid  From Date [" + RXDateRegularExpressionMsg + "]";
        REVEndDate.ValidationExpression = RXDateRegularExpression;
        REVEndDate.ErrorMessage = "Invalid  To Date [" + RXDateRegularExpressionMsg + "]";
    }


    private void LoadDataGrid(bool IsResetPageIndex, bool IsSort)
    {
        objManifest = new tblManifest();
        objDataTable = objManifest.LoadGridData(txtStartDate.Text, txtEndDate.Text);
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

        objManifest = null;
    }

    protected void btnGO_Click(object sender, System.EventArgs e)
    {
        LoadDataGrid(true, false );
    }
    protected void btnReset_Click(object sender, System.EventArgs e)
    {
        txtEndDate.Text = "";
        txtStartDate.Text = "";
        LoadDataGrid(true, false);
    }

    protected void dgvGridView_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        dgvGridView.PageIndex = e.NewPageIndex;
        LoadDataGrid(false, false);
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


    protected void dgvGridView_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
        {
            if (e.CommandName == "SaveFile")
            {
                GridViewRow gvRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                Int32 rowind = gvRow.RowIndex;
                FileUpload FileImg = (FileUpload)gvRow.FindControl("fileUpload");
                if (FileImg.HasFile)
                {
                    tblManifest objMenifest = new tblManifest();
                   
                    if (objMenifest.LoadByPrimaryKey(Convert.ToInt32(e.CommandArgument.ToString())))
                    {
                        if (objMenifest.s_AppUploadedManifest == "")
                        {
                            objCommon = new clsCommon();
                            string strError = "";
                            string Time = Convert.ToString(DateTime.Now.Month) + Convert.ToString(DateTime.Now.Day) + Convert.ToString(DateTime.Now.Year) + Convert.ToString(DateTime.Now.Hour) + Convert.ToString(DateTime.Now.Minute) + Convert.ToString(DateTime.Now.Second);
                            string strPath = objCommon.FileUpload_Images(FileImg.PostedFile, objMenifest.s_AppManifestID.Trim().Replace(" ", "_") + "_" + Time, "Uploads/Menifest/", ref strError, 0, objMenifest.s_AppUploadedManifest, false, 0, 2000);
                            if (strError == "")
                            {
                                objMenifest.AppUploadedManifest = strPath;
                            }
                            else
                            {
                                DInfo.ShowMessage(strError, Enums.MessageType.Error);
                                return;
                            }
                            objMenifest.Save();
                            objCommon = null;
                        }
                        else
                        {
                            DInfo.ShowMessage("Menifest not exists.", Enums.MessageType.Error);
                        }
                    }
                    else
                    {
                        DInfo.ShowMessage("Enter valid Menifest Number.", Enums.MessageType.Error);
                    }
                    objMenifest = null;
                    LoadDataGrid(true, false);
                }
                else
                {
                    DInfo.ShowMessage("Select Manifest File.", Enums.MessageType.Error);
                }
            }
        }
    }

}