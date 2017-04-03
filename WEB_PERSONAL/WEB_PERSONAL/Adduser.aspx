<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Adduser.aspx.cs" Inherits="WEB_PERSONAL.Adduser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(function () {
            //Initialize Select2 Elements
            $(".select2").select2();
            //Datemask dd/mm/yyyy
            $("#datemask").inputmask("dd/mm/yyyy", { "placeholder": "dd/mm/yyyy" });
            //Datemask2 mm/dd/yyyy
            $("#datemask2").inputmask("mm/dd/yyyy", { "placeholder": "mm/dd/yyyy" });
            //Money Euro
            $("[data-mask]").inputmask();
            //Date range picker
            $('#reservation').daterangepicker();
            //Date range picker with time picker
            $('#reservationtime').daterangepicker({ timePicker: true, timePickerIncrement: 30, format: 'MM/DD/YYYY h:mm A' });
            //Date range as a button
            $('#daterange-btn').daterangepicker(
                {
                    ranges: {
                        'Today': [moment(), moment()],
                        'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                        'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                        'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                        'This Month': [moment().startOf('month'), moment().endOf('month')],
                        'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
                    },
                    startDate: moment().subtract(29, 'days'),
                    endDate: moment()
                },
            function (start, end) {
                $('#reportrange span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
            }
            );
            //iCheck for checkbox and radio inputs
            $('input[type="checkbox"].minimal, input[type="radio"].minimal').iCheck({
                checkboxClass: 'icheckbox_minimal-blue',
                radioClass: 'iradio_minimal-blue'
            });
            //Red color scheme for iCheck
            $('input[type="checkbox"].minimal-red, input[type="radio"].minimal-red').iCheck({
                checkboxClass: 'icheckbox_minimal-red',
                radioClass: 'iradio_minimal-red'
            });
            //Flat red color scheme for iCheck
            $('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
                checkboxClass: 'icheckbox_flat-green',
                radioClass: 'iradio_flat-green'
            });
            //Colorpicker
            $(".my-colorpicker1").colorpicker();
            //color picker with addon
            $(".my-colorpicker2").colorpicker();
            //Timepicker
            $(".timepicker").timepicker({
                showInputs: false
            });
        });
    </script>

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
        $(function () {
            var thaiYear = function (ct) {
                var leap = 3;
                var dayWeek = ["พฤ.", "ศ.", "ส.", "อา.", "จ.", "อ.", "พ."];
                if (ct) {
                    var yearL = new Date(ct).getFullYear() - 543;
                    leap = (((yearL % 4 == 0) && (yearL % 100 != 0)) || (yearL % 400 == 0)) ? 2 : 3;
                    if (leap == 2) {
                        dayWeek = ["ศ.", "ส.", "อา.", "จ.", "อ.", "พ.", "พฤ."];
                    }
                }
                this.setOptions({
                    i18n: { th: { dayOfWeek: dayWeek } }, dayOfWeekStart: leap,
                })
            };
            $('#ContentPlaceHolder1_tbBirthday,#ContentPlaceHolder1_tbDateInwork,#ContentPlaceHolder1_tbDateStartThisU,#ContentPlaceHolder1_tbMovementDate').datetimepicker({
                timepicker: false,
                format: 'd/m/Y',
                lang: 'th',
                onChangeMonth: thaiYear,
                onShow: thaiYear,
                yearOffset: 543,
                closeOnDateSelect: true,
            });
        });
    </script>

    <script>
        function SelectNoTeach(selectElement) {
            var selectedOption = selectElement.selectedIndex;
            var JgetOBJ = $('#<%=ddlTeachISCED.ClientID%>');
            if (selectedOption == 2)
                JgetOBJ.val("").trigger("change");
            if (selectedOption == 2)
                JgetOBJ.select2("enable", false)
            else
                JgetOBJ.select2("enable", true)       
        }
    </script>

    <script>
        function SelectNoTwoOption(selectElement) {
            var selectedOption = selectElement.selectedIndex;
            var Obj1 = $('#<%=ddlGradISCED.ClientID%>');
            var Obj2 = $('#<%=ddlGradProg.ClientID%>');
            if (selectedOption == 1)
                Obj1.select2("enable", false)
            else
                Obj1.select2("enable", true)
            if (selectedOption == 1)
                Obj2.select2("enable", false)
            else
                Obj2.select2("enable", true)
        }
    </script>

    <script type = "text/javascript">
    function DisableButton() {
        document.getElementById("<%=btnAddPerson.ClientID %>").disabled = true;
    }
    window.onbeforeunload = DisableButton;
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="default_page_style">
        <div class="ps-header">
            <img src="Image/Small/add.png" />เพิ่มข้อมูลบุคลากร
            <span style="text-align:right; float:right;"><a href="listuser-admin.aspx">
            <img src="Image/Small/back.png" />ย้อนกลับ</a></span>
        </div>
        <div id="notification" runat="server"></div>

        <div class="panel panel-default">
            <div class="panel-body">
                <div class="ps-box-i0">
                <div class="ps-box-ct10">
                    <table class=" table-hover" style="display: inline-block; vertical-align: top;">
                        <tr>
                                    <td class="col1" style="width: 400px;">รหัสประจำตัวประชาชน<span class="ps-lb-red" />*</td>
                                    <td class="col2">
                                        <asp:TextBox ID="tbCitizenID" runat="server" CssClass="form-control input-sm" MaxLength="13" onkeypress="return isNumberKey(event)" onkeyup="keyup(this,event)" required="required" tabindex="1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1"><a href="Manage-Droplist.aspx?id=Univ">มหาวิทยาลัย</a></td>
                                    <td class="col2">
                                        <asp:DropDownList ID="ddlUniv" runat="server" CssClass="form-control input-sm select2"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1"><a href="Manage-Droplist.aspx?id=Title">คำนำหน้าชื่อ(ยึดตามบัตรประชาชน)</a></td>
                                    <td class="col2">
                                        <asp:DropDownList ID="ddlPrefixName" runat="server" CssClass="form-control input-sm select2"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1">ชื่อ<span class="ps-lb-red" />*</td>
                                    <td class="col2">
                                        <asp:TextBox ID="tbName" runat="server" CssClass="form-control input-sm" onkeyup="Check_txt(this,event)" required="required" tabindex="1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1">นามสกุล<span class="ps-lb-red" />*</td>
                                    <td class="col2">
                                        <asp:TextBox ID="tbLastName" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1"><a href="Manage-Droplist.aspx?id=Gender">เพศ</a></td>
                                    <td class="col2">
                                        <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control input-sm select2"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1">วันเกิด<span class="ps-lb-red" />*</td>
                                    <td class="col2">
                                        <asp:TextBox ID="tbBirthday" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"></asp:TextBox>
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
                                    <td class="col1"><a href="Manage-Droplist.aspx?id=Province">จังหวัด</a><span class="ps-lb-red" />*</td>
                                    <td class="col2">
                                        <asp:DropDownList ID="ddlProvince" runat="server" CssClass="form-control input-sm select2"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1"><a href="Manage-Droplist.aspx?id=Amphur">เขต/อำเภอ</a><span class="ps-lb-red" />*</td>
                                    <td class="col2">
                                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control input-sm select2"></asp:DropDownList>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1"><a href="Manage-Droplist.aspx?id=Tambon">แขวง/ตำบล</a><span class="ps-lb-red" />*</td>
                                    <td class="col2">
                                        <asp:DropDownList ID="ddlSubDistrict" runat="server" CssClass="form-control input-sm select2" required="required" tabindex="1"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1">หมายเลขโทรศัพท์ที่ทำงาน</td>
                                    <td class="col2">
                                        <asp:TextBox ID="tbTelephone" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1">รหัสไปรษณีย์<span id="spZip" runat="server"></span></td>
                                    <td class="col2">
                                        <asp:TextBox ID="tbZipcode" runat="server" CssClass="form-control input-sm" MaxLength="5" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1"><a href="Manage-Droplist.aspx?id=Nation">สัญชาติ</a><span class="ps-lb-red" />*</td>
                                    <td class="col2">
                                        <asp:DropDownList ID="ddlNation" runat="server" CssClass="form-control input-sm select2" required="required" tabindex="1"></asp:DropDownList>
                                    </td>
                                </tr>

                        <tr>
                                    <td class="col1" style="width: 400px;"><a href="Manage-Droplist.aspx?id=Stafftype">ประเภทบุคลากร</a><span class="ps-lb-red" />*</td>
                                    <td class="col2">
                                        <asp:DropDownList ID="ddlStafftype" runat="server" CssClass="form-control input-sm select2" required="required" tabindex="1"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1"><a href="Manage-Droplist.aspx?id=TimeContact">ระยะเวลาจ้าง</a><span class="ps-lb-red" />*</td>
                                    <td class="col2">
                                        <asp:DropDownList ID="ddlTimeContact" runat="server" CssClass="form-control input-sm select2" required="required" tabindex="1"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1"><a href="Manage-Droplist.aspx?id=Budget">ประเภทเงินจ้าง</a><span class="ps-lb-red" />*</td>
                                    <td class="col2">
                                        <asp:DropDownList ID="ddlBudget" runat="server" CssClass="form-control input-sm select2" required="required" tabindex="1"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1"><a href="Manage-Droplist.aspx?id=SubStafftype">ประเภทบุคลากรย่อย</a><span class="ps-lb-red" />*</td>
                                    <td class="col2">
                                        <asp:DropDownList ID="ddlSubStafftype" runat="server" CssClass="form-control input-sm select2" ></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1"><a href="Manage-Droplist.aspx?id=AdminPosition">ตำแหน่งบริหาร</a></td>
                                    <td class="col2">
                                        <asp:DropDownList ID="ddlAdminPosition" runat="server" CssClass="form-control input-sm select2"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1"><a href="Manage-Droplist.aspx?id=Position">ระดับตำแหน่ง</a><span class="ps-lb-red" />*</td>
                                    <td class="col2">
                                        <asp:DropDownList ID="ddlPosition" runat="server" CssClass="form-control input-sm select2" required="required" tabindex="1"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1">ตำแหน่งในสายงาน</td>
                                    <td class="col2">
                                        <asp:TextBox ID="tbPositionWork" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1"><a href="Manage-Droplist.aspx?id=Department">คณะ/หน่วยงานที่สังกัด หรือเทียบเท่า</a><span class="ps-lb-red" />*</td>
                                    <td class="col2">
                                        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control input-sm select2" required="required" tabindex="1"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1">วันที่เข้าทำงานครั้งแรก<span class="ps-lb-red" />*</td>
                                    <td class="col2">
                                        <asp:TextBox ID="tbDateInwork" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1">วันที่เข้าทำงาน ณ สถานที่ปัจจุบัน<span class="ps-lb-red" />*</td>
                                    <td class="col2">
                                        <asp:TextBox ID="tbDateStartThisU" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1">สาขางานที่เชี่ยวชาญ</td>
                                    <td class="col2">
                                        <asp:TextBox ID="tbSpecialName" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1"><a href="Manage-Droplist.aspx?id=TeachISCED">กลุ่มสาขาวิชาที่สอน(ISCED)</a><span id="spTeachISCED" runat="server" /></td>
                                    <td class="col2">
                                        <asp:DropDownList ID="ddlTeachISCED" runat="server" CssClass="form-control input-sm select2"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1"><a href="Manage-Droplist.aspx?id=GradLev">ระดับการศึกษาที่จบสูงสุด</a><span class="ps-lb-red" />*</td>
                                    <td class="col2">
                                        <asp:DropDownList ID="ddlGradLev" runat="server" CssClass="form-control input-sm select2" ></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1">หลักสูตรที่จบการศึกษาสูงสุด<span class="ps-lb-red" />*</td>
                                    <td class="col2">
                                        <asp:TextBox ID="tbGradCURR" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1"><a href="Manage-Droplist.aspx?id=TeachISCED">กลุ่มสาขาวิชาที่จบสูงสุด(ISCED)</a><span id="spGradISCED" runat="server" /></td>
                                    <td class="col2">
                                        <asp:DropDownList ID="ddlGradISCED" runat="server" CssClass="form-control input-sm select2" required="required" tabindex="1"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1"><a href="Manage-Droplist.aspx?id=GradProg">สาขาวิชาที่จบสูงสุด</a><span id="spGradProg" runat="server" /></td>
                                    <td class="col2">
                                        <asp:DropDownList ID="ddlGradProg" runat="server" CssClass="form-control input-sm select2" required="required" tabindex="1"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1">ชื่อสถาบันที่จบการศึกษาสูงสุด</td>
                                    <td class="col2">
                                        <asp:TextBox ID="tbGradUniv" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1"><a href="Manage-Droplist.aspx?id=Nation">ประเทศที่จบการศึกษาสูงสุด</a></td>
                                    <td class="col2">
                                        <asp:DropDownList ID="ddlGradCountry" runat="server" CssClass="form-control input-sm select2"></asp:DropDownList>
                                    </td>
                                </tr>

                        <tr>
                                    <td class="col1" style="width: 400px;"><a href="Manage-Droplist.aspx?id=Deform">ความพิการ</a></td>
                                    <td class="col2">
                                        <asp:DropDownList ID="ddlDeform" runat="server" CssClass="form-control input-sm select2"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1">เลขที่ตำแหน่ง</td>
                                    <td class="col2">
                                        <asp:TextBox ID="tbSitNo" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1">เงินเดือนปัจจุบัน</td>
                                    <td class="col2 input-group date">
                                        <asp:TextBox ID="tbSalary" runat="server" CssClass="form-control input-sm" onkeypress="return isNumberKey(event)"></asp:TextBox><span class="input-group-addon">.00</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1">เงินประจำตำแหน่งที่ได้รับ</td>
                                    <td class="col2 input-group date">
                                        <asp:TextBox ID="tbPositionSalary" runat="server" CssClass="form-control input-sm" onkeypress="return isNumberKey(event)"></asp:TextBox><span class="input-group-addon">.00</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1"><a href="Manage-Droplist.aspx?id=Religion">ศาสนา</a></td>
                                    <td class="col2">
                                        <asp:DropDownList ID="ddlReligion" runat="server" CssClass="form-control input-sm select2"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1"><a href="Manage-Droplist.aspx?id=MovementType">ประเภทการดำรงตำแหน่งปัจจุบัน</a></td>
                                    <td class="col2">
                                        <asp:DropDownList ID="ddlMovementType" runat="server" CssClass="form-control input-sm select2"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1">วันที่มีผลบังคับใช้ตามข้อมูล"การเข้าสู่ตำแหน่งปัจจุบัน"</td>
                                    <td class="col2">
                                        <asp:TextBox ID="tbMovementDate" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                    </td>
                                </tr>
                                
                            </table>
                </div>
            </div>

                        <div>
                            <div class="ps-div-title-red">ทำการบันทึกข้อมูลบุคลากรสำเร็จ</div>
                            <div style="color: #808080; margin-top: 10px; text-align: center;">
                                ระบบได้ทำการบันทึกข้อมูลบุคลากรเรียบร้อยแล้ว
                            </div>
                            <div style="text-align: center; margin-top: 10px;">
                                <a href="Default.aspx" class="ps-button btn btn-primary"><img src="Image/Small/home3.png" class="icon_left" />กลับหน้าหลัก</a>
                            </div>
                        </div>
            </div>
        </div>
    </div>
    <div style="text-align: center;">
        <asp:Button ID="btnAddPerson" runat="server" CssClass="btn btn-success" Text="บันทึก"></asp:Button>
    </div>
</asp:Content>