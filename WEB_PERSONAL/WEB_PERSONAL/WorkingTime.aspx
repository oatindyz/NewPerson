<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="WorkingTime.aspx.cs" Inherits="WEB_PERSONAL.WorkingTime" MaintainScrollPositionOnPostback="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function f1() {
            if ($("#ContentPlaceHolder1_cbVX1Late").prop("checked")) {
                $("#ContentPlaceHolder1_tbVX1HourIn").prop("disabled", true);
                $("#ContentPlaceHolder1_tbVX1MinuteIn").prop("disabled", true);
                $("#ContentPlaceHolder1_tbVX1HourOut").prop("disabled", true);
                $("#ContentPlaceHolder1_tbVX1MinuteOut").prop("disabled", true);
            } else {
                $("#ContentPlaceHolder1_tbVX1HourIn").prop("disabled", false);
                $("#ContentPlaceHolder1_tbVX1MinuteIn").prop("disabled", false);
                $("#ContentPlaceHolder1_tbVX1HourOut").prop("disabled", false);
                $("#ContentPlaceHolder1_tbVX1MinuteOut").prop("disabled", false);
            }
            
        }
        $(function () {
            $("#ContentPlaceHolder1_tbVX1Date").datepicker($.datepicker.regional["th"]);
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="default_page_style">
        <div class="ps-header"><img src="Image/Small/clock.png" />การลงเวลาปฏิบัติงาน</div>
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="View1" runat="server">
                <table class="ps-table">
                    <tr>
                        <td colspan="2" class="head">ข้อมูลการลงเวลาปฏิบัติงาน</td>
                    </tr>
                    <tr>
                        <td class="col1">รหัสพนักงาน</td>
                        <td class="col2">
                            <asp:TextBox ID="tbVX1CitizenID" runat="server" CssClass="ps-textbox"></asp:TextBox>
                            <asp:Label ID="lbVX1CitizenIDVD" runat="server" ForeColor="Red" Text="*" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">วันที่</td>
                        <td class="col2">
                            <asp:TextBox ID="tbVX1Date" runat="server" CssClass="ps-textbox"></asp:TextBox>
                            <asp:Label ID="lbVX1DateVD" runat="server" ForeColor="Red" Text="*" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">ประเภทกะงาน</td>
                        <td class="col2">
                            <div style="margin: 3px 0;">
                                <asp:DropDownList ID="ddlVX1WorktimeSec" runat="server" CssClass="dropdown dropdown_default" OnSelectedIndexChanged="ddlVX1WorktimeSec_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                <asp:Label ID="lbVX1KaVD" runat="server" ForeColor="Red" Text="*" Visible="False"></asp:Label>
                            </div>
                            <div style="margin: 3px 0;">
                                <asp:Label ID="lbVX1WorkTimeTime" runat="server"></asp:Label>
                            </div>
                            <div style="margin: 3px 0;">
                                <asp:Label ID="lbVX1WorkTimeDes" runat="server"></asp:Label>
                            </div>
                            
                            
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">เวลาที่มาปฏิบัติงาน</td>
                        <td class="col2">
                            <div style="margin: 3px 0;">
                                <asp:CheckBox ID="cbVX1Late" runat="server" text="ขาด" OnClick="f1();"/>
                            </div>
                            <div>
                                <div style="margin: 3px 0;">
                                    เข้าเวลา
                                    <asp:TextBox ID="tbVX1HourIn" runat="server" placeHolder="ชั่วโมง" Width="50" CssClass="ps-textbox"></asp:TextBox>
                                    
                                    <asp:Label ID="lbVX1HourInVD" runat="server" ForeColor="Red" Text="*" Visible="False"></asp:Label>
                                    
                                    <asp:TextBox ID="tbVX1MinuteIn" runat="server" placeHolder="นาที" Width="51px" CssClass="ps-textbox"></asp:TextBox>
                                    <asp:Label ID="lbVX1MinuteInVD" runat="server" ForeColor="Red" Text="*" Visible="False"></asp:Label>
                                </div>
                                <div style="margin: 3px 0;">
                                    เลิกเวลา
                                    <asp:TextBox ID="tbVX1HourOut" runat="server" placeHolder="ชั่วโมง" Width="50" CssClass="ps-textbox"></asp:TextBox>
                                    <asp:Label ID="lbVX1HourOutVD" runat="server" ForeColor="Red" Text="*" Visible="False"></asp:Label>
                                    <asp:TextBox ID="tbVX1MinuteOut" runat="server" placeHolder="นาที" Width="50" CssClass="ps-textbox"></asp:TextBox>
                                    <asp:Label ID="lbVX1MinuteOutVD" runat="server" ForeColor="Red" Text="*" Visible="False"></asp:Label>
                                </div>
                                
                                
                            </div>
                            
                            
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">หมายเหตุ</td>
                        <td class="col2">
                            <asp:TextBox ID="tbVX1Comment" runat="server" CssClass="ps-textbox"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <div class="ps-separator"></div>
                <asp:LinkButton ID="lbuVX1Next" runat="server" CssClass="ps-button" OnClick="lbuVX1Next_Click">ต่อไป<img src="Image/Small/next.png" class="icon_right"/></asp:LinkButton>
            </asp:View>
            <asp:View ID="View2" runat="server">
                <table class="ps-table">
                    <tr>
                        <td colspan="2" class="head">ข้อมูลการลงเวลาปฏิบัติงาน</td>
                    </tr>
                    <tr>
                        <td class="col1">รหัสพนักงาน : </td>
                        <td class="col2">
                            <asp:Label ID="lbVX2CitizenID" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">ชื่อ : </td>
                        <td class="col2">
                            <asp:Label ID="lbVX2Name" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">วันที่ : </td>
                        <td class="col2">
                            <asp:Label ID="lbVX2Date" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">กะงาน : </td>
                        <td class="col2">
                            <asp:Label ID="lbVX2Ka" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">เวลาที่มาปฏิบัติงาน : </td>
                        <td class="col2">
                            <asp:Label ID="lbVX2Time" runat="server" Text="Label"></asp:Label>
                            <div>
                                <asp:Label ID="lbVX2Late" runat="server" Text="Label"></asp:Label>
                            </div>
                            <div>
                                <asp:Label ID="lbVX2Over" runat="server" Text="Label"></asp:Label>
                            </div>
                            
                            
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">หมายเหตุ : </td>
                        <td class="col2">
                            <asp:Label ID="lbVX2Comment" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                </table>
                <div class="ps-separator"></div>
                <asp:LinkButton ID="lbuVX2Back" runat="server" CssClass="ps-button" OnClick="lbuVX2Back_Click"><img src="Image/Small/back.png" class="icon_left"/>ย้อนกลับ</asp:LinkButton>
                            <asp:LinkButton ID="lbuVX2Finish" runat="server" CssClass="ps-button" OnClick="lbuVX2Finish_Click">ตกลง<img src="Image/Small/add.png" class="icon_right"/></asp:LinkButton>
            </asp:View>
            <asp:View ID="View3" runat="server">
                <div>
                    เพื่มข้อมูลสำเร็จ
                </div>
                <asp:LinkButton ID="lbuVX3New" runat="server" OnClick="lbuVX3New_Click" CssClass="ps-button">กลับ</asp:LinkButton>
            </asp:View>
        </asp:MultiView>
        <asp:HiddenField ID="hfHI" runat="server" />
        <asp:HiddenField ID="hfMI" runat="server" />
        <asp:HiddenField ID="hfHO" runat="server" />
        <asp:HiddenField ID="hfMO" runat="server" />
        <asp:HiddenField ID="hfSql" runat="server" />
    </div>
</asp:Content>
