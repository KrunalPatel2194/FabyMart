
// Generated by MyGeneration Version # (1.3.0.3)

using System;
using System.Data;
namespace BusinessLayer
{
    public class tblReturnOrder : _tblReturnOrder
    {
        public tblReturnOrder()
        {

        }

        public DataTable LoadPendingReturnOrderAdmin(string strColumnName, string strColumnValue, string strStartDate = "", string strEndDate = "")
        {
            strColumnValue = strColumnValue.Replace("'", "");

            string StrQuery = " select tblReturnOrder.appReturnOrderID,tblReturnOrder.appOrderID,tblReturnOrder.appReason,tblReturnOrder.appNote,tblReturnOrder.appRequestedDate,tblReturnOrder.appReturnStatus,tblOrder.appReceiverName,tblOrder.appReceiverAddress,tblOrder.appReceiverContactNo1 ";
            StrQuery += " from tblReturnOrder     ";
            StrQuery += " Inner join (Select tblReturnOrderDetail.appReturnOrderID,min(tblReturnOrderDetail.appSubOrderID)appSubOrderID from tblReturnOrderDetail Group by tblReturnOrderDetail.appReturnOrderID )tblReturnOrderDetail on tblReturnOrderDetail.appReturnOrderID=tblReturnOrder.appReturnOrderID   ";
            StrQuery += " Inner join tblSubOrder on tblSubOrder.appSubOrderID=tblReturnOrderDetail.appSubOrderID ";
            StrQuery += " Inner join tblOrder on tblOrder.appOrderID= tblSubOrder.appOrderID   ";

            StrQuery += " Inner join tblCustomer on tblCustomer.appCustomerID=tblOrder.appCustomerID     ";
            StrQuery += " Inner join tblProductDetail on tblProductDetail.appProductDetailID=tblSubOrder.appProductDetailID ";
            StrQuery += " Inner join tblProductColor On tblProductColor.appProductColorID=tblProductDetail.appProductColorID    ";
            StrQuery += "  Inner join tblProduct On tblProductColor.appProductID=tblProduct.appProductID          ";
           
            StrQuery += "  where tblReturnOrder.appReturnStatus='" + Convert.ToInt32(Enums.Enum_ReturnStatus.Requested) + "'  ";
       
            if (!string.IsNullOrEmpty(strColumnValue) & strColumnName != "0")
            {
                StrQuery += " and tblProduct." + strColumnName + " LIKE '%" + strColumnValue + "%'";
            }
            if (strStartDate != "0" && strStartDate != "" && strEndDate != "0" && strEndDate != "")
            {
                StrQuery += " and  convert(varchar , tblReturnOrder.appRequestedDate,103) between convert(varchar , '" + strStartDate + "',103) and convert(varchar , '" + strEndDate + "',103) ";
            }
            StrQuery += " order by tblReturnOrder.appRequestedDate desc";
            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }
        public DataTable LoadReturnOrderdWiseProductWeight(string strReturnOrderId)
        {
            string StrQuery = " Select tblReturnOrder.appReturnOrderID,sum(isnull(tblProduct.appWeight,1)*isnull(tblSubOrder.appQty,1)) as appTotalWeight,tblOrder.appPaymentMode,tblOrder.appReceiverPIN  ";
            StrQuery += " From tblReturnOrderDetail ";
            StrQuery += " Inner join tblReturnOrder on tblReturnOrder.appReturnOrderID=tblReturnOrderDetail.appReturnOrderID  ";
            StrQuery += " Inner join tblSubOrder on tblSubOrder.appSubOrderID=tblReturnOrderDetail.appSubOrderID  ";
            StrQuery += " Inner join tblOrder on tblOrder.appOrderID=tblSubOrder.appOrderID  ";
            StrQuery += " Inner join tblProductDetail on tblProductDetail.appProductDetailID=tblSubOrder.appProductDetailID ";
            StrQuery += " Inner join tblProductColor On tblProductColor.appProductColorID=tblProductDetail.appProductColorID   ";
            StrQuery += " Inner join tblProduct On tblProductColor.appProductID=tblProduct.appProductID  ";
            StrQuery += "  where tblReturnOrderDetail.appReturnOrderID  =" + strReturnOrderId;
            StrQuery += " Group by tblReturnOrder.appReturnOrderID,tblOrder.appPaymentMode,tblOrder.appReceiverPIN ";
            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }
        public DataTable LoadReturOrderProduct(string strSubOrderID)
        {
            string StrQuery = " select  tblReturnOrderDetail.appReturnOrderDetailID, tblProductDetail.appProductDetailID,tblProductDetail.appSKUNo,tblProduct.appProductName,tblProduct.appProductCode,tblProductImage.appSmallImage,tblProductImage.appLargeImage,tblProductImage.appNormalImage,tblProductImage.appThumbImage ";
            StrQuery += " from tblReturnOrderDetail ";
            StrQuery += " Inner join tblSubOrder on tblSubOrder.appSubOrderID=tblReturnOrderDetail.appSubOrderID    ";
            StrQuery += " Inner join tblProductDetail on tblProductDetail.appProductDetailID=tblSubOrder.appProductDetailID ";
            StrQuery += " Inner join tblProductColor On tblProductColor.appProductColorID=tblProductDetail.appProductColorID   ";
            StrQuery += " Inner join tblProduct On tblProductColor.appProductID=tblProduct.appProductID          ";
            StrQuery += " Inner join tblProductImage on tblProductImage.appProductColorID=tblProductColor.appProductColorID and tblProductImage.appIsDefault=1   ";
            StrQuery += "  where tblReturnOrderDetail.appSubOrderID  =" + strSubOrderID;

            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }
        public DataTable LoadReturOrderProductFromReturnOrder(string strReturOrderID)
        {
            string StrQuery = " select  tblReturnOrderDetail.appReturnOrderDetailID, tblProductDetail.appProductDetailID,tblProductDetail.appSKUNo,tblProduct.appProductName,tblProduct.appProductCode,tblProductImage.appSmallImage,tblProductImage.appLargeImage,tblProductImage.appNormalImage,tblProductImage.appThumbImage ";
            StrQuery += " from tblReturnOrderDetail ";
            StrQuery += " Inner join tblSubOrder on tblSubOrder.appSubOrderID=tblReturnOrderDetail.appSubOrderID    ";
            StrQuery += " Inner join tblProductDetail on tblProductDetail.appProductDetailID=tblSubOrder.appProductDetailID ";
            StrQuery += " Inner join tblProductColor On tblProductColor.appProductColorID=tblProductDetail.appProductColorID   ";
            StrQuery += " Inner join tblProduct On tblProductColor.appProductID=tblProduct.appProductID          ";
            StrQuery += " Inner join tblProductImage on tblProductImage.appProductColorID=tblProductColor.appProductColorID and tblProductImage.appIsDefault=1   ";
            StrQuery += "  where tblReturnOrderDetail.appReturnOrderID  =" + strReturOrderID;

            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }
        public DataTable GetCourierCompanyIdFromData(string strWeight, string strPinCode, bool IsCOD)
        {
            string StrQuery = "Select Top 1 * From (  Select tblCourierCompany.appCourierCompanyID,tblCourierRate.appIsCOD,IsNull(tblCourierCompany.appCODRate,0) appCODRate, case when tblCourierRate.appIsCOD=1 then isnull(tblCourierRate.appRate,1)+ isnull(tblCourierCompany.appCODRate,1) else isnull(tblCourierRate.appRate,1) end as appRate	From tblCourierRate 	inner join tblPincode on tblPincode.appPinCodeID=tblCourierRate.appPinCodeID	inner join tblCourierCompany on tblCourierCompany.appCourierCompanyID=tblCourierRate.appCourierCompanyID ";
            StrQuery += " Where  " + strWeight + " between appMinWeight and appMaxWeight 	and tblPincode.appPinCode= " + strPinCode;
            if (IsCOD)
            {
                StrQuery += " and  tblCourierRate.appIsCOD='" + IsCOD.ToString() + "'";
            }
            StrQuery += " ) tblCourierCompany	order by appRate";
            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }
        public DataTable GetReturnOrderListWithCityStateCountry(string strReturnOrderId, string strStatus, string strSellerName, string strMobileNo, string strAddress, string strCountry, string strState, string strCity, string strPincode)
        {
            string StrQuery = " Select tblReturnOrder.appReturnOrderID,tblReturnOrder.appOrderId,tblReturnOrder.appReason,tblReturnOrder.appNote,tblReturnOrder.appRequestedDate,tblReturnOrder.appReturnStatus,tblReturnOrder.appCourierCompanyID,tblReturnOrder.appDocketNo,tblReturnOrder.appPickupName,tblReturnOrder.appPickupAddress,tblReturnOrder.appPickupContactNo1,tblReturnOrder.appPickupContactNo2,tblReturnOrder.appPickupPIN,tblReturnOrder.appPreferedTime,tblReturnOrder.appPreviousSubOrderStatus, tblCus.appState  ,tblCus.appCountry , tblCus.appCity , tblCus.appPinCode  ,'" + strSellerName + "' as appSellerName,'" + strAddress + "' as appSellerAddress,'" + strMobileNo + "' as appSellerContactNo1,'" + strState + "' as appSellerState,'" + strCountry + "' as appSellerCountry,  '" + strCity + "' as appSellerCity, '" + strPincode + "' as appSellerPincode";
            StrQuery += " From tblReturnOrder ";
            StrQuery += " left join (select tblState.appState,tblCountry.appCountry, tblCity.appCity, tblPinCode.appPinCode from tblPinCode  left join tblCity on tblCity.appCityID=tblPinCode.appCityID left join tblState on tblCity.appStateID=tblState.appStateID left join tblCountry on tblState.appCountryID=tblCountry.appCountryID) as  tblCus  on tblCus.appPinCode=tblReturnOrder.appPickupPIN  ";
            StrQuery += " Where tblReturnOrder.appReturnOrderID=" + strReturnOrderId;
            StrQuery += " and tblReturnOrder.appReturnStatus=" + strStatus;
            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }
        public DataTable LoadReturnOrderInvoice(string strReturnOrderId)
        {
            string StrQuery = " select  Row_Number() over(ORDER BY tblSubOrder.appSubOrderID ) as appRowNo, tblSubOrder.appSubOrderID,tblSubOrder.appOrderID,tblSubOrder.appProductDetailID,tblSubOrder.appQty,tblSubOrder.appSellingPrice,tblSubOrder.appSubOrderStatusID,convert(varchar,tblSubOrder.appSubOrderChangeDate,105) appSubOrderChangeDate,convert(varchar,tblSubOrder.appMaxDispatchDate,105) appMaxDispatchDate,tblSubOrder.appCourierCompanyID,tblSubOrder.appDocketNo,tblSubOrder.appShippingCharges,tblSubOrder.appShippedDate,tblSubOrder.appShippedStatus,tblSubOrder.appCommision,tblSubOrder.appCommisionRs,tblSubOrder.appServiceTax,tblSubOrder.appServiceTaxRs,tblSubOrder.appFixedFee,tblSubOrder.appManifestGenerated,tblSubOrder.appManifestID,tblSubOrder.appSelfCourier,convert(varchar,tblSubOrder.appDispatchDate,105) appDispatchDate,convert(varchar,tblSubOrder.appDeliveryDate,105) appDeliveryDate,tblSubOrder.appInvoiceGenerated,	(IsNull(tblSubOrder.appQty,0)*IsNull(tblSubOrder.appSellingPrice,0)) as appTotal,tblProductDetail.appProductDetailID,tblProductDetail.appProductColorID,tblProductDetail.appSKUNo,tblProductDetail.appSizeID, tblProduct.appProductId,tblProduct.appProductName,tblProduct.appProductCode, tblProduct.appWeight";
            StrQuery += " from tblReturnOrderDetail ";
            StrQuery += " Inner join tblSubOrder on tblSubOrder.appSubOrderID=tblReturnOrderDetail.appSubOrderID    ";
            StrQuery += " Inner join tblProductDetail on tblProductDetail.appProductDetailID=tblSubOrder.appProductDetailID ";
            StrQuery += " Inner join tblProductColor On tblProductColor.appProductColorID=tblProductDetail.appProductColorID   ";
            StrQuery += " Inner join tblProduct On tblProductColor.appProductID=tblProduct.appProductID          ";
            StrQuery += "  where tblReturnOrderDetail.appReturnOrderID  =" + strReturnOrderId;

            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }
        public DataTable LoadReturnReportOuterGrid()
        {
            string StrQuery = "select case when tblReturnOrder.appPickupName IS Null then tblOrder.appBillReceiverName COLLATE DATABASE_DEFAULT  else tblReturnOrder.appPickupName  COLLATE DATABASE_DEFAULT end as appPickupName,case when tblReturnOrder.appPickupAddress IS Null then tblOrder.appBillReceiverAddress COLLATE DATABASE_DEFAULT  else tblReturnOrder.appPickupAddress  COLLATE DATABASE_DEFAULT end as appPickupAddress,case when tblReturnOrder.appPickupContactNo1 IS Null then tblOrder.appBillReceiverContactNo1 COLLATE DATABASE_DEFAULT  else tblReturnOrder.appPickupContactNo1  COLLATE DATABASE_DEFAULT end as appPickupContactNo1,case when tblReturnOrder.appPickupPIN IS Null then tblOrder.appBillReceiverPIN COLLATE DATABASE_DEFAULT  else tblReturnOrder.appPickupPIN  COLLATE DATABASE_DEFAULT end as appPickupPIN,tblOrder.appCustomerID ";
            StrQuery += " from (select tblCustomer.appCustomerID,min(tblSubOrder.appSubOrderID) as appSubOrderID  from tblSubOrder    left join tblReturnOrderDetail on tblSubOrder.appSubOrderID=tblReturnOrderDetail.appSubOrderID left join tblReturnOrder on tblReturnOrder.appReturnOrderID=tblReturnOrderDetail.appReturnOrderID    left join tblOrder on tblOrder.appOrderID= tblSubOrder.appOrderID   left join tblCustomer on tblCustomer.appCustomerID=tblOrder.appCustomerID      left join tblOrderStatus on tblSubOrder.appSubOrderStatusID=tblOrderStatus.appOrderStatusID    ";
            StrQuery += "  where( (tblReturnOrder.appReturnStatus='" + Convert.ToInt32(Enums.Enum_ReturnStatus.Complete) + "' and tblSubOrder.appSubOrderStatusID in (" + Convert.ToInt32(Enums.Enums_OrderStatus.Returned) + ")";
          
            //if (!string.IsNullOrEmpty(strColumnValue) & strColumnName != "0")
            //{
            //    StrQuery += " and " + strColumnName + " LIKE '%" + strColumnValue + "%'";
            //}
            StrQuery += "    ) ";
            StrQuery += "  or (tblSubOrder.appSubOrderStatusID in (" + Convert.ToInt32(Enums.Enums_OrderStatus.CancelledByCustomer) + ") ";
          
            StrQuery += "   ) ";

            StrQuery += "   ) ";
            StrQuery += " Group by tblCustomer.appCustomerID )tblTemp ";

            StrQuery += " left join  tblCustomer  on tblTemp.appCustomerID=tblCustomer.appCustomerID  ";
            StrQuery += " left join  tblSubOrder  on tblTemp.appSubOrderID=tblSubOrder.appSubOrderID ";

            StrQuery += " left join tblReturnOrderDetail on tblSubOrder.appSubOrderID=tblReturnOrderDetail.appSubOrderID ";
            StrQuery += "  left join tblReturnOrder on tblReturnOrder.appReturnOrderID=tblReturnOrderDetail.appReturnOrderID   ";
            StrQuery += " left join tblOrder on tblSubOrder.appOrderID= tblOrder.appOrderID   ";
            StrQuery += "  order by tblSubOrder.appSubOrderID desc ";
            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }

     
        public DataTable GetProductData(string strCustomerID = "")
        {

            string StrQuery = "  Select convert(varchar, tblSubOrder.appSubOrderChangeDate,105)  as appCompletedDate, tblOrder.appOrderID, tblProduct.appProductID, convert(varchar, tblOrder.appCreatedDate,105)appCreatedDate1,tblSubOrder.appSubOrderID,tblSubOrder.appSubOrderNo,tblSubOrder.appQty,tblSubOrder.appSellingPrice,(IsNull(tblSubOrder.appQty,0)*IsNull(tblSubOrder.appSellingPrice,0)) as appTotal,tblSubOrder.appSubOrderStatusID,convert(varchar, tblSubOrder.appMaxDispatchDate,103)appMaxDispatchDate,tblProductDetail.appProductDetailID,tblProduct.appProductName,tblProduct.appProductCode,tblProductDetail.appProductColorID,tblProductDetail.appIsDefault,tblProductDetail.appSellerPrice,tblProductDetail.appMRP,tblProductDetail.appPrice,tblProductDetail.appQuantity,tblProductDetail.appSKUNo,tblProductDetail.appSizeID,tblProductColor.appColorID,tblColor.appColorName,tblColor.appColorCode,tblSize.appsize,tblProductImage.appThumbImage,tblProductImage.appLargeImage,tblProductImage.appNormalImage,tblOrder.*,tblProduct.appProductCode,tblOrderStatus.appOrderStatus From tblSubOrder ";
            StrQuery += " left join tblReturnOrderDetail on tblSubOrder.appSubOrderID=tblReturnOrderDetail.appSubOrderID ";
            StrQuery += " left join tblReturnOrder on tblReturnOrder.appReturnOrderID=tblReturnOrderDetail.appReturnOrderID     ";
            StrQuery += " left join tblOrder on tblOrder.appOrderID= tblSubOrder.appOrderID     ";
            StrQuery += " Inner join tblProductDetail on tblProductDetail.appProductDetailID=tblSubOrder.appProductDetailID   ";
            StrQuery += " Inner join tblProductColor On tblProductColor.appProductColorID=tblProductDetail.appProductColorID  ";
            StrQuery += " Inner join tblProduct on tblProduct.appProductID=tblProductColor.appProductID   ";
            StrQuery += " Inner join tblProductImage On tblProductImage.appProductColorID=tblProductColor.appProductColorID   ";
            StrQuery += " inner join tblColor On tblColor.appColorID=tblProductColor.appColorID   ";
            StrQuery += "  Left Join tblsize on tblSize.appsizeId=tblProductDetail.appsizeId   ";
            StrQuery += " left join tblOrderStatus on tblSubOrder.appSubOrderStatusID=tblOrderStatus.appOrderStatusID       ";


            StrQuery += " where tblProductImage.appIsDefault=1  ";
            StrQuery += "  and ( (tblReturnOrder.appReturnStatus='" + Convert.ToInt32(Enums.Enum_ReturnStatus.Complete) + "' and tblSubOrder.appSubOrderStatusID in (" + Convert.ToInt32(Enums.Enums_OrderStatus.Returned) + ")";


            StrQuery += "    ) ";
            StrQuery += "  or (tblSubOrder.appSubOrderStatusID in (" + Convert.ToInt32(Enums.Enums_OrderStatus.CancelledByCustomer) + ") ";

            StrQuery += "   ) )";

            if (strCustomerID != "0" && strCustomerID != "")
            {
                StrQuery += " And tblOrder.appCustomerID ='" + strCustomerID + "' ";
            }
          
            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }
      
    }
}
