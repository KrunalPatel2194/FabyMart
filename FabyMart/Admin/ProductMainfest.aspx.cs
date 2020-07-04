using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;

public partial class ProductMainfest : PageBase_Admin
{
    tblSubOrder objSubOrder;
    tblManifest objMenifest;
    int iMenifestID = 0;
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if ((Request.QueryString.Get("ID") != null))
            {
                string strId = "";
                objEncrypt = new clsEncryption();
                try
                {
                    // strId = objEncrypt.Decrypt(Request.QueryString.Get("ID"), appFunctions.strKey);
                    strId = Request.QueryString.Get("ID");
                    // string strCourierComapnyId = objEncrypt.Decrypt(Request.QueryString.Get("CID"), appFunctions.strKey);
                    if (strId != "")
                    {
                        objSubOrder = new tblSubOrder();
                        objDataTable = objSubOrder.GetSubOrderListMainfest(strId.TrimEnd(',').Trim(), Convert.ToInt32(Enums.Enums_OrderStatus.ReadyToShip).ToString());
                        dgvGridView.DataSource = null;
                        dgvGridView.DataBind();
                        if (objDataTable.Rows.Count > 0)
                        {
                            objMenifest = new tblManifest();
                            objMenifest.AddNew();
                            // objMenifest.AppSellerID = Convert.ToInt32(Session[appFunctions.Session.SellerID.ToString()]);
                            objMenifest.AppCreatedDate = GetDateTime();
                            objMenifest.Save();
                            iMenifestID = objMenifest.AppManifestID;
                            objMenifest = null;

                            printButton.Visible = true;
                            lblSiteName.Text = appFunctions.strAppname.ToString() + " (ID-" + iMenifestID.ToString() + ")";
                            lblSellerName.Text = appFunctions.strSellerName.ToString();
                            // spanMenifestNo.InnerHtml = iMenifestID.ToString();
                            //  spanProviderName.InnerHtml = objDataTable.Rows[0][tblCourierCompany.ColumnNames.AppCourierCompany].ToString();
                            if (objDataTable.Rows[0]["appSelfCourier"].ToString() != "")
                            {
                                lblCourierComp.Text = ((Enums.Enum_CourierCompany)int.Parse(objDataTable.Rows[0]["appSelfCourier"].ToString())).ToString();
                            }
                            spanPackage.InnerHtml = objDataTable.Rows.Count.ToString();
                            objSubOrder.SetSubOrderMenifest(strId.TrimEnd(',').Trim(), iMenifestID, Convert.ToInt32(Enums.Enums_OrderStatus.ReadyToShip).ToString());
                            divlbl.Visible = true;

                        }
                        else
                        {
                            printButton.Visible = false;
                            divlbl.Visible = false;
                        }
                        dgvGridView.DataSource = objDataTable;
                        dgvGridView.DataBind();
                        objSubOrder = null;
                    }
                    else
                    {
                        printButton.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                }
                objEncrypt = null;
            }
        }
    }
    protected void dgvGridView_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        DataControlRowType itemType = e.Row.RowType;
        switch (itemType)
        {
            case DataControlRowType.DataRow:

                string strSellerOrderID = dgvGridView.DataKeys[e.Row.RowIndex].Values[0].ToString();
                string strPaymentMode = dgvGridView.DataKeys[e.Row.RowIndex].Values[1].ToString();
                if (strSellerOrderID != "")
                {
                    Repeater RepOrderDetail = (Repeater)e.Row.FindControl("RepOrderDetail");
                    //    Label lblWeight = (Label)e.Row.FindControl("lblWeight");
                    //   lblWeight.Text = "0";
                    if (RepOrderDetail != null)
                    {
                        objSubOrder = new tblSubOrder();
                        objDataTable = objSubOrder.GetSubOrderDetailList(strSellerOrderID, Convert.ToInt32(Enums.Enums_OrderStatus.ReadyToShip).ToString(), false, true.ToString());
                        //if (objDataTable.Rows.Count > 0)
                        //{
                        //    lblWeight.Text = objDataTable.Compute("sum(appWeight)", "").ToString();
                        //}

                        RepOrderDetail.DataSource = objDataTable;
                        RepOrderDetail.DataBind();
                        objCommon = new clsCommon();
                        objCommon = null;
                        objSubOrder = null;
                    }
                }
                //if (strPaymentMode != "")
                //{
                //    Label lblPaymentMode = (Label)e.Row.FindControl("lblPaymentMode");
                //    if (strPaymentMode == Convert.ToInt32(Enums.PaymentMode.COD).ToString())
                //    {
                //        lblPaymentMode.Text = "COD";
                //    }
                //    if (strPaymentMode == Convert.ToInt32(Enums.PaymentMode.PayNow).ToString())
                //    {
                //        lblPaymentMode.Text = "Pre-Paid";
                //    }
                //}
                break;
        }
    }
}