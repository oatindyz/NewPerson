<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="DataManager.aspx.cs" Inherits="WEB_PERSONAL.DataManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="ps-header">
            <img src="Image/Small/wrench.png" />แก้ไขข้อมูลตัวเลือก (Dropdown List)
        </div>
        <div style="text-align: center;">
            <div class="ps-box-il" style="vertical-align: top; margin-right: 10px;">
                <div class="ps-box-i0">
                    <div class="ps-box-hd10"><img src="Image/Small/person2.png" class="icon_left"/>จัดการข้อมูลบุคลากร</div>
                </div>
                <div class="ps-box-i0">
                    <div class="ps-box-ct10" style="text-align: left;">
                        <div style="display: inline-block; vertical-align: top; margin-right: 10px;">
                            <div><a href="Rank-ADMIN.aspx" class="ps-link" ><img src="Image/Small/wrench.png" class="icon_left"/>ยศ</a></div>
                            <div><a href="TitleName-ADMIN.aspx" class="ps-link" ><img src="Image/Small/wrench.png" class="icon_left"/>คำนำหน้า</a></div>
                            <div><a href="Gender-ADMIN.aspx" class="ps-link" ><img src="Image/Small/wrench.png" class="icon_left"/>เพศ</a></div>
                            <div><a href="National-ADMIN.aspx" class="ps-link" ><img src="Image/Small/wrench.png" class="icon_left"/>เชื้อชาติ / สัญชาติ</a></div>
                            <div><a href="Blood-ADMIN.aspx" class="ps-link" ><img src="Image/Small/wrench.png" class="icon_left"/>กรุ๊ปเลือด</a></div>
                            <div><a href="Religion-ADMIN.aspx" class="ps-link" ><img src="Image/Small/wrench.png" class="icon_left"/>ศาสนา</a></div>
                            <div><a href="StatusPerson-ADMIN.aspx" class="ps-link" ><img src="Image/Small/wrench.png" class="icon_left"/>สถานภาพ</a></div>
                            <div><a href="Province-ADMIN.aspx" class="ps-link" ><img src="Image/Small/wrench.png" class="icon_left"/>จังหวัด</a></div>
                            <div><a href="Amphur-ADMIN.aspx" class="ps-link" ><img src="Image/Small/wrench.png" class="icon_left"/>อำเภอ</a></div>
                            <div><a href="District-ADMIN.aspx" class="ps-link" ><img src="Image/Small/wrench.png" class="icon_left"/>ตำบล</a></div>
                            <div><a href="Country-ADMIN.aspx" class="ps-link" ><img src="Image/Small/wrench.png" class="icon_left"/>ประเทศ</a></div>

                        </div>
                        <div style="display: inline-block; vertical-align: top;">
                            <div><a href="Campus-ADMIN.aspx" class="ps-link" ><img src="Image/Small/wrench.png" class="icon_left"/>วิทยาเขต</a></div>
                            <div><a href="Faculty-ADMIN.aspx" class="ps-link" ><img src="Image/Small/wrench.png" class="icon_left"/>สำนัก / สถาบัน / คณะ</a></div>
                            <div><a href="Division-ADMIN.aspx" class="ps-link" ><img src="Image/Small/wrench.png" class="icon_left"/>กอง / สำนักงานเลขา / ภาควิชา</a></div>
                            <div><a href="WorkDivision-ADMIN.aspx" class="ps-link" ><img src="Image/Small/wrench.png" class="icon_left"/>งาน / ฝ่าย</a></div>
                            <div><a href="StaffType-ADMIN.aspx" class="ps-link" ><img src="Image/Small/wrench.png" class="icon_left"/>ประเภทบุคลากร</a></div>
                            <div><a href="Budget-ADMIN.aspx" class="ps-link" ><img src="Image/Small/wrench.png" class="icon_left"/>ประเภทเงินจ้าง</a></div>
                            <div><a href="TeachISCED-ADMIN.aspx" class="ps-link" ><img src="Image/Small/wrench.png" class="icon_left"/>กลุ่มสาขาวิชาที่สอน</a></div>
                            <div><a href="StatusWork-ADMIN.aspx" class="ps-link" ><img src="Image/Small/wrench.png" class="icon_left"/>สถานะบุคลากร</a></div>
                            <div><a href="PositionWork-ADMIN.aspx" class="ps-link" ><img src="Image/Small/wrench.png" class="icon_left"/>ตำแหน่งในสายงาน</a></div>
                            <div><a href="AdminPosition-ADMIN.aspx" class="ps-link" ><img src="Image/Small/wrench.png" class="icon_left"/>ตำแหน่งทางบริหาร</a></div>
                            <div><a href="AcademicPosition-ADMIN.aspx" class="ps-link" ><img src="Image/Small/wrench.png" class="icon_left"/>ตำแหน่งทางวิชาการ</a></div>    
                        </div>
                    
                        <div style="display: inline-block; vertical-align: top;">
                            <div><a href="GradLev-ADMIN.aspx" class="ps-link" ><img src="Image/Small/wrench.png" class="icon_left"/>ระดับการศึกษา</a></div>
                            <div><a href="Month-ADMIN.aspx" class="ps-link" ><img src="Image/Small/wrench.png" class="icon_left"/>เดือน</a></div>
                            <div><a href="Year-ADMIN.aspx" class="ps-link" ><img src="Image/Small/wrench.png" class="icon_left"/>ปี</a></div>
                            <div><a href="Staff-ADMIN.aspx" class="ps-link" ><img src="Image/Small/wrench.png" class="icon_left"/>ตำแหน่งประเภท</a></div>                         
                            <div><a href="Position-ADMIN.aspx" class="ps-link" ><img src="Image/Small/wrench.png" class="icon_left"/>ระดับ</a></div>
                         </div>
 
                    </div>
                </div>
            </div>
            <div class="ps-box-il" style="vertical-align: top;">
                <div class="ps-box-i0">
                    <div class="ps-box-hd10"><img src="Image/Small/medal.png" class="icon_left"/>จัดการข้อมูลเครื่องราชอิสริยาภรณ์</div>
                </div>
                <div class="ps-box-i0">
                    <div class="ps-box-ct10" style="text-align: left;">
                        <div><a href="Claninsignia-ADMIN.aspx" class="ps-link" ><img src="Image/Small/wrench.png" class="icon_left"/>กลุ่มเครื่องราชฯ</a></div>
                        <div><a href="GradeInsignia-ADMIN.aspx" class="ps-link" ><img src="Image/Small/wrench.png" class="icon_left"/>เครื่่องราชฯ</a></div>
                        <div><a href="psPerson-ADMIN.aspx" class="ps-link" ><img src="Image/Small/wrench.png" class="icon_left"/>ตำแหน่งประเภท</a></div>
                        <div><a href="PosiGoverSalary-ADMIN.aspx" class="ps-link" ><img src="Image/Small/wrench.png" class="icon_left"/>ขั้นเงินเดือนตำแหน่ง</a></div>
                    </div>
                </div>
            </div>
        </div>
        <div class="ps-separator"></div>  
    </div>
</asp:Content>
