<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="CCAvenueOnlinePaymentStatus.aspx.cs" Inherits="CCAvenueOnlinePaymentStatus" %>

<%@ Register Src="~/UserControls/Customer.ascx" TagName="Customer" TagPrefix="Home" %>
<%@ Register Src="Admin/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta name="description" content="<%=metaDescription %>" />
    <meta name="keywords" content="<%=metaKeywords %>" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="mens">
        <div class="main">
            <div class="wrap">
                <div class="minHeight">
                    <div class="cont span_2_of_3">
                        <DInfo:DisplayInfo ID="DInfo" runat="server" />
                    </div>
                    <div class="clear"></div>
                    <div class="width">
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
                                            DataKeyField="appSubOrderID" BorderWidth="0" Width="100%" OnItemDataBound="DataListSubOrder_RowDataBound">
                                            <ItemTemplate>
                                                <div class="OrderSubBody">
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
                                                        <%#Eval("appSellingPrice")%>
                                                        <span class="spanBalck">x </span>
                                                        <%#Eval("appQty")%>
                                                        <span class="spanBalck">= </span><span class="spanValue">
                                                            <%#Eval("appTotal")%></span><br />
                                                        <div class="">
                                                            -<%#Eval("appTotalDiscount")%></div>
                                                        <br />
                                                        <hr class="hrPay" />
                                                        <div class="">
                                                            <%#Eval("appTotalamountToBePaid")%>
                                                        </div>
                                                    </div>
                                                   
                                                    <div style="clear: both;">
                                                    </div>
                                                </div>
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
                </div>
                <div class="rsingle span_1_of_single">
                    <%-- <Home:Customer ID="CusomerControl" runat="server" />--%>
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
