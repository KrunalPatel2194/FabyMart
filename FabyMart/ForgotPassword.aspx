<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="ForgotPassword.aspx.cs" Inherits="ForgetPassword" %>

<%@ Register Src="~/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta name="description" content="<%=metaDescription %>" />
    <meta name="keywords" content="<%=metaKeywords %>" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="mens">
        <div class="main">
            <div class="wrap">
                <DInfo:DisplayInfo runat="server" ID="DInfo" />
                <div class="pad">
                </div>
                <div class="col_1_of_changePassword span_1_of_login">
                    <h4 class="title">
                        Forgot Password</h4>
                    <div class="register_account">
                        <div style="width: 90%">
                        </div>
                        <br />
                        <div class="wrap">
                            <div id="divRegistration" class="Registration">
                                <fieldset class="input">
                                    <p id="P1">
                                        <span class="mandatory">*</span>
                                        <label for="modlgn_username">
                                            Email</label>
                                        <asp:TextBox runat="server" ID="txtEmail" placeholder="Email" CssClass="span_1_of_2"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="rfvEmail" runat="server" ErrorMessage="Enter Email"
                                            ValidationGroup="save" Text="*" ControlToValidate="txtEmail" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revEmail" runat="server" Display="Dynamic" ValidationGroup="save"
                                            Text="*" ControlToValidate="txtEmail" SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                    </p>
                                    <div class="remember">
                                        <div class="float-lt">
                                            <asp:Button ID="btnSend" runat="server" OnClick="btnSend_Click" Text="Send" CssClass="setTopMargin5"
                                                ValidationGroup="save" />
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
    <div>
        <asp:ValidationSummary ID="vsLogin" ShowMessageBox="true" EnableClientScript="true"
            HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
            ValidationGroup="save" />
    </div>
</asp:Content>
