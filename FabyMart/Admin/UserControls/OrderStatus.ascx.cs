using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
public partial class Admin_UserControls_OrderStatus : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SetOrderCount();
    }
    public void SetOrderCount()
    {
        tblOrderStatus objOrderStatus = new tblOrderStatus();
        DataTable objTempTable = objOrderStatus.GetCountStatusWiseSubOrder();
        if (objTempTable.Rows.Count > 0)
        {
            lblOrdered.Text = "[ " + objTempTable.Compute("sum(TotalCount)", tblOrderStatus.ColumnNames.AppOrderStatusID + "=" + Convert.ToInt32(Enums.Enums_OrderStatus.Ordered)).ToString() + " ]";
            lblConfirmed.Text = "[ " + objTempTable.Compute("sum(TotalCount)", tblOrderStatus.ColumnNames.AppOrderStatusID + "=" + Convert.ToInt32(Enums.Enums_OrderStatus.Confirmed)).ToString() + " ]";
            lblReady.Text = "[ " + objTempTable.Compute("sum(TotalCount)", tblOrderStatus.ColumnNames.AppOrderStatusID + "=" + Convert.ToInt32(Enums.Enums_OrderStatus.ReadyToShip)).ToString() + " ]";
            lblShipped.Text = "[ " + objTempTable.Compute("sum(TotalCount)", tblOrderStatus.ColumnNames.AppOrderStatusID + "=" + Convert.ToInt32(Enums.Enums_OrderStatus.Shipped)).ToString() + " ]";
            lblDelivered.Text = "[ " + objTempTable.Compute("sum(TotalCount)", tblOrderStatus.ColumnNames.AppOrderStatusID + "=" + Convert.ToInt32(Enums.Enums_OrderStatus.Delivered)).ToString() + " ]";
            lblCancelled.Text = "[ " + objTempTable.Compute("sum(TotalCount)", tblOrderStatus.ColumnNames.AppOrderStatusID + " in (" + Convert.ToInt32(Enums.Enums_OrderStatus.CancelledByAdmin) + "," + Convert.ToInt32(Enums.Enums_OrderStatus.CancelledByCustomer) + ")").ToString() + " ]";
            lblReturned.Text = "[ " + objTempTable.Compute("sum(TotalCount)", tblOrderStatus.ColumnNames.AppOrderStatusID + "=" + Convert.ToInt32(Enums.Enums_OrderStatus.Returned)).ToString() + " ]";
            lblComplete.Text = "[ " + objTempTable.Compute("sum(TotalCount)", tblOrderStatus.ColumnNames.AppOrderStatusID + "=" + Convert.ToInt32(Enums.Enums_OrderStatus.Complete)).ToString() + " ]";
            lblPaymentFail.Text = "[ " + objTempTable.Compute("sum(TotalCount)", tblOrderStatus.ColumnNames.AppOrderStatusID + "=" + Convert.ToInt32(Enums.Enums_OrderStatus.PaymentFail)).ToString() + " ]";
        
        
        }
      objOrderStatus = null;
    }
}