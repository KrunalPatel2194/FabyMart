<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="ProductDetail.aspx.cs" Inherits="ProductDetail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/ViewCart.ascx" TagName="Cart" TagPrefix="MyCart" %>
<%@ Register Src="~/UserControls/Login.ascx" TagName="Login" TagPrefix="MyLogin" %>
<%@ Register Src="~/UserControls/Inquiry.ascx" TagName="Inquiry" TagPrefix="MyInquiry" %>
<%@ Register Src="~/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta name="description" content="<%=metaDescription %>" />
    <meta name="keywords" content="<%=metaKeywords %>" />
    <script type="text/javascript" src="<%=strServerURL %>js/tabs.js"></script>
    <script src="<%=strServerURL %>js/jquery.zoom.js" type="text/javascript"></script>
    <script type="text/javascript">
        function Onload() {
            try {
                document.getElementById("img").src = document.getElementById("<%=hdnImg.ClientID%>").value;
                $('#imgZoom').zoom();
            }
            catch (e) {
            }
        }
        function setimg(img) {
            $(".selectimg").each(function () {
                $(this).removeClass("selectimg");
            });
            $(img).parent().parent().addClass("selectimg");
            var strPath = img.src.replace('_Normal', '_Large');
            document.getElementById("<%=hdnImg.ClientID%>").value = strPath;
            document.getElementById("img").src = strPath;
            //Thumb Large
            $('#imgZoom').zoom();
        }
    </script>
    <!-- start zoom -->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <asp:UpdatePanel runat="server" ID="up">
        <ContentTemplate>--%>
    <script type="text/javascript">
        Sys.Application.add_load(Onload);
    </script>
    <div class="mens setBottomMargin">
        <div class="main">
            <div class="wrap">
                <ul class="breadcrumb breadcrumb__t">
                    <a href='<%=objPageBase.GetAlias("Default.aspx") %>'>Home</a> / <a id="aCategory"
                        runat="server"></a><a id="aSubCategory" runat="server"></a>
                    <asp:Label runat="server" ID="lblLiteral"></asp:Label>
                </ul>
                <div class="cont ProductDetailLeft">
                    <div class="grid images_3_of_2">
                        <div>
                            <span class='zoom' id='imgZoom' style="width: 100%;">
                                <img id="img" width='100%' />
                            </span>
                        </div>
                        <div class="list">
                            <asp:Repeater ID="RepImg" runat="server">
                                <ItemTemplate>
                                    <div class="Imglist">
                                        <div class="product_image_in_ProductsImages">
                                            <div class="liImg <%# Eval("appIsDefault").ToString().ToLower() == "true" ? "selectimg" : "" %>">
                                                <img id="imgProduct" runat="server" width="100" src='<%#strServerURL +"admin/"+Eval("AppNormalImage") %>'
                                                    onclick="setimg(this)" />
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <div class="clear">
                            </div>
                        </div>
                    </div>
                    <div class="desc1 span_3_of_2">
                        <DInfo:DisplayInfo runat="server" ID="DInfo" />
                        <h3 class="m_3">
                            <asp:Label ID="lblProductName" runat="server"></asp:Label>
                        </h3>
                        <div class="float-lt" style="color: #555; font-size: 14px">
                            SKU No.:
                            <asp:Label ID="LabelSKUNo" runat="server" Style="color: #555; font-weight: bold;"></asp:Label>
                        </div>
                        <div style="clear: both">
                        </div>
                        <p class="m_5">
                            <div class="divPrice">
                                <div class="divpriceLeft">
                                    <div>
                                        <span id="SpanMRP" class="priceMiddle1" runat="server"></span>
                                    </div>
                                    <div>
                                        <span id="SpanMRP2" class="priceRight" runat="server"></span>
                                    </div>
                                    <div>
                                        <span id="SpanPrice" runat="server" class="blueFont"></span>
                                    </div>
                                </div>
                                <div class="divpriceImg">
                                    <img src="<%=strServerURL %>Images/guaranty.png" width="70" title="100% Lowest Price guaranty" />
                                </div>
                                <div id="divProductOff" runat="server">
                                    <p class="discountper" style="z-index: -1;">
                                        <asp:Label ID="lblDiscount" CssClass="per" runat="server"></asp:Label><span class="off">&nbsp;Off</span>
                                        <asp:Label ID="lblSaveRupee" CssClass="off" runat="server"></asp:Label>
                                    </p>
                                    <div style="clear: both">
                                    </div>
                                </div>
                                <div style="clear: both">
                                </div>
                            </div>
                        </p>
                        <div class="det_nav1" style="float: left; margin-right: 70Px; margin-top: 19px; color: #555;
                            font-size: 14px" id="divProductColor" runat="server">
                            <h5>
                                Colors :</h5>
                            <asp:DataList ID="dtProductColor" runat="server" RepeatDirection="Vertical" CellPadding="0"
                                RepeatColumns="9" CellSpacing="0" OnItemCommand="dtProductColor_ItemCommand">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbtnColor" runat="server" CommandName="ProductColor" ToolTip="Color"
                                        CommandArgument='<%#Eval("appProductColorID") %>'>
                                        <asp:Image ID="img1" Style="width: 24px; height: 24px; margin: 4px; margin-top: 10px;
                                            border: 1px solid #aaa;" runat="server" ImageUrl='<%#strServerURL +"admin/"+Eval("appColorImage") %>'
                                            AlternateText='<%#Eval("appColorName") %>' />
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                        <div class="det_nav1" style="float: left; margin-right: 80Px; margin-top: 19px; color: #555;
                            font-size: 14px" id="divProductSize" runat="server">
                            <h5>
                                Select a size :</h5>
                            <div class=" sky-form col col-4" style="margin-top: 10Px;">
                                <asp:DropDownList ID="ddlSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSize_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="det_nav1" style="float: left; margin-right: 80Px; margin-top: 19px; color: #555;
                            font-size: 14px" id="div1" runat="server">
                            <div class=" sky-form col col-4" style="margin-top: 10Px;">
                                <asp:LinkButton ID="lnkWish" runat="server" ToolTip="Add To Wishlist" OnClick="lnkWish_Click"><span class="active-icon addtofavourite"></span></asp:LinkButton>
                                <asp:LinkButton ID="lnkInquiry" runat="server" ToolTip="Inquiry about this product"
                                    OnClick="lnkInquiry_Click"><span class="active-icon enquiry"></span></asp:LinkButton>
                            </div>
                        </div>
                        <div style="clear: both;">
                        </div>
                        <div class="btn_form">
                            <asp:Panel ID="pnlQtyProductDetail" runat="server" DefaultButton="btnCart">
                                <div class="float-lt">
                                    <asp:TextBox ID="txtQty" runat="server" placeholder="Quantity" Text="1" Width="50px"
                                        onkeypress="return isNumber(event);" Style="text-align: center; padding: 8px"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="none" ID="rfvFirstName" runat="server" ErrorMessage="Enter Quantity"
                                        ValidationGroup="AddtoCart" Text="*" ControlToValidate="txtQty" SetFocusOnError="true"
                                        CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                    <asp:Button ID="btnCart" runat="server" Text="Add To Cart" OnClick="btnCart_Click"
                                        ValidationGroup="AddtoCart" />
                                </div>
                                <div class="btnExpressChk">
                                    <asp:Button ID="btnExpress" runat="server" Text="Express CheckOut" OnClick="btnExpress_Click"
                                        ValidationGroup="AddtoCart" />
                                </div>
                                <div class="clear">
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="btn_form">
                            <div class="float-lt" style="color: #555; font-size: 14px">
                                Shipping Days :
                                <asp:Label ID="lblDeliveryDays" runat="server" Style="color: #555; font-weight: bold;"></asp:Label>
                            </div>
                            <asp:Panel ID="pnlCoupenCode" runat="server" DefaultButton="btnCheckCouponCode">
                                <div style="float: right;" class="CouponCode">
                                    <div class="float-lt">
                                        <asp:TextBox ID="txtCouponCode" runat="server" placeholder="Coupon code" ValidationGroup="checkCouponCode"
                                            Style="padding: 8px; text-align: center; margin-right: 5px;" CssClass="setTxtCss"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="none" ID="rfvCouponCode" runat="server" ErrorMessage="Enter Coupon Code"
                                            ValidationGroup="checkCouponCode" Text="*" ControlToValidate="txtCouponCode"
                                            SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                    </div>
                                    <div style="float: right;">
                                        <asp:Button ID="btnCheckCouponCode" runat="server" Text="VERIFY" CssClass="btn" OnClick="btnCheckCouponCode_Click"
                                            ValidationGroup="checkCouponCode" />
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="clear">
                            </div>
                        </div>
                        <asp:Label ID="lblCouponCode" Style="font-size: 12px; left: 46.5%;" runat="server"
                            ViewStateMode="Disabled"></asp:Label>
                        <%--   <asp:Panel ID="pnlCheckAvailability" runat="server" DefaultButton="btnCheckPincode">
                            <asp:UpdatePanel ID="upPincode" runat="server">
                                <ContentTemplate>
                                    <div class="CheckAvailability btn_form registration_form">
                                        <span class="title">Check Availability </span>
                                        <asp:TextBox ID="txtPincode" runat="server" placeholder="Pincode" Style="text-align: center;"
                                            CssClass="pincodetxtBox" Width="80px" onkeypress="return isNumber(event);" MaxLength="6"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="none" ID="RFVPincode" runat="server" ErrorMessage="Enter Pincode"
                                            ValidationGroup="CheckPincode" Text="*" ControlToValidate="txtPincode" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>&nbsp;
                                        <asp:RegularExpressionValidator Display="none" ID="revPincode" runat="server"
                                            ValidationGroup="CheckPincode" Text="*" ControlToValidate="txtPincode"
                                            SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>&nbsp;
                                        <asp:Button ID="btnCheckPincode" runat="server" Text="CHECK" CssClass="btn" OnClick="btnCheckPincode_Click"
                                            ValidationGroup="CheckPincode" />
                                        <asp:Label ID="lblAvailability" runat="server" Visible="false" Font-Bold="true"></asp:Label>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>--%>
                        <div class="clear">
                        </div>
                        <div id="DataDescription" runat="server" class="det_nav1">
                            <div id="tabs">
                                <ul class="menu1">
                                    <li id="news" class="active">Description</li>
                                    <%-- <li id="tutorials">Wash Care</li>--%>
                                </ul>
                                <span class="clear"></span>
                                <div class="content news" style="padding-left: 5px; margin-bottom: 10px;">
                                    <div id="divDescription" runat="server" style="text-align: justify; color: #555;
                                        margin-top: 5px; overflow: hidden;">
                                    </div>
                                </div>
                                <div class="content tutorials">
                                    <div id="DivWashCare" runat="server" style="text-align: justify; color: #555; margin-top: 5px;
                                        overflow: hidden;">
                                    </div>
                                </div>
                                <div class="c">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                    <div class="clients" id="divRelatedProduct" runat="server">
                        <h3 class="m_3">
                            Related Products</h3>
                        <ul id="flexiselDemo3">
                            <asp:Repeater ID="RepRelatedProduct" runat="server">
                                <ItemTemplate>
                                    <li><a href='<%# objPageBase.GetAlias("ProductDetail.aspx") + generateUrl(Eval("appProductName").ToString()) %>'>
                                        <div class="product_image_in_relatedProducts">
                                            <img src='<%=strServerURL %>admin/<%#Eval("appNormalImage") %>' alt='<%#Eval("appProductName") %>'
                                                class="setImgWidth" /></div>
                                    </a><a href='<%# objPageBase.GetAlias("ProductDetail.aspx") + generateUrl(Eval("appProductName").ToString()) %>'>
                                        <%#Eval("appProductName") %></a>
                                        <%--<p>
                                <%#Eval("appDescription")%></p>--%>
                                        <p class="setColor">
                                            <%= Session[appFunctions.Session.CurrencyImage.ToString()]%><%# Math.Round( Convert.ToDecimal(Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString()) * Convert.ToDecimal(Eval("appPrice").ToString()),2)%>
                                            <%--  <%#Eval("appPrice")%>--%>
                                        </p>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                        <%--<ul id="flexiselDemo3">
                            <li>
                                <img src="<%=strServerURL %>Images/blooming-cream-super-net-designer-saree-800x1100.jpg" /><a href="#">Category</a><p>
                                    Rs 600</p>
                            </li>
                            <li>
                                <img src="<%=strServerURL %>Images/blooming-cream-super-net-designer-saree-800x1100.jpg" /><a href="#">Category</a><p>
                                    Rs 850</p>
                            </li>
                            <li>
                                <img src="<%=strServerURL %>Images/blooming-cream-super-net-designer-saree-800x1100.jpg" /><a href="#">Category</a><p>
                                    Rs 900</p>
                            </li>
                            <li>
                                <img src="<%=strServerURL %>Images/blooming-cream-super-net-designer-saree-800x1100.jpg" /><a href="#">Category</a><p>
                                    Rs 550</p>
                            </li>
                            <li>
                                <img src="<%=strServerURL %>Images/blooming-cream-super-net-designer-saree-800x1100.jpg" /><a href="#">Category</a><p>
                                    Rs 750</p>
                            </li>
                        </ul>--%>
                        <script type="text/javascript">
                            $(window).load(function () {
                                $("#flexiselDemo1").flexisel();
                                $("#flexiselDemo2").flexisel({
                                    enableResponsiveBreakpoints: true,
                                    responsiveBreakpoints: {
                                        portrait: {
                                            changePoint: 480,
                                            visibleItems: 1
                                        },
                                        landscape: {
                                            changePoint: 640,
                                            visibleItems: 2
                                        },
                                        tablet: {
                                            changePoint: 768,
                                            visibleItems: 3
                                        }
                                    }
                                });

                                $("#flexiselDemo3").flexisel({
                                    visibleItems: 5,
                                    animationSpeed: 1000,
                                    autoPlay: true,
                                    autoPlaySpeed: 3000,
                                    pauseOnHover: true,
                                    enableResponsiveBreakpoints: true,
                                    responsiveBreakpoints: {
                                        portrait: {
                                            changePoint: 480,
                                            visibleItems: 1
                                        },
                                        landscape: {
                                            changePoint: 640,
                                            visibleItems: 2
                                        },
                                        tablet: {
                                            changePoint: 768,
                                            visibleItems: 3
                                        }
                                    }
                                });

                            });
                        </script>
                        <script type="text/javascript" src="<%=strServerURL %>js/jquery.flexisel.js"></script>
                    </div>
                    <%--<div class="toogle">
                        <h3 class="m_3">
                            Product Details</h3>
                        <p class="m_text">
                            This is sample text to get user idea how original text will look like. This is sample
                            text to get user idea how original text will look like. This is sample text to get
                            user idea how original text will look like. This is sample text to get user idea
                            how original text will look like. This is sample text to get user idea how original
                            text will look like.This is sample text to get user idea how original text will
                            look like.</p>
                    </div>--%>
                </div>
                <div class="rsingle span_1_of_single">
                    <div class="col6">
                        <div class="sky-form">
                            <h4 class="setTopMargin">
                                Product Detail</h4>
                        </div>
                        <div class="pad">
                            <div>
                                <asp:Repeater ID="RepProperty" runat="server">
                                    <ItemTemplate>
                                        <%-- <label class="checkbox" id="lblCount" runat="server" title='<%# Eval("appPreValue") %>' onclick='<%# "ChkSize("+Eval("appPropertyPreValueID")+")" %>'><asp:CheckBox ID="chkValue"  runat="server" oncheckedchanged="chkValue_CheckedChanged" AutoPostBack="true" /><i></i><%#Eval("appPreValue")%></label>--%>
                                        <div class="float-lt setLabelWidth">
                                            <%# Eval("appDisplayName")%>:
                                        </div>
                                        <div class="float-lt setLabelWidth2">
                                            <%#Eval("appValue")%>
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="divhr">
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                    <%--<h5 class="m_1">
                        Categories</h5>
                    <select class="dropdown" tabindex="8" data-settings='{"wrapperClass":"metro"}'>
                        <option value="1">Mens</option>
                        <option value="2">Sub Category1</option>
                        <option value="3">Sub Category2</option>
                        <option value="4">Sub Category3</option>
                    </select>
                    <select class="dropdown" tabindex="8" data-settings='{"wrapperClass":"metro"}'>
                        <option value="1">Womens</option>
                        <option value="2">Sub Category1</option>
                        <option value="3">Sub Category2</option>
                        <option value="4">Sub Category3</option>
                    </select>
                    <ul class="kids">
                        <li><a href="#">Kids</a></li>
                        <li class="last"><a href="#">Glasses Shop</a></li>
                    </ul>--%>
                    <br />
                    <div class="w_sidebar" id="divRecentProduct" runat="server">
                        <div class="sky-form">
                            <h4>
                                Recent viewed
                            </h4>
                            <div class="prod-desc">
                                <asp:Repeater ID="RepRecentProduct" runat="server">
                                    <ItemTemplate>
                                        <div class="recentProduct">
                                            <a href='<%# objPageBase.GetAlias("ProductDetail.aspx") + generateUrl(Eval("appProductName").ToString()) %>'>
                                                <img src='<%=strServerURL %>admin/<%#Eval("appNormalImage")%>' align="middle" alt='<%#Eval("appProductName") %>' />
                                            </a>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <div class="clearfix">
                                </div>
                            </div>
                        </div>
                    </div>
                    <script src="<%=strServerURL %>js/jquery.easydropdown.js"></script>
                </div>
                <div class="clear">
                </div>
                <div>
                    <div style="width: 100%;">
                        <div class="col6">
                            <div class="sky-form">
                                <h4 class="setTopMargin">
                                    Reviews</h4>
                            </div>
                        </div>
                    </div>
                    <div style="padding: 5px;">
                        <DInfo:DisplayInfo runat="server" ID="DInfoReview" />
                        <asp:LinkButton ID="btnClickHere" runat="server" class="terms" OnClick="btnClickHere_Click">Click Here To Write a review</asp:LinkButton>
                        <div id="divReviewForm" class="registration_form" runat="server">
                            <div>
                                <span>
                                    <label>
                                        <b>Write Your Review</b></label></span> <span style="background-color: #ECECEC;">
                                            <cc1:Rating ID="RatingReview" runat="server" StarCssClass="Star" WaitingStarCssClass="WaitingStar"
                                                EmptyStarCssClass="Star" FilledStarCssClass="FilledStar">
                                            </cc1:Rating>
                                        </span>
                            </div>
                            <br />
                            <div>
                                <div>
                                    <span>
                                        <label>
                                            Title</label></span>
                                    <br />
                                    <span>
                                        <asp:TextBox runat="server" ID="txtTitle" placeholder="Enter Title"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RFVTitle" Text="*" runat="server" ErrorMessage="Please enter Title"
                                            ControlToValidate="txtTitle" ValidationGroup="Review" Display="none"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                                <div>
                                    <span>
                                        <label>
                                            Comment
                                        </label>
                                    </span>
                                    <br />
                                    <span>
                                        <asp:TextBox runat="server" ID="txtReview" placeholder="Enter Comment" Rows="5" TextMode="MultiLine"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RFVReview" Text="*" runat="server" ErrorMessage="Please enter Comment"
                                            ControlToValidate="txtReview" ValidationGroup="Review" Display="none"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                                <br />
                                <div>
                                    <span>
                                        <asp:LinkButton ID="btnaddReview" runat="server" CssClass="btn" ValidationGroup="Review"
                                            OnClick="btnaddReview_Click"> 
									        Submit Review</asp:LinkButton>
                                    </span>
                                </div>
                                <br />
                                <asp:ValidationSummary ID="vsReview" ShowMessageBox="true" EnableClientScript="true"
                                    HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                                    ValidationGroup="Review" />
                                <asp:HiddenField ID="hdnRating" runat="server" Value="" />
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <asp:Repeater runat="server" ID="RptRating">
                                <ItemTemplate>
                                    <div class="divReview">
                                        <div style="font-weight: bold;">
                                            <%#Eval("appTitle")%>
                                        </div>
                                        <div>
                                            <div style="font-size: 14px;">
                                                <%#Eval("appFullName")%>&nbsp;&nbsp;&nbsp;<%#Eval("appReviewDate")%><div>
                                                    <cc1:Rating ID="RatingReview1" AutoPostBack="true" runat="server" StarCssClass="Star"
                                                        CurrentRating='<%#Eval("appRating")%>' ReadOnly="true" WaitingStarCssClass="WaitingStar"
                                                        EmptyStarCssClass="Star" FilledStarCssClass="FilledStar">
                                                    </cc1:Rating>
                                                </div>
                                                <br />
                                            </div>
                                            <div>
                                                <p>
                                                    <%#Eval("appComment")%></p>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                </ItemTemplate>
                            </asp:Repeater>
                            <br />
                            <div>
                            </div>
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
    <div>
        <MyCart:Cart ID="ViewCart" runat="server" />
        <MyLogin:Login ID="CustLogin" runat="server" />
        <MyInquiry:Inquiry ID="UserInquiry" runat="server" />
        <asp:HiddenField ID="hdnPKID" runat="server" Value="" />
        <asp:HiddenField ID="hdnProductDetailId" runat="server" Value="" />
        <asp:HiddenField ID="hdnProductColorId" runat="server" Value="" />
        <asp:HiddenField ID="hdncolorName" runat="server" Value="" />
        <asp:HiddenField ID="hdnIsColor" runat="server" Value="false" />
        <asp:HiddenField ID="hdnIsSize" runat="server" Value="false" />
        <asp:HiddenField ID="hdnColorId" runat="server" Value="" />
        <asp:HiddenField ID="hdnProductName" runat="server" Value="" />
        <asp:HiddenField ID="hdnImg" runat="server" Value="" />
        <asp:HiddenField runat="server" ID="hdnPriceDiscount" Value="0" />
        <asp:ValidationSummary ID="vsCheckCouponCode" ShowMessageBox="true" EnableClientScript="true"
            HeaderText="You must Enter Following
                Fields" ShowSummary="false" runat="server" ValidationGroup="checkCouponCode" />
        <asp:ValidationSummary ID="vsCheckPinCode" ShowMessageBox="true" EnableClientScript="true"
            HeaderText="You must Enter Following
                Fields" ShowSummary="false" runat="server" ValidationGroup="CheckPincode" />
        <asp:ValidationSummary ID="vsAddtoCart" ShowMessageBox="true" EnableClientScript="true"
            HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
            ValidationGroup="AddtoCart" />
        <%-- <asp:HiddenField runat="server" Value="" ID="hdnProductName"
    />--%>
    </div>
    <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
