<%@ Page Title="Confirmed Order List" Language="C#" MasterPageFile="~/Admin/blank.master"
    AutoEventWireup="true" CodeFile="ConfirmedProductList.aspx.cs" Inherits="ConfirmedProductList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                        <asp:GridView ID="dgvGridView" Width="100%" runat="server" AutoGenerateColumns="False"
                            AllowPaging="false" AllowSorting="false" DataKeyNames="" CssClass="table table-striped table-bordered table-hover"
                            HeaderStyle-Wrap="false" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
                            PagerSettings-Mode="NumericFirstLast" PageSize="10" >
                            <Columns>
                             <asp:TemplateField HeaderText="Image" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                                    HeaderStyle-Width="1%">
                                                                    <ItemTemplate>
                                                                    <asp:Image ID="img" runat="server" ImageUrl='<%#Eval("appThumbImage") %>' AlternateText='<%#Eval("appProductName") %>'
                                                        Width="100" />
                                                                       <%-- <img height="100px" src="<%#strServerURL+Eval("appThumbImage") %>" />--%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                <asp:BoundField HeaderText="SKU No" DataField="appSKUNo" SortExpression="appSKUNo" />
                                <asp:TemplateField HeaderText="Product Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="70%"
                                    HeaderStyle-Width="70%" SortExpression="appProductName">
                                    <ItemTemplate>
                                        <%#Eval("appProductName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                                <asp:BoundField HeaderText="Qty" DataField="TotalQty" SortExpression="TotalQty" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
