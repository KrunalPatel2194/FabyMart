<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Main.master" AutoEventWireup="true"
    CodeFile="ViewComments.aspx.cs" Inherits="Admin_PendingReviewList" %>

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
                                <asp:DropDownList ID="ddlStatus" AutoPostBack="true" runat="server" CssClass="form-control"
                                    OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                </asp:DropDownList>
                                <div class="Separator">
                                    &nbsp;
                                </div>
                                Date:
                                <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>
                                <cc1:CalendarExtender ID="calStartDate" TargetControlID="txtStartDate" Format="dd/MM/yyyy"
                                    PopupButtonID="EventStartDate" runat="server">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator Display="Dynamic" ID="rfvStartDate" runat="server" ErrorMessage="Enter Start Date"
                                    ValidationGroup="save" Text="*" ControlToValidate="txtStartDate" SetFocusOnError="true"
                                    CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                To:
                                <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>
                                <cc1:CalendarExtender ID="calEndDate" TargetControlID="txtEndDate" Format="dd/MM/yyyy"
                                    PopupButtonID="EventEndDate" runat="server">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator Display="Dynamic" ID="rfvEndDate" runat="server" ErrorMessage="Enter End Date"
                                    ValidationGroup="save" Text="*" ControlToValidate="txtEndDate" SetFocusOnError="true"
                                    CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
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
                                        OnClientClick="return ConfirmMessage('Review','delete')" OnClick="btnDelete_Click" />&nbsp;
                                    <asp:Button ID="btnBack" runat="server" CssClass="btn btn-primary" Text="Back" OnClick="btnBack_Click"
                                        TabIndex="8" />&nbsp;
                                    
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
                            <h2 style="color: #3276B1;">
                                <asp:Label ID="lblProductName" runat="server">fdss</asp:Label></h2>
                            <hr class="nomargin" />
                            <DInfo:DisplayInfo runat="server" ID="DInfo" />
                            <div class="panel-search">
                                <div class="table-responsive">
                                    <asp:GridView ID="dgvGridView" Width="100%" runat="server" AutoGenerateColumns="False"
                                        AllowSorting="true" DataKeyNames="appReviewID,appProductDetailID,appReviewStatus"
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
                                            <asp:BoundField DataField="appReviewDate" HeaderText="Date" SortExpression="appReviewDate"
                                                HeaderStyle-Width="1%" ItemStyle-Width="1%" />
                                            <asp:BoundField DataField="appFullname" HeaderText="Name" SortExpression="appFullname"
                                                HeaderStyle-Width="1%" ItemStyle-Width="1%" />
                                            <asp:BoundField DataField="appRating" HeaderText="Rating" SortExpression="appRating"
                                                HeaderStyle-Width="1%" ItemStyle-Width="1%" />
                                            <asp:TemplateField HeaderText="Comments" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="4%"
                                                HeaderStyle-Width="4%">
                                                <ItemTemplate>
                                                   
                                                    <div class="divGrid">
                                                        <b><%#Eval("appTitle") %></b>
                                                    </div>
                                                   
                                                    <div class="divGrid">
                                                        <%#Eval("appComment") %>
                                                    </div>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        
                                            <asp:TemplateField HeaderText="Is Approved" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                                HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div id="divView" runat="server">
                                                        <asp:LinkButton runat="server" ID="lnkIsActive" >
                                                        <span class="action-icon set-icon <%# Eval("appReviewStatus").ToString() == "2" ? "green" : "red" %>"><i class="fa <%# Eval("appReviewStatus").ToString().ToLower() == "2" ? "fa-check" : "fa-ban" %>"></i></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <asp:HiddenField ID="hdnProductDetailID" runat="server" Value="" />
                                <asp:HiddenField ID="hdnPKID" runat="server" Value="" />
                                <asp:HiddenField ID="hdnSelectedIDs" Value="" runat="server" />
                                <asp:ValidationSummary ID="vsSearch" ShowMessageBox="true" EnableClientScript="true"
                                    HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                                    ValidationGroup="Search" />
                            </div>
                            <input type="button" runat="server" id="btnProductView" style="display: none;" />
                            <cc1:ModalPopupExtender ID="mpeReviewView" runat="server" TargetControlID="btnProductView"
                                PopupControlID="divReviewPreview" BackgroundCssClass="modalbackground" DropShadow="false"
                                CancelControlID="imgBtnImgClose">
                            </cc1:ModalPopupExtender>
                            <div id="divReviewPreview" class="modalpopup_ panel panel-default" runat="server"
                                style="display: none; width: 900px;">
                                <asp:LinkButton ID="imgBtnImgClose" CssClass="modalbclose" runat="server">
                                        <span class="action-icon set-icon"><i class="fa fa-minus"></i></span>
                                </asp:LinkButton>
                                <div class="modalheader_ panel-heading">
                                    <b>Review Info</b>
                                </div>
                                <hr class="nomargin" />
                                <div class="modaldetail">
                                    <div class="panel-search">
                                        <div class="table-responsive" style="margin-bottom: 0px;">
                                            <DInfo:DisplayInfo runat="server" ID="DinfoProductInfo" />
                                            <div>
                                                <div class="entryformmain left">
                                                    <h5>
                                                        <b>Review Detail</b></h5>
                                                    <div class="entryform">
                                                        <div class="labelstyle" style="width: 20%;">
                                                            User Name :
                                                        </div>
                                                        <div class="lblcontrolstyle" style="width: 80%;">
                                                            <asp:Label ID="lblUserName" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="entryform">
                                                        <div class="labelstyle" style="width: 20%;">
                                                            Comment :
                                                        </div>
                                                        <div class="lblcontrolstyle" style="width: 80%;">
                                                            <asp:Label ID="lblComment" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="entryform">
                                                        <div class="labelstyle" style="width: 20%;">
                                                            Date :
                                                        </div>
                                                        <div class="lblcontrolstyle" style="width: 80%;">
                                                            <asp:Label ID="lblDate" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="entryform">
                                                        <div class="labelstyle" style="width: 20%;">
                                                            Professional Name :
                                                        </div>
                                                        <div class="lblcontrolstyle" style="width: 80%;">
                                                            <asp:Label ID="lblSellerlName" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="entryform">
                                                        <div class="labelstyle" style="width: 20%;">
                                                            Rating :
                                                        </div>
                                                        <div class="lblcontrolstyle" style="width: 80%;">
                                                            <asp:Label ID="lblRating" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <%--  <div class="entryformmain left">
                                                    <h5>
                                                        <b>Rating Detail</b></h5>
                                                    <div class="entryform" style="margin: 20px;">
                                                          <cc1:Rating ID="RatingReviewType" AutoPostBack="true" runat="server" StarCssClass="Star"
                                                                            CurrentRating='<%# Convert.ToInt32(Eval("appRating"))%>' WaitingStarCssClass="WaitingStar"
                                                                            EmptyStarCssClass="Star" FilledStarCssClass="FilledStar" ReadOnly="true">
                                                                        </cc1:Rating>
                                                                    
                                                        <div class="clear">
                                                        </div>
                                                    </div>
                                                </div>--%>
                                                <div style="text-align: right; margin-top: 10px;">
                                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Approve"
                                                        OnClick="btnSave_Click" />
                                                    &nbsp;
                                                    <asp:Button ID="btnDisapproved" runat="server" CssClass="btn btn-danger" Text="Reject"
                                                        ValidationGroup="Status" OnClick="btnDisapproved_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
