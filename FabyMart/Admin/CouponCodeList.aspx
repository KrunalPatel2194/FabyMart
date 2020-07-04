<%@ Page Title="Property List" Language="C#" MasterPageFile="~/Admin/Main.master"
    AutoEventWireup="true" CodeFile="CouponCodeList.aspx.cs" Inherits="CouponCodeList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Coupon Code List
                </div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="panel-search">
                                Date
                                 <asp:DropDownList runat="server" ID="ddlDateType" CssClass="form-control" TabIndex="3">
                                    <asp:ListItem Text="Start Date" Value="appStartDate"></asp:ListItem>
                                    <asp:ListItem Text="End Date" Value="appEndDate"></asp:ListItem>
                                </asp:DropDownList> :
                                <asp:TextBox ID="txtStartDate" runat="server" CssClass="txtDate" placeholder="dd-MM-yyyy"></asp:TextBox>
                                <cc1:CalendarExtender ID="calStartDate" TargetControlID="txtStartDate" Format="dd-MM-yyyy"
                                    PopupButtonID="EventStartDate" runat="server">
                                </cc1:CalendarExtender>
                                <asp:RegularExpressionValidator Display="Dynamic" ID="REVStartDate" runat="server"
                                    ValidationGroup="Search" Text="*" ControlToValidate="txtStartDate" SetFocusOnError="true"
                                    CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                To
                                <asp:TextBox ID="txtEndDate" runat="server" CssClass="txtDate" placeholder="dd-MM-yyyy"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtEndDate" Format="dd-MM-yyyy"
                                    PopupButtonID="EventEndDate" runat="server">
                                </cc1:CalendarExtender>
                                <asp:RegularExpressionValidator Display="Dynamic" ID="REVEndDate" runat="server"
                                    ValidationGroup="Search" Text="*" ControlToValidate="txtEndDate" SetFocusOnError="true"
                                    CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                Select Criteria :
                                <asp:DropDownList runat="server" ID="ddlFields" CssClass="form-control" TabIndex="3">
                                    <asp:ListItem Text="--select Option--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Coupon Code" Value="appCouponCode"></asp:ListItem>
                                    <asp:ListItem Text="Discount" Value="appDiscountPer"></asp:ListItem>
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
                                        OnClientClick="return ConfirmMessage('Coupon Code','delete')" OnClick="btnDelete_Click" />
                                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-success" Text="Add New"
                                        TabIndex="6" OnClick="btnAdd_Click" />
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
                                        AllowSorting="true" DataKeyNames="appCouponCodeID" CssClass="table table-striped table-bordered table-hover"
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
                                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <a href='<%# "CouponCodeDetail.aspx?ID=" + objEncrypt.Encrypt(Eval("appCouponCodeID").ToString(), appFunctions.strKey) %>'
                                                        title="Edit"><span class="action-icon set-icon"><i class="fa fa-pencil"></i></span>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Coupon Code" DataField="appCouponCode" SortExpression="appCouponCode"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="70%"
                                                HeaderStyle-Width="70%" />
                                            <asp:BoundField HeaderText="Discount Per." DataField="appDiscountPer" SortExpression="appDiscountPer"
                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                HeaderStyle-Width="1%" />
                                            <asp:BoundField HeaderText="Start Date" DataField="appStartDate" SortExpression="appStartDate"
                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                HeaderStyle-Width="1%" />
                                            <asp:BoundField HeaderText="End Date" DataField="appEndDate" SortExpression="appEndDate"
                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                HeaderStyle-Width="1%" />
                                            <asp:TemplateField HeaderText="Type" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblType" runat="server" Text='<%# Eval("appType") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Is Active" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkIsActive" CommandName="IsActive" CommandArgument='<%#Eval("appCouponCodeID") %>'>
                                                        <span class="action-icon set-icon <%# Eval("appIsActive").ToString().ToLower() == "true" ? "green" : "red" %>"><i class="fa <%# Eval("appIsActive").ToString().ToLower() == "true" ? "fa-check" : "fa-ban" %>"></i></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
