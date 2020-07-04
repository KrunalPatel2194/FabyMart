<%@ Page Title="UploadExcelsDetail" Language="C#" MasterPageFile="~/admin/Main.master"
    AutoEventWireup="true" CodeFile="UploadExcels.aspx.cs" Inherits="UploadExcels" %>

<%@ Register Src="~/Admin/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Upload Pincode Detail
                </div>
                <div>
                   <%-- <div class="panel-search right-content">
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
                    </div>--%>
                   <%-- <hr class="nomargin" />--%>
                    <DInfo:DisplayInfo runat="server" ID="DInfo" />
                    <div class="panel-search">
                        <div class="table-responsive">
                           <%-- <asp:UpdatePanel ID="UpCountry" runat="server">
                                <ContentTemplate>
                           --%>         <%--<div class="entryformmain">
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                <span class="mandatory"></span>Excel Has Header ? :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:RadioButtonList ID="rbHDR" runat="server">
                                                    <asp:ListItem Text="Yes" Value="Yes" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                </asp:RadioButtonList>
                                                <%--<asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="ErrorLabelStyle"
                                                    Display="Dynamic" ErrorMessage="Select Country" SetFocusOnError="True" ValidationGroup="save"
                                                    Text="*" InitialValue="0" ControlToValidate="ddlCountry"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>--%>
                                    <div class="entryformmain">
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                <span class="mandatory">*</span> Select Excel :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:FileUpload ID="FileData" runat="server" />
                                                <br />
                                                <asp:Button runat="server" ID="btnUpload" class="btn btn-primary" Text="Upload File"
                                                    ValidationGroup="save" OnClick="btnUpload_Click"></asp:Button>
                                                <asp:RequiredFieldValidator ID="RFVFileUpload" runat="server" CssClass="ErrorLabelStyle"
                                                    Display="Dynamic" ErrorMessage="Select Excel File" SetFocusOnError="True" ValidationGroup="save"
                                                    Text="*"  ControlToValidate="FileData"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                              <%--  </ContentTemplate>
                            </asp:UpdatePanel>--%>
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
