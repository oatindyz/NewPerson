<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="LeaveReport.aspx.cs" Inherits="WEB_PERSONAL.LeaveReport1" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
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
    <script>
        function showSelfView() {
         /*   var vdd = document.getElementById("ContentPlaceHolder1_ddlView");
            var vtr = document.getElementById("ContentPlaceHolder1_trSelfView");
            
            if (vdd.options[vdd.selectedIndex].value == "6") {
                vtr.style.display = "table-row";
            } else {
                */vtr.style.display = "none";
            }
        }
        function showSelfViewLeaveID() {
           /* var vdd = document.getElementById("ContentPlaceHolder1_ddlSelfView");
            var vtr = document.getElementById("ContentPlaceHolder1_trSelfViewLeaveID");

            if (vdd.options[vdd.selectedIndex].value == "2") {
                vtr.style.display = "table-row";
            } else {
                vtr.style.display = "none";
            }*/
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="ps-div-title-red">
            สรุปข้อมูลการลาหยุดราชการ ขาดราชการ มาสาย ประจำปีงบประมาณ
        </div>
        <div style="text-align: center;">
            <table style="display: inline-block; text-align: left;">
                <tr>
                    <td>การแสดงผล</td>
                    <td>
                        <asp:DropDownList ID="ddlView" runat="server" CssClass="ps-dropdown" ></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>ปีงบประมาณ</td>
                    <td>
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="ps-dropdown"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="color: #808080; background-color: #f0f0f0; text-align: center;">กรณีแสดงผลเฉพาะตนเอง</td>
                </tr>
                <tr id="trSelfView" runat="server">
                    <td>การแสดงผลตนเอง</td>
                    <td>
                        <asp:DropDownList ID="ddlSelfView" runat="server" CssClass="ps-dropdown" OnSelectedIndexChanged="ddlSelfView_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </td>
                </tr>
                <tr id="trSelfViewLeaveID" runat="server">
                    <td>รหัสการลา</td>
                    <td>
                        <asp:DropDownList ID="ddlSelfViewLeaveID" runat="server" CssClass="ps-dropdown"></asp:DropDownList>
                    </td>
                </tr>              
                <tr>
                    <td></td>
                    <td>
                        <asp:LinkButton ID="lbuShow" runat="server" CssClass="ps-button" OnClick="lbuShow_Click"><img src="Image/Small/search.png" class="icon_left"/>แสดงผล</asp:LinkButton>
                        <asp:LinkButton ID="lbuExport" runat="server" CssClass="ps-button" OnClick="lbuExport_Click"><img src="Image/Small/excel.png" class="icon_left"/>Export</asp:LinkButton>
                    </td>
                </tr>
            </table>
             
            
        </div>
        <div class="ps-separator"></div>

        <div style="margin-top: 10px;">
            <asp:Panel ID="Panel1" runat="server" CssClass="ppp"></asp:Panel>
        </div>
    </div>
    
   
</asp:Content>
