using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using MyGeneration.dOOdads;
using System.Net.Mail;
using BusinessLayer;
using System.Web.UI.WebControls;

public class appFunctions
{


    #region "Enumaration"
    public static int intMaxImgFileSize = 99999;
    public enum Session
    {
        UserID,

        IsSuperAdmin,
        RoleID,
        UserName,
        ShowMessage,
        ShowMessageType,
        DataTable,
        SelectedFields,
        LoadMenu,
        QueryString,
        PageName,
        FromPage,
        EmployeeName,
        ClientUserID,
        ClientUserName,

        MemberID,
        MemberUsername,

        SearchWhere,
        CurrencyID,
        CurrencyImage,
        CurrencyName,
        CurrencyInRupee,
        DefaultCurrencyImage,
        LeadDataTable,
        UserLogID,

        PaymetnOrderId,
        PaymenthasId,
        PaymentTransactionId,
        PaymentEmailString,

        Search,
        LeadFiled,
        DesignationID,
        //UnderEmployeeId,
        //FirstLeveEmployeeId,
        LevelEmployeeId,
        ISAdvanceSearch,
        ProductID,
        ProductColorId,
        Cart,
        DInfoInquiry
    }

    public enum Enum_SortOrderBy
    {
        Asc,
        Desc
    }


    #endregion
    public static string MERCHANT_ID = "5122222";
    public static string MERCHANT_KEY = "ZVm6Kq";
    public static string SALT = "YJOBon7j";
    public static string PAYU_BASE_URL = "https://secure.payu.in";
    public static string action = "";
    public static string hashSequence = "key|txnid|amount|productinfo|firstname|email|udf1|udf2|udf3|udf4|udf5|udf6|udf7|udf8|udf9|udf10";
    public static string ServiceProvider = "payu_paisa";
    public static string strAppname = "Faby Mart";
    public static string strSellerName = "Faby Mart";
    public static string strAddress = "SUrat";
    public static string strCountry = "India";
    public static string strState = "Gujarat";
    public static string strCity = "Surat";
    public static string strPinColde = "395004";
    public static string strMobileNo = "9033143050";
    /*CCAvenue*/
    public static string strCCAvenueMerchant = "71295";
    public static string strCCAvenueworkingKey = "3CC01C3F3D33F9EEF831A9E16F5283DB";
    public static string strCCAvenueAccessCode = "AVLQ05CG62BC78QLCB";
    public static string  strCCAvenueBaseUrl = "https://secure.ccavenue.com/transaction/transaction.do?command=initiateTransaction";

    public static string strKey = "r0b1nr0y";
    public static bool AddProduct = false;
    public static string Extension = ".jpeg,.gif,.png,.jpg,.tiff,.tif,.bmp";

    public static string strCODOrderRegstered = "Dear,`uname` Your COD Order #`orderno` has been registered and we will call you for your order confirmation Shortly.";
    public static string strOrderConfirmed = "Dear,`uname` Your COD order #`orderno` is now confirmed.Once the order is shipped, we will send you an email and SMS with the tracking Information.";
    public static string strOnlineOrderConfirmed = "Dear,`uname` Your order #`orderno` is now confirmed.Once the order is shipped, we will send you an email and SMS with the tracking Information.";
    public static string strOnlineOrderFailed = "Dear,`uname` Your payment failed order #`orderno` is not confirmed.";
    public static string strCODOrderShipped = "Dear,`uname` Your Fabymart order #`orderno` has been shipped via `couriername` with the tracking #`trackingno`.Thanks -Fabymart.com";
    public static string strOrderShipped = "Dear,`uname` Your Fabymart order #`orderno` has been shipped via `couriername` with the tracking #`trackingno`.Thank You! -Fabymart.com";
    public static string strOrderDelivered = "Dear,`uname` Your Order #`orderno` has been delivered to you on `deliverydate` we hope you liked your purchase.Thank you for shopping with Fabymart.com and hope to see you back soon.";

    public static string strSMSURL = " http://dnd.suratsms.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=e78a965d44c97559fa76544a0b77dc4&message=`smstext`&senderId=SMSTST&routeId=1&mobileNos=`mobileno`&smsContentType=english ";

    public string RXEmailRegularExpression = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
    //"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"
    public string RXPhoneRegularExpression = "[0-9]{10}";
    public string RXEmailRegularExpressionMsg = "Ex:abc@xyz.com";
    public string RXPhoneRegularExpressionMsg = "Ex:1234567890";

    public string RXURLRegularExpressionMsg = "Ex:http://www.xyz.com";

    public string RXURLRegularExpression = "http(s)?://([\\w-]+\\.)+[\\w-]+(/[\\w- ./?%&=]*)?";
    public void FillQuantity(int intQuantityID, DropDownList SizeDropDownList, DropDownList QuantityDropDownList)
    {
        QuantityDropDownList.Items.Clear();
        if (SizeDropDownList.Items.Count != 1)
        {
            if (intQuantityID >= 10)
            {
                for (int i = 1; i <= 10; i++)
                {
                    QuantityDropDownList.Items.Add(i.ToString());
                }
            }
            else
            {
                for (int i = 1; i <= intQuantityID; i++)
                {
                    QuantityDropDownList.Items.Add(i + "");
                }
            }
        }
    }
}

