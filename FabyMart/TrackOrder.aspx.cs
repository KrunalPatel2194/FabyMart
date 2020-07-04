using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;

public partial class MyOrderList : PageBase_Client
{
    public PageBase objPageBase = new PageBase();
    tblSubOrder objSubOrder;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if ((Session[appFunctions.Session.ClientUserID.ToString()] != null))
            {
                if (string.IsNullOrEmpty(Session[appFunctions.Session.ClientUserName.ToString()].ToString()) | Session[appFunctions.Session.ClientUserID.ToString()].ToString() == "0")
                {
                    Response.Redirect(GetAlias("Login.aspx") + "TrackOrder");
                }
            }
            else
            {
                Response.Redirect(GetAlias("Login.aspx") + "TrackOrder");
            }
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

            objCommon.FillDropDownList(ddlCourierCompany, "tblCourierCompany ", tblCourierCompany.ColumnNames.AppCourierCompany, tblCourierCompany.ColumnNames.AppCourierCompanyID, "--Select Courier Company--", tblCourierCompany.ColumnNames.AppDisplayOrder, appFunctions.Enum_SortOrderBy.Asc, tblCourierCompany.ColumnNames.AppIsActive + "=1");
            ddlCourierCompany.Items.Add(new ListItem("Other", "-1"));
            objCommon = null;
            SetUpPageContent(ref metaDescription, ref metaKeywords);

        }
    }

    private void SetRegulerExpression()
    {
        revOrderNo.ValidationExpression = RXNumericRegularExpression;
        revOrderNo.ErrorMessage = "Invalid Order No. (" + RXNumericRegularExpression + ")";
       
        REVMobile1.ValidationExpression = RXPhoneRegularExpression;
        REVMobile1.ErrorMessage = "Invalid Mobile Number 1 (" + RXPhoneRegularExpressionMsg + ")";
        REVMobile2.ValidationExpression = RXPhoneRegularExpression;
        REVMobile2.ErrorMessage = "Invalid Mobile Number 2 (" + RXPhoneRegularExpressionMsg + ")";
        REVPickupPinCode.ValidationExpression = RXPinRegularExpression;
        REVPickupPinCode.ErrorMessage = "Invalid Pincode (" + RXPinRegularExpressionMsg + ")";
    }

    public void LoadMyorderList()
    {
        tblOrder objOrder = new tblOrder();
        objDataTable = objOrder.LoadMyOrderList(Session[appFunctions.Session.ClientUserID.ToString()].ToString(), txtOrderNo.Text);
        objOrder = null;

        DataListMyOrder.DataSource = null;
        DataListMyOrder.DataBind();
        if (objDataTable.Rows.Count > 0)
        {
            //   lblCount.Text = objDataTable.Rows.Count.ToString();
            DataListMyOrder.DataSource = objDataTable;
            DataListMyOrder.DataBind();
        }
        else
        {
            DInfo.ShowMessage("Please Enter Correct Number.", Enums.MessageType.Error);
            // lblCount.Text = "0";
        }
    }

    protected void dgvCart_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument.ToString() != "")
        {
            if (e.CommandName == "Cancel")
            {
                objSubOrder = new tblSubOrder();
                objSubOrder.SetOrderStatus(Convert.ToInt32(Enums.Enums_OrderStatus.CancelledByCustomer), e.CommandArgument.ToString(), GetCurrentDateTime().ToString());
                objSubOrder.SetProductQty(e.CommandArgument.ToString());
                objSubOrder = null;
                LoadMyorderList();
            }
        }
    }



    protected void dgvCart_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataControlRowType itemType = e.Row.RowType;

        switch (itemType)
        {
            case DataControlRowType.DataRow:

                break;
        }
    }

    protected void dgvCart_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }

    protected void DataListMyOrder_RowDataBound(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string strOrderId = DataListMyOrder.DataKeys[e.Item.ItemIndex].ToString();
            if (strOrderId != "")
            {
                DataList DataListSubOrder = (DataList)e.Item.FindControl("DataListSubOrder");
                if (DataListSubOrder != null)
                {
                    objSubOrder = new tblSubOrder();
                    DataListSubOrder.DataSource = objSubOrder.GetCustomerSubOrder(Session[appFunctions.Session.ClientUserID.ToString()].ToString(), strOrderId);
                    DataListSubOrder.DataBind();
                    objSubOrder = null;
                }
            }
        }
    }

    protected void DataListSubOrder_RowDataBound(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataList DataListSubOrder = (DataList)sender;
            string strSubOrderId = DataListSubOrder.DataKeys[e.Item.ItemIndex].ToString();
            //CheckBox chk = (CheckBox)e.Item.FindControl("chkSelectRow");
            System.Web.UI.HtmlControls.HtmlInputCheckBox chk = (System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkSelectRow");
            if (chk != null)
            {
                chk.ID = "chkSelectRow_" + strSubOrderId;
                chk.Attributes.Add("OnClick", "javascript:SelectRow(this," + strSubOrderId + ")");
            }
            HiddenField hdnStatus = (HiddenField)e.Item.FindControl("hdnStatus");
            HiddenField hdnOrderId = (HiddenField)e.Item.FindControl("hdnOrderId");
            HiddenField hdnReturnStatus = (HiddenField)e.Item.FindControl("hdnReturnStatus");
            HiddenField hdnDocketNo = (HiddenField)e.Item.FindControl("hdnDocketNo");
            HiddenField hdnTotaldiscount = (HiddenField)e.Item.FindControl("hdnTotaldiscount");
            System.Web.UI.HtmlControls.HtmlGenericControl divDiscount = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("divDiscount");
            if (hdnTotaldiscount != null)
            {
                if (hdnTotaldiscount.Value != "")
                {
                    if (Convert.ToDecimal(hdnTotaldiscount.Value) < 1)
                    {
                        divDiscount.Visible = false;
                    }
                    else
                    {
                        divDiscount.Visible = true;
                    }
                }
            }
            if (hdnStatus != null)
            {
                int statusID = Convert.ToInt32(hdnStatus.Value);
                LinkButton lnkbtnCancel = (LinkButton)e.Item.FindControl("lnkbtnCancel");
                LinkButton lnkbtnReturn = (LinkButton)e.Item.FindControl("lnkbtnReturn");
                //          lnkbtnCancel.CssClass = " btn btn-default danger ";
                //         lnkbtnReturn.CssClass = " btn btn-default danger ";
                if (statusID == Convert.ToInt32(Enums.Enums_OrderStatus.Complete) || statusID == Convert.ToInt32(Enums.Enums_OrderStatus.Shipped))
                {
                    lnkbtnReturn.Style.Add("display", "none");
                    lnkbtnCancel.Style.Add("display", "none");
                    if (chk != null)
                    {
                        chk.Style.Add("display", "none");
                    }
                }
                else if (statusID == Convert.ToInt32(Enums.Enums_OrderStatus.Delivered))
                {
                    lnkbtnReturn.Style.Add("display", "block");
                    //   lnkbtnReturn.CssClass = " btn btn-default danger btnReturn " + hdnOrderId.Value;
                    lnkbtnCancel.Style.Add("display", "none");
                    if (chk != null)
                    {
                        chk.Attributes.Add("class", "Return " + hdnOrderId.Value);
                    }
                }
                else if (statusID == Convert.ToInt32(Enums.Enums_OrderStatus.Ordered) || statusID == Convert.ToInt32(Enums.Enums_OrderStatus.Confirmed) || statusID == Convert.ToInt32(Enums.Enums_OrderStatus.ReadyToShip))
                {
                    lnkbtnCancel.Style.Add("display", "block");
                    //       lnkbtnCancel.CssClass = " btn btn-default danger btnCancel " + hdnOrderId.Value;
                    lnkbtnReturn.Style.Add("display", "none");
                    if (chk != null)
                    {
                        chk.Attributes.Add("class", "Cancel " + hdnOrderId.Value);
                    }
                }
                else if (statusID == Convert.ToInt32(Enums.Enums_OrderStatus.Returned))
                {
                    lnkbtnCancel.Style.Add("display", "none");
                    lnkbtnReturn.Style.Add("display", "none");
                    if (chk != null)
                    {
                        chk.Style.Add("display", "none");
                    }
                }
                else
                {
                    lnkbtnCancel.Style.Add("display", "none");
                    lnkbtnReturn.Style.Add("display", "none");
                    if (chk != null)
                    {
                        chk.Style.Add("display", "none");
                    }
                }
                if (hdnReturnStatus.Value != "")
                {
                    if (Convert.ToInt32(hdnReturnStatus.Value) == Convert.ToInt32(Enums.Enum_ReturnStatus.Dispatched))
                    {
                        lnkbtnReturn.Style.Add("display", "none");

                    }
                }
                Label lblStatusName = (Label)e.Item.FindControl("lblStatusName");
                if (statusID == Convert.ToInt32(Enums.Enums_OrderStatus.Ordered))
                {
                    lblStatusName.Text = Enums.Enums_OrderStatus.Ordered.ToString();
                }
                else if (statusID == Convert.ToInt32(Enums.Enums_OrderStatus.Confirmed))
                {
                    lblStatusName.Text = Enums.Enums_OrderStatus.Confirmed.ToString();
                }
                else if (statusID == Convert.ToInt32(Enums.Enums_OrderStatus.CancelledByAdmin))
                {
                    lblStatusName.Text = "Cancelled By FabyMart";
                }
                else if (statusID == Convert.ToInt32(Enums.Enums_OrderStatus.ReadyToShip))
                {
                    lblStatusName.Text = "Ready To Ship";
                }
                else if (statusID == Convert.ToInt32(Enums.Enums_OrderStatus.Shipped))
                {
                    lblStatusName.Text = Enums.Enums_OrderStatus.Shipped.ToString();
                }
                else if (statusID == Convert.ToInt32(Enums.Enums_OrderStatus.Delivered))
                {
                    lblStatusName.Text = Enums.Enums_OrderStatus.Delivered.ToString();
                }
                else if (statusID == Convert.ToInt32(Enums.Enums_OrderStatus.PaymentFail))
                {
                    lblStatusName.Text = "Payment Fail";
                    lblStatusName.Style.Add("color","red");
                }
                else if (statusID == Convert.ToInt32(Enums.Enums_OrderStatus.Returned))
                {
                    lblStatusName.Text = Enums.Enums_OrderStatus.Returned.ToString();
                    if (hdnReturnStatus.Value != "")
                    {
                        if (Convert.ToInt32(hdnReturnStatus.Value) == Convert.ToInt32(Enums.Enum_ReturnStatus.Requested))
                        {
                            lblStatusName.Text = lblStatusName.Text + " Request";
                        }
                        else if (Convert.ToInt32(hdnReturnStatus.Value) == Convert.ToInt32(Enums.Enum_ReturnStatus.Approved) || Convert.ToInt32(hdnReturnStatus.Value) == Convert.ToInt32(Enums.Enum_ReturnStatus.Slip))
                        {
                            lblStatusName.Text = lblStatusName.Text + " Approved ";
                            LinkButton lnkReturnAddress = (LinkButton)e.Item.FindControl("lnkReturnAddress");
                            lnkReturnAddress.CssClass = "SlipLink";
                            lnkReturnAddress.Visible = true;

                            LinkButton lnkDispatch = (LinkButton)e.Item.FindControl("lnkDispatch");

                            if (hdnDocketNo.Value == "")
                            {
                                lnkDispatch.Visible = false;
                            }
                            else
                            {
                                lnkDispatch.Visible = true;
                                lnkReturnAddress.Visible = false;
                            }
                        }
                        else if (Convert.ToInt32(hdnReturnStatus.Value) == Convert.ToInt32(Enums.Enum_ReturnStatus.Dispatched))
                        {
                            lblStatusName.Text = lblStatusName.Text + " Dispatched";
                        }
                        else if (Convert.ToInt32(hdnReturnStatus.Value) == Convert.ToInt32(Enums.Enum_ReturnStatus.Complete))
                        {
                            lblStatusName.Text = lblStatusName.Text + " Complete";
                        }
                        else if (Convert.ToInt32(hdnReturnStatus.Value) == Convert.ToInt32(Enums.Enum_ReturnStatus.Requested))
                        {
                            lblStatusName.Text = lblStatusName.Text + " Requested";
                        }
                    }
                }
                else if (statusID == Convert.ToInt32(Enums.Enums_OrderStatus.CancelledByCustomer))
                {
                    lblStatusName.Text = "Cancelled";
                }
                else if (statusID == Convert.ToInt32(Enums.Enums_OrderStatus.Complete))
                {
                    lblStatusName.Text = Enums.Enums_OrderStatus.Complete.ToString();
                }
            }

        }
    }

    protected void DataListSubOrder_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandArgument.ToString() != "")
        {
            if (e.CommandName == "ReturnAddress")
            {
                hdnReturnOrderID.Value = e.CommandArgument.ToString();
                tblReturnOrder objReturnOrder = new tblReturnOrder();
                if (objReturnOrder.LoadByPrimaryKey(Convert.ToInt32(e.CommandArgument.ToString())))
                {
                    if (objReturnOrder.AppReturnStatus == Convert.ToInt32(Enums.Enum_ReturnStatus.Slip) && objReturnOrder.s_AppDocketNo != "")
                    {
                        if (objReturnOrder.AppReturnStatus == Convert.ToInt32(Enums.Enum_ReturnStatus.Approved))
                        {
                            objReturnOrder.AppReturnStatus = Convert.ToInt32(Enums.Enum_ReturnStatus.Slip);
                            objReturnOrder.Save();
                        }
                        objEncrypt = new clsEncryption();
                        hdnReturnOrderID.Value = GetAlias("Productslip.aspx") + objEncrypt.Encrypt(objReturnOrder.s_AppReturnOrderID, appFunctions.strKey);
                        objEncrypt = null;
                        ScriptManager.RegisterStartupScript(UpMain, UpMain.GetType(), "myFunction", "CallProductInvoice();", true);
                    }
                    else
                    {
                        if (objReturnOrder.s_AppDocketNo != "")
                        {
                            txtDocketNo.Text = objReturnOrder.s_AppDocketNo;
                            txtDocketNo.Enabled = false;
                        }
                        else
                        {
                            ddlCourierCompany.SelectedIndex = 0;
                            txtCourierContactNo.Text = "";
                            txtSiteName.Text = "";
                            txtDocketNo.Text = "";
                            txtDocketNo.Enabled = true;
                        }

                        Mpeslip.Show();
                    }
                    //  Mpeslip.Show();
                }
                objReturnOrder = null;
            }
            if (e.CommandName == "ReturnRequest")
            {
                tblSubOrder objTempSubOrder = new tblSubOrder();
                objDataTable = objTempSubOrder.GetProductInvoiceInfo(e.CommandArgument.ToString());
                if (objDataTable.Rows.Count > 0)
                {
                    hdnPrevStatus.Value = objDataTable.Rows[0][tblSubOrder.ColumnNames.AppSubOrderStatusID].ToString();
                    txtPickupName.Text = objDataTable.Rows[0][tblOrder.ColumnNames.AppReceiverName].ToString();
                    txtPickupAddress.Text = objDataTable.Rows[0][tblOrder.ColumnNames.AppReceiverAddress].ToString();
                    txPickupMobile1.Text = objDataTable.Rows[0][tblOrder.ColumnNames.AppReceiverContactNo1].ToString();
                    txtPickupMobile2.Text = objDataTable.Rows[0][tblOrder.ColumnNames.AppReceiverContactNo2].ToString();
                    txtPickupPIN.Text = objDataTable.Rows[0][tblOrder.ColumnNames.AppReceiverPIN].ToString();
                }
                objTempSubOrder = null;

                MpeReturnRequest.Show();
            }
            if (e.CommandName == "Dispatch")
            {
                tblReturnOrder objReturnOrder = new tblReturnOrder();
                if (objReturnOrder.LoadByPrimaryKey(Convert.ToInt32(e.CommandArgument.ToString())))
                {

                    objReturnOrder.AppReturnStatus = Convert.ToInt32(Enums.Enum_ReturnStatus.Dispatched);
                    objReturnOrder.Save();
                    objReturnOrder = null;
                }
                LoadMyorderList();
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (hdnSelectedSubOrderIDs.Value != "")
        {
            string strIds = hdnSelectedSubOrderIDs.Value.Trim().TrimEnd(',');
            tblSubOrder objSubOrder = new tblSubOrder();
            objSubOrder.SetSubOrderStatus(Convert.ToInt32(Enums.Enums_OrderStatus.CancelledByCustomer), strIds, GetCurrentDateTime().ToString());
            objSubOrder = null;
            hdnSelectedSubOrderIDs.Value = "";
            DInfo.ShowMessage("Product Order Cancelled.", Enums.MessageType.Successfull);
            LoadMyorderList();
        }
        else
        {
            DInfo.ShowMessage("Choose Valid Product For Cancel Try again.", Enums.MessageType.Error);
        }
    }

    protected void btnReturnProduct_Click(object sender, EventArgs e)
    {
        if (hdnSelectedSubOrderIDs.Value != "" && hdnOrderId.Value != "")
        {
            string strIds = hdnSelectedSubOrderIDs.Value.Trim().TrimEnd(',');
            if (strIds != "")
            {
                string[] arIDs = strIds.Split(',');
                if (arIDs.Length > 0)
                {
                    tblReturnOrder objReturnOrder = new tblReturnOrder();
                    objReturnOrder.AddNew();
                    objReturnOrder.s_AppOrderID = hdnOrderId.Value;
                    objReturnOrder.AppReason = ddlReason.SelectedValue;
                    objReturnOrder.AppNote = txtNote.Text;
                    objReturnOrder.s_AppRequestedDate = GetCurrentDateTime().ToString();
                    objReturnOrder.AppReturnStatus = Convert.ToInt32(Enums.Enum_ReturnStatus.Requested);
                    objReturnOrder.s_AppPreviousSubOrderStatus = hdnPrevStatus.Value;
                    objReturnOrder.s_AppPickupName = txtPickupName.Text;
                    objReturnOrder.s_AppPickupContactNo1 = txPickupMobile1.Text;
                    objReturnOrder.s_AppPickupContactNo2 = txtPickupMobile2.Text;
                    objReturnOrder.s_AppPickupAddress = txtPickupAddress.Text;
                    objReturnOrder.s_AppPickupPIN = txtPickupPIN.Text;
                    objReturnOrder.s_AppPreferedTime = txtPreferedTime.Text;
                    //objReturnOrder.s_AppDocketNo = txtDocketNo.Text;
                    objReturnOrder.Save();

                    for (int i = 0; i <= arIDs.Length - 1; i++)
                    {
                        if (!string.IsNullOrEmpty(arIDs.GetValue(i).ToString()))
                        {
                            tblReturnOrderDetail objReturnOrderDetail = new tblReturnOrderDetail();
                            objReturnOrderDetail.AddNew();
                            objReturnOrderDetail.s_AppSubOrderID = arIDs.GetValue(i).ToString();
                            objReturnOrderDetail.AppReturnOrderID = objReturnOrder.AppReturnOrderID;
                            objReturnOrderDetail.Save();
                            objReturnOrderDetail = null;
                        }
                    }
                    objReturnOrder = null;
                    tblSubOrder objSubOrder = new tblSubOrder();
                    objSubOrder.SetSubOrderStatus(Convert.ToInt32(Enums.Enums_OrderStatus.Returned), strIds, GetCurrentDateTime().ToString());
                    objSubOrder = null;
                    ddlReason.SelectedValue = "0";
                    txtNote.Text = "";
                    hdnSelectedSubOrderIDs.Value = "";
                    hdnOrderId.Value = "";
                    DInfo.ShowMessage("Return Product Request Submitted.", Enums.MessageType.Successfull);
                    LoadMyorderList();
                }
            }
        }
        else
        {
            DInfo.ShowMessage("Choose Valid Product For Return Try again.", Enums.MessageType.Error);
        }
    }

    protected void btnReturnGenrateSlip_Click(object sender, EventArgs e)
    {
        if (hdnReturnOrderID.Value != "")
        {
            tblReturnOrder objReturnOrder = new tblReturnOrder();
            if (objReturnOrder.LoadByPrimaryKey(Convert.ToInt32(hdnReturnOrderID.Value)))
            {
                objReturnOrder.s_AppDocketNo = txtDocketNo.Text;
                if (Convert.ToInt32(ddlCourierCompany.SelectedValue) != -1)
                {
                    objReturnOrder.s_AppCourierCompanyID = ddlCourierCompany.SelectedValue;
                }
                else
                {
                    objReturnOrder.AppCourierCompanyContactNo = txtCourierContactNo.Text;
                    objReturnOrder.AppCourierCompanyWebsite = txtSiteName.Text;
                }
                objReturnOrder.Save();
                objEncrypt = new clsEncryption();
                hdnReturnOrderID.Value = GetAlias("Productslip.aspx") + objEncrypt.Encrypt(objReturnOrder.s_AppReturnOrderID, appFunctions.strKey);
                objEncrypt = null;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CallProductInvoice()", true);
            }
            objReturnOrder = null;

        }
        LoadMyorderList();

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadMyorderList();
    }



 

 

}