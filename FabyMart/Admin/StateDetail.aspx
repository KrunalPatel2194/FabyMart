<%@ Page Title="StateDetail" Language="C#" MasterPageFile="~/admin/Main.master" AutoEventWireup="true"
    CodeFile="StateDetail.aspx.cs" Inherits="StateDetail" %>

<%@ Register Src="~/Admin/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    State Detail
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
                                                <span class="mandatory">*</span>  Country :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RFVddlCountry" runat="server" CssClass="ErrorLabelStyle"
                                                    Display="Dynamic" ErrorMessage="Select Country" SetFocusOnError="True" ValidationGroup="save"
                                                    Text="*" InitialValue="0" ControlToValidate="ddlCountry"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                <span class="mandatory">*</span> State :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:TextBox ID="txtState" CssClass="form-control" Width="200" runat="server" TabIndex="1"></asp:TextBox>
                                                <asp:RequiredFieldValidator Display="Dynamic" ID="RFVState" runat="server" ErrorMessage="Enter State"
                                                    ValidationGroup="save" Text="*" ControlToValidate="txtState" SetFocusOnError="true"
                                                    CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                            </div>
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
                <asp:HiddenField ID="hdnPhoto" runat="server" />
                <asp:HiddenField ID="hdnImage" runat="server" />
                <asp:ValidationSummary ID="vsSearch" ShowMessageBox="true" EnableClientScript="true"
                    HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                    ValidationGroup="save" />
            </div>
        </div>
</asp:Content>
