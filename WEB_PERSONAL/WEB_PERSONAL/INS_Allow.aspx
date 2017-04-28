<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="INS_Allow.aspx.cs" Inherits="WEB_PERSONAL.INS_Allow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(function () {
            $("#ContentPlaceHolder1_tbDateAllow").datepicker($.datepicker.regional["th"]);
        });
    </script>
    <style>
        .col2 {
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="default_page_style">
        <div class="ps-header">
            <img src="Image/Small/medal.png" />อนุมัติการขอเครื่องราชฯ
        </div>
        <div id="error_area" runat="server"></div>

        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="View1" runat="server">
                <div>
                    <div class="ps-div-title-red">กรุณาเลือกรายการที่ต้องการอนุมัติการขอเครื่องราชฯ</div>
                    <asp:GridView ID="GridView1" runat="server" CssClass="ps-table-1" Style="margin: 0 auto; margin-bottom: 20px;"></asp:GridView>
                    <div style="text-align: center; margin-top: 10px;">
                        <asp:Label ID="lbNoData" runat="server" Text="ขณะนี้ไม่มีรายการที่ท่านต้องอนุมัติ" Style="color: #808080;"></asp:Label>
                    </div>
                </div>
            </asp:View>
            <asp:View ID="View2" runat="server">
                <div>
                    <div class="ps-div-title-red">ข้อมูลการขอเครื่องราชฯ</div>

                    <div style="text-align: center;">

                        <table class="ps-table-x16" style="display: inline-block; margin-right: 20px; vertical-align: top;">

                            <tr>
                                <td class="col1">คำนำหน้าชื่อ</td>
                                <td class="col2">
                                    <asp:Label ID="lbTitleName" runat="server" Text="TitleName"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="col1">ชื่อ</td>
                                <td class="col2">
                                    <asp:Label ID="lbName" runat="server" Text="Name"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="col1">นามสกุล</td>
                                <td class="col2">
                                    <asp:Label ID="lbLastName" runat="server" Text="LastName"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="col1">เพศ</td>
                                <td class="col2">
                                    <asp:Label ID="lbGender" runat="server" Text="Gender"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="col1">วัน/เดือน/ปีเกิด</td>
                                <td class="col2">
                                    <asp:Label ID="lbBirthDate" runat="server" Text="BirthDate"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="col1">วัน/เดือน/ปีที่เริ่มรับราชการ</td>
                                <td class="col2">
                                    <asp:Label ID="lbDateInwork" runat="server" Text="DateInWork"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="col1">ตำแหน่งและระดับที่เริ่มรับราชการ</td>
                                <td class="col2">
                                    <asp:Label ID="lbFirstPosition" runat="server" Text="FirstPosition"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="col1">ชื่อตำแหน่งปัจจุบัน</td>
                                <td class="col2">
                                    <asp:Label ID="lbPositionCurrent" runat="server" Text="PositionCurrent"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="col1">ประเภท</td>
                                <td class="col2">
                                    <asp:Label ID="lbType" runat="server" Text="Type"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="col1">ระดับ</td>
                                <td class="col2">
                                    <asp:Label ID="lbDegree" runat="server" Text="Degree"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="col1">เงินเดือนป้จจุบัน</td>
                                <td class="col2">
                                    <asp:Label ID="lbSalaryCurrent" runat="server" Text="SalaryCurrent"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="col1">เงินประจำตำแหน่ง</td>
                                <td class="col2">
                                    <asp:Label ID="lbPositionSalary" runat="server" Text="PostionSalary"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="col1">ชั้นเครื่องราชฯที่ขอ</td>
                                <td class="col2">
                                    <asp:Label ID="lbInsigReq" runat="server"></asp:Label><br />
                                    <asp:Image ID="imgInsigReq" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>

                <div class="ps-separator"></div>

                <div class="ps-div-title-red">อนุมัติการขอเครื่องราชฯ</div>
                <table class="ps-table-1" style="margin: 0 auto; margin-bottom: 10px;">
                    <tr>
                        <td class="col1">
                            <img src="Image/Small/calendar.png" class="icon_left" />วันที่ขอ</td>
                        <td class="col2">

                            <asp:Label ID="lbDateReq" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">
                            <img src="Image/Small/calendar.png" class="icon_left" />วันที่</td>
                        <td class="col2">
                            <asp:TextBox ID="tbDateAllow" runat="server" CssClass="ps-textbox" required="required" tabindex="1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">
                            <img src="Image/Small/correct.png" class="icon_left" />การอนุมัติ</td>
                        <td class="col2">
                            <asp:RadioButton ID="rbAllow" runat="server" GroupName="allow" Text="ได้รับ" Checked="true" />
                            <asp:RadioButton ID="rbNotAllow" runat="server" GroupName="allow" Text="ไม่ได้รับ" />
                        </td>
                    </tr>
                </table>

                <div style="text-align: center; margin-bottom: 10px;">
                    <asp:LinkButton ID="lbuBack" runat="server" CssClass="ps-button" OnClick="lbuBack_Click"><img src="Image/Small/back.png" class="icon_left"/>ย้อนกลับ</asp:LinkButton>
                    <asp:LinkButton ID="lbuAddComment" runat="server" CssClass="ps-button" OnClick="lbuAllow_Click">ยืนยันการอนุมัติ<img src="Image/Small/next.png" class="icon_right"/></asp:LinkButton>
                </div>




            </asp:View>
            <asp:View ID="View3" runat="server">
                <div class="ps-div-title-red">
                    <img src="Image/Small/correct.png" class="icon_left;" />อนุมัติการขอเครื่องราชฯสำเร็จ
                </div>
                <div style="text-align: center; margin-bottom: 10px;">
                    <asp:LinkButton ID="lbu1" runat="server" CssClass="ps-button" OnClick="lbu1_Click"><img src="Image/Small/back.png" class="icon_left"/>กลับหน้าหลัก</asp:LinkButton>
                    <asp:LinkButton ID="lbu2" runat="server" CssClass="ps-button" OnClick="lbu2_Click">อนุมัติการขอเครื่องราชฯต่อ<img src="Image/Small/next.png" class="icon_right"/></asp:LinkButton>
                </div>
            </asp:View>
        </asp:MultiView>

        <div class="ps-separator"></div>









    </div>
</asp:Content>
