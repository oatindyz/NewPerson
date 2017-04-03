<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Leave.aspx.cs" Inherits="WEB_PERSONAL.Leave" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(function () {
            $("#ContentPlaceHolder1_tbS1FromDate, #ContentPlaceHolder1_tbS1ToDate").datepicker($.datepicker.regional["th"]);
            $("#ContentPlaceHolder1_tbS1GBDate, #ContentPlaceHolder1_tbS1OrdainDate").datepicker($.datepicker.regional["th"]);
        });
    </script>
    <link href="CSS/Leave.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="default_page_style">
        <div class="ps-header">
            <img src="Image/Small/document-create.png" />ทำการลา
        </div>

        
        <asp:HiddenField ID="hfLeaveTypeID" runat="server" />
        <asp:HiddenField ID="hfLeaveTypeName" runat="server" />

        <div id="divReq" runat="server">
            <div class="ps-div-title-red">เงื่อนไขการลา</div>
            <table class="ps-table-1" style="margin: 0 auto; margin-bottom: 20px;">
                <tr>
                    <th>ประเภทการลา</th>
                    <th>เงื่อนไขการลา</th>
                </tr>
                <tr id="trReqSick" runat="server">
                    <td><div class="ps-lb-blue-b">ลาป่วย</div></td>
                    <td>
                        <div id="divSickFrom" runat="server">สามารถลาได้ตั้งแต่ <asp:Label ID="lbSickFrom" runat="server" Text="Label" CssClass="ps-lb-red-b"></asp:Label> เป็นต้นไป</div>
                        <div>ลาตั้งแต่ <span class="ps-lb-red-b">30</span> วันขึ้นไป จำเป็นต้องแนบ <span style="color: rgb(206,91,91); font-weight: bold;">ใบรับรองแพทย์</span></div>
                        <div>สามารถลาได้อีก <asp:Label ID="lbSickLeftDay" runat="server" Text="?" CssClass="ps-lb-red-b"></asp:Label> วัน</div>
                    </td>
                </tr>
                <tr id="trReqBusiness" runat="server">
                    <td><div class="ps-lb-blue-b">ลากิจ</div></td>
                    <td>
                        <div>จะต้องลาล่วงหน้า <asp:Label ID="lbReqBusinessDay" runat="server" Text="?" CssClass="ps-lb-red-b"></asp:Label> วันหรือตั้งแต่วันที่ <asp:Label ID="lbReqBusinessDate" runat="server" Text="?" CssClass="ps-lb-red-b"></asp:Label> เป็นต้นไป</div>
                        <div>สามารถลาได้อีก <asp:Label ID="lbBusinessLeftDay" runat="server" Text="?" CssClass="ps-lb-red-b"></asp:Label> วัน</div>
                    </td>
                </tr>
                <tr id="trReqRest" runat="server">
                    <td><div class="ps-lb-blue-b">ลาพักผ่อน</div></td>
                    <td>
                        <div>จะต้องลาล่วงหน้า <asp:Label ID="lbReqRestDay" runat="server" Text="?" CssClass="ps-lb-red-b"></asp:Label> วันหรือตั้งแต่วันที่ <asp:Label ID="lbReqRestDate" runat="server" Text="?" CssClass="ps-lb-red-b"></asp:Label> เป็นต้นไป</div>
                        <div>สามารถลาได้อีก <asp:Label ID="lbRestLeftDay" runat="server" Text="?" CssClass="ps-lb-red-b"></asp:Label> วัน</div>
                    </td>
                </tr>
                <tr id="trReqGiveBirth" runat="server">
                    <td><div class="ps-lb-blue-b">ลาคลอดบุตร</div></td>
                    <td>
                         <div>สามารถลาย้อนหลังได้ <asp:Label ID="lbReqGiveBirthDay" runat="server" Text="?" CssClass="ps-lb-red-b"></asp:Label> วันหรือตั้งแต่วันที่ <asp:Label ID="lbReqGiveBirthDate" runat="server" Text="?" CssClass="ps-lb-red-b"></asp:Label> เป็นต้นไป</div>
                    </td>
                </tr>
                <tr id="trReqHelpGiveBirth" runat="server">
                    <td><div class="ps-lb-blue-b">ลาไปช่วยเหลือภริยาที่คลอดบุตร</div></td>
                    <td>
                        <div>สามารถลาย้อนหลังได้ <asp:Label ID="lbReqHelpGiveBirthDay" runat="server" Text="?" CssClass="ps-lb-red-b"></asp:Label> วันหรือตั้งแต่วันที่ <asp:Label ID="lbReqHelpGiveBirthDate" runat="server" Text="?" CssClass="ps-lb-red-b"></asp:Label> เป็นต้นไป</div>
                    </td>
                </tr>
                <tr id="trReqOrdain" runat="server">
                    <td><div class="ps-lb-blue-b">ลาไปอุปสมบท</div></td>
                    <td>
                        <div>จะต้องลาล่วงหน้า <asp:Label ID="lbReqOrdainDay" runat="server" Text="?" CssClass="ps-lb-red-b"></asp:Label> วัน หรือตั้งแต่วันที่ <asp:Label ID="lbReqOrdainDate" runat="server" Text="?" CssClass="ps-lb-red-b"></asp:Label> เป็นต้นไป</div>
                        <div>สามารถลาได้อีก <asp:Label ID="lbOrdainLeftDay" runat="server" Text="?" CssClass="ps-lb-red-b"></asp:Label> วัน</div>
                    </td>
                </tr>
                <tr id="trReqHuj" runat="server">
                    <td><div class="ps-lb-blue-b">ลาไปประกอบพิธีฮัจญ์</div></td>
                    <td>
                        <div>จะต้องลาล่วงหน้า <asp:Label ID="lbReqHujDay" runat="server" Text="?" CssClass="ps-lb-red-b"></asp:Label> วัน หรือตั้งแต่วันที่ <asp:Label ID="lbReqHujDate" runat="server" Text="?" CssClass="ps-lb-red-b"></asp:Label> เป็นต้นไป</div>
                        <div>สามารถลาได้อีก <asp:Label ID="lbHujLeftDay" runat="server" Text="?" CssClass="ps-lb-red-b"></asp:Label> วัน</div>
                    </td>
                </tr>
            </table>

        </div>

        
        

        <div id="notification" runat="server"></div>

        <asp:MultiView ID="MV1" runat="server" ActiveViewIndex="0">
            <asp:View ID="MV1_V1" runat="server">
                <div>
                    <div class="ps-div-title-red">กรุณาเลือกการลา</div>
                    <div style="text-align: center;">
                        <asp:LinkButton ID="lbuSelectSick" runat="server" CssClass="ps-button" OnClick="lbuSelectSick_Click"><img src="Image/Small/document-create.png" class="icon_left" />ลาป่วย</asp:LinkButton>
                        <asp:LinkButton ID="lbuSelectBusiness" runat="server" CssClass="ps-button" OnClick="lbuSelectBusiness_Click"><img src="Image/Small/document-create.png" class="icon_left" />ลากิจ</asp:LinkButton>
                        <asp:LinkButton ID="lbuSelectRest" runat="server" CssClass="ps-button" OnClick="lbuSelectRest_Click"><img src="Image/Small/document-create.png" class="icon_left" />ลาพักผ่อน</asp:LinkButton>
                        <asp:LinkButton ID="lbuSelectGiveBirth" runat="server" CssClass="ps-button" OnClick="lbuSelectGiveBirth_Click"><img src="Image/Small/document-create.png" class="icon_left" />ลาคลอดบุตร</asp:LinkButton>
                        <asp:LinkButton ID="lbuSelectHelpGiveBirth" runat="server" CssClass="ps-button" OnClick="lbuSelectHelpGiveBirth_Click"><img src="Image/Small/document-create.png" class="icon_left" />ลาไปช่วยเหลือภริยาที่คลอดบุตร</asp:LinkButton>
                        <asp:LinkButton ID="lbuSelectOrdain" runat="server" CssClass="ps-button" OnClick="lbuSelectOrdain_Click"><img src="Image/Small/document-create.png" class="icon_left" />ลาไปอุปสมบท</asp:LinkButton>
                        <asp:LinkButton ID="lbuSelectHuj" runat="server" CssClass="ps-button" OnClick="lbuSelectHuj_Click"><img src="Image/Small/document-create.png" class="icon_left" />ลาไปประกอบพิธีฮัจญ์</asp:LinkButton>
                    </div>
                </div>
            </asp:View>
            <asp:View ID="MV1_V2" runat="server">
                
                <div>
                    <div class="ps-div-title-red">กรุณากรอกข้อมูลการลา</div>
                    <table class="ps-table-1" style="margin: 0 auto;">
                        <tr id="trS1WifeName" runat="server">
                            <td class="col1">
                                <img src="Image/Small/person2.png" class="icon_left" />ชื่อภริยา</td>
                            <td class="col2">
                                <asp:TextBox ID="tbS1WifeFirstName" runat="server" CssClass="ps-textbox" placeHolder="ชื่อจริง"></asp:TextBox>
                                <asp:TextBox ID="tbS1WifeLastName" runat="server" CssClass="ps-textbox" placeHolder="นามสกุล"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trS1GBDate" runat="server">
                            <td class="col1">
                                <img src="Image/Small/calendar.png" class="icon_left" />
                                คลอดบุตรวันที่</td>
                            <td class="col2">
                                <asp:TextBox ID="tbS1GBDate" runat="server" CssClass="ps-textbox" style="width: 80px;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trS1Ordained" runat="server">
                            <td class="col1">
                                <img src="Image/Small/question.png" class="icon_left" />
                                การอุปสมบท</td>
                            <td class="col2">
                                <asp:RadioButton ID="rbS1OrdainedT" runat="server" CssClass="radio_button radio_button_default" GroupName="ordained" Text="เคย" />
                                <asp:RadioButton ID="rbS1OrdainedF" runat="server" CssClass="radio_button radio_button_default" GroupName="ordained" Text="ไม่เคย" />
                            </td>
                        </tr>
                        <tr id="trS1TempleName" runat="server">
                            <td class="col1">
                                <img src="Image/Small/bell.png" class="icon_left" />
                                ชื่อวัด</td>
                            <td class="col2">
                                <asp:TextBox ID="tbS1TempleName" runat="server" CssClass="ps-textbox" style="width: 200px;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trS1TempleLocation" runat="server">
                            <td class="col1">
                                <img src="Image/Small/location.png" class="icon_left" />
                                สถานที่</td>
                            <td class="col2">
                                <asp:TextBox ID="tbS1TempleLocation" runat="server" CssClass="ps-textbox" style="width: 200px;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trS1OrdainDate" runat="server">
                            <td class="col1">
                                <img src="Image/Small/calendar.png" class="icon_left" />
                                อุปสมบทวันที่</td>
                            <td class="col2">
                                <asp:TextBox ID="tbS1OrdainDate" runat="server" CssClass="ps-textbox" style="width: 80px;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trS1Hujed" runat="server">
                            <td class="col1">
                                <img src="Image/Small/question.png" class="icon_left" />
                                การไปประกอบพิธฮัจย์</td>
                            <td class="col2">
                                <asp:RadioButton ID="rbS1HujedT" runat="server" CssClass="radio_button radio_button_default" GroupName="ordained" Text="เคย" />
                                <asp:RadioButton ID="rbS1HujedF" runat="server" CssClass="radio_button radio_button_default" GroupName="ordained" Text="ไม่เคย" />
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">
                                <img src="Image/Small/calendar.png" class="icon_left" />วันที่ลา</td>
                            <td class="col2">
                                <asp:TextBox ID="tbS1FromDate" runat="server" CssClass="ps-textbox" style="width: 80px;"></asp:TextBox>
                                <span style="width: 10px; display: inline-block;"></span>
                                <span style="color: #808080;">ถึง </span><asp:TextBox ID="tbS1ToDate" runat="server" CssClass="ps-textbox" style="width: 80px;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trS1Reason" runat="server">
                            <td class="col1">
                                <img src="Image/Small/a.png" class="icon_left" />เหตุผล</td>
                            <td class="col2">
                                <asp:TextBox ID="tbS1Reason" runat="server" CssClass="ps-textbox" style="width: 200px;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trS1Contact" runat="server">
                            <td class="col1">
                                <img src="Image/Small/comment.png" class="icon_left" />ติดต่อได้ที่</td>
                            <td class="col2">
                                <asp:TextBox ID="tbS1Contact" runat="server" CssClass="ps-textbox" style="width: 200px;" placeHolder="สถานที่"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trS1Phone" runat="server">
                            <td class="col1">
                                <img src="Image/Small/phone.png" class="icon_left" />เบอร์โทรศัพท์</td>
                            <td class="col2">
                                <asp:TextBox ID="tbS1Phone" runat="server" CssClass="ps-textbox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trS1DrCer" runat="server">
                            <td class="col1">
                                <img src="Image/Small/clip.png" class="icon_left" />ใบรับรองแพทย์</td>
                            <td class="col2">
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                            </td>
                        </tr>
                        
                    </table>
                    <div style="text-align: center; margin-top: 10px;">
                        <asp:LinkButton ID="lbuS1Back" runat="server" CssClass="ps-button" OnClick="lbuS1Back_Click"><img src="Image/Small/back.png" class="icon_left"/>ย้อนกลับ</asp:LinkButton>
                        <asp:LinkButton ID="lbuS1Check" runat="server" CssClass="ps-button" OnClick="lbuS1Check_Click">ต่อไป<img src="Image/Small/next.png" class="icon_right"/></asp:LinkButton>
                    </div>
   
                </div>
                

            </asp:View>
            <asp:View ID="MV1_V3" runat="server">
                <div>
                    <div class="ps-div-title-red">ข้อมูลการลา</div>
                    <table class="ps-table-1" style="margin: 0 auto;">
                        <tr>
                            <td class="col1">ประเภทการลา</td>
                            <td class="col2">
                                <asp:Label ID="lbS2LeaveTypeName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">
                                <img src="Image/Small/person2.png" class="icon_left" />ชื่อ</td>
                            <td class="col2">
                                <asp:Label ID="lbS2PSName" runat="server"></asp:Label>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ตำแหน่ง</td>
                            <td class="col2">
                                <asp:Label ID="lbS2PSPos" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ระดับ</td>
                            <td class="col2">
                                <asp:Label ID="lbS2PSAPos" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">สังกัด</td>
                            <td class="col2">
                                <asp:Label ID="lbS2PSDept" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trS2BirthDate" runat="server">
                            <td class="col1">
                                <img src="Image/Small/calendar.png" class="icon_left" />
                                เกิดวันที่</td>
                            <td class="col2">
                                <asp:Label ID="lbS2PSBirthDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trS2WorkInDate" runat="server">
                            <td class="col1">
                                <img src="Image/Small/calendar.png" class="icon_left" />
                                เข้ารับราชการเมื่อวันที่</td>
                            <td class="col2">
                                <asp:Label ID="lbS2PSWorkInDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trS2RestSave" runat="server">
                            <td class="col1">
                                <img src="Image/Small/calendar.png" class="icon_left" />วันลาพักผ่อนสะสม</td>
                            <td class="col2">
                                <asp:Label ID="lbS2RestSave" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trS2RestLeft" runat="server">
                            <td class="col1">
                                <img src="Image/Small/calendar.png" class="icon_left" />
                                มีสิทธิลาประจำปีนี้อีก</td>
                            <td class="col2">
                                <asp:Label ID="lbS2RestLeft" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trS2RestTotal" runat="server">
                            <td class="col1">
                                <img src="Image/Small/calendar.png" class="icon_left" />
                                รวม</td>
                            <td class="col2">
                                <asp:Label ID="lbS2RestTotal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trS2WifeName" runat="server">
                            <td class="col1">
                                <img src="Image/Small/person2.png" class="icon_left" />
                                ชื่อภริยา</td>
                            <td class="col2">
                                <asp:Label ID="lbS2WifeName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trS2GBDate" runat="server">
                            <td class="col1">
                                <img src="Image/Small/calendar.png" class="icon_left" />
                                คลอดบุตรวันที่</td>
                            <td class="col2">
                                <asp:Label ID="lbS2GBDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trS2Ordained" runat="server">
                            <td class="col1">
                                <img src="Image/Small/question.png" class="icon_left" />
                                การอุปสมบท</td>
                            <td class="col2">
                                <asp:Label ID="lbS2Ordained" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trS2TempleName" runat="server">
                            <td class="col1">
                                <img src="Image/Small/bell.png" class="icon_left" />
                                ชื่อวัด</td>
                            <td class="col2">
                                <asp:Label ID="lbS2TempleName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trS2TempleLocation" runat="server">
                            <td class="col1">
                                <img src="Image/Small/location.png" class="icon_left" />
                                สถานที่</td>
                            <td class="col2">
                                <asp:Label ID="lbS2TempleLocation" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trS2OrdainDate" runat="server">
                            <td class="col1">
                                <img src="Image/Small/calendar.png" class="icon_left" />
                                อุปสมบทวันที่</td>
                            <td class="col2">
                                <asp:Label ID="lbS2OrdainDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trS2Hujed" runat="server">
                            <td class="col1">
                                <img src="Image/Small/question.png" class="icon_left" />
                                การไปประกอบพิธีฮัจย์</td>
                            <td class="col2">
                                <asp:Label ID="lbS2Hujed" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trS2LastFTTDate" runat="server">
                            <td class="col1">
                                <img src="Image/Small/calendar.png" class="icon_left" />
                                ลาล่าสุด</td>
                            <td class="col2">
                                <asp:Label ID="lbS2LastFTTDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">
                                <img src="Image/Small/calendar.png" class="icon_left" />วันที่ลา</td>
                            <td class="col2">
                                <asp:Label ID="lbS2FTTDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trS2Statistic" runat="server">
                            <td class="col1">
                                <img src="Image/Small/calendar.png" class="icon_left" />สถิติการลา</td>
                            <td class="col2">
                                <asp:Label ID="lbS2Statistic" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trS2Reason" runat="server">
                            <td class="col1">
                                <img src="Image/Small/a.png" class="icon_left" />เหตุผล</td>
                            <td class="col2">
                                <asp:Label ID="lbS2Reason" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trS2Contact" runat="server">
                            <td class="col1">
                                <img src="Image/Small/a.png" class="icon_left" />ติดต่อได้ที่</td>
                            <td class="col2">
                                <asp:Label ID="lbS2Contact" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trS2Phone" runat="server">
                            <td class="col1">
                                <img src="Image/Small/phone.png" class="icon_left" />เบอร์โทรศัพท์</td>
                            <td class="col2">
                                <asp:Label ID="lbS2Phone" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trS2DrCer" runat="server">
                            <td class="col1">
                                <img src="Image/Small/clip.png" class="icon_left" />ใบรับรองแพทย์</td>
                            <td class="col2">
                                <asp:Label ID="lbS2DrCer" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>

                    <div class="ps-separator"></div>

                    <div class="ps-div-title-red" style="margin-top: 10px;">ผู้บังคับบัญชา</div>

                    <div style="text-align: center;">
                        <asp:Table ID="tbBoss" runat="server" Style="text-align: center; display: inline-block;"></asp:Table>
                    </div>
                    
                    <div style="text-align: center; margin-top: 10px;">
                        <asp:LinkButton ID="lbuS2Back" runat="server" CssClass="ps-button" OnClick="lbuS2Back_Click"><img src="Image/Small/back.png" class="icon_left"/>ย้อนกลับ</asp:LinkButton>
                    <asp:LinkButton ID="lbuS2Finish" runat="server" CssClass="ps-button" OnClick="lbuS2Finish_Click"><img src="Image/Small/document-create.png" class="icon_left"/>ยืนคำขอลา</asp:LinkButton>
                    </div>
                </div>
                
            </asp:View>
            <asp:View ID="VX_F" runat="server">
                <div class="ps-div-title-red">ส่งคำขอการลาสำเร็จ</div>
                <div style="text-align: center; color: #808080; margin-bottom: 10px;">การลาจะมีผลต่อเมื่อผู้บังคับบัญชาอนุมัติการลาเรียบร้อย<br />สามารถตรวจสอบสถานะการลาได้ที่เมนู การลา -> ประวัติการลา</div>
                <div style="text-align: center; margin-bottom: 10px;">
                    <asp:LinkButton ID="lbuBackMain" runat="server" CssClass="ps-button" OnClick="lbuBackMain_Click"><img class="icon_left" src="Image/Small/back.png" />กลับหน้าหลัก</asp:LinkButton>
                    <asp:LinkButton ID="lbuHistory" runat="server" CssClass="ps-button" OnClick="lbuHistory_Click"><img class="icon_left" src="Image/Small/back.png" />ไปหน้าสถานะและประวัติ</asp:LinkButton>
                </div>
            </asp:View>

        </asp:MultiView>
        <div class="ps-separator"></div>

    </div>

    <asp:HiddenField ID="hfSql" runat="server" />
    <asp:HiddenField ID="hfSql2" runat="server" />
    <asp:HiddenField ID="hfFileUploadName" runat="server" />

</asp:Content>
