<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Photo.aspx.cs" Inherits="Final_Project.Photo1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Photo Details</h2>

<div>
<asp:Label ID="Label1" runat="server"></asp:Label>
</div>
    <div class="row">
        <div class = "col-md-2">

       </div>
        <div class="col-md-6">
            <asp:Image ID="MainImage" ImageAlign="Middle" runat="server" />
            <br />
            <br />
            <asp:Label ID="ImageTitle" runat="server"></asp:Label>
        </div>
        <div class ="col-md-4">
            Posted by <asp:Label ID="PosterLabel" runat="server"></asp:Label>
            <br />
            Posted on <asp:Label ID="DateLabel" runat="server"></asp:Label>
            <br />
            Tags: <asp:Label ID="TagsLabel" runat="server"></asp:Label>
        </div>
    </div> 
    <div class ="row">
       <div class = "col-md-4">

       </div>        
       <div class = "col-md-4">
       <asp:GridView ID="GridViewComments" runat="server" GridLines="None" AutoGenerateColumns="False" Width="574px">
         <Columns>
             <asp:TemplateField HeaderText="Comments" SortExpression ="Comments">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLinkComment" runat="server" NavigateUrl='<%#"Comment.aspx?ID="+Eval("CommentID")%>'><%#Eval("CommentSubtext")%></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Responses" SortExpression="Responses">
                <ItemTemplate>
                    <asp:Label ID="LabelResponseCount" runat="server" Text = '<%#Eval("ResponsesCount")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
         </Columns>
       </asp:GridView>

       </div>
       <div class = "col-md-4">

       </div>
                        
   </div>
    <div class ="row">
        <div class="col-md-4">

        </div>
        <div class="col-md-4">
        Title:<br />
        <asp:TextBox ID="CommentTitle" runat="server" Width="512px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter a title!" ControlToValidate="CommentTitle"></asp:RequiredFieldValidator>
            <br />   
        Comment:<br />
        <asp:TextBox ID="CommentText"  SkinId="ResponseMultiTextSkin" runat="server" TextMode="MultiLine" Width="405px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter something!" ControlToValidate="CommentText"></asp:RequiredFieldValidator>
            <br />
            <br />
        <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" />
            <br />
        <asp:Label ID="OutputLabel" runat="server"></asp:Label>
        </div>
        <div class ="col-md-4">

        </div>
    </div>
  
</asp:Content>

