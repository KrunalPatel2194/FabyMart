
// Generated by MyGeneration Version # (1.3.0.3)

using System;
using System.Data;

namespace BusinessLayer
{
    public class tblUser : _tblUser
    {
        public tblUser()
        {

        }
        public DataTable LoadGridData(string strColumnName, string strColumnValue, Boolean isSuperAdmin, string strUserId = "")
        {
            strColumnValue = strColumnValue.Replace("'", "");
            string StrQuery = "select appUserId, appUserName, appFullName, appMobile, appPhone, appEmail,appAddress, appIsActive, appRoleName ";
            StrQuery += "from tblUser inner join tblRole on tblUser.appRoleId = tblRole.appRoleId Where 1=1";
            if (!isSuperAdmin)
            {
                StrQuery += " and tblUser.appCreatedBy = " + strUserId;
            }
            if (strColumnValue != "" && strColumnName != "0")
            {
                StrQuery += " and " + strColumnName + " LIKE '%" + strColumnValue + "%'";
            }
            else if (strColumnName == "0" && strColumnValue != "")
            {
                StrQuery += " and ( ";
                StrQuery += " appFullName like '%" + strColumnValue + "%' ";
                StrQuery += " or appEmail like '%" + strColumnValue + "%' ";
                StrQuery += " or appRoleName like '%" + strColumnValue + "%' ";
                StrQuery += " or appMobile like '%" + strColumnValue + "%' ";
                StrQuery += " or appUserName like '%" + strColumnValue + "%' ";
                StrQuery += " ) ";
            }

            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;

        }
        public DataTable LastLoggedInUsers()
        {
            string StrQuery = "select  appFullName, appRoleName, CONVERT(VARCHAR(10), appLastLoginTime, 111) as appLoginDate, ";
            StrQuery += " CONVERT(VARCHAR(8), appLastLoginTime,108) AS appLoginTime from tblUser inner join tblRole on tblUser.appRoleId = tblRole.appRoleId order by tblUser.appLastLoginTime Desc";

            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }
        public DataTable GetDashBoardInfo(string[] strColNames)
        {
            string StrQuery = "select (select count(*) from tblUser) as " + strColNames[0];
            StrQuery += ", (select count(*) from tblUser where appIsActive = 'true') as " + strColNames[1];
            StrQuery += ", (select count(*) from tblUser where appIsActive = 'false') as " + strColNames[2];
            StrQuery += ", (select count(*) from tblPage) as " + strColNames[3];
            StrQuery += ", (select count(*) from tblPage where appIsLink = 'true') as " + strColNames[4];
            StrQuery += ", (select count(*) from tblPage where appIsLink = 'false') as " + strColNames[5];
            StrQuery += ", (select count(*) from tblMenuType) as " + strColNames[6];
            StrQuery += ", (select count(*) from tblMenuType where appIsActive = 'true') as " + strColNames[7];
            StrQuery += ", (select count(*) from tblMenuType where appIsActive = 'false') as " + strColNames[8];

            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }
        public int GetTotalUser()
        {
            string StrQuery = "Select count(*) as Total from tblUser	Where appIsSuperAdmin<>1 ";

            base.LoadFromRawSql(StrQuery);
            if (base.DefaultView.Table.Rows.Count > 0)
            {
                if (base.DefaultView.Table.Rows[0][0].ToString() != "")
                {
                    return Convert.ToInt32(base.DefaultView.Table.Rows[0][0].ToString());
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
    }
}
