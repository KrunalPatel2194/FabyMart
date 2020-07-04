<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="GplusSignUp.aspx.cs" Inherits="GplusSignUp" %>

<%@ Register Src="~/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta name="description" content="<%=metaDescription %>" />
    <meta name="keywords" content="<%=metaKeywords %>" />
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        Sys.Application.add_load(FBSetting);
    </script>
    <div class="login">
        <div class="wrap">
            <DInfo:DisplayInfo runat="server" ID="DInfo" />
        </div>
        <div id="btngplus" runat="server">
            </div>
        
        <div class="wrap">
            <div class="col_1_of_login span_1_of_login">
                <h4 class="title">
                    New Customers
                    
                </h4>
                <div style=" text-align: center;">
                     <asp:Button ID="Button1"  class="gplusbtn" runat="server" OnClick="Login1" />
           
                    </div>
               
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
                                    <p id="P8">
                                        <span class="mandatory">*</span>
                                        <asp:Label ID="Label7" runat="server" Width="50%">Address</asp:Label>
                                        <%--  <label for="modlgn_username">
                                        </label>--%>
                                        <asp:TextBox runat="server" ID="txtAddress" Rows="3" placeholder="" TextMode="MultiLine"
                                            CssClass="textarea"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="rfvAddress" runat="server" ErrorMessage="Enter Address"
                                            ValidationGroup="register" Text="*" ControlToValidate="txtAddress" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                    </p>
                                    <div class="sky-form">
                                        <div class="sky_form1">
                                            <ul>
                                                <li>
                                                    <label class="radio first">
                                                        <asp:RadioButton ID="RbtnMale" runat="server" GroupName="Gender" /><i></i>Male</label></li>
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
              <div class="clear">
            </div>
            <div id="divShowRegisterPopUp" class="modalpopup" style="display: none;">
                <div class="modaldetail">
                    <div class="modalheader">
                        <div class="float-lt">
                        </div>
                        <div class="float-rt" onclick="divRegisterpopupClose();" style="cursor: pointer;">
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
 
    <div>
      
        <asp:ValidationSummary ID="vsRegister" ShowMessageBox="true" EnableClientScript="true"
            HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
            ValidationGroup="register" />
    </div>
    <asp:HiddenField ID="hdnEmail" Value="" runat="server" />
</asp:Content>
