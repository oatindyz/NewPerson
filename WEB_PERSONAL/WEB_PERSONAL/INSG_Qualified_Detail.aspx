<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="INSG_Qualified_Detail.aspx.cs" Inherits="WEB_PERSONAL.INSG_Qualififed_Detail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style type="text/css">
        .col1 {
            text-align: right;
        }

        .col2 {
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div>

        <div class="ps-header">
            <img src="Image/Small/medal.png" />ประวัติการรับเครื่องราชอิสริยาภรณ์
        </div>
        <div class="ps-div-title-red">ข้อมูลรายละเอียด</div>

        <div style="text-align: center;">
            <table class="ps-table-1" style="display: inline-block; text-align: left;" >
                <tr>
                    <td class="col1">รหัสประชาชน</td>
                    <td class="col2">
                        <asp:Label ID="lbCitizenID" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="col1">ชื่อ - สกุล</td>
                    <td class="col2">
                        <asp:Label ID="lblName" runat="server"></asp:Label>
                        <asp:Label ID="lblLastName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="col1">ประเภทบุคลากร</td>
                    <td class="col2">
                        <asp:Label ID="lblStafftype" runat="server" Width="100px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="col1">สังกัด/หน่วยงาน</td>
                    <td class="col2">
                        <asp:Label ID="lblUniversity" runat="server">มหาวิทยาลัยเทคโนโลยีราชมงคลตะวันออก </asp:Label>
                        <asp:Label ID="lblCampus" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="col1">ตำแหน่งงาน</td>
                    <td class="col2">
                        <asp:Label ID="lblPosition" runat="server" Width="100px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="col1">สถานะ</td>
                    <td class="col2">
                        <asp:Label ID="lblStatusPersonWork" runat="server" Width="100px"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="text-align: center; margin-top: 10px;">
            <asp:Table ID="Table1" runat="server" CssClass="ps-table-1" style="display: inline-block;"></asp:Table>
        </div>
        <div class="ps-separator"></div>


    </div>
</asp:Content>
