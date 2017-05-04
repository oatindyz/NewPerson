<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="INS_History.aspx.cs" Inherits="WEB_PERSONAL.INS_History" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .c2 {
        }

        .c1 {
            display: inline-block;
            text-decoration: none;
            padding: 3px 20px;
            background-color: #ffffff;
            color: #000000;
            border: 1px solid #c0c0c0;
        }

            .c1:hover {
                background-color: #f0f0f0;
            }

        .sec {
            background-color: #ffffff;
            margin-bottom: 1px;
            border-top: 1px solid rgb(235,235,235);
        }

        .sec2 {
            padding: 20px;
            padding-top: 0px;
        }

        .lbGV {
            display: block;
            margin-bottom: 5px;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="ps-header">
        <img src="Image/Small/medal.png" />ประวัติเครื่องราชฯ
    </div>
    <div>

        <div class="ps-div-title-red">รายการที่เสร็จสิ้น</div>
        <asp:Label ID="lbFinish" runat="server" Text="ไม่มีข้อมูล" CssClass="lbGV"></asp:Label>
        <asp:GridView ID="gvFinish" runat="server" CssClass="ps-table-1" Style="margin: 0 auto; margin-bottom: 20px;" AllowPaging="True" PageSize="10" OnPageIndexChanging="gvFinish_PageIndexChanging"></asp:GridView>

        <div class="ps-div-title-red">รายการที่อยู่ระหว่างการดำเนินการ</div>
        <asp:Label ID="lbProgressing" runat="server" Text="ไม่มีข้อมูล" CssClass="lbGV"></asp:Label>
        <asp:GridView ID="gvProgressing" runat="server" CssClass="ps-table-1" Style="margin: 0 auto; margin-bottom: 20px;" AllowPaging="True" PageSize="10" OnPageIndexChanging="gvProgressing_PageIndexChanging"></asp:GridView>

        <div class="ps-div-title-red">ประวัติการขอเครื่องราชฯ</div>
        <asp:Label ID="lbHistory" runat="server" Text="ไม่พบข้อมูล" CssClass="lbGV"></asp:Label>
        <asp:GridView ID="gvHistory" runat="server" CssClass="ps-table-1" Style="margin: 0 auto; margin-bottom: 20px;" AllowPaging="True" PageSize="10" OnPageIndexChanging="gvHistory_PageIndexChanging"></asp:GridView>

    </div>
</asp:Content>
