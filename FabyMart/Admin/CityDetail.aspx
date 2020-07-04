<%@ Page Title="CityDetail" Language="C#" MasterPageFile="~/admin/Main.master" AutoEventWireup="true"
    CodeFile="CityDetail.aspx.cs" Inherits="CityDetail" %>

<%@ Register Src="~/Admin/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    City Detail
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
                            <asp:UpdatePanel ID="UpCountry" runat="server">
                                <ContentTemplate>
                                    <div class="entryformmain">
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                <span class="mandatory">*</span> Select Country :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RFVddlCountry" runat="server" CssClass="ErrorLabelStyle"
                                                    Display="Dynamic" ErrorMessage="Select Country" SetFocusOnError="True" ValidationGroup="save"
                                                    Text="*" InitialValue="0" ControlToValidate="ddlCountry"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                <span class="mandatory">*</span> Select State :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RFVState" runat="server" CssClass="ErrorLabelStyle"
                                                    Display="Dynamic" ErrorMessage="Select State" SetFocusOnError="True" ValidationGroup="save"
                                                    Text="*" InitialValue="0" ControlToValidate="ddlState"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                <span class="mandatory">*</span> City :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:TextBox ID="txtCity" CssClass="form-control" Width="200" runat="server" TabIndex="1"></asp:TextBox>
                                                <asp:RequiredFieldValidator Display="Dynamic" ID="RFVCity" runat="server" ErrorMessage="Enter City"
                                                    ValidationGroup="save" Text="*" ControlToValidate="txtCity" SetFocusOnError="true"
                                                    CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
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
</asp:Content>
