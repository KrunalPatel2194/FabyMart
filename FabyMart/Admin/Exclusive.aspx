<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Main.master" AutoEventWireup="true" CodeFile="Exclusive.aspx.cs" Inherits="Admin_Exclusive" %>
<%@ Register Src="~/Admin/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Exclusive offer List
                </div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="panel-search">
                                Select Category :
                                <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control" 
                                    AutoPostBack="true" onselectedindexchanged="ddlCategory_SelectedIndexChanged">
                                    
                                </asp:DropDownList>
                                <%--<asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvSearch" runat="server" ValidationGroup="Search"
                                    ErrorMessage="Search Text" Display="None" Font-Bold="True" SetFocusOnError="true"
                                    ControlToValidate="txtSearch" CssClass="ErrorLabelStyle" Text="*">
                                </asp:RequiredFieldValidator>--%>
                                <asp:Button ID="btnGO" runat="server" CssClass="btn btn-primary" Text="Search" ValidationGroup="Search"
                                    OnClick="btnGO_Click" />
                                <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary" Text="Reset"
                                   OnClick="btnReset_Click" />
                            </div>
                            <hr class="nomargin" />
                            <div class="panel-search">
                                <div class="fleft">
                                    <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Delete"
                                        OnClientClick="return ConfirmMessage('Exclusive Offer','delete')" OnClick="btnDelete_Click" />
                                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-success" Text="Add New"
                                         OnClick="btnAdd_Click" />
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
                                        DataKeyNames="appExclusiveID,appDisplayOrder,appIsActive" CssClass="table table-striped table-bordered table-hover"
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
                                                    <a href='<%# "ExclusiveDetail.aspx?ID=" + objEncrypt.Encrypt(Eval("appExclusiveID").ToString(), appFunctions.strKey) %>'
                                                        title="Edit"><span class="action-icon set-icon"><i class="fa fa-pencil"></i></span>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Images" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Image ID="img" runat="server" ImageUrl='<%#Eval("appImage") %>' AlternateText='<%#Eval("appLink") %>'
                                                        Width="100" />
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                             <asp:BoundField HeaderText="Title" DataField="appTitle" SortExpression="appTitle" />
                                            <asp:BoundField HeaderText="Category" DataField="appCategory" SortExpression="appCategory" />
                                            <asp:BoundField HeaderText="Link" DataField="appLink" SortExpression="appLink" />
                                            <asp:TemplateField HeaderText="Is Active" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkIsActive" CommandName="IsActive" CommandArgument='<%#Eval("appExclusiveID") %>'>
                                                        <span class="action-icon set-icon <%# Eval("appIsActive").ToString().ToLower() == "true" ? "green" : "red" %>"><i class="fa <%# Eval("appIsActive").ToString().ToLower() == "true" ? "fa-check" : "fa-ban" %>"></i></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Display Order" ItemStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="1%" HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkUp" CommandName="Up" CommandArgument='<%#Eval("appExclusiveID") %>'
                                                        ToolTip="Up">
                                                        <span class="action-icon set-icon"><i class="fa fa-chevron-up"></i></span>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="lnkDown" CommandName="Down" CommandArgument='<%#Eval("appExclusiveID") %>'
                                                        ToolTip="Down">
                                                        <span class="action-icon set-icon"><i class="fa fa-chevron-down"></i></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <asp:HiddenField ID="hdnPKID" runat="server" Value="" />
                                <asp:HiddenField ID="hdnSortDir" Value="Asc" runat="server" />
                                <asp:HiddenField ID="hdnSortCol" Value="" runat="server" />
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

