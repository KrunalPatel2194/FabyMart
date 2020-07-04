using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer;
public partial class Main : System.Web.UI.MasterPage
{
    public PageBase objPageBase = new PageBase();
    public string strServerURL = PageBase.GetServerURL() + "/";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //LoadCategoryMenu();

            clsCommon objCommon = new clsCommon();
            objCommon.FillDropDownListWithOutDefaultValue(ddlCurrency, "tblCurrency", tblCurrency.ColumnNames.AppCurrency, tblCurrency.ColumnNames.AppCurrencyID, tblCurrency.ColumnNames.AppDisplayOrder, appFunctions.Enum_SortOrderBy.Asc, tblCurrency.ColumnNames.AppIsActive + "=1");
            objCommon = null;
            if (Session[appFunctions.Session.CurrencyID.ToString()] != null)
            {
                if (Session[appFunctions.Session.CurrencyID.ToString()].ToString() != "")
                {
                    ddlCurrency.SelectedValue = Session[appFunctions.Session.CurrencyID.ToString()].ToString();
                }
            }
            if ((Session[appFunctions.Session.ClientUserID.ToString()] != null))
            {
                if (!(string.IsNullOrEmpty(Session[appFunctions.Session.ClientUserName.ToString()].ToString()) | Session[appFunctions.Session.ClientUserID.ToString()].ToString() == "0"))
                {
                    divLogOut.Style.Add("display", "inline");

                    divLogin.Style.Add("display", "none");
                }
            }
            else
            {
                divLogin.Style.Add("display", "inline");
                divLogOut.Style.Add("display", "none");
            }
            if (Session[appFunctions.Session.ClientUserID.ToString()] != null)
            {

                if (Session[appFunctions.Session.ClientUserID.ToString()].ToString() != "")
                {
                    // liAccount.Visible = true;
                    tblFavouriteProduct objFavProduct = new tblFavouriteProduct();
                    objFavProduct.Where.AppCustomerID.Value = Session[appFunctions.Session.ClientUserID.ToString()].ToString();
                    objFavProduct.Query.Load();
                    lblWishCount.Text = "(" + objFavProduct.RowCount.ToString() + ")";
                    objFavProduct = null;
                    lblClientName.Text = "Hi, " + Session[appFunctions.Session.ClientUserName.ToString()].ToString();
                }
                else
                {
                    //  liAccount.Visible = false;
                }
            }
            else
            {
                // liAccount.Visible = false;
            }
            lnkCart.HRef = Request.RawUrl + "#";
            DataTable dtCart = new DataTable();
            if ((HttpContext.Current.Session[appFunctions.Session.Cart.ToString()] != null))
            {
                dtCart = (DataTable)HttpContext.Current.Session[appFunctions.Session.Cart.ToString()];
            }
            if (dtCart.Rows.Count > 0)
            {
                lblProductCount.Visible = true;
                lblProductCount.Text = dtCart.Rows.Count.ToString();
                lnkCart.HRef = objPageBase.GetAlias("Order.aspx");
            }
            else
            {
                lblProductCount.Visible = false;
            }
        }
    }
    //public void LoadCategoryMenu()
    //{
    //    tblCategory objCategory = new tblCategory();
    //   rpCategoryMenu.DataSource =objCategory.LoadAllCategory() ;
    //    rpCategoryMenu.DataBind();
    //    objCategory = null;
    //}
    //protected void rpCategoryMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
    //{
    //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
    //    {
    //        HiddenField sid = (HiddenField)e.Item.FindControl("hdnCategoryID");
    //        string s = sid.Value;
    //        if (!string.IsNullOrEmpty(sid.Value))
    //        {
    //            tblSubCategory objSubCategory = new tblSubCategory();

    //            Repeater rep = (Repeater)e.Item.FindControl("rpSubCategoryMenu");
    //            DataTable objTemp = objSubCategory.LoadAllSubCategoryByCategoryID(s);
    //            if (objTemp.Rows.Count > 0) {
    //                rep.DataSource = objTemp;
    //                rep.DataBind();
    //            }
    //            objTemp = null;
    //            objSubCategory = null;
    //        }
    //    }
    //}
    protected void lnkLogOut_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        objPageBase = new PageBase();
        Response.Redirect(objPageBase.GetAlias("Default.aspx"));
        objPageBase = null;

    }
    protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
    {

        Session[appFunctions.Session.CurrencyID.ToString()] = ddlCurrency.SelectedValue;
        PageBase_Client objClient = new PageBase_Client();
        objClient.SetCurrency();
        objClient = null;
        Response.Redirect(Request.RawUrl.ToString());
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect(objPageBase.GetAlias("SearchProduct.aspx") + objPageBase.generateUrl(txtSearch.Text.Trim()));
    }
}
