<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="INSG_RequestList.aspx.cs" Inherits="WEB_PERSONAL.INSG_RequestList" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- for Menu List -->
    <script src="bower_components/metisMenu/dist/metisMenu.min.js"></script>
    <script src="bower_components/datatables/media/js/jquery.dataTables.min.js"></script>
    <script src="bower_components/datatables-plugins/integration/bootstrap/3/dataTables.bootstrap.min.js"></script>
    <!-- for Menu List -->
    <script>
        $(document).ready(function () {
            $('#dataTables-Insig').DataTable({
                responsive: true
            });
        });
    </script>

    <script>
        $(function () {
            $("#ContentPlaceHolder1_tbInsertDateInsig").datepicker($.datepicker.regional["th"]);
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" CssClass="divpan">
        <div id="divheader1" runat="server" class="ps-header">
            <img src="Image/Small/medal.png" /><asp:Label ID="Label1" runat="server" Text="ข้อมูลเครื่องราชฯ"></asp:Label>
        </div>

        <div id="notification" runat="server"></div>
        <div id="divInsert" runat="server" class="dataTable_wrapper">
            <div class="ps-header">
                <img src="Image/Small/Edit.png" />อนุมัติข้อมูล        
            </div>
            <table class="table table-striped table-bordered table-hover">
                <tr>
                    <td>ชื่อ สกุล
                            <asp:TextBox ID="tbNameUser" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                    </td>
                    <td>ชั้นเครื่องราชฯที่ขอ
                            <asp:TextBox ID="tbInsigReq" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                    </td>
                    <td>วันที่ขอ
                            <asp:TextBox ID="tbInsigDateReq" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                    </td>
                    <td>วันที่ได้รับชั้นเครื่องราชฯ<span class="ps-lb-red" />*<br />
                        <asp:TextBox ID="tbInsertDateInsig" runat="server" MaxLength="15" CssClass="form-control input-sm" required="required" TabIndex="1" />
                    </td>
                    <td>สถานะ
                        <asp:DropDownList ID="ddlStatusID" runat="server" CssClass="form-control input-sm" required="required" TabIndex="1" ></asp:DropDownList>
                    </td>
                    <td>จัดการข้อมูล:<br />
                        <asp:Button ID="btnUpdateInsig" runat="server" CssClass="btn btn-info ekknidRight" OnClick="btnUpdateInsig_Click" Text="อัพเดทข้อมูล" />
                        <asp:LinkButton ID="lbuClearInsig" runat="server" CssClass="btn btn-success" OnClick="lbuClearInsig_Click" Text="ล้างข้อมูล" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="divLoadInsig" runat="server" class="dataTable_wrapper">
            <div class="ps-header">
                <img src="Image/Small/list.png" />ข้อมูล
            </div>
            <table class="table table-striped table-bordered table-hover" id="dataTables-Insig">
                <thead>
                    <tr>
                        <th>ลำดับที่</th>
                        <th>ชื่อ สกุล</th>
                        <th>ชั้นเครื่องราชฯที่ขอ</th>
                        <th>วันที่ขอ</th>
                        <th>วันที่ได้รับชั้นเครื่องราชฯ</th>
                        <th>สถานะ</th>
                        <th>ดึงข้อมูล</th>
                    </tr>
                </thead>
                <asp:Repeater ID="myRepeaterInsig" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Container.ItemIndex +1 %><asp:HiddenField ID="HFIP_ID" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "IP_ID").ToString()%>' />
                            </td>
                            <td>
                                <asp:Label ID="lbInsigName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PS_NAME") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbInsigReq" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "INSIG_NAME") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbInsigReqDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "REQ_DATE","{0:dd MMM yyyy}") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbInsigGetDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GET_DATE","{0:dd MMM yyyy}") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:HiddenField ID="HFSTATUS_ID" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "IP_STATUS_ID").ToString()%>' />
                                <asp:Label ID="lbStatusName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "StatusName") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:LinkButton ID="lnkEditInsig" Text="แก้ไข" runat="server" CssClass="btn btn-warning" OnClick="OnEditInsig" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>


    </asp:Panel>
</asp:Content>
