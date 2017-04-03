<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="WorkingTimeHistory.aspx.cs" Inherits="WEB_PERSONAL.WorkingTimeHistory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(function () {
            $("#ContentPlaceHolder1_tbDate").datepicker($.datepicker.regional["th"]);
        });
    </script>
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
            width: 100px;
            height: 75px;
            line-height: 75px;
            font-size: 12px;
            background-color: #ffffff;
            border-bottom: 1px solid #ffffff;
            border-right: 1px solid #ffffff;
            text-decoration: none;
            text-align: center;
        }
        .d_break span {
            display: inline-block;
            width: 100px;
            height: 75px;
            line-height: 30px;
            font-size: 12px;
            color: #ffffff;
            background-color: #ff478b;
            border-bottom: 1px solid #932b51;
            border-right: 1px solid #932b51;
            text-decoration: none;
            text-align: center;
        }
        .d_ss span {
            display: inline-block;
            width: 100px;
            height: 75px;
            line-height: 75px;
            font-size: 16px;
            color: #ffffff;
            background-color: #ff005e;
            border-bottom: 1px solid #c10047;
            border-right: 1px solid #c10047;
            text-decoration: none;
            text-align: center;
        }


        .d_normal span {
            display: inline-block;
            width: 100px;
            height: 75px;
            line-height: 30px;
            font-size: 12px;
            color: #000000;
            background-color: #f0f0f0;
            border-bottom: 1px solid #c3c3c3;
            border-right: 1px solid #c3c3c3;
            text-decoration: none;
            text-align: center;
        }
        .d_late span {
            display: inline-block;
            width: 100px;
            height: 75px;
            line-height: 30px;
            font-size: 12px;
            color: #ffffff;
            background-color: #ff7d00;
            border-bottom: 1px solid #c86200;
            border-right: 1px solid #c86200;
            text-decoration: none;
            text-align: center;
        }
        .d_absent span {
            display: inline-block;
            width: 100px;
            height: 75px;
            line-height: 30px;
            font-size: 12px;
            color: #ffffff;
            background-color: red;
            border-bottom: 1px solid #d30000;
            border-right: 1px solid #d30000;
            text-decoration: none;
            text-align: center;
        }
        .d_leave span {
            display: inline-block;
            width: 100px;
            height: 75px;
            line-height: 30px;
            font-size: 12px;
            color: #ffffff;
            background-color: #00a2e8;
            border-bottom: 1px solid #0071a1;
            border-right: 1px solid #0071a1;
            text-decoration: none;
            text-align: center;
        }
        .d_no_data span {
            display: inline-block;
            width: 100px;
            height: 75px;
            line-height: 30px;
            font-size: 12px;
            color: #808080;
            background-color: #202020;
            border-bottom: 1px solid #101010;
            border-right: 1px solid #101010;
            text-decoration: none;
            text-align: center;
        }
        .d_day_head {
            display: inline-block;
            width: 100px;
            height: 50px;
            line-height: 50px;
            font-size: 14px;
            color: #ffffff;
            background-color: #404040;
            border-right: 1px solid #303030;
            text-decoration: none;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="default_page_style">
        <div class="ps-header"><img src="Image/Small/table.png" />ประวัติเวลาปฏิบัติงาน</div>
        <div class="ps-box" style="margin-top: 10px;">
            <div class="ps-box-i0">
                <div class="ps-box-hd10">
                    <asp:Panel ID="Panel2" runat="server" DefaultButton="lbuSearchV2">
                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="ps-dropdown">
                            <asp:ListItem Text="มกราคม" Value="1"></asp:ListItem>
                            <asp:ListItem Text="กุมภาพันธ์" Value="2"></asp:ListItem>
                            <asp:ListItem Text="มีนาคม" Value="3"></asp:ListItem>
                            <asp:ListItem Text="เมษายน" Value="4"></asp:ListItem>
                            <asp:ListItem Text="พฤษภาคม" Value="5"></asp:ListItem>
                            <asp:ListItem Text="มิถุนายน" Value="6"></asp:ListItem>
                            <asp:ListItem Text="กรกฎาคม" Value="7"></asp:ListItem>
                            <asp:ListItem Text="สิงหาคม" Value="8"></asp:ListItem>
                            <asp:ListItem Text="กันยายน" Value="9"></asp:ListItem>
                            <asp:ListItem Text="ตุลาคม" Value="10"></asp:ListItem>
                            <asp:ListItem Text="พฤษจิกายน" Value="11"></asp:ListItem>
                            <asp:ListItem Text="ธันวาคม" Value="12"></asp:ListItem>
                        </asp:DropDownList>

                        <asp:TextBox ID="tbYear" runat="server" CssClass="ps-textbox" placeHolder="ปี พ.ศ." style="width: 50px;"></asp:TextBox>
                        <asp:LinkButton ID="lbuSearchV2" runat="server" OnClick="lbuSearchV2_Click" CssClass="ps-button"><img src="Image/Small/search.png" class="icon_left"/>ค้นหา</asp:LinkButton>
                        <asp:Label ID="lbError" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </asp:Panel>
                </div>
            </div>
            <div class="ps-box-i0">
                
                <div class="ps-box-ct10">
                    <asp:Panel ID="Panel1" runat="server"></asp:Panel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
