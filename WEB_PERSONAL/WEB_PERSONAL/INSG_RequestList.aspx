<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="INSG_RequestList.aspx.cs" Inherits="WEB_PERSONAL.INSG_RequestList" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(function () {
            $("#ContentPlaceHolder1_tbDateGet").datepicker($.datepicker.regional["th"]);
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hfIRID" runat="server" />
    <div>
        <div class="ps-header">
            <img src="Image/Small/medal.png" />รายชื่อการขอเครื่องราชฯ
        </div>
        <div id="notification" runat="server"></div>
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="View1" runat="server">
                <div style="text-align: center;">
                    <asp:Table ID="Table1" runat="server" CssClass="ps-table-1" style="display: inline-block;"></asp:Table>
                </div>
            </asp:View>
            <asp:View ID="View2" runat="server">
                <div style="text-align: center;">
                    <table class="ps-table-1" style="display: inline-block; text-align: left;">
                        <tr>
                            <th colspan="2" style="text-align: center;">แจ้งผลการขอเครืองราชฯ</th>
                        </tr>
                        <tr>
                            <td>ผลการขอเครื่องราช</td>
                            <td>
                                <asp:RadioButton ID="rbGet" runat="server" Text="ได้รับเครื่องราชฯ" Checked="true" GroupName="g1"/>
                                <asp:RadioButton ID="rbNotGet" runat="server" Text="ไม่ได้รับเครื่องราชฯ" GroupName="g1"/>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center; color: #808080;">กรณีได้รับเครื่องราชฯ</td>
                        </tr>
                        <tr>
                            <td>วันที่ได้รับ</td>
                            <td>
                                <asp:TextBox ID="tbDateGet" runat="server" Width="150px" CssClass="ps-textbox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>เอกสารอ้างอิง</td>
                            <td>
                                <asp:TextBox ID="tbRef" runat="server" Width="150px" CssClass="ps-textbox"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <div style="text-align: center; margin-top: 10px;">
                        <asp:LinkButton ID="lbBack" runat="server" CssClass="ps-button" OnClick="lbBack_Click"><img src="Image/Small/back.png" class="icon_left"/>กลับ</asp:LinkButton>
                        <asp:LinkButton ID="lbuSave" runat="server" CssClass="ps-button" OnClick="lbuSave_Click"><img src="Image/Small/save.png" class="icon_left"/>บันทึก</asp:LinkButton>
                    </div>
                </div>
           </asp:View>
            <asp:View ID="View3" runat="server">
                <div>
                    <div class="ps-div-title-red">แจ้งผลการขอเครื่องราชฯสำเร็จ</div>
                    <div style="text-align: center; margin-top: 10px;">
                        <a href="Default.aspx" class="ps-button"><img src="Image/Small/home3.png" class="icon_left"/>กลับหน้าหลัก</a>
                    </div>
                </div>
            </asp:View>
        </asp:MultiView>
        <div class="ps-separator"></div>
    </div>
</asp:Content>
