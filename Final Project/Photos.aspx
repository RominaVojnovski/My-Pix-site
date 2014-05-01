<%@ Page Title="Photos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Photos.aspx.cs" Inherits="Final_Project.Photos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<h2>Photos</h2>

    <asp:GridView ID="PhotosGridView" runat="server" Width="510px" AutoGenerateColumns="False" Height="300px">
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

    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>

</asp:Content>
