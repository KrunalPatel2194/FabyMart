using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using CCA.Util;
using System.Collections.Specialized;
public partial class CCAvenueOnlineCancelPayment : PageBase_Client
{
    public PageBase objPageBase = new PageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CheckSession();
            SetUpPageContent(ref metaDescription, ref metaKeywords);
            objCommon = new clsCommon();
            try
            {
                string strOrderId = "";
                bool IsSuccess = false;
                if (Session[appFunctions.Session.PaymetnOrderId.ToString()] != null)
                {
                    if (Session[appFunctions.Session.PaymetnOrderId.ToString()].ToString() != "")
                    {
                        strOrderId = Session[appFunctions.Session.PaymetnOrderId.ToString()].ToString();
                        Session[appFunctions.Session.PaymetnOrderId.ToString()] = "";
                        Session[appFunctions.Session.PaymetnOrderId.ToString()] = null;
                    }
                }
                if (Session[appFunctions.Session.PaymentTransactionId.ToString()] != null)
                {
                    if (Session[appFunctions.Session.PaymentTransactionId.ToString()].ToString() != "")
                    {
                        Session[appFunctions.Session.PaymentTransactionId.ToString()] = "";
                        Session[appFunctions.Session.PaymentTransactionId.ToString()] = null;
                    }
                }
                if (strOrderId != "")
                {
                    tblOrder objOrder = new tblOrder();
                    if (objOrder.LoadByPrimaryKey(Convert.ToInt32(strOrderId)))
                    {
                        if (IsSuccess == false)
                        {

                            objOrder.AppPaymentStatus = Convert.ToInt32(Enums.Enums_PaymentStatus.Failure);
                            objOrder.AppOrderStatusID = Convert.ToInt32(Enums.Enums_OrderStatus.PaymentFail);
                            tblSubOrder objSubOrder = new tblSubOrder();
                            objSubOrder.SetOrderStatus(Convert.ToInt32(Enums.Enums_OrderStatus.PaymentFail), strOrderId, GetCurrentDateTime().ToString(), Convert.ToInt32(Enums.Enums_OrderStatus.PaymentFail).ToString());
                            objSubOrder = null;
                            objOrder.Save();
                            try
                            {
                                SendMail(objOrder.s_AppOrderNo, objOrder.s_AppReceiverName, objOrder.s_AppReceiverContactNo1, objOrder.s_AppRecevierEmail, IsSuccess, objOrder.s_AppTransactionID, objOrder.s_AppBankRefNo);
                            }
                            catch (Exception ex)
                            {
                                Response.Write(ex);
                            }
                            objOrder = null;
                            objOrder = new tblOrder();
                            objDataTable = objOrder.LoadMyOrderList(Session[appFunctions.Session.ClientUserID.ToString()].ToString(), "", strOrderId);
                            objOrder = null;
                            DataListMyOrder.DataSource = null;
                            DataListMyOrder.DataBind();
                            if (objDataTable.Rows.Count > 0)
                            {
                                DataListMyOrder.DataSource = objDataTable;
                                DataListMyOrder.DataBind();
                            }
                        }
                    }
                    objOrder = null;
                }
            }
            catch (Exception ex)
            {
            }
            objCommon = null;
        }
    }

    protected void DataListMyOrder_RowDataBound(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string strOrderId = DataListMyOrder.DataKeys[e.Item.ItemIndex].ToString();
            if (strOrderId != "")
            {
                DataList DataListSubOrder = (DataList)e.Item.FindControl("DataListSubOrder");
                if (DataListSubOrder != null)
                {
                    tblSubOrder objSubOrder = new tblSubOrder();
                    DataListSubOrder.DataSource = objSubOrder.GetCustomerSubOrder(Session[appFunctions.Session.ClientUserID.ToString()].ToString(), strOrderId);
                    DataListSubOrder.DataBind();
                    objSubOrder = null;
                }
            }
        }
    }

    protected void DataListSubOrder_RowDataBound(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataList DataListSubOrder = (DataList)sender;
            string strSubOrderId = DataListSubOrder.DataKeys[e.Item.ItemIndex].ToString();
        }
    }

    public void SendMail(string strOrderNo, string strName, string strMobile, string strEmail, bool IsSuccess, string strTransctionId, string strBankRef)
    {
        try
        {
            if (Session[appFunctions.Session.PaymentEmailString.ToString()] != null)
            {
                if (Session[appFunctions.Session.PaymentEmailString.ToString()].ToString() != "")
                {
                    objCommon = new clsCommon();
                    string Strbody = "";
                    string strSubject;
                    if (IsSuccess)
                    {
                        strSubject = "Order confirmation- Your Order #" + strOrderNo + " with Fabymart has been successfully placed!";
                    }
                    else
                    {
                        strSubject = "Order confirmation- Your Order #" + strOrderNo + " with Fabymart has Failed!";
                    }

                    Strbody = Session[appFunctions.Session.PaymentEmailString.ToString()].ToString();
                    //Strbody = Strbody.Replace("`transction`", strTransctionId);
                    //Strbody = Strbody.Replace("`bakRefNo`", strBankRef);
                    if (IsSuccess)
                    {
                        //Strbody = Strbody.Replace("`status`", " is Successfully done ");
                        Strbody = Strbody.Replace("`confirmed`", " now confirmed ");
                    }
                    else
                    {
                        //Strbody = Strbody.Replace("`status`", " has failed ");
                        Strbody = Strbody.Replace("`confirmed`", " not confirmed ");
                    }
                    string strText = "";
                    if (IsSuccess)
                    {
                        strText = appFunctions.strOnlineOrderConfirmed;
                        DInfo.ShowMessage("You Successfull Pay for order.", BusinessLayer.Enums.MessageType.Successfull);
                    }
                    else
                    {
                        strText = appFunctions.strOnlineOrderFailed;
                        DInfo.ShowMessage("Your online payment Failed.", BusinessLayer.Enums.MessageType.Error);
                    }
                    strText = strText.Replace("`uname`", strName);
                    strText = strText.Replace("`orderno`", strOrderNo);
                    objCommon.SendOrderSMS(strText, strMobile);
                    objCommon.SendConfirmationMail(strEmail, strSubject, Strbody, Enums.Enum_Confirmation_Mail_type.order);
                    //objCommon.SendMail(strEmail, strSubject, Strbody);
                    objCommon = null;
                    Session[appFunctions.Session.PaymentEmailString.ToString()] = "";
                }
            }
        }
        catch (Exception ex)
        {
            //Response.Write(ex.StackTrace.ToString());
        }

    }

}