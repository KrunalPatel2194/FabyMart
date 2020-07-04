using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;

public partial class Approved : PageBase_Client
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SetUpPageContent(ref metaDescription, ref metaKeywords);
            objCommon = new clsCommon();
            string strIds = objCommon.GetParameterFromUrl(Enums.Enums_URLParameterType.ByNumber, "1");

            if (strIds != "")
            {
                Boolean approved = false;
                try
                {
                    string strId = objEncrypt.Decrypt(strIds, appFunctions.strKey);
                    tblCustomer objCustomer = new tblCustomer();
                    if (objCustomer.LoadByPrimaryKey(Convert.ToInt32(strId)))
                    {
                        if (!(objCustomer.AppIsVerified))
                        {
                            objCustomer.AppIsVerified = true;
                            objCustomer.Save();
                            approved = true;
                        }
                    }
                    objCustomer = null;
                    Response.Redirect(GetAlias("Login.aspx") + "?From=" + objEncrypt.Encrypt("ApprovedPage" + approved.ToString(), appFunctions.strKey), true);
                }
                catch (Exception ex)
                {
                }
            }
            objCommon = null;
        }
    }
}