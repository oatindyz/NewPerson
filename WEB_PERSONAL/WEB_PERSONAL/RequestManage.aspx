﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="RequestManage.aspx.cs" Inherits="WEB_PERSONAL.RequestManage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnAddComment.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>

    <script>
        $(function () {
            $("#ContentPlaceHolder1_tbBirthdayDate, #ContentPlaceHolder1_tbMovementDate").datepicker($.datepicker.regional["th"]);
            $("#ContentPlaceHolder1_tbDateInwork, #ContentPlaceHolder1_tbDateStartThisU").datepicker($.datepicker.regional["th"]);
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="default_page_style">
        <div class="ps-header">
            <img src="Image/Icon/edit.png" />คำร้องขอแก้ไขข้อมูลที่ถูกล็อค
            <span style="text-align: right; float: right;"><a href="ListRequest.aspx">
                <img src="Image/Small/back.png" />ย้อนกลับ</a></span>
        </div>
        <div id="notification" runat="server"></div>

        <div id="DataShow" runat="server" class="panel panel-default">
            <div class="panel-body">
                <div class="ps-box-ct10" style="text-align: center;">
                    <table class="ps-table-x16" style="display: inline-block; vertical-align: top; text-align: left;">
                        <thead>
                            <tr>
                                <td class="col1"></td>
                                <th class="col2">ข้อมูลเดิม</th>
                                <th class="col2">ข้อมูลที่ขอเปลี่ยน</th>
                            </tr>
                        </thead>
                        <tr id="trTitleID" runat="server">
                            <td class="col1">คำนำหน้า</td>
                            <td class="col2">
                                <asp:Label ID="lbTitleID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlTitleID" runat="server" CssClass="form-control input-sm select2" Enabled="false"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trFirstName" runat="server">
                            <td class="col1">ชื่อ</td>
                            <td class="col2">
                                <asp:Label ID="lbFirstName" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbFirstName" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trLastName" runat="server">
                            <td class="col1">นามสกุล</td>
                            <td class="col2">
                                <asp:Label ID="lbLastName" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbLastName" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trGenderID" runat="server">
                            <td class="col1">เพศ</td>
                            <td class="col2">
                                <asp:Label ID="lbGenderID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlGenderID" runat="server" CssClass="form-control input-sm select2" Enabled="false"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trBirthdayDate" runat="server">
                            <td class="col1">วันเกิด</td>
                            <td class="col2">
                                <asp:Label ID="lbBirthdayDate" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbBirthdayDate" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trEmail" runat="server">
                            <td class="col1">อีเมล</td>
                            <td class="col2">
                                <asp:Label ID="lbEmail" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbEmail" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trNationID" runat="server">
                            <td class="col1">สัญชาติ</td>
                            <td class="col2">
                                <asp:Label ID="lbNationID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlNationID" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trCampusID" runat="server">
                            <td class="col1">วิทยาเขต</td>
                            <td class="col2">
                                <asp:Label ID="lbCampusID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCampusID" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCampusID_SelectedIndexChanged" CssClass="form-control input-sm" Enabled="false"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trFacultyID" runat="server">
                            <td class="col1">สำนัก/สถาบัน/คณะ</td>
                            <td class="col2">
                                <asp:Label ID="lbFacultyID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFacultyID" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFacultyID_SelectedIndexChanged" CssClass="form-control input-sm" Enabled="false"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trDivisionID" runat="server">
                            <td class="col1">กอง/สำนักงานเลขา/ภาควิชา</td>
                            <td class="col2">
                                <asp:Label ID="lbDivisionID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDivisionID" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDivisionID_SelectedIndexChanged" CssClass="form-control input-sm" Enabled="false"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trWorkDivisionID" runat="server">
                            <td class="col1">งาน/ฝ่าย</td>
                            <td class="col2">
                                <asp:Label ID="lbWorkDivisionID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlWorkDivisionID" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trStafftypeID" runat="server">
                            <td class="col1">ประเภทบุคลากร</td>
                            <td class="col2">
                                <asp:Label ID="lbStafftypeID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlStafftypeID" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trTimeContactID" runat="server">
                            <td class="col1">ระยะเวลาจ้าง</td>
                            <td class="col2">
                                <asp:Label ID="lbTimeContactID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlTimeContactID" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trBudgetID" runat="server">
                            <td class="col1">ประเภทเงินจ้าง</td>
                            <td class="col2">
                                <asp:Label ID="lbBudgetID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlBudgetID" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trSubStafftypeID" runat="server">
                            <td class="col1">ประเภทบุคลากรย่อย</td>
                            <td class="col2">
                                <asp:Label ID="lbSubStafftypeID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSubStafftypeID" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trAdminPosID" runat="server">
                            <td class="col1">ตำแหน่งทางบริหาร</td>
                            <td class="col2">
                                <asp:Label ID="lbAdminPosID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlAdminPosID" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trWorkPosID" runat="server">
                            <td class="col1">ตำแหน่งในสายงาน</td>
                            <td class="col2">
                                <asp:Label ID="lbWorkPosID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlWorkPosID" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trDateInwork" runat="server">
                            <td class="col1">วันที่เข้าทำงานครั้งแรก</td>
                            <td class="col2">
                                <asp:Label ID="lbDateInwork" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbDateInwork" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trDateStartThisU" runat="server">
                            <td class="col1">วันที่เข้าทำงาน ณ สถานที่ปัจจุบัน</td>
                            <td class="col2">
                                <asp:Label ID="lbDateStartThisU" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbDateStartThisU" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trSpecialName" runat="server">
                            <td class="col1">สาขางานที่เชี่ยวชาญ</td>
                            <td class="col2">
                                <asp:Label ID="lbSpecialName" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbSpecialName" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trTeachIscedID" runat="server">
                            <td class="col1">กลุ่มสาขาวิชาที่สอน(ISCED)</td>
                            <td class="col2">
                                <asp:Label ID="lbTeachIscedID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlTeachIscedID" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trGradLevID" runat="server">
                            <td class="col1">ระดับการศึกษาที่จบสูงสุด</td>
                            <td class="col2">
                                <asp:Label ID="lbGradLevID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlGradLevID" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trGradCurr" runat="server">
                            <td class="col1">หลักสูตรที่จบการศึกษาสูงสุด</td>
                            <td class="col2">
                                <asp:Label ID="lbGradCurr" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbGradCurr" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trGradIscedID" runat="server">
                            <td class="col1">กลุ่มสาขาวิชาที่จบสูงสุด(ISCED)</td>
                            <td class="col2">
                                <asp:Label ID="lbGradIscedID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlGradIscedID" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trGradProgID" runat="server">
                            <td class="col1">สาขาวิชาที่จบสูงสุด</td>
                            <td class="col2">
                                <asp:Label ID="lbGradProgID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlGradProgID" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trGradUniv" runat="server">
                            <td class="col1">ชื่อสถาบันที่จบการศึกษาสูงสุด</td>
                            <td class="col2">
                                <asp:Label ID="lbGradUniv" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbGradUniv" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trGradCountryID" runat="server">
                            <td class="col1">ประเทศที่จบการศึกษาสูงสุด</td>
                            <td class="col2">
                                <asp:Label ID="lbGradCountryID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlGradCountryID" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trDeformID" runat="server">
                            <td class="col1">ความพิการ</td>
                            <td class="col2">
                                <asp:Label ID="lbDeformID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDeformID" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trSitNo" runat="server">
                            <td class="col1">เลขที่ตำแหน่ง</td>
                            <td class="col2">
                                <asp:Label ID="lbSitNo" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbSitNo" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trReligionID" runat="server">
                            <td class="col1">ศาสนา</td>
                            <td class="col2">
                                <asp:Label ID="lbReligionID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlReligionID" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trMovementTypeID" runat="server">
                            <td class="col1">ประเภทการดำรงตำแหน่งปัจจุบัน</td>
                            <td class="col2">
                                <asp:Label ID="lbMovementTypeID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlMovementTypeID" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trMovementDate" runat="server">
                            <td class="col1">วันที่มีผลบังคับใช้"การเข้าสู่ตำแหน่งปัจจุบัน"</td>
                            <td class="col2">
                                <asp:Label ID="lbMovementDate" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbMovementDate" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <div class="ps-separator"></div>

                <div class="ps-div-title-red">อนุมัติการยื่นคำร้องขอแก้ไขข้อมูล</div>
                <table class="ps-table-1" style="margin: 0 auto; margin-bottom: 10px;">
                    <tr>
                        <td class="col1">
                            <img src="Image/Small/calendar.png" class="icon_left" />วันที่ข้อมูล</td>
                        <td class="col2">
                            <asp:Label ID="lbDateReq" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">
                            <img src="Image/Small/calendar.png" class="icon_left" />ความเห็น</td>
                        <td class="col2">
                            <asp:TextBox ID="tbComment" runat="server" CssClass="ps-textbox" required="required" TabIndex="1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">
                            <img src="Image/Small/correct.png" class="icon_left" />การอนุมัติ</td>
                        <td class="col2">
                            <asp:RadioButton ID="rbAllow" runat="server" GroupName="allow" Text="อนุมัติ" Checked="true" />
                            <asp:RadioButton ID="rbNotAllow" runat="server" GroupName="allow" Text="ไม่อนุมัติ" />
                        </td>
                    </tr>
                </table>

                <div style="text-align: center; margin-bottom: 10px;">
                    <asp:Button ID="btnAddComment" runat="server" CssClass="btn btn-success" OnClick="btnAddComment_Click" Text="ยืนยันการอนุมัติ"></asp:Button>
                </div>
                </div>
            </div>
        </div>
        <div id="Accept" runat="server" visible="false" class="panel panel-default">
            <div class="panel-body">
                <div class="ps-box-ct10" style="text-align: center;">
                    <div class="ps-div-title-red">ทำการบันทึกข้อมูลสำเร็จ</div>
                    <div style="color: #808080; margin-top: 10px; text-align: center;">
                        ระบบได้ทำการอนุมัติคำร้องขอแก้ไขข้อมูลแล้ว
                    </div>
                    <div style="text-align: center; margin-top: 10px;">
                        <a href="Default.aspx" class="ps-button btn btn-primary">
                            <img src="Image/Small/home3.png" class="icon_left" />กลับหน้าหลัก</a>
                    </div>
                </div>
            </div>
        </div>
        <div id="NoAccept" runat="server" visible="false" class="panel panel-default">
            <div class="panel-body">
                <div class="ps-box-ct10" style="text-align: center;">
                    <div class="ps-div-title-red">ทำการบันทึกข้อมูลสำเร็จ</div>
                    <div style="color: #808080; margin-top: 10px; text-align: center;">
                        ระบบได้ยกเลิกคำร้องข้อแก้ไขข้อมูลแล้ว
                    </div>
                    <div style="text-align: center; margin-top: 10px;">
                        <a href="Default.aspx" class="ps-button btn btn-primary">
                            <img src="Image/Small/home3.png" class="icon_left" />กลับหน้าหลัก</a>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>