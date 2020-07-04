<%@ Page Title="Menifest List" Language="C#" MasterPageFile="~/Admin/Main.master"
    AutoEventWireup="true" CodeFile="MenifestList.aspx.cs" Inherits="MenifestList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Menifest List
                </div>
                <div style="margin-top: 10px;">
                    <div class="panel-search">
                        <div class="fleft">
                            Date
                            <asp:TextBox ID="txtStartDate" runat="server" CssClass="txtDate" placeholder="dd-MM-yyyy"></asp:TextBox>
                            <cc1:CalendarExtender ID="calStartDate" TargetControlID="txtStartDate" Format="dd-MM-yyyy"
                                PopupButtonID="EventStartDate" runat="server">
                            </cc1:CalendarExtender>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="rfvStartDate" runat="server" ErrorMessage="Enter Start Date"
                                ValidationGroup="Search" Text="*" ControlToValidate="txtStartDate" SetFocusOnError="true"
                                CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator Display="Dynamic" ID="REVStartDate" runat="server"
                                ValidationGroup="Search" Text="*" ControlToValidate="txtStartDate" SetFocusOnError="true"
                                CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                            To
                            <asp:TextBox ID="txtEndDate" runat="server" CssClass="txtDate" placeholder="dd-MM-yyyy"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtEndDate" Format="dd-MM-yyyy"
                                PopupButtonID="EventEndDate" runat="server">
                            </cc1:CalendarExtender>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="rfvEndDate" runat="server" ErrorMessage="Enter End Date"
                                ValidationGroup="Search" Text="*" ControlToValidate="txtEndDate" SetFocusOnError="true"
                                CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator Display="Dynamic" ID="REVEndDate" runat="server"
                                ValidationGroup="Search" Text="*" ControlToValidate="txtEndDate" SetFocusOnError="true"
                                CssClass="ErrorLabelStyle"></asp:RegularExpressionValidator>
                            <asp:Button ID="btnGO" runat="server" CssClass="btn btn-primary" Text="Search" ValidationGroup="Search"
                                TabIndex="2" OnClick="btnGO_Click" />
                            <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary" Text="Reset"
                                TabIndex="4" OnClick="btnReset_Click" />
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
                </div>
                <DInfo:DisplayInfo runat="server" ID="DInfo" />
                <div class="panel-search">
                    <div class="table-responsive">
                        <asp:GridView ID="dgvGridView" Width="100%" runat="server" AutoGenerateColumns="False"
                            AllowSorting="true" DataKeyNames="appManifestID" CssClass="table table-striped table-bordered table-hover"
                            HeaderStyle-Wrap="false" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
                            PagerSettings-Mode="NumericFirstLast" PageSize="10" OnPageIndexChanging="dgvGridView_PageIndexChanging"
                            OnSorting="dgvGridView_Sorting" OnRowCommand="dgvGridView_RowCommand">
                            <PagerSettings Position="Top" />
                            <PagerStyle CssClass="pagination" />
                            <Columns>
                                <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                                    HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="1%" SortExpression="appCreatedDate">
                                    <ItemTemplate>
                                        <%#Eval("appCreatedDate")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Menifest No." ItemStyle-HorizontalAlign="Left" ItemStyle-Width="58%"
                                    HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="58%" SortExpression="appManifestID">
                                    <ItemTemplate>
                                        <%#Eval("appManifestID") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="File" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%"
                                    HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <a href='<%=strServerURL%>Admin/<%# Eval("appUploadedManifest") %>' target="_blank"
                                            style='display: <%# Eval("appIsShow").ToString().ToLower()=="true"? "none":"block" %>'>
                                            Download</a>
                                        <div id="File" runat="server" visible='<%#Eval("appIsShow").ToString().ToLower()=="true"? true: false%>'>
                                            <asp:FileUpload ID="fileUpload" runat="server" Width="200px" />
                                            <br />
                                            <asp:Button ID="btn" runat="server" CommandName="SaveFile" CommandArgument='<%#Eval("appManifestID") %>'
                                                class="btn btn-primary" Text="Upload" />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <asp:HiddenField ID="hdnSelectedIDs" Value="" runat="server" />
                    <asp:ValidationSummary ID="vsSearch" ShowMessageBox="true" EnableClientScript="true"
                        HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                        ValidationGroup="Search" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
