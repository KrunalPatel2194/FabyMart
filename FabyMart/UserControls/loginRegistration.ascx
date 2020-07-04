<%@ Control Language="C#" AutoEventWireup="true" CodeFile="loginRegistration.ascx.cs"
    Inherits="loginRegistration" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<input type="button" runat="server" id="btnMPEloginRegistration" style="display: none;" />
<cc1:ModalPopupExtender ID="MPEloginRegistration" runat="server" TargetControlID="btnMPEloginRegistration"
    PopupControlID="divloginRegistration" BackgroundCssClass="modalbackground" DropShadow="false"
    CancelControlID="imbtnClose">
</cc1:ModalPopupExtender>
<div id="divloginRegistration" runat="server" class="modalpopup loginRegistrationPopUp" >
    <asp:ImageButton ID="imbtnClose" CssClass="modalbClose" ImageUrl="~/Images/popup_close_btn.png" ToolTip="Close"
        runat="server"></asp:ImageButton>
    <div class="modaldetail">
        <script type="text/javascript">
            function isNumber(evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                return true;
            } validateRegistrationForm

            function validateRegistrationForm() {
                if (document.getElementById("<%=txtFirstName.ClientID%>").value == "") {
                    alert("Enter First Name");
                    document.getElementById("<%=txtFirstName.ClientID%>").focus();
                    return false;
                }
                if (document.getElementById("<%=txtLastName.ClientID %>").value == "") {
                    alert("Enter Last Name ");
                    document.getElementById("<%=txtLastName.ClientID %>").focus();
                    return false;
                }
                if (document.getElementById("<%=txtMobile.ClientID%>").value == "") {
                    alert("Enter Mobile No.");
                    document.getElementById("<%=txtMobile.ClientID%>").focus();
                    return false;
                }
                if (document.getElementById("<%=txtEmailRegistration.ClientID %>").value == "") {
                    alert("Enter Email ");
                    document.getElementById("<%=txtEmailRegistration.ClientID %>").focus();
                    return false;
                }
                if (document.getElementById("<%=txtPasswordRegistration.ClientID%>").value == "") {
                    alert("Enter Password.");
                    document.getElementById("<%=txtPasswordRegistration.ClientID%>").focus();
                    return false;
                }
                Page_ClientValidate('Registration');
                if (Page_IsValid('Registration') == true) {
                    this.disabled = true;
                    return true;
                }
                else {
                    return false;
                }
                return true;
            }
            function validate() {
                if (document.getElementById("<%=txtEmail.ClientID%>").value == "") {
                    alert("Enter Login Email");
                    document.getElementById("<%=txtEmail.ClientID%>").focus();
                    return false;
                }
                if (document.getElementById("<%=txtPassword.ClientID %>").value == "") {
                    alert("Enter Login Password ");
                    document.getElementById("<%=txtPassword.ClientID %>").focus();
                    return false;
                }
                Page_ClientValidate('login');
                if (Page_IsValid('login') == true) {
                    this.disabled = true;
                    return true;
                }
                else {
                    return false;
                }
                return true;
            }
        </script>
        <asp:Panel ID="pnal" runat="server" DefaultButton="btnLogin">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <%--<div class="modalheader">
                        <center>
                            <img src="<%=strServerURL %>Images/FabyMartLogo.png" height="50" alt="" />
                        </center>
                    </div>--%>
                    <fieldset class="input">
                        <DInfo:DisplayInfo runat="server" ID="DInfo" />
                        <div class="loginPopup" >
                            <div class="wrappoupLogin">
                                <div class="col_1_of_loginPopup span_1_of_loginPopup">
                                    <asp:Panel ID="pnlFirstRegipopup" runat="server" DefaultButton="brnCreate">
                                        <h4 class="title">
                                            New Customers</h4>
                                        <div class="loginregisterPopup_account">
                                            <div style="">
                                                <DInfo:DisplayInfo runat="server" ID="DisplayInfoRegistration" />
                                            </div>
                                            <div class="wrap">
                                                <div id="Registration" class="loginRegistration">
                                                    <fieldset class="input">
                                                        <p id="P1">
                                                            <span class="mandatory">*</span>
                                                            <asp:Label ID="Label1" runat="server" Width="90%">First Name</asp:Label>
                                                            <asp:TextBox runat="server" ID="txtFirstName" CssClass="inputbox" placeholder=""></asp:TextBox>
                                                        </p>
                                                        <p id="P2">
                                                            <span class="mandatory">*</span>
                                                            <asp:Label ID="Label2" runat="server" Width="90%"> Last Name</asp:Label>
                                                            <asp:TextBox runat="server" ID="txtLastName" CssClass="inputbox" placeholder=""></asp:TextBox>
                                                        </p>
                                                        <p id="P3">
                                                            <span class="mandatory">*</span>
                                                            <asp:Label ID="Label3" runat="server" Width="90%">Mobile Number</asp:Label>
                                                            <asp:TextBox ID="txtMobile" runat="server" CssClass="inputbox" placeholder="" onkeypress="return isNumber(event)"
                                                                MaxLength="10"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="revMobile" runat="server" Display="Dynamic" ValidationGroup="Registration"
                                                                Text="*" ControlToValidate="txtMobile" SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                                        </p>
                                                        <p id="P5">
                                                            <span class="mandatory">*</span>
                                                            <asp:Label ID="Label4" runat="server" Width="90%"> Email</asp:Label>
                                                            <asp:TextBox runat="server" ID="txtEmailRegistration" CssClass="inputbox" placeholder=""></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="revEmailRegistration" runat="server" Display="Dynamic"
                                                                ValidationGroup="Registration" Text="*" ControlToValidate="txtEmailRegistration"
                                                                SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                                        </p>
                                                        <p id="P6">
                                                            <span class="mandatory">*</span>
                                                            <asp:Label ID="Label5" runat="server" Width="90%">Password</asp:Label>
                                                            <asp:TextBox ID="txtPasswordRegistration" runat="server" Width="96%" CssClass="inputbox"
                                                                TextMode="Password" placeholder=""></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="revPassword" runat="server" Display="Dynamic"
                                                                ValidationGroup="Registration" Text="*" ControlToValidate="txtPasswordRegistration"
                                                                SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                                        </p>
                                                        <%--  <p id="P7">
                                                        <span class="mandatory">*</span>
                                                        <asp:Label ID="Label6" runat="server" Width="50%">Confirm Password</asp:Label>
                                                        <asp:TextBox ID="txtConfirmPassword" runat="server" Width="96%" CssClass="inputbox"
                                                            placeholder="" TextMode="Password"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="revConfirmPassword" runat="server" Display="Dynamic"
                                                            ValidationGroup="Registration" Text="*" ControlToValidate="txtConfirmPassword"
                                                            SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                                        <asp:CompareValidator Display="Dynamic" ID="cmRetryPassword" runat="server" ErrorMessage="Password Does Not Match !"
                                                            ValidationGroup="Registration" Text="Password not Match !" ControlToValidate="txtConfirmPassword"
                                                            ControlToCompare="txtPasswordRegistration" SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:CompareValidator>
                                                    </p>
                                                    <p id="P8">
                                                        <span class="mandatory">*</span>
                                                        <asp:Label ID="Label7" runat="server" Width="50%">Address</asp:Label>
                                                        <asp:TextBox runat="server" ID="txtAddress" Rows="3" placeholder="" TextMode="MultiLine"
                                                            CssClass="textarea"></asp:TextBox>
                                                    </p>
                                                    <div class="sky-form">
                                                        <div class="sky_form1">
                                                            <ul>
                                                                <li>
                                                                    <label class="radio first">
                                                                        <asp:RadioButton ID="RbtnMale" runat="server" GroupName="Gender" Checked="true" /><i></i>Male</label></li>
                                                                <li>
                                                                    <label class="radio">
                                                                        <asp:RadioButton ID="RbtnFeMale" runat="server" GroupName="Gender" /><i></i>Female</label></li>
                                                            </ul>
                                                        </div>
                                                    </div>--%>
                                                        <br />
                                                        <div class="sky-form">
                                                            <label class="checkbox" style="margin-left: -27px;">
                                                                By clicking you agree to the <a href="#">Terms &amp; Conditions</a>.
                                                            </label>
                                                        </div>
                                                        <div class="remember">
                                                            <div class="float-lt">
                                                                <asp:Button runat="server" ID="brnCreate" Text="Register" CssClass="register" ValidationGroup="Registration"
                                                                    OnClick="brnCreate_Click" OnClientClick=" return validateRegistrationForm()" />
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
                                    </asp:Panel>
                                </div>
                                <div class="col_1_of_loginPopup_rt span_1_of_loginPopup">
                                    <div class="login-title">
                                        <h4 class="titlewidth">
                                            Registered Customers</h4>
                                        <div id="loginbox" class="loginbox">
                                            <fieldset class="input">
                                                <p id="login-form-username">
                                                    <span class="mandatory">*</span>
                                                    <label for="modlgn_username">
                                                        Email</label>
                                                    <asp:TextBox runat="server" ID="txtEmail" CssClass="inputbox"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="revEmail" runat="server" Display="Dynamic" ValidationGroup="login"
                                                        Text="*" ControlToValidate="txtEmail" SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                                </p>
                                                <p id="login-form-password">
                                                    <span class="mandatory">*</span>
                                                    <label for="modlgn_passwd">
                                                        Password</label>
                                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="inputbox" TextMode="Password"></asp:TextBox>
                                                </p>
                                                <div class="remember">
                                                    <div class="float-lt">
                                                        <asp:Button runat="server" ID="btnLogin" Text="Login" OnClientClick=" return validate()"
                                                            OnClick="btnLogin_Click" ValidationGroup="login" />
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
                        <asp:ValidationSummary ID="vsLogin" ShowMessageBox="true" EnableClientScript="true"
                            HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                            ValidationGroup="login" />
                        <asp:ValidationSummary ID="vsRegistration" ShowMessageBox="true" EnableClientScript="true"
                            HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                            ValidationGroup="Registration" />
                        </div>
                    </fieldset>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:HiddenField ID="hdnIsPage" runat="server" Value="false" />
        </asp:Panel>
    </div>
</div>
