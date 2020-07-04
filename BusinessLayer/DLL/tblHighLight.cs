
// Generated by MyGeneration Version # (1.3.0.3)

using System;
using System.Data;
using System.Data.SqlClient;


namespace BusinessLayer
{
    public class tblHighLight : _tblHighLight
    {
        public tblHighLight()
        {

        }

        public DataTable LoadGridData(string strColumnName, string strColumnValue)
        {
            strColumnValue = strColumnValue.Replace("'", "");

            string StrQuery = "select appHighLightID, appTitle, appImage,appIsActive,appDisplayOrder,appUrl  from tblHighLight ";

            if (!string.IsNullOrEmpty(strColumnValue) & strColumnName != "0")
            {
                StrQuery += "where " + strColumnName + " LIKE '%" + strColumnValue + "%'";
            }
            StrQuery += " order by appDisplayOrder ";
            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }
        public DataTable LoadHighLight(string strTop = "")
        {
            string StrQuery = "select ";
            if (strTop != "")
            {
                StrQuery += "  Top " + strTop;
            }
            StrQuery += "   appHighLightID, appTitle, appImage,appIsActive,appDisplayOrder,appUrl from tblHighLight  Where appIsActive=1 order by appDisplayOrder ";
            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }

    }
}
