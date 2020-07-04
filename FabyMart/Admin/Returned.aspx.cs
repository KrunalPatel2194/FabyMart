using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;

public partial class Returned : PageBase_Admin
{
    tblSubOrder objSubOrder;
    tblOrder objOrder;
    protected void Page_Load(object sender, System.EventArgs e)
    {
        hdnTabID.Value = Convert.ToInt32(Enums.Enum_ReturnStatus.Requested).ToString();
     
        if (!IsPostBack)
        {
                       ViewState["SortOrder"] = appFunctions.Enum_SortOrderBy.Asc;
            ViewState["SortColumn"] = "";
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
            SetTab();
        }
    }
    
    public void SetTab()
    {
        System.Web.UI.HtmlControls.HtmlGenericControl liOrder = (System.Web.UI.HtmlControls.HtmlGenericControl)UcOrderStratus.FindControl("Ordered");
        System.Web.UI.HtmlControls.HtmlGenericControl liConfirmed = (System.Web.UI.HtmlControls.HtmlGenericControl)UcOrderStratus.FindControl("Confirmed");
        System.Web.UI.HtmlControls.HtmlGenericControl liReadyToShip = (System.Web.UI.HtmlControls.HtmlGenericControl)UcOrderStratus.FindControl("ReadyToShip");
        System.Web.UI.HtmlControls.HtmlGenericControl liShipped = (System.Web.UI.HtmlControls.HtmlGenericControl)UcOrderStratus.FindControl("Shipped");
        System.Web.UI.HtmlControls.HtmlGenericControl liDelivered = (System.Web.UI.HtmlControls.HtmlGenericControl)UcOrderStratus.FindControl("Delivered");
        System.Web.UI.HtmlControls.HtmlGenericControl liCancelled = (System.Web.UI.HtmlControls.HtmlGenericControl)UcOrderStratus.FindControl("Cancelled");
        System.Web.UI.HtmlControls.HtmlGenericControl liReturned = (System.Web.UI.HtmlControls.HtmlGenericControl)UcOrderStratus.FindControl("Returned");
        System.Web.UI.HtmlControls.HtmlGenericControl liComplete = (System.Web.UI.HtmlControls.HtmlGenericControl)UcOrderStratus.FindControl("Complete");
        System.Web.UI.HtmlControls.HtmlGenericControl liPaymentFail = (System.Web.UI.HtmlControls.HtmlGenericControl)UcOrderStratus.FindControl("PaymentFail");
      
        liOrder.Attributes.Add("class", "");
        liConfirmed.Attributes.Add("class", "");
        liReadyToShip.Attributes.Add("class", "");
        liShipped.Attributes.Add("class", "");
        liDelivered.Attributes.Add("class", "");
        liCancelled.Attributes.Add("class", "");
        liReturned.Attributes.Add("class", "active");
        liComplete.Attributes.Add("class", "");
        liPaymentFail.Attributes.Add("class", "");
     

    }

    private void LoadDataGrid(bool IsResetPageIndex, bool IsSort, string strFieldName = "", string strFieldValue = "")
    {
        objOrder = new tblOrder();
        objDataTable = objOrder.LoadReturnData(ddlFields.SelectedValue, txtSearch.Text.Trim(), Convert.ToInt32(Enums.Enums_OrderStatus.Returned).ToString(), hdnTabID.Value);
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
        objSubOrder = null;
    }


    protected void btnGO_Click(object sender, System.EventArgs e)
    {
        LoadDataGrid(true, false);
    }

