<%@ Page Title="Quantity Wise Product Listing" Language="C#" MasterPageFile="~/Admin/Main.master"
    AutoEventWireup="true" CodeFile="ReportQtyWiseProduct.aspx.cs" Inherits="ReportQtyWiseProduct" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<%@ Register Src="~/Admin/UserControls/ExportFile.ascx" TagName="ExportFile" TagPrefix="ExpFile" %>
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
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Quantity Wise Product Listing
                        </div>
                        <div style="margin-top: 10px;">
                            <div class="panel-search">
                                <div class="fleft">
                                    Enter Quantity :
                                    <asp:TextBox ID="txtQuantity" CssClass="form-control" Width="100" runat="server"
                                        onkeypress="return isNumber(event)"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RFVQty" runat="server" ErrorMessage="Enter Quantity"
                                        ValidationGroup="Search" Text="*" ControlToValidate="txtQuantity" SetFocusOnError="true"
                                        CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator Display="Dynamic" ID="REVQuantity" runat="server"
                                        ValidationGroup="Search" Text="*" ControlToValidate="txtQuantity" SetFocusOnError="true"
                                        CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                    <asp:Button ID="btnGO" runat="server" CssClass="btn btn-primary" Text="Search" ValidationGroup="Search"
                                        TabIndex="2" OnClick="btnGO_Click" />
                                    <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary" Text="Reset"
                                        TabIndex="4" OnClick="btnReset_Click" />
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
                        </div>
                        <DInfo:DisplayInfo runat="server" ID="DInfo" />
                        <div class="panel-search">
                            <div class="table-responsive">
                                <asp:GridView ID="dgvGridView" Width="100%" runat="server" AutoGenerateColumns="False"
                                    AllowSorting="true" DataKeyNames="appProductDetailID" CssClass="table table-striped table-bordered table-hover"
                                    HeaderStyle-Wrap="false" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
                                    PagerSettings-Mode="NumericFirstLast" PageSize="10" OnPageIndexChanging="dgvGridView_PageIndexChanging"
                                    OnSorting="dgvGridView_Sorting">
                                    <PagerSettings Position="Top" />
                                    <PagerStyle CssClass="pagination" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Images" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                            HeaderStyle-Width="1%">
                                            <ItemTemplate>
                                                <asp:Image ID="img" runat="server" ImageUrl='<%#Eval("appNormalImage") %>' Width="32" />
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
                                        
                                        <asp:BoundField HeaderText="Product Code" DataField="appProductCode" SortExpression="appProductCode" />
                                        <asp:BoundField HeaderText="Product Name" DataField="appProductName" SortExpression="appProductName" />
                                        <asp:BoundField DataField="appSKUNo" HeaderText="SKU No" SortExpression="appSKUNo"
                                            ItemStyle-Width="1%" HeaderStyle-Width="1%" />
                                        <asp:BoundField DataField="appSize" HeaderText="Size" SortExpression="appSize" ItemStyle-Width="1%"
                                            HeaderStyle-Width="1%" />
                                        <asp:BoundField DataField="appQuantity" HeaderText="Quantity" SortExpression="appQuantity"
                                            ItemStyle-Width="1%" HeaderStyle-Width="1%" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <asp:ValidationSummary ID="vsSearch" ShowMessageBox="true" EnableClientScript="true"
                                HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                                ValidationGroup="Search" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <ExpFile:ExportFile ID="ExpFile" runat="server" />
</asp:Content>
