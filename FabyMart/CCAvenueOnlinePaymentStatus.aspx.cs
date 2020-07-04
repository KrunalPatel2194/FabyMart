using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using CCA.Util;
using System.Collections.Specialized;
public partial class CCAvenueOnlinePaymentStatus : PageBase_Client
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
                string strTransactionId = "";
                string order_id = "";
                string tracking_id = "";
                string bank_ref_no = "";
                string order_status = "";
                string payment_mode = "";
                string card_name = "";
                string currency = "";
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
                        strTransactionId = Session[appFunctions.Session.PaymentTransactionId.ToString()].ToString();
                        Session[appFunctions.Session.PaymentTransactionId.ToString()] = "";
                        Session[appFunctions.Session.PaymentTransactionId.ToString()] = null;
                    }
                }
                CCACrypto ccaCrypto = new CCACrypto();
                string encResponse = ccaCrypto.Decrypt(Request.Form["encResp"], appFunctions.strCCAvenueworkingKey);
                NameValueCollection Params = new NameValueCollection();
                string[] segments = encResponse.Split('&');
                foreach (string seg in segments)
                {
                    string[] parts = seg.Split('=');
                    if (parts.Length > 0)
                    {
                        string Key = parts[0].Trim();
                        string Value = parts[1].Trim();


                        Params.Add(Key, Value);
                    }
                }
                for (int i = 0; i < Params.Count; i++)
                {
                    if (Params.Keys[i].ToString().ToLower() == "order_id")
                    {
                        order_id = Params[i].ToString().ToLower();
                    }
                    else if (Params.Keys[i].ToString().ToLower() == "tracking_id")
                    {
                        tracking_id = Params[i].ToString().ToLower();
                    }
                    else if (Params.Keys[i].ToString().ToLower() == "bank_ref_no")
                    {
                        bank_ref_no = Params[i].ToString().ToLower();
                    }
                    else if (Params.Keys[i].ToString().ToLower() == "order_status")
                    {
                        order_status = Params[i].ToString().ToLower();
                    }
                    else if (Params.Keys[i].ToString().ToLower() == "payment_mode")
                    {
                        payment_mode = Params[i].ToString().ToLower();
                    }
                    else if (Params.Keys[i].ToString().ToLower() == "card_name")
                    {
                        card_name = Params[i].ToString().ToLower();
                    }
                    else if (Params.Keys[i].ToString().ToLower() == "currency")
                    {
                        currency = Params[i].ToString().ToLower();
                    }
                    //Response.Write(Params.Keys[i] + " = " + Params[i] + "<br>");
                }

                //Response.Write("order_id : " + order_id + "<br>");
                //Response.Write("tracking_id : " + tracking_id + "<br>");
                //Response.Write("bank_ref_no : " + bank_ref_no + "<br>");
                //Response.Write("order_status : " + order_status + "<br>");
                //Response.Write("payment_mode : " + payment_mode + "<br>");
                //Response.Write("card_name : " + card_name + "<br>");
                //Response.Write("currency : " + currency + "<br>");
                if (order_status == "success")
                {
                    IsSuccess = true;
                    //DInfo.ShowMessage("You Successfull Pay for order.", BusinessLayer.Enums.MessageType.Successfull);
                }
                else
                {
                    //DInfo.ShowMessage("Your online payment Failure.", BusinessLayer.Enums.MessageType.Error);
                }


                if (strOrderId != "")
                {
                    tblOrder objOrder = new tblOrder();
                    if (objOrder.LoadByPrimaryKey(Convert.ToInt32(strOrderId)))
                    {
                        objOrder.s_AppTransactionID = order_id;
                        objOrder.s_AppBankRefNo = bank_ref_no;
                        objOrder.s_AppPaymentType = payment_mode;
                        objOrder.s_AppCardName = card_name;
                        objOrder.s_AppCurrency = currency;
                        if (IsSuccess)
                        {
                            objOrder.AppPaymentStatus = Convert.ToInt32(Enums.Enums_PaymentStatus.success);
                            string strDefaultOrderStatusID = "";
                            tblOrderStatus objStatus = new tblOrderStatus();
                            objStatus.Where.AppIsDefault.Value = true;
                            objStatus.Query.Load();
                            if (objStatus.RowCount > 0)
                            {
                                strDefaultOrderStatusID = objStatus.s_AppOrderStatusID;
                            }
                            objStatus = null;
                            objOrder.s_AppOrderStatusID = strDefaultOrderStatusID;
                            tblSubOrder objSubOrder = new tblSubOrder();
                            objSubOrder.SetOrderStatus(Convert.ToInt32(strDefaultOrderStatusID), strOrderId, GetCurrentDateTime().ToString(), Convert.ToInt32(Enums.Enums_OrderStatus.PaymentFail).ToString());
                            objSubOrder = null;

                        }
                        else
                        {
                            objOrder.AppPaymentStatus = Convert.ToInt32(Enums.Enums_PaymentStatus.Failure);
                            objOrder.AppOrderStatusID = Convert.ToInt32(Enums.Enums_OrderStatus.PaymentFail);
                            tblSubOrder objSubOrder = new tblSubOrder();
                            objSubOrder.SetOrderStatus(Convert.ToInt32(Enums.Enums_OrderStatus.PaymentFail), strOrderId, GetCurrentDateTime().ToString(), Convert.ToInt32(Enums.Enums_OrderStatus.PaymentFail).ToString());
                            objSubOrder = null;
                        }
                        objOrder.Save();
                        SendMail(objOrder.s_AppOrderNo, objOrder.s_AppReceiverName, objOrder.s_AppReceiverContactNo1, objOrder.s_AppRecevierEmail, IsSuccess, objOrder.s_AppTransactionID, objOrder.s_AppBankRefNo);
                        objOrder = null;
                        objOrder = new tblOrder();
                        objDataTable = objOrder.LoadMyOrderList(Session[appFunctions.Session.ClientUserID.ToString()].ToString(), "", strOrderId);
                        DataListMyOrder.DataSource = null;
                        DataListMyOrder.DataBind();
                        if (objDataTable.Rows.Count > 0)
                        {
                            DataListMyOrder.DataSource = objDataTable;
                            DataListMyOrder.DataBind();
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

            HiddenField hdnStatus = (HiddenField)e.Item.FindControl("hdnStatus");
            HiddenField hdnOrderId = (HiddenField)e.Item.FindControl("hdnOrderId");
            HiddenField hdnReturnStatus = (HiddenField)e.Item.FindControl("hdnReturnStatus");
            HiddenField hdnDocketNo = (HiddenField)e.Item.FindControl("hdnDocketNo");
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