<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AddPosition.aspx.cs" Inherits="WEB_PERSONAL.AddPosition" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- for Menu List -->

    <script src="bower_components/metisMenu/dist/metisMenu.min.js"></script>
    <script src="bower_components/datatables/media/js/jquery.dataTables.min.js"></script>
    <script src="bower_components/datatables-plugins/integration/bootstrap/3/dataTables.bootstrap.min.js"></script>

    <!-- for Menu List -->
    <script>
        $(document).ready(function () {
            $('#dataTables-Position').DataTable({
                responsive: true
            });
        });
    </script>

    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnInsertPosition.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>

    <script>
        $(function () {
            $("#ContentPlaceHolder1_tbInsertDatePosition").datepicker($.datepicker.regional["th"]);
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" CssClass="divpan">
            <div id="divheader1" runat="server" class="ps-header">
                <img src="Image/Small/wrench.png" /><asp:Label ID="Label1" runat="server" Text="จัดการข้อมูลระดับตำแหน่ง"></asp:Label>
                <span style="text-align:right; float:right;"><a href="listproject-admin.aspx"><img src="Image/Small/back.png" />ย้อนกลับ</a></span>
            </div>
                    
            <div id="divInsertPosition" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/Add.png" />เพิ่มข้อมูล        
                </div>
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>ระดับตำแหน่ง<span class="ps-lb-red" />*<br />
                            <asp:DropDownList ID="ddlInsertIdPosition" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>วันที่ได้รับระดับตำแหน่ง<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertDatePosition" runat="server" MaxLength="15" CssClass="form-control input-sm" required="required" tabindex="1"/>
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
                            <th>ชื่อระดับตำแหน่ง</th>
                            <th>วันที่ได้รับระดับตำแหน่ง</th>
                            <th>จัดการข้อมูล</th>
                        </tr>
                    </thead>
                    <asp:Repeater ID="myRepeaterPosition" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex +1 %><asp:HiddenField ID="HFPH_ID" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "PH_ID").ToString()%>'/></td>
                                <td>
                                    <asp:HiddenField ID="HFPositionID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "P_ID") %>'/>
                                    <asp:Label ID="lbPositionName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbPositionDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GET_DATE","{0:dd MMM yyyy}") %>'></asp:Label>
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
</asp:Content>
