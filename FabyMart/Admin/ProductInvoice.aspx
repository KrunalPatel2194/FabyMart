<%@ Page Title="Confirmed Order List" Language="C#" MasterPageFile="~/Admin/blank.master"
    AutoEventWireup="true" CodeFile="ProductInvoice.aspx.cs" Inherits="ProductInvoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        window.onunload = refreshParent;
        function refreshParent() {
            window.opener.location.reload();
        }
        function printContent(el) {
            var restorepage = document.body.innerHTML;
            var printcontent = document.getElementById(el).innerHTML;
            document.body.innerHTML = printcontent;
            window.print();
            document.body.innerHTML = restorepage;
        }
    </script>
    <style type="text/css">
        @media print {
            @page { margin: 5px 0px; }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="font-size: 8px;">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div style="position: fixed; left: 845px; padding-top: 10px;">
                <button onclick="printContent('divContent')" class="btn btn-primary">
                    Print</button>
            </div>
        </div>
        <hr class="nomargin" />
        <div id="divContent" style="width: 842px; height: 595px; border: 1px solid #000;">
            <asp:DataList runat="server" ID="dtProductInvoice" DataKeyField="appOrderID" OnItemDataBound="dtProductInvoice_ItemDataBound">
                <ItemTemplate>
                    <div class="col-md-12 col-sm-12 col-xs-12">
                        <div style="padding: 0px;">
                            <div class="PrintPage">
                                <div class="LeftSilde">
                                    <div class="col-md-12" style="border-bottom: 3px solid #000; text-align: center;">
                                        <div class="col-md-4 left" style="text-transform: uppercase;">
                                            <div>
                                                ORDERED VIA:
                                            </div>
                                            <div>
                                                <img width="60" alt="" src='<%= strServerURL%>Images/FabyMartLogo.png' />
                                            </div>
                                        </div>
                                        <div class="col-md-4 left" style="background-color: #ececec; padding: 10px;">
                                            <div>
                                                PREPAID
                                            </div>
                                            <div>
                                                No Collection
                                            </div>
                                        </div>
                                        <div class="col-md-4 left">
                                            <div>
                                                &nbsp;
                                            </div>
                                            <div>
                                                Weight:
                                                        <asp:Label ID="lblWeight" runat="server" Text="0"></asp:Label>
                                                gms
                                            </div>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="printAddress" style="text-transform: uppercase;">
                                        <div class="lineheight">
                                            <div>
                                                Ship To:
                                            </div>
                                            <b>
                                                <%#Eval("appReceiverName")%></b>
                                        </div>
                                        <div class="lineheight">
                                            <%--<span>H.No :</span>--%>
                                            <%#Eval("appReceiverAddress")%>
                                        </div>
                                        <div class="lineheight">
                                            <span class="MainText">CITY :</span>
                                            <%#Eval("appBuyerCity")%>
                                        </div>
                                        <div class="lineheight">
                                            <span class="MainText">STATE :</span>
                                            <%#Eval("appBuyerState")%>
                                        </div>
                                        <div class="lineheight">
                                            <span class="MainText">PIN :</span>
                                            <%#Eval("appBuyerPinCode")%>
                                        </div>
                                        <%--<div class="lineheight">
                                                    <span class="MainText">COUNTRY :</span>
                                                    <%#Eval("appBuyerCountry")%>
                                                </div>--%>
                                        <div class="lineheight">
                                            <span class="MainText">MOBILE :</span>
                                            <%#Eval("appReceiverContactNo1")%>
                                        </div>
                                    </div>
                                    <div class="printAddress" style="text-transform: uppercase;">
                                        <div class="col-md-12" style="text-align: center;">
                                            DELIVERY - PREPAID
                                        </div>
                                        <div class="tablewidth">
                                            <div class="barcode">
                                                <%#Eval("AppOrderNo")%>
                                            </div>
                                            <div style="text-align: center;">
                                                AWB #:
                                                        <%#Eval("appDocketNo")%>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                    </div>
                                    <table class="table table-striped table-bordered" style="width: 100% !important; margin: 0px">
                                        <tr>
                                            <th style="text-align: center" width="1%">FabyMart<br />
                                                SKU
                                            </th>
                                            <th width="1%">Item Name
                                            </th>
                                            <th width="1%" style="text-align: center;">QTY
                                            </th>
                                            <th width="1%" style="text-align: center;">VALUE
                                            </th>
                                            <th style="text-align: center" width="1%">SHIPPING<br />
                                                CHARGE
                                            </th>
                                            <th style="text-align: center" width="1%">FINAL<br />
                                                AMOUNT
                                            </th>
                                        </tr>
                                        <asp:Repeater ID="dgvLeftGridView" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="text-align: center" width="1%">
                                                        <%#Eval("appSKUNo")%>
                                                    </td>
                                                    <td width="1%">
                                                        <%#Eval("appProductName")%><br />
                                                    </td>
                                                    <td width="1%" style="text-align: center;">
                                                        <%#Eval("appQty")%>
                                                    </td>
                                                    <td width="1%" style="text-align: center;">
                                                        <%#Eval("appSellingPrice")%>
                                                    </td>
                                                    <td style="text-align: center" width="1%">0
                                                    </td>
                                                    <td style="text-align: center" width="1%">
                                                        <%#Eval("appTotal")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr>
                                            <td colspan="2" style="text-align: right;">Total
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblTotalQty" runat="server" Text="0"></asp:Label>
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblAmount" runat="server" Text="0"></asp:Label>
                                            </td>
                                            <td style="text-align: center;">0
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblTotalAmount" runat="server" Text="0"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="printAddress" style="border-bottom: 3px solid #000;">
                                        <div class="col-md-12" style="text-align: center;">
                                            FABYMART ORDER REFERENCE
                                        </div>
                                        <div class="tablewidth">
                                            <div class="barcode">
                                                <%#Eval("AppOrderNo")%>
                                            </div>
                                            <div style="text-align: center;">
                                                <%-- SHIPMENT #: 952829180--%>
                                            </div>
                                            <div style="text-align: center; line-height: 0px">
                                                ORDER #:
                                                        <%#Eval("AppOrderNo")%>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div>
                                            <b>If Undelivered, return to:</b>
                                        </div>
                                    </div>
                                    <div class="printAddress">
                                        <div style="float: left;">
                                            <asp:Label runat="server" ID="lblSellerAddress"></asp:Label>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="borderBottom">
                                    </div>
                                </div>
                                <div class="RightSilde">
                                    <div class="bgcolor">
                                        RETAIL INVOICE
                                    </div>
                                    <div class="RightleftSide">
                                        <span class="MainText">INVOICE #:</span>
                                        <%#Eval("AppOrderNo")%>
                                    </div>
                                    <div class="RightrightSide">
                                        <span class="MainText">INVOICE DATE :</span>
                                        <%#Eval("AppCreatedDate")%>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div style="border-bottom: 2px solid #000; text-transform: uppercase;">
                                        <div class="RightleftSide Rightbborder">
                                            <div class="printAddress">
                                                <div class="lineheight" style="background-color: #ccc;">
                                                    <span class="SubHeader MainText">SELLER</span>
                                                </div>
                                                <div class="lineheight">
                                                    <b>
                                                        <%#Eval("AppDisplayName")%></b>
                                                </div>
                                                <div class="lineheight">
                                                    <%#Eval("AppAddress1_Pickup")%>
                                                    <%#Eval("AppAddress2_Pickup")%>
                                                </div>
                                                <div class="lineheight">
                                                    <span class="MainText">CITY: </span>
                                                    <%#Eval("appSellerCity")%>
                                                </div>
                                                <div class="lineheight">
                                                    <span class="MainText">STATE: </span>
                                                    <%#Eval("appSellerState")%>
                                                </div>
                                                <div class="lineheight">
                                                    <span class="MainText">PIN: </span>
                                                    <%#Eval("appSellerPincode")%>
                                                </div>
                                                <div class="lineheight">
                                                    <span class="MainText">PHONE:</span>
                                                    <%#Eval("AppContactNo1")%>
                                                </div>
                                                <div class="lineheight">
                                                    <span class="MainText">COUNTRY'S VAT TIN: </span>
                                                </div>
                                                <div class="lineheight">
                                                    <span class="MainText">COUNTRY'S PAN No.: </span>ABDPI3060L
                                                </div>
                                            </div>
                                        </div>
                                        <div class="RightrightSide">
                                            <div class="printAddress">
                                                <div class="lineheight" style="background-color: #ccc;">
                                                    <span class="SubHeader MainText">BUYER</span>
                                                </div>
                                                <div class="lineheight">
                                                    <b>
                                                        <%#Eval("appReceiverName")%></b>
                                                </div>
                                                <div class="lineheight">
                                                    <%#Eval("appReceiverAddress")%>
                                                </div>
                                            </div>
                                            <div class="lineheight">
                                                <span class="MainText">CITY: </span>
                                                <%#Eval("appBuyerCity")%>
                                            </div>
                                            <div class="lineheight">
                                                <span class="MainText">STATE: </span>
                                                <%#Eval("appBuyerState")%>
                                            </div>
                                            <div class="lineheight">
                                                <span class="MainText">PIN: </span>
                                                <%#Eval("appBuyerPinCode")%>
                                            </div>
                                            <%--<div class="lineheight">
                                                        <span class="MainText">COUNTRY :</span>
                                                        <%#Eval("appBuyerCountry")%>
                                                    </div>--%>
                                            <div class="lineheight">
                                                <span class="MainText">MOBILE: </span>
                                                <%#Eval("appReceiverContactNo1")%>
                                            </div>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div>
                                        <div class="RightleftSide" style="padding: 10px;">
                                            <div>
                                                <span>DISPATCHED VIA: </span>Delivery
                                            </div>
                                            <div>
                                                <span>AWB #: </span>
                                                <%#Eval("appDocketNo")%>
                                            </div>
                                        </div>
                                        <div class="RightrightSide" style="padding: 10px;">
                                            <div>
                                                <span>FABYMART ORDER REF:</span>
                                                <%#Eval("AppOrderNo")%>
                                            </div>
                                            <%-- <div>
                                                        <span>FABYMART SHIPMENT ID:</span> 952829180
                                                    </div>--%>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <table class="table table-striped table-bordered" style="margin-bottom: 5px">
                                        <tr>
                                            <th style="text-align: center" width="1%">S.NO
                                            </th>
                                            <th width="10%">ITEM DESCRIPTION
                                            </th>
                                            <th width="1%" style="text-align: center;" width="1%">QUANTITY
                                            </th>
                                            <th width="1%" style="text-align: center;" width="1%">RATE
                                            </th>
                                            <th style="text-align: center" width="1%">AMOUNT
                                            </th>
                                        </tr>
                                        <asp:Repeater ID="dgvRightgrid" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="text-align: center" width="1%">
                                                        <%#Eval("appRowNo")%>
                                                    </td>
                                                    <td width="10%">
                                                        <%#Eval("appProductName")%><br />
                                                    </td>
                                                    <td width="1%" style="text-align: center;">
                                                        <%#Eval("appQty")%>
                                                    </td>
                                                    <td width="1%" style="text-align: center;">
                                                        <%#Eval("appSellingPrice")%>
                                                    </td>
                                                    <td style="text-align: center" width="1%">
                                                        <%#Eval("appTotal")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr>
                                            <td colspan="4" style="text-align: right;">SUB-Total
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblRightSubTotal" runat="server" Text=''></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: right;">TOTAL AMOUNT FOR PRODUCTS MENTIONED ABOVE
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblRightTotal" runat="server" Text=''></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="clearfix">
                                    </div>
                                    <div class="printAddress" style="padding-left: 5px;">
                                        <div style="text-transform: uppercase;">
                                            <asp:Label runat="server" ID="lblAmountInWords"></asp:Label>
                                        </div>
                                        <div>
                                            <span class="MainText">DECLARATION</span>
                                        </div>
                                        <div>
                                            We declare that this invoice shows actual price of the goods described inclusive
                                                    of taxes and that all particulars are true and correct.
                                        </div>
                                        <div>
                                            If you find selling price on this invoice to be more than MRP mentioned on the product,
                                                    please inform us at fabymart.com. Goods sold as part of this invoice are intended
                                                    for end user consumption/retail sale and not for re-sale.
                                        </div>
                                        <div>
                                            <span class="MainText">CUSTOMER ACKNOWLEDGEMENT</span>
                                        </div>
                                        <div>
                                            I
                                                    <%#Eval("appReceiverName")%>
                                                    hereby confirm that the above said products are being purchased for my internal/personal
                                                    consumption and not for re-sale. I further understand and agree with fabymart.com
                                                    terms and conditions for sale.
                                        </div>
                                    </div>
                                    <div class="borderBottom">
                                    </div>
                                    <div class="LastHeader">
                                        <b>THIS IS COMPUTER GENERATED INVOICE AND DOES NOT REQUIRE SIGNATURE</b>
                                    </div>
                                    <div class="borderBottom">
                                    </div>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                        </div>
                    </div>

                    <div style="page-break-after: always">
                        <span style="display: none;">&nbsp;</span>
                    </div>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>
</asp:Content>
