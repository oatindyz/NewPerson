<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="WorkingDay.aspx.cs" Inherits="WEB_PERSONAL.WorkingDay" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .calendar {
            
        }
        .calendar_info {
            margin-top: 20px;
        }
        .calendar_info_item {
            display: inline-block;
            border: 1px solid #f0f0f0;
            padding: 5px 20px;
        }
        .calendar_info_item2 {
            display: inline-block;
            width: 55px;
            height: 50px;
            line-height: 50px;
            font-size: 20px;
            text-decoration: none;
            text-align: center;
        }
        .d_table {
            border-collapse: collapse;
            display: inline-table;
            margin-right: 50px;
        }
        .d_table td {
            margin: 0px;
            padding: 0px;
            display: inline-block;
        }
        .d_txt {
            text-align: center;
            font-size: 20px;
        }
        .d_blank {
            display: inline-block;
            width: 55px;
            height: 50px;
            line-height: 50px;
            font-size: 24px;
            background-color: #ffffff;
            text-decoration: none;
            text-align: center;
        }
        .d_ss a {
            display: inline-block;
            width: 55px;
            height: 50px;
            line-height: 50px;
            font-size: 20px;
            color: #ffffff;
            background-color: #ff005e;
            text-decoration: none;
            text-align: center;
        }
        /*.d_ss a:hover {
            background-color: #c30048;
        }*/
        .d_break a {
            display: inline-block;
            width: 55px;
            height: 50px;
            line-height: 50px;
            font-size: 20px;
            color: #ffffff;
            background-color: #ff478b;
            text-decoration: none;
            text-align: center;
        }
        .d_break a:hover {
            background-color: #bb3667;
        }
        .d_work a {
            display: inline-block;
            width: 55px;
            height: 50px;
            line-height: 50px;
            font-size: 20px;
            color: #808080;
            background-color: #f0f0f0;
            text-decoration: none;
            text-align: center;
        }
        .d_work a:hover {
            color: #ffffff;
            background-color: #bbbbbb;
        }
        .d_day_head {
            display: inline-block;
            width: 55px;
            height: 30px;
            line-height: 30px;
            font-size: 14px;
            color: #ffffff;
            background-color: #404040;
            text-decoration: none;
            text-align: center;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="default_page_style">
        <div class="ps-header"><img src="Image/Small/wrench.png" />จัดการวันปฏิบัติราชการ</div>
        <div class="ps-box-il">
            <div class="ps-box-i0">
                <div class="ps-box-hd10"><img src="Image/Small/calendar.png" class="icon_left"/>กำหนดวันหยุดราชการ</div>
                <div class="ps-box-ct10">
                     <div class="calendar">
            <asp:Table ID="Table1" runat="server" CssClass="d_table">
 
            </asp:Table>
            <asp:Table ID="Table2" runat="server" CssClass="d_table">
 
            </asp:Table>
        </div>
        <div class="calendar_info">
            <div class="calendar_info_item">
                <span class="calendar_info_item2" style="color: #808080; background-color: #f0f0f0;">1</span>
                <span style="display: inline-block; margin-left: 5px;">วันทำงานธรรมดา</span>
            </div>
            <div class="calendar_info_item">
                <span class="calendar_info_item2" style="color: #ffffff; background-color: #ff478b;">1</span>
                <span style="display: inline-block; margin-left: 5px;">วันหยุดนักขัตฤกษ์</span>
            </div>
            <div class="calendar_info_item">
                <span class="calendar_info_item2" style="color: #ffffff; background-color: #ff005e;">1</span>
                <span style="display: inline-block; margin-left: 5px;">วันหยุดเสาร์อาทิตย์</span>
            </div>
        </div>
        <div style="clear: both;"></div>

                </div>
            </div>
            <div class="ps-box-i0">
                <div class="ps-box-hd10"><img src="Image/Small/clock.png" class="icon_left"/>ปรับเวลาขาดมาสาย</div>
                <div class="ps-box-ct10">
                    <table class="ps-table-x16">
                <tr>
                    <td class="col1">เวลาสาย</td>
                    <td class="col2">
                        <asp:TextBox ID="tbLateHour" runat="server" Width="50" CssClass="ps-textbox" placeHolder="ชั่วโมง"></asp:TextBox>
                        <asp:TextBox ID="tbLateMinute" runat="server" Width="50" CssClass="ps-textbox" placeHolder="นาที"></asp:TextBox>
                        <asp:LinkButton ID="lbuChangeLate" runat="server" CssClass="ps-button" OnClick="lbuChangeLate_Click">ปรับ</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td class="col1">เวลาขาด</td>
                    <td class="col2">
                        <asp:TextBox ID="tbAbsentHour" runat="server" Width="50" CssClass="ps-textbox" placeHolder="ชั่วโมง"></asp:TextBox>
                        <asp:TextBox ID="tbAbsentMinute" runat="server" Width="50" CssClass="ps-textbox" placeHolder="นาที"></asp:TextBox>
                        <asp:LinkButton ID="lbuChangeAbsent" runat="server" CssClass="ps-button" OnClick="lbuChangeAbsent_Click">ปรับ</asp:LinkButton>
                    </td>
                </tr>
            </table>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
