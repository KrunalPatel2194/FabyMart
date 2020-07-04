<%@ Page Title="CustomerDetail" Language="C#" MasterPageFile="~/admin/Main.master"
    AutoEventWireup="true" CodeFile="CustomerDetail.aspx.cs" Inherits="CustomerDetail" %>

<%@ Register Src="~/Admin/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Customer Detail
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
                                <div class="entryform">
                                    <div class="labelstyle">
                                        <span class="mandatory">*</span> First Name :
                                    </div>
                                    <div class="controlstyle">
                                        <asp:TextBox ID="txtFirstName" CssClass="form-control" Width="200" runat="server"
                                            TabIndex="1"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="RFVFirstName" runat="server" ErrorMessage="Enter First Name"
                                            ValidationGroup="save" Text="*" ControlToValidate="txtFirstName" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="REVFirstName" runat="server" Display="None" ValidationGroup="save"
                                            Text="*" ControlToValidate="txtFirstName" SetFocusOnError="true" CssClass="ErrorLabelStyle">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="entryform">
                                    <div class="labelstyle">
                                        <span class="mandatory">*</span> Last Name :
                                    </div>
                                    <div class="controlstyle">
                                        <asp:TextBox ID="txtLastName" CssClass="form-control" Width="200" runat="server"
                                            TabIndex="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="RFVLastName" runat="server" ErrorMessage="Enter Last Name"
                                            ValidationGroup="save" Text="*" ControlToValidate="txtLastName" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="REVLastName" runat="server" Display="None" ValidationGroup="save"
                                            Text="*" ControlToValidate="txtLastName" SetFocusOnError="true" CssClass="ErrorLabelStyle">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="entryform">
                                    <div class="labelstyle">
                                        <span class="mandatory">*</span> Email ID :
                                    </div>
                                    <div class="controlstyle">
                                        <asp:TextBox ID="txtEmail" CssClass="form-control" Width="200" runat="server" TabIndex="3"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="RFVEmail" runat="server" ErrorMessage="Enter Email"
                                            ValidationGroup="save" Text="*" ControlToValidate="txtEmail" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revEmailRegistration" runat="server" Display="Dynamic"
                                            ValidationGroup="save" Text="*" ControlToValidate="txtEmail" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="entryform">
                                    <div class="labelstyle">
                                        <span class="mandatory">*</span> Password :
                                    </div>
                                    <div class="controlstyle">
                                        <asp:TextBox ID="txtPassword" CssClass="form-control" Width="200" runat="server"
                                            TabIndex="4" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="RFVPassword" runat="server" ErrorMessage="Enter Password"
                                            ValidationGroup="save" Text="*" ControlToValidate="txtPassword" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revPassword" runat="server" Display="Dynamic"
                                            ValidationGroup="save" Text="*" ControlToValidate="txtPassword" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="entryform">
                                    <div class="labelstyle">
                                        <span class="mandatory">*</span> Mobile:
                                    </div>
                                    <div class="controlstyle">
                                        <asp:TextBox ID="txtMobile" CssClass="form-control" Width="200" runat="server" TabIndex="5"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="RFVMobile" runat="server" ErrorMessage="Enter Mobile"
                                            ValidationGroup="save" Text="*" ControlToValidate="txtMobile" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revMobile" runat="server" Display="Dynamic" ValidationGroup="save"
                                            Text="*" ControlToValidate="txtMobile" SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="entryform">
                                    <div class="labelstyle">
                                        <span class="mandatory"></span>Phone:
                                    </div>
                                    <div class="controlstyle">
                                        <asp:TextBox ID="txtPhone" CssClass="form-control" Width="200" runat="server" TabIndex="6"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator Display="Dynamic" ID="RFVPhone" runat="server" ErrorMessage="Enter Phone"
                                            ValidationGroup="save" Text="*" ControlToValidate="txtPhone" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                              <asp:RegularExpressionValidator ID="revMobile1" runat="server" Display="Dynamic" ValidationGroup="save"
                                            Text="*" ControlToValidate="txtPhone" SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>--%>
                                    </div>
                                </div>
                                <div class="entryform">
                                    <div class="labelstyle">
                                        <span class="mandatory">*</span> Gender:
                                    </div>
                                    <div class="controlstyle">
                                        <asp:RadioButton ID="RbtnMale" runat="server" Text="" GroupName="rbd" Checked="true" />Male
                                        <asp:RadioButton ID="rbtnFeMale" runat="server" GroupName="rbd" />Female
                                    </div>
                                </div>
                                <div class="entryform">
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            Is Verified :
                                        </div>
                                        <div class="controlstyle">
                                            <asp:CheckBox ID="chkIsVerified" runat="server" Checked="True" TabIndex="7" />
                                        </div>
                                    </div>
                                </div>
                                <div class="entryform">
                                    <div class="labelstyle">
                                        Image :
                                    </div>
                                    <div class="controlstyle">
                                        <asp:FileUpload ID="FileUploadImg" runat="server" TabIndex="8" />
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
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            Is Active :
                                        </div>
                                        <div class="controlstyle">
                                            <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" TabIndex="9" />
                                        </div>
                                    </div>
                                </div>
                                <div class="entryform">
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            Is NewsLetter :
                                        </div>
                                        <div class="controlstyle">
                                            <asp:CheckBox ID="chkIsNewsLetter" runat="server" Checked="True" TabIndex="10" />
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
