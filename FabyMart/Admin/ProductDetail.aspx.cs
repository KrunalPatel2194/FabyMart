using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.IO;
using System.Drawing;


public partial class ProductDetail : PageBase_Admin
{
    tblProduct objproduct;
    tblProductSubCategory objProductSubCategory;
    tblProductColor objProductColor;
    tblProductDetail objProductDetail;
    tblRelatedProduct objRelatedProduct;
    int intPkId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SetRegulerExpression();
            lnkSaveAndAddnew.Visible = HasAdd;
            ViewState["SortOrder"] = appFunctions.Enum_SortOrderBy.Asc;
            ViewState["SortColumn"] = "";
            objCommon = new clsCommon();
            objCommon.FillDropDownList(ddlColor, "tblColor ", tblColor.ColumnNames.AppColorName, tblColor.ColumnNames.AppColorID, "--Select Color--", tblColor.ColumnNames.AppColorName, appFunctions.Enum_SortOrderBy.Asc);
            objCommon = null;
            divTag.Visible = false;
            if ((Request.QueryString.Get("ID") != null))
            {
                chkIsColor.Enabled = false;
                chkIsSize.Enabled = false;
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
                LoadAllData();
            }
            else
            {
                lnkSaveAndClose.Visible = false;
                lnkSaveAndAddnew.Visible = false;
                btnDropdown.Disabled = true;
                chkIsColor.Enabled = true;
                chkIsSize.Enabled = true;
                ddlColor.Enabled = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "HideTab()", true);
            }
        }
    }

    private bool SaveData(ref string strMsg)
    {
        objCommon = new clsCommon();
        if (objCommon.IsRecordExists("tblProduct", tblProduct.ColumnNames.AppProductCode, tblProduct.ColumnNames.AppProductID, txtCode.Text, hdnPKID.Value))
        {
            DInfo.ShowMessage("Product Code Name alredy exits.", Enums.MessageType.Error);
            return false;
        }
        if (objCommon.IsRecordExists("tblProduct", tblProduct.ColumnNames.AppProductName, tblProduct.ColumnNames.AppProductID, txtProductName.Text, hdnPKID.Value))
        {
            DInfo.ShowMessage("Product Name alredy exits.", Enums.MessageType.Error);
            return false;
        }
        objproduct = new tblProduct();
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objproduct.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value));
            strMsg = "Product has been updated successfully";
        }
        else
        {
            objproduct.AddNew();
            objproduct.s_AppCreatedBy = Session[appFunctions.Session.UserID.ToString()].ToString();
            // objproduct.s_AppCreatedDate = FormatDateString(DateTime.Now.ToString(strInputDateFormat), strInputDateFormat, strOutputDateFormat);
            objproduct.s_AppCreatedDate = GetCurrentDateTime();
            strMsg = "Product has been added successfully";
        }
        objproduct.AppProductCode = txtCode.Text;
        objproduct.AppProductName = txtProductName.Text;
        objproduct.AppProductTag = txtTag.Text;
        objproduct.AppDescription = txtDescription.Text;
        //  objproduct.AppWashCare = txtWashCare.Text;
        objproduct.AppEstimatedDeliveryDays = txtEstimatedDeliveryDays.Text;
        objproduct.s_AppWeight = txtWeight.Text;
        objproduct.AppIsNewArrival = chkIsNewArrival.Checked;
        objproduct.AppIsActive = chkIsActiveProduct.Checked;
        objproduct.AppIsFeatured = ChkIsFeatured.Checked;
        objproduct.AppIsBestSeller = ChkIsBestSeller.Checked;
        objproduct.AppMetaKeyWord = txtMetaKeyWord.Text;
        objproduct.AppMetaDescription = txtMetaDescription.Text;
        objproduct.AppBrowserTitle = txtBrowserTitle.Text;
        objproduct.Save();
        hdnPKID.Value = objproduct.s_AppProductID;
        if (objproduct.s_AppIsColor == "")
        {
            if (!chkIsColor.Checked)
            {
                SaveProductColorData(true);
            }
            else
            {
                SaveProductColorData();
            }
        }
        objproduct.AppIsColor = chkIsColor.Checked;
        objproduct.AppIsSize = chkIsSize.Checked;
        objproduct.Save();

        objproduct = null;
        objCommon = null;

        return true;
    }

    private void SetValuesToControls()
    {
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objproduct = new tblProduct();
            if (objproduct.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value)))
            {
                txtCode.Text = objproduct.AppProductCode;
                txtProductName.Text = objproduct.AppProductName;
                txtTag.Text = objproduct.AppProductTag;
                txtDescription.Text = objproduct.AppDescription;
                chkIsNewArrival.Checked = objproduct.AppIsNewArrival;
                chkIsActiveProduct.Checked = objproduct.AppIsActive;
                ChkIsFeatured.Checked = objproduct.AppIsFeatured;
                ChkIsBestSeller.Checked = objproduct.AppIsBestSeller;
                if (objproduct.s_AppIsColor != "")
                {
                    chkIsColor.Checked = objproduct.AppIsColor;
                }
                else
                {
                    chkIsColor.Checked = false;
                }
                if (objproduct.s_AppIsSize != "")
                {
                    chkIsSize.Checked = objproduct.AppIsSize;
                }
                else
                {
                    chkIsSize.Checked = false;
                }

                txtEstimatedDeliveryDays.Text = objproduct.s_AppEstimatedDeliveryDays;
                txtWeight.Text = objproduct.s_AppWeight;
                txtMetaKeyWord.Text = objproduct.AppMetaKeyWord;
                txtMetaDescription.Text = objproduct.AppMetaDescription;
                txtBrowserTitle.Text = objproduct.AppBrowserTitle;
                // txtWashCare.Text = objproduct.s_AppWashCare;

                lnkSaveAndClose.Visible = true;
                lnkSaveAndAddnew.Visible = true;
                btnDropdown.Disabled = false;
                chkIsColor.Enabled = false;
                chkIsSize.Enabled = false;
                ddlColor.Enabled = false;
                //btnDropdown.Visible = true ;
                //lnkSaveAndClose.Visible = true;
                //lnkSaveAndAddnew.Visible = true;
            }
            objproduct = null;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Product.aspx", true);
    }

    private void ResetControls()
    {
        txtCode.Text = "";
        txtProductName.Text = "";
        txtTag.Text = "";
        txtDescription.Text = "";
        chkIsNewArrival.Checked = false;
        ChkIsFeatured.Checked = false;
        ChkIsBestSeller.Checked = false;
        chkIsColor.Checked = true;
        chkIsSize.Checked = false;
        txtMetaKeyWord.Text = "";
        txtMetaDescription.Text = "";
        txtBrowserTitle.Text = "";
        txtEstimatedDeliveryDays.Text = "";
        txtWeight.Text = "";
        //txtWashCare.Text = "";
        hdnPKID.Value = "";
        btnDropdown.Visible = false;
        lnkSaveAndClose.Visible = false;
        lnkSaveAndAddnew.Visible = false;
        btnDropdown.Disabled = true;
        chkIsColor.Enabled = true;
        chkIsSize.Enabled = true;
        ddlColor.Enabled = true;
        objCommon = new clsCommon();
        objCommon.FillDropDownList(ddlColor, "tblColor ", tblColor.ColumnNames.AppColorName, tblColor.ColumnNames.AppColorCode, "--Select Color--", tblColor.ColumnNames.AppColorName, appFunctions.Enum_SortOrderBy.Asc);
        objCommon = null;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "HideTab()", true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string strMsg = "";
        if (SaveData(ref strMsg))
        {
            DInfo.ShowMessage(strMsg, Enums.MessageType.Successfull);
            SetValuesToControls();
            LoadAllData();
        }
        else
        {
            if (hdnPKID.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "HideTab()", true);
            }
        }
    }

    protected void lnkSaveAndClose_Click(object sender, EventArgs e)
    {
        string strMsg = "";
        if (SaveData(ref strMsg))
        {
            Session[appFunctions.Session.ShowMessage.ToString()] = strMsg;
            Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            Response.Redirect("Product.aspx");
        }
        else
        {
            if (hdnPKID.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "HideTab()", true);
            }
        }

    }

    protected void lnkSaveAndAddnew_Click(object sender, EventArgs e)
    {
        string strMsg = "";
        if (SaveData(ref strMsg))
        {
            DInfo.ShowMessage(strMsg, Enums.MessageType.Successfull);
            ResetControls();
        }
        else
        {
            if (hdnPKID.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "HideTab()", true);
            }
        }
        btnDropdown.Visible = false;
        lnkSaveAndClose.Visible = false;
        lnkSaveAndAddnew.Visible = false;
    }
    //Load All Data
    public void LoadAllData()
    {
        objCommon = new clsCommon();
        objCommon.FillRecordPerPage(ref ddlSubCategoryPerPage);
        //objCommon.FillRecordPerPage(ref ddlProductColorPerPage);
        objCommon.FillRecordPerPage(ref ddlProductImagePerPage);
        objCommon.FillRecordPerPage(ref ddlColorDetailPerPage);
        objCommon.FillRecordPerPage(ref ddlRelatedPerPage);
        objCommon.FillRecordPerPage(ref ddlPixelCode);
        objCommon.FillDropDownList(ddlCategory, "tblCategory ", tblCategory.ColumnNames.AppCategory, tblCategory.ColumnNames.AppCategoryID, "--Select Category--", tblCategory.ColumnNames.AppCategory, appFunctions.Enum_SortOrderBy.Asc);
        objCommon.FillDropDownList(ddlSerachCategory, "tblCategory ", tblCategory.ColumnNames.AppCategory, tblCategory.ColumnNames.AppCategoryID, "--Select Category--", tblCategory.ColumnNames.AppCategory, appFunctions.Enum_SortOrderBy.Asc);
        objCommon.FillDropDownList(ddlRelatedCategory, "tblCategory ", tblCategory.ColumnNames.AppCategory, tblCategory.ColumnNames.AppCategoryID, "--Select Category--", tblCategory.ColumnNames.AppCategory, appFunctions.Enum_SortOrderBy.Asc);
        ddlRelatedSubCategory.Items.Add(new ListItem("--Select Sub Category--", "0"));
        //objCommon.FillDropDownList(ddlColor, "tblColor ", tblColor.ColumnNames.AppColorName, tblColor.ColumnNames.AppColorCode, "--Select Color--", tblColor.ColumnNames.AppColorName, appFunctions.Enum_SortOrderBy.Asc);
        //objCommon.FillDropDownList(ddlSearchColor, "tblColor ", tblColor.ColumnNames.AppColorName, tblColor.ColumnNames.AppColorID, "--Select Color--", tblColor.ColumnNames.AppColorName, appFunctions.Enum_SortOrderBy.Asc);

        objCommon = null;
        FillSize();
        LoadSubCategory();
        LoadProductColorData(true, false);
        LoadProductPropertyData();
        ColorDetailDropDownFill();
        LoadColorDetali();
        LoadAllRelatedProduct();
        LoadPixelCode(false, false);
        if (!chkIsColor.Checked)
        {
            //divColorControl.Style.Add("display", "none");
            //divColorSerach.Style.Add("display", "none");
            //dgvProductColor.Columns[1].Visible = false;
        }
        else
        {
            //divColorControl.Style.Add("display", "block");
            //divColorSerach.Style.Add("display", "block");
        }

        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowTab()", true);
    }

    public void FillSize()
    {
        ddlSize.Items.Clear();
        objCommon = new clsCommon();
        if (!chkIsSize.Checked)
        {
            objCommon.FillDropDownListWithOutDefaultValue(ddlSize, "tblSize ", tblSize.ColumnNames.AppSize, tblSize.ColumnNames.AppSizeID, tblSize.ColumnNames.AppSize, appFunctions.Enum_SortOrderBy.Asc, tblSize.ColumnNames.AppIsDefault + " =1");
            ddlSize.Enabled = false;
            divddlSize.Style.Add("display", "none");
            divDetailIsDefault.Style.Add("display", "none");
        }
        else
        {
            tblSize objsize = new tblSize();
            string strId = objCommon.JoinWithComma(objsize.LoadSizeProduct(hdnPKID.Value), tblSize.ColumnNames.AppSizeID, Enums.Enum_DataType.iInteger);
            if (strId == "")
            {
                strId = "0";
            }
            objCommon.FillDropDownList(ddlSize, "tblSize ", tblSize.ColumnNames.AppSize, tblSize.ColumnNames.AppSizeID, "--Select Size--", tblSize.ColumnNames.AppSize, appFunctions.Enum_SortOrderBy.Asc, tblSize.ColumnNames.AppSizeID + " in (" + strId + ")");
            objsize = null;
            ddlSize.Enabled = true;
            divddlSize.Style.Add("display", "block");
            divDetailIsDefault.Style.Add("display", "block");
        }
        objCommon = null;
    }

    public void ColorDetailDropDownFill()
    {

        ddlColorDetail.Items.Clear();
        //ddlSerachColorDetail.Items.Clear();
        objCommon = new clsCommon();
        objCommon.FillDropDownListWithOutDefaultValue(ddlColor, "tblProductColor Inner Join tblcolor On tblcolor.appColorId= tblProductColor.appColorId ", tblColor.ColumnNames.AppColorName, tblProductColor.ColumnNames.AppProductColorID, tblProductColor.ColumnNames.AppProductColorID, appFunctions.Enum_SortOrderBy.Asc, tblProductColor.ColumnNames.AppProductID + "=" + hdnPKID.Value);
        objCommon.FillDropDownListWithOutDefaultValue(ddlColorDetail, "tblProductColor Inner Join tblcolor On tblcolor.appColorId= tblProductColor.appColorId ", tblColor.ColumnNames.AppColorName, tblProductColor.ColumnNames.AppProductColorID, tblProductColor.ColumnNames.AppProductColorID, appFunctions.Enum_SortOrderBy.Asc, tblProductColor.ColumnNames.AppProductID + "=" + hdnPKID.Value);
        divddlcolor.Style.Add("display", "none");
        //if (!chkIsColor.Checked)
        //{
        //    objCommon.FillDropDownListWithOutDefaultValue(ddlColorDetail, "tblProductColor Inner Join tblcolor On tblcolor.appColorId= tblProductColor.appColorId ", tblColor.ColumnNames.AppColorName, tblProductColor.ColumnNames.AppProductColorID, tblProductColor.ColumnNames.AppProductColorID, appFunctions.Enum_SortOrderBy.Asc, tblProductColor.ColumnNames.AppProductID + "=" + hdnPKID.Value);
        //    ddlColorDetail.Enabled = false;
        //    objCommon.FillDropDownList(ddlSerachColorDetail, "tblProductColor Inner Join tblcolor On tblcolor.appColorId= tblProductColor.appColorId ", tblColor.ColumnNames.AppColorName, tblProductColor.ColumnNames.AppProductColorID, "--Select Color--", tblProductColor.ColumnNames.AppProductID + "=" + hdnPKID.Value);
        //    divddlcolor.Style.Add("display", "none");
        //}
        //else
        //{
        //    objCommon.FillDropDownList(ddlColorDetail, "tblProductColor Inner Join tblcolor On tblcolor.appColorId= tblProductColor.appColorId ", tblColor.ColumnNames.AppColorName, tblProductColor.ColumnNames.AppProductColorID, "--Select Color--", tblProductColor.ColumnNames.AppProductID + "=" + hdnPKID.Value);
        //    objCommon.FillDropDownList(ddlSerachColorDetail, "tblProductColor Inner Join tblcolor On tblcolor.appColorId= tblProductColor.appColorId ", tblColor.ColumnNames.AppColorName, tblProductColor.ColumnNames.AppProductColorID, "--Select Color--", tblProductColor.ColumnNames.AppProductID + "=" + hdnPKID.Value);
        //    ddlColorDetail.Enabled = true;
        //    divddlcolor.Style.Add("display", "block");
        //}
        objCommon = null;
    }
    // For Sub Category
    public void LoadSubCategory()
    {
        ResetSubCategoryControl();
        LoadSubCategoryData(true, false);
    }

    public void ResetSubCategoryControl()
    {
        ddlCategory.SelectedIndex = 0;
        hdnSubCategoryId.Value = "";
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadUnselectedCategories();
        LoadSubCategoryData(false, false);
    }

    private bool SaveSubCategoryData()
    {
        if (hdnPKID.Value != "")
        {
            objProductSubCategory = new tblProductSubCategory();

            if (!string.IsNullOrEmpty(hdnSubCategoryId.Value) && hdnSubCategoryId.Value != "")
            {
                objProductSubCategory.LoadByPrimaryKey(Convert.ToInt32(hdnSubCategoryId.Value));
            }
            else
            {
                objProductSubCategory.AddNew();
                objProductSubCategory.AppDisplayOrder = objCommon.GetNextDisplayOrder("tblProductSubCategory", tblProductSubCategory.ColumnNames.AppDisplayOrder, tblProductSubCategory.ColumnNames.AppProductID + "=" + hdnPKID.Value);
            }
            objProductSubCategory.s_AppProductID = hdnPKID.Value;
            objProductSubCategory.Save();
            objProductSubCategory = null;

            return true;
        }
        else
        {
            return false;
        }
    }

    public void LoadSubCategoryData(bool IsResetPageIndex, bool IsSort, string strFieldName = "", string strFieldValue = "")
    {
        objProductSubCategory = new tblProductSubCategory();

        objDataTable = objProductSubCategory.LoadGridData(hdnPKID.Value, ddlSerachCategory.SelectedValue);

        //'Reset PageIndex of gridviews
        if (IsResetPageIndex)
        {
            if (dgvSubCategory.PageCount > 0)
            {
                dgvSubCategory.PageIndex = 0;
            }
        }

        dgvSubCategory.DataSource = null;
        dgvSubCategory.DataBind();
        lblSubCategoryCount.Text = 0.ToString();
        hdnSelectedIDs.Value = "";

        //'Check for data into datatable
        if (objDataTable.Rows.Count <= 0)
        {
            DinfoBranchGrid.ShowMessage("No data found", Enums.MessageType.Information);
            return;
        }
        else
        {
            if (ddlSubCategoryPerPage.SelectedItem.Text.ToLower() == "all")
            {
                dgvSubCategory.AllowPaging = false;
            }
            else
            {
                dgvSubCategory.AllowPaging = true;
                dgvSubCategory.PageSize = Convert.ToInt32(ddlSubCategoryPerPage.SelectedItem.Text);
            }

            lblSubCategoryCount.Text = objDataTable.Rows.Count.ToString();
            objDataTable = SortDatatable(objDataTable, ViewState["SortColumn"].ToString(), (appFunctions.Enum_SortOrderBy)ViewState["SortOrder"], IsSort);
            dgvSubCategory.DataSource = objDataTable;
            dgvSubCategory.DataBind();
        }

        objProductSubCategory = null;
        FillSize();
        LoadProductPropertyData();
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

    protected void btnSubCategoryReset_Click(object sender, System.EventArgs e)
    {
        ddlSerachCategory.SelectedIndex = 0;
        LoadSubCategoryData(true, false);
    }

    protected void ddlSerachCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon = new clsCommon();
        //  objCommon.FillDropDownList(ddlSerachSubCategory, "tblSubCategory ", tblSubCategory.ColumnNames.AppSubCategory, tblSubCategory.ColumnNames.AppSubCategoryID, "--Select Sub Category--", tblSubCategory.ColumnNames.AppSubCategory, appFunctions.Enum_SortOrderBy.Asc, tblSubCategory.ColumnNames.AppCategoryID + "=" + ddlSerachCategory.SelectedValue);
        objCommon = null;
        LoadSubCategoryData(false, false);
    }

    protected void ddlSubCategoryPerPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnSelectedIDs.Value = "";
        LoadSubCategoryData(true, false);
    }

    protected void dgvSubCategory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
        {
            objCommon = new clsCommon();
            if (e.CommandName == "Edit")
            {
                hdnSubCategoryId.Value = e.CommandArgument.ToString();
                SetSubCategoryValuesToControls();
            }
            else if (e.CommandName == "Up")
            {
                LinkButton inkButton = (LinkButton)e.CommandSource;
                GridViewRow drCurrent = (GridViewRow)inkButton.Parent.Parent;
                if (drCurrent.RowIndex > 0)
                {
                    GridViewRow drUp = dgvSubCategory.Rows[drCurrent.RowIndex - 1];
                    objCommon.SetDisplayOrder("tblProductSubCategory", tblProductSubCategory.ColumnNames.AppProductSubCategoryID, tblProductSubCategory.ColumnNames.AppDisplayOrder, (int)dgvSubCategory.DataKeys[drCurrent.RowIndex].Values[0], (int)dgvSubCategory.DataKeys[drCurrent.RowIndex].Values[1], (int)dgvSubCategory.DataKeys[drUp.RowIndex].Values[0], (int)dgvSubCategory.DataKeys[drUp.RowIndex].Values[1]);
                    LoadSubCategoryData(false, false);
                    objCommon = null;
                }
            }
            else if (e.CommandName == "Down")
            {
                LinkButton lnkButton = (LinkButton)e.CommandSource;
                GridViewRow drCurrent = (GridViewRow)lnkButton.Parent.Parent;
                if (drCurrent.RowIndex < dgvSubCategory.Rows.Count - 1)
                {
                    GridViewRow drUp = dgvSubCategory.Rows[drCurrent.RowIndex + 1];
                    objCommon.SetDisplayOrder("tblProductSubCategory", tblProductSubCategory.ColumnNames.AppProductSubCategoryID, tblProductSubCategory.ColumnNames.AppDisplayOrder, (int)dgvSubCategory.DataKeys[drCurrent.RowIndex].Values[0], (int)dgvSubCategory.DataKeys[drCurrent.RowIndex].Values[1], (int)dgvSubCategory.DataKeys[drUp.RowIndex].Values[0], (int)dgvSubCategory.DataKeys[drUp.RowIndex].Values[1]);
                    LoadSubCategoryData(false, false);
                    objCommon = null;
                }
            }
            else if (e.CommandName == "IsActive")
            {
                objProductSubCategory = new tblProductSubCategory();

                if (objProductSubCategory.LoadByPrimaryKey(Convert.ToInt32(e.CommandArgument.ToString())))
                {

                    if (objProductSubCategory.AppIsActive == true)
                    {
                        objProductSubCategory.AppIsActive = false;
                    }
                    else if (objProductSubCategory.AppIsActive == false)
                    {
                        objProductSubCategory.AppIsActive = true;
                    }
                    objProductSubCategory.Save();

                }
                LoadSubCategoryData(false, false);
                objProductSubCategory = null;
            }

        }
    }

    private void SetSubCategoryValuesToControls()
    {
        if (!string.IsNullOrEmpty(hdnSubCategoryId.Value) && hdnSubCategoryId.Value != "")
        {
            objProductSubCategory = new tblProductSubCategory();
            if (objProductSubCategory.LoadByPrimaryKey(Convert.ToInt32(hdnSubCategoryId.Value)))
            {
                tblSubCategory objTemp = new tblSubCategory();
                if (objTemp.LoadByPrimaryKey(objProductSubCategory.AppSubCategoryID))
                {
                    ddlCategory.SelectedValue = objTemp.s_AppCategoryID;
                    objCommon = new clsCommon();
                    //  objCommon.FillDropDownList(ddlSubCategory, "tblSubCategory ", tblSubCategory.ColumnNames.AppSubCategory, tblSubCategory.ColumnNames.AppSubCategoryID, "--Select Sub Category--", tblSubCategory.ColumnNames.AppSubCategory, appFunctions.Enum_SortOrderBy.Asc, tblSubCategory.ColumnNames.AppCategoryID + "=" + ddlCategory.SelectedValue);
                    objCommon = null;
                    //  ddlSubCategory.SelectedValue = objProductSubCategory.s_AppSubCategoryID;
                }
                objTemp = null;

                //chkSubCategoryIsActive.Checked = objProductSubCategory.AppIsActive;

            }
            objProductSubCategory = null;
        }
    }

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
            LoadSubCategoryData(false, false);
            LoadProductPropertyData();
            FillSize();
        }
        DInfoSubCategory.ShowMessage("Sub Category has been deleted successfully", Enums.MessageType.Successfull);
        hdnSelectedIDs.Value = "";
        LoadUnselectedCategories();
    }

    private bool DeleteSubCategory(int intPKID)
    {
        bool retval = false;
        objProductSubCategory = new tblProductSubCategory();
        var _with1 = objProductSubCategory;
        if (_with1.LoadByPrimaryKey(intPKID))
        {
            tblProductProperty objProductProperty = new tblProductProperty();
            objProductProperty.DeleteProperty(hdnPKID.Value, objProductSubCategory.s_AppSubCategoryID);
            objProductProperty = null;
            _with1.MarkAsDeleted();
            _with1.Save();
        }
        retval = true;


        objProductSubCategory = null;
        return retval;
    }

    protected void dgvSubCategory_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void dgvSubCategory_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    //Product Color
    public void LoadProductColor()
    {
        ResetProductColorControl();
        LoadProductColorData(true, false);
    }

    public void ResetProductColorControl()
    {
        //ddlColor.SelectedIndex = 0;
        //chkProductColorIsActive.Checked = true;
        //chkProductColorIsDefault.Checked = false;
        hdnProductColorId.Value = "";
    }

    protected void btnSaveProductColor_Click(object sender, EventArgs e)
    {
        if (SaveProductColorData())
        {
            //if (string.IsNullOrEmpty(hdnProductColorId.Value))
            //{
            //    DInfoProductColor.ShowMessage("Product Color has been added successfully", Enums.MessageType.Successfull);
            //}
            //else
            //{
            //    DInfoProductColor.ShowMessage("Product Color has been updated successfully", Enums.MessageType.Successfull);
            //}
            ResetProductColorControl();
            ColorDetailDropDownFill();
            LoadProductColorData(false, false);
        }
    }

    protected void btnClearProductColor_Click(object sender, EventArgs e)
    {
        ResetProductColorControl();
    }

    private bool SaveProductColorData(bool IsDefault = false)
    {
        if (hdnPKID.Value != "")
        {
            int intColorId = 0;
            if (IsDefault)
            {
                tblColor objColor = new tblColor();
                objColor.Where.AppIsDefault.Value = true;
                objColor.Query.Load();
                if (objColor.RowCount > 0)
                {
                    intColorId = objColor.AppColorID;
                }
                objColor = null;
            }
            else
            {
                intColorId = Convert.ToInt32(ddlColor.SelectedValue);
            }

            objCommon = new clsCommon();
            if (objCommon.IsRecordExists("tblProductColor", tblProductColor.ColumnNames.AppColorID, tblProductColor.ColumnNames.AppProductColorID, intColorId.ToString(), hdnProductColorId.Value, tblProductColor.ColumnNames.AppProductID + "=" + hdnPKID.Value))
            {
                //DInfoProductColor.ShowMessage("Product Color alredy exits.", Enums.MessageType.Error);
                DInfo.ShowMessage("Product Color alredy exits.", Enums.MessageType.Error);
                return false;
            }

            objProductColor = new tblProductColor();

            if (!string.IsNullOrEmpty(hdnProductColorId.Value) && hdnProductColorId.Value != "")
            {
                objProductColor.LoadByPrimaryKey(Convert.ToInt32(hdnProductColorId.Value));
            }
            else
            {
                objProductColor.AddNew();
                //objProductColor.AppDisplayOrder = objCommon.GetNextDisplayOrder("tblProductColor", tblProductColor.ColumnNames.AppDisplayOrder, tblProductColor.ColumnNames.AppProductID + "=" + hdnPKID.Value);
                objProductColor.AppDisplayOrder = 1;
            }
            objProductColor.AppColorID = intColorId;
            objProductColor.s_AppProductID = hdnPKID.Value;


            if (IsDefault)
            {
                tblProductColor objTempColor = new tblProductColor();
                objTempColor.SetProductDefaultColor(hdnPKID.Value);
                objTempColor = null;
                objProductColor.AppIsActive = true;
                objProductColor.AppIsDefault = true;
            }
            else
            {
                objProductColor.AppIsActive = true;
                objProductColor.AppIsDefault = true;
                //objProductColor.AppIsActive = chkProductColorIsActive.Checked;
                //if (chkProductColorIsDefault.Checked)
                //{
                //    tblProductColor objTempColor = new tblProductColor();
                //    objTempColor.SetProductDefaultColor(hdnPKID.Value);
                //    objTempColor = null;
                //    objProductColor.AppIsActive = true;
                //    objProductColor.AppIsDefault = true;
                //}
                //else
                //{
                //    tblProductColor objTempColor = new tblProductColor();
                //    objTempColor.Where.AppProductID.Value = hdnPKID.Value;
                //    objTempColor.Query.Load();
                //    if (objTempColor.RowCount <= 0)
                //    {
                //        objProductColor.AppIsActive = true;
                //        objProductColor.AppIsDefault = true;
                //    }
                //    else
                //    {
                //        objProductColor.AppIsDefault = false;
                //    }
                //    objTempColor = null;

                //}
            }

            objProductColor.Save();
            objProductColor = null;
            objCommon = null;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void LoadProductColorData(bool IsResetPageIndex, bool IsSort, string strFieldName = "", string strFieldValue = "")
    {
        objProductColor = new tblProductColor();
        //objDataTable = objProductColor.LoadGridData(hdnPKID.Value, ddlSearchColor.SelectedValue);
        objDataTable = objProductColor.LoadGridData(hdnPKID.Value, "0");
        //'Reset PageIndex of gridviews
        if (IsResetPageIndex)
        {
            if (dgvProductColor.PageCount > 0)
            {
                dgvProductColor.PageIndex = 0;
            }
        }

        dgvProductColor.DataSource = null;
        dgvProductColor.DataBind();
        //lblProductColorCount.Text = 0.ToString();
        hdnSelectedIDs.Value = "";

        //'Check for data into datatable
        if (objDataTable.Rows.Count <= 0)
        {
            DinfoProductColorData.ShowMessage("No data found", Enums.MessageType.Information);
            return;
        }
        else
        {
            //if (ddlProductColorPerPage.SelectedItem.Text.ToLower() == "all")
            //{
            dgvProductColor.AllowPaging = false;
            //}
            //else
            //{
            //    dgvProductColor.AllowPaging = true;
            //    dgvProductColor.PageSize = Convert.ToInt32(ddlProductColorPerPage.SelectedItem.Text);
            //}

            //lblProductColorCount.Text = objDataTable.Rows.Count.ToString();
            objDataTable = SortDatatable(objDataTable, ViewState["SortColumn"].ToString(), (appFunctions.Enum_SortOrderBy)ViewState["SortOrder"], IsSort);
            dgvProductColor.DataSource = objDataTable;
            dgvProductColor.DataBind();
        }

        objProductColor = null;
    }

    protected void btnProductColorReset_Click(object sender, EventArgs e)
    {
        //ddlSearchColor.SelectedIndex = 0;
        LoadProductColorData(true, false);
    }

    protected void dgvProductColor_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dgvProductColor.PageIndex = e.NewPageIndex;
        LoadProductColorData(false, false);
    }

    protected void dgvProductColor_RowCreated(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                string strID = dgvProductColor.DataKeys[e.Row.RowIndex].Values[0].ToString();
                //CheckBox chk = (CheckBox)e.Row.FindControl("chkSelectRow");
                //chk.ID = "chkSelectRow_" + strID;
                break;
        }
    }

    protected void dgvProductColor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataControlRowType itemType = e.Row.RowType;

        switch (itemType)
        {
            case DataControlRowType.DataRow:
                string strID = dgvProductColor.DataKeys[e.Row.RowIndex].Values[0].ToString();
                //CheckBox chk = (CheckBox)e.Row.FindControl("chkSelectRow");
                //if ((chk != null))
                //{
                //    chk.Attributes.Add("OnClick", "javascript:SelectRow(this," + strID + ")");
                //}
                break;
        }
    }

    protected void dgvProductColor_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnSelectedIDs.Value = "";
        ViewState["SortColumn"] = e.SortExpression;
        LoadProductColorData(false, true);
    }

    protected void ddlSearchColor_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnSelectedIDs.Value = "";
        LoadProductColorData(true, false);
    }

    protected void ddlProductColorPerPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnSelectedIDs.Value = "";
        LoadProductColorData(true, false);
    }

    protected void dgvProductColor_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
        {
            objCommon = new clsCommon();
            if (e.CommandName == "Edit")
            {
                hdnProductColorId.Value = e.CommandArgument.ToString();
                SetProductColorValuesToControls();
            }
            else if (e.CommandName == "Up")
            {
                LinkButton inkButton = (LinkButton)e.CommandSource;
                GridViewRow drCurrent = (GridViewRow)inkButton.Parent.Parent;
                if (drCurrent.RowIndex > 0)
                {
                    GridViewRow drUp = dgvProductColor.Rows[drCurrent.RowIndex - 1];
                    objCommon.SetDisplayOrder("tblProductColor", tblProductColor.ColumnNames.AppProductColorID, tblProductColor.ColumnNames.AppDisplayOrder, (int)dgvProductColor.DataKeys[drCurrent.RowIndex].Values[0], (int)dgvProductColor.DataKeys[drCurrent.RowIndex].Values[1], (int)dgvProductColor.DataKeys[drUp.RowIndex].Values[0], (int)dgvProductColor.DataKeys[drUp.RowIndex].Values[1]);
                    LoadProductColorData(false, false);
                    objCommon = null;
                }
            }
            else if (e.CommandName == "Down")
            {
                LinkButton lnkButton = (LinkButton)e.CommandSource;
                GridViewRow drCurrent = (GridViewRow)lnkButton.Parent.Parent;
                if (drCurrent.RowIndex < dgvProductColor.Rows.Count - 1)
                {
                    GridViewRow drUp = dgvProductColor.Rows[drCurrent.RowIndex + 1];
                    objCommon.SetDisplayOrder("tblProductColor", tblProductColor.ColumnNames.AppProductColorID, tblProductColor.ColumnNames.AppDisplayOrder, (int)dgvProductColor.DataKeys[drCurrent.RowIndex].Values[0], (int)dgvProductColor.DataKeys[drCurrent.RowIndex].Values[1], (int)dgvProductColor.DataKeys[drUp.RowIndex].Values[0], (int)dgvProductColor.DataKeys[drUp.RowIndex].Values[1]);
                    LoadProductColorData(false, false);
                    objCommon = null;
                }
            }
            else if (e.CommandName == "IsActive")
            {
                objProductColor = new tblProductColor();

                if (objProductColor.LoadByPrimaryKey(Convert.ToInt32(e.CommandArgument.ToString())))
                {
                    if (!objProductColor.AppIsDefault)
                    {
                        if (objProductColor.AppIsActive == true)
                        {
                            objProductColor.AppIsActive = false;
                        }
                        else if (objProductColor.AppIsActive == false)
                        {
                            objProductColor.AppIsActive = true;
                        }
                        objProductColor.Save();
                        LoadProductColorData(false, false);
                    }
                }
                objProductColor = null;
            }
            else if (e.CommandName == "IsDefault")
            {
                objProductColor = new tblProductColor();

                if (objProductColor.LoadByPrimaryKey(Convert.ToInt32(e.CommandArgument.ToString())))
                {
                    if (objProductColor.AppIsDefault == false)
                    {
                        tblProductColor objTempColor = new tblProductColor();
                        objTempColor.SetProductDefaultColor(hdnPKID.Value, objProductColor.s_AppProductColorID);
                        objTempColor = null;
                        objProductColor.AppIsDefault = true;
                        objProductColor.AppIsActive = true;
                    }
                    objProductColor.Save();
                    LoadProductColorData(false, false);
                }
                objProductColor = null;
            }
            else if (e.CommandName == "Image")
            {
                hdnCurrentProductColorId.Value = e.CommandArgument.ToString();
                LoadProductImageData(true, false);
                mpeProductImage.Show();
            }
            else if (e.CommandName == "SaveImg")
            {
                GridViewRow gvRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                Int32 rowind = gvRow.RowIndex;
                FileUpload FileImg = (FileUpload)gvRow.FindControl("fileUpload");
                if (FileImg.HasFile)
                {
                    HttpFileCollection fileCollection = Request.Files;
                    tblProductColor ObjTemp = new tblProductColor();
                    string strColotName = ObjTemp.GetProductColorName(e.CommandArgument.ToString()).Trim().Replace(" ", "_");
                    ObjTemp = null;

                    string StrFolder = "Uploads/Product/" + hdnPKID.Value + "/";
                    string strImgName = txtProductName.Text.Trim().Replace(" ", "_") + "_" + strColotName;

                    if (!(System.IO.Directory.Exists(Server.MapPath("~/admin/" + StrFolder))))
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/admin/" + StrFolder));
                    }
                    for (int i = 0; i <= fileCollection.Count - 1; i++)
                    {
                        HttpPostedFile uploadfiles = fileCollection[i];
                        string fileName = Path.GetFileName(uploadfiles.FileName);
                        if (fileName != "")
                        {
                            objCommon = new clsCommon();
                            tblProductImage objProductImg = new tblProductImage();
                            objProductImg.AddNew();
                            objProductImg.s_AppProductColorID = e.CommandArgument.ToString();
                            objProductImg.AppDisplayOrder = objCommon.GetNextDisplayOrder("tblProductImage", tblProductImage.ColumnNames.AppDisplayOrder, tblProductImage.ColumnNames.AppProductColorID + "=" + e.CommandArgument.ToString());

                            string strError = "";
                            string Time = Convert.ToString(DateTime.Now.Month) + Convert.ToString(DateTime.Now.Day) + Convert.ToString(DateTime.Now.Year) + Convert.ToString(DateTime.Now.Hour) + Convert.ToString(DateTime.Now.Minute) + Convert.ToString(DateTime.Now.Second) + Convert.ToString(DateTime.Now.Millisecond);
                            //string strPath = objCommon.FileUpload_Images(uploadfiles, strImgName + "_" + Time + "_Thumb", StrFolder, ref strError, 0, objProductImg.s_AppThumbImage, false, 0, 91);
                            string strPath = objCommon.FileUpload_Images(uploadfiles, strImgName + "_" + Time + "_Thumb", StrFolder, ref strError, 0, objProductImg.s_AppThumbImage, false, 0, 100);
                            if (strError == "")
                            {
                                objProductImg.s_AppThumbImage = strPath;
                            }
                            else
                            {
                                DinfoProductColorData.ShowMessage(strError, Enums.MessageType.Error);
                                //   return false;
                            }

                            //strPath = objCommon.FileUpload_Images(uploadfiles, strImgName + "_" + Time + "_Normal", StrFolder, ref strError, 0, objProductImg.s_AppNormalImage, false, 0, 300);
                            strPath = objCommon.FileUpload_Images(uploadfiles, strImgName + "_" + Time + "_Normal", StrFolder, ref strError, 0, objProductImg.s_AppNormalImage, false, 0, 400);
                            if (strError == "")
                            {
                                objProductImg.s_AppNormalImage = strPath;
                            }
                            else
                            {
                                DinfoProductColorData.ShowMessage(strError, Enums.MessageType.Error);
                                //   return false;
                            }

                            strPath = objCommon.FileUpload_Images(uploadfiles, strImgName + "_" + Time + "_Large", StrFolder, ref strError, 0, objProductImg.s_AppLargeImage, false, 0, 3800);
                            if (strError == "")
                            {
                                objProductImg.s_AppLargeImage = strPath;
                            }
                            else
                            {
                                DinfoProductColorData.ShowMessage(strError, Enums.MessageType.Error);
                                // return false;
                            }

                            //strPath = objCommon.FileUpload_Images(uploadfiles, strImgName + "_" + Time + "_Small", StrFolder, ref strError, 0, objProductImg.s_AppSmallImage, false, 0, 210);
                            strPath = objCommon.FileUpload_Images(uploadfiles, strImgName + "_" + Time + "_Small", StrFolder, ref strError, 0, objProductImg.s_AppSmallImage, false, 0, 200);
                            if (strError == "")
                            {
                                objProductImg.s_AppSmallImage = strPath;
                            }
                            else
                            {
                                DinfoProductColorData.ShowMessage(strError, Enums.MessageType.Error);
                                //  return false;
                            }
                            objProductImg.AppIsActive = true;

                            tblProductImage objTempmg = new tblProductImage();
                            objTempmg.Where.AppProductColorID.Value = e.CommandArgument.ToString();
                            objTempmg.Query.Load();
                            if (objTempmg.RowCount <= 0)
                            {
                                objProductImg.AppIsActive = true;
                                objProductImg.AppIsDefault = true;
                            }
                            else
                            {
                                objProductImg.AppIsDefault = false;
                            }
                            objTempmg = null;


                            objProductImg.Save();
                            objProductImg = null;
                            objCommon = null;
                        }
                    }
                }
                LoadProductColorData(true, false);
            }


        }
    }

    private void SetProductColorValuesToControls()
    {
        if (!string.IsNullOrEmpty(hdnProductColorId.Value) && hdnProductColorId.Value != "")
        {
            if (ddlColor.Items.FindByValue(hdnProductColorId.Value) != null)
            {
                ddlColor.SelectedValue = hdnProductColorId.Value;
            }
        }
    }

    protected void btnProductColorDelete_Click(object sender, EventArgs e)
    {
        string[] arIDs = hdnSelectedIDs.Value.ToString().TrimEnd(',').Split(',');
        bool IsDelete = false;

        for (int i = 0; i <= arIDs.Length - 1; i++)
        {
            if (!string.IsNullOrEmpty(arIDs.GetValue(i).ToString()))
            {
                if (DeleteProductColor(Convert.ToInt32(arIDs.GetValue(i))))
                {
                    IsDelete = true;
                }
            }
        }

        if (IsDelete)
        {
            LoadProductColorData(false, false);
            ColorDetailDropDownFill();
        }

        DinfoProductColorData.ShowMessage("Product Color has been deleted successfully", Enums.MessageType.Successfull);
        hdnSelectedIDs.Value = "";
    }

    private bool DeleteProductColor(int intPKID)
    {
        bool retval = false;
        objProductColor = new tblProductColor();
        if (objProductColor.LoadByPrimaryKey(intPKID))
        {
            if (!objProductColor.AppIsDefault)
            {
                objProductColor.MarkAsDeleted();
                objProductColor.Save();
                retval = true;
            }
        }
        objProductColor = null;
        return retval;
    }

    protected void dgvProductColor_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void dgvProductColor_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    //Load Model popupImage

    //public void ResetProductImageControl()
    //{
    //    chkProductImageIsActive.Checked = true;
    //    ChkProductImageIsDefault.Checked = false;
    //}

    //protected void BtnSaveImage_Click(object sender, EventArgs e)
    //{
    //    if (SaveProductImageData())
    //    {
    //        if (string.IsNullOrEmpty(hdnProductColorId.Value))
    //        {
    //            DinfoProductImage.ShowMessage("Product Image has been added successfully", Enums.MessageType.Successfull);
    //        }
    //        ResetProductImageControl();
    //    }
    //    //LoadProductImageData(false, false);
    //}

    //private bool SaveProductImageData()
    //{
    //    string str1 = hdnFilePath.Value;
    //    if (hdnCurrentProductColorId.Value != "" && FileProductImg.HasFile)
    //    {
    //              tblProductColor ObjTemp = new tblProductColor();
    //              string strColotName = ObjTemp.GetProductColorName(e.CommandArgument.ToString()).Trim().Replace(" ", "_");
    //              ObjTemp = null;

    //              string StrFolder = "Uploads/Product/" + hdnPKID.Value + "/";
    //              string strImgName = txtProductName.Text.Trim().Replace(" ", "_") + "_" + strColotName;

    //              if (!(System.IO.Directory.Exists(Server.MapPath("~/admin/" + StrFolder))))
    //              {
    //                  System.IO.Directory.CreateDirectory(Server.MapPath("~/admin/" + StrFolder));
    //              }
    //        HttpFileCollection fileCollection = Request.Files;
    //        for (int i = 0; i <= fileCollection.Count - 1; i++)
    //        {
    //            HttpPostedFile uploadfiles = fileCollection[i];
    //            string fileName = Path.GetFileName(uploadfiles.FileName);
    //            if (fileName != "")
    //            {
    //                objCommon = new clsCommon();
    //                tblProductImage objProductImg = new tblProductImage();
    //                objProductImg.AddNew();
    //                objProductImg.s_AppProductColorID = hdnCurrentProductColorId.Value;
    //                objProductImg.AppDisplayOrder = objCommon.GetNextDisplayOrder("tblProductImage", tblProductImage.ColumnNames.AppDisplayOrder, tblProductImage.ColumnNames.AppProductColorID + "=" + hdnCurrentProductColorId.Value);

    //                string strError = "";
    //                string Time = Convert.ToString(DateTime.Now.Month) + Convert.ToString(DateTime.Now.Day) + Convert.ToString(DateTime.Now.Year) + Convert.ToString(DateTime.Now.Hour) + Convert.ToString(DateTime.Now.Minute) + Convert.ToString(DateTime.Now.Second);
    //                string strPath = objCommon.FileUpload_Images(uploadfiles,strImgName + "_"  Time + "_Thumb", StrFolder, ref strError, 0, objProductImg.s_AppThumbImage);
    //                if (strError == "")
    //                {
    //                    objProductImg.s_AppThumbImage = strPath;
    //                }
    //                else
    //                {
    //                    DinfoProductImage.ShowMessage(strError, Enums.MessageType.Error);
    //                    return false;
    //                }

    //                strPath = objCommon.FileUpload_Images(uploadfiles, strImgName + "_" Time + "_Normal", StrFolder, ref strError, 0, objProductImg.s_AppNormalImage);
    //                if (strError == "")
    //                {
    //                    objProductImg.s_AppNormalImage = strPath;
    //                }
    //                else
    //                {
    //                    DinfoProductImage.ShowMessage(strError, Enums.MessageType.Error);
    //                    return false;
    //                }

    //                strPath = objCommon.FileUpload_Images(uploadfiles, strImgName + "_" Time + "_Large", StrFolder, ref strError, 0, objProductImg.s_AppLargeImage);
    //                if (strError == "")
    //                {
    //                    objProductImg.s_AppLargeImage = strPath;
    //                }
    //                else
    //                {
    //                    DinfoProductImage.ShowMessage(strError, Enums.MessageType.Error);
    //                    return false;
    //                }

    //                strPath = objCommon.FileUpload_Images(uploadfiles, strImgName + "_" Time + "_Small", StrFolder , ref strError, 0, objProductImg.s_AppSmallImage);
    //                if (strError == "")
    //                {
    //                    objProductImg.s_AppSmallImage = strPath;
    //                }
    //                else
    //                {
    //                    DinfoProductImage.ShowMessage(strError, Enums.MessageType.Error);
    //                    return false;
    //                }
    //                objProductImg.AppIsActive = chkProductImageIsActive.Checked;
    //                if (ChkProductImageIsDefault.Checked)
    //                {
    //                    tblProductImage objTempmg = new tblProductImage();
    //                    objTempmg.SetProductDefaultImage(hdnCurrentProductColorId.Value);
    //                    objTempmg = null;
    //                    objProductImg.AppIsActive = true;
    //                    objProductImg.AppIsDefault = true;
    //                }
    //                else
    //                {
    //                    tblProductImage objTempmg = new tblProductImage();
    //                    objTempmg.Where.AppProductColorID.Value = hdnCurrentProductColorId.Value;
    //                    objTempmg.Query.Load();
    //                    if (objTempmg.RowCount <= 0)
    //                    {
    //                        objProductImg.AppIsActive = true;
    //                        objProductImg.AppIsDefault = true;
    //                    }
    //                    else
    //                    {
    //                        objProductImg.AppIsDefault = false;
    //                    }
    //                    objTempmg = null;

    //                }
    //                objProductImg.Save();
    //                objProductImg = null;
    //                objCommon = null;
    //            }
    //        }
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    protected void imgBtnImgClose_Click(object sender, EventArgs e)
    {
        mpeProductImage.Hide();
    }

    public void LoadProductImageData(bool IsResetPageIndex, bool IsSort, string strFieldName = "", string strFieldValue = "")
    {
        tblProductImage objProductImage = new tblProductImage();

        objDataTable = objProductImage.LoadGridData(hdnCurrentProductColorId.Value);

        //'Reset PageIndex of gridviews
        if (IsResetPageIndex)
        {
            if (dgvProductColor.PageCount > 0)
            {
                dgvProductColor.PageIndex = 0;
            }
        }

        dgvProductImage.DataSource = null;
        dgvProductImage.DataBind();
        lblProductImageCount.Text = 0.ToString();
        hdnSelectedIDs.Value = "";

        //'Check for data into datatable
        if (objDataTable.Rows.Count <= 0)
        {
            DInfProductImageData.ShowMessage("No data found", Enums.MessageType.Information);
            return;
        }
        else
        {
            if (ddlProductImagePerPage.SelectedItem.Text.ToLower() == "all")
            {
                dgvProductImage.AllowPaging = false;
            }
            else
            {
                dgvProductImage.AllowPaging = true;
                dgvProductImage.PageSize = Convert.ToInt32(ddlProductImagePerPage.SelectedItem.Text);
            }

            lblProductImageCount.Text = objDataTable.Rows.Count.ToString();
            objDataTable = SortDatatable(objDataTable, ViewState["SortColumn"].ToString(), (appFunctions.Enum_SortOrderBy)ViewState["SortOrder"], IsSort);
            dgvProductImage.DataSource = objDataTable;
            dgvProductImage.DataBind();
        }

        objProductImage = null;
    }

    protected void dgvProductImage_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dgvProductImage.PageIndex = e.NewPageIndex;
        LoadProductImageData(false, false);
    }

    protected void dgvProductImage_RowCreated(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                string strID = dgvProductImage.DataKeys[e.Row.RowIndex].Values[0].ToString();
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSelectRow");
                chk.ID = "chkSelectRow_" + strID;
                break;
        }
    }

    protected void dgvProductImage_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataControlRowType itemType = e.Row.RowType;

        switch (itemType)
        {
            case DataControlRowType.DataRow:
                string strID = dgvProductImage.DataKeys[e.Row.RowIndex].Values[0].ToString();
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSelectRow");

                if ((chk != null))
                {
                    chk.Attributes.Add("OnClick", "javascript:SelectRow(this," + strID + ")");
                }


                break;
        }
    }

    protected void dgvProductImage_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnSelectedIDs.Value = "";
        ViewState["SortColumn"] = e.SortExpression;
        LoadProductImageData(false, true);
    }

    protected void ddlProductImagePerPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnSelectedIDs.Value = "";
        LoadProductImageData(true, false);
    }

    protected void dgvProductImage_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
        {
            objCommon = new clsCommon();
            if (e.CommandName == "Up")
            {
                LinkButton inkButton = (LinkButton)e.CommandSource;
                GridViewRow drCurrent = (GridViewRow)inkButton.Parent.Parent;
                if (drCurrent.RowIndex > 0)
                {
                    GridViewRow drUp = dgvProductImage.Rows[drCurrent.RowIndex - 1];
                    objCommon.SetDisplayOrder("tblProductImage", tblProductImage.ColumnNames.AppProductImageID, tblProductImage.ColumnNames.AppDisplayOrder, (int)dgvProductImage.DataKeys[drCurrent.RowIndex].Values[0], (int)dgvProductImage.DataKeys[drCurrent.RowIndex].Values[1], (int)dgvProductImage.DataKeys[drUp.RowIndex].Values[0], (int)dgvProductImage.DataKeys[drUp.RowIndex].Values[1]);
                    LoadProductImageData(false, false);
                    objCommon = null;
                }
            }
            else if (e.CommandName == "Down")
            {
                LinkButton lnkButton = (LinkButton)e.CommandSource;
                GridViewRow drCurrent = (GridViewRow)lnkButton.Parent.Parent;
                if (drCurrent.RowIndex < dgvProductImage.Rows.Count - 1)
                {
                    GridViewRow drUp = dgvProductImage.Rows[drCurrent.RowIndex + 1];
                    objCommon.SetDisplayOrder("tblProductImage", tblProductImage.ColumnNames.AppProductImageID, tblProductImage.ColumnNames.AppDisplayOrder, (int)dgvProductImage.DataKeys[drCurrent.RowIndex].Values[0], (int)dgvProductImage.DataKeys[drCurrent.RowIndex].Values[1], (int)dgvProductImage.DataKeys[drUp.RowIndex].Values[0], (int)dgvProductImage.DataKeys[drUp.RowIndex].Values[1]);
                    LoadProductImageData(false, false);
                    objCommon = null;
                }
            }
            else if (e.CommandName == "IsActive")
            {
                tblProductImage objProductImage = new tblProductImage();

                if (objProductImage.LoadByPrimaryKey(Convert.ToInt32(e.CommandArgument.ToString())))
                {
                    if (!objProductImage.AppIsDefault)
                    {
                        if (objProductImage.AppIsActive == true)
                        {
                            objProductImage.AppIsActive = false;
                        }
                        else if (objProductImage.AppIsActive == false)
                        {
                            objProductImage.AppIsActive = true;
                        }
                        objProductImage.Save();
                        LoadProductImageData(false, false);
                    }
                }
                objProductImage = null;
            }
            else if (e.CommandName == "IsDefault")
            {
                tblProductImage objProductImage = new tblProductImage();

                if (objProductImage.LoadByPrimaryKey(Convert.ToInt32(e.CommandArgument.ToString())))
                {
                    if (objProductImage.AppIsDefault == false)
                    {
                        tblProductImage objTempColor = new tblProductImage();
                        objTempColor.SetProductDefaultImage(hdnCurrentProductColorId.Value, objProductImage.s_AppProductImageID);
                        objTempColor = null;
                        objProductImage.AppIsDefault = true;
                        objProductImage.AppIsActive = true;
                    }
                    objProductImage.Save();
                    LoadProductImageData(false, false);
                }
                objProductImage = null;
            }
        }
    }

    protected void btnProductImageDelete_Click(object sender, EventArgs e)
    {
        string[] arIDs = hdnSelectedIDs.Value.ToString().TrimEnd(',').Split(',');
        bool IsDelete = false;

        for (int i = 0; i <= arIDs.Length - 1; i++)
        {
            if (!string.IsNullOrEmpty(arIDs.GetValue(i).ToString()))
            {
                if (DeleteProductImage(Convert.ToInt32(arIDs.GetValue(i))))
                {
                    IsDelete = true;
                }
            }
        }

        if (IsDelete)
        {
            LoadProductImageData(false, false);
        }

        DInfProductImageData.ShowMessage("Product Image has been deleted successfully", Enums.MessageType.Successfull);
        hdnSelectedIDs.Value = "";
    }

    private bool DeleteProductImage(int intPKID)
    {
        bool retval = false;
        tblProductImage objProductImage = new tblProductImage();
        if (objProductImage.LoadByPrimaryKey(intPKID))
        {
            if (!objProductImage.AppIsDefault)
            {
                if (System.IO.File.Exists(Server.MapPath(objProductImage.s_AppThumbImage)))
                {
                    System.IO.File.Delete(Server.MapPath(objProductImage.s_AppThumbImage));
                }
                if (System.IO.File.Exists(Server.MapPath(objProductImage.s_AppNormalImage)))
                {
                    System.IO.File.Delete(Server.MapPath(objProductImage.s_AppNormalImage));
                }
                if (System.IO.File.Exists(Server.MapPath(objProductImage.s_AppSmallImage)))
                {
                    System.IO.File.Delete(Server.MapPath(objProductImage.s_AppSmallImage));
                }
                if (System.IO.File.Exists(Server.MapPath(objProductImage.s_AppLargeImage)))
                {
                    System.IO.File.Delete(Server.MapPath(objProductImage.s_AppLargeImage));
                }
                objProductImage.MarkAsDeleted();
                objProductImage.Save();
                retval = true;
            }
        }
        objProductImage = null;
        return retval;
    }

    protected void dgvProductImage_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void dgvProductImage_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    public void LoadProductPropertyData()
    {
        tblProductProperty objProductProperty = new tblProductProperty();

        objDataTable = objProductProperty.LoadGridData(hdnPKID.Value);

        dgvProductProperty.DataSource = null;
        dgvProductProperty.DataBind();


        //'Check for data into datatable
        if (objDataTable.Rows.Count <= 0)
        {
            DInfoProductProperty.ShowMessage("No data found.Please add subCategory of perticular property in Property Tab.   ", Enums.MessageType.Information);
            return;
        }
        else
        {
            dgvProductProperty.DataSource = objDataTable;
            dgvProductProperty.DataBind();
        }

        objProductProperty = null;
    }

    protected void dgvProductProperty_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataControlRowType itemType = e.Row.RowType;

        switch (itemType)
        {
            case DataControlRowType.DataRow:
                bool IsPreDefine = Convert.ToBoolean(dgvProductProperty.DataKeys[e.Row.RowIndex].Values[1].ToString());
                string strPropertyId = dgvProductProperty.DataKeys[e.Row.RowIndex].Values[0].ToString();
                string strId = dgvProductProperty.DataKeys[e.Row.RowIndex].Values[2].ToString();
                TextBox txt = (TextBox)e.Row.FindControl("txtValue");
                DropDownList ddl = (DropDownList)e.Row.FindControl("ddlValue");
                if (IsPreDefine)
                {
                    txt.Visible = false;
                    objCommon = new clsCommon();
                    objCommon.FillDropDownList(ddl, "tblPropertyPreValue ", tblPropertyPreValue.ColumnNames.AppPreValue, tblPropertyPreValue.ColumnNames.AppPropertyPreValueID, "--Select Value--", tblPropertyPreValue.ColumnNames.AppDisplayOrder, appFunctions.Enum_SortOrderBy.Asc, tblPropertyPreValue.ColumnNames.AppPropertyID + "=" + strPropertyId + " and " + tblPropertyPreValue.ColumnNames.AppIsActive + "=1");
                    objCommon = null;

                    if (strId != "")
                    {
                        ddl.SelectedValue = strId;
                    }

                }
                else
                {
                    ddl.Visible = false;
                }

                break;
        }
    }

    protected void btnSaveProperty_Click(object sender, EventArgs e)
    {
        tblProductProperty objProductProperty;
        foreach (GridViewRow row in dgvProductProperty.Rows)
        {
            bool IsPreDefine = Convert.ToBoolean(dgvProductProperty.DataKeys[row.RowIndex].Values[1].ToString());
            string strPropertyId = dgvProductProperty.DataKeys[row.RowIndex].Values[0].ToString();
            string strId = dgvProductProperty.DataKeys[row.RowIndex].Values[2].ToString();
            TextBox txt = (TextBox)row.FindControl("txtValue");
            DropDownList ddl = (DropDownList)row.FindControl("ddlValue");

            objProductProperty = new tblProductProperty();
            objProductProperty.Where.AppProductID.Value = hdnPKID.Value;
            objProductProperty.Where.AppPropertyID.Value = strPropertyId;
            objProductProperty.Query.Load();
            if (objProductProperty.RowCount > 0)
            {

                if (IsPreDefine)
                {
                    if (ddl.SelectedIndex > 0)
                    {
                        objProductProperty.s_AppPropertyPreValueID = ddl.SelectedValue;
                    }
                    else
                    {
                        objProductProperty.MarkAsDeleted();

                    }
                }
                else
                {
                    if (txt.Text != "")
                    {
                        objProductProperty.s_AppValue = txt.Text;
                    }
                    else
                    {
                        objProductProperty.MarkAsDeleted();
                    }

                }
                objProductProperty.Save();

            }
            else
            {
                objProductProperty.AddNew();
                objProductProperty.s_AppProductID = hdnPKID.Value;
                objProductProperty.s_AppPropertyID = strPropertyId;
                if (IsPreDefine)
                {
                    if (ddl.SelectedIndex > 0)
                    {
                        objProductProperty.s_AppPropertyPreValueID = ddl.SelectedValue;
                        objProductProperty.Save();
                    }
                }
                else
                {
                    if (txt.Text != "")
                    {
                        objProductProperty.s_AppValue = txt.Text;
                        objProductProperty.Save();
                    }
                }

            }
            objProductProperty = null;
        }
    }

    // For Color Details

    public void LoadColorDetali()
    {
        ResetColorDetail();
        LoadColorDetail(true, false);
    }

    public void ResetColorDetail()
    {
        //if (chkIsColor.Checked)
        //{
        //    ddlColorDetail.SelectedIndex = 0;
        //}
        if (chkIsSize.Checked)
        {
            ddlSize.SelectedIndex = 0;
        }
        //txtSellerPrice.Text = "";
        txtMRP.Text = "";
        txtPrice.Text = "";
        txtQuantity.Text = "";
        txtSKUNo.Text = "";
        ChkProductDetailIsDefault.Checked = false;
        hdnProductDetailID.Value = "";
    }

    protected void btnSaveColorDetail_Click(object sender, EventArgs e)
    {
        if (SaveColorDetailData())
        {
            if (string.IsNullOrEmpty(hdnProductDetailID.Value))
            {
                DInfoColorDetail.ShowMessage("Product Detail has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfoColorDetail.ShowMessage("Product Detail  has been updated successfully", Enums.MessageType.Successfull);
            }
            ResetColorDetail();
            LoadColorDetail(false, false);

        }
    }

    protected void btnClearColorDetail_Click(object sender, EventArgs e)
    {
        ResetColorDetail();
    }

    private bool SaveColorDetailData()
    {
        if (hdnPKID.Value != "")
        {
            objCommon = new clsCommon();
            if (objCommon.IsRecordExists("tblProductDetail", tblProductDetail.ColumnNames.AppSizeID, tblProductDetail.ColumnNames.AppProductDetailID, ddlSize.SelectedValue, hdnProductDetailID.Value, tblProductDetail.ColumnNames.AppProductColorID + "=" + ddlColorDetail.SelectedValue))
            {
                DInfoColorDetail.ShowMessage("Product Color in size already exits.", Enums.MessageType.Error);
                return false;
            }
            if (objCommon.IsRecordExists("tblProductDetail", tblProductDetail.ColumnNames.AppSKUNo, tblProductDetail.ColumnNames.AppProductDetailID, txtSKUNo.Text, hdnProductDetailID.Value))
            {
                DInfoColorDetail.ShowMessage("Product SKU no. already exists.", Enums.MessageType.Error);
                return false;
            }
            objProductDetail = new tblProductDetail();

            if (!string.IsNullOrEmpty(hdnProductDetailID.Value) && hdnProductDetailID.Value != "")
            {
                objProductDetail.LoadByPrimaryKey(Convert.ToInt32(hdnProductDetailID.Value));
            }
            else
            {
                objProductDetail.AddNew();
            }
            objProductDetail.s_AppProductColorID = ddlColorDetail.SelectedValue;
            // objProductDetail.s_AppSellerPrice = txtSellerPrice.Text;
            objProductDetail.s_AppMRP = txtMRP.Text;
            objProductDetail.s_AppPrice = txtPrice.Text;
            objProductDetail.s_AppQuantity = txtQuantity.Text;
            objProductDetail.AppSKUNo = txtSKUNo.Text;
            objProductDetail.s_AppSizeID = ddlSize.SelectedValue;
            if (ChkProductDetailIsDefault.Checked)
            {
                tblProductDetail objTempmg = new tblProductDetail();
                objTempmg.SetProductDetailDefault(ddlColorDetail.SelectedValue);
                objTempmg = null;
                objProductDetail.AppIsDefault = true;
            }
            else
            {
                tblProductDetail objTempmg = new tblProductDetail();
                objTempmg.Where.AppProductColorID.Value = ddlColorDetail.SelectedValue;
                objTempmg.Query.Load();
                if (objTempmg.RowCount <= 0)
                {

                    objProductDetail.AppIsDefault = true;
                }
                else
                {
                    objProductDetail.AppIsDefault = false;
                }
                objTempmg = null;
            }
            objProductDetail.Save();
            objProductDetail = null;
            objCommon = null;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void LoadColorDetail(bool IsResetPageIndex, bool IsSort)
    {
        objProductDetail = new tblProductDetail();
        //objDataTable = objProductDetail.LoadGridData(hdnPKID.Value, ddlSerachColorDetail.SelectedValue);
        objDataTable = objProductDetail.LoadGridData(hdnPKID.Value, "0");
        //'Reset PageIndex of gridviews
        if (IsResetPageIndex)
        {
            if (dgvColorDetail.PageCount > 0)
            {
                dgvColorDetail.PageIndex = 0;
            }
        }

        dgvColorDetail.DataSource = null;
        dgvColorDetail.DataBind();
        lblColorDetailCount.Text = 0.ToString();
        hdnSelectedIDs.Value = "";

        //'Check for data into datatable
        if (objDataTable.Rows.Count <= 0)
        {
            DInfoColorDetailData.ShowMessage("No data found", Enums.MessageType.Information);
            return;
        }
        else
        {
            if (ddlColorDetailPerPage.SelectedItem.Text.ToLower() == "all")
            {
                dgvColorDetail.AllowPaging = false;
            }
            else
            {
                dgvColorDetail.AllowPaging = true;
                dgvColorDetail.PageSize = Convert.ToInt32(ddlColorDetailPerPage.SelectedItem.Text);
            }

            lblColorDetailCount.Text = objDataTable.Rows.Count.ToString();
            objDataTable = SortDatatable(objDataTable, ViewState["SortColumn"].ToString(), (appFunctions.Enum_SortOrderBy)ViewState["SortOrder"], IsSort);
            dgvColorDetail.DataSource = objDataTable;
            dgvColorDetail.DataBind();
        }

        objProductDetail = null;
    }

    protected void dgvColorDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dgvColorDetail.PageIndex = e.NewPageIndex;
        LoadColorDetail(false, false);
    }

    protected void dgvColorDetail_RowCreated(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                string strID = dgvColorDetail.DataKeys[e.Row.RowIndex].Values[0].ToString();
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSelectRow");
                chk.ID = "chkSelectRow_" + strID;
                break;
        }
    }

    protected void dgvColorDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataControlRowType itemType = e.Row.RowType;

        switch (itemType)
        {
            case DataControlRowType.DataRow:
                string strID = dgvColorDetail.DataKeys[e.Row.RowIndex].Values[0].ToString();
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSelectRow");
                if ((chk != null))
                {
                    chk.Attributes.Add("OnClick", "javascript:SelectRow(this," + strID + ")");
                }
                break;
        }
    }

    protected void dgvColorDetail_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnSelectedIDs.Value = "";
        ViewState["SortColumn"] = e.SortExpression;
        LoadColorDetail(false, true);
    }

    protected void btnColorDetailReset_Click(object sender, System.EventArgs e)
    {
        //ddlSerachColorDetail.SelectedIndex = 0;
        LoadColorDetail(true, false);
    }

    protected void ddlColorDetailPerPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnSelectedIDs.Value = "";
        LoadColorDetail(false, false);
    }

    protected void ddlSerachColorDetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadColorDetail(false, false);
    }

    protected void dgvColorDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
        {
            objCommon = new clsCommon();
            if (e.CommandName == "Edit")
            {
                hdnProductDetailID.Value = e.CommandArgument.ToString();
                SetColorDetailData();
            }
            else if (e.CommandName == "IsDefault")
            {
                objProductDetail = new tblProductDetail();

                if (objProductDetail.LoadByPrimaryKey(Convert.ToInt32(e.CommandArgument.ToString())))
                {
                    if (objProductDetail.AppIsDefault == false)
                    {
                        tblProductDetail objTempColor = new tblProductDetail();
                        objTempColor.SetProductDetailDefault(objProductDetail.s_AppProductColorID, objProductDetail.s_AppProductDetailID);
                        objTempColor = null;

                    }

                    LoadColorDetail(false, false);
                }
                objProductDetail = null;
            }

        }
    }

    private void SetColorDetailData()
    {
        if (!string.IsNullOrEmpty(hdnProductDetailID.Value) && hdnProductDetailID.Value != "")
        {
            objProductDetail = new tblProductDetail();
            if (objProductDetail.LoadByPrimaryKey(Convert.ToInt32(hdnProductDetailID.Value)))
            {
                ddlColorDetail.SelectedValue = objProductDetail.s_AppProductColorID;
                // txtSellerPrice.Text = objProductDetail.s_AppSellerPrice;
                txtMRP.Text = objProductDetail.s_AppMRP;
                txtPrice.Text = objProductDetail.s_AppPrice;
                txtQuantity.Text = objProductDetail.s_AppQuantity;
                txtSKUNo.Text = objProductDetail.s_AppSKUNo;
                ddlSize.SelectedValue = objProductDetail.s_AppSizeID;
                ChkProductDetailIsDefault.Checked = objProductDetail.AppIsDefault;
            }
            objProductDetail = null;
        }
    }

    protected void btnColorDetailDelete_Click(object sender, EventArgs e)
    {
        string[] arIDs = hdnSelectedIDs.Value.ToString().TrimEnd(',').Split(',');
        bool IsDelete = false;

        for (int i = 0; i <= arIDs.Length - 1; i++)
        {
            if (!string.IsNullOrEmpty(arIDs.GetValue(i).ToString()))
            {
                if (DeleteColorDetail(Convert.ToInt32(arIDs.GetValue(i))))
                {
                    IsDelete = true;
                }
            }
        }

        if (IsDelete)
        {
            LoadColorDetail(false, false);

        }
        DInfoColorDetailData.ShowMessage("Product Detail has been deleted successfully", Enums.MessageType.Successfull);
        hdnSelectedIDs.Value = "";
    }

    private bool DeleteColorDetail(int intPKID)
    {
        bool retval = false;
        objProductDetail = new tblProductDetail();
        var _with1 = objProductDetail;
        if (_with1.LoadByPrimaryKey(intPKID))
        {
            _with1.MarkAsDeleted();
            _with1.Save();
        }
        retval = true;
        objProductDetail = null;
        return retval;
    }

    protected void dgvColorDetail_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void dgvColorDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    public void SetRegulerExpression()
    {
        REVWeight.ValidationExpression = RXDecimalRegularExpression;
        REVWeight.ErrorMessage = " Invalid Weight (" + RXDecimalRegularExpressionMsg + ")";

        //   REVSellerPrice.ValidationExpression = RXDecimalRegularExpression;
        //   REVSellerPrice.ErrorMessage = " Invalid Seller Price (" + RXDecimalRegularExpressionMsg + ")";

        REVMRP.ValidationExpression = RXDecimalRegularExpression;
        REVMRP.ErrorMessage = " Invalid MRP (" + RXDecimalRegularExpressionMsg + ")";

        REVPrice.ValidationExpression = RXDecimalRegularExpression;
        REVPrice.ErrorMessage = " Invalid Price (" + RXDecimalRegularExpressionMsg + ")";

        REVQuantity.ValidationExpression = RXNumericRegularExpression;
        REVQuantity.ErrorMessage = " Invalid Quantity (" + RXNumericRegularExpressionMsg + ")";
    }

    //Related Product

    public void LoadAllRelatedProduct()
    {
        LoadUnselectedProduct();
        LoadRelatedProductData(true, false);
    }
    public void LoadAllSubCategories()
    {
        LoadUnselectedCategories();

    }
    public void LoadUnselectedCategories()
    {
        if (ddlCategory.SelectedIndex > 0)
        {
            objProductSubCategory = new tblProductSubCategory();

            objDataTable = objProductSubCategory.LoadUnSelectedCategories(hdnPKID.Value, ddlCategory.SelectedValue);

            dgvUnSelectedCategories.DataSource = null;
            dgvUnSelectedCategories.DataBind();
            //'Check for data into datatable
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

            objProductSubCategory = null;
        }
        else
        {
            dgvUnSelectedCategories.DataSource = null;
            dgvUnSelectedCategories.DataBind();
        }
    }
    public void LoadUnselectedProduct()
    {
        if (ddlRelatedCategory.SelectedIndex > 0)
        {
            objRelatedProduct = new tblRelatedProduct();

            objDataTable = objRelatedProduct.LoadUnSelectedProduct(hdnPKID.Value, ddlRelatedCategory.SelectedValue, ddlRelatedSubCategory.SelectedValue);

            dgvUnSelected.DataSource = null;
            dgvUnSelected.DataBind();
            //'Check for data into datatable
            if (objDataTable.Rows.Count <= 0)
            {
                DInfoUnSelected.ShowMessage("No data found", Enums.MessageType.Information);
                return;
            }
            else
            {
                dgvUnSelected.AllowPaging = false;
                dgvUnSelected.DataSource = objDataTable;
                dgvUnSelected.DataBind();
            }

            objRelatedProduct = null;
        }
        else
        {
            dgvUnSelected.DataSource = null;
            dgvUnSelected.DataBind();
        }
    }

    protected void dgvUnSelected_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
        {
            objCommon = new clsCommon();
            if (e.CommandName == "IsAdd")
            {
                objCommon = new clsCommon();
                if (objCommon.IsRecordExists("tblRelatedProduct", tblRelatedProduct.ColumnNames.AppRelatedID, tblRelatedProduct.ColumnNames.AppRelatedProductID, e.CommandArgument.ToString(), "", tblRelatedProduct.ColumnNames.AppProductID + "=" + hdnPKID.Value))
                {
                    DRelatedInfo.ShowMessage("Product alredy exits.", Enums.MessageType.Error);
                }
                else
                {
                    objRelatedProduct = new tblRelatedProduct();
                    objRelatedProduct.AddNew();
                    objRelatedProduct.AppDisplayOrder = objCommon.GetNextDisplayOrder("tblRelatedProduct", tblRelatedProduct.ColumnNames.AppDisplayOrder, tblRelatedProduct.ColumnNames.AppProductID + "=" + hdnPKID.Value);
                    objRelatedProduct.s_AppProductID = hdnPKID.Value;
                    objRelatedProduct.s_AppRelatedID = e.CommandArgument.ToString();
                    objRelatedProduct.AppIsActive = true;
                    objRelatedProduct.Save();
                    objRelatedProduct = null;
                    LoadAllRelatedProduct();
                }
                objCommon = null;

            }
        }
    }

    protected void dgvUnSelectedCategories_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
        {
            objCommon = new clsCommon();
            if (e.CommandName == "IsAdd")
            {
                if (objCommon.IsRecordExists("tblProductSubCategory", tblProductSubCategory.ColumnNames.AppSubCategoryID, tblProductSubCategory.ColumnNames.AppProductSubCategoryID, e.CommandArgument.ToString(), "", tblProductSubCategory.ColumnNames.AppProductID + "=" + hdnPKID.Value))
                {
                    DRelatedInfo.ShowMessage("Sub Category alredy exits.", Enums.MessageType.Error);
                }
                else
                {


                    objProductSubCategory = new tblProductSubCategory();
                    objProductSubCategory.AddNew();
                    objProductSubCategory.AppDisplayOrder = objCommon.GetNextDisplayOrder("tblProductSubCategory", tblProductSubCategory.ColumnNames.AppDisplayOrder, tblProductSubCategory.ColumnNames.AppProductID + "=" + hdnPKID.Value);
                    objProductSubCategory.s_AppProductID = hdnPKID.Value;
                    objProductSubCategory.s_AppSubCategoryID = e.CommandArgument.ToString();
                    objProductSubCategory.AppIsActive = true;
                    objProductSubCategory.Save();
                    objProductSubCategory = null;
                    //LoadUnselectedCategories();
                    LoadAllSubCategories();
                    LoadSubCategoryData(false, false);
                    objCommon = null;
                }
            }
        }
    }

    protected void ddlRelatedCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon = new clsCommon();
        objCommon.FillDropDownList(ddlRelatedSubCategory, "tblSubCategory ", tblSubCategory.ColumnNames.AppSubCategory, tblSubCategory.ColumnNames.AppSubCategoryID, "--Select Sub Category--", tblSubCategory.ColumnNames.AppSubCategory, appFunctions.Enum_SortOrderBy.Asc, tblSubCategory.ColumnNames.AppCategoryID + "=" + ddlRelatedCategory.SelectedValue);
        objCommon = null;
        LoadUnselectedProduct();
    }

    protected void ddlRelatedSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadUnselectedProduct();
    }

    public void LoadRelatedProductData(bool IsResetPageIndex, bool IsSort, string strFieldName = "", string strFieldValue = "")
    {
        objRelatedProduct = new tblRelatedProduct();

        objDataTable = objRelatedProduct.LoadSelectedProduct(hdnPKID.Value);

        //'Reset PageIndex of gridviews
        if (IsResetPageIndex)
        {
            if (dgvRelatedProduct.PageCount > 0)
            {
                dgvRelatedProduct.PageIndex = 0;
            }
        }

        dgvRelatedProduct.DataSource = null;
        dgvRelatedProduct.DataBind();
        lblRelatedCount.Text = 0.ToString();
        hdnSelectedIDs.Value = "";

        //'Check for data into datatable
        if (objDataTable.Rows.Count <= 0)
        {
            DInfoRelatedProduct.ShowMessage("No data found", Enums.MessageType.Information);
            return;
        }
        else
        {
            if (ddlRelatedPerPage.SelectedItem.Text.ToLower() == "all")
            {
                dgvRelatedProduct.AllowPaging = false;
            }
            else
            {
                dgvRelatedProduct.AllowPaging = true;
                dgvRelatedProduct.PageSize = Convert.ToInt32(ddlRelatedPerPage.SelectedItem.Text);
            }

            lblRelatedCount.Text = objDataTable.Rows.Count.ToString();
            objDataTable = SortDatatable(objDataTable, ViewState["SortColumn"].ToString(), (appFunctions.Enum_SortOrderBy)ViewState["SortOrder"], IsSort);
            dgvRelatedProduct.DataSource = objDataTable;
            dgvRelatedProduct.DataBind();
        }

        objRelatedProduct = null;
    }

    protected void ddlRelatedPerPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnSelectedIDs.Value = "";
        LoadRelatedProductData(false, false);
    }

    protected void dgvRelatedProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dgvRelatedProduct.PageIndex = e.NewPageIndex;
        LoadRelatedProductData(false, false);
    }

    protected void dgvRelatedProduct_RowCreated(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                string strID = dgvRelatedProduct.DataKeys[e.Row.RowIndex].Values[0].ToString();
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSelectRow");
                chk.ID = "chkSelectRow_" + strID;
                break;
        }
    }

    protected void dgvRelatedProduct_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataControlRowType itemType = e.Row.RowType;

        switch (itemType)
        {
            case DataControlRowType.DataRow:
                string strID = dgvRelatedProduct.DataKeys[e.Row.RowIndex].Values[0].ToString();
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSelectRow");
                if ((chk != null))
                {
                    chk.Attributes.Add("OnClick", "javascript:SelectRow(this," + strID + ")");
                }
                break;
        }
    }

    protected void dgvRelatedProduct_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnSelectedIDs.Value = "";
        ViewState["SortColumn"] = e.SortExpression;
        LoadRelatedProductData(false, true);
    }

    protected void dgvRelatedProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnSelectedIDs.Value = "";
        LoadRelatedProductData(true, false);
    }

    protected void dgvRelatedProduct_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
        {
            objCommon = new clsCommon();
            if (e.CommandName == "Up")
            {
                LinkButton inkButton = (LinkButton)e.CommandSource;
                GridViewRow drCurrent = (GridViewRow)inkButton.Parent.Parent;
                if (drCurrent.RowIndex > 0)
                {
                    GridViewRow drUp = dgvRelatedProduct.Rows[drCurrent.RowIndex - 1];
                    objCommon.SetDisplayOrder("tblRelatedProduct", tblRelatedProduct.ColumnNames.AppRelatedProductID, tblRelatedProduct.ColumnNames.AppDisplayOrder, (int)dgvRelatedProduct.DataKeys[drCurrent.RowIndex].Values[0], (int)dgvRelatedProduct.DataKeys[drCurrent.RowIndex].Values[1], (int)dgvRelatedProduct.DataKeys[drUp.RowIndex].Values[0], (int)dgvRelatedProduct.DataKeys[drUp.RowIndex].Values[1]);
                    LoadRelatedProductData(false, false);
                    objCommon = null;
                }
            }
            else if (e.CommandName == "Down")
            {
                LinkButton lnkButton = (LinkButton)e.CommandSource;
                GridViewRow drCurrent = (GridViewRow)lnkButton.Parent.Parent;
                if (drCurrent.RowIndex < dgvRelatedProduct.Rows.Count - 1)
                {
                    GridViewRow drUp = dgvRelatedProduct.Rows[drCurrent.RowIndex + 1];
                    objCommon.SetDisplayOrder("tblRelatedProduct", tblRelatedProduct.ColumnNames.AppRelatedProductID, tblRelatedProduct.ColumnNames.AppDisplayOrder, (int)dgvRelatedProduct.DataKeys[drCurrent.RowIndex].Values[0], (int)dgvRelatedProduct.DataKeys[drCurrent.RowIndex].Values[1], (int)dgvRelatedProduct.DataKeys[drUp.RowIndex].Values[0], (int)dgvRelatedProduct.DataKeys[drUp.RowIndex].Values[1]);
                    LoadRelatedProductData(false, false);
                    objCommon = null;
                }
            }
            else if (e.CommandName == "IsActive")
            {
                objRelatedProduct = new tblRelatedProduct();

                if (objRelatedProduct.LoadByPrimaryKey(Convert.ToInt32(e.CommandArgument.ToString())))
                {

                    if (objRelatedProduct.AppIsActive == true)
                    {
                        objRelatedProduct.AppIsActive = false;
                    }
                    else if (objRelatedProduct.AppIsActive == false)
                    {
                        objRelatedProduct.AppIsActive = true;
                    }
                    objRelatedProduct.Save();
                    LoadRelatedProductData(false, false);
                }
                objRelatedProduct = null;
            }

        }
    }

    protected void btnRelatedDelete_Click(object sender, EventArgs e)
    {
        string[] arIDs = hdnSelectedIDs.Value.ToString().TrimEnd(',').Split(',');
        bool IsDelete = false;

        for (int i = 0; i <= arIDs.Length - 1; i++)
        {
            if (!string.IsNullOrEmpty(arIDs.GetValue(i).ToString()))
            {
                if (DeleteRelatedProduct(Convert.ToInt32(arIDs.GetValue(i))))
                {
                    IsDelete = true;
                }
            }
        }

        if (IsDelete)
        {
            LoadAllRelatedProduct();

        }
        DInfoRelatedProduct.ShowMessage("Related Product has been deleted successfully", Enums.MessageType.Successfull);
        hdnSelectedIDs.Value = "";
    }

    private bool DeleteRelatedProduct(int intPKID)
    {
        bool retval = false;
        objRelatedProduct = new tblRelatedProduct();
        var _with1 = objRelatedProduct;
        if (_with1.LoadByPrimaryKey(intPKID))
        {
            _with1.MarkAsDeleted();
            _with1.Save();
        }
        retval = true;
        objRelatedProduct = null;
        return retval;
    }

    protected void dgvRelatedProduct_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void dgvRelatedProduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void btnClearPixelCode_Click(object sender, EventArgs e)
    {
        ResetPixelCodeForm();
    }
    public void ResetPixelCodeForm()
    {
        hdnPixcelCodeID.Value = "";
        txtName.Text = "";
        txtComment.Text = "";
        chkPixelIsActive.Checked = true;
        LoadPixelCode(true, false);

    }
    protected void btnSavePixelCode_Click(object sender, EventArgs e)
    {
        if (SavePixelCode())
        {
            if (string.IsNullOrEmpty(hdnPixcelCodeID.Value))
            {
                DInfoPixelCode.ShowMessage("Pixel Code has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfoPixelCode.ShowMessage("Pixel Code has been updated successfully", Enums.MessageType.Successfull);
            }
            ResetPixelCodeForm();
            LoadAllData();
        }
    }
    public bool SavePixelCode()
    {
        tblPixcelCode objPixcelCode = new tblPixcelCode();
        objCommon = new clsCommon();
        if (objCommon.IsRecordExists("tblPixcelCode", tblPixcelCode.ColumnNames.AppName, tblPixcelCode.ColumnNames.AppPixcelCodeID, txtName.Text, hdnPixcelCodeID.Value, tblPixcelCode.ColumnNames.AppProductId + "=" + hdnPKID.Value))
        {
            DInfoPixelCode.ShowMessage("Pixel Code alredy exits.", Enums.MessageType.Error);
            return false;
        }
        objCommon = null;
        if (!string.IsNullOrEmpty(hdnPixcelCodeID.Value) && hdnPixcelCodeID.Value != "")
        {
            objPixcelCode.LoadByPrimaryKey(Convert.ToInt32(hdnPixcelCodeID.Value));
        }
        else
        {

            objPixcelCode.AddNew();
        }

        objPixcelCode.s_AppProductId = hdnPKID.Value;
        objPixcelCode.AppName = txtName.Text;
        objPixcelCode.AppComment = txtComment.Text;
        objPixcelCode.AppIsActive = chkPixelIsActive.Checked;
        objPixcelCode.AppCreatedDate = GetDateTime();
        objPixcelCode.Save();

        objPixcelCode = null;
        return true;
    }
    public void LoadPixelCode(bool IsResetPageIndex, bool IsSort, string strFieldName = "", string strFieldValue = "")
    {

        tblPixcelCode objPixcelCode = new tblPixcelCode();
        objDataTable = objPixcelCode.LoadPixelCode(hdnPKID.Value);

        //'Reset PageIndex of gridviews
        if (IsResetPageIndex)
        {
            if (dgvPixelCode.PageCount > 0)
            {
                dgvPixelCode.PageIndex = 0;
            }
        }

        dgvPixelCode.DataSource = null;
        dgvPixelCode.DataBind();
        lblPixelCode.Text = 0.ToString();
        hdnSelectedIDs.Value = "";

        //'Check for data into datatable
        if (objDataTable.Rows.Count <= 0)
        {
            DInfoPixelCodeGrid.ShowMessage("No data found", Enums.MessageType.Information);
            return;
        }
        else
        {
            if (ddlPixelCode.SelectedItem.Text.ToLower() == "all")
            {
                dgvPixelCode.AllowPaging = false;
            }
            else
            {
                dgvPixelCode.AllowPaging = true;
                dgvPixelCode.PageSize = Convert.ToInt32(ddlRelatedPerPage.SelectedItem.Text);
            }

            lblPixelCode.Text = objDataTable.Rows.Count.ToString();
            objDataTable = SortDatatable(objDataTable, ViewState["SortColumn"].ToString(), (appFunctions.Enum_SortOrderBy)ViewState["SortOrder"], IsSort);
            dgvPixelCode.DataSource = objDataTable;
            dgvPixelCode.DataBind();
        }

        objPixcelCode = null;
    }

    protected void ddlPixelCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnSelectedIDs.Value = "";
        LoadPixelCode(false, false);
    }

    protected void dgvPixelCode_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dgvPixelCode.PageIndex = e.NewPageIndex;
        LoadPixelCode(false, false);
    }

    protected void dgvPixelCode_RowCreated(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                string strID = dgvPixelCode.DataKeys[e.Row.RowIndex].Values[0].ToString();
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSelectRow");
                chk.ID = "chkSelectRow_" + strID;
                break;
        }
    }

    protected void dgvPixelCode_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataControlRowType itemType = e.Row.RowType;

        switch (itemType)
        {
            case DataControlRowType.DataRow:
                string strID = dgvPixelCode.DataKeys[e.Row.RowIndex].Values[0].ToString();
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSelectRow");
                if ((chk != null))
                {
                    chk.Attributes.Add("OnClick", "javascript:SelectRow(this," + strID + ")");
                }
                break;
        }
    }

    protected void dgvPixelCode_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnSelectedIDs.Value = "";
        ViewState["SortColumn"] = e.SortExpression;
        LoadRelatedProductData(false, true);
    }

    protected void dgvPixelCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnSelectedIDs.Value = "";
        LoadRelatedProductData(true, false);
    }

    protected void dgvPixelCode_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
        {
            objCommon = new clsCommon();
            if (e.CommandName == "Edit")
            {
                hdnPixcelCodeID.Value = e.CommandArgument.ToString();
                SetPixelCodeValuesToControls();
            }
            if (e.CommandName == "IsActive")
            {
                tblPixcelCode objPixcelCode = new tblPixcelCode();

                if (objPixcelCode.LoadByPrimaryKey(Convert.ToInt32(e.CommandArgument.ToString())))
                {

                    if (objPixcelCode.AppIsActive == true)
                    {
                        objPixcelCode.AppIsActive = false;
                    }
                    else if (objPixcelCode.AppIsActive == false)
                    {
                        objPixcelCode.AppIsActive = true;
                    }
                    objPixcelCode.Save();
                    LoadPixelCode(false, false);
                }
                objPixcelCode = null;
            }

        }
    }
    private void SetPixelCodeValuesToControls()
    {
        if (!string.IsNullOrEmpty(hdnPixcelCodeID.Value) && hdnPixcelCodeID.Value != "")
        {
            tblPixcelCode objPixcelCode = new tblPixcelCode();
            if (objPixcelCode.LoadByPrimaryKey(Convert.ToInt32(hdnPixcelCodeID.Value)))
            {
                txtName.Text = objPixcelCode.AppName;
                txtComment.Text = objPixcelCode.AppComment;
                chkPixelIsActive.Checked = objPixcelCode.AppIsActive;
            }
            objPixcelCode = null;
        }
    }
    protected void btnPixelCodeDelete_Click(object sender, EventArgs e)
    {
        string[] arIDs = hdnSelectedIDs.Value.ToString().TrimEnd(',').Split(',');
        bool IsDelete = false;

        for (int i = 0; i <= arIDs.Length - 1; i++)
        {
            if (!string.IsNullOrEmpty(arIDs.GetValue(i).ToString()))
            {
                if (DeletePixelCode(Convert.ToInt32(arIDs.GetValue(i))))
                {
                    IsDelete = true;
                }
            }
        }

        if (IsDelete)
        {
            LoadPixelCode(true, false);

        }
        DInfoPixelCode.ShowMessage("Pixel Code has been deleted successfully", Enums.MessageType.Successfull);
        hdnSelectedIDs.Value = "";
    }

    private bool DeletePixelCode(int intPKID)
    {
        bool retval = false;
        tblPixcelCode objPixcelCode = new tblPixcelCode();
        var _with1 = objPixcelCode;
        if (_with1.LoadByPrimaryKey(intPKID))
        {
            _with1.MarkAsDeleted();
            _with1.Save();
        }
        retval = true;
        objPixcelCode = null;
        return retval;
    }

    protected void dgvPixelCode_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void dgvPixelCode_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}


