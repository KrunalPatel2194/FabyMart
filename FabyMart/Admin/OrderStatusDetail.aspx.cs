using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.IO;
using System.Drawing;


public partial class OrderStatusDetail : PageBase_Admin
{
    tblOrderStatus objOrderStatus;
    int intPkId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            lnkSaveAndAddnew.Visible = HasAdd;

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



    private bool SaveData()
    {
        objCommon = new clsCommon();
        if (objCommon.IsRecordExists("tblOrderStatus", tblOrderStatus.ColumnNames.AppOrderStatus, tblOrderStatus.ColumnNames.AppOrderStatusID, txtOrderStatus.Text, hdnPKID.Value))
        {
            DInfo.ShowMessage("Order Status alredy exits.", Enums.MessageType.Error);
            return false;
        }
        objOrderStatus = new tblOrderStatus();
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objOrderStatus.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value));
        }
        else
        {
            objOrderStatus.AddNew();
            objOrderStatus.AppDisplayOrder = objCommon.GetNextDisplayOrder("tblOrderStatus", tblOrderStatus.ColumnNames.AppDisplayOrder);
        }
        objOrderStatus.AppOrderStatus = txtOrderStatus.Text;
        objOrderStatus.AppIsActive = chkIsActive.Checked;
        if (chkIsDefault.Checked)
        {
            tblOrderStatus ObjTempcolor = new tblOrderStatus();
            ObjTempcolor.SetDefaultOrderStatus();
            ObjTempcolor = null;
            objOrderStatus.AppIsActive = true;
            objOrderStatus.AppIsDefault = true;
        }
        else
        {
            tblOrderStatus ObjTempcolor = new tblOrderStatus();
            ObjTempcolor.LoadAll();
            if (ObjTempcolor.RowCount <= 0)
            {
                objOrderStatus.AppIsActive = true;
                objOrderStatus.AppIsDefault = true;
            }
            else
            {
                objOrderStatus.AppIsDefault = false;
            }
            ObjTempcolor = null;
        }
        objOrderStatus.Save();
        intPkId = objOrderStatus.AppOrderStatusID;
        objOrderStatus = null;
        objCommon = null;
        return true;
    }

    private void SetValuesToControls()
    {
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objOrderStatus = new tblOrderStatus();
            if (objOrderStatus.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value)))
            {
                
                txtOrderStatus.Text = objOrderStatus.AppOrderStatus;
                chkIsActive.Checked = objOrderStatus.AppIsActive;
                chkIsDefault.Checked = objOrderStatus.AppIsDefault;
                if (chkIsDefault.Checked)
                {
                    chkIsDefault.Enabled = false;
                }
            }
            objOrderStatus = null;
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("OrderStatus.aspx", true);
    }

    private void ResetControls()
    {
     
        txtOrderStatus.Text = "";
        chkIsActive.Checked = true;
        chkIsActive.Checked = false;
        chkIsDefault.Enabled = true ;
        hdnPKID.Value = "";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("Order Status has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("Order Status has been updated successfully", Enums.MessageType.Successfull);
            }
            hdnPKID.Value = intPkId.ToString();
            SetValuesToControls();
        }
    }
    protected void lnkSaveAndClose_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "Order Status has been added successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            else
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "Order Status has been updated successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            Response.Redirect("OrderStatus.aspx");
        }
    }

    protected void lnkSaveAndAddnew_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("Order Status has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("Order Status has been updated successfully", Enums.MessageType.Successfull);
            }
            ResetControls();
        }
    }

}