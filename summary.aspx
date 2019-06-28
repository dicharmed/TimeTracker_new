<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="summary.aspx.cs" Inherits="TimeTracker.summaryPerDay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="title">
        Отчет
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
            -
              <div>
                <asp:TextBox ID="TextBox1" runat="server" TextMode="Date"></asp:TextBox>
            </div>
        </div>
        <div class="input-block">
            <div class="margin-right">
                <asp:Label ID="Label1" runat="server" Text="Выберите сотрудника: "></asp:Label>
            </div>
            <div>
                <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="id_employee"></asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:time_trackConnectionString %>" SelectCommand="SELECT [id_employee], [last_name] + ' ' + [first_name] as Name FROM [employees]"></asp:SqlDataSource>
            </div>
        </div>

        <div class="button-wrapper">
            <asp:Button ID="Button1" runat="server" Text="Получить отчет" CssClass="btn btn-pad mB" OnClick="Button1_Click" />        
   
            <asp:Button ID="Button2" runat="server" Text="Сохранить в Excel" CssClass="btn btn-pad panel-btn--blocked" OnClick="Button2_Click" />        
        </div>
        
        <div>
            <asp:GridView ID="GridView1" runat="server" CssClass="" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="data" HeaderText="Дата" SortExpression="data" DataFormatString = "{0:dd/MM/yyyy}" ReadOnly="true" />
                    <asp:BoundField DataField="first_name" HeaderText="Имя" SortExpression="first_name" ReadOnly="true" />
                    <asp:BoundField DataField="last_name" HeaderText="Фамилия" SortExpression="last_name" ReadOnly="true" />
                    <asp:BoundField DataField="name_of_position" HeaderText="Должность" SortExpression="name_of_position" ReadOnly="true" />
                    <asp:BoundField DataField="name_of_faculty" HeaderText="Кафедра" SortExpression="name_of_faculty" ReadOnly="true" />
                    <asp:BoundField DataField="name_of_act" HeaderText="Вид деятельности" SortExpression="name_of_act" ReadOnly="true" />
                    <asp:BoundField DataField="name_of_theme" HeaderText="Тема" SortExpression="name_of_theme" ReadOnly="true" />
                    <asp:BoundField DataField="work_hours" HeaderText="Длительность" SortExpression="work_hours" ReadOnly="true" />
                    <asp:TemplateField HeaderText="Общее время">
                        <ItemTemplate>
                            <asp:Label ID="FirstNameLabel"
                                Text=''
                                runat="server" />

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="id_employee" HeaderText="" SortExpression="id_employee" ItemStyle-CssClass="hide" HeaderStyle-CssClass="hide" ReadOnly="true">

                        <HeaderStyle CssClass="hide"></HeaderStyle>
                        <ItemStyle CssClass="hide"></ItemStyle>
                    </asp:BoundField>

                </Columns>
                <FooterStyle BackColor="White" ForeColor="#333333" />
                <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="White" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                <SortedAscendingHeaderStyle BackColor="#487575" />
                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                <SortedDescendingHeaderStyle BackColor="#275353" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>
