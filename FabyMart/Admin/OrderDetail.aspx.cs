using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.IO;
using System.Drawing;


public partial class OrderDetail : PageBase_Admin
{
    tblOrder objOrder;
    int intPkId = 0;
    tblSubOrder objSuborder;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["SortOrder"] = appFunctions.Enum_SortOrderBy.Asc;
            ViewState["SortColumn"] = "";

            objCommon = new clsCommon();
            objCommon.FillDropDownList(ddlstatus, "tblOrderStatus", tblOrderStatus.ColumnNames.AppOrderStatus, tblOrderStatus.ColumnNames.AppOrderStatusID, "-- Select Status --", tblOrderStatus.ColumnNames.AppDisplayOrder, appFunctions.Enum_SortOrderBy.Asc);
            objCommon.FillRecordPerPage(ref ddlPerPage);
            objCommon = null;
            if ((Request.QueryString.Get("ID") != null))
            {
                objEncrypt = new clsEncryption();
                try
                {
                    hdnPKID.Value = objEncrypt.Decrypt(Request.QueryString.Get("ID"), appFunctions.strKey);
                }
                catch (Exception ex)
                {
                    // noIdFoundRedirect("Employee.aspx");
                }
                objEncrypt = null;
                SetValuesToControls();
            }
            LoadDataGrid(true, false);
        }
    }

    private bool SaveData()
    {

        objOrder = new tblOrder();

        if (objOrder.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value)))
        {
            if (ddlstatus.SelectedValue != objOrder.s_AppOrderStatusID)
            {
                objOrder.s_AppOrderStatusID = ddlstatus.SelectedValue;
                objOrder.s_AppOrderStatusChangeDate = FormatDateString(DateTime.Now.ToString(strInputDateFormat), strInputDateFormat, strOutputDateFormat);
                objOrder.Save();
            }
        }
        return true;
    }

    private void SetValuesToControls()
    {
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objOrder = new tblOrder();
            if (objOrder.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value)))
            {
                lblOrderNo.Text = objOrder.s_AppOrderNo;
                ddlstatus.SelectedValue = objOrder.s_AppOrderStatusID;
                lblOrderAmount.Text = objOrder.s_AppOrderAmount;
                lblReceiverName.Text = objOrder.s_AppReceiverName;
                lblReceiverContactNo1.Text = objOrder.s_AppReceiverContactNo1;
                lblReceiverContactNo2.Text = objOrder.s_AppReceiverContactNo2;
                lblRecevierEmail.Text = objOrder.s_AppRecevierEmail;
                lblReceiverAddress.Text = objOrder.s_AppReceiverAddress;
                lblPreferedTime.Text = objOrder.s_AppPreferedTime;
                lblBillReceiverName.Text = objOrder.s_AppBillReceiverName;
                lblBillReceiverContactNo1.Text = objOrder.s_AppBillReceiverContactNo1;
                lblBillReceiverContactNo2.Text = objOrder.s_AppBillReceiverContactNo2;
                lblBillRecevierEmail.Text = objOrder.s_AppBillRecevierEmail;
                lblBillReceiverAddress.Text = objOrder.s_AppBillReceiverAddress;

                tblCustomer objCust = new tblCustomer();

                if (objCust.LoadByPrimaryKey(objOrder.AppCustomerID))
                {
                    lblCustomer.Text = objCust.s_AppFirstName + " " + objCust.s_AppLastName;
                    lblCustomerMobile.Text = objCust.s_AppMobile;
                    lblCustomerEmail.Text = objCust.s_AppEmailID;
                }
                objCust = null;

            }
            objOrder = null;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Order.aspx", true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("Order has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("Order has been updated successfully", Enums.MessageType.Successfull);
            }
            hdnPKID.Value = intPkId.ToString();
            SetValuesToControls();
        }
    }

    protected void lnkSaveAndClose_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "Order has been added successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            else
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "Order has been updated successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            Response.Redirect("Order.aspx");
        }
    }

    private void LoadDataGrid(bool IsResetPageIndex, bool IsSort, string strFieldName = "", string strFieldValue = "")
    {
        objSuborder = new tblSubOrder();

        objDataTable = objSuborder.LoadGridData(ddlFields.SelectedValue, txtSearch.Text.Trim(), hdnPKID.Value);

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
            DInfoSubOrder.ShowMessage("No data found", Enums.MessageType.Information);
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

        objSuborder = null;
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
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSelectRow");
                chk.ID = "chkSelectRow_" + strID;
                break;
        }
    }

    protected void dgvGridView_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        DataControlRowType itemType = e.Row.RowType;

        switch (itemType)
        {
            case DataControlRowType.DataRow:
                string strID = dgvGridView.DataKeys[e.Row.RowIndex].Values[0].ToString();
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSelectRow");

                if ((chk != null))
                {
                    chk.Attributes.Add("OnClick", "javascript:SelectRow(this," + strID + ")");
                }

                DropDownList ddlstatus = (DropDownList)e.Row.FindControl("ddlSubOrderStatus");
                objCommon = new clsCommon();
                objCommon.FillDropDownListWithOutDefaultValue(ddlstatus, "tblOrderStatus", tblOrderStatus.ColumnNames.AppOrderStatus, tblOrderStatus.ColumnNames.AppOrderStatusID, tblOrderStatus.ColumnNames.AppDisplayOrder, appFunctions.Enum_SortOrderBy.Asc);
                objCommon = null;
                ddlstatus.SelectedValue = dgvGridView.DataKeys[e.Row.RowIndex].Values[1].ToString();
                if ((ddlstatus != null))
                {
                    ddlstatus.Attributes.Add("onchange", "javascript:ChkOrder(" + strID + ")");
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

    protected void btnDelete_Click(object sender, System.EventArgs e)
    {
        string[] arIDs = hdnSelectedIDs.Value.ToString().TrimEnd(',').Split(',');
        bool IsDelete = false;

        for (int i = 0; i <= arIDs.Length - 1; i++)
        {
            if (!string.IsNullOrEmpty(arIDs.GetValue(i).ToString()))
            {
                if (Delete(Convert.ToInt32(arIDs.GetValue(i))))
                {
                    IsDelete = true;
                }
            }
        }

        if (IsDelete)
        {
            LoadDataGrid(false, false);
        }

        DInfo.ShowMessage("Sub Order has been deleted successfully", Enums.MessageType.Successfull);
        hdnSelectedIDs.Value = "";
    }

    private bool Delete(int intPKID)
    {
        bool retval = false;
        objSuborder = new tblSubOrder();

        var _with1 = objSuborder;

        if (_with1.LoadByPrimaryKey(intPKID))
        {
            _with1.MarkAsDeleted();
            _with1.Save();
        }

        retval = true;
        objSuborder = null;
        return retval;
    }

    protected void dgvGridView_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
        {
            objCommon = new clsCommon();

            if (e.CommandName == "Up")
            {

            }

        }
    }

    protected void ddlSubOrderStatus_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if(hdnSubOrderId.Value !="")
        {
            DropDownList ddl = (DropDownList)sender;
            objSuborder = new tblSubOrder();
            if (objSuborder.LoadByPrimaryKey(Convert.ToInt32(hdnSubOrderId.Value)))
            {
                if (ddl.SelectedValue != objSuborder.s_AppSubOrderStatusID)
                {
                    objSuborder.s_AppSubOrderStatusID  = ddl.SelectedValue;
                    objSuborder.s_AppSubOrderChangeDate  = FormatDateString(DateTime.Now.ToString(strInputDateFormat), strInputDateFormat, strOutputDateFormat);
                    objSuborder.Save();
                }
            }
            objSuborder = null;
            hdnSubOrderId.Value = "";
            LoadDataGrid(false, false);
        }
    }

}