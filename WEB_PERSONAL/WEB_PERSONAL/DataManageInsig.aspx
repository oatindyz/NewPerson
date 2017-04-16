<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="DataManageInsig.aspx.cs" Inherits="WEB_PERSONAL.DataManageInsig" %>

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
            $('#dataTables-GovAvailable,#dataTables-GovHighSalaryCon,#dataTables-GovInsigYearCon,#dataTables-GovPosYearCon,#dataTables-GovSalaryCon,#dataTables-GovSalaryYearCon,#dataTables-GovAddOne,#dataTables-EmpAvailable,#dataTables-EmpInsigYearCon,#dataTables-GovEmpAvailable,#dataTables-GovEmpInsigYearCon').DataTable({
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
        <div class="ps-ms-left-ext-submenu"><img src="Image/Small/medal.png" />ข้าราชการ</div>
        <asp:LinkButton ID="lbuMenuGovAvailable" runat="server" OnClick="lbuMenuGovAvailable_Click"><img src="Image/Small/wrench.png" class="icon_left"/> เงื่อนไขช่วงที่สามารถขอได้ถึง</asp:LinkButton>

        <asp:LinkButton ID="lbuMenuGovHighSalaryCon" runat="server" OnClick="lbuMenuGovHighSalaryCon_Click"><img src="Image/Small/wrench.png" class="icon_left"/> เงื่อนไขใช้เงินเดือนขั้นสูง</asp:LinkButton>

        <asp:LinkButton ID="lbuMenuGovInsigYearCon" runat="server" OnClick="lbuMenuGovInsigYearCon_Click"><img src="Image/Small/wrench.png" class="icon_left"/> เงื่อนไขระยะเวลาครองชั้นเครื่องราชฯ</asp:LinkButton>

        <asp:LinkButton ID="lbuMenuGovPosYearCon" runat="server" OnClick="lbuMenuGovPosYearCon_Click"><img src="Image/Small/wrench.png" class="icon_left"/> เงื่อนไขระยะเวลาดำรงตำแหน่ง</asp:LinkButton>

        <asp:LinkButton ID="lbuMenuGovSalaryCon" runat="server" OnClick="lbuMenuGovSalaryCon_Click"><img src="Image/Small/wrench.png" class="icon_left"/> เงื่อนไขเงินเดือน</asp:LinkButton>

        <asp:LinkButton ID="lbuMenuGovSalaryYearCon" runat="server" OnClick="lbuMenuGovSalaryYearCon_Click"><img src="Image/Small/wrench.png" class="icon_left"/> เงื่อนไขได้รับเงินเดือนต่อเนื่อง</asp:LinkButton>

        <asp:LinkButton ID="lbuMenuGovAddOne" runat="server" OnClick="lbuMenuGovAddOne_Click"><img src="Image/Small/wrench.png" class="icon_left"/> เงื่อนไขเมื่อเกษียณ</asp:LinkButton>
    </div>

    <div class="c1">
        <div class="ps-ms-left-ext-submenu"><img src="Image/Small/medal.png" />ลูกจ้างประจำ</div>

        <asp:LinkButton ID="lbuMenuEmpAvailable" runat="server" OnClick="lbuMenuEmpAvailable_Click"><img src="Image/Small/wrench.png" class="icon_left"/> เงื่อนไขช่วงที่สามารถขอได้ถึง</asp:LinkButton>

        <asp:LinkButton ID="lbuMenuEmpInsigYearCon" runat="server" OnClick="lbuMenuEmpInsigYearCon_Click"><img src="Image/Small/wrench.png" class="icon_left"/> เงื่อนไขระยะเวลาครองชั้นเครื่องราชฯ</asp:LinkButton>
    </div>

    <div class="c1">
        <div class="ps-ms-left-ext-submenu"><img src="Image/Small/medal.png" />พนักงานราชการ</div>

        <asp:LinkButton ID="lbuMenuGovEmpAvailable" runat="server" OnClick="lbuMenuGovEmpAvailable_Click"><img src="Image/Small/wrench.png" class="icon_left"/> เงื่อนไขช่วงที่สามารถขอได้ถึง</asp:LinkButton>

        <asp:LinkButton ID="lbuMenuGovEmpInsigYearCon" runat="server" OnClick="lbuMenuGovEmpInsigYearCon_Click"><img src="Image/Small/wrench.png" class="icon_left"/> เงื่อนไขระยะเวลาครองชั้นเครื่องราชฯ</asp:LinkButton>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>

        <asp:Panel ID="Panel1" runat="server" CssClass="divpan" Visible="false">
            <div class="ps-header">
                <img src="Image/Small/wrench.png" /><asp:Label ID="Label1" runat="server" Text="ข้าราชการ : เงื่อนไขช่วงที่สามารถขอได้ถึง"></asp:Label>
            </div>
            <div class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/Add.png" />เพิ่มข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>ระดับตำแหน่ง<span class="ps-lb-red" />*<br />
                            <asp:DropDownList ID="ddlInsertGovAvailablePositionID" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>เงินประจำตำแหน่ง<br />
                            <asp:TextBox ID="tbInsertGovAvailablePositionSalary" runat="server" onkeypress="return isNumberKey(event)" MaxLength="10" CssClass="form-control input-sm" />
                        </td>
                        <td>เริ่มต้นขอ<span class="ps-lb-red" />*<br />
                            <asp:DropDownList ID="ddlInsertGovAvailableInsigMin" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>เลื่อนได้ถึง<span class="ps-lb-red" />*<br />
                            <asp:DropDownList ID="ddlInsertGovAvailableInsigMax" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>จัดการข้อมูล:<br />
                            <asp:Button ID="btnInsertGovAvailable" runat="server" CssClass="btn btn-primary ekknidRight" OnClick="btnInsertGovAvailable_Click" Text="เพิ่มข้อมูล" />
                            <asp:Button ID="btnUpdateGovAvailable" runat="server" CssClass="btn btn-info ekknidRight" OnClick="btnUpdateGovAvailable_Click" Text="อัพเดทข้อมูล" />
                            <asp:LinkButton ID="lbuClearGovAvailable" runat="server" CssClass="btn btn-success" OnClick="lbuClearGovAvailable_Click" Text="ล้างข้อมูล" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/list.png" />ข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover" id="dataTables-GovAvailable">
                    <thead>
                        <tr>
                            <th>ลำดับที่</th>
                            <th>ระดับตำแหน่ง</th>
                            <th>เงินประจำตำแหน่ง</th>
                            <th>เริ่มต้นขอ</th>
                            <th>เลื่อนได้ถึง</th>
                            <th>จัดการข้อมูล</th>
                        </tr>
                    </thead>
                    <asp:Repeater ID="myRepeaterGovAvailable" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex +1 %><asp:HiddenField ID="HFGovAvailable" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "IA_ID") %>' />
                                </td>
                                <td>
                                    <asp:HiddenField ID="HFGovInsigPositionID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "P_ID") %>' />
                                    <asp:Label ID="lbGovInsigPositionName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbPositionSalary" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "POS_SALARY") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:HiddenField ID="HFGovInsigAvailableMin" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "INSIG_MIN") %>' />
                                    <asp:Label ID="lbGovInsigAvailableMin" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "INSIG_MIN_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:HiddenField ID="HFGovInsigAvailableMax" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "INSIG_MAX") %>' />
                                    <asp:Label ID="lbGovInsigAvailableMax" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "INSIG_MAX_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkEditGovAvailable" Text="แก้ไข" runat="server" CssClass="btn btn-warning" OnClick="OnEditGovAvailable" />
                                    <asp:LinkButton ID="lnkDeleteGovAvailable" Text="ลบ" runat="server" CssClass="btn btn-danger" OnClick="OnDeleteGovAvailable" OnClientClick="return confirm('คุณต้องการที่จะลบใช่หรือไม่?');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:Panel>

        <asp:Panel ID="Panel2" runat="server" CssClass="divpan" Visible="false">
            <div class="ps-header">
                <img src="Image/Small/wrench.png" /><asp:Label ID="Label2" runat="server" Text="ข้าราชการ : เงื่อนไขใช้เงินเดือนขั้นสูง"></asp:Label>
            </div>
            <div class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/Add.png" />เพิ่มข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>ระดับตำแหน่ง<span class="ps-lb-red" />*<br />
                            <asp:DropDownList ID="ddlInsertGovHighSalaryConP_ID" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>ชั้นเครื่องราชฯที่จะขอ<span class="ps-lb-red" />*<br />
                            <asp:DropDownList ID="ddlInsertGovHighSalaryConINSIG_TARGET" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>เงินเดือนขั้นสูงของระดับตำแหน่ง<span class="ps-lb-red" />*<br />
                            <asp:DropDownList ID="ddlInsertGovHighSalaryConP_ID_USE" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>จัดการข้อมูล:<br />
                            <asp:Button ID="btnInsertGovHighSalaryCon" runat="server" CssClass="btn btn-primary ekknidRight" OnClick="btnInsertGovHighSalaryCon_Click" Text="เพิ่มข้อมูล" />
                            <asp:Button ID="btnUpdateGovHighSalaryCon" runat="server" CssClass="btn btn-info ekknidRight" OnClick="btnUpdateGovHighSalaryCon_Click" Text="อัพเดทข้อมูล" />
                            <asp:LinkButton ID="lbuClearGovHighSalaryCon" runat="server" CssClass="btn btn-success" OnClick="lbuClearGovHighSalaryCon_Click" Text="ล้างข้อมูล" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/list.png" />ข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover" id="dataTables-GovHighSalaryCon">
                    <thead>
                        <tr>
                            <th>ลำดับที่</th>
                            <th>ระดับตำแหน่ง</th>
                            <th>ชั้นเครื่องราชฯที่จะขอ</th>
                            <th>เงินเดือนขั้นสูงของระดับตำแหน่ง</th>
                            <th>จัดการข้อมูล</th>
                        </tr>
                    </thead>
                    <asp:Repeater ID="myRepeaterGovHighSalaryCon" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex +1 %><asp:HiddenField ID="HFGovHighSalaryConID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "IGHSC_ID") %>' />
                                </td>
                                <td>
                                    <asp:HiddenField ID="HFGovHighSalaryConP_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "P_ID") %>' />
                                    <asp:Label ID="lbGovHighSalaryConP_ID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:HiddenField ID="HFGovHighSalaryConINSIG_TARGET" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "INSIG_TARGET") %>' />
                                    <asp:Label ID="lbGovHighSalaryConINSIG_TARGET" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "INSIG_TARGET_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:HiddenField ID="HFGovHighSalaryConP_ID_USE" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "P_ID_USE") %>' />
                                    <asp:Label ID="lbGovHighSalaryConP_ID_USE" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_ID_USE_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkEditGovHighSalaryCon" Text="แก้ไข" runat="server" CssClass="btn btn-warning" OnClick="OnEditGovHighSalaryCon" />
                                    <asp:LinkButton ID="lnkDeleteGovHighSalaryCon" Text="ลบ" runat="server" CssClass="btn btn-danger" OnClick="OnDeleteGovHighSalaryCon" OnClientClick="return confirm('คุณต้องการที่จะลบใช่หรือไม่?');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:Panel>

        <asp:Panel ID="Panel3" runat="server" CssClass="divpan" Visible="false">
            <div class="ps-header">
                <img src="Image/Small/wrench.png" /><asp:Label ID="Label3" runat="server" Text="ข้าราชการ : เงื่อนไขระยะเวลาครองชั้นเครื่องราชฯ"></asp:Label>
            </div>
            <div class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/Add.png" />เพิ่มข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>ระดับตำแหน่ง<span class="ps-lb-red" />*<br />
                            <asp:DropDownList ID="ddlInsertGovInsigYearConP_ID" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>เงินประจำตำแหน่ง<br />
                            <asp:TextBox ID="tbInsertGovInsigYearConSALARY" runat="server" onkeypress="return isNumberKey(event)" MaxLength="10" CssClass="form-control input-sm" />
                        </td>
                        <td>ชั้นเครื่องราชฯที่จะขอ<span class="ps-lb-red" />*<br />
                            <asp:DropDownList ID="ddlInsertGovInsigYearConINSIG_TARGET" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>ต้องครองชั้นเครื่องราชฯ<span class="ps-lb-red" />*<br />
                            <asp:DropDownList ID="ddlInsertGovInsigYearConINSIG_USE" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>จำนวนปี<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertGovInsigYearConINSIG_YEAR" runat="server" onkeypress="return isNumberKey(event)" MaxLength="2" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>จัดการข้อมูล:<br />
                            <asp:Button ID="btnInsertGovInsigYearCon" runat="server" CssClass="btn btn-primary ekknidRight" OnClick="btnInsertGovInsigYearCon_Click" Text="เพิ่มข้อมูล" />
                            <asp:Button ID="btnUpdateGovInsigYearCon" runat="server" CssClass="btn btn-info ekknidRight" OnClick="btnUpdateGovInsigYearCon_Click" Text="อัพเดทข้อมูล" />
                            <asp:LinkButton ID="lbuClearGovInsigYearCon" runat="server" CssClass="btn btn-success" OnClick="lbuClearGovInsigYearCon_Click" Text="ล้างข้อมูล" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/list.png" />ข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover" id="dataTables-GovInsigYearCon">
                    <thead>
                        <tr>
                            <th>ลำดับที่</th>
                            <th>ระดับตำแหน่ง</th>
                            <th>เงินประจำตำแหน่ง</th>
                            <th>ชั้นเครื่องราชฯที่จะขอ</th>
                            <th>ต้องครองชั้นเครื่องราชฯ</th>
                            <th>จำนวนปี</th>
                            <th>จัดการข้อมูล</th>
                        </tr>
                    </thead>
                    <asp:Repeater ID="myRepeaterGovInsigYearCon" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex +1 %><asp:HiddenField ID="HFGovInsigYearConID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "IGIYC_ID") %>' />
                                </td>
                                <td>
                                    <asp:HiddenField ID="HFGovInsigYearConP_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "P_ID") %>' />
                                    <asp:Label ID="lbGovInsigYearConP_ID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbPositionSalary" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SALARY") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:HiddenField ID="HFGovInsigYearConINSIG_TARGET" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "INSIG_TARGET") %>' />
                                    <asp:Label ID="lbGovInsigYearConINSIG_TARGET" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "INSIG_TARGET_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:HiddenField ID="HFGovInsigYearConINSIG_USE" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "INSIG_USE") %>' />
                                    <asp:Label ID="lbGovInsigYearConINSIG_USE" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "INSIG_USE_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbInsigYear" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "INSIG_YEAR") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkEditGovInsigYearCon" Text="แก้ไข" runat="server" CssClass="btn btn-warning" OnClick="OnEditGovInsigYearCon" />
                                    <asp:LinkButton ID="lnkDeleteGovInsigYearCon" Text="ลบ" runat="server" CssClass="btn btn-danger" OnClick="OnDeleteGovInsigYearCon" OnClientClick="return confirm('คุณต้องการที่จะลบใช่หรือไม่?');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:Panel>

        <asp:Panel ID="Panel4" runat="server" CssClass="divpan" Visible="false">
            <div class="ps-header">
                <img src="Image/Small/wrench.png" /><asp:Label ID="Label4" runat="server" Text="ข้าราชการ : เงื่อนไขระยะเวลาดำรงตำแหน่ง"></asp:Label>
            </div>
            <div class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/Add.png" />เพิ่มข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>ระดับตำแหน่ง<span class="ps-lb-red" />*<br />
                            <asp:DropDownList ID="ddlInsertGovPosYearConP_ID" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>ชั้นเครื่องราชฯที่จะขอ<span class="ps-lb-red" />*<br />
                            <asp:DropDownList ID="ddlInsertGovPosYearConINSIG_TARGET" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>ต้องครองระดับตำแหน่ง<span class="ps-lb-red" />*<br />
                            <asp:DropDownList ID="ddlInsertGovPosYearConP_ID_USE" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>จำนวนปี<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertGovPosYearConPOS_YEAR" runat="server" onkeypress="return isNumberKey(event)" MaxLength="2" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>จัดการข้อมูล:<br />
                            <asp:Button ID="btnInsertGovPosYearCon" runat="server" CssClass="btn btn-primary ekknidRight" OnClick="btnInsertGovPosYearCon_Click" Text="เพิ่มข้อมูล" />
                            <asp:Button ID="btnUpdateGovPosYearCon" runat="server" CssClass="btn btn-info ekknidRight" OnClick="btnUpdateGovPosYearCon_Click" Text="อัพเดทข้อมูล" />
                            <asp:LinkButton ID="lbuClearGovPosYearCon" runat="server" CssClass="btn btn-success" OnClick="lbuClearGovPosYearCon_Click" Text="ล้างข้อมูล" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/list.png" />ข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover" id="dataTables-GovPosYearCon">
                    <thead>
                        <tr>
                            <th>ลำดับที่</th>
                            <th>ระดับตำแหน่ง</th>
                            <th>ชั้นเครื่องราชฯที่จะขอ</th>
                            <th>ต้องครองระดับตำแหน่ง</th>
                            <th>จำนวนปี</th>
                            <th>จัดการข้อมูล</th>
                        </tr>
                    </thead>
                    <asp:Repeater ID="myRepeaterGovPosYearCon" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex +1 %><asp:HiddenField ID="HFGovPosYearConID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "IGPYC_ID") %>' />
                                </td>
                                <td>
                                    <asp:HiddenField ID="HFGovPosYearConP_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "P_ID") %>' />
                                    <asp:Label ID="lbGovPosYearConP_ID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:HiddenField ID="HFGovPosYearConINSIG_TARGET" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "INSIG_TARGET") %>' />
                                    <asp:Label ID="lbGovPosYearConINSIG_TARGET" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "INSIG_TARGET_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:HiddenField ID="HFGovPosYearConP_ID_USE" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "P_ID_USE") %>' />
                                    <asp:Label ID="lbGovPosYearConP_ID_USE" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_ID_USE_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbGovPosYearConPOS_YEAR" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "POS_YEAR") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkEditGovPosYearCon" Text="แก้ไข" runat="server" CssClass="btn btn-warning" OnClick="OnEditGovPosYearCon" />
                                    <asp:LinkButton ID="lnkDeleteGovPosYearCon" Text="ลบ" runat="server" CssClass="btn btn-danger" OnClick="OnDeleteGovPosYearCon" OnClientClick="return confirm('คุณต้องการที่จะลบใช่หรือไม่?');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:Panel>

        <asp:Panel ID="Panel5" runat="server" CssClass="divpan" Visible="false">
            <div class="ps-header">
                <img src="Image/Small/wrench.png" /><asp:Label ID="Label5" runat="server" Text="ข้าราชการ : เงื่อนไขเงินเดือน"></asp:Label>
            </div>
            <div class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/Add.png" />เพิ่มข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>ระดับตำแหน่ง<span class="ps-lb-red" />*<br />
                            <asp:DropDownList ID="ddlInsertGovSalaryConP_ID" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>ชั้นเครื่องราชฯที่จะขอ<span class="ps-lb-red" />*<br />
                            <asp:DropDownList ID="ddlInsertGovSalaryConINSIG_TARGET" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>ของระดับตำแหน่ง<span class="ps-lb-red" />*<br />
                            <asp:DropDownList ID="ddlInsertGovSalaryConP_ID_USE" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>เงื่อนไขเงินต่ำกว่า-ไม่ต่ำกว่า<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertGovSalaryConIGSC_CON" runat="server" onkeypress="return isNumberKey(event)" MaxLength="1" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>จัดการข้อมูล:<br />
                            <asp:Button ID="btnInsertGovSalaryCon" runat="server" CssClass="btn btn-primary ekknidRight" OnClick="btnInsertGovSalaryCon_Click" Text="เพิ่มข้อมูล" />
                            <asp:Button ID="btnUpdateGovSalaryCon" runat="server" CssClass="btn btn-info ekknidRight" OnClick="btnUpdateGovSalaryCon_Click" Text="อัพเดทข้อมูล" />
                            <asp:LinkButton ID="lbuClearGovSalaryCon" runat="server" CssClass="btn btn-success" OnClick="lbuClearGovSalaryCon_Click" Text="ล้างข้อมูล" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/list.png" />ข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover" id="dataTables-GovSalaryCon">
                    <thead>
                        <tr>
                            <th>ลำดับที่</th>
                            <th>ระดับตำแหน่ง</th>
                            <th>ชั้นเครื่องราชฯที่จะขอ</th>
                            <th>ของระดับตำแหน่ง</th>
                            <th>เงื่อนไขเงินต่ำกว่า-ไม่ต่ำกว่า</th>
                            <th>จัดการข้อมูล</th>
                        </tr>
                    </thead>
                    <asp:Repeater ID="myRepeaterGovSalaryCon" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex +1 %><asp:HiddenField ID="HFGovSalaryConID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "IGSC_ID") %>' />
                                </td>
                                <td>
                                    <asp:HiddenField ID="HFGovSalaryConP_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "P_ID") %>' />
                                    <asp:Label ID="lbGovSalaryConP_ID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:HiddenField ID="HFGovSalaryConINSIG_TARGET" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "INSIG_TARGET") %>' />
                                    <asp:Label ID="lbGovSalaryConINSIG_TARGET" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "INSIG_TARGET_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:HiddenField ID="HFGovSalaryConP_ID_USE" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "P_ID_USE") %>' />
                                    <asp:Label ID="lbGovSalaryConP_ID_USE" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_ID_USE_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbGovSalaryConIGSC_CON" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IGSC_CON") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkEditGovSalaryCon" Text="แก้ไข" runat="server" CssClass="btn btn-warning" OnClick="OnEditGovSalaryCon" />
                                    <asp:LinkButton ID="lnkDeleteGovSalaryCon" Text="ลบ" runat="server" CssClass="btn btn-danger" OnClick="OnDeleteGovSalaryCon" OnClientClick="return confirm('คุณต้องการที่จะลบใช่หรือไม่?');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:Panel>

        <asp:Panel ID="Panel6" runat="server" CssClass="divpan" Visible="false">
            <div class="ps-header">
                <img src="Image/Small/wrench.png" /><asp:Label ID="Label6" runat="server" Text="ข้าราชการ : เงื่อนไขได้รับเงินเดือนต่อเนื่อง"></asp:Label>
            </div>
            <div class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/Add.png" />เพิ่มข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>ระดับตำแหน่ง<span class="ps-lb-red" />*<br />
                            <asp:DropDownList ID="ddlInsertGovSalaryYearConP_ID" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>ชั้นเครื่องราชฯที่จะขอ<span class="ps-lb-red" />*<br />
                            <asp:DropDownList ID="ddlInsertGovSalaryYearConINSIG_TARGET" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>ของระดับตำแหน่ง<span class="ps-lb-red" />*<br />
                            <asp:DropDownList ID="ddlInsertGovSalaryYearConP_ID_USE" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>ระยะเวลาได้รับเงินเดือนต่อเนื่อง:จำนวนปี<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="lbInsertGovSalaryYearConGET_YEAR" runat="server" onkeypress="return isNumberKey(event)" MaxLength="2" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>จัดการข้อมูล:<br />
                            <asp:Button ID="btnInsertGovSalaryYearCon" runat="server" CssClass="btn btn-primary ekknidRight" OnClick="btnInsertGovSalaryYearCon_Click" Text="เพิ่มข้อมูล" />
                            <asp:Button ID="btnUpdateGovSalaryYearCon" runat="server" CssClass="btn btn-info ekknidRight" OnClick="btnUpdateGovSalaryYearCon_Click" Text="อัพเดทข้อมูล" />
                            <asp:LinkButton ID="lbuClearGovSalaryYearCon" runat="server" CssClass="btn btn-success" OnClick="lbuClearGovSalaryYearCon_Click" Text="ล้างข้อมูล" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/list.png" />ข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover" id="dataTables-GovSalaryYearCon">
                    <thead>
                        <tr>
                            <th>ลำดับที่</th>
                            <th>ระดับตำแหน่ง</th>
                            <th>ชั้นเครื่องราชฯที่จะขอ</th>
                            <th>ของระดับตำแหน่ง</th>
                            <th>ระยะเวลาได้รับเงินเดือนต่อเนื่อง:จำนวนปี</th>
                            <th>จัดการข้อมูล</th>
                        </tr>
                    </thead>
                    <asp:Repeater ID="myRepeaterGovSalaryYearCon" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex +1 %><asp:HiddenField ID="HFGovSalaryYearConID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "IGSYC_ID") %>' />
                                </td>
                                <td>
                                    <asp:HiddenField ID="HFGovSalaryYearConP_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "P_ID") %>' />
                                    <asp:Label ID="lbGovSalaryYearConP_ID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:HiddenField ID="HFGovSalaryYearConINSIG_TARGET" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "INSIG_TARGET") %>' />
                                    <asp:Label ID="lbGovSalaryYearConINSIG_TARGET" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "INSIG_TARGET_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:HiddenField ID="HFGovSalaryYearConP_ID_USE" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "P_ID_USE") %>' />
                                    <asp:Label ID="lbGovSalaryYearConP_ID_USE" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_ID_USE_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbGovSalaryYearConGET_YEAR" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GET_YEAR") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkEditGovSalaryYearCon" Text="แก้ไข" runat="server" CssClass="btn btn-warning" OnClick="OnEditGovSalaryYearCon" />
                                    <asp:LinkButton ID="lnkDeleteGovSalaryYearCon" Text="ลบ" runat="server" CssClass="btn btn-danger" OnClick="OnDeleteGovSalaryYearCon" OnClientClick="return confirm('คุณต้องการที่จะลบใช่หรือไม่?');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:Panel>

        <asp:Panel ID="Panel7" runat="server" CssClass="divpan" Visible="false">
            <div class="ps-header">
                <img src="Image/Small/wrench.png" /><asp:Label ID="Label7" runat="server" Text="ข้าราชการ : เงื่อนไขเมื่อเกษียณ"></asp:Label>
            </div>
            <div class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/Add.png" />เพิ่มข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>ระดับตำแหน่ง<span class="ps-lb-red" />*<br />
                            <asp:DropDownList ID="ddlInsertGovAddOneP_ID" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>เงินประจำตำแหน่ง<br />
                            <asp:TextBox ID="lbInsertGovAddOnePOS_SALARY" runat="server" onkeypress="return isNumberKey(event)" MaxLength="10" CssClass="form-control input-sm" />
                        </td>
                        <td>ชั้นเครื่องราชฯที่จะขอ<span class="ps-lb-red" />*<br />
                            <asp:DropDownList ID="ddlInsertGovAddOneINSIG_TARGET" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>เลื่อนได้ถึง<span class="ps-lb-red" />*<br />
                            <asp:DropDownList ID="ddlInsertGovAddOneINSIG_MAX" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>จัดการข้อมูล:<br />
                            <asp:Button ID="btnInsertGovAddOne" runat="server" CssClass="btn btn-primary ekknidRight" OnClick="btnInsertGovAddOne_Click" Text="เพิ่มข้อมูล" />
                            <asp:Button ID="btnUpdateGovAddOne" runat="server" CssClass="btn btn-info ekknidRight" OnClick="btnUpdateGovAddOne_Click" Text="อัพเดทข้อมูล" />
                            <asp:LinkButton ID="lbuClearGovAddOne" runat="server" CssClass="btn btn-success" OnClick="lbuClearGovAddOne_Click" Text="ล้างข้อมูล" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/list.png" />ข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover" id="dataTables-GovAddOne">
                    <thead>
                        <tr>
                            <th>ลำดับที่</th>
                            <th>ระดับตำแหน่ง</th>
                            <th>เงินประจำตำแหน่ง</th>
                            <th>ชั้นเครื่องราชฯที่จะขอ</th>
                            <th>เลื่อนได้ถึง</th>
                            <th>จัดการข้อมูล</th>
                        </tr>
                    </thead>
                    <asp:Repeater ID="myRepeaterGovAddOne" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex +1 %><asp:HiddenField ID="HFGovAddOneID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "IGAO_ID") %>' />
                                </td>
                                <td>
                                    <asp:HiddenField ID="HFGovAddOneP_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "P_ID") %>' />
                                    <asp:Label ID="lbGovAddOneP_ID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbGovAddOnePOS_SALARY" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "POS_SALARY") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:HiddenField ID="HFGovAddOneINSIG_TARGET" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "INSIG_TARGET") %>' />
                                    <asp:Label ID="lbGovAddOneINSIG_TARGET" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "INSIG_TARGET_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:HiddenField ID="HFGovAddOneINSIG_MAX" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "INSIG_MAX") %>' />
                                    <asp:Label ID="lbGovAddOneINSIG_MAX" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "INSIG_MAX_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkEditGovAddOne" Text="แก้ไข" runat="server" CssClass="btn btn-warning" OnClick="OnEditGovAddOne" />
                                    <asp:LinkButton ID="lnkDeleteGovAddOne" Text="ลบ" runat="server" CssClass="btn btn-danger" OnClick="OnDeleteGovAddOne" OnClientClick="return confirm('คุณต้องการที่จะลบใช่หรือไม่?');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:Panel>

        <asp:Panel ID="Panel8" runat="server" CssClass="divpan" Visible="false">
            <div class="ps-header">
                <img src="Image/Small/wrench.png" /><asp:Label ID="Label8" runat="server" Text="ลูกจ้างประจำ : เงื่อนไขช่วงที่สามารถขอได้ถึง"></asp:Label>
            </div>
            <div class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/Add.png" />เพิ่มข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>เงินเดือนขั้นต่ำ<span class="ps-lb-red" />*<br />
                            <asp:Textbox ID="tbEmpAvailableSALARY_MIN" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>เงินเดือนขั้นสูง<span class="ps-lb-red" />*<br />
                            <asp:Textbox ID="tbEmpAvailableSALARY_MAX" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>เริ่มต้นขอ<span class="ps-lb-red" />*<br />
                            <asp:DropDownList ID="ddlEmpAvailableINSIG_MIN" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>เลื่อนได้ถึง<span class="ps-lb-red" />*<br />
                            <asp:DropDownList ID="ddlEmpAvailableINSIG_MAX" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>จัดการข้อมูล:<br />
                            <asp:Button ID="btnInsertEmpAvailable" runat="server" CssClass="btn btn-primary ekknidRight" OnClick="btnInsertEmpAvailable_Click" Text="เพิ่มข้อมูล" />
                            <asp:Button ID="btnUpdateEmpAvailable" runat="server" CssClass="btn btn-info ekknidRight" OnClick="btnUpdateEmpAvailable_Click" Text="อัพเดทข้อมูล" />
                            <asp:LinkButton ID="lbuClearEmpAvailable" runat="server" CssClass="btn btn-success" OnClick="lbuClearEmpAvailable_Click" Text="ล้างข้อมูล" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/list.png" />ข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover" id="dataTables-EmpAvailable">
                    <thead>
                        <tr>
                            <th>ลำดับที่</th>
                            <th>เงินเดือนขั้นต่ำ</th>
                            <th>เงินเดือนขั้นสูง</th>
                            <th>เริ่มต้นขอ</th>
                            <th>เลื่อนได้ถึง</th>
                            <th>จัดการข้อมูล</th>
                        </tr>
                    </thead>
                    <asp:Repeater ID="myRepeaterEmpAvailable" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex +1 %><asp:HiddenField ID="HFEmpAvailableID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "IEA_ID") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="lbEmpAvailableSALARY_MIN" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SALARY_MIN") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbEmpAvailableSALARY_MAX" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SALARY_MAX") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:HiddenField ID="HFEmpAvailableINSIG_MIN" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "INSIG_MIN") %>' />
                                    <asp:Label ID="lbEmpAvailableINSIG_MIN" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "INSIG_MIN_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:HiddenField ID="HFEmpAvailableINSIG_MAX" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "INSIG_MAX") %>' />
                                    <asp:Label ID="lbEmpAvailableINSIG_MAX" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "INSIG_MAX_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkEditEmpAvailable" Text="แก้ไข" runat="server" CssClass="btn btn-warning" OnClick="OnEditEmpAvailable" />
                                    <asp:LinkButton ID="lnkDeleteEmpAvailable" Text="ลบ" runat="server" CssClass="btn btn-danger" OnClick="OnDeleteEmpAvailable" OnClientClick="return confirm('คุณต้องการที่จะลบใช่หรือไม่?');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:Panel>

        <asp:Panel ID="Panel9" runat="server" CssClass="divpan" Visible="false">
            <div class="ps-header">
                <img src="Image/Small/wrench.png" /><asp:Label ID="Label9" runat="server" Text="ลูกจ้างประจำ : เงื่อนไขระยะเวลาครองชั้นเครื่องราชฯ"></asp:Label>
            </div>
            <div class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/Add.png" />เพิ่มข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>เงินเดือนขั้นต่ำ<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbEmpInsigYearConSALARY_MIN" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>เงินเดือนขั้นสูง<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbEmpInsigYearConSALARY_MAX" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>ชั้นเครื่องราชฯที่จะขอ<span class="ps-lb-red" />*<br />
                            <asp:DropDownList ID="ddlEmpInsigYearConINSIG_TARGET" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>ต้องครองชั้นเครื่องราชฯ<span class="ps-lb-red" />*<br />
                            <asp:DropDownList ID="ddlEmpInsigYearConINSIG_USE" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>จำนวนปี<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbEmpInsigYearConINSIG_YEAR" runat="server" onkeypress="return isNumberKey(event)" MaxLength="2" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>จัดการข้อมูล:<br />
                            <asp:Button ID="btnInsertEmpInsigYearCon" runat="server" CssClass="btn btn-primary ekknidRight" OnClick="btnInsertEmpInsigYearCon_Click" Text="เพิ่มข้อมูล" />
                            <asp:Button ID="btnUpdateEmpInsigYearCon" runat="server" CssClass="btn btn-info ekknidRight" OnClick="btnUpdateEmpInsigYearCon_Click" Text="อัพเดทข้อมูล" />
                            <asp:LinkButton ID="lbuClearEmpInsigYearCon" runat="server" CssClass="btn btn-success" OnClick="lbuClearEmpInsigYearCon_Click" Text="ล้างข้อมูล" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/list.png" />ข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover" id="dataTables-EmpInsigYearCon">
                    <thead>
                        <tr>
                            <th>ลำดับที่</th>
                            <th>เงินเดือนขั้นต่ำ</th>
                            <th>เงินเดือนขั้นสูง</th>
                            <th>ชั้นเครื่องราชฯที่จะขอ</th>
                            <th>ต้องครองชั้นเครื่องราชฯ</th>
                            <th>จำนวนปี</th>
                            <th>จัดการข้อมูล</th>
                        </tr>
                    </thead>
                    <asp:Repeater ID="myRepeaterEmpInsigYearCon" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex +1 %><asp:HiddenField ID="HFEmpInsigYearConID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "IEIYC_ID") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="lbEmpInsigYearSALARY_MIN" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SALARY_MIN") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbEmpInsigYearSALARY_MAX" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SALARY_MAX") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:HiddenField ID="HFEmpInsigYearConINSIG_TARGET" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "INSIG_TARGET") %>' />
                                    <asp:Label ID="lbEmpInsigYearConINSIG_TARGET" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "INSIG_TARGET_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:HiddenField ID="HFEmpInsigYearConINSIG_USE" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "INSIG_USE") %>' />
                                    <asp:Label ID="lbEmpInsigYearConINSIG_USE" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "INSIG_USE_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbInsigYear" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "INSIG_YEAR") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkEditEmpInsigYearCon" Text="แก้ไข" runat="server" CssClass="btn btn-warning" OnClick="OnEditEmpInsigYearCon" />
                                    <asp:LinkButton ID="lnkDeleteEmpInsigYearCon" Text="ลบ" runat="server" CssClass="btn btn-danger" OnClick="OnDeleteEmpInsigYearCon" OnClientClick="return confirm('คุณต้องการที่จะลบใช่หรือไม่?');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:Panel>

        <asp:Panel ID="Panel10" runat="server" CssClass="divpan" Visible="false">
            <div class="ps-header">
                <img src="Image/Small/wrench.png" /><asp:Label ID="Label10" runat="server" Text="พนักงานราชการ : เงื่อนไขช่วงที่สามารถขอได้ถึง"></asp:Label>
            </div>
            <div class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/Add.png" />เพิ่มข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>ระดับตำแหน่ง<span class="ps-lb-red" />*<br />
                            <asp:DropDownList ID="ddlGovEmpAvailableP_ID" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>เริ่มต้นขอ<span class="ps-lb-red" />*<br />
                            <asp:DropDownList ID="ddlGovEmpAvailableINSIG_MIN" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>เลื่อนได้ถึง<span class="ps-lb-red" />*<br />
                            <asp:DropDownList ID="ddlGovEmpAvailableINSIG_MAX" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>จัดการข้อมูล:<br />
                            <asp:Button ID="btnInsertGovEmpAvailable" runat="server" CssClass="btn btn-primary ekknidRight" OnClick="btnInsertGovEmpAvailable_Click" Text="เพิ่มข้อมูล" />
                            <asp:Button ID="btnUpdateGovEmpAvailable" runat="server" CssClass="btn btn-info ekknidRight" OnClick="btnUpdateGovEmpAvailable_Click" Text="อัพเดทข้อมูล" />
                            <asp:LinkButton ID="lbuClearGovEmpAvailable" runat="server" CssClass="btn btn-success" OnClick="lbuClearGovEmpAvailable_Click" Text="ล้างข้อมูล" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/list.png" />ข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover" id="dataTables-GovEmpAvailable">
                    <thead>
                        <tr>
                            <th>ลำดับที่</th>
                            <th>ระดับตำแหน่ง</th>
                            <th>เริ่มต้นขอ</th>
                            <th>เลื่อนได้ถึง</th>
                            <th>จัดการข้อมูล</th>
                        </tr>
                    </thead>
                    <asp:Repeater ID="myRepeaterGovEmpAvailable" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex +1 %><asp:HiddenField ID="HFGovEmpAvailableID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "IGA_ID") %>' />
                                </td>
                                <td>
                                    <asp:HiddenField ID="HFGovEmpAvailableP_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "P_ID") %>' />
                                    <asp:Label ID="GovEmpAvailableP_ID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:HiddenField ID="HFGovEmpAvailableINSIG_MIN" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "INSIG_MIN") %>' />
                                    <asp:Label ID="lbGovEmpAvailableINSIG_MIN" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "INSIG_MIN_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:HiddenField ID="HFGovEmpAvailableINSIG_MAX" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "INSIG_MAX") %>' />
                                    <asp:Label ID="lbGovEmpAvailableINSIG_MAX" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "INSIG_MAX_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkEditGovEmpAvailable" Text="แก้ไข" runat="server" CssClass="btn btn-warning" OnClick="OnEditGovEmpAvailable" />
                                    <asp:LinkButton ID="lnkDeleteGovEmpAvailable" Text="ลบ" runat="server" CssClass="btn btn-danger" OnClick="OnDeleteGovEmpAvailable" OnClientClick="return confirm('คุณต้องการที่จะลบใช่หรือไม่?');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:Panel>

        <asp:Panel ID="Panel11" runat="server" CssClass="divpan" Visible="false">
            <div class="ps-header">
                <img src="Image/Small/wrench.png" /><asp:Label ID="Label11" runat="server" Text="พนักงานราชการ : เงื่อนไขระยะเวลาครองชั้นเครื่องราชฯ"></asp:Label>
            </div>
            <div class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/Add.png" />เพิ่มข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>ระดับตำแหน่ง<span class="ps-lb-red" />*<br />
                            <asp:DropDownList ID="ddlGovEmpInsigYearConP_ID" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>ชั้นเครื่องราชฯที่จะขอ<span class="ps-lb-red" />*<br />
                            <asp:DropDownList ID="ddlGovEmpInsigYearConINSIG_TARGET" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>ต้องครองชั้นเครื่องราชฯ<span class="ps-lb-red" />*<br />
                            <asp:DropDownList ID="ddlGovEmpInsigYearConINSIG_USE" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>จำนวนปี<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbGovEmpInsigYearConINSG_YEAR" runat="server" onkeypress="return isNumberKey(event)" MaxLength="2" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>จัดการข้อมูล:<br />
                            <asp:Button ID="btnInsertGovEmpInsigYearCon" runat="server" CssClass="btn btn-primary ekknidRight" OnClick="btnInsertGovEmpInsigYearCon_Click" Text="เพิ่มข้อมูล" />
                            <asp:Button ID="btnUpdateGovEmpInsigYearCon" runat="server" CssClass="btn btn-info ekknidRight" OnClick="btnUpdateGovEmpInsigYearCon_Click" Text="อัพเดทข้อมูล" />
                            <asp:LinkButton ID="lbuClearGovEmpInsigYearCon" runat="server" CssClass="btn btn-success" OnClick="lbuClearGovEmpInsigYearCon_Click" Text="ล้างข้อมูล" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/list.png" />ข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover" id="dataTables-GovEmpInsigYearCon">
                    <thead>
                        <tr>
                            <th>ลำดับที่</th>
                            <th>ระดับตำแหน่ง</th>
                            <th>ชั้นเครื่องราชฯที่จะขอ</th>
                            <th>ต้องครองชั้นเครื่องราชฯ</th>
                            <th>จำนวนปี</th>
                            <th>จัดการข้อมูล</th>
                        </tr>
                    </thead>
                    <asp:Repeater ID="myRepeaterGovEmpInsigYearCon" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex +1 %><asp:HiddenField ID="HFGovEmpInsigYearConID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "IGYC_ID") %>' />
                                </td>
                                <td>
                                    <asp:HiddenField ID="HFGovEmpInsigYearConP_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "P_ID") %>' />
                                    <asp:Label ID="lbGovEmpInsigYearConP_ID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:HiddenField ID="HFGovEmpInsigYearConINSIG_TARGET" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "INSIG_TARGET") %>' />
                                    <asp:Label ID="lbGovEmpInsigYearConINSIG_TARGET" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "INSIG_TARGET_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:HiddenField ID="HFGovEmpInsigYearConINSIG_USE" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "INSIG_USE") %>' />
                                    <asp:Label ID="lbGovEmpInsigYearConINSIG_USE" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "INSIG_USE_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbInsigYear" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "INSIG_YEAR") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkEditGovEmpInsigYearCon" Text="แก้ไข" runat="server" CssClass="btn btn-warning" OnClick="OnEditGovEmpInsigYearCon" />
                                    <asp:LinkButton ID="lnkDeleteGovEmpInsigYearCon" Text="ลบ" runat="server" CssClass="btn btn-danger" OnClick="OnDeleteGovEmpInsigYearCon" OnClientClick="return confirm('คุณต้องการที่จะลบใช่หรือไม่?');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:Panel>

    </div>
</asp:Content>
