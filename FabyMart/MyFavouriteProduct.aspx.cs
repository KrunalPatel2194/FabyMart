using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;

public partial class MyFavouriteProduct : PageBase_Client
{
    public PageBase objPageBase = new PageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack) {
            if ((Session[appFunctions.Session.ClientUserID.ToString()] != null))
            {
                if (string.IsNullOrEmpty(Session[appFunctions.Session.ClientUserName.ToString()].ToString()) | Session[appFunctions.Session.ClientUserID.ToString()].ToString() == "0")
                {
                    Response.Redirect(GetAlias("Login.aspx") + "WishList");
                }
            }
            else
            {
                Response.Redirect(GetAlias("Login.aspx") + "WishList");
            }
            SetUpPageContent(ref metaDescription, ref metaKeywords);
            LoadFavouriteProduct();
        }

    }
    public void LoadFavouriteProduct()
    {
        tblFavouriteProduct objFavouriteProduct = new tblFavouriteProduct();

        dgvFavourite.DataSource = objFavouriteProduct.LoadFavouriteProduct(Session[appFunctions.Session.ClientUserID.ToString()].ToString());
        dgvFavourite.DataBind();
        objFavouriteProduct = null;
    }

    protected void dgvFavourite_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            tblFavouriteProduct objFavouriteProduct = new tblFavouriteProduct();
            if (objFavouriteProduct.LoadByPrimaryKey(Convert.ToInt32(e.CommandArgument.ToString())))
            {
                objFavouriteProduct.MarkAsDeleted();
                objFavouriteProduct.Save();
            }
            
            objFavouriteProduct.Where.AppCustomerID.Value = Session[appFunctions.Session.ClientUserID.ToString()].ToString();
            objFavouriteProduct.Query.Load();
            ((Label)Master.FindControl("lblWishCount")).Text = "(" + objFavouriteProduct.RowCount.ToString() + ")";
            objFavouriteProduct = null;

            LoadFavouriteProduct();
        }
    }
    protected void dgvFavourite_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}