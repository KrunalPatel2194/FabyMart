<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="MyFavouriteProduct.aspx.cs" Inherits="MyFavouriteProduct" %>

<%@ Register Src="~/UserControls/Customer.ascx" TagName="Customer" TagPrefix="Home" %>
<%@ Register Src="~/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta name="description" content="<%=metaDescription %>" />
    <meta name="keywords" content="<%=metaKeywords %>" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="mens">
        <div class="main">
            <div class="wrap">
                <div class="minHeight">
                    <ul class="breadcrumb breadcrumb__t">
                        <a class="home" href='<%= objPageBase.GetAlias("Default.aspx")%>'>Home</a> / <span>My
                            Favourite Product</span>
                    </ul>
                    <div class="clear">
                    </div>
                    <div class="col_1_of_changePassword ">
                        <h4 class="titleTrackOrder">
                            Favourite Product</h4>
                        <br />
                        <div style="width: 100%" align="center">
                            <asp:GridView ID="dgvFavourite" runat="server" AutoGenerateColumns="false" AllowSorting="false"
                                DataKeyNames="appProductID" BorderWidth="0" CssClass="gridmain" Width="100%"
                                OnRowCommand="dgvFavourite_RowCommand" OnRowDeleting="dgvFavourite_RowDeleting">
                                <HeaderStyle CssClass="gridheader" />
                                <RowStyle CssClass="gridrow" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="80%" HeaderStyle-Width="80%" HeaderStyle-BorderColor="#666"
                                        HeaderText="Product Info" HeaderStyle-HorizontalAlign="Justify">
                                        <ItemTemplate>
                                            <div class="shoping1_of_1_Fav">
                                                <img src='<%=strServerURL %>admin/<%#Eval("appNormalImage") %>' class="img-responsive"
                                                    alt='<%#Eval("appProductName") %>' />
                                            </div>
                                            <div class="shoping1_of_2_Fav">
                                                <h4 class="setBottomMargin2">
                                                    <a href='<%# objPageBase.GetAlias("ProductDetail.aspx") + generateUrl(Eval("appProductName").ToString()) %>'
                                                        target="_blank" class="setColor">
                                                        <%#Eval("appProductName") %></a>
                                                </h4>
                                                <span>
                                                    <%--size : <b>
                                                <%#Eval("appSize") %></b>&nbsp;&nbsp; |--%>
                                                    <div class="setBottomMargin2">
                                                        Code : <b>
                                                            <%#Eval("appProductCode") %></b>
                                                    </div>
                                                    <div class="setBottomMargin2">
                                                        Date : <b>
                                                            <%#Eval("appCreatedDate")%></b></div>
                                                </span>
                                                <ul class="s_icons setTopMargin2">
                                                    <li>
                                                        <asp:LinkButton ID="lnkbtnColor" runat="server" CommandName="Delete" ToolTip="Delete"
                                                            OnClientClick="return confirm('Do you really want to delete Favourite Product?');"
                                                            CommandArgument='<%#Eval("appFavouriteProductID") %>'>
                                        <img src="images/s_icon3.png" alt=""></asp:LinkButton></li>
                                                </ul>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="20%" HeaderStyle-Width="20%" HeaderText="Price"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%= Session[appFunctions.Session.CurrencyImage.ToString()]%><%# Math.Round( Convert.ToDecimal(Session[appFunctions.Session.CurrencyInRupee.ToString()].ToString()) * Convert.ToDecimal(Eval("appPrice").ToString()),2)%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="rsingle span_1_of_single">
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
