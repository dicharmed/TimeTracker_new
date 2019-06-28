<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="editEmplolyess.aspx.cs" Inherits="TimeTracker.editEmplolyess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Label ID="Label6" runat="server"></asp:Label>

        <div class="inputs-wrapper">
            <div class="input-block">
                <div class="margin-right">
                    <asp:Label ID="Label1" runat="server" Text="Логин"></asp:Label>
                </div>
                <div>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="input-block">
                <div class="margin-right">
                    <asp:Label ID="Label2" runat="server" Text="Имя"></asp:Label>
                </div>
                <div>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </div>
            </div>

            <div class="input-block">
                <div class="margin-right">
                    <asp:Label ID="Label3" runat="server" Text="Фамилия"></asp:Label>
                </div>
                <div>
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="input-block">
                <div class="margin-right">
                    <asp:Label ID="Label4" runat="server" Text="Должность"></asp:Label>
                </div>
                <div>
                    <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource_forPosition" DataTextField="name_of_position" DataValueField="Id_positions"></asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource_forPosition" runat="server" ConnectionString="<%$ ConnectionStrings:time_trackConnectionString %>" SelectCommand="SELECT * FROM [positions]"></asp:SqlDataSource>

                </div>
            </div>

            <div class="input-block">
                <div class="margin-right">
                    <asp:Label ID="Label5" runat="server" Text="Кафедра"></asp:Label>
                </div>
                <div>
                    <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="SqlDataSource1_cafedra" DataTextField="name_of_faculty" DataValueField="Id_faculty"></asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource1_cafedra" runat="server" ConnectionString="<%$ ConnectionStrings:time_trackConnectionString %>" SelectCommand="SELECT * FROM [faculties]"></asp:SqlDataSource>
                </div>
            </div>
            <div class="button-wrapper">
                <asp:Button ID="Button1" runat="server" Text="Добавить сотрудника" OnClick="Button1_Click" CssClass="btn btn-pad" />
            </div>
            <div>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowUpdating="GridView1_RowUpdating" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal">
                    <Columns>

                        <asp:BoundField DataField="login" HeaderText="Логин" SortExpression="login" ReadOnly="true" />
                        <asp:BoundField DataField="first_name" HeaderText="Имя" SortExpression="first_name" />
                        <asp:BoundField DataField="last_name" HeaderText="Фамилия" SortExpression="last_name" />
                        <asp:BoundField DataField="name_of_position" HeaderText="Должность" SortExpression="name_of_position" />
                        <asp:BoundField DataField="name_of_faculty" HeaderText="Кафедра" SortExpression="name_of_faculty" />
                        <%--  <asp:TemplateField HeaderText = "Кафедра" ItemStyle-CssClass="" HeaderStyle-CssClass="Hide">
                    <ItemTemplate>         
                    <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="SqlDataSource1_cafedra" DataTextField="name_of_faculty" DataValueField="Id_faculty"></asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource3_cafedra" runat="server" ConnectionString="<%$ ConnectionStrings:time_trackConnectionString %>" SelectCommand="SELECT * FROM [faculties], [employees] WHERE [Id_faculty] = [faculty_id]"></asp:SqlDataSource>
                    </ItemTemplate>
                </asp:TemplateField>  --%>
                        <asp:BoundField DataField="id_employee" HeaderText="" SortExpression="id_employee" ItemStyle-CssClass="hide" HeaderStyle-CssClass="hide">

                            <HeaderStyle CssClass="hide"></HeaderStyle>

                            <ItemStyle CssClass="hide"></ItemStyle>
                        </asp:BoundField>

                        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />

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
    </div>
</asp:Content>
