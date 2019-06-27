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
        employeesDataContextDataContext db = new employeesDataContextDataContext();

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
 
            if(TextBox1.Text == "")
            {
                TextBox1.Text = TextBox2.Text;
            }

            DateTime dateSummary = Convert.ToDateTime(TextBox2.Text).Date;
            DateTime dateSummaryTo = Convert.ToDateTime(TextBox1.Text).Date;

            int getIdEmpl = Convert.ToInt32(DropDownList1.SelectedValue);
            var dataf = (from sH in db.schedule
                         join emP in db.employees on sH.employes_id equals emP.id_employee
                         join poS in db.positions on emP.position equals poS.Id_positions
                         join faC in db.faculties on emP.faculty_id equals faC.Id_faculty
                         join AcT in db.activities on sH.activity_id equals AcT.Id_activity
                         where sH.data >= dateSummary && sH.data <= dateSummaryTo && sH.employes_id == getIdEmpl
                         orderby sH.data
                         select new
                         {                             
                             emP.first_name,
                             emP.last_name,
                             poS.name_of_position,
                             faC.name_of_faculty,
                             sH.data,
                             sH.work_hours,
                             AcT.name_of_act,
                             emP.id_employee,
                             sH.name_of_theme
                         });


            GridView1.DataSource = dataf;
            GridView1.DataBind();



            TimeSpan s = getSUM();
            GridView1.Rows[0].Cells[8].Text = s.ToString();


            //TableCell b = new TableCell();
            //b.Text = s.ToString();
            //GridView1.Rows[0].Cells.Add(b);

        }


        TimeSpan getSUM()
        {
            TimeSpan summ = new TimeSpan();
            try
            {
                if (TextBox1.Text == "")
                {
                    TextBox1.Text = TextBox2.Text;
                }

                DateTime dateSummary = Convert.ToDateTime(TextBox2.Text).Date;
                DateTime dateSummaryTo = Convert.ToDateTime(TextBox1.Text).Date;

                int getIdEmpl = Convert.ToInt32(DropDownList1.SelectedValue);


                var dataf = (from sH in db.schedule.AsEnumerable()
                             join emP in db.employees on sH.employes_id equals emP.id_employee
                             join poS in db.positions on emP.position equals poS.Id_positions
                             join faC in db.faculties on emP.faculty_id equals faC.Id_faculty
                             where sH.data >= dateSummary && sH.data <= dateSummaryTo && sH.employes_id == getIdEmpl
                             group sH by sH.employes_id into g
                             select new
                             {
                                 Category = g.Key,
                                 Sum = g.Aggregate(new TimeSpan(), (sum, nextDate) => (sum.Add(nextDate.work_hours.Value)))
                             });

                
                foreach (var s in dataf)
                {
                    summ = s.Sum;
                }


            }
            catch (Exception)
            {
                Label3.Text = "Укажите дату!";
                Label3.CssClass = "msg msg-error";
            }

            return summ;
        }











        //TimeSpan getSUM()//to get SUM of work hours
        //{
        //    List<TimeSpan> objList = new List<TimeSpan>();

        //    for (var i = 0; i < GridView1.Rows.Count; i++)
        //    {
        //        objList.Add(TimeSpan.Parse(GridView1.Rows[i].Cells[6].Text));
        //    }

        //    var totalTime = TimeSpan.Zero;
        //    foreach (TimeSpan currentValue in objList)
        //    {
        //        totalTime = totalTime + currentValue;
        //    }
        //    return totalTime;
        //}
    }
}