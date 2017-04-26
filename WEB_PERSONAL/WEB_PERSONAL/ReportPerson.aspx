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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="default_page_style">
        <div class="ps-header">
        <img src="Image/Small/person2.png" />ออกรายงานบุคลากร</div>
        <div style="margin-bottom: 10px">
            <asp:DropDownList ID="ddlView" runat="server" CssClass="ps-dropdown input-sm select2" AutoPostBack="true"></asp:DropDownList>
        </div>
        <div class="ps-header">
            <img src="Image/Small/list.png" />ข้อมูล
        </div>
        <asp:Panel ID="Panel1" runat="server"></asp:Panel>

        <div style="text-align: center; margin: auto; margin-top: 10px;">
            <asp:LinkButton ID="lbuExport" runat="server" CssClass="ps-button" OnClick="lbuExport_Click"><img src="Image/Small/excel.png" class="icon_left"/>Export</asp:LinkButton>
        </div>
    </div>
</asp:Content>
