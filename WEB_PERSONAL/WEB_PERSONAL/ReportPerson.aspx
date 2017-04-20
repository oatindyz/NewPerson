<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ReportPerson.aspx.cs" Inherits="WEB_PERSONAL.ReportPerson" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(function () {
            $("#ContentPlaceHolder1_tbStartDate, #ContentPlaceHolder1_tbEndDate").datepicker($.datepicker.regional["th"]);
        });
    </script>

    <style>
        .ppp {
            text-align: center;
        }
        .ppp table {
            text-align: center;
        }
        .ppp th {
            text-align: center;
        }
        .ppp td {
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderLeft" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="ps-header"><img src="Image/Small/person2.png" />ออกรายงานบุคลากร</div>

    <div>
        ประเภทรายงาน 
        <select id="ddlR" class="ps-button">
            <option value="1">แสดงผู้จบปริญญา</option>
            <option value="2">แสดงข้อมูลจัดส่ง สกอ.</option>
        </select>
    </div>
    

    <div style="margin: 10px 0;"></div>

    <div id="vm">
        <div id="v1">
            <asp:LinkButton ID="lbuV1Search" runat="server" CssClass="ps-button" OnClick="lbuV1Search_Click"><img src="Image/Small/search.png" class="icon_left"/>ค้นหา</asp:LinkButton>
            <asp:LinkButton ID="lbuV1Export" runat="server" CssClass="ps-button" OnClick="lbuV1Export_Click"><img src="Image/Small/excel.png" class="icon_left"/>Export</asp:LinkButton>
        </div>
        <div id="v2" style="display: none;">
            <asp:LinkButton ID="lbuV2Search" runat="server" CssClass="ps-button" OnClick="lbuV2Search_Click"><img src="Image/Small/search.png" class="icon_left"/>ค้นหา</asp:LinkButton>
            <asp:LinkButton ID="lbuV2Export" runat="server" CssClass="ps-button" OnClick="lbuV2Export_Click"><img src="Image/Small/excel.png" class="icon_left"/>Export</asp:LinkButton>
        </div>
    </div>

    <div class="ps-separator"></div>

    <asp:Panel ID="Panel1" runat="server"></asp:Panel>

    <script>
        $(document).ready(function () {
            $("#ddlR").change(function () {
                $("#v1").css("display", "none");
                $("#v2").css("display", "none");
                switch ($(this).val()) {
                    case "1": $("#v1").css("display", "block"); break;
                    case "2": $("#v2").css("display", "block"); break;
                }
            });
        });
        
    </script>

</asp:Content>