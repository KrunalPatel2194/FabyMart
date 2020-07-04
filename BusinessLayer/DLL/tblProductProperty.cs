
// Generated by MyGeneration Version # (1.3.0.3)

using System;
using System.Data;

namespace BusinessLayer
{
    public class tblProductProperty : _tblProductProperty
    {
        public tblProductProperty()
        {

        }
        public DataTable LoadGridData(string strProductID)
        {

            string StrQuery = " Select DISTINCT ( tblPropertySubCategory.appPropertyID ),tblProperty.appPropertyName,tblProperty.appIsPredefine,tblProductProperty.appValue,tblProductProperty.appPropertyPreValueID,tblProperty.appDisplayOrder	From tblPropertySubCategory 	Inner Join tblProperty on tblProperty.appPropertyID=tblPropertySubCategory.appPropertyID	Inner Join tblProductSubCategory On tblProductSubCategory.appSubCategoryId=tblPropertySubCategory.appSubCategoryId	";

            StrQuery += " Left Join (select * From tblProductProperty ";

            if (strProductID != "0")
            {
                StrQuery += " Where  appProductID=" + strProductID;
            }
            StrQuery += " ) tblProductProperty On tblProductProperty.appPropertyID=tblPropertySubCategory.appPropertyID 	";
            StrQuery += " Where  tblProductSubCategory.appProductID=" + strProductID;
            StrQuery += " order by tblProperty.appDisplayOrder  ";
            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }

        public DataTable LoadProductProperty(string strProductID)
        {

            string StrQuery = " Select tblProductProperty.appProductId,tblProductProperty.appProductProertyID,Case When tblProductProperty.appValue Is Null then tblPropertyPreValue.appPreValue Else tblProductProperty.appValue End appValue,tblProductProperty.appPropertyPreValueID,tblProperty.appPropertyName,tblProperty.appDisplayName	From tblProductProperty	Inner Join tblProperty On tblProperty.appPropertyID=tblProductProperty.appPropertyID	Left join tblPropertyPreValue on tblPropertyPreValue.appPropertyPreValueID=tblProductProperty.appPropertyPreValueID";
            StrQuery += " Where  tblProductProperty.appProductID=" + strProductID;
            StrQuery += " order by tblProperty.appDisplayOrder";
            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }

        public void DeleteProperty(string strProductID, string strSubCategoryId)
        {

            string StrQuery = " Delete from tblProductProperty where tblProductProperty.appProductID=" + strProductID + " and  appPropertyID in (Select DISTINCT(appPropertyID) from tblPropertySubCategory 	where appSubCategoryID=" + strSubCategoryId + " except Select DISTINCT(appPropertyID) from tblPropertySubCategory 	where  appSubCategoryID in( Select appSubCategoryID From tblProductSubCategory where appProductID=" + strProductID + ") and appSubCategoryID<>" + strSubCategoryId + ")";
           
            base.LoadFromRawSql(StrQuery);

        }
    }
}