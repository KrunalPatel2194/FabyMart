<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Main.master" AutoEventWireup="true"
    CodeFile="ProductInformation.aspx.cs" Inherits="Admin_ProductInformation" %>

<%@ Register Src="~/Admin/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .panel-search
        {
            padding: 2px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Product Inforamtion
                </div>
                <div>
                    <div class="panel-search right-content">
                        <asp:Button ID="btnBack" runat="server" CssClass="btn btn-primary" Text="Back" OnClick="btnBack_Click"
                            TabIndex="8" />&nbsp;
                    </div>
                    <hr class="nomargin" />
                    <DInfo:DisplayInfo runat="server" ID="DInfo" />
                    <div class="panel-search">
                        <div class="table-responsive">
                            <div class="entryformmain">
                                <div style="float: left; width: 50%">
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            Product Code :
                                        </div>
                                        <div class="lblcontrolstyle">
                                            <asp:Label ID="lblProductCode" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            Product Name :
                                        </div>
                                        <div class="lblcontrolstyle">
                                            <asp:Label ID="lblProductName" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            Product Tag :
                                        </div>
                                        <div class="lblcontrolstyle">
                                            <asp:Label ID="lblProductTag" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            Description :
                                        </div>
                                        <div class="lblcontrolstyle">
                                            <div id="divDescription" runat="server">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div style="float: right; width: 50%">
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            Is Color :
                                        </div>
                                        <div class="lblcontrolstyle">
                                            <asp:Label ID="lblIsColor" runat="server" Text="No"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            Is Size :
                                        </div>
                                        <div class="lblcontrolstyle">
                                            <asp:Label ID="lblIsSize" runat="server" Text="No"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            Meta KeyWord :
                                        </div>
                                        <div class="lblcontrolstyle">
                                            <div id="divMetaKeyWord" runat="server">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            Meta Description :
                                        </div>
                                        <div class="lblcontrolstyle">
                                            <div id="divMetaDescription" runat="server">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div style="clear: both">
                                </div>
                            </div>
                        </div>
                        <div class="panel-search" id="divColor" runat="server">
                            <hr class="nomargin" />
                            <div class="page-inner-title">
                                Color List
                            </div>
                            <div class="ProductTable">
                                <asp:GridView ID="dgvColor" Width="100%" runat="server" AutoGenerateColumns="False"
                                    ShowHeader="false" AllowSorting="false" DataKeyNames="appProductColorID" HeaderStyle-Wrap="false"
                                    AllowPaging="false" OnRowDataBound="dgvColor_RowDataBound" BorderWidth="0" AlternatingRowStyle-BorderWidth="0"
                                    RowStyle-BorderWidth="0">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Color Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="1%"
                                            HeaderStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="page-inner-title">
                                                    <div style="float: left; width: 10%">
                                                        <div style="background-color: <%#Eval("appColorCode") %>; width: 30px;">&nbsp&nbsp
                                                            &nbsp&nbsp </div><div><%# Eval("appColorName")%></div></div>
                                                    <div style="float: right; width: 87%; text-align: left; margin-left: 5px;">
                                                        <div style="float: left; width: 5%">
                                                            <span class="action-icon set-icon <%# Eval("appIsDefault").ToString().ToLower() == "true" ? "green" : "red" %>">
                                                                <i class="fa <%# Eval("appIsDefault").ToString().ToLower() == "true" ? "fa-check" : "fa-ban" %>">
                                                                </i></span>
                                                        </div>
                                                        <div style="float: right; width: 94%; text-align: left; margin-left: 5px;">
                                                            <asp:Repeater ID="RepImg" runat="server">
                                                                <ItemTemplate>
                                                                    <div style="float: left; margin-right: 3px;">
                                                                        <asp:Image ID="img" runat="server" ImageUrl='<%#Eval("appNormalImage") %>' Height="64" />
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                            <div style="clear: both;">
                                                            </div>
                                                        </div>
                                                        <div style="clear: both;">
                                                        </div>
                                                    </div>
                                                    <div style="clear: both;">
                                                    </div>
                                                </div>
                                                <div class="ProductTable" id="divSize" runat="server">
                                                    <asp:GridView ID="dgvSize" Width="100%" runat="server" AutoGenerateColumns="False"
                                                        AllowSorting="false" DataKeyNames="appProductDetailID,appIsDefault" CssClass="table table-striped table-bordered table-hover"
                                                        HeaderStyle-Wrap="false" AllowPaging="false">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Seller Price" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%"
                                                                HeaderStyle-Width="40%" SortExpression="appSellerPrice">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSellerPrice" runat="server" Text='<%#Eval("appSellerPrice") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MRP" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%"
                                                                HeaderStyle-Width="40%" SortExpression="appMRP">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMRP" runat="server" Text='<%#Eval("appMRP") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Price" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%"
                                                                HeaderStyle-Width="40%" SortExpression="appPrice">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPrice" runat="server" Text='<%#Eval("appPrice") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Quantity" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%"
                                                                HeaderStyle-Width="40%" SortExpression="appQuantity">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblQuantity" runat="server" Text='<%#Eval("appQuantity") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="SKUNo" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%"
                                                                HeaderStyle-Width="40%" SortExpression="appSKUNo">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSKUNo" runat="server" Text='<%#Eval("appSKUNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%"
                                                                HeaderStyle-Width="40%" SortExpression="appSize">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSize" runat="server" Text='<%#Eval("appSize") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Is Default" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                                HeaderStyle-Width="1%">
                                                                <ItemTemplate>
                                                                    <span class="action-icon set-icon <%# Eval("appIsDefault").ToString().ToLower() == "true" ? "green" : "red" %>">
                                                                        <i class="fa <%# Eval("appIsDefault").ToString().ToLower() == "true" ? "fa-check" : "fa-ban" %>">
                                                                        </i></span>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="panel-search" id="divCategory" runat="server">
                            <hr class="nomargin" />
                            <div class="page-inner-title">
                                Category List
                            </div>
                            <div class="ProductTable">
                                <asp:GridView ID="dgvCategory" Width="100%" runat="server" AutoGenerateColumns="False"
                                    ShowHeader="false" AllowSorting="false" DataKeyNames="appCategoryID" HeaderStyle-Wrap="false"
                                    AllowPaging="false" OnRowDataBound="dgvCategory_RowDataBound" BorderWidth="0"
                                    AlternatingRowStyle-BorderWidth="0" RowStyle-BorderWidth="0">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Category Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="1%"
                                            HeaderStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="page-inner-title">
                                                    <%# Eval("appCategory")%>&nbsp;<asp:Label ID="lblCategory" runat="server"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="panel-search" id="divProperty" runat="server">
                            <hr class="nomargin" />
                            <div class="page-inner-title">
                                Property List
                            </div>
                            <div class="ProductTable">
                                <asp:GridView ID="dgvProperty" Width="100%" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
                                    ShowHeader="false" AllowSorting="false" DataKeyNames="appPropertyID,appIsPredefine,appPropertyPreValueID"
                                    HeaderStyle-Wrap="false" AllowPaging="false" OnRowDataBound="dgvProperty_RowDataBound"
                                    BorderWidth="0" AlternatingRowStyle-BorderWidth="0" RowStyle-BorderWidth="0">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Property" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="30%"
                                            HeaderStyle-Width="30%">
                                            <ItemTemplate>
                                                
                                                <asp:Label ID="lblProperty" runat="server" Text='<%#Eval("appPropertyName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="value" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="70%"
                                            HeaderStyle-Width="70%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblValue" runat="server" Text='<%#Eval("appValue") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="panel-search" id="divRelatedProduct" runat="server">
                            <hr class="nomargin" />
                            <div class="page-inner-title">
                                Related Product List
                            </div>
                            <div class="ProductTable">
                                <asp:GridView ID="dgvRelatedProduct" Width="100%" runat="server" AutoGenerateColumns="False"
                                    AllowSorting="false" DataKeyNames="appRelatedProductID,appIsActive" CssClass="table table-striped table-bordered table-hover"
                                    HeaderStyle-Wrap="false" AllowPaging="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Images" ItemStyle-HorizontalAlign="left" ItemStyle-Width="1%"
                                            HeaderStyle-Width="1%">
                                            <ItemTemplate>
                                                <asp:Image ID="img" runat="server" ImageUrl='<%#Eval("appNormalImage") %>' Width="32" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Product" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60%"
                                            HeaderStyle-Width="60%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProductName" runat="server" Text='<%#Eval("appProductName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Is Active" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                            HeaderStyle-Width="1%">
                                            <ItemTemplate>
                                                <span class="action-icon set-icon <%# Eval("appIsActive").ToString().ToLower() == "true" ? "green" : "red" %>">
                                                    <i class="fa <%# Eval("appIsActive").ToString().ToLower() == "true" ? "fa-check" : "fa-ban" %>">
                                                    </i></span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div>
                <asp:HiddenField ID="hdnPKID" Value="" runat="server" />
                <asp:ValidationSummary ID="vsSearch" ShowMessageBox="true" EnableClientScript="true"
                    HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                    ValidationGroup="save" />
            </div>
        </div>
    </div>
</asp:Content>
