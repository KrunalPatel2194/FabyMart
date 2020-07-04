using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;

public partial class ProductDetail : PageBase_Client
{
    tblProduct objProduct;
    tblReviews objReviews;
    HttpCookie httpCookie;
    public PageBase objPageBase = new PageBase();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SetUpPageContent(ref metaDescription, ref metaKeywords);
            SetCartProductCount();
            Setregularexpression();

            objCommon = new clsCommon();
            if (Session[appFunctions.Session.DInfoInquiry.ToString()] != null)
            {

                if (Session[appFunctions.Session.DInfoInquiry.ToString()] != "")
                {
                    DInfo.ShowMessage(Session[appFunctions.Session.DInfoInquiry.ToString()].ToString(), Enums.MessageType.Successfull);
                    Session[appFunctions.Session.DInfoInquiry.ToString()] = null;
                }
            }
            hdnProductName.Value = objCommon.GetParameterFromUrl(Enums.Enums_URLParameterType.ByNumber, "1");
            string strColor = objCommon.GetParameterFromUrl(Enums.Enums_URLParameterType.ByNumber, "2");

            if (hdnProductName.Value != "")
            {
                lblLiteral.Text = hdnProductName.Value;
                if (strColor != "")
                {
                    tblColor objColor = new tblColor();
                    objColor.Where.AppColorName.Value = strColor;
                    //objColor.Where.AppIsActive.Value = true;
                    objColor.Query.Load();
                    if (objColor.RowCount > 0)
                    {
                        hdnColorId.Value = objColor.s_AppColorID;
                    }
                    else
                    {
                        hdnColorId.Value = "0";
                    }
                    objColor = null;
                }
                objProduct = new tblProduct();
                objProduct.Where.AppProductName.Value = hdnProductName.Value;
                objProduct.Query.Load();
                if (objProduct.RowCount > 0)
                {
                    this.Page.Title = objProduct.AppProductName;
                    lblProductName.Text = objProduct.AppProductName;
                    if (objProduct.s_AppDescription != "")
                    {
                        divDescription.InnerHtml = objProduct.s_AppDescription;
                    }
                    else
                    {
                        DataDescription.Visible = false;
                    }
                    if (objProduct.s_AppMetaKeyWord != "")
                    {
                        metaKeywords = objProduct.s_AppMetaKeyWord;
                    }
                    if (objProduct.s_AppMetaDescription != "")
                    {
                        metaDescription = objProduct.s_AppMetaDescription;
                    }

                    DivWashCare.InnerHtml = objProduct.s_AppWashCare;
                    hdnPKID.Value = objProduct.s_AppProductID;

                    if (objProduct.s_AppEstimatedDeliveryDays != "")
                    {
                        lblDeliveryDays.Text = objProduct.s_AppEstimatedDeliveryDays;
                    }

                    if (objProduct.s_AppIsSize != "")
                    {
                        if (!objProduct.AppIsSize)
                        {
                            divProductSize.Style.Add("display", "none");
                            hdnIsSize.Value = "false";
                        }
                        else
                        {
                            divProductSize.Style.Add("display", "block");
                            hdnIsSize.Value = "true";
                        }
                    }
                    if (objProduct.s_AppIsColor != "")
                    {
                        if (!objProduct.AppIsColor)
                        {
                            divProductColor.Style.Add("display", "none");
                            hdnIsColor.Value = "false";
                        }
                        else
                        {
                            divProductColor.Style.Add("display", "block");
                            hdnIsColor.Value = "true";
                        }

                    }
                    LoadColor();
                    LoadColorWiseImages();
                    LoadReviewList();
                    LoadRelatedProduct();
                    LoadRecentProduct();
                    if ((Session[appFunctions.Session.ClientUserID.ToString()] != null))
                    {
                        if ((Session[appFunctions.Session.ClientUserName.ToString()]) != "" || (Session[appFunctions.Session.ClientUserID.ToString()]) != "0")
                        {
                            divReviewForm.Visible = true;
                            btnClickHere.Visible = false;
                        }
                        else
                        {
                            divReviewForm.Visible = false;
                            btnClickHere.Visible = true;
                        }
                    }
                    else
                    {
                        divReviewForm.Visible = false;
                        btnClickHere.Visible = true;
                    }
                }
                objProduct = null;
            }
            LoadPixcelCode();
            this.Page.Title = hdnProductName.Value;
        }
    }
    public void Setregularexpression()
    {
        //revPincode.ValidationExpression = RXPinRegularExpression;
        //revPincode.ErrorMessage = "Invalid Pincode (" + RXPinRegularExpressionMsg + ")";
    }
    public void LoadColorWiseImages(string strProductColorId = "0")
    {
        tblProductImage objImg = new tblProductImage();
        objDataTable = objImg.LoadProductColorImg(hdnPKID.Value, strProductColorId, hdnColorId.Value);
        if (objDataTable.Rows.Count > 0)
        {
            // lblProductPrice.Text = objDataTable.Rows[0][tblProductDetail.ColumnNames.AppPrice].ToString();
            // SpanPrice.InnerHtml = objDataTable.Rows[0][tblProductDetail.ColumnNames.AppPrice].ToString();
            //if (objDataTable.Rows[0][tblProductDetail.ColumnNames.AppPrice].ToString() != "")
            //{
            //  SpanPrice.InnerHtml = Session[appFunctions.Session.CurrencyImage.ToString()].ToString() + "" + Math.Round(Convert.ToDecimal(Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString()) * Convert.ToDecimal(objDataTable.Rows[0][tblProductDetail.ColumnNames.AppPrice].ToString()), 2).ToString();
            //}

            hdnImg.Value = strServerURL + "admin/" + objDataTable.Rows[0][tblProductImage.ColumnNames.AppLargeImage].ToString();
            LabelSKUNo.Text = objDataTable.Rows[0][tblProductDetail.ColumnNames.AppSKUNo].ToString();
            SpanPrice.InnerHtml = Session[appFunctions.Session.CurrencyImage.ToString()].ToString() + "" + Math.Round(Convert.ToDecimal(Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString()) * Convert.ToDecimal(objDataTable.Rows[0][tblProductDetail.ColumnNames.AppPrice].ToString()), 0).ToString();
            SpanMRP.InnerHtml = "Selling Price : " + Session[appFunctions.Session.CurrencyImage.ToString()].ToString() + "" + Math.Round(Convert.ToDecimal(Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString()) * Convert.ToDecimal(objDataTable.Rows[0][tblProductDetail.ColumnNames.AppMRP].ToString()), 0).ToString();
            decimal decPrice = Convert.ToDecimal(objDataTable.Rows[0][tblProductDetail.ColumnNames.AppPrice].ToString());
            decimal decMRP = Convert.ToDecimal(objDataTable.Rows[0][tblProductDetail.ColumnNames.AppMRP].ToString()); ;
            if (objDataTable.Rows[0]["appMRP1"].ToString() != "0.00")
            {
                SpanMRP2.InnerHtml = "MRP : " + Session[appFunctions.Session.CurrencyImage.ToString()].ToString() + "" + Math.Round(Convert.ToDecimal(Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString()) * Convert.ToDecimal(objDataTable.Rows[0]["appMRP1"].ToString()), 0).ToString();
                decMRP = Convert.ToDecimal(objDataTable.Rows[0]["appMRP1"].ToString());
                SpanMRP2.Style.Add("text-decoration", "line-through");
                SpanMRP.Style.Add("text-decoration", "line-through");
            }
            else
            {
                SpanMRP.InnerHtml = "MRP : " + Session[appFunctions.Session.CurrencyImage.ToString()].ToString() + "" + Math.Round(Convert.ToDecimal(Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString()) * Convert.ToDecimal(objDataTable.Rows[0][tblProductDetail.ColumnNames.AppMRP].ToString()), 0).ToString();
                SpanMRP2.Visible = false;
                SpanMRP.Style.Add("text-decoration", "line-through");
            }

            int iPercentage = Convert.ToInt32(Math.Round((100 - ((decPrice * 100) / decMRP)), 0));
            if (iPercentage != 0)
            {
                divProductOff.Style.Add("visibility", "visible");
                lblDiscount.Text = iPercentage.ToString() + "%";
                lblSaveRupee.Text = "(You are saving " + Session[appFunctions.Session.CurrencyImage.ToString()].ToString() + Math.Round(Convert.ToDecimal(Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString()) * (decMRP - decPrice), 0).ToString() + ")";
            }
            else
            {
                divProductOff.Style.Add("visibility", "hidden");
                lblDiscount.Text = "";
                lblSaveRupee.Text = "";
            }

            objCommon = new clsCommon();
            objCommon.FillDropDownListWithOutDefaultValue(ddlSize, "tblProductDetail Inner join tblSize On tblSize.appSizeId=tblProductDetail.appSizeId ", tblSize.ColumnNames.AppSize, tblProductDetail.ColumnNames.AppProductDetailID, tblProductDetail.ColumnNames.AppProductDetailID, appFunctions.Enum_SortOrderBy.Asc, tblProductDetail.ColumnNames.AppProductColorID + "=" + objDataTable.Rows[0][tblProductDetail.ColumnNames.AppProductColorID].ToString());
            objCommon = null;

            hdncolorName.Value = objDataTable.Rows[0][tblColor.ColumnNames.AppColorName].ToString();
            hdnProductColorId.Value = objDataTable.Rows[0][tblProductDetail.ColumnNames.AppProductColorID].ToString();
            ddlSize.SelectedValue = objDataTable.Rows[0][tblProductDetail.ColumnNames.AppProductDetailID].ToString();
            hdnProductDetailId.Value = objDataTable.Rows[0][tblProductDetail.ColumnNames.AppProductDetailID].ToString();
        }

        RepImg.DataSource = objDataTable;
        RepImg.DataBind();

        LoadProductProperty();

        objImg = null;

    }

    public void LoadColor()
    {
        if (hdnPKID.Value != "")
        {
            tblProductColor objColor = new tblProductColor();
            objDataTable = objColor.LoadProductColor(hdnPKID.Value);
            //if (objDataTable.Rows.Count <= 1)
            //{
            //    divProductColor.Style.Add("display", "none");
            //}
            dtProductColor.DataSource = objDataTable;
            dtProductColor.DataBind();
            objColor = null;
        }
    }

    protected void dtProductColor_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "ProductColor")
        {
            hdnColorId.Value = "0";
            LoadColorWiseImages(e.CommandArgument.ToString());
        }
    }

    protected void ddlSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        tblProductDetail objDetail = new tblProductDetail();
        objDataTable = objDetail.GetProductDetail(ddlSize.SelectedValue);
        if (objDataTable.Rows.Count > 0)
        {
            SpanPrice.InnerHtml = Session[appFunctions.Session.CurrencyImage.ToString()].ToString() + "" + Math.Round(Convert.ToDecimal(Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString()) * Convert.ToDecimal(objDataTable.Rows[0][tblProductDetail.ColumnNames.AppPrice].ToString()), 0).ToString();
            SpanMRP.InnerHtml = "Selling Price : " + Session[appFunctions.Session.CurrencyImage.ToString()].ToString() + "" + Math.Round(Convert.ToDecimal(Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString()) * Convert.ToDecimal(objDataTable.Rows[0][tblProductDetail.ColumnNames.AppMRP].ToString()), 0).ToString();
            decimal decPrice = Convert.ToDecimal(objDataTable.Rows[0][tblProductDetail.ColumnNames.AppPrice].ToString());
            decimal decMRP = Convert.ToDecimal(objDataTable.Rows[0][tblProductDetail.ColumnNames.AppMRP].ToString()); ;
            if (objDataTable.Rows[0]["appMRP1"].ToString() != "0.00")
            {
                SpanMRP2.InnerHtml = "MRP : " + Session[appFunctions.Session.CurrencyImage.ToString()].ToString() + "" + Math.Round(Convert.ToDecimal(Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString()) * Convert.ToDecimal(objDataTable.Rows[0]["appMRP1"].ToString()), 0).ToString();
                decMRP = Convert.ToDecimal(objDataTable.Rows[0]["appMRP1"].ToString());
                SpanMRP2.Style.Add("text-decoration", "line-through");
                SpanMRP.Style.Add("text-decoration", "line-through");
            }
            else
            {
                SpanMRP.InnerHtml = "MRP : " + Session[appFunctions.Session.CurrencyImage.ToString()].ToString() + "" + Math.Round(Convert.ToDecimal(Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString()) * Convert.ToDecimal(objDataTable.Rows[0][tblProductDetail.ColumnNames.AppMRP].ToString()), 0).ToString();
                SpanMRP2.Visible = false;
                SpanMRP.Style.Add("text-decoration", "line-through");
            }

            int iPercentage = Convert.ToInt32(Math.Round((100 - ((decPrice * 100) / decMRP)), 0));
            if (iPercentage != 0)
            {
                divProductOff.Style.Add("visibility", "visible");
                lblDiscount.Text = iPercentage.ToString() + "%";
                lblSaveRupee.Text = "(You are saving " + Session[appFunctions.Session.CurrencyImage.ToString()].ToString() + Math.Round(Convert.ToDecimal(Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString()) * (decMRP - decPrice), 0).ToString() + ")";
            }
            else
            {
                divProductOff.Style.Add("visibility", "hidden");
                lblDiscount.Text = "";
                lblSaveRupee.Text = "";
            }

        }
        objDetail = null;
    }

    public void LoadRelatedProduct()
    {
        if (hdnPKID.Value != "")
        {
            tblRelatedProduct objRelatedProduct = new tblRelatedProduct();
            objDataTable = objRelatedProduct.LoadRelatedProduct(hdnPKID.Value);
            if (objDataTable.Rows.Count <= 0)
            {
                divRelatedProduct.Style.Add("display", "none");
            }
            RepRelatedProduct.DataSource = objDataTable;
            RepRelatedProduct.DataBind();
            objRelatedProduct = null;
        }
    }

    public void LoadProductProperty()
    {
        if (hdnPKID.Value != "")
        {
            tblProductProperty objProductProperty = new tblProductProperty();
            objDataTable = objProductProperty.LoadProductProperty(hdnPKID.Value);

            if (Convert.ToBoolean(hdnIsSize.Value))
            {
                DataRow dr = objDataTable.NewRow();
                dr[tblProperty.ColumnNames.AppPropertyName] = "Size";
                dr[tblProperty.ColumnNames.AppDisplayName] = "Size";
                dr[tblProductProperty.ColumnNames.AppValue] = ddlSize.SelectedItem.Text;
                objDataTable.Rows.InsertAt(dr, 0);
            }
            //if (Convert.ToBoolean(hdnIsColor.Value))
            //{
            //    DataRow drColor = objDataTable.NewRow();
            //    drColor[tblProperty.ColumnNames.AppPropertyName] = "Color";
            //    drColor[tblProperty.ColumnNames.AppDisplayName] = "Color";
            //    drColor[tblProductProperty.ColumnNames.AppValue] = hdncolorName.Value;
            //    objDataTable.Rows.InsertAt(drColor, 0);
            //}

            RepProperty.DataSource = objDataTable;
            RepProperty.DataBind();
            objProductProperty = null;
        }
    }

    protected void lnkWish_Click(object sender, EventArgs e)
    {
        if (!IsLogin())
        {
            CustLogin.ShowLogin(true);
        }
        else
        {
            if (SaveFavouriteProduct())
            {
                DInfo.ShowMessage("Product successfully added in favourite.", Enums.MessageType.Successfull);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Product insert in Favourite List.');</script>", false);
                tblFavouriteProduct objFavProduct = new tblFavouriteProduct();
                objFavProduct.Where.AppCustomerID.Value = Session[appFunctions.Session.ClientUserID.ToString()].ToString();
                objFavProduct.Query.Load();
                ((Label)Master.FindControl("lblWishCount")).Text = "(" + objFavProduct.RowCount.ToString() + ")";
                objFavProduct = null;
            }
        }
    }

    protected void lnkInquiry_Click(object sender, EventArgs e)
    {
        if (hdnProductDetailId.Value != "")
        {
            UserInquiry.Show(hdnProductDetailId.Value, "ProductDetail.aspx");
        }
    }

    public bool SaveFavouriteProduct()
    {
        if (hdnPKID.Value != "")
        {
            objCommon = new clsCommon();
            if (objCommon.IsRecordExists("tblFavouriteProduct", tblFavouriteProduct.ColumnNames.AppProductID, tblFavouriteProduct.ColumnNames.AppFavouriteProductID, hdnPKID.Value, "", tblFavouriteProduct.ColumnNames.AppCustomerID + "=" + Session[appFunctions.Session.ClientUserID.ToString()].ToString()))
            {
                DInfo.ShowMessage("This product already exists in favourite.", Enums.MessageType.Error);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Product alredy exits in Favourite List');</script>", false);
                return false;
            }
            objCommon = null;

            tblFavouriteProduct objFavouriteProduct = new tblFavouriteProduct();
            objFavouriteProduct.AddNew();
            objFavouriteProduct.s_AppProductID = hdnPKID.Value;
            objFavouriteProduct.s_AppCustomerID = Session[appFunctions.Session.ClientUserID.ToString()].ToString();
            objFavouriteProduct.s_AppCreatedDate = FormatDateString(DateTime.Now.ToString(strInputDateFormat), strInputDateFormat, strOutputDateFormat);
            objFavouriteProduct.Save();
            objFavouriteProduct = null;

            return true;
        }
        else
        {
            return false;
        }
    }

    protected void btnClickHere_Click(object sender, EventArgs e)
    {
        CustLogin.ShowLogin(true);
    }

    protected void btnaddReview_Click(object sender, EventArgs e)
    {
        if (Saveview())
        {
            DInfoReview.ShowMessage("Your comment about this Product is saved", Enums.MessageType.Successfull);
            txtReview.Text = "";
            txtTitle.Text = "";
            RatingReview.CurrentRating = 0;
            LoadReviewList();
        }
    }

    public bool Saveview()
    {
        // CheckSession();

        objReviews = new tblReviews();


        objReviews.AddNew();
        objReviews.s_AppProductDetailID = hdnProductDetailId.Value;
        objReviews.s_AppCustomerID = Session[appFunctions.Session.ClientUserID.ToString()].ToString();
        objReviews.s_AppRating = RatingReview.CurrentRating.ToString();

        objReviews.AppReviewStatus = Convert.ToInt32(Enums.Enum_ReviewStatus.Submitted);

        objReviews.AppReviewDate = GetDateTime();
        objReviews.s_AppComment = txtReview.Text;
        objReviews.s_AppTitle = txtTitle.Text;
        objReviews.Save();
        objReviews = null;
        return true;
    }

    public void LoadReviewList()
    {
        if (hdnProductDetailId.Value != "")
        {
            tblReviews objReviews = new tblReviews();
            RptRating.DataSource = objReviews.LoadReviewsProductvise(hdnProductDetailId.Value);
            RptRating.DataBind();
            objReviews = null;
        }
    }

    public void LoadRecentProduct()
    {
        string strRecentProductId = "";
        objEncrypt = new clsEncryption();
        if (Request.Cookies.Get("FebyMart") != null)
        {
            if (Request.Cookies.Get("FebyMart").Value != "")
            {
                strRecentProductId = objEncrypt.Decrypt(Request.Cookies.Get("FebyMart").Value, appFunctions.strKey);
            }

        }
        if (strRecentProductId != "")
        {
            if (!strRecentProductId.Contains(hdnProductDetailId.Value))
            {
                strRecentProductId = strRecentProductId.Trim(',').TrimEnd(',').Trim();
                string[] strIds = strRecentProductId.Split(',');
                if (strIds.Length > 3)
                {
                    string strRecentId = "";
                    for (int i = 1; i < 4; i++)
                    {
                        strRecentId = strRecentId + "," + strIds[i];
                    }
                    strRecentProductId = strRecentId.Trim(',').TrimEnd(',').Trim() + "," + hdnProductDetailId.Value;
                }
                else
                {
                    strRecentProductId = strRecentProductId + "," + hdnProductDetailId.Value;
                }
            }
        }
        else
        {
            strRecentProductId = hdnProductDetailId.Value;
        }
        tblProduct objTempProduct = new tblProduct();
        objDataTable = objTempProduct.LoadRecentProduct(strRecentProductId, hdnProductDetailId.Value);
        if (objDataTable.Rows.Count > 0)
        {
            RepRecentProduct.DataSource = objDataTable;
            RepRecentProduct.DataBind();
            divRecentProduct.Visible = true;
        }
        else
        {
            divRecentProduct.Visible = false;
        }
        objTempProduct = null;
        httpCookie = new HttpCookie("FebyMart", objEncrypt.Encrypt(strRecentProductId, appFunctions.strKey));
        httpCookie.Expires = DateTime.Today.AddDays(10);
        Response.Cookies.Add(httpCookie);
        objEncrypt = null;
    }

    protected void btnExpress_Click(object sender, EventArgs e)
    {
        if (addCartExpress())
        {
            if ((Session[appFunctions.Session.ClientUserID.ToString()] != null))
            {
                if (string.IsNullOrEmpty(Session[appFunctions.Session.ClientUserName.ToString()].ToString()) | Session[appFunctions.Session.ClientUserID.ToString()].ToString() == "0")
                {
                    CustLogin.ShowLogin();
                }
                else
                {
                    Response.Redirect(objPageBase.GetAlias("Order.aspx"));
                }
            }
            else
            {
                CustLogin.ShowLogin();
            }

        }
        else
        {
            DInfo.ShowMessage("Product out of stock.", Enums.MessageType.Error);
        }


    }

    protected void btnCart_Click(object sender, EventArgs e)
    {
        if (hdnProductDetailId.Value != "")
        {
            tblProductDetail objProductDetail = new tblProductDetail();
            if (objProductDetail.LoadByPrimaryKey(Convert.ToInt32(hdnProductDetailId.Value)))
            {
                if (objProductDetail.AppQuantity > 0 & objProductDetail.AppQuantity >= Convert.ToInt32(txtQty.Text))
                {
                    objCommon = new clsCommon();
                    objCommon.AddToCart(hdnPKID.Value, hdnProductColorId.Value, ddlSize.SelectedValue, Convert.ToInt32(txtQty.Text), hdnPriceDiscount.Value);
                    objCommon = null;
                    txtQty.Text = "1";
                    SetCartProductCount();
                    ViewCart.LoadProduct();
                }
                else
                {
                    DInfo.ShowMessage("Product out of stock.", Enums.MessageType.Error);
                }
            }
            else
            {
                DInfo.ShowMessage("Product out of stock.", Enums.MessageType.Error);
            }
            objProductDetail = null;
        }
        else
        {
            DInfo.ShowMessage("Product out of stock.", Enums.MessageType.Error);
        }

    }

    public bool addCartExpress()
    {
        if (hdnProductDetailId.Value != "")
        {
            tblProductDetail objProductDetail = new tblProductDetail();
            if (objProductDetail.LoadByPrimaryKey(Convert.ToInt32(hdnProductDetailId.Value)))
            {
                if (objProductDetail.AppQuantity > 0)
                {

                    objCommon = new clsCommon();
                    objCommon.AddToCart(hdnPKID.Value, hdnProductColorId.Value, ddlSize.SelectedValue, Convert.ToInt32(txtQty.Text), hdnPriceDiscount.Value);
                    objCommon = null;
                    txtQty.Text = "1";

                }
                else
                {
                    DInfo.ShowMessage("Product out of stock.", Enums.MessageType.Error);
                    return false;
                }
            }
            else
            {
                DInfo.ShowMessage("Product out of stock.", Enums.MessageType.Error);
                return false;
            }
            objProductDetail = null;
            return true;
        }
        return false;
    }

    public void SetCartProductCount()
    {
        DataTable dtCart = new DataTable();
        if ((HttpContext.Current.Session[appFunctions.Session.Cart.ToString()] != null))
        {
            dtCart = (DataTable)HttpContext.Current.Session[appFunctions.Session.Cart.ToString()];
        }
        if (dtCart.Rows.Count > 0)
        {
            ((Label)Master.FindControl("lblProductCount")).Visible = true;
            ((Label)Master.FindControl("lblProductCount")).Text = dtCart.Rows.Count.ToString();
            ((System.Web.UI.HtmlControls.HtmlAnchor)Master.FindControl("lnkCart")).HRef = GetAlias("Order.aspx");
        }
        else
        {
            ((Label)Master.FindControl("lblProductCount")).Visible = false;
            ((System.Web.UI.HtmlControls.HtmlAnchor)Master.FindControl("lnkCart")).HRef = Request.RawUrl + "#";
        }
    }

    //protected void btnCheckPincode_Click(object sender, EventArgs e)
    //{
    //    tblPinCode objPinCode = new tblPinCode();
    //    objPinCode.Where.AppPinCode.Value = txtPincode.Text;
    //    objPinCode.Query.Load();
    //    if (objPinCode.RowCount > 0)
    //    {
    //        lblAvailability.Visible = true;
    //        lblAvailability.Text = "Available";
    //        lblAvailability.ForeColor = System.Drawing.Color.Green;
    //    }
    //    else
    //    {
    //        lblAvailability.Visible = true;
    //        lblAvailability.Text = "Not Available";
    //        lblAvailability.ForeColor = System.Drawing.Color.Red;
    //    }
    //    objPinCode = null;
    //}


    protected void btnCheckCouponCode_Click(object sender, EventArgs e)
    {
        decimal decprice = 0;
        decimal decpricediscount = 0;
        tblCouponCode objCouponCode = new tblCouponCode();
        objDataTable = objCouponCode.CheckCouponCodeExists(txtCouponCode.Text);
        objCouponCode = null;
        if (objDataTable.Rows.Count > 0 && lblDiscount.Text == "")
        {
            //decprice = Convert.ToDecimal(SpanPrice.InnerText.ToString().Split('.')[1] + "." + SpanPrice.InnerText.ToString().Split('.')[2]);
            decprice = Convert.ToDecimal(SpanPrice.InnerText.ToString().Split('.')[1]);
            decpricediscount = (Convert.ToDecimal(objDataTable.Rows[0][tblCouponCode.ColumnNames.AppDiscountPer].ToString()) * decprice) / 100;

            DataTable dtTemp = new DataTable();
            tblCouponCodeProduct objCouponCodeProduct = new tblCouponCodeProduct();
            if (objDataTable.Rows[0][tblCouponCode.ColumnNames.AppType].ToString() == Convert.ToInt32(Enums.Enum_CouponCodeType.General).ToString())
            {
                lblCouponCode.Text = "Price after discount: " + Convert.ToInt32(Math.Round((decprice - decpricediscount), 0));
                lblCouponCode.ForeColor = System.Drawing.Color.Green;
                hdnPriceDiscount.Value = Convert.ToInt32(Math.Round(decpricediscount, 0)).ToString();
            }
            else if (objDataTable.Rows[0][tblCouponCode.ColumnNames.AppType].ToString() == Convert.ToInt32(Enums.Enum_CouponCodeType.Category).ToString())
            {
                dtTemp = objCouponCodeProduct.GetReferenceIDByCouponID(objDataTable.Rows[0][tblCouponCode.ColumnNames.AppCouponCodeID].ToString(), Convert.ToInt32(Enums.Enum_CouponCodeType.Category), hdnPKID.Value);
                objCouponCodeProduct = null;
                if (dtTemp.Rows.Count > 0)
                {
                    lblCouponCode.Text = "Price after discount: " + Convert.ToInt32(Math.Round((decprice - decpricediscount), 0));
                    lblCouponCode.ForeColor = System.Drawing.Color.Green;
                    hdnPriceDiscount.Value = Convert.ToInt32(Math.Round(decpricediscount, 0)).ToString();
                }
                else
                {
                    lblCouponCode.Text = "This coupon code does not exist for this product.";
                    lblCouponCode.ForeColor = System.Drawing.Color.Red;
                    hdnPriceDiscount.Value = "0";
                }
            }
            else if (objDataTable.Rows[0][tblCouponCode.ColumnNames.AppType].ToString() == Convert.ToInt32(Enums.Enum_CouponCodeType.SubCategory).ToString())
            {
                dtTemp = objCouponCodeProduct.GetReferenceIDByCouponID(objDataTable.Rows[0][tblCouponCode.ColumnNames.AppCouponCodeID].ToString(), Convert.ToInt32(Enums.Enum_CouponCodeType.SubCategory), hdnPKID.Value);
                objCouponCodeProduct = null;
                if (dtTemp.Rows.Count > 0)
                {
                    lblCouponCode.Text = "Price after discount: " + Convert.ToInt32(Math.Round((decprice - decpricediscount), 0));
                    lblCouponCode.ForeColor = System.Drawing.Color.Green;
                    hdnPriceDiscount.Value = Convert.ToInt32(Math.Round(decpricediscount, 0)).ToString();
                }
                else
                {
                    lblCouponCode.Text = "This coupon code does not exist for this product.";

                    lblCouponCode.ForeColor = System.Drawing.Color.Red;
                    hdnPriceDiscount.Value = "0";
                }
            }
            else if (objDataTable.Rows[0][tblCouponCode.ColumnNames.AppType].ToString() == Convert.ToInt32(Enums.Enum_CouponCodeType.Product).ToString())
            {
                dtTemp = objCouponCodeProduct.GetReferenceIDByCouponID(objDataTable.Rows[0][tblCouponCode.ColumnNames.AppCouponCodeID].ToString(), Convert.ToInt32(Enums.Enum_CouponCodeType.Product), hdnPKID.Value);
                objCouponCodeProduct = null;
                if (dtTemp.Rows.Count > 0)
                {
                    lblCouponCode.Text = "Price after discount: " + Convert.ToInt32(Math.Round((decprice - decpricediscount), 0));
                    lblCouponCode.ForeColor = System.Drawing.Color.Green;
                    hdnPriceDiscount.Value = Convert.ToInt32(Math.Round(decpricediscount, 0)).ToString();
                }
                else
                {
                    lblCouponCode.Text = "This coupon code does not exist for this product.";
                    lblCouponCode.ForeColor = System.Drawing.Color.Red;
                    hdnPriceDiscount.Value = "0";
                }
            }
            objCouponCodeProduct = null;
            dtTemp = null;
        }
        else
        {
            hdnPriceDiscount.Value = Convert.ToInt32(Math.Round(decpricediscount, 0)).ToString();
            lblCouponCode.Text = "This coupon code does not exist for this product.";
            lblCouponCode.ForeColor = System.Drawing.Color.Red;
        }
    }

    private void LoadPixcelCode()
    {
        string str = "";
        if (hdnPKID.Value != "")
        {
            tblPixcelCode objPixelCode = new tblPixcelCode();
            objDataTable = objPixelCode.GetPixcelCode(hdnPKID.Value);
            if (objDataTable.Rows.Count > 0)
            {
                str += "<script>";
                foreach (DataRow row in objDataTable.Rows)
                {
                    DataRow[] dr = objDataTable.Select();
                    if (dr.Length > 0)
                    {
                        str += row[tblPixcelCode.ColumnNames.AppName].ToString() + ",";
                    }
                }
                str += "</script>";
                str = str.TrimEnd(',');
                Page.Controls.Add(new LiteralControl(str));
            }
            objPixelCode = null;
        }
    }
}