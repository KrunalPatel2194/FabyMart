using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer;

public partial class ViewCart : ControlBase
{
    public PageBase objPageBase = new PageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

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
            // lblTotalPrice.Text =  dtCart.Compute("sum(appTotalPrice)","").ToString();
            lblTotalPrice.Text = Session[appFunctions.Session.CurrencyImage.ToString()].ToString() + "" + Math.Round(Convert.ToDecimal(Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString()) * Convert.ToDecimal(dtCart.Compute("sum(appTotalPrice)", "").ToString()), 0).ToString();
            //  lblTotalPrice.Text = Math.Round(Convert.ToDecimal(Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString()) * Convert.ToDecimal(dtCart.Compute("sum(appTotalPrice)", "").ToString()), 2).ToString();

            divProductTotalPrice.Style.Add("display", "block");
            dgvCart.DataSource = dtCart;
            dgvCart.DataBind();
        }
        else
        {
            DInfo.ShowMessage("No data found.", BusinessLayer.Enums.MessageType.Information);
            divProductTotalPrice.Style.Add("display", "none");
            MpeCart.Hide();
            Response.Redirect(Request.RawUrl);
        }
        MpeCart.Show();
    }

    protected void btnplaceorder_Click(object sender, EventArgs e)
    {
        if (UpdateCart())
        {
            if ((Session[appFunctions.Session.ClientUserID.ToString()] != null))
            {
                if (string.IsNullOrEmpty(Session[appFunctions.Session.ClientUserName.ToString()].ToString()) | Session[appFunctions.Session.ClientUserID.ToString()].ToString() == "0")
                {
                    MpeCart.Hide();
                    CustLogin.ShowLogin();
                }
                else
                {
                    Response.Redirect(objPageBase.GetAlias("Order.aspx"));
                }
            }
            else
            {
                MpeCart.Hide();
                CustLogin.ShowLogin();
            }
        }
    }

    protected void btnContinueShopping_Click(object sender, EventArgs e)
    {
        if (UpdateCart())
        {
            MpeCart.Hide();
            Response.Redirect(Request.RawUrl);
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

            ((Label)this.Page.Master.FindControl("lblProductCount")).Text = dtCart.Rows.Count.ToString();
        }
        else
        {
            ((Label)this.Page.Master.FindControl("lblProductCount")).Text = "";
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
        MpeCart.Hide();
        Response.Redirect(Request.RawUrl);
    }

    protected void btnUpdateCart_Click(object sender, EventArgs e)
    {
        if (UpdateCart())
        {
            DInfo.ShowMessage("Your cart successfully updated.", Enums.MessageType.Successfull);
            LoadProduct();
        }

    }

    protected void imbtnClose_Click(object sender, EventArgs e)
    {
        MpeCart.Hide();
        Response.Redirect(Request.RawUrl);
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
                                //if (strCouponDiscount == "0")
                                //decimal appRealDiscountPrice = Convert.ToDecimal(dr[0]["appRealDiscountPrice"]);
                                //decimal appTotalDiscount = Convert.ToDecimal(iQty * appRealDiscountPrice);
                                //dr[0]["appDiscountPrice"] = appTotalDiscount;
                                //if (hdnIsInDeal.Value == "0")
                                //{
                                //    dr[0]["appTotalPrice"] = (Convert.ToDecimal(dr[0]["appPrice"].ToString()) * iQty - (appTotalDiscount)).ToString();
                                //}
                                //else
                                //{
                                //    dr[0]["appTotalPrice"] = (Convert.ToDecimal(dr[0]["appRealPrice"].ToString()) * iQty - (appTotalDiscount)).ToString();
                                //}

                                //     dr[0]["appTotalPrice"] = (Convert.ToDecimal(dr[0]["appRealPrice"].ToString()) * iQty).ToString();
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