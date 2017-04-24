<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="INS_GetList.aspx.cs" Inherits="WEB_PERSONAL.INS_GetList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="ps-header">
        <img src="Image/Small/medal.png" />รายชื่อผู้ที่ได้รับเครื่องราชฯ
    </div>
    <div>

        <div class="ps-div-title-red">ประวัติการได้รับเครื่องราชฯ</div>
        <asp:Label ID="lbHistory" runat="server" Text="ไม่พบข้อมูล" CssClass="lbGV"></asp:Label>
        <asp:GridView ID="gvHistory" runat="server" CssClass="ps-table-1" Style="margin: 0 auto; margin-bottom: 20px;" AllowPaging="True" PageSize="10" OnPageIndexChanging="gvHistory_PageIndexChanging"></asp:GridView>

    </div>
</asp:Content>
