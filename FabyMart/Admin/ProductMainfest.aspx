<%@ Page Title="Confirmed Order List" Language="C#" MasterPageFile="~/Admin/blank.master"
    AutoEventWireup="true" CodeFile="ProductMainfest.aspx.cs" Inherits="ProductMainfest" %>

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
                <hr class="nomargin" />
                <div class="panel-search">
                    <div class="table-responsive">
                        <div class="col-md-12 col-sm-12 col-xs-12 manifestheader" id="divlbl" runat="server"
                            visible="false">
                            <div class="col-md-3 col-sm-3 col-xs-3 fleft" style="color: #b80901; font-size: 20px;
                                font-weight: bold; width: 40%; padding-left: 0px;">
                                <asp:Label runat="server" ID="lblSiteName"></asp:Label>
                            </div>
                            <div class="fleft">
                                <asp:Label runat="server" ID="lblSellerName"></asp:Label>
                            </div>
                            <div class="fclear">
                            </div>
                            <%--<div class="col-md-3 col-sm-3 col-xs-3 ">
                                Manifest No : <span id="spanMenifestNo" runat="server" class="isbold"></span>
                            </div>--%>
                            <%--  <div class="col-md-6 col-sm-6 col-xs-6">
                                Shipping Provider Name : <span id="spanProviderName" runat="server" class="isbold">
                                </span>
                            </div>--%>
                            <div class="col-md-3 col-sm-3 col-xs-3" style="font-size: 20px; font-weight: bold;
                                width: 40%; padding-left: 0px;">
                                <%--FEDEX--%>
                                <asp:Label ID="lblCourierComp" runat="server"></asp:Label>
                                (Total Packages- <span id="spanPackage" runat="server" class="isbold"></span>)
                            </div>
                        </div>
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <asp:GridView ID="dgvGridView" Width="100%" runat="server" AutoGenerateColumns="False"
                                AllowSorting="true" DataKeyNames="appOrderID,appPaymentMode" CssClass="table table-striped table-bordered table-hover"
                                HeaderStyle-Wrap="false" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
                                HeaderStyle-BackColor="#ffcc65" PagerSettings-Mode="NumericFirstLast" PageSize="10"
                                OnRowDataBound="dgvGridView_RowDataBound">
                                <PagerSettings Position="Top" />
                                <Columns>
                                    <asp:BoundField HeaderText="No" DataField="appRowNo" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="2%" HeaderStyle-Width="2%" />
                                    <asp:BoundField HeaderText="Reference #" DataField="appOrderNo" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="2%" HeaderStyle-Width="2%" />
                                    <asp:BoundField HeaderText="SKU CODE" DataField="appSKUNo" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="2%" HeaderStyle-Width="2%" />
                                    <asp:BoundField HeaderText="Recipient Details" DataField="appReceiverDetail" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="2%" HeaderStyle-Width="2%" />
                                    <asp:BoundField HeaderText="Mobile" DataField="appReceiverContactNo1" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="2%" HeaderStyle-Width="2%" />
                                    <asp:TemplateField HeaderText="Product" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%"
                                        HeaderStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Repeater ID="RepOrderDetail" runat="server">
                                                <ItemTemplate>
                                                    <div style="margin-top: 2px; padding: 5px 0px;">
                                                        <%# Eval("appProductName") %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Manifest Date" DataField="appManifestDate" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%" />
                                    <%--<asp:BoundField HeaderText="pincode" DataField="appBuyerPinCode" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="1%" HeaderStyle-Width="1%" />--%>
                                    <%-- <asp:TemplateField HeaderText="Weight" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%"
                                        HeaderStyle-Width="2%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWeight" runat="server" Text='0'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="AWB Number" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%"
                                        HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <div>
                                                FORMS NEEDED
                                            </div>
                                            <div class="barcode">
                                                <%#Eval("AppOrderNo")%>
                                            </div>
                                            <div class="middle">
                                                <%#Eval("AppOrderNo")%>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- 
                                    <%--  <asp:TemplateField HeaderText="Barcode" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%"
                                        HeaderStyle-Width="2%">
                                        <ItemTemplate>
                                            <div class="barcode">
                                                <%#Eval("appDocketNo")%>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <div class="setLeftPadding">
                            <div>
                                <b>For Courier Personnel: </b>
                            </div>
                            <div>
                                I confirm that I have collected the packages as per the AWBs mention on this Manifest.<br />
                                <b>I also confirm that I have collected Forms for the Shipments marked as "FORMS NEEDED"
                                    in this Manifest</b>
                            </div>
                            <div>
                                <div class="fleft setWidth">
                                    ___________
                                </div>
                                <div class="fleft setWidth">
                                    ____________
                                </div>
                                <div class="fleft setWidth">
                                    ____________
                                </div>
                                <div class="fclear">
                                </div>
                            </div>
                            <div>
                                <div class="fleft setWidth">
                                    Name
                                </div>
                                <div class="fleft setWidth">
                                    Signature
                                </div>
                                <div class="fleft setWidth">
                                    Date and Time
                                </div>
                                <div class="fclear">
                                </div>
                            </div>
                            <div class="fclear">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
