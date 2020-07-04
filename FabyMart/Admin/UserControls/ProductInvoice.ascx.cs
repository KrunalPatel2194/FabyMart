using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer;
public partial class UserControls_ProductInvoice : ControlBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public void InvoiceDetails(string strInvoice)
    {
        DataTable objDataTable = new DataTable();
        tblSubOrder objSuborder = new tblSubOrder();
        objDataTable = objSuborder.GetProductFullInfo(strInvoice);
        if (objDataTable.Rows.Count > 0)
        {
            spanOrderId.InnerHtml = objDataTable.Rows[0][tblSubOrder.ColumnNames.AppSubOrderNo].ToString();
            spanBillingName.InnerHtml = objDataTable.Rows[0][tblOrder.ColumnNames.AppBillReceiverName].ToString();
            divBillingAddress.InnerHtml = "<b>Address : </b>" + objDataTable.Rows[0][tblOrder.ColumnNames.AppBillReceiverAddress].ToString();
            divBillingMobile.InnerHtml = "<b>Mobile : </b>" + objDataTable.Rows[0][tblOrder.ColumnNames.AppBillReceiverContactNo1].ToString();
            divBillingMobile1.InnerHtml = "<b>Mobile 2 : </b>" + objDataTable.Rows[0][tblOrder.ColumnNames.AppBillReceiverContactNo2].ToString();
            divBillingEmail.InnerHtml = "<b>Email : </b>" + objDataTable.Rows[0][tblOrder.ColumnNames.AppBillRecevierEmail].ToString();


            spanShippingName.InnerHtml = objDataTable.Rows[0][tblOrder.ColumnNames.AppReceiverName].ToString();
            divShippingAddress.InnerHtml = "<b>Address : </b>" + objDataTable.Rows[0][tblOrder.ColumnNames.AppReceiverAddress].ToString();
            divShippingMobile .InnerHtml = "<b>Mobile : </b>" + objDataTable.Rows[0][tblOrder.ColumnNames.AppReceiverContactNo1].ToString();
            divShippingMobile1.InnerHtml = "<b>Mobile 2 : </b>" + objDataTable.Rows[0][tblOrder.ColumnNames.AppReceiverContactNo2].ToString();
            divShippingEmail.InnerHtml = "<b>Email : </b>" + objDataTable.Rows[0][tblOrder.ColumnNames.AppRecevierEmail].ToString();
           // spanGrandTotal.InnerHtml = objDataTable.Compute("sum(appTotal)", "").ToString();

            ImgPhoto.Src = strServerURL + "admin/" + objDataTable.Rows[0][tblProductImage .ColumnNames.AppNormalImage ].ToString();
            spanProduct .InnerHtml = objDataTable.Rows[0][tblProduct .ColumnNames.AppProductName].ToString();
            spanSku.InnerHtml = objDataTable.Rows[0][tblProductDetail .ColumnNames.AppSKUNo].ToString();
            spancolor.InnerHtml = objDataTable.Rows[0][tblColor.ColumnNames.AppColorName].ToString();
            divcolor.Style.Add("background-color", objDataTable.Rows[0][tblColor.ColumnNames.AppColorCode].ToString());
            spanSize.InnerHtml = objDataTable.Rows[0][tblSize .ColumnNames.AppSize ].ToString();
            spanQty.InnerHtml = objDataTable.Rows[0][tblSubOrder.ColumnNames.AppQty].ToString();
            spanPrice.InnerHtml = objDataTable.Rows[0][tblSubOrder.ColumnNames.AppSellingPrice ].ToString();
            spanTotal.InnerHtml = objDataTable.Rows[0]["appTotal"].ToString();
            spanStatus.InnerHtml = objDataTable.Rows[0][tblOrderStatus .ColumnNames.AppOrderStatus ].ToString();
            spanPreferedTime.InnerHtml = objDataTable.Rows[0][tblOrder .ColumnNames.AppPreferedTime].ToString();
        }
        //dgvGridView.DataSource = objDataTable;
        //dgvGridView.DataBind();
        objSuborder = null;
        objDataTable = null;
        mpeProductinvoice.Show();
    }

}