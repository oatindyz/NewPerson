<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WEB_PERSONAL.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .complete_center {
            font-size: 16px;
        }


        .c3 {
            font-size: 24px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="ps-ms-page-header">หน้าหลัก</div>
        

        <div id="myCarousel" class="carousel slide" data-ride="carousel" style="width: 1024px; height:576px; margin: 0 auto;">
		  <!-- Indicators -->
		  <ol class="carousel-indicators">
			<li data-target="#myCarousel" data-slide-to="0" class="active"></li>
			<li data-target="#myCarousel" data-slide-to="1"></li>
			<li data-target="#myCarousel" data-slide-to="2"></li>
			<li data-target="#myCarousel" data-slide-to="3"></li>
		  </ol>

		  <!-- Wrapper for slides -->
		  <div class="carousel-inner" role="listbox">
			<div class="item active">
			  <img src="Image/rmutto_clear.jpg" alt="Chania" />
			</div>

			<div class="item">
			  <img src="https://placeholdit.imgix.net/~text?txtsize=85&txt=1024%C3%97576&w=1024&h=576" alt="Chania" />
			</div>

			<div class="item">
			  <img src="https://placeholdit.imgix.net/~text?txtsize=85&txt=1024%C3%97576&w=1024&h=576" alt="Flower" />
			</div>

			<div class="item">
			  <img src="https://placeholdit.imgix.net/~text?txtsize=85&txt=1024%C3%97576&w=1024&h=576" alt="Flower" />
			</div>
		  </div>

		  <!-- Left and right controls -->
		  <a class="left carousel-control" href="#myCarousel" role="button" data-slide="prev">
			<span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
			<span class="sr-only">Previous</span>
		  </a>
		  <a class="right carousel-control" href="#myCarousel" role="button" data-slide="next">
			<span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
			<span class="sr-only">Next</span>
		  </a>
		</div>
        
        <div class="ps-separator" style="margin-top: 50px;"></div>

    </div>
</asp:Content>
