
// Generated by MyGeneration Version # (1.3.0.3)

using System;
using System.Data;
namespace BusinessLayer
{
    public class tblCouponCodeProduct : _tblCouponCodeProduct
    {
        public tblCouponCodeProduct()
        {

        }
        public DataTable LoadGridData(int iCouponType, string strCouponCodeId, string strCategoryId="", string strSubCategoryId="")
        {
            string StrQuery = " select tblCouponCodeProduct.appCouponCodeProductID,tblCouponCodeProduct.appCouponCodeID,tblCouponCodeProduct.appReferenceID ";
            if (iCouponType == Convert.ToInt32(Enums.Enum_CouponCodeType.SubCategory))
            {
                StrQuery += " ,tblSubCategory.appSubCategory,tblCategory.appCategory";
            }
            if (iCouponType == Convert.ToInt32(Enums.Enum_CouponCodeType.Category))
            {
                StrQuery += " ,tblCategory.appCategory";
            }
            if (iCouponType == Convert.ToInt32(Enums.Enum_CouponCodeType.Product))
            {
                StrQuery += " ,tblProduct.appProductID,tblProduct.appIsActive,tblProduct.appProductName,tblProduct.appProductCode,tblProduct.appProductTag,tblProduct.appDescription,tblProduct.appMetaKeyWord,tblProduct.appMetaDescription,tblProduct.appBrowserTitle,tblProduct.appIsColor,tblProduct.appIsSize	,tblColor.appColorName,tblColor.appColorCode	,tblProductImage.appThumbImage,tblProductImage.appNormalImage,tblProductImage.appLargeImage,tblProductImage.appSmallImage ";
            }

            StrQuery += " from tblCouponCodeProduct ";
            if (iCouponType == Convert.ToInt32(Enums.Enum_CouponCodeType.SubCategory))
            {
                StrQuery += " Inner Join tblSubCategory On tblSubCategory.appSubCategoryID=tblCouponCodeProduct.appReferenceID ";
                StrQuery += " Inner Join tblCategory On tblCategory.appCategoryID=tblSubCategory.appCategoryID ";
            }
            if (iCouponType == Convert.ToInt32(Enums.Enum_CouponCodeType.Category))
            {
                StrQuery += " Inner Join tblCategory On tblCategory.appCategoryID=tblCouponCodeProduct.appReferenceID  ";
            }
            if (iCouponType == Convert.ToInt32(Enums.Enum_CouponCodeType.Product))
            {
                StrQuery += " Inner Join tblProduct On tblProduct.appProductID=tblCouponCodeProduct.appReferenceID  ";
                StrQuery += " Left join tblProductColor On tblProductColor.appProductID=tblProduct.appProductID and tblProductColor.appIsDefault=1  ";
                StrQuery += " Left join tblColor on tblColor.appColorID=tblProductColor.appColorID ";
                StrQuery += " Left join tblProductImage on tblProductImage.appProductColorID=tblProductColor.appProductColorID and tblProductImage.appIsDefault=1   ";
            }

            StrQuery += "  where tblCouponCodeProduct.appCouponCodeID="+strCouponCodeId;
            if (!string.IsNullOrEmpty(strCategoryId) & strCategoryId != "0")
            {
                StrQuery += " and tblCategory.appCategoryID=" + strCategoryId;
            }
            if (!string.IsNullOrEmpty(strSubCategoryId) & strSubCategoryId != "0")
            {
                StrQuery += " and tblSubCategory.appSubCategoryID=" + strSubCategoryId;
            }
            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }
       
