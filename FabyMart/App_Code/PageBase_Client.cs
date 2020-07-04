using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using BusinessLayer;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Configuration;

public class PageBase_Client : PageBase
{
    DataTable objBlockDt = new DataTable();
    tblBlock objBlock;
    tblMenuItem objMenuItem;
    tblMenuType objMenuType;
    DataTable objMenuDt = new DataTable();
    public string strServerURL = PageBase.GetServerURL() + "/";
    public string projectPrefix = GetServerURL() + "/";
    public string metaKeywords = "";

    public string metaDescription = "";
    private void Page_Load(object sender, System.EventArgs e)
    {
        SetCurrency();
        if ((ViewState["CurrentPageTitle"] != null))
        {
            if (!string.IsNullOrEmpty(ViewState["CurrentPageTitle"].ToString().Trim()))
            {
                Page.Title = ViewState["CurrentPageTitle"].ToString();
                metaDescription = ViewState["CurrentPageMetaDesc"].ToString();
                metaKeywords = ViewState["CurrentPageMetaKeyWords"].ToString();
            }
        }
        if (!IsPostBack)
        {
            SetUpBlocks();
        }

    }

    public void SetUpBlocks()
    {
        tblBlock objBlock = new tblBlock();
        StringBuilder blockContent = new StringBuilder();
        objBlockDt = objBlock.GetBlockByControlId();
        if (Cache["SiteName"] == null)
        {
            tblSettings objSettings = new tblSettings();
            objSettings.Query.AddResultColumn(tblSettings.ColumnNames.AppSiteName);
            objSettings.Query.AddResultColumn(tblSettings.ColumnNames.AppSiteTagLine);
            objSettings.Query.AddResultColumn(tblSettings.ColumnNames.AppSiteFavicon);
            objSettings.LoadAll();
            if (objSettings.RowCount > 0)
            {
                Cache["SiteName"] = objSettings.AppSiteName;
            }
        }

        for (int i = 0; i <= objBlockDt.Rows.Count - 1; i++)
        {
            blockContent = new StringBuilder();
            DataRow dr = objBlockDt.Rows[i];

            if (dr["appMenuTypeId"].ToString() != DBNull.Value.ToString() & (bool)dr["appIsShowContent"] == false)
            {
                objMenuItem = new tblMenuItem();
                objMenuDt = new DataTable();
                objMenuDt = objMenuItem.GetChildMenus((int)dr["appMenuTypeId"]);

                if (string.Compare(dr["appControlId"].ToString(), "divTopMenu") == 0)
                {

                    blockContent.Append(" <div style=\"background-color: #ED258F;\">");
                    blockContent.Append(" <div class=\"wrap\">");
                    blockContent.Append(" <div class=\"menu\">");
                    blockContent.Append("<ul class=\"megamenu skyblue\">");
                    LoadTopMenu(ref blockContent, 0);
                    blockContent.Append("</ul></div></div></div>");

                }
                else if (string.Compare(dr["appControlId"].ToString(), "divFooterBlock1") == 0)
                {

                    objMenuType = new tblMenuType();
                    if (objMenuType.LoadByPrimaryKey((int)dr["appMenuTypeId"]))
                    {
                        LoadFooterMenu(ref blockContent, 0, objMenuType.AppMenuTypeName);
                    }
                    objMenuType = null;

                }
                else if (string.Compare(dr["appControlId"].ToString(), "divFooterBlock2") == 0)
                {

                    objMenuType = new tblMenuType();
                    if (objMenuType.LoadByPrimaryKey((int)dr["appMenuTypeId"]))
                    {
                        LoadCategory(ref blockContent);
                    }
                    objMenuType = null;

                }
                else if (string.Compare(dr["appControlId"].ToString(), "divCategoryFooter") == 0)
                {
                    LoadCategoryFooter(ref blockContent);
                }
                else if (dr["appMenuTypeId"].ToString() != DBNull.Value.ToString())
                {
                    blockContent.Append("<ul>");
                    SetUpMenu(ref blockContent, 0);
                    blockContent.Append("</ul>");
                }

            }
            else if ((bool)dr["appIsShowContent"] == true)
            {
                blockContent.Append(dr["appContent"]);
            }

            StringBuilder strContent = new StringBuilder();
            strContent.Append(blockContent);
            blockContent = new StringBuilder();
            blockContent.Append(strContent.ToString().Replace("~GetServerURL()~", PageBase.GetServerURL() + "/"));


            if ((this.Master.FindControl("ContentPlaceHolder1").FindControl(dr["appControlId"].ToString()) != null))
            {
                ((HtmlContainerControl)this.Master.FindControl("ContentPlaceHolder1").FindControl(dr["appControlId"].ToString())).InnerHtml = blockContent.ToString();
            }
            else if ((this.Master.FindControl(dr["appControlId"].ToString()) != null))
            {
                ((HtmlContainerControl)this.Master.FindControl(dr["appControlId"].ToString())).InnerHtml = blockContent.ToString();
            }

        }

    }

