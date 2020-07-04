using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Web.Services;
using System.Text;

public partial class SearchProduct : PageBase_Client
{
    public PageBase objPageBase = new PageBase();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SetUpPageContent(ref metaDescription, ref metaKeywords);
            objCommon = new clsCommon();
            hdnProduct.Value = objCommon.GetParameterFromUrl(Enums.Enums_URLParameterType.ByNumber, "1");
            lblLiteral.Text = " / " + hdnProduct.Value;
            this.Page.Title = hdnCategory.Value;
            objCommon = null;

            tblProperty objProperty = new tblProperty();
            RepProperty.DataSource = objProperty.LoadProductWiseProperty(hdnProduct.Value);
            RepProperty.DataBind();
            objProperty = null;

            tblProduct objProduct = new tblProduct();
            decimal iPrice = objProduct.GetProductNameWiseMaxPrice(hdnProduct.Value);
            objProduct = null;
            iPrice = Math.Round(Convert.ToDecimal(HttpContext.Current.Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString()) * iPrice, 0) + 100;
            hdnMaxPrice.Value = iPrice.ToString();

            dtColor.DataSource = null;
            dtColor.DataBind();
            tblColor objColor = new tblColor();
            objColor.Where.AppIsActive.Value = true;
            objColor.Query.AddOrderBy(tblColor.ColumnNames.AppDisplayOrder, MyGeneration.dOOdads.WhereParameter.Dir.ASC);
            objColor.Query.Load();
            if (objColor.RowCount > 0)
            {
                if (objColor.RowCount == 1)
                {
                    if (objColor.s_AppIsDefault != "")
                    {
                        if (objColor.AppIsDefault)
                        {
                            Seccolor.Style.Add("display", "none");
                        }
                    }
                }
                dtColor.DataSource = objColor.DefaultView.Table; ;
                dtColor.DataBind();
            }
            objColor = null;
            hdnOrderBy.Value = " vw_SerachProduct.appProductId Desc";

        }
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
            ((Label)Master.FindControl("lblProductCount")).Text = dtCart.Rows.Count.ToString();
            ((System.Web.UI.HtmlControls.HtmlAnchor)Master.FindControl("lnkCart")).HRef = GetAlias("Order.aspx");
        }
        else
        {
            ((System.Web.UI.HtmlControls.HtmlAnchor)Master.FindControl("lnkCart")).HRef = "#";
        }
    }
    protected void RepProperty_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HiddenField hdn = (HiddenField)e.Item.FindControl("hdnPkId");
            if (hdn != null)
            {
                tblPropertyPreValue objValue = new tblPropertyPreValue();
                objDataTable = objValue.LoadProductPropertyValue(hdn.Value, hdnProduct.Value);
                if (objDataTable.Rows.Count > 0)
                {
                    Repeater Rep = (Repeater)e.Item.FindControl("RepPropertyValue");
                    Rep.DataSource = objDataTable;
                    Rep.DataBind();
                }
                objValue = null;
            }
        }

    }
    protected void chkValue_CheckedChanged(object sender, EventArgs e)
    {
        if (hdnTemp.Value != "")
        {
            string strIds = hdnPropertyIds.Value;
            strIds = "," + strIds + ",";
            if (strIds.Contains(hdnTemp.Value))
            {
                strIds = strIds.Replace(hdnTemp.Value + ",", " ");
            }
            else
            {
                strIds += hdnTemp.Value;
            }
            hdnPropertyIds.Value = strIds.Trim().Trim(',');
            hdnPropertyIds.Value = hdnPropertyIds.Value.TrimStart(',');
            hdnPropertyIds.Value = hdnPropertyIds.Value.TrimEnd(',');
            hdnPropertyIds.Value = hdnPropertyIds.Value.Trim();

            hdnTemp.Value = "";
            //  LoadData();
        }
    }

    [WebMethod]
    public static string LoadProductCount(string strCategory, string strSubCategory, string strProperty, string strColor, string strSize, string strOrderBy, string strProduct, string strPrice)
    {
        string strMinPrice = "";
        string strMaxPrice = "";
        if (strPrice != "")
        {
            string[] strvalue = strPrice.Split(';');
            strMinPrice = strvalue[0];
            strMaxPrice = strvalue[1];
        }
        string strRate = "1";
        //if (HttpContext.Current.Session[appFunctions.Session.CurrencyInRupee.ToString()] != null)
        //{
        //    if (HttpContext.Current.Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString() != "")
        //    {
        //        strRate = HttpContext.Current.Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString();
        //    }
        //}
        tblProduct objProduct = new tblProduct();
        return objProduct.LoadAllProductCount(strCategory, strSubCategory, strProperty, strColor, strSize, strOrderBy, strMinPrice, strMaxPrice, strRate, strProduct);
    }
    //[WebMethod]
    //public static string LoadProductCount(string strCategory, string strSubCategory, string strProperty, string strColor, string strSize, string strOrderBy)
    //{
    //    tblProduct objProduct = new tblProduct();
    //    return objProduct.LoadAllProductCount(strCategory, strSubCategory, strProperty, strColor, strSize, strOrderBy);
    //}
    [WebMethod]
    public static string LoadProductData(string strPageSize, string strPageIndex, string strCategory, string strSubCategory, string strProperty, string strColor, string strSize, string strOrderBy, string strProduct, string strPrice)
    {
        string strMinPrice = "";
        string strMaxPrice = "";
        if (strPrice != "")
        {
            string[] strvalue = strPrice.Split(';');
            strMinPrice = strvalue[0];
            strMaxPrice = strvalue[1];
        }
        string strRate = "1";
        //if (HttpContext.Current.Session[appFunctions.Session.CurrencyInRupee.ToString()] != null)
        //{
        //    if (HttpContext.Current.Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString() != "")
        //    {
        //        strRate = HttpContext.Current.Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString();
        //    }
        //}
        tblProduct objProduct = new tblProduct();
        DataTable objDataTable = objProduct.LoadAllProductPagingWise(strPageSize, strPageIndex, strCategory, strSubCategory, strProperty, strColor, strSize, strOrderBy, strProduct, strMinPrice, strMaxPrice, strRate);
        objProduct = null;

        StringBuilder strdiv = new StringBuilder();
        if (objDataTable.Rows.Count > 0)
        {
            PageBase objPage = new PageBase();
            string strPageName = objPage.GetAlias("ProductDetail.aspx");
            //lblTotalProduct.text = "";
            //  int i = 0;
            foreach (DataRow row in objDataTable.Rows)
            {
                strdiv.Append("  <div class='col_1_of_3 span_1_of_3'>");

                if (strSubCategory != "")
                {
                    strdiv.Append("<a href='" + strPageName + objPage.generateUrl(row[tblProduct.ColumnNames.AppProductName].ToString()) + "'>");
                }
                else
                {
                    strdiv.Append("<a href='" + strPageName + objPage.generateUrl(row[tblProduct.ColumnNames.AppProductName].ToString()) + "'>");
                }

                strdiv.Append("<div class='inner_content clearfix'>");
                strdiv.Append("<div class='product_image'>");
                if (Convert.ToInt32(row["appoff"].ToString()) == 0)
                {
                    strdiv.Append("<span class='discount' style='display:none'>" + row["appoff"].ToString() + "%&nbsp;<span>Off</span></span>");
                }
                else
                {
                    strdiv.Append("<span class='discount' style='display:block'>" + row["appoff"].ToString() + "%&nbsp;<span>Off</span></span>");
                }
                strdiv.Append("<img src='" + GetServerURL() + "/admin/" + row[tblProductImage.ColumnNames.AppNormalImage].ToString() + "' alt='' />");
                strdiv.Append("</div>");
                //strdiv.Append(" <div class='sale-box'>");
                //strdiv.Append(" <span class='on_sale title_shop'>New</span></div>");
                //strdiv.Append("</div>");
                strdiv.Append("<div class='price'>");
                strdiv.Append("<p class='title'>");
                strdiv.Append(row[tblProduct.ColumnNames.AppProductName].ToString());
                strdiv.Append("</p>");
                strdiv.Append("<div class='cart-left'>");

                strdiv.Append("<div class='price1'>");
                strdiv.Append(" <span class='actual'>");
                strdiv.Append(HttpContext.Current.Session[appFunctions.Session.CurrencyImage.ToString()].ToString() + Math.Round(Convert.ToDecimal(HttpContext.Current.Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString()) * Convert.ToDecimal(row[tblProductDetail.ColumnNames.AppPrice].ToString()), 0).ToString());
                strdiv.Append("</span>");
                if (Convert.ToInt32(row["appoff"].ToString()) == 0)
                {
                    strdiv.Append("<span class='priceMiddle' style='display:none'>");
                }
                else
                {
                    strdiv.Append("<span class='priceMiddle' style='display:block'>");
                }
                strdiv.Append("<strike><span>");
                strdiv.Append(HttpContext.Current.Session[appFunctions.Session.CurrencyImage.ToString()].ToString() + Math.Round(Convert.ToDecimal(HttpContext.Current.Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString()) * Convert.ToDecimal(row[tblProductDetail.ColumnNames.AppMRP].ToString()), 0).ToString());
                strdiv.Append("</span></strike></span>");
                strdiv.Append("</div>");
                strdiv.Append("</div>");
                //  strdiv.Append("<div class='cart-right'>");
                //   strdiv.Append(" Add To Cart</div>");
                strdiv.Append("<a href='javascript:void(0);' Class='cart-right' onclick='AddToCart(" + row[tblProductDetail.ColumnNames.AppProductDetailID].ToString() + "," + row[tblProduct.ColumnNames.AppProductID].ToString() + "," + row[tblProductColor.ColumnNames.AppProductColorID].ToString() + "," + row[tblProductDetail.ColumnNames.AppSizeID].ToString() + ")'>Add To Cart</a>");

                strdiv.Append("<div class='clear'>");
                strdiv.Append("</div>");
                strdiv.Append("</div>");
                strdiv.Append("</div>");
                strdiv.Append("</a>");
                strdiv.Append("</div>");
                //strdiv.Append(" <div class='content_box'>");
                //strdiv.Append(" <a href='" + strPageName + objPage.generateUrl(row[tblProduct.ColumnNames.AppProductName].ToString()) + "'>");
                //strdiv.Append(" <div class='view view-fifth'>");
                //strdiv.Append("   <img src='" + GetServerURL() + "/admin/" + row[tblProductImage.ColumnNames.AppNormalImage].ToString() + "' class='img-responsive' alt='" + row[tblProduct.ColumnNames.AppProductName].ToString() + "' />");
                //strdiv.Append("</div>");
                //strdiv.Append("</a>");
                //strdiv.Append("</div>");
                //strdiv.Append("<h4>");
                //strdiv.Append(" <a href='" + strPageName + objPage.generateUrl(row[tblProduct.ColumnNames.AppProductName].ToString()) + "'>");
                //strdiv.Append(row[tblProduct.ColumnNames.AppProductName].ToString());
                //strdiv.Append("</a>");
                //strdiv.Append("</h4>");
                //strdiv.Append(HttpContext.Current.Session[appFunctions.Session.CurrencyImage.ToString()].ToString() + Math.Round(Convert.ToDecimal(HttpContext.Current.Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString()) * Convert.ToDecimal(row[tblProductDetail.ColumnNames.AppPrice].ToString()), 2).ToString());
                //strdiv.Append("</div>");
                //i++;
                //if (i == 4)
                //{
                //    strdiv.Append("<div class='clear'>");
                //    strdiv.Append("</div>");
                //    i = 0;
                //}
            }
        }

        return strdiv.ToString();// +"|" + objDataTable.Rows.Count;
    }


    protected void lnkCategory_Click(object sender, EventArgs e)
    {
        //LoadProductData(hdnCategory.Value,"",
    }
    protected void btnClick_Click(object sender, EventArgs e)
    {
        if (hdnProductDetailID.Value != "")
        {
            tblProductDetail objProductDetail = new tblProductDetail();
            if (objProductDetail.LoadByPrimaryKey(Convert.ToInt32(hdnProductDetailID.Value)))
            {
                if (objProductDetail.AppQuantity > 0)
                {
                    objCommon = new clsCommon();
                    objCommon.AddToCart(hdnPKID.Value, hdnProductColorId.Value, hdnProductDetailID.Value, 1);
                    //objCommon.AddToCart(hdnPKID.Value, objProductDetail.s_AppProductColorID, objProductDetail.s_AppSizeID, 1, hdnPriceDiscount.Value);
                    objCommon = null;
                    //txtQty.Text = "1";
                    SetCartProductCount();
                    ViewCart.LoadProduct();
                }
                else
                {
                    //  DInfo.ShowMessage("Product out of stock.", Enums.MessageType.Error);
                }
            }
            else
            {
                //  DInfo.ShowMessage("Product out of stock.", Enums.MessageType.Error);
            }
            objProductDetail = null;

        }
        else
        {
            // DInfo.ShowMessage("Product out of stock.", Enums.MessageType.Error);
        }
    }
}