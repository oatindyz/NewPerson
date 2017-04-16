<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="WEB_PERSONAL.Edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="default_page_style">
        <div class="ps-header">
            <img src="Image/Icon/edit.png" />แก้ไขข้อมูลบุคลากร
            <span id="SpanBack" runat="server" visible="false" style="text-align: right; float: right;"><a href="ListRequest.aspx">
                <img src="Image/Small/back.png" />ย้อนกลับ</a></span>
        </div>

        <div id="notification" runat="server"></div>

        <div id="DataShow" runat="server" class="panel panel-default">
            <div class="panel-body">
                <div class="ps-box-ct10" style="text-align: center;">
                    <table class="ps-table-x16" style="display: inline-block; vertical-align: top; text-align: left;">
                        <tr>
                            <td class="col1">รหัสบัตรประชาชน</td>
                            <td class="col2">
                                <asp:Label ID="lbCitizenID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">คำนำหน้า</td>
                            <td class="col2">
                                <asp:Label ID="lbTitleID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ชื่อ</td>
                            <td class="col2">
                                <asp:Label ID="lbFirstName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">นามสกุล</td>
                            <td class="col2">
                                <asp:Label ID="lbLastName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">เพศ</td>
                            <td class="col2">
                                <asp:Label ID="lbGenderID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">วันเกิด</td>
                            <td class="col2">
                                <asp:Label ID="lbBirthdayDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">อีเมล</td>
                            <td class="col2">
                                <asp:Label ID="lbEmail" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">บ้านเลขที่</td>
                            <td class="col2">
                                <asp:Label ID="Label7" runat="server"></asp:Label>
                                <asp:TextBox ID="tbHomeAdd" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">หมู่</td>
                            <td class="col2">
                                <asp:Label ID="Label8" runat="server"></asp:Label>
                                <asp:TextBox ID="tbMoo" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ถนน</td>
                            <td class="col2">
                                <asp:Label ID="Label9" runat="server"></asp:Label>
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
                            <td class="col1">สัญชาติ</td>
                            <td class="col2">
                                <asp:Label ID="lbNationID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">วิทยาเขต</td>
                            <td class="col2">
                                <asp:Label ID="lbCampusID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">สำนัก/สถาบัน/คณะ</td>
                            <td class="col2">
                                <asp:Label ID="lbFacultyID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">กอง/สำนักงานเลขา/ภาควิชา</td>
                            <td class="col2">
                                <asp:Label ID="lbDivisionID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">งาน/ฝ่าย</td>
                            <td class="col2">
                                <asp:Label ID="lbWorkDivisionID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        
                    </table>
                    <table class="ps-table-x16" style="display: inline-block; vertical-align: top; text-align: left;">
                        <tr>
                            <td class="col1">ประเภทบุคลากร</td>
                            <td class="col2">
                                <asp:Label ID="lbStafftypeID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ระยะเวลาจ้าง</td>
                            <td class="col2">
                                <asp:Label ID="lbTimeContactID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ประเภทเงินจ้าง</td>
                            <td class="col2">
                                <asp:Label ID="lbBudgetID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ประเภทบุคลากรย่อย</td>
                            <td class="col2">
                                <asp:Label ID="lbSubStafftypeID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ตำแหน่งทางบริหาร</td>
                            <td class="col2">
                                <asp:Label ID="lbAdminPosID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ตำแหน่งในสายงาน</td>
                            <td class="col2">
                                <asp:Label ID="lbWorkPosID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">วันที่เข้าทำงานครั้งแรก</td>
                            <td class="col2">
                                <asp:Label ID="lbDateInwork" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">วันที่เข้าทำงาน ณ สถานที่ปัจจุบัน</td>
                            <td class="col2">
                                <asp:Label ID="lbDateStartThisU" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">สาขางานที่เชี่ยวชาญ</td>
                            <td class="col2">
                                <asp:Label ID="lbSpecialName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">กลุ่มสาขาวิชาที่สอน(ISCED)</td>
                            <td class="col2">
                                <asp:Label ID="lbTeachIscedID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ระดับการศึกษาที่จบสูงสุด</td>
                            <td class="col2">
                                <asp:Label ID="lbGradLevID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">หลักสูตรที่จบการศึกษาสูงสุด</td>
                            <td class="col2">
                                <asp:Label ID="lbGradCurr" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">กลุ่มสาขาวิชาที่จบสูงสุด(ISCED)</td>
                            <td class="col2">
                                <asp:Label ID="lbGradIscedID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trGradProgID" runat="server">
                            <td class="col1">สาขาวิชาที่จบสูงสุด</td>
                            <td class="col2">
                                <asp:Label ID="lbGradProgID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ชื่อสถาบันที่จบการศึกษาสูงสุด</td>
                            <td class="col2">
                                <asp:Label ID="lbGradUniv" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ประเทศที่จบการศึกษาสูงสุด</td>
                            <td class="col2">
                                <asp:Label ID="lbGradCountryID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ความพิการ</td>
                            <td class="col2">
                                <asp:Label ID="lbDeformID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">เลขที่ตำแหน่ง</td>
                            <td class="col2">
                                <asp:Label ID="lbSitNo" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ศาสนา</td>
                            <td class="col2">
                                <asp:Label ID="lbReligionID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ประเภทการดำรงตำแหน่งปัจจุบัน</td>
                            <td class="col2">
                                <asp:Label ID="lbMovementTypeID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">วันที่มีผลบังคับใช้"การเข้าสู่ตำแหน่งปัจจุบัน"</td>
                            <td class="col2">
                                <asp:Label ID="lbMovementDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <div style="text-align: center; margin-top: 20px">
                        <asp:Button ID="btnUpdateUser" runat="server" CssClass="btn btn-success" Text="บันทึก" OnClick="btnUpdateUser_Click"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>