    bool IsFirstMenu = true;

    public void LoadTopMenu(ref StringBuilder targetString, int intParentID)
    {
        DataRow[] dr = objMenuDt.Select(tblMenuItem.ColumnNames.AppParentId + "=" + intParentID, tblMenuItem.ColumnNames.AppDisplayOrder + " Asc");
        if (dr.Length > 0)
        {
            int i = 0;
            foreach (DataRow Row in dr)
            {
                targetString.Append("<li class=\" ");

                if (Convert.ToInt32(Row["appchildCount"].ToString()) > 0)
                {
                    targetString.Append("active_ has-sub");
                }
                if (i == dr.Length - 1)
                {

                    targetString.Append(" last");
                }
                targetString.Append(" \">");
                targetString.Append("<a href='" + projectPrefix + Row["appAlias"].ToString() + "'>");
                if (Row["appMenuItemType"].ToString() == "Home")
                {
                    targetString.Append("<img src=\"" + projectPrefix + "Images/home.png\" width=\"16px\" style=\"padding:2px 0px;\" />");
                }
                else
                {
                    targetString.Append(Row["appMenuItem"].ToString());
                }
                targetString.Append("</a>");
                


                //if (Row["appMenuItemId"].ToString() == "-1")
                //{
                //    targetString.Append("<ul>");
                //    SetupStaticMenu(ref targetString);
                //    targetString.Append("</ul>");
                //}
                //else 
                if (Convert.ToInt32(Row["appchildCount"].ToString()) > 0)
                {
                    targetString.Append("<ul>");
                    LoadTopMenu(ref targetString, Convert.ToInt32(Row[tblMenuItem.ColumnNames.AppMenuItemId]));
                    targetString.Append("</ul>");
                }
                targetString.Append("</li>");

                i++;
                if (i == 1)
                {
                    SetupStaticMenu(ref targetString);
                }
            }
        }
        else if (dr.Length == 0 && intParentID == 0)
        {
            SetupStaticMenu(ref targetString);
        }
    }

