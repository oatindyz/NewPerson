<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ReportInsig.aspx.cs" Inherits="WEB_PERSONAL.ReportInsig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderLeft" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="ps-header">
        <img src="Image/Small/person2.png" />ออกรายงานเครื่องราชฯ</div>
    <div id="divOfficer" runat="server" visible="false" class="panel panel-default">
        <div class="panel-body">
            <div class="panel-body">
                <div style="text-align: center;">
                    <table style="text-align: left; margin: auto;" class="ps-table-1">
                        <tr>
                            <td>เลือกข้อมูลการแสดงผล</td>
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
                                <asp:LinkButton ID="lbuV1Search" runat="server" CssClass="ps-button" OnClick="lbuV1Search_Click"><img src="Image/Small/search.png" class="icon_left"/>ค้นหา</asp:LinkButton>
                                <asp:LinkButton ID="lbuV1Export" runat="server" CssClass="ps-button" OnClick="lbuV1Export_Click"><img src="Image/Small/excel.png" class="icon_left"/>Export</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <asp:Panel ID="Panel1" runat="server" CssClass="ppp"></asp:Panel>
    </div>

    <div id="divUser" runat="server" visible="false" class="panel panel-default">
        <div class="panel-body">
            <div class="panel-body">
                <div style="text-align: center;">
                     <asp:Panel ID="Panel2" runat="server" CssClass="ppp"></asp:Panel>
                </div>
            </div>
        </div>
       
    </div>

</asp:Content>
