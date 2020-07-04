using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;

public partial class MyAccount : PageBase_Client
{
    public PageBase objPageBase = new PageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack) {
            CheckSession();
            SetUpPageContent(ref metaDescription, ref metaKeywords);
        }
    }
}