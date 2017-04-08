<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Adduser.aspx.cs" Inherits="WEB_PERSONAL.Adduser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function keyup(obj, e) {
            var keynum;
            var keychar;
            var id = '';
            if (window.event) {// IE
                keynum = e.keyCode;
            }
            else if (e.which) {// Netscape/Firefox/Opera
                keynum = e.which;
            }
            keychar = String.fromCharCode(keynum);


            var tagInput = document.getElementById('<%= tbCitizenID.ClientID %>').value;

            if (obj.value.length == 13) {

                if (checkID(tagInput)) {
                    nextObj.focus();
                }
                else {
                    alert('รหัสประจำตัวประชาชนไม่ถูกต้อง');
                    document.getElementById('<%= tbCitizenID.ClientID %>').value = "";
                    nextObj.focus();
                }

            }
        }
        function checkID(id) {
            if (id.length != 13) return false;
            for (i = 0, sum = 0; i < 12; i++)
                sum += parseFloat(id.charAt(i)) * (13 - i);
            if ((11 - sum % 11) % 10 != parseFloat(id.charAt(12)))
                return false;
            return true;

        }
    </script>

    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnAddUser.ClientID %>").disabled = true;
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
            <img src="Image/Icon/add.png" />เพิ่มข้อมูลบุคลากร
            <span style="text-align: right; float: right;"><a href="listuser-admin.aspx">
                <img src="Image/Small/back.png" />ย้อนกลับ</a></span>
        </div>
        <div id="notification" runat="server"></div>

        <div id="DataShow" runat="server" class="panel panel-default">
            <div class="panel-body">
                <div class="ps-box-ct10" style="text-align: center;">
                    <table class="ps-table-x16" style="display: inline-block; vertical-align: top; text-align: left;">
                        <tr>
                            <td class="col1">รหัสบัตรประชาชน<span class="ps-lb-red" />*</td>
                            <td class="col2">
                                <asp:TextBox ID="tbCitizenID" runat="server" CssClass="form-control input-sm" MaxLength="13" onkeypress="return isNumberKey(event)" onkeyup="keyup(this,event)" required="required" TabIndex="1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">คำนำหน้า</td>
                            <td class="col2">
                                <asp:DropDownList ID="ddlTitleID" runat="server" CssClass="form-control input-sm select2"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ชื่อ<span class="ps-lb-red" />*</td>
                            <td class="col2">
                                <asp:TextBox ID="tbFirstName" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">นามสกุล<span class="ps-lb-red" />*</td>
                            <td class="col2">
                                <asp:TextBox ID="tbLastName" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">เพศ</td>
                            <td class="col2">
                                <asp:DropDownList ID="ddlGenderID" runat="server" CssClass="form-control input-sm select2"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">วันเกิด<span class="ps-lb-red" />*</td>
                            <td class="col2">
                                <asp:TextBox ID="tbBirthdayDate" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">อีเมล<span class="ps-lb-red" />*</td>
                            <td class="col2">
                                <asp:TextBox ID="tbEmail" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">บ้านเลขที่</td>
                            <td class="col2">
                                <asp:TextBox ID="tbHomeAdd" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">หมู่</td>
                            <td class="col2">
                                <asp:TextBox ID="tbMoo" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ถนน</td>
                            <td class="col2">
                                <asp:TextBox ID="tbStreet" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">จังหวัด</td>
                            <td class="col2">
                                <asp:DropDownList ID="ddlProvinceID" runat="server" CssClass="form-control input-sm select2" AutoPostBack="True" OnSelectedIndexChanged="ddlProvinceID_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">เขต/อำเภอ</td>
                            <td class="col2">
                                <asp:DropDownList ID="ddlAmphurID" runat="server" CssClass="form-control input-sm select2" AutoPostBack="True" OnSelectedIndexChanged="ddlAmphurID_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">แขวง/ตำบล</td>
                            <td class="col2">
                                <asp:DropDownList ID="ddlDistrictID" runat="server" CssClass="form-control input-sm select2" AutoPostBack="True" OnSelectedIndexChanged="ddlDistrictID_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">รหัสไปรษณีย์</td>
                            <td class="col2">
                                <asp:TextBox ID="tbZipcode" runat="server" CssClass="form-control input-sm" MaxLength="5" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">หมายเลขโทรศัพท์ที่ทำงาน</td>
                            <td class="col2">
                                <asp:TextBox ID="tbTelephone" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">สัญชาติ<span class="ps-lb-red" />*</td>
                            <td class="col2">
                                <asp:DropDownList ID="ddlNationID" runat="server" CssClass="form-control input-sm select2" required="required" TabIndex="1"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">วิทยาเขต<span class="ps-lb-red" />*</td>
                            <td class="col2">
                                <asp:DropDownList ID="ddlCampusID" runat="server" CssClass="form-control input-sm select2" AutoPostBack="true" OnSelectedIndexChanged="ddlCampusID_SelectedIndexChanged" required="required" TabIndex="1"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">สำนัก/สถาบัน/คณะ<span class="ps-lb-red" />*</td>
                            <td class="col2">
                                <asp:DropDownList ID="ddlFacultyID" runat="server" CssClass="form-control input-sm select2" AutoPostBack="true" OnSelectedIndexChanged="ddlFacultyID_SelectedIndexChanged" required="required" TabIndex="1"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">กอง/สำนักงานเลขา/ภาควิชา<span class="ps-lb-red" />*</td>
                            <td class="col2">
                                <asp:DropDownList ID="ddlDivisionID" runat="server" CssClass="form-control input-sm select2" AutoPostBack="true" OnSelectedIndexChanged="ddlDivisionID_SelectedIndexChanged" required="required" TabIndex="1"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trWorkDivision" runat="server">
                            <td class="col1">งาน/ฝ่าย<span class="ps-lb-red" />*</td>
                            <td class="col2">
                                <asp:DropDownList ID="ddlWorkDivisionID" runat="server" CssClass="form-control input-sm select2" required="required" TabIndex="1"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ประเภทบุคลากร<span class="ps-lb-red" />*</td>
                            <td class="col2">
                                <asp:DropDownList ID="ddlStafftypeID" runat="server" CssClass="form-control input-sm select2" required="required" TabIndex="1"></asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <table class="ps-table-x16" style="display: inline-block; vertical-align: top; text-align: left;">
                        <tr>
                            <td class="col1">ระยะเวลาจ้าง<span class="ps-lb-red" />*</td>
                            <td class="col2">
                                <asp:DropDownList ID="ddlTimeContactID" runat="server" CssClass="form-control input-sm select2" required="required" TabIndex="1"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ประเภทเงินจ้าง<span class="ps-lb-red" />*</td>
                            <td class="col2">
                                <asp:DropDownList ID="ddlBudgetID" runat="server" CssClass="form-control input-sm select2" required="required" TabIndex="1"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ประเภทบุคลากรย่อย<span class="ps-lb-red" />*</td>
                            <td class="col2">
                                <asp:DropDownList ID="ddlSubStafftypeID" runat="server" CssClass="form-control input-sm select2" required="required" TabIndex="1"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ตำแหน่งทางบริหาร</td>
                            <td class="col2">
                                <asp:DropDownList ID="ddlAdminPosID" runat="server" CssClass="form-control input-sm select2"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ระดับตำแหน่ง<span class="ps-lb-red" />*</td>
                            <td class="col2">
                                <asp:DropDownList ID="ddlPositionID" runat="server" CssClass="form-control input-sm select2" required="required" TabIndex="1"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ตำแหน่งในสายงาน</td>
                            <td class="col2">
                                <asp:DropDownList ID="ddlWorkPosID" runat="server" CssClass="form-control input-sm select2"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">วันที่เข้าทำงานครั้งแรก<span class="ps-lb-red" />*</td>
                            <td class="col2">
                                <asp:TextBox ID="tbDateInwork" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">วันที่เข้าทำงาน ณ สถานที่ปัจจุบัน<span class="ps-lb-red" />*</td>
                            <td class="col2">
                                <asp:TextBox ID="tbDateStartThisU" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">สาขางานที่เชี่ยวชาญ</td>
                            <td class="col2">
                                <asp:TextBox ID="tbSpecialName" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">กลุ่มสาขาวิชาที่สอน(ISCED)</td>
                            <td class="col2">
                                <asp:DropDownList ID="ddlTeachIscedID" runat="server" CssClass="form-control input-sm select2"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ระดับการศึกษาที่จบสูงสุด<span class="ps-lb-red" />*</td>
                            <td class="col2">
                                <asp:DropDownList ID="ddlGradLevID" runat="server" CssClass="form-control input-sm select2" required="required" TabIndex="1"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">หลักสูตรที่จบการศึกษาสูงสุด</td>
                            <td class="col2">
                                <asp:TextBox ID="tbGradCurr" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">กลุ่มสาขาวิชาที่จบสูงสุด(ISCED)</td>
                            <td class="col2">
                                <asp:DropDownList ID="ddlGradIscedID" runat="server" CssClass="form-control input-sm select2"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">สาขาวิชาที่จบสูงสุด</td>
                            <td class="col2">
                                <asp:DropDownList ID="ddlGradProgID" runat="server" CssClass="form-control input-sm select2"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ชื่อสถาบันที่จบการศึกษาสูงสุด</td>
                            <td class="col2">
                                <asp:TextBox ID="tbGradUniv" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ประเทศที่จบการศึกษาสูงสุด</td>
                            <td class="col2">
                                <asp:DropDownList ID="ddlGradCountryID" runat="server" CssClass="form-control input-sm select2"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ความพิการ</td>
                            <td class="col2">
                                <asp:DropDownList ID="ddlDeformID" runat="server" CssClass="form-control input-sm select2"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">เลขที่ตำแหน่ง</td>
                            <td class="col2">
                                <asp:TextBox ID="tbSitNo" runat="server" CssClass="form-control input-sm" MaxLength="5" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ศาสนา</td>
                            <td class="col2">
                                <asp:DropDownList ID="ddlReligionID" runat="server" CssClass="form-control input-sm select2"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ประเภทการดำรงตำแหน่งปัจจุบัน</td>
                            <td class="col2">
                                <asp:DropDownList ID="ddlMovementTypeID" runat="server" CssClass="form-control input-sm select2"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">วันที่มีผลบังคับใช้"การเข้าสู่ตำแหน่งปัจจุบัน"</td>
                            <td class="col2">
                                <asp:TextBox ID="tbMovementDate" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <div style="text-align: center; margin-top: 20px">
                        <asp:Button ID="btnAddUser" runat="server" CssClass="btn btn-success" Text="บันทึก" OnClick="btnAddUser_Click"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
        <div id="SaveShow" runat="server" visible="false" class="panel panel-default">
            <div class="panel-body">
                <div class="ps-box-ct10" style="text-align: center;">
                    <div class="ps-div-title-red">ทำการบันทึกข้อมูลบุคลากรสำเร็จ</div>
                    <div style="color: #808080; margin-top: 10px; text-align: center;">
                        ระบบได้ทำการบันทึกข้อมูลบุคลากรเรียบร้อยแล้ว
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