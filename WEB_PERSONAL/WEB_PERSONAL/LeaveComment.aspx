<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="LeaveComment.aspx.cs" Inherits="WEB_PERSONAL.LeaveComment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="default_page_style">
        <div class="ps-header">
            <img src="Image/Small/pencil_y.png" />ลงความเห็นการลา</div>



        <div id="error_area" runat="server"></div>

        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="View1" runat="server">
                <div>
                    <div class="ps-div-title-red">กรุณาเลือกรายการที่ต้องการลงความเห็น</div>
                    <asp:GridView ID="GridView1" runat="server" CssClass="ps-table-1" style="margin: 0 auto; margin-bottom: 20px;"></asp:GridView>
                </div>
            </asp:View>
            <asp:View ID="View2" runat="server">
                <div>
                    <div class="ps-div-title-red">ข้อมูลการลา</div>
                            <table class="ps-table-1" style="margin: 0 auto; margin-bottom: 10px;">
                                <tr>
                                    <td class="col1">
                                        <img src="Image/Small/ID.png" class="icon_left" />
                                        รหัสการลา</td>
                                    <td class="col2">
                                        <asp:Label ID="lbLeaveID" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1">
                                        <img src="Image/Small/list.png" class="icon_left" />
                                        ประเภทการลา</td>
                                    <td class="col2">
                                        <asp:Label ID="lbLeaveTypeName" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1">
                                        <img src="Image/Small/calendar.png" class="icon_left"/>
                                        วันที่ข้อมูล</td>
                                    <td class="col2">
                                        <asp:Label ID="lbReqDate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1">
                                        <img src="Image/Small/person2.png" class="icon_left" />ผู้ลา</td>
                                    <td class="col2">
                                        <asp:Label ID="lbPSName" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1">ตำแหน่ง</td>
                                    <td class="col2">
                                        <asp:Label ID="lbPSPos" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1">ระดับ</td>
                                    <td class="col2">
                                        <asp:Label ID="lbPSAPos" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1">สังกัด</td>
                                    <td class="col2">
                                        <asp:Label ID="lbPSDept" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="trPSBirthDate" runat="server">
                                    <td class="col1">
                                        <img src="Image/Small/calendar.png" class="icon_left" />เกิดวันที่ </td>
                                    <td class="col2">
                                        <asp:Label ID="lbPSBirthDate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="trPSWorkInDate" runat="server">
                                    <td class="col1">
                                        <img src="Image/Small/calendar.png" class="icon_left" />เข้ารับราชการวันที่ </td>
                                    <td class="col2">
                                        <asp:Label ID="lbPSWorkInDate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="trRestSave" runat="server">
                                    <td class="col1">
                                        <img src="Image/Small/calendar.png" class="icon_left" />วันลาพักผ่อนสะสม</td>
                                    <td class="col2">
                                        <asp:Label ID="lbRestSave" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="trRestLeft" runat="server">
                                    <td class="col1">
                                        <img src="Image/Small/calendar.png" class="icon_left" />มีสิทธิลาประจำปีนี้อีก</td>
                                    <td class="col2">
                                        <asp:Label ID="lbRestLeft" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="trRestTotal" runat="server">
                                    <td class="col1">
                                        <img src="Image/Small/calendar.png" class="icon_left"/>
                                        รวม</td>
                                    <td class="col2">
                                        <asp:Label ID="lbRestTotal" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="trWifeName" runat="server">
                                    <td class="col1">
                                        <img src="Image/Small/person2.png" class="icon_left"/>
                                        ชื่อภริยา</td>
                                    <td class="col2">
                                        <asp:Label ID="lbWifeName" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="trGBDate" runat="server">
                                    <td class="col1">
                                        <img src="Image/Small/calendar.png" class="icon_left"/>
                                        คลอดบุตรวันที่</td>
                                    <td class="col2">
                                        <asp:Label ID="lbGBDate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="trOrdained" runat="server">
                                    <td class="col1">
                                        <img src="Image/Small/question.png" class="icon_left"/>
                                        การอุปสมบท</td>
                                    <td class="col2">
                                        <asp:Label ID="lbOrdained" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="trTempleName" runat="server">
                                    <td class="col1">
                                        <img src="Image/Small/bell.png" class="icon_left"/>
                                        ชื่อวัด</td>
                                    <td class="col2">
                                        <asp:Label ID="lbTempleName" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="trTempleLocation" runat="server">
                                    <td class="col1">
                                        <img src="Image/Small/location.png" class="icon_left" />
                                        สถานที่</td>
                                    <td class="col2">
                                        <asp:Label ID="lbTempleLocation" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="trOrdainDate" runat="server">
                                    <td class="col1">
                                        <img src="Image/Small/calendar.png" class="icon_left" />
                                        อุปสมบทวันที่</td>
                                    <td class="col2">
                                        <asp:Label ID="lbOrdainDate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="trHujed" runat="server">
                                    <td class="col1">
                                        <img src="Image/Small/question.png" class="icon_left" />
                                        การไปประกอบพิธีฮัจย์</td>
                                    <td class="col2">
                                        <asp:Label ID="lbHujed" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="trLastFTTDate" runat="server">
                                    <td class="col1">
                                        <img src="Image/Small/calendar.png" class="icon_left" />
                                        วันที่ลาล่าสุด </td>
                                    <td class="col2">
                                        <asp:Label ID="lbLastFTTDate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1">
                                        <img src="Image/Small/calendar.png" class="icon_left" />
                                        วันที่ลา </td>
                                    <td class="col2">
                                        <asp:Label ID="lbFTTDate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="trStatistic" runat="server">
                                    <td class="col1">
                                        <img src="Image/Small/calendar.png" class="icon_left" />
                                        สถิติการลา</td>
                                    <td class="col2">
                                        <asp:Label ID="lbStatistic" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="trReason" runat="server">
                                    <td class="col1">
                                        <img src="Image/Small/comment.png" class="icon_left" />เหตุผล</td>
                                    <td class="col2">
                                        <asp:Label ID="lbReason" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="trContact" runat="server">
                                    <td class="col1">
                                        <img src="Image/Small/a.png" class="icon_left" />ติดต่อได้ที่</td>
                                    <td class="col2">
                                        <asp:Label ID="lbContact" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="trPhone" runat="server">
                                    <td class="col1">
                                        <img src="Image/Small/phone.png" class="icon_left" />เบอร์โทรศัพท์</td>
                                    <td class="col2">
                                        <asp:Label ID="lbPhone" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="trDrCer" runat="server">
                                    <td class="col1">
                                        <img src="Image/Small/clip.png" class="icon_left" />เอกสารแนบ</td>
                                    <td class="col2">
                                        <div id="divDrCer" runat="server"></div>
                                    </td>
                                </tr>
                                <tr id="trOldComment" runat="server">
                                    <td class="col1">
                                        <img src="Image/Small/comment.png" class="icon_left" />
                                        ความเห็นเก่า</td>
                                    <td class="col2">
                                        <asp:Label ID="lbOldComment" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="trOldDate" runat="server">
                                    <td class="col1">
                                        <img src="Image/Small/calendar.png" class="icon_left" />
                                        วันที่</td>
                                    <td class="col2">
                                        <asp:Label ID="lbOldDate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="trCancelReason" runat="server">
                                    <td class="col1">
                                        <img src="Image/Small/comment.png" class="icon_left" />
                                        เหตุผลที่ยกเลิก</td>
                                    <td class="col2">
                                        <asp:Label ID="lbCancelReason" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1">
                                        <img src="Image/Small/comment.png" class="icon_left" />ความเห็น</td>
                                    <td class="col2">
                                        <asp:TextBox ID="tbF1Comment" runat="server" Height="50px" Width="300px" TextMode="MultiLine" CssClass="ps-textbox"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                    <div style="text-align: center; margin-bottom: 10px;">
                        <asp:LinkButton ID="lbuF1Back" runat="server" CssClass="ps-button" OnClick="lbuF1Back_Click"><img src="Image/Small/back.png" class="icon_left"/>ย้อนกลับ</asp:LinkButton>
                        <asp:LinkButton ID="lbuF1Add" runat="server" CssClass="ps-button" OnClick="lbuF1Add_Click">ยืนยันการลงความเห็น<img src="Image/Small/next.png" class="icon_right"/></asp:LinkButton>
                    </div>
                </div>
                

                
            </asp:View>

            <asp:View ID="View3" runat="server">
                <div class="ps-div-title-red"><img src="Image/Small/pencil_y.png" class="icon_left"/>ลงความเห็นการลาสำเร็จ</div>
                <div style="text-align: center; margin-bottom: 10px;">
                    <asp:LinkButton ID="lbu1" runat="server" CssClass="ps-button" OnClick="lbu1_Click"><img src="Image/Small/back.png" class="icon_left"/>กลับหน้าหลัก</asp:LinkButton>
                    <asp:LinkButton ID="lbu2" runat="server" CssClass="ps-button" OnClick="lbu2_Click">ลงความเห็นต่อ<img src="Image/Small/next.png" class="icon_right"/></asp:LinkButton>
                </div>
                

            </asp:View>
        </asp:MultiView>

        <div class="ps-separator"></div>





    </div>
</asp:Content>
