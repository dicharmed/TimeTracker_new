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
            //string time = DateTime.Now.ToLongTimeString();
            //TimeSpan time = DateTime.Now.TimeOfDay;
            DateTime time = DateTime.Now;
            return time;
        }

        public void Button1_Click(object sender, EventArgs e)//START
        {
            Button2.Enabled = true; //pause unlocked
            Button1.Text = "Продолжить";
            Button1.Enabled = false; //start locked

            Button1.CssClass = "panel-btn panel-btn--blocked";
            Button2.CssClass = "panel-btn panel-btn--active";

            DropDownList1.Enabled = false;
            TextBox1.Enabled = false;

            startTime = getTime(); 
            StartTimeTXT.Text = startTime.ToString();//START TIME

        }

        protected void Button2_Click(object sender, EventArgs e) //PAUSE
        {
            Button2.Enabled = false; //pause locked
            Button1.Enabled = true; //start/continue locked

            Button1.CssClass = "panel-btn panel-btn--active";
            Button2.CssClass = "panel-btn panel-btn--blocked";

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

            workTime += pauseTime - startTime;
            WorkTimeTXT.Text = workTime.ToString();//PAUSE TIME

        }

        protected void Button3_Click(object sender, EventArgs e) //FINISH
        { 
            try
            {
                finishTime = Convert.ToDateTime(PauseTimeTXT.Text);
                workTime = TimeSpan.Parse(WorkTimeTXT.Text);
            }
            catch(Exception)
            {
                finishTime = getTime();
                startTime = Convert.ToDateTime(StartTimeTXT.Text);
                workTime += finishTime - startTime;
                WorkTimeTXT.Text = workTime.ToString();//FINISH TIME
            }

            //-------lecture+activity
            DropDownList1.Enabled = true;
            DropDownList1.SelectedIndex = 0;
            TextBox1.Enabled = true;
            TextBox1.Text = "";

            //--------panel
            Button2.Enabled = false; //pause locked
            Button1.Enabled = true; //start/continue locked
            Button1.Text = "Начать работать";

            Button1.CssClass = "panel-btn panel-btn--active";
            Button2.CssClass = "panel-btn panel-btn--blocked";

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (TextBox1.Text != "")
            {
                Button1.Enabled = true;
                Button3.Enabled = true;
              
                Button1.CssClass = "panel-btn panel-btn--active";
                Button2.CssClass = "panel-btn panel-btn--active";
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