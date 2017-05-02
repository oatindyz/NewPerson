<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ReportPerson-Admin.aspx.cs" Inherits="WEB_PERSONAL.ReportPerson_Admin" %>
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
    <div class="ps-header">
        <img src="Image/Small/person2.png" />ออกรายงานบุคลากร
    </div>
    <div id="Menu" runat="server" class="panel panel-default">
        <div class="panel-body">
            <div class="panel-body">
                <div style="text-align: center;">
                    <table style="text-align: left; margin: auto;" class="ps-table-1">
                        <tr>
                            <td>ปีงบประมาณ</td>
                            <td>
                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="ps-dropdown input-sm select2"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>ข้อมูลการแสดงผล</td>
                            <td>
                                <asp:DropDownList ID="ddlView" runat="server" CssClass="ps-dropdown input-sm select2" AutoPostBack="true"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>วิทยาเขต</td>
                            <td>
                                <asp:DropDownList ID="ddlCampus" runat="server" CssClass="form-control select2"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:LinkButton ID="lbuSearch" runat="server" OnClick="lbuSearch_Click" CssClass="ps-button ekknidRight ekknidTop"><img src="Image/Small/search.png" class="icon_left"/>แสดงผล</asp:LinkButton>
                                <asp:LinkButton ID="lbuExport" runat="server" CssClass="ps-button" OnClick="lbuExport_Click"><img src="Image/Small/excel.png" class="icon_left"/>ออกรายงาน Excel</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>

    <div class="ps-header">
        <img src="Image/Small/list.png" />ข้อมูล
    </div>
    <div style="margin-top: 10px; overflow-x: auto;">
        <asp:Panel ID="Panel1" runat="server" CssClass="ppp"></asp:Panel>
    </div>
</asp:Content>
