<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="Final_Project.Upload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
    Upload a photo:
</h2>

    <asp:FileUpload ID="FileUploadPhoto" runat="server" Width="406px" BorderStyle="Solid" Height="27px" ForeColor="White" />
    <div class = "form-group">
        <br />
        Title:<asp:TextBox ID="TitleTextBox" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter a title!" ControlToValidate="TitleTextBox"></asp:RequiredFieldValidator>
    </div>
    
    <div class="form-group">
        <br />
        Enter tag(s):<asp:TextBox ID="TagTextBox" runat="server"></asp:TextBox> (If entering multiple tags use a comma in between tags)
    </div>
    <p>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" />
    </p>
    <p>
        <asp:Label ID="OutputLabel" runat="server"></asp:Label>
</p>
</asp:Content>
