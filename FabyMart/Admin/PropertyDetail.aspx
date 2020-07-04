<%@ Page Title="Property Detail" Language="C#" MasterPageFile="~/admin/Main.master"
    AutoEventWireup="true" CodeFile="PropertyDetail.aspx.cs" Inherits="PropertyDetail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Admin/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function ShowTab() {

            $("#tabs-2").find('*').css('visibility', 'visible');
            $("#liPropertyPreValue").show();

            $("#tabs-3").find('*').css('visibility', 'visible');
            $("#liPropertySubCategory").show();

            ValidationGroupEnable();

        }
        function HideTab() {
            $("#tabs-2").find('*').css('visibility', 'hidden');
            $("#liPropertyPreValue").hide();
            $("#tabs-3").find('*').css('visibility', 'hidden');
            $("#liPropertySubCategory").hide();
            ValidationGroupEnable();
        }

        function SomeShoW() {
            $("#tabs-2").find('*').css('visibility', 'hidden');
            $("#liPropertyPreValue").hide();
            $("#tabs-3").find('*').css('visibility', 'visible');
            $("#liPropertySubCategory").show();
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
        Sys.Application.add_load(Onload);
    </script>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Property Detail
                </div>
                <div class="panel-search right-content">
                    <asp:Button ID="btnBack" runat="server" CssClass="btn btn-primary" Text="Back" OnClick="btnBack_Click"
                        TabIndex="10" />&nbsp;
                </div>
                <hr class="nomargin" />
                <DInfo:DisplayInfo runat="server" ID="DInfo" />
                <ul class="nav nav-tabs">
                    <li class="active"><a href="#tabs-1" data-toggle="tab">Property Detail</a></li>
                    <li class=""><a id="liPropertyPreValue" onclick="getTabID(this)" href="#tabs-2" data-toggle="tab">
                        Property Pre-Value</a></li>
                    <li class=""><a id="liPropertySubCategory" onclick="getTabID(this)" href="#tabs-3"
                        data-toggle="tab">Property Sub Category</a></li>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane fade active in" id="tabs-1">
                        <div class="panel-search right-content">
                            <div class="btn-group">
                                <asp:Button runat="server" ID="btnSave" class="btn btn-primary" OnClick="btnSave_Click"
                                    Text="Save" ValidationGroup="save" TabIndex="7"></asp:Button>
                                <button data-toggle="dropdown" class="btn btn-primary dropdown-toggle down-arrow">
                                </button>
                                <ul class="dropdown-menu">
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkSaveAndClose" OnClick="lnkSaveAndClose_Click"
                                            Text="Save & Close" ValidationGroup="save" TabIndex="8"></asp:LinkButton>
                                    </li>
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkSaveAndAddnew" OnClick="lnkSaveAndAddnew_Click"
                                            Text="Save & Add New" ValidationGroup="save" TabIndex="9"></asp:LinkButton></li>
                                </ul>
                            </div>
                        </div>
                        <hr class="nomargin" />
                        <div class="panel-search">
                            <div class="table-responsive">
                                <div class="entryformmain">
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            <span class="mandatory">*</span> Property Name :
                                        </div>
                                        <div class="controlstyle">
                                            <asp:TextBox ID="txtPropertyName" CssClass="form-control" Width="200" runat="server"
                                                TabIndex="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RFVPropertyName" runat="server"
                                                ErrorMessage="Enter Property Name" ValidationGroup="save" Text="*" ControlToValidate="txtPropertyName"
                                                SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            <span class="mandatory">*</span> Display Name :
                                        </div>
                                        <div class="controlstyle">
                                            <asp:TextBox ID="txtDisplayName" CssClass="form-control" Width="200" runat="server"
                                                TabIndex="3"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RFVDisplayName" runat="server"
                                                ErrorMessage="Enter Display Name" ValidationGroup="save" Text="*" ControlToValidate="txtDisplayName"
                                                SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            Description :
                                        </div>
                                        <div class="controlstyle">
                                            <asp:TextBox ID="txtDescription" CssClass="form-control" Width="200" runat="server"
                                                TabIndex="3" TextMode="MultiLine" Columns="4"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            Is Predefine :
                                        </div>
                                        <div class="controlstyle">
                                            <asp:CheckBox ID="chkIsPredefine" runat="server" Checked="false" TabIndex="3" />
                                        </div>
                                    </div>
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            Is Show In Search :
                                        </div>
                                        <div class="controlstyle">
                                            <asp:CheckBox ID="ChkIsShowInSearch" runat="server" Checked="false" TabIndex="4" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane fade  in" id="tabs-2">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="panel-search">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <div class="table-responsive">
                                                <DInfo:DisplayInfo runat="server" ID="DInfoPropertyPreValue" />
                                                <div class="entryformmain">
                                                    <div class="entryform">
                                                        <div class="labelstyle">
                                                            <span class="mandatory">*</span>Property Pre-Value :
                                                        </div>
                                                        <div class="controlstyle">
                                                            <asp:TextBox ID="txtPreValue" CssClass="form-control" Width="200" runat="server"
                                                                TabIndex="1"></asp:TextBox>
                                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RFVPreValue" runat="server" ErrorMessage="Enter Property Pre-Value"
                                                                ValidationGroup="PropertyPreValue" Text="*" ControlToValidate="txtPreValue" SetFocusOnError="true"
                                                                CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="entryform">
                                                        <div class="labelstyle">
                                                            Is Active :
                                                        </div>
                                                        <div class="controlstyle">
                                                            <asp:CheckBox ID="chkPropertyPreValueIsActive" runat="server" Checked="True" TabIndex="2" />
                                                        </div>
                                                    </div>
                                                    <div class="entryform">
                                                        <div class="labelstyle">
                                                        </div>
                                                        <div class="controlstyle">
                                                            <asp:Button runat="server" ID="btnSavePropertyPreValue" class="btn btn-primary" Text="Save"
                                                                ValidationGroup="PropertyPreValue" TabIndex="3" OnClick="btnSavePropertyPreValue_Click">
                                                            </asp:Button>
                                                            <asp:Button runat="server" ID="btnClearPropertyPreValue" class="btn btn-primary" Text="Clear"
                                                                TabIndex="4" OnClick="btnClearPropertyPreValue_Click"></asp:Button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:ValidationSummary ID="VsPropertyPreValue" ShowMessageBox="true" EnableClientScript="true"
                                                HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                                                ValidationGroup="PropertyPreValue" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="panel-search">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="page-inner-title">
                                                Property Pre-Value List
                                            </div>
                                            <div class="table-responsive">
                                                <div class="panel-search">
                                                    Select Criteria :
                                                    <asp:DropDownList runat="server" ID="ddlPropertyPreValueFields" CssClass="form-control"
                                                        TabIndex="3">
                                                        <asp:ListItem Text="Pre Value" Value="appPreValue"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="txtPropertyPreValueSearch" runat="server" CssClass="form-control"
                                                        onkeydown="return (event.keyCode!=13)"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvSearch" runat="server" ValidationGroup="PropertyPreValueSearch"
                                                        ErrorMessage="Search Text" Display="None" Font-Bold="True" SetFocusOnError="true"
                                                        ControlToValidate="txtPropertyPreValueSearch" CssClass="ErrorLabelStyle" Text="*">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:Button ID="btnPreValueGO" runat="server" CssClass="btn btn-primary" Text="Search"
                                                        ValidationGroup="PropertyPreValueSearch" TabIndex="2" OnClick="btnPreValueGO_Click" />
                                                    <asp:Button ID="btnPreValueReset" runat="server" CssClass="btn btn-primary" Text="Reset"
                                                        TabIndex="4" OnClick="btnPreValueReset_Click" />
                                                </div>
                                                <hr class="nomargin" />
                                                <div class="panel-search">
                                                    <div class="fleft">
                                                        <asp:Button ID="btnPropertyPreValueDelete" runat="server" CssClass="btn btn-danger"
                                                            Text="Delete" OnClientClick="return ConfirmMessage('Property PreValue','delete')"
                                                            OnClick="btnPropertyPreValueDelete_Click" />
                                                    </div>
                                                    <div class="fright">
                                                        Total&nbsp;Records&nbsp;:&nbsp; <span class="RecordCount">
                                                            <asp:Label ID="lblPropertyPreValueCount" runat="server" Text="0"> </asp:Label>
                                                        </span>
                                                        <div class="Separator">
                                                            &nbsp;
                                                        </div>
                                                        Per Page :
                                                        <asp:DropDownList ID="ddlPropertyPreValuePerPage" runat="server" AutoPostBack="true"
                                                            CssClass="form-control" OnSelectedIndexChanged="ddlPropertyPreValuePerPage_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="fclear">
                                                    </div>
                                                </div>
                                                <hr class="nomargin" />
                                                <DInfo:DisplayInfo runat="server" ID="DinfoPropertyPreValueData" />
                                                <asp:GridView ID="dgvPropertyPreValue" Width="100%" runat="server" AutoGenerateColumns="False"
                                                    DataKeyNames="appPropertyPreValueID,appDisplayOrder,appIsActive" CssClass="table table-striped table-bordered table-hover"
                                                    HeaderStyle-Wrap="false" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
                                                    PagerSettings-Mode="NumericFirstLast" PageSize="10" OnPageIndexChanging="dgvPropertyPreValue_PageIndexChanging"
                                                    OnRowCommand="dgvPropertyPreValue_RowCommand" OnRowCreated="dgvPropertyPreValue_RowCreated"
                                                    OnRowDataBound="dgvPropertyPreValue_RowDataBound" OnSorting="dgvPropertyPreValue_Sorting"
                                                    OnRowDeleting="dgvPropertyPreValue_RowDeleting" OnRowEditing="dgvPropertyPreValue_RowEditing">
                                                    <PagerSettings Position="Top" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkHeader" runat="server" onclick="SelectAll(this,'dgvPropertyPreValue');" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelectRow" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                            HeaderStyle-Width="1%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lnkEdit" CommandName="Edit" CommandArgument='<%#Eval("appPropertyPreValueID") %>'>
                                                                        <span class="action-icon set-icon"><i class="fa fa-pencil"></i></span>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Pre-Value" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%"
                                                            HeaderStyle-Width="40%" SortExpression="appPreValue">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPreValue" runat="server" Text='<%#Eval("appPreValue") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Is Active" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                            HeaderStyle-Width="1%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lnkIsActive" CommandName="IsActive" CommandArgument='<%#Eval("appPropertyPreValueID") %>'>
                                                        <span class="action-icon set-icon <%# Eval("appIsActive").ToString().ToLower() == "true" ? "green" : "red" %>"><i class="fa <%# Eval("appIsActive").ToString().ToLower() == "true" ? "fa-check" : "fa-ban" %>"></i></span>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Display Order" ItemStyle-HorizontalAlign="Center"
                                                            ItemStyle-Width="1%" HeaderStyle-Width="1%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lnkUp" CommandName="Up" CommandArgument='<%#Eval("appPropertyPreValueID") %>'
                                                                    ToolTip="Up">
                                                        <span class="action-icon set-icon"><i class="fa fa-chevron-up"></i></span>
                                                                </asp:LinkButton>
                                                                <asp:LinkButton runat="server" ID="lnkDown" CommandName="Down" CommandArgument='<%#Eval("appPropertyPreValueID") %>'
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
                                <asp:HiddenField ID="hdnPropertyPreValueId" runat="server" />
                                <asp:ValidationSummary ID="vsGallery" ShowMessageBox="true" EnableClientScript="true"
                                    HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                                    ValidationGroup="PropertyPreValueSearch" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnSavePropertyPreValue" />
                                <asp:PostBackTrigger ControlID="btnPropertyPreValueDelete" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div class="tab-pane fade  in" id="tabs-3">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <div class="panel-search">
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
                                            <div class="table-responsive">
                                                <DInfo:DisplayInfo runat="server" ID="DInfoSubCategory" />
                                                 <div class="entryformmain">
                                                    <div class="entryform">
                                                        <div class="labelstyle">
                                                            <span class="mandatory">*</span> Category :
                                                        </div>
                                                        <div class="controlstyle">
                                                            <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control" AutoPostBack="true"
                                                                OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RFVCategory" runat="server" ErrorMessage="Select Category"
                                                                ValidationGroup="SubCategory" Text="*" ControlToValidate="ddlCategory" SetFocusOnError="true"
                                                                InitialValue="0" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                
                                                    <div class="entryform">
                                                        <DInfo:DisplayInfo runat="server" ID="DInfoSubCategoryGrid" />
                                                        <asp:GridView ID="dgvUnSelectedCategories" Width="100%" runat="server" AutoGenerateColumns="False"
                                                            DataKeyNames="appSubCategoryID" CssClass="table table-striped table-bordered table-hover"
                                                            HeaderStyle-Wrap="false" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
                                                            PagerSettings-Mode="NumericFirstLast" PageSize="10" OnRowCommand="dgvUnSelectedCategories_RowCommand">
                                                            <PagerSettings Position="Top" />
                                                            <Columns>
                                                              
                                                                <asp:TemplateField HeaderText="Sub Category" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%"
                                                                    HeaderStyle-Width="40%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSubCategory" runat="server" Text='<%#Eval("appSubCategory") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Add" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                                    HeaderStyle-Width="1%">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" ID="lnkIsAdd" CommandName="IsAdd" CommandArgument='<%#Eval("appSubCategoryID") %>'>
                                                        <span class="action-icon set-icon green"><i class="fa fa-plus-square"></i></span>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="panel-search">
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                        <ContentTemplate>
                                            <div class="page-inner-title">
                                                Property Sub Category List
                                            </div>
                                            <div class="table-responsive">
                                                <div class="panel-search">
                                                    <asp:DropDownList runat="server" ID="ddlSerachCategory" CssClass="form-control" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlSerachCategory_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <div class="Separator">
                                                        &nbsp;
                                                    </div>
                                                    <asp:DropDownList runat="server" ID="ddlSerachSubCategory" CssClass="form-control"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlSerachSubCategory_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:Button ID="btnSubCategoryReset" runat="server" CssClass="btn btn-primary" Text="Reset"
                                                        TabIndex="4" OnClick="btnSubCategoryReset_Click" />
                                                </div>
                                                <hr class="nomargin" />
                                                <div class="panel-search">
                                                    <div class="fleft">
                                                        <asp:Button ID="btnSubCategoryDelete" runat="server" CssClass="btn btn-danger" Text="Delete"
                                                            OnClientClick="return ConfirmMessage('Property Sub Category','delete')" OnClick="btnSubCategoryDelete_Click" />
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
                                                    DataKeyNames="appPropertySubCategoryID,appDisplayOrder,appIsActive" CssClass="table table-striped table-bordered table-hover"
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
                                                        <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                            HeaderStyle-Width="1%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lnkEdit" CommandName="Edit" CommandArgument='<%#Eval("appPropertySubCategoryID") %>'>
                                                                        <span class="action-icon set-icon"><i class="fa fa-pencil"></i></span>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%"
                                                            HeaderStyle-Width="40%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCategory" runat="server" Text='<%#Eval("appCategory") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sub Category" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%"
                                                            HeaderStyle-Width="40%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSubCategory" runat="server" Text='<%#Eval("appSubCategory") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Is Active" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                            HeaderStyle-Width="1%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lnkIsActive" CommandName="IsActive" CommandArgument='<%#Eval("appPropertySubCategoryID") %>'>
                                                        <span class="action-icon set-icon <%# Eval("appIsActive").ToString().ToLower() == "true" ? "green" : "red" %>"><i class="fa <%# Eval("appIsActive").ToString().ToLower() == "true" ? "fa-check" : "fa-ban" %>"></i></span>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Display Order" ItemStyle-HorizontalAlign="Center"
                                                            ItemStyle-Width="1%" HeaderStyle-Width="1%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lnkUp" CommandName="Up" CommandArgument='<%#Eval("appPropertySubCategoryID") %>'
                                                                    ToolTip="Up">
                                                        <span class="action-icon set-icon"><i class="fa fa-chevron-up"></i></span>
                                                                </asp:LinkButton>
                                                                <asp:LinkButton runat="server" ID="lnkDown" CommandName="Down" CommandArgument='<%#Eval("appPropertySubCategoryID") %>'
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
                                <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" EnableClientScript="true"
                                    HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                                    ValidationGroup="SubCategory" />
                            </ContentTemplate>
                            <Triggers>
                                <%--<asp:PostBackTrigger ControlID="btnSaveSubCategory" />--%>
                                <asp:PostBackTrigger ControlID="btnSubCategoryDelete" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div>
                <asp:HiddenField ID="hdnPKID" Value="" runat="server" />
                <asp:HiddenField ID="hdnSelectedIDs" Value="" runat="server" />
                <asp:HiddenField ID="hdnTabID" runat="server" />
                <asp:HiddenField ID="hdnSortDir" Value="Asc" runat="server" />
                <asp:HiddenField ID="hdnSortCol" Value="" runat="server" />
                <asp:ValidationSummary ID="vsSearch" ShowMessageBox="true" EnableClientScript="true"
                    HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                    ValidationGroup="save" />
            </div>
        </div>
    </div>
</asp:Content>
