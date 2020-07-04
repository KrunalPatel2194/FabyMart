<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Main.master" AutoEventWireup="true"
    CodeFile="Import.aspx.cs" Inherits="Import" %>

<%@ Register Src="~/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .GridWidth td th
        {
            width: 100px !important;
        }
        .GridWidth td th
        {
            width: 100px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Import Lead
                </div>
                <div>
                    <div class="panel-search">
                        <div class="fleft">
                            <asp:Button runat="server" ID="btnGenerateFile" class="btn btn-primary" Text="Download Excel File"
                                OnClick="btnGenerateFile_Click"></asp:Button>
                        </div>
                        <div class="fright">
                            <asp:Button runat="server" ID="btnClear" class="btn btn-danger " Text="Clear" OnClick="btnClear_Click">
                            </asp:Button>
                        </div>
                        <div class="fclear">
                        </div>
                    </div>
                    <hr class="nomargin" />
                    <DInfo:DisplayInfo runat="server" ID="DInfo" />
                    <div class="panel-search">
                        <div class="table-responsive">
                            <div class="entryformmain">
                                <div class="entryform">
                                    <div class="labelstyle">
                                        <span class="mandatory">*</span> File :
                                    </div>
                                    <div class="controlstyle">
                                        <asp:FileUpload ID="FileLead" runat="server" />
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="rfvLeadNo" runat="server" ErrorMessage="Choose Lead File"
                                            ValidationGroup="save" Text="*" ControlToValidate="FileLead" SetFocusOnError="true"
                                            CssClass="ErrorLabelStyle"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="entryformmain">
                                <div class="entryform">
                                    <div class="labelstyle">
                                        &nbsp;
                                    </div>
                                    <div class="controlstyle">
                                        <asp:Button runat="server" ID="btnUpload" class="btn btn-primary" Text="Upload File"
                                            ValidationGroup="save" OnClick="btnUpload_Click"></asp:Button>
                                        &nbsp;
                                        <asp:Button runat="server" ID="btnSave" class="btn btn-primary" Text="Save" Visible="false"
                                            OnClick="btnSave_Click"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <hr class="nomargin" />
                    <div class="panel-search">
                        <div class="table-responsive">
                            <asp:GridView ID="dgvGridData" runat="server" AutoGenerateColumns="true" AllowSorting="false"
                                CssClass="table table-striped table-bordered table-hover GridWidth" ShowHeader="true">
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div>
            <asp:ValidationSummary ID="vsSearch" ShowMessageBox="true" EnableClientScript="true"
                HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                ValidationGroup="save" />
        </div>
    </div>
</asp:Content>
