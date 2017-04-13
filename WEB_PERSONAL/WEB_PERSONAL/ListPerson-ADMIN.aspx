<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ListPerson-ADMIN.aspx.cs" Inherits="WEB_PERSONAL.ListPerson_ADMIN" %>
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
            $('#dataTables-example').DataTable({
                responsive: true
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="ps-header">
        <img src="Image/Icon/manage.png" />จัดการข้อมูลบุคลากร
        <span style="text-align:right; float:right;"><a href="adduser.aspx">
        <img src="Image/Icon/add.png" />เพิ่มข้อมูลบุคลากร</a></span>
    </div>
    <div id="notification" runat="server"></div>
  
    <div id="Dp1" runat="server" class="panel panel-default">
        <div class="panel-heading">บุคลากรภายในมหาวิทยาลัย</div>
        <div class="panel-body">
            <div class="panel-body">
                <div id="divLoad" runat="server" class="dataTable_wrapper">
                    <table class="table table-striped table-bordered table-hover" id="dataTables-example">
                        <thead>
                            <tr>
                                <th>ลำดับที่</th>
                                <th>ชื่อ-สกุล</th>
                                <th>ประเภทบุคลากร</th>
                                <th>วิทยาเขต</th>
                                <th>แก้ไขข้อมูลบุคลากร</th>
                                <th>จัดการข้อมูลตำแหน่ง</th>
                                <th>จัดการข้อมูลเงินเดือน</th>
                            </tr>
                        </thead>
                        <asp:Repeater ID="myRepeater" runat="server" OnItemCommand="myRepeater_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Container.ItemIndex +1 %><asp:HiddenField ID="HF1" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "PS_ID").ToString()%>'/></td>
                                    <td><%# DataBinder.Eval(Container.DataItem, "NAME") %></td>
                                    <td><%# DataBinder.Eval(Container.DataItem, "STAFFTYPE_NAME") %></td>
                                    <td><%# DataBinder.Eval(Container.DataItem, "CAMPUS_NAME") %></td>
                                    <td><asp:LinkButton ID="lbuEdituser" CommandName="Edituser" runat="server" CommandArgument='<%#WEB_PERSONAL.MyCrypto.GetEncryptedQueryString(DataBinder.Eval(Container.DataItem, "PS_CITIZEN_ID").ToString()) %>' class="btn btn-default ekknidRight"><img src="Image/Icon/manage.png" class="icon_left"/></asp:LinkButton></td>
                                    <td><asp:LinkButton ID="lbuManagePosition" CommandName="ManagePosition" runat="server" CommandArgument='<%#WEB_PERSONAL.MyCrypto.GetEncryptedQueryString(DataBinder.Eval(Container.DataItem, "PS_CITIZEN_ID").ToString()) %>' class="btn btn-default ekknidRight"><img src="Image/Icon/manage.png" class="icon_left"/></asp:LinkButton></td>
                                    <td><asp:LinkButton ID="lbuManageSalary" CommandName="ManageSalary" runat="server" CommandArgument='<%#WEB_PERSONAL.MyCrypto.GetEncryptedQueryString(DataBinder.Eval(Container.DataItem, "PS_CITIZEN_ID").ToString()) %>' class="btn btn-default ekknidRight"><img src="Image/Icon/manage.png" class="icon_left"/></asp:LinkButton></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>