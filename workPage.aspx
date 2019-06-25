<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="workPage.aspx.cs" Inherits="TimeTracker.workPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <div class="panel-block">
            <div class="panel-item">
                <asp:Button ID="Button1" runat="server" Enabled="False" OnClick="Button1_Click" Text="Начать работать" CssClass="panel-btn panel-btn--blocked" />
            </div>
            <div class="panel-item">
                <asp:Button ID="Button2" runat="server" Enabled="False" OnClick="Button2_Click" Text="Пауза" CssClass="panel-btn panel-btn--blocked" />
            </div>
            <div class="panel-item">
                <asp:Button ID="Button3" runat="server" Enabled="False" OnClick="Button3_Click" Text="Завершить работу" CssClass="panel-btn panel-btn--blocked" />
            </div>
        </div>

        <div class="inputs-wrapper">
            <div class="input-block">
                <div class="margin-right">
                    <asp:Label ID="Label2" runat="server" Text="Вид деятельности: "></asp:Label>
                </div>
                <div>
                    <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="name_of_act" DataValueField="Id_activity">
                    </asp:DropDownList>
                </div>
            </div>

            <div class="input-block">
                <div class="margin-right">
                    <asp:Label ID="Label1" runat="server" Text="Тема: "></asp:Label>
                </div>
                <div>
                    <asp:TextBox ID="TextBox1" runat="server" AutoPostBack="true" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:time_trackConnectionString %>" SelectCommand="SELECT * FROM [activities]"></asp:SqlDataSource>
    <asp:TextBox ID="StartTimeTXT" runat="server" Visible="false" ReadOnly="true"></asp:TextBox>
    <asp:TextBox ID="PauseTimeTXT" runat="server" Visible="false" ReadOnly="true"></asp:TextBox>
    <asp:TextBox ID="WorkTimeTXT" runat="server" Visible="false" ReadOnly="true"></asp:TextBox>
</asp:Content>
