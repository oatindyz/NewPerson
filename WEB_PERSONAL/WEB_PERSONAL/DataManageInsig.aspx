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
            $('#dataTables-GovAvailable,#dataTables-GovHighSalaryCon,#dataTables-GovInsigYearCon,#dataTables-GovPosYearCon').DataTable({
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
        <asp:LinkButton ID="lbuMenuGovAvailable" runat="server" OnClick="lbuMenuGovAvailable_Click"><img src="Image/Small/wrench.png" class="icon_left"/> เงื่อนไขช่วงที่สามารถขอได้ถึง</asp:LinkButton>

        <asp:LinkButton ID="lbuMenuGovHighSalaryCon" runat="server" OnClick="lbuMenuGovHighSalaryCon_Click"><img src="Image/Small/wrench.png" class="icon_left"/> เงื่อนไขใช้เงินเดือนขั้นสูง</asp:LinkButton>

        <asp:LinkButton ID="lbuMenuGovInsigYearCon" runat="server" OnClick="lbuMenuGovInsigYearCon_Click"><img src="Image/Small/wrench.png" class="icon_left"/> เงื่อนไขระยะเวลาชั้นเครื่องราชฯ</asp:LinkButton>

        <asp:LinkButton ID="lbuMenuGovPosYearCon" runat="server" OnClick="lbuMenuGovPosYearCon_Click"><img src="Image/Small/wrench.png" class="icon_left"/> เงื่อนไขระยะเวลาดำรงตำแหน่ง</asp:LinkButton>

        <asp:LinkButton ID="lbuMenuGovSalaryCon" runat="server"><img src="Image/Small/wrench.png" class="icon_left"/> เงื่อนไขเงินเดือน</asp:LinkButton>

        <asp:LinkButton ID="lbuMenuGovSalaryYearCon" runat="server"><img src="Image/Small/wrench.png" class="icon_left"/> เงื่อนไขได้รับเงินเดือนต่อเนื่อง</asp:LinkButton>

        <asp:LinkButton ID="lbuMenuGovAddOne" runat="server"><img src="Image/Small/wrench.png" class="icon_left"/> เงื่อนไขเมื่อเกษียณ</asp:LinkButton>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:Panel ID="Panel1" runat="server" CssClass="divpan" Visible="false">
            <div class="ps-header">
                <img src="Image/Small/wrench.png" /><asp:Label ID="Label1" runat="server" Text="เงื่อนไขช่วงที่สามารถขอได้ถึง"></asp:Label>
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
                        <td>เริ่มขอ<span class="ps-lb-red" />*<br />
                            <asp:DropDownList ID="ddlInsertGovAvailableInsigMin" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1" />
                        </td>
                        <td>เลื่อนถึง<span class="ps-lb-red" />*<br />
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
                            <th>เริ่มขอ</th>
                            <th>เลื่อนถึง</th>
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
                <img src="Image/Small/wrench.png" /><asp:Label ID="Label2" runat="server" Text="เงื่อนไขใช้เงินเดือนขั้นสูง"></asp:Label>
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
                <img src="Image/Small/wrench.png" /><asp:Label ID="Label3" runat="server" Text="เงื่อนไขระยะเวลาชั้นเครื่องราชฯ"></asp:Label>
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
                <img src="Image/Small/wrench.png" /><asp:Label ID="Label4" runat="server" Text="เงื่อนไขระยะเวลาดำรงตำแหน่ง"></asp:Label>
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
                        <td>ชั้นเครื่องราชฯที่จะขอ<br />
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

    </div>
</asp:Content>
