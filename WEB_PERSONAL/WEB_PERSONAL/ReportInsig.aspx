<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ReportInsig.aspx.cs" Inherits="WEB_PERSONAL.ReportInsig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderLeft" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="ps-header"><img src="Image/Small/person2.png" />ออกรายงานเครื่องราชฯ</div>

    <div>
        ประเภทรายงาน 
        <select id="ddlR" class="ps-button">
            <option value="1">แสดงรายชื่อผู้ที่ขอเครื่องราชฯ</option>
        </select>
    </div>
    

    <div style="margin: 10px 0;"></div>

    <div id="vm">
        <div id="v1">
            <asp:LinkButton ID="lbuV1Search" runat="server" CssClass="ps-button" OnClick="lbuV1Search_Click"><img src="Image/Small/search.png" class="icon_left"/>ค้นหา</asp:LinkButton>
            <asp:LinkButton ID="lbuV1Export" runat="server" CssClass="ps-button" OnClick="lbuV1Export_Click"><img src="Image/Small/excel.png" class="icon_left"/>Export</asp:LinkButton>
        </div>
    </div>

    <div class="ps-separator"></div>

    <asp:Panel ID="Panel1" runat="server"></asp:Panel>

    <script>
        $(document).ready(function () {
            $("#ddlR").change(function () {
                $("#v1").css("display", "none");
                //$("#v2").css("display", "none");
                switch ($(this).val()) {
                    case "1": $("#v1").css("display", "block"); break;
                    //case "2": $("#v2").css("display", "block"); break;
                }
            });
        });
        
    </script>

</asp:Content>
