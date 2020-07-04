<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Main.master" AutoEventWireup="true"
    CodeFile="OrderDetail.aspx.cs" Inherits="OrderDetail" %>

<%@ Register Src="~/Admin/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" id="sourcecode">

        function ChkOrder(strId) {
            document.getElementById("<%=hdnSubOrderId.ClientID%>").value = strId;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Order Detail
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
                                        <%--  <li>
                                    <asp:LinkButton runat="server" ID="lnkSaveAndAddnew" OnClick="lnkSaveAndAddnew_Click"
                                        Text="Save & Add New" ValidationGroup="save" TabIndex="7"></asp:LinkButton></li>--%>
                                    </ul>
                                </div>
                            </div>
                            <hr class="nomargin" />
                            <DInfo:DisplayInfo runat="server" ID="DInfo" />
                            <div class="panel-search">
                                <div class="table-responsive">
                                    <div class="entryformmain">
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                Order No:
                                            </div>
                                            <div class="lblcontrolstyle">
                                                <asp:Label ID="lblOrderNo" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                Order status:
                                            </div>
                                            <div class="controlstyle">
                                                <asp:DropDownList runat="server" ID="ddlstatus" CssClass="form-control">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator Display="Dynamic" ID="rfMenuType" runat="server" ErrorMessage="Select Status"
                                                    ValidationGroup="save" Text="*" ControlToValidate="ddlstatus" SetFocusOnError="true"
                                                    InitialValue="0" CssClass="ErrorLabelStyle" />
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                Order Total Amount:
                                            </div>
                                            <div class="lblcontrolstyle">
                                                <asp:Label ID="lblOrderAmount" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                Receiver Name :
                                            </div>
                                            <div class="lblcontrolstyle">
                                                <asp:Label ID="lblReceiverName" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                Receiver ContactNo 1 :
                                            </div>
                                            <div class="lblcontrolstyle">
                                                <asp:Label ID="lblReceiverContactNo1" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                Receiver ContactNo 2 :
                                            </div>
                                            <div class="lblcontrolstyle">
                                                <asp:Label ID="lblReceiverContactNo2" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                Receiver Email :
                                            </div>
                                            <div class="lblcontrolstyle">
                                                <asp:Label ID="lblRecevierEmail" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                Receiver Address :
                                            </div>
                                            <div class="lblcontrolstyle">
                                                <asp:Label ID="lblReceiverAddress" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                Prefered Time :
                                            </div>
                                            <div class="lblcontrolstyle">
                                                <asp:Label ID="lblPreferedTime" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                Bill Receiver Name:
                                            </div>
                                            <div class="lblcontrolstyle">
                                                <asp:Label ID="lblBillReceiverName" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                Bill Receiver ContactNo 1 :
                                            </div>
                                            <div class="lblcontrolstyle">
                                                <asp:Label ID="lblBillReceiverContactNo1" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                Bill Receiver ContactNo 2 :
                                            </div>
                                            <div class="lblcontrolstyle">
                                                <asp:Label ID="lblBillReceiverContactNo2" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                Bill Receiver Email :
                                            </div>
                                            <div class="lblcontrolstyle">
                                                <asp:Label ID="lblBillRecevierEmail" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                Bill Receiver Address :
                                            </div>
                                            <div class="lblcontrolstyle">
                                                <asp:Label ID="lblBillReceiverAddress" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                Customer Name :
                                            </div>
                                            <div class="lblcontrolstyle">
                                                <asp:Label ID="lblCustomer" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                Customer Mobile :
                                            </div>
                                            <div class="lblcontrolstyle">
                                                <asp:Label ID="lblCustomerMobile" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="entryform">
                                            <div class="labelstyle">
                                                Customer Email :
                                            </div>
                                            <div class="lblcontrolstyle">
                                                <asp:Label ID="lblCustomerEmail" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-search">
                                <div class="table-responsive">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="page-inner-title">
                                                Sub Order List
                                            </div>
                                            <div>
                                                <div class="panel-search">
                                                    Select Criteria :
                                                    <asp:DropDownList runat="server" ID="ddlFields" CssClass="form-control" TabIndex="3">
                                                        <asp:ListItem Text="Product" Value="appProductName"></asp:ListItem>
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
                                                            OnClientClick="return ConfirmMessage('Sub Order','delete')" OnClick="btnDelete_Click" />
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
                                                <DInfo:DisplayInfo runat="server" ID="DInfoSubOrder" />
                                                <div class="panel-search">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="dgvGridView" Width="100%" runat="server" AutoGenerateColumns="False"
                                                            AllowSorting="true" DataKeyNames="appSubOrderID,appSubOrderStatusID" CssClass="table table-striped table-bordered table-hover"
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
                                                                <asp:BoundField HeaderText="Sub order no" DataField="appSubOrderNo" SortExpression="appSubOrderNo"
                                                                    ItemStyle-Width="1%" HeaderStyle-Width="1%" />
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
                                                                <asp:TemplateField HeaderText="Images" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                                    HeaderStyle-Width="1%">
                                                                    <ItemTemplate>
                                                                        <asp:Image ID="img" runat="server" ImageUrl='<%#Eval("appNormalImage") %>' Width="100" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="Product Name" DataField="appProductName" SortExpression="appProductName" />
                                                                <asp:BoundField HeaderText="Qty" DataField="appQty" SortExpression="appQty" />
                                                                <asp:BoundField HeaderText="Price" DataField="appSellingPrice" SortExpression="appSellingPrice"
                                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                <asp:TemplateField HeaderText="Order Status" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                                    HeaderStyle-Width="1%">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="ddlSubOrderStatus" runat="server" AutoPostBack="true" CssClass="form-control" 
                                                                            OnSelectedIndexChanged="ddlSubOrderStatus_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="Size" DataField="appSize" SortExpression="appSize" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                    <asp:HiddenField ID="hdnSelectedIDs" Value="" runat="server" />
                                                    <asp:HiddenField ID="hdnSubOrderId" Value="" runat="server" />
                                                    <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" EnableClientScript="true"
                                                        HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                                                        ValidationGroup="Search" />
                                                </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div>
                        <asp:HiddenField ID="hdnPKID" Value="" runat="server" />
                        <asp:ValidationSummary ID="vsSearch" ShowMessageBox="true" EnableClientScript="true"
                            HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                            ValidationGroup="save" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
