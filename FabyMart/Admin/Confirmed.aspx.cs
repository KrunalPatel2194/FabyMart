using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using ShippingReference;
using System.Configuration;
using System.Web.Services;
public partial class Confirmed : PageBase_Admin
{
    tblSubOrder objSubOrder;
    tblOrder objOrder;
    private List<PickupItemDetail> _PickupItems = new List<PickupItemDetail>();
    private List<Shipment> _Shipments = null;

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
            objCommon.FillDropDownList(ddlCourierCompany, "tblCourierCompany", tblCourierCompany.ColumnNames.AppCourierCompany, tblCourierCompany.ColumnNames.AppCourierCompanyID, "--Select Courier Company--", tblCourierCompany.ColumnNames.AppDisplayOrder, appFunctions.Enum_SortOrderBy.Asc, tblCourierCompany.ColumnNames.AppIsActive + "=1");

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
        liConfirmed.Attributes.Add("class", "active");
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
        objDataTable = objOrder.GetOrderList(ddlFields.SelectedValue, txtSearch.Text.Trim(), Convert.ToInt32(Enums.Enums_OrderStatus.Confirmed).ToString());
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

    protected void GenerateDocketNum_Click(object sender, System.EventArgs e)
    {
        String strID = "";
        if (hdnSelectedIDs.Value != "")
        {
            string[] arIDs = hdnSelectedIDs.Value.ToString().TrimEnd(',').Split(',');
            if (arIDs.Length > 0)
            {
                for (int i = 0; i < arIDs.Length; i++)
                {
                    if (arIDs[i].ToString() != "")
                    {

                        objOrder = new tblOrder();
                        if (objOrder.LoadByPrimaryKey(Convert.ToInt32(arIDs[i].ToString())))
                        {
                            if (ddlCourierCompany.SelectedValue  == Convert.ToInt32(Enums.Enum_CourierCompany.AraMax).ToString())
                            {
                                decimal decWeight = 1; ;
                                int iQty = 1;
                                tblOrder objTempOrder = new tblOrder();
                                objDataTable = objTempOrder.GetWeightandQty(objOrder.s_AppOrderID, Convert.ToInt32(Enums.Enums_OrderStatus.Confirmed).ToString());
                                objTempOrder = null;
                                if (objDataTable.Rows.Count > 0)
                                {
                                    decWeight = Convert.ToDecimal(objDataTable.Rows[0]["appWeight"].ToString());
                                    iQty = Convert.ToInt32(objDataTable.Rows[0]["appQty"].ToString());
                                }
                                string strAccountCountryCode = "IN";
                                string strAccountEntity = "BOM";
                                string strAccountNumber = "36669982";
                                string strAccountPin = "443543";
                                string strUserName = "testingapi@aramex.com";
                                string strPassword = "R123456789$r";
                                //if (Convert.ToBoolean(ConfigurationManager.AppSettings["IsPayOnline"]) == true)
                                //{
                                //    strAccountCountryCode = appFunctions.AraMex_AccountCountryCode;
                                //    strAccountEntity = appFunctions.AraMex_Entity;
                                //    strAccountNumber = appFunctions.AraMex_AccountNo;
                                //    strAccountPin = appFunctions.AraMex_PinNumber;
                                //    strUserName = appFunctions.AraMex_UserName;
                                //    strPassword = appFunctions.AraMex_Password;
                                //}

                                Service_1_0Client _Client = new Service_1_0Client();

                                ShipmentCreationRequest _Request = new ShipmentCreationRequest();
                                _Request.ClientInfo = new ClientInfo();
                                _Request.ClientInfo.AccountCountryCode = strAccountCountryCode;
                                _Request.ClientInfo.AccountEntity = strAccountEntity;
                                _Request.ClientInfo.AccountNumber = strAccountNumber;
                                _Request.ClientInfo.AccountPin = strAccountPin;
                                _Request.ClientInfo.UserName = strUserName;
                                _Request.ClientInfo.Password = strPassword;
                                _Request.ClientInfo.Version = "v1.0";
                                //_Request.Transaction = new Transaction();
                                //_Request.Transaction.Reference1 = objOrder.s_AppOrderID ;
                                //_Request.Transaction.Reference2 = objOrder.s_AppOrderNo;
                                //_Request.Transaction.Reference3 = "";
                                //_Request.Transaction.Reference4 = "";
                                //_Request.Transaction.Reference5 = "";

                                ////Shipper // Seller Detail
                                //Shipment _Shipment = new Shipment();
                                //_Shipment.Shipper = new Party();
                                //_Shipment.Shipper.Reference1 = appFunctions.AraMex_CustomerName;
                                //_Shipment.Shipper.Reference2 = "";
                                //_Shipment.Shipper.AccountNumber = _Request.ClientInfo.AccountNumber;
                                //_Shipment.Shipper.Contact = new Contact();
                                //_Shipment.Shipper.Contact.Department = appFunctions.AraMex_Department;
                                //_Shipment.Shipper.Contact.PersonName = appFunctions.AraMex_PersonName;
                                //_Shipment.Shipper.Contact.Title = "";
                                //_Shipment.Shipper.Contact.CompanyName = appFunctions.AraMex_CustomerName;
                                //_Shipment.Shipper.Contact.PhoneNumber1 = appFunctions.AraMex_PhoneNumber;
                                //_Shipment.Shipper.Contact.CellPhone = appFunctions.AraMex_PhoneNumber;
                                //_Shipment.Shipper.Contact.EmailAddress = appFunctions.AraMex_EmailAddress;

                                //_Shipment.Shipper.PartyAddress = new Address();
                                //_Shipment.Shipper.PartyAddress.Line1 = "ship1 address";
                                //_Shipment.Shipper.PartyAddress.Line2 = "ship2 address";
                                //_Shipment.Shipper.PartyAddress.Line3 = "";
                                //_Shipment.Shipper.PartyAddress.City = "";
                                //_Shipment.Shipper.PartyAddress.StateOrProvinceCode = "";
                                //_Shipment.Shipper.PartyAddress.PostCode = "395004";
                                //_Shipment.Shipper.PartyAddress.CountryCode = "IN";

                                ////Consignee //Customer
                                //_Shipment.Consignee = new Party();
                                //_Shipment.Consignee.Reference1 = objOrder.s_AppReceiverName ;
                                //_Shipment.Consignee.Reference2 = objOrder.s_AppReceiverContactNo1;
                                //_Shipment.Consignee.AccountNumber = "";
                                //_Shipment.Consignee.Contact = new Contact();
                                //_Shipment.Consignee.Contact.Department = "";
                                //_Shipment.Consignee.Contact.PersonName = objOrder.s_AppReceiverName;
                                //_Shipment.Consignee.Contact.Title = "";
                                //_Shipment.Consignee.Contact.CompanyName = "aramex";
                                //_Shipment.Consignee.Contact.PhoneNumber1 = objOrder.s_AppReceiverContactNo1;
                                //_Shipment.Consignee.Contact.CellPhone = objOrder.s_AppReceiverContactNo1;
                                //_Shipment.Consignee.Contact.EmailAddress = objOrder.s_AppRecevierEmail ;
                                //_Shipment.Consignee.PartyAddress = new Address();
                                //_Shipment.Consignee.PartyAddress.Line1 = objOrder.s_AppReceiverAddress;
                                //_Shipment.Consignee.PartyAddress.Line2 = "";
                                //_Shipment.Consignee.PartyAddress.Line3 = "";
                                //_Shipment.Consignee.PartyAddress.City = "";
                                //_Shipment.Consignee.PartyAddress.StateOrProvinceCode = "";
                                //_Shipment.Consignee.PartyAddress.PostCode = objOrder.s_AppReceiverPIN;
                                //_Shipment.Consignee.PartyAddress.CountryCode = "IN";
                                //_Shipment.ShippingDateTime = GetDateTime().AddDays(1);


                                //ShipmentDetails _shipment_details = new ShipmentDetails();
                                //_shipment_details.NumberOfPieces = iQty ;
                                //_shipment_details.ActualWeight = new Weight();
                                //_shipment_details.ActualWeight.Value = Convert.ToDouble(decWeight);
                                //_shipment_details.ActualWeight.Unit = "kg";
                                //_shipment_details.ProductGroup = "DOM";
                                //_shipment_details.ProductType = "ONP";
                                //if (objOrder.AppPaymentMode == Convert.ToInt32(Enums.PaymentMode.COD))
                                //{
                                //    _shipment_details.PaymentType = "C";
                                //}
                                //else
                                //{
                                //    _shipment_details.PaymentType = "P";
                                //}

                                //_shipment_details.DescriptionOfGoods = "Clothes";
                                //_shipment_details.GoodsOriginCountry = "IN";
                                //_shipment_details.CustomsValueAmount = new Money() { CurrencyCode = "INR", Value = Convert.ToDouble(objOrder.AppOrderAmount )};
                                //_shipment_details.Dimensions = new Dimensions();
                                //_shipment_details.Dimensions.Length = 0;
                                //_shipment_details.Dimensions.Width = 0;
                                //_shipment_details.Dimensions.Height = 0;
                                //_shipment_details.Dimensions.Unit = "cm";

                                //_Shipment.Details = _shipment_details;
                                //_shipment_details.PaymentOptions = "";

                                //_Request.LabelInfo = new LabelInfo();
                                //_Request.LabelInfo.ReportID = 9201;
                                //_Request.LabelInfo.ReportType = "URL";
                                ////_Request.LabelInfo.ReportType = "RPT";

                                //_Request.Shipments = new Shipment[1] { _Shipment };
                                //ShipmentCreationResponse _Response = null;

                                //_Client.Open();
                                //_Response = _Client.CreateShipments(_Request);
                                //_Client.Close();

                                //strID = _Response.Shipments[0].ID;

                                _Request.Transaction = new Transaction();
                                _Request.Transaction.Reference1 = "";
                                _Request.Transaction.Reference2 = "";
                                _Request.Transaction.Reference3 = "";
                                _Request.Transaction.Reference4 = "";
                                _Request.Transaction.Reference5 = "";

                                Shipment _Shipment = new Shipment();

                                //Shipper

                                _Shipment.Shipper = new Party();

                                _Shipment.Shipper.Reference1 = "ref1";

                                _Shipment.Shipper.Reference2 = "ref2";

                                _Shipment.Shipper.AccountNumber = _Request.ClientInfo.AccountNumber;


                                _Shipment.Shipper.Contact = new Contact();

                                _Shipment.Shipper.Contact.Department = "";

                                _Shipment.Shipper.Contact.PersonName = "sadiq";

                                _Shipment.Shipper.Contact.Title = "";

                                _Shipment.Shipper.Contact.CompanyName = "aramex";

                                _Shipment.Shipper.Contact.PhoneNumber1 = "123";

                                _Shipment.Shipper.Contact.CellPhone = "12345";

                                _Shipment.Shipper.Contact.EmailAddress = "test@test.com";


                                _Shipment.Shipper.PartyAddress = new Address();

                                _Shipment.Shipper.PartyAddress.Line1 = "ship1 address";

                                _Shipment.Shipper.PartyAddress.Line2 = "ship2 address";

                                _Shipment.Shipper.PartyAddress.Line3 = "";

                                _Shipment.Shipper.PartyAddress.City = "";

                                _Shipment.Shipper.PartyAddress.StateOrProvinceCode = "";

                                _Shipment.Shipper.PartyAddress.PostCode = "400612";

                                _Shipment.Shipper.PartyAddress.CountryCode = "IN";

                                //Consignee

                                _Shipment.Consignee = new Party();

                                _Shipment.Consignee.Reference1 = "ref1";

                                _Shipment.Consignee.Reference2 = "";

                                _Shipment.Consignee.AccountNumber = "";


                                _Shipment.Consignee.Contact = new Contact();

                                _Shipment.Consignee.Contact.Department = "";

                                _Shipment.Consignee.Contact.PersonName = "vipin";

                                _Shipment.Consignee.Contact.Title = "";

                                _Shipment.Consignee.Contact.CompanyName = "aramex";

                                _Shipment.Consignee.Contact.PhoneNumber1 = "123";

                                _Shipment.Consignee.Contact.CellPhone = "12345";

                                _Shipment.Consignee.Contact.EmailAddress = "test@test.com";


                                _Shipment.Consignee.PartyAddress = new Address();

                                _Shipment.Consignee.PartyAddress.Line1 = "ship1 address";

                                _Shipment.Consignee.PartyAddress.Line2 = "ship2 address";

                                _Shipment.Consignee.PartyAddress.Line3 = "";

                                _Shipment.Consignee.PartyAddress.City = "";

                                _Shipment.Consignee.PartyAddress.StateOrProvinceCode = "";

                                _Shipment.Consignee.PartyAddress.PostCode = "121010";

                                _Shipment.Consignee.PartyAddress.CountryCode = "IN";


                                _Shipment.ShippingDateTime = new DateTime(2015, 09, 2);

                                ShipmentDetails _shipment_details = new ShipmentDetails();

                                _shipment_details.NumberOfPieces = iQty;

                                _shipment_details.ActualWeight = new Weight();

                                _shipment_details.ActualWeight.Value = Convert.ToDouble(decWeight);

                                _shipment_details.ActualWeight.Unit = "kg";

                                _shipment_details.ProductGroup = "DOM";

                                _shipment_details.ProductType = "ONP";

                                _shipment_details.PaymentType = "P";

                                _shipment_details.DescriptionOfGoods = "Clothes";

                                _shipment_details.GoodsOriginCountry = "IN";

                                _shipment_details.CustomsValueAmount = new Money() { CurrencyCode = "INR", Value = Convert.ToDouble(objOrder.AppOrderAmount) };

                                _shipment_details.Dimensions = new Dimensions();

                                _shipment_details.Dimensions.Length = 0;

                                _shipment_details.Dimensions.Width = 0;

                                _shipment_details.Dimensions.Height = 0;

                                _shipment_details.Dimensions.Unit = "cm";


                                _Shipment.Details = _shipment_details;

                                _shipment_details.PaymentOptions = "";


                                _Request.LabelInfo = new LabelInfo();
                                _Request.LabelInfo.ReportID = 9201;
                                _Request.LabelInfo.ReportType = "URL";
                                //_Request.LabelInfo.ReportType = "RPT";

                                _Request.Shipments = new Shipment[1] { _Shipment };
                                ShipmentCreationResponse _Response = null;


                                _Client.Open();
                                _Response = _Client.CreateShipments(_Request);
                                _Client.Close();

                                strID = _Response.Shipments[0].ID;
                                string strUrl = _Response.Shipments[0].ShipmentLabel.LabelURL;
                                tblSubOrder objSubOrder = new tblSubOrder();
                                objSubOrder.SetCourierInof(Convert.ToInt32(Enums.Enums_OrderStatus.Confirmed), objOrder.s_AppOrderID, ddlCourierCompany.SelectedValue, strID,strUrl, true);
                                objSubOrder = null;
                            }
                        }
                        else
                        {
                            tblSubOrder objSubOrder = new tblSubOrder();
                            objSubOrder.SetCourierCompanyIDOnOrderId(Convert.ToInt32(Enums.Enums_OrderStatus.Confirmed), objOrder.s_AppOrderID, ddlCourierCompany.SelectedValue, true);
                            objSubOrder = null;
                        }
                        objOrder = null;

                    }
                }
            }
            /* DInfo.ShowMessage("Order status has been Change.", Enums.MessageType.Successfull);
               hdnSelectedIDs.Value = "";
               UcOrderStratus.SetOrderCount();
               LoadDataGrid(true, false);*/

        }

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
                string strPaymentMode = dgvGridView.DataKeys[e.Row.RowIndex].Values[4].ToString();

                if (strOrderID != "")
                {
                    GridView dgvGrid = (GridView)e.Row.FindControl("dgvSubDetail");
                    if (dgvGrid != null)
                    {
                        objSubOrder = new tblSubOrder();
                        dgvGrid.DataSource = objSubOrder.GetSubOrderDetailList(strOrderID, Convert.ToInt32(Enums.Enums_OrderStatus.Confirmed).ToString());
                        dgvGrid.DataBind();
                        objSubOrder = null;
                    }
                }

                Label lbltype = (Label)e.Row.FindControl("type");
                if (dgvGridView.DataKeys[e.Row.RowIndex].Values[3].ToString() != "")
                {
                    if (Convert.ToBoolean(dgvGridView.DataKeys[e.Row.RowIndex].Values[3].ToString()))
                    {
                        lbltype.Text = "Invoice Generated";
                        lbltype.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lbltype.Text = "Invoice Not Generated";
                        lbltype.ForeColor = System.Drawing.Color.Brown;
                    }
                }
                else
                {
                    lbltype.Text = "Invoice Not Generated";
                    lbltype.ForeColor = System.Drawing.Color.Brown;
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

                // System.Web.UI.HtmlControls.HtmlGenericControl divCourierCompany = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Row.FindControl("divCourierCompany");
                // divCourierCompany.Visible = false;
                //TextBox txtDocketNo = (TextBox)e.Row.FindControl("txtDocketNo");
                //LinkButton lnkAddDocketNo = (LinkButton)e.Row.FindControl("lnkAddDocketNo");
                //if (strDocketNo != "")
                //{
                //    objSubOrder = new tblSubOrder();
                //    txtDocketNo.Text = strDocketNo;
                //    txtDocketNo.Style.Add("display", "block");
                //    txtDocketNo.Enabled = false;
                //    lnkAddDocketNo.Visible = false;
                //    objSubOrder = null;
                //}
                //else if (strCourierCompanyId != "" && strDocketNo == "")
                //{
                //    txtDocketNo.Style.Add("display", "block");
                //    lnkAddDocketNo.Visible = true;
                //}
                //else if (strCourierCompanyId == "" && strDocketNo == "")
                //{
                //    txtDocketNo.Style.Add("display", "none");
                //    lnkAddDocketNo.Visible = false;
                //    divCourierCompany.Visible = true;
                //    DropDownList ddlCourierCompany = (DropDownList)e.Row.FindControl("ddlCourierCompany");
                //    objCommon = new clsCommon();
                //    objCommon.FillDropDownListWithOutDefaultValue(ddlCourierCompany, "tblCourierCompany", tblCourierCompany.ColumnNames.AppCourierCompany, tblCourierCompany.ColumnNames.AppCourierCompanyID, tblCourierCompany.ColumnNames.AppDisplayOrder, appFunctions.Enum_SortOrderBy.Asc, tblCourierCompany.ColumnNames.AppIsActive + "=1");
                //    objCommon = null;
                //}
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

    protected void btnReadyToShip_Click(object sender, System.EventArgs e)
    {
        if (hdnSelectedIDs.Value != "")
        {
            string[] arIDs = hdnSelectedIDs.Value.ToString().TrimEnd(',').Split(',');
            if (arIDs.Length > 0)
            {
                objSubOrder = new tblSubOrder();
                objSubOrder.SetOrderStatus(Convert.ToInt32(Enums.Enums_OrderStatus.ReadyToShip), hdnSelectedIDs.Value.ToString().TrimEnd(','), GetCurrentDateTime().ToString(), Convert.ToInt32(Enums.Enums_OrderStatus.Confirmed).ToString());
                objSubOrder = null;
                //for (int i = 0; i < arIDs.Length; i++)
                //{
                //    if (arIDs[i].ToString() != "")
                //    {
                //        SendMail(Convert.ToInt32(Enums.Enums_OrderStatus.ReadyToShip), arIDs[i].ToString());
                //    }
                //}
                DInfo.ShowMessage("Order status has been change Confirmed To Ready To Ship status", Enums.MessageType.Successfull);
                hdnSelectedIDs.Value = "";
                UcOrderStratus.SetOrderCount();
                LoadDataGrid(true, false);
            }
            else
            {
                DInfo.ShowMessage("Order status has not been changed Confirmed To Ready To Ship status", Enums.MessageType.Error);
            }
        }
        else
        {
            DInfo.ShowMessage("Select any one Order for change status ", Enums.MessageType.Error);
        }
    }

    protected void dgvGridView_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        //if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
        //{
        //    if (e.CommandName == "addDocketNo")
        //    {
        //        GridViewRow gvRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
        //        Int32 rowind = gvRow.RowIndex;
        //        TextBox txtDocketNo = (TextBox)gvRow.FindControl("txtDocketNo");
        //        if (txtDocketNo != null && txtDocketNo.Text != "")
        //        {
        //            if (dgvGridView.DataKeys[gvRow.RowIndex].Values[1].ToString() != "" && dgvGridView.DataKeys[gvRow.RowIndex].Values[3].ToString() != "")
        //            {
        //                objSubOrder = new tblSubOrder();
        //                objSubOrder.SetDocketNumber(Convert.ToInt32(Enums.Enums_OrderStatus.Confirmed),dgvGridView.DataKeys[gvRow.RowIndex].Values[0].ToString(), dgvGridView.DataKeys[gvRow.RowIndex].Values[1].ToString(), txtDocketNo.Text,true );
        //                objSubOrder = null;
        //                LoadDataGrid(true, false);
        //            }
        //        }
        //        else
        //        {
        //            DInfo.ShowMessage("Enter Docket Number", Enums.MessageType.Error);
        //        }
        //    }
        //    if (e.CommandName == "SaveCourierCompany")
        //    {
        //        GridViewRow gvRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
        //        DropDownList ddlCourierCompany = (DropDownList)gvRow.FindControl("ddlCourierCompany");
        //        TextBox txtCourierCompanyDocket = (TextBox)gvRow.FindControl("txtCourierCompanyDocket");
        //        if (txtCourierCompanyDocket.Text != "")
        //        {
        //            objSubOrder = new tblSubOrder();
        //            objSubOrder.SetDocketNumberAndCourierCompanyIDOnOrderId(Convert.ToInt32(Enums.Enums_OrderStatus.Confirmed),e.CommandArgument.ToString(), ddlCourierCompany.SelectedValue, txtCourierCompanyDocket.Text, true);
        //            objSubOrder = null;
        //        }
        //        else
        //        {
        //            txtCourierCompanyDocket.Style.Add("border", "1px solid #D2322D");
        //        }
        //        objSubOrder = null;
        //        LoadDataGrid(true, false);
        //    }

        //}
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

    protected void dgvSubDetail_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
        {
            if (e.CommandName.ToString() == "CancelOrder")
            {
                objSubOrder = new tblSubOrder();
                objSubOrder.SetSubOrderStatus(Convert.ToInt32(Enums.Enums_OrderStatus.CancelledByAdmin), e.CommandArgument.ToString(), GetCurrentDateTime().ToString(), Convert.ToInt32(Enums.Enums_OrderStatus.Confirmed).ToString());
                objSubOrder = null;
                //SendMail(Convert.ToInt32(Enums.Enums_OrderStatus.CancelledByAdmin), e.CommandArgument.ToString());
                DInfo.ShowMessage("Order status has been  Cancel.", Enums.MessageType.Successfull);
                hdnSelectedIDs.Value = "";
                LoadDataGrid(true, false);
            }
        }
    }

    private void SendMail(int strStatus, string strOrderID)
    {
        string strEmail = "";
        try
        {
            tblOrder objOrder = new tblOrder();
            if (objOrder.LoadByPrimaryKey(Convert.ToInt32(strOrderID)))
            {
                if (objOrder.AppOrderStatusID == Convert.ToInt32(Enums.Enums_OrderStatus.ReadyToShip))
                {
                    objCommon = new clsCommon();
                    string Strbody = "";
                    string strSubject = "Fabymart.com order ";
                    Strbody = objCommon.readFile(Server.MapPath("~/Admin/EmailTemplates/OrderStatusMain.html"));

                    if (strStatus == Convert.ToInt32(Enums.Enums_OrderStatus.ReadyToShip))
                    {
                        Strbody = Strbody.Replace("`orderstatus`", "Ready To Ship");
                        strSubject += " Ready To Ship  - ";
                    }
                    else if (strStatus == Convert.ToInt32(Enums.Enums_OrderStatus.CancelledByAdmin))
                    {
                        Strbody = Strbody.Replace("`orderstatus`", "Cancelled By FabyMart");
                        strSubject += " Cancelled  - ";
                    }

                    strEmail = objOrder.AppRecevierEmail;
                    Strbody = Strbody.Replace("`uname`", objOrder.AppReceiverName);
                    Strbody = Strbody.Replace("`orderno`", objOrder.s_AppOrderNo);
                    strSubject += objOrder.s_AppOrderNo;


                    Strbody = Strbody.Replace("`shipmentdate`", "");
                    Strbody = Strbody.Replace("`link`", strServerURL);
                    Strbody = Strbody.Replace("`orderdate`", DateTime.Now.Date.ToString("dd-MM-yyyy"));

                    objCommon.SendConfirmationMail(strEmail, strSubject, Strbody, Enums.Enum_Confirmation_Mail_type.order);
                    //objCommon.SendMail(strEmail, strSubject, Strbody);
                    strEmail = "";
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

    [WebMethod]
    public static string[] SetCourierCompany(string strIds, string strCourierCompanyId)
    {
        string[] StrValue = new string[2];
        StrValue[0] = "true";
        if (strIds != "")
        {
            string[] arIDs = strIds.ToString().TrimEnd(',').Split(',');
            if (arIDs.Length > 0)
            {
                for (int i = 0; i < arIDs.Length; i++)
                {
                    if (arIDs[i].ToString() != "")
                    {

                        tblOrder objOrder = new tblOrder();
                        if (objOrder.LoadByPrimaryKey(Convert.ToInt32(arIDs[i].ToString())))
                        {
                            if (strCourierCompanyId == Convert.ToInt32(Enums.Enum_CourierCompany.AraMax).ToString())
                            {
                                decimal decWeight = 1; ;
                                int iQty = 1;
                                tblOrder objTempOrder = new tblOrder();
                                DataTable  objDataTable = objTempOrder.GetWeightandQty(objOrder.s_AppOrderID, Convert.ToInt32(Enums.Enums_OrderStatus.Confirmed).ToString());
                                objTempOrder = null;
                                if (objDataTable.Rows.Count > 0)
                                {
                                    decWeight = Convert.ToDecimal(objDataTable.Rows[0]["appWeight"].ToString());
                                    iQty = Convert.ToInt32(objDataTable.Rows[0]["appQty"].ToString());
                                }
                                //string strAccountCountryCode = "IN";
                                //string strAccountEntity = "BOM";
                                //string strAccountNumber = "36669982";
                                //string strAccountPin = "443543";
                                //string strUserName = "testingapi@aramex.com";
                                //string strPassword = "R123456789$r";
                                //if (Convert.ToBoolean(ConfigurationManager.AppSettings["IsPayOnline"]) == true)
                                //{
                                //    strAccountCountryCode = appFunctions.AraMex_AccountCountryCode;
                                //    strAccountEntity = appFunctions.AraMex_Entity;
                                //    strAccountNumber = appFunctions.AraMex_AccountNo;
                                //    strAccountPin = appFunctions.AraMex_PinNumber;
                                //    strUserName = appFunctions.AraMex_UserName;
                                //    strPassword = appFunctions.AraMex_Password;
                                //}

                                //Service_1_0Client _Client = new Service_1_0Client();

                                //ShipmentCreationRequest _Request = new ShipmentCreationRequest();
                                //_Request.ClientInfo = new ClientInfo();
                                //_Request.ClientInfo.AccountCountryCode = strAccountCountryCode;
                                //_Request.ClientInfo.AccountEntity = strAccountEntity;
                                //_Request.ClientInfo.AccountNumber = strAccountNumber;
                                //_Request.ClientInfo.AccountPin = strAccountPin;
                                //_Request.ClientInfo.UserName = strUserName;
                                //_Request.ClientInfo.Password = strPassword;
                                //_Request.ClientInfo.Version = "v1.0";
                                //_Request.Transaction = new Transaction();
                                //_Request.Transaction.Reference1 = objOrder.s_AppOrderID ;
                                //_Request.Transaction.Reference2 = objOrder.s_AppOrderNo;
                                //_Request.Transaction.Reference3 = "";
                                //_Request.Transaction.Reference4 = "";
                                //_Request.Transaction.Reference5 = "";

                                ////Shipper // Seller Detail
                                //Shipment _Shipment = new Shipment();
                                //_Shipment.Shipper = new Party();
                                //_Shipment.Shipper.Reference1 = appFunctions.AraMex_CustomerName;
                                //_Shipment.Shipper.Reference2 = "";
                                //_Shipment.Shipper.AccountNumber = _Request.ClientInfo.AccountNumber;
                                //_Shipment.Shipper.Contact = new Contact();
                                //_Shipment.Shipper.Contact.Department = appFunctions.AraMex_Department;
                                //_Shipment.Shipper.Contact.PersonName = appFunctions.AraMex_PersonName;
                                //_Shipment.Shipper.Contact.Title = "";
                                //_Shipment.Shipper.Contact.CompanyName = appFunctions.AraMex_CustomerName;
                                //_Shipment.Shipper.Contact.PhoneNumber1 = appFunctions.AraMex_PhoneNumber;
                                //_Shipment.Shipper.Contact.CellPhone = appFunctions.AraMex_PhoneNumber;
                                //_Shipment.Shipper.Contact.EmailAddress = appFunctions.AraMex_EmailAddress;

                                //_Shipment.Shipper.PartyAddress = new Address();
                                //_Shipment.Shipper.PartyAddress.Line1 = "ship1 address";
                                //_Shipment.Shipper.PartyAddress.Line2 = "ship2 address";
                                //_Shipment.Shipper.PartyAddress.Line3 = "";
                                //_Shipment.Shipper.PartyAddress.City = "";
                                //_Shipment.Shipper.PartyAddress.StateOrProvinceCode = "";
                                //_Shipment.Shipper.PartyAddress.PostCode = "395004";
                                //_Shipment.Shipper.PartyAddress.CountryCode = "IN";

                                ////Consignee //Customer
                                //_Shipment.Consignee = new Party();
                                //_Shipment.Consignee.Reference1 = objOrder.s_AppReceiverName ;
                                //_Shipment.Consignee.Reference2 = objOrder.s_AppReceiverContactNo1;
                                //_Shipment.Consignee.AccountNumber = "";
                                //_Shipment.Consignee.Contact = new Contact();
                                //_Shipment.Consignee.Contact.Department = "";
                                //_Shipment.Consignee.Contact.PersonName = objOrder.s_AppReceiverName;
                                //_Shipment.Consignee.Contact.Title = "";
                                //_Shipment.Consignee.Contact.CompanyName = "aramex";
                                //_Shipment.Consignee.Contact.PhoneNumber1 = objOrder.s_AppReceiverContactNo1;
                                //_Shipment.Consignee.Contact.CellPhone = objOrder.s_AppReceiverContactNo1;
                                //_Shipment.Consignee.Contact.EmailAddress = objOrder.s_AppRecevierEmail ;
                                //_Shipment.Consignee.PartyAddress = new Address();
                                //_Shipment.Consignee.PartyAddress.Line1 = objOrder.s_AppReceiverAddress;
                                //_Shipment.Consignee.PartyAddress.Line2 = "";
                                //_Shipment.Consignee.PartyAddress.Line3 = "";
                                //_Shipment.Consignee.PartyAddress.City = "";
                                //_Shipment.Consignee.PartyAddress.StateOrProvinceCode = "";
                                //_Shipment.Consignee.PartyAddress.PostCode = objOrder.s_AppReceiverPIN;
                                //_Shipment.Consignee.PartyAddress.CountryCode = "IN";
                                //_Shipment.ShippingDateTime = GetDateTime().AddDays(1);


                                //ShipmentDetails _shipment_details = new ShipmentDetails();
                                //_shipment_details.NumberOfPieces = iQty ;
                                //_shipment_details.ActualWeight = new Weight();
                                //_shipment_details.ActualWeight.Value = Convert.ToDouble(decWeight);
                                //_shipment_details.ActualWeight.Unit = "kg";
                                //_shipment_details.ProductGroup = "DOM";
                                //_shipment_details.ProductType = "ONP";
                                //if (objOrder.AppPaymentMode == Convert.ToInt32(Enums.PaymentMode.COD))
                                //{
                                //    _shipment_details.PaymentType = "C";
                                //}
                                //else
                                //{
                                //    _shipment_details.PaymentType = "P";
                                //}

                                //_shipment_details.DescriptionOfGoods = "Clothes";
                                //_shipment_details.GoodsOriginCountry = "IN";
                                //_shipment_details.CustomsValueAmount = new Money() { CurrencyCode = "INR", Value = Convert.ToDouble(objOrder.AppOrderAmount )};
                                //_shipment_details.Dimensions = new Dimensions();
                                //_shipment_details.Dimensions.Length = 0;
                                //_shipment_details.Dimensions.Width = 0;
                                //_shipment_details.Dimensions.Height = 0;
                                //_shipment_details.Dimensions.Unit = "cm";

                                //_Shipment.Details = _shipment_details;
                                //_shipment_details.PaymentOptions = "";

                                //_Request.LabelInfo = new LabelInfo();
                                //_Request.LabelInfo.ReportID = 9201;
                                //_Request.LabelInfo.ReportType = "URL";
                                ////_Request.LabelInfo.ReportType = "RPT";

                                //_Request.Shipments = new Shipment[1] { _Shipment };
                                //ShipmentCreationResponse _Response = null;

                                //_Client.Open();
                                //_Response = _Client.CreateShipments(_Request);
                                //_Client.Close();

                                //strID = _Response.Shipments[0].ID;

                                Service_1_0Client _Client = new Service_1_0Client();

                                ShipmentCreationRequest _Request = new ShipmentCreationRequest();

                                _Request.ClientInfo = new ClientInfo();

                                _Request.ClientInfo.AccountCountryCode = "IN";

                                _Request.ClientInfo.AccountEntity = "BOM";

                                _Request.ClientInfo.AccountNumber = "36669982";

                                _Request.ClientInfo.AccountPin = "443543";

                                _Request.ClientInfo.UserName = "testingapi@aramex.com";

                                _Request.ClientInfo.Password = "R123456789$r";

                                _Request.ClientInfo.Version = "v1.0";

                                _Request.Transaction = new Transaction();
                                _Request.Transaction.Reference1 = "";
                                _Request.Transaction.Reference2 = "";
                                _Request.Transaction.Reference3 = "";
                                _Request.Transaction.Reference4 = "";
                                _Request.Transaction.Reference5 = "";

                                Shipment _Shipment = new Shipment();

                                //Shipper

                                _Shipment.Shipper = new Party();

                                _Shipment.Shipper.Reference1 = "ref1";

                                _Shipment.Shipper.Reference2 = "ref2";

                                _Shipment.Shipper.AccountNumber = _Request.ClientInfo.AccountNumber;


                                _Shipment.Shipper.Contact = new Contact();

                                _Shipment.Shipper.Contact.Department = "";

                                _Shipment.Shipper.Contact.PersonName = "sadiq";

                                _Shipment.Shipper.Contact.Title = "";

                                _Shipment.Shipper.Contact.CompanyName = "aramex";

                                _Shipment.Shipper.Contact.PhoneNumber1 = "123";

                                _Shipment.Shipper.Contact.CellPhone = "12345";

                                _Shipment.Shipper.Contact.EmailAddress = "test@test.com";


                                _Shipment.Shipper.PartyAddress = new Address();

                                _Shipment.Shipper.PartyAddress.Line1 = "ship1 address";

                                _Shipment.Shipper.PartyAddress.Line2 = "ship2 address";

                                _Shipment.Shipper.PartyAddress.Line3 = "";

                                _Shipment.Shipper.PartyAddress.City = "";

                                _Shipment.Shipper.PartyAddress.StateOrProvinceCode = "";

                                _Shipment.Shipper.PartyAddress.PostCode = "400612";

                                _Shipment.Shipper.PartyAddress.CountryCode = "IN";

                                //Consignee

                                _Shipment.Consignee = new Party();

                                _Shipment.Consignee.Reference1 = "ref1";

                                _Shipment.Consignee.Reference2 = "";

                                _Shipment.Consignee.AccountNumber = "";


                                _Shipment.Consignee.Contact = new Contact();

                                _Shipment.Consignee.Contact.Department = "";

                                _Shipment.Consignee.Contact.PersonName = "vipin";

                                _Shipment.Consignee.Contact.Title = "";

                                _Shipment.Consignee.Contact.CompanyName = "aramex";

                                _Shipment.Consignee.Contact.PhoneNumber1 = "123";

                                _Shipment.Consignee.Contact.CellPhone = "12345";

                                _Shipment.Consignee.Contact.EmailAddress = "test@test.com";


                                _Shipment.Consignee.PartyAddress = new Address();

                                _Shipment.Consignee.PartyAddress.Line1 = "ship1 address";

                                _Shipment.Consignee.PartyAddress.Line2 = "ship2 address";

                                _Shipment.Consignee.PartyAddress.Line3 = "";

                                _Shipment.Consignee.PartyAddress.City = "";

                                _Shipment.Consignee.PartyAddress.StateOrProvinceCode = "";

                                _Shipment.Consignee.PartyAddress.PostCode = "121010";

                                _Shipment.Consignee.PartyAddress.CountryCode = "IN";


                                _Shipment.ShippingDateTime = new DateTime(2015, 09, 2);

                                ShipmentDetails _shipment_details = new ShipmentDetails();

                                _shipment_details.NumberOfPieces = 1;

                                _shipment_details.ActualWeight = new Weight();

                                _shipment_details.ActualWeight.Value = 1;

                                _shipment_details.ActualWeight.Unit = "kg";

                                _shipment_details.ProductGroup = "DOM";

                                _shipment_details.ProductType = "ONP";

                                _shipment_details.PaymentType = "P";

                                _shipment_details.DescriptionOfGoods = "Test";

                                _shipment_details.GoodsOriginCountry = "IN";

                                _shipment_details.CustomsValueAmount = new Money() { CurrencyCode = "INR", Value = 10 };

                                _shipment_details.Dimensions = new Dimensions();

                                _shipment_details.Dimensions.Length = 0;

                                _shipment_details.Dimensions.Width = 0;

                                _shipment_details.Dimensions.Height = 0;

                                _shipment_details.Dimensions.Unit = "cm";


                                _Shipment.Details = _shipment_details;

                                _shipment_details.PaymentOptions = "";


                                _Request.LabelInfo = new LabelInfo();
                                _Request.LabelInfo.ReportID = 9201;
                                _Request.LabelInfo.ReportType = "URL";
                                //_Request.LabelInfo.ReportType = "RPT";

                                _Request.Shipments = new Shipment[1] { _Shipment };
                                ShipmentCreationResponse _Response = null;


                                _Client.Open();
                                _Response = _Client.CreateShipments(_Request);
                                _Client.Close();


                                string strAWDID = _Response.Shipments[0].ID;
                                string strUrl = _Response.Shipments[0].ShipmentLabel.LabelURL;
                                tblSubOrder objSubOrder = new tblSubOrder();
                                objSubOrder.SetCourierInof(Convert.ToInt32(Enums.Enums_OrderStatus.Confirmed), objOrder.s_AppOrderID,strCourierCompanyId , strAWDID, strUrl, true);
                                objSubOrder = null;
                                StrValue[0] = "false";
                                StrValue[1] = "strUrl";
                            }
                            else
                            {
                                tblSubOrder objSubOrder = new tblSubOrder();
                                objSubOrder.SetCourierCompanyIDOnOrderId(Convert.ToInt32(Enums.Enums_OrderStatus.Confirmed), objOrder.s_AppOrderID, strCourierCompanyId, true);
                                objSubOrder = null;
                            }
                        }
                        
                        objOrder = null;

                    }
                }
            }
           

        }
        return StrValue ;
    }

}