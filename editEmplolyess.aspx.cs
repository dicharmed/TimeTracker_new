using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TimeTracker
{
    public partial class editEmplolyess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
            }

            Label editEm = (Label)Master.FindControl("editEm");
            Label editAc = (Label)Master.FindControl("editAc");
            Label getSummary = (Label)Master.FindControl("getSummary");

            editEm.Visible = true;
            editAc.Visible = true;
            getSummary.Visible = true;

        }
        protected void BindGridView()
        {
            employeesDataContextDataContext db = new employeesDataContextDataContext();

            var dataf = from f in db.employees
                        join h in db.positions on f.position equals h.Id_positions
                        join g in db.faculties on f.faculty_id equals g.Id_faculty
                        select new { f.id_employee, f.first_name, f.last_name, f.login, h.name_of_position, g.name_of_faculty };

            GridView1.DataSource = dataf;
            GridView1.DataBind();            

        }


        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Label6.Text = " ";
            int CurrentRowIndex = Convert.ToInt32(e.RowIndex);
            GridViewRow gvRow = GridView1.Rows[CurrentRowIndex];
            employeesDataContextDataContext db = new employeesDataContextDataContext();
            int idEmp = Convert.ToInt32(gvRow.Cells[5].Text);

            //Fetch particular member record from table directly with id.  
            var employeeDetail = (from a in db.employees where a.id_employee == idEmp select a).FirstOrDefault();
            db.employees.DeleteOnSubmit(employeeDetail);
            db.SubmitChanges();
            Label6.Text = "Запись успешно удалена!";
            Label6.CssClass = "msg msg-success";
            BindGridView();

        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            Label6.Text = " ";
            BindGridView();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label6.Text = " ";

            int CurrentRowIndex = Convert.ToInt32(e.RowIndex);
            GridViewRow gvRow = GridView1.Rows[CurrentRowIndex];

            employeesDataContextDataContext db = new employeesDataContextDataContext();

            //string login = ((gvRow.Cells[0].Controls[0] as TextBox).Text).Trim();
            string f_name = ((gvRow.Cells[1].Controls[0] as TextBox).Text).Trim();
            string l_name = ((gvRow.Cells[2].Controls[0] as TextBox).Text).Trim();
            string position = ((gvRow.Cells[3].Controls[0] as TextBox).Text).Trim();
            string faculty = ((gvRow.Cells[4].Controls[0] as TextBox).Text).Trim();


            string getIdEmp = ((gvRow.Cells[5].Controls[0] as TextBox).Text).Trim();
            int idEmp = Convert.ToInt32(getIdEmp);
            employees UpdateEmployee = (from a in db.employees where a.id_employee == idEmp select a).FirstOrDefault();


            int idPos = (from b in db.positions where b.name_of_position == position select b.Id_positions).FirstOrDefault();
            int idFaculty = (from d in db.faculties where d.name_of_faculty == faculty select d.Id_faculty).FirstOrDefault();

            //UpdateEmployee.login = login;
            UpdateEmployee.first_name = f_name;
            UpdateEmployee.last_name = l_name;
            UpdateEmployee.position = idPos;
            UpdateEmployee.faculty_id = idFaculty;

            db.SubmitChanges();

            Label6.Text = "Данные успешно обновлены!";
            Label6.CssClass = "msg msg-success";
           
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            Label6.Text = " ";
            BindGridView();
        }
        protected void clearFields()
        {
            TextBox1.Text = null;
            TextBox2.Text = null;
            TextBox3.Text = null;
            DropDownList1.SelectedValue = null;
            DropDownList2.SelectedValue = null;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Label6.Text = null; //for msg

            employeesDataContextDataContext db = new employeesDataContextDataContext();

            var get_log = (from a in db.employees where a.login == (TextBox1.Text.Trim()) select a.login).FirstOrDefault(); //get the needed log from db
            //login

            if (get_log == null)
            {
                if (TextBox1.Text == "" || TextBox2.Text == "" || TextBox3.Text == "")
                {
                    Label6.Text = "Заполните все поля!";
                    Label6.CssClass = "msg msg-error";
                }
                else
                {
                    //create
                    employees NewEmployee = new employees();
                    NewEmployee.login = TextBox1.Text;
                    NewEmployee.pswd = "1234";
                    NewEmployee.first_name = TextBox2.Text;
                    NewEmployee.last_name = TextBox3.Text;
                    NewEmployee.role = "user";
                    NewEmployee.position = Convert.ToInt32(DropDownList1.SelectedValue);
                    NewEmployee.faculty_id = Convert.ToInt32(DropDownList2.SelectedValue);
                    //Insert new record in tblmembers  
                    db.employees.InsertOnSubmit(NewEmployee);
                    //Update table  
                    db.SubmitChanges();
                    BindGridView();

                    Label6.Text = "Пользователь успешно зарегистрирован!";                   
                    Label6.CssClass = "msg msg-success";
                }

            }
            else
            {

                string login = Convert.ToString(get_log);
                login = System.Text.RegularExpressions.Regex.Replace(login, @"\s+", ""); //remove spaces

                if (login != null)
                {
                    Label6.Text = "Пользователь с таким логином уже зарегистрирован в системе!";                   
                    Label6.CssClass = "msg msg-error";
                }

            }

            clearFields();

        }
    }
}
