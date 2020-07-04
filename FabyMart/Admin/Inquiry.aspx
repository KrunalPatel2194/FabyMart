<%@ Page Title="Inquiry List" Language="C#" MasterPageFile="~/Admin/Main.master"
    AutoEventWireup="true" CodeFile="Inquiry.aspx.cs" Inherits="Inquiry" %>

<%@ Register Src="UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Inquiry List
                </div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="panel-search">
                                Select Criteria :
                                <asp:DropDownList runat="server" ID="ddlFields" CssClass="form-control" TabIndex="3">
                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Name" Value="appName"></asp:ListItem>
                                    <asp:ListItem Text="Email" Value="appEmail"></asp:ListItem>
                                    <asp:ListItem Text="Mobile" Value="appMobile"></asp:ListItem>
                                    <asp:ListItem Text="Product Name" Value="tblProduct.appProductName"></asp:ListItem>
                                    <asp:ListItem Text="Product Code" Value="tblProduct.appProductCode"></asp:ListItem>
                                    <asp:ListItem Text="Seller Price" Value="tblProductDetail.appSellerPrice"></asp:ListItem>
                                    <asp:ListItem Text="MRP" Value="tblProductDetail.appMRP"></asp:ListItem>
                                    <asp:ListItem Text="Price" Value="tblProductDetail.appPrice"></asp:ListItem>
                                    <asp:ListItem Text="SKUNo" Value="tblProductDetail.appSKUNo"></asp:ListItem>
                                    <asp:ListItem Text="Quantity" Value=" tblProductDetail.appQuantity"></asp:ListItem>
                                    <asp:ListItem Text="Size" Value="appSize"></asp:ListItem>
                                    <asp:ListItem Text="Color" Value="appColorName"></asp:ListItem>
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
                            </div>
                            <hr class="nomargin" />
                            <div class="panel-search">
                                <div class="fleft">
                                    <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Delete"
                                        OnClientClick="return ConfirmMessage('Inquiry','delete')" OnClick="btnDelete_Click" />
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
                            <DInfo:DisplayInfo runat="server" ID="DInfo" />
                            <div class="panel-search">
                                <div class="table-responsive">
                                    <asp:GridView ID="dgvGridView" Width="100%" runat="server" AutoGenerateColumns="False"
                                        DataKeyNames="appInquiryID" CssClass="table table-striped table-bordered table-hover"
                                        HeaderStyle-Wrap="false" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
                                        PagerSettings-Mode="NumericFirstLast" PageSize="10" OnPageIndexChanging="dgvGridView_PageIndexChanging"
                                        OnRowCreated="dgvGridView_RowCreated" OnRowDataBound="dgvGridView_RowDataBound"
                                        OnSorting="dgvGridView_Sorting" onrowcommand="dgvGridView_RowCommand">
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
                                            <asp:BoundField DataField="appName" HeaderText="Name " SortExpression="appName" />
                                            <asp:BoundField DataField="appEmail" HeaderText="Email " SortExpression="appEmail" />
                                            <asp:BoundField DataField="appMobile" HeaderText="Mobile No " SortExpression="appMobile" />
                                            <asp:TemplateField HeaderText="Images" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Image ID="img" runat="server" ImageUrl='<%#Eval("appThumbImage") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
                                            <asp:BoundField HeaderText="Seller Price" DataField="appSellerPrice" SortExpression="appSellerPrice"
                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                            <asp:BoundField HeaderText="MRP" DataField="appMRP" SortExpression="appMRP" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-HorizontalAlign="Center" />
                                            <asp:BoundField HeaderText="Price" DataField="appPrice" SortExpression="appPrice"
                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                            <asp:BoundField HeaderText="Quantity" DataField="appQuantity" SortExpression="appQuantity"
                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                            <asp:BoundField HeaderText="Size" DataField="appSize" SortExpression="appSize" />
                                             <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkView" CommandName="View" CommandArgument='<%#Eval("appInquiryID") %>'
                                                        ToolTip="View">
                                                        <span class="action-icon set-icon"><i class="fa fa-chevron-up"></i></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <asp:HiddenField ID="hdnPKID" runat="server" Value="" />
                                <asp:HiddenField ID="hdnSelectedIDs" Value="" runat="server" />
                                <asp:ValidationSummary ID="vsSearch" ShowMessageBox="true" EnableClientScript="true"
                                    HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                                    ValidationGroup="Search" />
                                <input type="button" runat="server" id="btnView" style="display: none;" />
                                <cc1:ModalPopupExtender ID="mpeview" runat="server" TargetControlID="btnView" PopupControlID="divView"
                                    BackgroundCssClass="modalbackground" DropShadow="false" CancelControlID="imgBtnClose">
                                </cc1:ModalPopupExtender>
                                <div id="divView" class="modalpopup_ panel panel-default" runat="server" style="display: none;
                                    width: 600px;">
                                    <asp:LinkButton ID="imgBtnClose" CssClass="modalbclose" runat="server">
                                        <span class="action-icon set-icon"><i class="fa fa-minus"></i></span>
                                    </asp:LinkButton>
                                    <div class="modalheader_ panel-heading" id="divTitle" runat="server">
                                    </div>
                                    <div class="modaldetail">
                                        <div class="panel-search">
                                            <div class="table-responsive" style="height: 250px; overflow: auto;" id="divDescription"
                                                runat="server">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
