
// Generated by MyGeneration Version # (1.3.0.3)

using System;
using System.Data ;

namespace BusinessLayer
{
	public class tblCouponCode : _tblCouponCode
	{
		public tblCouponCode()
		{
		
		}
        public DataTable LoadGridData(string strColumnName, string strColumnValue, string strDateType, string strStartDate, string strEndDate)
        {
            strColumnValue = strColumnValue.Replace("'", "");

            string StrQuery = " select appCouponCodeID,appCouponCode,appDiscountPer,appIsActive,appType,convert(varchar(10),appStartDate,103) as appStartDate,convert(varchar(10),appEndDate,103) as appEndDate from tblCouponCode where 1=1 ";

            if (!string.IsNullOrEmpty(strColumnValue) & strColumnName != "0")
            {
                StrQuery += " and " + strColumnName + " LIKE '%" + strColumnValue + "%'";
            }
            else if (strColumnName == "0" && strColumnValue!="")
            {
                StrQuery += " and ( appCouponCode LIKE '%" + strColumnValue + "%'";
                StrQuery += " or  appDiscountPer LIKE '%" + strColumnValue + "%' )";
            }
            if (strDateType !="" && strStartDate != "" & strEndDate != "")
            {
                StrQuery += "and " + strDateType + " between convert(datetime,'" + strStartDate + "',103) and convert(datetime,'" + strEndDate + "',103) ";
            }
            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }
        //ProductDetail.aspx
        public DataTable CheckCouponCodeExists(string strCouponCode)
        {

            string StrQuery = " select appCouponCodeID,appCouponCode,appDiscountPer,appType from tblCouponCode where 1=1 ";

            if (!string.IsNullOrEmpty(strCouponCode))
            {
                StrQuery += "and appCouponCode='" + strCouponCode + "' and getdate() between convert(datetime,appStartDate,103) and convert(datetime,appEndDate,103) ";
            }

            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }
	}
}
