<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="RequestManage.aspx.cs" Inherits="WEB_PERSONAL.RequestManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=lbuAddComment.ClientID %>").disabled = true;
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
            <img src="Image/Icon/edit.png" />อนุมัติคำร้องขอแก้ไขข้อมูล
        </div>
        <div id="notification" runat="server"></div>

        <div id="DataShow" runat="server">

            <div style="text-align: center;">
                <div class="ps-div-title-red">ข้อมูลบุคลากร</div>

                <table class="ps-table-x16" style="display: inline-block; vertical-align: top; text-align: left;">
                    <thead>
                        <tr>
                            <td class="col1"></td>
                            <th class="col2">ข้อมูลปัจจุบัน</th>
                            <th class="col2">ข้อมูลที่ขอเปลี่ยน</th>
                        </tr>
                    </thead>
                    <tr id="trTitleID" runat="server">
                        <td class="col1">คำนำหน้า</td>
                        <td class="col2">
                            <asp:Label ID="lbTitleID" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="ddlTitleID2" runat="server"></asp:Label>
                            <asp:DropDownList ID="ddlTitleID" runat="server" CssClass="form-control input-sm select2" Visible="false"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trFirstName" runat="server">
                        <td class="col1">ชื่อ</td>
                        <td class="col2">
                            <asp:Label ID="lbFirstName" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="tbFirstName2" runat="server"></asp:Label>
                            <asp:TextBox ID="tbFirstName" runat="server" CssClass="form-control input-sm" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trLastName" runat="server">
                        <td class="col1">นามสกุล</td>
                        <td class="col2">
                            <asp:Label ID="lbLastName" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="tbLastName2" runat="server"></asp:Label>
                            <asp:TextBox ID="tbLastName" runat="server" CssClass="form-control input-sm" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trGenderID" runat="server">
                        <td class="col1">เพศ</td>
                        <td class="col2">
                            <asp:Label ID="lbGenderID" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="ddlGenderID2" runat="server"></asp:Label>
                            <asp:DropDownList ID="ddlGenderID" runat="server" CssClass="form-control input-sm select2" Visible="false"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trBirthdayDate" runat="server">
                        <td class="col1">วันเกิด</td>
                        <td class="col2">
                            <asp:Label ID="lbBirthdayDate" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="tbBirthdayDate2" runat="server"></asp:Label>
                            <asp:TextBox ID="tbBirthdayDate" runat="server" CssClass="form-control input-sm" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trEmail" runat="server">
                        <td class="col1">อีเมล</td>
                        <td class="col2">
                            <asp:Label ID="lbEmail" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="tbEmail2" runat="server"></asp:Label>
                            <asp:TextBox ID="tbEmail" runat="server" CssClass="form-control input-sm" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trNationID" runat="server">
                        <td class="col1">สัญชาติ</td>
                        <td class="col2">
                            <asp:Label ID="lbNationID" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="ddlNationID2" runat="server"></asp:Label>
                            <asp:DropDownList ID="ddlNationID" runat="server" CssClass="form-control input-sm" Visible="false"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trCampusID" runat="server">
                        <td class="col1">วิทยาเขต</td>
                        <td class="col2">
                            <asp:Label ID="lbCampusID" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="ddlCampusID2" runat="server"></asp:Label>
                            <asp:DropDownList ID="ddlCampusID" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCampusID_SelectedIndexChanged" CssClass="form-control input-sm" Visible="false"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trFacultyID" runat="server">
                        <td class="col1">สำนัก/สถาบัน/คณะ</td>
                        <td class="col2">
                            <asp:Label ID="lbFacultyID" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="ddlFacultyID2" runat="server"></asp:Label>
                            <asp:DropDownList ID="ddlFacultyID" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFacultyID_SelectedIndexChanged" CssClass="form-control input-sm" Visible="false"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trDivisionID" runat="server">
                        <td class="col1">กอง/สำนักงานเลขา/ภาควิชา</td>
                        <td class="col2">
                            <asp:Label ID="lbDivisionID" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="ddlDivisionID2" runat="server"></asp:Label>
                            <asp:DropDownList ID="ddlDivisionID" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDivisionID_SelectedIndexChanged" CssClass="form-control input-sm" Visible="false"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trWorkDivisionID" runat="server">
                        <td class="col1">งาน/ฝ่าย</td>
                        <td class="col2">
                            <asp:Label ID="lbWorkDivisionID" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="ddlWorkDivisionID2" runat="server"></asp:Label>
                            <asp:DropDownList ID="ddlWorkDivisionID" runat="server" CssClass="form-control input-sm" Visible="false"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trStafftypeID" runat="server">
                        <td class="col1">ประเภทบุคลากร</td>
                        <td class="col2">
                            <asp:Label ID="lbStafftypeID" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="ddlStafftypeID2" runat="server"></asp:Label>
                            <asp:DropDownList ID="ddlStafftypeID" runat="server" CssClass="form-control input-sm" Visible="false"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trTimeContactID" runat="server">
                        <td class="col1">ระยะเวลาจ้าง</td>
                        <td class="col2">
                            <asp:Label ID="lbTimeContactID" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="ddlTimeContactID2" runat="server"></asp:Label>
                            <asp:DropDownList ID="ddlTimeContactID" runat="server" CssClass="form-control input-sm" Visible="false"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trBudgetID" runat="server">
                        <td class="col1">ประเภทเงินจ้าง</td>
                        <td class="col2">
                            <asp:Label ID="lbBudgetID" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="ddlBudgetID2" runat="server"></asp:Label>
                            <asp:DropDownList ID="ddlBudgetID" runat="server" CssClass="form-control input-sm" Visible="false"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trSubStafftypeID" runat="server">
                        <td class="col1">ประเภทบุคลากรย่อย</td>
                        <td class="col2">
                            <asp:Label ID="lbSubStafftypeID" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="ddlSubStafftypeID2" runat="server"></asp:Label>
                            <asp:DropDownList ID="ddlSubStafftypeID" runat="server" CssClass="form-control input-sm" Visible="false"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trAdminPosID" runat="server">
                        <td class="col1">ตำแหน่งทางบริหาร</td>
                        <td class="col2">
                            <asp:Label ID="lbAdminPosID" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="ddlAdminPosID2" runat="server"></asp:Label>
                            <asp:DropDownList ID="ddlAdminPosID" runat="server" CssClass="form-control input-sm" Visible="false"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trWorkPosID" runat="server">
                        <td class="col1">ตำแหน่งในสายงาน</td>
                        <td class="col2">
                            <asp:Label ID="lbWorkPosID" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="ddlWorkPosID2" runat="server"></asp:Label>
                            <asp:DropDownList ID="ddlWorkPosID" runat="server" CssClass="form-control input-sm" Visible="false"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trDateInwork" runat="server">
                        <td class="col1">วันที่เข้าทำงานครั้งแรก</td>
                        <td class="col2">
                            <asp:Label ID="lbDateInwork" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="tbDateInwork2" runat="server"></asp:Label>
                            <asp:TextBox ID="tbDateInwork" runat="server" CssClass="form-control input-sm" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trDateStartThisU" runat="server">
                        <td class="col1">วันที่เข้าทำงาน ณ สถานที่ปัจจุบัน</td>
                        <td class="col2">
                            <asp:Label ID="lbDateStartThisU" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="tbDateStartThisU2" runat="server"></asp:Label>
                            <asp:TextBox ID="tbDateStartThisU" runat="server" CssClass="form-control input-sm" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trSpecialName" runat="server">
                        <td class="col1">สาขางานที่เชี่ยวชาญ</td>
                        <td class="col2">
                            <asp:Label ID="lbSpecialName" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="tbSpecialName2" runat="server"></asp:Label>
                            <asp:TextBox ID="tbSpecialName" runat="server" CssClass="form-control input-sm" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trTeachIscedID" runat="server">
                        <td class="col1">กลุ่มสาขาวิชาที่สอน(ISCED)</td>
                        <td class="col2">
                            <asp:Label ID="lbTeachIscedID" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="ddlTeachIscedID2" runat="server"></asp:Label>
                            <asp:DropDownList ID="ddlTeachIscedID" runat="server" CssClass="form-control input-sm" Visible="false"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trGradLevID" runat="server">
                        <td class="col1">ระดับการศึกษาที่จบสูงสุด</td>
                        <td class="col2">
                            <asp:Label ID="lbGradLevID" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="ddlGradLevID2" runat="server"></asp:Label>
                            <asp:DropDownList ID="ddlGradLevID" runat="server" CssClass="form-control input-sm" Visible="false"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trGradCurr" runat="server">
                        <td class="col1">หลักสูตรที่จบการศึกษาสูงสุด</td>
                        <td class="col2">
                            <asp:Label ID="lbGradCurr" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="tbGradCurr2" runat="server"></asp:Label>
                            <asp:TextBox ID="tbGradCurr" runat="server" CssClass="form-control input-sm" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trGradIscedID" runat="server">
                        <td class="col1">กลุ่มสาขาวิชาที่จบสูงสุด(ISCED)</td>
                        <td class="col2">
                            <asp:Label ID="lbGradIscedID" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="ddlGradIscedID2" runat="server"></asp:Label>
                            <asp:DropDownList ID="ddlGradIscedID" runat="server" CssClass="form-control input-sm" Visible="false"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trGradProgID" runat="server">
                        <td class="col1">สาขาวิชาที่จบสูงสุด</td>
                        <td class="col2">
                            <asp:Label ID="lbGradProgID" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="ddlGradProgID2" runat="server"></asp:Label>
                            <asp:DropDownList ID="ddlGradProgID" runat="server" CssClass="form-control input-sm" Visible="false"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trGradUniv" runat="server">
                        <td class="col1">ชื่อสถาบันที่จบการศึกษาสูงสุด</td>
                        <td class="col2">
                            <asp:Label ID="lbGradUniv" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="tbGradUniv2" runat="server"></asp:Label>
                            <asp:TextBox ID="tbGradUniv" runat="server" CssClass="form-control input-sm" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trGradCountryID" runat="server">
                        <td class="col1">ประเทศที่จบการศึกษาสูงสุด</td>
                        <td class="col2">
                            <asp:Label ID="lbGradCountryID" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="ddlGradCountryID2" runat="server"></asp:Label>
                            <asp:DropDownList ID="ddlGradCountryID" runat="server" CssClass="form-control input-sm" Visible="false"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trDeformID" runat="server">
                        <td class="col1">ความพิการ</td>
                        <td class="col2">
                            <asp:Label ID="lbDeformID" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="ddlDeformID2" runat="server"></asp:Label>
                            <asp:DropDownList ID="ddlDeformID" runat="server" CssClass="form-control input-sm" Visible="false"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trReligionID" runat="server">
                        <td class="col1">ศาสนา</td>
                        <td class="col2">
                            <asp:Label ID="lbReligionID" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="ddlReligionID2" runat="server"></asp:Label>
                            <asp:DropDownList ID="ddlReligionID" runat="server" CssClass="form-control input-sm" Visible="false"></asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <div class="ps-separator"></div>

                <div class="ps-div-title-red">อนุมัติคำร้องขอแก้ไขข้อมูล</div>
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
                    <asp:LinkButton ID="lbuBack" runat="server" CssClass="ps-button" OnClick="lbuBack_Click"><img src="Image/Small/back.png" class="icon_left"/>ย้อนกลับ</asp:LinkButton>
                    <asp:LinkButton ID="lbuAddComment" runat="server" CssClass="ps-button" OnClick="lbuAllow_Click">ยืนยันการอนุมัติ<img src="Image/Small/next.png" class="icon_right"/></asp:LinkButton>
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
