using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer;
using System.Text.RegularExpressions;
public partial class Inquiry : ControlBase
{
    public PageBase objPageBase = new PageBase();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SetRegulerExpression();

        }
    }
    public void SetRegulerExpression()
    {
        REVEmail.ValidationExpression = objPageBase.RXEmailRegularExpression;
        REVEmail.ErrorMessage = "Invalid Email (" + objPageBase.RXEmailRegularExpressionMsg + ")";

        REVMobile.ValidationExpression = objPageBase.RXPhoneRegularExpression ;
        REVMobile.ErrorMessage = "Invalid Mobile (" + objPageBase.RXPhoneRegularExpressionMsg + ")";

    }
    public void Show(string strProductDetailId,string strURL="")
    {
        hdnProductDetailId.Value = strProductDetailId;
        if (strURL != "")
        {
            hdnURL.Value = strURL;

        }
        mpeInquiry.Show();
    }
    protected void btnInquiry_Click(object sender, EventArgs e)
    {
        if (Save())
        {
             txtName.Text="";
             txtEmail.Text = "";
             txtMobile.Text = "";
             txtMessage.Text = "";
             mpeInquiry.Hide();
             Session[appFunctions.Session.DInfoInquiry.ToString()] = "Your Inquiry has been sent successfully.";
             if (hdnURL.Value != "")
             {
                 Response.Redirect(Request.RawUrl);
             }
             
        }
    }

    public bool Save()
    {

        tblInquiry objInquiry = new tblInquiry();
        objInquiry.AddNew();
        objInquiry.s_AppName = txtName.Text;
        objInquiry.s_AppEmail = txtEmail.Text;
        objInquiry.s_AppMobile = txtMobile.Text;
        objInquiry.s_AppMessage = txtMessage.Text;
         objInquiry.s_AppProductDetailID=hdnProductDetailId.Value;
         objInquiry.Save();
        objInquiry = null;

        clsCommon objCommon = new clsCommon();
        string StrBody = "";
        string strSubject = "Inquiry";
        StrBody = objCommon.readFile(Server.MapPath("~/EmailTemplates/Inquiry.html"));
        StrBody = StrBody.Replace("`name`", txtName.Text);
        StrBody = StrBody.Replace("`mobileno`", txtMobile.Text);
        StrBody = StrBody.Replace("`email`", txtEmail.Text);
        StrBody = StrBody.Replace("`message`", txtMessage.Text);
        objCommon.SendMail("", strSubject, StrBody);
       
        objCommon = null;
        return true;
    }


}