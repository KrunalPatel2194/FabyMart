<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Login.ascx.cs" Inherits="Login" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<input type="button" runat="server" id="btnMPELogin" style="display: none;" />
<cc1:ModalPopupExtender ID="MPELogin" runat="server" TargetControlID="btnMPELogin"
    PopupControlID="divLogin" BackgroundCssClass="modalbackground" DropShadow="false"
    CancelControlID="imbtnClose">
</cc1:ModalPopupExtender>
<div id="divLogin" runat="server" class="modalpopup loginPopUp" >
    <asp:ImageButton ID="imbtnClose" CssClass="modalbClose" ImageUrl="~/Images/popup_close_btn.png"
        runat="server"></asp:ImageButton>
    <div class="modaldetail">
        <asp:Panel ID="pnal" runat="server" DefaultButton="btnLogin">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modalheader">
                        Login
                    </div>
                    <fieldset class="input">
                        <DInfo:DisplayInfo runat="server" ID="DInfo" />
                        <div class="registration_form">
                            <div class="loginPopupText">
                                <div>
                                    <label>
                                        <asp:TextBox ID="txtEmail" runat="server" placeholder="Enter Email" 
                                            CssClass="pad loginTextboxWidth"></asp:TextBox>
                                        <%-- <asp:RequiredFieldValidator Display="Dynamic" ID="RFVEmail" runat="server" ErrorMessage="Enter Email"
                                        ValidationGroup="UsLogin" Text="*" ControlToValidate="txtEmail" SetFocusOnError="true"
                                        CssClass="mandatory"></asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator Display="Dynamic" ID="REVEmail" runat="server" ValidationGroup="UsLogin"
                                            Text="*" ControlToValidate="txtEmail" SetFocusOnError="true" CssClass="mandatory"></asp:RegularExpressionValidator>
                                    </label>
                                </div>
                                <div>
                                    <label>
                                        <asp:TextBox ID="txtpassword" runat="server" TextMode="Password" placeholder="Enter password"
                                            CssClass="pad" ></asp:TextBox>
                                        <%-- <asp:RequiredFieldValidator Display="Dynamic" ID="rfvPassword" runat="server" ErrorMessage="Enter Password"
                                        ValidationGroup="UsLogin" Text="*" ControlToValidate="txtpassword" SetFocusOnError="true"
                                        CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>--%>
                                    </label>
                                </div>
                                <div class="sky-form">
                                    <label class="checkbox">
                                        <asp:CheckBox ID="chkRemeber" runat="server" Checked="True" />
                                        <i></i>Remember Me.
                                    </label>
                                </div>
                                <div class="float-lt" style="padding-bottom: 2px;">
                                    <asp:Button ID="btnLogin" runat="server" Text="Sign In" OnClick="btnLogin_Click"
                                       />
                                </div>
                                <div class="clear"></div>
                            </div>
                            <div style="padding: 0px !important; margin: 0px 2px;">
                                <div class="forget float-lt">
                                    <a href='<%= objPageBase.GetAlias("ForgotPassword.aspx") %>' target="_blank">Forgot
                                        Your Password</a>
                                </div>
                                <div class="forget float-rt">
                                    <a href='<%= objPageBase.GetAlias("Login.aspx") %>' target="_parent">Create New Account</a>
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                            <!-- /Form -->
                        </div>
                     <%--   <asp:ValidationSummary ID="vsLogin" ShowMessageBox="true" EnableClientScript="true"
                            HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                            ValidationGroup="UsLogin" />--%>
                    </fieldset>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:HiddenField ID="hdnIsPage" runat="server" Value="false" />
        </asp:Panel>
    </div>
</div>
