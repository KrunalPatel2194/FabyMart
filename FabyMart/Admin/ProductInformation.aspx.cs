using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
public partial class Admin_ProductInformation : PageBase_Admin
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
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
        }
    }
    private void SetValuesToControls()
    {
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            tblProduct objproduct = new tblProduct();
            if (objproduct.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value)))
            {
                lblProductCode.Text = objproduct.AppProductCode;
                lblProductName.Text = objproduct.AppProductName;
                lblProductTag.Text = objproduct.AppProductTag;
                divDescription.InnerHtml = objproduct.AppDescription;
                if (objproduct.s_AppIsColor != "")
                {
                    if (objproduct.AppIsColor)
                    {
                        lblIsColor.Text = "Yes";
                    }
                }

                if (objproduct.s_AppIsSize != "")
                {
                    if (objproduct.AppIsSize)
                    {
                        lblIsSize.Text = "Yes";
                    }
                }


                divMetaKeyWord.InnerHtml = objproduct.AppMetaKeyWord;
                divMetaDescription.InnerHtml = objproduct.AppMetaDescription;


                tblSubCategory objSubCategory = new tblSubCategory();
                objDataTable = objSubCategory.GetAllCategoryProductWise(objproduct.s_AppProductID);
                dgvCategory.DataSource = null;
                dgvCategory.DataBind();
                if (objDataTable.Rows.Count > 0)
                {
                    divCategory.Style.Add("display", "block");
                    dgvCategory.DataSource = objDataTable;
                    dgvCategory.DataBind();
                }
                else
                {
                    divCategory.Style.Add("display", "none");
                }
                objSubCategory = null;

                tblProductColor objProductColor = new tblProductColor();
                objDataTable = objProductColor.LoadGridData(objproduct.s_AppProductID, "0");
                dgvColor.DataSource = null;
                dgvColor.DataBind();
                if (objDataTable.Rows.Count > 0)
                {
                    divColor.Style.Add("display", "block");
                    dgvColor.DataSource = objDataTable;
                    dgvColor.DataBind();
                }
                else
                {
                    divColor.Style.Add("display", "none");
                }
                objProductColor = null;

                tblRelatedProduct objRelatedProduct = new tblRelatedProduct();
                objDataTable = objRelatedProduct.LoadRelatedProduct(objproduct.s_AppProductID);
                dgvRelatedProduct.DataSource = null;
                dgvRelatedProduct.DataBind();
                if (objDataTable.Rows.Count > 0)
                {
                    divRelatedProduct.Style.Add("display", "block");
                    dgvRelatedProduct.DataSource = objDataTable;
                    dgvRelatedProduct.DataBind();
                }
                else
                {
                    divRelatedProduct.Style.Add("display", "none");
                }
                objRelatedProduct = null;

                tblProductProperty objProductProperty = new tblProductProperty();

                objDataTable = objProductProperty.LoadGridData(hdnPKID.Value);
                dgvProperty.DataSource = null;
                dgvProperty.DataBind();
                if (objDataTable.Rows.Count > 0)
                {
                    divProperty.Style.Add("display", "block");
                    dgvProperty.DataSource = objDataTable;
                    dgvProperty.DataBind();
                }
                else
                {
                    divProperty.Style.Add("display", "none");
                }
                objProductProperty = null;


            }
            objproduct = null;
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Product.aspx", true);
    }

    protected void dgvCategory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataControlRowType itemType = e.Row.RowType;

        switch (itemType)
        {
            case DataControlRowType.DataRow:
                string strID = dgvCategory.DataKeys[e.Row.RowIndex].Values[0].ToString();
                Label lbl = (Label)e.Row.FindControl("lblCategory");

                tblSubCategory objSubCategory = new tblSubCategory();

                objCommon = new clsCommon();
                lbl.Text = ": " + objCommon.JoinWithComma(objSubCategory.GetAllSubCategoryProductWise(hdnPKID.Value, strID), tblSubCategory.ColumnNames.AppSubCategory, Enums.Enum_DataType.sString);
                objCommon = null;

                objSubCategory = null;
                break;
        }
    }

    protected void dgvColor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataControlRowType itemType = e.Row.RowType;

        switch (itemType)
        {
            case DataControlRowType.DataRow:
                string strID = dgvColor.DataKeys[e.Row.RowIndex].Values[0].ToString();
                Repeater RepImg = (Repeater)e.Row.FindControl("RepImg");
                tblProductImage objProductImage = new tblProductImage();
                objDataTable = objProductImage.LoadGridData(strID);
                RepImg.DataSource = objDataTable;
                RepImg.DataBind();
                objProductImage = null;

                System.Web.UI.HtmlControls.HtmlGenericControl divSize = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Row.FindControl("divSize");
                GridView dgvsize = (GridView)e.Row.FindControl("dgvSize");


                tblProductDetail objProductDetail = new tblProductDetail();
                objDataTable = objProductDetail.LoadGridData(hdnPKID.Value, strID);
                dgvsize.DataSource = null;
                dgvsize.DataBind();
                if (objDataTable.Rows.Count > 0)
                {
                    divSize.Style.Add("display", "block");
                    dgvsize.DataSource = objDataTable;
                    dgvsize.DataBind();
                }
                else
                {
                    divSize.Style.Add("display", "none");
                }
                objProductDetail = null;



                break;
        }
    }

    protected void dgvProperty_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataControlRowType itemType = e.Row.RowType;

        switch (itemType)
        {
            case DataControlRowType.DataRow:
                bool IsPreDefine = Convert.ToBoolean(dgvProperty.DataKeys[e.Row.RowIndex].Values[1].ToString());
                string strPropertyId = dgvProperty.DataKeys[e.Row.RowIndex].Values[0].ToString();
                string strId = dgvProperty.DataKeys[e.Row.RowIndex].Values[2].ToString();
                Label lbl = (Label)e.Row.FindControl("lblValue");
                if (IsPreDefine)
                {
                    if (strId != "")
                    {
                        tblPropertyPreValue objPropertyPreValue = new tblPropertyPreValue();
                        if (objPropertyPreValue.LoadByPrimaryKey(Convert.ToInt32(strId)))
                        {
                            lbl.Text = objPropertyPreValue.s_AppPreValue;
                        }
                        objPropertyPreValue = null;
                    }

                }


                break;
        }
    }
}