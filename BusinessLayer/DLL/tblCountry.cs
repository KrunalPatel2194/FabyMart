
// Generated by MyGeneration Version # (1.3.0.3)

using System;
using System.Data;
namespace BusinessLayer
{
	public class tblCountry : _tblCountry
	{
		public tblCountry()
		{
		
		}
        public DataTable LoadGridData(string strColumnName, string strColumnValue)
        {
            strColumnValue = strColumnValue.Replace("'", "");

            string StrQuery = "select * from tblCountry ";

            if (!string.IsNullOrEmpty(strColumnValue) & strColumnName != "0")
            {
                StrQuery += "where " + strColumnName + " LIKE '%" + strColumnValue + "%'";
            }
            //  StrQuery += " order by appDisplayOrder ";
            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }
	}
}
