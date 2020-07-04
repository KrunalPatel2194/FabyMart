<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>

<%@ Register Src="~/UserControls/Customer.ascx" TagName="Customer" TagPrefix="Home" %>
<%@ Register Src="~/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta name="description" content="<%=metaDescription %>" />
    <meta name="keywords" content="<%=metaKeywords %>" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="mens">
        <div class="main">
            <div class="wrap">
                <ul class="breadcrumb breadcrumb__t">
                    <a class="" href='<%= objPageBase.GetAlias("Default.aspx")%>'>Home</a> / <span>Change
                        Password</span>
                </ul>
                <div class="clear">
                </div>
                <div class="col_1_of_changePassword ">
                    <h4 class="title">
                        Change Password</h4>
                    <div class="register_account">
                        <div style="width: 90%">
                            <DInfo:DisplayInfo runat="server" ID="DInfo" />
                        </div>
                        <br />
                        <div class="wrap">
                            <div id="divRegistration" class="Registration">
                                <fieldset class="input">
                                    <p id="P1">
                                        <span class="mandatory">*</span>
                                        <asp:Label ID="Label1" runat="server" Width="50%"> Old Password</asp:Label>
                                        <%--  <label for="modlgn_username">
                                           </label>--%>
                                        <asp:TextBox runat="server" ID="txtOldPassword" placeholder="" TextMode="Password"
                                            CssClass="span_1_of_2"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="rfvOldPassword" runat="server"
                                            ErrorMessage="Enter Old Password" ValidationGroup="change" Text="*" ControlToValidate="txtOldPassword"
                                            SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revoldPassword" runat="server" Display="Dynamic"
                                            ValidationGroup="change" Text="*" ControlToValidate="txtOldPassword" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                    </p>
                                    <p id="P2">
                                        <span class="mandatory">*</span>
                                        <asp:Label ID="Label2" runat="server" Width="50%">New Password</asp:Label>
                                        <%-- <label for="modlgn_username">
                                            </label>--%>
                                        <asp:TextBox runat="server" ID="txtNewPassword" placeholder="" TextMode="Password"
                                            CssClass="span_1_of_2"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="rfvNewPassord" runat="server" ErrorMessage="Enter New Password"
                                            ValidationGroup="change" Text="*" ControlToValidate="txtNewPassword" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revNewPassword" runat="server" Display="Dynamic"
                                            ValidationGroup="change" Text="*" ControlToValidate="txtNewPassword" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                    </p>
                                    <p id="P3">
                                        <span class="mandatory">*</span>
                                        <asp:Label ID="Label3" runat="server" Width="50%">Retype New Password</asp:Label>
                                        <%--<label for="modlgn_username">
                                            </label>--%>
                                        <asp:TextBox ID="txtNewRetryPassword" runat="server" placeholder="" CssClass="span_1_of_2"
                                            TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="rfvRetryPassword" runat="server"
                                            ErrorMessage="Retype New Password" ValidationGroup="change" Text="*" ControlToValidate="txtNewRetryPassword"
                                            SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revRetryNewPassword" runat="server" Display="Dynamic"
                                            ValidationGroup="change" Text="*" ControlToValidate="txtNewRetryPassword" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                        <asp:CompareValidator Display="Dynamic" ID="cmRetryPassword" runat="server" ErrorMessage="The Password does not Match !"
                                            ValidationGroup="change" Text="The Password does not Match !" ControlToValidate="txtNewRetryPassword"
                                            ControlToCompare="txtNewPassword" SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:CompareValidator>
                                    </p>
                                    <div class="remember">
                                        <div class="float-lt">
                                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" CssClass="setTopMargin5"
                                                ValidationGroup="change" />
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>
    <div>
        <asp:ValidationSummary ID="vsLogin" ShowMessageBox="true" EnableClientScript="true"
            HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
            ValidationGroup="change" />
    </div>
</asp:Content>
