<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Comment.aspx.cs" Inherits="Final_Project.Comment1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<h2>Comment</h2>
    <div class = "row">
        <div class ="col-md-4">
            <asp:Image ID="MainImage" runat="server" />
        </div>
        <div class ="col-md-2">

        </div>
        <div class ="col-md-4">
            <h3>
            <asp:Label ID="LabelCommentTitle" runat="server" Text="Label"></asp:Label>
            </h3>
            <br />
            <asp:Label ID="LabelCommentBody" runat="server" Text="Label"></asp:Label>
        </div>
        <div class ="col-md-2">

        </div>

    </div>
    <br />
    <h4>Responses</h4>
    <hr />
    <asp:PlaceHolder ID="PlaceHolderDynamicDiv" runat="server"></asp:PlaceHolder>

    <div class = "row">
        <div class ="col-md-3">

        </div>
        <div class ="col-md-6">
            Response:
            <br />
            <asp:TextBox ID="TextBoxResponse" SkinId="ResponseMultiTextSkin" runat="server" Height="129px"  Width="416px" TextMode="MultiLine"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" Text="Reply" OnClick="Button1_Click" />
            <asp:Label ID="OutputLabel" runat="server"></asp:Label>
        </div>
        <div class ="col-md-3">

        </div>
    </div>
    

</asp:Content>
