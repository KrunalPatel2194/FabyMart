using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Web.Services;
public partial class _Default : PageBase_Client
{
    //public string strServerURL = PageBase.GetServerURL() + "/";
    tblFeaturedProduct objFeaturedProduct;
    tblBanner objBanner;
    tblNewArrival objNewArrival;
    tblBestSeller objBestSeller;
    tblDeal objDeal;
    public PageBase objPageBase = new PageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            //if (Request.Cookies.Get("FabyMartUsername") == null)
            //{
            //    if (Request.Cookies.Get("IsFirstTime") == null)
            //    {
            //        ViewRegistration.ShowRegistration(true);
            //    }
            //}
            if ((Session[appFunctions.Session.ClientUserID.ToString()] != null))
            {
                if (string.IsNullOrEmpty(Session[appFunctions.Session.ClientUserName.ToString()].ToString()) | Session[appFunctions.Session.ClientUserID.ToString()].ToString() == "0")
                {
                    ViewRegistration.ShowRegistration(true);
                }
            }
            else
            {
                ViewRegistration.ShowRegistration(true);
            }
            SetUpPageContent(ref metaDescription, ref metaKeywords);
            SetRegulerExpression();
            //if (Request.CurrentExecutionFilePath.ToLower().Contains("default.aspx") && Request.FilePath.ToLower().Contains("default.aspx"))
            //{

