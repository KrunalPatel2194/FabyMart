using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Globalization;
using System.Text.RegularExpressions;

public partial class _Dashboard : PageBase_Admin
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //tblUser objUser = new tblUser();

            //dgvGridView.DataSource = objUser.RecentUsers(Session[appFunctions.Session.UserID.ToString()].ToString(), Convert.ToBoolean(Session[appFunctions.Session.IsSuperAdmin.ToString()].ToString()));
            //dgvGridView.DataBind(); 
            //objUser = null;
            ViewState["SortOrder"] = appFunctions.Enum_SortOrderBy.Asc;
            ViewState["SortColumn"] = "";

            SetSubOrder();

            DataTable objPermissionDT = new DataTable();
            if ((bool)Session[appFunctions.Session.IsSuperAdmin.ToString()])
            {
                tblTab objTab = new tblTab();
                objTab.Query.AddOrderBy(tblTab.ColumnNames.AppDisplayOrder, MyGeneration.dOOdads.WhereParameter.Dir.ASC);
                objTab.Query.AddOrderBy(tblTab.ColumnNames.AppParentID, MyGeneration.dOOdads.WhereParameter.Dir.ASC);
                objTab.Where.AppIsShowOnDashboard.Value = true;
                objTab.Query.Load();
                objPermissionDT = objTab.DefaultView.Table;
            }
            else
            {
                tblPermission objPermission = new tblPermission();
                try
                {
                    objPermissionDT = objPermission.LoadTabsForRole(Convert.ToInt32(Session[appFunctions.Session.RoleID.ToString()].ToString()), true);
                }
                catch
                {
                    Response.Redirect("UserPannel.aspx");
                }
            }

            rptDashBoardLinks.DataSource = objPermissionDT;
            rptDashBoardLinks.DataBind();

            //string dashBoardString = "";
            //foreach (DataRow dr in objPermissionDT.Rows)
            //{
            //    dashBoardString += "<div class='col-md-2 col-sm-3 col-xs-3' align='Center'>";
            //    dashBoardString += "<a href='" + dr["appWebPageName"].ToString() + "'>";
            //    if (dr["appIconPath"].ToString() != "")
            //    {
            //        dashBoardString += "<img src='" + dr["appIconPath"].ToString() + "' height='100px' width='100px'  alt='" + dr["appTabName"] + "'/>";
            //    }
            //    else
            //    {
            //        dashBoardString += "<img src='Images/NoImg.png' height='100px' width='100px'  alt='" + dr["appTabName"] + "'/>";
            //    }
                
            //    dashBoardString += "<div style='text=align:center;Width:100px;line-height:20Px;height:40Px;'>" + dr["appTabName"].ToString() + "</div>";
            //    dashBoardString += "</a>";

            //    dashBoardString += "</div>";
            //}
            //DashBord.InnerHtml = dashBoardString;
            objCommon = new clsCommon();
            objCommon.FillRecordPerPage(ref ddlSubOrderPerPage);
            LoadDataGrid(true, false);
            txtSearch.Focus();
            objCommon = null;

        }
    }

    public void SetSubOrder()
    {

        tblSubOrder objSubOrder = new tblSubOrder();
        objSubOrder.Where.AppSubOrderStatusID.Value = Convert.ToInt32(Enums.Enums_OrderStatus.Ordered);
        objSubOrder.Query.Load();
        lblOrdered.Text = objSubOrder.RowCount.ToString();
        objSubOrder = null;

        objSubOrder = new tblSubOrder();
        objSubOrder.Where.AppSubOrderStatusID.Value = Convert.ToInt32(Enums.Enums_OrderStatus.Confirmed);
        objSubOrder.Query.Load();
        lblConfirmed.Text = objSubOrder.RowCount.ToString();
        objSubOrder = null;

        objSubOrder = new tblSubOrder();
        objSubOrder.Where.AppSubOrderStatusID.Value = Convert.ToInt32(Enums.Enums_OrderStatus.ReadyToShip);
        objSubOrder.Query.Load();
        lblReadyToShip.Text = objSubOrder.RowCount.ToString();
        objSubOrder = null;

        objSubOrder = new tblSubOrder();
        objSubOrder.Where.AppSubOrderStatusID.Value = Convert.ToInt32(Enums.Enums_OrderStatus.Shipped);
        objSubOrder.Query.Load();
        lblShipped.Text = objSubOrder.RowCount.ToString();
        objSubOrder = null;

        objSubOrder = new tblSubOrder();
        objDataTable = objSubOrder.GetTotalPriceDayWise(Convert.ToInt32(Enums.Enums_OrderStatus.Returned).ToString() + "," + Convert.ToInt32(Enums.Enums_OrderStatus.CancelledByCustomer).ToString() + "," + Convert.ToInt32(Enums.Enums_OrderStatus.CancelledByAdmin).ToString() + "," + Convert.ToInt32(Enums.Enums_OrderStatus.Confirmed).ToString(), GetDateTime().AddDays(-1).ToString(), GetDateTime().ToString());
        if (objDataTable.Rows.Count>0)
        {
            lblLastDayOrder.Text = objDataTable.Rows[0]["appTotalOrder"].ToString();
            lblLastDayOrderRupees.Text = objDataTable.Rows[0]["appTotalOrderPrice"].ToString();
        }
        objDataTable = null;
        objDataTable = objSubOrder.GetTotalPriceDayWise(Convert.ToInt32(Enums.Enums_OrderStatus.Returned).ToString() + "," + Convert.ToInt32(Enums.Enums_OrderStatus.CancelledByCustomer).ToString() + "," + Convert.ToInt32(Enums.Enums_OrderStatus.CancelledByAdmin).ToString() + "," + Convert.ToInt32(Enums.Enums_OrderStatus.Confirmed).ToString(), GetDateTime().AddDays(-7).ToString(), GetDateTime().ToString());
        if (objDataTable.Rows.Count > 0)
        {
            lblLastWeekOrder.Text = objDataTable.Rows[0]["appTotalOrder"].ToString();
            lblLastWeekOrderRupees.Text = objDataTable.Rows[0]["appTotalOrderPrice"].ToString();
        }
        objDataTable = null;
        objDataTable = objSubOrder.GetTotalPriceDayWise(Convert.ToInt32(Enums.Enums_OrderStatus.Returned).ToString() + "," + Convert.ToInt32(Enums.Enums_OrderStatus.CancelledByCustomer).ToString() + "," + Convert.ToInt32(Enums.Enums_OrderStatus.CancelledByAdmin).ToString() + "," + Convert.ToInt32(Enums.Enums_OrderStatus.Confirmed).ToString(), GetDateTime().AddMonths(-1).ToString(), GetDateTime().ToString());
        if (objDataTable.Rows.Count > 0)
        {
            lblLastMonthOrder .Text = objDataTable.Rows[0]["appTotalOrder"].ToString();
            lblLastMonthOrderRupees.Text = objDataTable.Rows[0]["appTotalOrderPrice"].ToString();
        }
        objDataTable = null;
        objDataTable = objSubOrder.GetTotalPriceDayWise(Convert.ToInt32(Enums.Enums_OrderStatus.Returned).ToString() + "," + Convert.ToInt32(Enums.Enums_OrderStatus.CancelledByCustomer).ToString() + "," + Convert.ToInt32(Enums.Enums_OrderStatus.CancelledByAdmin).ToString() + "," + Convert.ToInt32(Enums.Enums_OrderStatus.Confirmed).ToString(), GetDateTime().AddMonths(-1).ToString(), GetDateTime().ToString());
        if (objDataTable.Rows.Count > 0)
        {
            lblLastYearOrder .Text = objDataTable.Rows[0]["appTotalOrder"].ToString();
            lblLastYearOrderRupees.Text = objDataTable.Rows[0]["appTotalOrderPrice"].ToString();
        }
        objSubOrder = null;
    }

    private void LoadDataGrid(bool IsResetPageIndex, bool IsSort, string strFieldName = "", string strFieldValue = "")
    {
        tblSubOrder objSubOrder = new tblSubOrder();

        objDataTable = objSubOrder.GetDashBoradOrder(ddlFields.SelectedValue, txtSearch.Text.Trim(), Convert.ToInt32(Enums.Enums_OrderStatus.Ordered).ToString(),GetCurrentDateTime());

        //'Reset PageIndex of gridviews
        if (IsResetPageIndex)
        {
            if (dgvSubOrder.PageCount > 0)
            {
                dgvSubOrder.PageIndex = 0;
            }
        }

        dgvSubOrder.DataSource = null;
        dgvSubOrder.DataBind();
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
            if (ddlSubOrderPerPage.SelectedItem.Text.ToLower() == "all")
            {
                dgvSubOrder.AllowPaging = false;
            }
            else
            {
                dgvSubOrder.AllowPaging = true;
                dgvSubOrder.PageSize = Convert.ToInt32(ddlSubOrderPerPage.SelectedItem.Text);
            }

            lblCount.Text = objDataTable.Rows.Count.ToString();
            objDataTable = SortDatatable(objDataTable, ViewState["SortColumn"].ToString(), (appFunctions.Enum_SortOrderBy)ViewState["SortOrder"], IsSort);
            dgvSubOrder.DataSource = objDataTable;
            dgvSubOrder.DataBind();
        }

        objSubOrder = null;
    }

    protected void btnSubOrderGO_Click(object sender, System.EventArgs e)
    {
        LoadDataGrid(true, false);
    }

    protected void btnSubOrderReset_Click(object sender, System.EventArgs e)
    {
        txtSearch.Text = "";
        LoadDataGrid(true, false);
    }

    protected void dgvSubOrder_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        dgvSubOrder.PageIndex = e.NewPageIndex;
        LoadDataGrid(false, false);
    }

    protected void dgvSubOrder_RowCreated(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                string strID = dgvSubOrder.DataKeys[e.Row.RowIndex].Values[0].ToString();
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSelectRow");
                chk.ID = "chkSelectRow_" + strID;
                break;
        }
    }

    protected void dgvSubOrder_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        DataControlRowType itemType = e.Row.RowType;

        switch (itemType)
        {
            case DataControlRowType.DataRow:
                string strID = dgvSubOrder.DataKeys[e.Row.RowIndex].Values[0].ToString();
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSelectRow");
                if ((chk != null))
                {
                    chk.Attributes.Add("OnClick", "javascript:SelectRow(this," + strID + ")");
                }
                break;
        }
    }

    protected void dgvSubOrder_Sorting(object sender, System.Web.UI.WebControls.GridViewSortEventArgs e)
    {
        hdnSelectedIDs.Value = "";
        ViewState["SortColumn"] = e.SortExpression;
        LoadDataGrid(false, true);
    }

    protected void ddlSubOrderPerPage_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        hdnSelectedIDs.Value = "";
        LoadDataGrid(true, false);
    }

    protected void btnSubOrderConfirmed_Click(object sender, System.EventArgs e)
    {
        if (hdnSelectedIDs.Value != "")
        {
            string[] arIDs = hdnSelectedIDs.Value.ToString().TrimEnd(',').Split(',');
            if (arIDs.Length > 0)
            {
                tblSubOrder objSubOrder = new tblSubOrder();
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
                hdnSelectedIDs.Value = "";
                LoadDataGrid(true, false);
                SetSubOrder();
            }
            else
            {
                DInfo.ShowMessage("Order status has been change ordered to confirmed status", Enums.MessageType.Error);
            }
        }
        else
        {
            DInfo.ShowMessage("Select any one Order ", Enums.MessageType.Error);
        }
    }

    protected void dgvSubOrder_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
        {
            if (e.CommandName == "InvoiceShow")
            {
                UcInvoice.InvoiceDetails(e.CommandArgument.ToString());
            }
        }
    }

    protected void btnSubOrderCancelled_Click(object sender, EventArgs e)
    {
        if (hdnSelectedIDs.Value != "")
        {
            string[] arIDs = hdnSelectedIDs.Value.ToString().TrimEnd(',').Split(',');
            if (arIDs.Length > 0)
            {
                tblSubOrder objSubOrder = new tblSubOrder();
                objSubOrder.SetOrderStatus(Convert.ToInt32(Enums.Enums_OrderStatus.CancelledByAdmin), hdnSelectedIDs.Value.ToString().TrimEnd(','), GetCurrentDateTime().ToString());
                objSubOrder.SetProductQty(hdnSelectedIDs.Value.ToString().TrimEnd(','));
                objSubOrder = null;

                DInfo.ShowMessage("Order status has been change ordered to Cancel status", Enums.MessageType.Successfull);
                hdnSelectedIDs.Value = "";
                LoadDataGrid(true, false);
                SetSubOrder();
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