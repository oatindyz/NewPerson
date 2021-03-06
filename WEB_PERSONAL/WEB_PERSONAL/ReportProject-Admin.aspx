﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ReportProject-Admin.aspx.cs" Inherits="WEB_PERSONAL.ReportProject_Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- -->
    <link rel="stylesheet" href="plugins/select2/select2.min.css" />
    <!-- -->
    <script src="plugins/select2/select2.full.min.js"></script>

    <script>
        $(function () {
            $(".select2").select2();
        });
    </script>

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
            <img src="Image/Small/copy.png" />ออกรายงานข้อมูลการพัฒนาบุคลากร
        </div>
        <div id="notification" runat="server"></div>

        <div id="Menu" runat="server" class="panel panel-default">
            <div class="panel-body">
                <div class="panel-body">
                    <div style="text-align: center;">
                        <table style="text-align: left; margin: auto;" class="ps-table-1">
                            <tr>
                                <td>ข้อมูลการแสดงผล</td>
                                <td>
                                    <asp:DropDownList ID="ddlView" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>ปีงบประมาณ</td>
                                <td>
                                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>ชื่อบุคลากร</td>
                                <td>
                                    <asp:DropDownList ID="ddlPerson" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>วิทยาเขต</td>
                                <td>
                                    <asp:DropDownList ID="ddlCampus" runat="server" CssClass="form-control"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>ประเภทการอบรม</td>
                                <td>
                                    <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>รูปแบบประเภทการอบรม</td>
                                <td>
                                    <asp:DropDownList ID="ddlSubCountry" runat="server" CssClass="form-control"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>วันที่เข้าร่วม - วันสิ้นสุดโครงการ</td>
                                <td>
                                    <asp:TextBox ID="tbStartDate" runat="server" CssClass="ps-textbox ekknidRight"></asp:TextBox><asp:TextBox ID="tbEndDate" runat="server" CssClass="ps-textbox"></asp:TextBox>
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
        <div id="Div1" runat="server" class="panel panel-default">
            <div class="panel-body">
                <div class="panel-body">
                    <div style="text-align: center;">
                        <div style="margin-top: 10px; overflow-x: auto;">
                            <asp:Panel ID="Panel1" runat="server" CssClass="ppp"></asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
