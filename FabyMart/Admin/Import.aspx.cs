using System;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;

public partial class Import : PageBase_Admin
{
    clsCommon objCommon;
    OleDbConnection objOleDbConn;
    string strFileName = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ResetControl();
            FileLead.Focus();
        }
    }
    public void ResetControl()
    {
        btnSave.Visible = false;
        btnUpload.Visible = true;
        dgvGridData.DataSource = null;
        dgvGridData.DataBind();
        if (Session[appFunctions.Session.LeadFiled.ToString()] != null)
        {
            Session[appFunctions.Session.LeadFiled.ToString()] = "";
        }
        if (Session[appFunctions.Session.LeadDataTable.ToString()] != null)
        {
            Session[appFunctions.Session.LeadDataTable.ToString()] = null;
        }

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ResetControl();
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (ShowExcelToDataGrid())
        {
            btnUpload.Visible = false;
            btnSave.Visible = true;
            //  btnSave.Visible = IsAdd ;
        }
        else
        {
            ResetControl();
        }
    }

    public Boolean ShowExcelToDataGrid()
    {
        bool IsSuccess = true;
        if (FileLead.HasFile)
        {
            //try
            //{

            string strError = "";
            objCommon = new clsCommon();
            strFileName = Session[appFunctions.Session.UserID.ToString()].ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            objCommon.FileUpload_Commomn(FileLead, strFileName, "Uploads/Import/", ".xls,.xlsx", ref strError, 0);
            strFileName = "Uploads/Import/" + strFileName + Path.GetExtension(FileLead.FileName);
            objCommon = null;
            if (strError != "")
            {
                DInfo.ShowMessage(strError, Enums.MessageType.Error);
                return false;
            }

            string strFilePath = System.IO.Path.GetFullPath(Server.MapPath("~/admin/" + strFileName));
            if (strFilePath != "")
            {
                if (Path.GetExtension(strFilePath) == ".xls")
                {
                    objOleDbConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strFilePath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"");
                }
                else if (Path.GetExtension(strFilePath) == ".xlsx")
                {
                    objOleDbConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strFilePath + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';");
                }
                else
                {
                    DInfo.ShowMessage("File Formate not Valid.", Enums.MessageType.Error);
                    IsSuccess= false;
                }

                objOleDbConn.Open();

                OleDbCommand objcmd = new OleDbCommand();
                OleDbDataAdapter objDataAdapter = new OleDbDataAdapter();
                DataSet objDataset = new DataSet();

                dgvGridData.DataSource = null;
                dgvGridData.DataBind();


                objcmd.Connection = objOleDbConn;
                objcmd.CommandType = CommandType.Text;
                objcmd.CommandText = "SELECT * FROM [Sheet1$]";
                objDataAdapter = new OleDbDataAdapter(objcmd);
                objDataAdapter.Fill(objDataset);

                if (objDataset.Tables[0].DefaultView.Table.Rows.Count > 0)
                {

                    int intColumn = objDataset.Tables[0].DefaultView.Table.Columns.Count;
                    if (intColumn > 0)
                    {
                        dgvGridData.DataSource = objDataset.Tables[0];
                        dgvGridData.DataBind();
                        for (int i = 0; i < intColumn; i++)
                        {
                            objDataset.Tables[0].DefaultView.Table.Columns[i].ColumnName = objDataset.Tables[0].DefaultView.Table.Columns[i].ColumnName.ToString().ToLower();
                        }
                    }
                    else
                    {
                        DInfo.ShowMessage("Defind Column name", Enums.MessageType.Error);
                        IsSuccess = false;
                    }
                    Session[appFunctions.Session.LeadDataTable.ToString()] = objDataset.Tables[0].DefaultView.Table.Copy();

                }
                else
                {
                    IsSuccess = false;
                }
                objDataset.Clear();
                objcmd.Dispose();
                objOleDbConn.Close();

            }
            else
            {
                DInfo.ShowMessage("File Not Exist.", Enums.MessageType.Error);
                IsSuccess = false;
            }

            if (strFileName != "")
            {
                if (System.IO.File.Exists(Server.MapPath(strFileName)))
                {
                    System.IO.File.Delete(Server.MapPath(strFileName));
                }
                strFileName = "";
            }

        }
        else
        {
            DInfo.ShowMessage("Choose Product Data File.", Enums.MessageType.Error);
            IsSuccess = false;
        }
        return IsSuccess;
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveLeadData())
        {
            DInfo.ShowMessage("Data Inserted From File.", Enums.MessageType.Successfull);
            ResetControl();
        }

    }

    public Boolean SaveLeadData()
    {

        DataTable objDataTable = new DataTable();
        if (Session[appFunctions.Session.LeadDataTable.ToString()] != null)
        {
            objDataTable = (DataTable)Session[appFunctions.Session.LeadDataTable.ToString()];
        }
        if (objDataTable.Rows.Count <= 0)
        {
            DInfo.ShowMessage("Some Error Occure Try again!.", Enums.MessageType.Error);
            return false;
        }


        objCommon = new clsCommon();
        foreach (DataRow row in objDataTable.Rows)
        {
            tblProduct objProduct = new tblProduct();
            objProduct.Where.AppProductName.Value = row[Enums.Enums_ProductHeader.Product_Name.ToString().Replace("_", " ").Trim().ToLower()].ToString();
            objProduct.Query.Load();
            if (objProduct.RowCount <= 0)
            {
                objProduct.AddNew();
            }
            objProduct.s_AppProductName = row[Enums.Enums_ProductHeader.Product_Name.ToString().Replace("_", " ").Trim().ToLower()].ToString();
            objProduct.s_AppProductCode = row[Enums.Enums_ProductHeader.Product_Code.ToString().Replace("_", " ").Trim().ToLower()].ToString();
            objProduct.s_AppDescription = row[Enums.Enums_ProductHeader.Description.ToString().Replace("_", " ").Trim().ToLower()].ToString();
            //objProduct.s_AppProductTag = row[Enums.Enums_ProductHeader.Description.ToString().Replace("_", " ").Trim().ToLower()].ToString();
            objProduct.s_AppMetaKeyWord = row[Enums.Enums_ProductHeader.Meta_KeyWord.ToString().Replace("_", " ").Trim().ToLower()].ToString();
            objProduct.s_AppMetaDescription = row[Enums.Enums_ProductHeader.Meta_Description.ToString().Replace("_", " ").Trim().ToLower()].ToString();
            objProduct.AppIsFeatured = false;
            objProduct.AppIsNewArrival = false;
            objProduct.AppIsBestSeller = false;
            objProduct.AppIsColor = true;
            objProduct.AppIsSize = true;
            objProduct.Save();

            //Color Add or Modify

            string strColorId = GetColorId(row[Enums.Enums_ProductHeader.Color_Name.ToString().Replace("_", " ").Trim().ToLower()].ToString());
            tblProductColor objProductColor = new tblProductColor();
            objProductColor.Where.AppProductID.Value = objProduct.s_AppProductID;
            objProductColor.Where.AppColorID.Value = strColorId;
            objProductColor.Query.Load();
            if (objProductColor.RowCount <= 0)
            {
                objProductColor.AddNew();
                objProductColor.AppDisplayOrder = objCommon.GetNextDisplayOrder("tblProductColor", tblProductColor.ColumnNames.AppDisplayOrder, tblProductColor.ColumnNames.AppProductID + "=" + objProduct.s_AppProductID);
            }
            objProductColor.s_AppColorID = strColorId;
            objProductColor.s_AppProductID = objProduct.s_AppProductID;


            if (objProductColor.AppDisplayOrder == 1)
            {
                objProductColor.AppIsDefault = true;
            }
            else
            {
                objProductColor.AppIsDefault = false;
            }
            objProductColor.AppIsActive = true;

            objProductColor.Save();


            //Size Add or Modify
            string strSizeId = GetSizeId(row[Enums.Enums_ProductHeader.Size.ToString().Replace("_", " ").Trim().ToLower()].ToString());

            tblProductDetail objProductDetail = new tblProductDetail();
            objProductDetail.Where.AppProductColorID.Value = objProductColor.s_AppProductColorID;
            objProductDetail.Where.AppSizeID.Value = strSizeId;
            objProductDetail.Query.Load();
            if (objProductDetail.RowCount <= 0)
            {
                objProductDetail.AddNew();
            }
            objProductDetail.s_AppProductColorID = objProductColor.s_AppProductColorID;
            //objProductDetail.s_AppSellerPrice = row[Enums.Enums_ProductHeader.Seller_Price.ToString().Replace("_", " ").Trim().ToLower()].ToString();
            objProductDetail.s_AppMRP = row[Enums.Enums_ProductHeader.MRP.ToString().Replace("_", " ").Trim().ToLower()].ToString();
            objProductDetail.s_AppPrice = row[Enums.Enums_ProductHeader.Price.ToString().Replace("_", " ").Trim().ToLower()].ToString();
            objProductDetail.s_AppQuantity = row[Enums.Enums_ProductHeader.Quantity.ToString().Replace("_", " ").Trim().ToLower()].ToString();
            objProductDetail.AppSKUNo = row[Enums.Enums_ProductHeader.SKU_No.ToString().Replace("_", " ").Trim().ToLower()].ToString();
            objProductDetail.s_AppSizeID = strSizeId;
            objProductDetail.AppIsDefault = false;
            tblProductDetail objTempmg = new tblProductDetail();
            objTempmg.Where.AppProductColorID.Value = strSizeId;
            objTempmg.Query.Load();
            if (objTempmg.RowCount <= 0)
            {
                objProductDetail.AppIsDefault = true;
            }
            objTempmg = null;

            objProductDetail.Save();

            objProductDetail = null;


            string StrFolder = "Uploads/Product/" + objProduct.s_AppProductID + "/";
            string strImgName = objProduct.s_AppProductName.Trim().Replace(" ", "_") + "_" + row[Enums.Enums_ProductHeader.Color_Name.ToString().Replace("_", " ").Trim().ToLower()].ToString().Replace(" ", "_");

            if (!(System.IO.Directory.Exists(Server.MapPath("~/admin/" + StrFolder))))
            {
                System.IO.Directory.CreateDirectory(Server.MapPath("~/admin/" + StrFolder));
            }
            for (int i = 1; i <= 4; i++)
            {
                string ImgPath = row[Enums.Enums_ProductHeader.Image.ToString().Replace("_", " ").Trim().ToLower() + " " + i.ToString()].ToString();

                try
                {
                    System.Drawing.Bitmap upBmp = (System.Drawing.Bitmap)System.Drawing.Image.FromFile(ImgPath);

                    if (row[Enums.Enums_ProductHeader.Image.ToString().Replace("_", " ").Trim().ToLower() + " " + i.ToString()].ToString() != "")
                    {
                        tblProductImage objProductImg = new tblProductImage();
                        objProductImg.AddNew();
                        objProductImg.s_AppProductColorID = objProductColor.s_AppProductColorID;
                        objProductImg.AppDisplayOrder = objCommon.GetNextDisplayOrder("tblProductImage", tblProductImage.ColumnNames.AppDisplayOrder, tblProductImage.ColumnNames.AppProductColorID + "=" + objProductColor.s_AppProductColorID);
                        string strError = "";
                        string Time = Convert.ToString(DateTime.Now.Month) + Convert.ToString(DateTime.Now.Day) + Convert.ToString(DateTime.Now.Year) + Convert.ToString(DateTime.Now.Hour) + Convert.ToString(DateTime.Now.Minute) + Convert.ToString(DateTime.Now.Second) + Convert.ToString(DateTime.Now.Millisecond);
                        string strPath = objCommon.ResizeDirectImagesFile(ImgPath, strImgName + "_" + Time + "_Thumb", 0, ref strError, StrFolder, 0, 91);
                        if (strError == "")
                        {
                            objProductImg.s_AppThumbImage = strPath;
                        }
                        strError = "";
                        strPath = objCommon.ResizeDirectImagesFile(ImgPath, strImgName + "_" + Time + "_Normal", 0, ref strError, StrFolder, 0, 300);
                        if (strError == "")
                        {
                            objProductImg.s_AppNormalImage = strPath;
                        }
                        strError = "";
                        strPath = objCommon.ResizeDirectImagesFile(ImgPath, strImgName + "_" + Time + "_Large", 0, ref strError, StrFolder, 0, 900);
                        if (strError == "")
                        {
                            objProductImg.s_AppLargeImage = strPath;
                        }
                        strError = "";
                        strPath = objCommon.ResizeDirectImagesFile(ImgPath, strImgName + "_" + Time + "_Small", 0, ref strError, StrFolder, 0, 210);
                        if (strError == "")
                        {
                            objProductImg.s_AppSmallImage = strPath;
                        }
                        objProductImg.AppIsActive = true;
                        objProductImg.AppIsDefault = false;

                        if (objProductImg.AppDisplayOrder == 1)
                        {
                            objProductImg.AppIsActive = true;
                            objProductImg.AppIsDefault = true;
                        }
                        objProductImg.Save();
                        objProductImg = null;
                    }
                }
                catch (Exception ex)
                {
                }

            }
            objProductColor = null;

            objProduct = null;
            //objlead.s_appcampaignid = row["appcampaignid"].tostring();

        }
        objCommon = null;
        objDataTable = null;
        return true;

    }

    public string GetColorId(string strColorName)
    {
        string StrColor = "";
        tblColor objColor = new tblColor();
        objColor.Where.AppColorName.Value = strColorName;
        objColor.Query.Load();
        if (objColor.RowCount <= 0)
        {
            objColor.AddNew();
            objColor.s_AppColorName = strColorName;
            objColor.AppColorCode = "#FFF";
            objColor.AppIsActive = true;
            objColor.AppIsDefault = false;
            objCommon = new clsCommon();
            objColor.AppDisplayOrder = objCommon.GetNextDisplayOrder("tblColor", tblColor.ColumnNames.AppDisplayOrder);
            objCommon = null;
            objColor.Save();
        }

        StrColor = objColor.s_AppColorID;
        objColor = null;

        return StrColor;
    }

    public string GetSizeId(string strSizeName)
    {
        string StrSize = "";
        tblSize objSize = new tblSize();
        objSize.Where.AppSize.Value = strSizeName;
        objSize.Query.Load();
        if (objSize.RowCount <= 0)
        {
            objSize.AddNew();
            objSize.s_AppSize = strSizeName;
            objSize.AppIsActive = true;
            objSize.AppIsDefault = false;

            objSize.Save();
        }

        StrSize = objSize.s_AppSizeID;
        objSize = null;

        return StrSize;
    }

    protected void btnGenerateFile_Click(object sender, EventArgs e)
    {
        string path = Server.MapPath("~/admin/Uploads/Exportedfiles").ToString() + "\\";
        if (!Directory.Exists(path))   // CHECK IF THE FOLDER EXISTS. IF NOT, CREATE A NEW FOLDER.
        {
            Directory.CreateDirectory(path);
        }
        string strName = "ExportTemplate.xls";
        if (File.Exists(path + strName))
        {
            FileInfo file = new FileInfo(path + strName);
            if (file.Exists)
            {
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.WriteFile(file.FullName);
                Response.End();
            }
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
    }
}