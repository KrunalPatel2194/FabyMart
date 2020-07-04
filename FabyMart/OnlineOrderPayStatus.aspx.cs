using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;

public partial class OnlineOrderPayStatus : PageBase_Client
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
                string[] merc_hash_vars_seq;
                string merc_hash_string = string.Empty;
                string merc_hash = string.Empty;
                string order_id = string.Empty;
                string strOrderId = "";
                string strTransactionId = "";
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

                if (Request.Form["status"] == "success")
                {
                    merc_hash_vars_seq = appFunctions.hashSequence.Split('|');
                    Array.Reverse(merc_hash_vars_seq);
                    merc_hash_string = appFunctions.SALT + "|" + Request.Form["status"];
                    foreach (string merc_hash_var in merc_hash_vars_seq)
                    {
                        merc_hash_string += "|";
                        merc_hash_string = merc_hash_string + (Request.Form[merc_hash_var] != null ? Request.Form[merc_hash_var] : "");
                    }
                    // Response.Write(merc_hash_string);
                    //return;
                    merc_hash = objCommon.Generatehash512(merc_hash_string).ToLower();
                    if (merc_hash != Request.Form["hash"])
                    {
                        //Response.Write("Hash value did not matched");
                        DInfo.ShowMessage("Your online payment Failed", BusinessLayer.Enums.MessageType.Error);
                        IsSuccess = false;
                    }
                    else
                    {
                        IsSuccess = true;
                        order_id = Request.Form["txnid"];
                        DInfo.ShowMessage("Your online payment is done successfully and your transction ID is " + order_id + "", BusinessLayer.Enums.MessageType.Successfull);
                    }
                }
                else
                {
                    //Response.Write("Hash value did not matched");
                    IsSuccess = false;
                    DInfo.ShowMessage("Your online payment Failed", BusinessLayer.Enums.MessageType.Error);
                }

                if (strOrderId != "")
                {
                    tblOrder objOrder = new tblOrder();
                    if (objOrder.LoadByPrimaryKey(Convert.ToInt32(strOrderId)))
                    {
                        objOrder.s_AppTransactionID = order_id;
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
                            objSubOrder.SetOrderStatus(Convert.ToInt32(Enums.Enums_OrderStatus.PaymentFail), strOrderId, GetCurrentDateTime().ToString(), strDefaultOrderStatusID);
                            objSubOrder = null;
                        }
                        else
                        {
                            objOrder.AppPaymentStatus = Convert.ToInt32(Enums.Enums_PaymentStatus.Failure);
                            objOrder.AppOrderStatusID = Convert.ToInt32(Enums.Enums_OrderStatus.PaymentFail);
                            tblSubOrder objSubOrder = new tblSubOrder();
                            objSubOrder.SetOrderStatus(Convert.ToInt32(Enums.Enums_OrderStatus.PaymentFail), strOrderId, GetCurrentDateTime().ToString(), Convert.ToInt32(Enums.Enums_OrderStatus.Ordered).ToString());
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
                    }
                    else
                    {
                        strText = appFunctions.strOnlineOrderFailed;
                    }
                    strText = strText.Replace("`uname`", strName);
                    strText = strText.Replace("`orderno`", strOrderNo);
                    objCommon.SendOrderSMS(strText, strMobile);
                    objCommon.SendConfirmationMail(strEmail, strSubject, Strbody, Enums.Enum_Confirmation_Mail_type.order);

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