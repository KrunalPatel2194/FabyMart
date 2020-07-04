<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrderStatus.ascx.cs" Inherits="Admin_UserControls_OrderStatus" %>
<ul class="nav nav-tabs">
  
    <li id="Ordered" class="active" runat="server"><a href="Ordered.aspx">Ordered <asp:Label ID="lblOrdered" runat="server" Font-Bold="true"></asp:Label></a></li>
    <li id="Confirmed" class="" runat="server"><a href="Confirmed.aspx">Confirmed <asp:Label ID="lblConfirmed" runat="server"  Font-Bold="true"></asp:Label></a></li>
    <li id="ReadyToShip" class="" runat="server"><a href="ReadyToShip.aspx">Ready To Ship <asp:Label ID="lblReady" runat="server"  Font-Bold="true"></asp:Label></a></li>
    <li id="Shipped" class="" runat="server"><a href="Shipped.aspx">Shipped <asp:Label ID="lblShipped" runat="server"  Font-Bold="true"></asp:Label></a></li>
    <li id="Delivered" class="" runat="server"><a href="Delivered.aspx">Delivered <asp:Label ID="lblDelivered" runat="server"  Font-Bold="true"></asp:Label></a></li>
    <li id="Cancelled" class="" runat="server"><a href="Cancelled.aspx">Cancelled <asp:Label ID="lblCancelled" runat="server"  Font-Bold="true"></asp:Label></a></li>
    <li id="Returned" class="" runat="server"><a href="Returned.aspx">Returned <asp:Label ID="lblReturned" runat="server"  Font-Bold="true"></asp:Label></a></li>
    <li id="Complete" class="" runat="server"><a href="Complete.aspx">Complete <asp:Label ID="lblComplete" runat="server"  Font-Bold="true"></asp:Label></a></li>
    <li id="PaymentFail" class="" runat="server"><a href="PaymentFail.aspx">Pyment Fail <asp:Label ID="lblPaymentFail" runat="server"  Font-Bold="true"></asp:Label></a></li>
  

</ul>


