<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="INSG_Request.aspx.cs" Inherits="WEB_PERSONAL.INSG_Request" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .col1 {
            text-align: right;
        }

        .col2 {
            text-align: left;
        }

        .textred {
            color: red;
        }

        .auto-style1 {
            height: 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="default_page_style">
        <div class="ps-header">
            <img src="Image/Small/medal.png" />ขอเครื่องราชฯ
        </div>
        <div id="notification" runat="server"></div>
        <table style="width: 100%;">
            <tr>
                <td style="width: 350px;">
                    <div id="ShowInsig" runat="server"></div>
                </td>
                <td style="text-align: left;">
                    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                        <asp:View ID="View0" runat="server">
                            <div style="text-align: center; margin-top: 10px;">
                                <a href="Default.aspx" class="ps-button">
                                    <img src="Image/Small/home3.png" class="icon_left" />กลับหน้าหลัก</a>
                            </div>
                        </asp:View>
                        <asp:View ID="View1" runat="server">
                            <div style="display: inline-block;">
                                <div class="ps-div-title-red">ข้อมูลการขอเครื่องราชอิสริยาภรณ์</div>
                                <div style="text-align: center;">
                                    <div style="margin-bottom: 20px;">
                                        <table class="ps-table-x16" style="display: inline-block; margin-right: 20px; vertical-align: top;">

                                            <tr>
                                                <td class="col1">คำนำหน้าชื่อ</td>
                                                <td class="col2">
                                                    <asp:Label ID="lbTitleName" runat="server" Text="TitleName"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="col1">ชื่อ</td>
                                                <td class="col2">
                                                    <asp:Label ID="lbName" runat="server" Text="Name"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="col1">นามสกุล</td>
                                                <td class="col2">
                                                    <asp:Label ID="lbLastName" runat="server" Text="LastName"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="col1">เพศ</td>
                                                <td class="col2">
                                                    <asp:Label ID="lbGender" runat="server" Text="Gender"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="col1">วัน/เดือน/ปีเกิด</td>
                                                <td class="col2">
                                                    <asp:Label ID="lbBirthDate" runat="server" Text="BirthDate"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="col1">วัน/เดือน/ปีที่เริ่มรับราชการ</td>
                                                <td class="col2">
                                                    <asp:Label ID="lbDateInwork" runat="server" Text="DateInWork"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="col1">ตำแหน่งและระดับที่เริ่มรับราชการ</td>
                                                <td class="col2">
                                                    <asp:Label ID="lbFirstPosition" runat="server" Text="FirstPosition"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="col1">ชื่อตำแหน่งปัจจุบัน</td>
                                                <td class="col2">
                                                    <asp:Label ID="lbPositionCurrent" runat="server" Text="PositionCurrent"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="col1">ประเภท</td>
                                                <td class="col2">
                                                    <asp:Label ID="lbType" runat="server" Text="Type"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="col1">ระดับ</td>
                                                <td class="col2">
                                                    <asp:Label ID="lbDegree" runat="server" Text="Degree"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="col1">เงินเดือนป้จจุบัน</td>
                                                <td class="col2">
                                                    <asp:Label ID="lbSalaryCurrent" runat="server" Text="SalaryCurrent"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="col1">เงินประจำตำแหน่ง</td>
                                                <td class="col2">
                                                    <asp:Label ID="lbPositionSalary" runat="server" Text="PostionSalary"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="col1">สามารถเลื่อนได้ถึง</td> 
                                                <td class="col2">
                                                    <asp:Label ID="lbMinMaxInsig" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div style="text-align: center; margin-top: 10px;">
                                        <table class="ps-insig-show-table" style="display: inline-block; text-align: center;">
                                            <tr>
                                                <th class="auto-style1">
                                                    <img src="Image/Small/medal.png" class="icon_left" />เครื่องราชฯ ปัจจุบัน</th>
                                                <th class="auto-style1">
                                                    <img src="Image/Small/medal.png" class="icon_left" />เครื่องราชฯ ชั้นถัดไป</th>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <img src="~/Image/no_image.png" id="imgOldInsig" runat="server" style="display: inline-block; max-width: 100px; max-height: 100px; object-fit: contain;" />

                                                </td>
                                                <td>
                                                    <img src="~/Image/no_image.png" id="imgNewInsig" runat="server" style="display: inline-block; max-width: 100px; max-height: 100px; object-fit: contain;" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div style="margin-top: 5px;">
                                                        <asp:Label ID="lbOldInsigName" runat="server" Text="ไม่มีลำดับชั้นเครื่องราชฯล่าสุด"></asp:Label>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div style="margin-top: 5px;">
                                                        <asp:Label ID="lbNewInsigName" runat="server" Text="ไม่สามารถขอเครื่องราชฯชั้นถัดไป"></asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>

                                    <div style="margin: 20px 0; text-align: center;">
                                         <asp:Label ID="lbTest" runat="server" style="color: rgb(237, 28, 36); font-size: 16px;"></asp:Label>
                                    </div>

                                    <div style="margin: 20px 0; text-align: center;">
                                         <asp:Label ID="lbRetiring" runat="server" style="color: rgb(0, 162, 232); font-size: 16px;" Visible="false">* ช่วงเวลาที่เกษียณเพิ่มเครื่องราชหนึ่งลำดับ</asp:Label>
                                         <asp:Label ID="lbRetired" runat="server" style="color: rgb(237, 28, 36); font-size: 16px;" Visible="false">* ไม่สามารถขอเครื่องราชฯได้เนื่องจากเกินช่วงเวลาที่เกษียณแล้ว</asp:Label>
                                    </div>

                                    <div style="margin: 20px 0; text-align: center;">
                                         <asp:Label ID="lbInsigRequest" runat="server" style="color: rgb(237, 28, 36); font-size: 16px;" Visible="false">* อยู่ระหว่างดำเนินการ</asp:Label>
                                    </div>

                                    <div style="margin: 20px 0;">
                                        <asp:Table ID="TableCondition" runat="server" CssClass="ps-table-x16" Style="margin: 0 auto;"></asp:Table>
                                    </div>

                                </div>
                                <div style="text-align: center; margin-top: 10px;" class="center1">
                                    <asp:LinkButton ID="lbuSubmitView2" runat="server" CssClass="ps-button" OnClick="lbuSubmitView2_Click"><img src="Image/Small/save.png" class="icon_left"/>ขอเครื่องราชอิสริยาภรณ์</asp:LinkButton>
                                </div>
                            </div>
                        </asp:View>
                        <asp:View ID="View2" runat="server"> 
                            <div>
                                <div class="ps-div-title-red">ทำการขอเครื่องราชฯเรียบร้อย</div>
                                <div style="color: #808080; margin-top: 10px; text-align: center;">
                                    ระบบจะมีการแจ้งเตือนอีกครั้งเมื่อเจ้าหน้าที่ดำเนินเรื่องการขอเครื่องราชฯสำเร็จ
                                </div>
                                <div style="text-align: center; margin-top: 10px;">
                                    <a href="Default.aspx" class="ps-button">
                                        <img src="Image/Small/home3.png" class="icon_left" />กลับหน้าหลัก</a>
                                </div>
                            </div>
                        </asp:View>
                    </asp:MultiView>
                </td>
            </tr>
        </table>
        <div class="ps-separator"></div>
    </div>
</asp:Content>
