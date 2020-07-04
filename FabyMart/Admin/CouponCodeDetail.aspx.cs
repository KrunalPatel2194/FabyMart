using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.IO;
using System.Drawing;


public partial class CouponCodeDetail : PageBase_Admin
{
    tblCouponCode objCouponCode;
    tblCouponCodeProduct objCouponCodeProduct;
    int intPkId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            lnkSaveAndAddnew.Visible = HasAdd;
            ViewState["SortOrder"] = appFunctions.Enum_SortOrderBy.Asc;
            ViewState["SortColumn"] = "";
            SetRegulerExpression();
            objCommon = new clsCommon();
            objCommon.BindEnumtoDDL(typeof(Enums.Enum_CouponCodeType), ddlType, "-- Select Type --");
            objCouponCode = null;
            if ((Request.QueryString.Get("ID") != null))
            {
                objEncrypt = new clsEncryption();
                try
                {
                    hdnPKID.Value = objEncrypt.Decrypt(Request.QueryString.Get("ID"), appFunctions.strKey);
                }
                catch (Exception ex)
                {
                    // noIdFoundRedirect("Employee.aspx");
                }
                objEncrypt = null;
                SetValuesToControls();

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "HideTab()", true);
            }
        }
    }

    public void SetRegulerExpression()
    {
        REVStartDate.ValidationExpression = RXDateRegularExpression;
        REVStartDate.ErrorMessage = "Invalid  From Date [" + RXDateRegularExpressionMsg + "]";
        REVEndDate.ValidationExpression = RXDateRegularExpression;
        REVEndDate.ErrorMessage = "Invalid  To Date [" + RXDateRegularExpressionMsg + "]";
        REVDiscount.ValidationExpression = RXDecimalRegularExpression;
        REVDiscount.ErrorMessage = "Invalid  To Date [" + RXDecimalRegularExpressionMsg + "]";
    }

    private bool SaveData()
    {
        objCommon = new clsCommon();
        if (objCommon.IsRecordExists("tblCouponCode", tblCouponCode.ColumnNames.AppCouponCode, tblCouponCode.ColumnNames.AppCouponCodeID, txtCouponCode.Text, hdnPKID.Value))
        {
            DInfo.ShowMessage("Coupon Code alredy exits.", Enums.MessageType.Error);
            return false;
        }

        objCouponCode = new tblCouponCode();
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objCouponCode.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value));
        }
        else
        {
            objCouponCode.AddNew();
        }
        objCouponCode.AppCouponCode = txtCouponCode.Text;
        objCouponCode.s_AppDiscountPer = txtDiscountPer.Text;
        objCouponCode.s_AppStartDate = txtStartDate.Text;
        objCouponCode.s_AppEndDate = txtEndDate.Text;
        objCouponCode.AppIsActive = chkIsActive.Checked;
        objCouponCode.s_AppType = ddlType.SelectedValue;
        objCouponCode.Save();
        intPkId = objCouponCode.AppCouponCodeID;
        objCouponCode = null;
        objCommon = null;
        return true;
    }

    private void SetValuesToControls()
    {
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objCouponCode = new tblCouponCode();
            if (objCouponCode.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value)))
            {
                ddlType.SelectedValue = objCouponCode.s_AppType;
                hdnType.Value = objCouponCode.s_AppType;
                txtCouponCode.Text = objCouponCode.s_AppCouponCode;
                txtDiscountPer.Text = objCouponCode.s_AppDiscountPer;
                txtStartDate.Text = objCouponCode.AppStartDate.ToString("dd-MM-yyyy");
                txtEndDate.Text = objCouponCode.AppEndDate.ToString("dd-MM-yyyy");
                chkIsActive.Checked = objCouponCode.AppIsActive;
                if (objCouponCode.AppType == Convert.ToInt32(Enums.Enum_CouponCodeType.General))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "HideTab()", true);
                }
                LoadAllData();
            }
            objCouponCode = null;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("CouponCodeList.aspx", true);
    }

    private void ResetControls()
    {
        ddlType.SelectedIndex = 0;
        txtCouponCode.Text = "";
        txtDiscountPer.Text = "";
        txtStartDate.Text = "";
        txtEndDate.Text = "";
        chkIsActive.Checked = true;
        hdnPKID.Value = "";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "HideTab()", true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("Coupon Code has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("Coupon Code has been updated successfully", Enums.MessageType.Successfull);
            }
            hdnPKID.Value = intPkId.ToString();
            SetValuesToControls();
            // LoadAllData();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "HideTab()", true);

        }
    }

    protected void lnkSaveAndClose_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "Coupon Code has been added successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            else
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "Coupon Code has been updated successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            Response.Redirect("CouponCodeList.aspx");
        }
    }

    protected void lnkSaveAndAddnew_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("Coupon Code has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("Coupon Code has been updated successfully", Enums.MessageType.Successfull);
            }
            ResetControls();
        }
    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (hdnPKID.Value != "")
        {
            objCouponCodeProduct = new tblCouponCodeProduct();
            objCouponCodeProduct.Where.AppCouponCodeID.Value = hdnPKID.Value;
            objCouponCodeProduct.Query.Load();
            if (objCouponCodeProduct.RowCount > 0)
            {
                objCouponCodeProduct.DeleteAll();
                objCouponCodeProduct.Save();
            }
            objCouponCodeProduct = null;
        }
    }


    //Load All Data
    public void LoadAllData()
    {
        ResetAllGrid();
        objCommon = new clsCommon();
        objCommon.FillDropDownList(ddlSerachCategory, "tblCategory ", tblCategory.ColumnNames.AppCategory, tblCategory.ColumnNames.AppCategoryID, "--Select Category--", tblCategory.ColumnNames.AppCategory, appFunctions.Enum_SortOrderBy.Asc);
        ddlSerachSubCategory.Items.Add(new ListItem("--Select Sub Category--", "0"));
        divSearch.Visible = true;
        if (Convert.ToInt32(hdnType.Value) == Convert.ToInt32(Enums.Enum_CouponCodeType.SubCategory))
        {
            divSubCateogry.Visible = true;
            divCategory.Visible = false;
            divProduct.Visible = false;
            ddlSerachSubCategory.Visible = true;
            objCommon.FillDropDownList(ddlCategory, "tblCategory ", tblCategory.ColumnNames.AppCategory, tblCategory.ColumnNames.AppCategoryID, "--Select Category--", tblCategory.ColumnNames.AppCategory, appFunctions.Enum_SortOrderBy.Asc);
            LoadSubCategoryData(true, false);
        }
        else if (Convert.ToInt32(hdnType.Value) == Convert.ToInt32(Enums.Enum_CouponCodeType.Category))
        {
            divSubCateogry.Visible = false;
            divCategory.Visible = true;
            divProduct.Visible = false;
            ddlSerachSubCategory.Visible = false;
            LoadUnselectedCategories();
            LoadCategoryData(true, false);
        }
        else if (Convert.ToInt32(hdnType.Value) == Convert.ToInt32(Enums.Enum_CouponCodeType.Product))
        {
            divSubCateogry.Visible = false;
            divCategory.Visible = false;
            divProduct.Visible = true;
            ddlSerachSubCategory.Visible = true;
            divSearch.Visible = false;
            objCommon.FillDropDownList(ddlProductCategory, "tblCategory ", tblCategory.ColumnNames.AppCategory, tblCategory.ColumnNames.AppCategoryID, "--Select Category--", tblCategory.ColumnNames.AppCategory, appFunctions.Enum_SortOrderBy.Asc);
            ddlSubCate.Items.Add(new ListItem("--Select Sub Category--", "0"));
            LoadUnSelectedProduct(true, false);
            LoadProductDataGrid(true, false);
        }
        objCommon = null;
    }

    public void ResetAllGrid()
    {
        dgvUnSelectedCategories.DataSource = null;
        dgvUnSelectedCategories.DataBind();

        dgvUnSelectedCategory.DataSource = null;
        dgvUnSelectedCategory.DataBind();

        dgvProduct.DataSource = null;
        dgvProduct.DataBind();

        dgvSubCategory.DataSource = null;
        dgvSubCategory.DataBind();

        dgvCategory.DataSource = null;
        dgvCategory.DataBind();

        dgvGridViewProduct.DataSource = null;
        dgvGridViewProduct.DataBind();
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadUnselectedSubCategories();
    }

    public void LoadUnselectedSubCategories()
    {
        if (ddlCategory.SelectedIndex > 0)
        {
            objCouponCodeProduct = new tblCouponCodeProduct();
            objDataTable = objCouponCodeProduct.LoadUnSelectedCategories(Convert.ToInt32(hdnType.Value), hdnPKID.Value, ddlCategory.SelectedValue, "");
            dgvUnSelectedCategories.DataSource = null;
            dgvUnSelectedCategories.DataBind();
            if (objDataTable.Rows.Count <= 0)
            {
                DInfoSubCategoryGrid.ShowMessage("No data found", Enums.MessageType.Information);
                return;
            }
            else
            {
                dgvUnSelectedCategories.AllowPaging = false;
                dgvUnSelectedCategories.DataSource = objDataTable;
                dgvUnSelectedCategories.DataBind();
            }
            objCouponCodeProduct = null;
        }
        else
        {
            dgvUnSelectedCategories.DataSource = null;
            dgvUnSelectedCategories.DataBind();
        }
    }

    protected void dgvUnSelectedCategories_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
        {
            if (e.CommandName == "IsAdd")
            {
                objCouponCodeProduct = new tblCouponCodeProduct();
                objCouponCodeProduct.AddNew();
                objCouponCodeProduct.s_AppCouponCodeID = hdnPKID.Value;
                objCouponCodeProduct.s_AppReferenceID = e.CommandArgument.ToString();
                objCouponCodeProduct.Save();
                objCouponCodeProduct = null;
                LoadUnselectedSubCategories();
                LoadSubCategoryData(false, false);
            }
        }
    }

    public void LoadSubCategoryData(bool IsResetPageIndex, bool IsSort)
    {
        objCouponCodeProduct = new tblCouponCodeProduct();
        objDataTable = objCouponCodeProduct.LoadGridData(Convert.ToInt32(hdnType.Value), hdnPKID.Value, ddlSerachCategory.SelectedValue, ddlSerachSubCategory.SelectedValue);
        if (IsResetPageIndex)
        {
            if (dgvSubCategory.PageCount > 0)
            {
                dgvSubCategory.PageIndex = 0;
            }
        }
        dgvSubCategory.DataSource = null;
        dgvSubCategory.DataBind();
        hdnSelectedIDs.Value = "";
        if (objDataTable.Rows.Count <= 0)
        {
            DinfoBranchGrid.ShowMessage("No data found", Enums.MessageType.Information);
            return;
        }
        else
        {
            dgvSubCategory.AllowPaging = false;
            objDataTable = SortDatatable(objDataTable, ViewState["SortColumn"].ToString(), (appFunctions.Enum_SortOrderBy)ViewState["SortOrder"], IsSort);
            dgvSubCategory.DataSource = objDataTable;
            dgvSubCategory.DataBind();
        }

        objCouponCodeProduct = null;
    }

    protected void dgvSubCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dgvSubCategory.PageIndex = e.NewPageIndex;
        LoadSubCategoryData(false, false);
    }

    protected void dgvSubCategory_RowCreated(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                string strID = dgvSubCategory.DataKeys[e.Row.RowIndex].Values[0].ToString();
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSelectRow");
                chk.ID = "chkSelectRow_" + strID;
                break;
        }
    }

    protected void dgvSubCategory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataControlRowType itemType = e.Row.RowType;
        switch (itemType)
        {
            case DataControlRowType.DataRow:
                string strID = dgvSubCategory.DataKeys[e.Row.RowIndex].Values[0].ToString();
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSelectRow");
                if ((chk != null))
                {
                    chk.Attributes.Add("OnClick", "javascript:SelectRow(this," + strID + ")");
                }
                break;
        }
    }

    protected void dgvSubCategory_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnSelectedIDs.Value = "";
        ViewState["SortColumn"] = e.SortExpression;
        LoadSubCategoryData(false, true);
    }

    protected void dgvSubCategory_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void dgvSubCategory_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    /*-- For Delete --*/

    protected void btnSubCategoryDelete_Click(object sender, EventArgs e)
    {
        string[] arIDs = hdnSelectedIDs.Value.ToString().TrimEnd(',').Split(',');
        bool IsDelete = false;
        for (int i = 0; i <= arIDs.Length - 1; i++)
        {
            if (!string.IsNullOrEmpty(arIDs.GetValue(i).ToString()))
            {
                if (DeleteSubCategory(Convert.ToInt32(arIDs.GetValue(i))))
                {
                    IsDelete = true;
                }
            }
        }

        if (IsDelete)
        {
            CallRelatedGriad();
        }
        //DInfoSubCategory.ShowMessage("Property Sub Category has been deleted successfully", Enums.MessageType.Successfull);
        hdnSelectedIDs.Value = "";
    }

    private bool DeleteSubCategory(int intPKID)
    {
        bool retval = false;
        objCouponCodeProduct = new tblCouponCodeProduct();
        if (objCouponCodeProduct.LoadByPrimaryKey(intPKID))
        {
            objCouponCodeProduct.MarkAsDeleted();
            objCouponCodeProduct.Save();
            retval = true;

        }
        objCouponCodeProduct = null;
        return retval;
    }

    /*-- Searching --*/

    protected void btnSubCategoryReset_Click(object sender, System.EventArgs e)
    {
        ddlSerachCategory.SelectedIndex = 0;
        ddlSerachSubCategory.Items.Clear();
        ddlSerachSubCategory.Items.Add(new ListItem("--Select Sub Category--", "0"));
        CallRelatedGriad();
    }

    protected void ddlSerachCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon = new clsCommon();
        objCommon.FillDropDownList(ddlSerachSubCategory, "tblSubCategory ", tblSubCategory.ColumnNames.AppSubCategory, tblSubCategory.ColumnNames.AppSubCategoryID, "--Select Sub Category--", tblSubCategory.ColumnNames.AppSubCategory, appFunctions.Enum_SortOrderBy.Asc, tblSubCategory.ColumnNames.AppCategoryID + "=" + ddlSerachCategory.SelectedValue);
        objCommon = null;
        CallRelatedGriad();
    }

    protected void ddlSerachSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        CallRelatedGriad();
    }

    public void CallRelatedGriad()
    {
        if (Convert.ToInt32(hdnType.Value) == Convert.ToInt32(Enums.Enum_CouponCodeType.SubCategory))
        {
            LoadUnselectedSubCategories();
            LoadSubCategoryData(true, false);
        }
        else if (Convert.ToInt32(hdnType.Value) == Convert.ToInt32(Enums.Enum_CouponCodeType.Category))
        {
            LoadUnselectedCategories();
            LoadCategoryData(true, false);
        }
        else if (Convert.ToInt32(hdnType.Value) == Convert.ToInt32(Enums.Enum_CouponCodeType.Product))
        {
            LoadUnSelectedProduct(true, false);
            LoadProductDataGrid(true, false);
        }
    }

    /*--- For Category --*/
    public void LoadUnselectedCategories()
    {
        objCouponCodeProduct = new tblCouponCodeProduct();
        objDataTable = objCouponCodeProduct.LoadUnSelectedCategories(Convert.ToInt32(hdnType.Value), hdnPKID.Value);
        dgvUnSelectedCategory.DataSource = null;
        dgvUnSelectedCategory.DataBind();
        if (objDataTable.Rows.Count <= 0)
        {
            DInfoSubCategoryGrid.ShowMessage("No data found", Enums.MessageType.Information);
            return;
        }
        else
        {
            dgvUnSelectedCategory.AllowPaging = false;
            dgvUnSelectedCategory.DataSource = objDataTable;
            dgvUnSelectedCategory.DataBind();
        }
        objCouponCodeProduct = null;
    }

    protected void dgvUnSelectedCategory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
        {
            if (e.CommandName == "IsAdd")
            {
                objCouponCodeProduct = new tblCouponCodeProduct();
                objCouponCodeProduct.AddNew();
                objCouponCodeProduct.s_AppCouponCodeID = hdnPKID.Value;
                objCouponCodeProduct.s_AppReferenceID = e.CommandArgument.ToString();
                objCouponCodeProduct.Save();
                objCouponCodeProduct = null;
                LoadUnselectedCategories();
                LoadCategoryData(true, false);
            }
        }
    }

    public void LoadCategoryData(bool IsResetPageIndex, bool IsSort)
    {
        objCouponCodeProduct = new tblCouponCodeProduct();
        objDataTable = objCouponCodeProduct.LoadGridData(Convert.ToInt32(hdnType.Value), hdnPKID.Value, ddlSerachCategory.SelectedValue, ddlSerachSubCategory.SelectedValue);
        if (IsResetPageIndex)
        {
            if (dgvCategory.PageCount > 0)
            {
                dgvCategory.PageIndex = 0;
            }
        }
        dgvCategory.DataSource = null;
        dgvCategory.DataBind();
        hdnSelectedIDs.Value = "";
        if (objDataTable.Rows.Count <= 0)
        {
            DinfoBranchGrid.ShowMessage("No data found", Enums.MessageType.Information);
            return;
        }
        else
        {
            dgvCategory.AllowPaging = false;
            objDataTable = SortDatatable(objDataTable, ViewState["SortColumn"].ToString(), (appFunctions.Enum_SortOrderBy)ViewState["SortOrder"], IsSort);
            dgvCategory.DataSource = objDataTable;
            dgvCategory.DataBind();
        }

        objCouponCodeProduct = null;
    }

    protected void dgvCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dgvSubCategory.PageIndex = e.NewPageIndex;
        LoadCategoryData(false, false);
    }

    protected void dgvCategory_RowCreated(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                string strID = dgvCategory.DataKeys[e.Row.RowIndex].Values[0].ToString();
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSelectRow");
                chk.ID = "chkSelectRow_" + strID;
                break;
        }
    }

    protected void dgvCategory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataControlRowType itemType = e.Row.RowType;
        switch (itemType)
        {
            case DataControlRowType.DataRow:
                string strID = dgvCategory.DataKeys[e.Row.RowIndex].Values[0].ToString();
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSelectRow");
                if ((chk != null))
                {
                    chk.Attributes.Add("OnClick", "javascript:SelectRow(this," + strID + ")");
                }
                break;
        }
    }

    protected void dgvCategory_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnSelectedIDs.Value = "";
        ViewState["SortColumn"] = e.SortExpression;
        LoadCategoryData(false, true);
    }

    protected void dgvCategory_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void dgvCategory_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }


    /*-- For Product --*/

    protected void ddlProductCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon = new clsCommon();
        objCommon.FillDropDownList(ddlSubCate, "tblSubCategory ", tblSubCategory.ColumnNames.AppSubCategory, tblSubCategory.ColumnNames.AppSubCategoryID, "--Select SubCategory--", tblSubCategory.ColumnNames.AppCategoryID + "=" + ddlProductCategory.SelectedValue);
        if (ddlProductCategory.SelectedIndex > 0)
        {
            LoadUnSelectedProduct(true, false);
        }
        else
        {
            dgvProduct.DataSource = null;
            dgvProduct.DataBind();
        }
        objCommon = null;
    }

    protected void ddlSubCate_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadUnSelectedProduct(true, false);
    }

    public void LoadUnSelectedProduct(bool IsResetPageIndex, bool IsSort)
    {
        if (ddlProductCategory.SelectedIndex > 0)
        {
            objCouponCodeProduct = new tblCouponCodeProduct();
            objDataTable = objCouponCodeProduct.LoadUnSelectedCategories(Convert.ToInt32(hdnType.Value), hdnPKID.Value, ddlProductCategory.SelectedValue, ddlSubCate.SelectedValue);
            if (IsResetPageIndex)
            {
                if (dgvProduct.PageCount > 0)
                {
                    dgvProduct.PageIndex = 0;
                }
            }
            dgvProduct.DataSource = null;
            dgvProduct.DataBind();
            lblProductCount.Text = 0.ToString();
            hdnSelectedIDs.Value = "";
            //'Check for data into datatable
            if (objDataTable.Rows.Count <= 0)
            {
                DinfoProduct.ShowMessage("No data found", Enums.MessageType.Information);
                return;
            }
            else
            {
                dgvProduct.AllowPaging = false;
                lblProductCount.Text = objDataTable.Rows.Count.ToString();
                objDataTable = SortDatatable(objDataTable, ViewState["SortColumn"].ToString(), (appFunctions.Enum_SortOrderBy)ViewState["SortOrder"], IsSort);
                dgvProduct.DataSource = objDataTable;
                dgvProduct.DataBind();
            }
            objCouponCodeProduct = null;
        }

    }

    protected void btnProductReset_Click(object sender, EventArgs e)
    {
        ddlSubCate.SelectedIndex = 0;
        ddlCategory.SelectedIndex = 0;
        lblProductCount.Text = "0";
        dgvProduct.DataSource = null;
        dgvProduct.DataBind();

    }

    protected void dgvProduct_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
        {
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                if (e.CommandName == "IsAdd")
                {
                    objCouponCodeProduct = new tblCouponCodeProduct();
                    objCouponCodeProduct.AddNew();
                    objCouponCodeProduct.s_AppCouponCodeID = hdnPKID.Value;
                    objCouponCodeProduct.s_AppReferenceID = e.CommandArgument.ToString();
                    objCouponCodeProduct.Save();
                    objCouponCodeProduct = null;
                    LoadUnSelectedProduct(true, false);
                    LoadProductDataGrid(true, false);
                }
            }
        }
    }

    private void LoadProductDataGrid(bool IsResetPageIndex, bool IsSort, string strFieldName = "", string strFieldValue = "")
    {
        objCouponCodeProduct = new tblCouponCodeProduct();
        objDataTable = objCouponCodeProduct.LoadGridData(Convert.ToInt32(hdnType.Value), hdnPKID.Value);
        if (IsResetPageIndex)
        {
            if (dgvGridViewProduct.PageCount > 0)
            {
                dgvGridViewProduct.PageIndex = 0;
            }
        }

        dgvGridViewProduct.DataSource = null;
        dgvGridViewProduct.DataBind();
        hdnSelectedIDs.Value = "";
        if (objDataTable.Rows.Count <= 0)
        {
            DinfoBranchGrid.ShowMessage("No data found", Enums.MessageType.Information);
            return;
        }
        else
        {
            dgvGridViewProduct.AllowPaging = false;
            objDataTable = SortDatatable(objDataTable, ViewState["SortColumn"].ToString(), (appFunctions.Enum_SortOrderBy)ViewState["SortOrder"], IsSort);
            dgvGridViewProduct.DataSource = objDataTable;
            dgvGridViewProduct.DataBind();
        }
        objCouponCodeProduct = null;
    }

    protected void dgvGridViewProduct_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnSelectedIDs.Value = "";
        ViewState["SortColumn"] = e.SortExpression;
        LoadProductDataGrid(false, true);
    }

    protected void dgvGridViewProduct_RowCreated(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                string strID = dgvGridViewProduct.DataKeys[e.Row.RowIndex].Values[0].ToString();
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSelectRow");
                chk.ID = "chkSelectRow_" + strID;
                break;
        }
    }

    protected void dgvGridViewProduct_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataControlRowType itemType = e.Row.RowType;

        switch (itemType)
        {
            case DataControlRowType.DataRow:
                string strID = dgvGridViewProduct.DataKeys[e.Row.RowIndex].Values[0].ToString();
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSelectRow");
                if ((chk != null))
                {
                    chk.Attributes.Add("OnClick", "javascript:SelectRow(this," + strID + ")");
                }
                break;
        }
    }

    protected void dgvGridViewProduct_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }

    protected void dgvGridViewProduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

}
