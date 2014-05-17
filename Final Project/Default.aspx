<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Final_Project._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1> My Pix</h1><img src="Images/photo.jpg" />
        <p class="lead">My Pix is a photo sharing hub</p>
        <p><a href="Account/Register" class="btn btn-primary btn-large">Sign up!&raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <!--<div class ="button" style="border:solid 1px white; width:210px; height: 80px; text-align:center;">
                <div class="button_inner">
                    <h2>Get Started</h2>
                   
                </div>
                
            </div>
           -->
            <button class="btnc btnc-1 btnc-1a">&nbsp;&nbsp;Browse&nbsp;&nbsp;</button>
        </div>
        <div class="col-md-4">
          <button class="btnc btnc-1 btnc-1a">&nbsp;&nbsp;&nbsp;&nbsp;Share&nbsp;&nbsp;&nbsp;&nbsp;</button>
        </div>
        <div class="col-md-4">
            <button class="btnc btnc-1 btnc-1a">Socialize</button>
          
        </div>
    </div>

</asp:Content>
