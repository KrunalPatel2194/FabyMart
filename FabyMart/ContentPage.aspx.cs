using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;

public partial class ContentPage : PageBase_Client
{
    public PageBase objPageBase = new PageBase();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SetUpPageContent(ref divDescription, ref divTitle, ref metaDescription, ref metaKeywords);
        }
    }
}