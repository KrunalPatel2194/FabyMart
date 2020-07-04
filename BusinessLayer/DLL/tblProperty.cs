
// Generated by MyGeneration Version # (1.3.0.3)

using System;
using System.Data;

namespace BusinessLayer
{
    public class tblProperty : _tblProperty
    {
        public tblProperty()
        {

        }
        public DataTable LoadGridData(string strColumnName, string strColumnValue)
        {
            strColumnValue = strColumnValue.Replace("'", "");

            string StrQuery = " select * from tblProperty where 1=1 ";

            if (!string.IsNullOrEmpty(strColumnValue) & strColumnName != "0")
            {
                StrQuery += " and " + strColumnName + " LIKE '%" + strColumnValue + "%'";
            }
            else if (strColumnName == "0" && string.IsNullOrEmpty(strColumnValue))
            {
                StrQuery += " and ( appPropertyName LIKE '%" + strColumnValue + "%'";
                StrQuery += " or  appDisplayName LIKE '%" + strColumnValue + "%' )";
            }
            StrQuery += " order by appDisplayOrder ";
            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }

        public DataTable LoadSearchProperty(string strCategory, string strSubCategory)
        {
            string StrQuery = "Select tblProperty.appPropertyID,tblProperty.appPropertyName,tblProperty.appDisplayName from tblProperty inner join (Select distinct(appPropertyID) from tblPropertyPreValue	inner join (Select distinct(appPropertyPreValueID)	From tblProductProperty	inner join (Select distinct(vw_SerachProduct.appproductid) From vw_SerachProduct  ";
            StrQuery += " Inner Join (Select distinct(tblProductSubCategory.appProductID) From tblProductSubCategory 	Inner Join tblSubCategory On tblSubCategory.appSubCategoryID=tblProductSubCategory.appSubCategoryID	Inner Join tblCategory On tblCategory.appCategoryID=tblSubCategory.appCategoryID ";
            StrQuery += " Where 1=1";
            if (strCategory != "")
            {
                StrQuery += " And appCategory='" + strCategory + "'";
            }
            if (strSubCategory != "")
            {
                StrQuery += " And appSubCategory='" + strSubCategory + "'";
            }
            StrQuery += " )tblProductSubCategory On tblProductSubCategory.appProductID=vw_SerachProduct.appProductID ";
            StrQuery += " )tblproduct on tblproduct.appproductid=tblProductProperty.appproductid ) tblProductProperty on tblProductProperty.appPropertyPreValueID=tblPropertyPreValue.appPropertyPreValueID) tblPropertyPreValue on tblPropertyPreValue.appPropertyID=tblProperty.appPropertyID ";
            StrQuery += " Where  tblProperty.appIsShowInSearch=1 ";
            StrQuery += " order by tblProperty.appDisplayOrder ";
            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }

        public DataTable LoadProductWiseProperty(string strProduct)
        {
            string StrQuery = "Select tblProperty.appPropertyID,tblProperty.appPropertyName,tblProperty.appDisplayName from tblProperty inner join (Select distinct(appPropertyID) from tblPropertyPreValue	inner join (Select distinct(appPropertyPreValueID)	From tblProductProperty	inner join (Select distinct(vw_SerachProduct.appproductid) From vw_SerachProduct  ";
            StrQuery += " Where  (vw_SerachProduct.appProductName Like  '%" + strProduct + "%' or vw_SerachProduct.appSKUNo Like  '" + strProduct + "') ";
            StrQuery += " )tblproduct on tblproduct.appproductid=tblProductProperty.appproductid ) tblProductProperty on tblProductProperty.appPropertyPreValueID=tblPropertyPreValue.appPropertyPreValueID) tblPropertyPreValue on tblPropertyPreValue.appPropertyID=tblProperty.appPropertyID ";
            StrQuery += " Where  tblProperty.appIsShowInSearch=1 ";
            StrQuery += " order by tblProperty.appDisplayOrder ";
            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }

        public DataTable LoadOtherSearchWiseProperty(string strTableName)
        {
            string StrQuery = "Select tblProperty.appPropertyID,tblProperty.appPropertyName,tblProperty.appDisplayName from tblProperty inner join (Select distinct(appPropertyID) from tblPropertyPreValue	inner join (Select distinct(appPropertyPreValueID)	From tblProductProperty	inner join (Select distinct(" + strTableName + ".appproductid) From  " + strTableName;
            StrQuery += " Where "+strTableName+".appIsActive=1 )tblproduct on tblproduct.appproductid=tblProductProperty.appproductid ) tblProductProperty on tblProductProperty.appPropertyPreValueID=tblPropertyPreValue.appPropertyPreValueID) tblPropertyPreValue on tblPropertyPreValue.appPropertyID=tblProperty.appPropertyID ";
            StrQuery += " Where  tblProperty.appIsShowInSearch=1 ";
            StrQuery += " order by tblProperty.appDisplayOrder ";
            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }
    }
}