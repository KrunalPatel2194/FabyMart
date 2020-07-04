using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Text;
using System.Net;
using System.Configuration;
using CCA.Util;
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
public partial class Order : PageBase_Client
{
    public PageBase objPageBase = new PageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CheckSession("order");
            SetUpPageContent(ref metaDescription, ref metaKeywords);
            SetRegulerExpression();
            tblSettings objSetting = new tblSettings();
            objSetting.Query.Load();
            if (objSetting.RowCount > 0)
            {
                if (objSetting.s_AppIsCOD != "")
                {
                    if (objSetting.AppIsCOD)
                    {
                        btnCashOnDelivery.Visible = true;
                    }
                    else
                    {
                        btnCashOnDelivery.Visible = false;
                    }
                }
                else
                {
                    btnCashOnDelivery.Visible = false;
                }

            }
            else
            {
                btnCashOnDelivery.Visible = false;
            }
            objSetting = null;

            LoadProduct();
            objCommon = new clsCommon();
            objCommon.FillDropDownList(ddlCountry, "tblCountry", tblCountry.ColumnNames.AppCountry, tblCountry.ColumnNames.AppCountryID, "-- Select Country --", tblCountry.ColumnNames.AppCountry, appFunctions.Enum_SortOrderBy.Asc);
            ddlCountry.Items.Add(new ListItem("Other", "-1"));
            ddlState.Items.Clear();
            ddlState.Items.Add(new ListItem("-- Select State --", "0"));
            ddlCity.Items.Clear();
            ddlCity.Items.Add(new ListItem("-- Select City --", "0"));
            objCommon = null;
            divAdressForm.Visible = true;
            LoadAllAdresses();
        }
    }

    private void SetRegulerExpression()
    {
        REVPIN.ValidationExpression = RXPinRegularExpression;
        REVPIN.ErrorMessage = "Invalid PIN Code (" + RXPinRegularExpressionMsg + ")";
        REVMobile1.ValidationExpression = RXPhoneRegularExpression;
        REVMobile1.ErrorMessage = "Invalid Mobile Number (" + RXPhoneRegularExpressionMsg + ")";
        revRecevierEmail.ValidationExpression = RXEmailRegularExpression;
        revRecevierEmail.ErrorMessage = "Invalid Email (" + RXEmailRegularExpressionMsg + ")";

        REVtxtCoutry.ValidationExpression = RXAlphaRegularExpression;
        REVtxtCoutry.ErrorMessage = "Invalid Country (" + RXAlphaRegularExpressionMsg + ")";
        REVtxtState.ValidationExpression = RXAlphaRegularExpression;
        REVtxtState.ErrorMessage = "Invalid State (" + RXAlphaRegularExpressionMsg + ")";
        REVtxtCity.ValidationExpression = RXAlphaRegularExpression;
        REVtxtCity.ErrorMessage = "Invalid City (" + RXAlphaRegularExpressionMsg + ")";

    }

    protected void LoadDefaultAdresses(object sender, EventArgs e)
    {
        tblAddress objAdress = new tblAddress();
        objDataTable = objAdress.loadAllAdress(Session[appFunctions.Session.ClientUserID.ToString()].ToString(), true);
        DataListAdress.DataSource = null;
        DataListAdress.DataBind();
        if (objDataTable.Rows.Count > 0)
        {
            //lblCount.Text = objDataTable.Rows.Count.ToString();

            DataListAdress.DataSource = objDataTable;
            DataListAdress.DataBind();
        }
        divAdressList.Visible = true;
        divAdressForm.Visible = false;
    }

    public void LoadProduct()
    {
        DataTable dtCart = new DataTable();
        if ((HttpContext.Current.Session[appFunctions.Session.Cart.ToString()] != null))
        {
            dtCart = (DataTable)HttpContext.Current.Session[appFunctions.Session.Cart.ToString()];
        }
        dgvCart.DataSource = null;
        dgvCart.DataBind();
        if (dtCart.Rows.Count > 0)
        {
            //lblTotalPrice.Text = dtCart.Compute("sum(appTotalPrice)", "").ToString();
            lblTotalPrice.Text = Math.Round(Convert.ToDecimal(Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString()) * Convert.ToDecimal(dtCart.Compute("sum(appTotalPrice)", "").ToString()), 2).ToString();
            ((Label)Master.FindControl("lblProductCount")).Visible = true;
            ((Label)Master.FindControl("lblProductCount")).Text = dtCart.Rows.Count.ToString();
            lblCount.Text = dtCart.Rows.Count.ToString();
            //lblCount1.Text = dtCart.Rows.Count.ToString();
            dgvCart.DataSource = dtCart;
            dgvCart.DataBind();
            divProductTotalPrice.Style.Add("display", "block;");
        }
        else
        {
            ((Label)Master.FindControl("lblProductCount")).Visible = false;
            lblCount.Text = "0";
            //lblCount1.Text = "0";
            divProductTotalPrice.Style.Add("display", "none;");
            Response.Redirect(GetAlias("Default.aspx"));
        }
    }

    protected void dgvCart_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            DataTable dtCart = new DataTable();
            if ((HttpContext.Current.Session[appFunctions.Session.Cart.ToString()] != null))
            {
                dtCart = (DataTable)HttpContext.Current.Session[appFunctions.Session.Cart.ToString()];
            }
            if (dtCart.Rows.Count > 0)
            {
                DataRow[] dr = dtCart.Select("appProductId='" + e.CommandArgument.ToString() + "'");
                if (dr.Length > 0)
                {
                    dtCart.Rows.Remove(dr[0]);
                    dtCart.AcceptChanges();
                    Session[appFunctions.Session.Cart.ToString()] = null;
                    if (dtCart.Rows.Count > 0)
                    {
                        Session[appFunctions.Session.Cart.ToString()] = dtCart;
                    }
                    else
                    {

                        //DInfo.ShowMessage("Your cart empty Select product first.", Enums.MessageType.Error);
                    }
                }
            }
            LoadProduct();
        }
    }

    protected void dgvCart_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void btnHide_click(object sender, EventArgs e)
    {
        lblErrorAddress.Text = "Address Added Successfull.";
        lblErrorAddress.ForeColor = System.Drawing.Color.Green;
        LoadAllAdresses();
    }

    public void LoadAllAdresses()
    {
        tblAddress objAdress = new tblAddress();
        objDataTable = objAdress.loadAllAdress(Session[appFunctions.Session.ClientUserID.ToString()].ToString(), false);
        objAdress = null;

        DataListAdress.DataSource = null;
        DataListAdress.DataBind();
        if (objDataTable.Rows.Count > 0)
        {
            DataListAdress.DataSource = objDataTable;
            DataListAdress.DataBind();
            divAdressForm.Visible = false;
            divAdressList.Visible = true;
        }
        else
        {
            divAdressList.Visible = false;
            divAdressForm.Visible = true;
        }

    }

    public void LoadDefaultAdresses()
    {
        tblAddress objAdress = new tblAddress();
        objDataTable = objAdress.loadAllAdress(Session[appFunctions.Session.ClientUserID.ToString()].ToString(), true);
        objAdress = null;

        DataListAdress.DataSource = null;
        DataListAdress.DataBind();
        if (objDataTable.Rows.Count > 0)
        {
            divAdressForm.Visible = false;
            DataListAdress.DataSource = objDataTable;
            DataListAdress.DataBind();
        }
        else
        {
            divAdressList.Visible = true;
            divAdressForm.Visible = true;
        }
    }

    protected void DataListAdress_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandArgument.ToString() != "")
        {
            if (e.CommandName == "Edit")
            {
                hdnFormAdressID.Value = e.CommandArgument.ToString();
                divAdressForm.Visible = true;
                tblAddress objAdress = new tblAddress();
                if (objAdress.LoadByPrimaryKey(Convert.ToInt32(e.CommandArgument.ToString())))
                {

                    txtAddress.Text = objAdress.s_AppAddress;
                    txtReceiverPIN.Text = objAdress.AppPincode;
                    txtReceiverName.Text = objAdress.AppName;
                    txtEmail.Text = objAdress.AppEmail;
                    txtMobile.Text = objAdress.AppMobile;
                    if (objAdress.s_AppCountryId != "")
                    {
                        objCommon = new clsCommon();
                        if (ddlCountry.Items.FindByValue(objAdress.s_AppCountryId) != null)
                        {
                            ddlCountry.SelectedValue = objAdress.s_AppCountryId;
                            objCommon.FillDropDownList(ddlState, "tblState", tblState.ColumnNames.AppState, tblState.ColumnNames.AppStateID, "-- Select State --", tblState.ColumnNames.AppState, appFunctions.Enum_SortOrderBy.Asc, tblState.ColumnNames.AppCountryID + "=" + ddlCountry.SelectedValue);
                            ddlState.Items.Add(new ListItem("Other", "-1"));
                            if (objAdress.s_AppStateId != "")
                            {
                                if (ddlState.Items.FindByValue(objAdress.s_AppStateId) != null)
                                {
                                    ddlState.SelectedValue = objAdress.s_AppStateId;
                                    objCommon.FillDropDownList(ddlCity, "tblCity", tblCity.ColumnNames.AppCity, tblCity.ColumnNames.AppCityID, "-- Select City --", tblCity.ColumnNames.AppCity, appFunctions.Enum_SortOrderBy.Asc, tblCity.ColumnNames.AppStateID + "=" + ddlState.SelectedValue);
                                    ddlCity.Items.Add(new ListItem("Other", "-1"));
                                    if (objAdress.s_AppCityId != "")
                                    {
                                        if (ddlCity.Items.FindByValue(objAdress.s_AppCityId) != null)
                                        {
                                            ddlCity.SelectedValue = objAdress.s_AppCityId;
                                        }
                                    }
                                }
                            }
                        }
                        txtcountry.Visible = false;
                        txtState.Visible = false;
                        txtCity.Visible = false;
                        objCommon = null;
                    }
                }
                objAdress = null;
                divAdressList.Visible = false;
            }
            if (e.CommandName == "Cancel")
            {
                tblAddress objAdress = new tblAddress();
                if (objAdress.LoadByPrimaryKey(Convert.ToInt32(e.CommandArgument.ToString())))
                {
                    if (Convert.ToBoolean(objAdress.s_AppIsDefault) != true)
                    {
                        objAdress.MarkAsDeleted();
                        objAdress.Save();
                        lblErrorAddress.Text = "Address Deleted Successfull.";
                        lblErrorAddress.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblErrorAddress.Text = "you cann't Delete Default Address.";
                        lblErrorAddress.ForeColor = System.Drawing.Color.Red;
                    }
                }
                objAdress = null;
                LoadAllAdresses();
            }
            if (e.CommandName == "Default")
            {
                tblAddress objAdress = new tblAddress();
                objAdress.setDefault(Session[appFunctions.Session.ClientUserID.ToString()].ToString(), e.CommandArgument.ToString());
                objAdress = null;
                LoadAllAdresses();
            }

        }
    }

    protected void DataListAdress_RowDataBound(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataList DataListAdress = (DataList)sender;
            string appAddressId = DataListAdress.DataKeys[e.Item.ItemIndex].ToString();
            //CheckBox chk = (CheckBox)e.Item.FindControl("chkSelectRow");

            HiddenField hdnDefault = (HiddenField)e.Item.FindControl("hdnDefault");
            System.Web.UI.HtmlControls.HtmlGenericControl IsDefault = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("IsDefault");

            if (hdnDefault.Value == "True")
            {
                hdnDefault.Visible = true;
            }
            else
            {
                hdnDefault.Visible = false;
            }
            System.Web.UI.HtmlControls.HtmlGenericControl btnDefault = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("btnDefault");
            System.Web.UI.HtmlControls.HtmlGenericControl btnNotDefault = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("btnNotDefault");

            if (hdnDefault.Value == "True")
            {
                btnDefault.Visible = true;
                btnNotDefault.Visible = false;
            }
            else
            {
                btnDefault.Visible = false;
                btnNotDefault.Visible = true;
            }
        }
    }

    protected void lnkSeeAll_Click(object sender, EventArgs e)
    {
        divAdressList.Visible = true;
        LoadAllAdresses();
        divAdressForm.Visible = false;
    }

    protected void lnkAddNew_Click(object sender, EventArgs e)
    {
        resetControls();
        divAdressForm.Visible = true;
        divAdressList.Visible = false;
    }

    public void resetControls()
    {
        txtReceiverName.Text = "";
        txtAddress.Text = "";
        txtReceiverPIN.Text = "";
        txtMobile.Text = "";
        txtEmail.Text = "";
        objCommon = new clsCommon();
        objCommon.FillDropDownList(ddlCountry, "tblCountry", tblCountry.ColumnNames.AppCountry, tblCountry.ColumnNames.AppCountryID, "-- Select Country --", tblCountry.ColumnNames.AppCountry, appFunctions.Enum_SortOrderBy.Asc);
        ddlCountry.Items.Add(new ListItem("Other", "-1"));
        ddlState.Items.Clear();
        ddlState.Items.Add(new ListItem("-- Select State --", "0"));
        ddlCity.Items.Clear();
        ddlCity.Items.Add(new ListItem("-- Select City --", "0"));
        objCommon = null;
    }

    [WebMethod]
    public static string SaveRecord(string strName, string strAddressLine1, string strPincode, string strCity, string strState, string strCountry, string strCityName, string strStateName, string strCountryName, string strhdnFormAdressID, string strMobile, string strEmail)
    {
        int intCountryId = 0;
        int intStateId = 0;
        int intCityId = 0;
        if (Convert.ToInt32(strCountry) == -1)
        {
            tblCountry objCountry = new tblCountry();
            objCountry.Where.AppCountry.Value = strCountryName;
            objCountry.Query.Load();
            if (!(objCountry.RowCount > 0))
            {
                objCountry.AddNew();
                objCountry.AppCountry = strCountryName;
                objCountry.Save();
            }
            intCountryId = objCountry.AppCountryID;
            objCountry = null;
        }
        else
        {
            intCountryId = Convert.ToInt32(strCountry);
        }
        if (Convert.ToInt32(strState) == -1)
        {
            tblState objState = new tblState();
            objState.Where.AppCountryID.Value = intCountryId;
            objState.Where.AppState.Value = strStateName;
            objState.Query.Load();
            if (!(objState.RowCount > 0))
            {
                objState.AddNew();
                objState.AppState = strStateName;
                objState.AppCountryID = intCountryId;
                objState.Save();
            }
            intStateId = objState.AppStateID;

            objState = null;
        }
        else
        {
            intStateId = Convert.ToInt32(strState);
        }
        if (Convert.ToInt32(strCity) == -1)
        {
            tblCity objCity = new tblCity();
            objCity.Where.AppStateID.Value = intStateId;
            objCity.Where.AppCity.Value = strCityName;
            objCity.Query.Load();
            if (!(objCity.RowCount > 0))
            {
                objCity.AddNew();
                objCity.AppCity = strCityName;
                objCity.AppStateID = intStateId;
                objCity.Save();
            }
            intCityId = objCity.AppCityID;
            objCity = null;
        }
        else
        {
            intCityId = Convert.ToInt32(strCity);
        }

        string messageResult = string.Empty;
        tblAddress objAdress = new tblAddress();
        if (strhdnFormAdressID != "")
        {
            objAdress.LoadByPrimaryKey(Convert.ToInt32(strhdnFormAdressID));
        }
        else
        {
            objAdress.AddNew();
            tblAddress objAdresstemp = new tblAddress();
            objAdresstemp.Where.AppCustomerID.Value = Convert.ToInt32(HttpContext.Current.Session[appFunctions.Session.ClientUserID.ToString()].ToString());
            objAdresstemp.Query.Load();
            if (objAdresstemp.RowCount > 0)
            {
                objAdress.AppIsDefault = false;
            }
            else
            {
                objAdress.AppIsDefault = true;
            }
        }
        objAdress.AppName = strName;
        objAdress.AppAddress = strAddressLine1;
        objAdress.AppMobile = strMobile;
        objAdress.AppEmail = strEmail;
        objAdress.AppCountryId = intCountryId;
        objAdress.AppStateId = intStateId;
        objAdress.AppCityId = intCityId;
        objAdress.AppCustomerID = Convert.ToInt32(HttpContext.Current.Session[appFunctions.Session.ClientUserID.ToString()].ToString());
        objAdress.AppPincode = strPincode;
        objAdress.Save();

        return messageResult;
    }

    [WebMethod]

    public static Dictionary<string, object> PopulateStates(int CountryID)
    {
        PageBase objPage = new PageBase();
        Dictionary<string, object> d = new Dictionary<string, object>();


        tblState objState = new tblState();
        DataTable objDataTable = objState.getStatesFromCountryID(CountryID);


        d = ToJson(objDataTable);
        objState = null;
        objPage = null;
        return d;


    }
    [WebMethod]
    public static Dictionary<string, object> PopulateCities(int StateID)
    {
        PageBase objPage = new PageBase();
        Dictionary<string, object> d = new Dictionary<string, object>();


        tblState objState = new tblState();
        DataTable objDataTable = objState.getCitiesFromStateID(StateID);


        d = ToJson(objDataTable);
        objState = null;
        objPage = null;
        return d;


    }

    public static Dictionary<string, object> ToJson(DataTable table)
    {
        Dictionary<string, object> d = new Dictionary<string, object>();
        d.Add(table.TableName, rowtodictionary(table));
        return d;
    }

    public static List<Dictionary<string, object>> rowtodictionary(DataTable table)
    {
        List<Dictionary<string, object>> objs = new List<Dictionary<string, object>>();
        foreach (DataRow dr in table.Rows)
        {
            Dictionary<string, object> drow = new Dictionary<string, object>();
            for (int i = 0; i <= table.Columns.Count - 1; i++)
            {
                drow.Add(table.Columns[i].ColumnName, dr[i]);
            }
            objs.Add(drow);
        }
        return objs;
    }

    protected void btnPayNow_Click(object sender, EventArgs e)
    {
        if (UpdateCart())
        {
            SavePayOnlineOrder(Enums.PaymentMode.PayNow);
        }
    }

    public Boolean UpdateCart()
    {
        Boolean IsUpdate = true;
        string StrMsg = "";
        DataTable dtCart = new DataTable();
        if ((HttpContext.Current.Session[appFunctions.Session.Cart.ToString()] != null))
        {
            dtCart = (DataTable)HttpContext.Current.Session[appFunctions.Session.Cart.ToString()];
        }
        foreach (GridViewRow row in dgvCart.Rows)
        {
            string strID = dgvCart.DataKeys[row.RowIndex].Values[0].ToString();
            TextBox txt = (TextBox)row.FindControl("txtQty");
            if (txt.Text != "" && txt.Text != "0")
            {
                DataRow[] dr = dtCart.Select(tblProductDetail.ColumnNames.AppProductDetailID.ToString() + "=" + strID);
                if (dr.Length > 0)
                {
                    tblProductDetail objProductDetail = new tblProductDetail();
                    if (objProductDetail.LoadByPrimaryKey(Convert.ToInt32(strID)))
                    {
                        if (objProductDetail.AppQuantity > 0)
                        {
                            int iQty = Convert.ToInt32(txt.Text);
                            if (objProductDetail.AppQuantity >= iQty)
                            {
                                dr[0]["appQty"] = iQty.ToString();
                                decimal appRealDiscountPrice = Convert.ToDecimal(dr[0]["appRealDiscountPrice"]);
                                decimal appTotalDiscount = Convert.ToDecimal(Convert.ToDecimal(dr[0]["appQty"]) * appRealDiscountPrice);
                                dr[0]["appDiscountPrice"] = appTotalDiscount;
                                dr[0]["appTotalPrice"] = ((Convert.ToDecimal(dr[0]["appRealPrice"].ToString()) * Convert.ToDecimal(dr[0]["appQty"])) - appTotalDiscount).ToString();
                                dr[0][tblProductDetail.ColumnNames.AppPrice] = (Convert.ToDecimal(dr[0]["appRealPrice"].ToString()) * Convert.ToDecimal(dr[0]["appQty"])).ToString();
                                dtCart.AcceptChanges();
                            }
                            else
                            {
                                StrMsg = dr[0][tblProduct.ColumnNames.AppProductName].ToString() + " product only " + objProductDetail.s_AppQuantity + " Quantity avaliable ";
                            }
                        }
                        else
                        {
                            StrMsg = dr[0][tblProduct.ColumnNames.AppProductName].ToString() + " Product out of stock.";
                        }
                    }
                    objProductDetail = null;

                }
            }
            else
            {
                LoadProduct();
                if (txt.Text == "0")
                {
                    DInfo.ShowMessage("Quantity must be non zero", Enums.MessageType.Error);
                }
                else
                {
                    DInfo.ShowMessage("Quantity must be entered in product", Enums.MessageType.Error);
                }
                return false;
            }
        }
        if (IsUpdate)
        {
            HttpContext.Current.Session[appFunctions.Session.Cart.ToString()] = dtCart;
            LoadProduct();
        }
        if (StrMsg != "")
        {
            DInfo.ShowMessage(StrMsg, Enums.MessageType.Error);
            return false;
        }

        return true;
    }

    public void SavePayOnlineOrder(Enums.PaymentMode Enum)
    {
        tblAddress objAdress = new tblAddress();
        DataTable objAdressDetails = objAdress.loadAllAdress(Session[appFunctions.Session.ClientUserID.ToString()].ToString(), true);
        if (objAdressDetails.Rows.Count > 0)
        {
            DataTable dtCart = new DataTable();
            if ((HttpContext.Current.Session[appFunctions.Session.Cart.ToString()] != null))
            {
                dtCart = (DataTable)HttpContext.Current.Session[appFunctions.Session.Cart.ToString()];
            }
            if (dtCart.Rows.Count > 0)
            {
                string strDefaultOrderStatusID = Convert.ToInt32(Enums.Enums_OrderStatus.PaymentFail).ToString();

                objCommon = new clsCommon();
                tblOrder objOrder = new tblOrder();
                objOrder.AddNew();
                objOrder.s_AppOrderNo = objCommon.GetNextDisplayOrder("tblOrder", "Convert(int," + tblOrder.ColumnNames.AppOrderNo + ")").ToString().PadLeft(5, '0');
                objOrder.s_AppCustomerID = HttpContext.Current.Session[appFunctions.Session.ClientUserID.ToString()].ToString();
                objOrder.s_AppCreatedDate = FormatDateString(DateTime.Now.ToString(strInputDateFormat), strInputDateFormat, strOutputDateFormat);
                objOrder.s_AppOrderStatusID = strDefaultOrderStatusID;
                objOrder.s_AppOrderAmount = dtCart.Compute("sum(appTotalPrice)", "").ToString();
                objOrder.s_AppOrderStatusChangeDate = FormatDateString(DateTime.Now.ToString(strInputDateFormat), strInputDateFormat, strOutputDateFormat);

                objOrder.s_AppReceiverName = objAdressDetails.Rows[0][tblAddress.ColumnNames.AppName].ToString();
                objOrder.s_AppReceiverAddress = objAdressDetails.Rows[0][tblAddress.ColumnNames.AppAddress].ToString();
                objOrder.s_AppReceiverContactNo1 = objAdressDetails.Rows[0][tblAddress.ColumnNames.AppMobile].ToString();
                objOrder.s_AppReceiverContactNo2 = objAdressDetails.Rows[0][tblAddress.ColumnNames.AppMobile].ToString();
                //objOrder.s_AppPreferedTime = objDataTable.[0][tblAddress.ColumnNames.AppName];
                objOrder.s_AppRecevierEmail = objAdressDetails.Rows[0][tblAddress.ColumnNames.AppEmail].ToString();
                objOrder.s_AppReceiverPIN = objAdressDetails.Rows[0][tblAddress.ColumnNames.AppPincode].ToString();
                objOrder.s_AppReceiverCountryID = objAdressDetails.Rows[0][tblAddress.ColumnNames.AppCountryId].ToString();
                objOrder.s_AppReceiverStateID = objAdressDetails.Rows[0][tblAddress.ColumnNames.AppStateId].ToString();
                objOrder.s_AppReceiverCityID = objAdressDetails.Rows[0][tblAddress.ColumnNames.AppCityId].ToString();

                objOrder.s_AppBillReceiverName = objAdressDetails.Rows[0][tblAddress.ColumnNames.AppName].ToString();
                objOrder.s_AppBillReceiverAddress = objAdressDetails.Rows[0][tblAddress.ColumnNames.AppAddress].ToString();
                objOrder.s_AppBillReceiverContactNo1 = objAdressDetails.Rows[0][tblAddress.ColumnNames.AppMobile].ToString();
                objOrder.s_AppBillReceiverContactNo2 = objAdressDetails.Rows[0][tblAddress.ColumnNames.AppMobile].ToString();
                objOrder.s_AppBillRecevierEmail = objAdressDetails.Rows[0][tblAddress.ColumnNames.AppEmail].ToString();
                objOrder.s_AppBillReceiverPIN = objAdressDetails.Rows[0][tblAddress.ColumnNames.AppPincode].ToString();
                objOrder.s_AppBillReceiverCountryID = objAdressDetails.Rows[0][tblAddress.ColumnNames.AppCountryId].ToString();
                objOrder.s_AppBillReceiverStateID = objAdressDetails.Rows[0][tblAddress.ColumnNames.AppStateId].ToString();
                objOrder.s_AppBillReceiverCityID = objAdressDetails.Rows[0][tblAddress.ColumnNames.AppCityId].ToString();


                objOrder.AppPaymentMode = Convert.ToInt32(Enum);
                string Time = Convert.ToString(DateTime.Now.Month) + Convert.ToString(DateTime.Now.Day) + Convert.ToString(DateTime.Now.Year) + Convert.ToString(DateTime.Now.Hour) + Convert.ToString(DateTime.Now.Minute) + Convert.ToString(DateTime.Now.Second) + Convert.ToString(DateTime.Now.Millisecond);

                if (Convert.ToInt32(Enum) == Convert.ToInt32(Enums.PaymentMode.PayNow))
                {
                    //objOrder.AppPaymentStatus = Convert.ToInt32(Enums.Enums_PaymentStatus.Pending);
                    objOrder.AppPaymentStatus = Convert.ToInt32(Enums.Enums_PaymentStatus.Failure);
                }
                string txnid1 = string.Empty;
                if (Convert.ToBoolean(ConfigurationManager.AppSettings["IsCCAvenue"]) == true)
                {
                    objOrder.AppTransactionID = objOrder.s_AppOrderID + Time;
                }
                else
                {
                    Random rnd = new Random();
                    string strHash = objCommon.Generatehash512(rnd.ToString() + DateTime.Now);
                    txnid1 = strHash.ToString().Substring(0, 20);
                    objOrder.s_AppTransactionID = txnid1;
                }
                objOrder.Save();
                Session[appFunctions.Session.PaymetnOrderId.ToString()] = objOrder.s_AppOrderID;
                Session[appFunctions.Session.PaymentTransactionId.ToString()] = objOrder.AppTransactionID;
                int i = 1;
                string strTable = "";
                foreach (DataRow row in dtCart.Rows)
                {
                    tblSubOrder objsubOrder = new tblSubOrder();
                    objsubOrder.AddNew();
                    objsubOrder.s_AppSubOrderNo = objOrder.s_AppOrderNo + "-" + i.ToString();
                    i++;
                    // objsubOrder.s_AppSubOrderNo = objCommon.GetNextDisplayOrder("tblSubOrder", tblSubOrder.ColumnNames.AppSubOrderNo).ToString();
                    objsubOrder.s_AppOrderID = objOrder.s_AppOrderID;
                    objsubOrder.s_AppProductDetailID = row[tblProductDetail.ColumnNames.AppProductDetailID].ToString();
                    objsubOrder.s_AppQty = row["appQty"].ToString();
                    tblProductDetail objTemp = new tblProductDetail();
                    if (objTemp.LoadByPrimaryKey(Convert.ToInt32(row[tblProductDetail.ColumnNames.AppProductDetailID].ToString())))
                    {
                        if (objTemp.s_AppQuantity != "" & objTemp.AppQuantity > 0)
                        {
                            objTemp.AppQuantity = objTemp.AppQuantity - Convert.ToInt32(row["appQty"].ToString());
                            objTemp.Save();
                        }
                    }
                    objTemp = null;
                    objsubOrder.s_AppSellingPrice = row["appRealPrice"].ToString();
                    objsubOrder.s_AppDiscount = row["appRealDiscountPrice"].ToString();
                    objsubOrder.s_AppSubOrderStatusID = strDefaultOrderStatusID;
                    objsubOrder.s_AppSubOrderChangeDate = FormatDateString(DateTime.Now.ToString(strInputDateFormat), strInputDateFormat, strOutputDateFormat);
                    objsubOrder.s_AppMaxDispatchDate = FormatDateString(DateTime.Now.ToString(strInputDateFormat), strInputDateFormat, strOutputDateFormat);
                    objsubOrder.s_AppCurrencyID = Session[appFunctions.Session.CurrencyID.ToString()].ToString();
                    objsubOrder.s_AppRate = Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString();
                    objsubOrder.Save();
                    objsubOrder = null;
                    strTable += "<tr>";
                    strTable += "<td align=\"left\" style=\"text-transform: capitalize\"  width=\"100\"><img src=\"" + strServerURL + "admin/" + row[tblProductImage.ColumnNames.AppSmallImage].ToString() + "\" width=\"100\" /></td>";
                    strTable += "<td align=\"left\" style=\"text-transform: capitalize\" ><a target=\"_blank\" href=\"" + GetAlias("ProductDetail.aspx") + generateUrl(row[tblProduct.ColumnNames.AppProductName].ToString()) + " \">" + row[tblProduct.ColumnNames.AppProductName].ToString() + "</a><br />Sku no :" + row[tblProductDetail.ColumnNames.AppSKUNo].ToString() + " </td>";
                    strTable += "<td align=\"right\" width=\"60\">Rs " + row["appRealPrice"].ToString() + " </td>";
                    strTable += "<td align=\"right\" width=\"80\">" + row["appQty"].ToString() + "</td>";
                    strTable += "<td align=\"right\" width=\"60\">Rs " + row["appDiscountPrice"].ToString() + " </td>";
                    strTable += "<td align=\"right\" width=\"60\">Rs " + row["appTotalPrice"].ToString() + " </td>";
                    strTable += "</tr>";
                }


                SendMail(objOrder.s_AppOrderNo, objAdressDetails.Rows[0][tblAddress.ColumnNames.AppName].ToString(), objAdressDetails.Rows[0][tblAddress.ColumnNames.AppAddress].ToString(), objAdressDetails.Rows[0][tblCity.ColumnNames.AppCity].ToString(), objAdressDetails.Rows[0][tblAddress.ColumnNames.AppPincode].ToString(), objAdressDetails.Rows[0][tblState.ColumnNames.AppState].ToString(), objAdressDetails.Rows[0][tblCountry.ColumnNames.AppCountry].ToString(), objAdressDetails.Rows[0][tblAddress.ColumnNames.AppEmail].ToString(), objAdressDetails.Rows[0][tblAddress.ColumnNames.AppMobile].ToString(), strTable, "0", "0", dtCart.Compute("sum(appDiscountPrice)", "").ToString(), objOrder.s_AppOrderAmount);
                HttpContext.Current.Session[appFunctions.Session.Cart.ToString()] = null;
                if (Convert.ToInt32(Enum) == Convert.ToInt32(Enums.PaymentMode.PayNow))
                {
                    if (Convert.ToBoolean(ConfigurationManager.AppSettings["IsCCAvenue"]) == true)
                    {
                        string ccaRequest = "";
                        string strEncRequest = "";
                        try
                        {
                            ccaRequest += "tid=" + objOrder.s_AppTransactionID + "&";
                            ccaRequest += "merchant_id=" + appFunctions.strCCAvenueMerchant + "&";
                            ccaRequest += "order_id=" + objOrder.s_AppOrderID + "&";

                            if (Convert.ToBoolean(ConfigurationManager.AppSettings["IsPayOnline"]) == true)
                            {
                                ccaRequest += "amount=" + objOrder.s_AppOrderAmount + "&";
                            }
                            else
                            {
                                ccaRequest += "amount=1&";
                            }
                            ccaRequest += "currency=INR&";
                            ccaRequest += "redirect_url=" + GetAlias("CCAvenueOnlinePaymentStatus.aspx") + "&";
                            ccaRequest += "cancel_url=" + GetAlias("OnlineOrderPayStatus.aspx") + "&";

                            ccaRequest += "billing_name=" + objOrder.s_AppReceiverName + "&";
                            ccaRequest += "billing_address=" + objOrder.s_AppReceiverAddress + "&";
                            ccaRequest += "billing_city=" + objAdressDetails.Rows[0][tblCity.ColumnNames.AppCity].ToString() + "&";
                            ccaRequest += "billing_state=" + objAdressDetails.Rows[0][tblState.ColumnNames.AppState].ToString() + "&";
                            ccaRequest += "billing_zip=" + objOrder.s_AppReceiverPIN + "&";
                            ccaRequest += "billing_country=" + objAdressDetails.Rows[0][tblCountry.ColumnNames.AppCountry].ToString() + "&";
                            ccaRequest += "billing_tel=" + objOrder.s_AppReceiverContactNo1 + "&";
                            ccaRequest += "billing_email=" + objOrder.s_AppRecevierEmail + "&";

                            ccaRequest += "delivery_name=" + objOrder.s_AppBillReceiverName + "&";
                            ccaRequest += "delivery_address=" + objOrder.s_AppBillReceiverAddress + "&";
                            ccaRequest += "delivery_zip=" + objOrder.s_AppBillReceiverPIN + "&";
                            ccaRequest += "delivery_country=" + objAdressDetails.Rows[0][tblCity.ColumnNames.AppCity].ToString() + "&";
                            ccaRequest += "delivery_tel=" + objOrder.s_AppBillReceiverContactNo1 + "&";

                            ccaRequest += "customer_identifier=" + Session[appFunctions.Session.ClientUserID.ToString()].ToString() + "&";

                            CCACrypto ccaCrypto = new CCACrypto();
                            strEncRequest = ccaCrypto.Encrypt(ccaRequest, appFunctions.strCCAvenueworkingKey);
                            string strForm = CCAvenuePreparePOSTForm(strEncRequest);
                            Page.Controls.Add(new LiteralControl(strForm));
                        }
                        catch (Exception ex)
                        {
                            Response.Write("<span style='color:red'>" + ex.Message + "</span>");
                        }
                    }
                    else
                    {
                        try
                        {
                            string action1 = string.Empty;
                            string hash1 = string.Empty;

                            string[] hashVarsSeq;
                            string hash_string = string.Empty;

                            decimal decPayAmount = 1;
                            if (Convert.ToBoolean(ConfigurationManager.AppSettings["IsPayOnline"]) == true)
                            {
                                decPayAmount = objOrder.AppOrderAmount;
                            }

                            if (string.IsNullOrEmpty(Request.Form["hash"])) // generating hash value
                            {
                                hashVarsSeq = appFunctions.hashSequence.Split('|'); // spliting hash sequence from config
                                hash_string = "";
                                foreach (string hash_var in hashVarsSeq)
                                {
                                    if (hash_var == "key")
                                    {
                                        hash_string = hash_string + appFunctions.MERCHANT_KEY;
                                        hash_string = hash_string + '|';
                                    }
                                    else if (hash_var == "txnid")
                                    {
                                        hash_string = hash_string + txnid1;
                                        hash_string = hash_string + '|';
                                    }
                                    else if (hash_var == "amount")
                                    {
                                        hash_string = hash_string + decPayAmount.ToString("g29");
                                        hash_string = hash_string + '|';
                                    }
                                    else if (hash_var == "productinfo")
                                    {
                                        hash_string = hash_string + objOrder.s_AppOrderNo;
                                        hash_string = hash_string + '|';
                                    }
                                    else if (hash_var == "firstname")
                                    {
                                        hash_string = hash_string + objAdressDetails.Rows[0][tblAddress.ColumnNames.AppName].ToString();
                                        hash_string = hash_string + '|';
                                    }
                                    else if (hash_var == "email")
                                    {
                                        hash_string = hash_string + objAdressDetails.Rows[0][tblAddress.ColumnNames.AppEmail].ToString();
                                        hash_string = hash_string + '|';
                                    }
                                    else
                                    {

                                        hash_string = hash_string + (Request.Form[hash_var] != null ? Request.Form[hash_var] : "");// isset if else
                                        hash_string = hash_string + '|';
                                    }
                                }

                                hash_string += appFunctions.SALT;// appending SALT
                                objCommon = new clsCommon();
                                hash1 = objCommon.Generatehash512(hash_string).ToLower();         //generating hash
                                action1 = appFunctions.PAYU_BASE_URL + "/_payment";// setting URL
                            }

                            if (!string.IsNullOrEmpty(hash1))
                            {

                                System.Collections.Hashtable data = new System.Collections.Hashtable(); // adding values in gash table for data post
                                data.Add("hash", hash1);
                                data.Add("txnid", txnid1);
                                data.Add("key", appFunctions.MERCHANT_KEY);
                                string AmountForm = decPayAmount.ToString("g29");// eliminating trailing zeros
                                data.Add("amount", AmountForm);
                                data.Add("firstname", objAdressDetails.Rows[0][tblAddress.ColumnNames.AppName].ToString());
                                data.Add("email", objAdressDetails.Rows[0][tblAddress.ColumnNames.AppEmail].ToString());
                                data.Add("phone", objAdressDetails.Rows[0][tblAddress.ColumnNames.AppMobile].ToString());
                                data.Add("productinfo", objOrder.s_AppOrderNo);
                                string strUrl = GetAlias("OnlineOrderPayStatus.aspx");
                                data.Add("surl", strUrl);
                                data.Add("furl", strUrl);
                                data.Add("lastname", "");
                                data.Add("curl", strUrl);
                                data.Add("address1", objAdressDetails.Rows[0][tblAddress.ColumnNames.AppAddress].ToString());
                                data.Add("address2", "");
                                data.Add("city", "");
                                data.Add("state", "");
                                data.Add("country", "");
                                data.Add("zipcode", objAdressDetails.Rows[0][tblAddress.ColumnNames.AppPincode].ToString());
                                data.Add("udf1", "");
                                data.Add("udf2", "");
                                data.Add("udf3", "");
                                data.Add("udf4", "");
                                data.Add("udf5", "");
                                data.Add("pg", "");
                                data.Add("service_provider", appFunctions.ServiceProvider);
                                string strForm = PreparePOSTForm(action1, data);
                                Page.Controls.Add(new LiteralControl(strForm));
                            }
                            else
                            {
                                //no hash
                            }
                        }
                        catch (Exception ex)
                        {
                            Response.Write("<span style='color:red'>" + ex.Message + "</span>");
                        }
                    }
                }
                objOrder = null;
                objCommon = null;
            }
            else
            {
                DInfo.ShowMessage("Your cart empty. Select first product.", Enums.MessageType.Error);
            }
        }
        else
        {
            DInfo.ShowMessage("Fill Address Information first after procedure for order.", Enums.MessageType.Information);
        }
    }

    public void SendMail(string strOrderNo, string strName, string strAddress, string strCity, string strPincode, string strState, string strCountry, string strEmail, string strMobile, string strTable, string strShipping, string strCOD, string strDiscount, string strTotal)
    {
        try
        {
            objCommon = new clsCommon();
            string Strbody = "";
            string strSubject = "Fabymart.com order confirmation - " + strOrderNo;
            Strbody = objCommon.readFile(Server.MapPath("~/EmailTemplates/PayOnline.html"));
            Strbody = Strbody.Replace("`link`", strServerURL);
            Strbody = Strbody.Replace("`uname`", strName);
            Strbody = Strbody.Replace("`orderno`", strOrderNo);
            Strbody = Strbody.Replace("`name`", strName);
            Strbody = Strbody.Replace("`address`", strAddress);
            Strbody = Strbody.Replace("`city`", strCity);
            Strbody = Strbody.Replace("`pincode`", "-" + strPincode);
            Strbody = Strbody.Replace("`state`", strState);
            Strbody = Strbody.Replace("`country`", ", " + strCountry);
            Strbody = Strbody.Replace("`mobile`", strMobile);
            Strbody = Strbody.Replace("`email`", " " + strEmail);
            Strbody = Strbody.Replace("`table`", strTable);
            Strbody = Strbody.Replace("`Shipping`", "Rs. " + strShipping);
            Strbody = Strbody.Replace("`COD`", "Rs. " + strCOD);
            Strbody = Strbody.Replace("`Discounts`", "Rs. " + strDiscount);
            Strbody = Strbody.Replace("`Total`", "Rs. " + strTotal);

            Session[appFunctions.Session.PaymentEmailString.ToString()] = Strbody;
            objCommon = null;
        }
        catch (Exception ex)
        {
            Response.Write(ex.StackTrace.ToString());
        }

    }

    protected void btnUpdateCart_Click(object sender, EventArgs e)
    {
        if (UpdateCart())
        {
            DInfo.ShowMessage("Your cart successfully updated.", Enums.MessageType.Successfull);

        }
    }

    private string PreparePOSTForm(string url, System.Collections.Hashtable data)      // post form
    {
        //Set a name for the form
        string formID = "PostForm";
        //Build the form using the specified data to be posted.
        StringBuilder strForm = new StringBuilder();
        strForm.Append("<form id=\"" + formID + "\" name=\"" +
                       formID + "\" action=\"" + url +
                       "\" method=\"POST\">");

        foreach (System.Collections.DictionaryEntry key in data)
        {
            strForm.Append("<input type=\"hidden\" name=\"" + key.Key +
                           "\" value=\"" + key.Value + "\">");
        }

        strForm.Append("</form>");
        //Build the JavaScript which will do the Posting operation.
        StringBuilder strScript = new StringBuilder();
        strScript.Append("<script language='javascript'>");
        strScript.Append("var v" + formID + " = document." +
                         formID + ";");
        strScript.Append("v" + formID + ".submit();");
        strScript.Append("</script>");
        //Return the form and the script concatenated.
        //(The order is important, Form then JavaScript)
        return strForm.ToString() + strScript.ToString();
    }

    private string CCAvenuePreparePOSTForm(string strEncRequest)      // post form
    {
        //Set a name for the form
        string formID = "nonseamless";
        string strName = "redirect";
        //Build the form using the specified data to be posted.
        StringBuilder strForm = new StringBuilder();
        strForm.Append("<form id=\"" + formID + "\" name=\"" +
                       strName + "\" action=\"" + appFunctions.strCCAvenueBaseUrl +
                       "\" method=\"POST\">");

        strForm.Append("<input type=\"hidden\" id=\"encRequest\" name=\"encRequest\" value=\"" + strEncRequest + "\">");
        strForm.Append("<input type=\"hidden\" id=\"Hidden1\" name=\"access_code\" value=\"" + appFunctions.strCCAvenueAccessCode + "\">");


        strForm.Append("</form>");
        //Build the JavaScript which will do the Posting operation.
        StringBuilder strScript = new StringBuilder();
        strScript.Append("<script language='javascript'>");
        strScript.Append(" $(document).ready(function () {  $(\"#" + formID + "\").submit(); });");
        //strScript.Append(" $(window).load(function () {  $(\"#" + formID + "\").submit(); });");
        strScript.Append("</script>");

        //Return the form and the script concatenated.
        //(The order is important, Form then JavaScript)
        return strForm.ToString() + strScript.ToString();
    }

    protected void btnCashOnDelivery_Click(object sender, EventArgs e)
    {
        if (UpdateCart())
        {
            if (SaveOrder(Enums.PaymentMode.COD))
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "Your order successfull saved.";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
                Response.Redirect(GetAlias("MyOrderList.aspx"));
            }
        }
    }

    public bool SaveOrder(Enums.PaymentMode Enum)
    {
        tblAddress objAdress = new tblAddress();
        DataTable objAdressDetails = objAdress.loadAllAdress(Session[appFunctions.Session.ClientUserID.ToString()].ToString(), true);
        if (objAdressDetails.Rows.Count > 0)
        {
            DataTable dtCart = new DataTable();
            if ((HttpContext.Current.Session[appFunctions.Session.Cart.ToString()] != null))
            {
                dtCart = (DataTable)HttpContext.Current.Session[appFunctions.Session.Cart.ToString()];
            }
            if (dtCart.Rows.Count > 0)
            {
                string strDefaultOrderStatusID = "";
                tblOrderStatus objStatus = new tblOrderStatus();
                objStatus.Where.AppIsDefault.Value = true;
                objStatus.Query.Load();
                if (objStatus.RowCount > 0)
                {
                    strDefaultOrderStatusID = objStatus.s_AppOrderStatusID;
                }
                objStatus = null;

                objCommon = new clsCommon();
                tblOrder objOrder = new tblOrder();
                objOrder.AddNew();
                objOrder.s_AppOrderNo = objCommon.GetNextDisplayOrder("tblOrder", "Convert(int," + tblOrder.ColumnNames.AppOrderNo + ")").ToString().PadLeft(5, '0');
                objOrder.s_AppCustomerID = HttpContext.Current.Session[appFunctions.Session.ClientUserID.ToString()].ToString();
                objOrder.s_AppCreatedDate = FormatDateString(DateTime.Now.ToString(strInputDateFormat), strInputDateFormat, strOutputDateFormat);
                objOrder.s_AppOrderStatusID = strDefaultOrderStatusID;
                objOrder.s_AppOrderAmount = dtCart.Compute("sum(appTotalPrice)", "").ToString();
                objOrder.s_AppOrderStatusChangeDate = FormatDateString(DateTime.Now.ToString(strInputDateFormat), strInputDateFormat, strOutputDateFormat);

                objOrder.s_AppReceiverName = objAdressDetails.Rows[0][tblAddress.ColumnNames.AppName].ToString();
                objOrder.s_AppReceiverAddress = objAdressDetails.Rows[0][tblAddress.ColumnNames.AppAddress].ToString();
                objOrder.s_AppReceiverContactNo1 = objAdressDetails.Rows[0][tblAddress.ColumnNames.AppMobile].ToString();
                objOrder.s_AppReceiverContactNo2 = objAdressDetails.Rows[0][tblAddress.ColumnNames.AppMobile].ToString();
                //objOrder.s_AppPreferedTime = objDataTable.[0][tblAddress.ColumnNames.AppName];
                objOrder.s_AppRecevierEmail = objAdressDetails.Rows[0][tblAddress.ColumnNames.AppEmail].ToString();
                objOrder.s_AppReceiverPIN = objAdressDetails.Rows[0][tblAddress.ColumnNames.AppPincode].ToString();
                objOrder.s_AppReceiverCountryID = objAdressDetails.Rows[0][tblAddress.ColumnNames.AppCountryId].ToString();
                objOrder.s_AppReceiverStateID = objAdressDetails.Rows[0][tblAddress.ColumnNames.AppStateId].ToString();
                objOrder.s_AppReceiverCityID = objAdressDetails.Rows[0][tblAddress.ColumnNames.AppCityId].ToString();

                objOrder.s_AppBillReceiverName = objAdressDetails.Rows[0][tblAddress.ColumnNames.AppName].ToString();
                objOrder.s_AppBillReceiverAddress = objAdressDetails.Rows[0][tblAddress.ColumnNames.AppAddress].ToString();
                objOrder.s_AppBillReceiverContactNo1 = objAdressDetails.Rows[0][tblAddress.ColumnNames.AppMobile].ToString();
                objOrder.s_AppBillReceiverContactNo2 = objAdressDetails.Rows[0][tblAddress.ColumnNames.AppMobile].ToString();
                objOrder.s_AppBillRecevierEmail = objAdressDetails.Rows[0][tblAddress.ColumnNames.AppEmail].ToString();
                objOrder.s_AppBillReceiverPIN = objAdressDetails.Rows[0][tblAddress.ColumnNames.AppPincode].ToString();
                objOrder.s_AppBillReceiverCountryID = objAdressDetails.Rows[0][tblAddress.ColumnNames.AppCountryId].ToString();
                objOrder.s_AppBillReceiverStateID = objAdressDetails.Rows[0][tblAddress.ColumnNames.AppStateId].ToString();
                objOrder.s_AppBillReceiverCityID = objAdressDetails.Rows[0][tblAddress.ColumnNames.AppCityId].ToString();
                objOrder.AppPaymentMode = Convert.ToInt32(Enum);
                objOrder.Save();

                int i = 1;
                string strTable = "";
                foreach (DataRow row in dtCart.Rows)
                {
                    tblSubOrder objsubOrder = new tblSubOrder();
                    objsubOrder.AddNew();
                    objsubOrder.s_AppSubOrderNo = objOrder.s_AppOrderNo + "-" + i.ToString();
                    i++;
                    // objsubOrder.s_AppSubOrderNo = objCommon.GetNextDisplayOrder("tblSubOrder", tblSubOrder.ColumnNames.AppSubOrderNo).ToString();
                    objsubOrder.s_AppOrderID = objOrder.s_AppOrderID;
                    objsubOrder.s_AppProductDetailID = row[tblProductDetail.ColumnNames.AppProductDetailID].ToString();
                    objsubOrder.s_AppQty = row["appQty"].ToString();
                    tblProductDetail objTemp = new tblProductDetail();
                    if (objTemp.LoadByPrimaryKey(Convert.ToInt32(row[tblProductDetail.ColumnNames.AppProductDetailID].ToString())))
                    {
                        if (objTemp.s_AppQuantity != "" & objTemp.AppQuantity > 0)
                        {
                            objTemp.AppQuantity = objTemp.AppQuantity - Convert.ToInt32(row["appQty"].ToString());
                            objTemp.Save();
                        }
                    }
                    objTemp = null;
                    objsubOrder.s_AppSellingPrice = row["appRealPrice"].ToString();
                    objsubOrder.s_AppDiscount = row["appRealDiscountPrice"].ToString();
                    objsubOrder.s_AppSubOrderStatusID = strDefaultOrderStatusID;
                    objsubOrder.s_AppSubOrderChangeDate = FormatDateString(DateTime.Now.ToString(strInputDateFormat), strInputDateFormat, strOutputDateFormat);
                    objsubOrder.s_AppMaxDispatchDate = FormatDateString(DateTime.Now.ToString(strInputDateFormat), strInputDateFormat, strOutputDateFormat);
                    objsubOrder.s_AppCurrencyID = Session[appFunctions.Session.CurrencyID.ToString()].ToString();
                    objsubOrder.s_AppRate = Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString();
                    objsubOrder.Save();
                    objsubOrder = null;

                }

                SendMailToCOD(objOrder.s_AppOrderNo, objAdressDetails.Rows[0][tblAddress.ColumnNames.AppName].ToString(), objAdressDetails.Rows[0][tblAddress.ColumnNames.AppEmail].ToString(), objAdressDetails.Rows[0][tblAddress.ColumnNames.AppMobile].ToString());
                HttpContext.Current.Session[appFunctions.Session.Cart.ToString()] = null;

                objOrder = null;
                objCommon = null;
                return true;
            }
            else
            {
                DInfo.ShowMessage("Your cart empty. Select first product.", Enums.MessageType.Error);
                return false;
            }
        }
        else
        {
            DInfo.ShowMessage("Fill Address Information first after procedure for order.", Enums.MessageType.Information);
            return false;
        }
    }

    public void SendMailToCOD(string strOrderNo, string strName, string strEmail, string strMobile)
    {
        try
        {
            objCommon = new clsCommon();
            string Strbody = "";
            string strSubject = "Your order #" + strOrderNo + " has been registered - Cash on Delivery";
            Strbody = objCommon.readFile(Server.MapPath("~/EmailTemplates/CODOrderRegistered.html"));
            Strbody = Strbody.Replace("`link`", strServerURL);
            Strbody = Strbody.Replace("`uname`", strName);
            Strbody = Strbody.Replace("`orderno`", strOrderNo);
            string strText = appFunctions.strCODOrderRegstered;
            strText = strText.Replace("`uname`", strName);
            objCommon.SendOrderSMS(strText, strMobile);
            objCommon.SendMail(strEmail, strSubject, Strbody);
            // objCommon.SendConfirmationMail(strEmail, strSubject, Strbody, Enums.Enum_Confirmation_Mail_type.order);
            objCommon = null;
        }
        catch (Exception ex)
        {
            Response.Write(ex.StackTrace.ToString());
        }

    }

}

