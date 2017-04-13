<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Boss.aspx.cs" Inherits="WEB_PERSONAL.Boss" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
        function DisableButton() {
            document.getElementById("<%=lbuAdd.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="ps-header">
            <img src="Image/Small/person2.png" />จัดการแต่งตั้งหัวหน้า
        </div>
        <div>
            <table>
                <tr>
                    <td></td>
                    <td>เลขบัตรประชาชน</td>
                    <td>
                        <asp:TextBox ID="tbCitizenID" runat="server" CssClass="ps-textbox" onkeypress="return isNumberKey(event)" onkeyup="keyup(this,event)" required="required" tabindex="1"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:RadioButton ID="rbAtikan" runat="server" GroupName="g1" Checked="true"/>
                    </td>
                    <td class="col1">อธิการบดี</td>
                    <td class="col2">

                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:RadioButton ID="rbCampus" runat="server" GroupName="g1" Checked="true"/>
                    </td>
                    <td class="col1">วิทยาเขต</td>
                    <td class="col2">
                        <asp:DropDownList ID="ddlCampus" runat="server" CssClass="ps-dropdown" OnSelectedIndexChanged="ddlCampus_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:RadioButton ID="rbFaculty" runat="server" GroupName="g1"/>
                    </td>
                    <td class="col1">สำนัก / สถาบัน / คณะ</td>
                    <td class="col2">
                        <asp:DropDownList ID="ddlFaculty" runat="server" CssClass="ps-dropdown" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:RadioButton ID="rbDivision" runat="server" GroupName="g1"/>
                    </td>
                    <td class="col1">กอง / สำนักงานเลขา / ภาควิชา</td>
                    <td class="col2">
                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="ps-dropdown" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </td>
                </tr>
                <tr id="trWorkDivision" runat="server">
                    <td>
                        <asp:RadioButton ID="rbWorkDivision" runat="server" GroupName="g1"/>
                    </td>
                    <td class="col1">งาน / ฝ่าย</td>
                    <td class="col2">
                        <asp:DropDownList ID="ddlWorkDivision" runat="server" CssClass="ps-dropdown"></asp:DropDownList>
                    </td>
                </tr>
                 <tr>
                    <td></td>
                    <td class="col1">รหัสโหนดสูง</td>
                    <td class="col2">
                        <asp:DropDownList ID="ddlHighNode" runat="server" CssClass="ps-dropdown"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td style="text-align: left;">
                        <asp:LinkButton ID="lbuAdd" runat="server" CssClass="ps-button" OnClick="lbuAdd_Click"><img src="Image/Small/add.png" class="icon_left"/>เพิ่ม</asp:LinkButton>
                        <asp:Label ID="lbResult" runat="server" Text="เพิ่มข้อมูลสำเร็จ" ForeColor="Green" style="margin-left: 10px; display: inline-block;" Visible="false"></asp:Label>
                    </td>

                </tr>
            </table>
        </div>
        <div class="ps-separator"></div>
        <div>
            <asp:Table ID="tbBoss" runat="server" CssClass="ps-table-1"></asp:Table>
        </div>
    </div>
    
</asp:Content>
