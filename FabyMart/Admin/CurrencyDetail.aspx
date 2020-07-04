<%@ Page Title="Currency Detail" Language="C#" MasterPageFile="~/admin/Main.master"
    AutoEventWireup="true" CodeFile="CurrencyDetail.aspx.cs" Inherits="CurrencyDetail" %>

<%@ Register Src="~/Admin/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Currency Detail
                </div>
                <div>
                    <div class="panel-search right-content">
                        <asp:Button ID="btnBack" runat="server" CssClass="btn btn-primary" Text="Back" OnClick="btnBack_Click" />&nbsp;
                        <div class="btn-group">
                            <asp:Button runat="server" ID="btnSave" class="btn btn-primary" OnClick="btnSave_Click"
                                Text="Save" ValidationGroup="save"></asp:Button>
                            <button data-toggle="dropdown" class="btn btn-primary dropdown-toggle down-arrow">
                            </button>
                            <ul class="dropdown-menu">
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkSaveAndClose" OnClick="lnkSaveAndClose_Click"
                                        Text="Save & Close" ValidationGroup="save"></asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkSaveAndAddnew" OnClick="lnkSaveAndAddnew_Click"
                                        Text="Save & Add New" ValidationGroup="save"></asp:LinkButton></li>
                            </ul>
                        </div>
                    </div>
                    <hr class="nomargin" />
                    <DInfo:DisplayInfo runat="server" ID="DInfo" />
                    <div class="panel-search">
                        <div class="table-responsive">
                            <div class="entryformmain">
                                <div class="entryform">
                                    <div class="labelstyle">
                                        <span class="mandatory">*</span> Currency Name :
                                    </div>
                                    <div class="controlstyle">
                                        <asp:TextBox ID="txtCurrencyName" CssClass="form-control" Width="200" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="RFVCurrencyName" runat="server"
                                            ErrorMessage="Enter Currency Name" ValidationGroup="save" Text="*" ControlToValidate="txtCurrencyName"
                                            SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="entryform">
                                    <div class="labelstyle">
                                        <span class="mandatory">*</span> Currency Code :
                                    </div>
                                    <div class="controlstyle">
                                        <asp:TextBox ID="txtCurrencyCode" CssClass="form-control" Width="200" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="RFVCurrencyCode" runat="server"
                                            ErrorMessage="Enter Currency Code" ValidationGroup="save" Text="*" ControlToValidate="txtCurrencyCode"
                                            SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="entryform">
                                    <div class="labelstyle">
                                        <span class="mandatory">*</span> Rate :
                                    </div>
                                    <div class="controlstyle">
                                        <asp:TextBox ID="txtRate" CssClass="form-control" Width="200" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="RFVRate" runat="server" ErrorMessage="Enter Rate"
                                            ValidationGroup="save" Text="*" ControlToValidate="txtRate" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator Display="Dynamic" ID="REVRate" runat="server" ValidationGroup="save"
                                            Text="*" ControlToValidate="txtRate" SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="entryform">
                                    <div class="labelstyle">
                                        <span class="mandatory">*</span> Symbol :
                                    </div>
                                    <div class="controlstyle">
                                        <asp:TextBox ID="txtSymbol" CssClass="form-control" Width="200" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="RFVSymbol" runat="server" ErrorMessage="Enter Symbol"
                                            ValidationGroup="save" Text="*" ControlToValidate="txtSymbol" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
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
