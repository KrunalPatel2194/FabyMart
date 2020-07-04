<%@ Page Title="Property Detail" Language="C#" MasterPageFile="~/admin/Main.master"
    AutoEventWireup="true" CodeFile="CouponCodeDetail.aspx.cs" Inherits="CouponCodeDetail
    " %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Admin/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function ShowTab() {
            $("#tabs-2").find('*').css('visibility', 'visible');
            $("#liCouponCodeDetail").show();
            ValidationGroupEnable();

        }
        function HideTab() {
            $("#tabs-2").find('*').css('visibility', 'hidden');
            $("#liCouponCodeDetail").hide();
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
            if (document.getElementById("<%=hdnTabID.ClientID%>").value != "") {
                document.getElementById(document.getElementById("<%=hdnTabID.ClientID%>").value).click();
            }
        }
        function ddlChange(ddlId) {
            var ControlName = document.getElementById(ddlId.id);
            var id = $("#<%=hdnPKID.ClientID%>").val();
            if (id != "") {
                var result = confirm("if You Change Type so Related Item Can be remove ?");
                if (result) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                return false;
            }
        }

        function Onload() {
            try {
                selectedTab();
                var id = $("#<%=hdnPKID.ClientID%>").val();
                if (id == "") {
                    HideTab();
                }
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
    <asp:UpdatePanel ID="upMian" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Coupon Code Detail
                        </div>
                        <div class="panel-search right-content">
                            <asp:Button ID="btnBack" runat="server" CssClass="btn btn-primary" Text="Back" OnClick="btnBack_Click"
                                TabIndex="10" />&nbsp;
                        </div>
                        <hr class="nomargin" />
                        <DInfo:DisplayInfo runat="server" ID="DInfo" />
                        <ul class="nav nav-tabs">
                            <li class="active"><a id="liMain" onclick="getTabID(this)" href="#tabs-1" data-toggle="tab">
                                Coupon Code</a></li>
                            <li class=""><a id="liCouponCodeDetail" onclick="getTabID(this)" href="#tabs-2" data-toggle="tab">
                                Coupon Code Detail</a></li>
                        </ul>
                        <div class="tab-content">
                            <div class="tab-pane fade active in" id="tabs-1">
                                <div class="panel-search right-content">
                                    <div class="btn-group">
                                        <asp:Button runat="server" ID="btnSave" class="btn btn-primary" OnClick="btnSave_Click"
                                            Text="Save" ValidationGroup="save"></asp:Button>
                                        <button data-toggle="dropdown" class="btn btn-primary dropdown-toggle down-arrow">
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
                                                    <span class="mandatory">*</span> Coupon Type:
                                                </div>
                                                <div class="controlstyle">
                                                    <asp:DropDownList ID="ddlType" CssClass="form-control" runat="server" onchange="return ddlChange(this);"
                                                        OnSelectedIndexChanged="ddlType_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RFVType" runat="server" ErrorMessage="Select Coupon Type"
                                                        ValidationGroup="save" Text="*" ControlToValidate="ddlType" SetFocusOnError="true"
                                                        InitialValue="0" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="entryform">
                                                <div class="labelstyle">
                                                    <span class="mandatory">*</span> Coupon Code :
                                                </div>
                                                <div class="controlstyle">
                                                    <asp:TextBox ID="txtCouponCode" CssClass="form-control" Width="200" runat="server"
                                                        MaxLength="20"></asp:TextBox>
                                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RFVCouponCode" runat="server" ErrorMessage="Enter Coupon Code"
                                                        ValidationGroup="save" Text="*" ControlToValidate="txtCouponCode" SetFocusOnError="true"
                                                        CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="entryform">
                                                <div class="labelstyle">
                                                    <span class="mandatory">*</span>Discount Per. :
                                                </div>
                                                <div class="controlstyle">
                                                    <asp:TextBox ID="txtDiscountPer" CssClass="form-control" Width="200" runat="server"
                                                       ></asp:TextBox>
                                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RFVDiscountPer" runat="server"
                                                        ErrorMessage="Enter Discount Per." ValidationGroup="save" Text="*" ControlToValidate="txtDiscountPer"
                                                        SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator Display="Dynamic" ID="REVDiscount" runat="server"
                                                        ValidationGroup="save" Text="*" ControlToValidate="txtDiscountPer" SetFocusOnError="true"
                                                        CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                            <div class="entryform">
                                                <div class="labelstyle">
                                                    <span class="mandatory">*</span>Start Date :
                                                </div>
                                                <div class="controlstyle">
                                                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="txtDate" placeholder="dd-MM-yyyy"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="calStartDate" TargetControlID="txtStartDate" Format="dd-MM-yyyy"
                                                        PopupButtonID="EventStartDate" runat="server">
                                                    </cc1:CalendarExtender>
                                                    <asp:RegularExpressionValidator Display="Dynamic" ID="REVStartDate" runat="server"
                                                        ValidationGroup="save" Text="*" ControlToValidate="txtStartDate" SetFocusOnError="true"
                                                        CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RFVStartDate" runat="server" ErrorMessage="Enter Start Date"
                                                        ValidationGroup="save" Text="*" ControlToValidate="txtStartDate" SetFocusOnError="true"
                                                        CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="entryform">
                                                <div class="labelstyle">
                                                    <span class="mandatory">*</span>End Date :
                                                </div>
                                                <div class="controlstyle">
                                                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="txtDate" placeholder="dd-MM-yyyy"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="calEndDate" TargetControlID="txtEndDate" Format="dd-MM-yyyy"
                                                        PopupButtonID="EventEndDate" runat="server">
                                                    </cc1:CalendarExtender>
                                                    <asp:RegularExpressionValidator Display="Dynamic" ID="REVEndDate" runat="server"
                                                        ValidationGroup="save" Text="*" ControlToValidate="txtEndDate" SetFocusOnError="true"
                                                        CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RFVEndDate" runat="server" ErrorMessage="Enter End Date"
                                                        ValidationGroup="save" Text="*" ControlToValidate="txtEndDate" SetFocusOnError="true"
                                                        CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="entryform">
                                                <div class="entryform">
                                                    <div class="labelstyle">
                                                        Is Active :
                                                    </div>
                                                    <div class="controlstyle">
                                                        <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" />
                                                    </div>
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
                                                        <DInfo:DisplayInfo runat="server" ID="DInfoSubCategory" />
                                                        <div class="entryformmain" id="divSubCateogry" runat="server">
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
                                                        <div class="entryformmain" id="divCategory" runat="server">
                                                            <div class="entryform">
                                                                <DInfo:DisplayInfo runat="server" ID="DInfoCategory" />
                                                                <asp:GridView ID="dgvUnSelectedCategory" Width="100%" runat="server" AutoGenerateColumns="False"
                                                                    DataKeyNames="appCategoryID" CssClass="table table-striped table-bordered table-hover"
                                                                    HeaderStyle-Wrap="false" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
                                                                    PagerSettings-Mode="NumericFirstLast" PageSize="10" OnRowCommand="dgvUnSelectedCategory_RowCommand">
                                                                    <PagerSettings Position="Top" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%"
                                                                            HeaderStyle-Width="40%">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblCategory" runat="server" Text='<%#Eval("appCategory") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Add" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                                            HeaderStyle-Width="1%">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton runat="server" ID="lnkIsAdd" CommandName="IsAdd" CommandArgument='<%#Eval("appCategoryID") %>'>
                                                        <span class="action-icon set-icon green"><i class="fa fa-plus-square"></i></span>
                                                                                </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                        <div class="entryformmain" id="divProduct" runat="server">
                                                            <div class="panel-search">
                                                                <div class="fleft">
                                                                    <asp:DropDownList runat="server" ID="ddlProductCategory" CssClass="form-control"
                                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlProductCategory_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                    <div class="Separator">
                                                                        &nbsp;
                                                                    </div>
                                                                    <asp:DropDownList runat="server" ID="ddlSubCate" CssClass="form-control" AutoPostBack="true"
                                                                        OnSelectedIndexChanged="ddlSubCate_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                    <asp:Button ID="btnProductReset" runat="server" CssClass="btn btn-primary" Text="Reset"
                                                                        TabIndex="4" OnClick="btnProductReset_Click" />
                                                                </div>
                                                                <div class="fright" style="line-height: 32px;">
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
                                                                        AllowPaging="false" AllowSorting="false" DataKeyNames="appProductID" CssClass="table table-striped table-bordered table-hover"
                                                                        HeaderStyle-Wrap="false" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
                                                                        PagerSettings-Mode="NumericFirstLast" PageSize="10" OnRowCommand="dgvProduct_RowCommand">
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
                                                                                    <div style="background-color: <%#Eval("appColorCode") %>; width: 30px; border: 1px solid #3b3b3b;">
                                                                                        &nbsp&nbsp
                                                                                    </div>
                                                                                    <div>
                                                                                        <%#Eval("appColorName")%></div>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Product Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50%"
                                                                                SortExpression="appProductName" HeaderStyle-Width="50%">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("appProductName")%>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Add" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                                                HeaderStyle-Width="1%">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton runat="server" ID="lnkSave" CommandName="IsAdd" CommandArgument='<%#Eval("appProductID") %>'>
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
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="panel-search">
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                <ContentTemplate>
                                                    <div class="page-inner-title">
                                                        Coupon Refer List
                                                    </div>
                                                    <div class="table-responsive">
                                                        <div class="panel-search" id="divSearch" runat="server">
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
                                                                    OnClientClick="return ConfirmMessage('Coupon Refer','delete')" OnClick="btnSubCategoryDelete_Click" />
                                                            </div>
                                                            <div class="fclear">
                                                            </div>
                                                        </div>
                                                        <hr class="nomargin" />
                                                        <DInfo:DisplayInfo runat="server" ID="DinfoBranchGrid" />
                                                        <asp:GridView ID="dgvSubCategory" Width="100%" runat="server" AutoGenerateColumns="False"
                                                            AllowPaging="false" DataKeyNames="appCouponCodeProductID" CssClass="table table-striped table-bordered table-hover"
                                                            HeaderStyle-Wrap="false" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
                                                            PagerSettings-Mode="NumericFirstLast" PageSize="10" OnPageIndexChanging="dgvSubCategory_PageIndexChanging"
                                                            OnRowCreated="dgvSubCategory_RowCreated" OnRowDataBound="dgvSubCategory_RowDataBound"
                                                            OnSorting="dgvSubCategory_Sorting" OnRowDeleting="dgvSubCategory_RowDeleting"
                                                            OnRowEditing="dgvSubCategory_RowEditing">
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
                                                            </Columns>
                                                        </asp:GridView>
                                                        <asp:GridView ID="dgvCategory" Width="100%" runat="server" AutoGenerateColumns="False"
                                                            AllowPaging="false" DataKeyNames="appCouponCodeProductID" CssClass="table table-striped table-bordered table-hover"
                                                            HeaderStyle-Wrap="false" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
                                                            PagerSettings-Mode="NumericFirstLast" PageSize="10" OnPageIndexChanging="dgvCategory_PageIndexChanging"
                                                            OnRowCreated="dgvCategory_RowCreated" OnRowDataBound="dgvCategory_RowDataBound"
                                                            OnSorting="dgvCategory_Sorting" OnRowDeleting="dgvCategory_RowDeleting" OnRowEditing="dgvCategory_RowEditing">
                                                            <PagerSettings Position="Top" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="chkHeader" runat="server" onclick="SelectAll(this,'dgvCategory');" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkSelectRow" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCategory" runat="server" Text='<%#Eval("appCategory") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                        <asp:GridView ID="dgvGridViewProduct" Width="100%" runat="server" AutoGenerateColumns="False"
                                                            AllowSorting="true" DataKeyNames="appCouponCodeProductID" CssClass="table table-striped table-bordered table-hover"
                                                            HeaderStyle-Wrap="false" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
                                                            PagerSettings-Mode="NumericFirstLast" PageSize="10" OnRowCreated="dgvGridViewProduct_RowCreated"
                                                            OnRowDeleting="dgvGridViewProduct_RowDeleting" OnRowEditing="dgvGridViewProduct_RowEditing"
                                                            OnRowDataBound="dgvGridViewProduct_RowDataBound" OnSorting="dgvGridViewProduct_Sorting">
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
                                                                        <div style="background-color: <%#Eval("appColorCode") %>; width: 30px; border: 1px solid #3b3b3b;">
                                                                            &nbsp&nbsp
                                                                        </div>
                                                                        <div>
                                                                            <%#Eval("appColorName")%></div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Product Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50%"
                                                                    SortExpression="appProductName" HeaderStyle-Width="50%">
                                                                    <ItemTemplate>
                                                                        <%#Eval("appProductName")%>
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
                        <asp:HiddenField ID="hdnType" runat="server" />
                        <asp:ValidationSummary ID="vsSearch" ShowMessageBox="true" EnableClientScript="true"
                            HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                            ValidationGroup="save" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
