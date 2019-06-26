using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TimeTracker
{
    public partial class summaryPerDay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label3.Text = "";
            Label3.CssClass = "";
          
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            BindGridView();
        }

        protected void BindGridView()
        {
            employeesDataContextDataContext db = new employeesDataContextDataContext();
            try
            {
                DateTime dateSummary = Convert.ToDateTime(TextBox2.Text).Date;
                var dataf = (from sH in db.schedule
                         join emP in db.employees on sH.employes_id equals emP.id_employee
                         join poS in db.positions on emP.position equals poS.Id_positions
                         join faC in db.faculties on emP.faculty_id equals faC.Id_faculty
                         where sH.data == dateSummary
                         select new { emP.id_employee, emP.first_name, emP.last_name, poS.name_of_position, faC.name_of_faculty, sH.data, sH.work_hours });
                GridView1.DataSource = dataf;
                GridView1.DataBind();

            }catch(Exception)
            {
                Label3.Text = "Укажите дату!";
                Label3.CssClass = "msg msg-error";
            }

            getSUM(); //to get SUM of work hours

        }
        TimeSpan getSUM()//to get SUM of work hours
        {
            List<TimeSpan> objList = new List<TimeSpan>();

            for (var i = 0; i < GridView1.Rows.Count; i++)
            {
                objList.Add(TimeSpan.Parse(GridView1.Rows[i].Cells[6].Text));
            }

            var totalTime = TimeSpan.Zero;
            foreach (TimeSpan currentValue in objList)
            {
                totalTime = totalTime + currentValue;
            }
            return totalTime;
        }
    }
}