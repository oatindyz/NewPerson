<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Request.aspx.cs" Inherits="WEB_PERSONAL.Request" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function validateEmail(emailField) {
            var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

            if (reg.test(emailField.value) == false) {
                alert('อีเมลไม่ถูกต้อง');
                document.getElementById('<%= tbEmail.ClientID %>').value = "";
            }
            return true;
        }
    </script>

    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnSaveRequest.ClientID %>").disabled = true;
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
            <img src="Image/Icon/edit.png" />ยื่นคำร้องขอแก้ไขข้อมูล
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
                                <th class="col2">ข้อมูลที่ต้องการเปลี่ยน</th>
                            </tr>
                        </thead>
                        <tr>
                            <td class="col1">คำนำหน้า</td>
                            <td class="col2">
                                <asp:Label ID="lbTitleID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlTitleID" runat="server" CssClass="form-control input-sm select2"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ชื่อ</td>
                            <td class="col2">
                                <asp:Label ID="lbFirstName" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbFirstName" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">นามสกุล</td>
                            <td class="col2">
                                <asp:Label ID="lbLastName" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbLastName" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">เพศ</td>
                            <td class="col2">
                                <asp:Label ID="lbGenderID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlGenderID" runat="server" CssClass="form-control input-sm select2"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">วันเกิด</td>
                            <td class="col2">
                                <asp:Label ID="lbBirthdayDate" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbBirthdayDate" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">อีเมล</td>
                            <td class="col2">
                                <asp:Label ID="lbEmail" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbEmail" runat="server" CssClass="form-control input-sm" onblur="validateEmail(this);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">สัญชาติ</td>
                            <td class="col2">
                                <asp:Label ID="lbNationID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlNationID" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">วิทยาเขต</td>
                            <td class="col2">
                                <asp:Label ID="lbCampusID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCampusID" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCampusID_SelectedIndexChanged" CssClass="form-control input-sm"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">สำนัก/สถาบัน/คณะ</td>
                            <td class="col2">
                                <asp:Label ID="lbFacultyID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFacultyID" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFacultyID_SelectedIndexChanged" CssClass="form-control input-sm"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">กอง/สำนักงานเลขา/ภาควิชา</td>
                            <td class="col2">
                                <asp:Label ID="lbDivisionID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDivisionID" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDivisionID_SelectedIndexChanged" CssClass="form-control input-sm"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trWorkDivisionID" runat="server">
                            <td class="col1">งาน/ฝ่าย</td>
                            <td class="col2">
                                <asp:Label ID="lbWorkDivisionID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlWorkDivisionID" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ประเภทบุคลากร</td>
                            <td class="col2">
                                <asp:Label ID="lbStafftypeID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlStafftypeID" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ระยะเวลาจ้าง</td>
                            <td class="col2">
                                <asp:Label ID="lbTimeContactID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlTimeContactID" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ประเภทเงินจ้าง</td>
                            <td class="col2">
                                <asp:Label ID="lbBudgetID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlBudgetID" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ประเภทบุคลากรย่อย</td>
                            <td class="col2">
                                <asp:Label ID="lbSubStafftypeID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSubStafftypeID" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ตำแหน่งทางบริหาร</td>
                            <td class="col2">
                                <asp:Label ID="lbAdminPosID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlAdminPosID" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ตำแหน่งในสายงาน</td>
                            <td class="col2">
                                <asp:Label ID="lbWorkPosID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlWorkPosID" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">วันที่เข้าทำงานครั้งแรก</td>
                            <td class="col2">
                                <asp:Label ID="lbDateInwork" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbDateInwork" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">วันที่เข้าทำงาน ณ สถานที่ปัจจุบัน</td>
                            <td class="col2">
                                <asp:Label ID="lbDateStartThisU" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbDateStartThisU" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">สาขางานที่เชี่ยวชาญ</td>
                            <td class="col2">
                                <asp:Label ID="lbSpecialName" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbSpecialName" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">กลุ่มสาขาวิชาที่สอน(ISCED)</td>
                            <td class="col2">
                                <asp:Label ID="lbTeachIscedID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlTeachIscedID" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ระดับการศึกษาที่จบสูงสุด</td>
                            <td class="col2">
                                <asp:Label ID="lbGradLevID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlGradLevID" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">หลักสูตรที่จบการศึกษาสูงสุด</td>
                            <td class="col2">
                                <asp:Label ID="lbGradCurr" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbGradCurr" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">กลุ่มสาขาวิชาที่จบสูงสุด(ISCED)</td>
                            <td class="col2">
                                <asp:Label ID="lbGradIscedID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlGradIscedID" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">สาขาวิชาที่จบสูงสุด</td>
                            <td class="col2">
                                <asp:Label ID="lbGradProgID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlGradProgID" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ชื่อสถาบันที่จบการศึกษาสูงสุด</td>
                            <td class="col2">
                                <asp:Label ID="lbGradUniv" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbGradUniv" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ประเทศที่จบการศึกษาสูงสุด</td>
                            <td class="col2">
                                <asp:Label ID="lbGradCountryID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlGradCountryID" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ความพิการ</td>
                            <td class="col2">
                                <asp:Label ID="lbDeformID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDeformID" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ศาสนา</td>
                            <td class="col2">
                                <asp:Label ID="lbReligionID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlReligionID" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                            </td>
                        </tr>
                    </table>

                    <div style="text-align: center; margin-top: 20px">
                        <asp:Button ID="btnSaveRequest" runat="server" CssClass="btn btn-success" Text="บันทึก" OnClick="btnSaveRequest_Click"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
        <div id="SaveShow" runat="server" visible="false" class="panel panel-default">
            <div class="panel-body">
                <div class="ps-box-ct10" style="text-align: center;">
                    <div class="ps-div-title-red">ทำการบันทึกข้อมูลยื่นคำร้องแก้ไขข้อมูล</div>
                    <div style="color: #808080; margin-top: 10px; text-align: center;">
                        ระบบได้ทำการบันทึกข้อมูลแล้ว สถานะ:อยู่ระหว่างดำเนินการ
                    </div>
                    <div style="text-align: center; margin-top: 10px;">
                        <a href="Default.aspx" class="ps-button btn btn-primary"><img src="Image/Small/home3.png" class="icon_left" />กลับหน้าหลัก</a>
                    </div>
                </div>
            </div>
        </div>

        <div id="InProcess" runat="server" visible="false" class="panel panel-default">
            <div class="panel-body">
                <div class="ps-box-ct10" style="text-align: center;">
                    <div class="ps-div-title-red">คุณมีรายการที่กำลังดำเนินการ</div>
                    <div style="color: #808080; margin-top: 10px; text-align: center;">
                        สามารถตรวจสอบประวัติได้ที่เมนู > ประวัติคำร้องแก้ไขข้อมูล 
                    </div>
                    <div style="text-align: center; margin-top: 10px;">
                        <a href="Default.aspx" class="ps-button btn btn-primary"><img src="Image/Small/home3.png" class="icon_left" />กลับหน้าหลัก</a>
                        <a href="RequestHistory.aspx" class="ps-button btn btn-primary"><img src="Image/Small/clock-history.png" class="icon_left" />ประวัติคำร้องแก้ไขข้อมูล</a>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
