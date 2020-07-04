<%@ Page Title="Product Detail" Language="C#" MasterPageFile="~/admin/Main.master"
    AutoEventWireup="true" CodeFile="ProductDetail.aspx.cs" Inherits="ProductDetail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Admin/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function ShowTab() {
            $("#tabs-2").find('*').css('visibility', 'visible');
            $("#liProductSubCategory").show();
            $("#tabs-3").find('*').css('visibility', 'visible');
            $("#liProductColor").show();
            $("#tabs-4").find('*').css('visibility', 'visible');
            $("#liProductColorDetail").show();
            $("#tabs-5").find('*').css('visibility', 'visible');
            $("#linProductProperty").show();
            $("#tabs-6").find('*').css('visibility', 'visible');
            $("#linRelatedProduct").show();
            $("#tabs-7").find('*').css('visibility', 'visible');
            $("#linPixelCode").show();
            ValidationGroupEnable();
        }
        function HideTab() {
            $("#tabs-2").find('*').css('visibility', 'hidden');
            $("#liProductSubCategory").hide();
            $("#tabs-3").find('*').css('visibility', 'hidden');
            $("#liProductColor").hide();
            $("#tabs-4").find('*').css('visibility', 'hidden');
            $("#liProductColorDetail").hide();
            $("#tabs-5").find('*').css('visibility', 'hidden');
            $("#linProductProperty").hide();
            $("#tabs-6").find('*').css('visibility', 'hidden');
            $("#linRelatedProduct").hide();
            $("#tabs-7").find('*').css('visibility', 'hidden');
            $("#linPixelCode").hide();
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

        function ImgUpload(input) {
            if (input.files && input.files[0]) {
                document.getElementById("<%=hdnFilePath.ClientID%>").value = input.value;
            }
        }

        function hascolor() {
            if ($("#<%= chkIsColor.ClientID %>").prop('checked')) {
                $("#divColor").fadeIn();
                ValidatorEnable(document.getElementById("<%=RFVColor.ClientID %>"), true);
            }
            else {
                $("#divColor").fadeOut();
                ValidatorEnable(document.getElementById("<%=RFVColor.ClientID %>"), false);
            }
        }

        function Onload() {
            try {
                debugger;
                setUpCK('<%=txtDescription.ClientID %>');
                hascolor();
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
                    Product Detail
                </div>
                <div class="panel-search right-content">
                    <asp:Button ID="btnBack" runat="server" CssClass="btn btn-primary" Text="Back" OnClick="btnBack_Click" />&nbsp;
                </div>
                <hr class="nomargin" />
                <DInfo:DisplayInfo runat="server" ID="DInfo" />
                <ul class="nav nav-tabs">
                    <li class="active"><a href="#tabs-1" data-toggle="tab">Product Detail</a></li>
                    <li class=""><a id="liProductSubCategory" onclick="getTabID(this)" href="#tabs-2"
                        data-toggle="tab">Sub Categories</a></li>
                    <li class=""><a id="liProductColor" onclick="getTabID(this)" href="#tabs-3" data-toggle="tab">
                        Images</a></li>
                    <li class=""><a id="liProductColorDetail" onclick="getTabID(this)" href="#tabs-4"
                        data-toggle="tab">Sizes / Price</a></li>
                    <li class=""><a id="linProductProperty" onclick="getTabID(this)" href="#tabs-5" data-toggle="tab">
                        Property</a></li>
                    <li class=""><a id="linRelatedProduct" onclick="getTabID(this)" href="#tabs-6" data-toggle="tab">
                        Related Product</a></li>
                    <li class=""><a id="linPixelCode" onclick="getTabID(this)" href="#tabs-7" data-toggle="tab">
                        Pixel Code</a></li>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane fade active in" id="tabs-1">
                        <div class="panel-search right-content">
                            <div class="btn-group">
                                <asp:Button runat="server" ID="btnSave" class="btn btn-primary" OnClick="btnSave_Click"
                                    Text="Save" ValidationGroup="save"></asp:Button>
                                <button id="btnDropdown" runat="server" data-toggle="dropdown" class="btn btn-primary dropdown-toggle down-arrow">
                                </button>
                                <ul class="dropdown-menu">
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkSaveAndClose" OnClick="lnkSaveAndClose_Click"
                                            Text="Save & Close" ValidationGroup="save"></asp:LinkButton>
                                    </li>
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkSaveAndAddnew" OnClick="lnkSaveAndAddnew_Click"
                                            Text="Save & Add New" ValidationGroup="save"></asp:LinkButton></li>
                                </ul>
                            </div>
                        </div>
                        <hr class="nomargin" />
                        <div class="panel-search">
                            <div class="table-responsive">
                                <div class="entryformmain">
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            <span class="mandatory">*</span> Product Code :
                                        </div>
                                        <div class="controlstyle">
                                            <asp:TextBox ID="txtCode" CssClass="form-control" Width="200" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RFVCode" runat="server" ErrorMessage="Enter Product Code"
                                                ValidationGroup="save" Text="*" ControlToValidate="txtCode" SetFocusOnError="true"
                                                CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            <span class="mandatory">*</span> Product Name :
                                        </div>
                                        <div class="controlstyle">
                                            <asp:TextBox ID="txtProductName" CssClass="form-control" Width="300" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RFVProductName" runat="server"
                                                ErrorMessage="Enter Product Name" ValidationGroup="save" Text="*" ControlToValidate="txtProductName"
                                                SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="entryform" id="divTag" runat="server">
                                        <div class="labelstyle">
                                            <span class="mandatory">*</span> Product Tag :
                                        </div>
                                        <div class="controlstyle">
                                            <asp:TextBox ID="txtTag" CssClass="form-control" Width="300" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RFVTag" runat="server" ErrorMessage="Enter Product Tag"
                                                ValidationGroup="save" Text="*" ControlToValidate="txtTag" SetFocusOnError="true"
                                                CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            Is Active :
                                        </div>
                                        <div class="controlstyle">
                                            <asp:CheckBox ID="chkIsActiveProduct" runat="server" Checked="True" />
                                        </div>
                                    </div>
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            Has Color :
                                        </div>
                                        <div class="controlstyle">
                                            <div class="fleft">
                                                <asp:CheckBox ID="chkIsColor" runat="server" Checked="true" onchange="hascolor()" />
                                            </div>
                                            <div class="fleft" style="margin-left: 10px;">
                                                <div id="divColor">
                                                    <asp:DropDownList runat="server" ID="ddlColor" CssClass="form-control">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RFVColor" runat="server" ErrorMessage="Select Color"
                                                        ValidationGroup="save" Text="*" ControlToValidate="ddlColor" SetFocusOnError="true"
                                                        InitialValue="0" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="fclear">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            Has Size :
                                        </div>
                                        <div class="controlstyle">
                                            <asp:CheckBox ID="chkIsSize" runat="server" Checked="false" />
                                        </div>
                                    </div>
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            <span class="mandatory">*</span> Estimated Delivery Days :
                                        </div>
                                        <div class="controlstyle">
                                            <asp:TextBox ID="txtEstimatedDeliveryDays" CssClass="form-control" Width="300" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RFVEstimatedDeliveryDays" runat="server"
                                                ErrorMessage="Enter Estimated Delivery Days" ValidationGroup="save" Text="*"
                                                ControlToValidate="txtEstimatedDeliveryDays" SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            <span class="mandatory">*</span> Weight (gms) :
                                        </div>
                                        <div class="controlstyle">
                                            <asp:TextBox ID="txtWeight" CssClass="form-control" Width="300" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RFVWeight" runat="server" ErrorMessage="Enter Weight"
                                                ValidationGroup="save" Text="*" ControlToValidate="txtWeight" SetFocusOnError="true"
                                                CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator Display="Dynamic" ID="REVWeight" runat="server" ValidationGroup="save"
                                                Text="*" ControlToValidate="txtWeight" SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            Description :
                                        </div>
                                        <div class="controlstyle">
                                            <CKEditor:CKEditorControl ID="txtDescription" runat="server"></CKEditor:CKEditorControl>
                                        </div>
                                    </div>
                                    <%-- <div class="entryform">
                                        <div class="labelstyle">
                                            Wash-Care :
                                        </div>
                                        <div class="controlstyle">
                                            <CKEditor:CKEditorControl ID="txtWashCare" runat="server"></CKEditor:CKEditorControl>
                                        </div>
                                    </div>--%>
                                    <div class="page-inner-title" runat="server" visible="false">
                                        Product Type
                                    </div>
                                    <div class="entryform" runat="server" visible="false">
                                        <div class="labelstyle">
                                            Is New Arrival :
                                        </div>
                                        <div class="controlstyle">
                                            <asp:CheckBox ID="chkIsNewArrival" runat="server" Checked="false" />
                                        </div>
                                    </div>
                                    <div class="entryform" runat="server" visible="false">
                                        <div class="labelstyle">
                                            Is Featured :
                                        </div>
                                        <div class="controlstyle">
                                            <asp:CheckBox ID="ChkIsFeatured" runat="server" Checked="false" />
                                        </div>
                                    </div>
                                    <div class="entryform" runat="server" visible="false">
                                        <div class="labelstyle">
                                            Is Best Seller :
                                        </div>
                                        <div class="controlstyle">
                                            <asp:CheckBox ID="ChkIsBestSeller" runat="server" Checked="false" />
                                        </div>
                                    </div>
                                    <div class="page-inner-title">
                                        SEO Detail
                                    </div>
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            <span class="mandatory">*</span> MetaKey Word :
                                        </div>
                                        <div class="controlstyle">
                                            <asp:TextBox ID="txtMetaKeyWord" CssClass="form-control" Width="300" runat="server"
                                                TextMode="MultiLine"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RFVMetaKeyWord" runat="server"
                                                ErrorMessage="Enter MetaKey Word" ValidationGroup="save" Text="*" ControlToValidate="txtMetaKeyWord"
                                                SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            <span class="mandatory">*</span> Meta Description :
                                        </div>
                                        <div class="controlstyle">
                                            <asp:TextBox ID="txtMetaDescription" CssClass="form-control" Width="300" runat="server"
                                                TextMode="MultiLine"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RFVMetaDescription" runat="server"
                                                ErrorMessage="Enter Meta Description" ValidationGroup="save" Text="*" ControlToValidate="txtMetaDescription"
                                                SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="entryform" style="display: none;">
                                        <div class="labelstyle">
                                            <span class="mandatory">*</span> Browser Title :
                                        </div>
                                        <div class="controlstyle">
                                            <asp:TextBox ID="txtBrowserTitle" CssClass="form-control" Width="300" runat="server"></asp:TextBox>
                                            <%-- <asp:RequiredFieldValidator Display="Dynamic" ID="RFVBrowserTitle" runat="server"
                                                ErrorMessage="Enter Browser Title" ValidationGroup="save" Text="*" ControlToValidate="txtBrowserTitle"
                                                SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>--%>
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
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="page-inner-title">
                                                Sub Category List
                                            </div>
                                            <div class="table-responsive">
                                                <div class="panel-search">
                                                    <asp:DropDownList runat="server" ID="ddlSerachCategory" CssClass="form-control" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlSerachCategory_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <%--<div class="Separator">
                                                        &nbsp;
                                                    </div>
                                                    <asp:DropDownList runat="server" ID="ddlSerachSubCategory" CssClass="form-control"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlSerachSubCategory_SelectedIndexChanged">
                                                    </asp:DropDownList>--%>
                                                    <asp:Button ID="btnSubCategoryReset" runat="server" CssClass="btn btn-primary" Text="Reset"
                                                        OnClick="btnSubCategoryReset_Click" />
                                                </div>
                                                <hr class="nomargin" />
                                                <div class="panel-search">
                                                    <div class="fleft">
                                                        <asp:Button ID="btnSubCategoryDelete" runat="server" CssClass="btn btn-danger" Text="Delete"
                                                            OnClientClick="return ConfirmMessage('Sub Category','delete')" OnClick="btnSubCategoryDelete_Click" />
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
                                                    DataKeyNames="appProductSubCategoryID,appDisplayOrder,appIsActive" CssClass="table table-striped table-bordered table-hover"
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
                                                                <asp:LinkButton runat="server" ID="lnkEdit" CommandName="Edit" CommandArgument='<%#Eval("appProductSubCategoryID") %>'>
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
                                                                <asp:LinkButton runat="server" ID="lnkIsActive" CommandName="IsActive" CommandArgument='<%#Eval("appProductSubCategoryID") %>'>
                                                        <span class="action-icon set-icon <%# Eval("appIsActive").ToString().ToLower() == "true" ? "green" : "red" %>"><i class="fa <%# Eval("appIsActive").ToString().ToLower() == "true" ? "fa-check" : "fa-ban" %>"></i></span>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%-- <asp:TemplateField HeaderText="Display Order" ItemStyle-HorizontalAlign="Center"
                                                            ItemStyle-Width="1%" HeaderStyle-Width="1%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lnkUp" CommandName="Up" CommandArgument='<%#Eval("appProductSubCategoryID") %>'
                                                                    ToolTip="Up">
                                                        <span class="action-icon set-icon"><i class="fa fa-chevron-up"></i></span>
                                                                </asp:LinkButton>
                                                                <asp:LinkButton runat="server" ID="lnkDown" CommandName="Down" CommandArgument='<%#Eval("appProductSubCategoryID") %>'
                                                                    ToolTip="Down">
                                                        <span class="action-icon set-icon"><i class="fa fa-chevron-down"></i></span>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <asp:HiddenField ID="hdnSubCategoryId" runat="server" />
                                <asp:ValidationSummary ID="vsGallery" ShowMessageBox="true" EnableClientScript="true"
                                    HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                                    ValidationGroup="SubCategory" />
                            </ContentTemplate>
                            <Triggers>
                                <%--<asp:PostBackTrigger ControlID="btnSaveSubCategory" />--%>
                                <asp:PostBackTrigger ControlID="btnSubCategoryDelete" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div class="tab-pane fade  in" id="tabs-3">
                        <%--<asp:UpdatePanel ID="UPdateImage" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>--%>
                        <%--  <div class="panel-search" id="divColorControl" runat="server">
                            <div class="table-responsive">
                                <DInfo:DisplayInfo runat="server" ID="DInfoProductColor" />
                                <div class="entryformmain">
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            <span class="mandatory">*</span>Color :
                                        </div>
                                        <div class="controlstyle">
                                            <asp:DropDownList runat="server" ID="ddlColor" CssClass="form-control">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RFVColor" runat="server" ErrorMessage="Select Color"
                                                ValidationGroup="ProductColor" Text="*" ControlToValidate="ddlColor" SetFocusOnError="true"
                                                InitialValue="0" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            Is Default :
                                        </div>
                                        <div class="controlstyle">
                                            <asp:CheckBox ID="chkProductColorIsDefault" runat="server" Checked="false" />
                                        </div>
                                    </div>
                                    <div class="entryform">
                                        <div class="labelstyle">
                                            Is Active :
                                        </div>
                                        <div class="controlstyle">
                                            <asp:CheckBox ID="chkProductColorIsActive" runat="server" Checked="True" />
                                        </div>
                                    </div>
                                    <div class="entryform">
                                        <div class="labelstyle">
                                        </div>
                                        <div class="controlstyle">
                                            <asp:Button runat="server" ID="btnSaveProductColor" class="btn btn-primary" Text="Save"
                                                ValidationGroup="ProductColor" OnClick="btnSaveProductColor_Click"></asp:Button>
                                            <asp:Button runat="server" ID="btnClearProductColor" class="btn btn-primary" Text="Clear"
                                                OnClick="btnClearProductColor_Click"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>--%>
                        <div class="panel-search">
                            <%--  <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>--%>
                            <%--<div class="page-inner-title">
                                Product Color List
                            </div>--%>
                            <div class="table-responsive">
                                <%--    <div id="divColorSerach" runat="server">
                                    <div class="panel-search">
                                        <asp:DropDownList runat="server" ID="ddlSearchColor" CssClass="form-control" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSearchColor_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:Button ID="btnProductColorReset" runat="server" CssClass="btn btn-primary" Text="Reset"
                                            OnClick="btnProductColorReset_Click" />
                                    </div>
                                    <hr class="nomargin" />
                                    <div class="panel-search">
                                        <div class="fleft">
                                            <asp:Button ID="btnProductColorDelete" runat="server" CssClass="btn btn-danger" Text="Delete"
                                                OnClientClick="return ConfirmMessage('Product Color','delete')" OnClick="btnProductColorDelete_Click" />
                                        </div>
                                        <div class="fright">
                                            Total&nbsp;Records&nbsp;:&nbsp; <span class="RecordCount">
                                                <asp:Label ID="lblProductColorCount" runat="server" Text="0"> </asp:Label>
                                            </span>
                                            <div class="Separator">
                                                &nbsp;
                                            </div>
                                            Per Page :
                                            <asp:DropDownList ID="ddlProductColorPerPage" runat="server" AutoPostBack="true"
                                                CssClass="form-control" OnSelectedIndexChanged="ddlProductColorPerPage_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="fclear">
                                        </div>
                                    </div>
                                    <hr class="nomargin" />
                                </div>--%>
                                <DInfo:DisplayInfo runat="server" ID="DinfoProductColorData" />
                                <asp:GridView ID="dgvProductColor" Width="100%" runat="server" AutoGenerateColumns="False"
                                    DataKeyNames="appProductColorID,appDisplayOrder,appIsActive" CssClass="table table-striped table-bordered table-hover"
                                    HeaderStyle-Wrap="false" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
                                    PagerSettings-Mode="NumericFirstLast" PageSize="10" OnPageIndexChanging="dgvProductColor_PageIndexChanging"
                                    OnRowCommand="dgvProductColor_RowCommand" OnRowCreated="dgvProductColor_RowCreated"
                                    OnRowDataBound="dgvProductColor_RowDataBound" OnSorting="dgvProductColor_Sorting"
                                    OnRowDeleting="dgvProductColor_RowDeleting" OnRowEditing="dgvProductColor_RowEditing">
                                    <PagerSettings Position="Top" />
                                    <Columns>
                                        <%--<asp:TemplateField HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkHeader" runat="server" onclick="SelectAll(this,'dgvProductColor');" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelectRow" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <%-- <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                            HeaderStyle-Width="1%">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEdit" CommandName="Edit" CommandArgument='<%#Eval("appProductColorID") %>'>
                                                                        <span class="action-icon set-icon"><i class="fa fa-pencil"></i></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Color" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                            HeaderStyle-Width="1%">
                                            <ItemTemplate>
                                                <asp:Image ID="img" runat="server" ImageUrl='<%#Eval("appColorImage") %>' AlternateText='<%#Eval("appColorName") %>' />
                                                <div>
                                                    <%#Eval("appColorName")%></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="File Upload" ItemStyle-HorizontalAlign="left" ItemStyle-Width="60%"
                                            HeaderStyle-Width="60%">
                                            <ItemTemplate>
                                                <asp:FileUpload ID="fileUpload" runat="server" multiple="multiple" />
                                                ( preferable Image Size should be Width:900Px & Height:1200px )
                                                <asp:Button ID="btn" runat="server" CssClass="btn btn-primary" CommandName="SaveImg"
                                                    CommandArgument='<%#Eval("appProductColorID") %>' Text="Save" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Image" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                            HeaderStyle-Width="1%">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkImage" CommandName="Image" CommandArgument='<%#Eval("appProductColorID") %>'>
                                                        <span class="action-icon set-icon"><%--<i class="fa fa-briefcase "></i>--%> <%#Eval("appTotalImg")%></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Is Default" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                            HeaderStyle-Width="1%">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkIsDefault" CommandName="IsDefault" CommandArgument='<%#Eval("appProductColorID") %>'>
                                                        <span class="action-icon set-icon <%# Eval("appIsDefault").ToString().ToLower() == "true" ? "green" : "red" %>"><i class="fa <%# Eval("appIsDefault").ToString().ToLower() == "true" ? "fa-check" : "fa-ban" %>"></i></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Is Active" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                            HeaderStyle-Width="1%">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkIsActive" CommandName="IsActive" CommandArgument='<%#Eval("appProductColorID") %>'>
                                                        <span class="action-icon set-icon <%# Eval("appIsActive").ToString().ToLower() == "true" ? "green" : "red" %>"><i class="fa <%# Eval("appIsActive").ToString().ToLower() == "true" ? "fa-check" : "fa-ban" %>"></i></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <%--  <asp:TemplateField HeaderText="Display Order" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="1%" HeaderStyle-Width="1%">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkUp" CommandName="Up" CommandArgument='<%#Eval("appProductColorID") %>'
                                                    ToolTip="Up">
                                                        <span class="action-icon set-icon"><i class="fa fa-chevron-up"></i></span>
                                                </asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="lnkDown" CommandName="Down" CommandArgument='<%#Eval("appProductColorID") %>'
                                                    ToolTip="Down">
                                                        <span class="action-icon set-icon"><i class="fa fa-chevron-down"></i></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <%--</ContentTemplate>
                                    </asp:UpdatePanel>--%>
                        </div>
                        <asp:HiddenField ID="hdnProductColorId" runat="server" />
                        <asp:ValidationSummary ID="vsBanner" ShowMessageBox="true" EnableClientScript="true"
                            HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                            ValidationGroup="ProductColor" />
                        <input type="button" runat="server" id="btnProductImage" style="display: none;" />
                        <cc1:ModalPopupExtender ID="mpeProductImage" runat="server" TargetControlID="btnProductImage"
                            PopupControlID="divProductImage" BackgroundCssClass="modalbackground" DropShadow="false"
                            CancelControlID="imgBtnImgClose">
                        </cc1:ModalPopupExtender>
                        <div id="divProductImage" class="modalpopup_ panel panel-default" runat="server"
                            style="display: none; width: 600px;">
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>
                                    <asp:LinkButton ID="imgBtnImgClose" CssClass="modalbclose" runat="server" OnClick="imgBtnImgClose_Click">
                                        <span class="action-icon set-icon"><i class="fa fa-minus"></i></span>
                                    </asp:LinkButton>
                                    <div class="modalheader_ panel-heading">
                                        Product Image List
                                    </div>
                                    <div class="modaldetail">
                                        <%--<div class="panel-search right-content">
                                                            <asp:Button ID="BtnSaveImage" runat="server" CssClass="btn btn-primary" Text="Save"
                                                                ValidationGroup="ProductImage" OnClick="BtnSaveImage_Click" />
                                                        </div>--%>
                                        <%--   <hr class="nomargin" />
                                                        <div class="panel-search">
                                                            <div class="table-responsive">
                                                                <DInfo:DisplayInfo ID="DinfoProductImage" runat="server" />
                                                                <div class="entryformmain">
                                                                    <div class="entryform">
                                                                        <div class="labelstyle">
                                                                            <span class="mandatory">*</span> Image :
                                                                        </div>
                                                                        <div class="controlstyle">
                                                                            <asp:FileUpload ID="FileProductImg" runat="server" onchange="ImgUpload(this);" />
                                                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RFVFile" runat="server" ErrorMessage="Enter Date"
                                                                                ValidationGroup="ProductImage" Text="*" ControlToValidate="FileProductImg" SetFocusOnError="true"
                                                                                CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="entryform">
                                                                        <div class="labelstyle">
                                                                            Is Default :
                                                                        </div>
                                                                        <div class="controlstyle">
                                                                            <asp:CheckBox ID="ChkProductImageIsDefault" runat="server" Checked="false" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="entryform">
                                                                        <div class="labelstyle">
                                                                            Is Active :
                                                                        </div>
                                                                        <div class="controlstyle">
                                                                            <asp:CheckBox ID="chkProductImageIsActive" runat="server" Checked="True" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>--%>
                                        <hr class="nomargin" />
                                        <div class="panel-search">
                                            <%-- <div class="page-inner-title">
                                                                Product Color List
                                                            </div>--%>
                                            <div class="table-responsive" style="height: 400px; overflow: auto;">
                                                <div class="panel-search">
                                                    <div class="fleft">
                                                        <asp:Button ID="btnProductImageDelete" runat="server" CssClass="btn btn-danger" Text="Delete"
                                                            OnClientClick="return ConfirmMessage('Product Image','delete')" OnClick="btnProductImageDelete_Click" />
                                                    </div>
                                                    <div class="fright">
                                                        Total&nbsp;Records&nbsp;:&nbsp; <span class="RecordCount">
                                                            <asp:Label ID="lblProductImageCount" runat="server" Text="0"> </asp:Label>
                                                        </span>
                                                        <div class="Separator">
                                                            &nbsp;
                                                        </div>
                                                        Per Page :
                                                        <asp:DropDownList ID="ddlProductImagePerPage" runat="server" AutoPostBack="true"
                                                            CssClass="form-control" OnSelectedIndexChanged="ddlProductImagePerPage_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="fclear">
                                                    </div>
                                                </div>
                                                <hr class="nomargin" />
                                                <DInfo:DisplayInfo runat="server" ID="DInfProductImageData" />
                                                <asp:GridView ID="dgvProductImage" Width="100%" runat="server" AutoGenerateColumns="False"
                                                    DataKeyNames="appProductImageID,appDisplayOrder,appIsActive" CssClass="table table-striped table-bordered table-hover"
                                                    HeaderStyle-Wrap="false" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
                                                    PagerSettings-Mode="NumericFirstLast" PageSize="10" OnPageIndexChanging="dgvProductImage_PageIndexChanging"
                                                    OnRowCommand="dgvProductImage_RowCommand" OnRowCreated="dgvProductImage_RowCreated"
                                                    OnRowDataBound="dgvProductImage_RowDataBound" OnSorting="dgvProductImage_Sorting"
                                                    OnRowDeleting="dgvProductImage_RowDeleting" OnRowEditing="dgvProductImage_RowEditing">
                                                    <PagerSettings Position="Top" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkHeader" runat="server" onclick="SelectAll(this,'dgvProductImage');" />
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
                                                        <asp:TemplateField HeaderText="Is Default" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                            HeaderStyle-Width="1%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lnkIsDefault" CommandName="IsDefault" CommandArgument='<%#Eval("appProductImageID") %>'>
                                                        <span class="action-icon set-icon <%# Eval("appIsDefault").ToString().ToLower() == "true" ? "green" : "red" %>"><i class="fa <%# Eval("appIsDefault").ToString().ToLower() == "true" ? "fa-check" : "fa-ban" %>"></i></span>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Is Active" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                            HeaderStyle-Width="1%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lnkIsActive" CommandName="IsActive" CommandArgument='<%#Eval("appProductImageID") %>'>
                                                        <span class="action-icon set-icon <%# Eval("appIsActive").ToString().ToLower() == "true" ? "green" : "red" %>"><i class="fa <%# Eval("appIsActive").ToString().ToLower() == "true" ? "fa-check" : "fa-ban" %>"></i></span>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Display Order" ItemStyle-HorizontalAlign="Center"
                                                            ItemStyle-Width="1%" HeaderStyle-Width="1%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lnkUp" CommandName="Up" CommandArgument='<%#Eval("appProductImageID") %>'
                                                                    ToolTip="Up">
                                                        <span class="action-icon set-icon"><i class="fa fa-chevron-up"></i></span>
                                                                </asp:LinkButton>
                                                                <asp:LinkButton runat="server" ID="lnkDown" CommandName="Down" CommandArgument='<%#Eval("appProductImageID") %>'
                                                                    ToolTip="Down">
                                                        <span class="action-icon set-icon"><i class="fa fa-chevron-down"></i></span>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <asp:ValidationSummary ID="VsImages" ShowMessageBox="true" EnableClientScript="true"
                                            HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                                            ValidationGroup="ProductImage" />
                                        <asp:HiddenField ID="hdnCurrentProductColorId" runat="server" />
                                    </div>
                                    <asp:HiddenField ID="hdnFilePath" Value="" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <%--    </ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </div>
                    <div class="tab-pane fade  in" id="tabs-4">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <div class="panel-search">
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                        <ContentTemplate>
                                            <div class="table-responsive">
                                                <DInfo:DisplayInfo runat="server" ID="DInfoColorDetail" />
                                                <div class="entryformmain">
                                                    <div class="entryform" id="divddlcolor" runat="server">
                                                        <div class="labelstyle">
                                                            <span class="mandatory">*</span> Color :
                                                        </div>
                                                        <div class="controlstyle">
                                                            <asp:DropDownList runat="server" ID="ddlColorDetail" CssClass="form-control" AutoPostBack="false">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RFVColorDetail" runat="server"
                                                                ErrorMessage="Select Color" ValidationGroup="ColorDetail" Text="*" ControlToValidate="ddlColorDetail"
                                                                SetFocusOnError="true" InitialValue="0" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="entryform" id="divddlSize" runat="server">
                                                        <div class="labelstyle">
                                                            <span class="mandatory">*</span> Size :
                                                        </div>
                                                        <div class="controlstyle">
                                                            <asp:DropDownList runat="server" ID="ddlSize" CssClass="form-control">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RFVSize" runat="server" ErrorMessage="Select Size"
                                                                ValidationGroup="ColorDetail" Text="*" ControlToValidate="ddlSize" SetFocusOnError="true"
                                                                InitialValue="0" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <%--  <div class="entryform">
                                                        <div class="labelstyle">
                                                            <span class="mandatory">*</span> Seller Price :
                                                        </div>
                                                        <div class="controlstyle">
                                                            <asp:TextBox ID="txtSellerPrice" CssClass="form-control" Width="200" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RFVSellerPrice" runat="server"
                                                                ErrorMessage="Enter Seller Price " ValidationGroup="ColorDetail" Text="*" ControlToValidate="txtSellerPrice"
                                                                SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator Display="Dynamic" ID="REVSellerPrice" runat="server"
                                                                ValidationGroup="ColorDetail" Text="*" ControlToValidate="txtSellerPrice" SetFocusOnError="true"
                                                                CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>--%>
                                                    <div class="entryform">
                                                        <div class="labelstyle">
                                                            <span class="mandatory">*</span> MRP :
                                                        </div>
                                                        <div class="controlstyle">
                                                            <asp:TextBox ID="txtMRP" CssClass="form-control" Width="200" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RFVMRP" runat="server" ErrorMessage="Enter MRP"
                                                                ValidationGroup="ColorDetail" Text="*" ControlToValidate="txtMRP" SetFocusOnError="true"
                                                                CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator Display="Dynamic" ID="REVMRP" runat="server" ValidationGroup="ColorDetail"
                                                                Text="*" ControlToValidate="txtMRP" SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                    <div class="entryform">
                                                        <div class="labelstyle">
                                                            <span class="mandatory">*</span> Price :
                                                        </div>
                                                        <div class="controlstyle">
                                                            <asp:TextBox ID="txtPrice" CssClass="form-control" Width="200" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RFVPrice" runat="server" ErrorMessage="Enter Price"
                                                                ValidationGroup="ColorDetail" Text="*" ControlToValidate="txtPrice" SetFocusOnError="true"
                                                                CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator Display="Dynamic" ID="REVPrice" runat="server" ValidationGroup="ColorDetail"
                                                                Text="*" ControlToValidate="txtPrice" SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                    <div class="entryform">
                                                        <div class="labelstyle">
                                                            <span class="mandatory">*</span> Quantity :
                                                        </div>
                                                        <div class="controlstyle">
                                                            <asp:TextBox ID="txtQuantity" CssClass="form-control" Width="200" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RFVQuantity" runat="server" ErrorMessage="Enter Quantity"
                                                                ValidationGroup="ColorDetail" Text="*" ControlToValidate="txtQuantity" SetFocusOnError="true"
                                                                CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator Display="Dynamic" ID="REVQuantity" runat="server"
                                                                ValidationGroup="ColorDetail" Text="*" ControlToValidate="txtQuantity" SetFocusOnError="true"
                                                                CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                    <div class="entryform">
                                                        <div class="labelstyle">
                                                            SKUNo :
                                                        </div>
                                                        <div class="controlstyle">
                                                            <asp:TextBox ID="txtSKUNo" CssClass="form-control" Width="200" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="entryform" id="divDetailIsDefault" runat="server">
                                                        <div class="labelstyle">
                                                            Is Default :
                                                        </div>
                                                        <div class="controlstyle">
                                                            <asp:CheckBox ID="ChkProductDetailIsDefault" runat="server" Checked="false" />
                                                        </div>
                                                    </div>
                                                    <div class="entryform">
                                                        <div class="labelstyle">
                                                        </div>
                                                        <div class="controlstyle">
                                                            <asp:Button runat="server" ID="btnSaveColorDetail" class="btn btn-primary" Text="Save"
                                                                ValidationGroup="ColorDetail" OnClick="btnSaveColorDetail_Click"></asp:Button>
                                                            <asp:Button runat="server" ID="btnClearColorDetail" class="btn btn-primary" Text="Clear"
                                                                OnClick="btnClearColorDetail_Click"></asp:Button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="panel-search">
                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                        <ContentTemplate>
                                            <div class="page-inner-title">
                                                Color Detail List
                                            </div>
                                            <div class="table-responsive">
                                                <%-- <div class="panel-search">
                                                    <asp:DropDownList runat="server" ID="ddlSerachColorDetail" CssClass="form-control"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlSerachColorDetail_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:Button ID="btnColorDetailReset" runat="server" CssClass="btn btn-primary" Text="Reset"
                                                        OnClick="btnColorDetailReset_Click" />
                                                </div>
                                                <hr class="nomargin" />--%>
                                                <div class="panel-search">
                                                    <div class="fleft">
                                                        <asp:Button ID="btnColorDetailDelete" runat="server" CssClass="btn btn-danger" Text="Delete"
                                                            OnClientClick="return ConfirmMessage('Product Detail','delete')" OnClick="btnColorDetailDelete_Click" />
                                                    </div>
                                                    <div class="fright">
                                                        Total&nbsp;Records&nbsp;:&nbsp; <span class="RecordCount">
                                                            <asp:Label ID="lblColorDetailCount" runat="server" Text="0"> </asp:Label>
                                                        </span>
                                                        <div class="Separator">
                                                            &nbsp;
                                                        </div>
                                                        Per Page :
                                                        <asp:DropDownList ID="ddlColorDetailPerPage" runat="server" AutoPostBack="true" CssClass="form-control"
                                                            OnSelectedIndexChanged="ddlColorDetailPerPage_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="fclear">
                                                    </div>
                                                </div>
                                                <hr class="nomargin" />
                                                <DInfo:DisplayInfo runat="server" ID="DInfoColorDetailData" />
                                                <asp:GridView ID="dgvColorDetail" Width="100%" runat="server" AutoGenerateColumns="False"
                                                    DataKeyNames="appProductDetailID" CssClass="table table-striped table-bordered table-hover"
                                                    HeaderStyle-Wrap="false" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
                                                    PagerSettings-Mode="NumericFirstLast" PageSize="10" OnPageIndexChanging="dgvColorDetail_PageIndexChanging"
                                                    OnRowCommand="dgvColorDetail_RowCommand" OnRowCreated="dgvColorDetail_RowCreated"
                                                    OnRowDataBound="dgvColorDetail_RowDataBound" OnSorting="dgvColorDetail_Sorting"
                                                    OnRowDeleting="dgvColorDetail_RowDeleting" OnRowEditing="dgvColorDetail_RowEditing">
                                                    <PagerSettings Position="Top" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkHeader" runat="server" onclick="SelectAll(this,'dgvColorDetail');" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelectRow" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                            HeaderStyle-Width="1%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lnkEdit" CommandName="Edit" CommandArgument='<%#Eval("appProductDetailID") %>'>
                                                                        <span class="action-icon set-icon"><i class="fa fa-pencil"></i></span>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Color" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                            HeaderStyle-Width="1%">
                                                            <ItemTemplate>
                                                                <asp:Image ID="img1" runat="server" ImageUrl='<%#Eval("appColorImage") %>' AlternateText='<%#Eval("appColorName") %>' />
                                                                <div>
                                                                    <%#Eval("appColorName")%></div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%-- <asp:TemplateField HeaderText="Seller Price" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%"
                                                            HeaderStyle-Width="40%" SortExpression="appSellerPrice">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSellerPrice" runat="server" Text='<%#Eval("appSellerPrice") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="MRP" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%"
                                                            HeaderStyle-Width="40%" SortExpression="appMRP">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMRP" runat="server" Text='<%#Eval("appMRP") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Price" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%"
                                                            HeaderStyle-Width="40%" SortExpression="appPrice">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPrice" runat="server" Text='<%#Eval("appPrice") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Quantity" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%"
                                                            HeaderStyle-Width="40%" SortExpression="appQuantity">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblQuantity" runat="server" Text='<%#Eval("appQuantity") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SKUNo" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%"
                                                            HeaderStyle-Width="40%" SortExpression="appSKUNo">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSKUNo" runat="server" Text='<%#Eval("appSKUNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%"
                                                            HeaderStyle-Width="40%" SortExpression="appSize">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSize" runat="server" Text='<%#Eval("appSize") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Is Default" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                            HeaderStyle-Width="1%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lnkIsDefault" CommandName="IsDefault" CommandArgument='<%#Eval("appProductDetailID") %>'>
                                                        <span class="action-icon set-icon <%# Eval("appIsDefault").ToString().ToLower() == "true" ? "green" : "red" %>"><i class="fa <%# Eval("appIsDefault").ToString().ToLower() == "true" ? "fa-check" : "fa-ban" %>"></i></span>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <asp:HiddenField ID="hdnProductDetailID" runat="server" />
                                <asp:ValidationSummary ID="vsColorDetail" ShowMessageBox="true" EnableClientScript="true"
                                    HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                                    ValidationGroup="ColorDetail" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="tab-pane fade in" id="tabs-5">
                        <div class="panel-search">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <%-- <div class="page-inner-title">
                                        Product Property List
                                    </div>--%>
                                    <div class="panel-search right-content">
                                        <asp:Button ID="btnSaveProperty" runat="server" CssClass="btn btn-primary" Text="Save"
                                            OnClick="btnSaveProperty_Click" />
                                    </div>
                                    <hr class="nomargin" />
                                    <div class="table-responsive">
                                        <DInfo:DisplayInfo runat="server" ID="DInfoProductProperty" />
                                        <asp:GridView ID="dgvProductProperty" Width="100%" runat="server" AutoGenerateColumns="False"
                                            DataKeyNames="appPropertyID,appIsPredefine,appPropertyPreValueID" CssClass="table table-striped table-bordered table-hover"
                                            HeaderStyle-Wrap="false" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
                                            PagerSettings-Mode="NumericFirstLast" OnRowDataBound="dgvProductProperty_RowDataBound">
                                            <PagerSettings Position="Top" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Property" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%"
                                                    HeaderStyle-Width="40%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProperty" runat="server" Text='<%#Eval("appPropertyName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="value" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%"
                                                    HeaderStyle-Width="40%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtValue" runat="server" Text='<%#Eval("appValue") %>'></asp:TextBox>
                                                        <asp:DropDownList ID="ddlValue" runat="server">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="tab-pane fade  in" id="tabs-6">
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <div class="panel-search">
                                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                        <ContentTemplate>
                                            <div class="table-responsive">
                                                <DInfo:DisplayInfo runat="server" ID="DRelatedInfo" />
                                                <div class="entryformmain">
                                                    <div class="entryform">
                                                        <div class="labelstyle">
                                                            <span class="mandatory">*</span> Category :
                                                        </div>
                                                        <div class="controlstyle">
                                                            <asp:DropDownList runat="server" ID="ddlRelatedCategory" CssClass="form-control"
                                                                AutoPostBack="true" OnSelectedIndexChanged="ddlRelatedCategory_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="entryform">
                                                        <div class="labelstyle">
                                                            <span class="mandatory">*</span>Sub Category :
                                                        </div>
                                                        <div class="controlstyle">
                                                            <asp:DropDownList runat="server" ID="ddlRelatedSubCategory" AutoPostBack="true" CssClass="form-control"
                                                                OnSelectedIndexChanged="ddlRelatedSubCategory_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="entryform">
                                                        <DInfo:DisplayInfo runat="server" ID="DInfoUnSelected" />
                                                        <asp:GridView ID="dgvUnSelected" Width="100%" runat="server" AutoGenerateColumns="False"
                                                            DataKeyNames="appProductID" CssClass="table table-striped table-bordered table-hover"
                                                            HeaderStyle-Wrap="false" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
                                                            PagerSettings-Mode="NumericFirstLast" PageSize="10" OnRowCommand="dgvUnSelected_RowCommand">
                                                            <PagerSettings Position="Top" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Images" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                                    HeaderStyle-Width="1%">
                                                                    <ItemTemplate>
                                                                        <asp:Image ID="img" runat="server" ImageUrl='<%#Eval("appThumbImage") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Product" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%"
                                                                    HeaderStyle-Width="40%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblProductName" runat="server" Text='<%#Eval("appProductName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Add" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                                    HeaderStyle-Width="1%">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" ID="lnkIsAdd" CommandName="IsAdd" CommandArgument='<%#Eval("appProductID") %>'>
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
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                        <ContentTemplate>
                                            <div class="page-inner-title">
                                                Related Product List
                                            </div>
                                            <div class="table-responsive">
                                                <div class="panel-search">
                                                    <div class="fleft">
                                                        <asp:Button ID="btnRelatedDelete" runat="server" CssClass="btn btn-danger" Text="Delete"
                                                            OnClientClick="return ConfirmMessage('Related Product','delete')" OnClick="btnRelatedDelete_Click" />
                                                    </div>
                                                    <div class="fright">
                                                        Total&nbsp;Records&nbsp;:&nbsp; <span class="RecordCount">
                                                            <asp:Label ID="lblRelatedCount" runat="server" Text="0"> </asp:Label>
                                                        </span>
                                                        <div class="Separator">
                                                            &nbsp;
                                                        </div>
                                                        Per Page :
                                                        <asp:DropDownList ID="ddlRelatedPerPage" runat="server" AutoPostBack="true" CssClass="form-control"
                                                            OnSelectedIndexChanged="ddlRelatedPerPage_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="fclear">
                                                    </div>
                                                </div>
                                                <hr class="nomargin" />
                                                <DInfo:DisplayInfo runat="server" ID="DInfoRelatedProduct" />
                                                <asp:GridView ID="dgvRelatedProduct" Width="100%" runat="server" AutoGenerateColumns="False"
                                                    DataKeyNames="appRelatedProductID,appDisplayOrder,appIsActive" CssClass="table table-striped table-bordered table-hover"
                                                    HeaderStyle-Wrap="false" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
                                                    PagerSettings-Mode="NumericFirstLast" PageSize="10" OnPageIndexChanging="dgvRelatedProduct_PageIndexChanging"
                                                    OnRowCommand="dgvRelatedProduct_RowCommand" OnRowCreated="dgvRelatedProduct_RowCreated"
                                                    OnRowDataBound="dgvRelatedProduct_RowDataBound" OnSorting="dgvRelatedProduct_Sorting"
                                                    OnRowDeleting="dgvRelatedProduct_RowDeleting" OnRowEditing="dgvRelatedProduct_RowEditing">
                                                    <PagerSettings Position="Top" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkHeader" runat="server" onclick="SelectAll(this,'dgvRelatedProduct');" />
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
                                                        <asp:TemplateField HeaderText="Product" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%"
                                                            HeaderStyle-Width="40%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProductName" runat="server" Text='<%#Eval("appProductName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Is Active" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                            HeaderStyle-Width="1%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lnkIsActive" CommandName="IsActive" CommandArgument='<%#Eval("appRelatedProductID") %>'>
                                                        <span class="action-icon set-icon <%# Eval("appIsActive").ToString().ToLower() == "true" ? "green" : "red" %>"><i class="fa <%# Eval("appIsActive").ToString().ToLower() == "true" ? "fa-check" : "fa-ban" %>"></i></span>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Display Order" ItemStyle-HorizontalAlign="Center"
                                                            ItemStyle-Width="1%" HeaderStyle-Width="1%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lnkUp" CommandName="Up" CommandArgument='<%#Eval("appRelatedProductID") %>'
                                                                    ToolTip="Up">
                                                        <span class="action-icon set-icon"><i class="fa fa-chevron-up"></i></span>
                                                                </asp:LinkButton>
                                                                <asp:LinkButton runat="server" ID="lnkDown" CommandName="Down" CommandArgument='<%#Eval("appRelatedProductID") %>'
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
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" EnableClientScript="true"
                                    HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                                    ValidationGroup="SubCategory" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="tab-pane fade  in" id="tabs-7">
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                            <ContentTemplate>
                                <div class="panel-search">
                                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                        <ContentTemplate>
                                            <div class="table-responsive">
                                                <DInfo:DisplayInfo runat="server" ID="DInfoPixelCode" />
                                                <div class="entryformmain">
                                                    <div class="entryform">
                                                        <div class="labelstyle">
                                                            <span class="mandatory">*</span> Name :
                                                        </div>
                                                        <div class="controlstyle">
                                                            <asp:TextBox ID="txtName" CssClass="form-control" Width="200" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RFVPixelName" runat="server" ErrorMessage="Enter Pixel Name "
                                                                ValidationGroup="PixelCode" Text="*" ControlToValidate="txtName" SetFocusOnError="true"
                                                                CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="entryform">
                                                        <div class="labelstyle">
                                                            Comment :
                                                        </div>
                                                        <div class="controlstyle">
                                                            <asp:TextBox ID="txtComment" CssClass="form-control" Width="200" runat="server" TextMode="MultiLine"
                                                                Rows="4"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="entryform" id="div4" runat="server">
                                                        <div class="labelstyle">
                                                            Is Active :
                                                        </div>
                                                        <div class="controlstyle">
                                                            <asp:CheckBox ID="chkPixelIsActive" runat="server" Checked="true" />
                                                        </div>
                                                    </div>
                                                    <div class="entryform">
                                                        <div class="labelstyle">
                                                        </div>
                                                        <div class="controlstyle">
                                                            <asp:Button runat="server" ID="btnSavePixelCode" class="btn btn-primary" Text="Save"
                                                                ValidationGroup="PixelCode" OnClick="btnSavePixelCode_Click"></asp:Button>
                                                            <asp:Button runat="server" ID="btnClearPixelCode" class="btn btn-primary" Text="Clear"
                                                                OnClick="btnClearPixelCode_Click"></asp:Button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="panel-search">
                                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                        <ContentTemplate>
                                            <div class="page-inner-title">
                                                Pixel Code List
                                            </div>
                                            <div class="table-responsive">
                                                <div class="panel-search">
                                                    <div class="fleft">
                                                        <asp:Button ID="btnPixelCodeDelete" runat="server" CssClass="btn btn-danger" Text="Delete"
                                                            OnClientClick="return ConfirmMessage('Pixel Code','delete')" OnClick="btnPixelCodeDelete_Click" />
                                                    </div>
                                                    <div class="fright">
                                                        Total&nbsp;Records&nbsp;:&nbsp; <span class="RecordCount">
                                                            <asp:Label ID="lblPixelCode" runat="server" Text="0"> </asp:Label>
                                                        </span>
                                                        <div class="Separator">
                                                            &nbsp;
                                                        </div>
                                                        Per Page :
                                                        <asp:DropDownList ID="ddlPixelCode" runat="server" AutoPostBack="true" CssClass="form-control"
                                                            OnSelectedIndexChanged="ddlPixelCode_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="fclear">
                                                    </div>
                                                </div>
                                                <hr class="nomargin" />
                                                <DInfo:DisplayInfo runat="server" ID="DInfoPixelCodeGrid" />
                                                <asp:GridView ID="dgvPixelCode" Width="100%" runat="server" AutoGenerateColumns="False"
                                                    DataKeyNames="appPixcelCodeID" CssClass="table table-striped table-bordered table-hover"
                                                    HeaderStyle-Wrap="false" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
                                                    PagerSettings-Mode="NumericFirstLast" PageSize="10" OnPageIndexChanging="dgvPixelCode_PageIndexChanging"
                                                    OnRowCommand="dgvPixelCode_RowCommand" OnRowCreated="dgvPixelCode_RowCreated"
                                                    OnRowDataBound="dgvPixelCode_RowDataBound" OnSorting="dgvPixelCode_Sorting" OnRowDeleting="dgvPixelCode_RowDeleting"
                                                    OnRowEditing="dgvPixelCode_RowEditing">
                                                    <PagerSettings Position="Top" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkHeader" runat="server" onclick="SelectAll(this,'dgvColorDetail');" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelectRow" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                            HeaderStyle-Width="1%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lnkEdit" CommandName="Edit" CommandArgument='<%#Eval("appPixcelCodeID") %>'>
                                                                        <span class="action-icon set-icon"><i class="fa fa-pencil"></i></span>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%"
                                                            HeaderStyle-Width="40%" SortExpression="appName">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblName" runat="server" Text='<%#Eval("appName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Comment" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%"
                                                            HeaderStyle-Width="40%" SortExpression="appComment">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblComment" runat="server" Text='<%#Eval("appComment") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Is Active" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                            HeaderStyle-Width="1%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lnkIsActive" CommandName="IsActive" CommandArgument='<%#Eval("appPixcelCodeID") %>'>
                                                        <span class="action-icon set-icon <%# Eval("appIsActive").ToString().ToLower() == "true" ? "green" : "red" %>"><i class="fa <%# Eval("appIsActive").ToString().ToLower() == "true" ? "fa-check" : "fa-ban" %>"></i></span>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <asp:HiddenField ID="hdnPixcelCodeID" runat="server" />
                                <asp:ValidationSummary ID="ValidationSummary2" ShowMessageBox="true" EnableClientScript="true"
                                    HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                                    ValidationGroup="PixelCode" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
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
</asp:Content>