            //    tblPage objPage = new tblPage();
            //    objPage.Where.AppIsDefault.Value = true;
            //    objPage.Query.Load();
            //    if (objPage.RowCount > 0)
            //    {
            //        Response.Redirect(objPage.AppAlias);
            //    }
            //}
            LoadBanner();
            LoadHighLight();
            LoadFeaturedProduct();
            LoadNewPoducts();
            LoadBestSeller();
            DisplayTranding();
            LoadBestDeal();
        }
    }

    public void SetRegulerExpression()
    {
        revEmailRegistration.ValidationExpression = RXEmailRegularExpression;
        revEmailRegistration.ErrorMessage = "Invalid Email (" + RXEmailRegularExpressionMsg + ")";

    }

    public void LoadBestDeal()
    {
        objDeal = new tblDeal();
        objDataTable = objDeal.LoadBestDeal();
        dicBestDeal.Style.Add("display", "block");
        divDeal.Visible = true;
        if (objDataTable.Rows.Count <= 0)
        {
            dicBestDeal.Visible = false;
            divDeal.Visible = false;
        }
        rptBestDeal.DataSource = objDataTable;
        rptBestDeal.DataBind();
        objDeal = null;
    }

    public void LoadBanner()
    {
        objBanner = new tblBanner();
        rpBanner.DataSource = objBanner.LoadBanner();
        rpBanner.DataBind();
        objBanner = null;
    }

    public void LoadHighLight()
    {
        tblHighLight objHighLight = new tblHighLight();
        objDataTable = objHighLight.LoadHighLight("3");
        //RepHightLight.DataSource = objDataTable;
        //RepHightLight.DataBind();
        objHighLight = null;

        StringBuilder strBulider = new StringBuilder();
        strBulider.Append("");
        if (objDataTable.Rows.Count > 0)
        {
            int i = 1;
            foreach (DataRow row in objDataTable.Rows)
            {
                if (i == 1)
                {
                    strBulider.Append("  <div class=\"banner-bottom-grids\">");
                }

                strBulider.Append(" <div class=\"col-md-4 ");
                if (!(i % 2 == 0))
                {
                    strBulider.Append("first");
                }
                strBulider.Append(" bottom-grid\" >");
                //  strBulider.Append("<a href='" + row[tblHighLight.ColumnNames.AppUrl].ToString() != "" ? row[tblHighLight.ColumnNames.AppUrl].ToString() : "#" + "' >");
                strBulider.Append(" <img src=\"" + strServerURL + "admin/" + row[tblHighLight.ColumnNames.AppImage].ToString() + "\" alt=\"" + row[tblHighLight.ColumnNames.AppTitle].ToString() + "\" />");

                strBulider.Append("<div class=\"bottom-grid-info\">");
                strBulider.Append("<a href='");

                if (row[tblHighLight.ColumnNames.AppUrl].ToString() != "")
                {
                    strBulider.Append(row[tblHighLight.ColumnNames.AppUrl].ToString());
                }
                else
                {
                    strBulider.Append("#");
                }
                strBulider.Append("' >" + row[tblHighLight.ColumnNames.AppTitle].ToString() + " </a>");

                strBulider.Append(" </div>");
                //   strBulider.Append(" </a> ");
                strBulider.Append("</div>");

                i += 1;
                if (i == 4)
                {
                    strBulider.Append("<div class=\"clear\">");
                    strBulider.Append("</div>");
                    strBulider.Append("</div>");
                    i = 0;
                }


            }
        }
        ltHighLight.Text = strBulider.ToString();
    }

    public void LoadFeaturedProduct()
    {
        objFeaturedProduct = new tblFeaturedProduct();
        objDataTable = objFeaturedProduct.LoadFeaturedProduct("8");
        divFeatured.Visible = true;
        if (objDataTable.Rows.Count <= 0)
        {
            divFeatured.Visible = false;
        }
        rpFeaturedProduct.DataSource = objDataTable;
        rpFeaturedProduct.DataBind();
        objFeaturedProduct = null;
    }

    public void LoadNewPoducts()
    {
        objNewArrival = new tblNewArrival();
        objDataTable = objNewArrival.LoadNewArrival("8");
        divNewProducts.Visible = true;
        if (objDataTable.Rows.Count <= 0)
        {
            divNewProducts.Visible = false;
        }
        rpNewProduct.DataSource = objDataTable;
        rpNewProduct.DataBind();
        objNewArrival = null;
    }

    public void LoadBestSeller()
    {
        objBestSeller = new tblBestSeller();
        objDataTable = objBestSeller.LoadBestSeller("8");
        divBestSeller.Visible = true;
        if (objDataTable.Rows.Count <= 0)
        {
            divBestSeller.Visible = false;
        }
        rpBestSeller.DataSource = objDataTable;
        rpBestSeller.DataBind();
        objBestSeller = null;
    }

    public void rptBestDeal_ItemDataBound(object Sender, RepeaterCommandEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
        {

            //Label lblMRP = (Label)e.Item.FindControl("lblMRP");
            //lblMRP.Text=
            //Label lblDiscount = (Label)e.Item.FindControl("lblDiscount");

            //Label lblSaveRupee = (Label)e.Item.FindControl("lblSaveRupee");

        }
    }

    public void rptBestDeal_ItemCommand(Object Sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Add To Cart")
        {
            if (e.CommandArgument.ToString() != "")
            {
                tblProductDetail objProductDetail = new tblProductDetail();
                objDataTable = objProductDetail.LoadProductIDs(e.CommandArgument.ToString());
                if (objDataTable.Rows.Count > 0)
                {
                    if (Convert.ToInt32(objDataTable.Rows[0][tblProductDetail.ColumnNames.AppQuantity].ToString()) > 0)
                    {
                        objCommon = new clsCommon();
                        objCommon.AddToCart(objDataTable.Rows[0][tblProductColor.ColumnNames.AppProductID].ToString(), objDataTable.Rows[0][tblProductDetail.ColumnNames.AppProductColorID].ToString(), e.CommandArgument.ToString(), 1);
                        // objCommon.AddToCart(objDataTable.Rows[0][tblProductColor.ColumnNames.AppProductID].ToString(), objDataTable.Rows[0][tblProductDetail.ColumnNames.AppProductColorID].ToString(), e.CommandArgument.ToString());
                        objCommon = null;
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
    }

    public void rpFeaturedProduct_ItemCommand(Object Sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Add To Cart")
        {
            if (e.CommandArgument.ToString() != "")
            {
                tblProductDetail objProductDetail = new tblProductDetail();
                objDataTable = objProductDetail.LoadProductIDs(e.CommandArgument.ToString());
                if (objDataTable.Rows.Count > 0)
                {
                    if (Convert.ToInt32(objDataTable.Rows[0][tblProductDetail.ColumnNames.AppQuantity].ToString()) > 0)
                    {
                        objCommon = new clsCommon();
                        objCommon.AddToCart(objDataTable.Rows[0][tblProductColor.ColumnNames.AppProductID].ToString(), objDataTable.Rows[0][tblProductDetail.ColumnNames.AppProductColorID].ToString(), e.CommandArgument.ToString(), 1);
                        // objCommon.AddToCart(objDataTable.Rows[0][tblProductColor.ColumnNames.AppProductID].ToString(), objDataTable.Rows[0][tblProductDetail.ColumnNames.AppProductColorID].ToString(), e.CommandArgument.ToString());
                        objCommon = null;
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
    }

    public void rpBestSeller_ItemCommand(Object Sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Add To Cart")
        {
            if (e.CommandArgument.ToString() != "")
            {
                tblProductDetail objProductDetail = new tblProductDetail();
                objDataTable = objProductDetail.LoadProductIDs(e.CommandArgument.ToString());
                if (objDataTable.Rows.Count > 0)
                {
                    if (Convert.ToInt32(objDataTable.Rows[0][tblProductDetail.ColumnNames.AppQuantity].ToString()) > 0)
                    {
                        objCommon = new clsCommon();
                        objCommon.AddToCart(objDataTable.Rows[0][tblProductColor.ColumnNames.AppProductID].ToString(), objDataTable.Rows[0][tblProductDetail.ColumnNames.AppProductColorID].ToString(), e.CommandArgument.ToString(), 1);
                        //objCommon.AddToCart(objDataTable.Rows[0][tblProductColor.ColumnNames.AppProductID].ToString(), objDataTable.Rows[0][tblProductDetail.ColumnNames.AppProductColorID].ToString(), e.CommandArgument.ToString());
                        objCommon = null;
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
    }

    public void rpNewProduct_ItemCommand(Object Sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Add To Cart")
        {
            if (e.CommandArgument.ToString() != "")
            {
                tblProductDetail objProductDetail = new tblProductDetail();
                objDataTable = objProductDetail.LoadProductIDs(e.CommandArgument.ToString());
                if (objDataTable.Rows.Count > 0)
                {
                    if (Convert.ToInt32(objDataTable.Rows[0][tblProductDetail.ColumnNames.AppQuantity].ToString()) > 0)
                    {
                        objCommon = new clsCommon();
                        //objCommon.AddToCart(objDataTable.Rows[0][tblProductColor.ColumnNames.AppProductID].ToString(), objDataTable.Rows[0][tblProductDetail.ColumnNames.AppProductColorID].ToString(), e.CommandArgument.ToString());
                        objCommon.AddToCart(objDataTable.Rows[0][tblProductColor.ColumnNames.AppProductID].ToString(), objDataTable.Rows[0][tblProductDetail.ColumnNames.AppProductColorID].ToString(), e.CommandArgument.ToString(), 1);
                        objCommon = null;
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
            ((System.Web.UI.HtmlControls.HtmlAnchor)Master.FindControl("lnkCart")).HRef = "#";
        }
    }

    public void DisplayTranding()
    {
        tblTrending objTrending = new tblTrending();
        rpTranding.DataSource = objTrending.LoadTranding();
        rpTranding.DataBind();
        objTrending = null;
    }

    [WebMethod]
    public static string LoadCartProducts()
    {
        DataTable dtCart = new DataTable();
        StringBuilder strdiv = new StringBuilder();
        PageBase objPage = new PageBase();
        string strPageName = objPage.GetAlias("ProductDetail.aspx");

        if ((HttpContext.Current.Session[appFunctions.Session.Cart.ToString()] != null))
        {
            dtCart = (DataTable)HttpContext.Current.Session[appFunctions.Session.Cart.ToString()];
        }


        if (dtCart.Rows.Count > 0)
        {
            foreach (DataRow row in dtCart.Rows)
            {
                strdiv.Append("  <div class='col_1_of_3 span_3_of_3' style='width: 100%; '>");
                strdiv.Append("<a href='" + strPageName + objPage.generateUrl(row[tblProduct.ColumnNames.AppProductName].ToString()) + "'>");

                strdiv.Append("<div class='inner_content clearfix'>");
                strdiv.Append("<div class='float-lt product1_imageForCart'>");
                strdiv.Append("<img src='" + GetServerURL() + "/admin/" + row[tblProductImage.ColumnNames.AppNormalImage].ToString() + "' alt='' />");
                strdiv.Append("</div>");
                strdiv.Append("<div class='priceABC'>");
                strdiv.Append("<p class='float-lt title1'>");
                strdiv.Append(row[tblProduct.ColumnNames.AppProductName].ToString());
                //strdiv.Append("</a>");
                strdiv.Append("</p>");
                strdiv.Append("<div class='cart-left'>");


                strdiv.Append("</div>");

                strdiv.Append("<div class='clear'>");
                strdiv.Append("</div>");
                strdiv.Append("</div>");

                strdiv.Append("<div class='price1'>");
                strdiv.Append(" <span class='actual2'>");
                strdiv.Append(HttpContext.Current.Session[appFunctions.Session.CurrencyImage.ToString()].ToString() + Math.Round(Convert.ToDecimal(HttpContext.Current.Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString()) * Convert.ToDecimal(row["appTotalPrice"].ToString()), 2).ToString());
                strdiv.Append("</span>");

                strdiv.Append("</div>");
                strdiv.Append("</div>");
                strdiv.Append("</a>");
                strdiv.Append("<div class=\"hrInCart\"></div>");
                strdiv.Append("</div>");

            }

        }
        else
        {
            strdiv.Append("Your cart is empty");

        }

        return strdiv.ToString();
    }
    protected void btnSubScribe_Click(object sender, EventArgs e)
    {
        if (SaveSubscriber())
        {

            DisplayInfoSubScribe.ShowMessage("Please check your email account for subscription detail.", Enums.MessageType.Successfull);
            txtUserEmail.Text = "";
        }
    }
    public bool SaveSubscriber()
    {
        objCommon = new clsCommon();
        if (objCommon.IsRecordExists("tblSubscribe", tblSubscribe.ColumnNames.AppEmail, tblSubscribe.ColumnNames.AppSubscribeID, txtUserEmail.Text))
        {
            DisplayInfoSubScribe.ShowMessage("Email address already exist", Enums.MessageType.Error);
            return false;
        }
        objCommon = null;
        tblSubscribe objNewsLetterSubScriber = new tblSubscribe();
        objNewsLetterSubScriber.AddNew();
        objNewsLetterSubScriber.AppEmail = txtUserEmail.Text.Trim();

        objNewsLetterSubScriber.AppIsActive = true;

        objNewsLetterSubScriber.AppCreatedDate = GetDateTime();
        objNewsLetterSubScriber.Save();
        SendMail(objNewsLetterSubScriber.s_AppSubscribeID);


        objNewsLetterSubScriber = null;
        return true;
    }
    public void SendMail(string strId)
    {
        try
        {
            objCommon = new clsCommon();
            string Strbody = "";
            string strSubject = "Subscription Detail";
            Strbody = objCommon.readFile(Server.MapPath("~/EmailTemplates/NewsLetterSubscriber.html"));

            Strbody = Strbody.Replace("`username`", txtUserEmail.Text.Trim());

            objEncrypt = new clsEncryption();
            Strbody = Strbody.Replace("`Link`", PageBase.GetServerURL() + "/Approved.aspx?CID=" + objEncrypt.Encrypt(strId, appFunctions.strKey));
            objEncrypt = null;
            objCommon.SendMail(txtUserEmail.Text, strSubject, Strbody);
            objCommon = null;
        }
        catch (Exception ex)
        {
            Response.Write(ex.StackTrace.ToString());
        }

    }
}