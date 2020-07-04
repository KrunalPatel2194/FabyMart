using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;

public partial class Cart : PageBase_Client
{
    public PageBase objPageBase = new PageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SetUpPageContent(ref metaDescription, ref metaKeywords);
            LoadProduct();
        }
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
            lblTotalPrice.Text = Session[appFunctions.Session.CurrencyImage.ToString()].ToString() + "" + Math.Round(Convert.ToDecimal(Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString()) * Convert.ToDecimal(dtCart.Compute("sum(appTotalPrice)", "").ToString()), 2).ToString();
            // lblTotalPrice.Text = Math.Round(Convert.ToDecimal(Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString()) * Convert.ToDecimal(dtCart.Compute("sum(appTotalPrice)", "").ToString()), 2).ToString();

            ((Label)Master.FindControl("lblProductCount")).Text = dtCart.Rows.Count.ToString();
            ((Label)Master.FindControl("lblProductCount")).Visible = true;
            lblCount.Text = dtCart.Rows.Count.ToString();
            //lblCount1.Text = dtCart.Rows.Count.ToString();
            divProductTotalPrice.Style.Add("display", "block;");

            dgvCart.DataSource = dtCart;
            dgvCart.DataBind();
        }
        else
        {
            ((Label)Master.FindControl("lblProductCount")).Visible = false;
            lblCount.Text = "0";
            divProductTotalPrice.Style.Add("display", "none;");
            Response.Redirect(GetAlias("Default.aspx"));
        }
    }

    protected void btnplaceorder_Click(object sender, EventArgs e)
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
                }
            }
            LoadProduct();
        }
    }

    protected void dgvCart_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void btnUpdateCart_Click(object sender, EventArgs e)
    {
        if (UpdateCart())
        {
            DInfo.ShowMessage("Your cart is successfully updated.", Enums.MessageType.Successfull);
            LoadProduct();
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
        }
        if (StrMsg != "")
        {
            DInfo.ShowMessage(StrMsg, Enums.MessageType.Error);
            return false;
        }

        return true;
    }
}