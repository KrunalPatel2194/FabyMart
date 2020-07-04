<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="Cart.aspx.cs" Inherits="Cart" %>

<%@ Register Src="~/UserControls/Login.ascx" TagName="LoginInfo" TagPrefix="Login" %>
<%@ Register Src="~/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta name="description" content="<%=metaDescription %>" />
    <meta name="keywords" content="<%=metaKeywords %>" />
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="mens">
        <div class="main">
            <div class="wrap">
                <ul class="breadcrumb breadcrumb__t">
                    <a class="" href='<%= objPageBase.GetAlias("Default.aspx")%>'>Home</a> / <span>My Cart</span>
                </ul>
                <div class="col_1_of_changePassword ">
                    <h4 class="titleTrackOrder">
                        My Cart</h4>
                    <asp:Panel ID="pnlCart" runat="server" DefaultButton="btnUpdateCart">
                        <div id="divProduct" class="minHeight" runat="server">
                            <fieldset class="input">
                                <div class="shoping_bag">
                                    <h4>
                                        <div class="float-lt">
                                            <img src="images/bag1.png" /></div>
                                        <div class="float-lt setTopMargin4">
                                            My Shopping Bag / <span>
                                                <asp:Label ID="lblCount" runat="server" Text="0"></asp:Label>
                                                item</span>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </h4>
                                    <div class="clear">
                                    </div>
                                </div>
                                <div>
                                    <DInfo:DisplayInfo runat="server" ID="DInfo" />
                                    <asp:GridView ID="dgvCart" runat="server" AutoGenerateColumns="false" AllowSorting="false"
                                        DataKeyNames="appProductDetailID" BorderWidth="0" CssClass="gridmain" OnRowCommand="dgvCart_RowCommand"
                                        OnRowDeleting="dgvCart_RowDeleting">
                                        <HeaderStyle CssClass="gridheader" />
                                        <RowStyle CssClass="gridrow" />
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="20%" HeaderStyle-Width="20%">
                                                <ItemTemplate>
                                                    <div class="shoping1_of_1_Fav">
                                                        <img src='<%=strServerURL %>admin/<%#Eval("appNormalImage") %>' class="img-responsive"
                                                            alt='<%#Eval("appProductName") %>' />
                                                    </div>
                                                    <div class="shoping1_of_2">
                                                        <h4>
                                                            <a href='<%# objPageBase.GetAlias("ProductDetail.aspx") + generateUrl(Eval("appProductName").ToString())%>'
                                                                class="setColor" target="_blank">
                                                                <%#Eval("appProductName") %></a>
                                                        </h4>
                                                        <span>
                                                            <%--size : <b>
                                                        <%#Eval("appSize") %></b>&nbsp;&nbsp; | code : <b>
                                                            <%#Eval("appProductCode") %></b>--%>
                                                            SKU No : <b>
                                                                <%#Eval("appSKUNo")%></b></span>
                                                        <ul class="s_icons setTopMargin3" id="UlCancel" runat="server">
                                                            <li>
                                                                <asp:LinkButton ID="lnkbtnColor" runat="server" CommandName="Delete" ToolTip="Cancel order"
                                                                    OnClientClick="return confirm('Do you really want to Cancel Order?');" CommandArgument='<%#Eval("appProductID") %>'>
                                                                <img src="images/s_icon3.png" alt=""></asp:LinkButton></li>
                                                        </ul>
                                                    </div>
                                                    <div class="clear">
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="1%" HeaderStyle-Width="1%" HeaderText="Quantity"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <div class="registration_form">
                                                        <asp:TextBox ID="txtQty" runat="server" Text='<%#Eval("appQty") %>' onkeypress="return isNumber(event)"
                                                            Width="35" Style="text-align: center;"></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="5%" HeaderStyle-Width="5%" HeaderText="Real Price"
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
                                <div class="clear">
                                </div>
                                <div class="setPaddingCart" id="divProductTotalPrice" runat="server">
                                    <div>
                                        <div class="float-lt setBottomPad">
                                            <asp:Button ID="btnplaceorder" runat="server" Text="Place Order" CssClass="setRightMargin2"
                                                OnClick="btnplaceorder_Click" />
                                            &nbsp; &nbsp;
                                        </div>
                                        <div class="float-lt setBottomPad">
                                            <asp:Button ID="btnUpdateCart" runat="server" Text="Update Cart" OnClick="btnUpdateCart_Click" />
                                        </div>
                                    </div>
                                    <div class="float-rt ">
                                        <p class="tot">
                                            Total &nbsp;
                                            <asp:Label ID="lblTotalPrice" runat="server" Text="0" CssClass="setColor"></asp:Label></p>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                                <div class="clear">
                                </div>
                            </fieldset>
                        </div>
                    </asp:Panel>
                </div>
                <div class="rsingle span_1_of_single">
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
    </div>
    <div>
        <Login:LoginInfo ID="CustLogin" runat="server" />
    </div>
</asp:Content>
