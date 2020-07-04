<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Src="~/UserControls/loginRegistration.ascx" TagName="Registration" TagPrefix="MyloginRegistration" %>
<%@ Register Src="~/UserControls/ViewCart.ascx" TagName="Cart" TagPrefix="MyCart" %>
<%@ Register Src="~/UserControls/Login.ascx" TagName="Login" TagPrefix="MyLogin" %>
<%@ Register Src="~/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta name="description" content="<%=metaDescription %>" />
    <meta name="keywords" content="<%=metaKeywords %>" />
    <script type="text/javascript">
        $(document).ready(function () {
        });

        function validateNewsLetterSubscription() {


            Page_ClientValidate('register');
            if (Page_IsValid('register') == true) {
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
    <!-- start slider -->
    <div class="wrap" style="margin-top: 7Px;">
        <div id="fwslider">
            <div class="slider_container">
                <asp:Repeater runat="server" ID="rpBanner">
                    <ItemTemplate>
                        <div class="slide">
                            <a href="<%# Eval("appUrl").ToString().ToLower()=="" ? "#" : Eval("appUrl").ToString() %>">
                                <img src='<%=strServerURL %>admin/<%#Eval("appImage") %>' alt=' <%#Eval("appTitle")%>' />
                                <div class="slide_content">
                                    <div class="slide_content_wrap">
                                        <h4 class="title" style='display: <%# Eval("appTitle").ToString().ToLower() == "" ? "none" : "block" %>'>
                                            <%#Eval("appTitle")%>
                                        </h4>
                                        <p class="description" style='display: <%# Eval("appDescription").ToString().ToLower() == "" ? "none" : "block" %>'>
                                            <%#Eval("appDescription")%>
                                        </p>
                                    </div>
                                </div>
                            </a>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <!-- /Duplicate to create more slides -->
                <%-- <div class="slide">
                    <img src="images/6.jpg" alt="" />
                    <div class="slide_content">
                        <div class="slide_content_wrap">
                            <h4 class="title">
                                Gourgious Dress
                            </h4>
                            <p class="description">
                                Exclusive offer 40% off</p>
                        </div>
                    </div>
                </div>
                <div class="slide">
                    <img src="images/5.jpg" alt="" />
                    <div class="slide_content">
                        <div class="slide_content_wrap">
                            <h4 class="title">
                                Bridal Dress
                            </h4>
                            <p class="description">
                                Exclusive offer 20% off</p>
                        </div>
                    </div>
                </div>--%>
                <!--/slide -->
            </div>
            <div class="timers">
            </div>
            <div class="slidePrev">
                <span></span>
            </div>
            <div class="slideNext">
                <span></span>
            </div>
        </div>
    </div>
    <!--/slider -->
    <div class="main">
        <div class="wrap">
            <asp:Literal ID="ltHighLight" runat="server"></asp:Literal>
            <%--  <div class="banner-bottom-grids">

                 <div class="col-md-4 first bottom-grid">
                    <img src="images/111.jpg" alt="" />
                    <div class="bottom-grid-info">
                        <a href="#">Designer Saree</a>
                    </div>
                </div>
                <div class="col-md-4 bottom-grid">
                    <img src="images/222.jpg" alt="" />
                    <div class="bottom-grid-info">
                        <a href="#">Latest Design</a>
                    </div>
                </div>
                <div class="col-md-4 first bottom-grid">
                    <img src="images/333.jpg" alt="" />
                    <div class="bottom-grid-info">
                        <a href="#">Bridal Lahenga </a>
                    </div>
                </div>
                <div class="clear">
                </div>
            </div>--%>
            <DInfo:DisplayInfo runat="server" ID="DInfo" />
            <div class="section group">
                <div class="cont span_2_of_3_Default">
                    <div class="pageheadermain" id="divFeatured" runat="server">
                        <h2 class="head">
                            Featured Products</h2>
                        <div class="btnviewall">
                            <%-- <a href="#">View All</a>--%>
                            <a href='<%= objPageBase.GetAlias("SearchViewAll.aspx") + generateUrl("Featured Products").ToString() %>'>
                                View All</a>
                        </div>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="top-box">
                        <asp:Repeater runat="server" ID="rpFeaturedProduct" OnItemCommand="rpFeaturedProduct_ItemCommand">
                            <ItemTemplate>
                                <div class="col_1_of_3 span_1_of_3">
                                    <a href='<%# objPageBase.GetAlias("ProductDetail.aspx") + generateUrl(Eval("appProductName").ToString()) %>'>
                                        <div class="inner_content clearfix">
                                            <span class='discount' style='display: <%# Eval("appoff").ToString()== "0" ? "none" : "block" %>'>
                                                <%#Eval("appoff") %>%&nbsp;<span>Off</span></span>
                                            <div class="product_image">
                                                <img src='<%=strServerURL %>admin/<%#Eval("appNormalImage") %>' alt="" />
                                            </div>
                                            <div class="sale-box">
                                                <span class="on_sale title_shop">New</span></div>
                                            <div class="price">
                                                <p class="title">
                                                    <%#Eval("appProductName")%></p>
                                                <div class="cart-left">
                                                    <div class="price1">
                                                        <span class="actual">
                                                            <%-- <%#Eval("appPrice")%>--%>
                                                            <%= Session[appFunctions.Session.CurrencyImage.ToString()]%><%# Math.Round( Convert.ToDecimal(Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString()) * Convert.ToDecimal(Eval("appPrice").ToString()),0)%>
                                                        </span><span class='priceMiddle' style='display: <%# Eval("appoff").ToString()== "0" ? "none" : "block" %>'>
                                                            <strike><span>
                                                                <%= Session[appFunctions.Session.CurrencyImage.ToString()]%><%# Math.Round(Convert.ToDecimal(Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString()) * Convert.ToDecimal(Eval("appMRP").ToString()), 0)%>
                                                            </span></strike></span>
                                                    </div>
                                                </div>
                                                <asp:LinkButton runat="server" ID="lnkAddToCart" CssClass="cart-right" CommandName="Add To Cart"
                                                    CommandArgument='<%#Eval("appProductDetailID") %>'>Add To Cart</asp:LinkButton>
                                                <%-- <div class="cart-right">
                                                </div>--%>
                                                <div class="clear">
                                                </div>
                                            </div>
                                        </div>
                                    </a>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <%-- <div class="col_1_of_3 span_1_of_3">
                            <a href="#">
                                <div class="inner_content clearfix">
                                    <div class="product_image">
                                        <img src="images/brown-and-grey-shaded-georgette-casual-saree-800x1100.jpg" alt="" />
                                    </div>
                                    <div class="price">
                                        <div class="cart-left">
                                            <p class="title">
                                                Designer Saree with work</p>
                                            <div class="price1">
                                                <span class="actual">Rs. 1200</span>
                                            </div>
                                        </div>
                                        <div class="cart-right">
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div class="col_1_of_3 span_1_of_3">
                            <a href="#">
                                <div class="inner_content clearfix">
                                    <div class="product_image">
                                        <img src="images/captivating-cream-and-pink-applique-lehenga-saree-800x1100.jpg"
                                            alt="" />
                                    </div>
                                    <div class="sale-box1">
                                        <span class="on_sale title_shop">Sale</span></div>
                                    <div class="price">
                                        <div class="cart-left">
                                            <p class="title">
                                                Designer Saree with work</p>
                                            <div class="price1">
                                                <span class="actual">Rs. 1200</span>
                                            </div>
                                        </div>
                                        <div class="cart-right">
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div class="col_1_of_3 span_1_of_3">
                            <a href="#">
                                <div class="inner_content clearfix">
                                    <div class="product_image">
                                        <img src="images/karishma-kapoor-red-zari-work-churidar-salwar-suit-800x1100.jpg"
                                            alt="" />
                                    </div>
                                    <div class="price">
                                        <div class="cart-left">
                                            <p class="title">
                                                Designer Saree with work</p>
                                            <div class="price1">
                                                <span class="actual">Rs. 1200</span>
                                            </div>
                                        </div>
                                        <div class="cart-right">
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </div>--%>
                        <div class="clear">
                        </div>
                    </div>
                    <%--<div class="top-box">
                        <div class="col_1_of_3 span_1_of_3">
                            <a href="#">
                                <div class="inner_content clearfix">
                                    <div class="product_image">
                                        <img src="images/karishma-kapoor-red-zari-work-churidar-salwar-suit-800x1100.jpg"
                                            alt="" />
                                    </div>
                                    <div class="price">
                                        <div class="cart-left">
                                            <p class="title">
                                                Designer Saree with work</p>
                                            <div class="price1">
                                                <span class="actual">Rs. 1200</span>
                                            </div>
                                        </div>
                                        <div class="cart-right">
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div class="col_1_of_3 span_1_of_3">
                            <a href="#">
                                <div class="inner_content clearfix">
                                    <div class="product_image">
                                        <img src="images/outstanding-red-and-blue-faux-georgette-lehenga-saree-800x1100.jpg"
                                            alt="" />
                                    </div>
                                    <div class="sale-box">
                                        <span class="on_sale title_shop">New</span></div>
                                    <div class="price">
                                        <div class="cart-left">
                                            <p class="title">
                                                Designer Saree with work</p>
                                            <div class="price1">
                                                <span class="actual">Rs. 1200</span>
                                            </div>
                                        </div>
                                        <div class="cart-right">
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div class="col_1_of_3 span_1_of_3">
                            <a href="#">
                                <div class="inner_content clearfix">
                                    <div class="product_image">
                                        <img src="images/whiite-and-red-net-lehenga-saree-800x1100.jpg" alt="" />
                                    </div>
                                    <div class="price">
                                        <div class="cart-left">
                                            <p class="title">
                                                Designer Saree with work</p>
                                            <div class="price1">
                                                <span class="actual">Rs. 1200</span>
                                            </div>
                                        </div>
                                        <div class="cart-right">
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div class="col_1_of_3 span_1_of_3">
                            <a href="#">
                                <div class="inner_content clearfix">
                                    <div class="product_image">
                                        <img src="images/captivating-cream-and-pink-applique-lehenga-saree-800x1100.jpg"
                                            alt="" />
                                    </div>
                                    <div class="sale-box1">
                                        <span class="on_sale title_shop">Sale</span></div>
                                    <div class="price">
                                        <div class="cart-left">
                                            <p class="title">
                                                Designer Saree with work</p>
                                            <div class="price1">
                                                <span class="actual">Rs. 1200</span>
                                            </div>
                                        </div>
                                        <div class="cart-right">
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div class="clear">
                        </div>
                    </div>--%>
                    <div class="hrsep">
                    </div>
                    <div class="pageheadermain" id="divBestSeller" runat="server">
                        <h2 class="head">
                            Best Seller</h2>
                        <div class="btnviewall">
                            <%-- <a href="#">View All</a>--%>
                            <a href='<%= objPageBase.GetAlias("SearchViewAll.aspx") + generateUrl("Best Seller").ToString() %>'>
                                View All</a>
                        </div>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="top-box1">
                        <asp:Repeater runat="server" ID="rpBestSeller" OnItemCommand="rpBestSeller_ItemCommand">
                            <ItemTemplate>
                                <div class="col_1_of_3 span_1_of_3">
                                    <a href='<%# objPageBase.GetAlias("ProductDetail.aspx") + generateUrl(Eval("appProductName").ToString()) %>'>
                                        <div class="inner_content clearfix">
                                            <div class="product_image">
                                                <span class='discount' style='display: <%# Eval("appoff").ToString()== "0" ? "none" : "block" %>'>
                                                    <%#Eval("appoff") %>%&nbsp;<span>Off</span></span>
                                                <img src='<%=strServerURL %>admin/<%#Eval("appNormalImage") %>' alt="" />
                                            </div>
                                            <div class="price">
                                                <p class="title">
                                                    <%#Eval("appProductName")%></p>
                                                <div class="cart-left">
                                                    <div class="price1">
                                                        <span class="actual">
                                                            <%--  <%#Eval("appPrice")%>--%>
                                                            <%= Session[appFunctions.Session.CurrencyImage.ToString()]%><%# Math.Round( Convert.ToDecimal(Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString()) * Convert.ToDecimal(Eval("appPrice").ToString()),0)%>
                                                        </span><span class='priceMiddle' style='display: <%# Eval("appoff").ToString()== "0" ? "none" : "block" %>'>
                                                            <strike><span>
                                                                <%= Session[appFunctions.Session.CurrencyImage.ToString()]%><%# Math.Round(Convert.ToDecimal(Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString()) * Convert.ToDecimal(Eval("appMRP").ToString()), 0)%>
                                                            </span></strike></span>
                                                    </div>
                                                </div>
                                                <asp:LinkButton runat="server" ID="lnkBestAddToCart" CssClass="cart-right" CommandName="Add To Cart"
                                                    CommandArgument='<%#Eval("appProductDetailID") %>'>Add To Cart</asp:LinkButton>
                                                <%--<div class="cart-right">
                                                </div>--%>
                                                <div class="clear">
                                                </div>
                                            </div>
                                        </div>
                                    </a>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <%--<div class="col_1_of_3 span_1_of_3">
                            <a href="#">
                                <div class="inner_content clearfix">
                                    <div class="product_image">
                                        <img src="images/blooming-cream-super-net-designer-saree-800x1100.jpg" alt="" />
                                    </div>
                                    <div class="sale-box">
                                        <span class="on_sale title_shop">New</span></div>
                                    <div class="price">
                                        <div class="cart-left">
                                            <p class="title">
                                                Designer Saree with work</p>
                                            <div class="price1">
                                                <span class="actual">Rs. 1200</span>
                                            </div>
                                        </div>
                                        <div class="cart-right">
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </div>
                       <div class="col_1_of_3 span_1_of_3">
                            <a href="#">
                                <div class="inner_content clearfix">
                                    <div class="product_image">
                                        <img src="images/brown-and-grey-shaded-georgette-casual-saree-800x1100.jpg" alt="" />
                                    </div>
                                    <div class="price">
                                        <div class="cart-left">
                                            <p class="title">
                                                Designer Saree with work</p>
                                            <div class="price1">
                                                <span class="actual">Rs. 1200</span>
                                            </div>
                                        </div>
                                        <div class="cart-right">
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div class="col_1_of_3 span_1_of_3">
                            <a href="#">
                                <div class="inner_content clearfix">
                                    <div class="product_image">
                                        <img src="images/captivating-cream-and-pink-applique-lehenga-saree-800x1100.jpg"
                                            alt="" />
                                    </div>
                                    <div class="sale-box1">
                                        <span class="on_sale title_shop">Sale</span></div>
                                    <div class="price">
                                        <div class="cart-left">
                                            <p class="title">
                                                Designer Saree with work</p>
                                            <div class="price1">
                                                <span class="actual">Rs. 1200</span>
                                            </div>
                                        </div>
                                        <div class="cart-right">
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div class="col_1_of_3 span_1_of_3">
                            <a href="#">
                                <div class="inner_content clearfix">
                                    <div class="product_image">
                                        <img src="images/karishma-kapoor-red-zari-work-churidar-salwar-suit-800x1100.jpg"
                                            alt="" />
                                    </div>
                                    <div class="price">
                                        <div class="cart-left">
                                            <p class="title">
                                                Designer Saree with work</p>
                                            <div class="price1">
                                                <span class="actual">Rs. 1200</span>
                                            </div>
                                        </div>
                                        <div class="cart-right">
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </div>--%>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="hrsep">
                    </div>
                    <div class="pageheadermain" id="divNewProducts" runat="server">
                        <h2 class="head">
                            New Products</h2>
                        <div class="btnviewall">
                            <%--<a href="#">View All</a>--%>
                            <a href='<%= objPageBase.GetAlias("SearchViewAll.aspx") + generateUrl("New Products").ToString() %>'>
                                View All</a>
                        </div>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="section group">
                        <asp:Repeater runat="server" ID="rpNewProduct" OnItemCommand="rpNewProduct_ItemCommand">
                            <ItemTemplate>
                                <div class="col_1_of_3 span_1_of_3">
                                    <a href='<%# objPageBase.GetAlias("ProductDetail.aspx") + generateUrl(Eval("appProductName").ToString()) %>'>
                                        <div class="inner_content clearfix">
                                            <div class="product_image">
                                                <span class='discount' style='display: <%# Eval("appoff").ToString()== "0" ? "none" : "block" %>'>
                                                    <%#Eval("appoff") %>%&nbsp;<span>Off</span></span>
                                                <img src='<%=strServerURL %>admin/<%#Eval("appNormalImage") %>' alt="" />
                                            </div>
                                            <div class="price">
                                                <p class="title">
                                                    <%#Eval("appProductName")%></p>
                                                <div class="cart-left">
                                                    <div class="price1">
                                                        <span class="actual">
                                                            <%-- <%#Eval("appPrice")%>--%>
                                                            <%= Session[appFunctions.Session.CurrencyImage.ToString()]%><%# Math.Round( Convert.ToDecimal(Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString()) * Convert.ToDecimal(Eval("appPrice").ToString()),0)%>
                                                        </span><span class='priceMiddle' style='display: <%# Eval("appoff").ToString()== "0" ? "none" : "block" %>'>
                                                            <strike><span>
                                                                <%= Session[appFunctions.Session.CurrencyImage.ToString()]%><%# Math.Round(Convert.ToDecimal(Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString()) * Convert.ToDecimal(Eval("appMRP").ToString()), 0)%>
                                                            </span></strike></span>
                                                    </div>
                                                </div>
                                                <asp:LinkButton runat="server" ID="lnkBestAddToCart" CssClass="cart-right" CommandName="Add To Cart"
                                                    CommandArgument='<%#Eval("appProductDetailID") %>'>Add To Cart</asp:LinkButton>
                                                <%-- <div class="cart-right">
                                                </div>--%>
                                                <div class="clear">
                                                </div>
                                            </div>
                                        </div>
                                    </a>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <%--  <div class="col_1_of_3 span_1_of_3">
                            <a href="#">
                                <div class="inner_content clearfix">
                                    <div class="product_image">
                                        <img src="images/karishma-kapoor-red-zari-work-churidar-salwar-suit-800x1100.jpg"
                                            alt="" />
                                    </div>
                                    <div class="price">
                                        <div class="cart-left">
                                            <p class="title">
                                                Designer Saree with work</p>
                                            <div class="price1">
                                                <span class="actual">Rs. 1200</span>
                                            </div>
                                        </div>
                                        <div class="cart-right">
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div class="col_1_of_3 span_1_of_3">
                            <a href="#">
                                <div class="inner_content clearfix">
                                    <div class="product_image">
                                        <img src="images/outstanding-red-and-blue-faux-georgette-lehenga-saree-800x1100.jpg"
                                            alt="" />
                                    </div>
                                    <div class="sale-box">
                                        <span class="on_sale title_shop">New</span></div>
                                    <div class="price">
                                        <div class="cart-left">
                                            <p class="title">
                                                Designer Saree with work</p>
                                            <div class="price1">
                                                <span class="actual">Rs. 1200</span>
                                            </div>
                                        </div>
                                        <div class="cart-right">
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div class="col_1_of_3 span_1_of_3">
                            <a href="#">
                                <div class="inner_content clearfix">
                                    <div class="product_image">
                                        <img src="images/captivating-cream-and-pink-applique-lehenga-saree-800x1100.jpg"
                                            alt="" />
                                    </div>
                                    <div class="sale-box1">
                                        <span class="on_sale title_shop">Sale</span></div>
                                    <div class="price">
                                        <div class="cart-left">
                                            <p class="title">
                                                Designer Saree with work</p>
                                            <div class="price1">
                                                <span class="actual">Rs. 1200</span>
                                            </div>
                                        </div>
                                        <div class="cart-right">
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div class="col_1_of_3 span_1_of_3">
                            <a href="#">
                                <div class="inner_content clearfix">
                                    <div class="product_image">
                                        <img src="images/karishma-kapoor-red-zari-work-churidar-salwar-suit-800x1100.jpg"
                                            alt="" />
                                    </div>
                                    <div class="price">
                                        <div class="cart-left">
                                            <p class="title">
                                                Designer Saree with work</p>
                                            <div class="price1">
                                                <span class="actual">Rs. 1200</span>
                                            </div>
                                        </div>
                                        <div class="cart-right">
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </div>--%>
                        <div class="clear">
                        </div>
                    </div>
                </div>
                <div class="rsidebar1 span_1_of_left">
                    <div class="top-border">
                    </div>
                    <div class="border" id="dicBestDeal" runat="server">
                        <link href="css/default.css" rel="stylesheet" type="text/css" media="all" />
                        <link href="css/nivo-slider.css" rel="stylesheet" type="text/css" media="all" />
                        <script type="text/javascript" src="js/jquery.nivo.slider.js"></script>
                        <script type="text/javascript">
                            $(window).load(function () {
                                $('#slider').nivoSlider();
                            });
                        </script>
                        <div id="divDeal" runat="server" class="slider-wrapper theme-default">
                            <div style="padding-bottom: 15Px; color: #555;">
                                Best Offer</div>
                            <div>
                                <div id="slider" class="nivoSlider">
                                    <asp:Repeater runat="server" ID="rptBestDeal" OnItemCommand="rptBestDeal_ItemCommand">
                                        <ItemTemplate>
                                            <a href='<%# objPageBase.GetAlias("ProductDetail.aspx") + generateUrl(Eval("appProductName").ToString()) %>'>
                                                <%--   <span class='discount' style='display: <%# Eval("appoff").ToString()== "0" ? "none" : "block" %>'><%#Eval("appoff") %>%&nbsp;<span>Off</span></span>--%>
                                                <img src='<%=strServerURL %>admin/<%#Eval("appNormalImage") %>' alt='<%#Eval("appDescription")%>'
                                                    title='<%#Eval("appDescription")%>' />
                                            </a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                            <div>
                                <%--<asp:LinkButton runat="server" ID="lnkBestAddToCart" class="btnBestOffer" CommandName="Add To Cart"
                                    CommandArgument='<%#Eval("appProductDetailID") %>'>Add To Cart</asp:LinkButton>--%>
                            </div>
                        </div>
                    </div>
                    <div class="top-border">
                    </div>
                    <asp:Panel ID="pnlNewsLetterSubscription" runat="server" DefaultButton="brnSubScribe">
                        <div class="sidebar-bottom">
                            <h2 class="m_1">
                                Newsletters<br>
                                Signup</h2>
                            <p class="m_text">
                                Signup for latest product updates</p>
                            <div class="subscribe">
                                <%-- <form>--%>
                                <DInfo:DisplayInfo runat="server" ID="DisplayInfoSubScribe" />
                                <asp:TextBox runat="server" ID="txtUserEmail" CssClass="textbox" placeholder="Email:"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="Dynamic" ID="RFVEmailRegistration" runat="server"
                                    ErrorMessage="Enter Email" ValidationGroup="register" Text="*" ControlToValidate="txtUserEmail"
                                    SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revEmailRegistration" runat="server" Display="Dynamic"
                                    ValidationGroup="register" Text="*" ControlToValidate="txtUserEmail" SetFocusOnError="true"
                                    CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                <asp:Button runat="server" ID="brnSubScribe" Text="Subscribe" ValidationGroup="register"
                                    OnClientClick="return validateNewsLetterSubscription()" OnClick="btnSubScribe_Click" />
                                <asp:ValidationSummary ID="vsRegistration" ShowMessageBox="true" EnableClientScript="true"
                                    HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                                    ValidationGroup="register" />
                                <%--      <input type="submit" value="Subscribe">--%>
                                <%-- </form>--%>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="hrsep span_2_of_3_Default">
            </div>
            <div class="pageheadermain span_2_of_3_Default">
                <h2 class="head">
                    Trending</h2>
                <div class="btnviewall">
                    <!--<input type="button" value="View All" />-->
                    <%--<a href="#">View All</a>--%>
                    <a href='<%= objPageBase.GetAlias("SearchViewAll.aspx") + generateUrl("Trending").ToString() %>'>
                        View All</a>
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="boxmain span_2_of_3_Default">
                <asp:Repeater ID="rpTranding" runat="server">
                    <ItemTemplate>
                        <a href='<%# objPageBase.GetAlias("ProductDetail.aspx") + generateUrl(Eval("appProductName").ToString()) %>'>
                            <div class="smallbox first smallgrid">
                                <%-- <span class='trendingdiscount' style='display: <%# Eval("appoff").ToString()== "0" ? "none" : "block" %>'>
                                <%#Eval("appoff") %>%&nbsp;<span>Off</span></span>--%>
                                <img src='<%=strServerURL %>admin/<%#Eval("appNormalImage") %>' alt="" />
                                <div class="name">
                                    <div class="nameTrending">
                                        <p class="title">
                                            <%#Eval("appProductName")%>
                                        </p>
                                    </div>
                                    <div class="rate">
                                        <%= Session[appFunctions.Session.CurrencyImage.ToString()]%><%# Math.Round(Convert.ToDecimal(Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString()) * Convert.ToDecimal(Eval("appPrice").ToString()), 0)%></div>
                                </div>
                                <div class="clear">
                                </div>
                                <%-- <div class="nameTrending">
                               
                            </div>
                            <div class="rateTrending">
                              </div>
                            <div class="clear">
                            </div>--%>
                                <!--<div class="boxhover">
                        
                    </div>-->
                            </div>
                        </a>
                    </ItemTemplate>
                </asp:Repeater>
                <div class="clear">
                </div>
            </div>
            <%-- <div class="smallbox first smallgrid">
                    <img src="<%#strServerURL %>images/karishma-kapoor-red-zari-work-churidar-salwar-suit-800x1100.jpg"
                        alt="" />
                    <div class="name">
                        Designer Dress
                        <div class="rate">
                            Rs. 5874</div>
                    </div>
                    <div class="clear">
                    </div>
                    <!--<div class="boxhover">
                        
                    </div>-->
                </div>--%>
            <%-- <div class="smallbox smallgrid">
                    <img src="<%#strServerURL %>images/karishma-kapoor-red-zari-work-churidar-salwar-suit-800x1100.jpg"
                        alt="" />
                    <div class="name">
                        Designer Dress
                        <div class="rate">
                            Rs. 5874</div>
                    </div>
                    <div class="clear">
                    </div>
                    <!--<div class="boxhover">
                        
                    </div>-->
                </div>--%>
            <%--  <div class="smallbox smallgrid">
                    <img src="images/karishma-kapoor-red-zari-work-churidar-salwar-suit-800x1100.jpg"
                        alt="" />
                    <div class="name">
                        Designer Dress
                        <div class="rate">
                            Rs. 5874</div>
                    </div>
                    <div class="clear">
                    </div>
                    <!--<div class="boxhover">
                        
                    </div>-->
                </div>
                <div class="smallbox first smallgrid">
                    <img src="images/karishma-kapoor-red-zari-work-churidar-salwar-suit-800x1100.jpg"
                        alt="" />
                    <div class="name">
                        Designer Dress
                        <div class="rate">
                            Rs. 5874</div>
                    </div>
                    <div class="clear">
                    </div>
                    <!--<div class="boxhover">
                        
                    </div>-->
                </div>
                <div class="smallbox first smallgrid">
                    <img src="images/karishma-kapoor-red-zari-work-churidar-salwar-suit-800x1100.jpg"
                        alt="" />
                    <div class="name">
                        Designer Dress
                        <div class="rate">
                            Rs. 5874</div>
                    </div>
                    <div class="clear">
                    </div>
                    <!--<div class="boxhover">
                        
                    </div>-->
                </div>
                <div class="smallbox smallgrid">
                    <img src="images/karishma-kapoor-red-zari-work-churidar-salwar-suit-800x1100.jpg"
                        alt="" />
                    <div class="name">
                        Designer Dress
                        <div class="rate">
                            Rs. 5874</div>
                    </div>
                    <div class="clear">
                    </div>
                    <!--<div class="boxhover">
                        
                    </div>-->
                </div>
                <div class="smallbox smallgrid">
                    <img src="images/karishma-kapoor-red-zari-work-churidar-salwar-suit-800x1100.jpg"
                        alt="" />
                    <div class="name">
                        Designer Dress
                        <div class="rate">
                            Rs. 5874</div>
                    </div>
                    <div class="clear">
                    </div>
                    <!--<div class="boxhover">
                        
                    </div>-->
                </div>
                <div class="smallbox first smallgrid">
                    <img src="images/karishma-kapoor-red-zari-work-churidar-salwar-suit-800x1100.jpg"
                        alt="" />
                    <div class="name">
                        Designer Dress
                        <div class="rate">
                            Rs. 5874</div>
                    </div>
                    <div class="clear">
                    </div>
                    <!--<div class="boxhover">
                        
                    </div>-->
                </div>--%>
            <div class="clear">
            </div>
        </div>
    </div>
    <br />
    <!--<div class="hrsep" style="margin-bottom:10Px;"> </div>-->
    <div>
        <MyloginRegistration:Registration ID="ViewRegistration" runat="server" />
        <MyCart:Cart ID="ViewCart" runat="server" />
        <MyLogin:Login ID="CustLogin" runat="server" />
    </div>
</asp:Content>
