<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="_Login" %>

<%@ Register Src="~/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta name="description" content="<%=metaDescription %>" />
    <meta name="keywords" content="<%=metaKeywords %>" />
    <script type="text/javascript">
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
        function statusChangeCallback(response) {

            if (response.status === 'connected') {

                GetValueAPI();

            } else if (response.status === 'not_authorized') {

            } else {


            }
        }
        function checkLoginState() {
            FB.getLoginStatus(function (response) {
                statusChangeCallback(response);
            });
        }

        function FBSetting() {
            window.fbAsyncInit = function () {
                FB.init({
                    appId: '1446033349037337',
                    xfbml: true,
                    version: 'v2.2'
                });
            };


            FB.getLoginStatus(function (response) {
                statusChangeCallback(response);
            });

        }
        (function (d, s, id) {
            var js, fjs = d.getElementsByTagName(s)[0];
            if (d.getElementById(id)) return;
            js = d.createElement(s); js.id = id;
            js.src = "//connect.facebook.net/en_US/sdk.js";
            fjs.parentNode.insertBefore(js, fjs);
        } (document, 'script', 'facebook-jssdk'));
        function GetValueAPI() {

            FB.api('/me?fields=name,email', function (response) {

                document.getElementById("<%=hdnEmail.ClientID%>").value = response.email;
                //alert(document.getElementById("<%=hdnEmail.ClientID%>").value);
                CheckUserLogin();

            });
            return false;
        }
        function CheckUserLogin() {

            // alert(document.getElementById("<%=hdnEmail.ClientID%>").value);
            var EmailID = $("#<%=hdnEmail.ClientID%>").val();

            var obj = { EmailID: EmailID };
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "<%=strServerURL %>Login.aspx/UserLogin",
                data: JSON.stringify(obj),
                dataType: "json",
                success: function (response) {
                    //    alert(response.d);
                    if (response.d == "true") {
                        window.location.href = 'Home';
                    }
                    // location.reload(); 
                    //   window.location.href = 'My-Account/'+response.d;

                },
                failure: function (response) {
                    alert(response.responseText);
                },
                error: function (response) {
                    alert(response.responseText);
                }
            });
            return false;
        }
    </script>
    <script language="javascript" type="text/javascript">
        var overlay = $('<div class="overlay"></div>');
      
        var url = '<%=strServerURL %>';
        var REDIRECTURL = "";
        if (url == 'http://fabymart.com/') {
            REDIRECTURL = "http://fabymart.com/GoogleRedirect.aspx";
        } else {
            REDIRECTURL = "http://www.fabymart.com/GoogleRedirect.aspx";
        }
        var OAUTHURL = 'https://accounts.google.com/o/oauth2/auth?';
        var VALIDURL = 'https://www.googleapis.com/oauth2/v1/tokeninfo?access_token=';
        var SCOPE = 'https://www.googleapis.com/auth/userinfo.profile https://www.googleapis.com/auth/userinfo.email';
        var CLIENTID = '914728097472-9qg5960kfrjfech3b7mk28l3lis3levk.apps.googleusercontent.com';
        var REDIRECT = REDIRECTURL;
        var LOGOUT = 'http://accounts.google.com/Logout';
        var TYPE = 'token';
        var _url = OAUTHURL + 'scope=' + SCOPE + '&client_id=' + CLIENTID + '&redirect_uri=' + REDIRECT + '&response_type=' + TYPE;
        var acToken;
        var tokenType;
        var expiresIn;
        var user;
        var loggedIn = false;
        function login() {

            var win = window.open(_url, "windowname1", 'width=800, height=600');
            var pollTimer = window.setInterval(function () {
                try {
                   
                    if (win.document.URL.indexOf(REDIRECT) != -1) {
                        window.clearInterval(pollTimer);
                        var url = win.document.URL;
                        acToken = gup(url, 'access_token');
                        tokenType = gup(url, 'token_type');
                        expiresIn = gup(url, 'expires_in');
                        win.close();
                       
                        validateToken(acToken);
                    }
                }
                catch (e) {

                }
            }, 500);
        }
        function validateToken(token) {
           
            $.ajax(
            {
                url: VALIDURL + token,
                data: null,
                success: function (responseText) {
                    getUserInfo();
                    loggedIn = true;
                  
                    $('#loginText').show();
                    
                },
                dataType: "jsonp"
            });
        }
        function getUserInfo() {
            $.ajax({
                url: 'https://www.googleapis.com/oauth2/v1/userinfo?access_token=' + acToken,
                data: null,
                success: function (resp) {
                    user = resp;

                    document.getElementById("<%=hdnEmail.ClientID%>").value = user.email;
                    UserLogin();

                },
                dataType: "jsonp"
            });
        }
        //credits: http://www.netlobo.com/url_query_string_javascript.html  

        function gup(url, name) {
            namename = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regexS = "[\\#&]" + name + "=([^&#]*)";
            var regex = new RegExp(regexS);
            var results = regex.exec(url);
            if (results == null)
                return "";
            else
                return results[1];
        }
        function startLogoutPolling() {
            $('#loginText').show();
           
            loggedIn = false;
            $('#uName').text('Welcome ');
            $('#imgHolder').attr('src', 'none.jpg');
        }
        function UserLogin() {
            // alert("fdfdsds");
            var EmailID = $("#<%=hdnEmail.ClientID%>").val();
            var obj = { EmailID: EmailID };
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "<%=strServerURL %>Login.aspx/UserLogin",
                data: JSON.stringify(obj),
                dataType: "json",
                success: function (response) {
                    //  alert(response.d);
                    if (response.d == "true") {

                        window.location.href = 'My-Account';
                    } else {
                        ShowRegisterPopUp();
                        $("#divGoogle").show();
                    }
                    // location.reload(); 
                    //   window.location.href = 'My-Account/'+response.d;

                },
                failure: function (response) {
                    alert(response.responseText);
                },
                error: function (response) {
                    alert(response.responseText);
                }
            });
            return false;
        }

        function ShowRegisterPopUp() {


            overlay.show();
            overlay.appendTo(document.body);
            $('#divShowRegisterPopUp').css({ 'top': '40%', 'left': '33%', 'position': 'absolute', 'z-index': '110', 'width': '32%' }).fadeIn('slow');
            return false;

        }
        function divRegisterpopupClose() {
            overlay.appendTo(document.body).remove();
            $('#divShowRegisterPopUp').fadeOut('slow');

            return false;
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        Sys.Application.add_load(FBSetting);
    </script>
    <div class="login">
        <div class="wrap">
            <DInfo:DisplayInfo runat="server" ID="DInfo" />
        </div>
        <div class="wrap">
            <div class="col_1_of_login span_1_of_login">
                <h4 class="title">
                    New Customers</h4>
                <%-- <fb:login-button scope="public_profile,email" onlogin="checkLoginState();">
                    SignUp</fb:login-button>--%>
                <%--   <div class="button1">
                    <%-- <a href="register.html">
                        <input type="submit" name="Submit" value="Create an Account"></a>--%>
                <%--   <asp:LinkButton runat="server" ID="lbkCreateNewAccount" Text="Create New Account"
                        CssClass="grey" OnClick="lbkCreateNewAccount_Click"></asp:LinkButton>
                </div>--%>
                <asp:Panel ID="pnlNewRegisterAccount" runat="server" DefaultButton="brnCreate">
                    <div class="register_account">
                        <div style="width: 90%">
                            <DInfo:DisplayInfo runat="server" ID="DisplayInfoRegistration" />
                        </div>
                        <br />
                        <div class="wrap">
                            <div id="divRegistration" class="Registration">
                                <fieldset class="input">
                                    <p id="P1">
                                        <span class="mandatory">*</span>
                                        <asp:Label ID="Label1" runat="server" Width="50%">First Name</asp:Label>
                                        <%--<label for="modlgn_username">
                                        </label>--%>
                                        <asp:TextBox runat="server" ID="txtFirstName" CssClass="inputbox" placeholder=""></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="rfvFirstName" runat="server" ErrorMessage="Enter First Name"
                                            ValidationGroup="register" Text="*" ControlToValidate="txtFirstName" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="REVFirstName" runat="server" Display="None" ValidationGroup="register"
                                            Text="*" ControlToValidate="txtFirstName" SetFocusOnError="true" CssClass="ErrorLabelStyle">
                                        </asp:RegularExpressionValidator>
                                    </p>
                                    <p id="P2">
                                        <span class="mandatory">*</span>
                                        <asp:Label ID="Label2" runat="server" Width="50%"> Last Name</asp:Label>
                                        <%--<label for="modlgn_username">
                                        </label>--%>
                                        <asp:TextBox runat="server" ID="txtLastName" CssClass="inputbox" placeholder=""></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="rfvLastName" runat="server" ErrorMessage="Enter Last Name"
                                            ValidationGroup="register" Text="*" ControlToValidate="txtLastName" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="REVLastName" runat="server" Display="None" ValidationGroup="register"
                                            Text="*" ControlToValidate="txtLastName" SetFocusOnError="true" CssClass="ErrorLabelStyle">
                                        </asp:RegularExpressionValidator>
                                    </p>
                                    <p id="P3">
                                        <span class="mandatory">*</span>
                                        <asp:Label ID="Label3" runat="server" Width="50%">Mobile Number</asp:Label>
                                        <%--<label for="modlgn_username">
                                        </label>--%>
                                        <asp:TextBox ID="txtMobile" runat="server" CssClass="inputbox" placeholder="" onkeypress="return isNumber(event)"
                                            MaxLength="10"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="rfvMobile" runat="server" ErrorMessage="Enter Mobile Number"
                                            ValidationGroup="register" Text="*" ControlToValidate="txtMobile" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revMobile" runat="server" Display="Dynamic" ValidationGroup="register"
                                            Text="*" ControlToValidate="txtMobile" SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                    </p>
                                    <p id="P5">
                                        <span class="mandatory">*</span>
                                        <asp:Label ID="Label4" runat="server" Width="50%"> Email</asp:Label>
                                        <%--<label for="modlgn_username">
                                        </label>--%>
                                        <asp:TextBox runat="server" ID="txtEmailRegistration" CssClass="inputbox" placeholder=""></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="RFVEmailRegistration" runat="server"
                                            ErrorMessage="Enter Email" ValidationGroup="register" Text="*" ControlToValidate="txtEmailRegistration"
                                            SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revEmailRegistration" runat="server" Display="Dynamic"
                                            ValidationGroup="register" Text="*" ControlToValidate="txtEmailRegistration"
                                            SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                    </p>
                                    <p id="P6">
                                        <span class="mandatory">*</span>
                                        <asp:Label ID="Label5" runat="server" Width="50%">Password</asp:Label>
                                        <%--<label for="modlgn_username">
                                        </label>--%>
                                        <asp:TextBox ID="txtPasswordRegistration" runat="server" Width="96%" CssClass="inputbox"
                                            TextMode="Password" placeholder=""></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="RFVPasswordRegistration" runat="server"
                                            ErrorMessage="Enter Password" ValidationGroup="register" Text="*" ControlToValidate="txtPasswordRegistration"
                                            SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revPassword" runat="server" Display="Dynamic"
                                            ValidationGroup="register" Text="*" ControlToValidate="txtPasswordRegistration"
                                            SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                    </p>
                                    <p id="P7">
                                        <span class="mandatory">*</span>
                                        <asp:Label ID="Label6" runat="server" Width="50%">Confirm Password</asp:Label>
                                        <%-- <label for="modlgn_username">
                                        </label>--%>
                                        <asp:TextBox ID="txtConfirmPassword" runat="server" Width="96%" CssClass="inputbox"
                                            placeholder="" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="rfvConfirmPassword" runat="server"
                                            ErrorMessage="Confirm Password" ValidationGroup="register" Text="*" ControlToValidate="txtConfirmPassword"
                                            SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revConfirmPassword" runat="server" Display="Dynamic"
                                            ValidationGroup="register" Text="*" ControlToValidate="txtConfirmPassword" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                        <asp:CompareValidator Display="Dynamic" ID="cmRetryPassword" runat="server" ErrorMessage="The Password Does Not Match !"
                                            ValidationGroup="register" ControlToValidate="txtConfirmPassword" ControlToCompare="txtPasswordRegistration"
                                            SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:CompareValidator>
                                    </p>
                                  <%--  <p id="P8">
                                        <span class="mandatory">*</span>
                                        <asp:Label ID="Label7" runat="server" Width="50%">Address</asp:Label>
                                        <%--  <label for="modlgn_username">
                                        </label>--%>
                                        <%--<asp:TextBox runat="server" ID="txtAddress" Rows="3" placeholder="" TextMode="MultiLine"
                                            CssClass="textarea"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="rfvAddress" runat="server" ErrorMessage="Enter Address"
                                            ValidationGroup="register" Text="*" ControlToValidate="txtAddress" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                    </p>--%>
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
                                    </div>
                                    <br />
                                    <div class="sky-form setTopMargin5 clear">
                                        By clicking on Register, You are agree to the <a href="<%=strServerURL %>Terms-Conditions">
                                            Terms &amp; Conditions</a>.
                                    </div>
                                    <div class="remember">
                                        <%-- <input type="submit" name="Submit" class="button" value="Login">--%>
                                        <div class="float-lt">
                                            <asp:Button runat="server" ID="brnCreate" Text="Register" CssClass="register" ValidationGroup="register"
                                                OnClick="brnCreate_Click" />
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
            <div class="col_1_of_login span_1_of_login">
                <asp:Panel ID="pnlOldLogin" runat="server" DefaultButton="btnLogin">
                    <div class="login-title">
                        <h4 class="titlewidth">
                            Registered Customers</h4>
                        <%-- <fb:login-button scope="public_profile,email" onlogin="checkLoginState();"> Login with Facebook</fb:login-button>--%>
                        <div>
                            <center>
                                <div style="float: left; width: 52%; text-align: right;">
                                    <%--<fb:login-button scope="public_profile,email" onlogin="checkLoginState();">
                                        Login with Facebook
                            </fb:login-button>--%>
                                </div>
                                <div style="text-align: center;">
                                    <a href='#' onclick='login();' id="loginText">
                                        <img src="<%=strServerURL %>Images/googlepluslogin.png" style="height: 40px; border-radius: 5px;" /></a>
                                    <br />
                                    <br />
                                    OR
                                    <br />
                                </div>
                            </center>
                            <div class="clear">
                            </div>
                        </div>
                        <div id="loginbox" class="loginbox">
                            <fieldset class="input">
                                <p id="login-form-username">
                                    <span class="mandatory">*</span>
                                    <label for="modlgn_username">
                                        Email</label>
                                    <label style="color: Red; float: right; margin-right: 5%;" id="lblForError" runat="server"
                                        for="modlgn_username">
                                    </label>
                                    <%--<input id="modlgn_username" type="text" name="email" class="inputbox" size="18" autocomplete="off">--%>
                                    <asp:TextBox runat="server" ID="txtEmail" CssClass="inputbox"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="rfvEmail" runat="server" ErrorMessage="Enter Email"
                                        ValidationGroup="login" Text="*" ControlToValidate="txtEmail" SetFocusOnError="true"
                                        CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revEmail" runat="server" Display="Dynamic" ValidationGroup="login"
                                        Text="*" ControlToValidate="txtEmail" SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                </p>
                                <p id="login-form-password">
                                    <span class="mandatory">*</span>
                                    <label for="modlgn_passwd">
                                        Password</label>
                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="inputbox" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RFVLoginPwd" runat="server" ErrorMessage="Enter Password"
                                        ValidationGroup="login" Text="*" ControlToValidate="txtPassword" SetFocusOnError="true"
                                        CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                </p>
                                <div class="sky-form setTopMargin5">
                                    <label class="checkbox">
                                        <asp:CheckBox ID="chkRemeberMe" runat="server" Checked="True" />
                                        <i></i>Remember Me.
                                    </label>
                                </div>
                                <div class="remember">
                                    <%-- <input type="submit" name="Submit" class="button" value="Login">--%>
                                    <div class="float-lt">
                                        <asp:Button runat="server" ID="btnLogin" Text="Login" OnClick="btnLogin_Click" ValidationGroup="login" />
                                    </div>
                                    <div class="float-rt remember">
                                        <asp:LinkButton runat="server" ID="lnkForgotPass" Text="Forgot Your Password ?" OnClick="lnkForgotPass_Click"></asp:LinkButton>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <div class="clear">
            </div>
            <div id="divShowRegisterPopUp" class="modalpopup" style="display: none;">
                <div class="modaldetail">
                    <div class="modalheader">
                        <div class="float-lt">
                        </div>
                        <div class="float-rt" onclick="divRegisterpopupClose();" style="cursor:pointer;">
                            <img src='<%=strServerURL %>Images/popup_close_btn.png' alt="" />
                        </div>
                        <div class="clear">
                        </div>
                    </div>
                    <div style="background: #fff; padding: 20px;">
                        This email is not registered with us, Please register first.
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Button ID="btnRegisterWithFacebook" runat="server" OnClick="btnRegisterWithFacebook_Click"
        Style="visibility: hidden" />
    <div>
        <asp:ValidationSummary ID="vsLogin" ShowMessageBox="true" EnableClientScript="true"
            HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
            ValidationGroup="login" />
        <asp:ValidationSummary ID="vsRegister" ShowMessageBox="true" EnableClientScript="true"
            HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
            ValidationGroup="register" />
    </div>
    <asp:HiddenField ID="hdnEmail" Value="" runat="server" />
</asp:Content>