        public DataTable LoadUnSelectedCategories(int iCouponType, string strCouponCodeId, string strCategoryId="", string strSubCategoryId="")
        {

            string StrQuery = "   select  ";
            if (iCouponType == Convert.ToInt32(Enums.Enum_CouponCodeType.SubCategory))
            {
                StrQuery += " * From  tblSubCategory ";
                StrQuery += " where  1=1";
                if (!string.IsNullOrEmpty(strCategoryId) & strCategoryId != "0")
                {
                    StrQuery += " and tblSubCategory.appCategoryID=" + strCategoryId;
                }
                if (!string.IsNullOrEmpty(strSubCategoryId) & strSubCategoryId != "0")
                {
                    StrQuery += " and tblSubCategory.appSubCategoryID=" + strSubCategoryId;
                }
                StrQuery += " and   tblSubCategory.appSubCategoryID not in ( Select appReferenceID from tblCouponCodeProduct where appCouponCodeID=" + strCouponCodeId + " )";
            }
            if (iCouponType == Convert.ToInt32(Enums.Enum_CouponCodeType.Category))
            {
                StrQuery += " * From  tblCategory";
                StrQuery += " where    tblCategory.appCategoryID not in ( Select appReferenceID from tblCouponCodeProduct where appCouponCodeID=" + strCouponCodeId + " )";

            }
            if (iCouponType == Convert.ToInt32(Enums.Enum_CouponCodeType.Product))
            {
                StrQuery += " vw_SerachProduct.* From vw_SerachProduct Inner Join (Select distinct(tblProductSubCategory.appProductID) From tblProductSubCategory 	Inner Join tblSubCategory On tblSubCategory.appSubCategoryID=tblProductSubCategory.appSubCategoryID	Where 1=1 ";
                if (strCategoryId != "" && strCategoryId != "0")
                {
                    StrQuery += " And tblSubCategory.appCategoryID=" + strCategoryId;
                }
                if (strSubCategoryId != "" && strSubCategoryId != "0")
                {
                    StrQuery += " And tblSubCategory.appSubCategoryID=" + strSubCategoryId;
                }
                StrQuery += " )tblProductSubCategory On tblProductSubCategory.appProductID=vw_SerachProduct.appProductID ";

                StrQuery += " Where 1=1 ";

                StrQuery += " And appProductColorIsDefault=1";

                StrQuery += " And appProductDetailIsDefault=1";
                StrQuery += "  and vw_SerachProduct.appProductId Not In (  Select appReferenceID from tblCouponCodeProduct where appCouponCodeID=" + strCouponCodeId + " )";
            }

            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }

        public DataTable GetReferenceIDByCouponID(string strCouponCodeId, int iCouponType, string strProductID)
        {
            string StrQuery = " select tblCouponCodeProduct.appCouponCodeProductID,tblCouponCodeProduct.appCouponCodeID,tblCouponCodeProduct.appReferenceID ";
            if (iCouponType == Convert.ToInt32(Enums.Enum_CouponCodeType.SubCategory))
            {
                StrQuery += " ,tblSubCategory.appSubCategoryID,tblCategory.appCategory";
            }
            if (iCouponType == Convert.ToInt32(Enums.Enum_CouponCodeType.Category))
            {
                StrQuery += " ,tblCategory.appCategoryID";
            }
            if (iCouponType == Convert.ToInt32(Enums.Enum_CouponCodeType.Product))
            {
                StrQuery += " ,tblProduct.appProductID,tblProduct.appIsActive,tblProduct.appProductName ";
            }

            StrQuery += " from tblCouponCodeProduct ";
            if (iCouponType == Convert.ToInt32(Enums.Enum_CouponCodeType.SubCategory))
            {
                StrQuery += " Inner Join tblSubCategory On tblSubCategory.appSubCategoryID=tblCouponCodeProduct.appReferenceID ";
                StrQuery += " Inner Join tblCategory On tblCategory.appCategoryID=tblSubCategory.appCategoryID ";
            }
            if (iCouponType == Convert.ToInt32(Enums.Enum_CouponCodeType.Category))
            {
                StrQuery += " Inner Join tblCategory On tblCategory.appCategoryID=tblCouponCodeProduct.appReferenceID  ";
            }
            if (iCouponType == Convert.ToInt32(Enums.Enum_CouponCodeType.Product))
            {
                StrQuery += " Inner Join tblProduct On tblProduct.appProductID=tblCouponCodeProduct.appReferenceID  ";

            }

            StrQuery += "  where tblCouponCodeProduct.appCouponCodeID=" + strCouponCodeId;
            if (iCouponType == Convert.ToInt32(Enums.Enum_CouponCodeType.SubCategory))
            {
                StrQuery += " and tblCouponCodeProduct.appReferenceID in( Select distinct(appSubCategoryID) From tblProductSubCategory Where appProductId= " + strProductID + ")";
            }
            if (iCouponType == Convert.ToInt32(Enums.Enum_CouponCodeType.Category))
            {
                StrQuery += " and tblCouponCodeProduct.appReferenceID in( Select distinct(tblSubCategory.appCategoryID) From tblSubCategory inner join tblProductSubCategory on tblProductSubCategory.appSubCategoryID=tblSubCategory.appSubCategoryID Where tblProductSubCategory.appProductId= " + strProductID + ")";
            }
            if (iCouponType == Convert.ToInt32(Enums.Enum_CouponCodeType.Product))
            {
                StrQuery += " and tblProduct.appProductID= " + strProductID;
            }
            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }
    }
}
