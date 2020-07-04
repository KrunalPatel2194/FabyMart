
// Generated by MyGeneration Version # (1.3.0.3)

using System;
using System.Data;
using System.Collections.Specialized;

namespace BusinessLayer
{
	public class tblMenuItem : _tblMenuItem
	{
		public tblMenuItem()
		{
		
		}


        public DataTable LoadGridData(string strColumnName, string strColumnValue, string whereCondition = "")
        {
            strColumnValue = strColumnValue.Replace("'", "");

            string StrQuery = "select main.appMenuItemId, main.appMenuItem, main.appIsActive, main.appDisplayOrder, (select count(*) from tblMenuItem where appParentId = main.appMenuItemId) as appChildCount ";
            StrQuery += " from tblMenuItem as main ";

            if (string.IsNullOrEmpty(whereCondition))
            {
                StrQuery += " where 1 = 1 ";
            }
            else
            {
                StrQuery += " where " + whereCondition;
            }

            if (!string.IsNullOrEmpty(strColumnValue) & strColumnName != "0")
            {
                StrQuery += " and main." + strColumnName + " LIKE '%" + strColumnValue + "%'";
            }
            else if (!string.IsNullOrEmpty(strColumnName))
            {
                StrQuery += " and main." + ColumnNames.AppMenuItem + " LIKE '%" + strColumnValue + "%'  ";
            }

            StrQuery += " order by appDisplayOrder Asc ";
            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }

        public DataTable GetSiteMap(int appMenuItemId)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add("@MenuItemId", appMenuItemId);

            base.LoadFromSql("getSiteMap", parameters);
            return base.DefaultView.Table;
        }

        public DataTable GetMenuType(int MenuItemId, bool IsMenuType)
        {
            string StrQuery = "";
            if (IsMenuType == false)
            {
                StrQuery = "select tblMenuItem.appMenuTypeId, appMenuTypeName from tblMenuItem inner join tblMenuType ";
                StrQuery += " on tblMenuItem.appMenuTypeId = tblMenuType.appMenuTypeId  where appMenuItemId = " + MenuItemId.ToString();
            }
            else
            {
                StrQuery = " Select appMenuTypeID, appMenuTypeName from tblMenuType where appMenuTypeID=" + MenuItemId;
            }

            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }

        public DataTable GetChildMenus(int MenuTypeId)
        {
            string StrQuery = "select tblMenuItem.appMenuItemId, tblPage.appPageId, tblPage.appAlias, tblPage.appIsOpenInNewTab, tblMenuItem.appParentId, tblMenuItem.appDisplayOrder, tblMenuItem.appMenuItem,tblMenuItem.appMenuItemTypeID,tblMenuItemType.appMenuItemType,(select count(*) from tblMenuItem a where a.appParentId = tblMenuItem.appMenuItemId) as appchildCount  from tblMenuItem Left join tblPage on tblPage.appPageId = tblMenuItem.appPageId  Left Join tblMenuItemType on tblMenuItemType.appMenuItemTypeID=tblMenuItem.appMenuItemTypeID ";
            StrQuery += "  Where tblMenuItem.appMenuTypeId = " + MenuTypeId.ToString() + "and tblMenuItem.appIsActive = 1";

            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }

        public DataTable  LoadMenuItems(string MenuTypeId = "", string parentID = "")
        {
            string StrQuery = "select appMenuItemId, appMenuItem, appMenuTypeId, appParentId,appMenuItemTypeID, appDisplayOrder from tblMenuItem";
            StrQuery += " where appIsActive = 1 ";

            if (!string.IsNullOrEmpty(MenuTypeId))
            {
                StrQuery += " And  appMenuTypeId = " + MenuTypeId;
            }

            if (!string.IsNullOrEmpty(parentID))
            {
                StrQuery += " And appParentId = " + parentID;
            }

            StrQuery += " order by appDisplayOrder Asc";

            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }

        public DataTable  getMenuItem(int intMenuTypeId, int intNoOfLevel)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add("@MenuTypeId", intMenuTypeId);
            parameters.Add("@NoOfLevel", intNoOfLevel);

            base.LoadFromSql("getParentName", parameters);

            return base.DefaultView.Table;
        }

        public DataTable GetSiteMapDT(int intMenuItemID)
        {
            string strQry = " with cteLevels as ( ";
            strQry += " select t.appMenuItemId, t.appParentId, t.appMenuItem, t.appPageID from tblMenuItem t where t.appMenuItemId =" + intMenuItemID;
            strQry += " union all ";
            strQry += " select t.appMenuItemId, t.appParentId, t.appMenuItem, t.appPageID from tblMenuItem t inner join cteLevels c on t.appMenuItemId = c.appParentId ";
            strQry += " ) ";
            strQry += " select c.*,P.appAlias from cteLevels c left join tblPage p on c.appPageID=P.appPageID order by appParentId ";
            base.LoadFromRawSql(strQry);
            return base.DefaultView.Table;
        }

        public DataTable LoadSitemapMenuItem(int intParentID, int intMenuTypeID, string strServerURL, string strWhere = "")
        {
            string strQry = " Select appMenuItemID,appMenuItem,'" + strServerURL + "/' + appAlias as appAlias from tblMenuItem MI left join tblPage P on MI.appPageID=P.appPageID ";
            strQry += " where 1=1 ";

            if (!string.IsNullOrEmpty(strWhere))
            {
                strQry += " and " + strWhere;
            }
            else
            {
                strQry += " and MI.appParentID = " + intParentID;
            }

            if (intMenuTypeID != 0)
            {
                strQry += " and MI.appMenuTypeID=" + intMenuTypeID;
            }



            strQry += " Order By appDisplayOrder ";

            base.LoadFromRawSql(strQry);
            return base.DefaultView.Table;
        }

        public DataTable LoadMenuItemFromControlID(string strServerURL, string srtControlID, int intParentID = -1)
        {
            string strQry = "select MI.appMenuItemID,MI.appMenuItem,'" + strServerURL + "' + P.appAlias as appAlias,MI.appMenuItemTypeID from tblMenuType MY inner join tblBlock B on MY.appBlockID=B.appBlockID ";
            strQry += " inner join tblMenuItem MI on MI.appMenuTypeID=MY.appMenuTypeID ";
            strQry += " left join tblPage P on MI.appPageID=P.appPageID ";
            strQry += " where appControlID='" + srtControlID + "' ";

            if (intParentID != -1)
            {
                strQry += " and appParentID=" + intParentID;
            }

            strQry += " and MI.appIsActive='true' Order By MI.appDisplayOrder Asc ";

            base.LoadFromRawSql(strQry);
            return base.DefaultView.Table;
        }

	}
}
