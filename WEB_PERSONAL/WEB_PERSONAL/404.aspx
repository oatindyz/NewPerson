<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="404.aspx.cs" Inherits="WEB_PERSONAL._404" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        @font-face {
            font-family: 'Ubuntu';
            font-style: normal;
            font-weight: 700;
            src: local('Ubuntu Bold'), local('Ubuntu-Bold'), url('http://themes.googleusercontent.com/static/fonts/ubuntu/v4/0ihfXUL2emPh0ROJezvraD8E0i7KZn-EPnyo3HZu7kw.woff') format('woff');
        }

        html, body {
            padding: 0;
            margin: 0;
            height: 100%;
            width: 100%;
        }

        body {
            font-family: "Calibri",sans-serif;
            background: radial-gradient(#fff, #ccc);
            background: radial-gradient(#ffffff, #fdfdfd 16%, #fbfbfb 33%, #f8f8f8 49%, #efefef 66%, #dfdfdf 82%, #bfbfbf 100%);
        }

        #notfound {
            margin: 0 auto;
        }

            #notfound svg {
                -webkit-animation: noise 2s linear infinite;
                animation: noise 2s linear infinite;
            }

        @-webkit-keyframes noise {
            0%, 3%, 5%, 42%, 44%, 100% {
                -webkit-transform: scaleY(1);
            }

            4.3% {
                -webkit-transform: scaleY(1.7);
            }

            43% {
                -webkit-transform: scaleX(1.5);
            }
        }

        @keyframes noise {
            0%, 3%, 5%, 42%, 44%, 100% {
                transform: scaleY(1);
            }

            4.3% {
                transform: scaleY(1.7);
            }

            43% {
                transform: scaleX(1.5);
            }
        }

        p {
            text-align: center;
        }

            p.small {
                font-size: 0.8em;
                opacity: 0.5;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel8" runat="server" Height="400px">
            <div id="notfound">
                <div>
                <div class="ps-div-title-red">แจ้งเตือน</div>
                <div style="color: #808080; margin-top: 10px; text-align: center;">
                    ไม่มีหน้าดังกล่าวในระบบ โปรดตรวจสอบ URL ใหม่อีกครั้ง!
                </div>
                <div style="text-align: center; margin-top: 10px;">
                <a href="Default.aspx" class="ps-button">
                <img src="Image/Small/home3.png" class="icon_left" />กลับหน้าหลัก</a>
                </div>
            </div>
            </div>
    <script src='http://cdnjs.cloudflare.com/ajax/libs/svg.js/1.0.0-rc.8/svg.min.js'></script>
    <script src='http://strangeplanet.fr/files/misc/perlin-noise-simplex.js'></script>
    <script src="js/index.js"></script>

    </asp:Panel>
</asp:Content>
