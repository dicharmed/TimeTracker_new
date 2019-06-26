using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TimeTracker
{
    public partial class workPage : System.Web.UI.Page
    {
        DateTime startTime;
        DateTime pauseTime;
        DateTime finishTime;
        TimeSpan workTime;

        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox1.TextChanged += TextBox1_TextChanged;
        }

        private DateTime getTime()
        {
            DateTime time = DateTime.Now;
            return time;
        }

        public void Button1_Click(object sender, EventArgs e)//START
        {
            Button1.CssClass = "panel-btn panel-btn--blocked";
            Button2.CssClass = "panel-btn panel-btn--active";
            Button3.CssClass = "panel-btn panel-btn--blocked";

            Button2.Enabled = true; //pause unlocked
            Button1.Text = "Продолжить";
            Button1.Enabled = false; //start locked   
            Button3.Enabled = false; // finish unlocked

            DropDownList1.Enabled = false;
            TextBox1.Enabled = false;

            startTime = getTime(); 
            StartTimeTXT.Text = startTime.ToString();//START TIME
        }

        protected void Button2_Click(object sender, EventArgs e) //PAUSE
        {
            Button2.Enabled = false; //pause locked
            Button1.Enabled = true; //start/continue unlocked
            Button3.Enabled = true; //finish unlocked

            Button1.CssClass = "panel-btn panel-btn--active";
            Button2.CssClass = "panel-btn panel-btn--blocked";
            Button3.CssClass = "panel-btn panel-btn--active";

            pauseTime = getTime();
            PauseTimeTXT.Text = pauseTime.ToString();
            startTime = Convert.ToDateTime(StartTimeTXT.Text);

            try
            {
                workTime = TimeSpan.Parse(WorkTimeTXT.Text);
            }
            catch (Exception)
            {
                workTime = new TimeSpan();
            }

            //for TEST--> workTime should be 5 hrs
            //startTime = new DateTime(2015, 7, 20, 23, 30, 25);
            //pauseTime = new DateTime(2015, 7, 21, 04, 30, 25);

            workTime += pauseTime - startTime;            
            WorkTimeTXT.Text = workTime.ToString("hh':'mm':'ss");//PAUSE TIME

        }

        protected void Button3_Click(object sender, EventArgs e) //FINISH
        {
            if (StartTimeTXT.Text != "")
            {
                //--------panel
                Button2.Enabled = false; //pause locked
                Button1.Enabled = true; //start/continue locked
                Button1.Text = "Начать работать";

                Button1.CssClass = "panel-btn panel-btn--active";
                Button2.CssClass = "panel-btn panel-btn--blocked";

                startTime = Convert.ToDateTime(StartTimeTXT.Text);

                try
                {
                    finishTime = Convert.ToDateTime(PauseTimeTXT.Text);
                    workTime = TimeSpan.Parse(WorkTimeTXT.Text);

                    if (TextBox1.Text == "")
                    {
                        throw new Exception();
                    }
                }
                catch (Exception)
                {
                    finishTime = getTime();

                    workTime += finishTime - startTime;
                    WorkTimeTXT.Text = workTime.ToString("hh':'mm':'ss");//FINISH TIME

                    TextBox1.Text = "Без названия";
                }

                //TO DATABASE
                employeesDataContextDataContext db = new employeesDataContextDataContext();
                int id_empl = Convert.ToInt32(Server.UrlDecode(Request.QueryString["id"].ToString()));

                schedule NewSchedule = new schedule();

                TimeSpan startTimetoDB = TimeSpan.Parse(startTime.ToLongTimeString());
                TimeSpan finishTimetoDB = TimeSpan.Parse(finishTime.ToLongTimeString());

                NewSchedule.employes_id = id_empl;
                NewSchedule.data = DateTime.Today;
                NewSchedule.started_to_work_time = startTimetoDB;
                NewSchedule.ended_to_work_time = finishTimetoDB;
                NewSchedule.activity_id = Convert.ToInt32(DropDownList1.Text);
                NewSchedule.work_hours = workTime;
                NewSchedule.name_of_theme = TextBox1.Text;

                //Insert new record in tblmembers
                db.schedule.InsertOnSubmit(NewSchedule);
                //Update table  
                db.SubmitChanges();
                
                //reset TXT
                StartTimeTXT.Text = "";
                PauseTimeTXT.Text = "";
                WorkTimeTXT.Text = "";

                //-------lecture+activity
                DropDownList1.Enabled = true;
                DropDownList1.SelectedIndex = 0;
                TextBox1.Enabled = true;
                TextBox1.Text = "";
            }
           
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (TextBox1.Text != "")
            {
                Button1.Enabled = true;
                Button3.Enabled = true;

                Button1.CssClass = "panel-btn panel-btn--active";
                Button2.CssClass = "panel-btn panel-btn--blocked";
                Button3.CssClass = "panel-btn panel-btn--active";
            }
            else
            {
                Button1.Enabled = false;
                Button3.Enabled = false;

                Button1.CssClass = "panel-btn panel-btn--blocked";
                Button2.CssClass = "panel-btn panel-btn--blocked";
                Button3.CssClass = "panel-btn panel-btn--blocked";
            }
        }
    }
}