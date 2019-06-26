using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TimeTracker
{
    public partial class editActivities : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
            }

            Label6.CssClass = ""; //reset classes
        }
        protected void BindGridView()
        {
            employeesDataContextDataContext db = new employeesDataContextDataContext();

            var dataf = from f in db.activities                    
                        select new { f.name_of_act, f.Id_activity };

            GridView1.DataSource = dataf;
            GridView1.DataBind();

        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Label6.Text = "";
            int CurrentRowIndex = Convert.ToInt32(e.RowIndex);
            GridViewRow gvRow = GridView1.Rows[CurrentRowIndex];
            employeesDataContextDataContext db = new employeesDataContextDataContext();
            int id = Convert.ToInt32(gvRow.Cells[1].Text);

            //Fetch particular member record from table directly with id.  
            var actDetail = (from a in db.activities where a.Id_activity == id select a).FirstOrDefault();
            db.activities.DeleteOnSubmit(actDetail);
            db.SubmitChanges();

            Label6.Text = "Запись успешно удалена!";
            Label6.CssClass = "msg msg-success";
            BindGridView();

        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            Label6.Text = "";
            BindGridView();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label6.Text = "";

            int CurrentRowIndex = Convert.ToInt32(e.RowIndex);
            GridViewRow gvRow = GridView1.Rows[CurrentRowIndex];

            employeesDataContextDataContext db = new employeesDataContextDataContext();
           
            string nameOfAct = ((gvRow.Cells[0].Controls[0] as TextBox).Text).Trim();
            string getId = ((gvRow.Cells[1].Controls[0] as TextBox).Text).Trim();

            int id = Convert.ToInt32(getId);
            activities UpdateAct = (from a in db.activities where a.Id_activity == id select a).FirstOrDefault();

            UpdateAct.name_of_act = nameOfAct;     

            db.SubmitChanges();

            Label6.Text = "Данные успешно обновлены!";
            Label6.CssClass = "msg msg-success";

        }
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            Label6.Text = "";
            BindGridView();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Label6.Text = ""; //for msg

            employeesDataContextDataContext db = new employeesDataContextDataContext();

            var get_name = (from a in db.activities where a.name_of_act == (TextBox1.Text.Trim()) select a.name_of_act).FirstOrDefault(); //get the needed log from db
            //login

            if (get_name == null)
            {
                if (TextBox1.Text == "")
                {
                    Label6.Text = "Заполните все поля!";
                    Label6.CssClass = "msg msg-error";
                }
                else
                {
                    //create
                    activities NewAct = new activities();
                    NewAct.name_of_act = TextBox1.Text.Trim();
                   
                    //Insert new record in tblmembers  
                    db.activities.InsertOnSubmit(NewAct);
                    //Update table  
                    db.SubmitChanges();
                    BindGridView();

                    Label6.Text = "Запись успешно добавлена!";
                    Label6.CssClass = "msg msg-success";
                }

            }
            else
            {
                Label6.Text = "Данный вид деятельности уже зарегистрирован в системе!";
                Label6.CssClass = "msg msg-error";          

            }

            TextBox1.Text = "";

        }
    }
}