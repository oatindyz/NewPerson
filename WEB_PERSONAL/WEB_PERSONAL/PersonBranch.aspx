<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="PersonBranch.aspx.cs" Inherits="WEB_PERSONAL.PersonBranch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="ps-header">
            <img src="Image/Small/person2.png" />แผนผังบุคลากร
        </div>
        <div>
            <table class="ps-table-1" style="margin: 0 auto; margin-bottom: 20px;">
                <tr>
                    <td class="col1">วิทยาเขต</td>
                    <td class="col2">
                        <asp:DropDownList ID="ddlCampus" runat="server" CssClass="ps-dropdown" OnSelectedIndexChanged="ddlCampus_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="col1">สำนัก / สถาบัน / คณะ</td>
                    <td class="col2">
                        <asp:DropDownList ID="ddlFaculty" runat="server" CssClass="ps-dropdown" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="col1">กอง / สำนักงานเลขา / ภาควิชา</td>
                    <td class="col2">
                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="ps-dropdown" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </td>
                </tr>
                <tr id="trWorkDivision" runat="server">
                    <td class="col1">งาน / ฝ่าย</td>
                    <td class="col2">
                        <asp:DropDownList ID="ddlWorkDivision" runat="server" CssClass="ps-dropdown"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center;">
                        <asp:LinkButton ID="lbuSearch" runat="server" OnClick="lbuSearch_Click" CssClass="ps-button"><img src="Image/Small/search.png" class="icon_left"/>ค้นหาบุคลากร</asp:LinkButton>
                    </td>
                    
                </tr>
            </table>
        </div>
        <div id="ccc" runat="server" style="display: none;">
            <div class="ps-div-title-red">หัวหน้า<span id="spWorkDivisionName" runat="server"></span></div>
            <asp:Panel ID="pBoss" runat="server" style="text-align: center;"></asp:Panel>
            <div class="ps-separator"></div>
            <div class="ps-div-title-red" style="margin-top: 20px;">บุคลากร</div>
            <asp:Panel ID="pMember" runat="server" style="text-align: center;"></asp:Panel>
        </div>
        
    </div>
    
</asp:Content>