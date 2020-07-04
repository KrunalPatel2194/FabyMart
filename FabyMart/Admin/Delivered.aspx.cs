using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;

public partial class Delivered : PageBase_Admin
{
    tblSubOrder objSubOrder;
    tblOrder objOrder;
    protected void Page_Load(object sender, System.EventArgs e)
    {
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
        liDelivered.Attributes.Add("class", "active");
        liCancelled.Attributes.Add("class", "");
        liReturned.Attributes.Add("class", "");
        liComplete.Attributes.Add("class", "");
        liPaymentFail.Attributes.Add("class", "");
     
    }

    private void LoadDataGrid(bool IsResetPageIndex, bool IsSort, string strFieldName = "", string strFieldValue = "")
    {
        objOrder = new tblOrder();
        objDataTable = objOrder.GetOrderList(ddlFields.SelectedValue, txtSearch.Text.Trim(), Convert.ToInt32(Enums.Enums_OrderStatus.Delivered).ToString());
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
                System.Web.UI.HtmlControls.HtmlInputCheckBox chk = (System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Row.FindControl("chkSelectRow");
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
                        dgvGrid.DataSource = objSubOrder.GetSubOrderDetailList(strOrderID, Convert.ToInt32(Enums.Enums_OrderStatus.Delivered).ToString());
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

    protected void btnComplete_Click(object sender, System.EventArgs e)
    {
        if (hdnSelectedIDs.Value != "")
        {
            string[] arIDs = hdnSelectedIDs.Value.ToString().TrimEnd(',').Split(',');
            if (arIDs.Length > 0)
            {

                objSubOrder = new tblSubOrder();
                objSubOrder.SetOrderStatus(Convert.ToInt32(Enums.Enums_OrderStatus.Complete), hdnSelectedIDs.Value.ToString().TrimEnd(','), GetCurrentDateTime().ToString(), Convert.ToInt32(Enums.Enums_OrderStatus.Delivered).ToString());
                objSubOrder = null;
                for (int i = 0; i < arIDs.Length; i++)
                {
                    if (arIDs[i].ToString() != "")
                    {
                        SendMail(Convert.ToInt32(Enums.Enums_OrderStatus.Complete), arIDs[i].ToString());
                    }
                }
                DInfo.ShowMessage("Order status has been Change.", Enums.MessageType.Successfull);
                hdnSelectedIDs.Value = "";
                UcOrderStratus.SetOrderCount();
                LoadDataGrid(true, false);
            }
            else
            {
                DInfo.ShowMessage("Select any one Order for Completion.", Enums.MessageType.Error);
            }
        }
        else
        {
            DInfo.ShowMessage("Select any one Order for Completion.", Enums.MessageType.Error);
        }
    }
    private void SendMail(int strStatus, string strOrderID)
    {
        string strEmail = "";
        try
        {
            objCommon = new clsCommon();
            string Strbody = "";
            string strSubject = "Fabymart.com order confirmed  - " + strOrderID;
            Strbody = objCommon.readFile(Server.MapPath("~/EmailTemplates/OrderConfirmation.html"));
            Strbody = Strbody.Replace("`orderno`", strOrderID);
            if (strStatus == Convert.ToInt32(Enums.Enums_OrderStatus.Complete))
            {
                Strbody = Strbody.Replace("`orderstatus`", "Complete");
            }
            else if (strStatus == Convert.ToInt32(Enums.Enums_OrderStatus.CancelledByAdmin))
            {
                Strbody = Strbody.Replace("`orderstatus`", "Cancelled By FabyMart");
            }

            Strbody = Strbody.Replace("`orderdate`", GetCurrentDateTime().ToString());
            tblOrder objOrder = new tblOrder();
            if (objOrder.LoadByPrimaryKey(Convert.ToInt32(strOrderID)))
            {
                strEmail = objOrder.AppRecevierEmail;
            }
            objOrder = null;
            objCommon.SendConfirmationMail(strEmail, strSubject, Strbody, Enums.Enum_Confirmation_Mail_type.order);
            strEmail = "";
            objCommon = null;
        }
        catch (Exception ex)
        {
            Response.Write(ex.StackTrace.ToString());
        }
    }

    

}