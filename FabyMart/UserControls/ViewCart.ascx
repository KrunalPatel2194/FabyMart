<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ViewCart.ascx.cs" Inherits="ViewCart" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<%@ Register Src="~/UserControls/Login.ascx" TagName="LoginInfo" TagPrefix="Login" %>
<input type="button" runat="server" id="btnCart" style="display: none;" />
<cc1:ModalPopupExtender ID="MpeCart" runat="server" TargetControlID="btnCart" PopupControlID="divCart"
    BackgroundCssClass="modalbackground" DropShadow="false">
</cc1:ModalPopupExtender>
<div id="divCart" runat="server" class="modalpopup cartpopup">
    <asp:ImageButton ID="imbtnClose" CssClass="modalbClose" ImageUrl="~/images/popup_close_btn.png" OnClick="imbtnClose_Click"
        runat="server"></asp:ImageButton>
    <div class="modaldetail">
        <asp:Panel ID="pnal" runat="server">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modalheader">
                        My Cart
                    </div>
                    <div class="panel-search">
                        <script type="text/javascript">
                            function isNumber(evt) {
                                evt = (evt) ? evt : window.event;
                                var charCode = (evt.which) ? evt.which : evt.keyCode;
                                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                                    return false;
                                }
                                return true;
                            }
                        </script>
                        <DInfo:DisplayInfo runat="server" ID="DInfo" />
                        <div class="table-responsive" style="height: 400px; overflow: auto;">
                            <%-- <asp:DataList ID="dtProduct" runat="server" CellPadding="0" CellSpacing="0" RepeatColumns="1"
                                Width="100%" DataKeyField="appProductID" OnItemCommand="dtProduct_ItemCommand">
                                <ItemTemplate>
                                    <div class="shoping_bag1">
                                        <div class="shoping_left">
                                            <div class="shoping1_of_1">
                                                <img src='<%=strServerURL %>admin/<%#Eval("appLargeImage") %>' class="img-responsive"
                                                    alt='<%#Eval("appProductName") %>' />
                                            </div>
                                            <div class="shoping1_of_2">
                                                <h4>
                                                    <a href='<%# objPageBase.GetAlias("ProductDetail.aspx") + objPageBase.generateUrl(Eval("appProductName").ToString()) %>'>
                                                        <%#Eval("appProductName") %></a>
                                                </h4>
                                                <span>size : <b>
                                                    <%#Eval("appSize") %></b>&nbsp;&nbsp; | qty : <b>
                                                        <%#Eval("appQty") %></b> | code : <b><%#Eval("appProductCode") %></b></span>
                                                <ul class="s_icons">
                                                 
                                                    <li>
                                                        <asp:LinkButton ID="lnkbtnColor" runat="server" CommandName="Delete" ToolTip="Delete"
                                                            OnClientClick="return confirm('Do you really want to delete Product?');" CommandArgument='<%#Eval("appProductID") %>'>
                                        <img src="<%=strServerURL %>images/s_icon3.png" alt=""></asp:LinkButton></li>
                                                </ul>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="shoping_right">
                                            <p>
                                                <span class="WebRupee">Rs</span><%#Eval("appPrice")%></p>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>--%>
                            <asp:GridView ID="dgvCart" runat="server" AutoGenerateColumns="false" AllowSorting="false"
                                DataKeyNames="appProductDetailID" BorderWidth="0" CssClass="gridmain nomargin"
                                OnRowCommand="dgvCart_RowCommand" OnRowDeleting="dgvCart_RowDeleting">
                                <HeaderStyle CssClass="gridheader" />
                                <RowStyle CssClass="gridrow" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="30%" HeaderStyle-Width="30%">
                                        <ItemTemplate>
                                            <div class="shoping1_of_1_Cart">
                                                <img src='<%=strServerURL %>admin/<%#Eval("appNormalImage") %>' class="img-responsive"
                                                    alt='<%#Eval("appProductName") %>' />
                                            </div>
                                            <div class="shoping1_of_2">
                                                <h4 class="setBottomMargin2">
                                                    <a href='<%# objPageBase.GetAlias("ProductDetail.aspx") + objPageBase.generateUrl(Eval("appProductName").ToString()) %>'
                                                        class="setColor">
                                                        <%#Eval("appProductName") %></a>
                                                </h4>
                                                <span>
                                                    <%--size : <b>
                                                    <%#Eval("appSize") %></b>&nbsp;&nbsp; | code : <b>
                                                        <%#Eval("appProductCode") %>
                                                 </b>--%>
                                                    SKU No : <b>
                                                        <%#Eval("appSKUNo")%></b> </span>
                                                <ul class="s_icons setTopMargin2">
                                                    <li>
                                                        <asp:LinkButton ID="lnkbtnColor" runat="server" CommandName="Delete" ToolTip="Delete"
                                                            OnClientClick="return confirm('Do you really want to delete Product?');" CommandArgument='<%#Eval("appProductID") %>'>
                                        <img src="<%=strServerURL %>Images/s_icon3.png" alt=""></asp:LinkButton></li>
                                                </ul>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="1%" HeaderStyle-Width="1%" HeaderText="Quantity" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
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
                                            <%-- <span><span class="WebRupee">Rs</span><%#Eval("appPrice")%></span>--%>
                                            <%= Session[appFunctions.Session.CurrencyImage.ToString()]%><%# Math.Round(Convert.ToDecimal(Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString()) * Convert.ToDecimal(Eval("appPrice").ToString()), 0)%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="5%" HeaderStyle-Width="5%" HeaderText="Discount"
                                        HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <%-- <span><span class="WebRupee">Rs</span><%#Eval("appPrice")%></span>--%>
                                            <%= Session[appFunctions.Session.CurrencyImage.ToString()]%><%# Math.Round(Convert.ToDecimal(Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString()) * Convert.ToDecimal(Eval("appDiscountPrice").ToString()), 0)%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="5%" HeaderStyle-Width="5%" HeaderText="Total"
                                        HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <%--   <span><span class="WebRupee">Rs</span><%#Eval("appTotalPrice")%></span>--%>
                                            <%= Session[appFunctions.Session.CurrencyImage.ToString()]%><%# Math.Round(Convert.ToDecimal(Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString()) * Convert.ToDecimal(Eval("appTotalPrice").ToString()), 0)%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="shoping_bag2 btn_form" id="divProductTotalPrice" runat="server" style="padding: 5px;">
                            <div class="float-lt width">
                                <asp:Button ID="btnContinueShopping" runat="server" Text="Continue Shopping" OnClick="btnContinueShopping_Click"
                                  class="setstyle"  />
                                &nbsp;
                                <asp:Button ID="btnplaceorder" runat="server" Text="Place Order" OnClick="btnplaceorder_Click"
                                 class="setstyle"  />
                                &nbsp;
                                <asp:Button ID="btnUpdateCart" runat="server" Text="Update Cart" OnClick="btnUpdateCart_Click"
                                 class="setstyle"  />
                            </div>
                            <div class="float-rt">
                                <p class="tot">
                                    Total : &nbsp;<asp:Label ID="lblTotalPrice" runat="server" Text="0" CssClass="setColor"></asp:Label></p>
                            </div>
                            <div class="clear">
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
</div>
<Login:LoginInfo ID="CustLogin" runat="server" />
