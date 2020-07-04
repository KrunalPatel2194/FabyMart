<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="UpdateProfile.aspx.cs" Inherits="UpdateProfile" %>

<%@ Register Src="~/UserControls/Customer.ascx" TagName="Customer" TagPrefix="Home" %>
<%@ Register Src="~/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta name="description" content="<%=metaDescription %>" />
    <meta name="keywords" content="<%=metaKeywords %>" />
    <script type="text/javascript">
        function showimagepreview(input) {
            if (input.files && input.files[0]) {
                var filerdr = new FileReader();
                filerdr.onload = function (e) {
                    $('#<%= img.ClientID%>').attr('src', e.target.result);
                }
                filerdr.readAsDataURL(input.files[0]);

            }
        }
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="mens">
        <div class="main">
            <div class="wrap">
                <ul class="breadcrumb breadcrumb__t">
                    <a class="" href='<%= objPageBase.GetAlias("Default.aspx")%>'>Home</a> / <span>Update
                        Profile </span>
                </ul>
                <div class="clear">
                </div>
                <DInfo:DisplayInfo runat="server" ID="DInfo" />
                <div class="col_1_of_changePassword ">
                    <h4 class="titleTrackOrder">
                        Update Profile</h4>
                    <div class="register_account1">
                        <div style="width: 90%">
                        </div>
                        <br />
                        <div class="wrap">
                            <div id="divRegistration" class="Registration1">
                                <fieldset class="input">
                                    <p id="P1">
                                      <div>   <span class="mandatory">*</span>
                                     <asp:Label ID="Label2" runat="server">First Name</asp:Label>
                                       </div>  
                                        <asp:TextBox runat="server" ID="txtFirstName" placeholder="First Name" ></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="None" ID="rfvFirstName" runat="server" ErrorMessage="Enter First Name"
                                            ValidationGroup="save" Text="*" ControlToValidate="txtFirstName" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="REVFirstName" runat="server" Display="None" ValidationGroup="save"
                                            Text="*" ControlToValidate="txtFirstName" SetFocusOnError="true" CssClass="ErrorLabelStyle">
                                        </asp:RegularExpressionValidator>
                                    </p>
                                    <p id="P2">
                                         <div>  <span class="mandatory">*</span>
                                        <asp:Label ID="Label1" runat="server" >Last Name</asp:Label>
                                       </div>
                                        <asp:TextBox runat="server" ID="txtLastName" placeholder="Last Name"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="None" ID="rfvLastName" runat="server" ErrorMessage="Enter Last Name"
                                            ValidationGroup="save" Text="*" ControlToValidate="txtLastName" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="REVLastName" runat="server" Display="None" ValidationGroup="save"
                                            Text="*" ControlToValidate="txtLastName" SetFocusOnError="true" CssClass="ErrorLabelStyle">
                                        </asp:RegularExpressionValidator>
                                    </p>
                                    <p id="P3">
                                       <div>    <span class="mandatory">*</span>
                                        
                                        <asp:Label runat="server">Email</asp:Label></div>
                                        <asp:TextBox runat="server" ID="txtEmail" placeholder="Email" ></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="None" ID="rfvEmail" runat="server" ErrorMessage="Enter Email"
                                            ValidationGroup="save" Text="*" ControlToValidate="txtEmail" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revEmail" runat="server" Display="None" ValidationGroup="save"
                                            Text="*" ControlToValidate="txtEmail" SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                    </p>
                                    <p id="P4">
                                       <div>    <span class="mandatory">*</span>
                                        <asp:Label ID="Label5" runat="server" >Mobile No</asp:Label>
                                      </div>
                                        <asp:TextBox ID="txtMobile" runat="server" placeholder="Mobile Number" 
                                            onkeypress="return isNumber(event)"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="None" ID="rfvMobile" runat="server" ErrorMessage="Enter Mobile Number"
                                            ValidationGroup="save" Text="*" ControlToValidate="txtMobile" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revMobile" runat="server" Display="None" ValidationGroup="save"
                                            Text="*" ControlToValidate="txtMobile" SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                    </p>
                                    <p id="P5">
                                     <div>      
                                        <asp:Label ID="Label4" runat="server" >Phone No</asp:Label>
                                    </div>
                                        <asp:TextBox ID="txtPhone" runat="server" placeholder="Phone Number" 
                                            onkeypress="return isNumber(event)"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revPhone" runat="server" Display="None" ValidationGroup="save"
                                            Text="*" ControlToValidate="txtPhone" SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                    </p>
                                  <%--  <p id="P6">
                                   <div>        <span class="mandatory">*</span>
                                        <asp:Label ID="Label3" runat="server" >Address</asp:Label>
                                       </div>
                                        <asp:TextBox ID="txtAddress" runat="server" class="updateProfiletextArea1" Rows="3"
                                            placeholder="Address" TextMode="MultiLine">
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator Display="None" ID="RFVAddress" runat="server" ErrorMessage="Enter Address"
                                            ValidationGroup="save" Text="*" ControlToValidate="txtAddress" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                    </p>
                                    <p id="P7">
                                    <div>       <span class="mandatory">*</span>
                                        <asp:Label ID="lblPincode" runat="server" >Pincode</asp:Label></div>
                                        <asp:TextBox ID="txtPincode" runat="server" placeholder="Pincode" 
                                            onkeypress="return isNumber(event)" MaxLength="6"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="None" ID="RFVPincode" runat="server" ErrorMessage="Enter Pincode"
                                            ValidationGroup="save" Text="*" ControlToValidate="txtPincode" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="REVPincode" runat="server" Display="Dynamic"
                                            ValidationGroup="save" Text="*" ControlToValidate="txtPincode" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                    </p>--%>
                                 <%--   <asp:UpdatePanel ID="upAddress" runat="server">
                                        <ContentTemplate>
                                            <p>
                                         <div>          <span class="mandatory">*</span>
                                                <asp:Label ID="Label6" runat="server">  Country </asp:Label></div>
                                                <div class="UpdateDropDown">
                                                    <div class="dropdownUpdateProfileLeft">
                                                        <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator Display="none" ID="RFVCountry" runat="server" ErrorMessage="Select Country"
                                                            ValidationGroup="save" Text="*" ControlToValidate="ddlCountry" SetFocusOnError="true"
                                                            InitialValue="0" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="dropdownLeft1">
                                                        <asp:TextBox ID="txtcountry" runat="server" placeholder="Country" Visible="false">
                                                        </asp:TextBox>
                                                        <asp:RequiredFieldValidator Display="none" ID="RFVtxtCoutry" runat="server" ErrorMessage="Enter Coutry"
                                                            ValidationGroup="save" Text="*" ControlToValidate="txtcountry" SetFocusOnError="true"
                                                            CssClass="ErrorLabelStyle" Enabled="false"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="REVtxtCoutry" runat="server" Display="none" ValidationGroup="save"
                                                            Text="*" ControlToValidate="txtcountry" SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                                    </div>
                                                    <div style="clear: both">
                                                    </div>
                                                </div>
                                            </p>
                                            <p>
                                                <span class="mandatory">*</span>
                                                <asp:Label ID="Label7" runat="server">   State </asp:Label>
                                                <div class="UpdateDropDown">
                                                    <div class="dropdownUpdateProfileLeft">
                                                        <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator Display="none" ID="RFVState" runat="server" ErrorMessage="Select State"
                                                            ValidationGroup="save" Text="*" ControlToValidate="ddlState" SetFocusOnError="true"
                                                            InitialValue="0" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="dropdownLeft1">
                                                        <asp:TextBox ID="txtState" runat="server" placeholder="State" Visible="false">
                                                        </asp:TextBox>
                                                        <asp:RequiredFieldValidator Display="none" ID="RFVtxtState" runat="server" ErrorMessage="Enter State"
                                                            ValidationGroup="save" Text="*" ControlToValidate="txtState" SetFocusOnError="true"
                                                            CssClass="ErrorLabelStyle" Enabled="false"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="REVtxtState" runat="server" Display="none" ValidationGroup="save"
                                                            Text="*" ControlToValidate="txtState" SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                                    </div>
                                                    <div style="clear: both">
                                                    </div>
                                                </div>
                                            </p>
                                            <p>
                                                <span class="mandatory">*</span>
                                                <asp:Label ID="Label8" runat="server">  City </asp:Label>
                                                <div class="UpdateDropDown">
                                                    <div class="dropdownUpdateProfileLeft">
                                                        <asp:DropDownList ID="ddlCity" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator Display="none" ID="RFVCity" runat="server" ErrorMessage="Select City"
                                                            ValidationGroup="save" Text="*" ControlToValidate="ddlCity" SetFocusOnError="true"
                                                            InitialValue="0" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="dropdownLeft1">
                                                        <asp:TextBox ID="txtCity" runat="server" placeholder="City" Visible="false">
                                                        </asp:TextBox>
                                                        <asp:RequiredFieldValidator Display="none" ID="RFVtxtCity" runat="server" ErrorMessage="Enter City"
                                                            ValidationGroup="save" Text="*" ControlToValidate="txtCity" SetFocusOnError="true"
                                                            CssClass="ErrorLabelStyle" Enabled="false"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="REVtxtCity" runat="server" Display="none" ValidationGroup="save"
                                                            Text="*" ControlToValidate="txtCity" SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                                    </div>
                                                    <div style="clear: both">
                                                    </div>
                                                </div>
                                            </p>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>--%>
                                    <div class="sky-form">
                                        <div class="sky_form1">
                                            <ul>
                                                <li>
                                                    <label class="radio first">
                                                        <asp:RadioButton ID="RbtnMale" runat="server" GroupName="Gender" Checked="true" /><i></i>Male</label></li>
                                                <li>
                                                    <label class="radio">
                                                        <asp:RadioButton ID="rbtnFeMale" runat="server" GroupName="Gender" /><i></i>Female</label></li>
                                            </ul>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="updateProfilelabel">
                                        <br />
                                        <asp:FileUpload ID="FileUploadImg" runat="server" onchange="showimagepreview(this) " />
                                    </div>
                                    <br />
                                    <div class="updateProfilelabel">
                                        <asp:Image ID="img" runat="server" Width="100" />
                                    </div>
                                    <div class="remember">
                                        <div class="float-lt">
                                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" CssClass="setTopMargin5"
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
    <asp:ValidationSummary ID="vsLogin" ShowMessageBox="true" EnableClientScript="true"
        HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
        ValidationGroup="save" />
</asp:Content>