    protected void btnReset_Click(object sender, System.EventArgs e)
    {
        txtSearch.Text = "";
        LoadDataGrid(true, false);
    }
    protected void lnkRequested_Click(object sender, System.EventArgs e)
    {
        hdnTabID.Value = Convert.ToInt32(Enums.Enum_ReturnStatus.Requested).ToString();
        Requested.Attributes.Add("class", "active");
        Dispatched.Attributes.Remove("class");
        Complete.Attributes.Remove("class");
        Approved.Attributes.Remove("class");
        LoadDataGrid(true, false);
    }
    protected void lnkApproved_Click(object sender, System.EventArgs e)
    {
        hdnTabID.Value = Convert.ToInt32(Enums.Enum_ReturnStatus.Approved).ToString();

        Requested.Attributes.Remove("class");
        Dispatched.Attributes.Remove("class");
        Complete.Attributes.Remove("class");
        Approved.Attributes.Add("class", "active");
        LoadDataGrid(true, false);
    }
    protected void lnkDispatched_Click(object sender, System.EventArgs e)
    {
        hdnTabID.Value = Convert.ToInt32(Enums.Enum_ReturnStatus.Dispatched).ToString();
        Requested.Attributes.Remove("class");
        Dispatched.Attributes.Add("class", "active");
        Complete.Attributes.Remove("class");
        Approved.Attributes.Remove("class");
        LoadDataGrid(true, false);
    }
    protected void lnkComplete_Click(object sender, System.EventArgs e)
    {
        hdnTabID.Value = Convert.ToInt32(Enums.Enum_ReturnStatus.Complete).ToString();
        Requested.Attributes.Remove("class");
        Dispatched.Attributes.Remove("class");
        Complete.Attributes.Add("class", "active");
        Approved.Attributes.Remove("class");
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
                //string strID = dgvGridView.DataKeys[e.Row.RowIndex].Values[0].ToString();
                //System.Web.UI.HtmlControls.HtmlInputCheckBox chk = (System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Row.FindControl("chkSelectRow");
                //chk.ID = "chkSelectRow_" + strID;
                //chk.Attributes.Add("OnClick", "javascript:SelectRow(this," + strID + ")");
                break;
        }
    }

    protected void dgvGridView_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        DataControlRowType itemType = e.Row.RowType;

        switch (itemType)
        {
            case DataControlRowType.DataRow:
               
                string strPaymentMode = dgvGridView.DataKeys[e.Row.RowIndex].Values[0].ToString();
                string strSubOrderID = dgvGridView.DataKeys[e.Row.RowIndex].Values[1].ToString();

                if (strSubOrderID != "")
                {
                    GridView dgvGrid = (GridView)e.Row.FindControl("dgvSubDetail");
                    if (dgvGrid != null)
                    {
                        tblReturnOrder objReturnOrder = new tblReturnOrder();
                        dgvGrid.DataSource = objReturnOrder.LoadReturOrderProduct(strSubOrderID);
                        dgvGrid.DataBind();

                        objReturnOrder = null;
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

                Label lblReturnStatus = (Label)e.Row.FindControl("lblReturnStatus");
                LinkButton lnkbtnReturnBack = (LinkButton)e.Row.FindControl("lnkbtnReturnBack");
                if (lblReturnStatus != null)
                {
                    if (lblReturnStatus.Text != "")
                    {
                        if (Convert.ToInt32(lblReturnStatus.Text) == Convert.ToInt32(Enums.Enum_ReturnStatus.Slip))
                        {
                            lnkbtnReturnBack.Visible = true;
                        }
                        if (Convert.ToInt32(lblReturnStatus.Text) == Convert.ToInt32(Enums.Enum_ReturnStatus.Requested))
                        {
                            lblReturnStatus.Text = Enums.Enum_ReturnStatus.Requested.ToString();
                        }
                        else if (Convert.ToInt32(lblReturnStatus.Text) == Convert.ToInt32(Enums.Enum_ReturnStatus.Approved) || Convert.ToInt32(lblReturnStatus.Text) == Convert.ToInt32(Enums.Enum_ReturnStatus.Slip))
                        {
                            lblReturnStatus.Text = Enums.Enum_ReturnStatus.Approved.ToString();
                        }
                        else if (Convert.ToInt32(lblReturnStatus.Text) == Convert.ToInt32(Enums.Enum_ReturnStatus.Complete))
                        {
                            lblReturnStatus.Text = Enums.Enum_ReturnStatus.Complete.ToString();
                            lnkbtnReturnBack.Visible = false;
                        }
                        else if (Convert.ToInt32(lblReturnStatus.Text) == Convert.ToInt32(Enums.Enum_ReturnStatus.Dispatched))
                        {
                            lblReturnStatus.Text = Enums.Enum_ReturnStatus.Dispatched.ToString();
                            lnkbtnReturnBack.Visible = true;
                        }
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


    protected void dgvGridView_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
        {
            if (e.CommandName.ToString() == "ReturnComplete")
            {
                tblReturnOrder objReturnOrder = new tblReturnOrder();
                if (objReturnOrder.LoadByPrimaryKey(Convert.ToInt32(e.CommandArgument.ToString())))
                {
                    objReturnOrder.AppReturnStatus = Convert.ToInt32(Enums.Enum_ReturnStatus.Complete);
                    objReturnOrder.Save();
                }
                objReturnOrder = null;
                LoadDataGrid(true, false);
            }

        }
    }

    protected void dgvSubDetail_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
        {
           
            if (e.CommandName == "IsView")
            {
                tblReturnOrder objReturnOrder = new tblReturnOrder();
                if (objReturnOrder.LoadByPrimaryKey(Convert.ToInt32(e.CommandArgument.ToString())))
                {
                    if (objReturnOrder.s_AppCourierCompanyID == "" || objReturnOrder.AppCourierCompanyID == null)
                    {
                        lblForLabel1.Text = "Contact No.";
                        lblValue1.Text = objReturnOrder.AppCourierCompanyContactNo;
                        lblForLabel2.Text = "Site URL.";
                        lblValue2.Text = objReturnOrder.AppCourierCompanyWebsite;
                    }
                    else
                    {
                        lblForLabel1.Text = "Courier Company";
                        tblCourierCompany objCourierCompany = new tblCourierCompany();
                        if (objCourierCompany.LoadByPrimaryKey(objReturnOrder.AppCourierCompanyID))
                        {
                            lblValue1.Text = objCourierCompany.AppCourierCompany;
                        }
                        objCourierCompany = null;

                        lblForLabel2.Text = "Docket No";
                        lblValue2.Text = objReturnOrder.AppDocketNo;
                    }

                    mpeView.Show();
                }

            }

        }
    }
}