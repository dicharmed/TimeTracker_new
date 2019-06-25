<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="auth.aspx.cs" Inherits="TimeTracker.auth" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="auth-block">
        <div class="">
            <asp:Label ID="Label3" runat="server"></asp:Label>
        </div>
        <div class="inputs-wrapper">
            <div class="input-block">
                <div class="margin-right">
                    <asp:Label ID="Label1" runat="server" Text="Логин: " CssClass="input-label"></asp:Label>
                </div>
                <div>
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="input-field"></asp:TextBox>
                </div>
            </div>
            <div class="input-block">
                <div class="margin-right">
                    <asp:Label ID="Label2" runat="server" Text="Пароль: " CssClass="input-label"></asp:Label>
                </div>
                <div>
                    <asp:TextBox ID="TextBox2" runat="server" CssClass="input-field" TextMode="Password"></asp:TextBox>
                </div>
            </div>
            <div class="button-wrapper">
                <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="OK" CssClass="btn btn-pad" />
            </div>

        </div>
    </div>

</asp:Content>
