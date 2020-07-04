<%@ Page Title="Courier Company Detail" Language="C#" MasterPageFile="~/admin/Main.master"
    AutoEventWireup="true" CodeFile="CourierCompanyDetail.aspx.cs" Inherits="CourierCompanyDetail" %>

<%@ Register Src="~/Admin/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
                    Courier Company Detail
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
                        <li class="active"><a href="#tabs-1" data-toggle="tab">Courier Company</a></li>
                        <li class=""><a id="liSize" onclick="getTabID(this)" href="#tabs-2" data-toggle="tab">
                            Courier Rate </a></li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane fade active in" id="tabs-1">
                            <div class="panel-search">
                                <div class="table-responsive">
                                    <div class="entryformmain">
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                <span class="mandatory">*</span> Courier Company Name :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:TextBox ID="txtCourierCompanyName" CssClass="form-control" Width="200" runat="server"
                                                    TabIndex="1"></asp:TextBox>
                                                <asp:RequiredFieldValidator Display="Dynamic" ID="RFVCourierCompanyName" runat="server"
                                                    ErrorMessage="Enter Courier Company Name" ValidationGroup="save" Text="*" ControlToValidate="txtCourierCompanyName"
                                                    SetFocusOnError="true" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                <span class="mandatory">*</span> COD Rate :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:TextBox ID="txtCODRate" CssClass="form-control" Width="200" runat="server" TabIndex="5"></asp:TextBox>
                                                <asp:RequiredFieldValidator Display="Dynamic" ID="RFVCODRate" runat="server" ErrorMessage="Enter COD Rate"
                                                    ValidationGroup="save" Text="*" ControlToValidate="txtCODRate" SetFocusOnError="true"
                                                    CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator Display="Dynamic" ID="REVCODRate" runat="server"
                                                    ValidationGroup="save" Text="*" ControlToValidate="txtCODRate" SetFocusOnError="true"
                                                    CssClass="ErrorLabelStyle">
                                                </asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                <span class="mandatory">*</span> Email :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:TextBox ID="txtEmail" CssClass="form-control" Width="200" runat="server" TabIndex="2"></asp:TextBox>
                                                <asp:RequiredFieldValidator Display="Dynamic" ID="RFVEmail" runat="server" ErrorMessage="Enter Email"
                                                    ValidationGroup="save" Text="*" ControlToValidate="txtEmail" SetFocusOnError="true"
                                                    CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator Display="Dynamic" ID="REVEmail" runat="server" ValidationGroup="save"
                                                    Text="*" ControlToValidate="txtEmail" SetFocusOnError="true" CssClass="ErrorLabelStyle">
                                                </asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                <span class="mandatory">*</span> Contact No :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:TextBox ID="txtContactNo" CssClass="form-control" Width="200" runat="server"
                                                    TabIndex="3"></asp:TextBox>
                                                <asp:RequiredFieldValidator Display="Dynamic" ID="RFVContactNo" runat="server" ErrorMessage="Enter Contact No"
                                                    ValidationGroup="save" Text="*" ControlToValidate="txtContactNo" SetFocusOnError="true"
                                                    CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator Display="Dynamic" ID="REVContact" runat="server"
                                                    ValidationGroup="save" Text="*" ControlToValidate="txtContactNo" SetFocusOnError="true"
                                                    CssClass="ErrorLabelStyle">
                                                </asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                <span class="mandatory">*</span> Web site :
                                            </div>
                                            <div class="controlstyle">
                                                <asp:TextBox ID="txtWebsite" CssClass="form-control" Width="200" runat="server" TabIndex="5"></asp:TextBox>
                                                <asp:RequiredFieldValidator Display="Dynamic" ID="RFVWebsite" runat="server" ErrorMessage="Enter Website"
                                                    ValidationGroup="save" Text="*" ControlToValidate="txtWebsite" SetFocusOnError="true"
                                                    CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator Display="Dynamic" ID="REVWebsite" runat="server"
                                                    ValidationGroup="save" Text="*" ControlToValidate="txtWebsite" SetFocusOnError="true"
                                                    CssClass="ErrorLabelStyle">
                                                </asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="entryform">
                                                <div class="labelstyle">
                                                    Is Active :
                                                </div>
                                                <div class="controlstyle">
                                                    <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" TabIndex="9" />
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
                                                    <div class="entryformmain">
                                                        <div class="entryform">
                                                            <div class="labelstyle">
                                                                <span class="mandatory">*</span> PIN Code :
                                                            </div>
                                                            <div class="controlstyle">
                                                                <asp:DropDownList runat="server" ID="ddlPINCode" CssClass="form-control">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator Display="Dynamic" ID="RFVMainCategory" runat="server"
                                                                    ErrorMessage="Select PIN Code" ValidationGroup="save1" Text="*" ControlToValidate="ddlPINCode"
                                                                    SetFocusOnError="true" InitialValue="0" CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="entryform">
                                                            <div class="labelstyle">
                                                                <span class="mandatory">*</span> Min Weight :
                                                            </div>
                                                            <div class="controlstyle">
                                                                <asp:TextBox ID="txtMinWeight" CssClass="form-control" Width="200" runat="server"
                                                                    TabIndex="2"></asp:TextBox>
                                                                <asp:RequiredFieldValidator Display="Dynamic" ID="RFVMinWeight" runat="server" ErrorMessage="Enter Min Weight"
                                                                    ValidationGroup="save1" Text="*" ControlToValidate="txtMinWeight" SetFocusOnError="true"
                                                                    CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="entryform">
                                                            <div class="labelstyle">
                                                                <span class="mandatory">*</span> Max Weight :
                                                            </div>
                                                            <div class="controlstyle">
                                                                <asp:TextBox ID="txtMaxWeight" CssClass="form-control" Width="200" runat="server"
                                                                    TabIndex="2"></asp:TextBox>
                                                                <asp:RequiredFieldValidator Display="Dynamic" ID="RFVMaxWeight" runat="server" ErrorMessage="Enter Max Weight"
                                                                    ValidationGroup="save1" Text="*" ControlToValidate="txtMaxWeight" SetFocusOnError="true"
                                                                    CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="entryform">
                                                            <div class="labelstyle">
                                                                <span class="mandatory">*</span> Rate :
                                                            </div>
                                                            <div class="controlstyle">
                                                                <asp:TextBox ID="txtRate" CssClass="form-control" Width="200" runat="server" TabIndex="2"></asp:TextBox>
                                                                <asp:RequiredFieldValidator Display="Dynamic" ID="RFVRate" runat="server" ErrorMessage="Enter Rate"
                                                                    ValidationGroup="save1" Text="*" ControlToValidate="txtRate" SetFocusOnError="true"
                                                                    CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="entryform">
                                                            <div class="entryform">
                                                                <div class="labelstyle">
                                                                    Is COD :
                                                                </div>
                                                                <div class="controlstyle">
                                                                    <asp:CheckBox ID="chkIsCOD" runat="server" Checked="True" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="entryform">
                                                            <div class="labelstyle">
                                                            </div>
                                                            <div class="controlstyle">
                                                                <asp:Button runat="server" ID="btnSaveCourierRate" class="btn btn-primary" Text="Save"
                                                                    ValidationGroup="save1" TabIndex="3" OnClick="btnSaveCourierRate_Click"></asp:Button>
                                                                <asp:Button runat="server" ID="btnClearCourierRate" class="btn btn-primary" Text="Clear"
                                                                    TabIndex="4" OnClick="btnClearCourierRate_Click"></asp:Button>
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
                                                    Courier Rate
                                                </div>
                                                <div class="table-responsive">
                                                    <div class="panel-search">
                                                        <div class="fleft">
                                                            <asp:Button ID="btnCourierRateDelete" runat="server" CssClass="btn btn-danger" Text="Delete"
                                                                OnClientClick="return ConfirmMessage('CourierRate','delete')" OnClick="btnCourierRateDelete_Click" />
                                                        </div>
                                                        <div class="fright">
                                                            Total&nbsp;Records&nbsp;:&nbsp; <span class="RecordCount">
                                                                <asp:Label ID="lblCourierRateCount" runat="server" Text="0"> </asp:Label>
                                                            </span>
                                                            <div class="Separator">
                                                                &nbsp;
                                                            </div>
                                                            Per Page :
                                                            <asp:DropDownList ID="ddlCourierRatePerPage" runat="server" AutoPostBack="true" CssClass="form-control"
                                                                OnSelectedIndexChanged="ddlCourierRatePerPage_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="fclear">
                                                        </div>
                                                    </div>
                                                    <hr class="nomargin" />
                                                    <DInfo:DisplayInfo runat="server" ID="DinfoBranchGrid" />
                                                    <asp:GridView ID="dgvCourierRate" Width="100%" runat="server" AutoGenerateColumns="False"
                                                        DataKeyNames="appCourierRateID,appIsCOD" CssClass="table table-striped table-bordered table-hover"
                                                        HeaderStyle-Wrap="false" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
                                                        PagerSettings-Mode="NumericFirstLast" PageSize="10" OnPageIndexChanging="dgvCourierRate_PageIndexChanging"
                                                        OnRowCreated="dgvCourierRate_RowCreated" OnRowDataBound="dgvCourierRate_RowDataBound"
                                                        OnSorting="dgvCourierRate_Sorting" OnRowDeleting="dgvCourierRate_RowDeleting"
                                                        OnRowCommand="dgvCourierRate_RowCommand" OnRowEditing="dgvCourierRate_RowEditing">
                                                        <PagerSettings Position="Top" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkHeader" runat="server" onclick="SelectAll(this,'dgvCourierRate');" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSelectRow" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="PIN Code" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%"
                                                                HeaderStyle-Width="40%" SortExpression="appPinCodeID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPinCodeID" runat="server" Text='<%#Eval("appPinCode") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Min Weight" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%"
                                                                HeaderStyle-Width="40%" SortExpression="appMinWeight">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMinWeight" runat="server" Text='<%#Eval("appMinWeight") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Max Weight" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%"
                                                                HeaderStyle-Width="40%" SortExpression="apMaxWeight">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMaxWeight" runat="server" Text='<%#Eval("appMaxWeight") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText=" Rate" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%"
                                                                HeaderStyle-Width="40%" SortExpression="appRate">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRate" runat="server" Text='<%#Eval("appRate") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Is COD" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                                HeaderStyle-Width="1%">
                                                                <ItemTemplate>
                                                                    <%--    <asp:LinkButton runat="server" ID="lnkIsCOD" CommandName="IsCOD" CommandArgument='<%#Eval("appCourierRateID") %>'>--%>
                                                                    <span class="action-icon set-icon <%# Eval("appIsCOD").ToString().ToLower() == "true" ? "green" : "red" %>">
                                                                        <i class="fa <%# Eval("appIsCOD").ToString().ToLower() == "true" ? "fa-check" : "fa-ban" %>">
                                                                        </i></span>
                                                                    <%--   </asp:LinkButton>--%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <asp:HiddenField ID="hdnCourierRateId" runat="server" />
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
