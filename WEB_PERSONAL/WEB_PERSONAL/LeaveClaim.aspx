<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="LeaveClaim.aspx.cs" Inherits="WEB_PERSONAL.LeaveClaim" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="default_page_style">
        <div class="ps-header"><img src="Image/Small/table.png" />สิทธิในการลา</div>
        <asp:GridView ID="GridView1" runat="server" CssClass="ps-gridview"></asp:GridView>
    </div>
</asp:Content>
