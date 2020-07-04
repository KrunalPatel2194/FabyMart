<%@ Page Title="SubCategory Detail" Language="C#" MasterPageFile="~/admin/Main.master" AutoEventWireup="true"
    CodeFile="SubCategoryDetail.aspx.cs" Inherits="SubCategoryDetail" %>

<%@ Register Src="~/Admin/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function ShowTab() {
            $("#tabs-2").find('*').css('visibility', 'visible');
            $("#liSize").show();
            ValidationGroupEnable();
        }
        function HideTab() {
            $("#tabs-2").find('*').css('visibility', 'hidden');
            $("#liSize").hide();

            ValidationGroupEnable();
        }
        function ValidationGroupEnable() {
            for (i = 0; i < Page_Validators.length; i++) {
                var visible = $('#' + Page_Validators[i].controltovalidate).css('visibility');
                if (visible == 'visible') {
                    ValidatorEnable(Page_Validators[i], true);
                } else {
                    ValidatorEnable(Page_Validators[i], false);
                }
            }
        }

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
        Sys.Application.add_load(Load);
    </script>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Sub Category Detail
                </div>
                <div>
                    <div class="panel-search right-content">
                        <asp:Button ID="btnBack" runat="server" CssClass="btn btn-primary" Text="Back" OnClick="btnBack_Click"
                            TabIndex="8" />&nbsp;
                        <div class="btn-group">
                            <asp:Button runat="server" ID="btnSave" class="btn btn-primary" OnClick="btnSave_Click"
                                Text="Save" ValidationGroup="save" TabIndex="5"></asp:Button>
                            <button data-toggle="dropdown" class="btn btn-primary dropdown-toggle down-arrow">
                            </button>
                            <ul class="dropdown-menu">
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkSaveAndClose" OnClick="lnkSaveAndClose_Click"
                                        Text="Save & Close" ValidationGroup="save" TabIndex="6"></asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkSaveAndAddnew" OnClick="lnkSaveAndAddnew_Click"
                                        Text="Save & Add New" ValidationGroup="save" TabIndex="7"></asp:LinkButton></li>
                            </ul>
                        </div>
                    </div>
                    <hr class="nomargin" />
                    <DInfo:DisplayInfo runat="server" ID="DInfo" />
                    <ul class="nav nav-tabs">
                        <li class="active"><a href="#tabs-1" data-toggle="tab">Sub Category</a></li>
                        <li class=""><a id="liSize" onclick="getTabID(this)" href="#tabs-2" data-toggle="tab">
                            Sub Category Size </a></li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane fade active in" id="tabs-1">
                            <div class="panel-search">
                                <div class="table-responsive">
                                    <div class="entryformmain">
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                <span class="mandatory">*</span> Category :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator Display="Dynamic" ID="RFVCategory" runat="server" ErrorMessage="Select Category"
                                                    ValidationGroup="save" Text="*" ControlToValidate="ddlCategory" SetFocusOnError="true"
                                                    InitialValue="0" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                <span class="mandatory">*</span> Sub Category Name:
                                            </div>
                                            <div class="controlstyle">
                                                <asp:TextBox ID="txtSubCategoryName" CssClass="form-control" Width="200" runat="server"
                                                    TabIndex="2"></asp:TextBox>
                                                <asp:RequiredFieldValidator Display="Dynamic" ID="RFVSubCategoryName" runat="server"
                                                    ErrorMessage="Enter Sub-Category Name" ValidationGroup="save" Text="*" ControlToValidate="txtSubCategoryName"
                                                    SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                Is Active :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" TabIndex="3" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade  in" id="tabs-2">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <div class="panel-search">
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <div class="table-responsive">
                                                    <DInfo:DisplayInfo runat="server" ID="DInfoSizeGrid" />
                                                    
                                                       
                                                       
                                                    <div class="entryform">
                                                       
                                                        <asp:GridView ID="dgvUnSelectedSize" Width="100%" runat="server" AutoGenerateColumns="False"
                                                            DataKeyNames="appSizeID" CssClass="table table-striped table-bordered table-hover"
                                                            HeaderStyle-Wrap="false" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
                                                            PagerSettings-Mode="NumericFirstLast" PageSize="10" OnRowCommand="dgvUnSelectedSize_RowCommand">
                                                            <PagerSettings Position="Top" />
                                                            <Columns>
                                                              
                                                                <asp:TemplateField HeaderText="Sub Category" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%"
                                                                    HeaderStyle-Width="40%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSubCategory" runat="server" Text='<%#Eval("appSize") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Add" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                                    HeaderStyle-Width="1%">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" ID="lnkIsAdd" CommandName="IsAdd" CommandArgument='<%#Eval("appSizeID") %>'>
                                                        <span class="action-icon set-icon green"><i class="fa fa-plus-square"></i></span>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>

                                                   
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="panel-search">
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                            <ContentTemplate>
                                                <div class="page-inner-title">
                                                    Size Sub Category List
                                                </div>
                                                <div class="table-responsive">
                                                    <div class="panel-search">
                                                        <div class="fleft">
                                                            <asp:Button ID="btnSubCategoryDelete" runat="server" CssClass="btn btn-danger" Text="Delete"
                                                                OnClientClick="return ConfirmMessage('Size','delete')" OnClick="btnSubCategoryDelete_Click" />
                                                        </div>
                                                        <div class="fright">
                                                            Total&nbsp;Records&nbsp;:&nbsp; <span class="RecordCount">
                                                                <asp:Label ID="lblSubCategoryCount" runat="server" Text="0"> </asp:Label>
                                                            </span>
                                                            <div class="Separator">
                                                                &nbsp;
                                                            </div>
                                                            Per Page :
                                                            <asp:DropDownList ID="ddlSubCategoryPerPage" runat="server" AutoPostBack="true" CssClass="form-control"
                                                                OnSelectedIndexChanged="ddlSubCategoryPerPage_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="fclear">
                                                        </div>
                                                    </div>
                                                    <hr class="nomargin" />
                                                    <DInfo:DisplayInfo runat="server" ID="DinfoBranchGrid" />
                                                    <asp:GridView ID="dgvSubCategory" Width="100%" runat="server" AutoGenerateColumns="False"
                                                        DataKeyNames="appSizeSubCategoryID,appDisplayOrder" CssClass="table table-striped table-bordered table-hover"
                                                        HeaderStyle-Wrap="false" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
                                                        PagerSettings-Mode="NumericFirstLast" PageSize="10" OnPageIndexChanging="dgvSubCategory_PageIndexChanging"
                                                        OnRowCommand="dgvSubCategory_RowCommand" OnRowCreated="dgvSubCategory_RowCreated"
                                                        OnRowDataBound="dgvSubCategory_RowDataBound" OnSorting="dgvSubCategory_Sorting"
                                                        OnRowDeleting="dgvSubCategory_RowDeleting" OnRowEditing="dgvSubCategory_RowEditing">
                                                        <PagerSettings Position="Top" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkHeader" runat="server" onclick="SelectAll(this,'dgvSubCategory');" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSelectRow" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                           
                                                            <asp:TemplateField HeaderText="Size" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%"
                                                                HeaderStyle-Width="40%" SortExpression="appSize">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCategory" runat="server" Text='<%#Eval("appSize") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Display Order" ItemStyle-HorizontalAlign="Center"
                                                                ItemStyle-Width="1%" HeaderStyle-Width="1%">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="lnkUp" CommandName="Up" CommandArgument='<%#Eval("appSizeSubCategoryID") %>'
                                                                        ToolTip="Up">
                                                        <span class="action-icon set-icon"><i class="fa fa-chevron-up"></i></span>
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton runat="server" ID="lnkDown" CommandName="Down" CommandArgument='<%#Eval("appSizeSubCategoryID") %>'
                                                                        ToolTip="Down">
                                                        <span class="action-icon set-icon"><i class="fa fa-chevron-down"></i></span>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <asp:HiddenField ID="hdnSubCategoryId" runat="server" />
                                    <asp:ValidationSummary ID="VsSuc" ShowMessageBox="true" EnableClientScript="true"
                                        HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                                        ValidationGroup="SubCategory" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
            <div>
                <asp:HiddenField ID="hdnPKID" Value="" runat="server" />
                <asp:HiddenField ID="hdnSelectedIDs" Value="" runat="server" />
                <asp:HiddenField ID="hdnTabID" runat="server" />
                <asp:ValidationSummary ID="vsSearch" ShowMessageBox="true" EnableClientScript="true"
                    HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                    ValidationGroup="save" />
            </div>
        </div>
    </div>
</asp:Content>
