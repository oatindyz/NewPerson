<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="WEB_PERSONAL.ChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="ps-header">
            <img src="Image/Small/wrench.png" />เปลี่ยนรหัสผ่าน
        </div>
        <div style="text-align: center;">

            <div style="text-align: center; margin-bottom: 10px;">
                <asp:Label ID="lbResult" runat="server" Text="" CssClass="ps-div-title-red"></asp:Label>
            </div>

            <div class="ps-box-il" style="width: 300px;">
                <div class="ps-box-i0">
                    <div class="ps-box-ct10">
                        <div class="ps-lb-red-b" style="text-align: center;">รหัสผ่านเก่า</div>
                        <div style="text-align: center;">
                            <asp:TextBox ID="tbOld" runat="server" CssClass="ps-textbox" TextMode="Password" MaxLength="12" placeHolder="รหัสผ่านเก่า"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="ps-box-i0">
                    <div class="ps-box-ct10">
                         <div class="ps-lb-blue-b" style="text-align: center;">รหัสผ่านใหม่</div>
                        <div style="text-align: center;">
                            <div><asp:TextBox ID="tbNew" runat="server" CssClass="ps-textbox" TextMode="Password" MaxLength="12" style="margin-bottom: 5px;" placeHolder="รหัสผ่านใหม่"></asp:TextBox></div>
                            <asp:TextBox ID="tbNew2" runat="server" CssClass="ps-textbox" TextMode="Password" MaxLength="12" placeHolder="ยืนยันรหัสผ่าน"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="ps-box-i0">
                    <div class="ps-box-ct10">
                        <asp:LinkButton ID="lbuFinish" runat="server" CssClass="ps-button" OnClick="lbuFinish_Click"><img src="Image/Small/wrench.png" class="icon_left"/>เปลี่ยนรหัสผ่าน</asp:LinkButton>
                    </div>
                </div>
            </div>

        </div>
        <div class="ps-separator"></div>
        
    </div>
</asp:Content>
