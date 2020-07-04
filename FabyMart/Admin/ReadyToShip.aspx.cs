using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using ShippingReference;

public partial class ReadyToShip : PageBase_Admin
{
    tblOrder objOrder;
    tblSubOrder objSubOrder;
    clsCommon objClsCommon;
    private List<PickupItemDetail> _PickupItems = new List<PickupItemDetail>();
    private List<Shipment> _Shipments = null;

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {

            ViewState["SortOrder"] = appFunctions.Enum_SortOrderBy.Asc;
            ViewState["SortColumn"] = "";

            SetReguExpression();

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
            //objCommon.FillDropDownListWithOutDefaultValue(ddlCourierCompany, "tblCourierCompany", tblCourierCompany.ColumnNames.AppCourierCompany, tblCourierCompany.ColumnNames.AppCourierCompanyID, tblCourierCompany.ColumnNames.AppDisplayOrder, appFunctions.Enum_SortOrderBy.Asc, tblCourierCompany.ColumnNames.AppIsActive + "=1");
            objCommon.FillDropDownList(ddlCourierCompany, "tblCourierCompany", tblCourierCompany.ColumnNames.AppCourierCompany, tblCourierCompany.ColumnNames.AppCourierCompanyID, "--Select Courier Company--", tblCourierCompany.ColumnNames.AppDisplayOrder, appFunctions.Enum_SortOrderBy.Asc, tblCourierCompany.ColumnNames.AppIsActive + "=1");
            objCommon.FillRecordPerPage(ref ddlPerPage);
            //  objCommon.FillDropDownList(ddlCourierComapny, "tblCourierCompany ", tblCourierCompany.ColumnNames.AppCourierCompany, tblCourierCompany.ColumnNames.AppCourierCompanyID, "--Select Courier Company--", tblCourierCompany.ColumnNames.AppDisplayOrder, appFunctions.Enum_SortOrderBy.Asc, tblCourierCompany.ColumnNames.AppIsActive + "=1");
            LoadDataGrid(true, false);
            txtSearch.Focus();
            objCommon = null;
            SetTab();
        }

    }

    public void SetReguExpression()
    {
        //   REVCourierDate.ValidationExpression = RXDateRegularExpression;
        //  REVCourierDate.ErrorMessage = "Invalid Courier Date(" + RXDateRegularExpressionMsg + ")";
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
        System.Web.UI.HtmlControls.HtmlGenericControl liPaymentFail = (System.Web.UI.HtmlControls.HtmlGenericControl)UcOrderStratus.FindControl("PaymentFail");

        liOrder.Attributes.Add("class", "");
        liConfirmed.Attributes.Add("class", "");
        liReadyToShip.Attributes.Add("class", "active");
        liShipped.Attributes.Add("class", "");
        liDelivered.Attributes.Add("class", "");
        liCancelled.Attributes.Add("class", "");
        liReturned.Attributes.Add("class", "");
        liPaymentFail.Attributes.Add("class", "");

    }

    private void LoadDataGrid(bool IsResetPageIndex, bool IsSort, string strFieldName = "", string strFieldValue = "")
    {
        objOrder = new tblOrder();
        objDataTable = objOrder.GetOrderList(ddlFields.SelectedValue, txtSearch.Text.Trim(), Convert.ToInt32(Enums.Enums_OrderStatus.ReadyToShip).ToString(), ddlCourierCompany.SelectedValue);
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

    protected void btnMenifestList_Click(object sender, System.EventArgs e)
    {
        Response.Redirect("MenifestList.aspx");
    }

    protected void btnUploadMenifest_Click(object sender, System.EventArgs e)
    {
        if (FileUploadMenifest.HasFile)
        {
            if (SaveMenifest())
            {
                DInfo.ShowMessage("Menifest uploaded.", Enums.MessageType.Successfull);
            }
        }
        else
        {
            DInfo.ShowMessage("Select File.", Enums.MessageType.Error);
        }
    }

    protected void btnMenifestReset_Click(object sender, System.EventArgs e)
    {
        resetMenifestForm();
    }

    public void resetMenifestForm()
    {
        txtMenifestNo.Text = "";
    }

    public bool SaveMenifest()
    {
        tblManifest objMenifest = new tblManifest();
        string strError = "";
        if (objMenifest.LoadByPrimaryKey(Convert.ToInt32(txtMenifestNo.Text)))
        {
            //if (objMenifest.s_AppUploadedManifest == "")
            //{
            objClsCommon = new clsCommon();
            string Time = Convert.ToString(DateTime.Now.Month) + Convert.ToString(DateTime.Now.Day) + Convert.ToString(DateTime.Now.Year) + Convert.ToString(DateTime.Now.Hour) + Convert.ToString(DateTime.Now.Minute) + Convert.ToString(DateTime.Now.Second);
            string strPath = objClsCommon.FileUpload_Images(FileUploadMenifest.PostedFile, txtMenifestNo.Text.Trim().Replace(" ", "_") + "_" + Time, "Uploads/Menifest/", ref strError, 0, objMenifest.s_AppUploadedManifest, false, 0, 2000);
            if (strError == "")
            {
                objMenifest.AppUploadedManifest = strPath;
            }
            else
            {
                DInfo.ShowMessage(strError, Enums.MessageType.Error);
                return false;
            }
            objMenifest.Save();
            objClsCommon = null;
            //}
            //else { DInfo.ShowMessage("Menifest not exists.", Enums.MessageType.Error); }
        }
        else
        {
            DInfo.ShowMessage("Enter valid Menifest Number", Enums.MessageType.Error);
        }
        resetMenifestForm();

        objMenifest = null;
        return true;
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
                string strSellerOrderID = dgvGridView.DataKeys[e.Row.RowIndex].Values[0].ToString();
                string strPaymentMode = dgvGridView.DataKeys[e.Row.RowIndex].Values[1].ToString();


                if (strSellerOrderID != "")
                {
                    GridView dgvGrid = (GridView)e.Row.FindControl("dgvSubDetail");
                    if (dgvGrid != null)
                    {
                        objSubOrder = new tblSubOrder();
                        dgvGrid.DataSource = objSubOrder.GetSubOrderDetailList(strSellerOrderID, Convert.ToInt32(Enums.Enums_OrderStatus.ReadyToShip).ToString());
                        dgvGrid.DataBind();
                        objSubOrder = null;
                    }
                }
                Label lbltype = (Label)e.Row.FindControl("type");
                if (dgvGridView.DataKeys[e.Row.RowIndex].Values[2].ToString() != "")
                {
                    if (Convert.ToBoolean(dgvGridView.DataKeys[e.Row.RowIndex].Values[2].ToString()))
                    {
                        lbltype.Text = "Menifest Generated";
                        lbltype.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lbltype.Text = "Menifest Not Generated";
                        lbltype.ForeColor = System.Drawing.Color.Brown;
                    }
                }
                else
                {
                    lbltype.Text = "Menifest Not Generated";
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

    protected void btnShipped_Click(object sender, System.EventArgs e)
    {
        String strID = "";
        if (hdnSelectedIDs.Value != "")
        {
            if (ddlCourierCompany.SelectedValue != "0")
            {
                string[] arIDs = hdnSelectedIDs.Value.ToString().TrimEnd(',').Split(',');
                if (arIDs.Length > 0)
                {
                    //objSubOrder = new tblSubOrder();
                    //objSubOrder.SetOrderStatus(Convert.ToInt32(Enums.Enums_OrderStatus.Shipped), hdnSelectedIDs.Value.ToString().TrimEnd(','), GetCurrentDateTime().ToString(), Convert.ToInt32(Enums.Enums_OrderStatus.ReadyToShip).ToString());
                    for (int i = 0; i < arIDs.Length; i++)
                    {
                        if (arIDs[i].ToString() != "")
                        {
                            //objSubOrder.Where.AppOrderID.Value = arIDs[i].ToString();
                            //objSubOrder.Query.Load();
                            //if (objSubOrder.RowCount > 0)
                            //{
                            // SendMail(Convert.ToInt32(Enums.Enums_OrderStatus.Shipped), arIDs[i].ToString());
                            if (ddlCourierCompany.SelectedItem.ToString() == Enums.Enum_CourierCompany.AraMax.ToString())
                            {
                                PickupCreationRequest _Request = new PickupCreationRequest();

                                //ClientInfo
                                _Request.ClientInfo = new ClientInfo();
                                _Request.ClientInfo.AccountCountryCode = "IN";
                                _Request.ClientInfo.AccountEntity = "BOM";
                                _Request.ClientInfo.AccountNumber = "36669982";
                                _Request.ClientInfo.AccountPin = "443543";
                                _Request.ClientInfo.UserName = "testingapi@aramex.com";
                                _Request.ClientInfo.Password = "R123456789$r";
                                _Request.ClientInfo.Version = "v1.0";

                                //Transaction
                                _Request.Transaction = new Transaction();
                                _Request.Transaction.Reference1 = "";
                                _Request.Transaction.Reference2 = "";
                                _Request.Transaction.Reference3 = "";
                                _Request.Transaction.Reference4 = "";
                                _Request.Transaction.Reference5 = "";

                                //Pickup
                                _Request.Pickup = new Pickup();

                                //PickupContact
                                _Request.Pickup.PickupContact = new Contact();
                                _Request.Pickup.PickupContact.Department = "";
                                _Request.Pickup.PickupContact.PersonName = "Sadiq";
                                _Request.Pickup.PickupContact.Title = "";
                                _Request.Pickup.PickupContact.CompanyName = "Aramex";
                                _Request.Pickup.PickupContact.PhoneNumber1 = "1111111";
                                _Request.Pickup.PickupContact.PhoneNumber1Ext = "";
                                _Request.Pickup.PickupContact.PhoneNumber2 = "";
                                _Request.Pickup.PickupContact.PhoneNumber2Ext = "";
                                _Request.Pickup.PickupContact.FaxNumber = "";
                                _Request.Pickup.PickupContact.CellPhone = "1111111";
                                _Request.Pickup.PickupContact.EmailAddress = "test@test.com";
                                _Request.Pickup.PickupContact.Type = "";

                                //PickupAddress
                                _Request.Pickup.PickupAddress = new Address();
                                _Request.Pickup.PickupAddress.Line1 = "Testing address";
                                _Request.Pickup.PickupAddress.Line2 = "";
                                _Request.Pickup.PickupAddress.Line3 = "";
                                _Request.Pickup.PickupAddress.City = "";
                                _Request.Pickup.PickupAddress.StateOrProvinceCode = "";
                                _Request.Pickup.PickupAddress.PostCode = "400093";
                                _Request.Pickup.PickupAddress.CountryCode = "IN";

                                //ClosingTime
                                _Request.Pickup.ClosingTime = DateTime.Now.AddDays(1);

                                //Comments
                                _Request.Pickup.Comments = "";

                                //LastPickupTime
                                _Request.Pickup.LastPickupTime = DateTime.Now.AddDays(1);

                                //PickupDate 
                                _Request.Pickup.PickupDate = DateTime.Now.AddDays(1);

                                //PickupLocation 
                                _Request.Pickup.PickupLocation = "Reception";

                                //ReadyTime 
                                _Request.Pickup.ReadyTime = DateTime.Now.AddDays(1);

                                //Reference1
                                _Request.Pickup.Reference1 = "test";

                                //Reference2
                                _Request.Pickup.Reference2 = "";

                                //Vehicle
                                _Request.Pickup.Vehicle = "Car";

                                //Status
                                _Request.Pickup.Status = "Ready";

                                //Items


                                //List<PickupItemDetail> lstPickupItemDetail = new List<PickupItemDetail>();
                                PickupItemDetail objPickupItemDetail = new PickupItemDetail();
                                //objPickupItemDetail.CashAmount = "";
                                //objPickupItemDetail.Comments = "";
                                //objPickupItemDetail.ExtensionData = "";
                                //objPickupItemDetail.ExtraCharges = "";
                                objPickupItemDetail.NumberOfPieces = arIDs.Length;
                                objPickupItemDetail.NumberOfShipments = 1;
                                objPickupItemDetail.PackageType = "BOX";
                                objPickupItemDetail.Payment = "P";
                                objPickupItemDetail.ProductGroup = "DOM";
                                objPickupItemDetail.ProductType = "ONP";
                                //objPickupItemDetail.ShipmentDimensions = "";
                                //objPickupItemDetail.ShipmentVolume = "";
                                //objPickupItemDetail.ShipmentWeight = "";


                                _PickupItems.Add(objPickupItemDetail);
                                _Request.Pickup.PickupItems = _PickupItems.ToArray();

                                //Shipments
                                _Request.Pickup.Shipments = (_Shipments == null) ? null : _Shipments.ToArray();

                                _Request.LabelInfo = null;

                                _Request.LabelInfo = new LabelInfo();
                                _Request.LabelInfo.ReportID = 9201;

                                _Request.LabelInfo.ReportType = "URL";

                                //_Request.LabelInfo.ReportType = "RPT";

                                PickupCreationResponse _Response = null;
                                Service_1_0Client _Client = new Service_1_0Client();

                                _Client.Open();
                                _Response = _Client.CreatePickup(_Request);
                                _Client.Close();

                                strID = Convert.ToString(_Response.ProcessedPickup.GUID);
                            }
                            // objSubOrder.AppDocketNo = strID;
                            //objSubOrder.s_AppCourierCompanyID = ddlCourierCompany.SelectedValue;
                            //objSubOrder.Save();
                            strID = "";

                            //}
                        }
                    }
                   //objSubOrder = null;
                    DInfo.ShowMessage("Order status has been Change.", Enums.MessageType.Successfull);
                    hdnSelectedIDs.Value = "";
                    UcOrderStratus.SetOrderCount();
                    LoadDataGrid(true, false);

                }
                else
                {
                    DInfo.ShowMessage("Select any one Order for Shipped.", Enums.MessageType.Error);
                }
            }
            else
            {
                DInfo.ShowMessage("Select any Courier Company for Shipped.", Enums.MessageType.Error);

            }
        }
        else
        {
            DInfo.ShowMessage("Select any one Order for Shipped.", Enums.MessageType.Error);
        }
    }

    protected void dgvGridView_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
        {

        }
    }

    protected void dgvSubDetail_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
        {
            if (e.CommandName.ToString() == "CancelOrder")
            {
                objSubOrder = new tblSubOrder();
                objSubOrder.SetSubOrderStatus(Convert.ToInt32(Enums.Enums_OrderStatus.CancelledByAdmin), e.CommandArgument.ToString(), GetCurrentDateTime().ToString(), Convert.ToInt32(Enums.Enums_OrderStatus.ReadyToShip).ToString());
                objSubOrder = null;
                //  SendMail(Convert.ToInt32(Enums.Enums_OrderStatus.CancelledByAdmin), e.CommandArgument.ToString());
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
                if (objOrder.AppOrderStatusID == Convert.ToInt32(Enums.Enums_OrderStatus.Shipped))
                {

                    objCommon = new clsCommon();
                    string Strbody = "";
                    string strSubject = "Fabymart.com order ";
                    Strbody = objCommon.readFile(Server.MapPath("~/Admin/EmailTemplates/OrderConfirmation.html"));


                    if (strStatus == Convert.ToInt32(Enums.Enums_OrderStatus.Shipped))
                    {
                        Strbody = Strbody.Replace("`orderstatus`", "Shipped");
                        strSubject += " Shipped  - ";
                    }
                    else if (strStatus == Convert.ToInt32(Enums.Enums_OrderStatus.CancelledByAdmin))
                    {
                        Strbody = Strbody.Replace("`orderstatus`", "Cancelled By FabyMart");
                        strSubject += " Cancelled  - ";
                    }

                    Strbody = Strbody.Replace("`shipmentdate`", "");
                    Strbody = Strbody.Replace("`link`", strServerURL);
                    Strbody = Strbody.Replace("`orderdate`", DateTime.Now.Date.ToString("dd-MM-yyyy"));

                    strEmail = objOrder.AppRecevierEmail;
                    Strbody = Strbody.Replace("`uname`", objOrder.AppReceiverName);
                    Strbody = Strbody.Replace("`orderno`", objOrder.s_AppOrderNo);
                    strSubject += objOrder.s_AppOrderNo;

                    //objCommon.SendConfirmationMail(strEmail, strSubject, Strbody, Enums.Enum_Confirmation_Mail_type.order);
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

    //protected void ddlCourierComapny_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    LoadDataGrid(true, false);
    //    hdnCourier.Value = objEncrypt.Encrypt(ddlCourierComapny.SelectedValue, appFunctions.strKey);
    //}

    protected void ddlCourierCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDataGrid(true, false);
    }
}