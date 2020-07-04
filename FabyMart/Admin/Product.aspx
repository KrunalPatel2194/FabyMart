<%@ Page Title="Product List" Language="C#" MasterPageFile="~/Admin/Main.master"
    AutoEventWireup="true" CodeFile="Product.aspx.cs" Inherits="Product" %>

<%@ Register Src="UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<%@ Register Src="~/Admin/UserControls/ExportFile.ascx" TagName="ExportFile" TagPrefix="ExpFile" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Product List
                </div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="panel-search">
                                <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:DropDownList runat="server" ID="ddlSubCategory" CssClass="form-control" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlSubCategory_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:DropDownList runat="server" ID="ddlColor" CssClass="form-control" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlColor_SelectedIndexChanged">
                                </asp:DropDownList>
                                Select Criteria :
                                <asp:DropDownList runat="server" ID="ddlFields" CssClass="form-control" TabIndex="3">
                                    <asp:ListItem Text="--Select Option--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Product Name" Value="appProductName"></asp:ListItem>
                                    <asp:ListItem Text="Product Code" Value="appProductCode"></asp:ListItem>
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
                                        OnClientClick="return ConfirmMessage('Product','delete')" OnClick="btnDelete_Click" />
                                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-success" Text="Add New"
                                        TabIndex="6" OnClick="btnAdd_Click" />
                                    &nbsp;
                                    <asp:Button ID="btnExportExcel" runat="server" CssClass="btn btn-primary" Text="Export"
                                        OnClick="btnExportExcel_Click" />
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
                                        AllowSorting="true" DataKeyNames="appProductID,appIsActive" CssClass="table table-striped table-bordered table-hover"
                                        HeaderStyle-Wrap="false" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
                                        PagerSettings-Mode="NumericFirstLast" PageSize="10" OnPageIndexChanging="dgvGridView_PageIndexChanging"
                                        OnRowCreated="dgvGridView_RowCreated" OnRowDataBound="dgvGridView_RowDataBound"
                                        OnSorting="dgvGridView_Sorting" OnRowCommand="dgvGridView_RowCommand">
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
                                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <a href='<%# "ProductDetail.aspx?ID=" + objEncrypt.Encrypt(Eval("appProductID").ToString(), appFunctions.strKey) %>'
                                                        title="Edit"><span class="action-icon set-icon"><i class="fa fa-pencil"></i></span>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Images" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Image ID="img" runat="server" ImageUrl='<%#Eval("appNormalImage") %>' Width="32" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Color" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Image ID="img1" runat="server" ImageUrl='<%#Eval("appColorImage") %>' AlternateText='<%#Eval("appColorName") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Product Code" DataField="appProductCode" SortExpression="appProductCode" />
                                            <asp:BoundField HeaderText="Product Name" DataField="appProductName" SortExpression="appProductName" />
                                            <asp:TemplateField HeaderText="Is Active" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkIsActive" CommandName="IsActive" CommandArgument='<%#Eval("appProductID") %>'>
                                                        <span class="action-icon set-icon <%# Eval("appIsActive").ToString().ToLower() == "true" ? "green" : "red" %>"><i class="fa <%# Eval("appIsActive").ToString().ToLower() == "true" ? "fa-check" : "fa-ban" %>"></i></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <a href='<%# "ProductInformation.aspx?ID=" + objEncrypt.Encrypt(Eval("appProductID").ToString(), appFunctions.strKey) %>'
                                                        target="_blank"><span class="action-icon set-icon"><i class="fa fa-chevron-up"></i>
                                                        </span></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quantity Update" ItemStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="1%" HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ImageUrl="~/Admin/Images/editPrice.png" ID="btnImgQty" runat="server"
                                                        CommandName="QtyEdit" CommandArgument='<%#Eval("appProductID") %>' Width="28" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <asp:HiddenField ID="hdnSortDir" Value="Asc" runat="server" />
                                <asp:HiddenField ID="hdnSortCol" Value="" runat="server" />
                                <asp:HiddenField ID="hdnSelectedIDs" Value="" runat="server" />
                                <asp:ValidationSummary ID="vsSearch" ShowMessageBox="true" EnableClientScript="true"
                                    HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                                    ValidationGroup="Search" />
                            </div>
                            <input type="button" runat="server" id="btnProductQty" style="display: none;" />
                            <cc1:ModalPopupExtender ID="MPEProductQty" runat="server" TargetControlID="btnProductQty"
                                PopupControlID="divProductQty" BackgroundCssClass="modalbackground" DropShadow="false"
                                CancelControlID="imgBtnQtyClose">
                            </cc1:ModalPopupExtender>
                            <div id="divProductQty" class="modalpopup_ panel panel-default" runat="server" style="display: none;
                                width: 600px;">
                                <asp:LinkButton ID="imgBtnQtyClose" CssClass="modalbclose" runat="server">
                                        <span class="action-icon set-icon"><i class="fa fa-minus"></i></span>
                                </asp:LinkButton>
                                <div class="modalheader_ panel-heading">
                                    Product Quantity List
                                </div>
                                <div class="modaldetail">
                                    <div class="panel-search right-content">
                                        <asp:Button ID="btnUpdateQty" runat="server" CssClass="btn btn-primary" Text="Save"
                                            OnClick="btnUpdateQty_Click" />
                                    </div>
                                    <hr class="nomargin" />
                                    <div class="table-responsive" id="dgvProduct" runat="server">
                                        <asp:GridView ID="dgvProductQtyGridView" Width="100%" AllowSorting="false" runat="server"
                                            AllowPaging="false" AutoGenerateColumns="False" DataKeyNames="appProductDetailID"
                                            HeaderStyle-Wrap="false" CssClass="table table-striped table-bordered table-hover"
                                            PagerStyle-CssClass="pagination">
                                            <PagerSettings Position="Top" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Color" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                    HeaderStyle-Width="1%">
                                                    <ItemTemplate>
                                                        <div style="background-color: <%#Eval("appColorCode") %>; width: 30px; border: 1px solid #3b3b3b;">
                                                            &nbsp&nbsp
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="appSKUNo" HeaderText="SKU No" ItemStyle-Width="1%" HeaderStyle-Width="1%" />
                                                <asp:BoundField DataField="appSize" HeaderText="Size" ItemStyle-Width="1%" HeaderStyle-Width="1%" />
                                                <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="1%" HeaderStyle-Width="1%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtQuantity" Text='<%#Eval("appQuantity") %>' CssClass="form-control"
                                                            Width="100" runat="server" onkeypress="return isNumber(event)"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <asp:HiddenField ID="hdnPkId" Value="" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <ExpFile:ExportFile ID="ExpFile" runat="server" />
</asp:Content>
