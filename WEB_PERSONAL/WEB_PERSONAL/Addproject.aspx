<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Addproject.aspx.cs" Inherits="WEB_PERSONAL.Addproject" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type = "text/javascript">
    function DisableButton() {
        document.getElementById("<%=btnAddProject.ClientID %>").disabled = true;
    }
    window.onbeforeunload = DisableButton;
    </script>

    <script>
        $(function () {
            $("#ContentPlaceHolder1_tbStartDate, #ContentPlaceHolder1_tbEndDate").datepicker($.datepicker.regional["th"]);
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="default_page_style">
        <div class="ps-header">
            <img src="Image/Small/add.png" />เพิ่มข้อมูลการพัฒนาบุคลากร
            <span style="text-align:right; float:right;"><a href="listproject.aspx">
            <img src="Image/Small/back.png" />ย้อนกลับ</a></span>
        </div>
        <div id="notification" runat="server"></div>

        <div id="Notsuccess" runat="server" class="panel panel-default">
            <div class="panel-body">
                <table style="width: 97%;">
                    <tr>
                        <td>
                            <div class="form-group">
                                <asp:Label ID="lbCategoryID" runat="server">ประเภทโครงการ<span class="ps-lb-red" />*</asp:Label>
                                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control input-sm ekknidRight" required="required" TabIndex="1"></asp:DropDownList>
                            </div>
                        </td>
                        <td>
                            <div class="form-group">
                                <asp:Label ID="lbCountry" runat="server">ประเภทการอบรม<span class="ps-lb-red" />*</asp:Label>
                                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control input-sm ekknidRight" required="required" TabIndex="1"></asp:DropDownList>
                            </div>
                        </td>
                        <td>
                            <div class="form-group">
                                <asp:Label ID="lbTypeProject" runat="server">รูปแบบประเภทการอบรม<span class="ps-lb-red" />*</asp:Label>
                                <asp:DropDownList ID="ddlSubCountry" runat="server" CssClass="form-control input-sm ekknidRight" required="required" TabIndex="1"></asp:DropDownList>
                            </div>
                        </td>
                        <td>
                            <div class="form-group">
                                <asp:Label ID="lbFile" runat="server">แนบไฟล์ .pdf (รูปภาพ,เอกสาร ประกอบการอบรม)<span class="ps-lb-red" />*</asp:Label>
                                <asp:FileUpload ID="FUdocument" runat="server" Width="250px" required="required" TabIndex="1"/>
                            </div>
                        </td>
                    </tr>
                </table>
                <div>
                    <asp:Label ID="lbProjectName" runat="server">ชื่อโครงการ<span class="ps-lb-red" />*</asp:Label>
                    <asp:TextBox ID="tbProjectName" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1"></asp:TextBox>
                </div>
                <div>
                    <asp:Label ID="lbAddressProject" runat="server">สถานที่จัดโครงการ<span class="ps-lb-red" />*</asp:Label>
                    <asp:TextBox ID="tbAddressProject" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1"></asp:TextBox>
                </div>
                <div>
                    <asp:Label ID="lbStartDateEndDate" runat="server">วันที่เริ่มโครงการ - วันที่สิ้นสุดโครงการ<span class="ps-lb-red" />*</asp:Label>
                    <div class="form-group date">
                        <asp:TextBox ID="tbStartDate" runat="server" CssClass="input-sm" Width="200px" Placeholder="วันที่เริ่มโครงการ" required="required" TabIndex="1"></asp:TextBox>
                        <asp:TextBox ID="tbEndDate" runat="server" CssClass="input-sm ekknidLeft" Width="200px" Placeholder="วันที่สิ้นสุดโครงการ" required="required" TabIndex="1"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="lbExpenses" runat="server">ค่าใช้จ่ายตลอดโครงการ<span class="ps-lb-red" />*</asp:Label>
                    <div class="form-group date">
                        <asp:TextBox ID="tbExpenses" runat="server" CssClass="form-control input-sm" onkeypress="return isNumberKey(event)" required="required" TabIndex="1"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="lbFunding" runat="server">แหล่งงบประมาณสนับสนุน</asp:Label>
                    <asp:TextBox ID="tbFunding" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="lbCertificate" runat="server">ประกาศนียบัตรที่ได้รับ(ถ้ามี)</asp:Label>
                    <asp:TextBox ID="tbCertificate" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                </div>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <div class="form-group">
                                <asp:Label ID="lbSummarizeProject" runat="server">สรุปผลโครงการ</asp:Label>
                                <asp:TextBox ID="tbSummarizeProject" runat="server" TextMode="MultiLine" CssClass="form-control input-sm" Height="50px" Width="95%" Placeholder="สรุปผลโครงการ"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lbResultTeaching" runat="server">การนำผลที่ได้รับจากโครงการด้านการเรียนการสอน</asp:Label>
                                <asp:TextBox ID="tbResultTeaching" runat="server" TextMode="MultiLine" CssClass="form-control input-sm" Height="50px" Width="95%" Placeholder="การนำผลที่ได้รับจากโครงการด้านการเรียนการสอน"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lbResultAcademic" runat="server">การนำผลที่ได้รับจากโครงการด้านการบริการวิชาการ</asp:Label>
                                <asp:TextBox ID="tbResultAcademic" runat="server" TextMode="MultiLine" CssClass="form-control input-sm" Height="50px" Width="95%" Placeholder="การนำผลที่ได้รับจากโครงการด้านการบริการวิชาการ"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lbDifficultyProject" runat="server">ปัญหาอุปสรรคในโครงการ</asp:Label>
                                <asp:TextBox ID="tbDifficultyProject" runat="server" TextMode="MultiLine" CssClass="form-control input-sm" Height="50px" Width="95%" Placeholder="ปัญหาอุปสรรคในโครงการ"></asp:TextBox>
                            </div>
                        </td>

                        <td>
                            <div class="form-group">
                                <asp:Label ID="lbResultProject" runat="server">ผลที่ได้รับจากโครงการ</asp:Label>
                                <asp:TextBox ID="tbResultProject" runat="server" TextMode="MultiLine" CssClass="form-control input-sm" Height="50px" Placeholder="ผลที่ได้รับจากโครงการ"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lbResultResearching" runat="server">การนำผลที่ได้รับจากโครงการด้านการวิจัย</asp:Label>
                                <asp:TextBox ID="tbResultResearching" runat="server" TextMode="MultiLine" CssClass="form-control input-sm" Height="50px" Placeholder="การนำผลที่ได้รับจากโครงการด้านการวิจัย"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lbResultOther" runat="server">การนำผลที่ได้รับจากโครงการด้านอื่นๆ</asp:Label>
                                <asp:TextBox ID="tbResultOther" runat="server" TextMode="MultiLine" CssClass="form-control input-sm" Height="50px" Placeholder="การนำผลที่ได้รับจากโครงการด้านอื่นๆ"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lbCounsel" runat="server">ข้อเสนอแนะอื่นๆ</asp:Label>
                                <asp:TextBox ID="tbCounsel" runat="server" TextMode="MultiLine" CssClass="form-control input-sm" Height="50px" Placeholder="ข้อเสนอแนะอื่นๆ"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                </table>

                <div style="text-align: center; margin-top: 10px;">
                    <asp:Button ID="btnAddProject" runat="server" CssClass="btn btn-success" OnClick="btnAddProject_Click" Text="บันทึก"></asp:Button>
                </div>

            </div>
        </div>

        <div id="success" runat="server">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="ps-div-title-red">ทำการบันทึกข้อมูลการพัฒนาบุคลากรสำเร็จ</div>
                    <div style="color: #808080; margin-top: 10px; text-align: center;">
                        ระบบได้ทำการบันทึกข้อมูลการพัฒนาบุคลากรเรียบร้อยแล้ว
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
