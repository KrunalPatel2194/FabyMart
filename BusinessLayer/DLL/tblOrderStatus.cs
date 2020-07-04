
// Generated by MyGeneration Version # (1.3.0.3)

using System;
using System.Data;

namespace BusinessLayer
{
	public class tblOrderStatus : _tblOrderStatus
	{
		public tblOrderStatus()
		{
		

		}
        public DataTable LoadGridData(string strColumnName, string strColumnValue)
        {
            strColumnValue = strColumnValue.Replace("'", "");

            string StrQuery = " select * from tblOrderStatus where 1=1 ";

            if (!string.IsNullOrEmpty(strColumnValue) & strColumnName != "0")
            {
                StrQuery += " and " + strColumnName + " LIKE '%" + strColumnValue + "%'";
            }
            else if (strColumnName == "0" && string.IsNullOrEmpty(strColumnValue))
            {
                StrQuery += " and ( appOrderStatus LIKE '%" + strColumnValue + "%'";
                StrQuery += " or  appOrderStatus LIKE '%" + strColumnValue + "%' )";
            }

            StrQuery += " order by appDisplayOrder  ";
            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }


        public void SetDefaultOrderStatus(string strOrderStatusId = "0")
        {
            string StrQuery = " UPDATE tblOrderStatus SET appIsDefault='False' ";
            base.LoadFromRawSql(StrQuery);
            if (strOrderStatusId != "0")
            {
                StrQuery = "UPDATE tblOrderStatus SET appIsDefault='True',appIsActive='True' Where appOrderStatusID=" + strOrderStatusId;
                base.LoadFromRawSql(StrQuery);
            }
        }
        public DataTable GetCountStatusWiseSubOrder()
        {
            string StrQuery = "Select tblOrderStatus.appOrderStatusID,isnull(tblSubOrder.TotalCount,0) as TotalCount From tblOrderStatus Left join ( Select tblsubOrder.appSubOrderStatusID,count(distinct tblsubOrder.appOrderID) as TotalCount	From tblsubOrder			 Group by tblsubOrder.appSubOrderStatusID ) tblSubOrder on tblsubOrder.appSubOrderStatusID=tblOrderStatus.appOrderStatusID ";
            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }
	}
}