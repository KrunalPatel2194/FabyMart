using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;

public partial class Product : PageBase_Admin
{
    tblProduct objProduct;
    override protected void OnInit(EventArgs e)
    {
        ExpFile.btnClick += new EventHandler(btnSetExportData_Click);
        base.OnInit(e);
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["SortOrder"] = appFunctions.Enum_SortOrderBy.Asc;
            ViewState["SortColumn"] = "";
            btnAdd.Visible = HasAdd;
            btnDelete.Visible = HasDelete;
            dgvGridView.Columns[0].Visible = HasDelete;
            dgvGridView.Columns[1].Visible = HasEdit;

            if ((Session[appFunctions.Session.ShowMessage.ToString()] != null))
            {
                if (!string.IsNullOrEmpty(Session[appFunctions.Session.ShowMessage.ToString()].ToString()))
                {
                    DInfo.ShowMessage(Session[appFunctions.Session.ShowMessage.ToString()].ToString(), (Enums.MessageType)Session[appFunctions.Session.ShowMessageType.ToString()]);
                    Session[appFunctions.Session.ShowMessage.ToString()] = "";
                    Session[appFunctions.Session.ShowMessageType.ToString()] = "";
                }
            }

            objCommon = new clsCommon();
            objCommon.FillDropDownList(ddlCategory, "tblCategory ", tblCategory.ColumnNames.AppCategory, tblCategory.ColumnNames.AppCategoryID, "--Select Category--", tblCategory.ColumnNames.AppCategory, appFunctions.Enum_SortOrderBy.Asc);
            //  objCommon.FillDropDownList(ddlSubCategory, "tblSubCategory ", tblSubCategory.ColumnNames.AppSubCategory, tblSubCategory.ColumnNames.AppSubCategoryID, "--Select Sub Category--", tblSubCategory.ColumnNames.AppSubCategory, appFunctions.Enum_SortOrderBy.Asc);

            objCommon.FillDropDownList(ddlColor, "tblColor ", tblColor.ColumnNames.AppColorName, tblColor.ColumnNames.AppColorID, "--Select Color--", tblColor.ColumnNames.AppColorName, appFunctions.Enum_SortOrderBy.Asc);
            ddlSubCategory.Items.Add(new ListItem("--Select Sub Category--", "0"));
            objCommon.FillRecordPerPage(ref ddlPerPage);
            LoadDataGrid(true, false);
            txtSearch.Focus();
            objCommon = null;
        }

    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon = new clsCommon();
        objCommon.FillDropDownList(ddlSubCategory, "tblSubCategory ", tblSubCategory.ColumnNames.AppSubCategory, tblSubCategory.ColumnNames.AppSubCategoryID, "--Select SubCategory--", tblSubCategory.ColumnNames.AppCategoryID + "=" + ddlCategory.SelectedValue);
        objCommon = null;
        LoadDataGrid(true, false);
    }

    protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDataGrid(true, false);
    }

    protected void ddlColor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDataGrid(true, false);
    }

    private void LoadDataGrid(bool IsResetPageIndex, bool IsSort, string strFieldName = "", string strFieldValue = "")
    {
        objProduct = new tblProduct();
        objDataTable = objProduct.LoadGridData(ddlFields.SelectedValue, txtSearch.Text.Trim(), ddlCategory.SelectedValue, ddlSubCategory.SelectedValue, ddlColor.SelectedValue);
        //'Reset PageIndex of gridviews
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

        //'Check for data into datatable
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
            }

            lblCount.Text = objDataTable.Rows.Count.ToString();
            objDataTable = SortDatatable(objDataTable, ViewState["SortColumn"].ToString(), (appFunctions.Enum_SortOrderBy)ViewState["SortOrder"], IsSort);
            dgvGridView.DataSource = objDataTable;
            dgvGridView.DataBind();
        }

        objProduct = null;
    }

    protected void btnGO_Click(object sender, System.EventArgs e)
    {
        LoadDataGrid(true, false);
    }

    protected void btnReset_Click(object sender, System.EventArgs e)
    {
        txtSearch.Text = "";
        ddlCategory.SelectedIndex = 0;
        ddlSubCategory.Items.Clear();
        ddlSubCategory.Items.Add(new ListItem("--Select Sub Category--", "0"));
        ddlColor.SelectedIndex = 0;
        LoadDataGrid(true, false);
    }

    protected void dgvGridView_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        dgvGridView.PageIndex = e.NewPageIndex;
        LoadDataGrid(false, false);
    }

    protected void dgvGridView_RowCreated(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
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

    protected void dgvGridView_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        DataControlRowType itemType = e.Row.RowType;

        switch (itemType)
        {
            case DataControlRowType.DataRow:
                string strID = dgvGridView.DataKeys[e.Row.RowIndex].Values[0].ToString();
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSelectRow");

                if ((chk != null))
                {
                    chk.Attributes.Add("OnClick", "javascript:SelectRow(this," + strID + ")");
                }


                break;
        }
    }

    protected void dgvGridView_Sorting(object sender, System.Web.UI.WebControls.GridViewSortEventArgs e)
    {
        hdnSelectedIDs.Value = "";
        ViewState["SortColumn"] = e.SortExpression;
        LoadDataGrid(false, true);
    }

    protected void ddlPerPage_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        hdnSelectedIDs.Value = "";
        LoadDataGrid(true, false);
    }

    protected void btnDelete_Click(object sender, System.EventArgs e)
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
            DInfo.ShowMessage("Product has been deleted successfully", Enums.MessageType.Successfull);
            LoadDataGrid(false, false);
        }
        else
        {
            DInfo.ShowMessage("Product is in order so you can't delete them", Enums.MessageType.Error);
        }


        hdnSelectedIDs.Value = "";
    }

    private bool Delete(int intPKID)
    {
        bool retval = false;
        objProduct = new tblProduct();
        if (objProduct.IsProductInOrder(intPKID.ToString()))
        {
            return retval;
        }

        if (objProduct.LoadByPrimaryKey(intPKID))
        {
            tblPixcelCode objPixelCode = new tblPixcelCode();
            objPixelCode.Where.AppProductId.Value = intPKID;
            objPixelCode.Query.Load();
            if (objPixelCode.RowCount > 0)
            {
                objPixelCode.DeleteAll();
                objPixelCode.Save();
            }
            objPixelCode = null;

            tblProductSubCategory objProductSubCategory = new tblProductSubCategory();
            objProductSubCategory.Where.AppProductID.Value = intPKID;
            objProductSubCategory.Query.Load();
            if (objProductSubCategory.RowCount > 0)
            {
                objProductSubCategory.DeleteAll();
                objProductSubCategory.Save();
            }
            objProductSubCategory = null;

            tblRelatedProduct objRelatedProduct = new tblRelatedProduct();
            objRelatedProduct.Where.AppProductID.Value = intPKID;
            objRelatedProduct.Query.Load();
            if (objRelatedProduct.RowCount > 0)
            {
                objRelatedProduct.DeleteAll();
                objRelatedProduct.Save();
            }
            objRelatedProduct = null;

            tblProductProperty objProductProperty = new tblProductProperty();
            objProductProperty.Where.AppProductID.Value = intPKID;
            objProductProperty.Query.Load();
            if (objProductProperty.RowCount > 0)
            {
                objProductProperty.DeleteAll();
                objProductProperty.Save();
            }
            objProductProperty = null;

            tblProductColor objProductColor = new tblProductColor();
            objProductColor.Where.AppProductID.Value = intPKID;
            objProductColor.Query.Load();
            if (objProductColor.RowCount > 0)
            {
                while (!(objProductColor.EOF))
                {
                    tblProductDetail objProductDetail = new tblProductDetail();
                    objProductDetail.Where.AppProductColorID.Value = objProductColor.AppProductColorID;
                    objProductDetail.Query.Load();
                    if (objProductDetail.RowCount > 0)
                    {
                        objProductDetail.DeleteAll();
                        objProductDetail.Save();
                    }
                    objProductDetail = null;

                    tblProductImage objProductImage = new tblProductImage();
                    objProductImage.Where.AppProductColorID.Value = objProductColor.AppProductColorID;
                    objProductImage.Query.Load();
                    if (objProductImage.RowCount > 0)
                    {
                        //while(! objProductImage.EOF )
                        //{
                        //    if (System.IO.File.Exists(Server.MapPath(objProductImage.s_AppLargeImage )))
                        //    {
                        //        System.IO.File.Delete(Server.MapPath(objProductImage.s_AppLargeImage));
                        //    }
                        //    if (System.IO.File.Exists(Server.MapPath(objProductImage.s_AppNormalImage)))
                        //    {
                        //        System.IO.File.Delete(Server.MapPath(objProductImage.s_AppNormalImage));
                        //    }
                        //    if (System.IO.File.Exists(Server.MapPath(objProductImage.s_AppSmallImage)))
                        //    {
                        //        System.IO.File.Delete(Server.MapPath(objProductImage.s_AppSmallImage));
                        //    }
                        //    if (System.IO.File.Exists(Server.MapPath(objProductImage.s_AppThumbImage)))
                        //    {
                        //        System.IO.File.Delete(Server.MapPath(objProductImage.s_AppLargeImage));
                        //    }
                        //    objProductImage.MoveNext();
                        //}
                        objProductImage.DeleteAll();
                        objProductImage.Save();
                    }
                    objProductImage = null;


                    objProductColor.MoveNext();
                }
                objProductColor.DeleteAll();
                objProductColor.Save();
            }
            objProductColor = null;

            string StrFolder = "Uploads/Product/" + objProduct.s_AppProductID + "/";

            if (System.IO.Directory.Exists(Server.MapPath("~/admin/" + StrFolder)))
            {
                System.IO.Directory.Delete(Server.MapPath("~/admin/" + StrFolder), true);
            }

            objProduct.MarkAsDeleted();
            objProduct.Save();
        }

        retval = true;
        objProduct = null;
        return retval;
    }

    protected void dgvGridView_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
        {
            if (e.CommandName == "IsActive")
            {
                objProduct = new tblProduct();
                if (objProduct.LoadByPrimaryKey(Convert.ToInt32(e.CommandArgument.ToString())))
                {
                    if (objProduct.AppIsActive == true)
                    {
                        objProduct.AppIsActive = false;
                    }
                    else if (objProduct.AppIsActive == false)
                    {
                        objProduct.AppIsActive = true;
                    }
                    objProduct.Save();
                    LoadDataGrid(false, false);
                }
                objProduct = null;
            }
            else if (e.CommandName == "QtyEdit")
            {
                hdnPkId.Value = e.CommandArgument.ToString();
                tblProductDetail objProductDetail = new tblProductDetail();
                objDataTable = objProductDetail.GetProductWiseProductDetail(e.CommandArgument.ToString());
                objProductDetail = null;

                if (objDataTable.Rows.Count > 0)
                {
                    dgvProductQtyGridView.DataSource = objDataTable;
                    dgvProductQtyGridView.DataBind();
                    MPEProductQty.Show();
                }
                else
                {
                    DInfo.ShowMessage("Insert Data First in Product.", Enums.MessageType.Error);
                }

            }
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProductDetail.aspx");
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        ExpFile.SetListBoxData(GetAllExportColumns());
        ExpFile.Show();
    }

    public DataTable GetAllExportColumns()
    {
        DataTable objDt = new DataTable();
        objDt.Columns.Add("ColumnName");
        objDt.Columns.Add("Text");

        DataRow dr = objDt.NewRow();
        dr[0] = "appProductCode";
        dr[1] = "Product Code";
        objDt.Rows.Add(dr);

        dr = objDt.NewRow();
        dr[0] = "appSKUNo";
        dr[1] = "SKUNo";
        objDt.Rows.Add(dr);

        dr = objDt.NewRow();
        dr[0] = "appProductName";
        dr[1] = "Product Name";
        objDt.Rows.Add(dr);

        dr = objDt.NewRow();
        dr[0] = "appColorName";
        dr[1] = "Color Name";
        objDt.Rows.Add(dr);

        dr = objDt.NewRow();
        dr[0] = "appColorCode";
        dr[1] = "Color Code";
        objDt.Rows.Add(dr);

        dr = objDt.NewRow();
        dr[0] = "appSize";
        dr[1] = "Size";
        objDt.Rows.Add(dr);

        dr = objDt.NewRow();
        dr[0] = "appSellerPrice";
        dr[1] = "Seller Price";
        objDt.Rows.Add(dr);

        dr = objDt.NewRow();
        dr[0] = "appMRP";
        dr[1] = "MRP";
        objDt.Rows.Add(dr);

        dr = objDt.NewRow();
        dr[0] = "appPrice";
        dr[1] = "Price";
        objDt.Rows.Add(dr);

        dr = objDt.NewRow();
        dr[0] = "appQuantity";
        dr[1] = "Quantity";
        objDt.Rows.Add(dr);

        //dr = objDt.NewRow();
        //dr[0] = "appIsNewArrival";
        //dr[1] = "New Arrival";
        //objDt.Rows.Add(dr);

        //dr = objDt.NewRow();
        //dr[0] = "appIsBestSeller";
        //dr[1] = "Is BestSeller";
        //objDt.Rows.Add(dr);

        //dr = objDt.NewRow();
        //dr[0] = "appIsFeatured";
        //dr[1] = "Is Featured";
        //objDt.Rows.Add(dr);

        //dr = objDt.NewRow();
        //dr[0] = "appIsColor";
        //dr[1] = "Is Color";
        //objDt.Rows.Add(dr);

        //dr = objDt.NewRow();
        //dr[0] = "appIsSize";
        //dr[1] = "Is Size";
        //objDt.Rows.Add(dr);

        dr = objDt.NewRow();
        dr[0] = "appEstimatedDeliveryDays";
        dr[1] = "Estimated Delivery Days";
        objDt.Rows.Add(dr);

        dr = objDt.NewRow();
        dr[0] = "appDescription";
        dr[1] = "Description";
        objDt.Rows.Add(dr);

        dr = objDt.NewRow();
        dr[0] = "appMetaKeyWord";
        dr[1] = "Meta KeyWord";
        objDt.Rows.Add(dr);

        dr = objDt.NewRow();
        dr[0] = "appMetaDescription";
        dr[1] = "Meta Description";
        objDt.Rows.Add(dr);
        return objDt;
    }

    protected void btnSetExportData_Click(object sender, EventArgs e)
    {
        objProduct = new tblProduct();
        objDataTable = objProduct.ExportAllInfoProduct(ddlFields.SelectedValue, txtSearch.Text.Trim(), ddlCategory.SelectedValue, ddlSubCategory.SelectedValue, ddlColor.SelectedValue, hdnSelectedIDs.Value.ToString().TrimEnd(','));
        objProduct = null;
        ExpFile.SetFileName("Product");
        ExpFile.SetExportData(objDataTable);
    }

    protected void btnUpdateQty_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in dgvProductQtyGridView.Rows)
        {
            int intProductDetailID = Convert.ToInt32(dgvProductQtyGridView.DataKeys[row.RowIndex][0].ToString());
            TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
            if (txtQuantity != null)
            {
                if (txtQuantity.Text != "")
                {
                    tblProductDetail objProductDetail = new tblProductDetail();
                    if (objProductDetail.LoadByPrimaryKey(intProductDetailID))
                    {
                        objProductDetail.s_AppQuantity = txtQuantity.Text;
                        objProductDetail.Save();
                    }
                    objProductDetail = null;
                }
            }
        }
        DInfo.ShowMessage("Quantity is updated successfully.", Enums.MessageType.Successfull);

    }

}