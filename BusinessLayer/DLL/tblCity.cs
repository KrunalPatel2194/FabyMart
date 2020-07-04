
// Generated by MyGeneration Version # (1.3.0.3)

using System;
using System.Data;
namespace BusinessLayer
{
	public class tblCity : _tblCity
	{
		public tblCity()
		{
		
		}
        public DataTable LoadGridData( String strCity = "",string strCountry = "", string strState = "")
        {
           // strColumnValue = strColumnValue.Replace("'", "");

            string StrQuery = "SELECT  tblCity.*,   tblCountry.appCountry, tblState.appState FROM tblCity  ";
            StrQuery += " INNER JOIN tblState ON tblCity.appStateID = tblState.appStateID  ";
            StrQuery += " INNER JOIN tblCountry ON tblState.appCountryID = tblCountry.appCountryID  where 1=1 ";
            if (strCity != "")
            {
                StrQuery += " and  tblCity.appCity LIKE '%" + strCity + "%' ";
            }
            if (strCountry != "0" && strCountry != "")
            {
                StrQuery += " and  tblCountry.appCountryID='" + strCountry + "'";
            }
            if (strState != "0" && strState != "")
            {
                StrQuery += " and  tblState.appStateID='" + strState + "'";
            }
            
            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }
	}
}
