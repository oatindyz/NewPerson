<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ViewRequestForm.aspx.cs" Inherits="WEB_PERSONAL.ViewRequestForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="ps-header">
        <img src="Image/Small/table.png" />รายละเอียดคำร้องการแก้ไขข้อมูล
    </div>

    <div style="text-align: center;">
        <div class="ps-div-title-red">รายละเอียดข้อมูล</div>
    </div>
    <div id="DataShow" runat="server">

        <div class="ps-box-ct10" style="text-align: center;">
            <div id="div1" runat="server" class="ps-table-1" style="display: inline-block; vertical-align: top; text-align: left;"></div>
        </div>
        <div style="text-align: center; margin-top: 10px;">
            <a href="Default.aspx" class="ps-button btn btn-primary">
                <img src="Image/Small/home3.png" class="icon_left" />กลับหน้าหลัก</a>
            <a href="RequestHistory.aspx" class="ps-button btn btn-primary">
                <img src="Image/Small/clock-history.png" class="icon_left" />ประวัติคำร้องแก้ไขข้อมูล</a>
        </div>

    </div>
</asp:Content>
