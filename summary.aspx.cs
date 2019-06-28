using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Office.Interop.Excel;

namespace TimeTracker
{
    public partial class summaryPerDay : System.Web.UI.Page
    {
        employeesDataContextDataContext db = new employeesDataContextDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            Label3.Text = "";
            Label3.CssClass = "";

            if(TextBox2.Text != "")
            {
                BindGridView();
            }

            System.Web.UI.WebControls.Label editEm = (System.Web.UI.WebControls.Label)Master.FindControl("editEm");
            System.Web.UI.WebControls.Label editAc = (System.Web.UI.WebControls.Label)Master.FindControl("editAc");
            System.Web.UI.WebControls.Label getSummary = (System.Web.UI.WebControls.Label)Master.FindControl("getSummary");

            editEm.Visible = true;
            editAc.Visible = true;
            getSummary.Visible = true;
        }

        protected void ToExcel()
        {
            Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = excelapp.Workbooks.Add();
            Microsoft.Office.Interop.Excel.Worksheet worksheet = workbook.ActiveSheet;

            for (int i = 1; i < GridView1.Rows.Count + 1; i++)
            {
                for (int j = 1; j < GridView1.Columns.Count; j++)
                {
                    worksheet.Rows[1].Columns[j] = GridView1.Columns[j - 1].HeaderText;                    
                    worksheet.Rows[i + 1].Columns[j] = GridView1.Rows[i - 1].Cells[j - 1].Text;
                }
            }

            excelapp.AlertBeforeOverwriting = false;
            workbook.SaveAs(@"C:\Users\Xiaomi\source\repos\TimeTracker\Отчет.xls");
            excelapp.Quit();
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

            Button2.CssClass = "btn btn-pad panel-btn--active";

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

        protected void Button2_Click(object sender, EventArgs e)
        {
            ToExcel();
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