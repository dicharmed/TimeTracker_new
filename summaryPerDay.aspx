<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="summaryPerDay.aspx.cs" Inherits="TimeTracker.summaryPerDay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="title">Отчет
    </div>
    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
    <div class="inputs-wrapper">
        <div class="input-block">
            <div class="margin-right">
                <asp:Label ID="Label2" runat="server" Text="Введите дату: "></asp:Label>
            </div>
            <div>
                <asp:TextBox ID="TextBox2" runat="server" TextMode="Date"></asp:TextBox>
            </div>
        </div>
        <div class="button-wrapper">
            <asp:Button ID="Button1" runat="server" Text="Получить отчет"  CssClass="btn btn-pad" OnClick="Button1_Click"/>
        </div>
        <div>
            <asp:GridView ID="GridView1" runat="server" CssClass="hide"></asp:GridView>
        </div>
    </div>
</asp:Content>
