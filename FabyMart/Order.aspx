<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="Order.aspx.cs" Inherits="Order" EnableEventValidation="false" %>

<%@ Register Src="~/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta name="description" content="<%=metaDescription %>" />
    <meta name="keywords" content="<%=metaKeywords %>" />
    <script type="text/javascript">

        $(window).load(function () {
            $("#<%=txtcountry.ClientID %>").hide();
            $("#<%=txtState.ClientID %>").hide();
            $("#<%=txtCity.ClientID %>").hide();
            ValidatorEnable(document.getElementById("<%=RFVtxtCoutry.ClientID %>"), false);
            ValidatorEnable(document.getElementById("<%=RFVtxtState.ClientID %>"), false);
            ValidatorEnable(document.getElementById("<%=RFVtxtCity.ClientID %>"), false);
            $('#ddlCountry').change(function () {
                if ($('#<%=ddlCountry.ClientID%>').val() == "-1") {
                    document.getElementById("<%=txtcountry.ClientID %>").value = "";

                    $("#<%=txtcountry.ClientID %>").show();
                    ValidatorEnable(document.getElementById("<%=RFVtxtCoutry.ClientID %>"), true);

                    $('#ddlState').empty();
                    $('#<%=ddlState.ClientID %>').append('<option selected="selected" value="0">-- Select State --</option>');
                    $('#<%=ddlState.ClientID %>').append('<option value="-1">Other</option>');
                    $('#ddlCity').empty();
                    $('#<%=ddlCity.ClientID %>').append('<option selected="selected" value="0">-- Select City --</option>');
                    $('#<%=ddlCity.ClientID %>').append('<option value="-1">Other</option>');
                    $("#<%=txtState.ClientID %>").hide();
                    $("#<%=txtCity.ClientID %>").hide();
                    ValidatorEnable(document.getElementById("<%=RFVtxtState.ClientID %>"), false);
                    ValidatorEnable(document.getElementById("<%=RFVtxtCity.ClientID %>"), false);

                }
                else {
                    $.ajax({
                        type: "POST",
                        url: "<%=Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath %>/Order.aspx/PopulateStates",
                        data: '{CountryID: ' + $('#<%=ddlCountry.ClientID%>').val() + '}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var items = response.d.tblState;
                            $('#ddlState').empty();
                            $('#<%=ddlState.ClientID %>').append('<option  value="0">-- Select State --</option>');

                            for (var i = 0; i < items.length; i++) {
                                $('#<%=ddlState.ClientID%>').append($('<option></option>').val(items[i].appStateID).html(items[i].appState));
                            }
                            $('#<%=ddlState.ClientID %>').append('<option  value="-1">Other</option>');
                            $("#<%=txtcountry.ClientID %>").hide();
                            ValidatorEnable(document.getElementById("<%=RFVtxtCoutry.ClientID %>"), false);
                            $('#ddlCity').empty();
                            $('#<%=ddlCity.ClientID %>').append('<option selected="selected" value="0">-- Select City --</option>');
                            $('#<%=ddlCity.ClientID %>').append('<option value="-1">Other</option>');

                        },
                        failure: function (response) {
                            alert(response.d);
                        }
                    });
                    $("#<%=txtState.ClientID %>").hide();
                    $("#<%=txtCity.ClientID %>").hide();
                    ValidatorEnable(document.getElementById("<%=RFVtxtState.ClientID %>"), false);
                    ValidatorEnable(document.getElementById("<%=RFVtxtCity.ClientID %>"), false);

                }
            });
            $('#ddlState').change(function () {
                debugger
                if ($('#<%=ddlState.ClientID%>').val() == "-1") {
                    document.getElementById("<%=txtState.ClientID %>").value = "";
                    $("#<%=txtState.ClientID %>").show();

                    $('#ddlCity').empty();
                    $('#<%=ddlCity.ClientID %>').append('<option selected="selected" value="0">-- Select City --</option>');
                    $('#<%=ddlCity.ClientID %>').append('<option value="-1">Other</option>');
                    $("#<%=txtCity.ClientID %>").hide();
                    ValidatorEnable(document.getElementById("<%=RFVtxtState.ClientID %>"), true);
                    ValidatorEnable(document.getElementById("<%=RFVtxtCity.ClientID %>"), false);
                }
                else {
                    $.ajax({
                        type: "POST",
                        url: "<%=Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath %>/Order.aspx/PopulateCities",
                        data: '{StateID: ' + $('#<%=ddlState.ClientID%>').val() + '}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var items = response.d.tblState;
                            $('#ddlCity').empty();
                            $('#<%=ddlCity.ClientID %>').append('<option  value="0">-- Select City --</option>');

                            for (var i = 0; i < items.length; i++) {
                                $('#<%=ddlCity.ClientID%>').append($('<option></option>').val(items[i].appCityID).html(items[i].appCity));
                            }
                            $('#<%=ddlCity.ClientID %>').append('<option  value="-1">Other</option>');
                            $("#<%=txtState.ClientID %>").hide();
                            ValidatorEnable(document.getElementById("<%=RFVtxtState.ClientID %>"), false);
                        },
                        failure: function (response) {
                            alert(response.d);
                        }
                    });
                    $("#<%=txtCity.ClientID %>").hide();
                    ValidatorEnable(document.getElementById("<%=RFVtxtCity.ClientID %>"), false);

                }
            });
            $('#ddlCity').change(function () {

                debugger
                if ($('#<%=ddlCity.ClientID%>').val() == "-1") {
                    document.getElementById("<%=txtCity.ClientID %>").value = "";
                    $("#<%=txtCity.ClientID %>").show();

                    ValidatorEnable(document.getElementById("<%=RFVtxtCity.ClientID %>"), true);

                }
                else {
                    $("#<%=txtCity.ClientID %>").hide();

                    ValidatorEnable(document.getElementById("<%=RFVtxtCity.ClientID %>"), false);

                }
            });
            $('#btnSubmit').on('click', function () {
                var validated = Page_ClientValidate('Shippingsave');
                if (validated) {
                    var CityName = "", StateName = "", CountryName = "", hdnFormAdressID = "", obj;
                    var Name = $('#txtReceiverName').val();
                    var AddressLine1 = $('#txtAddress').val();
                    var Pincode = $('#txtReceiverPIN').val();
                    var City = $('#ddlCity').val();
                    var State = $('#ddlState').val();
                    var Mobile = $('#txtMobile').val();
                    var Email = $('#txtEmail').val();
                    var Country = $('#ddlCountry').val();
                    if ($('#txtCity').val() != null) {
                        CityName = $('#txtCity').val();
                    }
                    if ($('#txtState').val() != null) {
                        StateName = $('#txtState').val();
                    }
                    if ($('#txtcountry').val() != null) {
                        CountryName = $('#txtcountry').val();
                    }
                    if ($("#hdnFormAdressID").val() != null) {
                        hdnFormAdressID = $("#hdnFormAdressID").val();
                    }
                    obj = { strName: Name, strAddressLine1: AddressLine1, strPincode: Pincode, strCity: City, strState: State, strCountry: Country, strCityName: CityName, strStateName: StateName, strCountryName: CountryName, strhdnFormAdressID: hdnFormAdressID, strMobile: Mobile, strEmail: Email };
                }
                $.ajax(
                {
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "<%=Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath %>/Order.aspx/SaveRecord",
                    data: JSON.stringify(obj),
                    dataType: "json",
                    success: function (data) {
                        $("#<%=divAdressForm.ClientID %>").hide();
                        $("#<%=divAdressList.ClientID %>").show();
                        document.getElementById('<%= btnHide.ClientID %>').click();
                    },
                    error: function (data) {
                    }
                });
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--  <asp:UpdatePanel ID="upmail" runat="server">
        <ContentTemplate>--%>
    <div>
        <div class="mens">
            <div class="main">
                <div class="wrap">
                    <ul class="breadcrumb breadcrumb__t">
                        <a class="" href='<%= objPageBase.GetAlias("Default.aspx")%>'>Home</a> / <span>My Cart</span>
                    </ul>
                    <div style="padding: 1%;">
                        <div id="divProduct" class="minHeight OrderGrid"   runat="server">
                            <fieldset class="input">
                                <div class="shoping_bag">
                                    <h4>
                                        <div class="float-lt">
                                            <img src="images/bag1.png" /></div>
                                        <div class="float-lt setTopMargin4">
                                            My Shopping Bag / <span>
                                                <asp:Label ID="lblCount" runat="server" Text="0"></asp:Label>
                                                item</span>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </h4>
                                    <div class="clear">
                                    </div>
                                </div>
                                <div>
                                    <DInfo:DisplayInfo runat="server" ID="DInfo" />
                                    <asp:GridView ID="dgvCart" runat="server" AutoGenerateColumns="false" AllowSorting="false"
                                        DataKeyNames="appProductDetailID" BorderWidth="0" CssClass="gridmain" OnRowCommand="dgvCart_RowCommand"
                                        OnRowDeleting="dgvCart_RowDeleting">
                                        <HeaderStyle CssClass="gridheader" />
                                        <RowStyle CssClass="gridrow" />
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="20%" HeaderStyle-Width="20%">
                                                <ItemTemplate>
                                                    <div class="shoping1_of_1_Product">
                                                        <img src='<%=strServerURL %>admin/<%#Eval("appNormalImage") %>' class="img-responsive"
                                                            alt='<%#Eval("appProductName") %>' />
                                                    </div>
                                                    <div class="shoping1_of_2_ProductName">
                                                        <h4>
                                                            <a href='<%# objPageBase.GetAlias("ProductDetail.aspx") + generateUrl(Eval("appProductName").ToString()) %>'
                                                                class="setColor" target="_blank">
                                                                <%#Eval("appProductName") %></a>
                                                        </h4>
                                                        <span>SKU No : <b>
                                                            <%#Eval("appSKUNo")%></b></span>
                                                        <ul class="s_icons setTopMargin2">
                                                            <li>
                                                                <asp:LinkButton ID="lnkbtnColor" runat="server" CommandName="Delete" ToolTip="Delete"
                                                                    OnClientClick="return confirm('Do you really want to delete Product?');" CommandArgument='<%#Eval("appProductID") %>'>
                                        <img src="images/s_icon3.png" alt=""></asp:LinkButton></li>
                                                        </ul>
                                                    </div>
                                                    <div class="clear">
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="1%" HeaderStyle-Width="1%" HeaderText="Quantity"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <div class="registration_form">
                                                        <asp:TextBox ID="txtQty" runat="server" Text='<%#Eval("appQty") %>' onkeypress="return isNumber(event)"
                                                            Width="35" Style="text-align: center;"></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="5%" HeaderStyle-Width="5%" HeaderText="Price"
                                                HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <%= Session[appFunctions.Session.CurrencyImage.ToString()]%><%# Math.Round(Convert.ToDecimal(Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString()) * Convert.ToDecimal(Eval("appRealPrice").ToString()), 0)%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="5%" HeaderStyle-Width="5%" HeaderText="Discount"
                                                HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <%= Session[appFunctions.Session.CurrencyImage.ToString()]%><%# Math.Round(Convert.ToDecimal(Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString()) * Convert.ToDecimal(Eval("appDiscountPrice").ToString()), 0)%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="5%" HeaderStyle-Width="5%" HeaderText="Total"
                                                HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <%= Session[appFunctions.Session.CurrencyImage.ToString()]%><%# Math.Round(Convert.ToDecimal(Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString()) * Convert.ToDecimal(Eval("appTotalPrice").ToString()), 0)%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <br />
                                <div class="setPadding" id="divProductTotalPrice" runat="server">
                                    <div class="float-lt setBottomPad">
                                        <asp:Button ID="btnUpdateCart" runat="server" Text="Update Cart" OnClick="btnUpdateCart_Click" />
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                                <div class="clear">
                                </div>
                            </fieldset>
                        </div>
                        <div class="OrderAdress" >
                            <div>
                                <div class="shoping_bag">
                                    <h4>
                                        <div class="float-lt">
                                            <%--  <img src="images/bag1.png" />--%></div>
                                        <div class="float-lt setTopMargin4">
                                            ADDRESS DETAILS <span>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </h4>
                                    <div class="clear">
                                    </div>
                                </div>
                                <h3>
                                </h3>
                                <div id="divAdressList" runat="server">
                                    <div align="center">
                                        <asp:Label ID="lblErrorAddress" runat="server" EnableViewState="false"></asp:Label>
                                    </div>
                                    <asp:DataList ID="DataListAdress" runat="server" AutoGenerateColumns="false" AllowSorting="false"
                                        DataKeyField="appAddressId" BorderWidth="0"  OnItemCommand="DataListAdress_ItemCommand"
                                        OnItemDataBound="DataListAdress_RowDataBound">
                                        <ItemTemplate>
                                            <div class="AdressBlock" >
                                                <div>
                                                    <div class="float-lt" width="80%">
                                                        <%#Eval("appName")%>
                                                    </div>
                                                    <div id="btnDefault" class="float-rt" runat="server">
                                                        <asp:LinkButton ID="lnkbtnDefault" runat="server" CommandName="Default" CommandArgument='<%#Eval("appAddressId")%>'>
                                                            <asp:Image ID="Image1" class="btnDefault" runat="server" src='<%#strServerURL+"Images/img2.png"%>' />
                                                        </asp:LinkButton>
                                                    </div>
                                                    <div id="btnNotDefault" class="float-rt" runat="server">
                                                        <asp:LinkButton ID="lnkbtnNotDefault" runat="server" CommandName="Default" CommandArgument='<%#Eval("appAddressId")%>'>
                                                            <asp:Image ID="Image2" class="btnDefault" runat="server" src='<%#strServerURL+"Images/img1.png"%>' />
                                                        </asp:LinkButton>
                                                    </div>
                                                    <div class="clear">
                                                    </div>
                                                </div>
                                                <div>
                                                    <%#Eval("appMobile")%>
                                                </div>
                                                <div>
                                                    <%#Eval("appEmail")%>
                                                </div>
                                                <div>
                                                    <%#Eval("appAddress")%>-<%#Eval("appPincode")%>
                                                </div>
                                                <div>
                                                    <%#Eval("appCity")%>
                                                </div>
                                                <div>
                                                    <%#Eval("appState")%>
                                                </div>
                                                <div>
                                                    <%#Eval("appCountry")%></div>
                                                <div id="IsDefault" runat="server">
                                                </div>
                                                <div>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" CommandArgument='<%#Eval("appAddressId")%>'>Edit</asp:LinkButton>
                                                    /
                                                    <asp:LinkButton ID="lnkbtnCancel" runat="server" CommandName="Cancel" OnClientClick="return confirm('Are you sure you want to delete Address ?');"
                                                        CommandArgument='<%#Eval("appAddressId")%>'>Delete</asp:LinkButton>
                                                </div>
                                                <br />
                                            </div>
                                            <asp:HiddenField ID="hdnAdressID" runat="server" Value='<%#Eval("appAddressId")%>' />
                                            <asp:HiddenField ID="hdnDefault" runat="server" Value='<%#Eval("appIsDefault")%>' />
                                        </ItemTemplate>
                                    </asp:DataList></div>
                            </div>
                            <div class="divAdressList">
                                <div id="divAdressForm" runat="server">
                                    <div class="formPad">
                                        <label>
                                            Full Name :
                                        </label>
                                        <div>
                                            <asp:TextBox class="AdressFormtxt" ID="txtReceiverName" runat="server" ClientIDMode="Static"
                                                placeholder="Name" TabIndex="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="none" ID="RfvReceiver" runat="server" ErrorMessage="Enter Receiver Name"
                                                ValidationGroup="Shippingsave" Text="*" ControlToValidate="txtReceiverName" SetFocusOnError="true"
                                                CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator></div>
                                    </div>
                                    <div class="formPad">
                                        <label>
                                            Mobile :
                                        </label>
                                        <div>
                                            <asp:TextBox class="AdressFormtxt" ID="txtMobile" runat="server" ClientIDMode="Static"
                                                placeholder="Mobile" TabIndex="4">
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator Display="none" ID="RequiredFieldValidator1" runat="server"
                                                ErrorMessage="Enter Mobile" ValidationGroup="Shippingsave" Text="*" ControlToValidate="txtMobile"
                                                SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="REVMobile1" runat="server" Display="none" ValidationGroup="Shippingsave"
                                                Text="*" ControlToValidate="txtMobile" SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="formPad">
                                        <label>
                                            Email :
                                        </label>
                                        <div>
                                            <asp:TextBox class="AdressFormtxt" ID="txtEmail" runat="server" ClientIDMode="Static"
                                                placeholder="Email" TabIndex="4">
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator Display="none" ID="RequiredFieldValidator2" runat="server"
                                                ErrorMessage="Enter Email" ValidationGroup="Shippingsave" Text="*" ControlToValidate="txtEmail"
                                                SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revRecevierEmail" runat="server" Display="none"
                                                ValidationGroup="Shippingsave" Text="*" ControlToValidate="txtEmail" SetFocusOnError="true"
                                                CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="formPad">
                                        <label>
                                            Address :
                                        </label>
                                        <div>
                                            <asp:TextBox class="AdressFormtxt" ID="txtAddress" runat="server" ClientIDMode="Static"
                                                placeholder="Address" TabIndex="4">
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator Display="none" ID="RFVAddress" runat="server" ErrorMessage="Enter Address"
                                                ValidationGroup="Shippingsave" Text="*" ControlToValidate="txtAddress" SetFocusOnError="true"
                                                CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="formPad">
                                        <label>
                                            Pincode :
                                        </label>
                                        <div>
                                            <asp:TextBox class="AdressFormtxt" ID="txtReceiverPIN" runat="server" ClientIDMode="Static"
                                                placeholder="Pincode" TabIndex="4">
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator Display="none" ID="rfvReceiverPIN" runat="server" ErrorMessage="Enter Receiver PIN Code"
                                                ValidationGroup="Shippingsave" Text="*" ControlToValidate="txtReceiverPIN" SetFocusOnError="true"
                                                CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="REVPIN" runat="server" Display="none" ValidationGroup="Shippingsave"
                                                Text="*" ControlToValidate="txtReceiverPIN" SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <asp:UpdatePanel ID="upAddress" runat="server">
                                        <ContentTemplate>
                                            <div class="formPad">
                                                <label>
                                                    Country :
                                                </label>
                                                <div>
                                                    <div class="dropdownLeftOrder">
                                                        <%-- <asp:DropDownList ID="ddlCountry" class="select1" ClientIDMode="Static" runat="server" 
                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                                        </asp:DropDownList>--%>
                                                        <asp:DropDownList ID="ddlCountry" class="select1" runat="server" ClientIDMode="Static">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator Display="none" ID="RFVCountry" runat="server" ErrorMessage="Select Country"
                                                            ValidationGroup="Shippingsave" Text="*" ControlToValidate="ddlCountry" SetFocusOnError="true"
                                                            InitialValue="0" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="dropdownLeft1Order">
                                                        <asp:TextBox ID="txtcountry" class="AdressFormtxt" ClientIDMode="Static" runat="server"
                                                            placeholder="Country">
                                                        </asp:TextBox>
                                                        <asp:RequiredFieldValidator Display="none" ID="RFVtxtCoutry" runat="server" ErrorMessage="Enter Coutry"
                                                            ValidationGroup="Shippingsave" Text="*" ControlToValidate="txtcountry" SetFocusOnError="true"
                                                            CssClass="ErrorLabelStyle" Enabled="false"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="REVtxtCoutry" runat="server" Display="none" ValidationGroup="Shippingsave"
                                                            Text="*" ControlToValidate="txtcountry" SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                                    </div>
                                                    <div style="clear: both">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="formPad">
                                                <label>
                                                    State :
                                                </label>
                                                <div>
                                                    <div class="dropdownLeftOrder">
                                                        <asp:DropDownList ID="ddlState" class="select1" ClientIDMode="Static" runat="server">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator Display="none" ID="RFVState" runat="server" ErrorMessage="Select State"
                                                            ValidationGroup="Shippingsave" Text="*" ControlToValidate="ddlState" SetFocusOnError="true"
                                                            InitialValue="0" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="dropdownLeft1Order">
                                                        <asp:TextBox ID="txtState" class="AdressFormtxt" ClientIDMode="Static" runat="server"
                                                            placeholder="State">
                                                        </asp:TextBox>
                                                        <asp:RequiredFieldValidator Display="none" ID="RFVtxtState" runat="server" ErrorMessage="Enter State"
                                                            ValidationGroup="Shippingsave" Text="*" ControlToValidate="txtState" SetFocusOnError="true"
                                                            CssClass="ErrorLabelStyle" Enabled="false"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="REVtxtState" runat="server" Display="none" ValidationGroup="Shippingsave"
                                                            Text="*" ControlToValidate="txtState" SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                                    </div>
                                                    <div style="clear: both">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="formPad">
                                                <label>
                                                    City :
                                                </label>
                                                <div>
                                                    <div class="dropdownLeftOrder">
                                                        <asp:DropDownList ID="ddlCity" class="select1" ClientIDMode="Static" runat="server">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator Display="none" ID="RFVCity" runat="server" ErrorMessage="Select City"
                                                            ValidationGroup="Shippingsave" Text="*" ControlToValidate="ddlCity" SetFocusOnError="true"
                                                            InitialValue="0" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="dropdownLeft1Order">
                                                        <asp:TextBox ID="txtCity" class="AdressFormtxt" ClientIDMode="Static" runat="server"
                                                            placeholder="City">
                                                        </asp:TextBox>
                                                        <asp:RequiredFieldValidator Display="none" ID="RFVtxtCity" runat="server" ErrorMessage="Enter City"
                                                            ValidationGroup="Shippingsave" Text="*" ControlToValidate="txtCity" SetFocusOnError="true"
                                                            CssClass="ErrorLabelStyle" Enabled="false"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="REVtxtCity" runat="server" Display="none" ValidationGroup="Shippingsave"
                                                            Text="*" ControlToValidate="txtCity" SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                                    </div>
                                                    <div style="clear: both">
                                                    </div>
                                                    <asp:ValidationSummary ID="vsnewOrder" runat="server" EnableClientScript="true" HeaderText="You must Enter Following Fields"
                                                        ShowMessageBox="true" ShowSummary="false" ValidationGroup="Shippingsave" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <%--     <asp:Button ID="btnSubmit" runat="server" ClientIDMode="Static" Text="Save" CssClass="btnPayOrder" />
                                    --%>
                                    <input type="button" id="btnSubmit" class="btnPayOrder" value="Save" />
                                </div>
                                <div>
                                    <asp:LinkButton ID="lnkSeeAll" runat="server" OnClick="lnkSeeAll_Click"> See All</asp:LinkButton>
                                    /
                                    <asp:LinkButton ID="lnkAddNew" runat="server" OnClick="lnkAddNew_Click"> Add </asp:LinkButton>
                                </div>
                                <div>
                                </div>
                                <asp:HiddenField ID="hdnFormAdressID" runat="server" ClientIDMode="Static" />
                            </div>
                        </div>
                        <div class="OrderAmount " style="">
                            <div>
                                <div class="shoping_bag">
                                    <h4>
                                        <div class="float-lt">
                                            <%--<img src="images/bag1.png" />--%></div>
                                        <div class="float-lt setTopMargin4">
                                            Amount <span>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </h4>
                                    <div class="clear">
                                    </div>
                                </div>
                                <h3>
                                </h3>
                            </div>
                            <div class="divAdressList">
                                <div>
                                    <div>
                                        <div class="float-lt">
                                            <p class="tot">
                                                Total &nbsp;
                                            </p>
                                        </div>
                                        <div class="float-rt">
                                            <asp:Label ID="lblTotalPrice" runat="server" Text="0" CssClass="setColor"></asp:Label>
                                        </div>
                                         <div class="clear">
                                        </div>
                                        <div>
                                            <%--    <asp:Button ID="btnUpdateCart" runat="server" Text="Confirm & Next" OnClick="btnUpdateCart_Click" />
                                            --%>
                                            <asp:Button ID="btnCashOnDelivery" runat="server" Text="COD" CssClass="btnPayOrder" style="width:100%"
                                                OnClick="btnCashOnDelivery_Click" />
                                            <asp:Button ID="btnPayNow" runat="server" Text="Pay Online Now" CssClass="btnPayOrder" style="width:100%"
                                                OnClick="btnPayNow_Click" />
                                        </div>
                                       
                                    </div>
                                </div>
                                <asp:HiddenField ID="HiddenField1" runat="server" ClientIDMode="Static" />
                            </div>
                        </div>
                        <div class="clear">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Button ID="btnHide" runat="server" Style="display: none;" OnClick="btnHide_click" />
    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
