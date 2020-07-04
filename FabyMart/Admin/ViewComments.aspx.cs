using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;

public partial class Admin_PendingReviewList : PageBase_Admin
{
    tblReviews objReviews;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hdnProductDetailID.Value = objEncrypt.Decrypt(Request.QueryString.Get("ID"), appFunctions.strKey);
            ViewState["SortOrder"] = appFunctions.Enum_SortOrderBy.Asc;
            ViewState["SortColumn"] = "";
            //btnAdd.Visible = HasAdd;
            btnDelete.Visible = HasDelete;

            dgvGridView.Columns[0].Visible = HasDelete;
            // dgvGridView.Columns[1].Visible = HasEdit;
            objCommon = new clsCommon();
            objCommon.BindEnumtoDDL(typeof( Enums.Enum_ReviewStatus), ddlStatus, "--Select Status--");
          
         //   objCommon.FillDropDownList(ddlUserName, "tblRegistration", tblRegistration.ColumnNames.AppFullName, tblRegistration.ColumnNames.AppRegistrationID, "--Select UserName--", tblRegistration.ColumnNames.AppUserStatus + "<>" + Convert.ToInt32(Enums.Enum_UserStatus.Created));
          //  objCommon.FillDropDownList(ddlProfessionalName, "tblProfessional", tblProfessional.ColumnNames.AppProfessionalName, tblProfessional.ColumnNames.AppProfessionalID, "--Select Professional--", tblProfessional.ColumnNames.AppPublishedBy + "<>" + "0");
            objCommon = null;
            if (Session[appFunctions.Session.ShowMessage.ToString()] != null)
            {
                if (!string.IsNullOrEmpty(Session[appFunctions.Session.ShowMessage.ToString()].ToString()))
                {
                    DInfo.ShowMessage(Session[appFunctions.Session.ShowMessage.ToString()].ToString(), (Enums.MessageType)Session[appFunctions.Session.ShowMessageType.ToString()]);
                    Session[appFunctions.Session.ShowMessage.ToString()] = "";
                    Session[appFunctions.Session.ShowMessageType.ToString()] = "";
                }
            }
            objCommon = new clsCommon();
            objCommon.FillRecordPerPage(ref ddlPerPage);
            if ((Request.QueryString.Get("ID") != null))
            {
                objEncrypt = new clsEncryption();
                try
                {
                    string pid = objEncrypt.Decrypt(Request.QueryString.Get("ID"), appFunctions.strKey);
               //     ddlProfessionalName.SelectedValue = pid;
                }
                catch (Exception ex)
                {
                    // noIdFoundRedirect("Employee.aspx");
                }

            }
            LoadDataGrid(true, false);
            //txtSearch.Focus();
            objCommon = null;
        }

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminReviewList.aspx", true);
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        string[] arIDs = hdnSelectedIDs.Value.ToString().TrimEnd(',').Split(',');
        bool IsReject = false;
        if (txtRemark.Text == "")
        {
            DInfo.ShowMessage("Add remarks for rejection of selected comments.", Enums.MessageType.Successfull);

        }
        else
        {
            for (int i = 0; i <= arIDs.Length - 1; i++)
            {
                if (!string.IsNullOrEmpty(arIDs.GetValue(i).ToString()))
                {
                    if (Reject(Convert.ToInt32(arIDs.GetValue(i))))
                    {
                        IsReject = true;
                    }
                }
            }

            if (IsReject)
            {
                LoadDataGrid(false, false);
                DInfo.ShowMessage("Reviews has been Rejected successfully", Enums.MessageType.Successfull);
            }
            hdnSelectedIDs.Value = "";
            txtRemark.Text = "";
        }
    }
    private bool Reject(int intPKID)
    {
        bool retval = false;
        objReviews = new tblReviews();
        if (objReviews.LoadByPrimaryKey(intPKID))
        {
            if (objReviews.RowCount > 0)
            {
                objReviews.s_AppRemark = txtRemark.Text;
                objReviews.AppReviewStatus = Convert.ToInt32(Enums.Enum_ReviewStatus.Rejected);
                objReviews.Save();

            }
            objReviews = null;
        }
        retval = true;
        objReviews = null;
        return retval;
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        string[] arIDs = hdnSelectedIDs.Value.ToString().TrimEnd(',').Split(',');
        bool IsApprove = false;
        for (int i = 0; i <= arIDs.Length - 1; i++)
        {
            if (!string.IsNullOrEmpty(arIDs.GetValue(i).ToString()))
            {
                if (Approve(Convert.ToInt32(arIDs.GetValue(i))))
                {
                    IsApprove = true;
                }
            }
        }

        if (IsApprove)
        {
            LoadDataGrid(false, false);
            DInfo.ShowMessage("Review has been approved successfully", Enums.MessageType.Successfull);
        }
        hdnSelectedIDs.Value = "";
    }
    private bool Approve(int intPKID)
    {
        bool retval = false;
        objReviews = new tblReviews();
        if (objReviews.LoadByPrimaryKey(intPKID))
        {
            if (objReviews.RowCount > 0)
            {
                objReviews.AppRemark = "";
                objReviews.AppReviewStatus = Convert.ToInt32(Enums.Enum_ReviewStatus.Approved);
                objReviews.Save();
            }
            objReviews = null;
        }
        retval = true;
        objReviews = null;
        return retval;
    }

    private void LoadDataGrid(bool IsResetPageIndex, bool IsSort)
    {
        objReviews = new tblReviews();
      //  objDataTable = objReviews.LoadPendingReviewGridData(ddlFields.SelectedValue, txtSearch.Text.Trim(), ddlUserName.SelectedValue, ddlProfessionalName.SelectedValue);
        objDataTable = objReviews.LoadPendingReviewGridData(hdnProductDetailID.Value,"",Convert.ToInt32(ddlStatus.SelectedValue) ,txtStartDate.Text,txtEndDate.Text);

        if (IsResetPageIndex)
        {
            if (dgvGridView.PageCount > 0)
            {
                dgvGridView.PageIndex = 0;
            }
        }

        dgvGridView.DataSource = null;
        dgvGridView.DataBind();
        lblCount.Text = 0.ToString();
        hdnSelectedIDs.Value = "";

        if (objDataTable.Rows.Count <= 0)
        {
            DInfo.ShowMessage("No data found", Enums.MessageType.Information);
            return;
        }
        else
        {
            if (ddlPerPage.SelectedItem.Text.ToLower() == "all")
            {
                dgvGridView.AllowPaging = false;
            }
            else
            {
                dgvGridView.AllowPaging = true;
                dgvGridView.PageSize = Convert.ToInt32(ddlPerPage.SelectedItem.Text);
            }//objDataTable.Rows[0][tblProduct.ColumnNames.AppProductID].ToString();
            lblProductName.Text=objDataTable.Rows[0][tblProduct.ColumnNames.AppProductName].ToString();
            lblCount.Text = objDataTable.Rows.Count.ToString();
            if (IsSort)
            {
                objDataTable = SortDatatable(objDataTable, ViewState["SortColumn"].ToString(), (appFunctions.Enum_SortOrderBy)ViewState["SortOrder"], IsSort);
            }
            dgvGridView.DataSource = objDataTable;
            dgvGridView.DataBind();
        }

        objReviews = null;

    }

    protected void btnGO_Click(object sender, EventArgs e)
    {
        LoadDataGrid(true, false);
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtStartDate.Text = "";
        txtEndDate.Text = "";
        ddlStatus.SelectedIndex = 0;
             LoadDataGrid(true, false);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string[] arIDs = hdnSelectedIDs.Value.ToString().TrimEnd(',').Split(',');
        bool IsDelete = false;
        for (int i = 0; i <= arIDs.Length - 1; i++)
        {
            if (!string.IsNullOrEmpty(arIDs.GetValue(i).ToString()))
            {
                if (Delete(Convert.ToInt32(arIDs.GetValue(i))))
                {
                    IsDelete = true;
                }
            }
        }

        if (IsDelete)
        {
            LoadDataGrid(false, false);
            DInfo.ShowMessage("Review has been deleted successfully", Enums.MessageType.Successfull);
        }
        hdnSelectedIDs.Value = "";
    }
    private bool Delete(int intPKID)
    {
        bool retval = false;
        objReviews = new tblReviews();
      
        var _with1 = objReviews;
        if (_with1.LoadByPrimaryKey(intPKID))
        {
            _with1.MarkAsDeleted();
            _with1.Save();
                    }
        
        retval = true;
        objReviews = null;
        return retval;

    }
 
    protected void dgvGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dgvGridView.PageIndex = e.NewPageIndex;
        LoadDataGrid(false, false);
    }
    protected void dgvGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
        {
            objCommon = new clsCommon();

           
            if (e.CommandName == "IsView")
            {
                objReviews = new tblReviews();

                objDataTable = objReviews.LoadReviewByID(e.CommandArgument.ToString());
                objReviews = null;

                if (objDataTable.Rows.Count > 0)
                {
                    hdnPKID.Value = e.CommandArgument.ToString();
                    lblUserName.Text = objDataTable.Rows[0][tblCustomer.ColumnNames.AppFirstName].ToString() + " " + objDataTable.Rows[0][tblCustomer.ColumnNames.AppLastName].ToString();
                    lblComment.Text = objDataTable.Rows[0][tblReviews.ColumnNames.AppComment].ToString();
                   
                    lblDate.Text = objDataTable.Rows[0][tblReviews.ColumnNames.AppReviewDate].ToString();
                    lblRating.Text = objDataTable.Rows[0][tblReviews.ColumnNames.AppRating].ToString();
                  
                    mpeReviewView.Show();
                }

            }
            objCommon = null;
        }
    }
    
  
    protected void btnSave_Click(object sender, EventArgs e)
    {
        objReviews = new tblReviews();
        objReviews.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value));
        objReviews.s_AppPublishedBy = Session[appFunctions.Session.UserID.ToString()].ToString();
        objReviews.AppPublishedDate = GetDateTime();
        objReviews.s_AppReviewStatus = Convert.ToInt32(Enums.Enum_ReviewStatus.Approved).ToString();
        objReviews.Save();
        LoadDataGrid(true, false);
    }
    protected void btnDisapproved_Click(object sender, EventArgs e)
    {
        objReviews = new tblReviews();
        objReviews.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value));
        objReviews.s_AppPublishedBy = Session[appFunctions.Session.UserID.ToString()].ToString();
        objReviews.AppPublishedDate = GetDateTime();
        objReviews.s_AppReviewStatus = Convert.ToInt32(Enums.Enum_ReviewStatus.Rejected).ToString();
        objReviews.Save();
        LoadDataGrid(true, false);
        mpeReviewView.Hide();
    }
    protected void dgvGridView_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnSelectedIDs.Value = "";
        ViewState["SortColumn"] = e.SortExpression;
        LoadDataGrid(false, true);
    }
    protected void dgvGridView_RowCreated(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                string strID = dgvGridView.DataKeys[e.Row.RowIndex].Values[0].ToString();
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSelectRow");
                chk.ID = "chkSelectRow_" + strID;
                break;
        }
    }
    protected void dgvGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataControlRowType itemType = e.Row.RowType;
        switch (itemType)
        {
            case DataControlRowType.DataRow:
             
                string strID = dgvGridView.DataKeys[e.Row.RowIndex].Values[0].ToString();
                
                     string ReviewStatusID = dgvGridView.DataKeys[e.Row.RowIndex].Values[2].ToString();
               
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSelectRow");

                if ((chk != null))
                {
                    chk.Attributes.Add("OnClick", "javascript:SelectRow(this," + strID + ")");
                }
               

                break;
        }
    }
    protected void ddlPerPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnSelectedIDs.Value = "";
        LoadDataGrid(true, false);
    }
    //protected void ddlUserName_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    LoadDataGrid(true, false);

    //}

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDataGrid(true, false);

    }
}   