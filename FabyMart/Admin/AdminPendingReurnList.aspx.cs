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
        objReturnOrder = new tblReturnOrder();
        objDataTable = objReturnOrder.LoadPendingReturnOrderAdmin(ddlFields.SelectedValue, txtSearch.Text, txtStartDate.Text, txtEndDate.Text);

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

        objReturnOrder = null;

    }

    protected void btnGO_Click(object sender, EventArgs e)
    {
        LoadDataGrid(true, false);
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtSearch.Text = "";

        txtEndDate.Text = "";
        txtStartDate.Text = "";


        LoadDataGrid(true, false);
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        string[] arIDs = hdnSelectedIDs.Value.ToString().TrimEnd(',').Split(',');
        bool IsApprove = false;
        for (int i = 0; i <= arIDs.Length - 1; i++)
        {
            if (!string.IsNullOrEmpty(arIDs.GetValue(i).ToString()))
            {
                if (Approve(Convert.ToInt32(arIDs.GetValue(i))))
                {
                    IsApprove = true;
                }
            }
            //if (arIDs[i].ToString() != "")
            //{
            //    SendMail(Convert.ToInt32(Enums.Enum_ReturnStatus.Approved), arIDs[i].ToString());
            //}
        }
        if (IsApprove)
        {
            LoadDataGrid(false, false);
            DInfo.ShowMessage("Return Order has been approved successfully", Enums.MessageType.Successfull);
        }
        hdnSelectedIDs.Value = "";
    }

    private void SendMail(int strStatus, string strOrderID)
    {
        string strEmail = "";
        try
        {
            objCommon = new clsCommon();
            string Strbody = "";
            string strSubject = "Fabymart.com order ";

            Strbody = objCommon.readFile(Server.MapPath("~/Admin/EmailTemplates/OrderConfirmation.html"));
            
            if (strStatus == Convert.ToInt32(Convert.ToInt32(Enums.Enum_ReturnStatus.Approved)))
            {
                Strbody = Strbody.Replace("`orderstatus`", "Return Approved");
                strSubject += " Return Approved - ";
            }
            else if (strStatus == Convert.ToInt32(Enums.Enum_ReturnStatus.Reject))
            {
                Strbody = Strbody.Replace("`orderstatus`", "Return Rejected By FabyMart");
                strSubject += " Return Rejected - " ;
            }
            Strbody = Strbody.Replace("`shipmentdate`", "");
            Strbody = Strbody.Replace("`link`", strServerURL);
            Strbody = Strbody.Replace("`orderdate`", DateTime.Now.Date.ToString("dd-MM-yyyy"));
            tblOrder objOrder = new tblOrder();
            if (objOrder.LoadByPrimaryKey(Convert.ToInt32(strOrderID)))
            {
                strEmail = objOrder.AppRecevierEmail;
                Strbody = Strbody.Replace("`uname`", objOrder.AppReceiverName);
                Strbody = Strbody.Replace("`orderno`", objOrder.s_AppOrderNo);
                strSubject += objOrder.s_AppOrderNo;
            }
            objOrder = null;
            objCommon.SendMail(strEmail, strSubject, Strbody);
            // objCommon.SendConfirmationMail(strEmail, strSubject, Strbody, Enums.Enum_Confirmation_Mail_type.order);
          
            strEmail = "";
            objCommon = null;
        }
        catch (Exception ex)
        {
            Response.Write(ex.StackTrace.ToString());
        }
    }


    private bool Approve(int intPKID)
    {
        bool retval = false;
        objReturnOrder = new tblReturnOrder();
        if (objReturnOrder.LoadByPrimaryKey(intPKID))
        {
            //string strCourierCompanyID = "";
            //string strDocketNo = "";
            //tblReturnOrder objTempReturnOrder = new tblReturnOrder();
            //objDataTable = objTempReturnOrder.LoadReturnOrderdWiseProductWeight(objReturnOrder.s_AppReturnOrderID);
            //if (objDataTable.Rows.Count > 0)
            //{
            //    bool IsCod = false;
            //    //if (objDataTable.Rows[0][tblOrder.ColumnNames.AppPaymentMode].ToString() != "")
            //    //{
            //    //    if (Convert.ToInt32(objDataTable.Rows[0][tblOrder.ColumnNames.AppPaymentMode].ToString()) == Convert.ToInt32(Enums.PaymentMode.COD))
            //    //    {
            //    //        IsCod = true;
            //    //    }
            //    //}
            //    DataTable objCourierCompany = objTempReturnOrder.GetCourierCompanyIdFromData(objDataTable.Rows[0]["appTotalWeight"].ToString(), objDataTable.Rows[0][tblOrder.ColumnNames.AppReceiverPIN].ToString(), IsCod);
            //    if (objCourierCompany.Rows.Count > 0)
            //    {
            //        objCommon = new clsCommon();
            //        strDocketNo = objCommon.GenerateDocketNo(8) + objCommon.GenerateDocketNo(2);
            //        objCommon = null;
            //        strCourierCompanyID = objCourierCompany.Rows[0][tblCourierCompany.ColumnNames.AppCourierCompanyID].ToString();
            //        //  objSubOrder.SetSellerOrderMovetoConfirm(Convert.ToInt32(Enums.Enums_OrderStatus.Ordered), Convert.ToInt32(Enums.Enums_OrderStatus.Confirmed), arIDs[i].ToString(), GetCurrentDateTime().ToString(), objCourierCompany.Rows[0][tblCourierCompany.ColumnNames.AppCourierCompanyID].ToString(), strDocketNo, objCourierCompany.Rows[0][tblCourierRate.ColumnNames.AppRate].ToString(), objCourierCompany.Rows[0][tblCourierCompany.ColumnNames.AppCODRate].ToString());
            //    }
            //}
            //objTempReturnOrder = null;
            //objReturnOrder.s_AppCourierCompanyID = strCourierCompanyID;
            //objReturnOrder.s_AppDocketNo = strDocketNo;
            objReturnOrder.AppReturnStatus = Convert.ToInt32(Enums.Enum_ReturnStatus.Approved);
            objReturnOrder.Save();
        }
        objReturnOrder = null;
        retval = true;
        objReturnOrder = null;
        return retval;
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
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSelectRow");
                chk.ID = "chkSelectRow_" + strID;
                chk.Attributes.Add("OnClick", "javascript:SelectRow(this," + strID + ")");
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
                GridView dgvSubDetail = (GridView)e.Row.FindControl("dgvSubDetail");
                if (dgvSubDetail != null)
                {
                    objReturnOrder = new tblReturnOrder();
                    dgvSubDetail.DataSource = objReturnOrder.LoadReturOrderProductFromReturnOrder(strID);
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


    protected void ddlProductName_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDataGrid(true, false);

    }

    protected void btnRejected_Click(object sender, EventArgs e)
    {
        string[] arIDs = hdnSelectedIDs.Value.ToString().TrimEnd(',').Split(',');
        bool IsReject = false;
        for (int i = 0; i <= arIDs.Length - 1; i++)
        {
            if (!string.IsNullOrEmpty(arIDs.GetValue(i).ToString()))
            {
                objReturnOrder = new tblReturnOrder();
                if (objReturnOrder.LoadByPrimaryKey(Convert.ToInt32(arIDs.GetValue(i))))
                {
                    if (objReturnOrder.RowCount > 0)
                    {
                        tblSubOrder objSubOrder = new tblSubOrder();
                        objSubOrder.SetOrderstatusWhenReturnReject(Convert.ToInt32(Enums.Enums_OrderStatus.Returned), objReturnOrder.AppPreviousSubOrderStatus, objReturnOrder.s_AppReturnOrderID, GetCurrentDateTime().ToString());
                        objSubOrder = null;
                        objReturnOrder.AppReturnStatus = Convert.ToInt32(Enums.Enum_ReturnStatus.Reject);
                        objReturnOrder.Save();
                        //if (arIDs[i].ToString() != "")
                        //{
                        //    SendMail(Convert.ToInt32(Enums.Enum_ReturnStatus.Reject), arIDs[i].ToString());
                        //}
                        IsReject = true;
                    }
                }
                objReturnOrder = null;
            }
        }

        if (IsReject)
        {
            LoadDataGrid(false, false);
            DInfo.ShowMessage("Return Order has been Reject successfully", Enums.MessageType.Successfull);
        }
        hdnSelectedIDs.Value = "";
    }
}