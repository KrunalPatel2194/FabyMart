using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;

public partial class Shipped : PageBase_Admin
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
        liShipped.Attributes.Add("class", "active");
        liDelivered.Attributes.Add("class", "");
        liCancelled.Attributes.Add("class", "");
        liReturned.Attributes.Add("class", "");
        liComplete.Attributes.Add("class", "");
        liPaymentFail.Attributes.Add("class", "");

    }

    private void LoadDataGrid(bool IsResetPageIndex, bool IsSort, string strFieldName = "", string strFieldValue = "")
    {
        objOrder = new tblOrder();
        objDataTable = objOrder.GetOrderList(ddlFields.SelectedValue, txtSearch.Text.Trim(), Convert.ToInt32(Enums.Enums_OrderStatus.Shipped).ToString());
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
                string strPaymentMode = dgvGridView.DataKeys[e.Row.RowIndex].Values[3].ToString();
                string strCourierCompanyId = dgvGridView.DataKeys[e.Row.RowIndex].Values[1].ToString();
                string strDocketNo = dgvGridView.DataKeys[e.Row.RowIndex].Values[2].ToString();
                if (strOrderID != "")
                {
                    GridView dgvGrid = (GridView)e.Row.FindControl("dgvSubDetail");
                    if (dgvGrid != null)
                    {
                        objSubOrder = new tblSubOrder();
                        dgvGrid.DataSource = objSubOrder.GetSubOrderDetailList(strOrderID, Convert.ToInt32(Enums.Enums_OrderStatus.Shipped).ToString());
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

                System.Web.UI.HtmlControls.HtmlGenericControl divCourierCompany = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Row.FindControl("divCourierCompany");
                divCourierCompany.Visible = false;
                TextBox txtDocketNo = (TextBox)e.Row.FindControl("txtDocketNo");
                LinkButton lnkAddDocketNo = (LinkButton)e.Row.FindControl("lnkAddDocketNo");
                if (strDocketNo != "")
                {
                    objSubOrder = new tblSubOrder();
                    txtDocketNo.Text = strDocketNo;
                    txtDocketNo.Style.Add("display", "block");
                    txtDocketNo.Enabled = false;
                    lnkAddDocketNo.Visible = false;
                    objSubOrder = null;
                }
                else if (strCourierCompanyId != "" && strDocketNo == "")
                {
                    txtDocketNo.Style.Add("display", "block");
                    lnkAddDocketNo.Visible = true;
                }
                else if (strCourierCompanyId == "" && strDocketNo == "")
                {
                    txtDocketNo.Style.Add("display", "none");
                    lnkAddDocketNo.Visible = false;
                    divCourierCompany.Visible = true;
                    DropDownList ddlCourierCompany = (DropDownList)e.Row.FindControl("ddlCourierCompany");
                    objCommon = new clsCommon();
                    objCommon.FillDropDownListWithOutDefaultValue(ddlCourierCompany, "tblCourierCompany", tblCourierCompany.ColumnNames.AppCourierCompany, tblCourierCompany.ColumnNames.AppCourierCompanyID, tblCourierCompany.ColumnNames.AppDisplayOrder, appFunctions.Enum_SortOrderBy.Asc, tblCourierCompany.ColumnNames.AppIsActive + "=1");
                    objCommon = null;
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

    protected void btnDelivered_Click(object sender, System.EventArgs e)
    {
        if (hdnSelectedIDs.Value != "")
        {
            string[] arIDs = hdnSelectedIDs.Value.ToString().TrimEnd(',').Split(',');
            if (arIDs.Length > 0)
            {

                objSubOrder = new tblSubOrder();
                //objSubOrder.SetOrderMoveToDelivered(Convert.ToInt32(Enums.Enums_OrderStatus.Shipped), Convert.ToInt32(Enums.Enums_OrderStatus.Delivered), hdnSelectedIDs.Value.ToString().TrimEnd(','), GetCurrentDateTime().ToString());
                objSubOrder.SetOrderStatus(Convert.ToInt32(Enums.Enums_OrderStatus.Delivered), hdnSelectedIDs.Value.ToString().TrimEnd(','), GetCurrentDateTime().ToString(), Convert.ToInt32(Enums.Enums_OrderStatus.Shipped).ToString());
                objSubOrder = null;
                for (int i = 0; i < arIDs.Length; i++)
                {
                    if (arIDs[i].ToString() != "")
                    {
                        SendMail(Convert.ToInt32(Enums.Enums_OrderStatus.Delivered), arIDs[i].ToString());
                    }
                }
                DInfo.ShowMessage("Order status has been Change.", Enums.MessageType.Successfull);
                hdnSelectedIDs.Value = "";
                UcOrderStratus.SetOrderCount();
                LoadDataGrid(true, false);
            }
            else
            {
                DInfo.ShowMessage("Select any one Order for Delivered.", Enums.MessageType.Error);
            }
        }
        else
        {
            DInfo.ShowMessage("Select any one Order for Delivered.", Enums.MessageType.Error);
        }
    }

    protected void dgvGridView_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
        {
            if (e.CommandName == "addDocketNo")
            {
                GridViewRow gvRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                Int32 rowind = gvRow.RowIndex;
                TextBox txtDocketNo = (TextBox)gvRow.FindControl("txtDocketNo");
                if (txtDocketNo != null && txtDocketNo.Text != "")
                {
                    if (dgvGridView.DataKeys[gvRow.RowIndex].Values[1].ToString() != "" && dgvGridView.DataKeys[gvRow.RowIndex].Values[3].ToString() != "")
                    {
                        objSubOrder = new tblSubOrder();
                        objSubOrder.SetDocketNumber(Convert.ToInt32(Enums.Enums_OrderStatus.Confirmed), dgvGridView.DataKeys[gvRow.RowIndex].Values[0].ToString(), dgvGridView.DataKeys[gvRow.RowIndex].Values[1].ToString(), txtDocketNo.Text, true);
                        objSubOrder = null;
                        SendShippingMail(e.CommandArgument.ToString());
                        LoadDataGrid(true, false);
                    }
                }
                else
                {
                    DInfo.ShowMessage("Enter Docket Number", Enums.MessageType.Error);
                }
            }
            if (e.CommandName == "SaveCourierCompany")
            {
                GridViewRow gvRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                DropDownList ddlCourierCompany = (DropDownList)gvRow.FindControl("ddlCourierCompany");
                TextBox txtCourierCompanyDocket = (TextBox)gvRow.FindControl("txtCourierCompanyDocket");
                if (txtCourierCompanyDocket.Text != "")
                {
                    objSubOrder = new tblSubOrder();
                    objSubOrder.SetDocketNumberAndCourierCompanyIDOnOrderId(Convert.ToInt32(Enums.Enums_OrderStatus.Shipped), e.CommandArgument.ToString(), ddlCourierCompany.SelectedValue, txtCourierCompanyDocket.Text, true);
                    objSubOrder = null;
                    SendShippingMail(e.CommandArgument.ToString());
                }
                else
                {
                    txtCourierCompanyDocket.Style.Add("border", "1px solid #D2322D");
                }
                objSubOrder = null;
                LoadDataGrid(true, false);
            }

        }
    }

    protected void dgvSubDetail_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
        {
            if (e.CommandName.ToString() == "CancelOrder")
            {
                //objSubOrder = new tblSubOrder();
                //objSubOrder.SetOrderSellerWiseOrderStatus(Convert.ToInt32(Enums.Enums_OrderStatus.Shipped).ToString(), Convert.ToInt32(Enums.Enums_OrderStatus.CancelledBySeller), hdnSelectedIDs.Value.ToString().TrimEnd(','), GetCurrentDateTime().ToString());
                //objSubOrder = null;
                //objSubOrder = new tblSubOrder();
                //objSubOrder.SetOrderStatus(Convert.ToInt32(Enums.Enums_OrderStatus.CancelledByAdmin), hdnSelectedIDs.Value.ToString().TrimEnd(','), GetCurrentDateTime().ToString(), Convert.ToInt32(Enums.Enums_OrderStatus.Shipped).ToString());
                //objSubOrder = null;
                //DInfo.ShowMessage("Order status has been  Cancel.", Enums.MessageType.Successfull);
                //hdnSelectedIDs.Value = "";
                //LoadDataGrid(true, false);
            }
        }
    }


    private void SendMail(int strStatus, string strOrderID)
    {
        try
        {
            tblOrder objOrder = new tblOrder();
            if (objOrder.LoadByPrimaryKey(Convert.ToInt32(strOrderID)))
            {
                if (objOrder.AppOrderStatusID == Convert.ToInt32(Enums.Enums_OrderStatus.Delivered))
                {
                    objCommon = new clsCommon();
                    string Strbody = "";
                    string strSubject = "Order Delivered - Your fabymart Order #" + objOrder.s_AppOrderNo + " has been delivered";
                    if (objOrder.AppPaymentMode == Convert.ToInt32(Enums.PaymentMode.COD))
                    {
                        Strbody = objCommon.readFile(Server.MapPath("~/Admin/EmailTemplates/CODOrderDelivered.html"));
                    }
                    else
                    {
                        Strbody = objCommon.readFile(Server.MapPath("~/Admin/EmailTemplates/OrderDelivered.html"));
                    }
                    Strbody = Strbody.Replace("`orderno`", objOrder.s_AppOrderNo);
                    Strbody = Strbody.Replace("`uname`", objOrder.AppReceiverName);
                    Strbody = Strbody.Replace("`link`", strServerURL);
                    Strbody = Strbody.Replace("`Date`", objOrder.AppOrderStatusChangeDate.ToString("dd-MM-yyyy"));
                    objCommon.SendConfirmationMail(objOrder.AppRecevierEmail, strSubject, Strbody, Enums.Enum_Confirmation_Mail_type.order);
                    //objCommon.SendMail(objOrder.AppRecevierEmail, strSubject, Strbody);
                    string strText = appFunctions.strOrderDelivered;
                    strText = strText.Replace("`uname`", objOrder.AppReceiverName);
                    strText = strText.Replace("`orderno`", objOrder.AppOrderNo);
                    strText = strText.Replace("`deliverydate`", objOrder.AppOrderStatusChangeDate.ToString("dd-MM-yyyy"));
                    objCommon.SendOrderSMS(strText, objOrder.AppReceiverContactNo1);
                    objCommon = null;
                }
            }
            objOrder = null;
        }
        catch (Exception ex)
        {
            Response.Write(ex.StackTrace.ToString());
        }
    }

    private void SendShippingMail(string strOrderID)
    {
        try
        {
            tblOrder objOrder = new tblOrder();
            objDataTable = objOrder.GetShippedOrderInfo(strOrderID, Convert.ToInt32(Enums.Enums_OrderStatus.Shipped).ToString());
            if (objDataTable.Rows.Count > 0)
            {

                objCommon = new clsCommon();
                string Strbody = "";
                string strSubject = "Order Shipped- Your order #" + objDataTable.Rows[0][tblOrder.ColumnNames.AppOrderNo].ToString() + " has been shipped!";
                if (Convert.ToInt32(objDataTable.Rows[0][tblOrder.ColumnNames.AppPaymentMode].ToString()) == Convert.ToInt32(Enums.PaymentMode.COD))
                {
                    Strbody = objCommon.readFile(Server.MapPath("~/Admin/EmailTemplates/CODOrderShipped.html"));
                }
                else
                {
                    Strbody = objCommon.readFile(Server.MapPath("~/Admin/EmailTemplates/OrderShipped.html"));
                }


                Strbody = Strbody.Replace("`link`", strServerURL);
                Strbody = Strbody.Replace("`uname`", objDataTable.Rows[0][tblOrder.ColumnNames.AppReceiverName].ToString());
                Strbody = Strbody.Replace("`orderno`", objDataTable.Rows[0][tblOrder.ColumnNames.AppOrderNo].ToString());
                Strbody = Strbody.Replace("`date`", Convert.ToDateTime(objDataTable.Rows[0][tblOrder.ColumnNames.AppCreatedDate].ToString()).ToString("MMMM dd, yyyy HH:MM:ss tt"));
                Strbody = Strbody.Replace("`ShippedBy`", objDataTable.Rows[0][tblCourierCompany.ColumnNames.AppCourierCompany].ToString());
                Strbody = Strbody.Replace("`TrackingNumber`", objDataTable.Rows[0][tblSubOrder.ColumnNames.AppDocketNo].ToString());

                Strbody = Strbody.Replace("`name`", objDataTable.Rows[0][tblOrder.ColumnNames.AppReceiverName].ToString());
                Strbody = Strbody.Replace("`address`", objDataTable.Rows[0][tblOrder.ColumnNames.AppReceiverAddress].ToString());
                Strbody = Strbody.Replace("`city`", objDataTable.Rows[0][tblCity.ColumnNames.AppCity].ToString());
                Strbody = Strbody.Replace("`state`", objDataTable.Rows[0][tblState.ColumnNames.AppState].ToString());
                Strbody = Strbody.Replace("`country`", objDataTable.Rows[0][tblCountry.ColumnNames.AppCountry].ToString());
                Strbody = Strbody.Replace("`pincode`", objDataTable.Rows[0][tblOrder.ColumnNames.AppReceiverPIN].ToString());
                Strbody = Strbody.Replace("`mobile`", objDataTable.Rows[0][tblOrder.ColumnNames.AppReceiverContactNo1].ToString());
                Strbody = Strbody.Replace("`email`", objDataTable.Rows[0][tblOrder.ColumnNames.AppRecevierEmail].ToString());

                Strbody = Strbody.Replace("`billname`", objDataTable.Rows[0][tblOrder.ColumnNames.AppBillReceiverName].ToString());
                Strbody = Strbody.Replace("`billaddress`", objDataTable.Rows[0][tblOrder.ColumnNames.AppBillReceiverAddress].ToString());
                Strbody = Strbody.Replace("`billcity`", objDataTable.Rows[0]["AppBillCity"].ToString());
                Strbody = Strbody.Replace("`billstate`", objDataTable.Rows[0]["AppBillState"].ToString());
                Strbody = Strbody.Replace("`billcountry`", objDataTable.Rows[0]["AppBillCountry"].ToString());
                Strbody = Strbody.Replace("`billpincode`", objDataTable.Rows[0][tblOrder.ColumnNames.AppBillReceiverPIN].ToString());
                Strbody = Strbody.Replace("`billmobile`", objDataTable.Rows[0][tblOrder.ColumnNames.AppBillReceiverContactNo1].ToString());
                Strbody = Strbody.Replace("`billemail`", objDataTable.Rows[0][tblOrder.ColumnNames.AppBillRecevierEmail].ToString());

                objCommon.SendConfirmationMail(objDataTable.Rows[0][tblOrder.ColumnNames.AppRecevierEmail].ToString(), strSubject, Strbody, Enums.Enum_Confirmation_Mail_type.order);
                //objCommon.SendMail(objDataTable.Rows[0][tblOrder.ColumnNames.AppRecevierEmail].ToString(), strSubject, Strbody);
                string strText = "";

                if (Convert.ToInt32(objDataTable.Rows[0][tblOrder.ColumnNames.AppPaymentMode].ToString()) == Convert.ToInt32(Enums.PaymentMode.COD))
                {
                    strText = appFunctions.strOrderShipped;
                }
                else
                {
                    strText = appFunctions.strOrderShipped;
                }
                strText = strText.Replace("`uname`", objDataTable.Rows[0][tblOrder.ColumnNames.AppReceiverName].ToString());
                strText = strText.Replace("`orderno`", objDataTable.Rows[0][tblOrder.ColumnNames.AppOrderNo].ToString());
                strText = strText.Replace("`couriername`", objDataTable.Rows[0][tblCourierCompany.ColumnNames.AppCourierCompany].ToString());
                strText = strText.Replace("`trackingno`", objDataTable.Rows[0][tblSubOrder.ColumnNames.AppDocketNo].ToString());
                objCommon.SendOrderSMS(strText, objDataTable.Rows[0][tblOrder.ColumnNames.AppReceiverContactNo1].ToString());
                objCommon = null;
            }
            objOrder = null;
        }
        catch (Exception ex)
        {
            Response.Write(ex.StackTrace.ToString());
        }
    }
}