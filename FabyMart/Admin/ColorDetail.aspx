<%@ Page Title="Color Detail" Language="C#" MasterPageFile="~/admin/Main.master"
    AutoEventWireup="true" CodeFile="ColorDetail.aspx.cs" Inherits="ColorDetail" %>

<%@ Register Src="~/Admin/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Color Detail
                </div>
                <div>
                    <div class="panel-search right-content">
                        <asp:Button ID="btnBack" runat="server" CssClass="btn btn-primary" Text="Back" OnClick="btnBack_Click"
                            TabIndex="8" />&nbsp;
                        <div class="btn-group">
                            <asp:Button runat="server" ID="btnSave" class="btn btn-primary" OnClick="btnSave_Click"
                                Text="Save" ValidationGroup="save" TabIndex="5"></asp:Button>
                            <button data-toggle="dropdown" class="btn btn-primary dropdown-toggle down-arrow">
                            </button>
                            <ul class="dropdown-menu">
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkSaveAndClose" OnClick="lnkSaveAndClose_Click"
                                        Text="Save & Close" ValidationGroup="save" TabIndex="6"></asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkSaveAndAddnew" OnClick="lnkSaveAndAddnew_Click"
                                        Text="Save & Add New" ValidationGroup="save" TabIndex="7"></asp:LinkButton></li>
                            </ul>
                        </div>
                    </div>
                    <hr class="nomargin" />
                    <DInfo:DisplayInfo runat="server" ID="DInfo" />
                    <div class="panel-search">
                        <div class="table-responsive">
                            <div class="entryformmain">
                                <%--<div class="entryform">
                                    <div class="labelstyle">
                                        <span class="mandatory">*</span> Color :
                                    </div>
                                    <div class="controlstyle">
                                        <asp:TextBox ID="txtColor" CssClass="form-control" Width="200" runat="server" TabIndex="1"
                                          ></asp:TextBox>
                                          <input type="color" class="btn btn" id="btnColor" onchange="javascript:document.getElementById('<%= txtColor.ClientID%>').value=document.getElementById('btnColor').value; javascript:document.getElementById('<%= txtColor.ClientID%>').style.backgroundColor=document.getElementById('btnColor').value;"/>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="RFVColor" runat="server" ErrorMessage="Enter Color"
                                            ValidationGroup="save" Text="*" ControlToValidate="txtColor" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="REColorCode" runat="server" ErrorMessage="Color Code"
                                            ValidationGroup="save" Text="*" ControlToValidate="txtColor" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle" ValidationExpression="^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$"></asp:RegularExpressionValidator>
                                    </div>
                                </div>--%>
                                
                                <div class="entryform">
                                    <div class="labelstyle">
                                        <span class="mandatory">*</span> Color Name:
                                    </div>
                                    <div class="controlstyle">
                                        <asp:TextBox ID="txtColorName" CssClass="form-control" Width="200" runat="server"
                                            TabIndex="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="RFVColorName" runat="server" ErrorMessage="Enter Color Name"
                                            ValidationGroup="save" Text="*" ControlToValidate="txtColorName" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="entryform">
                                    <div class="labelstyle">
                                        <span class="mandatory">*</span> Color :
                                    </div>
                                    <div class="controlstyle">
                                        <asp:FileUpload ID="fileUploadColorImg" runat="server" />
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="RFVfileUploadColorImg" runat="server"
                                            ErrorMessage="Choose Color Image" ValidationGroup="Career" Text="*" ControlToValidate="fileUploadColorImg"
                                            SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                 <div class="entryform">
                                    <div class="entryform">
                                        <div class="labelstyle">
                                        </div>
                                        <div class="controlstyle">
                                            <asp:Image ID="img" runat="server" Width="50" />
                                        </div>
                                    </div>
                                </div>
                                <div class="entryform">
                                    <div class="labelstyle">
                                        Is Default :
                                    </div>
                                    <div class="controlstyle">
                                        <asp:CheckBox ID="chkIsDefault" runat="server" Checked="false" TabIndex="3" />
                                    </div>
                                </div>
                                <div class="entryform">
                                    <div class="labelstyle">
                                        Is Active :
                                    </div>
                                    <div class="controlstyle">
                                        <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" TabIndex="4" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div>
                <asp:HiddenField ID="hdnPKID" Value="" runat="server" />
                <asp:ValidationSummary ID="vsSearch" ShowMessageBox="true" EnableClientScript="true"
                    HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                    ValidationGroup="save" />
            </div>
        </div>
    </div>
</asp:Content>
