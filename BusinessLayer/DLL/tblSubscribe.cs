
// Generated by MyGeneration Version # (1.3.0.3)

using System;
using System.Data;
namespace BusinessLayer
{
	public class tblSubscribe : _tblSubscribe
	{
		public tblSubscribe()
		{
		
		}
        public DataTable LoadGridData(string strColumnName, string strColumnValue)
        {
            strColumnValue = strColumnValue.Replace("'", "");

            string StrQuery = " select * from tblSubscribe where 1=1 ";

            if (!string.IsNullOrEmpty(strColumnValue) & strColumnName != "0")
            {
                StrQuery += " and " + strColumnName + " LIKE '%" + strColumnValue + "%'";
            }

            StrQuery += " order by appCreatedDate desc ";
            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }
	}
}
