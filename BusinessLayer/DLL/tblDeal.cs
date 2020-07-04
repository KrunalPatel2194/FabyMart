
// Generated by MyGeneration Version # (1.3.0.3)

using System;
using System.Data;
namespace BusinessLayer
{
    public class tblDeal : _tblDeal
    {
        public tblDeal()
        {

        }
        public DataTable LoadGridData(string strColumnName, string strColumnValue)
        {
            strColumnValue = strColumnValue.Replace("'", "");

            string StrQuery = "select tblDeal.appdealID,tblDeal.appTitle,tblDeal.appdiscountPer,tblDeal.appIsActive,tblDeal.appDisplayOrder,tblDeal.appProductID,a.appSmallImage,a.appProductName  from tblDeal ";
            StrQuery += "inner join(select tblproduct.appProductID,tblProductImage.appSmallImage,tblproduct.appProductName from tblproduct ";
            StrQuery += "inner join tblProductColor on tblProductColor.appProductID = tblproduct.appProductID ";
            StrQuery += "inner join tblProductImage on tblProductImage.appProductColorID = tblProductColor.appProductColorID ";
            StrQuery += "where tblProductColor.appIsDefault = 1 and tblProductImage.appIsDefault = 1 )a on a.appProductID = tblDeal.appProductID";
            if (!string.IsNullOrEmpty(strColumnValue) & strColumnName != "0")
            {
                StrQuery += " and " + strColumnName + " LIKE '%" + strColumnValue + "%'";
            }


            StrQuery += " order by tblDeal.appDisplayOrder  ";
            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }

        public DataTable LoadTopDeal(string strTopRecord = "", string strLength = "")
        {
            string StrQuery = "select ";
            if (strTopRecord != "")
            {
                StrQuery += " Top " + strTopRecord;
            }
            StrQuery += " tblDeal.appdealID,tblDeal.appTitle,";
            if (strLength != "")
            {
                StrQuery += " case when tblDeal.appDescription is null then '' else case When LEN(tblDeal.appDescription) <= " + strLength + "  then appDescription else LEFT(tblDeal.appDescription, " + strLength + ")  End End appDescription,";
            }
            else
            {
                StrQuery += " tblDeal.appDescription, ";
            }
            StrQuery += " tblDeal.appdiscountPer,tblDeal.appIsActive,tblDeal.appDisplayOrder,tblDeal.appProductID,a.appSmallImage,a.appProductName,a.appThumbImage,a.appNormalImage  from tblDeal ";
            StrQuery += " inner join(select tblproduct.appProductID,tblProductImage.appNormalImage,tblProductImage.appThumbImage,tblProductImage.appSmallImage,tblproduct.appProductName from tblproduct ";
            StrQuery += " inner join tblProductColor on tblProductColor.appProductID = tblproduct.appProductID ";
            StrQuery += " inner join tblProductImage on tblProductImage.appProductColorID = tblProductColor.appProductColorID ";
            StrQuery += " where tblProductColor.appIsDefault = 1 and tblProductImage.appIsDefault = 1 )a on a.appProductID = tblDeal.appProductID";
            StrQuery += " order by tblDeal.appDisplayOrder  ";
            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }
        public DataTable LoadBestDeal()
        {
            string StrQuery = "  select tblDeal.appDiscountPer,tblDeal.appDescription,tblProductDetail.appProductDetailID,tblProductDetail.appSellerPrice,case when tblDeal.appDiscountPer Is null then tblProductDetail.appPrice else (isnull(tblProductDetail.appPrice,0)-((isnull(tblProductDetail.appPrice,0)*tblDeal.appDiscountPer)/100)) End appPrice,case when tblDeal.appDiscountPer Is null then tblProductDetail.appMRP else tblProductDetail.appPrice End appMRP,case when tblDeal.appDiscountPer Is null then 0 else tblProductDetail.appMRP End appMRP1,tblDeal.appTitle, tblProductImage.appNormalImage,tblProduct.appProductName,case when tblDeal.appDiscountPer is null then Cast(ROUND((100-(isnull(dbo.tblProductDetail.appPrice,0)*100)/isnull(dbo.tblProductDetail.appMRP,0)),0) as Decimal(2)) else Cast(ROUND((100-(isnull( cast((isnull(tblProductDetail.appPrice,0)-((isnull(tblProductDetail.appPrice,0)*tblDeal.appDiscountPer)/100)) as Decimal(10,2)),0)*100)/isnull(dbo.tblProductDetail.appMRP,0)),0) as decimal(2,0))  end appOff  from tblDeal ";
            StrQuery += "   Inner join tblProduct on tblProduct.appProductID=tblDeal.appProductID   ";
            StrQuery += "  Inner join tblProductColor On tblProductColor.appProductID=tblProduct.appProductID  ";
            StrQuery += "  Inner join tblProductDetail on tblProductDetail.appProductColorID=tblProductColor.appProductColorID   ";
            StrQuery += "  Inner join tblProductImage On tblProductImage.appProductColorID=tblProductColor.appProductColorID   ";
            StrQuery += "   inner join tblColor On tblColor.appColorID=tblProductColor.appColorID   ";
            StrQuery += "    Left Join tblsize on tblSize.appsizeId=tblProductDetail.appsizeId   ";
            StrQuery += "    where  tblProductImage.appIsDefault=1 and tblProductDetail.appIsDefault=1 and tblProductColor.appIsDefault=1 and tblDeal.appIsActive='true' order by tblDeal.appDisplayOrder ";
            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }
    }
}
