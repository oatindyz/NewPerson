<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Request.aspx.cs" Inherits="WEB_PERSONAL.Request" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
            <img src="Image/Icon/edit.png" />ยื่นคำร้องขอแก้ไขข้อมูลที่ถูกล็อค
            <span id="SpanBack" runat="server" visible="false" style="text-align: right; float: right;"><a href="ListRequest.aspx">
                <img src="Image/Small/back.png" />ย้อนกลับ</a></span>
        </div>
        <div id="notification" runat="server"></div>

        <div id="DataShow" runat="server" class="panel panel-default">
            <div class="panel-body">
                <div class="ps-box-ct10" style="text-align: center;">
                    <table class="ps-table-x16" style="display: inline-block; vertical-align: top; text-align: left;">
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
                                <asp:TextBox ID="tbEmail" runat="server" CssClass="form-control input-sm"></asp:TextBox>
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
                    <div class="ps-div-title-red">ทำการบันทึกข้อมูลคำร้องแก้ไขข้อมูลที่ถูกล็อคไว้สำเร็จ</div>
                    <div style="color: #808080; margin-top: 10px; text-align: center;">
                        ระบบได้ทำการบันทึกข้อมูลแล้ว กรุณารอเจ้าหน้าที่บุคลากรอนุมัติคำร้องขอ !
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