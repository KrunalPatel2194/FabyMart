<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Customer.ascx.cs" Inherits="Customer" %>
<div>
    <div>
        <%--<div class="float-lt">
            <a id="lnkBtnHome" runat="server">My Account </a>
        </div>
        <div class="float-lt">
            <a id="lnkbtnMyFavouriteProduct" runat="server">My Favourite</a>
        </div>
        <div class="float-lt">
        <a id="lnkbtnMyOrderList" runat="server">My Order List</a>
        </div>
        <div class="float-lt">
        <a id="lnkBtnUpdateProfile" runat="server">Update Profile </a>
        </div>
        <div class="float-lt">
        <a id="lnkBtnChangePwd" runat="server">Change Password </a>
        </div>
        <div class="float-lt">
        <asp:LinkButton ID="lnkBtnLogOut" runat="server" Text="Log Out" OnClick="lnkBtnLogOut_Click"></asp:LinkButton>
        </div>
        <div class="clear">
        </div>--%>
        <ul class="custom">
            <li><a class="" id="lnkBtnHome" runat="server">My Account </a></li>
            <li class="setMarginLeft"><a id="lnkbtnMyFavouriteProduct" runat="server">My Favourite</a></li>
            <li class="setMarginLeft"><a id="lnkbtnMyOrderList" runat="server">My Order List</a></li>
            <li class="setMarginLeft"><a id="lnkBtnUpdateProfile" runat="server">Update Profile </a></li>
            <li class="setMarginLeft"><a id="lnkBtnChangePwd" runat="server">Change Password </a></li>
            <li class="setMarginLeft"><a id="lnkTrackOrder" runat="server">Track Order </a></li>
            <li class="setMarginLeft">
                <asp:LinkButton ID="lnkBtnLogOut" runat="server" Text="Log Out" OnClick="lnkBtnLogOut_Click"></asp:LinkButton>
            </li>
        </ul>
    </div>
</div>