    public void SetupStaticMenu(ref StringBuilder targetString)
    {
        tblCategory objCategory = new tblCategory();
        DataTable dtCategory = objCategory.LoadAllCategory();
        if (dtCategory.Rows.Count > 0)
        {
            string PageName = "";
            PageName = GetAlias("Products.aspx");
            tblSubCategory objSubCategory = new tblSubCategory();
            DataTable dtSubCategory = objSubCategory.LoadAllSubCategory();
            objSubCategory = null;
            foreach (DataRow Row in dtCategory.Rows)
            {
                targetString.Append("<li>");
                targetString.Append("<a href='" + PageName + generateUrl(Row[tblCategory.ColumnNames.AppCategory].ToString()) + "'>" + Row[tblCategory.ColumnNames.AppCategory].ToString() + "</a>");
                targetString.Append("<a class=\"plus\">+</a>");

                DataRow[] Subdr = dtSubCategory.Select(tblSubCategory.ColumnNames.AppCategoryID + "=" + Row[tblCategory.ColumnNames.AppCategoryID], tblSubCategory.ColumnNames.AppDisplayOrder + " Asc");
                if (Subdr.Length > 0)
                {
                    targetString.Append("<div class=\"megapanel\">");
                    targetString.Append(" <div class=\"row\">");
                    targetString.Append(" <div class=\"col2\">");
                    targetString.Append(" <div class=\"h_nav\">");
                    targetString.Append(" <h4>");
                    targetString.Append(Row[tblCategory.ColumnNames.AppCategory].ToString());
                    targetString.Append("</h4>");

                    int i = 0;
                    targetString.Append("<ul>");
                    foreach (DataRow SubRow in Subdr)
                    {
                        targetString.Append("<li>");
                        //if (i == Subdr.Length - 1)
                        //{

                        //    targetString.Append(" last");
                        //}
                        //targetString.Append(" \">");
                        targetString.Append("<a href='" + PageName + generateUrl(Row[tblCategory.ColumnNames.AppCategory].ToString()) + "/" + generateUrl(SubRow[tblSubCategory.ColumnNames.AppSubCategory].ToString()) + "'>" + SubRow[tblSubCategory.ColumnNames.AppSubCategory].ToString() + "</a>");
                        targetString.Append("</li>");
                        i++;
                    }
                    targetString.Append("</ul>");
                    targetString.Append("</div>");
                    targetString.Append("</div>");
                    tblExclusive objExclusive = new tblExclusive();
                    DataTable dt = objExclusive.LoadTop2Data(Row[tblCategory.ColumnNames.AppCategoryID].ToString());
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dtRow in dt.Rows)
                        {
                            //targetString.Append("<a href='" + projectPrefix + Row["appAlias"].ToString() + "'>");
                            //if (Row["appMenuItemType"].ToString() == "Home")
                            //{
                            //    targetString.Append("<img src=\"" + projectPrefix + "Images/home.png\" width=\"16px\" style=\"padding:2px 0px;\" />");
                            //}

                            targetString.Append("<a href='" + dtRow[tblExclusive.ColumnNames.AppLink].ToString() + "'>");
                            targetString.Append("<div class=\"col2\"><div class=\"h_nav\"><h4>");
                            if (dtRow[tblExclusive.ColumnNames.AppTitle].ToString() != "")
                            {
                                targetString.Append(dtRow[tblExclusive.ColumnNames.AppTitle].ToString());
                            }
                            else
                            {
                                targetString.Append("Exclusive");
                            }
                            targetString.Append("</h4><div class=\"menuimg\"><img src=\"" + strServerURL + "admin/" + dtRow[tblExclusive.ColumnNames.AppImage].ToString() + "\"  /></div></div></div>");
                            targetString.Append("</a>");
                            //targetString.Append("<div class=\"col2\"><div class=\"h_nav\"><h4>Exclusive</h4><img src=\"" + strServerURL + "images/222.jpg\" width=\"100%\" /></div></div>");
                        }
                    }
                    objExclusive = null;

                    targetString.Append("</div>");
                    targetString.Append("</div>");
                }
                targetString.Append("</li>");
            }
        }
        dtCategory = null;
        objCategory = null;
        //string PageName = "";
        //if (Enums.Enums_Type.Solution == intEnum)
        //{
        //    tblSolution objSolution = new tblSolution();
        //    objData = objSolution.GetAllSolution();
        //    objSolution = null;
        //    PageName = GetAlias("SolutionDetails.aspx");
        //}
        //else if (Enums.Enums_Type.Product == intEnum)
        //{
        //    tblProduct objProduct = new tblProduct();
        //    objData = objProduct.GetAllProduct();
        //    objProduct = null;
        //    PageName = GetAlias("ProductDetails.aspx");
        //}
        //if (objData.Rows.Count > 0)
        //{
        //    targetString.Append("<ul class=\"dropdown-menu\" > ");
        //    foreach (DataRow row in objData.Rows)
        //    {
        //        targetString.Append("<li><a href='" + PageName + generateUrl(row["appPage"].ToString()) + "'>" + row["appPage"].ToString() + "</a></li>");
        //    }

        //    targetString.Append("</ul>");
        //}
        //objData = null;
    }

    public void LoadCategoryFooter(ref StringBuilder targetString)
    {
        tblCategory objCategory = new tblCategory();
        DataTable dtCategory = objCategory.LoadAllCategory();
        if (dtCategory.Rows.Count > 0)
        {
            string PageName = "";
            PageName = GetAlias("Products.aspx");
            targetString.Append("<ul class=\"f-list1\">");
            foreach (DataRow Row in dtCategory.Rows)
            {
                targetString.Append("<li>");
                targetString.Append("<a href='" + PageName + generateUrl(Row[tblCategory.ColumnNames.AppCategory].ToString()) + "'>" + Row[tblCategory.ColumnNames.AppCategory].ToString() + "</a>");
                targetString.Append("</li>");
            }
            targetString.Append("</ul>");
        }
        dtCategory = null;
        objCategory = null;

    }

    public void LoadFooterMenu(ref StringBuilder targetString, int intParentID, String strMenuType)
    {
        targetString.Append("<h4>" + strMenuType + "</h4>");
        targetString.Append("<ul class=\"f_nav\">");

        DataRow[] dr = objMenuDt.Select(tblMenuItem.ColumnNames.AppParentId + "=" + intParentID, tblMenuItem.ColumnNames.AppDisplayOrder + " Asc");

        foreach (DataRow Row in dr)
        {
            targetString.Append("<li>");

            targetString.Append("<a href='" + projectPrefix + Row["appAlias"].ToString() + "'>" + Row["appMenuItem"].ToString() + "</a>");

            targetString.Append("</li>");

        }
        targetString.Append("</ul>");

    }

    public void LoadCategory(ref StringBuilder targetString)
    {
        targetString.Append("<h4>Shop Online</h4>");
        tblCategory objCategory = new tblCategory();
        objCategory.Where.AppIsActive.Value = true;
        objCategory.Query.Load();

        targetString.Append("<ul class=\"f_nav\">");

        while (objCategory.EOF != true)
        {
            targetString.Append("<li>");

            targetString.Append("<a href='" + GetAlias("Product.aspx") + objCategory.AppCategory + "'>" + objCategory.AppCategory + "</a>");

            targetString.Append("</li>");

            objCategory.MoveNext();
        }

        targetString.Append("</ul>");

    }


    public void SetUpMenu(ref StringBuilder targetString, int intId)
    {
        //By rahul prajapati 06102014
        //dynamic ChildMenus = from res in objMenuDt.AsEnumerablewhere res(tblMenuItem.ColumnNames.AppParentId) == intIdorderby res.Field<int>(tblMenuItem.ColumnNames.AppDisplayOrder)resres.Field<int>("appParentId")GroupparentIdGroup;

        //foreach (object Data in ChildMenus) {

        //    foreach (DataRow row in Data.childMenuItems) {
        //        if (row["appChildCount"] > 0) {
        //            targetString.Append("<li ><a href='" + strServerURL + row["appAlias"].ToString() + "'");
        //        } else if (row["appChildCount"] == 0) {
        //            targetString.Append("<li><a href='" + strServerURL + row["appAlias"].ToString() + "'");
        //        }

        //        if (row["appIsOpenInNewTab"].ToString() == "True") {
        //            targetString.Append(" target = '_blank' ");
        //        }

        //        targetString.Append(">" + row["appMenuItem"] + "</a>");


        //        if (row["appChildCount"] > 0) {
        //            //If classNameForUl <> "" Then
        //            //    targetString.Append("<ul class='" + classNameForUl + "'>")
        //            //Else
        //            //    targetString.Append("<ul>")
        //            //End If
        //            targetString.Append("<ul>");
        //            SetUpMenu(ref targetString, row["appMenuItemId"]);
        //            targetString.Append("</ul>");
        //        }

        //        targetString.Append("</li>");
        //    }
        //}

    }
    public void SetUpPageContent(ref string metaDescription, ref string metaKeywords)
    {

        string pageName = "";
        pageName = GetPageAliasFromURL();


        tblPage objPage = new tblPage();

        DataTable dtPageDetail = objPage.GetPageDetailByAlias();
        DataRow[] arDataRow;
        if (Request.CurrentExecutionFilePath.ToLower().Contains("default.aspx") && Request.FilePath.ToLower().Contains("default.aspx"))
        {
            arDataRow = dtPageDetail.Select("appIsDefault='true'");
        }
        else
        {
            arDataRow = dtPageDetail.Select("appAlias='" + pageName + "'");
        }


        if (!(arDataRow.Length > 0))
        {
            arDataRow = dtPageDetail.Select("appAlias='" + pageName.Split('/')[0] + "/{*name}" + "'");
        }


        if (arDataRow.Length > 0)
        {
            Page.Title = arDataRow[0][tblPage.ColumnNames.AppPageTitle].ToString();

            ViewState["CurrentPageTitle"] = arDataRow[0][tblPage.ColumnNames.AppPageTitle];



            metaDescription = arDataRow[0][tblPage.ColumnNames.AppSEODescription].ToString();
            metaKeywords = arDataRow[0][tblPage.ColumnNames.AppSEOWord].ToString();

            ViewState["CurrentPageMetaDesc"] = arDataRow[0][tblPage.ColumnNames.AppSEODescription];
            ViewState["CurrentPageMetaKeyWords"] = arDataRow[0][tblPage.ColumnNames.AppSEOWord];



        }
    }

    public void SetUpPageContent(ref HtmlGenericControl PageContentControl, ref HtmlGenericControl PageHeadingControl, ref string metaDescription, ref string metaKeywords)
    {

        string pageName = "";
        pageName = GetPageAliasFromURL();


        tblPage objPage = new tblPage();

        DataTable dtPageDetail = objPage.GetPageDetailByAlias();

        DataRow[] arDataRow = dtPageDetail.Select("appAlias='" + pageName + "'");
        if (!(arDataRow.Length > 0))
        {
            arDataRow = dtPageDetail.Select("appAlias='" + pageName.Split('/')[0] + "/{*name}" + "'");
        }

        //objPage.Where.AppPageName.Value = pageName
        //'objPage.Where.AppAlias.Value = pageName
        //'objPage.Query.AddResultColumn(tblPage.ColumnNames.AppPageId)
        //'objPage.Query.AddResultColumn(tblPage.ColumnNames.AppPageTitle)
        //'objPage.Query.AddResultColumn(tblPage.ColumnNames.AppPageContent)
        //'objPage.Query.AddResultColumn(tblPage.ColumnNames.AppPageHeading)
        //'objPage.Query.AddResultColumn(tblPage.ColumnNames.AppSEOWord)
        //'objPage.Query.AddResultColumn(tblPage.ColumnNames.AppSEODescription)
        //'objPage.Query.Load()


        if (arDataRow.Length > 0)
        {
            Page.Title = arDataRow[0][tblPage.ColumnNames.AppPageTitle].ToString();

            ViewState["CurrentPageTitle"] = arDataRow[0][tblPage.ColumnNames.AppPageTitle];


            if ((PageContentControl != null))
            {
                PageContentControl.InnerHtml = arDataRow[0][tblPage.ColumnNames.AppPageContent].ToString().Replace("<pre ", "<p ").Replace("</pre>", "</p>");
                PageContentControl.InnerHtml = PageContentControl.InnerHtml.Replace("~GetServerURL()~", PageBase.GetServerURL() + "/");
            }

            if ((PageHeadingControl != null))
            {
                PageHeadingControl.InnerText = arDataRow[0][tblPage.ColumnNames.AppPageHeading].ToString();
            }

            metaDescription = arDataRow[0][tblPage.ColumnNames.AppSEODescription].ToString();
            metaKeywords = arDataRow[0][tblPage.ColumnNames.AppSEOWord].ToString();

            ViewState["CurrentPageMetaDesc"] = arDataRow[0][tblPage.ColumnNames.AppSEODescription];
            ViewState["CurrentPageMetaKeyWords"] = arDataRow[0][tblPage.ColumnNames.AppSEOWord];


        }
    }

    public void SetUpPageContent(ref HtmlGenericControl PageContentControl, ref HtmlGenericControl PageHeadingControl, ref string metaDescription, ref string metaKeywords, ref HtmlGenericControl objSiteMapControl)
    {

        string pageName = "";
        pageName = GetPageAliasFromURL();


        tblPage objPage = new tblPage();

        DataTable dtPageDetail = objPage.GetPageDetailByAlias();

        DataRow[] arDataRow = dtPageDetail.Select("appAlias='" + pageName + "'");
        if (!(arDataRow.Length > 0))
        {
            arDataRow = dtPageDetail.Select("appAlias='" + pageName.Split('/')[0] + "/{*name}" + "'");
        }

        //objPage.Where.AppPageName.Value = pageName
        //'objPage.Where.AppAlias.Value = pageName
        //'objPage.Query.AddResultColumn(tblPage.ColumnNames.AppPageId)
        //'objPage.Query.AddResultColumn(tblPage.ColumnNames.AppPageTitle)
        //'objPage.Query.AddResultColumn(tblPage.ColumnNames.AppPageContent)
        //'objPage.Query.AddResultColumn(tblPage.ColumnNames.AppPageHeading)
        //'objPage.Query.AddResultColumn(tblPage.ColumnNames.AppSEOWord)
        //'objPage.Query.AddResultColumn(tblPage.ColumnNames.AppSEODescription)
        //'objPage.Query.Load()


        if (arDataRow.Length > 0)
        {
            Page.Title = arDataRow[0][tblPage.ColumnNames.AppPageTitle].ToString();

            ViewState["CurrentPageTitle"] = arDataRow[0][tblPage.ColumnNames.AppPageTitle];


            if ((PageContentControl != null))
            {
                PageContentControl.InnerHtml = arDataRow[0][tblPage.ColumnNames.AppPageContent].ToString().Replace("<pre ", "<p ").Replace("</pre>", "</p>");
                PageContentControl.InnerHtml = PageContentControl.InnerHtml.Replace("~GetServerURL()~", PageBase.GetServerURL() + "/");
            }

            if ((PageHeadingControl != null))
            {
                PageHeadingControl.InnerText = arDataRow[0][tblPage.ColumnNames.AppPageHeading].ToString();
            }

            metaDescription = arDataRow[0][tblPage.ColumnNames.AppSEODescription].ToString();
            metaKeywords = arDataRow[0][tblPage.ColumnNames.AppSEOWord].ToString();

            ViewState["CurrentPageMetaDesc"] = arDataRow[0][tblPage.ColumnNames.AppSEODescription];
            ViewState["CurrentPageMetaKeyWords"] = arDataRow[0][tblPage.ColumnNames.AppSEOWord];

            if ((objSiteMapControl != null))
            {
                tblMenuItem objMenuItem = new tblMenuItem();
                DataTable dtSiteMap = objMenuItem.GetSiteMapDT((int)arDataRow[0][tblMenuItem.ColumnNames.AppMenuItemId]);
                objSiteMapControl.InnerHtml = "<div class=\"itemnode\"><a href='" + strServerURL + "'>Home</a></div>";
                for (int i = 0; i <= dtSiteMap.Rows.Count - 2; i++)
                {
                    objSiteMapControl.InnerHtml += "<div class=\"itemseparator\"> </div>";
                    if (!string.IsNullOrEmpty(dtSiteMap.Rows[i]["appAlias"].ToString()))
                    {
                        objSiteMapControl.InnerHtml += "<div class='itemnode'><a href='" + strServerURL + dtSiteMap.Rows[i]["appAlias"] + "'>" + dtSiteMap.Rows[i]["appMenuItem"] + "</a></div>";
                    }
                    else
                    {
                        objSiteMapControl.InnerHtml += "<div class='itemnode'>" + dtSiteMap.Rows[i]["appMenuItem"] + "</div>";
                    }

                }

                objSiteMapControl.InnerHtml += "<div class=\"itemseparator\"> </div>";
                objSiteMapControl.InnerHtml += "<div class='currentitem itemnode'>" + dtSiteMap.Rows[dtSiteMap.Rows.Count - 1]["appMenuItem"] + "</div>";
            }




        }
    }

    public string GetPageAliasFromURL()
    {
        string strRawUrl = Context.Request.RawUrl;
        string pageName = strRawUrl;
        if (Convert.ToBoolean(ConfigurationManager.AppSettings["IsOnServer"].ToString()) == true)
        {
            //'For Online
            pageName = pageName.TrimStart('/');
        }
        else
        {
            //'For Localhost
            for (int i = 0; i <= 1; i++)
            {
                pageName = pageName.Substring(pageName.IndexOf("/") + 1);
            }

        }
        //If Microsoft.VisualBasic.Split(pageName, "/").Length > 1 Then
        //    pageName = pageName.Split("/")(0) & "/{*name}"
        //End If
        return pageName;
    }

    public string GetPageQueryStringFromURL()
    {
        string strRawUrl = Context.Request.RawUrl;
        string strQueryString = strRawUrl;

        if (strQueryString.Split('?').Length > 1)
        {
            strQueryString = strQueryString.Split('?')[1];
        }
        else
        {
            strQueryString = "";
        }
        return strQueryString;
    }

    public Boolean IsLogin()
    {
        if ((Session[appFunctions.Session.ClientUserID.ToString()] != null))
        {
            if (string.IsNullOrEmpty(Session[appFunctions.Session.ClientUserName.ToString()].ToString()) | Session[appFunctions.Session.ClientUserID.ToString()].ToString() == "0")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
    }

    public void CheckSession(string strQueryString="")
    {

        if ((Session[appFunctions.Session.ClientUserID.ToString()] != null))
        {
            if (string.IsNullOrEmpty(Session[appFunctions.Session.ClientUserName.ToString()].ToString()) | Session[appFunctions.Session.ClientUserID.ToString()].ToString() == "0")
            {
                string strPage = GetAlias("Login.aspx");
                if(strQueryString!="")
                {
                    strPage += strQueryString;
                }
                Response.Redirect(strPage);
            }
        }
        else
        {
            string strPage = GetAlias("Login.aspx");
            if (strQueryString != "")
            {
                strPage +=  strQueryString;
            }
            Response.Redirect(strPage);
        }
    }
    public void ExitsSession()
    {
        if ((Session[appFunctions.Session.ClientUserID.ToString()] != null))
        {
            if (!(string.IsNullOrEmpty(Session[appFunctions.Session.ClientUserName.ToString()].ToString()) | Session[appFunctions.Session.ClientUserID.ToString()].ToString() == "0"))
            {
                Response.Redirect(GetAlias("Default.aspx"));
            }
        }

    }

    public void SetCurrency()
    {
        bool IsDefault = true;
        if ((Session[appFunctions.Session.CurrencyID.ToString()] != null))
        {
            if (Session[appFunctions.Session.CurrencyID.ToString()].ToString() != "")
            {
                IsDefault = false;
            }
        }
        tblCurrency objCurrency = new tblCurrency();
        if (IsDefault)
        {
            objCurrency.Where.AppIsDefault.Value = true;
            objCurrency.Query.Load();
        }
        else
        {
            objCurrency.LoadByPrimaryKey(Convert.ToInt32(Session[appFunctions.Session.CurrencyID.ToString()].ToString()));
        }
        if (objCurrency.RowCount > 0)
        {

            Session[appFunctions.Session.CurrencyID.ToString()] = objCurrency.s_AppCurrencyID;
            Session[appFunctions.Session.CurrencyImage.ToString()] = objCurrency.s_AppSymbol;
            Session[appFunctions.Session.CurrencyInRupee.ToString()] = objCurrency.s_AppRate;
        }
        objCurrency = null;
    }

    public PageBase_Client()
    {
        Load += Page_Load;
    }
}
