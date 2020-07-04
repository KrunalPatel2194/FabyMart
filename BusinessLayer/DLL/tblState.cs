
// Generated by MyGeneration Version # (1.3.0.3)

using System;
using System.Data;
namespace BusinessLayer
{
	public class tblState : _tblState
	{
		public tblState()
		{
		
		}
        public DataTable LoadGridData( String strStatename = "",string strCountry = "")
        {
            //strColumnValue = strColumnValue.Replace("'", "");

            string StrQuery = "select *,tblCountry.appCountry from tblState ";
            StrQuery += " inner join tblCountry on tblCountry.appCountryID=tblState.appCountryID where 1=1";
            if (strCountry != "0" && strCountry != "")
            {
                StrQuery += " and  tblCountry.appCountryID='" + strCountry + "'";
            }
            if (strStatename != "")
            {
                StrQuery += " and  tblState.appState  LIKE '%" + strStatename + "%' ";
            }
            //  StrQuery += " order by appDisplayOrder ";
            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }
        public DataTable getStatesFromCountryID(int strCountryID )
        {
            //strColumnValue = strColumnValue.Replace("'", "");

            string StrQuery = "select appState,appStateID from tblState where 1=1";

            if (strCountryID != 0)
            {
                StrQuery += " and  tblState.appCountryID =" + strCountryID + " ";
            }
            //  StrQuery += " order by appDisplayOrder ";
            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }
        public DataTable getCitiesFromStateID(int strStateID)
        {
            //strColumnValue = strColumnValue.Replace("'", "");

            string StrQuery = "select appCity,appCityID from tblCity where 1=1";

            if (strStateID != 0)
            {
                StrQuery += " and  tblCity.appStateID =" + strStateID + " ";
            }
            //  StrQuery += " order by appDisplayOrder ";
            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }
	}
}
