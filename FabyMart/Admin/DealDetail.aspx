<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Main.master" AutoEventWireup="true"
    CodeFile="DealDetail.aspx.cs" Inherits="Admin_DealDetail" %>

<%@ Register Src="~/Admin/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Deal Detail
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
                    <div class="tab-content">
                        <div class="panel-search">
                            <div class="table-responsive">
                                <div class="entryformmain">
                                    <asp:UpdatePanel runat="server" ID="up1">
                                        <ContentTemplate>
                                            <div class="entryform">
                                                <div class="labelstyle">
                                                    <span class="mandatory">*</span> Product :
                                                </div>
                                                <div class="controlstyle">
                                                    <asp:DropDownList runat="server" ID="ddlProduct" CssClass="form-control" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RFVddlProduct" runat="server" ErrorMessage="Select Product"
                                                        ValidationGroup="save" Text="*" ControlToValidate="ddlProduct" SetFocusOnError="true"
                                                        InitialValue="0" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                    <asp:Image ID="img" runat="server" AlternateText="" Width="32" Height="32" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            <span class="mandatory">*</span> Title:
                                        </div>
                                        <div class="controlstyle">
                                            <asp:TextBox ID="txtTitle" CssClass="form-control" Width="200" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RFVTitle" runat="server" ErrorMessage="Enter Title"
                                                ValidationGroup="save" Text="*" ControlToValidate="txtTitle" SetFocusOnError="true"
                                                CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            <span class="mandatory">*</span> Discount:
                                        </div>
                                        <div class="controlstyle">
                                            <asp:TextBox ID="txtDiscount" CssClass="form-control" Width="100" runat="server"></asp:TextBox><span style="font-size:18px;font-weight:bold;"> %</span>
                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RFVDiscount" runat="server" ErrorMessage="Enter Discount"
                                                ValidationGroup="save" Text="*" ControlToValidate="txtDiscount" SetFocusOnError="true"
                                                CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator Display="Dynamic" ID="REVRate" runat="server" ValidationGroup="save"
                                                Text="*" ControlToValidate="txtDiscount" SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="entryform">
                                        <div class="labelstyle">
                                         <%--   <span class="mandatory">*</span> --%>Description:
                                        </div>
                                        <div class="controlstyle">
                                            <asp:TextBox ID="txtDescription" CssClass="form-control" Width="400" Height="85"
                                                runat="server" TextMode="MultiLine"></asp:TextBox>
                                            <%-- <asp:RequiredFieldValidator Display="Dynamic" ID="RFVDiscription" runat="server"
                                                ErrorMessage="Enter Description" ValidationGroup="save" Text="*" ControlToValidate="txtDescription"
                                                SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            Is Active :
                                        </div>
                                        <div class="controlstyle">
                                            <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" TabIndex="3" />
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
                <asp:ValidationSummary ID="vsSearch" ShowMessageBox="true" EnableClientScript="true"
                    HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                    ValidationGroup="save" />
            </div>
        </div>
    </div>
</asp:Content>
