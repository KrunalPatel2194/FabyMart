using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;

public partial class Ordered : PageBase_Admin
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

        liOrder.Attributes.Add("class", "active");
        liConfirmed.Attributes.Add("class", "");
        liReadyToShip.Attributes.Add("class", "");
        liShipped.Attributes.Add("class", "");
        liDelivered.Attributes.Add("class", "");
        liCancelled.Attributes.Add("class", "");
        liReturned.Attributes.Add("class", "");
        liComplete.Attributes.Add("class", "");
        liPaymentFail.Attributes.Add("class", "");

    }

    private void LoadDataGrid(bool IsResetPageIndex, bool IsSort, string strFieldName = "", string strFieldValue = "")
    {
        objOrder = new tblOrder();
        objDataTable = objOrder.GetOrderList(ddlFields.SelectedValue, txtSearch.Text.Trim(), Convert.ToInt32(Enums.Enums_OrderStatus.Ordered).ToString());
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
                        objDataTable = objSubOrder.GetSubOrderDetailList(strOrderID, Convert.ToInt32(Enums.Enums_OrderStatus.Ordered).ToString());
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

    protected void dgvSubDetail_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
        {
            if (e.CommandName.ToString() == "CancelOrder")
            {
                objSubOrder = new tblSubOrder();
                objSubOrder.SetSubOrderStatus(Convert.ToInt32(Enums.Enums_OrderStatus.CancelledByAdmin), e.CommandArgument.ToString(), GetCurrentDateTime().ToString(), Convert.ToInt32(Enums.Enums_OrderStatus.Ordered).ToString());
                objSubOrder = null;
                // SendMail(Convert.ToInt32(Enums.Enums_OrderStatus.CancelledByAdmin), e.CommandArgument.ToString());
                DInfo.ShowMessage("Order status has been  Cancel.", Enums.MessageType.Successfull);
                hdnSelectedIDs.Value = "";
                LoadDataGrid(true, false);
            }
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

    protected void btnConfirmed_Click(object sender, System.EventArgs e)
    {
        if (hdnSelectedIDs.Value != "")
        {
            string[] arIDs = hdnSelectedIDs.Value.ToString().TrimEnd(',').Split(',');

            if (arIDs.Length > 0)
            {

                objSubOrder = new tblSubOrder();
                //objSubOrder.SetOrderMovetoConfirm(Convert.ToInt32(Enums.Enums_OrderStatus.Ordered), Convert.ToInt32(Enums.Enums_OrderStatus.Confirmed), arIDs[i].ToString(), GetCurrentDateTime().ToString(), "", "");
                objSubOrder.SetOrderStatus(Convert.ToInt32(Enums.Enums_OrderStatus.Confirmed), hdnSelectedIDs.Value.ToString().TrimEnd(','), GetCurrentDateTime().ToString(), Convert.ToInt32(Enums.Enums_OrderStatus.Ordered).ToString());
                objSubOrder = null;
                for (int i = 0; i < arIDs.Length; i++)
                {
                    if (arIDs[i].ToString() != "")
                    {
                        SendMail(arIDs[i].ToString());
                    }
                }
                DInfo.ShowMessage("Order status has been Change.", Enums.MessageType.Successfull);
                UcOrderStratus.SetOrderCount();
                hdnSelectedIDs.Value = "";
                LoadDataGrid(true, false);
            }
            else
            {
                DInfo.ShowMessage("Select any one Order for Confirm.", Enums.MessageType.Error);
            }
        }
        else
        {
            DInfo.ShowMessage("Select any one Order for Confirm.", Enums.MessageType.Error);
        }
    }

    protected void dgvGridView_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
        {
            if (e.CommandName == "InvoiceShow")
            {
                //UcInvoice.InvoiceDetails(e.CommandArgument.ToString());
            }
        }
    }

    protected void btnCancelled_Click(object sender, EventArgs e)
    {
        if (hdnSelectedIDs.Value != "")
        {
            string[] arIDs = hdnSelectedIDs.Value.ToString().TrimEnd(',').Split(',');
            if (arIDs.Length > 0)
            {
                objSubOrder = new tblSubOrder();
                objSubOrder.SetOrderStatus(Convert.ToInt32(Enums.Enums_OrderStatus.CancelledByAdmin), hdnSelectedIDs.Value.ToString().TrimEnd(','), GetCurrentDateTime().ToString());
                objSubOrder.SetProductQty(hdnSelectedIDs.Value.ToString().TrimEnd(','));
                objSubOrder = null;
                //for (int i = 0; i < arIDs.Length; i++)
                //{
                //    if (arIDs[i].ToString() != "")
                //    {
                //        SendMail(Convert.ToInt32(Enums.Enums_OrderStatus.CancelledByAdmin), arIDs[i].ToString());
                //    }
                //}
                DInfo.ShowMessage("Order status has been change ordered to Cancel status", Enums.MessageType.Successfull);
                hdnSelectedIDs.Value = "";
                LoadDataGrid(true, false);
            }
            else
            {
                DInfo.ShowMessage("Order status has been change ordered to Cancel status", Enums.MessageType.Error);
            }
        }
        else
        {
            DInfo.ShowMessage("Select any one Order ", Enums.MessageType.Error);
        }
    }

    protected void ddlColor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDataGrid(true, false);
    }

    private void SendMail(string strOrderID)
    {
        try
        {
            tblOrder objOrder = new tblOrder();
            objDataTable = objOrder.GetOrderInfo(strOrderID, Convert.ToInt32(Enums.Enums_OrderStatus.Confirmed).ToString(), Convert.ToInt32(Enums.PaymentMode.COD).ToString());
            if (objDataTable.Rows.Count > 0)
            {
                objCommon = new clsCommon();
                string Strbody = "";
                string strSubject = "Order confirmation- Your COD Order #" + objDataTable.Rows[0][tblOrder.ColumnNames.AppOrderNo].ToString() + " with Fabymart has been successfully placed!";
                Strbody = objCommon.readFile(Server.MapPath("~/Admin/EmailTemplates/CODOrderConfirmation.html"));
                Strbody = Strbody.Replace("`link`", strServerURL);
                Strbody = Strbody.Replace("`uname`", objDataTable.Rows[0][tblOrder.ColumnNames.AppReceiverName].ToString());
                Strbody = Strbody.Replace("`orderno`", objDataTable.Rows[0][tblOrder.ColumnNames.AppOrderNo].ToString());
                Strbody = Strbody.Replace("`name`", objDataTable.Rows[0][tblOrder.ColumnNames.AppReceiverName].ToString());
                Strbody = Strbody.Replace("`address`", objDataTable.Rows[0][tblOrder.ColumnNames.AppReceiverAddress].ToString());
                Strbody = Strbody.Replace("`city`", objDataTable.Rows[0][tblCity.ColumnNames.AppCity].ToString());
                Strbody = Strbody.Replace("`pincode`", "-" + objDataTable.Rows[0][tblOrder.ColumnNames.AppReceiverPIN].ToString());
                Strbody = Strbody.Replace("`state`", objDataTable.Rows[0][tblState.ColumnNames.AppState].ToString());
                Strbody = Strbody.Replace("`country`", ", " + objDataTable.Rows[0][tblCountry.ColumnNames.AppCountry].ToString());
                Strbody = Strbody.Replace("`mobile`", objDataTable.Rows[0][tblOrder.ColumnNames.AppReceiverContactNo1].ToString());
                Strbody = Strbody.Replace("`email`", " " + objDataTable.Rows[0][tblOrder.ColumnNames.AppRecevierEmail].ToString());

                string strTable = "";
                foreach (DataRow row in objDataTable.Rows)
                {
                    strTable += "<tr>";
                    strTable += "<td align=\"left\" style=\"text-transform: capitalize\"  width=\"100\"><img src=\"" + strServerURL + "admin/" + row[tblProductImage.ColumnNames.AppSmallImage].ToString() + "\" width=\"100\" /></td>";
                    strTable += "<td align=\"left\" style=\"text-transform: capitalize\" ><a target=\"_blank\" href=\"" + GetAlias("ProductDetail.aspx") + generateUrl(row[tblProduct.ColumnNames.AppProductName].ToString()) + " \">" + row[tblProduct.ColumnNames.AppProductName].ToString() + "</a><br />Sku no :" + row[tblProductDetail.ColumnNames.AppSKUNo].ToString() + " </td>";
                    strTable += "<td align=\"right\" width=\"60\">Rs " + row["appRealPrice"].ToString() + " </td>";
                    strTable += "<td align=\"right\" width=\"80\">" + row["appQty"].ToString() + "</td>";
                    strTable += "<td align=\"right\" width=\"60\">Rs " + row["appDiscountPrice"].ToString() + " </td>";
                    strTable += "<td align=\"right\" width=\"60\">Rs " + row["appTotalPrice"].ToString() + " </td>";
                    strTable += "</tr>";
                }

                Strbody = Strbody.Replace("`table`", strTable);
                Strbody = Strbody.Replace("`Shipping`", "Rs. " + "0");
                Strbody = Strbody.Replace("`COD`", "Rs. " + "0");
                Strbody = Strbody.Replace("`Discounts`", "Rs. " + objDataTable.Compute("sum(appDiscountPrice)", "").ToString());
                Strbody = Strbody.Replace("`Total`", "Rs. " + objDataTable.Rows[0][tblOrder.ColumnNames.AppOrderAmount].ToString());

                string strText = appFunctions.strOrderConfirmed;
                strText = strText.Replace("`uname`", objDataTable.Rows[0][tblOrder.ColumnNames.AppReceiverName].ToString());
                strText = strText.Replace("`orderno`", objDataTable.Rows[0][tblOrder.ColumnNames.AppOrderNo].ToString());
                objCommon.SendOrderSMS(strText, objDataTable.Rows[0][tblOrder.ColumnNames.AppReceiverContactNo1].ToString());
                //objCommon.SendMail(objDataTable.Rows[0][tblOrder.ColumnNames.AppRecevierEmail].ToString(), strSubject, Strbody);
                objCommon.SendConfirmationMail(objDataTable.Rows[0][tblOrder.ColumnNames.AppRecevierEmail].ToString(), strSubject, Strbody, Enums.Enum_Confirmation_Mail_type.order);
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