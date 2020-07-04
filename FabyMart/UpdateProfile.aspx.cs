using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
public partial class UpdateProfile : PageBase_Client
{
    tblCustomer objCustomer;
    public PageBase objPageBase = new PageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SetRegulerExpression();
            CheckSession();
            SetUpPageContent(ref metaDescription, ref metaKeywords);

            objCommon = new clsCommon();
            //objCommon.FillDropDownList(ddlCountry, "tblCountry", tblCountry.ColumnNames.AppCountry, tblCountry.ColumnNames.AppCountryID, "-- Select Country --", tblCountry.ColumnNames.AppCountry, appFunctions.Enum_SortOrderBy.Asc);
            //ddlCountry.Items.Add(new ListItem("Other", "-1"));
            //ddlState.Items.Clear();
            //ddlState.Items.Add(new ListItem("-- Select State --", "0"));
            //ddlCity.Items.Clear();
            //ddlCity.Items.Add(new ListItem("-- Select City --", "0"));

            objCommon = null;
            CustomerInfo();
        }

    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon = new clsCommon();
        //objCommon.FillDropDownList(ddlState, "tblState", tblState.ColumnNames.AppState, tblState.ColumnNames.AppStateID, "-- Select State --", tblState.ColumnNames.AppState, appFunctions.Enum_SortOrderBy.Asc, tblState.ColumnNames.AppCountryID + "=" + ddlCountry.SelectedValue);
        //ddlState.Items.Add(new ListItem("Other", "-1"));
        //ddlCity.Items.Clear();
        //ddlCity.Items.Add(new ListItem("-- Select City --", "0"));
        //objCommon = null;
        //if (ddlCountry.SelectedValue == "-1")
        //{
        //    txtcountry.Visible = true;
        //    RFVtxtCoutry.Enabled = true;
        //}
        //else
        //{
        //    txtcountry.Visible = false;
        //    RFVtxtCoutry.Enabled = false;

        //}
        //txtState.Visible = false;
        //RFVtxtState.Enabled = false;
        //txtState.Text = "";
        //txtCity.Visible = false;
        //RFVtxtCity.Enabled = false;
        //txtCity.Text = "";
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon = new clsCommon();
        //objCommon.FillDropDownList(ddlCity, "tblCity", tblCity.ColumnNames.AppCity, tblCity.ColumnNames.AppCityID, "-- Select City --", tblCity.ColumnNames.AppCity, appFunctions.Enum_SortOrderBy.Asc, tblCity.ColumnNames.AppStateID + "=" + ddlState.SelectedValue);
        //ddlCity.Items.Add(new ListItem("Other", "-1"));
        //objCommon = null;
        //if (ddlState.SelectedValue == "-1")
        //{
        //    txtState.Visible = true;
        //    RFVtxtState.Enabled = true;
        //}
        //else
        //{
        //    txtState.Visible = false;
        //    RFVtxtState.Enabled = false;
        //}
        //txtCity.Visible = false;
        //RFVtxtCity.Enabled = false;
        //txtCity.Text = "";
    }

    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlCity.SelectedValue == "-1")
        //{
        //    txtCity.Visible = true;
        //    RFVtxtCity.Enabled = true;
        //}
        //else
        //{
        //    txtCity.Visible = false;
        //    RFVtxtCity.Enabled = false;
        //}
    }

    private void SetRegulerExpression()
    {
        revEmail.ValidationExpression = RXEmailRegularExpression;
        revEmail.ErrorMessage = "Invalid Email (" + RXEmailRegularExpressionMsg + ")";
        revMobile.ValidationExpression = RXPhoneRegularExpression;
        revMobile.ErrorMessage = "Invalid Mobile Number (" + RXPhoneRegularExpressionMsg + ")";
        REVFirstName.ValidationExpression = RXAlphaRegularExpression;
        REVFirstName.ErrorMessage = "Invalid First Name ( Alphabates Only)";
        REVLastName.ValidationExpression = RXAlphaRegularExpression;
        REVLastName.ErrorMessage = "Invalid Last Name ( Alphabates Only)";
        revPhone.ValidationExpression = RXPhoneRegularExpression;
        revPhone.ErrorMessage = "Invalid Phone Number (" + RXPhoneRegularExpressionMsg + ")";
        //REVPincode.ValidationExpression = RXPinRegularExpression;
        //REVPincode.ErrorMessage = "Invalid Pincode (" + RXPinRegularExpressionMsg + ")";
        ////REVtxtCoutry.ValidationExpression = RXAlphaRegularExpression;
        //REVtxtCoutry.ErrorMessage = "Invalid Country (" + RXAlphaRegularExpressionMsg + ")";
        //REVtxtState.ValidationExpression = RXAlphaRegularExpression;
        //REVtxtState.ErrorMessage = "Invalid State (" + RXAlphaRegularExpressionMsg + ")";
        //REVtxtCity.ValidationExpression = RXAlphaRegularExpression;
        //REVtxtCity.ErrorMessage = "Invalid City (" + RXAlphaRegularExpressionMsg + ")";
    }

    private void CustomerInfo()
    {
        objCustomer = new tblCustomer();
        if (objCustomer.LoadByPrimaryKey(Convert.ToInt32(Session[appFunctions.Session.ClientUserID.ToString()].ToString())))
        {
            txtFirstName.Text = objCustomer.AppFirstName;
            txtLastName.Text = objCustomer.AppLastName;
            txtEmail.Text = objCustomer.AppEmailID;
            txtEmail.Enabled = false;
            txtMobile.Text = objCustomer.AppMobile;
            txtPhone.Text = objCustomer.AppPhone;
            if (objCustomer.s_AppCountryId != "")
            {
                objCommon = new clsCommon();
                //if (ddlCountry.Items.FindByValue(objCustomer.s_AppCountryId) != null)
                //{
                //    ddlCountry.SelectedValue = objCustomer.s_AppCountryId;
                //    objCommon.FillDropDownList(ddlState, "tblState", tblState.ColumnNames.AppState, tblState.ColumnNames.AppStateID, "-- Select State --", tblState.ColumnNames.AppState, appFunctions.Enum_SortOrderBy.Asc, tblState.ColumnNames.AppCountryID + "=" + ddlCountry.SelectedValue);
                //    ddlState.Items.Add(new ListItem("Other", "-1"));
                //    if (objCustomer.s_AppStateId != "")
                //    {
                //        if (ddlState.Items.FindByValue(objCustomer.s_AppStateId) != null)
                //        {
                //            ddlState.SelectedValue = objCustomer.s_AppStateId;
                //            objCommon.FillDropDownList(ddlCity, "tblCity", tblCity.ColumnNames.AppCity, tblCity.ColumnNames.AppCityID, "-- Select City --", tblCity.ColumnNames.AppCity, appFunctions.Enum_SortOrderBy.Asc, tblCity.ColumnNames.AppStateID + "=" + ddlState.SelectedValue);
                //            ddlCity.Items.Add(new ListItem("Other", "-1"));
                //            if (objCustomer.s_AppCityId != "")
                //            {
                //                if (ddlCity.Items.FindByValue(objCustomer.s_AppCityId) != null)
                //                {
                //                    ddlCity.SelectedValue = objCustomer.s_AppCityId;
                //                }
                //            }
                //        }
                //    }
                //}
                //txtcountry.Visible = false;
                //txtState.Visible = false;
                //txtCity .Visible = false;
                objCommon = null;
            }

            if (objCustomer.s_AppGender != "")
            {
                if (objCustomer.AppGender)
                {
                    RbtnMale.Checked = true;
                }
                else
                {
                    rbtnFeMale.Checked = true;
                }
            }
            else
            {
                RbtnMale.Checked = true;
            }
            if (objCustomer.AppImage != "")
            {
                img.ImageUrl = objCustomer.AppImage;
            }
            img.ImageUrl = objCustomer.AppImage;
          //  txtAddress.Text = objCustomer.AppAddress;
            //if (objCustomer.s_AppPincode != "")
            //{
            //    txtPincode.Text = objCustomer.s_AppPincode;
            //}
        }
        objCustomer = null;
    }

    public bool SaveCustomer()
    {
        objCommon = new clsCommon();
        if (objCommon.IsRecordExists("tblCustomer", tblCustomer.ColumnNames.AppEmailID, tblCustomer.ColumnNames.AppCustomerID, txtEmail.Text, Session[appFunctions.Session.ClientUserID.ToString()].ToString()))
        {
            DInfo.ShowMessage("Email address already exist.", Enums.MessageType.Error);
            return false;
        }

        tblCustomer objCustomer = new tblCustomer();
        if (objCustomer.LoadByPrimaryKey(Convert.ToInt32(Session[appFunctions.Session.ClientUserID.ToString()].ToString())))
        {
            objCustomer.AppFirstName = txtFirstName.Text.Trim();
            objCustomer.AppLastName = txtLastName.Text.Trim();
            objCustomer.AppEmailID = txtEmail.Text.Trim();
            objCustomer.AppMobile = txtMobile.Text.Trim();
            objCustomer.AppPhone = txtPhone.Text;
            //objCustomer.AppAddress = txtAddress.Text;
            //objCustomer.s_AppPincode = txtPincode.Text;

            if (RbtnMale.Checked)
            {
                objCustomer.AppGender = true;
            }
            else
            {
                objCustomer.AppGender = false;
            }
            if (FileUploadImg.HasFile)
            {
                string strError = "";
                string Time = Convert.ToString(DateTime.Now.Month) + Convert.ToString(DateTime.Now.Day) + Convert.ToString(DateTime.Now.Year) + Convert.ToString(DateTime.Now.Hour) + Convert.ToString(DateTime.Now.Minute) + Convert.ToString(DateTime.Now.Second);
                string strPath = objCommon.FileUpload_Images(FileUploadImg.PostedFile, txtFirstName.Text.Trim().Replace(" ", "_") + "_" + txtLastName.Text.Trim().Replace(" ", "_") + "_" + Time, "Uploads/Customer/", ref strError, 0, objCustomer.s_AppImage, true);
                if (strError == "")
                {
                    objCustomer.AppImage = strPath;
                }
                else
                {
                    DInfo.ShowMessage(strError, Enums.MessageType.Error);
                    return false;
                }

            }

            //string strCountryId = ddlCountry.SelectedValue;
            //string strStateId = ddlState.SelectedValue;
            //string strCityID = ddlCity.SelectedValue;
            //if (ddlCountry.SelectedValue == "-1")
            //{
            //    tblCountry objCountry = new tblCountry();
            //    objCountry.Where.AppCountry.Value = txtcountry.Text;
            //    objCountry.Query.Load();
            //    if (!(objCountry.RowCount > 0))
            //    {
            //        objCountry.AddNew();
            //        objCountry.AppCountry = txtcountry.Text;
            //        objCountry.Save();
            //    }
            //    strCountryId = objCountry.s_AppCountryID;
            //    objCountry = null;
            //}
            //if (ddlState.SelectedValue == "-1")
            //{
            //    tblState objState = new tblState();
            //    objState.Where.AppCountryID.Value = strCountryId;
            //    objState.Where.AppState.Value = txtState.Text;
            //    objState.Query.Load();
            //    if (!(objState.RowCount > 0))
            //    {
            //        objState.AddNew();
            //        objState.AppState = txtState.Text;
            //        objState.s_AppCountryID = strCountryId;
            //        objState.Save();
            //    }
            //    strStateId = objState.s_AppStateID;
            //    objState = null;
            //}
            //if (ddlCity.SelectedValue == "-1")
            //{
            //    tblCity objCity = new tblCity();
            //    objCity.Where.AppStateID.Value = strStateId;
            //    objCity.Where.AppCity.Value = txtCity.Text;
            //    objCity.Query.Load();
            //    if (!(objCity.RowCount > 0))
            //    {
            //        objCity.AddNew();
            //        objCity.AppCity = txtCity.Text;
            //        objCity.s_AppStateID = strStateId;
            //        objCity.Save();
            //    }
            //    strCityID = objCity.s_AppCityID;
            //    objCity = null;
            //}
            //            objCustomer.s_AppCityId = strCityID;
            //objCustomer.s_AppStateId = strStateId;
            //objCustomer.s_AppCountryId = strCountryId;
                        objCustomer.Save();

        }
        objCustomer = null;
        objCommon = null;
        return true;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveCustomer())
        {
            DInfo.ShowMessage("Profile Successfully Updated.", Enums.MessageType.Successfull);
            CustomerInfo();
        }
    }
}