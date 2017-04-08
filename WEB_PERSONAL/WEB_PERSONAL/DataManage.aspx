<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="DataManage.aspx.cs" Inherits="WEB_PERSONAL.DataManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- for Menu List -->
    <script src="bower_components/jquery/dist/jquery.min.js"></script>
    <script src="bower_components/metisMenu/dist/metisMenu.min.js"></script>
    <script src="bower_components/datatables/media/js/jquery.dataTables.min.js"></script>
    <script src="bower_components/datatables-plugins/integration/bootstrap/3/dataTables.bootstrap.min.js"></script>
    <script src="dist/js/sb-admin-2.js"></script>
    <!-- for Menu List -->
    <script>
        $(document).ready(function () {
            $('#dataTables-Title,#dataTables-Gender,#dataTables-Province,#dataTables-Amphur,#dataTables-Tambon,#dataTables-Nation,#dataTables-Campus,#dataTables-Faculty,#dataTables-Division,#dataTables-WorkDivision,#dataTables-Stafftype,#dataTables-TimeContact,#dataTables-Budget,#dataTables-SubStafftype,#dataTables-AdminPosition,#dataTables-Position,#dataTables-WorkPos,#dataTables-TeachISCED,#dataTables-GradLev,#dataTables-GradProg,#dataTables-Deform,#dataTables-Religion,#dataTables-MovementType').DataTable({
                responsive: true
            });
        });
    </script>
    
    

    <style>
        .c1 {
            padding: 20px 0;
        }
        .c1 a {
            display: block;
            padding: 3px 20px;
            margin-bottom: 1px;
            color: #000000;
            border-radius: 2px;
        }
        .c1 a:hover {
            color: #ffffff;
            background-color: rgb(0, 142, 212);
        }
    </style>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderLeft" runat="server">
    <div class="c1">
        
            <asp:LinkButton ID="lbuMenuTitle" runat="server" OnClick="lbuMenuTitle_Click"><img src="Image/Small/wrench.png" class="icon_left"/> คำนำหน้า</asp:LinkButton>
        
            <asp:LinkButton ID="lbuMenuGender" runat="server" OnClick="lbuMenuGender_Click"><img src="Image/Small/wrench.png" class="icon_left"/> เพศ</asp:LinkButton>
        
            <asp:LinkButton ID="lbuMenuProvince" runat="server" OnClick="lbuMenuProvince_Click"><img src="Image/Small/wrench.png" class="icon_left"/> จังหวัด</asp:LinkButton>
        
            <asp:LinkButton ID="lbuMenuAmphur" runat="server" OnClick="lbuMenuAmphur_Click"><img src="Image/Small/wrench.png" class="icon_left"/> เขต/อำเภอ</asp:LinkButton>
        
            <asp:LinkButton ID="lbuMenuTambon" runat="server" OnClick="lbuMenuTambon_Click"><img src="Image/Small/wrench.png" class="icon_left"/> แขวง/ตำบล</asp:LinkButton>
        
            <asp:LinkButton ID="lbuMenuNation" runat="server" OnClick="lbuMenuNation_Click"><img src="Image/Small/wrench.png" class="icon_left"/> สัญชาติ</asp:LinkButton>
        
            <asp:LinkButton ID="lbuMenuCampus" runat="server" OnClick="lbuMenuCampus_Click"><img src="Image/Small/wrench.png" class="icon_left"/> วิทยาเขต</asp:LinkButton>
        
            <asp:LinkButton ID="lbuMenuFaculty" runat="server" OnClick="lbuMenuFaculty_Click"><img src="Image/Small/wrench.png" class="icon_left"/> สำนัก/สถาบัน/คณะ</asp:LinkButton>
       
            <asp:LinkButton ID="lbuMenuDivision" runat="server" OnClick="lbuMenuDivision_Click"><img src="Image/Small/wrench.png" class="icon_left"/> กอง/สำนักงานเลขา/ภาควิชา</asp:LinkButton>
        
            <asp:LinkButton ID="lbuMenuWorkDivision" runat="server" OnClick="lbuMenuWorkDivision_Click"><img src="Image/Small/wrench.png" class="icon_left"/> งาน/ฝ่าย</asp:LinkButton>

        
            <asp:LinkButton ID="lbuMenuStafftype" runat="server" OnClick="lbuMenuStafftype_Click"><img src="Image/Small/wrench.png" class="icon_left"/> ประเภทบุคลากร</asp:LinkButton>
        
            <asp:LinkButton ID="lbuMenuTimeContact" runat="server" OnClick="lbuMenuTimeContact_Click"><img src="Image/Small/wrench.png" class="icon_left"/> ระยะเวลาจ้าง</asp:LinkButton>
        
            <asp:LinkButton ID="lbuMenuBudget" runat="server" OnClick="lbuMenuBudget_Click"><img src="Image/Small/wrench.png" class="icon_left"/> ประเภทเงินจ้าง</asp:LinkButton>
        
            <asp:LinkButton ID="lbuMenuSubStafftype" runat="server" OnClick="lbuMenuSubStafftype_Click"><img src="Image/Small/wrench.png" class="icon_left"/> ประเภทบุคลากรย่อย</asp:LinkButton>
        
            <asp:LinkButton ID="lbuMenuAdminPosition" runat="server" OnClick="lbuMenuAdminPosition_Click"><img src="Image/Small/wrench.png" class="icon_left"/> ตำแหน่งทางบริหาร</asp:LinkButton>
        
            <asp:LinkButton ID="lbuMenuPosition" runat="server" OnClick="lbuMenuPosition_Click"><img src="Image/Small/wrench.png" class="icon_left"/> ระดับตำแหน่ง</asp:LinkButton>
        
            <asp:LinkButton ID="lbuMenuWorkPos" runat="server" OnClick="lbuMenuWorkPos_Click"><img src="Image/Small/wrench.png" class="icon_left"/> ตำแหน่งในสายงาน</asp:LinkButton>
        
            <asp:LinkButton ID="lbuMenuTeachISCED" runat="server" OnClick="lbuMenuTeachISCED_Click"><img src="Image/Small/wrench.png" class="icon_left"/> กลุ่มสาขาวิชา</asp:LinkButton>
        
            <asp:LinkButton ID="lbuMenuGradLev" runat="server" OnClick="lbuMenuGradLev_Click"><img src="Image/Small/wrench.png" class="icon_left"/> ระดับการศึกษา</asp:LinkButton>
        
            <asp:LinkButton ID="lbuMenuGradProg" runat="server" OnClick="lbuMenuGradProg_Click"><img src="Image/Small/wrench.png" class="icon_left"/> สาขาวิชา</asp:LinkButton>
        
            <asp:LinkButton ID="lbuMenuDeform" runat="server" OnClick="lbuMenuDeform_Click"><img src="Image/Small/wrench.png" class="icon_left"/> ความพิการ</asp:LinkButton>
        
            <asp:LinkButton ID="lbuMenuReligion" runat="server" OnClick="lbuMenuReligion_Click"><img src="Image/Small/wrench.png" class="icon_left"/> ศาสนา</asp:LinkButton>
        
            <asp:LinkButton ID="lbuMenuMovementType" runat="server" OnClick="lbuMenuMovementType_Click"><img src="Image/Small/wrench.png" class="icon_left"/> ประเภทการดำรงตำแหน่งปัจจุบัน</asp:LinkButton>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div>
        <asp:Panel ID="Panel1" runat="server" CssClass="divpan" visible="false">
            <div id="divheader1" runat="server" class="ps-header">
                <img src="Image/Small/wrench.png" /><asp:Label ID="Label1" runat="server" Text="คำนำหน้าชื่อ"></asp:Label></div>
            <div id="divInsertTitle" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/Add.png" />เพิ่มข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>รหัสคำนำหน้าชื่อ<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertIdTitle" runat="server" CssClass="form-control input-sm" onkeypress="return isNumberKey(event)" MaxLength="4" required="required" tabindex="1"/>
                        </td>
                        <td>ชื่อคำนำหน้า<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertNameTitle" runat="server" MaxLength="255" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>จัดการข้อมูล:<br />
                            <asp:Button ID="btnInsertTitle" runat="server" CssClass="btn btn-primary ekknidRight" OnClick="btnInsertTitle_Click" Text="เพิ่มข้อมูล" />
                            <asp:Button ID="btnUpdateTitle" runat="server" CssClass="btn btn-info ekknidRight" OnClick="btnUpdateTitle_Click" Text="อัพเดทข้อมูล" />
                            <asp:LinkButton ID="lbuClearTitle" runat="server" CssClass="btn btn-success" OnClick="lbuClearTitle_Click" Text="ล้างข้อมูล" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divLoadTitle" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/list.png" />ข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover" id="dataTables-Title">
                    <thead>
                        <tr>
                            <th>ลำดับที่</th>
                            <th>รหัสคำนำหน้าชื่อ</th>
                            <th>ชื่อคำนำหน้า</th>
                            <th>จัดการข้อมูล</th>
                        </tr>
                    </thead>
                    <asp:Repeater ID="myRepeaterTitle" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex +1 %></td>
                                <td>
                                    <asp:Label ID="lbTitleID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TITLE_ID") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbTitleName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TITLE_NAME_TH") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkEditTitle" Text="แก้ไข" runat="server" CssClass="btn btn-warning" OnClick="OnEditTitle" />
                                    <asp:LinkButton ID="lnkDeleteTitle" Text="ลบ" runat="server" CssClass="btn btn-danger" OnClick="OnDeleteTitle" OnClientClick="return confirm('คุณต้องการที่จะลบใช่หรือไม่?');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:Panel>

        <asp:Panel ID="Panel2" runat="server" CssClass="divpan" visible="false">
            <div id="divheader2" runat="server" class="ps-header">
                <img src="Image/Small/wrench.png" /><asp:Label ID="Label2" runat="server" Text="เพศ"></asp:Label></div>
            <div id="divInsertGender" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/Add.png" />เพิ่มข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>รหัสเพศ<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertIdGender" runat="server" onkeypress="return isNumberKey(event)" MaxLength="4" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>ชื่อเพศ<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertNameGender" runat="server" MaxLength="255" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>จัดการข้อมูล:<br />
                            <asp:Button ID="btnInsertGender" runat="server" CssClass="btn btn-primary ekknidRight" OnClick="btnInsertGender_Click" Text="เพิ่มข้อมูล" />
                            <asp:Button ID="btnUpdateGender" runat="server" CssClass="btn btn-info ekknidRight" OnClick="btnUpdateGender_Click" Text="อัพเดทข้อมูล" />
                            <asp:LinkButton ID="lbuClearGender" runat="server" CssClass="btn btn-success" OnClick="lbuClearGender_Click" Text="ล้างข้อมูล" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divLoadGender" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/list.png" />ข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover" id="dataTables-Gender">
                    <thead>
                        <tr>
                            <th>ลำดับที่</th>
                            <th>รหัสเพศ</th>
                            <th>ชื่อเพศ</th>
                            <th>จัดการข้อมูล</th>
                        </tr>
                    </thead>
                    <asp:Repeater ID="myRepeaterGender" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex +1 %></td>
                                <td>
                                    <asp:Label ID="lbGenderID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GENDER_ID") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbGenderName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GENDER_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkEditGender" Text="แก้ไข" runat="server" CssClass="btn btn-warning" OnClick="OnEditGender" />
                                    <asp:LinkButton ID="lnkDeleteGender" Text="ลบ" runat="server" CssClass="btn btn-danger" OnClick="OnDeleteGender" OnClientClick="return confirm('คุณต้องการที่จะลบใช่หรือไม่?');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:Panel>

        <asp:Panel ID="Panel3" runat="server" CssClass="divpan" visible="false">
            <div id="divheader3" runat="server" class="ps-header">
                <img src="Image/Small/wrench.png" /><asp:Label ID="Label3" runat="server" Text="จังหวัด"></asp:Label></div>
            <div id="divInsertProvince" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/Add.png" />เพิ่มข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>รหัสจังหวัด<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertIdProvince" runat="server" onkeypress="return isNumberKey(event)" MaxLength="4" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>ชื่อจังหวัด<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertNameProvince" runat="server" MaxLength="255" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>จัดการข้อมูล:<br />
                            <asp:Button ID="btnInsertProvince" runat="server" CssClass="btn btn-primary ekknidRight" OnClick="btnInsertProvince_Click" Text="เพิ่มข้อมูล" />
                            <asp:Button ID="btnUpdateProvince" runat="server" CssClass="btn btn-info ekknidRight" OnClick="btnUpdateProvince_Click" Text="อัพเดทข้อมูล" />
                            <asp:LinkButton ID="lbuClearProvince" runat="server" CssClass="btn btn-success" OnClick="lbuClearProvince_Click" Text="ล้างข้อมูล" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divLoadProvince" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/list.png" />ข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover" id="dataTables-Province">
                    <thead>
                        <tr>
                            <th>ลำดับที่</th>
                            <th>รหัสจังหวัด</th>
                            <th>ชื่อจังหวัด</th>
                            <th>จัดการข้อมูล</th>
                        </tr>
                    </thead>
                    <asp:Repeater ID="myRepeaterProvince" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex +1 %></td>
                                <td>
                                    <asp:Label ID="lbProvinceID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PROVINCE_ID") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbProvinceName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PROVINCE_TH") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkEditProvince" Text="แก้ไข" runat="server" CssClass="btn btn-warning" OnClick="OnEditProvince" />
                                    <asp:LinkButton ID="lnkDeleteProvince" Text="ลบ" runat="server" CssClass="btn btn-danger" OnClick="OnDeleteProvince" OnClientClick="return confirm('คุณต้องการที่จะลบใช่หรือไม่?');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:Panel>

        <asp:Panel ID="Panel4" runat="server" CssClass="divpan" visible="false">
            <div id="divheader4" runat="server" class="ps-header">
                <img src="Image/Small/wrench.png" /><asp:Label ID="Label4" runat="server" Text="เขต/อำเภอ"></asp:Label></div>
            <div id="divInsertAmphur" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/Add.png" />เพิ่มข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>รหัสเขต/อำเภอ<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertIdAmphur" runat="server" onkeypress="return isNumberKey(event)" MaxLength="4" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>ชื่อเขต/อำเภอ<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertNameAmphur" runat="server" MaxLength="255" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>อยู่ในจังหวัด<span class="ps-lb-red" />*<br />
                            <asp:DropDownList ID="ddlProvinceInAmphur" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>จัดการข้อมูล:<br />
                            <asp:Button ID="btnInsertAmphur" runat="server" CssClass="btn btn-primary ekknidRight" OnClick="btnInsertAmphur_Click" Text="เพิ่มข้อมูล" />
                            <asp:Button ID="btnUpdateAmphur" runat="server" CssClass="btn btn-info ekknidRight" OnClick="btnUpdateAmphur_Click" Text="อัพเดทข้อมูล" />
                            <asp:LinkButton ID="lbuClearAmphur" runat="server" CssClass="btn btn-success" OnClick="lbuClearAmphur_Click" Text="ล้างข้อมูล" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divLoadAmphur" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/list.png" />ข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover" id="dataTables-Amphur">
                    <thead>
                        <tr>
                            <th>ลำดับที่</th>
                            <th>รหัสเขต/อำเภอ</th>
                            <th>ชื่อเขต/อำเภอ</th>
                            <th>อยู่ในจังหวัด</th>
                            <th>จัดการข้อมูล</th>
                        </tr>
                    </thead>
                    <asp:Repeater ID="myRepeaterAmphur" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex +1 %></td>
                                <td>
                                    <asp:Label ID="lbAmphurID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AMPHUR_ID") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbAmphurName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AMPHUR_TH") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:HiddenField ID="HFProvinceInAmphur" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "PROVINCE_ID") %>'/>
                                    <asp:Label ID="lbProvinceName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PROVINCE_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkEditAmphur" Text="แก้ไข" runat="server" CssClass="btn btn-warning" OnClick="OnEditAmphur" />
                                    <asp:LinkButton ID="lnkDeleteAmphur" Text="ลบ" runat="server" CssClass="btn btn-danger" OnClick="OnDeleteAmphur" OnClientClick="return confirm('คุณต้องการที่จะลบใช่หรือไม่?');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:Panel>

        <asp:Panel ID="Panel5" runat="server" CssClass="divpan" visible="false">
            <div id="divheader5" runat="server" class="ps-header">
                <img src="Image/Small/wrench.png" /><asp:Label ID="Label5" runat="server" Text="แขวง/ตำบล"></asp:Label></div>
            <div id="divInsertTambon" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/Add.png" />เพิ่มข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>รหัสแขวง/ตำบล<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertIdTambon" runat="server" onkeypress="return isNumberKey(event)" MaxLength="4" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>ชื่อแขวง/ตำบล<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertNameTambon" runat="server" MaxLength="255" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>อยู่ในจังหวัด<span class="ps-lb-red" />*<br />
                            <asp:DropDownList ID="ddlProvinceInTambon" runat="server" OnSelectedIndexChanged="ddlProvinceInTambon_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>อยู่ในอำเภอ<span class="ps-lb-red" />*<br />
                            <asp:DropDownList ID="ddlAmphurInTambon" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>จัดการข้อมูล:<br />
                            <asp:Button ID="btnInsertTambon" runat="server" CssClass="btn btn-primary ekknidRight" OnClick="btnInsertTambon_Click" Text="เพิ่มข้อมูล" />
                            <asp:Button ID="btnUpdateTambon" runat="server" CssClass="btn btn-info ekknidRight" OnClick="btnUpdateTambon_Click" Text="อัพเดทข้อมูล" />
                            <asp:LinkButton ID="lbuClearTambon" runat="server" CssClass="btn btn-success" OnClick="lbuClearTambon_Click" Text="ล้างข้อมูล" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divLoadTambon" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/list.png" />ข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover" id="dataTables-Tambon">
                    <thead>
                        <tr>
                            <th>ลำดับที่</th>
                            <th>รหัสแขวง/ตำบล</th>
                            <th>ชื่อแขวง/ตำบล</th>
                            <th>อยู่ในจังหวัด</th>
                            <th>อยู่ในอำเภอ</th>
                            <th>จัดการข้อมูล</th>
                        </tr>
                    </thead>
                    <asp:Repeater ID="myRepeaterTambon" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex +1 %></td>
                                <td>
                                    <asp:Label ID="lbTambonID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DISTRICT_ID") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbTambonName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DISTRICT_TH") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:HiddenField ID="HFProvinceInTambon" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "PROVINCE_ID") %>'/>
                                    <asp:Label ID="lbProvinceName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PROVINCE_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:HiddenField ID="HFAmphurInTambon" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "AMPHUR_ID") %>'/>
                                    <asp:Label ID="lbAmphurName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AMPHUR_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkEditTambon" Text="แก้ไข" runat="server" CssClass="btn btn-warning" OnClick="OnEditTambon" />
                                    <asp:LinkButton ID="lnkDeleteTambon" Text="ลบ" runat="server" CssClass="btn btn-danger" OnClick="OnDeleteTambon" OnClientClick="return confirm('คุณต้องการที่จะลบใช่หรือไม่?');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:Panel>

        <asp:Panel ID="Panel6" runat="server" CssClass="divpan" visible="false">
            <div id="divheader6" runat="server" class="ps-header">
                <img src="Image/Small/wrench.png" /><asp:Label ID="Label6" runat="server" Text="สัญชาติ/ประเทศ"></asp:Label></div>
            <div id="divInsertNation" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/Add.png" />เพิ่มข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>รหัสสัญชาติ/ประเทศ<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertIdNation" runat="server" onkeypress="return isNumberKey(event)" MaxLength="4" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>ตัวย่อชื่อสัญชาติ/ประเทศ<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertNameNationShort" runat="server" onkeypress="return isLettersOnly(event)" MaxLength="2" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>ชื่อสัญชาติ/ประเทศ<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertNameNationFull" runat="server" MaxLength="255" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>จัดการข้อมูล:<br />
                            <asp:Button ID="btnInsertNation" runat="server" CssClass="btn btn-primary ekknidRight" OnClick="btnInsertNation_Click" Text="เพิ่มข้อมูล" />
                            <asp:Button ID="btnUpdateNation" runat="server" CssClass="btn btn-info ekknidRight" OnClick="btnUpdateNation_Click" Text="อัพเดทข้อมูล" />
                            <asp:LinkButton ID="lbuClearNation" runat="server" CssClass="btn btn-success" OnClick="lbuClearNation_Click" Text="ล้างข้อมูล" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divLoadNation" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/list.png" />ข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover" id="dataTables-Nation">
                    <thead>
                        <tr>
                            <th>ลำดับที่</th>
                            <th>รหัสสัญชาติ/ประเทศ</th>
                            <th>ตัวย่อชื่อสัญชาติ/ประเทศ</th>
                            <th>ชื่อสัญชาติ/ประเทศ</th>
                            <th>จัดการข้อมูล</th>
                        </tr>
                    </thead>
                    <asp:Repeater ID="myRepeaterNation" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex +1 %></td>
                                <td>
                                    <asp:Label ID="lbNationID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "NATION_ID") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbNationShortName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "NATION_SHORT") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbNationFullName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "NATION_NAME_EN") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkEditNation" Text="แก้ไข" runat="server" CssClass="btn btn-warning" OnClick="OnEditNation" />
                                    <asp:LinkButton ID="lnkDeleteNation" Text="ลบ" runat="server" CssClass="btn btn-danger" OnClick="OnDeleteNation" OnClientClick="return confirm('คุณต้องการที่จะลบใช่หรือไม่?');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:Panel>

        <asp:Panel ID="Panel7" runat="server" CssClass="divpan" visible="false">
            <div id="divheader7" runat="server" class="ps-header">
                <img src="Image/Small/wrench.png" /><asp:Label ID="Label21" runat="server" Text="วิทยาเขต"></asp:Label></div>
            <div id="divInsertCampus" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/Add.png" />เพิ่มข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>รหัสวิทยาเขต<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertIdCampus" runat="server" onkeypress="return isNumberKey(event)" MaxLength="4" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>ชื่อวิทยาเขต<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertNameCampus" runat="server" MaxLength="255" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>จัดการข้อมูล:<br />
                            <asp:Button ID="btnInsertCampus" runat="server" CssClass="btn btn-primary ekknidRight" OnClick="btnInsertCampus_Click" Text="เพิ่มข้อมูล" />
                            <asp:Button ID="btnUpdateCampus" runat="server" CssClass="btn btn-info ekknidRight" OnClick="btnUpdateCampus_Click" Text="อัพเดทข้อมูล" />
                            <asp:LinkButton ID="lbuClearCampus" runat="server" CssClass="btn btn-success" OnClick="lbuClearCampus_Click" Text="ล้างข้อมูล" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divLoadCampus" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/list.png" />ข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover" id="dataTables-Campus">
                    <thead>
                        <tr>
                            <th>ลำดับที่</th>
                            <th>รหัสวิทยาเขต</th>
                            <th>ชื่อวิทยาเขต</th>
                            <th>จัดการข้อมูล</th>
                        </tr>
                    </thead>
                    <asp:Repeater ID="myRepeaterCampus" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex +1 %></td>
                                <td>
                                    <asp:Label ID="lbCampusID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CAMPUS_ID") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbCampusName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CAMPUS_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkEditCampus" Text="แก้ไข" runat="server" CssClass="btn btn-warning" OnClick="OnEditCampus" />
                                    <asp:LinkButton ID="lnkDeleteCampus" Text="ลบ" runat="server" CssClass="btn btn-danger" OnClick="OnDeleteCampus" OnClientClick="return confirm('คุณต้องการที่จะลบใช่หรือไม่?');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:Panel>

        <asp:Panel ID="Panel8" runat="server" CssClass="divpan" visible="false">
            <div id="divheader8" runat="server" class="ps-header">
                <img src="Image/Small/wrench.png" /><asp:Label ID="Label22" runat="server" Text="สำนัก/สถาบัน/คณะ"></asp:Label></div>
            <div id="divInsertFaculty" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/Add.png" />เพิ่มข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>รหัสสำนัก/สถาบัน/คณะ<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertIdFaculty" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>ชื่อสำนัก/สถาบัน/คณะ<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertNameFaculty" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>อยู่ในวิทยาเขต<span class="ps-lb-red" />*<br />
                            <asp:DropDownList ID="ddlCampusInFaculty" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>จัดการข้อมูล:<br />
                            <asp:Button ID="btnInsertFaculty" runat="server" CssClass="btn btn-primary ekknidRight" OnClick="btnInsertFaculty_Click" Text="เพิ่มข้อมูล" />
                            <asp:Button ID="btnUpdateFaculty" runat="server" CssClass="btn btn-info ekknidRight" OnClick="btnUpdateFaculty_Click" Text="อัพเดทข้อมูล" />
                            <asp:LinkButton ID="lbuClearFaculty" runat="server" CssClass="btn btn-success" OnClick="lbuClearFaculty_Click" Text="ล้างข้อมูล" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divLoadFaculty" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/list.png" />ข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover" id="dataTables-Faculty">
                    <thead>
                        <tr>
                            <th>ลำดับที่</th>
                            <th>รหัสสำนัก/สถาบัน/คณะ</th>
                            <th>ชื่อสำนัก/สถาบัน/คณะ</th>
                             <th>อยู่ในวิทยาเขต</th>
                            <th>จัดการข้อมูล</th>
                        </tr>
                    </thead>
                    <asp:Repeater ID="myRepeaterFaculty" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex +1 %></td>
                                <td>
                                    <asp:Label ID="lbFacultyID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FACULTY_ID") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbFacultyName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FACULTY_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:HiddenField ID="HFCampusInFaculty" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "CAMPUS_ID") %>'/>
                                    <asp:Label ID="lbCampusName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CAMPUS_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkEditFaculty" Text="แก้ไข" runat="server" CssClass="btn btn-warning" OnClick="OnEditFaculty" />
                                    <asp:LinkButton ID="lnkDeleteFaculty" Text="ลบ" runat="server" CssClass="btn btn-danger" OnClick="OnDeleteFaculty" OnClientClick="return confirm('คุณต้องการที่จะลบใช่หรือไม่?');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:Panel>

        <asp:Panel ID="Panel9" runat="server" CssClass="divpan" visible="false">
            <div id="divheader9" runat="server" class="ps-header">
                <img src="Image/Small/wrench.png" /><asp:Label ID="Label23" runat="server" Text="กอง/สำนักงานเลขา/ภาควิชา"></asp:Label></div>
            <div id="divInsertDivision" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/Add.png" />เพิ่มข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>รหัสกอง/สำนักงานเลขา/ภาควิชา<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertIdDivision" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>ชื่อกอง/สำนักงานเลขา/ภาควิชา<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertNameDivision" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>จัดการข้อมูล:<br />
                            <asp:Button ID="btnInsertDivision" runat="server" CssClass="btn btn-primary ekknidRight" OnClick="btnInsertDivision_Click" Text="เพิ่มข้อมูล" />
                            <asp:Button ID="btnUpdateDivision" runat="server" CssClass="btn btn-info ekknidRight" OnClick="btnUpdateDivision_Click" Text="อัพเดทข้อมูล" />
                            <asp:LinkButton ID="lbuClearDivision" runat="server" CssClass="btn btn-success" OnClick="lbuClearDivision_Click" Text="ล้างข้อมูล" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divLoadDivision" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/list.png" />ข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover" id="dataTables-Division">
                    <thead>
                        <tr>
                            <th>ลำดับที่</th>
                            <th>รหัสกอง/สำนักงานเลขา/ภาควิชา</th>
                            <th>ชื่อกอง/สำนักงานเลขา/ภาควิชา</th>
                            <th>จัดการข้อมูล</th>
                        </tr>
                    </thead>
                    <asp:Repeater ID="myRepeaterDivision" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex +1 %></td>
                                <td>
                                    <asp:Label ID="lbDivisionID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DIVISION_ID") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbDivisionName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DIVISION_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkEditDivision" Text="แก้ไข" runat="server" CssClass="btn btn-warning" OnClick="OnEditDivision" />
                                    <asp:LinkButton ID="lnkDeleteDivision" Text="ลบ" runat="server" CssClass="btn btn-danger" OnClick="OnDeleteDivision" OnClientClick="return confirm('คุณต้องการที่จะลบใช่หรือไม่?');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:Panel>

        <asp:Panel ID="Panel10" runat="server" CssClass="divpan" visible="false">
            <div id="divheader10" runat="server" class="ps-header">
                <img src="Image/Small/wrench.png" /><asp:Label ID="Label24" runat="server" Text="งาน/ฝ่าย"></asp:Label></div>
            <div id="divInsertWorkDivision" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/Add.png" />เพิ่มข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>รหัสงาน/ฝ่าย<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertIdWorkDivision" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>ชื่องาน/ฝ่าย<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertNameWorkDivision" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>จัดการข้อมูล:<br />
                            <asp:Button ID="btnInsertWorkDivision" runat="server" CssClass="btn btn-primary ekknidRight" OnClick="btnInsertWorkDivision_Click" Text="เพิ่มข้อมูล" />
                            <asp:Button ID="btnUpdateWorkDivision" runat="server" CssClass="btn btn-info ekknidRight" OnClick="btnUpdateWorkDivision_Click" Text="อัพเดทข้อมูล" />
                            <asp:LinkButton ID="lbuClearWorkDivision" runat="server" CssClass="btn btn-success" OnClick="lbuClearWorkDivision_Click" Text="ล้างข้อมูล" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divLoadWorkDivision" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/list.png" />ข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover" id="dataTables-WorkDivision">
                    <thead>
                        <tr>
                            <th>ลำดับที่</th>
                            <th>รหัสงาน/ฝ่าย</th>
                            <th>ชื่องาน/ฝ่าย</th>
                            <th>จัดการข้อมูล</th>
                        </tr>
                    </thead>
                    <asp:Repeater ID="myRepeaterWorkDivision" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex +1 %></td>
                                <td>
                                    <asp:Label ID="lbWorkDivisionID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WORK_ID") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbWorkDivisionName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WORK_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkEditWorkDivision" Text="แก้ไข" runat="server" CssClass="btn btn-warning" OnClick="OnEditWorkDivision" />
                                    <asp:LinkButton ID="lnkDeleteWorkDivision" Text="ลบ" runat="server" CssClass="btn btn-danger" OnClick="OnDeleteWorkDivision" OnClientClick="return confirm('คุณต้องการที่จะลบใช่หรือไม่?');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:Panel>

        <asp:Panel ID="Panel11" runat="server" CssClass="divpan" visible="false">
            <div id="divheader11" runat="server" class="ps-header">
                <img src="Image/Small/wrench.png" /><asp:Label ID="Label7" runat="server" Text="ประเภทบุคลากร"></asp:Label></div>
            <div id="divInsertStafftype" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/Add.png" />เพิ่มข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>รหัสประเภทบุคลากร<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertIdStafftype" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>ชื่อประเภทบุคลากร<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertNameStafftype" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>จัดการข้อมูล:<br />
                            <asp:Button ID="btnInsertStafftype" runat="server" CssClass="btn btn-primary ekknidRight" OnClick="btnInsertStafftype_Click" Text="เพิ่มข้อมูล" />
                            <asp:Button ID="btnUpdateStafftype" runat="server" CssClass="btn btn-info ekknidRight" OnClick="btnUpdateStafftype_Click" Text="อัพเดทข้อมูล" />
                            <asp:LinkButton ID="lbuClearStafftype" runat="server" CssClass="btn btn-success" OnClick="lbuClearStafftype_Click" Text="ล้างข้อมูล" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divLoadStafftype" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/list.png" />ข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover" id="dataTables-Stafftype">
                    <thead>
                        <tr>
                            <th>ลำดับที่</th>
                            <th>รหัสประเภทบุคลากร</th>
                            <th>ชื่อประเภทบุคลากร</th>
                            <th>จัดการข้อมูล</th>
                        </tr>
                    </thead>
                    <asp:Repeater ID="myRepeaterStafftype" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex +1 %></td>
                                <td>
                                    <asp:Label ID="lbStafftypeID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "STAFFTYPE_ID") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbStafftypeName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "STAFFTYPE_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkEditStafftype" Text="แก้ไข" runat="server" CssClass="btn btn-warning" OnClick="OnEditStafftype" />
                                    <asp:LinkButton ID="lnkDeleteStafftype" Text="ลบ" runat="server" CssClass="btn btn-danger" OnClick="OnDeleteStafftype" OnClientClick="return confirm('คุณต้องการที่จะลบใช่หรือไม่?');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:Panel>
        
        <asp:Panel ID="Panel12" runat="server" CssClass="divpan" visible="false">
            <div id="divheader12" runat="server" class="ps-header">
                <img src="Image/Small/wrench.png" /><asp:Label ID="Label8" runat="server" Text="ระยะเวลาจ้าง"></asp:Label></div>
            <div id="divInsertTimeContact" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/Add.png" />เพิ่มข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>รหัสระยะเวลาจ้าง<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertIdTimeContact" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>ชื่อระยะเวลาจ้าง<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertNameTimeContact" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>จัดการข้อมูล:<br />
                            <asp:Button ID="btnInsertTimeContact" runat="server" CssClass="btn btn-primary ekknidRight" OnClick="btnInsertTimeContact_Click" Text="เพิ่มข้อมูล" />
                            <asp:Button ID="btnUpdateTimeContact" runat="server" CssClass="btn btn-info ekknidRight" OnClick="btnUpdateTimeContact_Click" Text="อัพเดทข้อมูล" />
                            <asp:LinkButton ID="lbuClearTimeContact" runat="server" CssClass="btn btn-success" OnClick="lbuClearTimeContact_Click" Text="ล้างข้อมูล" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divLoadTimeContact" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/list.png" />ข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover" id="dataTables-TimeContact">
                    <thead>
                        <tr>
                            <th>ลำดับที่</th>
                            <th>รหัสระยะเวลาจ้าง</th>
                            <th>ชื่อระยะเวลาจ้าง</th>
                            <th>จัดการข้อมูล</th>
                        </tr>
                    </thead>
                    <asp:Repeater ID="myRepeaterTimeContact" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex +1 %></td>
                                <td>
                                    <asp:Label ID="lbTimeContactID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TIME_CONTACT_ID") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbTimeContactName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TIME_CONTACT_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkEditTimeContact" Text="แก้ไข" runat="server" CssClass="btn btn-warning" OnClick="OnEditTimeContact" />
                                    <asp:LinkButton ID="lnkDeleteTimeContact" Text="ลบ" runat="server" CssClass="btn btn-danger" OnClick="OnDeleteTimeContact" OnClientClick="return confirm('คุณต้องการที่จะลบใช่หรือไม่?');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:Panel>

        <asp:Panel ID="Panel13" runat="server" CssClass="divpan" visible="false">
            <div id="divheader13" runat="server" class="ps-header">
                <img src="Image/Small/wrench.png" /><asp:Label ID="Label9" runat="server" Text="ประเภทเงินจ้าง"></asp:Label></div>
            <div id="divInsertBudget" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/Add.png" />เพิ่มข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>รหัสประเภทเงินจ้าง<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertIdBudget" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>ชื่อประเภทเงินจ้าง<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertNameBudget" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>จัดการข้อมูล:<br />
                            <asp:Button ID="btnInsertBudget" runat="server" CssClass="btn btn-primary ekknidRight" OnClick="btnInsertBudget_Click" Text="เพิ่มข้อมูล" />
                            <asp:Button ID="btnUpdateBudget" runat="server" CssClass="btn btn-info ekknidRight" OnClick="btnUpdateBudget_Click" Text="อัพเดทข้อมูล" />
                            <asp:LinkButton ID="lbuClearBudget" runat="server" CssClass="btn btn-success" OnClick="lbuClearBudget_Click" Text="ล้างข้อมูล" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divLoadBudget" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/list.png" />ข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover" id="dataTables-Budget">
                    <thead>
                        <tr>
                            <th>ลำดับที่</th>
                            <th>รหัสประเภทเงินจ้าง</th>
                            <th>ชื่อประเภทเงินจ้าง</th>
                            <th>จัดการข้อมูล</th>
                        </tr>
                    </thead>
                    <asp:Repeater ID="myRepeaterBudget" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex +1 %></td>
                                <td>
                                    <asp:Label ID="lbBudgetID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BUDGET_ID") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbBudgetName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BUDGET_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkEditBudget" Text="แก้ไข" runat="server" CssClass="btn btn-warning" OnClick="OnEditBudget" />
                                    <asp:LinkButton ID="lnkDeleteBudget" Text="ลบ" runat="server" CssClass="btn btn-danger" OnClick="OnDeleteBudget" OnClientClick="return confirm('คุณต้องการที่จะลบใช่หรือไม่?');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:Panel>

        <asp:Panel ID="Panel14" runat="server" CssClass="divpan" visible="false">
            <div id="divheader14" runat="server" class="ps-header">
                <img src="Image/Small/wrench.png" /><asp:Label ID="Label10" runat="server" Text="ประเภทบุคลากรย่อย"></asp:Label></div>
            <div id="divInsertSubStafftype" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/Add.png" />เพิ่มข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>รหัสประเภทบุคลากรย่อย<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertIdSubStafftype" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>ชื่อประเภทบุคลากรย่อย<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertNameSubStafftype" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>จัดการข้อมูล:<br />
                            <asp:Button ID="btnInsertSubStafftype" runat="server" CssClass="btn btn-primary ekknidRight" OnClick="btnInsertSubStafftype_Click" Text="เพิ่มข้อมูล" />
                            <asp:Button ID="btnUpdateSubStafftype" runat="server" CssClass="btn btn-info ekknidRight" OnClick="btnUpdateSubStafftype_Click" Text="อัพเดทข้อมูล" />
                            <asp:LinkButton ID="lbuClearSubStafftype" runat="server" CssClass="btn btn-success" OnClick="lbuClearSubStafftype_Click" Text="ล้างข้อมูล" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divLoadSubStafftype" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/list.png" />ข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover" id="dataTables-SubStafftype">
                    <thead>
                        <tr>
                            <th>ลำดับที่</th>
                            <th>รหัสประเภทบุคลากรย่อย</th>
                            <th>ชื่อประเภทบุคลากรย่อย</th>
                            <th>จัดการข้อมูล</th>
                        </tr>
                    </thead>
                    <asp:Repeater ID="myRepeaterSubStafftype" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex +1 %></td>
                                <td>
                                    <asp:Label ID="lbSubStafftypeID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SUBSTAFFTYPE_ID") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbSubStafftypeName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SUBSTAFFTYPE_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkEditSubStafftype" Text="แก้ไข" runat="server" CssClass="btn btn-warning" OnClick="OnEditSubStafftype" />
                                    <asp:LinkButton ID="lnkDeleteSubStafftype" Text="ลบ" runat="server" CssClass="btn btn-danger" OnClick="OnDeleteSubStafftype" OnClientClick="return confirm('คุณต้องการที่จะลบใช่หรือไม่?');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:Panel>

        <asp:Panel ID="Panel15" runat="server" CssClass="divpan" visible="false">
            <div id="divheader15" runat="server" class="ps-header">
                <img src="Image/Small/wrench.png" /><asp:Label ID="Label11" runat="server" Text="ตำแหน่งบริหาร"></asp:Label></div>
            <div id="divInsertAdminPosition" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/Add.png" />เพิ่มข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>รหัสตำแหน่งบริหาร<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertIdAdminPosition" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>ชื่อตำแหน่งบริหาร<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertNameAdminPosition" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>จัดการข้อมูล:<br />
                            <asp:Button ID="btnInsertAdminPosition" runat="server" CssClass="btn btn-primary ekknidRight" OnClick="btnInsertAdminPosition_Click" Text="เพิ่มข้อมูล" />
                            <asp:Button ID="btnUpdateAdminPosition" runat="server" CssClass="btn btn-info ekknidRight" OnClick="btnUpdateAdminPosition_Click" Text="อัพเดทข้อมูล" />
                            <asp:LinkButton ID="lbuClearAdminPosition" runat="server" CssClass="btn btn-success" OnClick="lbuClearAdminPosition_Click" Text="ล้างข้อมูล" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divLoadAdminPosition" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/list.png" />ข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover" id="dataTables-AdminPosition">
                    <thead>
                        <tr>
                            <th>ลำดับที่</th>
                            <th>รหัสตำแหน่งบริหาร</th>
                            <th>ชื่อตำแหน่งบริหาร</th>
                            <th>จัดการข้อมูล</th>
                        </tr>
                    </thead>
                    <asp:Repeater ID="myRepeaterAdminPosition" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex +1 %></td>
                                <td>
                                    <asp:Label ID="lbAdminPositionID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ADMIN_POSITION_ID") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbAdminPositionName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ADMIN_POSITION_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkEditAdminPosition" Text="แก้ไข" runat="server" CssClass="btn btn-warning" OnClick="OnEditAdminPosition" />
                                    <asp:LinkButton ID="lnkDeleteAdminPosition" Text="ลบ" runat="server" CssClass="btn btn-danger" OnClick="OnDeleteAdminPosition" OnClientClick="return confirm('คุณต้องการที่จะลบใช่หรือไม่?');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:Panel>

        <asp:Panel ID="Panel16" runat="server" CssClass="divpan" visible="false">
            <div id="divheader16" runat="server" class="ps-header">
                <img src="Image/Small/wrench.png" /><asp:Label ID="Label12" runat="server" Text="ระดับตำแหน่ง"></asp:Label></div>
            <div id="divInsertPosition" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/Add.png" />เพิ่มข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>รหัสระดับตำแหน่ง<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertIdPosition" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>ชื่อระดับตำแหน่ง<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertNamePosition" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>จัดการข้อมูล:<br />
                            <asp:Button ID="btnInsertPosition" runat="server" CssClass="btn btn-primary ekknidRight" OnClick="btnInsertPosition_Click" Text="เพิ่มข้อมูล" />
                            <asp:Button ID="btnUpdatePosition" runat="server" CssClass="btn btn-info ekknidRight" OnClick="btnUpdatePosition_Click" Text="อัพเดทข้อมูล" />
                            <asp:LinkButton ID="lbuClearPosition" runat="server" CssClass="btn btn-success" OnClick="lbuClearPosition_Click" Text="ล้างข้อมูล" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divLoadPosition" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/list.png" />ข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover" id="dataTables-Position">
                    <thead>
                        <tr>
                            <th>ลำดับที่</th>
                            <th>รหัสระดับตำแหน่ง</th>
                            <th>ชื่อระดับตำแหน่ง</th>
                            <th>จัดการข้อมูล</th>
                        </tr>
                    </thead>
                    <asp:Repeater ID="myRepeaterPosition" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex +1 %></td>
                                <td>
                                    <asp:Label ID="lbPositionID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_ID") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbPositionName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkEditPosition" Text="แก้ไข" runat="server" CssClass="btn btn-warning" OnClick="OnEditPosition" />
                                    <asp:LinkButton ID="lnkDeletePosition" Text="ลบ" runat="server" CssClass="btn btn-danger" OnClick="OnDeletePosition" OnClientClick="return confirm('คุณต้องการที่จะลบใช่หรือไม่?');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:Panel>

        <asp:Panel ID="Panel17" runat="server" CssClass="divpan" visible="false">
            <div id="divheader17" runat="server" class="ps-header">
                <img src="Image/Small/wrench.png" /><asp:Label ID="Label13" runat="server" Text="ตำแหน่งในสายงาน"></asp:Label></div>
            <div id="divInsertWorkPos" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/Add.png" />เพิ่มข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>รหัสตำแหน่งในสายงาน<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertIdWorkPos" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>ชื่อตำแหน่งในสายงาน<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertNameWorkPos" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>จัดการข้อมูล:<br />
                            <asp:Button ID="btnInsertWorkPos" runat="server" CssClass="btn btn-primary ekknidRight" OnClick="btnInsertWorkPos_Click" Text="เพิ่มข้อมูล" />
                            <asp:Button ID="btnUpdateWorkPos" runat="server" CssClass="btn btn-info ekknidRight" OnClick="btnUpdateWorkPos_Click" Text="อัพเดทข้อมูล" />
                            <asp:LinkButton ID="lbuClearWorkPos" runat="server" CssClass="btn btn-success" OnClick="lbuClearWorkPos_Click" Text="ล้างข้อมูล" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divLoadWorkPos" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/list.png" />ข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover" id="dataTables-WorkPos">
                    <thead>
                        <tr>
                            <th>ลำดับที่</th>
                            <th>รหัสตำแหน่งในสายงาน</th>
                            <th>ชื่อตำแหน่งในสายงาน</th>
                            <th>จัดการข้อมูล</th>
                        </tr>
                    </thead>
                    <asp:Repeater ID="myRepeaterWorkPos" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex +1 %></td>
                                <td>
                                    <asp:Label ID="lbWorkPosID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "POSITION_WORK_ID") %>'></asp:Label>
                                </td>
                                <td style="width:550px">
                                    <asp:Label ID="lbWorkPosName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "POSITION_WORK_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkEditWorkPos" Text="แก้ไข" runat="server" CssClass="btn btn-warning" OnClick="OnEditWorkPos" />
                                    <asp:LinkButton ID="lnkDeleteWorkPos" Text="ลบ" runat="server" CssClass="btn btn-danger" OnClick="OnDeleteWorkPos" OnClientClick="return confirm('คุณต้องการที่จะลบใช่หรือไม่?');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:Panel>

        <asp:Panel ID="Panel18" runat="server" CssClass="divpan" visible="false">
            <div id="divheader18" runat="server" class="ps-header">
                <img src="Image/Small/wrench.png" /><asp:Label ID="Label14" runat="server" Text="กลุ่มสาขาวิชา"></asp:Label></div>
            <div id="divInsertTeachISCED" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/Add.png" />เพิ่มข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>รหัสกลุ่มสาขาวิชา<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertIdTeachISCED" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>ชื่อกลุ่มสาขาวิชา<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertNameTeachISCED" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>จัดการข้อมูล:<br />
                            <asp:Button ID="btnInsertTeachISCED" runat="server" CssClass="btn btn-primary ekknidRight" OnClick="btnInsertTeachISCED_Click" Text="เพิ่มข้อมูล" />
                            <asp:Button ID="btnUpdateTeachISCED" runat="server" CssClass="btn btn-info ekknidRight" OnClick="btnUpdateTeachISCED_Click" Text="อัพเดทข้อมูล" />
                            <asp:LinkButton ID="lbuClearTeachISCED" runat="server" CssClass="btn btn-success" OnClick="lbuClearTeachISCED_Click" Text="ล้างข้อมูล" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divLoadTeachISCED" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/list.png" />ข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover" id="dataTables-TeachISCED">
                    <thead>
                        <tr>
                            <th>ลำดับที่</th>
                            <th>รหัสกลุ่มสาขาวิชา</th>
                            <th>ชื่อกลุ่มสาขาวิชา</th>
                            <th>จัดการข้อมูล</th>
                        </tr>
                    </thead>
                    <asp:Repeater ID="myRepeaterTeachISCED" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex +1 %></td>
                                <td>
                                    <asp:Label ID="lbTeachISCEDID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ISCED_CODE") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbTeachISCEDName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ISCED_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkEditTeachISCED" Text="แก้ไข" runat="server" CssClass="btn btn-warning" OnClick="OnEditTeachISCED" />
                                    <asp:LinkButton ID="lnkDeleteTeachISCED" Text="ลบ" runat="server" CssClass="btn btn-danger" OnClick="OnDeleteTeachISCED" OnClientClick="return confirm('คุณต้องการที่จะลบใช่หรือไม่?');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:Panel>

        <asp:Panel ID="Panel19" runat="server" CssClass="divpan" visible="false">
            <div id="divheader19" runat="server" class="ps-header">
                <img src="Image/Small/wrench.png" /><asp:Label ID="Label15" runat="server" Text="ระดับการศึกษา"></asp:Label></div>
            <div id="divInsertGradLev" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/Add.png" />เพิ่มข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>รหัสระดับการศึกษา<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertIdGradLev" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>ชื่อระดับการศึกษา<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertNameGradLev" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>จัดการข้อมูล:<br />
                            <asp:Button ID="btnInsertGradLev" runat="server" CssClass="btn btn-primary ekknidRight" OnClick="btnInsertGradLev_Click" Text="เพิ่มข้อมูล" />
                            <asp:Button ID="btnUpdateGradLev" runat="server" CssClass="btn btn-info ekknidRight" OnClick="btnUpdateGradLev_Click" Text="อัพเดทข้อมูล" />
                            <asp:LinkButton ID="lbuClearGradLev" runat="server" CssClass="btn btn-success" OnClick="lbuClearGradLev_Click" Text="ล้างข้อมูล" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divLoadGradLev" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/list.png" />ข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover" id="dataTables-GradLev">
                    <thead>
                        <tr>
                            <th>ลำดับที่</th>
                            <th>รหัสระดับการศึกษา</th>
                            <th>ชื่อระดับการศึกษา</th>
                            <th>จัดการข้อมูล</th>
                        </tr>
                    </thead>
                    <asp:Repeater ID="myRepeaterGradLev" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex +1 %></td>
                                <td>
                                    <asp:Label ID="lbGradLevID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LEV_ID") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbGradLevName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LEV_NAME_TH") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkEditGradLev" Text="แก้ไข" runat="server" CssClass="btn btn-warning" OnClick="OnEditGradLev" />
                                    <asp:LinkButton ID="lnkDeleteGradLev" Text="ลบ" runat="server" CssClass="btn btn-danger" OnClick="OnDeleteGradLev" OnClientClick="return confirm('คุณต้องการที่จะลบใช่หรือไม่?');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:Panel>

        <asp:Panel ID="Panel20" runat="server" CssClass="divpan" visible="false">
            <div id="divheader20" runat="server" class="ps-header">
                <img src="Image/Small/wrench.png" /><asp:Label ID="Label16" runat="server" Text="สาขาวิชา"></asp:Label></div>
            <div id="divInsertGradProg" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/Add.png" />เพิ่มข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>รหัสสาขาวิชา<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertIdGradProg" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>ชื่อสาขาวิชา<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertNameGradProg" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>จัดการข้อมูล:<br />
                            <asp:Button ID="btnInsertGradProg" runat="server" CssClass="btn btn-primary ekknidRight" OnClick="btnInsertGradProg_Click" Text="เพิ่มข้อมูล" />
                            <asp:Button ID="btnUpdateGradProg" runat="server" CssClass="btn btn-info ekknidRight" OnClick="btnUpdateGradProg_Click" Text="อัพเดทข้อมูล" />
                            <asp:LinkButton ID="lbuClearGradProg" runat="server" CssClass="btn btn-success" OnClick="lbuClearGradProg_Click" Text="ล้างข้อมูล" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divLoadGradProg" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/list.png" />ข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover" id="dataTables-GradProg">
                    <thead>
                        <tr>
                            <th>ลำดับที่</th>
                            <th>รหัสสาขาวิชา</th>
                            <th>ชื่อสาขาวิชา</th>
                            <th>จัดการข้อมูล</th>
                        </tr>
                    </thead>
                    <asp:Repeater ID="myRepeaterGradProg" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex +1 %></td>
                                <td>
                                    <asp:Label ID="lbGradProgID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PROGRAM_ID_NEW") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbGradProgName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PROGRAM_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkEditGradProg" Text="แก้ไข" runat="server" CssClass="btn btn-warning" OnClick="OnEditGradProg" />
                                    <asp:LinkButton ID="lnkDeleteGradProg" Text="ลบ" runat="server" CssClass="btn btn-danger" OnClick="OnDeleteGradProg" OnClientClick="return confirm('คุณต้องการที่จะลบใช่หรือไม่?');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:Panel>

        <asp:Panel ID="Panel21" runat="server" CssClass="divpan" visible="false">
            <div id="divheader21" runat="server" class="ps-header">
                <img src="Image/Small/wrench.png" /><asp:Label ID="Label17" runat="server" Text="ความพิการ"></asp:Label></div>
            <div id="divInsertDeform" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/Add.png" />เพิ่มข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>รหัสความพิการ<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertIdDeform" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>ชื่อความพิการ<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertNameDeform" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>จัดการข้อมูล:<br />
                            <asp:Button ID="btnInsertDeform" runat="server" CssClass="btn btn-primary ekknidRight" OnClick="btnInsertDeform_Click" Text="เพิ่มข้อมูล" />
                            <asp:Button ID="btnUpdateDeform" runat="server" CssClass="btn btn-info ekknidRight" OnClick="btnUpdateDeform_Click" Text="อัพเดทข้อมูล" />
                            <asp:LinkButton ID="lbuClearDeform" runat="server" CssClass="btn btn-success" OnClick="lbuClearDeform_Click" Text="ล้างข้อมูล" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divLoadDeform" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/list.png" />ข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover" id="dataTables-Deform">
                    <thead>
                        <tr>
                            <th>ลำดับที่</th>
                            <th>รหัสความพิการ</th>
                            <th>ชื่อความพิการ</th>
                            <th>จัดการข้อมูล</th>
                        </tr>
                    </thead>
                    <asp:Repeater ID="myRepeaterDeform" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex +1 %></td>
                                <td>
                                    <asp:Label ID="lbDeformID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DEFORM_ID") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbDeformName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DEFORM_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkEditDeform" Text="แก้ไข" runat="server" CssClass="btn btn-warning" OnClick="OnEditDeform" />
                                    <asp:LinkButton ID="lnkDeleteDeform" Text="ลบ" runat="server" CssClass="btn btn-danger" OnClick="OnDeleteDeform" OnClientClick="return confirm('คุณต้องการที่จะลบใช่หรือไม่?');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:Panel>

        <asp:Panel ID="Panel22" runat="server" CssClass="divpan" visible="false">
            <div id="divheader22" runat="server" class="ps-header">
                <img src="Image/Small/wrench.png" /><asp:Label ID="Label18" runat="server" Text="ศาสนา"></asp:Label></div>
            <div id="divInsertReligion" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/Add.png" />เพิ่มข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>รหัสศาสนา<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertIdReligion" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>ชื่อศาสนา<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertNameReligion" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>จัดการข้อมูล:<br />
                            <asp:Button ID="btnInsertReligion" runat="server" CssClass="btn btn-primary ekknidRight" OnClick="btnInsertReligion_Click" Text="เพิ่มข้อมูล" />
                            <asp:Button ID="btnUpdateReligion" runat="server" CssClass="btn btn-info ekknidRight" OnClick="btnUpdateReligion_Click" Text="อัพเดทข้อมูล" />
                            <asp:LinkButton ID="lbuClearReligion" runat="server" CssClass="btn btn-success" OnClick="lbuClearReligion_Click" Text="ล้างข้อมูล" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divLoadReligion" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/list.png" />ข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover" id="dataTables-Religion">
                    <thead>
                        <tr>
                            <th>ลำดับที่</th>
                            <th>รหัสศาสนา</th>
                            <th>ชื่อศาสนา</th>
                            <th>จัดการข้อมูล</th>
                        </tr>
                    </thead>
                    <asp:Repeater ID="myRepeaterReligion" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex +1 %></td>
                                <td>
                                    <asp:Label ID="lbReligionID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RELIGION_ID") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbReligionName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RELIGION_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkEditReligion" Text="แก้ไข" runat="server" CssClass="btn btn-warning" OnClick="OnEditReligion" />
                                    <asp:LinkButton ID="lnkDeleteReligion" Text="ลบ" runat="server" CssClass="btn btn-danger" OnClick="OnDeleteReligion" OnClientClick="return confirm('คุณต้องการที่จะลบใช่หรือไม่?');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:Panel>

        <asp:Panel ID="Panel23" runat="server" CssClass="divpan" visible="false">
            <div id="divheader23" runat="server" class="ps-header">
                <img src="Image/Small/wrench.png" /><asp:Label ID="Label19" runat="server" Text="ประเภทการดำรงตำแหน่งปัจจุบัน"></asp:Label></div>
            <div id="divInsertMovementType" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/Add.png" />เพิ่มข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>รหัสประเภทการดำรงตำแหน่งปัจจุบัน<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertIdMovementType" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>ชื่อประเภทการดำรงตำแหน่งปัจจุบัน<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertNameMovementType" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>จัดการข้อมูล:<br />
                            <asp:Button ID="btnInsertMovementType" runat="server" CssClass="btn btn-primary ekknidRight" OnClick="btnInsertMovementType_Click" Text="เพิ่มข้อมูล" />
                            <asp:Button ID="btnUpdateMovementType" runat="server" CssClass="btn btn-info ekknidRight" OnClick="btnUpdateMovementType_Click" Text="อัพเดทข้อมูล" />
                            <asp:LinkButton ID="lbuClearMovementType" runat="server" CssClass="btn btn-success" OnClick="lbuClearMovementType_Click" Text="ล้างข้อมูล" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divLoadMovementType" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/list.png" />ข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover" id="dataTables-MovementType">
                    <thead>
                        <tr>
                            <th>ลำดับที่</th>
                            <th>รหัสประเภทการดำรงตำแหน่งปัจจุบัน</th>
                            <th>ชื่อประเภทการดำรงตำแหน่งปัจจุบัน</th>
                            <th>จัดการข้อมูล</th>
                        </tr>
                    </thead>
                    <asp:Repeater ID="myRepeaterMovementType" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex +1 %></td>
                                <td>
                                    <asp:Label ID="lbMovementTypeID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MOVEMENT_TYPE_ID") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbMovementTypeName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MOVEMENT_TYPE_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkEditMovementType" Text="แก้ไข" runat="server" CssClass="btn btn-warning" OnClick="OnEditMovementType" />
                                    <asp:LinkButton ID="lnkDeleteMovementType" Text="ลบ" runat="server" CssClass="btn btn-danger" OnClick="OnDeleteMovementType" OnClientClick="return confirm('คุณต้องการที่จะลบใช่หรือไม่?');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
