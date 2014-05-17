<%@ Page Title="Photos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Photos.aspx.cs" Inherits="Final_Project.Photos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<link rel="stylesheet" type="text/css" href="Content/flipping_gallery.css" />
<h2>Photos</h2>
    <div class="row">
        <div class="col-md-12">
            View:&nbsp;
            <a href="Photos.aspx?view=gallery">Gallery</a>
            &nbsp;
            <a href="Photos.aspx?view=list">List</a>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-md-12">
            
            <div id ="gridDiv">
                <asp:GridView ID="PhotosGridView" runat="server" GridLines="None" Width="719px" AutoGenerateColumns="False" Height="300px">
                    <Columns>
                        <asp:TemplateField HeaderText="Thumb">
                            <ItemTemplate>
                                <asp:Image ID="ThumbImage" runat="server" ImageUrl='<%#"~/Logic/ThumbNailImage.ashx?img="+Eval("Photo")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Title" SortExpression="Title">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#"Photo.aspx?ID="+Eval("PhotoID")%>'><%# Eval("Title") %></asp:HyperLink>     
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField= "Poster" HeaderText="Poster" SortExpression="Poster"/>
                        <asp:BoundField DataField="Tags" HeaderText="Tags" SortExpression="Tags" />
                    </Columns>
                </asp:GridView>    
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">

        </div>
        <div class="col-md-8">
            <div id="galleryDiv">
                <br />
                <br />
                <br />
                <br />
                <asp:ListView ID="ListViewGal" runat="server">
                    <ItemTemplate>
                        <a href="#"><img src=<%#Eval("FullPhotoPath")%>></a> 
                    </ItemTemplate>
                </asp:ListView>
            </div>
        </div>
        <div class="col-md-2">

        </div>
    </div>
    <div id="blank_space">
        <div class="row">
            <div class="col-md-12">
                <div id="emtpydiv" style="height:400px;">
                
                </div>
            </div>
        </div>
    </div>
 
    <script>

        function getQuerystring(key) {
            var query = window.location.search.substring(1);
            var vars = query.split("&");
            for(var i =0;i< vars.length; i++){
                var pair = vars[i].split("=");
                if(pair[0] == key){
                    return pair[1];
                }

            }
        }
        
       




        $(window).ready(function () {
            if(getQuerystring("view")=="gallery"){
                $("#emptydiv").show();
                $("#galleryDiv").flipping_gallery({
                    enableScroll: false
                });
                
            }
    
        });
  
    </script>
</asp:Content>
