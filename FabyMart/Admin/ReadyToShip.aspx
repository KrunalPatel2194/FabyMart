﻿<%@ Page Title="Ready To Ship Order List" Language="C#" MasterPageFile="~/Admin/Main.master"
    AutoEventWireup="true" CodeFile="ReadyToShip.aspx.cs" Inherits="ReadyToShip" %>

<%@ Register Src="UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<%@ Register Src="~/Admin/UserControls/OrderStatus.ascx" TagName="OrderStatus" TagPrefix="MyOrder" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function Manifest() {
            if ($("#<%= ddlCourierCompany.ClientID%>").val() == 0) {
                alert("Please select courier company");
                return false;
            }
            var arIDs = document.getElementById("<%=hdnSelectedIDs.ClientID%>").value;
            if (arIDs != null && arIDs != "") {
                var strval = 'ProductMainfest.aspx?ID=' + arIDs
                wopen(strval, 'Mainfest List', 1000, 500);
                document.getElementById("<%=hdnSelectedIDs.ClientID%>").value = "";
                return false;
            }
            else {
                alert("Select any product.");
            }
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Ready To Ship Order List
                </div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="panel-search">
                                <MyOrder:OrderStatus ID="UcOrderStratus" runat="server" />
                            </div>
                            <div class="panel-search">
                                Select Criteria :
                                <asp:DropDownList runat="server" ID="ddlFields" CssClass="form-control" TabIndex="3">
                                    <asp:ListItem Text="--Select Option--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Product" Value="appProductName"></asp:ListItem>
                                    <asp:ListItem Text="Qty" Value="appQty"></asp:ListItem>
                                    <asp:ListItem Text="Price" Value="appSellingPrice"></asp:ListItem>
                                    <asp:ListItem Text="Sku No" Value="appSKUNo"></asp:ListItem>
                                    <asp:ListItem Text="Customer Name" Value="appCustomerName"></asp:ListItem>
                                    <asp:ListItem Text="Order No." Value="appOrderNo"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvSearch" runat="server" ValidationGroup="Search"
                                    ErrorMessage="Search Text" Display="None" Font-Bold="True" SetFocusOnError="true"
                                    ControlToValidate="txtSearch" CssClass="ErrorLabelStyle" Text="*">
                                </asp:RequiredFieldValidator>
                                <asp:Button ID="btnGO" runat="server" CssClass="btn btn-primary" Text="Search" ValidationGroup="Search"
                                    TabIndex="2" OnClick="btnGO_Click" />
                                <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary" Text="Reset"
                                    TabIndex="4" OnClick="btnReset_Click" />
                                <div class="fright">
                                    <asp:Button ID="btnMenifestList" runat="server" CssClass="btn btn-primary" Text="Menifest List"
                                        TabIndex="5" OnClick="btnMenifestList_Click" />
                                </div>
                            </div>
                            <hr class="nomargin" />
                            <div class="panel-search">
                                <div class="fleft">
                                   <asp:DropDownList ID="ddlCourierCompany" runat="server" CssClass="form-control" AutoPostBack="true"
                                        onselectedindexchanged="ddlCourierCompany_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:Button ID="btnMainFest" runat="server" CssClass="btn btn-primary" Text="1. Manifest"
                                        OnClientClick="Manifest();" />&nbsp;
                                    
                                    <asp:Button ID="btnShipped" runat="server" CssClass="btn btn-primary" Text="2. Shipped"
                                        OnClick="btnShipped_Click" />
                                    &nbsp;
                                    <%-- <asp:DropDownList runat="server" ID="ddlCourierComapny" CssClass="form-control" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlCourierComapny_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    &nbsp;--%>
                                </div>
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
                            <hr class="nomargin" />
                            <div class="panel-search">
                                <asp:TextBox ID="txtMenifestNo" runat="server" CssClass="form-control" placeholder="Enter Menifest No"
                                    Width="140"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFVMenifestNo" runat="server" ValidationGroup="Upload"
                                    ErrorMessage="Enter Menifest No." Display="None" Font-Bold="True" SetFocusOnError="true"
                                    ControlToValidate="txtMenifestNo" CssClass="ErrorLabelStyle" Text="*">
                                </asp:RequiredFieldValidator>
                                <asp:FileUpload ID="FileUploadMenifest" runat="server" Style="display: inline-block;" />
                                <asp:Button ID="btnUploadMenifest" runat="server" CssClass="btn btn-primary" ValidationGroup="Upload"
                                    Text="Upload Menifest" TabIndex="4" OnClick="btnUploadMenifest_Click" />
                                <asp:Button ID="btnMenifestReset" runat="server" CssClass="btn btn-primary" Text="Reset"
                                    TabIndex="4" OnClick="btnMenifestReset_Click" />
                            </div>
                            <hr class="nomargin" />
                            <DInfo:DisplayInfo runat="server" ID="DInfo" />
                            <div class="panel-search">
                                <div class="table-responsive">
                                    <asp:GridView ID="dgvGridView" Width="100%" runat="server" AutoGenerateColumns="False"
                                        AllowSorting="true" DataKeyNames="appOrderID,appPaymentMode,appManifestGenerated"
                                        CssClass="table table-striped table-bordered table-hover" HeaderStyle-Wrap="false"
                                        PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last" PagerSettings-Mode="NumericFirstLast"
                                        PageSize="10" OnPageIndexChanging="dgvGridView_PageIndexChanging" OnRowCreated="dgvGridView_RowCreated"
                                        OnRowDataBound="dgvGridView_RowDataBound" OnSorting="dgvGridView_Sorting" OnRowCommand="dgvGridView_RowCommand">
                                        <PagerSettings Position="Top" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                                <HeaderTemplate>
                                                    <label class="tasks-list-item">
                                                        <input type="checkbox" id="chkHeader" runat="server" class="tasks-list-cb" onclick="SelectAll(this);">
                                                        <span class="tasks-list-mark"></span>
                                                    </label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <label class="tasks-list-item">
                                                        <input type="checkbox" id="chkSelectRow" runat="server" class="tasks-list-cb">
                                                        <span class="tasks-list-mark"></span>
                                                    </label>
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
                                                            DataKeyNames="appSubOrderID" CssClass="table table-bordered" HeaderStyle-Wrap="false"
                                                            AllowPaging="false" AllowSorting="false" OnRowCommand="dgvSubDetail_RowCommand">
                                                            <PagerSettings Position="Top" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                                    HeaderStyle-Width="1%">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkbtnCancel" runat="server" CommandName="CancelOrder" ToolTip="Cancel order"
                                                                            OnClientClick="return confirm('Do you really want to Cancel Order?');" CommandArgument='<%#Eval("appSubOrderID") %>'>
                                                                            <span class="action-icon set-icon red"><i class="fa fa-trash-o"></i></span>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Image" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                                    HeaderStyle-Width="1%">
                                                                    <ItemTemplate>
                                                                        <asp:Image ID="img" runat="server" ImageUrl='<%#Eval("appThumbImage") %>' AlternateText='<%#Eval("appProductName") %>'
                                                                            Width="100" />
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
                                                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                                                    <ItemTemplate>
                                                                        <div class="divGrid">
                                                                            <div class="payLeft">
                                                                                <%# Eval("appSellingPrice")%>
                                                                                <span class="spanBalck">x </span>
                                                                                <%# Eval("appQty")%>
                                                                                :
                                                                            </div>
                                                                            <div style="clear: both">
                                                                            </div>
                                                                        </div>
                                                                        <div class="divGrid">
                                                                            <div class="payLeft">
                                                                                Discount :
                                                                            </div>
                                                                            <div style="clear: both">
                                                                            </div>
                                                                        </div>
                                                                        <hr class="hrPay" />
                                                                        <div class="divGrid">
                                                                            <div class="payLeft">
                                                                                Amount :
                                                                            </div>
                                                                            <div style="clear: both">
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Price" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                                                    ItemStyle-Width="15%" HeaderStyle-Width="15%">
                                                                    <ItemTemplate>
                                                                        <div class="divGrid">
                                                                            <div class="payritght" style="color: <%# Convert.ToDecimal( Eval("appSellingPrice").ToString()) >= 0 ? "green" : "red" %>">
                                                                                <%# Eval("PriceMulQty")%>
                                                                            </div>
                                                                            <div style="clear: both">
                                                                            </div>
                                                                        </div>
                                                                        <div class="divGrid">
                                                                            <div class="payritght" style="color: <%# Convert.ToDecimal( Eval("appSellingPrice").ToString()) >= 0 ? "red" : "green" %>">
                                                                                -<%# Eval("appTotalDiscount")%></div>
                                                                            <div style="clear: both">
                                                                            </div>
                                                                        </div>
                                                                        <hr class="hrPay" />
                                                                        <div class="divGrid">
                                                                            <div class="payritght" style="color: <%# Convert.ToDecimal( Eval("appSellingPrice").ToString()) >= 0 ? "green" : "red" %>">
                                                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("appTotalamountToBePaid")%>'></asp:Label>
                                                                            </div>
                                                                            <div style="clear: both">
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="Quantity" DataField="appQty" SortExpression="appQty"
                                                                    ItemStyle-Width="1%" HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="Center" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Total Amount" DataField="appOrderAmount" SortExpression="appOrderAmount"
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
                                            <%--   <asp:TemplateField HeaderText="Courier Company" ItemStyle-HorizontalAlign="Justify"
                                                ItemStyle-Width="1%" HeaderStyle-Width="1%" SortExpression="appCourierCompany">
                                                <ItemTemplate>
                                                    <div class="divGrid divPriceValuewithoutPadding">
                                                        Courier Company
                                                    </div>
                                                    <div class="divGrid ">
                                                        <%#Eval("appCourierCompany") %>
                                                    </div>
                                                    <div class="divGrid divPriceValuewithoutPadding">
                                                        Docket No :
                                                    </div>
                                                    <div class="divGrid ">
                                                        <%#Eval("appDocketNo") %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="1%"
                                                HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="divGrid divPriceValuewithoutPadding">
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("appOrderStatus") %>'></asp:Label>
                                                    </div>
                                                    <div class="divGrid divPriceValuewithoutPadding" align="center" style="margin-top: 5px;">
                                                        <asp:Label ID="type" runat="server" Font-Bold="true" Font-Size="10" Style="padding: 4px;"></asp:Label>
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
                                <asp:HiddenField ID="hdnCourier" Value="" runat="server" />
                                <asp:ValidationSummary ID="vsSearch" ShowMessageBox="true" EnableClientScript="true"
                                    HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                                    ValidationGroup="Search" />
                                <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" EnableClientScript="true"
                                    HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                                    ValidationGroup="Courier" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnUploadMenifest" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
