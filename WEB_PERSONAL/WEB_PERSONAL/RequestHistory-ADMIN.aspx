﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="RequestHistory-ADMIN.aspx.cs" Inherits="WEB_PERSONAL.RequestHistory_ADMIN" %>
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
        <img src="Image/Small/clock-history.png" />ประวัติคำร้องขอแก้ไขข้อมูล
    </div>
    <div>
        <div class="ps-div-title-red">ประวัติคำร้องแก้ไขข้อมูล</div>
        <asp:Label ID="lbHistory" runat="server" Text="ไม่พบข้อมูล" CssClass="lbGV"></asp:Label>
        <asp:GridView ID="gvHistory" runat="server" CssClass="ps-table-1" Style="margin: 0 auto; margin-bottom: 20px;" AllowPaging="True" PageSize="10" OnPageIndexChanging="gvHistory_PageIndexChanging"></asp:GridView>
    </div>
</asp:Content>
