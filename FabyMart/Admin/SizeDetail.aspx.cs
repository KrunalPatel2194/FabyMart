using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.IO;
using System.Drawing;


public partial class SizeDetail : PageBase_Admin
{
    tblSize objSize;
    //tblStatus objstatus;
    tblSubCategory objProjectImage;
    clsEncryption objEncrypt;
    clsCommon objClsCommon;
    tblSizeSubCategory objSizeSubCategory;
    int iprojectID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadUnselectedCategories();
       
        if (!Page.IsPostBack)
        {
            ViewState["SortOrder"] = appFunctions.Enum_SortOrderBy.Asc;
            ViewState["SortColumn"] = "";

            lnkSaveAndAddnew.Visible = HasAdd;
            txtSize.Focus();

            //objCommon = new clsCommon();
            //objCommon.FillDropDownList(ddlstatus, "tblStatus", tblStatus.ColumnNames.AppStatus, tblStatus.ColumnNames.AppStatusID, "--Select Status--", tblStatus.ColumnNames.AppIsActive + "=1");
            //objCommon = null;

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
                LoadAllData();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "HideTab()", true);
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
              


                    objSizeSubCategory = new tblSizeSubCategory();
                    objSizeSubCategory.AddNew();

                    objSizeSubCategory.s_AppDisplayOrder = (objCommon.GetNextDisplayOrder("tblSizeSubCategory", tblSizeSubCategory.ColumnNames.AppDisplayOrder, tblSizeSubCategory.ColumnNames.AppSubCategoryID + "=" + hdnPKID.Value).ToString());
                     objSizeSubCategory.s_AppSizeID = hdnPKID.Value;
                    objSizeSubCategory.s_AppSubCategoryID = e.CommandArgument.ToString();
                   // objSizeSubCategory.AppIsActive = true;
                    objSizeSubCategory.Save();
                    objSizeSubCategory = null;
                    //LoadUnselectedCategories();
                    LoadUnselectedCategories();
                    LoadSubCategoryData(false, false);
                    objCommon = null;
                
            }
        }
    }
    public void LoadUnselectedCategories()
    {
        if (ddlCategory.SelectedIndex > 0)
        {
            objSizeSubCategory = new tblSizeSubCategory();

            objDataTable = objSizeSubCategory.LoadUnSelectedCategories(hdnPKID.Value, ddlCategory.SelectedValue);

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

            objSizeSubCategory = null;
        }
        else
        {
            dgvUnSelectedCategories.DataSource = null;
            dgvUnSelectedCategories.DataBind();
        }
    }
    private bool SaveData()
    {
        objCommon = new clsCommon();
        if (objCommon.IsRecordExists("tblSize", tblSize.ColumnNames.AppSize, tblSize.ColumnNames.AppSizeID, txtSize.Text, hdnPKID.Value))
        {

            DInfo.ShowMessage("Size is already exists", Enums.MessageType.Warning);
            return false;
        }
        objSize = new tblSize();
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objSize.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value));
        }
        else
        {
            objSize.AddNew();


        }

        objSize.s_AppSize = txtSize.Text.Trim();
        objSize.AppIsActive = chkIsActive.Checked;
        if (chkIsDefault.Checked)
        {
            tblSize ObjTemp= new tblSize();
            ObjTemp.SetDefaultSize();
            ObjTemp = null;
            objSize.AppIsActive = true;
            objSize.AppIsDefault = true;
        }
        else
        {
            tblSize ObjTemp = new tblSize();
            ObjTemp.LoadAll();
            if (ObjTemp.RowCount <= 0)
            {
                objSize.AppIsActive = true;
                objSize.AppIsDefault = true;
            }
            else
            {
                objSize.AppIsDefault = false;
            }
            ObjTemp = null;
        }


        objSize.Save();
        iprojectID = objSize.AppSizeID;
        objSize = null;
        objClsCommon = null;
        return true;
    }

    private void SetValuesToControls()
    {
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objSize = new tblSize();
            if (objSize.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value)))
            {
                txtSize.Text = objSize.AppSize;

                chkIsActive.Checked = objSize.AppIsActive;
                chkIsDefault.Checked = objSize.AppIsDefault;
                if (chkIsDefault.Checked)
                {
                    chkIsDefault.Enabled = false;
                }
            }
            objSize = null;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Size.aspx", true);
    }

    private void ResetControls()
    {
        txtSize.Text = "";
        chkIsActive.Checked = true;
        chkIsDefault.Checked = false;
        chkIsDefault.Enabled = true;
        hdnPKID.Value = "";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "HideTab()", true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("Size has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("Size has been updated successfully", Enums.MessageType.Successfull);
            }
            hdnPKID.Value = iprojectID.ToString();
            SetValuesToControls();
            LoadAllData();

        }
    }

    protected void lnkSaveAndClose_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "Size has been added successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            else
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "Size has been updated successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            Response.Redirect("Size.aspx");
        }
    }

    protected void lnkSaveAndAddnew_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("Size been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("Size has been updated successfully", Enums.MessageType.Successfull);
            }
            ResetControls();
        }
    }

    public void LoadAllData()
    {
        objCommon = new clsCommon();
        objCommon.FillRecordPerPage(ref ddlSubCategoryPerPage);
        objCommon.FillDropDownList(ddlCategory, "tblCategory ", tblCategory.ColumnNames.AppCategory, tblCategory.ColumnNames.AppCategoryID, "--Select Category--", tblCategory.ColumnNames.AppCategory, appFunctions.Enum_SortOrderBy.Asc);
        objCommon.FillDropDownList(ddlSerachCategory, "tblCategory ", tblCategory.ColumnNames.AppCategory, tblCategory.ColumnNames.AppCategoryID, "--Select Category--", tblCategory.ColumnNames.AppCategory, appFunctions.Enum_SortOrderBy.Asc);
        //ddlSubCategory.Items.Add(new ListItem("--Select Sub Category--", "0"));
        ddlSerachSubCategory.Items.Add(new ListItem("--Select Sub Category--", "0"));
        objCommon = null;
        LoadSubCategory();
        LoadUnselectedCategories();
    }

    public void LoadSubCategory()
    {
        ResetSubCategoryControl();
        LoadSubCategoryData(true, false);
    }

    public void ResetSubCategoryControl()
    {
        ddlCategory.SelectedIndex = 0;
       // ddlSubCategory.Items.Clear();
       // ddlSubCategory.Items.Add(new ListItem("--Select Sub Category--", "0"));

        hdnSubCategoryId.Value = "";
    }

    protected void btnSaveSubCategory_Click(object sender, EventArgs e)
    {
        if (SaveSubCategoryData())
        {
            if (string.IsNullOrEmpty(hdnSubCategoryId.Value))
            {
                DInfoSubCategory.ShowMessage("Size Sub Category  has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfoSubCategory.ShowMessage("Size Sub Category  has been updated successfully", Enums.MessageType.Successfull);
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

 
    private bool SaveSubCategoryData()
    {
        if (hdnPKID.Value != "")
        {

            objCommon = new clsCommon();
            //if (objCommon.IsRecordExists("tblSizeSubCategory", tblSizeSubCategory.ColumnNames.AppSubCategoryID, tblSizeSubCategory.ColumnNames.AppSizeSubCategoryID, ddlSubCategory.SelectedValue, hdnSubCategoryId.Value, tblSizeSubCategory.ColumnNames.AppSubCategoryID + "=" + ddlSubCategory.SelectedValue))
            //{
            //    DInfoSubCategory.ShowMessage("Size in Sub Category alredy exits.", Enums.MessageType.Error);
            //    return false;
            //}
            objSizeSubCategory = new tblSizeSubCategory();

            if (!string.IsNullOrEmpty(hdnSubCategoryId.Value) && hdnSubCategoryId.Value != "")
            {
                objSizeSubCategory.LoadByPrimaryKey(Convert.ToInt32(hdnSubCategoryId.Value));
            }
            else
            {
                objSizeSubCategory.AddNew();
              //  objSizeSubCategory.s_AppDisplayOrder = objCommon.GetNextDisplayOrder("tblSizeSubCategory", tblSizeSubCategory.ColumnNames.AppDisplayOrder, tblSizeSubCategory.ColumnNames.AppSubCategoryID + "=" + ddlSubCategory.SelectedValue).ToString();
            }
            objSizeSubCategory.s_AppSizeID = hdnPKID.Value;
    //        objSizeSubCategory.s_AppSubCategoryID = ddlSubCategory.SelectedValue;
            objSizeSubCategory.Save();
            objSizeSubCategory = null;
            objCommon = null;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void LoadSubCategoryData(bool IsResetPageIndex, bool IsSort, string strFieldName = "", string strFieldValue = "")
    {
        objSizeSubCategory = new tblSizeSubCategory();

        objDataTable = objSizeSubCategory.LoadGridData(hdnPKID.Value, ddlSerachCategory.SelectedValue, ddlSerachSubCategory.SelectedValue);

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

        objSizeSubCategory = null;
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
                    objCommon.SetDisplayOrder("tblSizeSubCategory", tblSizeSubCategory.ColumnNames.AppSizeSubCategoryID, tblSizeSubCategory.ColumnNames.AppDisplayOrder, (int)dgvSubCategory.DataKeys[drCurrent.RowIndex].Values[0], (int)dgvSubCategory.DataKeys[drCurrent.RowIndex].Values[1], (int)dgvSubCategory.DataKeys[drUp.RowIndex].Values[0], (int)dgvSubCategory.DataKeys[drUp.RowIndex].Values[1]);
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
                    objCommon.SetDisplayOrder("tblSizeSubCategory", tblSizeSubCategory.ColumnNames.AppSizeSubCategoryID, tblSizeSubCategory.ColumnNames.AppDisplayOrder, (int)dgvSubCategory.DataKeys[drCurrent.RowIndex].Values[0], (int)dgvSubCategory.DataKeys[drCurrent.RowIndex].Values[1], (int)dgvSubCategory.DataKeys[drUp.RowIndex].Values[0], (int)dgvSubCategory.DataKeys[drUp.RowIndex].Values[1]);
                    LoadSubCategoryData(false, false);
                    objCommon = null;
                }
            }


        }
    }

    private void SetSubCategoryValuesToControls()
    {
        if (!string.IsNullOrEmpty(hdnSubCategoryId.Value) && hdnSubCategoryId.Value != "")
        {
            objSizeSubCategory = new tblSizeSubCategory();
            if (objSizeSubCategory.LoadByPrimaryKey(Convert.ToInt32(hdnSubCategoryId.Value)))
            {
                tblSubCategory objTemp = new tblSubCategory();
                if (objTemp.LoadByPrimaryKey(objSizeSubCategory.AppSubCategoryID))
                {
                    ddlCategory.SelectedValue = objTemp.s_AppCategoryID;
                    objCommon = new clsCommon();
                  //  objCommon.FillDropDownList(ddlSubCategory, "tblSubCategory ", tblSubCategory.ColumnNames.AppSubCategory, tblSubCategory.ColumnNames.AppSubCategoryID, "--Select Sub Category--", tblSubCategory.ColumnNames.AppSubCategory, appFunctions.Enum_SortOrderBy.Asc, tblSubCategory.ColumnNames.AppCategoryID + "=" + ddlCategory.SelectedValue);
                    objCommon = null;
                   // ddlSubCategory.SelectedValue = objSizeSubCategory.s_AppSubCategoryID;
                }
                objTemp = null;

            }
            objSizeSubCategory = null;
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
        DInfoSubCategory.ShowMessage("Size Sub Category has been deleted successfully", Enums.MessageType.Successfull);
        hdnSelectedIDs.Value = "";
        LoadUnselectedCategories();
        
    }

    private bool DeleteSubCategory(int intPKID)
    {
        bool retval = false;
        objSizeSubCategory = new tblSizeSubCategory();
        var _with1 = objSizeSubCategory;
        if (_with1.LoadByPrimaryKey(intPKID))
        {
            _with1.MarkAsDeleted();
            _with1.Save();
        }
        retval = true;
        objSizeSubCategory = null;
        return retval;
    }

    protected void dgvSubCategory_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void dgvSubCategory_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }


}