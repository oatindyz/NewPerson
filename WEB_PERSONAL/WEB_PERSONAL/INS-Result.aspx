<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="INS-Result.aspx.cs" Inherits="WEB_PERSONAL.INS_Result" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .TMZ {
            font-family: tahoma;
            text-align: left;
            color: #9999ff;
            font-weight: 900;
        }

        .ui-datepicker {
            font-family: tahoma;
            text-align: center;
            color: dodgerblue;
        }

        fieldset {
            padding: 0.2em 0.5em;
            border: 3px solid #99e6ff;
            color: black;
            font-size: 90%;
            text-align: left;
        }

        legend {
            padding: 0.2em 0.5em;
            border: 3px solid #99e6ff;
            color: #99e6ff;
            font-size: 120%;
            text-align: left;
        }

        .tb5 {
            background-repeat: repeat-x;
            border: 1px solid #ff9900;
            width: 200px;
            color: #333333;
            padding: 3px;
            margin-right: 4px;
            margin-bottom: 8px;
            font-family: tahoma, arial, sans-serif;
            border-radius: 10px;
            resize: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <fieldset>
                <legend class="TMZ">แจ้งผลการขอเครื่องราช</legend>
                <asp:GridView ID="GridView1" runat="server" CssClass="ps-gridview"></asp:GridView>

            </fieldset>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <fieldset>
                <legend class="TMZ">รายละเอียดผู้ขอเครืองราชอิสริยาภรณ์</legend>
                <table>
                    <tr>
                        <td class="col1">ชื่อ</td>
                        <td class="col2">
                            <asp:TextBox ID="tbName" runat="server" CssClass="tb5"></asp:TextBox></td>
                        <td class="col1">นามสกุล</td>
                        <td class="col2">
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="tb5"></asp:TextBox></td>
                        <td class="col1">ชั้นเครื่องราชอิสริยาภรณ์ที่ขอ</td>
                        <td class="col2">
                            <asp:TextBox ID="TextBox2" runat="server" CssClass="tb5"></asp:TextBox></td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td class="col2">
                            <asp:LinkButton ID="lbuV1Back" runat="server" CssClass="ps-button" OnClick="lbuV1Back_Click">ย้อนกลับ</asp:LinkButton></td>
                        <td class="col2">
                            <asp:LinkButton ID="lbuV2Next" runat="server" CssClass="ps-button">ถัดไป</asp:LinkButton></td>
                    </tr>
                </table>
            </fieldset>
        </asp:View>
    </asp:MultiView>
</asp:Content>
