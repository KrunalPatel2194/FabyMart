
// Generated by MyGeneration Version # (1.3.0.3)

using System;
using System.Data;

namespace BusinessLayer
{
    public class tblPageFormat : _tblPageFormat
    {
        public tblPageFormat()
        {

        }
        public DataTable LoadGridData(string strColumnName, string strColumnValue, string whereCondition = "")
        {
            string StrQuery = "select appPageFormatId, appPageName ,appPageFormatName, appIsActive from tblPageFormat Where 1=1";

            if (strColumnValue != "" && strColumnName != "0")
            {
                StrQuery += " and " + strColumnName + " LIKE '%" + strColumnValue + "%'";
            }
            else if (strColumnValue != "" && strColumnName == "0")
            {
                StrQuery += " and (" + ColumnNames.AppPageFormatName + " LIKE '%" + strColumnValue + "%' ";
                StrQuery += " OR " + ColumnNames.AppPageName + " LIKE '%" + strColumnValue + "%' )";
            }
            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }
    }
}