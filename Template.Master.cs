using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TimeTracker
{
    public partial class Template : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int getId;
            try {
                getId = Convert.ToInt32(Server.UrlDecode(Request.QueryString["id"].ToString()));

                employeesDataContextDataContext db = new employeesDataContextDataContext();

                var employee = (from a in db.employees where a.id_employee == getId select a).FirstOrDefault(); //get the needed log from db

                //fio
                string l_name = Convert.ToString(employee.last_name);
                l_name = System.Text.RegularExpressions.Regex.Replace(l_name, @"\s+", ""); //remove spaces

                string f_name = Convert.ToString(employee.first_name);
                f_name = System.Text.RegularExpressions.Regex.Replace(f_name, @"\s+", ""); //remove spaces

                //post
                int post = Convert.ToInt32(employee.position);
                var positions = (from a in db.positions where a.Id_positions == post select a).FirstOrDefault();
                string pos_of_emp = Convert.ToString(positions.name_of_position);

                Label1.Text = l_name + " " + f_name + ", " + pos_of_emp;

                Label2.Text = DateTime.Today.ToShortDateString();

            } catch (Exception)
            {
                getId = 0;
                Label1.Text = "";
                Label2.Text = "";
            }
            

          

        }

    }
}