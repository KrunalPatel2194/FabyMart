<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="RegisterWithFB.aspx.cs" Inherits="RegisterWithFB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta name="description" content="<%=metaDescription %>" />
    <meta name="keywords" content="<%=metaKeywords %>" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="mens setBottomMargin">
        <div class="main">
            <div class="wrap">
                <ul class="breadcrumb breadcrumb__t">
                    <a href='<%=objPageBase.GetAlias("Default.aspx") %>'>Home</a> / <a id="aCategory"
                        runat="server"></a><a id="aSubCategory" runat="server"></a>
                    <asp:Label runat="server" ID="lblLiteral"></asp:Label>
                </ul>
                <div>
                    <div id="FBReg" runat="server">
                        <%-- <iframe src="https://www.facebook.com/plugins/registration.php?client_id=1446033349037337&redirect_uri=http://www.fabymart.com/RegisterWithFB.aspx&fields=name,email,password,gender,birthday,location"
                            scrolling="auto" frameborder="true" style="border: 1px" allowtransparency="true"
                            width="78%" height="600"></iframe>--%>
                        <iframe src="https://www.facebook.com/plugins/registration.php?client_id=1446033349037337&redirect_uri=http://localhost:50727/fabymart/RegisterWithFB.aspx&fields=name,email,password,gender,birthday,location"
                            scrolling="auto" frameborder="true" style="border: 1px" allowtransparency="true"
                            width="78%" height="600"></iframe>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
