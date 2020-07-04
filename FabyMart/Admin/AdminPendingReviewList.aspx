<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Main.master" AutoEventWireup="true"
    CodeFile="AdminPendingReviewList.aspx.cs" Inherits="Admin_PendingReviewList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Pending Review List
                </div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="panel-search">
                                Select Criteria :
                                <%-- <asp:DropDownList ID="ddlSeller" AutoPostBack="true" runat="server" 
                                    CssClass="form-control" 
                                    onselectedindexchanged="ddlSeller_SelectedIndexChanged">
                                </asp:DropDownList>--%>
                                <div class="Separator">
                                    &nbsp;
                                </div>
                                <asp:DropDownList runat="server" ID="ddlFields" CssClass="form-control" TabIndex="3">
                                    <asp:ListItem Text="Product Name" Value="appProductName"></asp:ListItem>
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
                                    <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" ValidationGroup="Remark"
                                        onkeydown="return (event.keyCode!=13)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvRemark" runat="server" ValidationGroup="Remark"
                                        ErrorMessage="Search Remark" Display="None" Font-Bold="True" SetFocusOnError="true"
                                        ControlToValidate="txtRemark" CssClass="ErrorLabelStyle" Text="*">
                                    </asp:RequiredFieldValidator>
                                    <asp:Button ID="btnReject" runat="server" CssClass="btn btn-primary" Text="Reject"
                                        ValidationGroup="Remark" TabIndex="2" OnClick="btnReject_Click" OnClientClick="return ConfirmMessage('Review','Reject')" />
                                    <asp:Button ID="btnApprove" runat="server" CssClass="btn btn-primary" Text="Approve"
                                        TabIndex="4" OnClick="btnApprove_Click" OnClientClick="return ConfirmMessage('Review','Approve')" />
                                    <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Delete"
                                        OnClientClick="return ConfirmMessage('Review','delete')" OnClick="btnDelete_Click" />
                                    <%--<asp:Button ID="btnAdd" runat="server" CssClass="btn btn-success" Text="Add New"
                                        TabIndex="6" OnClick="btnAdd_Click" />--%>
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
                                        AllowSorting="true" DataKeyNames="appReviewID" CssClass="table table-striped table-bordered table-hover"
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
                                            <%-- <asp:TemplateField HeaderText="Images" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                              <img src="<%#strServerURL+Eval("appThumbImage") %>" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="ProductName" ItemStyle-HorizontalAlign="Left" SortExpression="appProductName">
                                                <ItemTemplate>
                                                    <a href='<%# objPageBase.GetAlias("ProductDetail.aspx") + generateUrl(Eval("appProductName").ToString())  + "/" + objEncrypt.Encrypt(Eval("appProductDetailID").ToString(), appFunctions.strKey) %>'>
                                                        <p>
                                                            <%#Eval("appProductName")%></p>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Comments" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60%"
                                                HeaderStyle-Width="60%">
                                                <ItemTemplate>
                                                    <div class="divGrid">
                                                        <b>
                                                            <%#Eval("appTitle") %></b>
                                                    </div>
                                                    <div class="divGrid">
                                                        <%#Eval("appComment") %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--  <asp:TemplateField HeaderText="Is Approved" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                 <div id="divView" runat="server">
                                                    <asp:LinkButton runat="server" ID="lnkIsActive" CommandName="IsPublished" CommandArgument='<%#Eval("appReviewID") %>'>
                                                        <span class="action-icon set-icon <%# Eval("appReviewStatus").ToString().ToLower() == "2" ? "green" : "red" %>"><i class="fa <%# Eval("appReviewStatus").ToString().ToLower() == "true" ? "fa-check" : "fa-ban" %>"></i></span>
                                                    </asp:LinkButton>
                                                       </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <%--  <asp:BoundField DataField="NoOfComments" HeaderText="Total Comments" SortExpression="NoOfComments" />
                                        <asp:BoundField DataField="appDisplayName" HeaderText="Display Name" SortExpression="appDisplayName" />
                                    
                                            <asp:TemplateField HeaderText="View Comments" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                  <a href='<%# "ViewComments.aspx?ID=" + objEncrypt.Encrypt(Eval("appProductDetailID").ToString(), appFunctions.strKey) %>'
                                                        title="Edit"><span class="action-icon set-icon"><i class="fa fa-pencil"></i></span>
                                                    </a>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            --%>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <asp:HiddenField ID="hdnPKID" runat="server" Value="" />
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
