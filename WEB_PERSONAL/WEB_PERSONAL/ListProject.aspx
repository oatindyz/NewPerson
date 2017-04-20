<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ListProject.aspx.cs" Inherits="WEB_PERSONAL.ListProject" %>
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
        <img src="Image/Icon/manage.png" />จัดการข้อมูลการพัฒนาบุคลากร
        <span style="text-align:right; float:right;"><a href="addproject.aspx">
        <img src="Image/Small/add.png" />เพิ่มข้อมูลการพัฒนาบุคลากร</a></span>
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
                                <th>ประเภทโครงการ</th>
                                <th>ชื่อโครงการ</th>
                                <th>สถานที่จัดโครงการ</th>
                                <th><img src="Image/Small/document-edit.png" class="icon_left" />จัดการข้อมูล</th>
                            </tr>
                        </thead>
                        <asp:Repeater ID="myRepeater" runat="server" OnItemCommand="myRepeater_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Container.ItemIndex + 1 %><asp:HiddenField ID="HF1" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "PRO_ID").ToString()%>'/></td>
                                    <td><%# DataBinder.Eval(Container.DataItem, "NAME") %></td>
                                    <td><%# DataBinder.Eval(Container.DataItem, "CATEGORY_ID") %></td>
                                    <td><%# DataBinder.Eval(Container.DataItem, "PROJECT_NAME") %></td>
                                    <td><%# DataBinder.Eval(Container.DataItem, "ADDRESS_PROJECT") %></td>
                                    <td>
                                        <a><asp:LinkButton ID="lbuPreview" CommandName="Preview" runat="server" CommandArgument='<%#WEB_PERSONAL.MyCrypto.GetEncryptedQueryString(DataBinder.Eval(Container.DataItem, "PRO_ID").ToString()) %>' class="btn btn-info ekknidRight">ดู</asp:LinkButton></a>
                                        <a><asp:LinkButton ID="lbuEdit" CommandName="Edit" runat="server" CommandArgument='<%#WEB_PERSONAL.MyCrypto.GetEncryptedQueryString(DataBinder.Eval(Container.DataItem, "PRO_ID").ToString()) %>' class="btn btn-warning ekknidRight">แก้ไข</asp:LinkButton></a>
                                        <a><asp:LinkButton ID="lbuDelete" CommandName="Delete" OnClientClick="javascript:if(!confirm('คุณต้องการที่จะลบใช่หรือไม่'))return false;" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "PRO_ID").ToString()%>' runat="server" class="btn btn-danger ekknidRight">ลบ</asp:LinkButton></a>
                                        <a><asp:LinkButton ID="lbuReport" CommandName="Report" CommandArgument='<%#WEB_PERSONAL.MyCrypto.GetEncryptedQueryString(DataBinder.Eval(Container.DataItem, "PRO_ID").ToString())%>' runat="server" class="btn btn-primary">ออกรายงาน</asp:LinkButton></a>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>