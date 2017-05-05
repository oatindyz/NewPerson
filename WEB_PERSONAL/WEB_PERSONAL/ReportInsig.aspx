<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ReportInsig.aspx.cs" Inherits="WEB_PERSONAL.ReportInsig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="plugins/select2/select2.min.css" />
    <!-- -->
    <script src="plugins/select2/select2.full.min.js"></script>

    <script>
        $(function () {
            $(".select2").select2();
        });
    </script>

    <style>
        .ppp {
            text-align: center;
        }

            .ppp table {
                display: inline-block;
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
        <img src="Image/Small/medal.png" />ออกรายงานเครื่องราชฯ
    </div>
    <div id="divOfficer" runat="server" visible="false" class="panel panel-default">
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
                            <td></td>
                            <td>
                                <asp:LinkButton ID="lbuV1Search" runat="server" CssClass="ps-button" OnClick="lbuV1Search_Click"><img src="Image/Small/search.png" class="icon_left"/>ค้นหา</asp:LinkButton>
                                <asp:LinkButton ID="lbuV1Export" runat="server" CssClass="ps-button" OnClick="lbuV1Export_Click"><img src="Image/Small/excel.png" class="icon_left"/>Export</asp:LinkButton>
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
                        <asp:Panel ID="Panel1" runat="server" CssClass="ppp" Style="text-align: left; margin: auto;" class="ps-table-1"></asp:Panel>
                    </div>
                    <div id="divUser" runat="server" visible="false">
                        <div style="text-align: center; margin-bottom: 10px;">
                            <asp:Panel ID="Panel2" runat="server" CssClass="ppp"></asp:Panel>
                            <asp:Label ID="lbFinish" runat="server" Text="ไม่มีข้อมูล" CssClass="lbGV"></asp:Label>
                        </div>
                    </div>
                    <div>
                        <asp:LinkButton ID="lbuV2Export" runat="server" CssClass="ps-button" OnClick="lbuV2Export_Click"><img src="Image/Small/excel.png" class="icon_left"/>Export</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
