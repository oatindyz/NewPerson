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
                <div id="ShowOldPass" runat="server" class="ps-box-i0" visible="false">
                    <div class="ps-box-ct10">
                        <div class="ps-lb-red-b" style="text-align: center;">รหัสผ่านเก่า</div>
                        <div style="text-align: center;">
                            <asp:TextBox ID="tbOld" runat="server" CssClass="form-control input-sm" TextMode="Password" placeHolder="รหัสผ่านเก่า" required="required" TabIndex="1"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div id="ShowNewPass" runat="server" class="ps-box-i0" visible="false">
                    <div class="ps-box-ct10">
                         <div class="ps-lb-blue-b" style="text-align: center;">รหัสผ่านใหม่</div>
                        <div style="text-align: center;">
                            <div><asp:TextBox ID="tbNew" runat="server" CssClass="form-control input-sm" TextMode="Password" style="margin-bottom: 5px;" placeHolder="รหัสผ่านใหม่" required="required" TabIndex="1"></asp:TextBox></div>
                            <asp:TextBox ID="tbNew2" runat="server" CssClass="form-control input-sm" TextMode="Password" placeHolder="ยืนยันรหัสผ่าน" required="required" TabIndex="1"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="ps-box-i0">
                    <div class="ps-box-ct10">
                        <asp:Button ID="lbuFinish1" runat="server" CssClass="ps-button" OnClick="lbuFinish_Click" Text="เปลี่ยนรหัสผ่าน"></asp:Button>
                    </div>
                </div>
            </div>

        </div>
        <div class="ps-separator"></div>
        
    </div>
</asp:Content>
