<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="ContentPage.aspx.cs" Inherits="ContentPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta name="description" content="<%=metaDescription %>" />
    <meta name="keywords" content="<%=metaKeywords %>" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="mens setBottomMargin">
        <div class="main">
            <div class="wrap">
                <ul class="breadcrumb breadcrumb__t">
                    <a href='<%=objPageBase.GetAlias("Default.aspx") %>'>Home</a> / <span id="divTitle" runat="server"></span>
                </ul>
                <div class="content-top" id="divDescription" runat="server" style="text-align:justify;padding:5px;margin-top:5px;">
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
</asp:Content>
