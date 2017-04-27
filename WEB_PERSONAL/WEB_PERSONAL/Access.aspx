<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Access.aspx.cs" Inherits="WEB_PERSONAL.Access" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link rel="icon" href="Image/favicon.ico" />
    <title>ระบบบุคลากร - มหาวิทยาลัยเทคโนโลยีราชมงคลตะวันออก</title>

    <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css" />
    <!-- Ionicons -->
    <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css" />
    <!-- daterange picker -->
    <link rel="stylesheet" href="plugins/daterangepicker/daterangepicker-bs3.css" />
    <!-- iCheck for checkboxes and radio inputs -->
    <link rel="stylesheet" href="plugins/iCheck/all.css" />
    <!-- Bootstrap Color Picker -->
    <link rel="stylesheet" href="plugins/colorpicker/bootstrap-colorpicker.min.css" />
    <!-- Bootstrap time Picker -->
    <link rel="stylesheet" href="plugins/timepicker/bootstrap-timepicker.min.css" />
    <!-- Select2 -->
    <link rel="stylesheet" href="plugins/select2/select2.min.css" />

    <script src="plugins/jQuery/jQuery-2.1.4.min.js"></script>
    <!-- Bootstrap 3.3.5 -->
    <script src="bootstrap/js/bootstrap.min.js"></script>
    <!-- Select2 -->
    <script src="plugins/select2/select2.full.min.js"></script>
    <!-- InputMask -->
    <script src="plugins/input-mask/jquery.inputmask.js"></script>
    <script src="plugins/input-mask/jquery.inputmask.date.extensions.js"></script>
    <script src="plugins/input-mask/jquery.inputmask.extensions.js"></script>
    <!-- date-range-picker -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.10.2/moment.min.js"></script>
    <script src="plugins/daterangepicker/daterangepicker.js"></script>
    <!-- bootstrap color picker -->
    <script src="plugins/colorpicker/bootstrap-colorpicker.min.js"></script>
    <!-- bootstrap time picker -->
    <script src="plugins/timepicker/bootstrap-timepicker.min.js"></script>
    <!-- SlimScroll 1.3.0 -->
    <script src="plugins/slimScroll/jquery.slimscroll.min.js"></script>
    <!-- iCheck 1.0.1 -->
    <script src="plugins/iCheck/icheck.min.js"></script>
    <!-- FastClick -->
    <script src="plugins/fastclick/fastclick.min.js"></script>

    <!-- Page script -->
    <script src="jquery.datetimepicker.js"></script>
    <!-- -->

    <link rel="stylesheet" type="text/css" href="CSS/Master.css" />
    <link href="CSS/Access.css" rel="stylesheet" />

    <script>
        $.datepicker.regional['th'] = {
            changeMonth: true,
            changeYear: true,
            //defaultDate: GetFxupdateDate(FxRateDateAndUpdate.d[0].Day),
            yearOffSet: 543,
            //showOn: "button",
            //buttonImage: 'jQueryCalendarThai/images/calendar.gif',
            //buttonImageOnly: true,
            dateFormat: 'dd M yy',
            dayNames: ['อาทิตย์', 'จันทร์', 'อังคาร', 'พุธ', 'พฤหัสบดี', 'ศุกร์', 'เสาร์'],
            dayNamesMin: ['อา', 'จ', 'อ', 'พ', 'พฤ', 'ศ', 'ส'],
            monthNames: ['มกราคม', 'กุมภาพันธ์', 'มีนาคม', 'เมษายน', 'พฤษภาคม', 'มิถุนายน', 'กรกฎาคม', 'สิงหาคม', 'กันยายน', 'ตุลาคม', 'พฤศจิกายน', 'ธันวาคม'],
            monthNamesShort: ['ม.ค.', 'ก.พ.', 'มี.ค.', 'เม.ย.', 'พ.ค.', 'มิ.ย.', 'ก.ค.', 'ส.ค.', 'ก.ย.', 'ต.ค.', 'พ.ย.', 'ธ.ค.'],
            constrainInput: true,

            prevText: 'ก่อนหน้า',
            nextText: 'ถัดไป',
            yearRange: '-100:+100',
            buttonText: 'เลือก',
            beforeShow: function () {
                if ($(this).val() != "") {
                    var arrayDate = $(this).val().split(" ");
                    if (arrayDate.length == 4) {
                        arrayDate[2] = arrayDate[3];
                    }
                    $(this).val(arrayDate[0] + " " + arrayDate[1] + " " + arrayDate[2]);
                } else {
                    $(this).datepicker("setDate", new Date()); //Set ค่าวันปัจจุบัน
                }
            },
            onClose: function (dateText, inst) {
                $(this).datepicker('option', 'dateFormat', 'dd M yy');
            }
        };
        $.datepicker.setDefaults($.datepicker.regional['th']);
    </script>

    <script>
        $(function () {
            $("#tbPassDate").datepicker($.datepicker.regional["th"]);
        });
    </script>

    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode != 46 && charCode > 31
              && (charCode < 48 || charCode > 57)) {
                $("#errmsg").html("Digits Only").show().fadeOut("slow");
                return false;
            }
            return true;
        }
    </script>

    <script type="text/javascript">
        function RefreshUpdatePanel() {
            if (this.value.length != 13) return false;
            if (this.value.length == 13) __doPostBack('<%= tbUsername.ClientID %>', '');
            document.getElementById('<%= tbPassword.ClientID %>').focus();
            return true;
        };
    </script>

    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnLogin.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div class="login_popup">
            <div class="login_popup_in_access">
                <div class="login_popup_in2">
                    <div style="width: 400px;">
                        <div style="text-align: center; margin-bottom: 20px;">
                            <div>
                                <img class="login_logo" src="Image/RMUTTO.png" />
                                <div class="t1">ระบบบุคลากร</div>
                                <div class="t2">มหาวิทยาลัยเทคโนโลยีราชมงคลตะวันออก</div>
                            </div>
                        </div>
                        <div style="text-align: center; margin-bottom: 10px;">
                            <div>
                                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnLogin">
                                    <div class="input-group date">
                                        <asp:TextBox ID="tbUsername" runat="server" CssClass="form-control" MaxLength="13" placeHolder="รหัสประชาชน" onkeypress="return isNumberKey(event)" required="required" TabIndex="1"></asp:TextBox>
                                        <div class="input-group-addon"><span class="glyphicon glyphicon-user"></span></div>
                                    </div>
                                    <div>

                                        <asp:Label ID="LabelTop" runat="server" CssClass="cerror"></asp:Label>

                                    </div>

                                    <asp:UpdatePanel ID="UpdatetbPassword" runat="server">
                                        <ContentTemplate>
                                            <div class="input-group date" data-provide="datepicker" data-date-format="mm/dd/yyyy">
                                                <asp:TextBox ID="tbPassword" TextMode="Password" runat="server" CssClass="form-control" placeHolder="รหัสผ่าน" MaxLength="25" required="required" TabIndex="1"></asp:TextBox>
                                                <div class="input-group-addon"><span class="glyphicon glyphicon-lock"></span></div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="tbPassword" />
                                        </Triggers>
                                    </asp:UpdatePanel>


                                    <div style="margin-top: 20px;">
                                        <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-success" OnClick="btnLogin_Click" Text="เข้าสู่ระบบ" />
                                        <button type="button" class="btn btn-info" data-toggle="modal" data-target="#myModal">ลืมรหัสผ่าน</button>
                                    </div>
                                    <div>
                                        <asp:UpdatePanel ID="UpdateLabel12X" runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="LabelBottom" runat="server" CssClass="cerror"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <div style="text-align: center;">
                        <div style="font-size: 16px; margin-bottom: 10px;">
                            <img src="Image/Small/web.png" class="icon_left" />เว็บไซต์ในสถาบัน
                        </div>
                        <div>
                            <div class="web-link">
                                <a href="http://www.rmutto.ac.th">บางพระ</a>
                                <a href="http://www.chan.rmutto.ac.th">จันทบุรี</a>
                                <a href="http://www.cpc.rmutto.ac.th">จักรพงษภูวนารถ</a>
                                <a href="http://www.uthen.rmutto.ac.th">อุเทนถวาย</a>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="myModal" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">ลืมรหัสผ่าน</h4>
                    </div>

                    <div class="modal-footer" style="text-align: center;">
                        <asp:TextBox ID="tbCitizenID" runat="server" CssClass="ps-textbox" MaxLength="13" onblur="validateEmail(this);" onkeypress="return isNumberKey(event)" placeholder="กรุณากรอกรหัสบัตรประชาชน" Width="300px"></asp:TextBox><br />
                        <asp:TextBox ID="tbEmail" runat="server" CssClass="ps-textbox" placeholder="กรุณากรอกอีเมล" Width="300px"></asp:TextBox><br />
                        <asp:LinkButton ID="lbuForget" runat="server" CssClass="btn btn-default" OnClick="lbuForget_Click">กู้คืนรหัสผ่าน</asp:LinkButton>
                    </div>
                </div>

            </div>
        </div>
    </form>
</body>
</html>
