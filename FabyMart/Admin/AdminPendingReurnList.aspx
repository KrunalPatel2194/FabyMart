<%@ Page Title="AdminPendingReurnList" Language="C#" MasterPageFile="~/admin/Main.master"
    AutoEventWireup="true" CodeFile="AdminPendingReurnList.aspx.cs" Inherits="AdminPendingReurnList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Pending Return Order List
                </div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="panel-search">
                                Select Criteria :
                               
                                <asp:DropDownList runat="server" ID="ddlFields" CssClass="form-control" TabIndex="3">
                                    <asp:ListItem Text="Product Name" Value="appProductName"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13)"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="rfvSearch" runat="server" ValidationGroup="Search"
                                    ErrorMessage="Search Text" Display="None" Font-Bold="True" SetFocusOnError="true"
                                    ControlToValidate="txtSearch" CssClass="ErrorLabelStyle" Text="*">
                                </asp:RequiredFieldValidator>--%>
                                <div class="Separator">
                                    &nbsp;
                                </div>
                                Date:
                                <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>
                                <cc1:CalendarExtender ID="calStartDate" TargetControlID="txtStartDate" Format="dd/MM/yyyy"
                                    PopupButtonID="EventStartDate" runat="server">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator Display="Dynamic" ID="rfvStartDate" runat="server" ErrorMessage="Enter Start Date"
                                    ValidationGroup="save" Text="*" ControlToValidate="txtStartDate" SetFocusOnError="true"
                                    CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                To:
                                <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>
                                <cc1:CalendarExtender ID="calEndDate" TargetControlID="txtEndDate" Format="dd/MM/yyyy"
                                    PopupButtonID="EventEndDate" runat="server">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator Display="Dynamic" ID="rfvEndDate" runat="server" ErrorMessage="Enter End Date"
                                    ValidationGroup="save" Text="*" ControlToValidate="txtEndDate" SetFocusOnError="true"
                                    CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                <asp:Button ID="btnGO" runat="server" CssClass="btn btn-primary" Text="Search" ValidationGroup="Search"
                                    TabIndex="2" OnClick="btnGO_Click" />
                                <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary" Text="Reset"
                                    TabIndex="4" OnClick="btnReset_Click" />
                            </div>
                            <hr class="nomargin" />
                            <div class="panel-search">
                                <div class="fleft">
                                 <asp:Button ID="btnRejected" runat="server" CssClass="btn btn-danger" Text="Rejected"
                                        OnClientClick="return ConfirmMessage('Return Product','Rejected')" 
                                        onclick="btnRejected_Click"  />&nbsp;
                                    <asp:Button ID="btnApprove" runat="server" CssClass="btn btn-primary" Text="Approve"
                                        TabIndex="4" OnClick="btnApprove_Click" OnClientClick="return ConfirmMessage('Return Product','Approve')" />
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
                                        AllowSorting="true" DataKeyNames="appReturnOrderID,appReturnStatus" CssClass="table table-striped table-bordered table-hover"
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
                                            <%--  <asp:TemplateField HeaderText="Image" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <img src="<%#strServerURL+Eval("appThumbImage") %>" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ProductName" ItemStyle-HorizontalAlign="Left" SortExpression="appProductName">
                                                <ItemTemplate>
                                                    <a href='<%# objPageBase.GetAlias("ProductDetail.aspx") + generateUrl(Eval("appProductName").ToString())  + "/" + objEncrypt.Encrypt(Eval("appProductDetailID").ToString(), appFunctions.strKey) %>'>
                                                        <p>
                                                            <%#Eval("appProductName")%></p>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Product Info" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%"
                                                HeaderStyle-Width="30%">
                                                <ItemTemplate>
                                                    <div style="margin-top: 5px;">
                                                        <asp:GridView ID="dgvSubDetail" Width="100%" runat="server" AutoGenerateColumns="False" ShowHeader="false"
                                                            DataKeyNames="appReturnOrderDetailID" CssClass="table table-bordered" HeaderStyle-Wrap="false"
                                                            AllowPaging="false" AllowSorting="false">
                                                            <PagerSettings Position="Top" />
                                                            <Columns>
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
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                         <%--   <asp:TemplateField HeaderText="Seller Name" ItemStyle-HorizontalAlign="Left" SortExpression="appDisplayName">
                                                <ItemTemplate>
                                                    <%#Eval("appDisplayName")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="Left" SortExpression="appRequestedDate">
                                                <ItemTemplate>
                                                    <%#Eval("appRequestedDate")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
                                            <asp:TemplateField HeaderText="Info" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60%"
                                                HeaderStyle-Width="60%">
                                                <ItemTemplate>
                                                    <div class="divGrid">
                                                        <b>
                                                            <%#Eval("appReason") %></b>
                                                    </div>
                                                    <div class="divGrid">
                                                        <%#Eval("appNote") %>
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
