<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="NoPermission.aspx.cs" Inherits="WEB_PERSONAL.NoPermission" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="NoPermissionz" runat="server">
        <div class="ps-div-title-red">แจ้งเตือน</div>
        <div style="color: #808080; margin-top: 10px; text-align: center;">
            คุณไม่มีสิทธิ์ใช้งานหน้านี้
        </div>
        <div style="text-align: center; margin-top: 10px;">
            <a href="Default.aspx" class="ps-button">
                <img src="Image/Small/home3.png" class="icon_left" />กลับหน้าหลัก</a>
        </div>
    </div>
    <!--<div id="NoStafftype" runat="server" visible="false">
        <div class="ps-div-title-red">แจ้งเตือน</div>
        <div style="color: #808080; margin-top: 10px; text-align: center;">
            ไม่สามารถใช้ได้ เนื่องจาก ประเภทบุคลากร:ไม่ใช่ข้าราชการหรือพนักงานในสถาบัน
        </div>
        <div style="text-align: center; margin-top: 10px;">
            <a href="Default.aspx" class="ps-button">
                <img src="Image/Small/home3.png" class="icon_left" />กลับหน้าหลัก</a>
        </div>
    </div>-->
</asp:Content>
