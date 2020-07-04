<%@ Page Title="Returned Order List" Language="C#" MasterPageFile="~/Admin/Main.master"
    AutoEventWireup="true" CodeFile="Returned.aspx.cs" Inherits="Returned" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<%@ Register Src="~/Admin/UserControls/OrderStatus.ascx" TagName="OrderStatus" TagPrefix="MyOrder" %>
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
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Returned Order List
                </div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="panel-search">
                                <MyOrder:OrderStatus ID="UcOrderStratus" runat="server" />
                            </div>
                            <div class="panel-search">
                                Select Criteria :
                                <asp:DropDownList runat="server" ID="ddlFields" CssClass="form-control" TabIndex="3">
                                    <asp:ListItem Text="--Select Option--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Product" Value="appProductName"></asp:ListItem>
                                    <asp:ListItem Text="Qty" Value="appQty"></asp:ListItem>
                                    <asp:ListItem Text="Price" Value="appSellingPrice"></asp:ListItem>
                                    <asp:ListItem Text="Sku No" Value="appSKUNo"></asp:ListItem>
                                    <asp:ListItem Text="Customer Name" Value="AppCustomerName"></asp:ListItem>
                                    <asp:ListItem Text="Order No." Value="appOrderNo"></asp:ListItem>
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
                            <ul class="nav nav-tabs">
                                <li id="Requested" class="active" runat="server">
                                    <asp:LinkButton ID="lnkRequested" runat="server" OnClick="lnkRequested_Click" Text="Requested" />
                                </li>
                                <li id="Approved" class="" runat="server">
                                    <asp:LinkButton ID="lnkApproved" runat="server" OnClick="lnkApproved_Click" Text="Approved" />
                                </li>
                                <li id="Dispatched" class="" runat="server">
                                    <asp:LinkButton ID="lnkDispatched" runat="server" OnClick="lnkDispatched_Click" Text="Dispatched" />
                                </li>
                                <li id="Complete" class="" runat="server">
                                    <asp:LinkButton ID="lnkComplete" runat="server" OnClick="lnkComplete_Click" Text="Complete" />
                                </li>
                            </ul>
                            <div class="panel-search">
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
                                        AllowSorting="true" DataKeyNames="appPaymentMode,appSubOrderID" CssClass="table table-striped table-bordered table-hover"
                                        HeaderStyle-Wrap="false" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
                                        PagerSettings-Mode="NumericFirstLast" PageSize="10" OnPageIndexChanging="dgvGridView_PageIndexChanging"
                                        OnRowCreated="dgvGridView_RowCreated" OnRowDataBound="dgvGridView_RowDataBound"
                                        OnSorting="dgvGridView_Sorting" OnRowCommand="dgvGridView_RowCommand">
                                        <PagerSettings Position="Top" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Product" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="70%"
                                                HeaderStyle-Width="70%">
                                                <ItemTemplate>
                                                    <div style="margin-top: 5px;">
                                                        <asp:GridView ID="dgvSubDetail" Width="100%" runat="server" AutoGenerateColumns="False"
                                                            DataKeyNames="appReturnOrderDetailID" CssClass="table table-bordered" HeaderStyle-Wrap="false"
                                                            AllowPaging="false" AllowSorting="false" OnRowCommand="dgvSubDetail_RowCommand">
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
                                                                <%--     <asp:BoundField HeaderText="Price" DataField="appSellingPrice" SortExpression="appSellingPrice"
                                                                           ItemStyle-Width="1%" HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="Center"
                                                                            HeaderStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField HeaderText="Quantity" DataField="appQty" SortExpression="appQty"
                                                                            ItemStyle-Width="1%" HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="Center" />
                                                                --%>
                                                                <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                                    HeaderStyle-Width="1%">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" ID="lnkIsView" CommandName="IsView" CommandArgument='<%#Eval("appReturnOrderDetailID") %>'>
                                                        <span class="action-icon set-icon"><i class="fa fa-chevron-up"></i>
                                                        </span></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Customer Detail" ItemStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="5%" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <div class="divGrid divPriceValuewithoutPadding">
                                                        Name :
                                                    </div>
                                                    <div class="divGrid ">
                                                        <asp:Label ID="lblCustomerName" runat="server" Text='<%#Eval("appPickupName") %>'></asp:Label>
                                                    </div>
                                                    <div class="divGrid divPriceValuewithoutPadding">
                                                        Address :
                                                    </div>
                                                    <div class="divGrid ">
                                                        <asp:Label ID="lblAddress" runat="server" Text='<%#Eval("appPickupAddress") %>'></asp:Label>
                                                    </div>
                                                    <div class="divGrid divPriceValuewithoutPadding">
                                                        Mobile No. :
                                                    </div>
                                                    <div class="divGrid ">
                                                        <asp:Label ID="lblMobile" runat="server" Text='<%#Eval("appPickupContactNo1") %>'></asp:Label>
                                                    </div>
                                                    <div class="divGrid divPriceValuewithoutPadding">
                                                        PIN Code. :
                                                    </div>
                                                    <div class="divGrid ">
                                                        <asp:Label ID="lblPincode" runat="server" Text='<%#Eval("appPickupPIN") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Info" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%"
                                                HeaderStyle-Width="60%">
                                                <ItemTemplate>
                                                    <div class="divGrid">
                                                        <b>
                                                            <%#Eval("appReason") %></b>
                                                    </div>
                                                    <div class="divGrid">
                                                        <%#Eval("appNote") %>
                                                    </div>
                                                    <div class="divGrid">
                                                        <b>Preffered Time :</b><%#Eval("appPreferedTime")%>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="1%"
                                                HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="divGrid divPriceValuewithoutPadding">
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("appOrderStatus") %>'></asp:Label>
                                                    </div>
                                                    <div class="divGrid divPriceValuewithoutPadding">
                                                        <asp:Label ID="lblReturnStatus" runat="server" Text='<%#Eval("appReturnStatus") %>'></asp:Label>
                                                    </div>
                                                    <div class="divGrid divPriceValuewithoutPadding">
                                                        <asp:LinkButton ID="lnkbtnReturnBack" runat="server" CommandName="ReturnComplete"
                                                            Visible="false" CommandArgument='<%#Eval("appReturnOrderID") %>' Text="Product Back"
                                                            CssClass="btn btn-primary" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Requested Date" DataField="appRequestedDate" SortExpression="appRequestedDate"
                                                ItemStyle-Width="1%" HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField HeaderText="Return Date" DataField="appSubOrderChangeDate" SortExpression="appSubOrderChangeDate"
                                                ItemStyle-Width="1%" HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="Center" />
                                            <asp:TemplateField HeaderText="Payment" HeaderStyle-Width="1%" ItemStyle-Width="1%"
                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblPaymentMode" Font-Bold="true" Font-Size="10" Style="padding: 4px;
                                                        color: #FFF" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <asp:HiddenField ID="hdnTabID" runat="server" />
                                <asp:HiddenField ID="hdnSelectedIDs" Value="" runat="server" />
                                <asp:ValidationSummary ID="vsSearch" ShowMessageBox="true" EnableClientScript="true"
                                    HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                                    ValidationGroup="Search" />
                                <input type="button" runat="server" id="btnProductView" style="display: none;" />
                                <cc1:ModalPopupExtender ID="mpeView" runat="server" TargetControlID="btnProductView"
                                    PopupControlID="divProductView" BackgroundCssClass="modalbackground" DropShadow="false"
                                    CancelControlID="imgBtnImgClose">
                                </cc1:ModalPopupExtender>
                                <div id="divProductView" class="modalpopup_ panel panel-default" runat="server" style="display: none;
                                    width: 900px;">
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                        <ContentTemplate>
                                            <asp:LinkButton ID="imgBtnImgClose" CssClass="modalbclose" runat="server">
                                        <span class="action-icon set-icon"><i class="fa fa-minus"></i></span>
                                            </asp:LinkButton>
                                            <div class="modalheader_ panel-heading">
                                                <b>Courier Company Detail</b>
                                            </div>
                                            <hr class="nomargin" />
                                            <div class="modaldetail">
                                                <div class="panel-search">
                                                    <div class="table-responsive">
                                                        <div class="entryformmain">
                                                            <div style="float: left; width: 49%">
                                                                <div class="entryform">
                                                                    <div class="labelstyle">
                                                                        <asp:Label ID="lblForLabel1" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div class="lblcontrolstyle">
                                                                        <asp:Label ID="lblValue1" runat="server"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="entryform">
                                                                    <div class="labelstyle">
                                                                        <asp:Label ID="lblForLabel2" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div class="lblcontrolstyle">
                                                                        <asp:Label ID="lblValue2" runat="server"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
