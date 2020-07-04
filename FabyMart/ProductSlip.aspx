<%@ Page Title="Return Product Slip" Language="C#" MasterPageFile="~/blank.master"
    AutoEventWireup="true" CodeFile="ProductSlip.aspx.cs" Inherits="ProductSlip" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        window.onunload = refreshParent;
        function refreshParent() {
            window.opener.location.reload();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div>
                <div class="panel-search right-content">
                    <asp:Button ID="printButton" runat="server" CssClass="btn btn-primary" Text="Print"
                        OnClientClick="javascript:window.print();" />
                </div>
            </div>
        </div>
        <hr class="nomargin" />
        <asp:DataList runat="server" ID="dtProductInvoice" DataKeyField="appReturnOrderID"
            OnItemDataBound="dtProductInvoice_ItemDataBound">
            <ItemTemplate>
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12">
                        <div>
                            <div class="panel-search">
                                <div class="table-responsive">
                                    <div class="PrintPage">
                                        <div class="LeftSilde">
                                            <div class="HeaderText">
                                                FebyMart
                                            </div>
                                            <div class="borderBottom">
                                            </div>
                                            <div class="printAddress">
                                                <div class="lineheight">
                                                    <%#Eval("appSellerName")%>
                                                </div>
                                                <div class="lineheight">
                                                    <span>H.No :</span>
                                                    <%#Eval("appSellerAddress")%>
                                                </div>
                                                <div class="lineheight">
                                                    <span class="MainText">CITY :</span>
                                                    <%#Eval("appSellerCity")%>
                                                </div>
                                                <div class="lineheight">
                                                    <span class="MainText">STATE :</span>
                                                    <%#Eval("appSellerState")%>
                                                </div>
                                                <div class="lineheight">
                                                    <span class="MainText">PIN :</span>
                                                    <%#Eval("appSellerPinCode")%>
                                                </div>
                                                <div class="lineheight">
                                                    <span class="MainText">COUNTRY :</span>
                                                    <%#Eval("appSellerCountry")%>
                                                </div>
                                                <div class="lineheight">
                                                    <span class="MainText">PHONE :</span>
                                                    <%#Eval("appSellerContactNo1")%>
                                                </div>
                                            </div>
                                            <div class="borderBottom">
                                            </div>
                                            <div class="tablewidth printAddress">
                                                <div class="box">
                                                    -
                                                </div>
                                                <div class="box">
                                                    <span class="MainText">TSK :</span>-
                                                    <%--<span id="spanLeftTopTSK" runat="server"></span>--%>
                                                </div>
                                                <div class="box">
                                                    <span class="MainText">FORM ID :</span>-
                                                    <%--<span id="SpanLeftTopCourierCompanyForm"
                                                    runat="server"></span>--%>
                                                </div>
                                                <div class="box">
                                                    <%--<span class="MainText" id="SpanLeftTopCourierCompanyCode" runat="server">-</span>--%>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                                <div class="barcode">
                                                    <%#Eval("appDocketNo")%>
                                                </div>
                                                <div class="box">
                                                    <span class="MainText">STANDARD</span>
                                                </div>
                                                <div class="box">
                                                    <span class="MainText">COD</span>
                                                </div>
                                                <div class="box">
                                                    <span class="MainText">DEL</span>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </div>
                                            <div class="tablewidth">
                                                <asp:GridView ID="dgvLeftGridView" Width="100%" runat="server" AutoGenerateColumns="False"
                                                    AllowSorting="false" DataKeyNames="appSubOrderID" CssClass="table table-striped table-bordered"
                                                    HeaderStyle-Wrap="false" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
                                                    PagerSettings-Mode="NumericFirstLast">
                                                    <PagerSettings Position="Top" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Product Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="90%"
                                                            HeaderStyle-Width="90%" SortExpression="appProductName">
                                                            <ItemTemplate>
                                                                <div>
                                                                    <%#Eval("appProductName")%>
                                                                </div>
                                                                <div>
                                                                    Code :
                                                                    <%#Eval("appSKUNo")%>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Qty" DataField="appQty" SortExpression="appQty" ItemStyle-Width="1%"
                                                            HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField HeaderText="Unit Price" DataField="appSellingPrice" SortExpression="appSellingPrice"
                                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                            HeaderStyle-Width="1%" />
                                                        <asp:BoundField HeaderText="Sub Total" DataField="appTotal" SortExpression="appTotal"
                                                            ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" ItemStyle-Width="1%"
                                                            HeaderStyle-Width="1%" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="tablewidth">
                                                <div class="left">
                                                    <div class="lineheight">
                                                        &nbsp;
                                                    </div>
                                                    <div class="lineheight">
                                                        COD : <span id="spanLeftTotalCOD" runat="server"></span>
                                                    </div>
                                                </div>
                                                <div class="middle">
                                                    <div class="lineheight">
                                                        Total :
                                                    </div>
                                                    <div class="lineheight">
                                                        Cash On Delivery Charges :
                                                    </div>
                                                    <div class="lineheight">
                                                        Shipping Charges :
                                                    </div>
                                                    <div class="lineheight OnTopBorder">
                                                        Amount :
                                                    </div>
                                                </div>
                                                <div class="right">
                                                    <div class="lineheight" id="lineheight">
                                                        <asp:Label ID="lblLeftOrdeTotal" runat="server" Text=''></asp:Label>
                                                    </div>
                                                    <div class="lineheight">
                                                        <asp:Label ID="lblLeftCODCharges" runat="server" Text=''></asp:Label>
                                                    </div>
                                                    <div class="lineheight">
                                                        <asp:Label ID="lblLeftShippingCharges" runat="server" Text=''></asp:Label>
                                                    </div>
                                                    <div class="lineheight OnTopBorder">
                                                        <asp:Label ID="lblLeftAmount" runat="server" Text=''></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                            <div class="borderBottom">
                                            </div>
                                            <div class="printAddress">
                                                <div>
                                                    <span class="MainText">if undelivered, return to :</span>
                                                </div>
                                                <div>
                                                    <span class="MainText">
                                                        <%#Eval("appPickupName")%></span>,
                                                </div>
                                                <div>
                                                    <%#Eval("appPickupAddress")%>
                                                </div>
                                            </div>
                                            <div class="borderBottom">
                                            </div>
                                            <div class="tablewidth printAddress">
                                                <div class="box">
                                                    <span class="MainText">TRK :</span>-
                                                </div>
                                                <div class="box">
                                                    <span class="MainText">FORM ID :</span>
                                                </div>
                                                <div class="box">
                                                    <span class="MainText">COD Amount :</span> <span id="SpanLeftCODAmount" runat="server">
                                                    </span>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                                <div class="barcode">
                                                    <%#Eval("appDocketNo")%>
                                                </div>
                                                <div class="box">
                                                    <span class="MainText">PRIORITY_OVERNIGHT</span>
                                                </div>
                                                <div class="box">
                                                </div>
                                                <div class="box">
                                                </div>
                                                <div class="box">
                                                    <span class="MainText">CODE RETURN</span>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="RightSilde">
                                            <div class="bgcolor">
                                                RETAIL INVOICE
                                            </div>
                                            <div class="RightleftSide">
                                                Return OrderNo <span class="MainText">
                                                    <%#Eval("appReturnOrderID")%></span>
                                            </div>
                                            <div class="RightrightSide">
                                                Return Date : <span class="MainText">
                                                    <%#Eval("appRequestedDate")%></span>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                            <div class="RightleftSide Rightbborder">
                                                <div class="printAddress">
                                                    <div class="lineheight">
                                                        <span class="SubHeader MainText">Seller :</span>
                                                    </div>
                                                    <div class="lineheight">
                                                        <%#Eval("appSellerName")%>
                                                    </div>
                                                    <div class="lineheight">
                                                        <span>H.NO :</span><%#Eval("appSellerAddress")%></div>
                                                </div>
                                                <div class="lineheight">
                                                    <span class="MainText">CITY :</span><%#Eval("appSellerCity")%></div>
                                                <div class="lineheight">
                                                    <span class="MainText">STATE :</span>
                                                    <%#Eval("appSellerState")%>
                                                </div>
                                                <div class="lineheight">
                                                    <span class="MainText">PIN :</span><%#Eval("appSellerPinCode")%></div>
                                                <div class="lineheight">
                                                    <span class="MainText">COUNTRY :</span>
                                                    <%#Eval("appSellerCountry")%>
                                                </div>
                                                <div class="lineheight">
                                                    <span class="MainText">MOBILE :</span><%#Eval("appSellerContactNo1")%></div>
                                            </div>
                                            <div class="RightrightSide">
                                                <div class="printAddress">
                                                    <div class="lineheight">
                                                        <span class="SubHeader MainText">Customer :</span>
                                                    </div>
                                                    <div class="lineheight">
                                                        <%#Eval("appPickupName")%>
                                                    </div>
                                                    <div class="lineheight">
                                                        <%#Eval("appPickupAddress")%>
                                                    </div>
                                                    <div class="lineheight">
                                                        <span class="MainText">CITY :</span><%#Eval("appCity")%></div>
                                                    <div class="lineheight">
                                                        <span class="MainText">STATE :</span>
                                                        <%#Eval("appState")%>
                                                    </div>
                                                    <div class="lineheight">
                                                        <span class="MainText">PIN :</span><%#Eval("appPickupPIN ")%>
                                                    </div>
                                                    <div class="lineheight">
                                                        <span class="MainText">COUNTRY :</span><%#Eval("appCountry")%>
                                                    </div>
                                                    <div class="lineheight">
                                                        <span class="MainText">MOBILE :</span>
                                                        <%#Eval("appPickupContactNo1")%>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                            <div class="tablewidth">
                                                <asp:GridView ID="dgvRightgrid" Width="100%" runat="server" AutoGenerateColumns="False"
                                                    AllowSorting="false" DataKeyNames="appSubOrderID" CssClass="table table-striped table-bordered"
                                                    HeaderStyle-Wrap="false" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
                                                    PagerSettings-Mode="NumericFirstLast">
                                                    <PagerSettings Position="Top" />
                                                    <Columns>
                                                        <asp:BoundField HeaderText="S.No" DataField="appRowNo" SortExpression="appRowNo"
                                                            ItemStyle-Width="1%" HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="Center"
                                                            HeaderStyle-HorizontalAlign="Center" />
                                                        <asp:TemplateField HeaderText="Product Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="90%"
                                                            HeaderStyle-Width="90%" SortExpression="appProductName">
                                                            <ItemTemplate>
                                                                <div>
                                                                    <%#Eval("appProductName")%>
                                                                </div>
                                                                <div>
                                                                    Code :
                                                                    <%#Eval("appSKUNo")%>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Qty" DataField="appQty" SortExpression="appQty" ItemStyle-Width="1%"
                                                            HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField HeaderText="Unit Price" DataField="appSellingPrice" SortExpression="appSellingPrice"
                                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                            HeaderStyle-Width="1%" />
                                                        <asp:BoundField HeaderText="Sub Total" DataField="appTotal" SortExpression="appTotal"
                                                            ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" ItemStyle-Width="1%"
                                                            HeaderStyle-Width="1%" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="tablewidth">
                                                <div class="left">
                                                    <div class="lineheight">
                                                        &nbsp;
                                                    </div>
                                                    <div class="lineheight">
                                                        &nbsp;
                                                    </div>
                                                </div>
                                                <div class="middle">
                                                    <div class="lineheight">
                                                        Total :
                                                    </div>
                                                    <div class="lineheight OnTopBorder">
                                                        Amount :
                                                    </div>
                                                </div>
                                                <div class="right">
                                                    <div class="lineheight">
                                                        <asp:Label ID="lblRightTotal" runat="server" Text=''></asp:Label>
                                                        <%--<span id="SpanRightTotal" runat="server">0</span>--%>
                                                    </div>
                                                    <div class="lineheight OnTopBorder">
                                                        <asp:Label ID="lblRightAmount" runat="server" Text=''></asp:Label>
                                                        <%--<span id="SpanRightAmount" runat="server" style="font-weight: bold;">0</span>--%>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                            <div class="borderBottom">
                                            </div>
                                            <div class="printAddress">
                                                <div>
                                                    <span class="MainText">Declaration:</span>
                                                </div>
                                                <div>
                                                    We hereby declare that the particulars mentioned above are true and correct.
                                                </div>
                                                <div>
                                                    Above price in inclusive of all taxes.
                                                </div>
                                                <div>
                                                    if you find selling price on this invoice to be more than MRP mentioned on the product,
                                                    please inforum us at care@fabfiza.com
                                                </div>
                                                <div>
                                                    Goods sold as part of this invoice are intended for end user consumption/retail
                                                    sale and not for re-sale.
                                                </div>
                                            </div>
                                            <div class="borderBottom">
                                            </div>
                                            <div class="LastHeader">
                                                THIS IS COMPUTER GENERATED INVOICE AND DOES NOT REQUIRE SIGNATURE.
                                            </div>
                                            <div class="borderBottom">
                                            </div>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div style="page-break-after: always">
                    <span style="display: none;">&nbsp;</span></div>
            </ItemTemplate>
        </asp:DataList>
    </div>
</asp:Content>
