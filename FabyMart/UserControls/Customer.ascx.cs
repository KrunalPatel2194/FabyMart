using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Customer : System.Web.UI.UserControl
{
    PageBase objPageBase;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            objPageBase = new PageBase();
            lnkBtnHome.HRef = objPageBase.GetAlias("MyAccount.aspx");
            lnkbtnMyOrderList.HRef = objPageBase.GetAlias("MyOrderList.aspx");
            lnkBtnUpdateProfile.HRef = objPageBase.GetAlias("UpdateProfile.aspx");
            lnkbtnMyFavouriteProduct.HRef = objPageBase.GetAlias("MyFavouriteProduct.aspx");
            lnkBtnChangePwd.HRef = objPageBase.GetAlias("ChangePassword.aspx");
            lnkTrackOrder.HRef = objPageBase.GetAlias("TrackOrder.aspx");
        }
    }
    protected void lnkBtnLogOut_Click(object sender, EventArgs e)
    {

        Session.Abandon();
        objPageBase = new PageBase();
        Response.Redirect(objPageBase.GetAlias("Default.aspx"));
    }
}