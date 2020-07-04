<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Inquiry.ascx.cs" Inherits="Inquiry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/DisplayInfo.ascx" TagName="DisplayInfo" TagPrefix="DInfo" %>
<input type="button" runat="server" id="btnmpeInquiry" style="display: none;" />
<cc1:ModalPopupExtender ID="mpeInquiry" runat="server" TargetControlID="btnmpeInquiry"
    PopupControlID="divInquiry" BackgroundCssClass="modalbackground" DropShadow="false"
    CancelControlID="imbtnClose">
</cc1:ModalPopupExtender>
<div id="divInquiry" runat="server" class="modalpopup modalInquiry" >
    <asp:ImageButton ID="imbtnClose" CssClass="modalbClose" ImageUrl="~/images/popup_close_btn.png"
        runat="server"></asp:ImageButton>
    <div class="modaldetail">
        <asp:Panel ID="pnal" runat="server" DefaultButton="btnInquiry">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modalheader">
                        Inquiry
                    </div>
                    <DInfo:DisplayInfo runat="server" ID="DInfo" />
                    <div class="registration_form">
                        <div class="divInquiryPopup" >
                            <div>
                                <label>
                                    <asp:TextBox ID="txtName" runat="server" placeholder="Enter Name" Width="200px"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RFVname" runat="server" ErrorMessage="Enter Name"
                                        ValidationGroup="Inquiry" Text="*" ControlToValidate="txtName" SetFocusOnError="true"
                                        CssClass="mandatory"></asp:RequiredFieldValidator>
                                </label>
                            </div>
                            <div>
                                <label>
                                    <asp:TextBox ID="txtMobile" runat="server" placeholder="Enter Mobile" Width="200px"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RFVMobiu" runat="server" ErrorMessage="Enter Mobile"
                                        ValidationGroup="Inquiry" Text="*" ControlToValidate="txtMobile" SetFocusOnError="true"
                                        CssClass="mandatory"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator Display="Dynamic" ID="REVMobile" runat="server" ValidationGroup="Inquiry"
                                        Text="*" ControlToValidate="txtMobile" SetFocusOnError="true" CssClass="mandatory"></asp:RegularExpressionValidator>
                                </label>
                            </div>
                            <div>
                                <label>
                                    <asp:TextBox ID="txtEmail" runat="server" placeholder="Enter Email" Width="200px"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RFVEmail" runat="server" ErrorMessage="Enter Email"
                                        ValidationGroup="Inquiry" Text="*" ControlToValidate="txtEmail" SetFocusOnError="true"
                                        CssClass="mandatory"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator Display="Dynamic" ID="REVEmail" runat="server" ValidationGroup="Inquiry"
                                        Text="*" ControlToValidate="txtEmail" SetFocusOnError="true" CssClass="mandatory"></asp:RegularExpressionValidator>
                                </label>
                            </div>
                            <div>
                                <label>
                                    <asp:TextBox ID="txtMessage" runat="server" placeholder="Enter Message" Width="200px"
                                        TextMode="MultiLine"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RFVMessage" runat="server" ErrorMessage="Enter Message"
                                        ValidationGroup="Inquiry" Text="*" ControlToValidate="txtMessage" SetFocusOnError="true"
                                        CssClass="mandatory"></asp:RequiredFieldValidator>
                                </label>
                            </div>
                            <div class="sky-form" style="padding-bottom: 2px;">
                                <asp:Button ID="btnInquiry" runat="server" Text="Send" ValidationGroup="Inquiry"
                                    TabIndex="10" OnClick="btnInquiry_Click" CssClass="btnInquiry" />
                            </div>
                        </div>
                        <!-- /Form -->
                        <asp:ValidationSummary ID="vsLogin" ShowMessageBox="true" EnableClientScript="true"
                            HeaderText="You must Enter Following Fields" ShowSummary="false" runat="server"
                            ValidationGroup="Inquiry" />
                    </div>
                    <asp:HiddenField ID="hdnProductDetailId" runat="server" Value="" />
                    <asp:HiddenField ID="hdnURL" runat="server" Value="" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
</div>
