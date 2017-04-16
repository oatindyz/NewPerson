<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="INS_History.aspx.cs" Inherits="WEB_PERSONAL.INS_History" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .c1 {
            font-size: 16px;
        }
        .c1 th {
            padding: 10px 20px;
            background-color: #f0f0f0;
        }
        .c1 td {
            padding: 10px 20px;
            background-color: #f8f8f8;
        }
        .c1 tr:nth-child(odd) td {
            background-color: #ffffff;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderLeft" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="ps-header">
        <img src="Image/Small/medal.png" />ประวัติการได้รับเครื่องราชฯ
    </div>
    <div id="div1" runat="server"></div>
</asp:Content>
