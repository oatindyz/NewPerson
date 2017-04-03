<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="WEB_PERSONAL.Profile" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .c1 {
            background-color: #ffffff;
            min-height: 200px;
            padding: 5px;
        }

            .c1 a {
                border: 1px solid transparent;
                display: inline-block;
                margin: 2px;
            }

                .c1 a:hover {
                    border: 1px solid #ffffff;
                }

            .c1 img {
                max-height: 190px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="default_page_style">
        <div class="ps-header">
            <img src="Image/Small/person2.png" />ข้อมูลผู้ใช้งาน</div>
        <div class="ps-box">
            <div class="ps-box-i0">
                <div class="ps-box-hd10">
                    <img src="Image/Small/document.png" />ข้อมูลพื้นฐาน</div>
                <div class="ps-box-ct10">
                    <table class="ps-table-x16" style="vertical-align: top;">
                        <tr>
                            <td class="col1">รหัสประชาชน</td>
                            <td class="col2">
                                <asp:label id="lbCitizenID" runat="server"></asp:label>
                            </td>
                            <td class="col1">&nbsp;</td>
                            <td class="col2"></td>
                        </tr>
                        <tr>
                            <td class="col1">ชื่อจริง</td>
                            <td class="col2">
                                <asp:label id="lbFirstName" runat="server"></asp:label>
                            </td>
                            <td class="col1">นามสกุล</td>
                            <td class="col2">

                                <asp:label id="lbLastName" runat="server"></asp:label>

                            </td>
                        </tr>
                        <tr>
                            <td class="col1">เพศ</td>
                            <td class="col2">
                                <asp:label id="lbGender" runat="server"></asp:label>
                            </td>
                            <td class="col1">วันเกิด</td>
                            <td class="col2">

                                <asp:label id="lbBirthday" runat="server"></asp:label>

                            </td>
                        </tr>
                        <tr>
                            <td class="col1">เชื้อชาติ</td>
                            <td class="col2">
                                <asp:label id="lbRace" runat="server"></asp:label>
                            </td>
                            <td class="col1">สัญชาติ</td>
                            <td class="col2">

                                <asp:label id="lbNation" runat="server"></asp:label>

                            </td>
                        </tr>
                        <tr>
                            <td class="col1">สถานภาพ</td>
                            <td class="col2">
                                <asp:label id="lbStatusPerson" runat="server"></asp:label>
                            </td>
                            <td class="col1">กรุ๊ปเลือด</td>
                            <td class="col2">

                                <asp:label id="lbBlood" runat="server"></asp:label>

                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ศาสนา</td>
                            <td class="col2">
                                <asp:label id="lbReligion" runat="server"></asp:label>
                            </td>
                            <td class="col1">เบอร์โทรศัพท์</td>
                            <td class="col2">

                                <asp:label id="lbPhone" runat="server"></asp:label>

                            </td>
                        </tr>
                        <tr>
                            <td class="col1">อีเมล</td>
                            <td class="col2">
                                <asp:label id="lbEmail" runat="server"></asp:label>
                            </td>
                            <td class="col1">&nbsp;</td>
                            <td class="col2"></td>
                        </tr>
                        <tr>
                            <td class="col1">ชื่อบิดา</td>
                            <td class="col2">
                                <asp:label id="lbFather" runat="server"></asp:label>
                            </td>
                            <td class="col1">ชื่อมารดา</td>
                            <td class="col2">

                                <asp:label id="lbMother" runat="server"></asp:label>

                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ชื่อคู่ครอง</td>
                            <td class="col2">
                                <asp:label id="lbCouple" runat="server"></asp:label>
                            </td>
                            <td class="col1">&nbsp;</td>
                            <td class="col2"></td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="ps-box-i0">
                <div class="ps-box-hd10">
                    <img src="Image/Small/office.png" />ข้อมูลทางการงาน</div>
                <div class="ps-box-ct10">
                    <table class="ps-table-x16" style="vertical-align: top;">
                        <tr>
                            <td class="col1">สถานะการทำงาน</td>
                            <td class="col2">
                                <asp:label id="lbStatusWork" runat="server"></asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">วันที่เริ่มเข้ารับราชการ</td>
                            <td class="col2">
                                <asp:label id="lbInWorkDay" runat="server"></asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ตำแหน่งในสายงาน</td>
                            <td class="col2">
                                <asp:label id="lbPosition" runat="server"></asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ตำแหน่งในทางบริหาร</td>
                            <td class="col2">
                                <asp:label id="lbAdminPosition" runat="server"></asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">งาน</td>
                            <td class="col2">
                                <asp:label id="lbWorkDivision" runat="server"></asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">แผนก</td>
                            <td class="col2">
                                <asp:label id="lbDept" runat="server"></asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">คณะ</td>
                            <td class="col2">
                                <asp:label id="lbFaculty" runat="server"></asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">วิทยาเขต</td>
                            <td class="col2">
                                <asp:label id="lbCampus" runat="server"></asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">กระทรวง</td>
                            <td class="col2">
                                <asp:label id="lbMinistry" runat="server"></asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">กรม</td>
                            <td class="col2">
                                <asp:label id="lbGrom" runat="server"></asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">เบอร์โทรศัพท์ที่ทำงาน</td>
                            <td class="col2">
                                <asp:label id="lbWorkPhone" runat="server"></asp:label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="ps-box-i0">
                <div class="ps-box-hd10">
                    <img src="Image/Small/location.png" />ที่อยู่</div>
                <div class="ps-box-ct10">
                    <table class="ps-table-x16" style="display: inline-block; vertical-align: top;">
                        <tr>
                            <td class="head" colspan="2">ที่อยู่</td>
                        </tr>
                        <tr>
                            <td class="col1">บ้านเลขที่</td>
                            <td class="col2">
                                <asp:label id="lbHomeAdd" runat="server"></asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ซอย</td>
                            <td class="col2">
                                <asp:label id="lbSoi" runat="server"></asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">หมู่</td>
                            <td class="col2">
                                <asp:label id="lbMoo" runat="server"></asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ถนน</td>
                            <td class="col2">
                                <asp:label id="lbStreet" runat="server"></asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">จังหวัด</td>
                            <td class="col2">
                                <asp:label id="lbProvince" runat="server"></asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">อำเภอ</td>
                            <td class="col2">
                                <asp:label id="lbAmphur" runat="server"></asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ตำบล</td>
                            <td class="col2">
                                <asp:label id="lbDistrict" runat="server"></asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">รหัสไปรษณีย์</td>
                            <td class="col2">
                                <asp:label id="lbZipcode" runat="server"></asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ประเทศ</td>
                            <td class="col2">
                                <asp:label id="lbCountry" runat="server"></asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">รัฐ</td>
                            <td class="col2">
                                <asp:label id="lbState" runat="server"></asp:label>
                            </td>
                        </tr>
                    </table>
                    <table class="ps-table-x16" style="display: inline-block; vertical-align: top;">
                        <tr>
                            <td class="head" colspan="2">ที่อยู่ปัจจุบัน</td>
                        </tr>
                        <tr>
                            <td class="col1">บ้านเลขที่</td>
                            <td class="col2">
                                <asp:label id="lbHomeAddNow" runat="server"></asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ซอย</td>
                            <td class="col2">
                                <asp:label id="lbSoiNow" runat="server"></asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">หมู่</td>
                            <td class="col2">
                                <asp:label id="lbMooNow" runat="server"></asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ถนน</td>
                            <td class="col2">
                                <asp:label id="lbStreetNow" runat="server"></asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">จังหวัด</td>
                            <td class="col2">
                                <asp:label id="lbProvinceNow" runat="server"></asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">อำเภอ</td>
                            <td class="col2">
                                <asp:label id="lbAmphurNow" runat="server"></asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ตำบล</td>
                            <td class="col2">
                                <asp:label id="lbDistrictNow" runat="server"></asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">รหัสไปรษณีย์</td>
                            <td class="col2">
                                <asp:label id="lbZipcodeNow" runat="server"></asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">ประเทศ</td>
                            <td class="col2">
                                <asp:label id="lbCountryNow" runat="server"></asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col1">รัฐ</td>
                            <td class="col2">
                                <asp:label id="lbStateNow" runat="server"></asp:label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="ps-box-i0">
                <div class="ps-box-hd10">
                    <img src="Image/Small/image.png" />รูปภาพ</div>
                <div class="ps-box-ct10">
                    <div style="background-color: #f0f0f0; color: #000000; padding: 5px 10px;" id="id1" runat="server">
                        <asp:fileupload id="FileUpload1" runat="server" />
                        <asp:linkbutton id="lbuUploadPicture" runat="server" cssclass="ps-button" onclick="lbuUploadPicture_Click"><img src="Image/Small/upload.png" class="icon_left"/>อัพโหลด</asp:linkbutton>
                    </div>
                    <div class="c1" id="profile_images" runat="server">
                    </div>
                </div>
            </div>
        </div>

    </div>

</asp:Content>
