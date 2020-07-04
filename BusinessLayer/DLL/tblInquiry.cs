
// Generated by MyGeneration Version # (1.3.0.3)

using System;
using System.Data;

namespace BusinessLayer
{
	public class tblInquiry : _tblInquiry
	{
		public tblInquiry()
		{
		
		}
        public DataTable LoadGridData(string strColumnName, string strSearchText)
        {
            string StrQuery = "Select tblInquiry.appInquiryID,tblInquiry.appName,tblInquiry.appEmail,tblInquiry.appMobile,tblInquiry.appMessage,tblProductDetail.appSellerPrice,tblProductDetail.appMRP,tblProductDetail.appPrice,tblProductDetail.appQuantity,tblProductDetail.appSKUNo,tblProductDetail.appSizeID,tblSize.appSize,tblProduct.appProductName,tblProduct.appProductCode,tblColor.appColorName,tblColor.appColorCode,tblColor.appColorImage,tblProductImage.appThumbImage,tblProductImage.appNormalImage,tblProductImage.appLargeImage,tblProductImage.appSmallImage,case When tblProduct.appIsColor=1  Then tblColor.appColorName else '' End appColorLink 	From tblInquiry	Inner Join tblProductDetail On tblProductDetail.appProductDetailID=tblInquiry.appProductDetailID Inner Join tblsize On tblSize.appSizeId=tblProductDetail.appSizeId	Inner join tblProductColor On tblProductColor.appProductColorID=tblProductDetail.appProductColorID	Inner join tblProduct on tblProduct.appProductID=tblProductColor.appProductID	Inner join tblColor on tblColor.appColorID=tblProductColor.appColorID	Inner join tblProductImage on tblProductImage.appProductColorID=tblProductColor.appProductColorID and tblProductImage.appIsDefault=1 Where 1=1 ";

            if (strColumnName != "" && strSearchText != "")
            {
                StrQuery += " And " + strColumnName + " like '%" + strSearchText + "%'";
            }
            else if (strColumnName == "0" && strSearchText != "")
            {
                StrQuery += " And ( tblInquiry.appName like '%" + strSearchText + "%'";
                StrQuery += " Or tblInquiry.appEmail like '%" + strSearchText + "%' ";
                StrQuery += " Or tblInquiry.appMobile like '%" + strSearchText + "%'";
                StrQuery += " Or tblInquiry.appMessage like '%" + strSearchText + "%' ";
                StrQuery += " Or tblProduct.appProductName like '%" + strSearchText + "%' ";
                StrQuery += " Or tblProduct.appProductCode like '%" + strSearchText + "%' ";
                StrQuery += " Or tblProductDetail.appSellerPrice like '%" + strSearchText + "%' ";
                StrQuery += " Or tblProductDetail.appMRP like '%" + strSearchText + "%' ";
                StrQuery += " Or tblProductDetail.appPrice like '%" + strSearchText + "%' ";
                StrQuery += " Or tblProductDetail.appSKUNo like '%" + strSearchText + "%' ";
                StrQuery += " Or tblProductDetail.appQuantity like '%" + strSearchText + "%' ";
                StrQuery += " Or tblSize.appSize like '%" + strSearchText + "%' ";
                StrQuery += " Or tblColor.appColorName like '%" + strSearchText + "%' ";
                StrQuery += " Or tblColor.appColorCode like '%" + strSearchText + "%' )";
            }
            StrQuery += " Order by appInquiryID Desc";
            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }
	}
}
