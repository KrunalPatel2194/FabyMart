<%@ Page Title="AdminPendingReurnList" Language="C#" MasterPageFile="~/admin/Main.master"
    AutoEventWireup="true" CodeFile="ReturnReport.aspx.cs" Inherits="AdminPendingReurnList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Return Report
                </div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
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
                            <hr class="nomargin" />
                            <DInfo:DisplayInfo runat="server" ID="DInfo" />
                            <div class="panel-search">
                                <div class="table-responsive">
                                    <asp:GridView ID="OuterGrid" Width="100%" runat="server" AutoGenerateColumns="False"
                                        AllowSorting="true" DataKeyNames="appCustomerID" CssClass="table table-striped table-bordered table-hover"
                                        HeaderStyle-Wrap="false" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
                                        PagerSettings-Mode="NumericFirstLast" PageSize="10" OnPageIndexChanging="OuterGrid_PageIndexChanging"
                                        OnRowCreated="OuterGrid_RowCreated" OnRowDataBound="OuterGrid_RowDataBound" OnSorting="OuterGrid_Sorting">
                                        <PagerSettings Position="Top" />
                                        <PagerStyle CssClass="pagination" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Info" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%"
                                                HeaderStyle-Width="30%">
                                                <ItemTemplate>
                                                    <div style="margin-top: 5px;">
                                                        <asp:GridView ID="dgvSubDetail" Width="100%" runat="server" AutoGenerateColumns="False"
                                                            ShowHeader="true" DataKeyNames="appProductID" CssClass="table table-bordered"
                                                            HeaderStyle-Wrap="false" AllowPaging="false" AllowSorting="false">
                                                            <PagerSettings Position="Top" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Order" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="4%"
                                                                    HeaderStyle-Width="4%">
                                                                    <ItemTemplate>
                                                                        <div class="divGrid divPriceValuewithoutPadding">
                                                                            No :
                                                                        </div>
                                                                        <div class="divGrid ">
                                                                            <asp:Label ID="lblOrderNo" runat="server" Text='<%#Eval("appSubOrderNo") %>'></asp:Label>
                                                                        </div>
                                                                        <div class="divGrid divPriceValuewithoutPadding">
                                                                            Date :
                                                                        </div>
                                                                        <div class="divGrid ">
                                                                            <asp:Label ID="lblOrderDate" runat="server" Text='<%#Eval("appCompletedDate") %>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Image" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                                    HeaderStyle-Width="1%">
                                                                    <ItemTemplate>
                                                                        <asp:Image ID="img" runat="server" ImageUrl='<%#Eval("appThumbImage") %>' AlternateText='<%#Eval("appProductName") %>'
                                                                            Width="100" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Product Info" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%"
                                                                    HeaderStyle-Width="30%">
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
                                                                <asp:TemplateField HeaderText="Price" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="20%"
                                                                    HeaderStyle-Width="20%">
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
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
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
                                            <asp:TemplateField HeaderText="Customer Detail" ItemStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="5%" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <div class="divGrid divPriceValuewithoutPadding">
                                                        Name :
                                                    </div>
                                                    <div class="divGrid ">
                                                        <asp:Label ID="lblCustomerName" runat="server" Text='<%#Eval("appPickupName") %>'></asp:Label>
                                                    </div>
                                                    <div class="divGrid divPriceValuewithoutPadding">
                                                        Address :
                                                    </div>
                                                    <div class="divGrid ">
                                                        <asp:Label ID="lblAddress" runat="server" Text='<%#Eval("appPickupAddress") %>'></asp:Label>
                                                    </div>
                                                    <div class="divGrid divPriceValuewithoutPadding">
                                                        Mobile No. :
                                                    </div>
                                                    <div class="divGrid ">
                                                        <asp:Label ID="lblMobile" runat="server" Text='<%#Eval("appPickupContactNo1") %>'></asp:Label>
                                                    </div>
                                                    <div class="divGrid divPriceValuewithoutPadding">
                                                        PIN Code. :
                                                    </div>
                                                    <div class="divGrid ">
                                                        <asp:Label ID="lblPincode" runat="server" Text='<%#Eval("appPickupPIN") %>'></asp:Label>
                                                    </div>
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
