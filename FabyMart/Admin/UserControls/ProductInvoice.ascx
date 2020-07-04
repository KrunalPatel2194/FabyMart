<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductInvoice.ascx.cs"
    Inherits="UserControls_ProductInvoice" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Admin/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<input type="button" runat="server" id="btnProductinvoice" style="display: none;" />
<cc1:ModalPopupExtender ID="mpeProductinvoice" runat="server" TargetControlID="btnProductinvoice"
    PopupControlID="divProductinvoice" BackgroundCssClass="modalbackground" DropShadow="false"
    CancelControlID="imgbtnProductinvoiceClose">
</cc1:ModalPopupExtender>
<div id="divProductinvoice" class="modalpopup_ panel panel-default" runat="server"
    style="display: none; width: 750px;">
    <asp:LinkButton ID="imgbtnProductinvoiceClose" CssClass="modalbclose" runat="server">
                                        <span class="action-icon set-icon"><i class="fa fa-minus"></i></span>
    </asp:LinkButton>
    <div class="modalheader_ panel-heading">
        Invoice Detail
    </div>
    <div class="modaldetail">
        <div class="panel-search">
            <div style="float: left; width: 18%;">
                <img id="ImgPhoto" runat="server" height="180" />
            </div>
            <div style="float: left; width: 80%;">
                <div style="padding-left: 10px; margin-bottom: 5px;">
                    Product : <b><span id="spanProduct" runat="server"></span></b>
                    <hr class="nomargin" />
                </div>
                <div style="float: left; width: 50%;">
                    <div class="table-responsive">
                        <div class="entryformmain">
                            <div class="entryform">
                                <div class="labelstyle">
                                    Order No :
                                </div>
                                <div class="lblcontrolstyle">
                                    <b><span id="spanOrderId" runat="server"></span></b>
                                </div>
                            </div>
                            <div class="entryform">
                                <div class="labelstyle">
                                    SKU No :
                                </div>
                                <div class="lblcontrolstyle">
                                    <b><span id="spanSku" runat="server"></span></b>
                                </div>
                            </div>
                            <div class="entryform">
                                <div class="labelstyle">
                                    Color :
                                </div>
                                <div class="lblcontrolstyle">
                                    <span style="width: 30px; border: 1px solid #3b3b3b;" id="divcolor" runat="server">&nbsp&nbsp
                                    </span>&nbsp<b><span id="spancolor" runat="server"></span></b>
                                </div>
                            </div>
                            <div class="entryform">
                                <div class="labelstyle">
                                    Size :
                                </div>
                                <div class="lblcontrolstyle">
                                    <b><span id="spanSize" runat="server"></span></b>
                                </div>
                            </div>
                            <div class="entryform">
                                Prefered Time : <b><span id="spanPreferedTime" runat="server"></span></b>
                            </div>
                        </div>
                    </div>
                </div>
                <div style="float: right; width: 50%;">
                    <div class="table-responsive">
                        <div class="entryformmain">
                            <div class="entryform">
                                <div class="labelstyle">
                                    Qty :
                                </div>
                                <div class="lblcontrolstyle">
                                    <b><span id="spanQty" runat="server"></span></b>
                                </div>
                            </div>
                            <div class="entryform">
                                <div class="labelstyle">
                                    Price :
                                </div>
                                <div class="lblcontrolstyle">
                                    <b><span id="spanPrice" runat="server"></span></b>
                                </div>
                            </div>
                            <div class="entryform">
                                <div class="labelstyle">
                                    Total :
                                </div>
                                <div class="lblcontrolstyle">
                                    <b><span id="spanTotal" runat="server"></span></b>
                                </div>
                            </div>
                            <div class="entryform">
                                <div class="labelstyle">
                                    Status :
                                </div>
                                <div class="lblcontrolstyle">
                                    <b><span id="spanStatus" runat="server"></span></b>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div style="clear: both;">
                </div>
            </div>
            <div style="clear: both;">
            </div>
        </div>
        <hr class="nomargin" />
        <div class="panel-search" style="height: 200px; overflow: auto;">
            <div style="float: left; width: 46%; margin-right: 15px;">
                <b>Billing Address :</b><br />
                <hr class="nomargin" />
                <div class="table-responsive">
                    <div class="entryformmain">
                        <div class="entryform">
                            <div class="labelstyle" style="width: 100%; text-align: left;">
                                <b><span id="spanBillingName" runat="server"></span></b>
                            </div>
                        </div>
                        <div class="entryform">
                            <div class="labelstyle" style="width: 100%; text-align: left;" id="divBillingMobile"
                                runat="server">
                            </div>
                        </div>
                        <div class="entryform">
                            <div class="labelstyle" style="width: 100%; text-align: left;" id="divBillingEmail"
                                runat="server">
                            </div>
                        </div>
                        <div class="entryform">
                            <div class="labelstyle" style="width: 100%; text-align: justify;" id="divBillingAddress"
                                runat="server">
                            </div>
                        </div>
                        <div class="entryform">
                            <div class="labelstyle" style="width: 100%; text-align: left;" id="divBillingMobile1"
                                runat="server">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div style="float: left; width: 46%; margin-right: 5px;">
                <b>Shipping Address :</b>
                <hr class="nomargin" />
                <div class="table-responsive">
                    <div class="entryformmain">
                        <div class="entryform">
                            <div class="labelstyle" style="width: 100%; text-align: left;">
                                <b><span id="spanShippingName" runat="server"></span></b>
                            </div>
                        </div>
                        <div class="entryform">
                            <div class="labelstyle" style="width: 100%; text-align: left;" id="divShippingMobile"
                                runat="server">
                            </div>
                        </div>
                        <div class="entryform">
                            <div class="labelstyle" style="width: 100%; text-align: left;" id="divShippingEmail"
                                runat="server">
                            </div>
                        </div>
                        <div class="entryform">
                            <div class="labelstyle" style="width: 100%; text-align: justify;" id="divShippingAddress"
                                runat="server">
                            </div>
                        </div>
                        <div class="entryform">
                            <div class="labelstyle" style="width: 100%; text-align: left;" id="divShippingMobile1"
                                runat="server">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div style="clear: both;">
            </div>
        </div>
        <%-- 
         <hr class="nomargin" />
        <div class="panel-search">
            <asp:GridView ID="dgvGridView" Width="100%" runat="server" AutoGenerateColumns="False"
                AllowSorting="false" DataKeyNames="appSubOrderID" CssClass="table table-striped table-bordered table-hover"
                HeaderStyle-Wrap="false" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
                PagerSettings-Mode="NumericFirstLast" PageSize="10">
                <PagerSettings Position="Top" />
                <Columns>
                    <asp:TemplateField HeaderText="Images" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                        HeaderStyle-Width="1%">
                        <ItemTemplate>
                            <img src='<%=strServerURL %>admin/<%#Eval("appNormalImage") %>' width="30" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Color" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                        HeaderStyle-Width="1%">
                        <ItemTemplate>
                            <div style="background-color: <%#Eval("appColorCode") %>; width: 30px; border: 1px solid #3b3b3b;">
                                &nbsp&nbsp
                            </div>
                            <div>
                                <%#Eval("appColorName")%></div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="SKU No" DataField="appSKUNo" SortExpression="appSKUNo" />
                    <asp:BoundField HeaderText="Product" DataField="appProductName" SortExpression="appProductName"
                        ItemStyle-HorizontalAlign="left" ItemStyle-Width="50%" HeaderStyle-Width="50%" />
                    <asp:BoundField HeaderText="Size" DataField="appSize" SortExpression="appSize" />
                    <asp:BoundField HeaderText="Qty" DataField="appQty" SortExpression="appQty" />
                    <asp:BoundField HeaderText="Price" DataField="appSellingPrice" SortExpression="appSellingPrice"
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Total" DataField="appTotal" SortExpression="appTotal"
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                </Columns>
            </asp:GridView>
        </div>
        <hr class="nomargin" />
        <div class="panel-search">
            <div style="float: left; width: 80%; margin-right: 10px; text-align: right;">
                <span style="font-size: 20px;">Grand Total </span>
            </div>
            <div style="float: right; margin-right: 5px;">
                <span style="font-size: 20px; font-weight: bold;" id="spanGrandTotal" runat="server">
                </span>Rs /-
            </div>
            <div style="clear: both;">
            </div>
        </div>--%>
    </div>
</div>
