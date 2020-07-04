<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="MyAccount.aspx.cs" Inherits="MyAccount" %>

<%@ Register Src="~/UserControls/Customer.ascx" TagName="Customer" TagPrefix="Home" %>
<%@ Register Src="Admin/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta name="description" content="<%=metaDescription %>" />
    <meta name="keywords" content="<%=metaKeywords %>" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="mens">
        <div class="main">
            <div class="wrap">
                <div class="pad">
                    <ul class="breadcrumb breadcrumb__t">
                        <a class="" href='<%= objPageBase.GetAlias("Default.aspx")%>'>Home</a> / <span>My Account</span>
                    </ul>
                   <%-- <div>
                        <Home:Customer ID="CusomerControl" runat="server" />
                    </div>--%>
                </div>
                <div class="minHeight">
                <div class="cont span_2_of_3">
                </div>
                </div>
                <div class="rsingle span_1_of_single">
                    <%-- <Home:Customer ID="CusomerControl" runat="server" />--%>
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
