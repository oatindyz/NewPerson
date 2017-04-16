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
            $('#dataTables-GovAvailable,#dataTables-Gender').DataTable({
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
        
            <asp:LinkButton ID="lbuMenuGovInsigYearCon" runat="server" OnClick="lbuMenuGovInsigYearCon_Click"><img src="Image/Small/wrench.png" class="icon_left"/> เงื่อนไขระยะเวลาครองชั้นเครื่องราชฯ</asp:LinkButton>
        
            <asp:LinkButton ID="lbuMenuGovPosYearCon" runat="server" OnClick="lbuMenuGovPosYearCon_Click"><img src="Image/Small/wrench.png" class="icon_left"/> เงื่อนไขระยะเวลาดำรงตำแหน่ง</asp:LinkButton>
        
            <asp:LinkButton ID="lbuMenuGovSalaryCon" runat="server" OnClick="lbuMenuGovSalaryCon_Click"><img src="Image/Small/wrench.png" class="icon_left"/> เงื่อนไขเงินเดือนต่ำกว่า-ไม่ต่ำกว่า</asp:LinkButton>
        
            <asp:LinkButton ID="lbuMenuGovSalaryYearCon" runat="server" OnClick="lbuMenuGovSalaryYearCon_Click"><img src="Image/Small/wrench.png" class="icon_left"/> เงื่อนไขได้รับเงินเดือนต่อเนื่อง</asp:LinkButton>

            <asp:LinkButton ID="lbuMenuGovAddOne" runat="server" OnClick="lbuMenuGovAddOne_Click"><img src="Image/Small/wrench.png" class="icon_left"/> เงื่อนไขเมื่อเกษียณ</asp:LinkButton>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div>
        <asp:Panel ID="Panel1" runat="server" CssClass="divpan" visible="false">
            <div id="divheader1" runat="server" class="ps-header">
                <img src="Image/Small/wrench.png" /><asp:Label ID="Label1" runat="server" Text="เงื่อนไขช่วงที่สามารถขอได้ถึง"></asp:Label></div>
            <div id="divInsertGovAvailable" runat="server" class="dataTable_wrapper">
                <div class="ps-header">
                    <img src="Image/Small/Add.png" />เพิ่มข้อมูล
                </div>
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>ระดับตำแหน่ง<span class="ps-lb-red" />*<br />
                            <asp:DropDownList ID="ddlInsertGovAvailablePositionID" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>เงินประจำตำแหน่ง<span class="ps-lb-red" />*<br />
                            <asp:TextBox ID="tbInsertGovAvailablePositionSalary" runat="server" onkeypress="return isNumberKey(event)" MaxLength="10" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>เริ่มขอ<span class="ps-lb-red" />*<br />
                            <asp:DropDownList ID="ddlInsertGovAvailableInsigMin" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>เลื่อนถึง<span class="ps-lb-red" />*<br />
                            <asp:DropDownList ID="ddlInsertGovAvailableInsigMax" runat="server" CssClass="form-control input-sm" required="required" tabindex="1"/>
                        </td>
                        <td>จัดการข้อมูล:<br />
                            <asp:Button ID="btnInsertGovAvailable" runat="server" CssClass="btn btn-primary ekknidRight" OnClick="btnInsertGovAvailable_Click" Text="เพิ่มข้อมูล" />
                            <asp:Button ID="btnUpdateGovAvailable" runat="server" CssClass="btn btn-info ekknidRight" OnClick="btnUpdateGovAvailable_Click" Text="อัพเดทข้อมูล" />
                            <asp:LinkButton ID="lbuClearGovAvailable" runat="server" CssClass="btn btn-success" OnClick="lbuClearGovAvailable_Click" Text="ล้างข้อมูล" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divLoadGovAvailable" runat="server" class="dataTable_wrapper">
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
                                <td><%# Container.ItemIndex +1 %><asp:HiddenField ID="HFGovAvailable" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "IA_ID") %>' /></td>
                                <td>
                                    <asp:Label ID="lbPositionID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_ID") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbPositionSalary" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "POS_SALARY") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:HiddenField ID="HFGovInsigAvailableMin" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "INSIG_MIN") %>'/>
                                    <asp:Label ID="lbGovInsigAvailableMin" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "INSIG_MIN_NAME") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:HiddenField ID="HFGovInsigAvailableMax" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "INSIG_MAX") %>'/>
                                    <asp:Label ID="lbGovInsigAvailableMax" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "INSIG_MAX_NAME") %>'></asp:Label>
                                </td>
                                    <asp:LinkButton ID="lnkEditGovAvailable" Text="แก้ไข" runat="server" CssClass="btn btn-warning" OnClick="OnEditGovAvailable" />
                                    <asp:LinkButton ID="lnkDeleteGovAvailable" Text="ลบ" runat="server" CssClass="btn btn-danger" OnClick="OnDeleteGovAvailable" OnClientClick="return confirm('คุณต้องการที่จะลบใช่หรือไม่?');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:Panel>

        
    </div>
</asp:Content>
