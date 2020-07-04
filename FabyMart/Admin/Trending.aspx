<%@ Page Title="Trending" Language="C#" MasterPageFile="~/admin/Main.master" AutoEventWireup="true"
    CodeFile="Trending.aspx.cs" Inherits="Trending" %>

<%@ Register Src="~/Admin/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function getTabID(tab) {
            var ID = tab.id;
            document.getElementById("<%=hdnTabID.ClientID%>").value = ID;
            document.getElementById("<%=hdnSelectedIDs.ClientID%>").value = "";
        }
        function selectedTab() {
            document.getElementById(document.getElementById("<%=hdnTabID.ClientID%>").value).click();
        }

        function Onload() {
            try {
                selectedTab();
            }
            catch (e) {
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        Sys.Application.add_load(Onload);
    </script>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Trending
                </div>
                <hr class="nomargin" />
                <div style="margin-top: 15px;">
                    <ul class="nav nav-tabs">
                        <li class="active"><a id="liList" href="#tabs-1" onclick="getTabID(this)" data-toggle="tab">
                            Trending List</a></li>
                        <li class=""><a id="liAdd" onclick="getTabID(this)" href="#tabs-2" data-toggle="tab">
                            Add Product</a></li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane fade active in" id="tabs-1">
                            <div class="panel-search">
                                Select Criteria :
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
                                    <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Delete"
                                        OnClientClick="return ConfirmMessage('Trending','delete')" OnClick="btnDelete_Click" />
                                </div>
                                <div class="fright">
                                    Total&nbsp;Records&nbsp;:&nbsp; <span class="RecordCount">
                                        <asp:Label ID="lblCount" runat="server" Text="0"> </asp:Label>
                                    </span>
                                    <div class="Separator">
                                        &nbsp;
                                    </div>
                                    Per Page :
                                    <asp:DropDownList ID="ddlPerPage" runat="server" AutoPostBack="true" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="fclear">
                                </div>
                                <DInfo:DisplayInfo runat="server" ID="DInfo" />
                            </div>
                            <hr class="nomargin" />
                            <div class="panel-search">
                                <div class="table-responsive">
                                    <asp:GridView ID="dgvGridView" Width="100%" runat="server" AutoGenerateColumns="False"
                                        AllowSorting="true" DataKeyNames="appTrendingID,appIsActive,appDisplayOrder"
                                        CssClass="table table-striped table-bordered table-hover" HeaderStyle-Wrap="false"
                                        PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last" PagerSettings-Mode="NumericFirstLast"
                                        PageSize="10" OnPageIndexChanging="dgvGridView_PageIndexChanging" OnRowCreated="dgvGridView_RowCreated"
                                        OnRowDataBound="dgvGridView_RowDataBound" OnSorting="dgvGridView_Sorting" OnRowCommand="dgvGridView_RowCommand">
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
                                            <asp:TemplateField HeaderText="Product Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50%"
                                                SortExpression="appProductName" HeaderStyle-Width="50%">
                                                <ItemTemplate>
                                                    <a href='<%# "ProductInformation.aspx?ID=" + objEncrypt.Encrypt(Eval("appProductID").ToString(), appFunctions.strKey) %>'
                                                        target="_blank">
                                                        <%#Eval("appProductName")%></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Is Active" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkIsActive" CommandName="IsActive" CommandArgument='<%#Eval("appTrendingID") %>'>
                                                        <span class="action-icon set-icon <%# Eval("appIsActive").ToString().ToLower() == "true" ? "green" : "red" %>"><i class="fa <%# Eval("appIsActive").ToString().ToLower() == "true" ? "fa-check" : "fa-ban" %>"></i></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Display Order" ItemStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="1%" HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkUp" CommandName="Up" CommandArgument='<%#Eval("appTrendingID") %>'
                                                        ToolTip="Up">
                                                        <span class="action-icon set-icon"><i class="fa fa-chevron-up"></i></span>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="lnkDown" CommandName="Down" CommandArgument='<%#Eval("appTrendingID") %>'
                                                        ToolTip="Down">
                                                        <span class="action-icon set-icon"><i class="fa fa-chevron-down"></i></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade  in" id="tabs-2">
                            <div class="panel-search">
                                <div class="fleft">
                                    <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <div class="Separator">
                                        &nbsp;
                                    </div>
                                    <asp:DropDownList runat="server" ID="ddlSubCate" CssClass="form-control" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlSubCate_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <div class="Separator">
                                        &nbsp;
                                    </div>
                                    <asp:DropDownList runat="server" ID="ddlColor" CssClass="form-control" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlColor_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:Button ID="btnProductReset" runat="server" CssClass="btn btn-primary" Text="Reset"
                                        TabIndex="4" OnClick="btnProductReset_Click" />
                                </div>
                                <div class="fright" style="line-height:32px;">
                                    Total&nbsp;Records&nbsp;:&nbsp; <span class="RecordCount">
                                        <asp:Label ID="lblProductCount" runat="server" Text="0"> </asp:Label>
                                    </span>
                                </div>
                                <div class="fclear">
                                </div>
                            </div>
                            <hr class="nomargin" />
                            <div class="panel-search">
                                <div class="table-responsive">
                                    <DInfo:DisplayInfo runat="server" ID="DinfoProduct" />
                                    <asp:GridView ID="dgvProduct" Width="100%" runat="server" AutoGenerateColumns="False"
                                        AllowSorting="true" DataKeyNames="appProductID" CssClass="table table-striped table-bordered table-hover"
                                        HeaderStyle-Wrap="false" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
                                        PagerSettings-Mode="NumericFirstLast" PageSize="10" OnPageIndexChanging="dgvProduct_PageIndexChanging"
                                        OnRowCommand="dgvProduct_RowCommand" OnRowCreated="dgvProduct_RowCreated" OnRowDataBound="dgvProduct_RowDataBound"
                                        OnSorting="dgvProduct_Sorting" OnRowDeleting="dgvProduct_RowDeleting" OnRowEditing="dgvProduct_RowEditing">
                                        <PagerSettings Position="Top" />
                                        <Columns>
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
                                             <asp:TemplateField HeaderText="Product Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50%"
                                                SortExpression="appProductName" HeaderStyle-Width="50%">
                                                <ItemTemplate>
                                                    <a href='<%# "ProductInformation.aspx?ID=" + objEncrypt.Encrypt(Eval("appProductID").ToString(), appFunctions.strKey) %>'
                                                        target="_blank">
                                                        <%#Eval("appProductName")%></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Add" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkSave" CommandName="Save" CommandArgument='<%#Eval("appProductID") %>'>
                                                        <span class="action-icon set-icon"><i class="fa fa-plus"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:ValidationSummary ID="vsSearch" ShowMessageBox="true" EnableClientScript="true"
                    HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                    ValidationGroup="save" />
                <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" EnableClientScript="true"
                    HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                    ValidationGroup="Search" />
            </div>
        </div>
        <asp:HiddenField ID="hdnPKID" Value="" runat="server" />
        <asp:HiddenField ID="hdnSelectedIDs" Value="" runat="server" />
        <asp:HiddenField ID="hdnTabID" runat="server" />
    </div>
</asp:Content>
