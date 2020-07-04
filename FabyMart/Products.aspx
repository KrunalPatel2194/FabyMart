<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="Products.aspx.cs" Inherits="Products" %>
    
<%@ Register Src="~/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<%@ Register Src="~/UserControls/ViewCart.ascx" TagName="Cart" TagPrefix="MyCart" %>
<%@ Register Src="~/UserControls/Login.ascx" TagName="Login" TagPrefix="MyLogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta name="description" content="<%=metaDescription %>" />
    <meta name="keywords" content="<%=metaKeywords %>" />
    <!-- bin/jquery.slider.min.css -->
    <link rel="stylesheet" href="<%=strServerURL %>css/jslider.css" type="text/css">
    <link rel="stylesheet" href="<%=strServerURL %>css/jslider.plastic.css" type="text/css">
    <!-- end -->
    <!-- bin/jquery.slider.min.js -->
    <script type="text/javascript" src="<%=strServerURL %>js/jshashtable-2.1_src.js"></script>
    <script type="text/javascript" src="<%=strServerURL %>js/jquery.numberformatter-1.2.3.js"></script>
    <script type="text/javascript" src="<%=strServerURL %>js/tmpl.js"></script>
    <script type="text/javascript" src="<%=strServerURL %>js/jquery.dependClass-0.1.js"></script>
    <script type="text/javascript" src="<%=strServerURL %>js/draggable-0.1.js"></script>
    <script type="text/javascript" src="<%=strServerURL %>js/jquery.slider.js"></script>
    <%--<script src="<%=strServerURL %>JS/jquery-1.11.1.min.js" type="text/javascript"></script>--%>
    <script type="text/javascript">
        var pageIndex = 1;
        var pageCount;
        $(function () {
            Onload();
            $("#aPriceHigh").css("font-weight", "normal");
            $("#aSizeClear").click(function (event) {
                document.getElementById("<%=hdnSizeIds.ClientID%>").value = "";
                ResetAll();
                $('.size').each(function () {
                    this.checked = false;
                });
            });
            $("#AClorClear").click(function (event) {
                document.getElementById("<%=hdnColorIds.ClientID%>").value = "";
                ResetAll();
            });
            $("#<%=ddlSortByPrice.ClientID%>").change(function () {
                if ($(this).val() == "3") {
                    //                    $(this).css("font-weight", "bold");
                    $("#aPriceHigh").css("font-weight", "normal");
                    document.getElementById("<%=hdnOrderBy.ClientID%>").value = " AppPrice Asc";
                    ResetAll();
                } else if ($(this).val() == "4") {
                    //                    $(this).css("font-weight", "bold");
                    $("#aPriceLow").css("font-weight", "normal");
                    document.getElementById("<%=hdnOrderBy.ClientID%>").value = " AppPrice Desc";
                    ResetAll();
                } else if ($(this).val() == "1") {
                    //                    $(this).css("font-weight", "bold");
                    $("#aPriceLow").css("font-weight", "normal");
                    document.getElementById("<%=hdnOrderBy.ClientID%>").value = " vw_SerachProduct.appProductId Desc";
                    ResetAll();
                } else if ($(this).val() == "2") {
                    //                    $(this).css("font-weight", "bold");
                    $("#aPriceLow").css("font-weight", "normal");
                    document.getElementById("<%=hdnOrderBy.ClientID%>").value = " vw_SerachProduct.appProductId Asc";
                    ResetAll();
                }
                // alert($(this).val())
            });
            $("#<%=aCategory.ClientID%>").click(function (event) {
                $("#<%=hdnSubCategory.ClientID%>").val("");
                $("#divProduct").html("");
                $("#divProcessing").css("display", "block");
                $("#divMsg").css("display", "none");
                ResetAll();
                // GetRecords();

            });
            //            $("#aPriceLow").click(function (event) {
            //                $(this).css("font-weight", "bold");
            //                $("#aPriceHigh").css("font-weight", "normal");
            //                document.getElementById("<%=hdnOrderBy.ClientID%>").value = " AppPrice Asc";
            //                ResetAll();
            //            });
            //            $("#aPriceHigh").click(function (event) {
            //                $(this).css("font-weight", "bold");
            //                $("#aPriceLow").css("font-weight", "normal");
            //                document.getElementById("<%=hdnOrderBy.ClientID%>").value = " AppPrice Desc";
            //                ResetAll();
            //            });
            ResetAll();
        });

        function silderChange() {
            ResetAll();
        }


        $(window).scroll(function () {

            if ($(window).scrollTop() >= ($(document).height() - $(window).height() - 500)) {
                var IsProcess = $("#hdnIsProcess").val();
                if (IsProcess == "") {
                    var IsData = $("#hdnIsNoData").val();
                    if (IsData == "") {
                        $("#hdnIsProcess").val("True");
                        pageIndex++;
                        $("#divProcessing").css("display", "block");
                        $("#divMsg").css("display", "none");
                        $("#divMoreData").css("display", "none");
                        GetRecords();
                    }
                }
            }
        });

        function GetRecords() {
            //    alert("getrecords");
            var PageSize = 8;
            var Category = $("#<%=hdnCategory.ClientID%>").val();
            var SubCategory = $("#<%=hdnSubCategory.ClientID%>").val();
            var Property = $("#<%=hdnPropertyIds.ClientID%>").val();
            var Color = $("#<%=hdnColorIds.ClientID%>").val();
            var Size = $("#<%=hdnSizeIds.ClientID%>").val();
            var OrderBy = $("#<%=hdnOrderBy.ClientID%>").val();
            var Price = $("#Slider").val();
            var obj = { strPageSize: PageSize, strPageIndex: pageIndex, strCategory: Category, strSubCategory: SubCategory, strProperty: Property, strColor: Color, strSize: Size, strOrderBy: OrderBy, strPrice: Price };
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "<%=Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath %>/Products.aspx/LoadProductData",
                data: JSON.stringify(obj),
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    //alert(response.responseText);
                },
                error: function (response) {
                    //  alert(response.responseText);
                }
            });
        }

        function AddToCart(varProductDetailId, varProductID, varProductColorID, varProductSizeID) {
            $("#<%=hdnProductDetailID.ClientID%>").val(varProductDetailId);
            $("#<%=hdnPKID.ClientID%>").val(varProductID);
            $("#<%=hdnProductColorId.ClientID%>").val(varProductColorID);
            $("#<%=hdndefaultSizeID.ClientID%>").val(varProductSizeID);
            document.getElementById("<%= btnClick.ClientID %>").click();
        }

        function OnSuccess(response) {
            var data = response.d;
            //alert(data);
            $("#divProduct").append(data);
            $("#divProcessing").css("display", "none");

            if ((document.getElementById("divProduct").innerHTML == "") && (data != "")) {
                $("#divMsg").css("display", "none");
                $("#divMoreData").css("display", "none");
                $("#hdnIsNoData").val("True");
            }
            else if ((document.getElementById("divProduct").innerHTML == "") && (data == "")) {
                $("#divMsg").css("display", "block");
                $("#divMoreData").css("display", "none");
                $("#hdnIsNoData").val("True");
            }
            else if ((document.getElementById("divProduct").innerHTML != "") && (data == "")) {
                $("#divMsg").css("display", "none");
                $("#divMoreData").css("display", "block");
                $("#hdnIsNoData").val("True");
            }
            else if ((document.getElementById("divProduct").innerHTML != "") && (data != "")) {
                $("#divMsg").css("display", "none");
                $("#divMoreData").css("display", "none");
                $("#hdnIsNoData").val("");
            }

            $("#hdnIsProcess").val("");
        }
        //                function OnSuccess(response) {
        //                    var data = response.d;
        //                    $("#divProduct").append(data);
        //                    $("#divProcessing").css("display", "none");
        //                    if (document.getElementById("divProduct").innerHTML != "" && data != "") {
        //                        $("#divMsg").css("display", "none");
        //                        $("#divMoreData").css("display", "block");
        //                        $("#hdnIsNoData").val("True");
        //                    } else if (document.getElementById("divProduct").innerHTML != "" && data == "") {
        //                        $("#divMsg").css("display", "block");
        //                        $("#divMoreData").css("display", "none");
        //                        $("#hdnIsNoData").val("True");
        //                    } else if (document.getElementById("divProduct").innerHTML == "" && data != "") {
        //                        $("#divMsg").css("display", "block");
        //                        $("#divMoreData").css("display", "none");
        //                        $("#hdnIsNoData").val("True");
        //                    } else if (document.getElementById("divProduct").innerHTML == "" && data == "") {
        //                        $("#divMsg").css("display", "block");
        //                        $("#divMoreData").css("display", "none");
        //                        $("#hdnIsNoData").val("True");
        //                    }
        //                    $("#hdnIsProcess").val("");
        //                }

        function Onload() {
            try {
                document.getElementById("Slider").value = "0;" + document.getElementById("<%=hdnMaxPrice.ClientID%>").value;
                jQuery("#Slider").slider({ from: 0, to: document.getElementById("<%=hdnMaxPrice.ClientID%>").value, step: 10, smooth: true, round: 0, dimension: "&nbsp;", skin: "plastic" });
            }
            catch (e) {
            }
        }
        function SelectChkSize(strId) {
            var strIds = document.getElementById("<%=hdnSizeIds.ClientID%>").value;

            if (strIds.indexOf(strId) > -1) {
                strIds = strIds.replace(strId, " ");
            }
            else {
                strIds += "," + strId;
            }
            strIds = strIds.trim();
            var res = strIds.split(",");

            var strValue = "";
            for (var i = 0; i < res.length; i++) {
                if (res[i] != "" && res[i] != " ") {
                    strValue += res[i] + ",";
                }

            }
            strValue = strValue.substring(0, strValue.length - 1);
            document.getElementById("<%=hdnSizeIds.ClientID%>").value = strValue;
            ResetAll();

        }

        function SelectColor(strId) {
            document.getElementById("<%=hdnColorIds.ClientID%>").value = strId;
            ResetAll();

        }

        function SelectChkPropertyPreValue(strId) {
            var strIds = document.getElementById("<%=hdnPropertyIds.ClientID%>").value;
            if (strIds.indexOf(strId) > -1) {
                strIds = strIds.replace(strId, " ");
            }
            else {
                strIds += "," + strId;
            }
            strIds = strIds.trim();
            var res = strIds.split(",");
            var strValue = "";
            for (var i = 0; i < res.length; i++) {
                if (res[i] != "" && res[i] != " ") {
                    strValue += res[i] + ",";
                }
            }
            strValue = strValue.substring(0, strValue.length - 1);
            document.getElementById("<%=hdnPropertyIds.ClientID%>").value = strValue;
            ResetAll();

        }

        function ClearProperty(strId) {
            var strIds = document.getElementById("<%=hdnPropertyIds.ClientID%>").value;
            $('.ClearProperty' + strId).each(function () {
                if (this.checked) {
                    var id = this.value;
                    if (strIds.indexOf(id) > -1) {
                        strIds = strIds.replace(id, " ");
                    }
                }
                this.checked = false;
            });
            strIds = strIds.trim();
            var res = strIds.split(",");
            var strValue = "";
            for (var i = 0; i < res.length; i++) {
                if (res[i] != "" && res[i] != " ") {
                    strValue += res[i] + ",";
                }
            }
            strValue = strValue.substring(0, strValue.length - 1);
            document.getElementById("<%=hdnPropertyIds.ClientID%>").value = strValue;
            ResetAll();
        }

        function ResetAll() {
            //alert("reset");
            $("#divProcessing").css("display", "block");
            $("#divMsg").css("display", "none");
            $("#divMoreData").css("display", "none");
            $("#divProduct").empty();
            $("#hdnIsProcess").val("");
            $("#hdnIsNoData").val("");
            var Price = $("#Slider").val();
            var obj = { strCategory: $("#<%=hdnCategory.ClientID%>").val(), strSubCategory: $("#<%=hdnSubCategory.ClientID%>").val(), strProperty: $("#<%=hdnPropertyIds.ClientID%>").val(), strColor: $("#<%=hdnColorIds.ClientID%>").val(), strSize: $("#<%=hdnSizeIds.ClientID%>").val(), strOrderBy: $("#<%=hdnOrderBy.ClientID%>").val(), strPrice: Price };
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "<%=strServerURL %>Products.aspx/LoadProductCount",
                data: JSON.stringify(obj),
                dataType: "json",
                success: function (response) {
                    document.getElementById("<%=lblTotalProduct.ClientID%>").innerHTML = response.d;
                },
                failure: function (response) {
                    alert(response.responseText);
                },
                error: function (response) {
                    alert(response.responseText);
                }
            });
            pageIndex = 1;
            GetRecords();
        }
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        Sys.Application.add_load(Onload);
    </script>
    <div class="mens">
        <div class="main">
            <div class="wrap">
                <ul class="breadcrumb breadcrumb__t">
                    <a href='<%=objPageBase.GetAlias("Default.aspx") %>'>Home</a> <a id="aCategory" class="clearlink"
                        style="cursor: pointer;" runat="server"></a>
                    <asp:Label runat="server" ID="lblLiteral"></asp:Label>
                    <%-- <h4>
                                <asp:Label runat="server" ID="lblCategory"></asp:Label>
                                <asp:Label runat="server" ID="lblSubCategory"></asp:Label>
                            </h4>--%>
                </ul>
                <DInfo:DisplayInfo runat="server" ID="DInfo" />
                <div class="rsidebar Properties_in_left">
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
                    <asp:Repeater ID="RepProperty" runat="server" OnItemDataBound="RepProperty_ItemDataBound">
                        <ItemTemplate>
                            <div class="sky-form">
                                <h4>
                                    <div class="float-lt">
                                        <%#Eval("appDisplayName") %>
                                    </div>
                                    <div class="float-rt">
                                        <a id="Clear" class="lblClear" onclick='<%# "ClearProperty("+Eval("appPropertyID")+")" %>'>
                                            Clear</a>
                                        <%--<asp:LinkButton ID="lnkCLear" CssClass="clearlink" runat="server" CommandArgument='<%#Eval("appPropertyID") %>' CommandName="ClearAll" >Clear</asp:LinkButton>--%>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </h4>
                                <div class="row row1 scroll-pane">
                                    <div class="col col-4">
                                        <asp:Repeater ID="RepPropertyValue" runat="server">
                                            <ItemTemplate>
                                                <%-- <label class="checkbox" id="lblCount" runat="server" title='<%# Eval("appPreValue") %>' onclick='<%# "ChkSize("+Eval("appPropertyPreValueID")+")" %>'><asp:CheckBox ID="chkValue"  runat="server" oncheckedchanged="chkValue_CheckedChanged" AutoPostBack="true" /><i></i><%#Eval("appPreValue")%></label>--%>
                                                <label class="checkbox">
                                                    <input type="checkbox" name="checkbox" id="chkProperty" value='<%# Eval("appPropertyPreValueID") %>'
                                                        class='ClearProperty<%#Eval("appPropertyID") %>' onchange='<%# "SelectChkPropertyPreValue("+Eval("appPropertyPreValueID")+")" %>' />
                                                    <i></i>
                                                    <%#Eval("appPreValue")%></label>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                    <asp:HiddenField ID="hdnPkId" runat="server" Value='<%#Eval("appPropertyID") %>' />
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div class="sky-form">
                        <h4>
                            <div class="float-lt">
                                Price
                            </div>
                            <div class="float-rt">
                            </div>
                            <div class="clear">
                            </div>
                        </h4>
                        <div class="row1 scroll-pane" style="height: 20px; padding: 20px 5px 20px 10px;">
                            <div class="col col-4">
                                <span class="pricebar" onmouseup="silderChange();">
                                    <input id="Slider" type="slider" name="price" value="0;10000" /></span>
                            </div>
                        </div>
                    </div>
                    <div class="sky-form" id="Seccolor" runat="server">
                        <h4>
                            <div class="float-lt">
                                color
                            </div>
                            <div class="float-rt">
                                <a id="AClorClear" class="lblClear">Clear</a>
                            </div>
                            <div class="clear">
                            </div>
                        </h4>
                        <div class="color-list">
                            <asp:Repeater ID="dtColor" runat="server">
                                <ItemTemplate>
                                    <a id="lnkbtnColor" onclick='<%# "SelectColor("+Eval("appColorId")+")" %>' style="cursor: pointer;">
                                          <asp:Image ID="img1" runat="server" ImageUrl='<%#strServerURL +"admin/"+Eval("appColorImage") %>' AlternateText='<%#Eval("appColorName") %>'
                                                       />
                                    </a>
                                </ItemTemplate>
                            </asp:Repeater>
                            <%-- <li><a href="#"><span class="color1"></span>
                                        <p class="red">
                                            Red</p>
                                    </a></li>
                            --%>
                        </div>
                    </div>
                </div>
                <div class="cont Products_in_right">
                    <%--<h2 class="head">
                        Women's</h2>--%>
                    <div class="mens-toolbar">
                        <div class="sort float-lt sort-by">
                            <div class="float-lt">
                                <label class="lblcenter">
                                    Sort : 
                                </label>
                            </div>
                            <div class="float-lt sort">
                                <asp:DropDownList runat="server" ID="ddlSortByPrice">
                                    <asp:ListItem Text="--Select Option--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Newest-Oldest" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Oldest-Newest" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Low-High" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="High-Low " Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="clear">
                            </div>
                            <%--<select>
                                            <option value="">Position </option>
                                            <option value="">Name </option>
                                            <option value="">Price </option>
                                        </select>--%>
                            <%--<a href="">
                                            <img src="<%=strServerURL %>Images/arrow2.gif" alt="" class="v-middle"></a>--%>
                        </div>
                        <div class="float-rt">
                            <div class="limiter visible-desktop">
                                <ul class="w_nav">
                                    <li>
                                        <div class="lblcenter">
                                            [ Total Product :<b>
                                                <asp:Label runat="server" ID="lblTotalProduct" Text="0"></asp:Label></b> ]</div>
                                    </li>
                                    <li></li>
                                    <%-- <li>Sort : Price - </li>
                                            <li><a id="aPriceLow" class="clearlink" style="cursor: pointer;">Low-High</a>
                                                <%-- <asp:LinkButton ID="lnkbtnPriceLow" runat="server" OnClick="lnkbtnPriceLow_Click">Low-High</asp:LinkButton>
                                            </li>
                                            |
                                            <li><a id="aPriceHigh" class="clearlink" style="cursor: pointer;">High-Low</a>
                                                <%--  <asp:LinkButton ID="lnkbtnPriceHigh" runat="server" OnClick="lnkbtnPriceHigh_Click"
                                    Font-Bold="true">High-Low</asp:LinkButton>
                                            </li>--%>
                                    <div class="clear">
                                    </div>
                                </ul>
                            </div>
                        </div>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="top-box">
                        <%--<div class="grids_of_4" id="divProduct">
                                </div>
                                <div class="clear">
                                </div>
                                <div class="grids_of_4" id="divProcessing" align="center">
                                    <img src="<%=strServerURL %>Images/Processing.gif" width="20%" />
                                </div>
                                <div id="divMsg" align="center">
                                    No Product Available
                                </div>
                                <div id="divMoreData" align="center">
                                    No More Product Available
                                </div>--%>
                        <!-- grids_of_4 -->
                        <div class="grids_of_4" id="divProduct">
                        </div>
                        <div class="clear">
                        </div>
                        <div class="grids_of_4" id="divProcessing" align="center">
                            <img src="<%=strServerURL %>Images/Processing.gif" width="20%" />
                        </div>
                        <div class="grids_of_4 divMsg" id="divMsg" align="center">
                             Product not available
                        </div>
                        <div class="grids_of_4 divMsg" id="divMoreData" align="center">
                            No more product available
                        </div>
                        <%-- <div class="col_1_of_3 spans_1_of_3">
                                    <a href="single.html">
                                        <div class="inner_content clearfix">
                                            <div class="product_image">
                                                <img src="images/brown-and-grey-shaded-georgette-casual-saree-800x1100.jpg" alt="" />
                                            </div>
                                            <div class="sale-box">
                                                <span class="on_sale title_shop">New</span></div>
                                            <div class="price">
                                                <div class="cart-left">
                                                    <p class="title">
                                                        Designer Saree</p>
                                                    <div class="price1">
                                                        <span class="actual">Rs.12.00</span>
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
                                    <a href="single.html">
                                        <div class="inner_content clearfix">
                                            <div class="product_image">
                                                <img src="images/brown-and-grey-shaded-georgette-casual-saree-800x1100.jpg" alt="" />
                                            </div>
                                            <div class="price">
                                                <div class="cart-left">
                                                    <p class="title">
                                                        Designer Saree</p>
                                                    <div class="price1">
                                                        <span class="actual">Rs.12.00</span>
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
                <div class="clear">
                </div>
            </div>
        </div>
    </div>
    <asp:Button ID="btnClick" runat="server" Style="display: none" OnClick="btnClick_Click" />
    <asp:HiddenField ID="hdnProductColorId" runat="server" Value="" />
    <asp:HiddenField ID="hdndefaultSizeID" runat="server" Value="" />
    <asp:HiddenField ID="hdnPKID" runat="server" Value="" />
    <asp:HiddenField ID="hdnProductDetailID" runat="server" Value="" />
    <asp:HiddenField ID="hdnCategory" runat="server" Value="" />
    <asp:HiddenField ID="hdnSubCategory" runat="server" Value="" />
    <asp:HiddenField ID="hdnColorIds" runat="server" Value="" />
    <asp:HiddenField ID="hdnPropertyIds" runat="server" Value="" />
    <asp:HiddenField ID="hdnSizeIds" runat="server" Value="" />
    <asp:HiddenField ID="hdnOrderBy" runat="server" Value="" />
    <asp:HiddenField ID="hdnTemp" runat="server" Value="" />
    <asp:HiddenField ID="hdnMaxPrice" runat="server" Value="" />
    <input type="hidden" id="hdnIsProcess" value="" />
    <input type="hidden" id="hdnIsNoData" value="" />
    <div>
        <MyCart:Cart ID="ViewCart" runat="server" />
        <MyLogin:Login ID="CustLogin" runat="server" />
    </div>
</asp:Content>
