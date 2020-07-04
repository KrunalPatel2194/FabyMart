<%@ Page Title="Order List" Language="C#" MasterPageFile="~/Admin/Main.master" AutoEventWireup="true"
    CodeFile="OrderList.aspx.cs" Inherits="OrderList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<%@ Register Src="~/Admin/UserControls/OrderStatus.ascx" TagName="OrderStatus" TagPrefix="MyOrder" %>
<%@ Register Src="~/Admin/UserControls/ProductInvoice.ascx" TagName="ProductInvoice"
    TagPrefix="MyInvoice" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Order List
                </div>
                <div>
                    <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>--%>
                    <div class="panel-search">
                        <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <hr class="nomargin" />
                    <div class="panel-search">
                       
                            Select Criteria :
                            <asp:DropDownList runat="server" ID="ddlFields" CssClass="form-control" TabIndex="3">
                                <asp:ListItem Text="--Select Option--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Product" Value="appProductName"></asp:ListItem>
                                <asp:ListItem Text="Quantity" Value="appQty"></asp:ListItem>
                                <asp:ListItem Text="Total Amount" Value="tblTemp.appAmount"></asp:ListItem>
                                <asp:ListItem Text="Sku No" Value="appSKUNo"></asp:ListItem>
                                <asp:ListItem Text="Customer Name" Value="appCustomerName"></asp:ListItem>
                                <asp:ListItem Text="Order No." Value="appOrderNo"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13)"></asp:TextBox>
                            <div class="Separator">
                                &nbsp;
                            </div>
                            <asp:DropDownList runat="server" ID="ddlDateType" CssClass="form-control" TabIndex="3">
                                <asp:ListItem Text="--Select Date Type--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Order Date" Value="tblOrder.appCreatedDate"></asp:ListItem>
                                <asp:ListItem Text="Dispatch Date" Value="tblSubOrder.appMaxDispatchDate"></asp:ListItem>
                                <asp:ListItem Text="Completed Date" Value="tblSubOrder.appCompletedDate"></asp:ListItem>
                                <asp:ListItem Text="Delivery Date" Value="tblSubOrder.appDeliveryDate"></asp:ListItem>
                            </asp:DropDownList>
                            Date
                            <asp:TextBox ID="txtStartDate" runat="server" CssClass="txtDate" placeholder="dd-MM-yyyy"></asp:TextBox>
                            <cc1:CalendarExtender ID="calStartDate" TargetControlID="txtStartDate" Format="dd-MM-yyyy"
                                PopupButtonID="EventStartDate" runat="server">
                            </cc1:CalendarExtender>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="rfvStartDate" runat="server" ErrorMessage="Enter Start Date"
                                ValidationGroup="Search" Text="*" ControlToValidate="txtStartDate" SetFocusOnError="true"
                                CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator Display="Dynamic" ID="REVStartDate" runat="server"
                                ValidationGroup="Search" Text="*" ControlToValidate="txtStartDate" SetFocusOnError="true"
                                CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                            To
                            <asp:TextBox ID="txtEndDate" runat="server" CssClass="txtDate" placeholder="dd-MM-yyyy"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtEndDate" Format="dd-MM-yyyy"
                                PopupButtonID="EventEndDate" runat="server">
                            </cc1:CalendarExtender>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="rfvEndDate" runat="server" ErrorMessage="Enter End Date"
                                ValidationGroup="Search" Text="*" ControlToValidate="txtEndDate" SetFocusOnError="true"
                                CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator Display="Dynamic" ID="REVEndDate" runat="server"
                                ValidationGroup="Search" Text="*" ControlToValidate="txtEndDate" SetFocusOnError="true"
                                CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                            <asp:Button ID="btnGO" runat="server" CssClass="btn btn-primary" Text="Search" ValidationGrou
                                p="Search" TabIndex="2" OnClick="btnGO_Click" />
                            <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary" Text="Reset"
                                TabIndex="4" OnClick="btnReset_Click" />
                      
                    </div>
                    <hr class="nomargin" />
                    <div class="panel-search">
                        <div class="fright">
                            Total&nbsp;Records&nbsp;:&nbsp; <span class="RecordCount">
                                <asp:Label ID="lblCount" runat="server" Text="0"> </asp:Label>
                            </span>
                            <div class="Separator">
                                &nbsp;
                            </div>
                            Per Page :
                            <asp:DropDownList ID="ddlPerPage" runat="server" AutoPostBack="true" CssClass="form-control"
                                OnSelectedIndexChanged="ddlPerPage_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="fclear">
                        </div>
                    </div>
                </div>
                <hr class="nomargin" />
                <DInfo:DisplayInfo runat="server" ID="DInfo" />
                <div class="panel-search">
                    <div class="table-responsive">
                        <asp:GridView ID="dgvGridView" Width="100%" runat="server" AutoGenerateColumns="False"
                            AllowSorting="true" DataKeyNames="appOrderId,appPaymentMode" CssClass="table table-striped table-bordered table-hover"
                            HeaderStyle-Wrap="false" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
                            PagerSettings-Mode="NumericFirstLast" PageSize="10" OnPageIndexChanging="dgvGridView_PageIndexChanging"
                            OnRowCreated="dgvGridView_RowCreated" OnRowDataBound="dgvGridView_RowDataBound"
                            OnSorting="dgvGridView_Sorting">
                            <PagerSettings Position="Top" />
                            <Columns>
                                <asp:TemplateField HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkHeader" runat="server" onclick="SelectAll(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelectRow" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="4%"
                                    HeaderStyle-Width="4%">
                                    <ItemTemplate>
                                        <div class="divGrid divPriceValuewithoutPadding">
                                            No :
                                        </div>
                                        <div class="divGrid ">
                                            <asp:Label ID="lblOrderNo" runat="server" Text='<%#Eval("appOrderNo") %>'></asp:Label>
                                        </div>
                                        <div class="divGrid divPriceValuewithoutPadding">
                                            Date :
                                        </div>
                                        <div class="divGrid ">
                                            <asp:Label ID="lblOrderDate" runat="server" Text='<%#Eval("appCreatedDate") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="70%"
                                    HeaderStyle-Width="70%">
                                    <ItemTemplate>
                                        <div style="margin-top: 5px;">
                                            <asp:GridView ID="dgvSubDetail" Width="100%" runat="server" AutoGenerateColumns="False"
                                                DataKeyNames="appSubOrderID,appSellingPrice,appTotalDiscount,appTotalamountToBePaid"
                                                CssClass="table table-bordered" HeaderStyle-Wrap="false" AllowPaging="false"
                                                AllowSorting="false">
                                                <PagerSettings Position="Top" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Image" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                        HeaderStyle-Width="1%">
                                                        <ItemTemplate>
                                                            <asp:Image ID="img" runat="server" ImageUrl='<%#Eval("appThumbImage") %>' AlternateText='<%#Eval("appProductName") %>'
                                                                Width="100" />
                                                            <%--   <img src="<%#strServerURL+Eval("appThumbImage") %>" />--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Product Info" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%"
                                                        HeaderStyle-Width="40%">
                                                        <ItemTemplate>
                                                            <div class="divGrid">
                                                                <span class="divPriceValuewithoutPadding">SKU No :</span> <span class="divGrid">
                                                                    <%#Eval("appSKUNo") %></span>
                                                            </div>
                                                            <div class="divGrid divPriceValuewithoutPadding">
                                                                Product Name :
                                                            </div>
                                                            <div class="divGrid">
                                                                <asp:Label ID="lblProductName" runat="server" Text='<%#Eval("appProductName") %>'></asp:Label>
                                                            </div>
                                                            <div class="divGrid divPriceValuewithoutPadding">
                                                                Product Code :
                                                            </div>
                                                            <div class="divGrid">
                                                                <asp:Label ID="lblProductCode" runat="server" Text='<%#Eval("appProductCode") %>'></asp:Label>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Price" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="40%"
                                                        HeaderStyle-Width="40%">
                                                        <ItemTemplate>
                                                            <div class="divGrid">
                                                                <div class="payLeft">
                                                                    <%# Eval("appSellingPrice")%>
                                                                    <span class="spanBalck">x </span>
                                                                    <%# Eval("appQty")%>
                                                                    :
                                                                </div>
                                                                <div class="payritght" style="color: <%# Convert.ToDecimal( Eval("appSellingPrice").ToString()) >= 0 ? "green" : "red" %>">
                                                                    <%# Eval("appSellingPrice")%>
                                                                </div>
                                                                <div style="clear: both">
                                                                </div>
                                                            </div>
                                                            <div class="divGrid">
                                                                <div class="payLeft">
                                                                    Discount :
                                                                </div>
                                                                <div class="payritght" style="color: <%# Convert.ToDecimal( Eval("appSellingPrice").ToString()) >= 0 ? "red" : "green" %>">
                                                                    -<%# Eval("appTotalDiscount")%>
                                                                </div>
                                                                <div style="clear: both">
                                                                </div>
                                                            </div>
                                                            <hr class="hrPay" />
                                                            <div class="divGrid">
                                                                <div class="payLeft">
                                                                    Amount :
                                                                </div>
                                                                <div class="payritght" style="color: <%# Convert.ToDecimal( Eval("appSellingPrice").ToString()) >= 0 ? "green" : "red" %>">
                                                                    <asp:Label runat="server" Text='<%# Eval("appTotalamountToBePaid")%>'></asp:Label>
                                                                </div>
                                                                <div style="clear: both">
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Quantity" DataField="appQty" SortExpression="appQty"
                                                        ItemStyle-Width="1%" HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="1%"
                                                        HeaderStyle-Width="1%">
                                                        <ItemTemplate>
                                                            <div class="divGrid divPriceValuewithoutPadding">
                                                                <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("appOrderStatus") %>'></asp:Label>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Total Amount" DataField="appAmount" SortExpression="appAmount"
                                    ItemStyle-Width="1%" HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="Center" />
                                <asp:TemplateField HeaderText="Customer Detail" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="5%" HeaderStyle-Width="5%">
                                    <ItemTemplate>
                                        <div class="divGrid divPriceValuewithoutPadding">
                                            Name :
                                        </div>
                                        <div class="divGrid ">
                                            <asp:Label ID="lblCustomerName" runat="server" Text='<%#Eval("appReceiverName") %>'></asp:Label>
                                        </div>
                                        <div class="divGrid divPriceValuewithoutPadding">
                                            Address :
                                        </div>
                                        <div class="divGrid ">
                                            <asp:Label ID="lblAddress" runat="server" Text='<%#Eval("appReceiverAddress") %>'></asp:Label>
                                        </div>
                                        <div class="divGrid divPriceValuewithoutPadding">
                                            Mobile No. :
                                        </div>
                                        <div class="divGrid ">
                                            <asp:Label ID="lblMobile" runat="server" Text='<%#Eval("appReceiverContactNo1") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dispatch Date" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="1%"
                                    HeaderStyle-Width="1%">
                                    <ItemTemplate>
                                        <div class="divGrid divPriceValuewithoutPadding" align="center" style="margin-top: 5px;">
                                            <div>
                                                <%#Eval("appRemmingDispatchDay")%>
                                            </div>
                                            <div>
                                                Remaining Days
                                            </div>
                                        </div>
                                        <div class="divGrid " align="center">
                                            <asp:Label ID="lblDispatchDate" runat="server" Text='<%#Eval("appMaxDispatchDate") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payment" HeaderStyle-Width="1%" ItemStyle-Width="1%"
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblPaymentMode" Font-Bold="true" Font-Size="10" Style="padding: 4px;
                                            color: #FFF" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <asp:HiddenField ID="hdnSelectedIDs" Value="" runat="server" />
                    <asp:ValidationSummary ID="vsSearch" ShowMessageBox="true" EnableClientScript="true"
                        HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                        ValidationGroup="Search" />
                </div>
                <%-- </ContentTemplate>
                    </asp:UpdatePanel>--%>
            </div>
        </div>
    </div>
    </div>
</asp:Content>
