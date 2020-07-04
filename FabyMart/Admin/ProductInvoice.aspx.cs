using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;

public partial class ProductInvoice : PageBase_Admin
{
    tblSubOrder objSubOrder;
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Request.QueryString.Get("ID") != null))
            {
                string strId = "";
                objEncrypt = new clsEncryption();
                try
                {
                    // strId = objEncrypt.Decrypt(Request.QueryString.Get("ID"), appFunctions.strKey);
                    strId = Request.QueryString.Get("ID");
                    if (strId != "")
                    {
                        objSubOrder = new tblSubOrder();
                        objDataTable = objSubOrder.GetSubOrderListWithCityStateCountry(strId.TrimEnd(',').Trim(), Convert.ToInt32(Enums.Enums_OrderStatus.Confirmed).ToString(), appFunctions.strSellerName, appFunctions.strMobileNo, appFunctions.strAddress, appFunctions.strCountry, appFunctions.strState, appFunctions.strCity, appFunctions.strPinColde);
                        if (objDataTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in objDataTable.Rows)
                            {
                                row[tblOrder.ColumnNames.AppCreatedDate] = GetDateTime().ToString("dd-MMM-yyyy | hh:mm ") +"hrs";
                            }
                        }
                        
                        dtProductInvoice.DataSource = objDataTable;
                        dtProductInvoice.DataBind();
                        objSubOrder.SetProductInvoiceGenerated(Convert.ToInt32(Enums.Enums_OrderStatus.Confirmed).ToString(),strId.TrimEnd(',').Trim());
                        objSubOrder = null;
                    }
                   
                   
                
                }
                catch (Exception ex)
                {
                    Response.Write(ex.StackTrace.ToString());
                }
                objEncrypt = null;
            }
        }
    }

    protected void dtProductInvoice_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string strAddress = appFunctions.strSellerName.ToString() + "<br/>" + appFunctions.strAddress.ToString() + "<br/>" + appFunctions.strCity.ToString() + ", " + appFunctions.strState.ToString() + ", " + appFunctions.strCountry.ToString() + " - " + appFunctions.strPinColde.ToString() + "<br/><b>(" + appFunctions.strPinColde.ToString() + ")</b><br/>";
            
            string strSellerOrderID = dtProductInvoice.DataKeys[e.Item.ItemIndex].ToString();
            Repeater dgvLeft = (Repeater)e.Item.FindControl("dgvLeftGridView");
            Repeater dgvRIght = (Repeater)e.Item.FindControl("dgvRightgrid");

            if (strSellerOrderID != "" && dgvLeft != null && dgvRIght != null)
            {

                tblSubOrder objSubOrder = new tblSubOrder();
                objDataTable = objSubOrder.GetSubOrderDetailList(strSellerOrderID, Convert.ToInt32(Enums.Enums_OrderStatus.Confirmed).ToString());
                dgvLeft.DataSource = objDataTable;
                dgvLeft.DataBind();
                dgvRIght.DataSource = objDataTable;
                dgvRIght.DataBind();
                string strTotal = objDataTable.Compute("sum(appTotal)", "").ToString();
               // Label lblLeftOrdeTotal = (Label)e.Item.FindControl("lblLeftOrdeTotal"); lblLeftOrdeTotal.Text = strTotal;
                
                //Label lblLeftCODCharges = (Label)e.Item.FindControl("lblLeftCODCharges"); lblLeftCODCharges.Text = "0";
               // Label lblLeftShippingCharges = (Label)e.Item.FindControl("lblLeftShippingCharges"); lblLeftShippingCharges.Text = "0";
              
                //  Label lblLeftAmount = (Label)e.Item.FindControl("lblLeftAmount"); lblLeftAmount.Text = strTotal;
                Label lblRightTotal = (Label)e.Item.FindControl("lblRightTotal"); lblRightTotal.Text = strTotal;
                Label lblRightSubTotal = (Label)e.Item.FindControl("lblRightSubTotal"); lblRightSubTotal.Text = strTotal;

                if(objDataTable.Rows.Count>0)
                {
                    Label lblWeight = (Label)e.Item.FindControl("lblWeight");
                    lblWeight.Text = objDataTable.Compute("sum(appWeight)", "").ToString();

                    Label lblTotalQty = (Label)e.Item.FindControl("lblTotalQty");
                    lblTotalQty.Text = objDataTable.Compute("sum(appQty)", "").ToString();

                    Label lblAmount = (Label)e.Item.FindControl("lblAmount");
                    lblAmount.Text = objDataTable.Compute("sum(appSellingPrice)", "").ToString();

                    Label lblTotalAmount = (Label)e.Item.FindControl("lblTotalAmount");
                    lblTotalAmount.Text = objDataTable.Compute("sum(appTotal)", "").ToString();
                }
                

                
               // Label lblRightAmount = (Label)e.Item.FindControl("lblRightAmount"); lblRightAmount.Text = strTotal;
                Label lblSellerAddress = (Label)e.Item.FindControl("lblSellerAddress");
                lblSellerAddress.Text = strAddress;
                Label lblAmountInWords = (Label)e.Item.FindControl("lblAmountInWords");
                RupeeIntoWord objRupeeIntoWord = new RupeeIntoWord();

                lblAmountInWords.Text = "AMOUNT IN WORDS :" + objRupeeIntoWord.AmtInWord(Convert.ToDecimal(lblRightTotal.Text)) + "<br/>";
                objRupeeIntoWord = null;
                objSubOrder = null;

            }
        }
    }
}