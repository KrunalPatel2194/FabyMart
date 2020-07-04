<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="TrackOrder.aspx.cs" Inherits="MyOrderList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/Customer.ascx" TagName="Customer" TagPrefix="Home" %>
<%@ Register Src="Admin/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta name="description" content="<%=metaDescription %>" />
    <meta name="keywords" content="<%=metaKeywords %>" />
    <script type="text/javascript">
        function btnClick(Control, type, id, btnType, e) {
            $("#<%=hdnOrderId.ClientID %>").val("");
            $("#<%=hdnOrderId.ClientID %>").val(id);
            $('#divReturnProduct').fadeOut('slow');
            var IsChk = "false";
            var chkVal = "";
            $("." + type + "." + id).each(function () {
                if (this.checked) {
                    IsChk = "true";
                    chkVal = chkVal + $(this).val() + ",";
                }
            });
            $('input:checkbox').removeAttr('checked');
            if (IsChk == "true") {
                if (type == "Cancel") {
                    var result = confirm("Are your sure " + type + " Product ?");
                    if (result) {
                        $("#<%=hdnSelectedSubOrderIDs.ClientID %>").val(chkVal);
                        return true;
                    }
                    else {
                        $("#<%=hdnSelectedSubOrderIDs.ClientID %>").val("");
                        $("#<%=hdnOrderId.ClientID %>").val("");
                    }
                }
                if (type == "Return") {
                    $("#<%=hdnSelectedSubOrderIDs.ClientID %>").val("");
                    $("#<%=hdnSelectedSubOrderIDs.ClientID %>").val(chkVal);
                    return true;
                    //                    var Wheight = e.pageY - 250;
                    //                    var Wwidth = 200;
                    //                    overlay.show();
                    //                    overlay.appendTo(document.body);
                    //                    $('#divReturnProduct').css({ 'top': Wheight, 'left': Wwidth, 'position': 'absolute', 'z-index': '110' }).fadeIn('slow');
                }
            }
            else {
                alert(" Choose Valid product for " + type);
            }
            return false;
        }
        function divProductQtypopupClose() {
            $find("MpeReturnRequest").hide();
            $("#<%=hdnOrderId.ClientID %>").val("");
            return false;
        }
        function divReturnProductOk() {
            if (Page_ClientValidate("ReturnProduct")) {
                var result = confirm("Are your sure  Return Product ?");
                if (result) {
                    $find("MpeReturnRequest").hide();
                    return true;
                }
                else {
                    $("#<%=hdnSelectedSubOrderIDs.ClientID %>").val("");
                    $("#<%=hdnOrderId.ClientID %>").val("");
                    $find("MpeReturnRequest").hide();
                    return false;
                }
            }
            return false;
        }
        function divReturnProductslipClose() {
            $find("MpeReturnslip").hide();
            $("#<%=hdnReturnOrderID.ClientID %>").val("");
            return false;
        }
        function CallProductInvoice() {
            wopen($("#<%=hdnReturnOrderID.ClientID %>").val(), "Product Slip", 1000, 500);
        }
        function ddlCourierCompany() {
            var iCourierCompanyId = $("#<%=ddlCourierCompany.ClientID%>").val();

            if (iCourierCompanyId == -1) {
                $("#CourierCompanyContact").fadeIn("slow");

            }
            else {
                $("#CourierCompanyContact").fadeOut("slow");

            }
        }
        function validatemobilenumber(mobilenumber) {
            var pattern = /^\d{10}$/;
            if (pattern.test(mobilenumber)) {

                return true;
            } else {
                return false;
            }

            return false;
        }
        function validateURL(URL) {
            var filter = /^http[s]*\:\/\/[wwW]{3}\.+[a-zA-Z0-9]+\.[a-zA-Z]{2,3}.*$|^http[s]*\:\/\/[^w]{3}[a-zA-Z0-9]+\.[a-zA-Z]{2,3}.*$|http[s]*\:\/\/[0-9]{2,3}\.[0-9]{2,3}\.[0-9]{2,3}\.[0-9]{2,3}.*$/;
            if (filter.test(URL)) {
                return true;
            }
            else {
                return false;
            }
        }

        function check() {
            var iCourierCompanyId = $("#<%=ddlCourierCompany.ClientID%>").val();
            var msg = "";
            if (iCourierCompanyId == -1) {
                var txtCourierContactNo = $("#<%=txtCourierContactNo.ClientID %>").val();
                var txtSiteName = $("#<%=txtSiteName.ClientID %>").val();
                var txtDocketNo = $("#<%=txtDocketNo.ClientID %>").val();
                if (txtCourierContactNo == "") {
                    msg += "- Please enter contact number" + "\n";
                } else if (!validatemobilenumber(txtCourierContactNo)) {
                    msg += "- Please enter valid contact number" + "\n";
                }
                if (txtSiteName == "") {
                    msg += "- Please enter site url" + "\n";
                }
                else if (!validateURL(txtSiteName)) {
                    msg += "- Please enter valid site url" + "\n";
                }
                if (txtDocketNo == "") {
                    msg += "- Please enter docket number" + "\n";
                }
                if (msg != "") {
                    alert(msg);
                    return false;
                } else {
                    return true;
                }
            }
            else {
                var ddlCourierCompany = $("#<%=ddlCourierCompany.ClientID %>").val();
                var txtDocketNo = $("#<%=txtDocketNo.ClientID %>").val();
                if (ddlCourierCompany == "0") {
                    msg += "- Please select courier company" + "\n";
                }

                if (txtDocketNo == "") {
                    msg += "- Please enter docket number" + "\n";
                }
                if (msg != "") {
                    alert(msg);
                    return false;
                } else {
                    return true;
                }
            }
            return false;
        }

        function searchValidate() {

            if (document.getElementById("<%=txtOrderNo.ClientID%>").value == "") {
                alert("Enter Order No..");
                document.getElementById("<%=txtOrderNo.ClientID%>").focus();
                return false;
            }
            Page_ClientValidate('SearchOrder');
            if (Page_IsValid('SearchOrder') == true) {
                this.disabled = true;
                return true;
            }
            else {
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
                    <a class="home" href='<%= objPageBase.GetAlias("Default.aspx")%>'>Home</a> / <span>Track
                        Order</span>
                </ul>
                <div class="clear">
                </div>
                <div class="minHeight">
                    <div class="main">
                        <asp:UpdatePanel ID="UpMain" runat="server">
                            <ContentTemplate>
                                <div>
                                    <div class="setDInfoWidth">
                                        <DInfo:DisplayInfo runat="server" ID="DInfo" />
                                    </div>
                                    <div class="col_1_of_changePassword ">
                                        <h4 class="titleTrackOrder">
                                            Track Order</h4>
                                        <div class="register_account1">
                                            <asp:Panel ID="pnlTrackOrder" runat="server" DefaultButton="btnSearch">
                                                <fieldset class="input">
                                                    <div id="divTrackOrder" class="divTrackOrder">
                                                        <asp:Label ID="lblOrderNo" runat="server">Order No.</asp:Label>
                                                        <asp:TextBox runat="server" ID="txtOrderNo" placeholder="Order No." CssClass="divTrackOrderTxtbox"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="revOrderNo" runat="server" Display="Dynamic"
                                                            ValidationGroup="SearchOrder" Text="*" ControlToValidate="txtOrderNo" SetFocusOnError="true"
                                                            CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                                        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search"
                                                            CssClass="divTrackOrderbtn" OnClientClick=" return searchValidate() " ValidationGroup="SearchOrder" />
                                                        <div class="clear">
                                                        </div>
                                                    </div>
                                                </fieldset>
                                            </asp:Panel>
                                        </div>
                                        <br />
                                        <br />
                                        <div class="width_">
                                            <asp:DataList ID="DataListMyOrder" runat="server" AutoGenerateColumns="false" AllowSorting="false"
                                                DataKeyField="appOrderID" BorderWidth="0" Width="100%" OnItemDataBound="DataListMyOrder_RowDataBound">
                                                <ItemTemplate>
                                                    <div class="OrderList">
                                                        <div class="OrderHeader">
                                                            <div class="Order">
                                                                Order No :
                                                                <%#Eval("appOrderNo")%>
                                                            </div>
                                                            <div class="OrderDate">
                                                                <%# DataBinder.Eval(Container.DataItem, "appCreatedDate", "{0:MMMM dd, yyyy}")%>
                                                            </div>
                                                        </div>
                                                        <div class="OrderBody">
                                                            <asp:DataList ID="DataListSubOrder" runat="server" AutoGenerateColumns="false" AllowSorting="false"
                                                                DataKeyField="appSubOrderID" BorderWidth="0" Width="100%" OnItemDataBound="DataListSubOrder_RowDataBound"
                                                                OnItemCommand="DataListSubOrder_ItemCommand">
                                                                <ItemTemplate>
                                                                    <div class="OrderSubBody">
                                                                        <div class="OrderChk ">
                                                                            &nbsp;
                                                                            <input type="checkbox" id="chkSelectRow" runat="server" value=' <%#Eval("appSubOrderID")%>' />
                                                                        </div>
                                                                        <div class="OrderImg">
                                                                            <img src="<%#strServerURL+"admin/"+Eval("appNormalImage") %>" />
                                                                        </div>
                                                                        <div class="OrderProduct">
                                                                            <div class="productName">
                                                                                <a href='<%# objPageBase.GetAlias("ProductDetail.aspx") + generateUrl(Eval("appProductName").ToString()) + "/" +  generateUrl(Eval("appColorLink").ToString())%>'
                                                                                    target="_blank" style="font-size: 15px; color: #ED258F;">
                                                                                    <%#Eval("appProductName")%>
                                                                                </a>
                                                                            </div>
                                                                            <div class="Skuno">
                                                                                <%-- <span>size : <b>
                                                                            <%#Eval("appSize") %></b>&nbsp;&nbsp; | code : <b>
                                                                                <%#Eval("appProductCode") %></b></span> |--%>
                                                                                SKU NO :<span> <b>
                                                                                    <%#Eval("appSKUNo")%></b></span></div>
                                                                            <div style="clear: both;">
                                                                            </div>
                                                                        </div>
                                                                        <div class="OrderPriceQty">
                                                                            <div>
                                                                                <div class="PriceqtyInOrderList">
                                                                                    <%#Eval("appSellingPrice")%>
                                                                                    <span class="spanBalck">x </span>
                                                                                    <%#Eval("appQty")%>
                                                                                </div>
                                                                                <div class="float-lt">
                                                                                    <span class="spanBalck">= </span><span class="spanValue">
                                                                                </div>
                                                                                <div class="PriceInright">
                                                                                    <%#Eval("appTotal")%></span><br />
                                                                                </div>
                                                                                <div class="clear">
                                                                                </div>
                                                                            </div>
                                                                            <div runat="server" id="divDiscount">
                                                                                <div class="PriceqtyInOrderList">
                                                                                    Discount
                                                                                </div>
                                                                                <div class="float-lt">
                                                                                    <span class="spanBalck">=</span><span class="spanValue">
                                                                                </div>
                                                                                <div class="divDiscount">
                                                                                    -<%#Eval("appTotalDiscount")%>
                                                                                </div>
                                                                                <div class="clear">
                                                                                </div>
                                                                            </div>
                                                                            <div>
                                                                                <br />
                                                                                <hr class="hrPay" />
                                                                            </div>
                                                                            <div>
                                                                                <div style="text-align: right; color: #ED258F;">
                                                                                    <%#Eval("appTotalamountToBePaid")%>
                                                                                </div>
                                                                                <div class="clear">
                                                                                </div>
                                                                            </div>
                                                                            <div class="clear">
                                                                            </div>
                                                                        </div>
                                                                        <div class="OrderStatusName">
                                                                            <asp:Label ID="lblStatusName" runat="server">-</asp:Label>
                                                                        </div>
                                                                        <div class="OrderStatus">
                                                                            <asp:LinkButton ID="lnkbtnCancel" runat="server" CssClass="btnInOrderList" OnClientClick='<%# "return btnClick(this,\"Cancel\","+Eval("appOrderID") +",\"btnCancel\",event);"%>'
                                                                                OnClick="btnCancel_Click"> Cancel</asp:LinkButton>
                                                                            <asp:LinkButton ID="lnkbtnReturn" runat="server" CssClass="btnInOrderList" OnClientClick='<%# "return btnClick(this,\"Return\","+Eval("appOrderID") +",\"btnReturn\",event);"%>'
                                                                                CommandName="ReturnRequest" CommandArgument='<%#Eval("appSubOrderID")%>'>  Return</asp:LinkButton>
                                                                            <asp:LinkButton ID="lnkReturnAddress" runat="server" Visible="false" CommandName="ReturnAddress"
                                                                                CssClass="SlipLink" CommandArgument='<%#Eval("appReturnOrderID")%>'>ShippingSlip</asp:LinkButton>
                                                                            <asp:LinkButton ID="lnkDispatch" runat="server" Visible="false" CssClass="btnInOrderList"
                                                                                CommandName="Dispatch" CommandArgument='<%#Eval("appReturnOrderID")%>'>Dispatch</asp:LinkButton>
                                                                        </div>
                                                                        <div style="clear: both;">
                                                                        </div>
                                                                    </div>
                                                                    <asp:HiddenField ID="hdnTotaldiscount" runat="server" Value='<%#Eval("appTotalDiscount")%>' />
                                                                    <asp:HiddenField ID="hdnDocketNo" runat="server" Value='<%#Eval("appDocketNo")%>' />
                                                                    <asp:HiddenField ID="hdnStatus" runat="server" Value='<%#Eval("appSubOrderStatusID")%>' />
                                                                    <asp:HiddenField ID="hdnOrderId" runat="server" Value='<%#Eval("appOrderID")%>' />
                                                                    <asp:HiddenField ID="hdnReturnStatus" runat="server" Value='<%#Eval("appReturnStatus")%>' />
                                                                </ItemTemplate>
                                                            </asp:DataList>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                    <br />
                                    <input type="button" runat="server" id="btnReturnRequest" style="display: none;" />
                                    <cc1:ModalPopupExtender ID="MpeReturnRequest" BehaviorID="MpeReturnRequest" runat="server"
                                        TargetControlID="btnReturnRequest" PopupControlID="divReturnProduct" BackgroundCssClass="modalbackground"
                                        DropShadow="false">
                                    </cc1:ModalPopupExtender>
                                    <div id="divReturnProduct" class="divPopup" style="display: none; width: 600px;">
                                        <div class="modalbclose" onclick="divProductQtypopupClose();">
                                            X </span>
                                        </div>
                                        <div class="HeaderPart">
                                            <b>Return Product</b>
                                        </div>
                                        <div>
                                            <asp:Panel ID="pnlRetuenProduct" runat="server" DefaultButton="btnReturn">
                                                <div style="margin-top: 10px;">
                                                    <div class="divPopupbody" align="center">
                                                        <div class="registration" style="padding: 0px 0px;">
                                                            <div class="registration_left">
                                                                <h2>
                                                                    Pickup Address
                                                                </h2>
                                                                <div class="registration_form">
                                                                    <div>
                                                                        <label>
                                                                            <asp:TextBox ID="txtPickupName" runat="server" placeholder="PickUp Name:"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator Display="none" ID="RfvReceiver" runat="server" ErrorMessage="Enter Your Name"
                                                                                ValidationGroup="ReturnProduct" Text="*" ControlToValidate="txtPickupName" SetFocusOnError="true"
                                                                                CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                                        </label>
                                                                    </div>
                                                                    <div>
                                                                        <label>
                                                                            <asp:TextBox ID="txPickupMobile1" runat="server" placeholder="mobile number:"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator Display="none" ID="RFVMobile1" runat="server" ErrorMessage="Enter Mobile Number 1"
                                                                                ValidationGroup="ReturnProduct" Text="*" ControlToValidate="txPickupMobile1"
                                                                                SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                                            <asp:RegularExpressionValidator ID="REVMobile1" runat="server" Display="none" ValidationGroup="ReturnProduct"
                                                                                Text="*" ControlToValidate="txPickupMobile1" SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                                                        </label>
                                                                    </div>
                                                                    <div>
                                                                        <label>
                                                                            <asp:TextBox ID="txtPickupMobile2" runat="server" placeholder="mobile number:"></asp:TextBox>
                                                                            <asp:RegularExpressionValidator ID="REVMobile2" runat="server" Display="none" ValidationGroup="ReturnProduct"
                                                                                Text="*" ControlToValidate="txtPickupMobile2" SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                                                        </label>
                                                                    </div>
                                                                    <div>
                                                                        <label>
                                                                            <asp:TextBox ID="txtPickupAddress" runat="server" placeholder="Address">
                                                                            </asp:TextBox>
                                                                            <asp:RequiredFieldValidator Display="none" ID="RFVAddress" runat="server" ErrorMessage="Enter Address"
                                                                                ValidationGroup="ReturnProduct" Text="*" ControlToValidate="txtPickupAddress"
                                                                                SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                                        </label>
                                                                    </div>
                                                                    <div>
                                                                        <label>
                                                                            <asp:TextBox ID="txtPickupPIN" runat="server" placeholder="PIN Code">
                                                                            </asp:TextBox>
                                                                            <asp:RequiredFieldValidator Display="none" ID="rfvReceiverPIN" runat="server" ErrorMessage="Enter PIN Code"
                                                                                ValidationGroup="ReturnProduct" Text="*" ControlToValidate="txtPickupPIN" SetFocusOnError="true"
                                                                                CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                                            <asp:RegularExpressionValidator ID="REVPickupPinCode" runat="server" Display="none"
                                                                                ValidationGroup="ReturnProduct" Text="*" ControlToValidate="txtPickupPIN" SetFocusOnError="true"
                                                                                CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                                                        </label>
                                                                    </div>
                                                                    <div>
                                                                        <label>
                                                                            <asp:TextBox ID="txtPreferedTime" runat="server" placeholder="Prefered Time">
                                                                            </asp:TextBox>
                                                                        </label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="registration_left">
                                                                <h2>
                                                                    Why ? <span>Return Product </span>
                                                                </h2>
                                                                <div class="registration_form">
                                                                    <div>
                                                                        <label>
                                                                            <asp:DropDownList runat="server" ID="ddlReason">
                                                                                <asp:ListItem Text="--Select Reason--" Value="0"></asp:ListItem>
                                                                                <asp:ListItem Text="Reason1" Value="Reason 1"></asp:ListItem>
                                                                                <asp:ListItem Text="Reason2" Value="Reason 2"></asp:ListItem>
                                                                                <asp:ListItem Text="Reason3" Value="Reason 3"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator Display="none" ID="RFVReason1" runat="server" ErrorMessage="Select Reason"
                                                                                ValidationGroup="ReturnProduct" Text="*" ControlToValidate="ddlReason" SetFocusOnError="true"
                                                                                InitialValue="0"></asp:RequiredFieldValidator>
                                                                        </label>
                                                                    </div>
                                                                    <div>
                                                                        <label>
                                                                            <asp:TextBox runat="server" ID="txtNote" Placeholder="Enter Note" TextMode="MultiLine"
                                                                                Rows="5">
                                                                            </asp:TextBox>
                                                                            <asp:RequiredFieldValidator Display="none" ID="RFVNote" runat="server" ErrorMessage="Enter Text"
                                                                                ValidationGroup="ReturnProduct" Text="*" ControlToValidate="txtNote" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                        </label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                                <div class="divRightButton">
                                                    <asp:Button runat="server" ID="btnReturn" CssClass="divbtn " Text="Save" ValidationGroup="ReturnProduct"
                                                        oolTip="Return order" OnClientClick="return divReturnProductOk();" OnClick="btnReturnProduct_Click">
                                                    </asp:Button>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                    <input type="button" runat="server" id="btnReturnslip" style="display: none;" />
                                    <cc1:ModalPopupExtender ID="Mpeslip" BehaviorID="MpeReturnslip" runat="server" TargetControlID="btnReturnslip"
                                        PopupControlID="divslip" BackgroundCssClass="modalbackground" DropShadow="false">
                                    </cc1:ModalPopupExtender>
                                    <div id="divslip" runat="server" class="divPopup" style="display: none; width: 700px;">
                                        <div class="modalbclose" onclick="divReturnProductslipClose();">
                                            X </span>
                                        </div>
                                        <div class="HeaderPart">
                                            <b>Return Product Slip</b>
                                        </div>
                                        <div>
                                            <asp:Panel ID="pnlReturnProductSlip" runat="server" DefaultButton="btnReturnGenrateSlip">
                                                <div style="margin-top: 10px;">
                                                    <div class="divPopupbody">
                                                        <div class="row ">
                                                            <%--<div class="login-form">--%>
                                                            <div class="margin1">
                                                                Enter Docket No :
                                                                <asp:TextBox Class="ReturnSlipControl" ID="txtDocketNo" runat="server" placeholder="Docket No:"></asp:TextBox>
                                                                <%--<asp:RequiredFieldValidator Display="none" ID="RFVDocketNo" runat="server" ErrorMessage="Enter Docket No"
                                                                ValidationGroup="Returnslip" Text="*" ControlToValidate="txtDocketNo" SetFocusOnError="true"
                                                                CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>--%>
                                                            </div>
                                                            <div class="margin1">
                                                                <div class="float-lt">
                                                                    Courier Company</div>
                                                                <div class="float-lt">
                                                                    <asp:DropDownList ID="ddlCourierCompany" runat="server" class="DrpdwnInReturnSlip"
                                                                        onchange="ddlCourierCompany()">
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                                <%-- <asp:RequiredFieldValidator Display="none" ID="RFVCourierCompany" runat="server"
                                                                ErrorMessage="Select Courier Company" ValidationGroup="Returnslip" Text="*" ControlToValidate="ddlCourierCompany"
                                                                InitialValue="0" SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>--%>
                                                            </div>
                                                            <div id="CourierCompanyContact" style="display: none;">
                                                                <div class="margin1">
                                                                    Courier Contact no.
                                                                    <asp:TextBox Class="ReturnSlipControl" runat="server" ID="txtCourierContactNo" Placeholder="Courier Company Contact No."> </asp:TextBox>
                                                                    <%-- <asp:RequiredFieldValidator Display="none" ID="RFVMobileNo" runat="server" ErrorMessage="Enter Courier Company Contact No."
                                                                    ValidationGroup="Returnslip" Text="*" Enabled="false" ControlToValidate="txtCourierContactNo"
                                                                    SetFocusOnError="true">
                                                                </asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="REVMobile" runat="server" Display="none" ValidationGroup="Returnslip"
                                                                    Text="*" ControlToValidate="txtCourierContactNo" SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>--%>
                                                                </div>
                                                                <div class="margin1">
                                                                    Site URL:
                                                                    <asp:TextBox Class="ReturnSlipControl" runat="server" ID="txtSiteName" Placeholder="Site URL"> </asp:TextBox>
                                                                    <%-- <asp:RequiredFieldValidator Display="none" ID="RFVSiteName" runat="server" ErrorMessage="Enter Courier Company Site URL."
                                                                    ValidationGroup="Returnslip" Text="*" Enabled="false" ControlToValidate="txtSiteName"
                                                                    SetFocusOnError="true">
                                                                </asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="REVSiteName" runat="server" Display="none" ValidationGroup="Returnslip"
                                                                    Text="*" ControlToValidate="txtSiteName" SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>--%>
                                                                </div>
                                                            </div>
                                                            <%--</div>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                                <hr class="nomargin" style="margin: 5px 0px;" />
                                                <div class="divRightButton">
                                                    <asp:Button runat="server" ID="btnReturnGenrateSlip" CssClass="divbtn " Text="Genrate"
                                                        OnClientClick="return check();" OnClick="btnReturnGenrateSlip_Click" ValidationGroup="Returnslip">
                                                    </asp:Button>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                    <asp:HiddenField ID="hdnOrderId" Value="" runat="server" />
                                    <asp:HiddenField ID="hdnSelectedIDs" Value="" runat="server" />
                                    <asp:HiddenField ID="hdnSelectedSubOrderIDs" Value="" runat="server" />
                                    <asp:HiddenField ID="hdnType" Value="" runat="server" />
                                    <asp:HiddenField ID="hdnReturnOrderID" Value="" runat="server" />
                                    <asp:HiddenField ID="hdnPrevStatus" Value="" runat="server" />
                                    <asp:ValidationSummary ID="vsReturnProduct" ShowMessageBox="true" EnableClientScript="true"
                                        HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                                        ValidationGroup="ReturnProduct" />
                                    <asp:ValidationSummary ID="vsReturnslip" ShowMessageBox="true" EnableClientScript="true"
                                        HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                                        ValidationGroup="Returnslip" />
                                    <asp:ValidationSummary ID="vsSearchOrder" ShowMessageBox="true" EnableClientScript="true"
                                        HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                                        ValidationGroup="SearchOrder" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
        <div class="clear">
        </div>
    </div>
</asp:Content>
