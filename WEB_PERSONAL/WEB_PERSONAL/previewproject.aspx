<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="previewproject.aspx.cs" Inherits="WEB_PERSONAL.previewproject" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="default_page_style">
        <div class="ps-header">
            <img src="Image/Small/search.png" />ดูข้อมูลการพัฒนาบุคลากร
            <span style="text-align:right; float:right;"><a href="listproject.aspx">
            <img src="Image/Small/back.png" />ย้อนกลับ</a></span>
        </div>
        <div id="notification" runat="server"></div>

        <div id="Notsuccess" runat="server" class="panel panel-default">
            <div class="panel-body">
                <table class="table table-striped table-bordered table-hover ps-table-1">
                    <tr>
                        <td class="col1" style="width: 400px;">ประเภทโครงการ</td>
                        <td>
                            <asp:Label ID="lbCategory" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">ประเภทการอบรม</td>
                        <td>
                            <asp:Label ID="lbCountry" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">รูปแบบประเภทการอบรม</td>
                        <td>
                            <asp:Label ID="lbSubCountry" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">ชื่อโครงการ :</td>
                        <td class="col2">
                            <asp:Label ID="lbProjectName" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="col1">สถานที่จัดโครงการ :</td>
                        <td class="col2">
                            <asp:Label ID="lbAddressProject" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="col1">วันที่เริ่มโครงการ - วันที่สิ้นสุดโครงการ :</td>
                        <td class="col2">
                            <asp:Label ID="lbStartDate" runat="server"></asp:Label>
                            <asp:Label ID="lb1" runat="server" CssClass="ekknidLeft ekknidRight"> - </asp:Label>
                            <asp:Label ID="lbEndDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">ค่าใช้จ่ายตลอดโครงการ :</td>
                        <td class="col2">
                            <asp:Label ID="lbExpenses" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="col1">แหล่งงบประมาณสนับสนุน :</td>
                        <td class="col2">
                            <asp:Label ID="lbFunding" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="col1">ประกาศนียบัตรที่ได้รับ :</td>
                        <td class="col2">
                            <asp:Label ID="lbCertificate" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="col1">สรุปผลโครงการ :</td>
                        <td class="col2">
                            <asp:Label ID="lbSummarizeProject" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="col1">ผลที่ได้รับจากโครงการ :</td>
                        <td class="col2">
                            <asp:Label ID="lbResultTeaching" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="col1">การนำผลที่ได้รับจากโครงการด้านการเรียนการสอน :</td>
                        <td class="col2">
                            <asp:Label ID="lbResultAcademic" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="col1">การนำผลที่ได้รับจากโครงการด้านการวิจัย :</td>
                        <td class="col2">
                            <asp:Label ID="lbDifficultyProject" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="col1">การนำผลที่ได้รับจากโครงการด้านบริการวิชาการ :</td>
                        <td class="col2">
                            <asp:Label ID="lbResultProject" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="col1">การนำผลที่ได้รับจากโครงการด้านอื่นๆ :</td>
                        <td class="col2">
                            <asp:Label ID="lbResultResearching" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="col1">ปัญหาอุปสรรคในโครงการ :</td>
                        <td class="col2">
                            <asp:Label ID="lbResultOther" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="col1">ข้อเสนอแนะอื่นๆ :</td>
                        <td class="col2">
                            <asp:Label ID="lbCounsel" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="col1">ไฟล์แนบ (รูปภาพ,เอกสาร ประกอบการอบรม) :</td>
                        <td class="col2">
                            <div class="c1" id="file_pdf" runat="server"></div>
                        </td>
                    </tr>
                </table>
            </div>

            <div style="text-align: center; margin-top: 20px; margin-bottom: 20px">
                <asp:LinkButton ID="lbuEdit" runat="server" class="btn btn-warning ekknidRight" OnClick="lbuEdit_Click">แก้ไข</asp:LinkButton>
                <asp:LinkButton ID="lbuDelete" runat="server" class="btn btn-danger" OnClientClick="javascript:if(!confirm('คุณต้องการที่จะลบใช่หรือไม่'))return false;" OnClick="lbuDelete_Click">ลบ</asp:LinkButton>
            </div>
        </div>
    </div>

    <div id="delete" runat="server">
        <div class="panel panel-default">
            <div class="panel-body">
                <div class="ps-div-title-red">ทำการลบข้อมูลการพัฒนาบุคลากรสำเร็จ</div>
                <div style="color: #808080; margin-top: 10px; text-align: center;">
                    ระบบได้ทำการลบข้อมูลการพัฒนาบุคลากรเรียบร้อย
                </div>
                <div style="text-align: center; margin-top: 10px;">
                    <a href="listproject.aspx" class="ps-button btn btn-primary">ย้อนกลับ</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>