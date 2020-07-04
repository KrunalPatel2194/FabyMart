<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Main.master" AutoEventWireup="true"
    CodeFile="Dashboard.aspx.cs" Inherits="_Dashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Admin/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<%@ Register Src="~/Admin/UserControls/ProductInvoice.ascx" TagName="ProductInvoice"
    TagPrefix="MyInvoice" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <h2>
                Dashboard</h2>
            <h5>
                Welcome
                <asp:Label ID="lblUserName" runat="server"></asp:Label>
                , Love to see you back.
            </h5>
        </div>
    </div>
    <!-- /. ROW  -->
    <hr />
    <div class="row">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-6">
                        <div class="panel panel-back noti-box">
                            <span class="icon-box bg-color-red set-icon"><i class="fa fa-envelope-o"></i></span>
                            <div class="text-box">
                                <p class="main-text">
                                    <asp:Label runat="server" ID="lblOrdered" Text="0"></asp:Label>
                                    <!--New-->
                                </p>
                            </div>
                            <div class="text-muted fclear text-center">
                                New Ordered</div>
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-6 col-xs-6">
                        <div class="panel panel-back noti-box">
                            <span class="icon-box bg-color-green set-icon"><i class="fa fa-bars"></i></span>
                            <div class="text-box">
                                <p class="main-text">
                                    <asp:Label runat="server" ID="lblConfirmed" Text="0"></asp:Label>
                                    <!--Pending-->
                                </p>
                            </div>
                            <div class="text-muted fclear text-center">
                                Confirmed</div>
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-6 col-xs-6">
                        <div class="panel panel-back noti-box">
                            <span class="icon-box bg-color-blue set-icon"><i class="fa fa-bell-o"></i></span>
                            <div class="text-box">
                                <p class="main-text">
                                    <asp:Label ID="lblReadyToShip" runat="server"></asp:Label>
                                    <!--New-->
                                </p>
                            </div>
                            <div class="text-muted fclear text-center">
                                Ready To Ship
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-6 col-xs-6">
                        <div class="panel panel-back noti-box">
                            <span class="icon-box bg-color-brown set-icon"><i class="fa fa-trophy"></i></span>
                            <div class="text-box">
                                <p class="main-text">
                                    <asp:Label ID="lblShipped" runat="server"></asp:Label>
                                    <!--Orders-->
                                </p>
                            </div>
                            <div class="text-muted fclear text-center">
                                Shipped</div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-6">
                        <div class="panel panel-back noti-box">
                            <span class="icon-box bg-color-red set-icon"><i class="fa fa-envelope-o"></i></span>
                            <div class="text-box">
                                <p class="main-text" style="font-size: 27px !important;">
                                    Last Day
                                    <!--New-->
                                </p>
                            </div>
                            <div class="text-muted fclear text-center">
                                <asp:Label runat="server" ID="lblLastDayOrder" Text="0"></asp:Label>
                                /
                                <asp:Label runat="server" ID="lblLastDayOrderRupees" Text="0"></asp:Label></div>
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-6 col-xs-6">
                        <div class="panel panel-back noti-box">
                            <span class="icon-box bg-color-green set-icon"><i class="fa fa-bars"></i></span>
                            <div class="text-box">
                                <p class="main-text" style="font-size: 27px !important;">
                                    Last Week
                                    <!--Pending-->
                                </p>
                            </div>
                            <div class="text-muted fclear text-center">
                                <asp:Label runat="server" ID="lblLastWeekOrder" Text="0"></asp:Label>
                                /
                                <asp:Label runat="server" ID="lblLastWeekOrderRupees" Text="0"></asp:Label></div>
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-6 col-xs-6">
                        <div class="panel panel-back noti-box">
                            <span class="icon-box bg-color-blue set-icon"><i class="fa fa-bell-o"></i></span>
                            <div class="text-box">
                                <p class="main-text" style="font-size: 27px !important;">
                                    Last Month
                                    <!--New-->
                                </p>
                            </div>
                            <div class="text-muted fclear text-center">
                                <asp:Label runat="server" ID="lblLastMonthOrder" Text="0"></asp:Label>
                                /
                                <asp:Label runat="server" ID="lblLastMonthOrderRupees" Text="0"></asp:Label></div>
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-6 col-xs-6">
                        <div class="panel panel-back noti-box">
                            <span class="icon-box bg-color-brown set-icon"><i class="fa fa-trophy"></i></span>
                            <div class="text-box">
                                <p class="main-text" style="font-size: 27px !important;">
                                    Last Year
                                </p>
                            </div>
                            <div class="text-muted fclear text-center">
                                <asp:Label runat="server" ID="lblLastYearOrder" Text="0"></asp:Label>
                                /
                                <asp:Label runat="server" ID="lblLastYearOrderRupees" Text="0"></asp:Label></div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!-- /. ROW  -->
    <hr />
    <div class="row">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Today Order List
                        </div>
                        <div>
                            <div class="panel-search">
                                
                                Select Criteria :
                                <asp:DropDownList runat="server" ID="ddlFields" CssClass="form-control" TabIndex="3">
                                    <asp:ListItem Text="--Select Option--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Product" Value="appProductName"></asp:ListItem>
                                    <asp:ListItem Text="Qty" Value="appQty"></asp:ListItem>
                                    <asp:ListItem Text="Price" Value="appSellingPrice"></asp:ListItem>
                                    <asp:ListItem Text="Sku No" Value="appSKUNo"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvSearch" runat="server" ValidationGroup="Search"
                                    ErrorMessage="Search Text" Display="None" Font-Bold="True" SetFocusOnError="true"
                                    ControlToValidate="txtSearch" CssClass="ErrorLabelStyle" Text="*">
                                </asp:RequiredFieldValidator>
                                <asp:Button ID="btnSubOrderGO" runat="server" CssClass="btn btn-primary" Text="Search" ValidationGroup="Search"
                                    TabIndex="2" OnClick="btnSubOrderGO_Click" />
                                <asp:Button ID="btnSubOrderReset" runat="server" CssClass="btn btn-primary" Text="Reset"
                                    TabIndex="4" OnClick="btnSubOrderReset_Click" />
                            </div>
                            <hr class="nomargin" />
                            <div class="panel-search">
                                <div class="fleft">
                                    <asp:Button ID="btnSubOrderConfirmed" runat="server" CssClass="btn btn-primary" Text="Confirm"
                                        OnClick="btnSubOrderConfirmed_Click" />
                                    <asp:Button ID="btnSubOrderCancelled" runat="server" CssClass="btn btn-danger" Text="Cancel"
                                        OnClick="btnSubOrderCancelled_Click" />
                                </div>
                                <div class="fright">
                                    Total&nbsp;Records&nbsp;:&nbsp; <span class="RecordCount">
                                        <asp:Label ID="lblCount" runat="server" Text="0"> </asp:Label>
                                    </span>
                                    <div class="Separator">
                                        &nbsp;
                                    </div>
                                    Per Page :
                                    <asp:DropDownList ID="ddlSubOrderPerPage" runat="server" AutoPostBack="true" CssClass="form-control"
                                        OnSelectedIndexChanged="ddlSubOrderPerPage_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="fclear">
                                </div>
                            </div>
                            <hr class="nomargin" />
                            <DInfo:DisplayInfo runat="server" ID="DInfo" />
                            <div class="panel-search" style="height:300px;overflow:auto;">
                                <div class="table-responsive">
                                    <asp:GridView ID="dgvSubOrder" Width="100%" runat="server" AutoGenerateColumns="False"
                                        AllowSorting="true" DataKeyNames="appSubOrderID" CssClass="table table-striped table-bordered table-hover"
                                        HeaderStyle-Wrap="false" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
                                        PagerSettings-Mode="NumericFirstLast" PageSize="10" OnPageIndexChanging="dgvSubOrder_PageIndexChanging"
                                        OnRowCreated="dgvSubOrder_RowCreated" OnRowDataBound="dgvSubOrder_RowDataBound"
                                        OnSorting="dgvSubOrder_Sorting" OnRowCommand="dgvSubOrder_RowCommand">
                                        <PagerSettings Position="Top" />
                                        <PagerStyle CssClass="pagination" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkHeader" runat="server" onclick="SelectAll(this);" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelectRow" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Invoice" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkInvoice" CommandName="InvoiceShow" CommandArgument='<%#Eval("appSubOrderID") %>'
                                                        ToolTip="Up">
                                                        <span class="action-icon set-icon"><i class="fa fa-file-text"></i></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Images" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Image ID="img" runat="server" ImageUrl='<%#Eval("appThumbImage") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Order No" DataField="appSubOrderNo" SortExpression="appSubOrderNo"
                                                ItemStyle-Width="1%" HeaderStyle-Width="1%" />
                                            <asp:TemplateField HeaderText="Color" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Image ID="img1" runat="server" ImageUrl='<%#Eval("appColorImage") %>' AlternateText='<%#Eval("appColorName") %>'
                                                       />
                                                    <div>
                                                        <%#Eval("appColorName")%></div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="SKU No" DataField="appSKUNo" SortExpression="appSKUNo" />
                                            <asp:TemplateField HeaderText="Product Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="1%"
                                                HeaderStyle-Width="1%" SortExpression="appProductName">
                                                <ItemTemplate>
                                                    <a href='<%# GetAlias("ProductDetail.aspx") + generateUrl(Eval("appProductName").ToString()) +"/" + generateUrl(Eval("appColorLink ").ToString())%>'
                                                        target="_blank">
                                                        <%#Eval("appProductName")%></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Size" DataField="appSize" SortExpression="appSize" />
                                            <asp:BoundField HeaderText="Qty" DataField="appQty" SortExpression="appQty" />
                                            <asp:BoundField HeaderText="Price" DataField="appSellingPrice" SortExpression="appSellingPrice"
                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                            <asp:BoundField HeaderText="Total" DataField="appTotal" SortExpression="appTotal"
                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                            <asp:TemplateField HeaderText="Buyer Detail" ItemStyle-HorizontalAlign="Justify"
                                                ItemStyle-Width="1%" HeaderStyle-Width="1%" SortExpression="appSubOrderStatusID">
                                                <ItemTemplate>
                                                    <%#Eval("AppCustomerName")%>
                                                    <br />
                                                    <%#Eval("AppBillReceiverAddress")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                              
                                <asp:ValidationSummary ID="vsSearch" ShowMessageBox="true" EnableClientScript="true"
                                    HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                                    ValidationGroup="Search" />
                                <MyInvoice:ProductInvoice ID="UcInvoice" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <hr />
    <asp:Repeater runat="server" ID="rptDashBoardLinks">
        <ItemTemplate>
            <div style="float: left; margin: 10Px; padding: 10Px; border: 1px solid #eee; width: 122Px;
                text-align: center; background-color: #f8f8f8;">
                <a style="display: block;" href="<%# Eval("appWebPageName")%>">
                    <div style="width: 100Px; height: 100Px; text-align: center; overflow: hidden">
                        <img src="<%# Eval("appIconPath").ToString()=="" ? "images/NoImg.png" : Eval("appIconPath").ToString()%>"
                            style="max-width: 100Px;" />
                    </div>
                    <div style="line-height: 20Px; height: 40Px;">
                        <%#Eval("appTabName") %>
                    </div>
                </a>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <div style="clear: both;">
    </div>
    <%--<div class="row" id="DashBord" runat="server">
        <div class="col-md-1 col-sm-3 col-xs-3">
            sadsad
        </div>
        <div class="col-md-1 col-sm-3 col-xs-3">
            sadsad
        </div>
    </div>--%>
    <%--<hr />
    <div class="row">
        <div class="col-md-6 col-sm-12 col-xs-12">
            <div class="panel panel-back noti-box">
                <span class="icon-box bg-color-blue"><i class="fa fa-warning"></i></span>
                <div class="text-box">
                    <p class="main-text">
                        Important Issues to Fix
                    </p>
                    <p class="text-muted">
                        Please fix these issues as soon as possible</p>
                    <p class="text-muted">
                        Time Left: 4 hrs</p>
                    <hr />
                    <p class="text-muted">
                        <span class="text-muted color-bottom-txt"><i class="fa fa-edit"></i>This is sample text
                            to get user idea, how original text will look like.</span>
                    </p>
                </div>
            </div>
        </div>
        <div class="col-md-3 col-sm-12 col-xs-12">
            <div class="panel back-dash">
                <i class="fa fa-dashboard fa-3x"></i><strong>&nbsp; SPEED</strong>
                <p class="text-muted">
                    Lorem ipsum dolor sit amet, consectetur adipiscing sit ametsit amet elit ftr. Lorem
                    ipsum dolor sit amet, consectetur adipiscing elit.
                </p>
            </div>
        </div>
        <div class="col-md-3 col-sm-12 col-xs-12 ">
            <div class="panel ">
                <div class="main-temp-back">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-xs-6">
                                <i class="fa fa-cloud fa-3x"></i>Newyork City
                            </div>
                            <div class="col-xs-6">
                                <div class="text-temp">
                                    10°
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-back noti-box">
                <span class="icon-box bg-color-green set-icon"><i class="fa fa-desktop"></i></span>
                <div class="text-box">
                    <p class="main-text">
                        Display</p>
                    <p class="text-muted">
                        Looking Good</p>
                </div>
            </div>
        </div>
    </div>--%>
    <!-- /. ROW  -->
    <%-- <div class="row">
        <div class="col-md-9 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Lead Chart
                </div>
                <div class="panel-body">
                    <div id="morris-bar-chart">
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-3 col-sm-12 col-xs-12">
            <div class="panel panel-primary text-center no-boder bg-color-green">
                <div class="panel-body">
                    <i class="fa fa-bar-chart-o fa-5x"></i>
                    <h3>
                        205
                    </h3>
                </div>
                <div class="panel-footer back-footer-green">
                    Ordered
                </div>
            </div>
            <div class="panel panel-primary text-center no-boder bg-color-red">
                <div class="panel-body">
                    <i class="fa fa-edit fa-5x"></i>
                    <h3>
                        158
                    </h3>
                </div>
                <div class="panel-footer back-footer-red">
                    Lead Rejected
                </div>
            </div>
        </div>
        <!-- /. ROW  -->
        <hr />
        <div class="row">
            <div class="col-md-3 col-sm-12 col-xs-12">
                <div class="panel panel-primary text-center no-boder bg-color-green">
                    <div class="panel-body">
                        <i class="fa fa-comments-o fa-5x"></i>
                        <h4>
                            200 New Comments
                        </h4>
                        <h4>
                            See All Comments
                        </h4>
                    </div>
                    <div class="panel-footer back-footer-green">
                        <i class="fa fa-rocket fa-5x"></i>Lorem ipsum dolor sit amet sit sit, consectetur
                        adipiscing elitsit sit gthn ipsum dolor sit amet ipsum dolor sit amet
                    </div>
                </div>
            </div>
            <%--   <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Recent User's Login
                    </div>
                    <div class="panel-body">
                        <div class="table-responsive" style="height: 200px; margin: 0px;">
                            <asp:GridView runat="server" ID="dgvGridView" CssClass="table table-striped table-bordered table-hover"
                                AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="appFullName" HeaderText="Name" />
                                    <asp:BoundField DataField="appMobile" HeaderText="Mobile" />
                                    <asp:BoundField DataField="appEmail" HeaderText="Email" />
                                    <asp:BoundField DataField="appLastLoginTime" HeaderText="Last Login time" />
                                </Columns>
                            </asp:GridView>
                        </div>
                        <%--<div class="table-responsive">
                        <table class="table table-striped table-bordered table-hover">
                            <thead>
                                <tr>
                                    <th>
                                        #
                                    </th>
                                    <th>
                                        First Name
                                    </th>
                                    <th>
                                        Last Name
                                    </th>
                                    <th>
                                        Username
                                    </th>
                                    <th>
                                        Date & Time
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>
                                        1
                                    </td>
                                    <td>
                                        First Name 1
                                    </td>
                                    <td>
                                        Last Name 1
                                    </td>
                                    <td>
                                        User 1
                                    </td>
                                    <td>
                                        7/10/2014 11:30 AM
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        2
                                    </td>
                                    <td>
                                        First Name 2
                                    </td>
                                    <td>
                                        Last Name 2
                                    </td>
                                    <td>
                                        User 2
                                    </td>
                                    <td>
                                        7/10/2014 12:30 AM
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        3
                                    </td>
                                    <td>
                                        First Name 3
                                    </td>
                                    <td>
                                        Last Name 3
                                    </td>
                                    <td>
                                        User 3
                                    </td>
                                    <td>
                                        8/10/2014 11:30 AM
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        4
                                    </td>
                                    <td>
                                        First Name 4
                                    </td>
                                    <td>
                                        Last Name 4
                                    </td>
                                    <td>
                                        User 4
                                    </td>
                                    <td>
                                        8/10/2014 10:30 AM
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        5
                                    </td>
                                    <td>
                                        First Name 5
                                    </td>
                                    <td>
                                        Last Name 5
                                    </td>
                                    <td>
                                        User 5
                                    </td>
                                    <td>
                                        7/10/2014 11:30 AM
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        6
                                    </td>
                                    <td>
                                        First Name 6
                                    </td>
                                    <td>
                                        Last Name 6
                                    </td>
                                    <td>
                                        User 6
                                    </td>
                                    <td>
                                        6/10/2014 12:30 AM
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
    </div> </div> </div> </div>
    <!-- /. ROW -->
    <div class="row">
        <div class="col-md-6 col-sm-12
    col-xs-12">
            <div class="chat-panel panel panel-default chat-boder chat-panel-head">
                <div class="panel-heading">
                    <i class="fa fa-comments fa-fw"></i>Chat Box
                    <div class="btn-group
    pull-right">
                        <button type="button" class="btn btn-default btn-xs dropdown-toggle" data-toggle="dropdown">
                            <i class="fa fa-chevron-down"></i>
                        </button>
                        <ul class="dropdown-menu
    slidedown">
                            <li><a href="#"><i class="fa fa-refresh fa-fw"></i>Refresh </a></li>
                            <li><a href="#"><i class="fa fa-check-circle fa-fw"></i>Available </a></li>
                            <li><a href="#"><i class="fa fa-times fa-fw"></i>Busy </a></li>
                            <li><a href="#"><i class="fa
    fa-clock-o fa-fw"></i>Away </a></li>
                            <li class="divider"></li>
                            <li><a href="#"><i class="fa fa-sign-out fa-fw"></i>Sign Out </a></li>
                        </ul>
                    </div>
                </div>
                <div class="panel-body">
                    <ul class="chat-box">
                        <li class="left clearfix"><span class="chat-img pull-left">
                            <img src="assets/img/1.png" alt="User" class="img-circle" />
                        </span>
                            <div class="chat-body">
                                <strong>Jack Sparrow</strong> <small class="pull-right text-muted"><i class="fa
    fa-clock-o fa-fw"></i>12 mins ago </small>
                                <p>
                                    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur bibendum ornare
                                    dolor, quis ullamcorper ligula sodales.
                                </p>
                            </div>
                        </li>
                        <li class="right clearfix"><span class="chat-img pull-right">
                            <img src="assets/img/2.png" alt="User" class="img-circle" />
                        </span>
                            <div class="chat-body
    clearfix">
                                <small class=" text-muted"><i class="fa fa-clock-o fa-fw"></i>13 mins ago</small>
                                <strong class="pull-right">Jhonson Deed</strong>
                                <p>
                                    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur bibendum ornare
                                    dolor, quis ullamcorper ligula sodales.
                                </p>
                            </div>
                        </li>
                        <li class="left clearfix"><span class="chat-img
    pull-left">
                            <img src="assets/img/3.png" alt="User" class="img-circle" />
                        </span>
                            <div class="chat-body clearfix">
                                <strong>Jack Sparrow</strong> <small class="pull-right
    text-muted"><i class="fa fa-clock-o fa-fw"></i>14 mins ago</small>
                                <p>
                                    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur bibendum ornare
                                    dolor, quis ullamcorper ligula sodales.
                                </p>
                            </div>
                        </li>
                        <li class="right clearfix"><span class="chat-img
    pull-right">
                            <img src="assets/img/4.png" alt="User" class="img-circle" />
                        </span>
                            <div class="chat-body clearfix">
                                <small class=" text-muted"><i class="fa fa-clock-o
    fa-fw"></i>15 mins ago</small> <strong class="pull-right">Jhonson Deed</strong>
                                <p>
                                    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur bibendum ornare
                                    dolor, quis ullamcorper ligula sodales.
                                </p>
                            </div>
                        </li>
                        <li class="left
    clearfix"><span class="chat-img pull-left">
        <img src="assets/img/1.png" alt="User" class="img-circle" />
    </span>
                            <div class="chat-body">
                                <strong>Jack Sparrow</strong> <small class="pull-right text-muted"><i class="fa fa-clock-o fa-fw">
                                </i>12 mins ago </small>
                                <p>
                                    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur bibendum ornare
                                    dolor, quis ullamcorper ligula sodales.
                                </p>
                            </div>
                        </li>
                        <li class="right
    clearfix"><span class="chat-img pull-right">
        <img src="assets/img/2.png" alt="User" class="img-circle" />
    </span>
                            <div class="chat-body clearfix">
                                <small class=" text-muted"><i class="fa fa-clock-o fa-fw"></i>13 mins ago</small>
                                <strong class="pull-right">Jhonson Deed</strong>
                                <p>
                                    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur bibendum ornare
                                    dolor, quis ullamcorper ligula sodales.
                                </p>
                            </div>
                        </li>
                    </ul>
                </div>
                <div class="panel-footer">
                    <div class="input-group">
                        <input id="btn-input" type="text" class="form-control input-sm" placeholder="Type your message to send..." />
                        <span class="input-group-btn">
                            <button class="btn btn-warning btn-sm" id="btn-chat">
                                Send
                            </button>
                        </span>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-6 col-sm-12
    col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Label Examples
                </div>
                <div class="panel-body">
                    <span class="label label-default">Default</span> <span class="label label-primary">Primary</span>
                    <span class="label label-success">Success</span> <span class="label label-info">Info</span>
                    <span class="label label-warning">Warning</span> <span class="label label-danger">Danger</span>
                </div>
            </div>
            <div class="panel panel-default">
                <div class="panel-heading">
                    Donut Chart Example
                </div>
                <div class="panel-body">
                    <div id="morris-donut-chart">
                    </div>
                </div>
            </div>
        </div>
    </div>
    --%>
    <!-- /. ROW -->
      <asp:HiddenField ID="hdnSelectedIDs" Value="" runat="server" />
</asp:Content>
