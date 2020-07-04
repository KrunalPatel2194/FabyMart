using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;

public partial class ProductSlip : PageBase_Client
{
    tblReturnOrder objReturnOrder;
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string strId = "";
                objEncrypt = new clsEncryption();
                objCommon = new clsCommon();
                strId = objCommon.GetParameterFromUrl(Enums.Enums_URLParameterType.ByNumber, "1");
                strId = objEncrypt.Decrypt(strId, appFunctions.strKey);
                objEncrypt = null;

                if (strId != "")
                {
                    objReturnOrder = new tblReturnOrder();
                    objDataTable = objReturnOrder.GetReturnOrderListWithCityStateCountry(strId, Convert.ToInt32(Enums.Enum_ReturnStatus.Slip).ToString(),appFunctions.strSellerName,appFunctions.strMobileNo,appFunctions.strAddress,appFunctions.strCountry ,appFunctions.strState,appFunctions.strCity,appFunctions.strPinColde);

                    if (objDataTable.Rows.Count > 0)
                    {
                        printButton.Visible = true;
                    }
                    else
                    {
                        printButton.Visible = false;
                    }
                    dtProductInvoice.DataSource = objDataTable;
                    dtProductInvoice.DataBind();
                    //  objSubOrder.SetProductInvoiceGenerated(Convert.ToInt32(Enums.Enums_OrderStatus.Confirmed).ToString(),strId.TrimEnd(',').Trim());
                    objReturnOrder = null;
                }
                else
                {
                    printButton.Visible = false;
                }

            }
            catch (Exception ex)
            {
                printButton.Visible = false;
                Response.Write(ex.StackTrace.ToString());
            }
        }
    }

    protected void dtProductInvoice_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string strReturnOrderId = dtProductInvoice.DataKeys[e.Item.ItemIndex].ToString();
            GridView dgvLeft = (GridView)e.Item.FindControl("dgvLeftGridView");
            GridView dgvRIght = (GridView)e.Item.FindControl("dgvRightgrid");

            if (strReturnOrderId != "" && dgvLeft != null && dgvRIght != null)
            {

                objReturnOrder = new tblReturnOrder();
                objDataTable = objReturnOrder.LoadReturnOrderInvoice(strReturnOrderId);
                dgvLeft.DataSource = objDataTable;
                dgvLeft.DataBind();
                dgvRIght.DataSource = objDataTable;
                dgvRIght.DataBind();

                string strTotal = objDataTable.Compute("sum(appTotal)", "").ToString();
                Label lblLeftOrdeTotal = (Label)e.Item.FindControl("lblLeftOrdeTotal"); lblLeftOrdeTotal.Text = strTotal;
                Label lblLeftCODCharges = (Label)e.Item.FindControl("lblLeftCODCharges"); lblLeftCODCharges.Text = "0";
                Label lblLeftShippingCharges = (Label)e.Item.FindControl("lblLeftShippingCharges"); lblLeftShippingCharges.Text = "0";
                Label lblLeftAmount = (Label)e.Item.FindControl("lblLeftAmount"); lblLeftAmount.Text = strTotal;
                Label lblRightTotal = (Label)e.Item.FindControl("lblRightTotal"); lblRightTotal.Text = strTotal;
                Label lblRightAmount = (Label)e.Item.FindControl("lblRightAmount"); lblRightAmount.Text = strTotal;
                objReturnOrder = null;

            }
        }
    }
}