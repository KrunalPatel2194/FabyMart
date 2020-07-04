using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.IO;
using System.Drawing;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Xml.Linq;
using System.Collections;
using System.Collections.Specialized;
public partial class UploadExcels: PageBase_Admin
{
    tblPinCode objPinCode;
    clsCommon objClsCommon;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        
        }
    }

    private void ResetControls()
    {
        hdnPKID.Value = "";
    }


    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (FileData.HasFile)
        {
            string FileName = Path.GetFileName(FileData.PostedFile.FileName);
            string Extension = Path.GetExtension(FileData.PostedFile.FileName);
            string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
            string FilePath = Server.MapPath(FolderPath + FileName);
            FileData.SaveAs(FilePath);
            Import_To_Grid(FilePath, Extension);

        }
        else
        {
            DInfo.ShowMessage("Please Select Excel File..", Enums.MessageType.Error);
        }
    }
    private void Import_To_Grid(string FilePath, string Extension)
    {
        string conStr = "";
        switch (Extension)
        {
            case ".xls": //Excel 97-03
                conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                break;
            case ".xlsx": //Excel 07
                conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                break;
        }

        conStr = String.Format(conStr, FilePath);
        OleDbConnection connExcel = new OleDbConnection(conStr);
        OleDbCommand cmdExcel = new OleDbCommand();
        OleDbDataAdapter oda = new OleDbDataAdapter();
        DataTable dt = new DataTable();
        cmdExcel.Connection = connExcel;

        //Get the name of First Sheet
        connExcel.Open();
        DataTable dtExcelSchema;
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
        connExcel.Close();

        //Read Data from First Sheet
        connExcel.Open();
        cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
        oda.SelectCommand = cmdExcel;
        oda.Fill(dt);
        connExcel.Close();
        getAllDetail(dt);
    }
    void getAllDetail(DataTable objDt)
    {
        string strmessage = "";
        string strXML = new XElement("Countries",
                 from empList in objDt.AsEnumerable()
                 select new XElement("Country",
                        new XElement("PINCODE", empList.Field<double>("PINCODE")),
                        new XElement("ControlingStation", empList.Field<string>("Controling Station")),
                        new XElement("LocationCode", empList.Field<string>("Location Code")),
                        new XElement("DefaultCity", empList.Field<string>("Default City")),
                        new XElement("CITYNAME", empList.Field<string>("CITY NAME")),
                        new XElement("STATE_UNIONTERRITORY", empList.Field<string>("STATE/UNION TERRITORY")),
                        new XElement("Zone", empList.Field<string>("Zone")),
                        new XElement("Region", empList.Field<string>("Region")),
                        new XElement("EXP", empList.Field<string>("EXP")),
                        new XElement("ToPay", empList.Field<string>("To Pay")),
                        new XElement("COD", empList.Field<string>("COD"))
                 )).ToString();
        objPinCode = new tblPinCode();
        strmessage=objPinCode.InsertCountriesXMLData(strXML);
        if (strmessage != "")
        {
            DInfo.ShowMessage(strmessage,Enums.MessageType.Successfull);
        }
        else
        {
            DInfo.ShowMessage("Data Not Save..", Enums.MessageType.Warning);
        }
        objPinCode = null;
    
    }

}