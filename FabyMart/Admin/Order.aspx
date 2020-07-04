<%@ Page Title="Product List" Language="C#" MasterPageFile="~/Admin/Main.master"
    AutoEventWireup="true" CodeFile="Order.aspx.cs" Inherits="Order" %>

<%@ Register Src="UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
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
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="panel-search">
                                Select Criteria :
                                <asp:DropDownList runat="server" ID="ddlFields" CssClass="form-control" TabIndex="3">
                                    <asp:ListItem Text="--select Option--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Receiver Name" Value="appReceiverName"></asp:ListItem>
                                    <asp:ListItem Text="Receiver Mobile 1" Value="appReceiverContactNo1"></asp:ListItem>
                                   <%-- <asp:ListItem Text="Receiver Mobile 2" Value="appReceiverContactNo2"></asp:ListItem>
                                    <asp:ListItem Text="Receiver Prefered Time" Value="appPreferedTime"></asp:ListItem>--%>
                                    <asp:ListItem Text="Receiver Email" Value="appRecevierEmail"></asp:ListItem>
                                    <asp:ListItem Text="Bill Receiver Name" Value="appBillReceiverName"></asp:ListItem>
                                    <%--<asp:ListItem Text="Bill Receiver Mobile 1" Value="appBillReceiverContactNo1"></asp:ListItem>
                                    <asp:ListItem Text="Bill Receiver Mobile 2" Value="appBillReceiverContactNo2"></asp:ListItem>
                                    <asp:ListItem Text="Bill Receiver Email" Value="appBillRecevierEmail"></asp:ListItem>--%>
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
                                        OnClientClick="return ConfirmMessage('Order','delete')" OnClick="btnDelete_Click" />
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
                                        AllowSorting="true" DataKeyNames="appOrderID" CssClass="table table-striped table-bordered table-hover"
                                        HeaderStyle-Wrap="false" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
                                        PagerSettings-Mode="NumericFirstLast" PageSize="10" OnPageIndexChanging="dgvGridView_PageIndexChanging"
                                        OnRowCreated="dgvGridView_RowCreated" OnRowDataBound="dgvGridView_RowDataBound"
                                        OnSorting="dgvGridView_Sorting" OnRowCommand="dgvGridView_RowCommand">
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
                                            <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <a href='<%# "OrderDetail.aspx?ID=" + objEncrypt.Encrypt(Eval("appOrderID").ToString(), appFunctions.strKey) %>'
                                                        title="Edit"><span class="action-icon set-icon"><i class="fa fa-pencil"></i></span>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Order No" DataField="appOrderNo" SortExpression="appOrderNo" />
                                            <asp:BoundField HeaderText="Receiver Name" DataField="appReceiverName" SortExpression="appReceiverName" />
                                            <asp:BoundField HeaderText="Receiver Mobile 1" DataField="appReceiverContactNo1"
                                                SortExpression="appReceiverContactNo1" />
                                          <%--  <asp:BoundField HeaderText="Receiver Mobile 2" DataField="appReceiverContactNo2"
                                                SortExpression="appReceiverContactNo2" />--%>
                                            <asp:BoundField HeaderText="Receiver Email" DataField="appRecevierEmail" SortExpression="appRecevierEmail" />
                                           <%-- <asp:BoundField HeaderText="Receiver Prefered Time" DataField="appPreferedTime" SortExpression="appPreferedTime" />--%>
                                            <asp:BoundField HeaderText="Customer Name" DataField="AppCustomerName" SortExpression="AppCustomerName" />
                                               <asp:BoundField HeaderText="Total Amount" DataField="appOrderAmount" SortExpression="appOrderAmount" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                                            <asp:BoundField HeaderText="Bill Receiver Name" DataField="appBillReceiverName" SortExpression="appBillReceiverName" />
                                             <asp:BoundField HeaderText="Status" DataField="appOrderStatus" SortExpression="appOrderStatus" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <asp:HiddenField ID="hdnSelectedIDs" Value="" runat="server" />
                                <asp:ValidationSummary ID="vsSearch" ShowMessageBox="true" EnableClientScript="true"
                                    HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                                    ValidationGroup="Search" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
