<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="NoPermission.aspx.cs" Inherits="WEB_PERSONAL.NoPermission" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="ps-div-title-red">แจ้งเตือน</div>
        <div style="color: #808080; margin-top: 10px; text-align: center;">
            คุณไม่มีสิทธิ์ใช้งานหน้าดังกล่าว เนื่องจากสิทธิ์ไม่ถึง
        </div>
        <div style="text-align: center; margin-top: 10px;">
            <a href="Default.aspx" class="ps-button">
                <img src="Image/Small/home3.png" class="icon_left" />กลับหน้าหลัก</a>
        </div>
    </div>
</asp:Content>
