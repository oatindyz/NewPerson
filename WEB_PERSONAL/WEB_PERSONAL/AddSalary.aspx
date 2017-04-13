<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AddSalary.aspx.cs" Inherits="WEB_PERSONAL.AddSalary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- for Menu List -->
    <script src="bower_components/metisMenu/dist/metisMenu.min.js"></script>
    <script src="bower_components/datatables/media/js/jquery.dataTables.min.js"></script>
    <script src="bower_components/datatables-plugins/integration/bootstrap/3/dataTables.bootstrap.min.js"></script>
    <!-- for Menu List -->

    <script>
        $(document).ready(function () {
            $('#dataTables-Salary').DataTable({
                responsive: true
            });
        });
    </script>

    <script>
        $(function () {
            $("#ContentPlaceHolder1_tbInsertDateSalary").datepicker($.datepicker.regional["th"]);
        });
    </script>

    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnInsertSalary.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" CssClass="divpan">
        <div id="divheader1" runat="server" class="ps-header">
            <img src="Image/Small/wrench.png" /><asp:Label ID="Label1" runat="server" Text="จัดการข้อมูลเงินเดือน"></asp:Label>
            <span style="text-align: right; float: right;"><a href="listPerson-admin.aspx">
                <img src="Image/Small/back.png" />ย้อนกลับ</a></span>
        </div>

        <div id="notification" runat="server"></div>

        <div id="divInsertSalary" runat="server" class="dataTable_wrapper">
            <div class="ps-header">
                <img src="Image/Small/Add.png" />เพิ่มข้อมูล        
            </div>
            <table class="table table-striped table-bordered table-hover" style="width:400px">
                <tr>
                    <td>เงินเดือน<span class="ps-lb-red" />*
                        <asp:TextBox ID="tbSalary" runat="server" MaxLength="10" onkeypress="return isNumberKey(event)" CssClass="form-control input-sm" required="required" TabIndex="1" />
                    </td>
                </tr>
                <tr>
                    <td>เงินประจำตำแหน่ง
                        <asp:TextBox ID="tbPositionSalary" runat="server" MaxLength="10" onkeypress="return isNumberKey(event)" CssClass="form-control input-sm"/>
                    </td>
                </tr>
                <tr>
                    <td>ผลการประเมิณรอบงานที่1
                        <asp:TextBox ID="tbResult1" runat="server" MaxLength="10" CssClass="form-control input-sm"/>
                    </td>
                </tr>
                <tr>
                    <td>ร้อยละการเลื่อนขั้นเงินเดือนรอบที่1
                        <asp:TextBox ID="tbPercentSalary1" runat="server" MaxLength="255" CssClass="form-control input-sm"/>
                    </td>
                </tr>
                <tr>
                    <td>ผลการประเมิณรอบงานที่2
                        <asp:TextBox ID="tbResult2" runat="server" MaxLength="10" CssClass="form-control input-sm"/>
                    </td>
                </tr>
                <tr>
                    <td>ร้อยละการเลื่อนขั้นเงินเดือนรอบที่2
                        <asp:TextBox ID="tbPercentSalary2" runat="server" MaxLength="255" CssClass="form-control input-sm"/>
                    </td>
                </tr>
                <tr>
                    <td>วันที่<span class="ps-lb-red" />*
                        <asp:TextBox ID="tbInsertDateSalary" runat="server" MaxLength="15" CssClass="form-control input-sm"  required="required" TabIndex="1" />
                    </td>
                </tr>
                <tr>
                    <td>จัดการข้อมูล:<br />
                        <asp:Button ID="btnInsertSalary" runat="server" CssClass="btn btn-primary ekknidRight" OnClick="btnInsertSalary_Click" Text="เพิ่มข้อมูล" />
                        <asp:Button ID="btnUpdateSalary" runat="server" CssClass="btn btn-info ekknidRight" OnClick="btnUpdateSalary_Click" Text="อัพเดทข้อมูล" />
                        <asp:LinkButton ID="lbuClearSalary" runat="server" CssClass="btn btn-success" OnClick="lbuClearSalary_Click" Text="ล้างข้อมูล" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="divLoadSalary" runat="server" class="dataTable_wrapper">
            <div class="ps-header">
                <img src="Image/Small/list.png" />ข้อมูล
            </div>
            <table class="table table-striped table-bordered table-hover" id="dataTables-Salary">
                <thead>
                    <tr>
                        <th>ลำดับที่</th>
                        <th>เงินเดือน</th>
                        <th>เงินประจำตำแหน่ง</th>
                        <th>ผลการประเมิณรอบงานที่1</th>
                        <th>ร้อยละการเลื่อนขั้นเงินเดือนรอบที่1</th>
                        <th>ผลการประเมิณรอบงานที่2</th>
                        <th>ร้อยละการเลื่อนขั้นเงินเดือนรอบที่2</th>
                        <th>วันที่</th>
                        <th>จัดการข้อมูล</th>
                    </tr>
                </thead>
                <asp:Repeater ID="myRepeaterSalary" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Container.ItemIndex +1 %><asp:HiddenField ID="HFSalary_ID" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "SALARY_ID").ToString()%>' />
                            </td>
                            <td>
                                <asp:Label ID="lbSalary" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SALARY") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbPositionSalary" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "POSITION_SALARY") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbResult1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RESULT1") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbPercentSalary1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PERCENT_SALARY1") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbResult2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RESULT2") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbPercentSalary2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PERCENT_SALARY2") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbDoDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DO_DATE","{0:dd MMM yyyy}") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:LinkButton ID="lnkEditSalary" Text="แก้ไข" runat="server" CssClass="btn btn-warning" OnClick="OnEditSalary" />
                                <asp:LinkButton ID="lnkDeleteSalary" Text="ลบ" runat="server" CssClass="btn btn-danger" OnClick="OnDeleteSalary" OnClientClick="return confirm('คุณต้องการที่จะลบใช่หรือไม่?');" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </asp:Panel>
</asp:Content>
