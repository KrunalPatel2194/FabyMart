<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Main.master" AutoEventWireup="true"
    CodeFile="ExclusiveDetail.aspx.cs" Inherits="Admin_ExclusiveDetail" %>

<%@ Register Src="UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Exclusive offer Detail
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
                                        <span class="mandatory">*</span> Select Category
                                    </div>
                                    <div class="controlstyle">
                                        <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="RFVddlCategory" runat="server"
                                            ErrorMessage="Select Category" ValidationGroup="save" Text="*" ControlToValidate="ddlCategory"
                                            SetFocusOnError="true" CssClass="ErrorLabelStyle" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                 <div class="entryform">
                                    <div class="labelstyle">
                                     <%--   <span class="mandatory">*</span>--%> Title
                                    </div>
                                    <div class="controlstyle">
                                       <asp:TextBox ID="txtTitle" CssClass="form-control" Width="200" runat="server"></asp:TextBox>
                                     <%--   <asp:RequiredFieldValidator Display="Dynamic" ID="rfvTitle" runat="server" ErrorMessage="Enter Title"
                                            ValidationGroup="save" Text="*" ControlToValidate="txtTitle" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                <div class="entryform">
                                    <div class="labelstyle">
                                        <span class="mandatory">*</span> Image :
                                    </div>
                                    <div class="controlstyle">
                                        <asp:FileUpload ID="FileUploadImg" runat="server" />
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="rfvFileUpload" runat="server" ErrorMessage="Upload image"
                                            ValidationGroup="save" Text="*" ControlToValidate="FileUploadImg" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                        ( preferable Image Size should be Width:300Px & Height:adjust )
                                    </div>
                                </div>
                                <div class="entryform">
                                    <div class="entryform">
                                        <div class="labelstyle">
                                        </div>
                                        <div class="controlstyle">
                                            <asp:Image ID="img" runat="server" Width="100" />
                                        </div>
                                    </div>
                                </div>
                                <div class="entryform">
                                    <div class="labelstyle">
                                        <span class="mandatory">*</span> Link :
                                    </div>
                                    <div class="controlstyle">
                                        <asp:TextBox ID="txtLink" CssClass="form-control" Width="200" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="rfvLink" runat="server" ErrorMessage="Enter Link"
                                            ValidationGroup="save" Text="*" ControlToValidate="txtLink" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator Display="Dynamic" ID="revLink" runat="server" ValidationGroup="save"
                                            Text="*" ControlToValidate="txtLink" SetFocusOnError="true" CssClass="ErrorLabelStyle">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="entryform">
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            Is Active :
                                        </div>
                                        <div class="controlstyle">
                                            <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" />
                                        </div>
                                    </div>
                                </div>
                            </div>
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
    </div>
</asp:Content>
