using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.IO;
using System.Drawing;


public partial class PropertyDetail : PageBase_Admin
{
    tblProperty objProperty;
    tblPropertyPreValue objPropertyPreValue;
    tblPropertySubCategory objPropertySubCategory;
    clsEncryption objEncrypt;
    clsCommon objClsCommon;
    int intPkId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
          
            lnkSaveAndAddnew.Visible = HasAdd;
            ViewState["SortOrder"] = appFunctions.Enum_SortOrderBy.Asc;
            ViewState["SortColumn"] = "";

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

    private bool SaveData()
    {
        objCommon = new clsCommon();
        if (objCommon.IsRecordExists("tblProperty", tblProperty.ColumnNames.AppPropertyName, tblProperty.ColumnNames.AppPropertyID, txtPropertyName.Text, hdnPKID.Value))
        {
            DInfo.ShowMessage("Property Name alredy exits.", Enums.MessageType.Error);
            return false;
        }
        if (objCommon.IsRecordExists("tblProperty", tblProperty.ColumnNames.AppDisplayName, tblProperty.ColumnNames.AppPropertyID, txtDisplayName.Text, hdnPKID.Value))
        {
            DInfo.ShowMessage("Display Name alredy exits.", Enums.MessageType.Error);
            return false;
        }
        objProperty = new tblProperty();
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objProperty.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value));
        }
        else
        {
            objProperty.AddNew();
            objProperty.AppDisplayOrder = objCommon.GetNextDisplayOrder("tblProperty", tblProperty.ColumnNames.AppDisplayOrder);
            objProperty.s_AppCreatedBy = Session[appFunctions.Session.UserID.ToString()].ToString();
            objProperty.AppCreatedDate = DateTime.Now;
            

           // objBanner.AppDisplayOrder = objClsCommon.GetNextDisplayOrder("tblBanner", tblBanner.ColumnNames.AppDisplayOrder);
        }
        objProperty.AppPropertyName = txtPropertyName.Text;
        objProperty.AppDisplayName = txtDisplayName.Text;
        objProperty.AppDescription = txtDescription.Text;
        objProperty.AppIsPredefine = chkIsPredefine.Checked;
        objProperty.AppIsShowInSearch = ChkIsShowInSearch.Checked;
        objProperty.Save();
        intPkId = objProperty.AppPropertyID;
        objProperty = null;
        objCommon = null;
        return true;
    }

    private void SetValuesToControls()
    {
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objProperty = new tblProperty();
            if (objProperty.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value)))
            {
                txtPropertyName.Text = objProperty.AppPropertyName;
                txtDisplayName.Text = objProperty.AppDisplayName;
                txtDescription.Text = objProperty.AppDescription;
                chkIsPredefine.Checked = objProperty.AppIsPredefine;
                ChkIsShowInSearch.Checked = objProperty.AppIsShowInSearch;
                if (!objProperty.AppIsPredefine)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "SomeShoW()", true);
                }
                LoadAllData();

            }
            objProperty = null;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Property.aspx", true);
    }

    private void ResetControls()
    {
        txtPropertyName.Text = "";
        txtDisplayName.Text = "";
        txtDescription.Text = "";
        chkIsPredefine.Checked = false;
        ChkIsShowInSearch.Checked = false;
        hdnPKID.Value = "";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "HideTab()", true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("Property has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("Property has been updated successfully", Enums.MessageType.Successfull);
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
                Session[appFunctions.Session.ShowMessage.ToString()] = "Property has been added successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            else
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "Property has been updated successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            Response.Redirect("Property.aspx");
        }
    }

    protected void lnkSaveAndAddnew_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("Property has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("Property has been updated successfully", Enums.MessageType.Successfull);
            }
            ResetControls();
        }
    }

    //Load All Data
    public void LoadAllData()
    {
        objCommon = new clsCommon();
        objCommon.FillRecordPerPage(ref ddlPropertyPreValuePerPage);
        objCommon.FillRecordPerPage(ref ddlSubCategoryPerPage);
        objCommon.FillDropDownList(ddlCategory, "tblCategory ", tblCategory.ColumnNames.AppCategory, tblCategory.ColumnNames.AppCategoryID, "--Select Category--", tblCategory.ColumnNames.AppCategory, appFunctions.Enum_SortOrderBy.Asc);
        objCommon.FillDropDownList(ddlSerachCategory, "tblCategory ", tblCategory.ColumnNames.AppCategory, tblCategory.ColumnNames.AppCategoryID, "--Select Category--", tblCategory.ColumnNames.AppCategory, appFunctions.Enum_SortOrderBy.Asc);
    
        ddlSerachSubCategory.Items.Add(new ListItem("--Select Sub Category--", "0"));
        objCommon = null;
        LoadPropertyPreValue();
        LoadSubCategory();
    }

    

    public void LoadPropertyPreValue()
    {
        ResetPropertyPreValueControl();
        LoadPropertyPreValueData(true, false);
    }

    public void ResetPropertyPreValueControl()
    {
        txtPreValue.Text = "";
        chkPropertyPreValueIsActive.Checked = true;
        hdnPropertyPreValueId.Value = "";
    }

    protected void btnSavePropertyPreValue_Click(object sender, EventArgs e)
    {
        if (SavePropertyPreValueData())
        {
            if (string.IsNullOrEmpty(hdnPropertyPreValueId.Value))
            {
                DInfoPropertyPreValue.ShowMessage("sub category  has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfoPropertyPreValue.ShowMessage("sub category  has been updated successfully", Enums.MessageType.Successfull);
            }
            ResetPropertyPreValueControl();
            LoadPropertyPreValueData(false, false);
        }
    }

    protected void btnClearPropertyPreValue_Click(object sender, EventArgs e)
    {
        ResetPropertyPreValueControl();
    }

    private bool SavePropertyPreValueData()
    {
        if (hdnPKID.Value != "")
        {

            objCommon = new clsCommon();
            if (objCommon.IsRecordExists("tblPropertyPreValue", tblPropertyPreValue.ColumnNames.AppPreValue, tblPropertyPreValue.ColumnNames.AppPropertyPreValueID, txtPreValue.Text, hdnPropertyPreValueId.Value, tblPropertyPreValue.ColumnNames.AppPropertyID + "=" + hdnPKID.Value))
            {
                DInfoPropertyPreValue.ShowMessage("Pre-Value alredy exits.", Enums.MessageType.Error);
                return false;
            }
            objPropertyPreValue = new tblPropertyPreValue();

            if (!string.IsNullOrEmpty(hdnPropertyPreValueId.Value) && hdnPropertyPreValueId.Value != "")
            {
                objPropertyPreValue.LoadByPrimaryKey(Convert.ToInt32(hdnPropertyPreValueId.Value));
            }
            else
            {
                objPropertyPreValue.AddNew();
                objPropertyPreValue.s_AppCreatedBy = Session[appFunctions.Session.UserID.ToString()].ToString();
               objPropertyPreValue.AppCreatedDate = DateTime.Now;
                objPropertyPreValue.AppDisplayOrder = objCommon.GetNextDisplayOrder("tblPropertyPreValue", tblPropertyPreValue.ColumnNames.AppDisplayOrder, tblPropertyPreValue.ColumnNames.AppPropertyID + "=" + hdnPKID.Value);
            }
            objPropertyPreValue.s_AppPropertyID = hdnPKID.Value;
            objPropertyPreValue.AppPreValue = txtPreValue.Text;
            objPropertyPreValue.AppIsActive = chkPropertyPreValueIsActive.Checked;

            objPropertyPreValue.Save();
            objPropertyPreValue = null;
            objCommon = null;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void LoadPropertyPreValueData(bool IsResetPageIndex, bool IsSort, string strFieldName = "", string strFieldValue = "")
    {
        objPropertyPreValue = new tblPropertyPreValue();

        objDataTable = objPropertyPreValue.LoadGridData(ddlPropertyPreValueFields.SelectedValue, txtPropertyPreValueSearch.Text, hdnPKID.Value);

        //'Reset PageIndex of gridviews
        if (IsResetPageIndex)
        {
            if (dgvPropertyPreValue.PageCount > 0)
            {
                dgvPropertyPreValue.PageIndex = 0;
            }
        }

        dgvPropertyPreValue.DataSource = null;
        dgvPropertyPreValue.DataBind();
        lblPropertyPreValueCount.Text = 0.ToString();
        hdnSelectedIDs.Value = "";

        //'Check for data into datatable
        if (objDataTable.Rows.Count <= 0)
        {
            DinfoPropertyPreValueData.ShowMessage("No data found", Enums.MessageType.Information);
            return;
        }
        else
        {
            if (ddlPropertyPreValuePerPage.SelectedItem.Text.ToLower() == "all")
            {
                dgvPropertyPreValue.AllowPaging = false;
            }
            else
            {
                dgvPropertyPreValue.AllowPaging = true;
                dgvPropertyPreValue.PageSize = Convert.ToInt32(ddlPropertyPreValuePerPage.SelectedItem.Text);
            }

            lblPropertyPreValueCount.Text = objDataTable.Rows.Count.ToString();
            objDataTable = SortDatatable(objDataTable, ViewState["SortColumn"].ToString(), (appFunctions.Enum_SortOrderBy)ViewState["SortOrder"], IsSort);
            dgvPropertyPreValue.DataSource = objDataTable;
            dgvPropertyPreValue.DataBind();
        }

        objPropertyPreValue = null;
    }

    protected void btnPreValueGO_Click(object sender, System.EventArgs e)
    {
        LoadPropertyPreValueData(true, false);
    }

    protected void btnPreValueReset_Click(object sender, System.EventArgs e)
    {
        txtPropertyPreValueSearch.Text = "";
        LoadPropertyPreValueData(true, false);
    }

    protected void dgvPropertyPreValue_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dgvPropertyPreValue.PageIndex = e.NewPageIndex;
        LoadPropertyPreValueData(false, false);
    }

    protected void dgvPropertyPreValue_RowCreated(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                string strID = dgvPropertyPreValue.DataKeys[e.Row.RowIndex].Values[0].ToString();
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSelectRow");
                chk.ID = "chkSelectRow_" + strID;
                break;
        }
    }

    protected void dgvPropertyPreValue_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataControlRowType itemType = e.Row.RowType;

        switch (itemType)
        {
            case DataControlRowType.DataRow:
                string strID = dgvPropertyPreValue.DataKeys[e.Row.RowIndex].Values[0].ToString();
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSelectRow");
                if ((chk != null))
                {
                    chk.Attributes.Add("OnClick", "javascript:SelectRow(this," + strID + ")");
                }
                break;
        }
    }

    protected void dgvPropertyPreValue_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnSelectedIDs.Value = "";
        ViewState["SortColumn"] = e.SortExpression;
        LoadPropertyPreValueData(false, true);
    }

    protected void ddlPropertyPreValuePerPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnSelectedIDs.Value = "";
        LoadPropertyPreValueData(true, false);
    }

    protected void dgvPropertyPreValue_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
        {
            objCommon = new clsCommon();
            if (e.CommandName == "Edit")
            {
                hdnPropertyPreValueId.Value = e.CommandArgument.ToString();
                SetPropertyPreValueValuesToControls();
            }
            else if (e.CommandName == "Up")
            {
                LinkButton inkButton = (LinkButton)e.CommandSource;
                GridViewRow drCurrent = (GridViewRow)inkButton.Parent.Parent;
                if (drCurrent.RowIndex > 0)
                {
                    GridViewRow drUp = dgvPropertyPreValue.Rows[drCurrent.RowIndex - 1];
                    objCommon.SetDisplayOrder("tblPropertyPreValue", tblPropertyPreValue.ColumnNames.AppPropertyPreValueID, tblPropertyPreValue.ColumnNames.AppDisplayOrder, (int)dgvPropertyPreValue.DataKeys[drCurrent.RowIndex].Values[0], (int)dgvPropertyPreValue.DataKeys[drCurrent.RowIndex].Values[1], (int)dgvPropertyPreValue.DataKeys[drUp.RowIndex].Values[0], (int)dgvPropertyPreValue.DataKeys[drUp.RowIndex].Values[1]);
                    LoadPropertyPreValueData(false, false);
                    objCommon = null;
                }
            }
            else if (e.CommandName == "Down")
            {
                LinkButton lnkButton = (LinkButton)e.CommandSource;
                GridViewRow drCurrent = (GridViewRow)lnkButton.Parent.Parent;
                if (drCurrent.RowIndex < dgvPropertyPreValue.Rows.Count - 1)
                {
                    GridViewRow drUp = dgvPropertyPreValue.Rows[drCurrent.RowIndex + 1];
                    objCommon.SetDisplayOrder("tblPropertyPreValue", tblPropertyPreValue.ColumnNames.AppPropertyPreValueID, tblPropertyPreValue.ColumnNames.AppDisplayOrder, (int)dgvPropertyPreValue.DataKeys[drCurrent.RowIndex].Values[0], (int)dgvPropertyPreValue.DataKeys[drCurrent.RowIndex].Values[1], (int)dgvPropertyPreValue.DataKeys[drUp.RowIndex].Values[0], (int)dgvPropertyPreValue.DataKeys[drUp.RowIndex].Values[1]);
                    LoadPropertyPreValueData(false, false);
                    objCommon = null;
                }
            }
            else if (e.CommandName == "IsActive")
            {
                objPropertyPreValue = new tblPropertyPreValue();

                if (objPropertyPreValue.LoadByPrimaryKey(Convert.ToInt32(e.CommandArgument.ToString())))
                {

                    if (objPropertyPreValue.AppIsActive == true)
                    {
                        objPropertyPreValue.AppIsActive = false;
                    }
                    else if (objPropertyPreValue.AppIsActive == false)
                    {
                        objPropertyPreValue.AppIsActive = true;
                    }
                    objPropertyPreValue.Save();
                    LoadPropertyPreValueData(false, false);
                }
                objPropertyPreValue = null;
            }

        }
    }

    private void SetPropertyPreValueValuesToControls()
    {
        if (!string.IsNullOrEmpty(hdnPropertyPreValueId.Value) && hdnPropertyPreValueId.Value != "")
        {
            objPropertyPreValue = new tblPropertyPreValue();
            if (objPropertyPreValue.LoadByPrimaryKey(Convert.ToInt32(hdnPropertyPreValueId.Value)))
            {
                txtPreValue.Text = objPropertyPreValue.AppPreValue;
                chkPropertyPreValueIsActive.Checked = objPropertyPreValue.AppIsActive;

            }
            objPropertyPreValue = null;
        }
    }

    protected void btnPropertyPreValueDelete_Click(object sender, EventArgs e)
    {
        string[] arIDs = hdnSelectedIDs.Value.ToString().TrimEnd(',').Split(',');
        bool IsDelete = false;

        for (int i = 0; i <= arIDs.Length - 1; i++)
        {
            if (!string.IsNullOrEmpty(arIDs.GetValue(i).ToString()))
            {
                if (DeletePropertyPreValue(Convert.ToInt32(arIDs.GetValue(i))))
                {
                    IsDelete = true;
                }
            }
        }

        if (IsDelete)
        {
            LoadPropertyPreValueData(false, false);
        }
        DInfoPropertyPreValue.ShowMessage("Property PreValue has been deleted successfully", Enums.MessageType.Successfull);
        hdnSelectedIDs.Value = "";
    }

    private bool DeletePropertyPreValue(int intPKID)
    {
        bool retval = false;
        objPropertyPreValue = new tblPropertyPreValue();
        var _with1 = objPropertyPreValue;
        if (_with1.LoadByPrimaryKey(intPKID))
        {
            _with1.MarkAsDeleted();
            _with1.Save();
        }
        retval = true;
        objPropertyPreValue = null;
        return retval;
    }

    protected void dgvPropertyPreValue_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void dgvPropertyPreValue_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    
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

    protected void btnSaveSubCategory_Click(object sender, EventArgs e)
    {
        if (SaveSubCategoryData())
        {
            if (string.IsNullOrEmpty(hdnSubCategoryId.Value))
            {
                DInfoSubCategory.ShowMessage("Property Sub Category  has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfoSubCategory.ShowMessage("Property Sub Category  has been updated successfully", Enums.MessageType.Successfull);
            }
            ResetSubCategoryControl();
            LoadSubCategoryData(false, false);
        }
    }

    protected void btnClearSubCategory_Click(object sender, EventArgs e)
    {
        ResetSubCategoryControl();
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadUnselectedCategories();
        LoadSubCategoryData(false, false);
    }
    public void LoadUnselectedCategories()
    {
        if (ddlCategory.SelectedIndex > 0)
        {
            objPropertySubCategory = new tblPropertySubCategory();

            objDataTable = objPropertySubCategory.LoadUnSelectedCategories(hdnPKID.Value, ddlCategory.SelectedValue);

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

            objPropertySubCategory = null;
        }
        else
        {
            dgvUnSelectedCategories.DataSource = null;
            dgvUnSelectedCategories.DataBind();
        }
    }
    private bool SaveSubCategoryData()
    {
        if (hdnPKID.Value != "")
        {

            objCommon = new clsCommon();
          
            objPropertySubCategory = new tblPropertySubCategory();

          
                objPropertySubCategory.AddNew();
                objPropertySubCategory.s_AppCreatedBy = Session[appFunctions.Session.UserID.ToString()].ToString();
             
                objPropertySubCategory.AppCreatedDate = DateTime.Now;
                objPropertySubCategory.AppDisplayOrder = objCommon.GetNextDisplayOrder("tblPropertySubCategory", tblPropertySubCategory.ColumnNames.AppDisplayOrder, tblPropertySubCategory.ColumnNames.AppPropertyID + "=" + hdnPKID.Value);
           
            objPropertySubCategory.s_AppPropertyID = hdnPKID.Value;
         
            objPropertySubCategory.Save();
            objPropertySubCategory = null;
            objCommon = null;
            return true;
        }
        else
        {
            return false;
        }
    }
    protected void dgvUnSelectedCategories_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
        {
            objCommon = new clsCommon();
            if (e.CommandName == "IsAdd")
            {


                objPropertySubCategory = new tblPropertySubCategory();
                objPropertySubCategory.AddNew();
                objPropertySubCategory.AppDisplayOrder = objCommon.GetNextDisplayOrder("tblPropertySubCategory", tblPropertySubCategory.ColumnNames.AppDisplayOrder, tblPropertySubCategory.ColumnNames.AppPropertyID + "=" + hdnPKID.Value);
                objPropertySubCategory.s_AppPropertyID = hdnPKID.Value;
                objPropertySubCategory.s_AppSubCategoryID = e.CommandArgument.ToString();
                objPropertySubCategory.AppIsActive = true;
                objPropertySubCategory.Save();
                objPropertySubCategory = null;
                  
                    LoadUnselectedCategories();
                    LoadSubCategoryData(false, false);
                    objCommon = null;
               
            }
        }
    }

    public void LoadSubCategoryData(bool IsResetPageIndex, bool IsSort, string strFieldName = "", string strFieldValue = "")
    {
        objPropertySubCategory = new tblPropertySubCategory();

        objDataTable = objPropertySubCategory.LoadGridData(hdnPKID.Value, ddlSerachCategory.SelectedValue, ddlSerachSubCategory.SelectedValue);

       
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

        objPropertySubCategory = null;
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
        ddlSerachSubCategory.Items.Clear();
        ddlSerachSubCategory.Items.Add(new ListItem("--Select Sub Category--", "0"));
        LoadSubCategoryData(true, false);
    }

    protected void ddlSerachCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon = new clsCommon();
        objCommon.FillDropDownList(ddlSerachSubCategory, "tblSubCategory ", tblSubCategory.ColumnNames.AppSubCategory, tblSubCategory.ColumnNames.AppSubCategoryID, "--Select Sub Category--", tblSubCategory.ColumnNames.AppSubCategory, appFunctions.Enum_SortOrderBy.Asc, tblSubCategory.ColumnNames.AppCategoryID + "=" + ddlSerachCategory.SelectedValue);
        objCommon = null;
        LoadSubCategoryData(false, false);
    }

    protected void ddlSerachSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
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
                    objCommon.SetDisplayOrder("tblPropertySubCategory", tblPropertySubCategory.ColumnNames.AppPropertySubCategoryID, tblPropertySubCategory.ColumnNames.AppDisplayOrder, (int)dgvSubCategory.DataKeys[drCurrent.RowIndex].Values[0], (int)dgvSubCategory.DataKeys[drCurrent.RowIndex].Values[1], (int)dgvSubCategory.DataKeys[drUp.RowIndex].Values[0], (int)dgvSubCategory.DataKeys[drUp.RowIndex].Values[1]);
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
                    objCommon.SetDisplayOrder("tblPropertySubCategory", tblPropertySubCategory.ColumnNames.AppPropertySubCategoryID, tblPropertySubCategory.ColumnNames.AppDisplayOrder, (int)dgvSubCategory.DataKeys[drCurrent.RowIndex].Values[0], (int)dgvSubCategory.DataKeys[drCurrent.RowIndex].Values[1], (int)dgvSubCategory.DataKeys[drUp.RowIndex].Values[0], (int)dgvSubCategory.DataKeys[drUp.RowIndex].Values[1]);
                    LoadSubCategoryData(false, false);
                    objCommon = null;
                }
            }
            else if (e.CommandName == "IsActive")
            {
                objPropertySubCategory = new tblPropertySubCategory();

                if (objPropertySubCategory.LoadByPrimaryKey(Convert.ToInt32(e.CommandArgument.ToString())))
                {

                    if (objPropertySubCategory.AppIsActive == true)
                    {
                        objPropertySubCategory.AppIsActive = false;
                    }
                    else if (objPropertySubCategory.AppIsActive == false)
                    {
                        objPropertySubCategory.AppIsActive = true;
                    }
                    objPropertySubCategory.Save();
                    LoadSubCategoryData(false, false);
                }
                objPropertySubCategory = null;
            }

        }
    }

    private void SetSubCategoryValuesToControls()
    {
        if (!string.IsNullOrEmpty(hdnSubCategoryId.Value) && hdnSubCategoryId.Value != "")
        {
            objPropertySubCategory = new tblPropertySubCategory();
            if (objPropertySubCategory.LoadByPrimaryKey(Convert.ToInt32(hdnSubCategoryId.Value)))
            {
                tblSubCategory objTemp = new tblSubCategory();
                if (objTemp.LoadByPrimaryKey(objPropertySubCategory.AppSubCategoryID))
                {
                    ddlCategory.SelectedValue = objTemp.s_AppCategoryID;
                    objCommon = new clsCommon();
                    objCommon = null;
               
                }
                objTemp = null;

            

            }
            objPropertySubCategory = null;
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
        }
        DInfoSubCategory.ShowMessage("Property Sub Category has been deleted successfully", Enums.MessageType.Successfull);
        hdnSelectedIDs.Value = "";
        LoadUnselectedCategories();
    }

    private bool DeleteSubCategory(int intPKID)
    {
        bool retval = false;
        objPropertySubCategory = new tblPropertySubCategory();
        var _with1 = objPropertySubCategory;
        if (_with1.LoadByPrimaryKey(intPKID))
        {
            _with1.MarkAsDeleted();
            _with1.Save();
        }
        retval = true;
        objPropertySubCategory = null;
        return retval;
    }

    protected void dgvSubCategory_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void dgvSubCategory_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }



}
