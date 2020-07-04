
// Generated by MyGeneration Version # (1.3.0.3)

using System;
using System.Data;


namespace BusinessLayer
{
	public class tblCategory : _tblCategory
	{
		public tblCategory()
		{
		
		}
        public DataTable LoadGridData(string strColumnName, string strColumnValue)
        {
            strColumnValue = strColumnValue.Replace("'", "");

            string StrQuery = " select * from tblCategory where 1=1 ";

            if (!string.IsNullOrEmpty(strColumnValue) & strColumnName != "0")
            {
                StrQuery += " and " + strColumnName + " LIKE '%" + strColumnValue + "%'";
            }
            
            StrQuery += " order by appDisplayOrder  ";
            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }
        public DataTable LoadAllCategory()
        {

            string StrQuery = " select * from tblCategory where tblCategory.appIsActive=1 ";

            StrQuery += " order by tblCategory.appDisplayOrder  ";
            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }
	}
}
