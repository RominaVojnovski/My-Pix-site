<%@ Page Title="Tags" Language="C#" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeBehind="Tags.aspx.cs" Inherits="Final_Project.Tags" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Tags</h2>
    <div class ="row">
       
        <div class ="col-md-4" id="rblistdiv">
            <asp:RadioButtonList ID="RadioButtonListTags"  runat="server" Height="21px" OnSelectedIndexChanged="RadioButtonListTags_SelectedIndexChanged" AutoPostBack="True"></asp:RadioButtonList>
        </div>
        <div class="col-md-4" id ="updatepaneldiv">
            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                
                <ContentTemplate>
                    
                    <asp:GridView ID="GridViewPhotosList" runat="server" AutoGenerateColumns="False" Width="536px">
                        <Columns>
                            <asp:TemplateField HeaderText="Title">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#"Photo.aspx?ID="+Eval("PhotoID")%>'><%# Eval("Title") %></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:BoundField DataField="DatePosted" HeaderText="Date_Posted" SortExpression="Date_Posted"/>
                            
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
                
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="RadioButtonListTags" EventName="SelectedIndexChanged" />
                </Triggers>
                
            </asp:UpdatePanel>
            
        </div>
        <div class ="col-md-4">

        </div>
    </div>
   
    <div class ="row">
        <div class ="col-md-12">
        Enter a new tag:
        <asp:TextBox ID="TextBoxTag" runat="server"></asp:TextBox><asp:Button ID="ButtonTag" runat="server" Text="Submit" OnClick="ButtonTag_Click" /><asp:Label ID="LabelMessage" runat="server"></asp:Label>
        </div>  
    </div> 
    
    <script>

        var div = document.getElementById('rblistdiv');
        $(document).ready(function () {
           

            $(document.getElementsByName('ctl00$MainContent$RadioButtonListTags')).change(function () {
                

                if ($("input[name='ctl00$MainContent$RadioButtonListTags']:checked").val()) {

                    //$("#updatepaneldiv").animate({ width: 'toggle' });
                    $("#updatepaneldiv").css('display','none');
                    $("#updatepaneldiv").animate({ left: '0px' });
                    $("#updatepaneldiv").css('display','block');
                    $("#updatepaneldiv").animate({ left: '250px' });
                    
                    //alert('One of the radio buttons is checked!');
                }
            });
        });

    </script> 
</asp:Content>
